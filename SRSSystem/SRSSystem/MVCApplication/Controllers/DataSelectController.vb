Imports System.Net
Imports System.Web.Http

Imports SRS.Standards.MvcFramework.Core.MVC
Imports SRS.Standards.MvcFramework.Core.Facade
Imports SRS.Standards.MvcFramework.Core.Information
Imports SRS.Standards.MvcFramework.Core.Security
Imports SRS.Standards.MvcFramework.Core.Http
Imports SRSSystem.Common.Utilities

Namespace Controllers

    ''' <userName>SRS</userName>
    ''' <sysName>トレーニング</sysName>
    ''' <subSysName>SRS ASP.NET MVC アプリケーション</subSysName>
    ''' <summary>
    ''' データセレクト取得コントローラ
    ''' </summary>
    ''' <author>SRSTaro</author>
    ''' <Version>
    ''' 001, 2018-01-11, 新規作成
    ''' </Version>
    Public Class DataSelectController
        Inherits ApiControllerBase(Of Business.UtilityFacade)

        ''' <summary>
        ''' データセレクト取得
        ''' </summary>
        ''' <param name="viewModel">モデル</param>
        ''' <returns>ActionResult</returns>
        ''' <permission>View(Model)の結果を返します</permission>
        Function [Post](<FromBody()> viewModel As Model.DataSelectModel) As IEnumerable(Of Model.DataSelectItemModel)

            ' 初期化を実行
            Dim _facade As New Business.UtilityFacade()
            Return _facade.GetDataSelect(viewModel)

        End Function

    End Class

End Namespace
