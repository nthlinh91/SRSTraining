Imports System.Text
Imports System.Web.Mvc
Imports SRS.Standards.MvcFramework.Core.Business
Imports SRS.Standards.MvcFramework.Core.DataAccess
Imports SRS.Standards.MvcFramework.Core.MVC
Imports SRS.Standards.MvcFramework.Core.Utilities
Imports SRS.Standards.MvcFramework.Core.Utilities.Extensions

Imports SRSSystem.Common.Utilities
Namespace DataAccess

    ''' <userName>住友ゴム工業株式会社</userName>
    ''' <sysName>タイヤ海外業務基幹システム（JUST）</sysName>
    ''' <subSysName>共通</subSysName>
    ''' <summary>
    ''' 最終仕向地検索 データアクセスクラス
    ''' </summary>
    ''' <author>sfunaoka/SDS</author>
    ''' <Version>
    ''' 001, 2015-09-15, 新規作成
    ''' </Version>
    Public Class EC1040DataAccess
        Inherits DataAccessBase

        ''' <summary>
        ''' 一覧画面 キー検索
        ''' </summary>
        ''' <param name="listModel">一覧画面モデル</param>
        ''' <returns>詳細項目リスト</returns>
        Public Function SearchListKey(ByVal listModel As Model.EC1040ListModel) As Model.EC1040ListModel
            ' 引数を作成
            Dim _param As New Dapper.DynamicParameters()
            _param.Add("IVC_SHIMUKECHICD", listModel.ShimukechiCdCondition)
            _param.Add("IVC_SAISHUSHIMUKECHINM", listModel.SaichushimukechiNmCondition)
            _param.Add("IVC_GYMSEIGYOKBN", listModel.GymSeigyoKbn)
            _param.Add("IVC_LOGINDAIKBN", listModel.LoginDaiKbn)

            _param.Add("RETURN", direction:=ParameterDirection.ReturnValue)

            ' ストアドプロシージャ実行
            'Me.ExecuteStoredProcedureForPagingKey("EC1040List_P_GET_SAISHUSHIMUKECHISEARCHKEY", _param)

            'Dim _returnValue = _param.Get(Of Integer)("RETURN")
            listModel = ExcelUtility.MapTo(Of Model.EC1040ListModel)("Data\EC\EC1040\EC1040ConditionData.xlsx").FirstOrDefault()


            Return listModel


        End Function

        ''' <summary>
        ''' 一覧画面 検索
        ''' </summary>
        ''' <param name="listModel">一覧画面モデル</param>
        ''' <returns>詳細項目リスト</returns>
        Public Function SearchList(ByVal listModel As Model.EC1040ListModel) As IList(Of Model.EC1040ListElementModel)

            Dim _param As New Dapper.DynamicParameters()

            ' ストアドプロシージャ実行
            'Dim _result = Me.ExecuteStoredProcedureAndPaging(Of Model.EC1040ListElementModel)("EC1040List_P_GET_SAISHUSHIMUKECHISEARCHDETAIL", listModel, _param).ToList

            'Return _result
            Return ExcelUtility.MapTo(Of Model.EC1040ListElementModel)("Data\EC\EC1040\EC1040Data.xlsx")

        End Function

    End Class

End Namespace

