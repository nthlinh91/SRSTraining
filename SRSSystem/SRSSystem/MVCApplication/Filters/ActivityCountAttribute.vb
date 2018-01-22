Imports SRS.Standards.MvcFramework.Core.Configuration
Imports SRS.Standards.MvcFramework.Core.Facade
Imports SRS.Standards.MvcFramework.Core.Logging
Imports SRS.Standards.MvcFramework.Core.MVC
Imports SRS.Standards.MvcFramework.Core.Utilities
Imports SRSSystem.Common.Utilities

''' <userName>SRS</userName>
''' <sysName>トレーニング</sysName>
''' <subSysName>SRS ASP.NET MVC アプリケーション</subSysName>
''' <summary>
''' コントローラに対する、Indexメソッドアクセスをユーザ毎にカウントするフィルタ
''' </summary>
''' <author>SRSTaro</author>
''' <Version>
''' 001, 2018-01-11, 新規作成
''' </Version>
Public Class ActivityCountAttribute
    Inherits ActionFilterAttribute

    ''' <summary>
    ''' アクセスカウント処理
    ''' </summary>
    ''' <param name="filterContext">呼出時情報</param>
    Public Overrides Sub OnActionExecuted(filterContext As System.Web.Mvc.ActionExecutedContext)

        If Not filterContext.RouteData.Values.Keys.Contains("menuId") OrElse
            Not filterContext.RouteData.Values.Keys.Contains("action") Then
            ' メニューIDが存在しない場合はチェック不要
            Return
        End If

        If filterContext.Controller Is Nothing OrElse filterContext.Controller.ViewData Is Nothing OrElse
            filterContext.Controller.ViewData.Model Is Nothing Then
            Return
        End If

        Dim _method = DirectCast(filterContext.RouteData.Values("action"), String)

        Try
            ' 新しくウィンドウを開くときはRefererがない Or Menu からになるので、それを利用する
            If _method = "Index" Then

                Dim _referer = filterContext.HttpContext.Request.UrlReferrer
                Dim _refererUrl = If(_referer Is Nothing, "", _referer.AbsoluteUri)
                If _referer Is Nothing OrElse _refererUrl.EndsWith("Menu") Then
                    '' ファサードを介してDBにアクセスする
                    DBUtility.CountUpActivity(DirectCast(filterContext.RouteData.Values("menuId"), String))
                End If
            End If
        Catch ex As Exception
            LogManager.BusinessLog.Error("Activity Count Error", ex)
        End Try

    End Sub

End Class
