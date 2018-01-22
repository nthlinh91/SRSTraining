/********************************************************************
 * ユーザ名       : SRS
 * システム名     : トレーニング
 * サブシステム名 : 共通
 * 処理名称       : 共通JavaScript関数
 * 機能           : 共通JavaScript関数 検索ダイアログ関連
 * 作成者         : SRSTaro
 * 改版履歴       : 001, 2018-01-11, SRSTaro, 新規作成
 *******************************************************************/

//// モーダルダイアログを表示する (旧形式、実ウィンドウ表示する)
////   Edge, Chrome, 将来のマルチプロセスFirefoxなどでは動かないので廃止
//function commonModalDialogLegacy(nextpage, menutitle, params, width, height, callback) {

//    // rootPath (サーバ上での配置場所)は、ViewのShared/_Layout.vbhtmlで出力されることを想定
//    var url = nextpage;
//    if (url.indexOf("://") < 0) {
//        url = rootPath + url;
//    }

//    var encodetitle = encodeURI(menutitle);
//    var returnObj = window.showModalDialog(url,
//                                           params,
//                                           "dialogWidth:" + width +
//                                           ";dialogHeight:" + height +
//                                           ";center:yes;status:yes;help:no;resizable:yes");
//    // モーダル表示しない場合の参考コード
//    //var returnObj = window.open(url, menutitle, "width=" + width + ",height=" + height + ",alwaysRaised=yes,dependent=yes,directories=no,location=no,menubar=no,resizable=yes,scrollbars=yes,status=yes,toolbar=no,top=100,left=200,titlebar=yes");

//    // コールバックを実行
//    callback(returnObj);

//    return returnObj;
//}

//モーダルダイアログを表示する (iframe 子画面ver.)
// nextpage: 表示するURL
// callback: ダイアログが閉じられた後に実行される関数。第一引数に戻りオブジェクトが入ります。
function commonModalDialog(nextpage, menutitle, params, width, height, callback) {

    // rootPath (サーバ上での配置場所)は、ViewのShared/_Layout.vbhtmlで出力されることを想定
    var url = nextpage;
    if (url.indexOf("://") < 0) {
        url = rootPath + url;
    }

    // メッセージ受け取りイベントハンドラ
    var receiveMessage = function (event) {
        // JSONをパースする
        var result = JSON.parse(event.data);
        $("#frame-window").dialog("close");
        callback(result);
    }

    // Web Messaging の受け取り口を開く (IE11以降)
    //window.addEventListener("message", receiveMessage, false);
    $(window).on("message", function (e) {
        receiveMessage(e.originalEvent);
    });

    // iframe生成し、モーダルとして開く (URLを開く) → iframe側でメッセージを送信する
    var iframeWin = $("#frame-window");
    var iframeDiv;
    iframeWin.dialog({
        autoOpen: true,
        width: parseInt(width),
        height: parseInt(height),
        modal: true,
        open: function () {
            iframeWin.append('<iframe id="iframe-div" frameborder="0"></iframe>');
            iframeDiv = $("#iframe-div");
            iframeDiv.attr({
                src: url,
                width: iframeWin.width() - 4,
                height: iframeWin.height() - 4
            });
        },
        close: function () {
            returnObjectToParentWindow(null);
            iframeDiv.remove();
            iframeDiv = null;
        },
        resize: function () {
            var p = $(this).parent();
            if (p[0]) {
                var title = $(".ui-dialog-titlebar");
                iframeWin.width(p[0].clientWidth - 4);
                iframeWin.height(p[0].clientHeight - title.height() - 15);
            } else {
                iframeWin.width(iframeWin.width() + 41);
                iframeWin.height(iframeWin.height() + 17);
            }
            iframeDiv.css("width", iframeWin.width() - 4);
            iframeDiv.css("height", iframeWin.height() - 4);
        }
    });

    // iframe側からメッセージを送る場合は、returnObjectToParentWindow を使用する

    return;
}

//**************************************************
//機能概要 : 親画面に指定したオブジェクトを返して画面を閉じる
//           (iframe, modalWindow両対応)
//**************************************************
function returnObjectToParentWindow(obj) {
    var isFrame = $("#iframe-div", parent.document).length;
    window.parent.postMessage(JSON.stringify(obj), "*");
    if (!isFrame) {
        // iframeではない
        window.returnValue = obj;
        window.close();
    }
}


//**************************************************
//機能概要 : 待ち画面を開く
//  引数1  : 処理ID
//  引数2  : 処理完了後に呼び出すコールバック関数
//**************************************************
function openWaitWindow(processId, callback) {

    //検索子画面表示
    // urlは相対パスになるため、同じ機能ID内であればコントローラ名を指定するだけでOKです。
    // 他の機能IDを呼び出したい場合は、階層を遡るか、絶対パス指定をする必要があります。
    var url = "/Wait?id=" + processId;

    var menutitle = "";
    var params = null;
    var width = "600px";
    var height = "480px";
    return commonModalDialog(url, menutitle, params, width, height, callback);
}

//**************************************************
//機能概要 : 汎用のHTMLノード値設定。指定したセレクタの要素に値を設定する。
//           INPUT/SELECTタグのときは value、それ以外のときは innerText に設定。
//  引数: selector   設定対象ノードを特定するためのjQueryセレクタ
//  引数: value      設定する値
//**************************************************
function setValueToAnyNode(selector, value) {
    var nodes = $(selector);
    for (i = 0; i < nodes.length; i++) {
        var tagName = nodes[i].tagName.toUpperCase();
        if (tagName == "INPUT" || tagName == "SELECT" || tagName == "TEXTAREA") {
            nodes[i].value = value;
        } else {
            // 最低限のエスケープを施す (innerTextは、他表示とスペースの詰めが異なるので使用しない)
            nodes[i].innerHTML = $('<div>').text(value).html();
            // nodes[i].innerText = value;
        }
    }
    nodes.trigger("change", true);
}

//**************************************************
//機能概要 : 汎用のHTMLノード値取得。指定したセレクタの要素から値を取得する。
//           INPUT/SELECTタグのときは value、それ以外のときは innerText を取得。
//  引数: selector   取得対象ノードを特定するためのjQueryセレクタ
//**************************************************
function getValueFromAnyNode(selector) {
    var nodes = $(selector);
    for (i = 0; i < nodes.length; i++) {
        var tagName = nodes[i].tagName.toUpperCase();
        if (tagName == "INPUT" || tagName == "SELECT" || tagName == "TEXTAREA") {
            return nodes[i].value;
        } else {
            // 最低限のエスケープを戻す
            var x = nodes[i].innerHTML;
            x = x.replace(/&lt;/g, '<').replace(/&gt;/g, '>').replace(/&amp;/g, '&');
            return x;
            // return nodes[i].innerText;
        }
    }
}

//**************************************************
//機能概要 : 汎用のHTMLノード値取得(特殊)。指定したセレクタがフォーム要素のときのみ値を取得する。
//           INPUT/SELECTタグのときは value、それ以外のときは 空文字 を取得。
//  引数: selector   取得対象ノードを特定するためのjQueryセレクタ
//**************************************************
function getValueFromInputNode(selector) {
    var nodes = $(selector);
    for (i = 0; i < nodes.length; i++) {
        var tagName = nodes[i].tagName.toUpperCase();
        if (tagName == "INPUT" || tagName == "SELECT" || tagName == "TEXTAREA") {
            return nodes[i].value;
        } else {
            return "";
        }
    }
}

//**************************************************
//機能概要 : ドロップダウンコンボ値設定。指定したIDの要素に値を設定する。
//           指定した値のみチェックを入れ、指定されなかった値のチェックは外れる。
//  引数: id         設定対象ノードを特定するためのID。※セレクタではない
//  戻値: カンマ区切りの値
//**************************************************
function getValuesFromDropDownCheckBox(id) {

    // 連番になっているチェックボックスを全て取得
    var nodes = $("input:checkbox[id=^" + id + "_]:checked");
    return nodes.select(function (x) { x.val(); });
}
//**************************************************
//機能概要 : ドロップダウンコンボ値設定。指定したIDの要素に値を設定する。
//           指定した値のみチェックを入れ、指定されなかった値のチェックは外れる。
//  引数: id         設定対象ノードを特定するためのID。※セレクタではない
//  引数: values     設定する値 (文字列の配列 or カンマ(,)区切りの文字列)
//**************************************************
function setValuesToDropDownCheckBox(id, values) {

    // 連番になっているチェックボックスを全て取得
    var nodes = $("input:checkbox[id=^" + id + "_]");

    if (!Array.isArray(values)) {
        values = values.toString().split(",");
    }

    // 一旦全てのチェックを外す
    nodes.prop("checked", false);
    var i;
    // 値が一致するものだけチェックを付ける
    for (i = 0; i < values.length; i++) {
        nodes.filter("[value=" + values[i] + "]").prop("checked", true);
    }
}
