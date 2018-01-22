Imports System.Web
Imports System.Web.Mvc
Imports System.ComponentModel
Imports System.ComponentModel.DataAnnotations
Imports SRS.Standards.MvcFramework.Core.Business
Imports SRS.Standards.MvcFramework.Core.MVC

Namespace Model

    ''' <userName>住友ゴム工業株式会社</userName>
    ''' <sysName>タイヤ海外業務基幹システム（JUST）</sysName>
    ''' <subSysName>共通</subSysName>
    ''' <summary>
    ''' 最終仕向地検索 一覧画面 メインモデル
    ''' </summary>
    ''' <author>sfunaoka/SDS</author>
    ''' <Version>
    ''' 001, 2015-09-15, 新規作成
    ''' </Version>
    <Serializable()>
    Public Class EC1040ListModel
        Inherits ViewPagerModelBase

        ''' <summary>
        ''' 画面タイトル
        ''' </summary>
        ''' <remarks>固定文言を設定</remarks>
        Public Overrides Property FuncName As String
            Get
                '画面タイトルをセット
                Return "lbl_EC1040"
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
                'Windowタイトルをセット
                Return "lbl_EC1040"
            End Get
            Set(value As String)
                MyBase.WindowTitle = value
            End Set
        End Property

        ''' <summary>
        ''' １ページあたりの行数
        ''' </summary>
        ''' <remarks>固定行数を返す</remarks>
        Public Overrides Property RowCount As Integer
            Get
                '1ページあたりの最大行数をセット
                Return 20
            End Get
            Set(value As Integer)
                MyBase.RowCount = value
            End Set
        End Property

        ''' <summary>
        ''' 仕向地コード(検索条件)
        ''' </summary>
        <DisplayName("lbl_EC1040_SHIMUKECHICD")>
        <SRSStringValidation(True, CharacterType.Numeric, 7, 7)>
        Public Property ShimukechiCdCondition As String

        ''' <summary>
        ''' 最終仕向地名(検索条件)
        ''' </summary>
        <DisplayName("lbl_EC1040_SAISHUSHIMUKECHINM")>
        <SRSStringValidation(False, CharacterType.ASCII, 0, 40)>
        Public Property SaichushimukechiNmCondition As String

        ''' <summary>
        ''' 仕向地コード(引数)
        ''' </summary>
        Public Property ShimukechiCdConditionHidden As String

        ''' <summary>
        ''' 最終仕向地名(引数)
        ''' </summary>
        Public Property SaichushimukechiNmConditionHidden As String

#Region "検索条件保持用"

        ''' <summary>
        ''' 仕向地コード(検索条件保持用)
        ''' </summary>
        Public Property ShimukechiCdConditionSaved As String

        ''' <summary>
        ''' 最終仕向地名(検索条件保持用)
        ''' </summary>
        Public Property SaichushimukechiNmConditionSaved As String

#End Region

        ''' <summary>
        ''' 一覧データ
        ''' </summary>
        Public Property ListData As IList(Of Model.EC1040ListElementModel)

        ''' <summary>
        ''' [選択]
        ''' </summary>
        ''' <remarks>選択された行番号が入る(0スタート)</remarks>
        Public Property SelectedIndex As Integer?

        ''' <summary>
        ''' 業務制御区分
        ''' </summary>
        Public Property GymSeigyoKbn As String

        ''' <summary>
        ''' ログイン代理店区分
        ''' </summary>
        Public Property LoginDaiKbn As String

    End Class

End Namespace

