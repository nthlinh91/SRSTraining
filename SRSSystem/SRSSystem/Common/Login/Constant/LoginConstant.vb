Imports System.Text
Imports SRS.Standards.MvcFramework.Core.Facade
Imports SRS.Standards.MvcFramework.Core.Business
Imports SRS.Standards.MvcFramework.Core.Utilities
Imports SRS.Standards.MvcFramework.Core.MVC
Imports SRS.Standards.MvcFramework.Core.Constant

Namespace Login.Constant

    ''' <userName>SRS</userName>
    ''' <sysName>トレーニング</sysName>
    ''' <subSysName>共通ライブラリ</subSysName>
    ''' <summary>
    ''' ログイン画面用 定数クラス
    ''' </summary>
    ''' <author>SRSTaro</author>
    ''' <Version>
    ''' 001, 2018-01-11, 新規作成
    ''' </Version>
    Public Class LoginConstant

        ''' <summary>ログイン失敗回数を覚えておくためのセッションキー文字列</summary>
        Public Const LoginFailureKey As String = "LoginFailure"

        ''' <summary>ログイン失敗を許す回数。</summary>
        Public Const LoginFailureMax = 2

    End Class

End Namespace

