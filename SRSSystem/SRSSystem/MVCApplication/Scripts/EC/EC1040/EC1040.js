/// <reference path="../../jquery/jquery-1.7.1-vsdoc.js" />
/// <reference path="../../jquery/jquery-ui-1.8.20.js" />
/// <reference path="../../function.js" />
/********************************************************************
 * ユーザ名       : 住友ゴム工業株式会社
 * システム名     : タイヤ海外業務基幹システム（JUST）
 * サブシステム名 : 共通
 * 処理名称       : 最終仕向地検索
 * 機能           : 最終仕向地検索 JavaScript
 * 作成者         : sfunaoka/SDS
 * 改版履歴       : 001, 2015-09-15, sfunaoka/SDS, 新規作成
 *******************************************************************/

// 以下 jquery
$(document).ready(function () {

    //**************************************************
    //機能概要 : 明細行押下時処理を行う
    //**************************************************
    $("tr.table-list-row").on("click", function clickListRow() {
        setRadioButton($(this));
    });

    //**************************************************
    //機能概要 : 選択ボタン押下時処理を行なう
    //**************************************************
    $("input.select-btn").on("click", returnValueToParent);

    //**************************************************
    //機能概要 : 参照画面から値を返す
    //**************************************************
    function returnValueToParent() {
        // チェックの入っているradio buttonを取得し、値を取得する
        var selectNode = $(":radio:checked");

        if (selectNode.length === 0) {
            alertTargetNotFound();
            return;
        }

        //ラジオボタンが1つのときはlengthが取れないので、1つめで確定
        var rowNo = selectNode.val();

        // 戻値に値を設定して自ウィンドウを閉じる
        if (rowNo !== undefined) {

            // ↓↓戻値オブジェクトを作成
            var obj = new Object();

            obj.shimukechiCd = $("input[name='ListData[" + rowNo + "].SHIMUKECHICD']").val();
            obj.shimukechiNm = $("input[name='ListData[" + rowNo + "].SHIMUKECHINM']").val();
            obj.saishuShimukechiCd = $("input[name='ListData[" + rowNo + "].SAISHUSHIMUKECHICD']").val();
            obj.saishuShimukechiNm = $("input[name='ListData[" + rowNo + "].SAISHUSHIMUKECHINM']").val();
            obj.saishuShimukechiRnm = $("input[name='ListData[" + rowNo + "].SAISHUSHIMUKECHIRNM']").val();

            // ↑↑戻値オブジェクトを作成
            
            // オブジェクトを戻値に設定して、呼出元画面に戻る
            window.returnValue = obj;
            $('#FinalDestination', parent.document).val(obj.saishuShimukechiCd);
            $('#ReferenceRnm', parent.document).val(obj.saishuShimukechiRnm);
            window.parent.$('#frame-window').dialog('close');
        }
    }
});
