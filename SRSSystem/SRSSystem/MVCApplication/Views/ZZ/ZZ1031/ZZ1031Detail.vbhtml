@Imports SRS.Standards.MvcFramework.Core.Utilities
@Imports SRS.Standards.MvcFramework.Core.Facade
@Imports SRS.Standards.MvcFramework.Core.MVC
@ModelType SRSSystem.ZZ.ZZ1031.Model.ZZ1031DetailModel
@*
'--------------------------------------------------------------------
'  ユーザ名          : SRS
'  システム名        : トレーニング
'  サブシステム名    : トレニンーグ機能グループ
'  処理名称          : 納品見積登録
'  機能              : 明細画面
'  作成者            : SRSTaro
'  改版履歴          : 001, 2018-01-11, SRSTaro, 新規作成
'--------------------------------------------------------------------
*@

@Code
    ' 画面表示制御
    Dim _disabledInsUpd As Boolean = True
    Dim _disabledOther As Boolean = False
    If Model.ViewMode = ViewModeType.InsertConfirmed OrElse
       Model.ViewMode = ViewModeType.UpdateConfirmed Then
        _disabledInsUpd = False
    End If
    If Model.ValidLevel = ValidLevelType.Fatal Then
        ' 致命的エラー発生時
        _disabledInsUpd = True
        _disabledOther = True
    End If
End Code

@Section BusinessCss
    <!-- 画面固有CSS -->
    <style>
        .col-total {
            width: 300px;
        }
    </style>

End Section

@Section BusinessScript
    <!-- 画面固有JavaScript -->
    <script type="text/javascript" src="~/Scripts/ZZ/ZZ1031/ZZ1031.js"></script>
End Section

<div class="content-main content-detail">
  <div class="content-section">
    <div class="button-list">
      @Html.SRSSubmitButton("Calculation", "lbl_CMN_CALC", cssClass:="btn btn-medium")
      @Html.SRSSubmitButton("Confirm", "lbl_CMN_CONFIRMATION", disabled:=_disabledOther, cssClass:="btn btn-large")
      @If Model.ViewMode = ViewModeType.Insert OrElse
          Model.ViewMode = ViewModeType.InsertConfirmed Then
          ' 追加時
          @Html.SRSSubmitButton("Insert", "lbl_CMN_REGISTRATION", disabled:=_disabledInsUpd, cssClass:="btn btn-large insert-btn")
      Else
          ' 詳細時
          @Html.SRSSubmitButton("Update", "lbl_CMN_RENEWAL", disabled:=_disabledInsUpd, cssClass:="btn btn-large update-btn")
          @Html.Raw("&nbsp;")
          @Html.SRSSubmitButton("Delete", "lbl_CMN_ELIMINATION", disabled:=_disabledOther, cssClass:="btn btn-large delete-btn")
      End If
      @Html.SRSSubmitButton("Cancel", "lbl_CMN_CANCEL", cssClass:="btn btn-medium")
    </div>
  </div>
  <div class="content-section">
    <table class="table-items">
      <tbody>
        @*TODO:↓↓詳細画面の主キー項目に合わせて修正してください。*@
        <tr>
          <td class="table-item-title table-item-require">@Html.SRSLabelFor(Function(m) m.EstimateNo)</td>
      @If Model.ViewMode = ViewModeType.Insert OrElse
                Model.ViewMode = ViewModeType.InsertConfirmed Then
          ' 追加時
          @<td>@Html.SRSHiddenFor(Function(m) m.EstimateNo)</td>
      Else
          ' 更新時
          @<td>
          @Html.SRSTextBoxFor(Function(m) m.EstimateNo, disabled:=_disabledInsUpd, cssClass:="item-text first-focus")
          </td>
      End If
        </tr>
        @*TODO:↑↑詳細画面の主キー項目に合わせて修正してください。*@
      </tbody>
    </table>
  </div>
  <div class="content-section">
    <table class="table-items">
      <tbody>
        @*TODO:↓↓詳細画面の項目に合わせて修正してください。*@
        <tr>
            <td class="table-item-title table-item-require">@Html.SRSLabelFor(Function(m) m.Item)</td>
            <td>@Html.SRSTextBoxFor(Function(m) m.Item, cssClass:="item-numeric")</td>
        </tr>

          <tr>
              <td class="table-item-title table-item-require">@Html.SRSLabelFor(Function(m) m.FinalDestination)</td>
              <td>@Html.SRSTextBoxFor(Function(m) m.FinalDestination, cssClass:="item-numeric")</td>
              <td>
                  @Html.SRSButton("ReferenceCd-refer-btn", "lbl_CMN_REFERRING", disabled:=_disabledOther, cssClass:="btn btn-smallreferencecd-btn")
                  @Html.SRSTextBoxFor(Function(m) m.ReferenceRnm, disabled:=_disabledOther, cssClass:="item-numeric ReferenceRnm")
              </td>
          </tr>

        <tr>    
          <td class="table-item-title table-item-require">@Html.SRSLabelFor(Function(m) m.ListSet)</td>
          <td>
            @Html.SRSDropDownListFor(Function(m) m.ListSet, Model.ListSetData, optionLabel:="lbl_CMN_CMBSELECT", cssClass:="item-text first-focus")
          </td>
        </tr>

        <tr>
          <td class="table-item-title table-item-require">@Html.SRSLabelFor(Function(m) m.UnitCost)</td>
          <td>@Html.SRSTextBoxFor(Function(m) m.UnitCost, disabled:=_disabledInsUpd, cssClass:="item-text")</td>
        </tr>

          <tr>
              <td class="table-item-title table-item-require">@Html.SRSLabelFor(Function(m) m.EstimateNumber)</td>
              <td>@Html.SRSTextBoxFor(Function(m) m.EstimateNumber, disabled:=_disabledOther, cssClass:="item-text")</td>
          </tr>

          <tr>
              <td class="table-item-title table-item-require">@Html.SRSLabelFor(Function(m) m.Price)</td>
              <td>@Html.SRSTextBoxFor(Function(m) m.Price, disabled:=_disabledInsUpd, cssClass:="item-text")</td>
          </tr>

          <tr>
              <td class="table-item-title table-item-require">@Html.SRSLabelFor(Function(m) m.ListFactory)</td>
              <td>
                  @Html.SRSDropDownListFor(Function(m) m.ListFactory, Model.ListFactoryData, optionLabel:="lbl_CMN_CMBSELECT", cssClass:="item-text")
              </td>
          </tr>

          <tr>
              <td class="table-item-title">@Html.SRSLabelFor(Function(m) m.DeliveryTime)</td>
              <td>@Html.SRSTextBoxFor(Function(m) m.DeliveryTime, cssClass:="item-text")</td>
          </tr>


          <tr>
              <td class="table-item-title">@Html.SRSLabelFor(Function(m) m.Remark)</td>
              <td>@Html.SRSTextBoxFor(Function(m) m.Remark, cssClass:="item-text")</td>
          </tr>

        @*TODO:↑↑詳細画面の項目に合わせて修正してください。*@
      </tbody>
    </table>
  </div>
  @*TODO:↓↓詳細画面の非表示項目に合わせて修正してください。*@
  @Html.SRSHiddenFor(Function(m) m.SearchEstimateNo)
  @Html.SRSHiddenFor(Function(m) m.RowId)
  @Html.SRSHiddenFor(Function(m) m.Revision)
  @*TODO:↑↑詳細画面の非表示項目に合わせて修正してください。*@
</div>
