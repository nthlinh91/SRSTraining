Imports SRS.Standards.MvcFramework.Core.MVC
Imports SRS.Standards.MvcFramework.Core.Facade
Imports SRS.Standards.MvcFramework.Core.Information
Imports SRS.Standards.MvcFramework.Core.Security
Imports SRSSystem.Common.Wait

Namespace Controllers

    ''' <userName>SRS</userName>
    ''' <sysName>トレーニング</sysName>
    ''' <subSysName>SRS ASP.NET MVC アプリケーション</subSysName>
    ''' <summary>
    ''' 待ち画面コントローラ
    ''' </summary>
    ''' <author>SRSTaro</author>
    ''' <Version>
    ''' 001, 2018-01-11, 新規作成
    ''' </Version>
    Public Class WaitController
        Inherits ControllerBase(Of Business.WaitFacade, Model.WaitModel)

        ''' <summary>
        ''' 初期処理
        ''' </summary>
        ''' <param name="id">処理ID (URLから指定)</param>
        ''' <returns>ActionResult</returns>
        ''' <permission>View(Model)の結果を返します</permission>
        Function Index(id As String) As ActionResult

            ' 初期化を実行
            Dim _facade As New Business.WaitFacade()
            Dim _waitModel = _facade.Init(id)

            Return View(_waitModel)

        End Function

        ''' <summary>
        ''' 監視処理
        ''' </summary>
        ''' <param name="waitModel">待ち画面モデル</param>
        ''' <returns>ActionResult</returns>
        ''' <permission>View(Model)の結果を返します</permission>
        Function Watch(waitModel As Model.WaitModel) As ActionResult

            Dim _facade As New Business.WaitFacade()
            _facade.Watch(waitModel)

            Return View(waitModel)

        End Function

    End Class

End Namespace
