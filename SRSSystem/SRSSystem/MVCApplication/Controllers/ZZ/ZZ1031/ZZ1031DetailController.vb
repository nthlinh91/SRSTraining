Imports SRS.Standards.MvcFramework.Core.MVC
Imports SRS.Standards.MvcFramework.Core.Facade
Imports SRS.Standards.MvcFramework.Core.Information

Imports SRSSystem.ZZ.ZZ1031

Namespace Controllers.ZZ.ZZ1031

    ''' <userName>SRS</userName>
    ''' <sysName>トレーニング</sysName>
    ''' <subSysName>トレニンーグ機能グループ</subSysName>
    ''' <summary>
    ''' 納品見積登録 詳細画面 コントローラ
    ''' </summary>
    ''' <author>SRSTaro</author>
    ''' <Version>
    ''' 001, 2018-01-11, 新規作成
    ''' </Version>
    Public Class ZZ1031DetailController
        Inherits ControllerBase(Of Business.ZZ1031DetailFacade, Model.ZZ1031DetailModel)

        ''' <summary>
        ''' 初期処理
        ''' </summary>
        ''' <returns>ActionResult</returns>
        ''' <permission>View(Model)の結果を返します</permission>
        Function Index() As ActionResult

            ' 初期化処理を実行
            Dim _detailModel = Me.Facade.Index()

            Return View(_detailModel)

        End Function

        ''' <summary>
        ''' 確認ボタン押下時処理
        ''' </summary>
        ''' <param name="detailModel">詳細画面モデル</param>
        ''' <returns>ActionResult</returns>
        ''' <permission>View(Model)の結果を返します</permission>
        Function Confirm(detailModel As Model.ZZ1031DetailModel) As ActionResult

            ' 確認処理を実行
            detailModel = Me.Facade.Confirm(detailModel)
            ModelState.Clear()

            Return View(detailModel)

        End Function

        ''' <summary>
        ''' 登録ボタン押下時処理
        ''' </summary>
        ''' <param name="detailModel">詳細画面モデル</param>
        ''' <returns>ActionResult</returns>
        ''' <permission>View(Model)の結果を返します</permission>
        Function Insert(detailModel As Model.ZZ1031DetailModel) As ActionResult

            ' 登録処理を実行
            detailModel = Me.Facade.Insert(detailModel)
            If detailModel.ValidLevel = ValidLevelType.Normal Then
                ' 処理正常時
                ' 遷移情報にセット
                InformationManager.TransferInfo.TransferData = detailModel
                ' 検索画面へ遷移
                detailModel.RedirectName = Constant.ZZ1031Constant.RedirectSearch
                detailModel.JudgeTrans = JudgeTransType.AnyRedirect
            End If

            Return View(detailModel)

        End Function

        ''' <summary>
        ''' 更新ボタン押下時処理
        ''' </summary>
        ''' <param name="detailModel">詳細画面モデル</param>
        ''' <returns>ActionResult</returns>
        ''' <permission>View(Model)の結果を返します</permission>
        Function Update(detailModel As Model.ZZ1031DetailModel) As ActionResult

            ' 更新処理を実行
            detailModel = Me.Facade.Update(detailModel)
            If detailModel.ValidLevel = ValidLevelType.Normal Then
                ' 処理正常時
                ' 遷移情報にセット
                InformationManager.TransferInfo.TransferData = detailModel
                ' 検索画面へ遷移
                detailModel.RedirectName = Constant.ZZ1031Constant.RedirectSearch
                detailModel.JudgeTrans = JudgeTransType.AnyRedirect
            End If

            Return View(detailModel)

        End Function

        ''' <summary>
        ''' 削除ボタン押下時処理
        ''' </summary>
        ''' <param name="detailModel">詳細画面モデル</param>
        ''' <returns>ActionResult</returns>
        ''' <permission>View(Model)の結果を返します</permission>
        Function Delete(detailModel As Model.ZZ1031DetailModel) As ActionResult

            ' 削除処理を実行
            detailModel = Me.Facade.Delete(detailModel)
            If detailModel.ValidLevel = ValidLevelType.Normal Then
                ' 処理正常時
                ' 遷移情報にセット
                InformationManager.TransferInfo.TransferData = detailModel
                ' 検索画面へ遷移
                detailModel.RedirectName = Constant.ZZ1031Constant.RedirectSearch
                detailModel.JudgeTrans = JudgeTransType.AnyRedirect
            End If

            Return View(detailModel)

        End Function

        ''' <summary>
        ''' キャンセルボタン押下時処理
        ''' </summary>
        ''' <param name="detailModel">詳細画面モデル</param>
        ''' <returns>ActionResult</returns>
        ''' <permission>View(Model)の結果を返します</permission>
        Function Cancel(detailModel As Model.ZZ1031DetailModel) As ActionResult

            ' 遷移情報にセット
            InformationManager.TransferInfo.TransferData = detailModel

            ' 検索画面へ遷移
            detailModel.RedirectName = Constant.ZZ1031Constant.RedirectSearch
            detailModel.JudgeTrans = JudgeTransType.AnyRedirect

            Return View(detailModel)

        End Function

        ''' <summary>
        ''' 計算ボタン押下時処理
        ''' </summary>
        ''' <param name="detailModel">詳細画面モデル</param>
        ''' <returns>ActionResult</returns>
        ''' <permission>View(Model)の結果を返します</permission>
        Function Calculation(detailModel As Model.ZZ1031DetailModel) As ActionResult

            ' 確認処理を実行
            detailModel = Me.Facade.Calculation(detailModel)
            ModelState.Clear()

            Return View(detailModel)

        End Function


    End Class

End Namespace
