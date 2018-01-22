Imports System.Text
Imports SRS.Standards.MvcFramework.Core.Facade
Imports SRS.Standards.MvcFramework.Core.Business
Imports SRS.Standards.MvcFramework.Core.Utilities
Imports SRS.Standards.MvcFramework.Core.MVC
Imports SRS.Standards.MvcFramework.Core.Constant

Namespace Constant

    ''' <userName>SRS</userName>
    ''' <sysName>トレーニング</sysName>
    ''' <subSysName>トレニンーグ機能グループ</subSysName>
    ''' <summary>
    ''' 納品見積登録 定数クラス
    ''' </summary>
    ''' <author>SRSTaro</author>
    ''' <Version>
    ''' 001, 2018-01-11, 新規作成
    ''' </Version>
    Public Class ZZ1031Constant

        'TODO：↓↓各画面で使用する定数(パス、リテラル文字列など)を定義してください。
        ''' <summary>
        ''' 検索画面遷移文字列
        ''' </summary>
        Public Const RedirectSearch As String = "ZZ/ZZ1031/ZZ1031Search"

        ''' <summary>
        ''' 詳細画面遷移文字列
        ''' </summary>
        Public Const RedirectDetail As String = "ZZ/ZZ1031/ZZ1031Detail"

        ''' <summary>
        ''' ＫＥＹコードの文字列長
        ''' </summary>
        Public Const LengthKeyCd As Integer = 5

        ''' <summary>
        ''' 略称(全角)の文字列長
        ''' </summary>
        Public Const LengthRyakusyoZenkaku As Integer = 12

        ''' <summary>
        ''' 略称(半角)の文字列長
        ''' </summary>
        Public Const LengthRyakusyoHankaku As Integer = 12

        ''' <summary>
        ''' 数字コードの最小値
        ''' </summary>
        Public Const MinSujiCd As Integer = 0

        ''' <summary>
        ''' 数字コードの最大値
        ''' </summary>
        Public Const MaxSujiCd As Integer = 9999
        'TODO：↑↑各画面で使用する定数(パス、リテラル文字列など)を定義してください。

    End Class

End Namespace

