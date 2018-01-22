/********************************************************************
 * ユーザ名       : SRS
 * システム名     : トレーニング
 * サブシステム名 : 共通
 * 処理名称       : 共通JavaScript関数
 * 機能           : 共通JavaScript関数
 * 作成者         : SRSTaro
 * 改版履歴       : 001, 2018-01-11, SRSTaro, 新規作成
 *******************************************************************/

// 指定した文字列のURLエンコードを行う。
function fncEncode(argEncStr) {
    return escape(argEncStr);
}
// 指定した文字列のURLエンコードを行う。
function fncDecode(argDecStr) {
    return unescape(argDecStr);
}
// 値が空かどうかを判定します。
function isNullValue(value) {
    return value === undefined || value === null || value === "";
}
// 配列かどうかを判定します。
if (!Array.isArray) {
    Array.isArray = function (vArg) {
        return Object.prototype.toString.call(vArg) === "[object Array]";
    };
}
// オブジェクトかどうかを判定します。
function isObject(o) {
    return (o instanceof Object && !(o instanceof Array)) ? true : false;
}

//**************************************************
//機能概要 : 業務画面のメッセージ表示を行なう
//**************************************************

// 一覧画面用 確認処理
function confirmListReference() {
    if ($("input.sel-radio:checked").size() > 0) {
        return true;
    } else {
        return alertTargetNotFound()
    }
}
function confirmListUpdate() {
    if ($("input.sel-radio:checked").size() > 0) {
        return true;
    } else {
        return alertTargetNotFound()
    }
}
function confirmListInsert() {
    if ($("input.sel-radio:checked").size() > 0) {
        return true;
    } else {
        return alertInsertTargetNotFound();
    }
}
function confirmListDelete() {
    if ($("input.sel-radio:checked").size() > 0) {
        return confirmDelete();
    } else {
        return alertDeleteTargetNotFound();
    }
}
function confirmListCancel() {
    if ($("input.sel-radio:checked").size() > 0) {
        return confirmCancel();
    }
}

// 複数更新画面用 確認処理
function confirmMultiInsertConfirm() {
    if ($("input.sel-check:checked").size() > 0) {
        return true;
    } else {
        return alertInsertTargetNotFound();
    }
}
function confirmMultiUpdateConfirm() {
    if ($("input.sel-check:checked").size() > 0) {
        return true;
    } else {
        return alertUpdateTargetNotFound();
    }
}
function confirmMultiInsert() {
    if ($("input.sel-check:checked").size() > 0) {
        return confirmInsert();
    } else {
        return alertInsertTargetNotFound();
    }
}
function confirmMultiUpdate() {
    if ($("input.sel-check:checked").size() > 0) {
        return confirmUpdate();
    } else {
        return alertUpdateTargetNotFound();
    }
}
function confirmMultiDelete() {
    if ($("input.sel-check:checked").size() > 0) {
        return confirmDelete();
    } else {
        return alertDeleteTargetNotFound();
    }
}
function confirmMultiCancel() {
    if ($("input.sel-check:checked").size() > 0) {
        return confirmCancel();
    }
}

// 画面 確認処理
function confirmInsert() {
    // 登録してよろしいですか？"
    return confirm(l("CMN_N00041"));
}
function confirmUpdate() {
    // 更新してよろしいですか？
    return confirm(l("CMN_N00042"));
}
function confirmDelete() {
    // 削除してよろしいですか？
    return confirm(l("CMN_N00043"));
}
function confirmCancel() {
    // 処理を取り消します。よろしいですか？
    return confirm(l("CMN_N00039"));
}
function confirmPrint() {
    // 出力してよろしいですか？
    return confirm(l("CMN_N00047"));
}

// 画面 エラー表示
function alertTargetNotFound() {
    // 処理対象データを選択してください。
    alert(l("CMN_E00044"));
    return false;
}
function alertUpdateTargetNotFound() {
    // 更新対象の明細が存在しません。
    alert(l("CMN_E00014"));
    return false;
}
function alertInsertTargetNotFound() {
    // 登録対象の明細が存在しません。
    alert(l("CMN_E00013"));
    return false;
}
function alertDeleteTargetNotFound() {
    // 削除対象の明細が存在しません。
    alert(l("CMN_E00015"));
    return false;
}


//**************************************************
//機能概要 : 業務画面の制御を行なう
//**************************************************

var myOpenWin = new Object();

// 業務画面を開く
function openNextPage( argNextPage, argNextPageName, argWindowSize ) {

    var width = "1010";
    var height = "678";

    // 引数でサイズが渡された場合、分解して設定する
    if (argWindowSize) {
        var sizes = argWindowSize.match(/^([0-9]+)x([0-9]+)$/);
        if (sizes !== null) {
            width = sizes[1];
            height = sizes[2];
        }
    }

    var url = argNextPage;
    if (url.indexOf("://") < 0) {
        url = rootUrl + url;
    }

    if (myOpenWin[argNextPageName] == undefined) {

        //初めて画面を開く場合、新規ウィンドウを開く
        myOpenWin[argNextPageName] = window.open(url, argNextPageName,
        "width=" + width + ",height=" + height + ",alwaysRaised=yes,dependent=yes,directories=no,location=no,menubar=no,resizable=yes,scrollbars=yes,status=yes,toolbar=no,top=0,left=0,titlebar=yes" );

    } else {

        if (myOpenWin[argNextPageName].closed) {

            //閉じられている場合、新規ウィンドウを開く
            myOpenWin[argNextPageName] = window.open(url, argNextPageName,
            "width=" + width + ",height=" + height + ",alwaysRaised=yes,dependent=yes,directories=no,location=no,menubar=no,resizable=yes,scrollbars=yes,status=yes,toolbar=no,top=0,left=0,titlebar=yes");

        } else {

            //既に開いている画面は最前面に表示させる
            myOpenWin[argNextPageName].focus();
        }
    }
}

// 自ウィンドウを判断して、メインメニューから業務画面を開く
function mainOpenNextPage( argNextPage, argNextPageName, argWindowSize ) {
    if (window.name == "mainmenu") {
        openNextPage( argNextPage, argNextPageName, argWindowSize );
    } else {
        if (opener != undefined && opener.closed == false) {
            opener.openNextPage( argNextPage, argNextPageName, argWindowSize );
        }
    }
}

// 全ての業務画面を閉じる
function fncAllClose() {
    var winName;

    for ( winName in myOpenWin ) {
        if (myOpenWin[winName].closed) {
        } else {
            myOpenWin[winName].opener = "_dummy";
            myOpenWin[winName].close();
        }
    }
}

// 指定した画面を閉じる
function fncDisplayClose(argWin) {
    if (argWin) {
        if (argWin.closed) {
        } else {
            argWin.close();
        }
    }
}

//開いている画面の名前を表示
function fncName() {
    var winName;
    for ( winName in myOpenWin ) {
        alert(winName);
    }
}

//自画面を閉じる
function fncWinClose() {
  if (window.dialogArguments !== undefined) {
      // モーダルの場合は普通に閉じる
      window.close();
  } else {
      var isFrame = $("#iframeDiv", parent.document).length;
      // iframeのときは、呼出元にメッセージを投げて閉じてもらう
      window.parent.postMessage(JSON.stringify(null), "*");
      if (!isFrame) {
          // iframeではない
          // サーバ跨りで呼出された場合も、警告なしで画面を閉じる
          window.open("about:blank", "_self").close();
      }
  }
}

//メニュー画面を最前面に表示する
function fncMenu(){
    top.opener.focus();
}

//各業務画面から呼出元のサブメニューを呼出す
function fncSubmenu( argSubmenuName ) {
    top.window.fncSubmenuFocus(argSubmenuName);
}

//上記 fncSubmenu() で使用する関数
function fncSubmenuFocus( argSubmenuName ) {
    if (argSubmenuName != "" && argSubmenuName != null) {
        //トップメニューが生きている場合
        if (opener != undefined && opener.closed == false) {
            //呼出画面が生きている場合
            if (opener.myOpenWin[argSubmenuName] != undefined && opener.myOpenWin[argSubmenuName].closed == false) {
                opener.myOpenWin[argSubmenuName].focus();
            }
        }
    } else {
        top.opener.focus();
    }
}


//**************************************************
//機能概要 : 業務画面のコントロール制御を行う
//**************************************************
// 項目ロック処理 ---------------------------------------------------------
//条件画面の制御

//チェックボックスの制御 (チェックを付ける)
// @param element  対象チェックボックスを含むjQueryオブジェクト
function setCheckbox(element) {
    var elem = $(element);
    // 固定列の場合を判定
    var grid = elem.closest(".grid-table");
    if (grid.length > 0) {
        var rowno = elem.attr("data-rowno");
        var target;
        if (rowno) {
            // 
            target = grid.find("#fixed-data tr[data-rowno=" + rowno + "] :checkbox");
        } else {
            var groupno = elem.attr("data-groupno");
            target = grid.find("#fixed-data tbody[data-groupno=" + groupno + "] :checkbox");
        }
        target.first().prop("checked", true);
    } else {
        elem.find(":checkbox").first().prop("checked", true);
    }
}

// ラジオボタンの制御
// @param element  対象ラジオボタンを含むjQueryオブジェクト
//                 通常の一覧の場合、クリックされた tr を渡し、複数行グループ一覧の場合は tbody を渡す
function setRadioButton(element) {
    var elem = $(element);
    // 固定列の場合を判定
    var grid = elem.closest(".grid-table");
    if (grid.length > 0) {
        var rowno = elem.attr("data-rowno");
        var target;
        if (rowno) {
            // 
            target = grid.find("#fixed-data tr[data-rowno=" + rowno + "] :radio");
        } else {
            var groupno = elem.attr("data-groupno");
            target = grid.find("#fixed-data tbody[data-groupno=" + groupno + "] :radio");
        }
        target.first().prop("checked", true);
    } else {
        elem.find(":radio").first().prop("checked", true);
    }
}

function contrlCondition(bDisable, frameName, formName, objectCount) {
    for (i = 0; i < objectCount; i++) {
        objString = "parent." + frameName + ".document." + formName + ".elements[i]";
        objElement = eval(objString);
        objElement.disabled = bDisable;
    }
}

//**************************************************
//機能概要 : 画面フッタのメッセージ欄にメッセージとクラス名を設定します。
//  引数: message       表示するメッセージ
//  引数: className     メッセージに設定するクラス
//**************************************************
function setFooterMessage(message, className) {
    var messageNode = $(".page-footer-message");
    var popupButton = $("#__MessagePopupButton");
    var popupMessage = $("#__message-tooltip-text");
    message = l(message);
    var firstLine = message;
    var lines = message.split("\n");
    if (lines.length > 1) {
        popupButton.removeClass("hide");
    } else {
        popupButton.addClass("hide");
    }
    firstLine = lines[0];

    messageNode.text(firstLine);
    messageNode.addClass(className);
    popupMessage.text(message);
}

//**************************************************
//機能概要 : フッターメッセージをポップアップで表示します。
//            (省略された部分も全て表示)
//**************************************************
function showFooterMessagePopup() {
    var popupMessage = $("#__message-tooltip");
    popupMessage.show();
}
//**************************************************
//機能概要 : フッターメッセージをポップアップを閉じます。
//**************************************************
function hideFooterMessagePopup() {
    var popupMessage = $("#__message-tooltip");
    popupMessage.hide();
}
//**************************************************
//機能概要 : フッターメッセージポップアップの表示を切替えます。
//**************************************************
function toggleFooterMessagePopup() {
    var popupMessage = $("#__message-tooltip");
    popupMessage.toggle();
}

//**************************************************
//機能概要 : 指定したDOMノードについて、指定した文字列で始まるクラス名を取得します。
// (initializeSelectableGrid で使用)
//  引数: node         DOMノード。※jQueryオブジェクトではない
//  引数: pattern      クラス名の先頭と一致させる文字列。
//  引数: lines        1グループとみなす行数。通常は1を指定します。
//**************************************************
function getStartWithClassNames(node, pattern, lines) {
    var className = node.className.split(" ");
    if (className.length === 0) {
        return null;
    }
    for (i = 0; i < className.length - 1; i++) {
        if (className[i].lastIndexOf(pattern, 0) === 0) {
        
            // 複数行のとき、全ての行クラス名を計算
            var baseRowNo = Number(className[i].substring(3));
            baseRowNo -= baseRowNo % lines;
            var classNames = [];
            for (i = 0; i < lines; i++) {
                classNames[i] = "g_R" + String(baseRowNo + i);
            }
            return classNames;
        }
    }
    return null;
}

//**************************************************
//機能概要 : 列固定テーブルを選択可能テーブルとして初期化します。
// (Grid.js の機能で作成したテーブルに、一覧画面(選択)パターンと同じ機能を付加します)
//  引数: lines        1グループとみなす行数。通常は1を指定します。
//**************************************************
function initializeSelectableGrid(lines) {
    var x = $("div.g_Body .g_C");
    // 行クリック時、先頭のラジオボタンを選択状態にする
    x.on("click", function clickListRow() {
        // 対象行のクラス名取得
        var classNames = getStartWithClassNames($(this)[0], "g_R", lines);
        if (classNames === null) {
            return;
        }
        var selector = ".g_BR." + classNames.join(", .g_BR.");
        // ラジオボタンをチェック
        var radio = $(selector).find(":radio")
        radio.prop("checked", true);
    });

    // ホバー時、行全体の色をハイライトする
    x.hover(function onHover() {
        // 対象行のクラス名取得
        var classNames = getStartWithClassNames($(this)[0], "g_R", lines);
        if (classNames === null) {
            return;
        }
        var selector = ".g_BR." + classNames.join(", .g_BR.");
        // ホバークラスを設定
        $(".g_BR." + selector).addClass("table-row-hover");
    }, function offHover() {
        // 対象行のクラス名取得
        var classNames = getStartWithClassNames($(this)[0], "g_R", lines);
        if (classNames === null) {
            return;
        }
        var selector = ".g_BR." + classNames.join(", .g_BR.");
        // ホバークラスを削除
        $(".g_BR." + selector).removeClass("table-row-hover");
    });
}



var Wait = {
    /**
    * 検索待機中の要素の無効化<br>
    */
    setDisabled: function(elem) {
        if (elem.tagName == "INPUT"){
            elem.disabled = true;
        }
    }
};



//ダウンロード処理 --------------------------------------------------------
var downloadmsg;

//open download message box
function openDownloadMsgBox(argNextPage, argWindowName) {
    downloadmsg = window.open(argNextPage,argWindowName,
    "width=600,height=480,alwaysRaised=yes,dependent=yes,directories=no,location=no,menubar=no,resizable=yes,scrollbars=yes,status=yes,toolbar=no,top=100,left=200,titlebar=yes" );

    return downloadmsg;

}

//close download message box
function closeDownloadMsgBox() {
    if (downloadmsg != undefined ) {
        downloadmsg.close();
    }
}


//プレビュー画面用 --------------------------------------------------------
//open preview window
function openPreview(argNextPage, argWindowName) {
    downloadmsg = window.open(argNextPage, argWindowName,
    "width=1010,height=687,alwaysRaised=yes,dependent=yes,directories=no,location=no,menubar=no,resizable=yes,scrollbars=yes,status=yes,toolbar=no,top=0,left=0,titlebar=yes" );
}




//**************************************************
//機能概要 : スクロールの制御を行なう
//**************************************************
var head_left;
var head_right;
var detail_left;
var detail_right;

function initScroll() {
    head_left = document.getElementById("left-header");
    head_right = document.getElementById("right-header");
    detail_left = document.getElementById("left-detail");
    detail_right = document.getElementById("right-detail");
    detail_right.onscroll=scroll_detail;
    detail_left.onscroll=scroll_detail2;
}

function scroll_detail(){
    head_right.scrollLeft = detail_right.scrollLeft;
    head_right.scrollTop  = detail_right.scrollTop;
    detail_left.scrollLeft = detail_right.scrollLeft;
    detail_left.scrollTop  = detail_right.scrollTop;
}

function scroll_detail2(){
    detail_right.scrollTop  = detail_left.scrollTop;
}

//**************************************************
//機能概要 : 入力コントロールの書式制御を行う (多言語対応)
//**************************************************

// 数値書式整形 (桁区切り文字付与)
function numberFormat(pVal) {
    // 設定は _Layout.vbhtml 内で定義
    //var decimalPoint = "."
    //var numberDelimiter = ","

    // 数値でないときは抜ける
    if (pVal === "" || pVal === null) {
        return "";
    }

    // 区切り文字を消去
    pVal = String(pVal).replace(new RegExp("\\"+numberDelimiter, "g"), "");

    // 整数部と小数部に分割
    var integer = "";
    var decimal = "";
    if (String(pVal).indexOf(decimalPoint) > -1) {
        var sp = String(pVal).split(decimalPoint);
        integer = sp[0];
        decimal = sp[1];
    } else {
        integer = String(pVal);
    }

    // 桁区切りを3桁毎に付与
    integer = integer.replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1" + numberDelimiter);

    // 結果を返す
    if (decimal === "") {
        return integer;
    } else {
        return integer + decimalPoint + decimal;
    }
}
// 数値書式整形 (桁区切り文字削除)
function noNumberFormat(pVal) {
    // 数値でないときは抜ける
    if (pVal === "" || pVal === null) {
        return "";
    }
    // 区切り文字を消去
    pVal = String(pVal).replace(new RegExp("\\" + numberDelimiter, "g"), "");
    return pVal;
}

//日付書式整形(年月日)
function ymdFormat(val) {
    //var pat1 = /^[0-9]{4}[01][0-9][0-3][0-9]$/;
    var pat2 = /^[0-9]{2}[01][0-9][0-3][0-9]$/;

    // 2桁年月日のとき、20xx年と見做す
    if (val.match(pat2) !== null) {
        val = "20" + val;
    }
    return val
}

//日付書式整形(年月)
function ymFormat(val) {
    //var pat1 = /^[0-9]{4}[01][0-9]$/;
    var pat2 = /^[0-9]{2}[01][0-9]$/;

    // 2桁年月日のとき、20xx年と見做す
    if (val.match(pat2) !== null) {
        val = "20" + val;
    }
    return val
}

//**************************************************
//機能概要 : 指定した日付フォーマットに、与えた値を整形して返す。
//  足りない桁数は現在の日付を設定する。
//  (不正な日付かどうかは判定しない)
// 引数1 format: 日付フォーマット。(例 YYYYMMDD)
// 引数2 val:    元にする値。
// 戻り値:  整形後の値。
//**************************************************
function dateFormatExact(format, val) {
    // formatに現在の日付を設定する
    var date = new Date();
    if (isNullValue(format)) format = "YYYYMMDD";
    format = format.replace(/YYYY/g, date.getFullYear());
    format = format.replace(/YY/g, date.getFullYear().toString().substring(2));
    format = format.replace(/MM/g, ("0" + (date.getMonth() + 1)).slice(-2));
    format = format.replace(/DD/g, ("0" + date.getDate()).slice(-2));
    format = format.replace(/hh/g, ("0" + date.getHours()).slice(-2));
    format = format.replace(/mm/g, ("0" + date.getMinutes()).slice(-2));
    format = format.replace(/ss/g, ("0" + date.getSeconds()).slice(-2));
    if (format.match(/S/g)) {
        var milliSeconds = ("00" + date.getMilliseconds()).slice(-3);
        var length = format.match(/S/g).length;
        for (var i = 0; i < length; i++) format = format.replace(/S/, milliSeconds.substring(i, i + 1));
    }
    // valの値をformatに上書きする (右詰め)
    if (isNullValue(val)) {
        val = "";
    }
    if (val.length > format.length) {
        val = val.substring(val.length - format.length, val.length);
    }
    format = format.substring(0, format.length - val.length) + val;

    return format;
}

//日付妥当性チェック
function isDate(pyyyy, pmm, pdd) {
    var maxDayOfMonth = Array(31,29,31,30,31,30,31,31,30,31,30,31);
    if(pmm < 1 || pmm > 12) {
        return false;
    }
    if(pdd < 1 || pdd > maxDayOfMonth[pmm - 1]) {
        return false;
    }
    if(pmm != 2) {
        return true;
    }
    if(pdd < 29) {
        return true;
    }
    if((pyyyy % 4) == 0 && (pyyyy % 100) != 0) {
        return true;
    }
    if((pyyyy % 400) == 0) {
        return true;
    }
    return false;
}

/* 任意のオブジェクトを、ASP.NET MVCのモデルバインド可能な形でHiddenに設定します。
既にIDが存在する場合は、その要素に値を設定します。 */
function setToHidden(data) {

    var innerSetToHidden = function (id, name, data) {
        var elemId, elemName;
        if (Array.isArray(data)) {
            for (var i = 0; i < data.length; i++) {
                elemId = id + '_' + i.toString() + '_';
                elemName = name + '[' + i.toString() + ']';
                innerSetToHidden(elemId, elemName, data[i]);
            }
            return;
        }
        if (isObject(data)) {
            for (var elem in data) {
                elemId = id + (id === '' ? '' : '_') + elem;
                elemName = name + (id === '' ? '' : '.') + elem;
                innerSetToHidden(elemId, elemName, data[elem]);
            }
            return;
        }

        if (id === '') {
            return;
        }

        var nodes = $("#" + id);
        if (nodes.length > 0) {
            nodes.val(data);
        } else {
            var hidden = $('<input>').attr({ type: 'hidden', id: id, name: name, value: data });
            hidden.appendTo('form');
        }
    };

    innerSetToHidden("", "", data);
}

// 項目自動編集設定
$(document).ready(function () {
    // 数値テキストボックス編集処理 (桁区切り)
    $("input.number-delimited").on("focus", function () {
        var node = $(this);
        node.val(noNumberFormat(node.val()));
        var elm = node[0];
        if (elm.createTextRange) {
            var range = elm.createTextRange();
            range.moveEnd('character', elm.value.length);
            range.select();
        } else if (elm.setSelectionRange) {
            elm.setSelectionRange(0, elm.value.length);
        }
    });
    $("input.number-delimited").on("blur", function () {
        var node = $(this);
        node.val(numberFormat(node.val()));
    });
    $("input.number-delimited").each(function () {
        var node = $(this);
        node.val(numberFormat(node.val()));
    });

    // 年月日テキストボックス編集処理 (2桁年入力対応)
    $("input.ymd").on("blur", function () {
        var node = $(this);
        node.val(ymdFormat(node.val()));
    });
    // 年月テキストボックス編集処理 (2桁年入力対応)
    $("input.ym").on("blur", function () {
        var node = $(this);
        node.val(ymFormat(node.val()));
    });
    // 任意フォーマット年月日テキストボックス編集処理
    $("input.date-exact").on("blur", function () {
        var node = $(this);
        var format = node.attr("data-date-format");
        node.val(dateFormatExact(format, node.val()));
    });
});


//**************************************************
//機能概要 : ウインドウの高さに合わせて明細テーブルの高さを伸縮させる
// (リサイズ終わるまで処理しない版)
//**************************************************
function adjustDetailTableHeight() {

    var timer = false;
    var resizeEvent = function () {
        if (timer !== false) {
            clearTimeout(timer);
        }
        timer = setTimeout(function () {
            var divElement = $("div.table-list-body-container");
            if (divElement.length === 0) {
                return;
            }
            // スクロールバーをできるだけ考慮
            //var windowHeight = (window.innerHeight || document.documentElement.clientHeight);
            var windowHeight = (document.documentElement.offsetHeight || window.innerHeight);
            var elementPointY = (document.documentElement.scrollTop || document.body.scrollTop) + parseInt(divElement.offset().top);

            // ウインドウ高さ － ドキュメント左上から明細までの距離(スクロール済みY座標 ＋ 表示時点の明細Y座標) － 40px(適当)で求める
            var adjusted = (windowHeight - elementPointY - 40)
            if (adjusted > 0) {
                divElement.css("height", (adjusted) + "px");
            }
        }, 200);
    }
    $(window).resize(resizeEvent);
    resizeEvent();
}

//**************************************************
//機能概要 : ウインドウの幅に合わせて明細テーブルの幅を伸縮させる（可変部用）
// (リサイズ終わるまで処理しない版)
//**************************************************
function adjustDetailTableWidth() {

    var timer = false;
    var resizeEvent = function () {
        if (timer !== false) {
            clearTimeout(timer);
        }
        timer = setTimeout(function () {
            var divElement = $("div.grid-flow-tables");
            if (divElement.length === 0) {
                return;
            }
            var windowWidth = (window.innerWidth || document.documentElement.clientWidth);
            var elementPointX = (document.documentElement.scrollLeft || document.body.scrollLeft) + parseInt(divElement.offset().left);

            // ウインドウ高さ － ドキュメント左上から明細までの距離(スクロール済みX座標 ＋ 表示時点の明細X座標) － 25px(適当)で求める
            var adjusted = (windowWidth - elementPointX - 25)
            if (adjusted > 0) {
                divElement.css("width", (adjusted) + "px");
            }
        }, 200);
    }
    $(window).resize(resizeEvent);
    resizeEvent();
}

//*****************************************************************************
//機能概要 : 指定したテキスト入力をモーダルポップアップで編集するように設定する
//*****************************************************************************
// モーダル要素を予め追加 (Views/_Shared/_Layout.vbhtmlに追加したのでここでは追加不要)
// $("body").append('<div class="modal-overlay"><div class="modal-content"><input type="button" class="modal-close" value="閉じる" /><textarea class="modal-textarea"></textarea></div></div>');
function setPopupTextInput(targetSelector) {

    // 非活性要素の場合イベントが発生しないので、発生するよう細工する
    $(targetSelector).each(function () {
        if ($(this).attr("disabled")) {
            var node = $('<span style="position:relative;display:inline-block;"><div style="position:absolute; left:0; right:0; top:0; bottom:0; cursor: pointer;"></div></span>');
            $(this).replaceWith(node);
            node.prepend(this);
            node.on("click", function (e) {
                // 細工した要素にクリック時ハンドラを設定
                onTargetClick(e, $(this).children().first());
            });
        }
    });

    // 対象INPUTにクリック時ハンドラを設定
    $(targetSelector).on("click", function (e) {
        onTargetClick(e, $(this));
    });

    // 対象要素をクリックしたときの処理
    function onTargetClick(e, $this) {
        // 再帰的に処理されないようにする
        if ($this.hasClass("modal-textarea")) {
            return;
        }

        var $overlay = $(".modal-overlay");
        var $content = $(".modal-content");
        var $closeButton = $(".modal-close");
        var $textarea = $(".modal-textarea");

        // 値を設定
        $textarea.val($this.val());
        // disabled/readonlyの値を継承
        $textarea.prop("disabled", function () { return $this.prop("disabled"); });
        $textarea.prop("readonly", function () { return $this.prop("readonly"); });
        $textarea.prop("maxlength", function () { return $this.prop("maxlength"); });

        // オーバーレイをフェードイン
        $overlay.fadeIn(300, function () {
            $textarea.focus();
        });
        // モーダルコンテンツの表示位置を設定
        modalResize();

        // クリックで閉じる (以前設定されていたハンドラを差し替える)
        $closeButton.off().on("click", closeModal);
        // フチなどをクリックしたときは閉じない
        $content.off().on("click", function (e) {
            e.stopPropagation();
        });

        // リサイズしたら表示位置を再取得
        $(window).on("resize", modalResize);

        // モーダル表示を閉じる処理 (元の要素に値を書き戻す)
        function closeModal() {
            // モーダルコンテンツとオーバーレイをフェードアウト
            // 値を書き戻し
            $this.val($textarea.val());
            $this.trigger("blur", true);
            $overlay.fadeOut(300);
        }

        // モーダルコンテンツの表示位置を設定する関数
        function modalResize() {
            // ウィンドウの横幅、高さを取得
            var w = $(window).width();
            var h = $(window).height();

            // モーダルコンテンツの表示位置を取得
            var x = (w - $content.outerWidth(true)) / 2;
            var y = (h - $content.outerHeight(true)) / 2;

            // モーダルコンテンツの表示位置を設定
            $content.css({ "left": x + "px", "top": y + "px" });
        }

    }
}

//**************************************************
//機能概要 : 一覧テーブル要素で、はみ出した文章をツールチップ表示する
//**************************************************
$(function () {
    // ツールチップ要素を予め追加 (Views/_Shared/_Layout.vbhtmlに追加したのでここでは追加不要)
    // $("body").append('<div id="__tooltip" class="list-tooltop "></div>')

    var targetSelector = ".table-list:not('.no-ellipsis') .item-text, .table-list:not('.no-ellipsis') th";

    // ツールチップ表示用の関数 (外側の変数を利用して動的表示する)
    var $tooltip = $("#__tooltip");
    var $left = 0, $top = 0, $text = '';
    var displayTooltip = function () {
        $tooltip.text($text);
        $tooltip.css("left", $left);
        $tooltip.css("top", $top);
        $tooltip.css("display", "block");
    };

    // 要素にマウスが載ったときの処理
    $(document).on("mouseenter", targetSelector, function (e) {
        var $this = $(this);

        var $offsetWidth = this.clientWidth;
        if ($offsetWidth < this.scrollWidth && !$this.attr("title")) { // 正確でないが動作軽量

            // ツールチップ位置は、マウスカーソル位置を基準とする
            $left = e.pageX;
            $top = e.pageY + 30;
            $text = $this.text();
            var minWidth = Math.max($offsetWidth, 100);
            if ($left + minWidth > $(window).width()) {
                // ツールチップ表示幅として、元要素の幅は確保する
                $left = $(window).width() - minWidth;
            }

            // 0.4秒ディレイで表示 (常に1つ以下の待ち状態になるよう、キューを空にしてからディレイする)
            $tooltip.stop();
            $tooltip.delay(400).queue(displayTooltip);
        }
    });
    // 要素からマウスが離れたときの処理
    $(document).on("mouseleave", targetSelector, function () {
        // 抜けたら待ち状態と表示状態を解除する
        $tooltip.stop();
        $tooltip.css("display", "none");
    });
});

//**************************************************
//機能概要 : jquery.submit を実行したときの処理を改善する
//      submit時のイベント処理を動かさないようにする
//**************************************************
var _org_jquery_submit = $.fn['submit'];
$.fn['submit'] = function (data, fn) {
    // イベントハンドラを退避して消去
    var evdata = $._data($("form").get(0));
    var events = evdata.events;
    evdata.events = undefined;
    // jqueryの元処理を実行
    if (data === undefined) {
        _org_jquery_submit.bind(this)();
        // イベントハンドラを復帰
        evdata.events = events;
    } else {
        _org_jquery_submit.bind(this, data, fn);
    }
};
