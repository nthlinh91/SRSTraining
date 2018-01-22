Imports SRS.Standards.MvcFramework.Core.MVC
Imports SRS.Standards.MvcFramework.Core.Facade
Imports SRS.Standards.MvcFramework.Core.Information
Imports SRS.Standards.MvcFramework.Core.Security
Imports SRSSystem.Common.Menu

Namespace Controllers

    ''' <userName>SRS</userName>
    ''' <sysName>トレーニング</sysName>
    ''' <subSysName>SRS ASP.NET MVC アプリケーション</subSysName>
    ''' <summary>
    ''' メニュー画面コントローラ
    ''' </summary>
    ''' <author>SRSTaro</author>
    ''' <Version>
    ''' 001, 2018-01-11, 新規作成
    ''' </Version>
    Public Class MenuController
        Inherits ControllerBase(Of Business.MenuFacade, Model.MenuModel)

        ''' <summary>
        ''' 初期処理
        ''' </summary>
        ''' <returns>ActionResult</returns>
        ''' <permission>View(Model)の結果を返します</permission>
        Function Index() As ActionResult

            ' 初期化処理を実行
            Dim _menuModel = Me.Facade.Init

            Return View(_menuModel)

        End Function

        ''' <summary>
        ''' ログアウトを行います。
        ''' </summary>
        ''' <returns>ActionResult</returns>
        Shadows Function Logout() As ActionResult

            MyBase.Logout()

            Dim _model As New Model.MenuModel
            _model.JudgeTrans = JudgeTransType.LoginRedirect

            Return View(_model)

        End Function

    End Class

End Namespace
