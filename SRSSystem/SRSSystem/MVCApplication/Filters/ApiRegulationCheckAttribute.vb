Imports SRS.Standards.MvcFramework.Core.Configuration
Imports SRS.Standards.MvcFramework.Core.Security
Imports SRSSystem.Common.Login

''' <userName>SRS</userName>
''' <sysName>トレーニング</sysName>
''' <subSysName>SRS ASP.NET MVC アプリケーション</subSysName>
''' <summary>
''' WebAPIコントローラに対する業務規制フィルタ
''' </summary>
''' <author>SRSTaro</author>
''' <Version>
''' 001, 2018-01-11, 新規作成
''' </Version>
Public Class ApiRegulationCheckAttribute
    Inherits System.Web.Http.Filters.ActionFilterAttribute

    ''' <summary>
    ''' 業務規制処理
    ''' </summary>
    ''' <param name="actionContext">呼出時情報</param>
    Public Overrides Sub OnActionExecuting(actionContext As System.Web.Http.Controllers.HttpActionContext)

        If Not actionContext.ControllerContext.RouteData.Values.Keys.Contains("menuId") Then
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
            actionContext.ControllerContext.RouteData.Values.Remove("menuId")
            '' 業務規制中の表示を返す
            actionContext.Response.StatusCode = Net.HttpStatusCode.ServiceUnavailable
        End If

    End Sub

End Class
