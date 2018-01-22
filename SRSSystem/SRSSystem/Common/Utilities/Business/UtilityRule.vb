Imports System.ComponentModel
Imports System.Text
Imports System.Text.RegularExpressions
Imports SRS.Standards.MvcFramework.Core.Business
Imports SRS.Standards.MvcFramework.Core.Configuration
Imports SRS.Standards.MvcFramework.Core.DataAccess
Imports SRS.Standards.MvcFramework.Core.Facade
Imports SRS.Standards.MvcFramework.Core.Information
Imports SRS.Standards.MvcFramework.Core.Logging
Imports SRS.Standards.MvcFramework.Core.Utilities
Imports SRS.Standards.MvcFramework.Core.Utilities.Extensions
Imports SRS.Standards.MvcFramework.Core.Message
Imports SRS.Standards.MvcFramework.Core.MVC
Imports SRS.Standards.MvcFramework.Core.IO

Imports SRSSystem.Common.Utilities

Namespace Utilities.Business

    ''' <userName>SRS</userName>
    ''' <sysName>トレーニング</sysName>
    ''' <subSysName>共通ライブラリ</subSysName>
    ''' <summary>
    ''' ユーティリティ Ruleクラス
    ''' </summary>
    ''' <author>SRSTaro</author>
    ''' <Version>
    ''' 001, 2018-01-11, 新規作成
    ''' </Version>
    Public Class UtilityRule
        Inherits RuleBase

        ''' <summary>
        ''' データセレクトを取得します。
        ''' </summary>
        ''' <param name="viewModel">画面モデル</param>
        ''' <returns>データセレクト項目モデル</returns>
        Public Function GetDataSelect(viewModel As Model.DataSelectModel) As IEnumerable(Of Model.DataSelectItemModel)

            Dim _da As New DataAccessBase

            Dim _params As Dapper.DynamicParameters = Nothing

            '' パラメータを扱う
            If viewModel IsNot Nothing AndAlso viewModel.Parameters IsNot Nothing AndAlso viewModel.Parameters.Count > 0 Then
                _params = New Dapper.DynamicParameters
                For Each _item In viewModel.Parameters
                    If _item Is Nothing OrElse _item.Length < 2 Then
                        Continue For
                    End If
                    _params.Add(_item(0), _item(1))
                Next
            End If

            Return _da.GetDataSelect(Of Model.DataSelectItemModel)(viewModel.DataSelectName, _params)

        End Function

    End Class

End Namespace

