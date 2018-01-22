Imports SRS.Standards.MvcFramework.Core.MVC
Imports SRS.Standards.MvcFramework.Core.Facade
Imports SRS.Standards.MvcFramework.Core.Information

Imports SRSSystem.ZZ.ZZ1031

Namespace Controllers.ZZ.ZZ1031

    ''' <userName>SRS</userName>
    ''' <sysName>トレーニング</sysName>
    ''' <subSysName>トレニンーグ機能グループ</subSysName>
    ''' <summary>
    ''' 納品見積登録 検索画面 コントローラ
    ''' </summary>
    ''' <author>SRSTaro</author>
    ''' <Version>
    ''' 001, 2018-01-11, 新規作成
    ''' </Version>
    Public Class ZZ1031SearchController
        Inherits ControllerBase(Of Business.ZZ1031SearchFacade, Model.ZZ1031SearchModel)

        ''' <summary>
        ''' 初期処理
        ''' </summary>
        ''' <returns>ActionResult</returns>
        ''' <permission>View(Model)の結果を返します</permission>
        Function Index() As ActionResult

            ' 初期化処理を実行
            Dim _searchModel = Me.Facade.Index()

            Return View(_searchModel)

        End Function

        ''' <summary>
        ''' 検索ボタン押下時処理
        ''' </summary>
        ''' <param name="searchModel">検索画面モデル</param>
        ''' <returns>ActionResult</returns>
        ''' <permission>View(Model)の結果を返します</permission>
        Function Search(searchModel As Model.ZZ1031SearchModel) As ActionResult

            ' 検索処理を実行
            searchModel = Me.Facade.Search(searchModel)

            If searchModel.ValidLevel = ValidLevelType.Normal Then
                ' 正常時は検索成功
                ' 遷移情報にセット
                InformationManager.TransferInfo.TransferData = searchModel
                ' 詳細画面へ遷移
                searchModel.DetailViewMode = ViewModeType.Detail
                searchModel.RedirectName = Constant.ZZ1031Constant.RedirectDetail
                searchModel.JudgeTrans = JudgeTransType.AnyRedirect
            End If

            Return View(searchModel)

        End Function

        ''' <summary>
        ''' 追加ボタン押下時処理
        ''' </summary>
        ''' <param name="searchModel">検索画面モデル</param>
        ''' <returns>ActionResult</returns>
        ''' <permission>View(Model)の結果を返します</permission>
        Function Add(searchModel As Model.ZZ1031SearchModel) As ActionResult

            ' 遷移情報にセット
            InformationManager.TransferInfo.TransferData = searchModel

            ' 詳細画面へ遷移
            searchModel.DetailViewMode = ViewModeType.Insert
            searchModel.RedirectName = Constant.ZZ1031Constant.RedirectDetail
            searchModel.JudgeTrans = JudgeTransType.AnyRedirect

            Return View(searchModel)

        End Function

    End Class

End Namespace
