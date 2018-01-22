@Imports SRS.Standards.MvcFramework.Core.Configuration
@Imports SRS.Standards.MvcFramework.Core.Facade
@Imports SRS.Standards.MvcFramework.Core.Information
@Imports SRS.Standards.MvcFramework.Core.Message
@Imports SRS.Standards.MvcFramework.Core.MVC
@Imports SRS.Standards.MvcFramework.Core.Security
@Imports SRS.Standards.MvcFramework.Core.Utilities
@Imports SRSSystem.Common.Constant
@ModelType SRS.Standards.MvcFramework.Core.Facade.IViewModel
@*
'--------------------------------------------------------------------
'  ユーザ名          : SRS
'  システム名        : トレーニング
'  サブシステム名    : 共通
'  処理名称          : 共通画面
'  機能              : 共通画面レイアウト
'  作成者            : SRSTaro
'  改版履歴          : 001, 2018-01-11, SRSTaro, 新規作成
'--------------------------------------------------------------------
*@
@Code
    ' 画面描画用設定
    Dim _request = HttpContext.Current.Request
    Dim _rootUrl As String = String.Format("{0}://{1}{2}", _request.Url.Scheme, _request.Url.Authority, Url.Content("~"))
    Dim _rootPath As String = Url.Content("~")
    _rootPath = _rootPath.Substring(0, _rootPath.Length - 1)
    Dim _cultureInfoName As String = If(Session Is Nothing, "en-US", InformationManager.BusinessInfo.CultureInfoName)
    Dim _resourceFileName As String = "{0}_{1}.js"
    _resourceFileName = String.Format(_resourceFileName,
                                      ExtensionConfigurationManager.Config.BrowserResourceBaseName.Replace(CommonConstant.LiteralYenMark, CommonConstant.LiteralSlash),
                                      _cultureInfoName)
    _resourceFileName = Url.Content(_resourceFileName)
    Dim _titlePrefix As String = String.Empty
    If Not String.IsNullOrWhiteSpace(InformationManager.AppInfo.MenuId) Then
        _titlePrefix = InformationManager.AppInfo.MenuId & ":"
    End If
End Code
<!DOCTYPE html>
<html>
<head>
  <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
  <meta http-equiv="X-UA-Compatible" content="IE=Edge" />

  <meta http-equiv="Expires" content="-1"/>
  <meta http-equiv="Cache-Control" content="no-cache"/>
  <meta http-equiv="Pragma" content="no-cache"/>

  <title>
    @_titlePrefix
    @(If(Session Is Nothing, Model.WindowTitle, Html.SRSRaw(Model.WindowTitle)))
  </title>

  <!-- モーダルダイアログ内の遷移は、画面内で行う -->
  <base target="_self" />

  <!-- 共通CSS -->
  @Styles.Render("~/Content/themes/base/css")
  @Styles.Render("~/Content/srsCss")
  @* 画面固有CSS *@
  @RenderSection("BusinessCss", required:=False)

  <!-- 多言語対応用リソースファイル -->
  <!-- (対応言語を増やす場合は設定を追加) -->
  <!-- 必ずJavascriptより先にlinkを記述する(script load時にlinkタグを列挙しているため) -->
  <link rel="localization" hreflang="@_cultureInfoName" href="@_resourceFileName" type="application/vnd.oftn.l10n+json"/>
  <!-- フレームワークのJavaScriptと業務固有のJavaScriptが展開される -->
  @Scripts.Render("~/bundles/modernizr")
  @Scripts.Render("~/bundles/jquery")
  @Scripts.Render("~/bundles/jqueryui")
  @Scripts.Render("~/bundles/srsScript")
  @RenderSection("BusinessScript", required:=False)
  <script type="text/javascript">
      $(function () {
          // Submitボタン押下時制御
          $("input[type=submit], button[type=submit]").on("click", function () {
              $("form").attr("action", $(this).attr("action"));
              return;
          });
          // inputでのenter押下で、送信されるのを抑制する
          $("input[type!=submit][type!=button][type!=hidden]").keypress(function (ev) {
              if ((ev.which && ev.which === 13) ||
                  (ev.keyCode && ev.keyCode === 13)) {
                  return false;
              } else {
                  return true;
              }
          });
          // フォーム送信開始時、全てのsubmitボタンを無効にする
          $("form").on("submit", function () {
              $("input[type=submit], button[type=submit]").attr("disabled", "disabled");
          });
          // 閉じるボタン押下時
          $("input.close-btn").on("click", function clickOnClose() {
              fncWinClose();
          });
          // ログアウトボタン押下時
          $("input.logout-btn").on("click", function clickOnLogout() {
              fncAllClose();
              //return confirm("logout?");
          });
          // フォーカス処理
          var errors = $("input.input-validation-error:visible, select.input-validation-error:visible, textarea.input-validation-error:visible");
          if (errors.length > 0) {
              errors.first().focus();
          } else {
              $(".first-focus:visible").first().focus();
          }
      });

      // 多言語対応初期化
      String.defaultLocale = "@_cultureInfoName";
      if (location.search) {
          String.locale = location.search.substr(1);
      }
      var l = function (arg) {
          var args = arguments;
          if (args.length === 1) {
              // 1引数の場合
              return arg.toLocaleString();
          } else {
              // 複数引数だった場合
              var rep_fn = function (m, k) { return args[parseInt(k) + 1].toLocaleString(); }
              return args[0].toLocaleString().replace(/\{(\w+)\}/g, rep_fn);
          }
      };
      // ユーザ設定 (小数点記号、数値区切り)
      var decimalPoint = ".";
      var numberDelimiter = ",";

      // 絶対パス生成用
      var rootUrl = "@_rootUrl";
      var rootPath = "@_rootPath";
  </script>
  @* アイコンを指定する場合 *@
  @*<link rel="icon" type="image/vnd.microsoft.icon" href="~/Content/favicon.ico" />*@

</head>
<body>
@Using Html.SRSBeginForm()
@<div class="page-header">
  <div class="page-header-content">
    <h1 class="page-header-title">
        @_titlePrefix
        @(If(Session Is Nothing, Model.FuncName, Html.SRSRaw(Model.FuncName)))
    </h1>
    @* ログインページ以外で、ユーザ情報等を見せる *@
    @If Not TypeOf Model Is AuthenticationModel Then
        @<div class="page-header-detail">
          <span class="page-header-detail-info">@(Date.Now.ToString("yyyy/MM/dd"))　@(InformationManager.UserInfo.UserId)：@(InformationManager.UserInfo.UserName)</span>
          <div class="button-list">
          @If TypeOf Model Is SRSSystem.Common.Menu.Model.MenuModel Then
            @Html.SRSSubmitButton("Logout", "lbl_CMN_LOGOUT", cssClass:="btn btn-large update-medium logout-btn", tabIndex:=-1)
          Else
            @Html.SRSButton("Close", "lbl_CMN_CLOSE", cssClass:="btn btn-large update-medium close-btn", tabIndex:=-1)
          End If
          </div>
        </div>
    End If
  </div>
</div>
@<div class="page-body">
    @Html.SRSHiddenOnShared(Model.GetPropertyName(Function(m) m.ViewMode), Model.ViewMode)
    @RenderBody()
</div>
End Using
<div class="page-footer">
  <div class="page-footer-content">
    @Code
        Dim _messageCss As String = "page-footer-message"
        If Model.ValidLevel = ValidLevelType.Error OrElse
           Model.ValidLevel = ValidLevelType.Fatal Then
            ' エラー時、エラー用のクラスを追加
            _messageCss += " message-error"
        ElseIf Model.ValidLevel = ValidLevelType.Warn Then
            ' 警告時、警告用のクラスを追加
            _messageCss += " message-warn"
        End If

        Dim _message = DBNullToEmptyString(If(Session Is Nothing,
                                              MessageManager.GetMessage(System.Globalization.CultureInfo.InvariantCulture, Model.Message, Model.MessageParams),
                                              MessageManager.GetMessage(Model.Message, Model.MessageParams)))
        ' 改行タグは削除する
        _message = _message.Replace("<br ?/?>", "", RegexOptions.IgnoreCase)
        Dim _lines = _message.Split(vbNewLine)
        Dim _firstLine = _lines(0)

        Dim _additionalClass = ""
        If _lines Is Nothing OrElse _lines.Count <= 1 Then
            _additionalClass = " hide"
        End If
    End Code
    @Html.SRSButton("__MessagePopupButton", "lbl_CC0020_MESSAGEPOPUP", cssClass:="btn" & _additionalClass, tabIndex:=-1, culture:=If(Session Is Nothing, System.Globalization.CultureInfo.InvariantCulture ,Nothing))
    <span class="@_messageCss">
      @Html.Raw(_firstLine)
    </span>
  </div>
</div>
@* 子画面ダイアログ用div要素 *@
<div id="frame-window" class="frame-window hide"></div>
@* ツールチップ用の要素 (普段は非表示) *@
<div id="__tooltip" class="list-tooltip"></div>
@* メッセージ用の要素 (普段は非表示) *@
<div class="message-tooltip" id="__message-tooltip">
  <span id="__message-tooltip-text">@_message</span>
</div>
</body>
</html>
