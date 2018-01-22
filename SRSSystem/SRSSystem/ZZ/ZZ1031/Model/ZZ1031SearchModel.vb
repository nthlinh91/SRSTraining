Imports System.Web
Imports System.Web.Mvc
Imports System.ComponentModel
Imports System.ComponentModel.DataAnnotations
Imports SRS.Standards.MvcFramework.Core.Business
Imports SRS.Standards.MvcFramework.Core.MVC

Imports System.Linq.Expressions

Namespace Model

    ''' <userName>SRS</userName>
    ''' <sysName>トレーニング</sysName>
    ''' <subSysName>トレニンーグ機能グループ</subSysName>
    ''' <summary>
    ''' 納品見積登録 検索画面 メインモデル
    ''' </summary>
    ''' <author>SRSTaro</author>
    ''' <Version>
    ''' 001, 2018-01-11, 新規作成
    ''' </Version>
    <Serializable()>
    Public Class ZZ1031SearchModel
        Inherits ViewModelBase

        ''' <summary>
        ''' 画面タイトル
        ''' </summary>
        ''' <remarks>固定文言を設定</remarks>
        Public Overrides Property FuncName As String
            Get
                'TODO: ↓↓画面タイトルのメッセージIDを修正してください。
                Return "lbl_ZZ_SINGLEUPDATEPATTERN"
                'TODO: ↑↑画面タイトルのメッセージIDを修正してください。
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
                'TODO: ↓↓WindowタイトルのメッセージIDを修正してください。
                Return "lbl_ZZ_SINGLEUPDATEPATTERN"
                'TODO: ↑↑WindowタイトルのメッセージIDを修正してください。
            End Get
            Set(value As String)
                MyBase.WindowTitle = value
            End Set
        End Property

        'TODO: ↓↓画面の検索項目に合わせて修正してください。
        ''' <summary>
        ''' 見積No(検索条件)
        ''' </summary>
        <DisplayName("lbl_ZZ_ESTIMATENO")>
        Public Property EstimateNoCondition As String

        ''' <summary>
        ''' 開始日
        ''' </summary>
        <DisplayName("lbl_ZZ_RYAKUSYOHANKAKU")>
        Public Property DateFrom As String
        ''' <summary>
        ''' 終了日
        ''' </summary>
        <DisplayName("lbl_ZZ_RYAKUSYOHANKAKU")>
        Public Property DateTo As String


        ''' <summary>
        ''' リストセットデータ
        ''' </summary>
        Public Property ListSetData As IEnumerable(Of SRSSelectListItem)

        ''' <summary>
        ''' リスト工場データ
        ''' </summary>
        Public Property ListFactoryData As IEnumerable(Of SRSSelectListItem)

        'TODO: ↑↑画面の検索項目に合わせて修正してください。

        ''' <summary>
        ''' 詳細画面のViewMode
        ''' </summary>
        Public Property DetailViewMode As ViewModeType

    End Class

End Namespace

