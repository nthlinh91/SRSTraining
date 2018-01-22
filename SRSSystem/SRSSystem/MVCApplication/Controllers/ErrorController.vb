Namespace Controllers

    ''' <userName>SRS</userName>
    ''' <sysName>トレーニング</sysName>
    ''' <subSysName>SRS ASP.NET MVC アプリケーション</subSysName>
    ''' <summary>
    ''' エラー画面コントローラ
    ''' </summary>
    ''' <author>SRSTaro</author>
    ''' <Version>
    ''' 001, 2018-01-11, 新規作成
    ''' </Version>
    Public Class ErrorController
        Inherits Controller

        ''' <summary>
        ''' 初期処理
        ''' </summary>
        ''' <returns>ActionResult</returns>
        ''' <permission>View(Model)の結果を返します</permission>
        Function Index() As ActionResult

            Return View("Error")

        End Function

    End Class

End Namespace
