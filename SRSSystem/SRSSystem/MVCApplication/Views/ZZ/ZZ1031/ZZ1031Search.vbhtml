@Imports SRS.Standards.MvcFramework.Core.Utilities
@Imports SRS.Standards.MvcFramework.Core.Facade
@Imports SRS.Standards.MvcFramework.Core.MVC
@ModelType SRSSystem.ZZ.ZZ1031.Model.ZZ1031SearchModel
@*
'--------------------------------------------------------------------
'  ユーザ名          : SRS
'  システム名        : トレーニング
'  サブシステム名    : トレニンーグ機能グループ
'  処理名称          : 納品見積登録
'  機能              : 検索画面
'  作成者            : SRSTaro
'  改版履歴          : 001, 2018-01-11, SRSTaro, 新規作成
'--------------------------------------------------------------------
*@

@Code
    ' 画面制御処理
End Code

@Section BusinessCss
    <!-- 画面固有CSS -->
    <style>
        .col-estimateno{
            width: 260px;
        }
    </style>
End Section

@Section BusinessScript
    <!-- 画面固有JavaScript -->
    <script type="text/javascript" src="~/Scripts/ZZ/ZZ1031/ZZ1031.js"></script>
End Section

<div class="content-header">
  <div class="content-section content-condition">
    <table class="table-items">
      <tbody>
        @*TODO:↓↓検索項目に合わせて修正してください。*@
        <tr>
          <td class="table-item-title">@Html.SRSLabelFor(Function(m) m.EstimateNoCondition)</td>
          <td>
            @Html.SRSTextBoxFor(Function(m) m.EstimateNoCondition, cssClass:="item-text")
          </td>
        </tr>
        @*TODO:↑↑検索項目に合わせて修正してください。*@
      </tbody>
    </table>
  </div>
  <div class="content-section">
    <div class="button-list">
      @Html.SRSSubmitButton("Search", "lbl_CMN_SEARCH", cssClass:="btn btn-large")
      @Html.SRSSubmitButton("Add", "lbl_CMN_ADDITION", cssClass:="btn btn-large")
    </div>
  </div>
  @*TODO:↓↓検索画面の非表示項目に合わせて修正してください。*@
  @*TODO:↑↑検索画面の非表示項目に合わせて修正してください。*@
</div>
