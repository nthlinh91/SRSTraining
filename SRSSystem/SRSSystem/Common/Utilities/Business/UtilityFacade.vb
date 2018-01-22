Imports System.Text
Imports SRS.Standards.MvcFramework.Core.Business
Imports SRS.Standards.MvcFramework.Core.Facade
Imports SRS.Standards.MvcFramework.Core.Information
Imports SRS.Standards.MvcFramework.Core.Logging
Imports SRS.Standards.MvcFramework.Core.Utilities
Imports SRS.Standards.MvcFramework.Core.Utilities.Extensions
Imports SRS.Standards.MvcFramework.Core.MVC

Imports SRSSystem.Common.Wait.Constant

Namespace Utilities.Business

    ''' <userName>SRS</userName>
    ''' <sysName>トレーニング</sysName>
    ''' <subSysName>共通ライブラリ</subSysName>
    ''' <summary>
    ''' ユーティリティ Facadeクラス
    ''' </summary>
    ''' <author>SRSTaro</author>
    ''' <Version>
    ''' 001, 2018-01-11, 新規作成
    ''' </Version>
    Partial Public Class UtilityFacade
        Inherits FacadeBase

        ''' <summary>
        ''' データセレクトを取得します。
        ''' </summary>
        ''' <param name="viewModel">画面モデル</param>
        ''' <returns>データセレクト項目モデル</returns>
        Public Function GetDataSelect(viewModel As Model.DataSelectModel) As IEnumerable(Of Model.DataSelectItemModel)

            Dim _rule As New Business.UtilityRule

            ' 取得を実行
            Return _rule.GetDataSelect(viewModel)

        End Function

    End Class

End Namespace

