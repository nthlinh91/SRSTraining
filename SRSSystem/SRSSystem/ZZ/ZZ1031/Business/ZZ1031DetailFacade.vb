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
    ''' 納品見積登録 詳細画面 Facadeクラス
    ''' </summary>
    ''' <author>SRSTaro</author>
    ''' <Version>
    ''' 001, 2018-01-11, 新規作成
    ''' </Version>
    Public Class ZZ1031DetailFacade
        Inherits FacadeBase

        ''' <summary>
        ''' 詳細画面 初期化処理
        ''' </summary>
        ''' <returns>詳細画面モデル</returns>
        Public Function Index() As Model.ZZ1031DetailModel

            ' モデルを生成
            Dim _detailModel As New Model.ZZ1031DetailModel

            ' 初期化処理を実行
            Dim _rule As New Business.ZZ1031DetailRule
            _detailModel = _rule.Init(_detailModel)

            ' 表示情報取得を実行
            _detailModel = _rule.GetCommonData(_detailModel)

            Return _detailModel

        End Function

        ''' <summary>
        ''' 詳細画面 確認処理
        ''' </summary>
        ''' <param name="detailModel">詳細画面モデル</param>
        ''' <returns>詳細画面モデル</returns>
        Public Function Confirm(detailModel As Model.ZZ1031DetailModel) As Model.ZZ1031DetailModel

            Dim _rule As New Business.ZZ1031DetailRule
            ' 表示情報取得を実行
            detailModel = _rule.GetCommonData(detailModel)

            ' 確認処理を実行
            detailModel = _rule.CheckInput(detailModel)

            Return detailModel

        End Function

        ''' <summary>
        ''' 詳細画面 登録処理
        ''' </summary>
        ''' <param name="detailModel">詳細画面モデル</param>
        ''' <returns>詳細画面モデル</returns>
        Public Function Insert(detailModel As Model.ZZ1031DetailModel) As Model.ZZ1031DetailModel

            Dim _rule As New Business.ZZ1031DetailRule
            ' 表示情報取得を実行
            detailModel = _rule.GetCommonData(detailModel)

            '' 権限チェック
            If Not _rule.HasInsertPermission(detailModel) Then
                Return detailModel
            End If

            ' 確認処理を実行
            detailModel = _rule.CheckInput(detailModel)
            If detailModel.ValidLevel = ValidLevelType.Normal Then
                ' 正常時、登録処理実行
                detailModel = _rule.InsertDetail(detailModel)
            End If

            ' エラーメッセージが発生していないか確認
            Me.AssertModel(detailModel)

            Return detailModel

        End Function

        ''' <summary>
        ''' 詳細画面 更新処理
        ''' </summary>
        ''' <param name="detailModel">詳細画面モデル</param>
        ''' <returns>詳細画面モデル</returns>
        Public Function Update(detailModel As Model.ZZ1031DetailModel) As Model.ZZ1031DetailModel

            Dim _rule As New Business.ZZ1031DetailRule
            ' 表示情報取得を実行
            detailModel = _rule.GetCommonData(detailModel)

            '' 権限チェック
            If Not _rule.HasUpdatePermission(detailModel) Then
                Return detailModel
            End If

            ' 確認処理を実行
            detailModel = _rule.CheckInput(detailModel)
            If detailModel.ValidLevel = ValidLevelType.Normal Then
                ' 正常時、更新処理実行
                detailModel = _rule.UpdateDetail(detailModel)
            End If

            ' エラーメッセージが発生していないか確認
            Me.AssertModel(detailModel)

            Return detailModel

        End Function

        ''' <summary>
        ''' 詳細画面 削除処理
        ''' </summary>
        ''' <param name="detailModel">詳細画面モデル</param>
        ''' <returns>詳細画面モデル</returns>
        Public Function Delete(detailModel As Model.ZZ1031DetailModel) As Model.ZZ1031DetailModel

            Dim _rule As New Business.ZZ1031DetailRule
            ' 表示情報取得を実行
            detailModel = _rule.GetCommonData(detailModel)

            '' 権限チェック
            If Not _rule.HasDeletePermission(detailModel) Then
                Return detailModel
            End If

            ' 確認処理を実行
            detailModel = _rule.CheckInput(detailModel)
            If detailModel.ValidLevel = ValidLevelType.Normal Then
                ' 正常時、削除処理実行
                detailModel = _rule.DeleteDetail(detailModel)
            End If

            ' エラーメッセージが発生していないか確認
            Me.AssertModel(detailModel)

            Return detailModel

        End Function

        ''' <summary>
        ''' 詳細画面 計算処理
        ''' </summary>
        ''' <param name="detailModel">詳細画面モデル</param>
        ''' <returns>詳細画面モデル</returns>
        Public Function Calculation(detailModel As Model.ZZ1031DetailModel) As Model.ZZ1031DetailModel

            detailModel.Price = detailModel.UnitCost * detailModel.EstimateNumber

            Return detailModel

        End Function


    End Class

End Namespace

