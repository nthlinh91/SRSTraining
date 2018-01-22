Imports System.Web
Imports System.Web.Mvc

''' <userName>SRS</userName>
''' <sysName>トレーニング</sysName>
''' <subSysName>SRS ASP.NET MVC アプリケーション</subSysName>
''' <summary>
''' コントローラのフィルタ設定
''' </summary>
''' <author>SRSTaro</author>
''' <Version>
''' 001, 2018-01-11, 新規作成
''' </Version>
Public Class FilterConfig
    ''' <summary>
    ''' フィルター情報を登録する
    ''' </summary>
    ''' <param name="filters">フィルター情報</param>
    Public Shared Sub RegisterGlobalFilters(filters As GlobalFilterCollection)
        filters.Add(New HandleErrorAttribute())
        filters.Add(New RegulationCheckAttribute())
        filters.Add(New EnablePermissionAttribute())
        filters.Add(New ActivityCountAttribute())
    End Sub
End Class
