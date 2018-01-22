Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Web.Mvc
Imports SRS.Standards.MvcFramework.Core.Business
Imports SRS.Standards.MvcFramework.Core.Configuration
Imports SRS.Standards.MvcFramework.Core.DataAccess
Imports SRS.Standards.MvcFramework.Core.Facade
Imports SRS.Standards.MvcFramework.Core.Information
Imports SRS.Standards.MvcFramework.Core.Logging
Imports SRS.Standards.MvcFramework.Core.Message
Imports SRS.Standards.MvcFramework.Core.MVC
Imports SRS.Standards.MvcFramework.Core.Utilities
Imports SRS.Standards.MvcFramework.Core.Utilities.Extensions

Imports SRSSystem.Common.Utilities
Imports SRSSystem.ZZ.ZZ1031.Constant

Namespace Business

    ''' <userName>SRS</userName>
    ''' <sysName>トレーニング</sysName>
    ''' <subSysName>トレニンーグ機能グループ</subSysName>
    ''' <summary>
    ''' 納品見積登録 検索画面 Ruleクラス
    ''' </summary>
    ''' <author>SRSTaro</author>
    ''' <Version>
    ''' 001, 2018-01-11, 新規作成
    ''' </Version>
    Public Class ZZ1031SearchRule
        Inherits RuleBase

        ''' <summary>
        ''' 検索画面 初期化処理
        ''' </summary>
        ''' <param name="searchModel">検索画面モデル</param>
        ''' <returns>検索画面モデル</returns>
        Public Function Init(searchModel As Model.ZZ1031SearchModel) As Model.ZZ1031SearchModel

            ' 詳細画面からの情報存在チェック
            If InformationManager.TransferInfo.TransferData IsNot Nothing AndAlso
               (TypeOf InformationManager.TransferInfo.TransferData Is Model.ZZ1031DetailModel) Then
                ' 詳細画面モデルを受け取れた場合
                Dim _detailModel As Model.ZZ1031DetailModel =
                    DirectCast(InformationManager.TransferInfo.TransferData, Model.ZZ1031DetailModel)
                ' 検索条件の情報を引き継ぐ (処理メッセージ等)
                searchModel.Message = _detailModel.Message
                'TODO：↓↓検索条件に合わせて修正してください。
                '' 検索キーを復元する場合は、ここで行う
                'searchModel.KeyCdCondition = _detailModel.SearchKeyCd
                'TODO：↑↑検索条件に合わせて修正してください。
            End If

            Return searchModel

        End Function

        ''' <summary>
        ''' 表示情報取得
        ''' </summary>
        ''' <param name="searchModel">検索画面モデル</param>
        ''' <returns>検索画面モデル</returns>
        Public Function GetCommonData(searchModel As Model.ZZ1031SearchModel) As Model.ZZ1031SearchModel

            ' データアクセス実行
            Dim _da As New DataAccess.ZZ1031DataAccess

            'TODO: ↓↓ドロップダウンリストの取得処理を実装してください。
            ' 選択リストボックス内容
            'searchModel.ListCdData = _da.GetDataSelectListItem("ListCdList")
            searchModel.ListSetData = _da.GetDataSelectListForSetItem()
            searchModel.ListFactoryData = _da.GetDataSelectListForFactoryItem()
            'TODO: ↑↑ドロップダウンリストの取得処理を実装してください。

            Return searchModel

        End Function

        ''' <summary>
        ''' 検索画面 入力値チェック処理
        ''' </summary>
        ''' <param name="searchModel">検索画面モデル</param>
        ''' <returns>検索画面モデル</returns>
        Public Function CheckCondition(searchModel As Model.ZZ1031SearchModel) As Model.ZZ1031SearchModel

            ' 入力値チェックを実行
            If searchModel.Validate() Then

                'TODO: ↓↓検索条件の項目に合わせて修正してください。

                '業務チェックを追加します。
                If Not IsNullOrEmpty(searchModel.DateFrom) And Not IsNullOrEmpty(searchModel.DateTo) Then
                    If searchModel.DateFrom > searchModel.DateTo Then
                        searchModel.Message = "日付は、From < Toになるように入力してください"
                        Return searchModel
                    End If
                End If

                'TODO: ↑↑検索条件の項目に合わせて修正してください。

            End If

            Return searchModel

        End Function

        ''' <summary>
        ''' 検索画面 検索処理
        ''' </summary>
        ''' <param name="searchModel">検索画面モデル</param>
        ''' <returns>検索画面モデル</returns>
        Public Function Search(searchModel As Model.ZZ1031SearchModel) As Model.ZZ1031SearchModel

            ' データアクセス実行
            Dim _da As New DataAccess.ZZ1031DataAccess
            ' 検索処理
            Dim _count = _da.GetDetailCount(searchModel)
            If _count <= 0 Then
                ' 取得できなかった場合
                ' エラー設定 (指定されたデータは存在しません。)
                searchModel.SetMessage(Nothing, "CMN_E00016")
            End If

            ' 正常時
            ' 検索後に設定
            searchModel.ViewMode = ViewModeType.Searched

            Return searchModel

        End Function

    End Class

End Namespace

