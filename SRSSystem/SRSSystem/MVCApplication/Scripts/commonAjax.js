/********************************************************************
 * ユーザ名       : SRS
 * システム名     : トレーニング
 * サブシステム名 : 共通
 * 処理名称       : 共通JavaScript関数
 * 機能           : 共通JavaScript関数 AJAX関連
 * 作成者         : SRSTaro
 * 改版履歴       : 001, 2018-01-11, SRSTaro, 新規作成
 *******************************************************************/

//***************************************************************************
/**
* AJAXでデータセレクトプロパティ情報を取得します。
* @name    データセレクト名称
* @params  パラメータ。文字列配列の配列で指定。(例: [['KEY1','VALUE1], ['KEY2', 'VALUE2']]
* @return  データセレクトデータ
*/
//***************************************************************************
function getDataSelectItemAjax(name, params) {
    var param = {
        DataSelectName: name,
        Parameters: params
    };

    return $.ajax({
        url: rootPath + '/api/DataSelect',
        type: "POST",
        data: param
    });
}

//***************************************************************************
/**
* 取得したデータセレクトプロパティ情報を、指定したコンボボックスに設定する。
* @selector 設定するコンボボックスのjQueryセレクタ。
* @data     データセレクトデータ。
* @return   なし
*/
//***************************************************************************
function setItemDataToComboBox(selector, data) {
    var select = $(selector);
    $(selector + ' > option').each(function () {
        var val = $(this).attr('value');
        if ($(this).attr('value') !== '') { $(this).remove(); }
    });
    $.each(data, function (idx, item) {
        var option = $('<option>').val(item.Value).text(item.Text);
        select.append(option);
    });
}
