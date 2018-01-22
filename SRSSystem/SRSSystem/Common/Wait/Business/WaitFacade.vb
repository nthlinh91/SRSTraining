Imports System.Text
Imports SRS.Standards.MvcFramework.Core.Facade
Imports SRS.Standards.MvcFramework.Core.Business
Imports SRS.Standards.MvcFramework.Core.Utilities
Imports SRS.Standards.MvcFramework.Core.MVC

Namespace Wait.Business

    ''' <userName>SRS</userName>
    ''' <sysName>トレーニング</sysName>
    ''' <subSysName>共通ライブラリ</subSysName>
    ''' <summary>
    ''' 待ち画面用(サンプル)業務Facadeクラス
    ''' </summary>
    ''' <author>SRSTaro</author>
    ''' <Version>
    ''' 001, 2018-01-11, 新規作成
    ''' </Version>
    Public Class WaitFacade
        Inherits FacadeBase

        ''' <summary>
        ''' 待ち画面 初期化処理
        ''' </summary>
        ''' <param name="id">処理ID</param>
        ''' <returns>待ち画面モデル</returns>
        <TransactionScope(Enabled:=False)>
        Public Function Init(id As String) As Model.WaitModel

            Dim _waitModel As New Model.WaitModel()

            ' 帳票処理IDを設定する
            _waitModel.ProcessId = id
            _waitModel.Waiting = True

            ' 状態を取得して状態に反映
            Dim _rule As New WaitRule()
            _rule.GetProcessStatus(_waitModel)

            Return _waitModel

        End Function

        ''' <summary>
        ''' 待ち画面 状態監視処理
        ''' </summary>
        ''' <param name="waitModel">待ち画面モデル</param>
        ''' <returns>待ち画面モデル</returns>
        <TransactionScope(Enabled:=False)>
        Public Function Watch(waitModel As Model.WaitModel) As Model.WaitModel

            ' 状態を取得して状態に反映
            Dim _rule As New WaitRule()
            _rule.GetProcessStatus(waitModel)

            Return waitModel

        End Function

    End Class

End Namespace

