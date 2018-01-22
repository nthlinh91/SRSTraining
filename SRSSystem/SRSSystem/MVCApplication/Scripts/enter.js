/********************************************************************
 * ユーザ名       : SRS
 * システム名     : トレーニング
 * サブシステム名 : 共通
 * 処理名称       : 共通JavaScript関数
 * 機能           : 共通JavaScript関数 フォーカス制御
 * 作成者         : SRSTaro
 * 改版履歴       : 001, 2018-01-11, SRSTaro, 新規作成
 *******************************************************************/

var focusableSimpleEnabled = true; // フォーカス有効簡易フラグ
var focusableDetailEnabled = false; // フォーカス有効詳細フラグ


$(function () {
    // ページ読み込み後初期化処理
    // 簡易フォーカス移動を有効にする
    $("html").on("keydown", moveFocusSimple);
    $("html").on("keydown", moveFocusDetail);
});

//***************************************************************************
/**
  Enterキー フォーカス移動制御説明

  【1.概要】
     Enterキー押下により、画面内の入力項目のフォーカス移動を行います。
     基本的には簡易設定を利用し、必要な場合は詳細設定を利用して下さい。

  【2.使い方】
     簡易設定
       ①特に設定は必要ありません。
         無効にする場合、 focusableSimpleEnabled を false に設定します。

     詳細設定
       ①focusableDetailEnabled を true に設定します。
       
       ②フォーカスを移動させる要素に以下を設定を記述します。
           class属性        :  "focusable"（他のclassとの共存も可）


  【3.仕様・制限事項】
     ・disabled、readonlyの要素は自動的にスキップされます。
     ・Shift＋Enterキーで逆順にフォーカスが移動します。
     ・最後尾の要素でEnterキーを押下すると先頭の要素にフォーカスが移動します。
     ・制御対象はEnterキーのみです。Tabキーによるフォーカス移動には対応しません。
     ・フォーカス移動対象はinput(text/file/radio/password/checkbox)、textarea、select、buttonタグとなります。
     ・textarea内でEnterキーを押下した場合はフォーカス移動せず、通常通りtextarea内に改行を入力します。
     ・Ajax利用時にエラーが出た場合はフォーカス遷移しません。(※旧仕様)

     ・tabIndexを指定した場合、Tab押下時と同様にフォーカス順が変わります。

 */
//***************************************************************************


//***************************************************************************
/**
 * フォーカス移動(簡易)を行います。
 *
 * @param event キー押下イベント
 */
//***************************************************************************
function moveFocusSimple(event) {

    // 機能が無効のときは終了
    if (focusableSimpleEnabled === false) {
        return true;
    }
    if (simpleTargets === null) {
        simpleTargets = $("input, button, select, textarea").filter(":visible:enabled");
        // tabindex考慮
        simpleTargets = sortByTabIndex(simpleTargets);
    }

    return moveFocus(event, simpleTargets);
}
var simpleTargets = null;


//***************************************************************************
/**
 * フォーカス移動(詳細)を行います。
 *
 * @param event キー押下イベント
 */
//***************************************************************************
function moveFocusDetail(event) {

    // 機能が無効のときは終了
    if (focusableDetailEnabled === false) {
        return true;
    }
    if (detailTargets === null) {
        detailTargets = $(".focusable").filter(":visible:enabled");
        // tabindex考慮
        detailTargets = sortByTabIndex(detailTargets);
    }

    return moveFocus(event, detailTargets);
}
var detailTargets = null;

//***************************************************************************
/**
* TabIndexの値に従って要素を並び換えます。
* (Tab押下の挙動と一致させる)
*
* @param arr   並び換え対象の要素配列
* @return      並べ替え後の配列 
*/
//***************************************************************************
function sortByTabIndex(arr) {
    return arr.filter(function () {
        // tabIndex が負数の場合はフォーカスしない
        return (!isFinite(this.tabIndex) || Number(this.tabIndex) >= 0);
    }).sort(function (a, b) {
        // フォーカス順序
        // 1.tabIndex1以上の要素を順にフォーカス
        // 2.tabIndex0または未指定の要素を表示順にフォーカス
        var ai = (isFinite(a.tabIndex) ? Number(a.tabIndex) : 0);
        var bi = (isFinite(b.tabIndex) ? Number(b.tabIndex) : 0);
        if (ai === 0 && bi !== 0) {
            return -1;
        }
        if (ai !== 0 && bi === 0) {
            return 1;
        }
        return ai - bi;
    });
}

//***************************************************************************
/**
* フォーカス移動を行います。
*
* @param event   キー押下イベント
* @param targets 対象コントロール (jQueryオブジェクト)
*/
//***************************************************************************
function moveFocus(event, targets) {

    var keyCode = event.which;

    // Enterキー押下でフォーカス移動（Tabは挙動が異なるので対象外）
    if (keyCode === 13) {
        var ctrl = event.ctrlKey;
        var shift = event.shiftKey;

        // 対象コントロールを求める
        // inputとtextareaとselectとbuttonので、表示されていて有効なもののみ対象とする
        var currentIndex = targets.index(event.target);

        // 対象を選択していないときは、最初の要素を選択して抜ける
        if (currentIndex < 0) {
            if (targets.length > 0) {
                targets[0].focus();
                selectText(targets[0], true);
                return false;
            }
            return true;
        }
        // ajaxエラーのときは抜ける
        if ($(event.target).hasClass("ajaxError")) {
            return true;
        }
        // textarea内でのEnterはフォーカス移動しない
        if (event.target.tagName.toUpperCase() === "TEXTAREA") {
            return true;
        }
        // 現在のコントロールの選択を解除する
        selectText(event.target, false);

        // 次のコントロールのIndexを求める
        var nextIndex = currentIndex;
        if (shift === false) {
            nextIndex += 1;
            if (nextIndex >= targets.size()) {
                nextIndex = 0;
            }
        } else {
            nextIndex -= 1;
            if (nextIndex < 0) {
                nextIndex = targets.size() - 1;
            }
        }
        // フォーカスしてイベントをキャンセル
        var node = targets[nextIndex];
        node.focus();
        selectText(node, true);
        return false;
    }
}

//***************************************************************************
/**
* 指定した要素について、テキストを全選択または選択解除する (例外抑制)
*
* @param node   対象要素
* @param isSelect true のとき全選択、falseのとき選択解除
*/
//***************************************************************************
function selectText(node, isSelect) {
    try {
        var max = 0;
        var tagName = '', type = '';
        var tagName = '', type = '';
        if (isSelect && node && node.value !== undefined) {
            max = node.value.length;
            tagName = node.tagName.toUpperCase();
            if (node.type) {
                type = node.type.toUpperCase();
            }
            if (tagName === 'BUTTON' || (tagName === 'INPUT' && (type === 'SUBMIT' || type === 'BUTTON'))) {
                isSelect = false;
                max = 0;
            }
        }
        if (typeof (node.selectionStart) != "undefined" && typeof (node.selectionStart) != "unknown") {
            node.selectionStart = 0;
            node.selectionEnd = max;
        } else if (document.selection) {
            var range = node.createTextRange();
            range.collapse();
            range.moveStart("character", 0);
            range.moveEnd("character", max);
            range.select();
        }
    } catch (e) {
    }
}
