Imports System.Text
Imports SRS.Standards.MvcFramework.Core.Facade
Imports SRS.Standards.MvcFramework.Core.Business
Imports SRS.Standards.MvcFramework.Core.Utilities
Imports SRS.Standards.MvcFramework.Core.MVC

Namespace Menu.Business

    ''' <userName>SRS</userName>
    ''' <sysName>トレーニング</sysName>
    ''' <subSysName>共通ライブラリ</subSysName>
    ''' <summary>
    ''' メニュー画面　Facadeクラス
    ''' </summary>
    ''' <author>SRSTaro</author>
    ''' <Version>
    ''' 001, 2018-01-11, 新規作成
    ''' </Version>
    Public Class MenuFacade
        Inherits FacadeBase

        ''' <summary>
        ''' メニュー画面 初期化処理
        ''' </summary>
        ''' <returns>メニュー画面モデル</returns>
        <TransactionScope(Enabled:=False)>
        Public Function Init() As Model.MenuModel

            ' モデルを生成
            Dim _menuModel As New Model.MenuModel
            Dim _rule As New Menu.Business.MenuRule

            ' メニュー項目取得
            _menuModel = _rule.GetMenuData(_menuModel)

            ' お知らせ取得
            _menuModel = _rule.GetNoticeData(_menuModel)

            Return _menuModel

        End Function

    End Class

End Namespace
