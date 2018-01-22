Imports System.Text
Imports System.Web.Mvc
Imports SRS.Standards.MvcFramework.Core.Business
Imports SRS.Standards.MvcFramework.Core.Facade
Imports SRS.Standards.MvcFramework.Core.Information
Imports SRS.Standards.MvcFramework.Core.Logging
Imports SRS.Standards.MvcFramework.Core.Message
Imports SRS.Standards.MvcFramework.Core.MVC
Imports SRS.Standards.MvcFramework.Core.Utilities
Imports SRS.Standards.MvcFramework.Core.Utilities.Extensions

Namespace Business

    ''' <userName>SRS</userName>
    ''' <sysName>トレーニング</sysName>
    ''' <subSysName>トレニンーグ機能グループ</subSysName>
    ''' <summary>
    ''' 納品見積登録 検索画面 Facadeクラス
    ''' </summary>
    ''' <author>SRSTaro</author>
    ''' <Version>
    ''' 001, 2018-01-11, 新規作成
    ''' </Version>
    Public Class ZZ1031SearchFacade
        Inherits FacadeBase

        ''' <summary>
        ''' 検索画面 初期化処理
        ''' </summary>
        ''' <returns>検索画面モデル</returns>
        Public Function Index() As Model.ZZ1031SearchModel

            ' モデルを生成
            Dim _searchModel As New Model.ZZ1031SearchModel

            ' 初期化処理を実行
            Dim _rule As New Business.ZZ1031SearchRule
            _searchModel = _rule.Init(_searchModel)

            ' 表示情報取得を実行
            _searchModel = _rule.GetCommonData(_searchModel)

            Return _searchModel

        End Function

        ''' <summary>
        ''' 検索画面 検索処理
        ''' </summary>
        ''' <param name="searchModel">検索画面モデル</param>
        ''' <returns>検索画面モデル</returns>
        Public Function Search(searchModel As Model.ZZ1031SearchModel) As Model.ZZ1031SearchModel

            Dim _rule As New Business.ZZ1031SearchRule
            ' 表示情報取得を実行
            searchModel = _rule.GetCommonData(searchModel)

            '' 権限チェック
            If Not _rule.HasSearchPermission(searchModel) Then
                Return searchModel
            End If

            ' 入力チェックを実行
            searchModel = _rule.CheckCondition(searchModel)
            If Not searchModel.ValidLevel = ValidLevelType.Normal Then
                ' 入力チェックNGの場合
                Return searchModel
            End If

            ' 検索処理を実行
            searchModel = _rule.Search(searchModel)
            Return searchModel

        End Function

    End Class

End Namespace

