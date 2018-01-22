Imports System.Text
Imports System.Text.RegularExpressions
Imports SRS.Standards.MvcFramework.Core.Business
Imports SRS.Standards.MvcFramework.Core.DataAccess
Imports SRS.Standards.MvcFramework.Core.Facade
Imports SRS.Standards.MvcFramework.Core.Logging
Imports SRS.Standards.MvcFramework.Core.MVC
Imports SRS.Standards.MvcFramework.Core.Utilities

Namespace Wait.Business

    ''' <userName>SRS</userName>
    ''' <sysName>トレーニング</sysName>
    ''' <subSysName>共通ライブラリ</subSysName>
    ''' <summary>
    ''' 待ち画面 Ruleクラス
    ''' </summary>
    ''' <author>SRSTaro</author>
    ''' <Version>
    ''' 001, 2018-01-11, 新規作成
    ''' </Version>
    Public Class WaitRule
        Inherits RuleBase

        ''' <summary>
        ''' 処理状態取得
        ''' </summary>
        ''' <param name="waitModel">待ち画面モデル</param>
        ''' <returns>待ち画面モデル</returns>
        Public Function GetProcessStatus(waitModel As Model.WaitModel) As Model.WaitModel
            ' データアクセス実行
            Dim _da As New DataAccess.WaitDataAccess
            ' 現在の状態を取得
            Dim _waitDetail = _da.GetProcessDetail(waitModel.ProcessId)

            If _waitDetail Is Nothing Then
                waitModel.Waiting = False
                waitModel.WindowTitle = "lbl_CMN_RESULT" ' 処理結果
                waitModel.Message = "CMN_E00037" ' 処理IDが不正です。

                Return waitModel
            End If

            waitModel.ProcessStatus = _waitDetail.GetProcessStatus
            waitModel.ProcessType = _waitDetail.GetProcessType

            ' 状態に応じてメッセージを設定
            Select Case waitModel.ProcessType

                Case Constant.ProcessType.Upload
                    Select Case waitModel.ProcessStatus
                        Case Constant.ProcessStatus.Processing
                            waitModel.Waiting = True
                            waitModel.DisplayMessage = "CMN_N00033" '' アップロード処理中です。しばらくお待ちください。
                        Case Constant.ProcessStatus.Completed
                            waitModel.Waiting = False
                            waitModel.WindowTitle = "lbl_CMN_RESULT" ' 処理結果
                            waitModel.Message = "CMN_N00011" ' 処理は正常に終了しました。
                        Case Constant.ProcessStatus.Aborted
                            waitModel.Waiting = False
                            waitModel.WindowTitle = "lbl_CMN_RESULT" ' 処理結果
                            waitModel.Message = "CMN_E00030" ' データ形式が不正です。指定形式のファイルを選択してください。
                        Case Constant.ProcessStatus.WarnList
                            waitModel.Waiting = False
                            waitModel.Message = "CMN_W00068" ' アップロードファイルに警告データがあります。問題がないか確認を行ってください。
                            waitModel.WindowTitle = "lbl_CMN_RESULT" ' 処理結果
                            waitModel.DisplayMessage = "CMN_W00069" ' 警告データがあります。<br />(処理は正常に終了しました。)<br /><br />詳細はエラーリストに記載していますので、問題がないか確認を行ってください。
                        Case Constant.ProcessStatus.ErrorList
                            waitModel.Waiting = False
                            waitModel.Message = "CMN_E00031" ' アップロードファイルに不正データがあります。確認を行ってください。
                            waitModel.WindowTitle = "lbl_CMN_RESULT" ' 処理結果
                            waitModel.DisplayMessage = "CMN_E00035" ' エラーが発生しました。<br />(一括登録は全てキャンセルされました。)<br /><br />詳細をエラーリストで確認し、対応後、再度登録を行ってください。
                        Case Constant.ProcessStatus.ExclusiveError
                            waitModel.Waiting = False
                            waitModel.WindowTitle = "lbl_CMN_RESULT" ' 処理結果
                            waitModel.Message = "CMN_E00002" ' このデータは他の端末で更新された為、処理できませんでした。
                    End Select


                Case Constant.ProcessType.Download
                    Select Case waitModel.ProcessStatus
                        Case Constant.ProcessStatus.Processing
                            waitModel.Waiting = True
                            waitModel.DisplayMessage = "CMN_N00034" '' ダウンロード中です。しばらくお待ちください。
                        Case Constant.ProcessStatus.Completed
                            waitModel.Waiting = False
                            waitModel.WindowTitle = "lbl_CMN_RESULT" ' 処理結果
                            waitModel.Message = "CMN_N00011" ' 処理は正常に終了しました。
                        Case Constant.ProcessStatus.Aborted
                            waitModel.Waiting = False
                            waitModel.WindowTitle = "lbl_CMN_RESULT" ' 処理結果
                            waitModel.Message = "CMN_E00032" ' ファイル作成中にエラーが発生しました。
                        Case Constant.ProcessStatus.ExclusiveError
                            waitModel.Waiting = False
                            waitModel.WindowTitle = "lbl_CMN_RESULT" ' 処理結果
                            waitModel.Message = "CMN_E00002" ' このデータは他の端末で更新された為、処理できませんでした。
                    End Select


                Case Constant.ProcessType.Process
                    Select Case waitModel.ProcessStatus
                        Case Constant.ProcessStatus.Processing
                            waitModel.Waiting = True
                            waitModel.DisplayMessage = "CMN_N00036" '' 処理中です…しばらくお待ち下さい
                        Case Constant.ProcessStatus.Completed
                            waitModel.Waiting = False
                            waitModel.WindowTitle = "lbl_CMN_RESULT" ' 処理結果
                            waitModel.Message = "CMN_N00011" ' 処理は正常に終了しました。
                        Case Constant.ProcessStatus.Aborted
                            waitModel.Waiting = False
                            waitModel.WindowTitle = "lbl_CMN_RESULT" ' 処理結果
                            waitModel.Message = "CMN_E00001" ' 処理続行不能なエラーが発生しました。
                        Case Constant.ProcessStatus.ExclusiveError
                            waitModel.Waiting = False
                            waitModel.WindowTitle = "lbl_CMN_RESULT" ' 処理結果
                            waitModel.Message = "CMN_E00002" ' このデータは他の端末で更新された為、処理できませんでした。
                    End Select


            End Select

            Return waitModel

        End Function

        ''' <summary>
        ''' 処理ID採番処理
        ''' </summary>
        ''' <returns>採番された処理ID</returns>
        Public Function GetProcessId() As String

            Dim _da As New DataAccess.WaitDataAccess

            Dim _processId = _da.GetProcessId()
            Dim _result = _da.InsertProcessDetail(_processId)

            Return _processId

        End Function

        ''' <summary>
        ''' 処理ID採番処理
        ''' </summary>
        ''' <param name="processId">処理ID</param>
        ''' <returns>採番された処理ID</returns>
        Public Function GetProcessFilePath(processId As String) As String

            Dim _da As New DataAccess.WaitDataAccess
            Return _da.GetProcessFilePath(processId)

        End Function

        ''' <summary>
        ''' 処理状態設定
        ''' </summary>
        ''' <param name="processId">処理ID</param>
        ''' <param name="type">処理種別</param>
        ''' <param name="status">設定する状態</param>
        ''' <returns>待ち画面モデル</returns>
        Public Function SetProcessStatus(processId As String, type As Constant.ProcessType, status As Constant.ProcessStatus) As Integer
            ' データアクセス実行
            Dim _da As New DataAccess.WaitDataAccess
            ' 現在の状態を取得
            Return _da.SetProcessStatus(processId, type, status)

        End Function

        ''' <summary>
        ''' 処理IDに設定されたファイルパスを取得します。
        ''' </summary>
        ''' <param name="processId">処理ID</param>
        ''' <param name="filePath">設定するファイルパス</param>
        ''' <returns>採番された処理ID</returns>
        Public Function SetProcessFilePath(processId As String, filePath As String) As Integer

            Dim _da As New DataAccess.WaitDataAccess
            Return _da.SetProcessFilePath(processId, filePath)

        End Function

        ''' <summary>
        ''' 例外に対応するエラー処理状態を返します。
        ''' </summary>
        ''' <param name="ex">例外オブジェクト</param>
        ''' <returns>エラー処理状態。</returns>
        Public Shared Function GetErrorStatus(ex As Exception) As Constant.ProcessStatus

            If GetType(NonExclusiveUpdateException).IsAssignableFrom(ex.GetType()) Then
                Return Constant.ProcessStatus.ExclusiveError
            End If

            Return Constant.ProcessStatus.Aborted

        End Function

    End Class

End Namespace

