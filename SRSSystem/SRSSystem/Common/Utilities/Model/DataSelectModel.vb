Imports System.ComponentModel
Imports System.ComponentModel.DataAnnotations
Imports SRS.Standards.MvcFramework.Core.DataAccess
Imports SRS.Standards.MvcFramework.Core.Business
Imports SRS.Standards.MvcFramework.Core.MVC
Imports SRS.Standards.MvcFramework.Core.Utilities.Extensions

Namespace Utilities.Model

    ''' <userName>SRS</userName>
    ''' <sysName>トレーニング</sysName>
    ''' <subSysName>共通ライブラリ</subSysName>
    ''' <summary>
    ''' データセレクト取得用モデル
    ''' </summary>
    ''' <author>SRSTaro</author>
    ''' <Version>
    ''' 001, 2018-01-11, 新規作成
    ''' </Version>
    <Serializable()>
    Public Class DataSelectModel
        Inherits ViewModelBase

        ''' <summary>
        ''' データセレクト名称
        ''' </summary>
        Public Property DataSelectName As String

        ''' <summary>
        ''' 検索パラメータ
        ''' </summary>
        Public Property Parameters As IEnumerable(Of String())

    End Class

End Namespace

