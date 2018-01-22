Namespace Wait.Model

    ''' <userName>SRS</userName>
    ''' <sysName>トレーニング</sysName>
    ''' <subSysName>共通ライブラリ</subSysName>
    ''' <summary>
    ''' 処理待ちテーブル用モデル
    ''' </summary>
    ''' <author>SRSTaro</author>
    ''' <Version>
    ''' 001, 2018-01-11, 新規作成
    ''' </Version>
    Public Class CTPROCESSWAIT

        ''' <summary>
        ''' 処理ID
        ''' </summary>
        Public Property ProcessId As String

        ''' <summary>
        ''' 処理区分
        ''' </summary>
        Public Property ProcessType As String

        ''' <summary>
        ''' 処理状態
        ''' </summary>
        Public Property ProcessStatus As String

        ''' <summary>
        ''' ファイル名
        ''' </summary>
        Public Property FileName As String

        ''' <summary>
        ''' ProcessType値を取得します。
        ''' </summary>
        ''' <returns>ProcessType値</returns>
        Public Function GetProcessType() As Constant.ProcessType
            Dim _type As Integer = -1
            Integer.TryParse(Me.ProcessType, _type)
            '' 正常な値かチェック
            If _type < 0 OrElse Not [Enum].IsDefined(GetType(Constant.ProcessType), _type) Then
                Throw New SRS.Standards.MvcFramework.Core.DataAccess.DataAccessException("Invalid process type code")
            End If
            Return DirectCast(_type, Constant.ProcessType)
        End Function

        ''' <summary>
        ''' ProcessStatus値を取得します。
        ''' </summary>
        ''' <returns>ProcessStatus値</returns>
        Public Function GetProcessStatus() As Constant.ProcessStatus
            Dim _status As Integer = -1
            Integer.TryParse(Me.ProcessStatus, _status)
            '' 正常な値かチェック
            If _status < 0 OrElse Not [Enum].IsDefined(GetType(Constant.ProcessStatus), _status) Then
                Throw New SRS.Standards.MvcFramework.Core.DataAccess.DataAccessException("Invalid process status code")
            End If
            Return DirectCast(_status, Constant.ProcessStatus)
        End Function

    End Class

End Namespace
