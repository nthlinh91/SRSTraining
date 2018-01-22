Namespace Constant

    ''' <userName>SRS</userName>
    ''' <sysName>トレーニング</sysName>
    ''' <subSysName>共通ライブラリ</subSysName>
    ''' <summary>
    ''' 業務共通 定数クラス
    ''' </summary>
    ''' <author>SRSTaro</author>
    ''' <Version>
    ''' 001, 2018-01-11, 新規作成
    ''' </Version>
    Public Class CommonConstant

        ''' <summary>
        ''' 円マーク(\)
        ''' </summary>
        Public Const LiteralYenMark As String = "\"

        ''' <summary>
        ''' スラッシュ(/)
        ''' </summary>
        Public Const LiteralSlash As String = "/"

        ''' <summary>
        ''' ユーザ設定用のセクション名。Global.asax にて使用。
        ''' </summary>
        Public Const UserConfigSectionName As String = "SRS.Standards.MvcFramework/UserConfiguration"

    End Class

End Namespace

