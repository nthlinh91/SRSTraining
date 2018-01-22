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
'  機能              : 一覧画面 一覧部
'  作成者            : sfunaoka/SDS
'  改版履歴          : 001, 2015-09-15, sfunaoka/SDS, 新規作成
'--------------------------------------------------------------------
*@

@Code
    ' 画面制御処理
    Dim _disabledOther As Boolean = False
    If Model.ValidLevel = ValidLevelType.Fatal Then
        ' 致命的エラー発生時
        _disabledOther = True
    End If
End Code

<div class="content-main">
  <div class="content-section">
    <div class="button-list">
      @Html.SRSButton("Select", "lbl_CMN_DECISION", disabled:=_disabledOther, cssClass:="btn btn-large select-btn")
      @Html.SRSPartial("Shared/_Pager")
    </div>
  </div>
  <div class="content-section">
    <div class="table-list selectable broad">
      <table class="table-list-header">
        <thead>
          <tr>
            <th class="col-select"></th>
            <th class="col-char50">@Html.SRSLabelFor(Function(m) m.ListData(0).Shimukechi)</th>
            <th class="col-char45">@Html.SRSLabelFor(Function(m) m.ListData(0).SaishuShimukechi)</th>
          </tr>
        </thead>
      </table>
      <div class="table-list-body-container">
        <table class="table-list-body">
          <tbody>
            <tr class="table-list-fixed-cols">
              <td class="col-select"></td>
              <td class="col-char50"></td>
              <td class="col-char45"></td>
            </tr>
            @For Each _elementModel In Model.ListData
                ' リストデータの件数分ループ
                ' インデックス取得
                Dim i As Integer = Model.ListData.IndexOf(_elementModel)
                Dim _trCssClass As String = String.Format("table-record{0} table-list-row", (i Mod 2) + 1)
          
                @<tr class="@_trCssClass">
                  <td class="col-select">
                    @Html.SRSSelectRadioButtonFor(Function(m) m.SelectedIndex, i, cssClass:="sel-radio")
                  </td>
                  <td class="item-text">
                    @Html.SRSRaw(Model.ListData(i).Shimukechi)
                    @Html.SRSHiddenFor(Function(m) m.ListData(i).Shimukechi)
                  </td>
                  <td class="item-text">
                    @Html.SRSRaw(Model.ListData(i).SaishuShimukechi)
                    @Html.SRSHiddenFor(Function(m) m.ListData(i).SaishuShimukechi)
                    @Html.SRSHiddenFor(Function(m) m.ListData(i).ShimukechiCd)
                    @Html.SRSHiddenFor(Function(m) m.ListData(i).ShimukechiNm)
                    @Html.SRSHiddenFor(Function(m) m.ListData(i).SaishuShimukechiCd)
                    @Html.SRSHiddenFor(Function(m) m.ListData(i).SaishuShimukechiNm)
                    @Html.SRSHiddenFor(Function(m) m.ListData(i).SaishuShimukechiRnm)
                  </td>
                </tr>
            Next
          </tbody>
        </table>
      </div>
    </div>
  </div>
</div>            







