Imports System.Globalization
Imports System.Text.RegularExpressions
Imports SRS.Standards.MvcFramework.Core.Information

Namespace Utilities

    ''' <userName>SRS</userName>
    ''' <sysName>トレーニング</sysName>
    ''' <subSysName>共通ライブラリ</subSysName>
    ''' <summary>
    ''' システム基盤に関係するユーティリティ関数を定義します。
    ''' </summary>
    ''' <author>SRSTaro</author>
    ''' <Version>
    ''' 001, 2018-01-11, 新規作成
    ''' </Version>
    Public NotInheritable Class DBUtility

        ''' <summary>
        ''' ダミーコンストラクタ
        ''' </summary>
        Private Sub New()
        End Sub

        ''' <summary>
        ''' ダウンロードしたファイルの履歴をログに記録します。
        ''' </summary>
        ''' <param name="viewModel">画面モデル。</param>
        ''' <remarks>ファイルの記録は、Filter経由で行う
        ''' 仮実装。最終的にはテーブルに記録する。</remarks>
        Public Shared Sub LogDownloadFile(viewModel As SRS.Standards.MvcFramework.Core.Facade.IViewModel)

            SRS.Standards.MvcFramework.Core.Logging.LogManager.BusinessLog.Info(viewModel.DownloadFilePath)

        End Sub

        ''' <summary>
        ''' 機能IDに対するアクセス回数を1増やします。
        ''' </summary>
        ''' <param name="menuId">機能ID。</param>
        ''' <returns>アクセス回数。</returns>
        Public Shared Function CountUpActivity(menuId As String) As Long

            Dim _da As New DataAccess.UtilityDataAccess

            Return _da.CountUpActivity(menuId)

        End Function

    End Class

End Namespace
