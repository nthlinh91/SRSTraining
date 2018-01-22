Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.Mvc
Imports System.Web.Routing

''' <userName>SRS</userName>
''' <sysName>トレーニング</sysName>
''' <subSysName>SRS ASP.NET MVC アプリケーション</subSysName>
''' <summary>
''' コントローラのルーティング設定
''' </summary>
''' <author>SRSTaro</author>
''' <Version>
''' 001, 2018-01-11, 新規作成
''' </Version>
Public Class RouteConfig
    ''' <summary>
    ''' ルーティングを登録する
    ''' </summary>
    ''' <param name="routes">ルート情報</param>
    Public Shared Sub RegisterRoutes(routes As RouteCollection)
        routes.IgnoreRoute("{resource}.axd/{*pathInfo}")
        routes.IgnoreRoute("favicon.ico")

        ' 追加順＝優先順位の為注意
        routes.MapRoute(
            name:="Business",
            url:="{group}/{menuId}/{controller}/{action}/{param}",
            defaults:=New With {.action = "Index", .param = UrlParameter.Optional}
        )

        routes.MapRoute(
            name:="Common",
            url:="{controller}/{action}/{param}",
            defaults:=New With {.controller = "Login", .action = "Index", .param = UrlParameter.Optional}
        )

        'routes.MapRoute(
        '    name:="Default",
        '    url:="{controller}/{action}/{id}",
        '    defaults:=New With {.controller = "Home", .action = "Index", .id = UrlParameter.Optional}
        ')


    End Sub

End Class

