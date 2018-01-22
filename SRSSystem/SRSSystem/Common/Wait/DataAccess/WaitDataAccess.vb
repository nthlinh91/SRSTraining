Imports System.Text
Imports SRS.Standards.MvcFramework.Core.Business
Imports SRS.Standards.MvcFramework.Core.DataAccess
Imports SRS.Standards.MvcFramework.Core.Utilities
Imports SRS.Standards.MvcFramework.Core.Information

Namespace Wait.DataAccess

    ''' <userName>SRS</userName>
    ''' <sysName>トレーニング</sysName>
    ''' <subSysName>共通ライブラリ</subSysName>
    ''' <summary>
    ''' 待ち画面 データアクセスクラス
    ''' </summary>
    ''' <author>SRSTaro</author>
    ''' <Version>
    ''' 001, 2018-01-11, 新規作成
    ''' </Version>
    Public Class WaitDataAccess
        Inherits DataAccessBase

        ''' <summary>
        ''' 待ち画面 採番処理
        ''' </summary>
        ''' <returns>採番された処理ID</returns>
        Public Function GetProcessId() As String

            ' SQL生成
            Dim _sbSqlMain As New StringBuilder()
            ' ↓SQL2012以降はSEQUENCEが使える
            '_sbSqlMain.Append("SELECT NEXT VALUE FOR CTPROCESSWAITID")

            '' ID 取得処理
            'Dim _id = Me.ExecuteQuery(Of Decimal)(_sbSqlMain.ToString(), Nothing)

            '' 10桁の左0埋め数値に変換
            'Return _id(0).ToString("0000000000")
            ' ↑SQL2012以降はSEQUENCEが使える

            ' ↓ SQL2008以前は無理矢理なんとかする
            _sbSqlMain.Append("INSERT INTO CTPROCESSWAIT").AppendLine()
            _sbSqlMain.Append("  (PROCESSID, PROCESSTYPE, PROCESSSTATUS, FILEPATH)").AppendLine()
            _sbSqlMain.Append(" VALUES ('', '0', '0', '');").AppendLine()
            _sbSqlMain.Append("SELECT SCOPE_IDENTITY() as ID").AppendLine()

            '' ID 取得処理
            Dim _id = Me.ExecuteQuery(Of Decimal)(_sbSqlMain.ToString(), Nothing)

            If _id IsNot Nothing AndAlso _id.Count > 0 Then

                ' 10桁の左0埋め数値に変換
                Dim _processId = _id(0).ToString("0000000000")

                ' 主キーを修正
                _sbSqlMain.Length = 0
                _sbSqlMain.Append("UPDATE CTPROCESSWAIT").AppendLine()
                _sbSqlMain.Append("   SET PROCESSID = @ProcessId").AppendLine()
                _sbSqlMain.Append(" WHERE ROWID = @RowId")
                Dim _count = Me.ExecuteNonQuery(_sbSqlMain.ToString(),
                                                 New With {.ProcessId = _processId, .RowId = _id})

                Return _processId

            Else
                Return Nothing
            End If
            ' ↑ SQL2008以前は無理矢理なんとかする

        End Function

        ''' <summary>
        ''' 待ち画面 管理テーブル挿入処理
        ''' </summary>
        ''' <param name="processId">処理ID</param>
        ''' <returns>処理件数</returns>
        Public Function InsertProcessDetail(processId As String) As Integer

            ' ↓SQL2012以降はSEQUENCEでIDを取得してから挿入する
            '' SQL生成
            'Dim _sbSqlMain As New StringBuilder()
            '_sbSqlMain.Append("INSERT INTO CTPROCESSWAIT").AppendLine()
            '_sbSqlMain.Append("  (PROCESSID, PROCESSTYPE, PROCESSSTATUS, FILEPATH)").AppendLine()
            '_sbSqlMain.Append(" VALUES (@ProcessId, '0', '0', '');").AppendLine()

            ' '' テーブル登録
            'Dim _count = Me.ExecuteNonQuery(_sbSqlMain.ToString(), New With {.ProcessId = processId})
            ' Return _count
            ' ↑SQL2012以降はSEQUENCEでIDを取得してから挿入する

            ' SQL2008以前は、ID取得と同時に挿入も完了しているのでSKIP
            Return 1

        End Function

        ''' <summary>
        ''' 待ち画面 処理待ち詳細取得
        ''' </summary>
        ''' <param name="processId">処理ID</param>
        ''' <returns>処理詳細モデル</returns>
        Public Function GetProcessDetail(processId As String) As Model.CTPROCESSWAIT

            ' SQL生成
            Dim _sbSqlMain As New StringBuilder()
            _sbSqlMain.Append("SELECT PROCESSID, PROCESSTYPE, PROCESSSTATUS FROM CTPROCESSWAIT").AppendLine()
            _sbSqlMain.AppendFormat(" WHERE PROCESSID = @ProcessId").AppendLine()

            Dim _result = Me.ExecuteQuery(Of Model.CTPROCESSWAIT)(_sbSqlMain.ToString(), New With {.ProcessId = processId})
            If _result.Count = 0 Then
                ' IDが存在しなかった
                Return Nothing
            End If

            Return _result.Single

        End Function

        ''' <summary>
        ''' 待ち画面 待ち状態設定
        ''' </summary>
        ''' <param name="processId">処理ID</param>
        ''' <param name="type">設定する処理種別</param>
        ''' <param name="status">設定する処理状態</param>
        ''' <returns>処理件数</returns>
        Public Function SetProcessStatus(processId As String, type As Constant.ProcessType, status As Constant.ProcessStatus) As Integer

            ' SQL生成
            Dim _sbSqlMain As New StringBuilder()
            _sbSqlMain.Append("UPDATE CTPROCESSWAIT").AppendLine()
            _sbSqlMain.Append("   SET PROCESSTYPE = @Type,").AppendLine()
            _sbSqlMain.Append("       PROCESSSTATUS = @Status").AppendLine()
            _sbSqlMain.Append(" WHERE PROCESSID = @ProcessId")

            Dim _count = Me.ExecuteNonQuery(_sbSqlMain.ToString(),
                                             New With {.ProcessId = processId, .Type = type, .Status = status})

            Return _count

        End Function

        ''' <summary>
        ''' 待ち画面 ファイルパス取得
        ''' </summary>
        ''' <param name="processId">処理ID</param>
        ''' <returns>ファイルパス</returns>
        Public Function GetProcessFilePath(processId As String) As String

            ' SQL生成
            Dim _sbSqlMain As New StringBuilder()
            _sbSqlMain.Append("SELECT FILEPATH FROM CTPROCESSWAIT").AppendLine()
            _sbSqlMain.Append(" WHERE PROCESSID = @ProcessId").AppendLine()

            Return Me.ExecuteQuery(Of String)(_sbSqlMain.ToString(), New With {.ProcessId = processId}).Single

        End Function

        ''' <summary>
        ''' 待ち画面 ファイルパス設定
        ''' </summary>
        ''' <param name="processId">処理ID</param>
        ''' <param name="filePath">ファイルパス</param>
        ''' <returns>処理件数</returns>
        Public Function SetProcessFilePath(processId As String, filePath As String) As Integer

            ' SQL生成
            Dim _sbSqlMain As New StringBuilder()
            _sbSqlMain.Append("UPDATE CTPROCESSWAIT").AppendLine()
            _sbSqlMain.Append("   SET FILEPATH = @FilePath").AppendLine()
            _sbSqlMain.Append(" WHERE PROCESSID = @ProcessId")

            Dim _count = Me.ExecuteNonQuery(_sbSqlMain.ToString(),
                                             New With {.ProcessId = processId, .FilePath = filePath})

            Return _count

        End Function

    End Class

End Namespace

