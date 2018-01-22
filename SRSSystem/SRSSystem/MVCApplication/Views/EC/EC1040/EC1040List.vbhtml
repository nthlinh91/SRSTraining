@Imports SRS.Standards.MvcFramework.Core.Facade
@Imports SRS.Standards.MvcFramework.Core.MVC
@Imports SRS.Standards.MvcFramework.Core.Utilities
@ModelType JUST.EC.EC1040.Model.EC1040ListModel
@*
'--------------------------------------------------------------------
'  ユーザ名          : 住友ゴム工業株式会社
'  システム名        : タイヤ海外業務基幹システム（JUST）
'  サブシステム名    : 共通
'  処理名称          : 最終仕向地検索
'  機能              : 一覧画面
'  作成者            : sfunaoka/SDS
'  改版履歴          : 001, 2015-09-15, sfunaoka/SDS, 新規作成
'--------------------------------------------------------------------
*@

@Code
    ' 画面制御処理
End Code

@Section BusinessCss
    <!-- 画面固有CSS -->
    <link rel="stylesheet" href="~/Content/EC/EC1040/EC1040.css" />
End Section

@Section BusinessScript
    <!-- 画面固有JavaScript -->
    <script type="text/javascript" src="~/Scripts/EC/EC1040/EC1040.js"></script>
End Section

<div class="content-header">
  <div class="content-section content-condition">
    <table class="table-items">
      <tbody>
        <tr>
          <td class="table-item-title table-item-require">@Html.SRSLabelFor(Function(m) m.ShimukechiCdCondition)</td>
          <td>
            @Html.SRSTextBoxFor(Function(m) m.ShimukechiCdCondition, maxLength:=7, cssClass:="item-text first-focus text-size7")
          </td>
        </tr>
        <tr>
          <td class="table-item-title">@Html.SRSLabelFor(Function(m) m.SaichushimukechiNmCondition)</td>
          <td>
            @Html.SRSTextBoxFor(Function(m) m.SaichushimukechiNmCondition, maxLength:=40, cssClass:="item-text text-size40")
          </td>
        </tr>
      </tbody>
    </table>
  </div>
  <div class="content-section">
    <div class="button-list">
      @Html.SRSSubmitButton("Search", "lbl_CMN_SEARCH", cssClass:="btn btn-large")
    </div>
  </div>
  @Html.SRSHiddenFor(Function(m) m.ShimukechiCdConditionSaved)
  @Html.SRSHiddenFor(Function(m) m.SaichushimukechiNmConditionSaved)
</div>

@If Model.ViewMode = ViewModeType.Searched Then
    If Model.ListData IsNot Nothing AndAlso Model.ListData.Count > 0 Then
        ' 検索後の場合、リスト部を出力
  @<hr />
  @Html.SRSPartial("EC/EC1040/EC1040ListBody")
    End If
End If
