Imports System.Text
Imports SRS.Standards.MvcFramework.Core.Business
Imports SRS.Standards.MvcFramework.Core.DataAccess
Imports SRS.Standards.MvcFramework.Core.Utilities
Imports SRS.Standards.MvcFramework.Core.Utilities.Extensions

Namespace Menu.DataAccess

    ''' <userName>SRS</userName>
    ''' <sysName>トレーニング</sysName>
    ''' <subSysName>共通ライブラリ</subSysName>
    ''' <summary>
    ''' メニュー画面 データアクセスクラス
    ''' </summary>
    ''' <author>SRSTaro</author>
    ''' <Version>
    ''' 001, 2018-01-05, 新規作成
    ''' </Version>
    Public Class MenuDataAccess
        Inherits DataAccessBase

        ''' <summary>
        ''' メニュー項目取得
        ''' </summary>
        ''' <param name="identifyId">業務識別ID</param>
        ''' <returns>>管理業務モデルの集合</returns>
        Public Function GetMenuData(identifyId As String) As IList(Of Model.MenuElementModel)

            ' SQL生成
            Dim _sbSqlMain As New StringBuilder()
            _sbSqlMain.Append("SELECT DISTINCT").AppendLine()
            _sbSqlMain.Append("       M.MENUID").AppendLine()
            _sbSqlMain.Append("     , M.URL").AppendLine()
            _sbSqlMain.Append("     , M.TITLE").AppendLine()
            _sbSqlMain.Append("     , M.MENUTYPE").AppendLine()
            _sbSqlMain.Append("     , K.SORTNO").AppendLine()
            _sbSqlMain.Append("  FROM CMMENU As M").AppendLine()
            _sbSqlMain.Append(" INNER JOIN CMMENUKOSEI AS K").AppendLine()
            _sbSqlMain.Append("    ON M.MENUID = K.MENUID").AppendLine()
            _sbSqlMain.Append(" INNER JOIN CMGYOMUSIK AS S").AppendLine()
            _sbSqlMain.Append("    ON K.MENUGROUPID = S.MENUGROUPID").AppendLine()
            _sbSqlMain.Append(" WHERE S.GYOMUSIKID = @BusinessIdentifyId").AppendLine()
            _sbSqlMain.Append("   AND M.DELFLG = '0'").AppendLine()
            _sbSqlMain.Append("   AND K.DELFLG = '0'").AppendLine()
            _sbSqlMain.Append("   AND S.DELFLG = '0'").AppendLine()
            _sbSqlMain.Append(" ORDER BY K.SORTNO").AppendLine()

            'SQL実行

            'DB利用不可対応
            'DBを利用しないためSQL実行はコメントアウトし固定ユーザーSRS1の情報を生成
            'Return Me.ExecuteQuery(Of Model.MenuElementModel)(_sbSqlMain.ToString,
            '                                                 New With {.BusinessIdentifyId = identifyId}).ToList

            Return Utilities.ExcelUtility.MapTo(Of Model.MenuElementModel)("Data\Common\Menu\MenuData.xlsx")

        End Function

    End Class

End Namespace

