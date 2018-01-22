Imports System.Text
Imports SRS.Standards.MvcFramework.Core.Facade
Imports SRS.Standards.MvcFramework.Core.Business
Imports SRS.Standards.MvcFramework.Core.Utilities
Imports SRS.Standards.MvcFramework.Core.MVC
Imports SRS.Standards.MvcFramework.Core.Constant

Namespace Constant

    ''' <userName>住友ゴム工業株式会社</userName>
    ''' <sysName>タイヤ海外業務基幹システム（JUST）</sysName>
    ''' <subSysName>共通</subSysName>
    ''' <summary>
    ''' 最終仕向地検索 定数クラス
    ''' </summary>
    ''' <author>sfunaoka/SDS</author>
    ''' <Version>
    ''' 001, 2015-09-15, 新規作成
    ''' </Version>
    Public Class EC1040Constant

        ''' <summary>
        ''' 業務制御区分（T海販）
        ''' </summary>
        Public Const GymSeigyoKbnTkaiHan As String = "TKAIHAN"

        ''' <summary>
        ''' 業務制御区分（T海業）
        ''' </summary>
        Public Const GymSeigyoKbnTkaiGyo As String = "TKAIGYO"

        ''' <summary>
        ''' 業務制御区分（OE部署）
        ''' </summary>
        Public Const GymSeigyoKbnOEBusho As String = "OE"

        ''' <summary>
        ''' 業務制御区分（統括販社）
        ''' </summary>
        Public Const GymSeigyoKbnHansha As String = "TOKATSUHANSHA"

        ''' <summary>
        ''' 業務制御区分（その他）
        ''' </summary>
        Public Const GymSeigyoKbnSonota As String = "SONOTA"

        ''' <summary>
        ''' 業務制御区分（代理店区分）
        ''' </summary>
        Public Const GymSeigyoKbnDairiten As String = "DAIRITENKBN"

        ''' <summary>
        ''' 業務制御コード（T海販）
        ''' </summary>
        Public Const GymSeigyoCdTkaiHan As String = "1"

        ''' <summary>
        ''' 業務制御コード（T海業）
        ''' </summary>
        Public Const GymSeigyoCdTkaiGyo As String = "1"

        ''' <summary>
        ''' 業務制御コード（OE部署）
        ''' </summary>
        Public Const GymSeigyoCdOEBusho As String = "1"

        ''' <summary>
        ''' 業務制御コード（統括販社）
        ''' </summary>
        Public Const GymSeigyoCdHansha As String = "1"

    End Class

End Namespace

