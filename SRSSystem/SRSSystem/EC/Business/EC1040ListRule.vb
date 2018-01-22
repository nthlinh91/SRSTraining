Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Web.Mvc
Imports SRS.Standards.MvcFramework.Core.Business
Imports SRS.Standards.MvcFramework.Core.Configuration
Imports SRS.Standards.MvcFramework.Core.DataAccess
Imports SRS.Standards.MvcFramework.Core.Facade
Imports SRS.Standards.MvcFramework.Core.Information
Imports SRS.Standards.MvcFramework.Core.Logging
Imports SRS.Standards.MvcFramework.Core.Utilities
Imports SRS.Standards.MvcFramework.Core.Utilities.Extensions
Imports SRS.Standards.MvcFramework.Core.Message
Imports SRS.Standards.MvcFramework.Core.MVC

Imports JUST.Common.Utilities
Imports JUST.EC.EC1040.Constant

Namespace Business

    ''' <userName>住友ゴム工業株式会社</userName>
    ''' <sysName>タイヤ海外業務基幹システム（JUST）</sysName>
    ''' <subSysName>共通</subSysName>
    ''' <summary>
    ''' 最終仕向地検索 一覧画面 Ruleクラス
    ''' </summary>
    ''' <author>sfunaoka/SDS</author>
    ''' <Version>
    ''' 001, 2015-09-15, 新規作成
    ''' </Version>
    Public Class EC1040ListRule
        Inherits RuleBase

        ''' <summary>
        ''' 表示情報取得
        ''' </summary>
        ''' <param name="listModel">一覧画面モデル</param>
        ''' <returns>一覧画面モデル</returns>
        Public Function GetCommonData(ByVal listModel As Model.EC1040ListModel) As Model.EC1040ListModel
            ' データアクセス実行
            Dim _da As New DataAccess.EC1040DataAccess

            ' 業務制御区分
            Dim _gymSeigyoKbn As String = String.Empty
            ' 代理店区分
            Dim _loginDaiKbn As String = Nothing
            'If InformationUtility.HasBusinessCtrlKbn(EC1040Constant.GymSeigyoKbnTkaiHan, EC1040Constant.GymSeigyoCdTkaiHan) Then
            '    ' ◆T海販
            '    _gymSeigyoKbn = EC1040Constant.GymSeigyoKbnTkaiHan
            'ElseIf InformationUtility.HasBusinessCtrlKbn(EC1040Constant.GymSeigyoKbnTkaiGyo, EC1040Constant.GymSeigyoCdTkaiGyo) Then
            '    ' ◆T海業
            '    _gymSeigyoKbn = EC1040Constant.GymSeigyoKbnTkaiGyo
            'ElseIf InformationUtility.HasBusinessCtrlKbn(EC1040Constant.GymSeigyoKbnHansha, EC1040Constant.GymSeigyoCdHansha) Then
            '    ' ◆統括販社
            '    _gymSeigyoKbn = EC1040Constant.GymSeigyoKbnHansha
            '    _loginDaiKbn = InformationUtility.GetBusinessCtrlKbn(EC1040Constant.GymSeigyoKbnDairiten)(0)
            'ElseIf InformationUtility.HasBusinessCtrlKbn(EC1040Constant.GymSeigyoKbnOEBusho, EC1040Constant.GymSeigyoCdOEBusho) Then
            '    ' ◆OE
            '    _gymSeigyoKbn = EC1040Constant.GymSeigyoKbnOEBusho
            'Else
            '    ' ◆その他
            '    _gymSeigyoKbn = EC1040Constant.GymSeigyoKbnSonota
            'End If
            listModel.GymSeigyoKbn = _gymSeigyoKbn
            listModel.LoginDaiKbn = _loginDaiKbn

            Return listModel

        End Function

        ''' <summary>
        ''' 一覧画面 入力値チェック処理
        ''' </summary>
        ''' <param name="listModel">一覧画面モデル</param>
        ''' <returns>一覧画面モデル</returns>
        Public Function CheckCondition(ByVal listModel As Model.EC1040ListModel) As Model.EC1040ListModel

            ' 入力値チェックを実行
            'listModel.Validate()

            Return listModel

        End Function

        ''' <summary>
        ''' 一覧画面 検索処理 (キー取得)
        ''' </summary>
        ''' <param name="listModel">一覧画面モデル</param>
        ''' <returns>一覧画面モデル</returns>
        Public Function SearchListKey(ByVal listModel As Model.EC1040ListModel) As Model.EC1040ListModel

            ' データアクセス実行
            Dim _da As New DataAccess.EC1040DataAccess
            listModel = _da.SearchListKey(listModel)

            Return listModel

        End Function

        ''' <summary>
        ''' 一覧画面 検索処理 (明細取得)
        ''' </summary>
        ''' <param name="listModel">一覧画面モデル</param>
        ''' <returns>一覧画面モデル</returns>
        Public Function SearchList(ByVal listModel As Model.EC1040ListModel) As Model.EC1040ListModel

            ' データアクセス実行
            Dim _da As New DataAccess.EC1040DataAccess
            ' 検索処理
            listModel.ListData = _da.SearchList(listModel)
            If listModel.ListData.Count <= 0 Then
                ' 取得できなかった場合
                ' エラー設定
                Me.SetError(listModel, String.Empty, "MSG_E00418")
            End If

            ' 正常時
            ' 選択状態を解除
            listModel.SelectedIndex = Nothing
            ' 検索後に設定
            listModel.ViewMode = ViewModeType.Searched

            Return listModel

        End Function

        ''' <summary>
        ''' 画面パラメータ情報取得
        ''' </summary>
        ''' <param name="listModel">一覧画面モデル</param>
        ''' <returns>一覧画面モデル</returns>

        Public Function GetParameterData(ByVal listModel As Model.EC1040ListModel) As Model.EC1040ListModel
            '仕向地コード
            If InformationManager.AppInfo.GetParameters.ContainsKey("shimukechiCd") Then
                listModel.ShimukechiCdCondition = InformationManager.AppInfo.GetParameters("shimukechiCd")
            End If
            '最終仕向地名
            If InformationManager.AppInfo.GetParameters.ContainsKey("saishuShimukechiNm") Then
                listModel.SaichushimukechiNmCondition = InformationManager.AppInfo.GetParameters("saishuShimukechiNm")
            End If

            Return listModel

        End Function

        ''' <summary>
        ''' 検索条件を保持領域にコピーする
        ''' </summary>
        ''' <param name="listModel">一覧画面モデル</param>
        ''' <returns>一覧画面モデル</returns>
        Public Function SaveConditions(listModel As Model.EC1040ListModel) As Model.EC1040ListModel

            With listModel
                .ShimukechiCdConditionSaved = .ShimukechiCdCondition
                .SaichushimukechiNmConditionSaved = .SaichushimukechiNmCondition
            End With

            Return listModel
        End Function

        ''' <summary>
        ''' 保持領域から検索条件を復帰する
        ''' </summary>
        ''' <param name="listModel">一覧画面モデル</param>
        ''' <returns>一覧画面モデル</returns>
        Public Function LoadConditions(listModel As Model.EC1040ListModel) As Model.EC1040ListModel

            With listModel
                .ShimukechiCdCondition = .ShimukechiCdConditionSaved
                .SaichushimukechiNmCondition = .SaichushimukechiNmConditionSaved
            End With

            Return listModel
        End Function

    End Class

End Namespace

