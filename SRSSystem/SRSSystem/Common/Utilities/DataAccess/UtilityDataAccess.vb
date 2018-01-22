Imports System.Text
Imports SRS.Standards.MvcFramework.Core.Business
Imports SRS.Standards.MvcFramework.Core.DataAccess
Imports SRS.Standards.MvcFramework.Core.MVC
Imports SRS.Standards.MvcFramework.Core.Utilities
Imports SRS.Standards.MvcFramework.Core.Utilities.Extensions
Imports SRS.Standards.MvcFramework.Core.Information

Namespace Utilities.DataAccess

    ''' <userName>SRS</userName>
    ''' <sysName>トレーニング</sysName>
    ''' <subSysName>共通ライブラリ</subSysName>
    ''' <summary>
    ''' ユーティリティ データアクセスクラス
    ''' </summary>
    ''' <author>SRSTaro</author>
    ''' <Version>
    ''' 001, 2018-01-11, 新規作成
    ''' </Version>
    Public Class UtilityDataAccess
        Inherits DataAccessBase

        ''' <summary>
        ''' 機能IDに対するアクセス回数を1増やします。
        ''' </summary>
        ''' <param name="menuId">機能ID。</param>
        ''' <returns>更新データ数。</returns>
        Public Function CountUpActivity(menuId As String) As Integer

            ' 引数を作成
            Dim _param As New Dapper.DynamicParameters()
            _param.Add("IVC_LOGINID", InformationManager.UserInfo.UserId)
            _param.Add("IVC_MENUID", menuId)

            _param.Add("RETURN", direction:=ParameterDirection.ReturnValue, dbType:=DbType.Int32)

            Me.ExecuteStoredProcedure("CP_UPD_INCACTIVITYLOG", _param)

            Dim _returnValue = _param.Get(Of Integer)("RETURN")

            Return _returnValue

        End Function

    End Class

End Namespace

