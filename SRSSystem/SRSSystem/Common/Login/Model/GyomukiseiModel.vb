Namespace Login.Model

    ''' <userName>SRS</userName>
    ''' <sysName>トレーニング</sysName>
    ''' <subSysName>共通ライブラリ</subSysName>
    ''' <summary>
    ''' 業務規制データ取得用モデル
    ''' </summary>
    ''' <author>SRSTaro</author>
    ''' <Version>
    ''' 001, 2018-01-11, 新規作成
    ''' </Version>
    Public Class GyomuKiseiModel
        ''' <summary>
        ''' 業務規制ＩＤ
        ''' </summary>
        Public Property GYOMUKISEIID As String
        ''' <summary>
        ''' 開始日付
        ''' </summary>
        Public Property STARTYMD As String
        ''' <summary>
        ''' 開始時間
        ''' </summary>
        Public Property STARTHHMM As String
        ''' <summary>
        ''' 終了日付
        ''' </summary>
        Public Property ENDYMD As String
        ''' <summary>
        ''' 終了時間
        ''' </summary>
        Public Property ENDHHMM As String
    End Class

End Namespace
