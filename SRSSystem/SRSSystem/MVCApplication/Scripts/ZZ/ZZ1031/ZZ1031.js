﻿/// <reference path="../../function.js" />
/********************************************************************
 * ユーザ名       : SRS
 * システム名     : トレーニング
 * サブシステム名 : トレニンーグ機能グループ
 * 処理名称       : 納品見積登録
 * 機能           : 納品見積登録 JavaScript
 * 作成者         : SRSTaro
 * 改版履歴       : 001, 2018-01-11, SRSTaro, 新規作成
 *******************************************************************/

// 以下 jquery
$(document).ready(function () {

    //**************************************************
    //機能概要 : 登録ボタン押下時処理を行なう
    //**************************************************
    $("input.insert-btn").on("click", confirmInsert);

    //**************************************************
    //機能概要 : 更新ボタン押下時処理を行なう
    //**************************************************
    $("input.update-btn").on("click", confirmUpdate);

    //**************************************************
    //機能概要 : 削除ボタン押下時処理を行なう
    //**************************************************
    $("input.delete-btn").on("click", confirmDelete);

    //**************************************************
    //機能概要 : 登録内容変更時に登録/更新ボタンを押下不可能にする
    //**************************************************
    $("table.table-items").find(":input").on("change", function () {
        $("#Insert").attr("disabled", "disabled");
        $("#Update").attr("disabled", "disabled");
    });

    //**************************************************
    //機能概要 : 参照ボタン押下時処理を行なう
    //**************************************************
    $("input.shimukesakicd-btn").on("click", function () {
        openSaishuShimukechiSelectWindow();
        return false;
    });

});
