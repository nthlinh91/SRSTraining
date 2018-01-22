Imports SRS.Standards.MvcFramework.Core.Configuration
Imports SRS.Standards.MvcFramework.Core.Information
Imports SRS.Standards.MvcFramework.Core.Security
Imports SRSSystem.Common.Login

''' <userName>SRS</userName>
''' <sysName>トレーニング</sysName>
''' <subSysName>SRS ASP.NET MVC アプリケーション</subSysName>
''' <summary>
''' コントローラに対する業務規制フィルタ
''' </summary>
''' <author>SRSTaro</author>
''' <Version>
''' 001, 2018-01-11, 新規作成
''' </Version>
Public Class RegulationCheckAttribute
    Inherits ActionFilterAttribute

    ''' <summary>
    ''' 業務規制処理
    ''' </summary>
    ''' <param name="filterContext">呼出時情報</param>
    Public Overrides Sub OnActionExecuting(filterContext As System.Web.Mvc.ActionExecutingContext)

        If Not filterContext.RouteData.Values.Keys.Contains("menuId") Then
            ' メニューIDが存在しない場合はチェック不要
            Return
        End If

        Dim _facade As New Business.LoginFacade
        Dim _securityModel As New SecurityRegulationModel
        ' 業務規制チェックを実施
        _securityModel = _facade.RegulationCheck(_securityModel)
        If _securityModel.SecurityCheck <> BusinessSecurityType.Authority Then
            ' チェックＮＧの場合
            '' ルートを変更しないと変になる
            filterContext.RouteData.Values.Remove("menuId")
            filterContext.RouteData.Values.Remove("group")
            filterContext.RouteData.Values.Remove("controller")
            filterContext.RouteData.Values.Add("controller", ExtensionConfigurationManager.Config.RegulationView)
            '' 業務規制中の表示を返す
            filterContext.Result = New ViewResult With {.ViewName = ExtensionConfigurationManager.Config.RegulationView,
                                                        .ViewData = New ViewDataDictionary(_securityModel)}
        End If

    End Sub

End Class
