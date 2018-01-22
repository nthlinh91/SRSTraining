Imports System.ComponentModel
Imports System.ComponentModel.DataAnnotations
Imports SRS.Standards.MvcFramework.Core.Business
Imports SRS.Standards.MvcFramework.Core.MVC

Namespace Model

    ''' <userName>住友ゴム工業株式会社</userName>
    ''' <sysName>タイヤ海外業務基幹システム（JUST）</sysName>
    ''' <subSysName>共通</subSysName>
    ''' <summary>
    ''' 最終仕向地検索 一覧画面 明細データモデル
    ''' </summary>
    ''' <author>sfunaoka/SDS</author>
    ''' <Version>
    ''' 001, 2015-09-15, 新規作成
    ''' </Version>
    <Serializable()>
    Public Class EC1040ListElementModel
        Inherits ViewElementModelBase

        ''' <summary>
        ''' 仕向地
        ''' </summary>
        <DisplayName("lbl_EC1040_SHIMUKECHI")>
        Public Property SHIMUKECHI As String

        ''' <summary>
        ''' 最終仕向地
        ''' </summary>
        <DisplayName("lbl_EC1040_SAISHUSHIMUKECHI")>
        Public Property SAISHUSHIMUKECHI As String

        ''' <summary>
        ''' 仕向地コード
        ''' </summary>
        Public Property SHIMUKECHICD As String

        ''' <summary>
        ''' 仕向地名
        ''' </summary>
        Public Property SHIMUKECHINM As String

        ''' <summary>
        ''' 最終仕向地コード
        ''' </summary>
        Public Property SAISHUSHIMUKECHICD As String

        ''' <summary>
        ''' 最終仕向地名
        ''' </summary>
        Public Property SAISHUSHIMUKECHINM As String

        ''' <summary>
        ''' 最終仕向地名略称
        ''' </summary>
        Public Property SAISHUSHIMUKECHIRNM As String
    End Class

End Namespace

