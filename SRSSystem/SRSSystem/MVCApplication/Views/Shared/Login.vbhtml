@Imports SRS.Standards.MvcFramework.Core.Facade
@Imports SRS.Standards.MvcFramework.Core.MVC
@Imports SRS.Standards.MvcFramework.Core.Utilities
@Imports SRS.Standards.MvcFramework.Core.Information
@Imports SRS.Standards.MvcFramework.Core.Message
@Imports SRS.Standards.MvcFramework.Core.Configuration
@Imports SRS.Standards.MvcFramework.Core.Security
@Imports SRSSystem.Common.Constant
@Imports SRSSystem.Common.Utilities
@ModelType AuthenticationModel
@*
'--------------------------------------------------------------------
'  ユーザ名          : SRS
'  システム名        : トレーニング
'  サブシステム名    : 共通
'  処理名称          : 共通画面
'  機能              : ログイン画面
'  作成者            : SRSTaro
'  改版履歴          : 001, 2018-01-11, SRSTaro, 新規作成
'--------------------------------------------------------------------
*@
@Code
    ' ログイン画面の言語は独自制御
    Dim _model As New SRSSystem.Common.Login.Model.LoginModel
    Dim _culture = _model.LoginCulture

    ' Cultureのコンボボックスを選択状態にする
    Dim _jaSelected = If(_culture.Name = "ja-JP", "selected=""selected""", "")

    ' タイトルを設定
    Model.WindowTitle = MessageManager.GetMessage(_culture, "#- {lbl_CMN_LOGIN} -")
    Model.FuncName = MessageManager.GetMessage(_culture, "#- {lbl_CMN_LOGIN} -")

    Dim _locked As Boolean = False
    If Model.ValidLevel = ValidLevelType.Fatal Then
        _locked = True
    End If
End Code
  <script type="text/javascript">
      $(function () {
          $("#language-select").on("change", function () {
              localStorage["login_id"] = new String($("#UserId").val());
              localStorage["login_pass"] = new String($("#Password").val());
              // 言語をCookieに設定してリロード
              var culture = $(this).val();
              docCookies.setItem("Login_Culture", culture, Infinity, rootPath+'/');
              location.href = rootUrl
          })
          var id = localStorage["login_id"];
          localStorage["login_id"] = "";
          if (!isNullValue(id)) {
              $("#UserId").val(id);
          }

          var pass = localStorage["login_pass"];
          localStorage["login_pass"] = "";
          if (!isNullValue(pass)) {
              $("#Password").val(pass);
          }
      });
  </script>
    @Html.SRSHiddenOnShared(Model.GetPropertyName(Function(m) m.ReturnUrl), Model.ReturnUrl)
    <div class="login-description">
      <span>@MessageManager.GetMessage(_culture, "CMN_E00010")</span>
    </div>
    <div class="login-input-container">
      <div class="login-input-item">
        <span>@Html.SRSLabel(_culture, Model.GetPropertyFullName(Function(m) m.UserId), MessageManager.GetMessage(_culture, "lbl_CMN_LOGINID"))</span>
        <span>@Html.SRSTextBoxFor(Function(m) m.UserId, cssClass:="item-code first-focus", disabled:=_locked)</span>
      </div>
      <div class="login-input-item">
        <span>@Html.SRSLabel(_culture, Model.GetPropertyFullName(Function(m) m.Password), MessageManager.GetMessage(_culture, "lbl_CMN_PASSWORD"))</span>
        <span>@Html.SRSPasswordFor(Function(m) m.Password, cssClass:="item-code", disabled:=_locked)</span>
      </div>

      <div class="login-input-item">
        <span>@Html.SRSLabel(_culture, Model.GetPropertyFullName(Function(m) m.Password), MessageManager.GetMessage(_culture, "lbl_CMN_LANGUAGE"))</span>
        <span>
          <select id="language-select" @(If(_locked, "disabled",""))>
            <option value="en-US">English</option>
            <option value="ja-JP" @_jaSelected >Japanese</option>
          </select>
        </span>
      </div>
    </div>

    <div class="login-submit-button">
      <span>@Html.SRSSubmitButton("Login", MessageManager.GetMessage(_culture, "lbl_CMN_LOGIN"), cssClass:="btn btn-medium", disabled:=_locked, culture:=_culture)</span>
    </div>

    <!-- information messages -->
    <div class="login-information-container">
      <span class="login-information-title">@MessageManager.GetMessage(_culture, "lbl_CMN_INFORMATION"): </span>
      <div class="login-information-content">
        @Code
            Dim _fileName = "Content/login_" & _culture.Name & ".txt"
            Dim _filePath = FileUtilities.MapPath(_fileName)
            Dim _text As String
            Using _reader As New System.IO.StreamReader(_filePath)
                _text = _reader.ReadToEnd()
            End Using
        End Code
        @Html.Raw(_text)
      </div>
    </div>
