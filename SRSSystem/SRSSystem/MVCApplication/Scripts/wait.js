/********************************************************************
 * ユーザ名       : SRS
 * システム名     : トレーニング
 * サブシステム名 : 共通
 * 処理名称       : 待ち画面
 * 機能           : 待ち画面 JavaScript
 * 作成者         : SRSTaro
 * 改版履歴       : 001, 2018-01-11, SRSTaro, 新規作成
 *******************************************************************/

function onTick() {
    // submit form
    $("form").attr("action", rootPath + "/Wait/Watch");
    $("form").submit();
}


function returnObject(message, processStatus, returnStatus) {
    var obj = new Object();
    obj.message = message;
    if (processStatus === "Completed") { // 完了
        obj.className = "message-normal";
    } else if (processStatus === "WarnList") {
        obj.className = "message-warn";
    } else {
        obj.className = "message-error";
    }
    obj.status = returnStatus;
    returnObjectToParentWindow(obj);
}

$(document).ready(function () {

    var id = $("#ProcessId").val()
    var waiting = $("#Waiting").val()
    var status = $("#ProcessStatus").val()
    var message = $("#Message").val()

    if (status === "ErrorList" || status === "WarnList") {
        // エラーリスト出力の場合
        $("#ErrorList").on("click", function () {
            returnObject(message, status, "Download");
        });
    } else if (waiting === "False") {
        if (status === "Completed") {
            returnObject(message, status, "Completed");
        } else {
            returnObject(message, status, "Error");
        }
    } else if (id !== undefined && id !== "") {
        setTimeout(onTick, 2000);
    }

});
