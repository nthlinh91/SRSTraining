@Imports SRS.Standards.MvcFramework.Core.Facade
@Imports SRS.Standards.MvcFramework.Core.Constant
@Imports SRS.Standards.MvcFramework.Core.MVC
@Imports SRS.Standards.MvcFramework.Core.Utilities
@Imports SRS.Standards.MvcFramework.Core.Information
@Imports SRS.Standards.MvcFramework.Core.Configuration
@Imports SRSSystem.Common.Constant
@ModelType SRSSystem.Common.Wait.Model.WaitModel
@*
'--------------------------------------------------------------------
'  ユーザ名          : SRS
'  システム名        : トレーニング
'  サブシステム名    : 共通
'  処理名称          : 共通画面
'  機能              : 待ち画面
'  作成者            : SRSTaro
'  改版履歴          : 001, 2018-01-11, SRSTaro, 新規作成
'--------------------------------------------------------------------
*@
@Code
    Layout = Nothing
    ' 画面描画用設定
    Dim _cultureInfoName As String = InformationManager.BusinessInfo.CultureInfoName
    Dim _rootUrl As String = String.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"))
    Dim _rootPath As String = Url.Content("~")
    _rootPath = _rootPath.Substring(0, _rootPath.Length - 1)
    Dim _resourceFileName As String = "{0}_{1}.js"
    _resourceFileName = String.Format(_resourceFileName,
                                      ExtensionConfigurationManager.Config.BrowserResourceBaseName.Replace(CommonConstant.LiteralYenMark, CommonConstant.LiteralSlash),
                                      _cultureInfoName)
    _resourceFileName = Url.Content(_resourceFileName)
End Code
<!DOCTYPE html>
<html>
<head>
  <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
  <meta http-equiv="X-UA-Compatible" content="IE=Edge" />

  <meta http-equiv="Expires" content="-1"/>
  <meta http-equiv="Cache-Control" content="no-cache"/>
  <meta http-equiv="Pragma" content="no-cache"/>

  <title>@Html.SRSRaw(Model.WindowTitle)</title>

  <!-- モーダルダイアログ内の遷移は、画面内で行う -->
  <base target="_self" />

  <!-- 共通CSS -->
  @Styles.Render("~/Content/srsCss")

  <!-- 多言語対応用リソースファイル -->
  <!-- (対応言語を増やす場合は設定を追加) -->
  <!-- 必ずJavascriptより先にlinkを記述する(script load時にlinkタグを列挙しているため) -->
  <link rel="localization" hreflang="@_cultureInfoName" href="@_resourceFileName" type="application/vnd.oftn.l10n+json"/>
  <!-- フレームワークのJavaScriptと業務固有のJavaScriptが展開される -->
  @Scripts.Render("~/bundles/modernizr")
  @Scripts.Render("~/bundles/jquery")
  @Scripts.Render("~/bundles/srsScript")
  <!-- 画面固有JavaScript -->
  <script type="text/javascript" src="~/Scripts/wait.js"></script>
  <script type="text/javascript">
        // 多言語対応初期化
        String.defaultLocale = "@_cultureInfoName";
        if (location.search) {
            String.locale = location.search.substr(1);
        }
        var l = function (string) {
            return string.toLocaleString();
        };
        var rootUrl = "@_rootUrl";
        var rootPath = "@_rootPath";
  </script>

</head>
<body>

<div class="fill-container">
  <div class="fill-contents">
    @Html.SRSRaw(Model.DisplayMessage)
  </div>
  <div class="fill-command-area">
    @Using Html.SRSBeginForm()
        @Html.SRSHiddenFor(Function(m) m.ProcessId)
        @Html.SRSHiddenFor(Function(m) m.ProcessStatus)
        @Html.SRSHiddenFor(Function(m) m.Waiting)
        @Html.SRSHiddenFor(Function(m) m.Message)
        @Html.SRSHiddenFor(Function(m) m.DisplayMessage)
        @If Model.ProcessStatus = SRSSystem.Common.Wait.Constant.ProcessStatus.ErrorList OrElse
            Model.ProcessStatus = SRSSystem.Common.Wait.Constant.ProcessStatus.WarnList Then
            '' エラーリストが存在するときのみ表示
            @Html.SRSButton("ErrorList", "lbl_CMN_ERRORLIST", cssClass:="btn btn-large")
        End If
    End Using
  </div>
</div>
</body>
</html>
