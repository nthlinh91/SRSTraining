Imports System.ComponentModel
Imports System.ComponentModel.DataAnnotations
Imports SRS.Standards.MvcFramework.Core.Business
Imports SRS.Standards.MvcFramework.Core.MVC

Namespace Menu.Model

    ''' <userName>SRS</userName>
    ''' <sysName>トレーニング</sysName>
    ''' <subSysName>共通ライブラリ</subSysName>
    ''' <summary>
    ''' メニュー画面 メニュー項目モデル
    ''' </summary>
    ''' <author>SRSTaro</author>
    ''' <Version>
    ''' 001, 2018-01-11, 新規作成
    ''' </Version>
    <Serializable()>
    Public Class MenuElementModel
        Inherits ViewModelBase

        ''' <summary>
        ''' メニューID
        ''' </summary>
        Public Property MenuId As String

        ''' <summary>
        ''' URL
        ''' </summary>
        Public Property Url As String

        ''' <summary>
        ''' メニュー区分
        ''' </summary>
        Public Property MenuType As String

        ''' <summary>
        ''' タイトル
        ''' </summary>
        Public Property Title As String

        ''' <summary>
        ''' 子項目
        ''' </summary>
        Public Property Children As IList(Of MenuElementModel)

    End Class

End Namespace


