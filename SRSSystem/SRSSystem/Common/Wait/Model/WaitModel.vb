Imports System.ComponentModel
Imports System.ComponentModel.DataAnnotations
Imports SRS.Standards.MvcFramework.Core.Business
Imports SRS.Standards.MvcFramework.Core.MVC

Namespace Wait.Model

    ''' <userName>SRS</userName>
    ''' <sysName>トレーニング</sysName>
    ''' <subSysName>共通ライブラリ</subSysName>
    ''' <summary>
    ''' 待ち画面用(サンプル)モデル
    ''' </summary>
    ''' <author>SRSTaro</author>
    ''' <Version>
    ''' 001, 2018-01-11, 新規作成
    ''' </Version>
    <Serializable()>
    Public Class WaitModel
        Inherits ViewModelBase

        ''' <summary>
        ''' 処理ID
        ''' </summary>
        Public Property ProcessId As String

        ''' <summary>
        ''' 現在待ち状態かどうか (Trueのとき待ち状態)
        ''' </summary>
        Public Property Waiting As Boolean

        ''' <summary>
        ''' 処理種別
        ''' </summary>
        Public Property ProcessType As Constant.ProcessType

        ''' <summary>
        ''' 処理状態
        ''' </summary>
        Public Property ProcessStatus As Constant.ProcessStatus

        ''' <summary>
        ''' 待ち画面に表示するメッセージ
        ''' </summary>
        Public Property DisplayMessage As String

        ''' <summary>
        ''' 画面タイトル
        ''' </summary>
        ''' <remarks>既定文言を設定</remarks>
        Public Overrides Property FuncName As String = "CMN_N00036"

        ''' <summary>
        ''' Windowタイトル
        ''' </summary>
        ''' <remarks>既定文言を設定</remarks>
        Public Overrides Property WindowTitle As String = "CMN_N00036"

    End Class

End Namespace

