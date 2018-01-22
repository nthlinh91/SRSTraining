Imports SRS.Standards.MvcFramework.Core.Configuration
Imports SRS.Standards.MvcFramework.Core.Information
Imports SRS.Standards.MvcFramework.Core.Security
Imports SRSSystem.Common.Login

''' <userName>SRS</userName>
''' <sysName>トレーニング</sysName>
''' <subSysName>SRS ASP.NET MVC アプリケーション</subSysName>
''' <summary>
''' APIコントローラに対する権限読み込みフィルタ
''' </summary>
''' <author>SRSTaro</author>
''' <Version>
''' 001, 2018-01-11, 新規作成
''' </Version>
Public Class ApiEnablePermissionAttribute
    Inherits System.Web.Http.Filters.ActionFilterAttribute

    ''' <summary>
    ''' 権限取得処理
    ''' </summary>
    ''' <param name="actionContext">呼出時情報</param>
    Public Overrides Sub OnActionExecuting(actionContext As System.Web.Http.Controllers.HttpActionContext)

        If Not actionContext.ControllerContext.RouteData.Values.Keys.Contains("menuId") Then
            ' メニューIDが存在しない場合はチェック不要
            Return
        End If

        Dim _facade As New Business.LoginFacade
        Dim _securityModel As New SecurityRegulationModel

        ' 権限取得処理を実施
        _securityModel = _facade.GetPermission()
        InformationManager.AppInfo.CanDelete = _securityModel.CanDelete
        InformationManager.AppInfo.CanInsert = _securityModel.CanInsert
        InformationManager.AppInfo.CanSearch = _securityModel.CanSearch
        InformationManager.AppInfo.CanUpdate = _securityModel.CanUpdate

    End Sub

End Class
