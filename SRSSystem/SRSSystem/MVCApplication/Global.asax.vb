' メモ: IIS6 または IIS7 のクラシック モードの詳細については、
' http://go.microsoft.com/?LinkId=9394802 を参照してください
Imports System.Web.Http
Imports System.Web.Optimization
Imports System.Runtime.Remoting.Messaging

Imports SRS.Standards.MvcFramework.Core.Logging
Imports SRS.Standards.MvcFramework.Core.Configuration
Imports SRS.Standards.MvcFramework.Core.Information
Imports SRS.Standards.MvcFramework.Core.MVC

''' <userName>SRS</userName>
''' <sysName>トレーニング</sysName>
''' <subSysName>SRS ASP.NET MVC アプリケーション</subSysName>
''' <summary>
''' MVCアプリケーション共通処理
''' </summary>
''' <author>SRSTaro</author>
''' <Version>
''' 001, 2018-01-11, 新規作成
''' </Version>
Public Class MvcApplication
    Inherits System.Web.HttpApplication

    ''' <summary>
    ''' アプリリケーション開始時イベント
    ''' </summary>
    ''' <remarks>アプリケーション開始時のみ実行</remarks>
    Sub Application_Start()
        AreaRegistration.RegisterAllAreas()

        WebApiConfig.Register(GlobalConfiguration.Configuration)
        FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters)
        RouteConfig.RegisterRoutes(RouteTable.Routes)
        BundleConfig.RegisterBundles(BundleTable.Bundles)

        ' 拡張したModelバインダを追加
        ModelBinders.Binders.Add(GetType(SRSHttpPostedFile), New SRSHttpPostedFileModelBinder())

        ' 拡張したRazorViewEngineを追加
        ViewEngines.Engines.Clear()
        ViewEngines.Engines.Add(New ExtendedRazorViewEngine)

        ' 拡張したControllerFactoryを追加
        ControllerBuilder.Current.SetControllerFactory(GetType(ExtendedControllerFactory))

        ' プレロード情報生成
        InformationManager.PreloadInfo = New PreloadInformation
        ' リソースファイル定義
        InformationManager.PreloadInfo.ResourceServerPath = Server.MapPath(ExtensionConfigurationManager.Config.ResourceBaseName)
        ' アップロードパス定義
        If System.IO.Directory.Exists(ExtensionConfigurationManager.Config.UploadTempPath) Then
            ' 指定されたパスが存在する場合はそのまま使用
            InformationManager.PreloadInfo.UploadTempPath = ExtensionConfigurationManager.Config.UploadTempPath
        Else
            ' パスが存在しない場合は仮想パスにして使用
            InformationManager.PreloadInfo.UploadTempPath = Server.MapPath(ExtensionConfigurationManager.Config.UploadTempPath)
        End If

        ' ユーザ設定を読み込み
        ExtensionConfigurationManager.InitializeUserConfig(Of Common.Utilities.UserConfig)(Common.Constant.CommonConstant.UserConfigSectionName)

    End Sub

    ''' <summary>
    ''' セッション開始時イベント
    ''' </summary>
    ''' <param name="sender">イベントオブジェクト</param>
    ''' <param name="e">イベント情報</param>
    Private Sub Session_Start(sender As Object, e As System.EventArgs)
        ' セッション開始時処理
        ' 共通情報(セッション格納分)を生成
        InformationManager.UserInfo = New UserInformation
        InformationManager.TransferInfo = New TransferInformation
        InformationManager.BusinessInfo = New BusinessInformation
    End Sub

    ''' <summary>
    ''' 例外発生時処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>例外発生時実行(Facade実行手前)</remarks>
    Private Sub MvcApplication_Error(sender As Object, e As System.EventArgs) Handles Me.Error
        ' 例外ログを出力
        LogManager.ExceptionLog.Log(Me.Server.GetLastError())
    End Sub

End Class
