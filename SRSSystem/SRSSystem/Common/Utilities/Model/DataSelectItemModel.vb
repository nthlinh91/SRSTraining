Imports System.ComponentModel
Imports System.ComponentModel.DataAnnotations
Imports SRS.Standards.MvcFramework.Core.DataAccess
Imports SRS.Standards.MvcFramework.Core.Business
Imports SRS.Standards.MvcFramework.Core.MVC
Imports SRS.Standards.MvcFramework.Core.Utilities.Extensions

Imports System.Runtime.Serialization

Namespace Utilities.Model

    ''' <userName>SRS</userName>
    ''' <sysName>トレーニング</sysName>
    ''' <subSysName>共通ライブラリ</subSysName>
    ''' <summary>
    ''' データセレクト取得データモデル
    ''' </summary>
    ''' <author>SRSTaro</author>
    ''' <Version>
    ''' 001, 2018-01-11, 新規作成
    ''' </Version>
    <Serializable()>
    <DataContract()>
    Public Class DataSelectItemModel

        ''' <summary>表示テキスト</summary>
        <DataMember()>
        Public Property Text As String

        ''' <summary>値</summary>
        <DataMember()>
        Public Property Value As String

    End Class

End Namespace

