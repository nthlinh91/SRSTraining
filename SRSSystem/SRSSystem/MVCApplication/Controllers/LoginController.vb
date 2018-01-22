Imports SRS.Standards.MvcFramework.Core.Message
Imports SRS.Standards.MvcFramework.Core.Configuration
Imports SRS.Standards.MvcFramework.Core.Information
Imports SRS.Standards.MvcFramework.Core.Facade
Imports SRS.Standards.MvcFramework.Core.Logging
Imports SRS.Standards.MvcFramework.Core.Security
Imports SRS.Standards.MvcFramework.Core.MVC

Imports SRSSystem.Common.Login

Namespace Controllers

    ''' <userName>SRS</userName>
    ''' <sysName>トレーニング</sysName>
    ''' <subSysName>SRS ASP.NET MVC アプリケーション</subSysName>
    ''' <summary>
    ''' ログイン画面コントローラ
    ''' </summary>
    ''' <author>SRSTaro</author>
    ''' <Version>
    ''' 001, 2018-01-11, 新規作成
    ''' </Version>
    <AllowAnonymous()>
    Public Class LoginController
        Inherits ControllerBase(Of Business.LoginFacade, AuthenticationModel)

        ''' <summary>
        ''' 初期処理
        ''' </summary>
        ''' <param name="model">認証モデル</param>
        ''' <returns>ActionResult</returns>
        ''' <permission>View(Model)の結果を返します</permission>
        Function Index(model As AuthenticationModel) As ActionResult

            Return View(model)

        End Function

        ''' <summary>
        ''' 日本語切替(参考用)
        ''' </summary>
        ''' <param name="model">ログイン画面モデル</param>
        ''' <returns>ActionResult</returns>
        ''' <permission>View(Model)の結果を返します</permission>
        Function JP(model As AuthenticationModel) As ActionResult

            Me.SetCulture("ja-JP")
            Return View(model)

        End Function

        ''' <summary>
        ''' 英語切替(参考用)
        ''' </summary>
        ''' <param name="model">ログイン画面モデル</param>
        ''' <returns>ActionResult</returns>
        ''' <permission>View(Model)の結果を返します</permission>
        Function EN(model As AuthenticationModel) As ActionResult

            Me.SetCulture("en-US")
            Return View(model)

        End Function

        ''' <summary>
        ''' リソースのリロードを行います。
        ''' </summary>
        ''' <returns>ActionResult</returns>
        Function Reload() As ActionResult

            SRS.Standards.MvcFramework.Core.Message.MessageManager.Reload()
            Dim _model As New AuthenticationModel
            Return View(_model)

        End Function

    End Class

End Namespace
