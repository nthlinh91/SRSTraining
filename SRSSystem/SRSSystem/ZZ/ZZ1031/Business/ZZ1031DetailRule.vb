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
    ''' 納品見積登録 詳細画面 Ruleクラス
    ''' </summary>
    ''' <author>SRSTaro</author>
    ''' <Version>
    ''' 001, 2018-01-11, 新規作成
    ''' </Version>
    Public Class ZZ1031DetailRule
        Inherits RuleBase

        ''' <summary>
        ''' 詳細画面 初期化処理
        ''' </summary>
        ''' <param name="detailModel">詳細画面モデル</param>
        ''' <returns>詳細画面モデル</returns>
        Public Function Init(detailModel As Model.ZZ1031DetailModel) As Model.ZZ1031DetailModel

            ' 遷移情報存在チェック
            If InformationManager.TransferInfo.TransferData Is Nothing OrElse
               (Not TypeOf InformationManager.TransferInfo.TransferData Is Model.ZZ1031SearchModel) Then
                ' 受け取れない、検索画面モデルでない場合は不正処理である為、検索画面へ戻る
                detailModel = New Model.ZZ1031DetailModel
                detailModel.RedirectName = Constant.ZZ1031Constant.RedirectSearch
                detailModel.JudgeTrans = JudgeTransType.AnyRedirect
                Return detailModel
            End If

            ' 引き継ぎモデルを取得
            Dim _searchModel = DirectCast(InformationManager.TransferInfo.TransferData, Model.ZZ1031SearchModel)

            ' データアクセス生成
            Dim _da As New DataAccess.ZZ1031DataAccess

            If _searchModel.DetailViewMode = ViewModeType.Detail Then
                ' 詳細モードが指定された場合

                ' 詳細データ取得
                'TODO: ↓↓検索条件に合わせて項目を修正してください。
                ' 主キー設定
                detailModel.EstimateNo = _searchModel.EstimateNoCondition
                'TODO: ↑↑検索条件に合わせて項目を修正してください。

                ' 検索処理
                Dim _detailList = _da.GetDetail(detailModel)
                If _detailList.Count <= 0 Then
                    ' データが取得できなかった場合(継続不能)
                    Throw New RuleException()
                End If

                ' 取得データを詳細モデルに設定
                detailModel = _detailList(0)

                detailModel.ViewMode = ViewModeType.Detail
            Else
                ' 詳細モード以外の場合は追加
                detailModel.ViewMode = ViewModeType.Insert

            End If

            ' 検索情報引き継ぎ
            'TODO: ↓↓検索条件に合わせて項目を修正してください。
            detailModel.SearchEstimateNo = _searchModel.EstimateNoCondition
            'TODO: ↑↑検索条件に合わせて項目を修正してください。

            Return detailModel

        End Function

        ''' <summary>
        ''' 表示情報取得
        ''' </summary>
        ''' <param name="detailModel">詳細画面モデル</param>
        ''' <returns>詳細画面モデル</returns>
        Public Function GetCommonData(detailModel As Model.ZZ1031DetailModel) As Model.ZZ1031DetailModel

            ' データアクセス実行
            Dim _da As New DataAccess.ZZ1031DataAccess
            'TODO: ↓↓ドロップダウンリストの取得処理を実装してください
            ' 選択リストボックス内容
            'detailModel.ListCdData = _da.GetDataSelectListItem("ListCdList")
            detailModel.ListSetData = _da.GetDataSelectListForSetItem()
            detailModel.ListFactoryData = _da.GetDataSelectListForFactoryItem()
            'TODO: ↑↑ドロップダウンリストの取得処理を実装してください。

            Return detailModel

        End Function

        ''' <summary>
        ''' 入力内容チェック処理
        ''' </summary>
        ''' <param name="detailModel">詳細画面モデル</param>
        ''' <returns>詳細画面モデル</returns>
        Public Function CheckInput(detailModel As Model.ZZ1031DetailModel) As Model.ZZ1031DetailModel

            ' 入力値チェックを実行
            If detailModel.Validate() Then

                'TODO: ↓↓入力項目に合わせて修正してください。

                ' データアクセス生成
                Dim _da As New DataAccess.ZZ1031DataAccess

                ' 1) 主キー
                If detailModel.ViewMode = ViewModeType.Insert OrElse
                    detailModel.ViewMode = ViewModeType.InsertConfirmed Then
                    ' 追加モードのみ場合のみチェック
                    If Not String.IsNullOrWhiteSpace(detailModel.EstimateNo) Then
                        ' ＤＢ登録済みエラーチェック
                        ' 詳細データ取得
                        ' 検索処理
                        Dim _detailList = _da.GetDetail(detailModel)
                        If _detailList.Count > 0 Then
                            ' データが取得できた場合は重複エラーセット
                            detailModel.SetMessage(detailModel.GetPropertyFullName(Function(m) m.EstimateNo),
                                                   "CMN_E00017")

                        End If
                    End If
                End If
                '業務チェックを追加します。
                If Not IsNullOrEmpty(detailModel.DateFrom) And Not IsNullOrEmpty(detailModel.DateTo) Then
                    If detailModel.DateFrom > detailModel.DateTo Then
                        detailModel.Message = "日付は、From < Toになるように入力してください"
                        Return detailModel
                    End If
                End If


                'TODO: ↑↑入力項目に合わせて修正してください。

            End If

            If detailModel.ValidLevel = ValidLevelType.Normal OrElse
                detailModel.ValidLevel = ValidLevelType.Warn Then
                ' 入力チェック正常時、画面モードを確認後へ
                If detailModel.ViewMode = ViewModeType.Insert OrElse
                    detailModel.ViewMode = ViewModeType.InsertConfirmed Then
                    ' 登録時
                    detailModel.ViewMode = ViewModeType.InsertConfirmed
                Else
                    ' 更新時
                    detailModel.ViewMode = ViewModeType.UpdateConfirmed
                End If

                If detailModel.ValidLevel = ValidLevelType.Normal Then
                    'メッセージセット
                    detailModel.SetMessage(Nothing, "CMN_N00040")
                End If
            Else
                ' 入力チェックＮＧ時は確認前状態へ
                If detailModel.ViewMode = ViewModeType.Insert OrElse
                    detailModel.ViewMode = ViewModeType.InsertConfirmed Then
                    ' 登録時
                    detailModel.ViewMode = ViewModeType.Insert
                Else
                    ' 更新時
                    detailModel.ViewMode = ViewModeType.Detail
                End If
            End If

            Return detailModel

        End Function

        ''' <summary>
        ''' 入力データ登録処理
        ''' </summary>
        ''' <param name="detailModel">詳細画面モデル</param>
        ''' <returns>詳細画面モデル</returns>
        Public Function InsertDetail(detailModel As Model.ZZ1031DetailModel) As Model.ZZ1031DetailModel

            ' データアクセス生成
            Dim _da As New DataAccess.ZZ1031DataAccess

            Dim _retCount As Integer = _da.InsertDetail(detailModel)

            If _retCount <= 0 Then
                ' 継続不能
                Throw New RuleException()
            End If

            ' 正常終了時
            detailModel.SetMessage(Nothing, "CMN_N00011")

            Return detailModel

        End Function

        ''' <summary>
        ''' 入力データ更新処理
        ''' </summary>
        ''' <param name="detailModel">詳細画面モデル</param>
        ''' <returns>詳細画面モデル</returns>
        Public Function UpdateDetail(detailModel As Model.ZZ1031DetailModel) As Model.ZZ1031DetailModel

            ' データアクセス生成
            Dim _da As New DataAccess.ZZ1031DataAccess

            ' 排他制御用データを検証
            'TODO: ↓↓制御の対象となるテーブルを指定してください。
            _da.CheckExclusiveUpdate("ZZSAMPLE", detailModel)
            'TODO: ↑↑制御の対象となるテーブルを指定してください。

            Dim _retCount As Integer = _da.UpdateDetail(detailModel)

            If _retCount <= 0 Then
                ' 継続不能
                Throw New RuleException()
            End If

            ' 正常終了時
            detailModel.SetMessage(Nothing, "CMN_N00011")

            Return detailModel

        End Function

        ''' <summary>
        ''' 入力データ削除処理
        ''' </summary>
        ''' <param name="detailModel">詳細画面モデル</param>
        ''' <returns>詳細画面モデル</returns>
        Public Function DeleteDetail(detailModel As Model.ZZ1031DetailModel) As Model.ZZ1031DetailModel

            ' データアクセス生成
            Dim _da As New DataAccess.ZZ1031DataAccess

            ' 排他制御用データを検証
            'TODO: ↓↓制御の対象となるテーブルを指定してください。
            _da.CheckExclusiveUpdate("ZZSAMPLE", detailModel)
            'TODO: ↑↑制御の対象となるテーブルを指定してください。

            Dim _retCount As Integer = _da.DeleteDetail(detailModel)

            If _retCount <= 0 Then
                ' 継続不能
                Throw New RuleException()
            End If

            ' 正常終了時
            detailModel.SetMessage(Nothing, "CMN_N00011")

            Return detailModel

        End Function

    End Class

End Namespace

