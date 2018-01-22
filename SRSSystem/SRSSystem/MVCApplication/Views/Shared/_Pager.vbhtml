@Imports SRS.Standards.MvcFramework.Core.MVC
@Imports SRS.Standards.MvcFramework.Core.Facade
@Imports SRSSystem.Common.Constant
@ModelType SRS.Standards.MvcFramework.Core.MVC.ViewPagerModelBase
@*
'--------------------------------------------------------------------
'  ユーザ名          : SRS
'  システム名        : トレーニング
'  サブシステム名    : 共通
'  処理名称          : 共通画面
'  機能              : ページング処理用コントロール
'  作成者            : SRSTaro
'  改版履歴          : 001, 2018-01-11, SRSTaro, 新規作成
'--------------------------------------------------------------------
*@
@Code
    ' ページ選択用のリストボックス生成
    Model.ExecutePageList()
    ' 前ページ・次ページ押下可否制御
    Dim _disabledPrevious As Boolean = False
    If Model.PageNum <= 1 Then
        _disabledPrevious = True
    End If
    Dim _disabledNext As Boolean = False
    If Model.PageNum >= Model.MaxPage Then
        _disabledNext = True
    End If
    Dim _disabledOther As Boolean = False
    If Model.ValidLevel = ValidLevelType.Fatal Then
        ' 致命的エラー発生時は全て操作不可
        _disabledPrevious = True
        _disabledNext = True
        _disabledOther = True
    End If

End Code
<!-- ページャー部分ビュー -->   
<script type="text/javascript">
    //前へ次へボタン非活性処理
    $(document).ready(function () {
        // ページドロップダウンリスト変更時POST処理 
        $("#SelectPageNum").on("change", function () {
            $("#SelectPage").click();
            // submit後は値を元に戻す(キャンセル対応)
            $(this).val(selectVal);
        });
        // 描画時の値を保持
        var selectVal = $("#SelectPageNum").val();
    });
</script>
<div class="paging-control">
  <!-- ページングエリア -->
  <div class="paging-control">
    @Html.SRSSubmitButton("PreviousPage", "lbl_CMN_PREVIOUS", disabled:=_disabledPrevious, cssClass:="btn btn-medium btn-previous")
    @Html.SRSSubmitButton("NextPage", "lbl_CMN_NEXT", disabled:=_disabledNext, cssClass:="btn btn-medium btn-next")
    @Html.SRSHiddenFor(Function(e) e.PageNum)
    @Html.SRSDropDownListFor(Function(m) m.SelectPageNum, Model.PageLists, disabled:=_disabledOther, cssClass:="ddl-paging")
    @Html.SRSRaw("/")
    @Html.SRSRaw(Model.MaxPage)
    @Html.SRSHiddenFor(Function(e) e.MaxPage)
    @Html.SRSSubmitButton("SelectPage", String.Empty, disabled:=_disabledOther, cssClass:="btn btn-medium hide btn-selectpage")
  </div>
</div>
