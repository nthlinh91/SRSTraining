Imports System.Text
Imports System.Web.Mvc
Imports SRS.Standards.MvcFramework.Core.Business
Imports SRS.Standards.MvcFramework.Core.Facade
Imports SRS.Standards.MvcFramework.Core.Information
Imports SRS.Standards.MvcFramework.Core.Logging
Imports SRS.Standards.MvcFramework.Core.Utilities
Imports SRS.Standards.MvcFramework.Core.Utilities.Extensions
Imports SRS.Standards.MvcFramework.Core.MVC

Namespace Business

    ''' <userName>住友ゴム工業株式会社</userName>
    ''' <sysName>タイヤ海外業務基幹システム（JUST）</sysName>
    ''' <subSysName>共通</subSysName>
    ''' <summary>
    ''' 最終仕向地検索 一覧画面 Facadeクラス
    ''' </summary>
    ''' <author>sfunaoka/SDS</author>
    ''' <Version>
    ''' 001, 2015-09-15, 新規作成
    ''' </Version>
    Public Class EC1040ListFacade
        Inherits FacadeBase

        ''' <summary>
        ''' 一覧画面 初期化処理
        ''' </summary>
        ''' <returns>一覧画面モデル</returns>
        Public Function Index() As Model.EC1040ListModel

            ' モデルを生成
            Dim _listModel As New Model.EC1040ListModel
            Dim _rule As New Business.EC1040ListRule
            ' 表示情報取得を実行
            _listModel = _rule.GetCommonData(_listModel)
            ' 画面パラメータ取得
            _listModel = _rule.GetParameterData(_listModel)
            Return _listModel

        End Function

        ''' <summary>
        ''' 一覧画面 検索処理
        ''' </summary>
        ''' <param name="listModel">一覧画面モデル</param>
        ''' <param name="resetCache">検索キャッシュをリセットする場合 <c>True</c> 。</param>
        ''' <returns>一覧画面モデル</returns>
        Public Function Search(listModel As Model.EC1040ListModel,
                               Optional resetCache As Boolean = False) As Model.EC1040ListModel

            Dim _rule As New Business.EC1040ListRule
            ' 表示情報取得を実行
            listModel = _rule.GetCommonData(listModel)

            '' 権限チェック
            If Not _rule.HasSearchPermission(listModel) Then
                Return listModel
            End If

            ' 検索処理を実行
            If resetCache Then
                ' 入力チェックを実行
                listModel = _rule.CheckCondition(listModel)
                If Not listModel.ValidLevel = ValidLevelType.Normal Then
                    ' 入力チェックNGの場合
                    listModel.ViewMode = ViewModeType.Init
                    Return listModel
                End If

                listModel = _rule.SearchListKey(listModel)
                ' 検索条件を保存
                _rule.SaveConditions(listModel)
            Else
                ' 検索条件を復帰
                _rule.LoadConditions(listModel)
            End If
            listModel = _rule.SearchList(listModel)
            Return listModel

        End Function

    End Class

End Namespace
