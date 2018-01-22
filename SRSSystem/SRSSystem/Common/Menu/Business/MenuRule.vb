Imports System.Text
Imports System.Text.RegularExpressions
Imports SRS.Standards.MvcFramework.Core.Business
Imports SRS.Standards.MvcFramework.Core.DataAccess
Imports SRS.Standards.MvcFramework.Core.Facade
Imports SRS.Standards.MvcFramework.Core.Information
Imports SRS.Standards.MvcFramework.Core.Logging
Imports SRS.Standards.MvcFramework.Core.Utilities
Imports SRS.Standards.MvcFramework.Core.Utilities.Extensions
Imports SRS.Standards.MvcFramework.Core.Message
Imports SRS.Standards.MvcFramework.Core.MVC

Namespace Menu.Business

    ''' <userName>SRS</userName>
    ''' <sysName>トレーニング</sysName>
    ''' <subSysName>共通ライブラリ</subSysName>
    ''' <summary>
    ''' メニュー画面　Ruleクラス
    ''' </summary>
    ''' <author>SRSTaro</author>
    ''' <Version>
    ''' 001, 2018-01-11, 新規作成
    ''' </Version>
    Public Class MenuRule
        Inherits RuleBase

        ''' <summary>
        ''' メニュー情報取得
        ''' </summary>
        ''' <param name="menuModel">メニュー画面モデル</param>
        ''' <returns>メニュー画面モデル</returns>
        Public Function GetMenuData(menuModel As Model.MenuModel) As Model.MenuModel

            Dim _da As New DataAccess.MenuDataAccess
            ' メニューリスト[DB取得結果]
            Dim _selectMenuList As IList(Of Model.MenuElementModel)
            ' サブメニュー
            Dim _tmpSubMenu As Model.MenuElementModel = Nothing
            ' 構築するメニューデータ
            Dim _menuData As New List(Of Model.MenuElementModel)

            ' メニュー項目取得
            _selectMenuList = _da.GetMenuData(InformationManager.UserInfo.BusinessIdentifyId)

            ' メニュー情報作成
            For Each _elementMenu In _selectMenuList

                ' メニュー区分がサブメニュー[0]の場合
                If _elementMenu.MenuType = Menu.Constant.MenuConstant.SubMenu Then

                    ' サブメニュー情報作成
                    _tmpSubMenu = GetMenuDetail(_elementMenu)
                    _tmpSubMenu.Children = New List(Of Model.MenuElementModel)
                    _menuData.Add(_tmpSubMenu)

                Else
                    ' メニュー区分が業務画面の場合
                    If Not _tmpSubMenu Is Nothing Then
                        ' サブメニューの子項目に設定
                        _tmpSubMenu.Children.Add(GetMenuDetail(_elementMenu))
                    End If
                End If
            Next

            ' 作成したメニューリストをモデルに設定
            menuModel.MenuData = _menuData
            Return menuModel

        End Function

        ''' <summary>
        '''  メニュー項目情報取得
        ''' </summary>
        ''' <param name="memuModel">メニュー項目モデル</param>
        ''' <returns>メニュー項目モデル</returns>
        Public Function GetMenuDetail(memuModel As Model.MenuElementModel) As Model.MenuElementModel

            ' メニュー情報設定
            Return New Model.MenuElementModel With {.MenuId = memuModel.MenuId,
                                                    .Url = memuModel.Url,
                                                    .Title = memuModel.Title
                                                    }

        End Function

        ''' <summary>
        ''' お知らせ取得
        ''' </summary>
        ''' <param name="menuModel">メニュー画面モデル</param>
        ''' <returns>メニュー画面モデル</returns>
        Public Function GetNoticeData(menuModel As Model.MenuModel) As Model.MenuModel

            ' お知らせの取得
            'TODO: ↓↓任意のお知らせを設定してください。
            menuModel.NoticeMessage = "お知らせテスト"
            'TODO: ↑↑任意のお知らせを設定してください。

            Return menuModel

        End Function

    End Class

End Namespace
