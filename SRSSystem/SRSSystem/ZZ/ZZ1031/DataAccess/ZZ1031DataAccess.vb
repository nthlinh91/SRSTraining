Imports System.Text
Imports System.Web.Mvc
Imports SRS.Standards.MvcFramework.Core.Business
Imports SRS.Standards.MvcFramework.Core.DataAccess
Imports SRS.Standards.MvcFramework.Core.Information
Imports SRS.Standards.MvcFramework.Core.Message
Imports SRS.Standards.MvcFramework.Core.MVC
Imports SRS.Standards.MvcFramework.Core.Utilities
Imports SRS.Standards.MvcFramework.Core.Utilities.Extensions

Imports SRSSystem.Common.Utilities

Namespace DataAccess

    ''' <userName>SRS</userName>
    ''' <sysName>トレーニング</sysName>
    ''' <subSysName>トレニンーグ機能グループ</subSysName>
    ''' <summary>
    ''' 納品見積登録 データアクセスクラス
    ''' </summary>
    ''' <author>SRSTaro</author>
    ''' <Version>
    ''' 001, 2018-01-11, 新規作成
    ''' </Version>
    Public Class ZZ1031DataAccess
        Inherits DataAccessBase

        'TODO：↓↓検索画面の明細件数を取得してください。
        ''' <summary>
        ''' 検索画面 詳細データ件数取得
        ''' </summary>
        ''' <param name="searchModel">詳細画面モデル</param>
        ''' <returns>データ件数</returns>
        Public Function GetDetailCount(searchModel As Model.ZZ1031SearchModel) As Integer

            ' 引数を作成
            '' TODO：↓↓引数を編集してください。
            Dim _param As New Dapper.DynamicParameters()
            _param.Add("IVC_ESTIMATENO", searchModel.EstimateNoCondition)
            '' TODO：↑↑引数を編集してください。
            _param.Add("RETURN", direction:=ParameterDirection.ReturnValue)

            '' ストアドプロシージャ実行
            ' '' TODO：↓↓ストアド名を編集してください。
            'Dim _result = Me.ExecuteStoredProcedure(Of Object)("ZZ0000Search_P_GET_ZZSAMPLECOUNT", _param).ToList
            ' '' TODO：↑↑ストアド名を編集してください。
            'Dim _returnValue = _param.Get(Of Integer)("RETURN")


            '' クエリ実行の場合
            '' TODO：↓↓クエリを編集してください。
            Dim _sqlText As New StringBuilder()
            _sqlText.AppendLine("SELECT")
            _sqlText.AppendLine("    COUNT(*)")
            _sqlText.AppendLine("FROM")
            _sqlText.AppendLine("    ZZSAMPLE")
            _sqlText.AppendLine("WHERE")
            _sqlText.AppendLine("    ESTIMATENO = @IVC_ESTIMATENO")
            ' '' TODO：↑↑クエリを編集してください。
            'Dim _result = Me.ExecuteQuery(Of Integer)(_sqlText.ToString(), _param).ToList
            ' 結果が1つと仮定して取得
            'Dim _returnValue = _result.Single


            ' 検索実行
            'Return _returnValue
            Return 1

        End Function
        'TODO：↑↑検索画面の明細件数を取得してください。

        'TODO：↓↓詳細画面の項目内容を取得してください。
        ''' <summary>
        ''' 詳細画面 詳細データ取得
        ''' </summary>
        ''' <param name="detailModel">詳細画面モデル</param>
        ''' <returns>詳細画面モデルリスト</returns>
        Public Function GetDetail(detailModel As Model.ZZ1031DetailModel) As IEnumerable(Of Model.ZZ1031DetailModel)

            ' 引数を作成
            '' TODO：↓↓引数を編集してください。
            Dim _param As New Dapper.DynamicParameters()
            _param.Add("IVC_ESTIMATENO", detailModel.EstimateNo)
            '' TODO：↑↑引数を編集してください。

            '' ストアドプロシージャ実行の場合
            '_param.Add("RETURN", direction:=ParameterDirection.ReturnValue)
            ' '' TODO：↓↓ストアド名を編集してください。
            'Dim _result = Me.ExecuteStoredProcedure(Of Model.ZZ1031DetailModel)("ZZ0000Detail_P_GET_ZZSAMPLE", _param).ToList
            ' '' TODO：↑↑ストアド名を編集してください。
            'Dim _returnValue = _param.Get(Of Integer)("RETURN")


            '' クエリ実行の場合
            '' TODO：↓↓クエリを編集してください。
            Dim _sqlText As New StringBuilder()
            _sqlText.AppendLine("SELECT")
            _sqlText.AppendLine("    M.ESTIMATENO")
            _sqlText.AppendLine("  , M.ITEM")
            _sqlText.AppendLine("  , M.FINALDES")
            _sqlText.AppendLine("  , M.SET")
            _sqlText.AppendLine("  , M.UNITPRICE")
            _sqlText.AppendLine("  , M.ESTIMATEBUMBER")
            _sqlText.AppendLine("  , M.FACTORY")
            _sqlText.AppendLine("  , M.DELIVERYTIME")
            _sqlText.AppendLine("  , M.REMARK")
            '' ↓↓結果に必ずROWIDとREVISIONを含めてください
            _sqlText.AppendLine("  , M.ROWID")
            _sqlText.AppendLine("  , M.REVISION")
            _sqlText.AppendLine("FROM")
            _sqlText.AppendLine("    ZZSAMPLE M")
            _sqlText.AppendLine("INNER JOIN")
            _sqlText.AppendLine("    ZZSAMPLENAME N")
            _sqlText.AppendLine("ON")
            '_sqlText.AppendLine("    N.LISTCD = M.LISTCD")
            _sqlText.AppendLine("WHERE")
            _sqlText.AppendLine("    M.ESTIMATENO = @IVC_ESTIMATENO")
            ' '' TODO：↑↑クエリを編集してください。
            'Dim _result = Me.ExecuteQuery(Of Model.ZZ1031DetailModel)(_sqlText.ToString(), _param).ToList

            'Return _result

            Return ExcelUtility.MapTo(Of Model.ZZ1031DetailModel)("Data\ZZ\ZZ1031\ZZ1031Data.xlsx")

        End Function
        'TODO：↑↑詳細画面の項目内容を取得してください。

        'TODO：↓↓詳細画面の登録内容を修正してください。
        ''' <summary>
        ''' 詳細画面 詳細データ登録処理
        ''' </summary>
        ''' <param name="detailModel">詳細画面モデル</param>
        ''' <returns>処理件数</returns>
        Public Function InsertDetail(detailModel As Model.ZZ1031DetailModel) As Integer

            ' 引数を作成
            '' TODO：↓↓引数を編集してください。
            Dim _param As New Dapper.DynamicParameters()
            '_param.Add("IVC_ESTIMATENO", detailModel.EstimateNo)
            '_param.Add("IVC_LISTCD", detailModel.ListCd)
            '_param.Add("IVC_SUJICD", detailModel.SujiCd)
            '_param.Add("IVC_RYAKUSYOZENKAKU", detailModel.RyakusyoZenkaku)
            '_param.Add("IVC_RYAKUSYOHANKAKU", detailModel.RyakusyoHankaku)
            _param.Add("IVC_USERID", detailModel.LoginUserId)
            _param.Add("IVC_PGID", detailModel.CurrentMenuId)
            '' TODO：↑↑引数を編集してください。

            '' ストアドプロシージャ実行の場合
            '_param.Add("RETURN", direction:=ParameterDirection.ReturnValue)
            ' '' TODO：↓↓ストアド名を編集してください。
            'Dim _result = Me.ExecuteStoredProcedure(Of Object)("ZZ0000Detail_P_INS_ZZSAMPLE", _param).ToList
            ' '' TODO：↑↑ストアド名を編集してください。
            'Dim _returnValue = _param.Get(Of Integer)("RETURN")


            '' クエリ実行の場合
            '' TODO：↓↓クエリを編集してください。
            Dim _sqlText As New StringBuilder()
            _sqlText.AppendLine("INSERT INTO ZZSAMPLE (")
            _sqlText.AppendLine("    KEYCD")
            _sqlText.AppendLine("  , INSDATE")
            _sqlText.AppendLine("  , INSCD")
            _sqlText.AppendLine("  , INSPGID")
            _sqlText.AppendLine("  , UPDDATE")
            _sqlText.AppendLine("  , UPDCD")
            _sqlText.AppendLine("  , UPDPGID")
            _sqlText.AppendLine("  , LISTCD")
            _sqlText.AppendLine("  , SUJICD")
            _sqlText.AppendLine("  , RYAKUSYOZENKAKU")
            _sqlText.AppendLine("  , RYAKUSYOHANKAKU")
            _sqlText.AppendLine(") VALUES (")
            _sqlText.AppendLine("    @IVC_ESTIMATENO")
            _sqlText.AppendLine("  , GETDATE()")
            _sqlText.AppendLine("  , @IVC_USERID")
            _sqlText.AppendLine("  , @IVC_PGID")
            _sqlText.AppendLine("  , GETDATE()")
            _sqlText.AppendLine("  , @IVC_USERID")
            _sqlText.AppendLine("  , @IVC_PGID")
            _sqlText.AppendLine("  , @IVC_LISTCD")
            _sqlText.AppendLine("  , @IVC_SUJICD")
            _sqlText.AppendLine("  , @IVC_RYAKUSYOZENKAKU")
            _sqlText.AppendLine("  , @IVC_RYAKUSYOHANKAKU")
            _sqlText.AppendLine(")")
            ' '' TODO：↑↑クエリを編集してください。
            Dim _returnValue = Me.ExecuteNonQuery(_sqlText.ToString(), _param)

            Return _returnValue

        End Function
        'TODO：↑↑詳細画面の登録内容を修正してください。

        'TODO：↓↓詳細画面の更新内容を修正してください。
        ''' <summary>
        ''' 詳細画面 詳細データ更新処理
        ''' </summary>
        ''' <param name="detailModel">詳細画面モデル</param>
        ''' <returns>処理件数</returns>
        Public Function UpdateDetail(detailModel As Model.ZZ1031DetailModel) As Integer

            ' 引数を作成
            '' TODO：↓↓引数を編集してください。
            Dim _param As New Dapper.DynamicParameters()
            _param.Add("IVC_ESTIMATENO", detailModel.EstimateNo)
            '_param.Add("IVC_LISTCD", detailModel.ListCd)
            ' _param.Add("IVC_SUJICD", detailModel.SujiCd)
            '_param.Add("IVC_RYAKUSYOZENKAKU", detailModel.RyakusyoZenkaku)
            '_param.Add("IVC_RYAKUSYOHANKAKU", detailModel.RyakusyoHankaku)
            _param.Add("IVC_USERID", detailModel.LoginUserId)
            _param.Add("IVC_PGID", detailModel.CurrentMenuId)
            '' TODO：↑↑引数を編集してください。

            '' ストアドプロシージャ実行の場合
            '_param.Add("RETURN", direction:=ParameterDirection.ReturnValue)
            ' '' TODO：↓↓ストアド名を編集してください。
            'Dim _result = Me.ExecuteStoredProcedure(Of Object)("ZZ0000Detail_P_UPD_ZZSAMPLE", _param).ToList
            ' '' TODO：↑↑ストアド名を編集してください。
            'Dim _returnValue = _param.Get(Of Integer)("RETURN")


            '' クエリ実行の場合
            '' TODO：↓↓クエリを編集してください。
            Dim _sqlText As New StringBuilder()
            _sqlText.AppendLine("UPDATE")
            _sqlText.AppendLine("    ZZSAMPLE")
            _sqlText.AppendLine("SET")
            _sqlText.AppendLine("    UPDDATE = GETDATE()")
            _sqlText.AppendLine("  , UPDCD = @IVC_USERID")
            _sqlText.AppendLine("  , UPDPGID = @IVC_PGID")
            _sqlText.AppendLine("  , REVISION = CASE WHEN REVISION = 99999 THEN 1 ELSE REVISION + 1 END")
            _sqlText.AppendLine("  , LISTCD = @IVC_LISTCD")
            _sqlText.AppendLine("  , SUJICD = @IVC_SUJICD")
            _sqlText.AppendLine("  , RYAKUSYOZENKAKU = @IVC_RYAKUSYOZENKAKU")
            _sqlText.AppendLine("  , RYAKUSYOHANKAKU = @IVC_RYAKUSYOHANKAKU")
            _sqlText.AppendLine("WHERE")
            _sqlText.AppendLine("    KEYCD = @IVC_ESTIMATENO")
            ' '' TODO：↑↑クエリを編集してください。
            Dim _returnValue = Me.ExecuteNonQuery(_sqlText.ToString(), _param)

            Return _returnValue

        End Function
        'TODO：↑↑詳細画面の更新内容を修正してください。

        'TODO：↓↓詳細画面の削除内容を修正してください。
        ''' <summary>
        ''' 詳細画面 詳細データ削除処理
        ''' </summary>
        ''' <param name="detailModel">詳細画面モデル</param>
        ''' <returns>処理件数</returns>
        Public Function DeleteDetail(detailModel As Model.ZZ1031DetailModel) As Integer

            ' 引数を作成
            '' TODO：↓↓引数を編集してください。
            Dim _param As New Dapper.DynamicParameters()
            _param.Add("IVC_ESTIMATENO", detailModel.EstimateNo)
            '' TODO：↑↑引数を編集してください。

            '' ストアドプロシージャ実行の場合
            '_param.Add("RETURN", direction:=ParameterDirection.ReturnValue)
            ' '' TODO：↓↓ストアド名を編集してください。
            'Dim _result = Me.ExecuteStoredProcedure(Of Object)("ZZ0000Detail_P_DEL_ZZSAMPLE", _param).ToList
            ' '' TODO：↑↑ストアド名を編集してください。
            'Dim _returnValue = _param.Get(Of Integer)("RETURN")


            '' クエリ実行の場合
            '' TODO：↓↓クエリを編集してください。
            Dim _sqlText As New StringBuilder()
            _sqlText.AppendLine("DELETE")
            _sqlText.AppendLine("    ZZSAMPLE")
            _sqlText.AppendLine("WHERE")
            _sqlText.AppendLine("    KEYCD = @IVC_ESTIMATENO")
            ' '' TODO：↑↑クエリを編集してください。
            Dim _returnValue = Me.ExecuteNonQuery(_sqlText.ToString(), _param)

            Return _returnValue

        End Function

        'TODO：↑↑詳細画面の削除内容を修正してください。


        Public Function GetDataSelectListForSetItem() As IList(Of SRSSelectListItem)
            Return ExcelUtility.MapTo(Of SRSSelectListItem)("Data\ZZ\ZZ1031\ListSetData.xlsx")
        End Function


        Public Function GetDataSelectListForFactoryItem() As IList(Of SRSSelectListItem)
            Return ExcelUtility.MapTo(Of SRSSelectListItem)("Data\ZZ\ZZ1031\ListFactoryData.xlsx")
        End Function





    End Class

End Namespace

