Imports SRS.Standards.MvcFramework.Core.MVC
Imports SRS.Standards.MvcFramework.Core.Facade
Imports SRS.Standards.MvcFramework.Core.Information

Imports JUST.EC.EC1040

Namespace Controllers.EC.EC1040

    ''' <userName>住友ゴム工業株式会社</userName>
    ''' <sysName>タイヤ海外業務基幹システム（JUST）</sysName>
    ''' <subSysName>共通</subSysName>
    ''' <summary>
    ''' 最終仕向地検索 一覧画面 コントローラ
    ''' </summary>
    ''' <author>sfunaoka/SDS</author>
    ''' <Version>
    ''' 001, 2015-09-15, 新規作成
    ''' </Version>
    Public Class EC1040ListController
        Inherits ControllerBase(Of Business.EC1040ListFacade, Model.EC1040ListModel)

        ''' <summary>
        ''' 初期処理
        ''' </summary>
        ''' <returns>ActionResult</returns>
        ''' <permission>View(Model)の結果を返します</permission>
        Function Index() As ActionResult

            ' 初期化処理を実行
            Dim _listModel = Me.Facade.Index()

            Return View(_listModel)

        End Function

        ''' <summary>
        ''' 検索ボタン押下時処理
        ''' </summary>
        ''' <param name="listModel">一覧画面モデル</param>
        ''' <returns>ActionResult</returns>
        ''' <permission>View(Model)の結果を返します</permission>
        Function Search(listModel As Model.EC1040ListModel) As ActionResult

            ' 1ページ目を設定
            listModel.PageNum = 1
            ' 検索処理を実行
            listModel = Me.Facade.Search(listModel, True)

            Return View(listModel)

        End Function

        ''' <summary>
        ''' 前ページボタン押下時処理
        ''' </summary>
        ''' <param name="listModel">一覧画面モデル</param>
        ''' <returns>ActionResult</returns>
        ''' <permission>View(Model)の結果を返します</permission>
        Function PreviousPage(listModel As Model.EC1040ListModel) As ActionResult

            ' 1ページ前を設定
            listModel.PageNum -= 1
            ' 検索処理を実行
            listModel = Me.Facade.Search(listModel)

            Return View(listModel)

        End Function

        ''' <summary>
        ''' 次ページボタン押下時処理
        ''' </summary>
        ''' <param name="listModel">一覧画面モデル</param>
        ''' <returns>ActionResult</returns>
        ''' <permission>View(Model)の結果を返します</permission>
        Function NextPage(listModel As Model.EC1040ListModel) As ActionResult

            ' 1ページ後を設定
            listModel.PageNum += 1
            ' 検索処理を実行
            listModel = Me.Facade.Search(listModel)

            Return View(listModel)

        End Function

        ''' <summary>
        ''' ページ番号選択時処理
        ''' </summary>
        ''' <param name="listModel">一覧画面モデル</param>
        ''' <returns>ActionResult</returns>
        ''' <permission>View(Model)の結果を返します</permission>
        Function SelectPage(listModel As Model.EC1040ListModel) As ActionResult

            ' 選択したページ番号を設定
            listModel.PageNum = listModel.SelectPageNum
            ' 検索処理を実行
            listModel = Me.Facade.Search(listModel)

            Return View(listModel)

        End Function

    End Class

End Namespace
