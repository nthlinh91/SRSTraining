Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web.Http

Imports SRS.Standards.MvcFramework.Core.Http.WebHost

''' <userName>SRS</userName>
''' <sysName>トレーニング</sysName>
''' <subSysName>SRS ASP.NET MVC アプリケーション</subSysName>
''' <summary>
''' WebAPIのルート設定
''' </summary>
''' <author>SRSTaro</author>
''' <Version>
''' 001, 2018-01-11, 新規作成
''' </Version>
Public Class WebApiConfig
    ''' <summary>
    ''' ルート設定登録
    ''' </summary>
    ''' <param name="config">設定</param>
    Public Shared Sub Register(config As HttpConfiguration)

        'config.Routes.MapHttpRoute(
        '    name:="BusinessApi",
        '    routeTemplate:="api/{group}/{menuId}/{controller}/{id}",
        '    defaults:=New With {.id = RouteParameter.Optional}
        ')

        'config.Routes.MapHttpRoute(
        '    name:="DefaultApi",
        '    routeTemplate:="api/{controller}/{id}",
        '    defaults:=New With {.id = RouteParameter.Optional}
        ')

        Dim _handler As New SessionHttpControllerRouteHandler(config)

        '' SessionをサポートするAPIとしてルーティングする
        RouteTable.Routes.MapHttpRoute(
            name:="BusinessApi",
            routeTemplate:="api/{group}/{menuId}/{controller}/{id}",
            defaults:=New With {.id = RouteParameter.Optional}
        ).RouteHandler = _handler

        RouteTable.Routes.MapHttpRoute(
            name:="DefaultApi",
            routeTemplate:="api/{controller}/{id}",
            defaults:=New With {.id = RouteParameter.Optional}
        ).RouteHandler = _handler

        config.Filters.Add(New SRS.Standards.MvcFramework.Core.Http.ApiExecutionFilterAttribute())
        config.Filters.Add(New ApiRegulationCheckAttribute())
        config.Filters.Add(New ApiEnablePermissionAttribute())

    End Sub
End Class
