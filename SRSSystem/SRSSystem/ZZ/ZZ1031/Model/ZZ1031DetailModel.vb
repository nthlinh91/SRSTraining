Imports System.Web.Mvc
Imports System.ComponentModel
Imports System.ComponentModel.DataAnnotations
Imports SRS.Standards.MvcFramework.Core.DataAccess
Imports SRS.Standards.MvcFramework.Core.Business
Imports SRS.Standards.MvcFramework.Core.MVC
Imports SRS.Standards.MvcFramework.Core.Utilities.Extensions

Namespace Model

    ''' <userName>SRS</userName>
    ''' <sysName>トレーニング</sysName>
    ''' <subSysName>トレニンーグ機能グループ</subSysName>
    ''' <summary>
    ''' 納品見積登録 詳細画面モデル
    ''' </summary>
    ''' <author>SRSTaro</author>
    ''' <Version>
    ''' 001, 2018-01-11, 新規作成
    ''' </Version>
    <Serializable()>
    Public Class ZZ1031DetailModel
        Inherits ViewModelBase

        ''' <summary>
        ''' 画面タイトル
        ''' </summary>
        ''' <remarks>固定文言を設定</remarks>
        Public Overrides Property FuncName As String
            Get
                'TODO: ↓↓画面タイトルのメッセージIDを修正してください。
                Return "#{lbl_ZZ_SINGLEUPDATEPATTERN} ({lbl_ZZ_DETAIL})"
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
                Return "#{lbl_ZZ_SINGLEUPDATEPATTERN} ({lbl_ZZ_DETAIL})"
                'TODO: ↑↑WindowタイトルのメッセージIDを修正してください。
            End Get
            Set(value As String)
                MyBase.WindowTitle = value
            End Set
        End Property

        'TODO: ↓↓詳細画面の項目に合わせて修正してください。
        ''' <summary>
        ''' 見積No
        ''' </summary>
        <DisplayName("lbl_ZZ_ESTIMATENO")>
        Public Property EstimateNo As String

        ''' <summary>
        ''' アイテムNo
        ''' </summary>
        <DisplayName("lbl_ZZ_ITEM")>
        Public Property Item As String


        ''' <summary>
        ''' 最終仕向先
        ''' </summary>
        <DisplayName("lbl_ZZ_FINALDES")>
        Public Property FinalDestination As String

        ''' <summary>
        ''' 参照ボタン
        ''' </summary>
        <DisplayName("lbl_ZZ_REFERENCE")>
        Public Property ReferenceRnm As String

        ''' <summary>
        ''' セットリスト
        ''' </summary>
        <DisplayName("lbl_ZZ_SET")>
        Public Property ListSet As String

        ''' <summary>
        ''' 単価
        ''' </summary>
        <DisplayName("lbl_ZZ_UNITCOST")>
        Public Property UnitCost As Integer

        ''' <summary>
        ''' 見積数
        ''' </summary>
        <DisplayName("lbl_ZZ_ESTIMATEBUMBER")>
        Public Property EstimateNumber As Integer

        ''' <summary>
        ''' 価格
        ''' </summary>
        <DisplayName("lbl_ZZ_PRICE")>
        Public Property Price As Integer

        ''' <summary>
        ''' 工場
        ''' </summary>
        <DisplayName("lbl_ZZ_FACTORY")>
        Public Property ListFactory As String

        ''' <summary>
        ''' 搬入期限
        ''' </summary>
        <DisplayName("lbl_ZZ_DELIVERYTIME")>
        Public Property DeliveryTime As String

        ''' <summary>
        ''' 備考
        ''' </summary>
        <DisplayName("lbl_ZZ_REMARK")>
        Public Property Remark As String


        ''' <summary>
        ''' リストSETデータ
        ''' </summary>
        Public Property ListSetData As IEnumerable(Of SRSSelectListItem)

        ''' <summary>
        ''' リストSETデータ
        ''' </summary>
        Public Property ListFactoryData As IEnumerable(Of SRSSelectListItem)

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

        'TODO: ↑↑詳細画面の項目に合わせて修正してください。

        'TODO: ↓↓検索条件に合わせて修正してください。
        ''' <summary>
        ''' 検索時の見積No
        ''' </summary>
        Public Property SearchEstimateNo As String
        'TODO: ↑↑検索条件に合わせて修正してください。

    End Class

End Namespace

