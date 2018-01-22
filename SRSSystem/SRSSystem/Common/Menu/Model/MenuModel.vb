Imports System.ComponentModel
Imports System.ComponentModel.DataAnnotations
Imports SRS.Standards.MvcFramework.Core.Business
Imports SRS.Standards.MvcFramework.Core.MVC

Namespace Menu.Model

    ''' <userName>SRS</userName>
    ''' <sysName>トレーニング</sysName>
    ''' <subSysName>共通ライブラリ</subSysName>
    ''' <summary>
    ''' メニュー画面　メニュー画面モデル
    ''' </summary>
    ''' <author>SRSTaro</author>
    ''' <Version>
    ''' 001, 2018-01-11, 新規作成
    ''' </Version>
    <Serializable()>
    Public Class MenuModel
        Inherits ViewModelBase

        ''' <summary>
        ''' 画面タイトル
        ''' </summary>
        ''' <remarks>固定文言を設定</remarks>
        Public Overrides Property FuncName As String
            Get
                Return "lbl_CMN_MENU"
            End Get
            Set(value As String)
                MyBase.FuncName = value
            End Set
        End Property

        ''' <summary>
        ''' Windowタイトル
        ''' </summary>
        ''' <remarks>固定文言を設定</remarks>
        Public Overrides Property WindowTitle As String
            Get
                Return "lbl_CMN_MENU"
            End Get
            Set(value As String)
                MyBase.WindowTitle = value
            End Set
        End Property

        ''' <summary>
        ''' メニューデータ
        ''' </summary>
        Public Property MenuData As IList(Of MenuElementModel)

        ''' <summary>
        ''' お知らせメッセージ
        ''' </summary>
        Public Property NoticeMessage As String

    End Class

End Namespace

