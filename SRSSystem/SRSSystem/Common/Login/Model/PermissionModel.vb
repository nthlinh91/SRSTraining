Namespace Login.Model

    ''' <userName>SRS</userName>
    ''' <sysName>トレーニング</sysName>
    ''' <subSysName>共通ライブラリ</subSysName>
    ''' <summary>
    ''' 権限データ取得用モデル
    ''' </summary>
    ''' <author>SRSTaro</author>
    ''' <Version>
    ''' 001, 2018-01-11, 新規作成
    ''' </Version>
    Public Class PermissionModel

        ''' <summary>
        ''' 登録可能フラグ
        ''' </summary>
        Public Property CanInsertFlag As String

        ''' <summary>
        ''' 更新可能フラグ
        ''' </summary>
        Public Property CanUpdateFlag As String

        ''' <summary>
        ''' 削除可能フラグ
        ''' </summary>
        Public Property CanDeleteFlag As String

        ''' <summary>
        ''' 検索可能フラグ
        ''' </summary>
        Public Property CanSearchFlag As String

    End Class

End Namespace
