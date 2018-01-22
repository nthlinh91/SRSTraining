Imports System.Text
Imports System.Text.RegularExpressions
Imports SRS.Standards.MvcFramework.Core.Business
Imports SRS.Standards.MvcFramework.Core.DataAccess
Imports SRS.Standards.MvcFramework.Core.Facade
Imports SRS.Standards.MvcFramework.Core.Information
Imports SRS.Standards.MvcFramework.Core.Logging
Imports SRS.Standards.MvcFramework.Core.Utilities
Imports SRS.Standards.MvcFramework.Core.Utilities.Extensions
Imports SRS.Standards.MvcFramework.Core.Message
Imports SRS.Standards.MvcFramework.Core.MVC
Imports SRS.Standards.MvcFramework.Core.Security

Namespace Login.Business

    ''' <userName>SRS</userName>
    ''' <sysName>トレニンーグ</sysName>
    ''' <subSysName>共通ライブラリ</subSysName>
    ''' <summary>
    ''' ログイン Ruleクラス
    ''' </summary>
    ''' <author>SRSTaro</author>
    ''' <Version>
    ''' 001, 2018-01-09, 新規作成
    ''' </Version>
    Public Class LoginRule
        Inherits RuleBase

        ''' <summary>
        ''' 認証を実行し、ユーザ情報を取得します。
        ''' </summary>
        ''' <param name="authModel">認証画面モデル。</param>
        ''' <returns>認証成功したユーザ情報モデル。</returns>
        Public Function GetAuthentication(authModel As AuthenticationModel) As Model.LoginModel

            Dim _da As New DataAccess.LoginDataAccess()
            Dim _models = _da.GetAuthentication(authModel)

            If _models Is Nothing OrElse _models.Count = 0 Then
                Return Nothing
            End If

            Return _models.First

        End Function

        ''' <summary>
        ''' ユーザ基本情報を認証画面モデルに設定します。
        ''' </summary>
        ''' <param name="authModel">認証画面モデル</param>
        ''' <param name="userModel">ユーザ情報モデル</param>
        Public Sub SetUserInfo(authModel As AuthenticationModel, userModel As Model.LoginModel)

            authModel.UserName = userModel.UserName
            authModel.BusinessIdentifyId = userModel.BusinessIdentifyId
            authModel.Organization = String.Empty

            InformationManager.UserInfo.UserAttribute.Clear()

            ' 言語情報を設定
            InformationManager.BusinessInfo.CultureInfoName = userModel.LoginCulture.Name

            '' TODO 任意の情報を設定
            authModel.UserAttribute.Add("YmdFormat", userModel.YmdFormat)
            authModel.UserAttribute.Add("DecimalPoint", userModel.DecimalPoint)
            authModel.UserAttribute.Add("NumberDelimiter", userModel.NumberDelimiter)

        End Sub

#Region "パスワード失敗回数記憶関連"

        ''' <summary>
        ''' ログイン失敗回数を増加
        ''' </summary>
        Public Sub IncrementLoginFailedCount()

            ' ログイン失敗回数を増加
            'InformationManager.UserInfo.UserAttribute.AddOrUpdate("LoginFailure", "1",
            '                                                      Function(k, v) (Integer.Parse(v) + 1).ToString())

            Dim _failedCountValue = System.Web.HttpContext.Current.Session.Item(Constant.LoginConstant.LoginFailureKey)
            Dim _failedCount As Integer
            If _failedCountValue Is Nothing Then
                _failedCount = 0
            Else
                _failedCount = DirectCast(_failedCountValue, Integer)
            End If

            System.Web.HttpContext.Current.Session.Item(Constant.LoginConstant.LoginFailureKey) = _failedCount + 1

        End Sub

        ''' <summary>
        ''' ログイン失敗回数をクリア
        ''' </summary>
        Public Sub ClearLoginFailedCount()

            ' ログイン失敗回数をクリア
            'InformationManager.UserInfo.UserAttribute.AddOrUpdate("LoginFailure", "0",
            '                                                      Function(k, v) "0")

            System.Web.HttpContext.Current.Session.Item(Constant.LoginConstant.LoginFailureKey) = 0

        End Sub

        ''' <summary>
        ''' ログイン失敗回数をチェックし、3回以上になっていればエラーを設定する。
        ''' </summary>
        ''' <param name="model">認証画面モデル</param>
        ''' <returns>ログイン失敗回数が3回未満ならば <c>True</c></returns>
        Public Function CheckLoginFailedLockDown(model As AuthenticationModel) As Boolean

            Dim _failedCountValue = System.Web.HttpContext.Current.Session.Item(Constant.LoginConstant.LoginFailureKey)
            Dim _failedCount As Integer
            If _failedCountValue Is Nothing Then
                _failedCount = 0
            Else
                _failedCount = DirectCast(_failedCountValue, Integer)
            End If

            If _failedCount > Constant.LoginConstant.LoginFailureMax Then
                ' 3回連続でログインに失敗したため、画面をロックします。
                Dim _loginModel As New Model.LoginModel
                model.SetValidateMember(ValidLevelType.Fatal,
                                        Nothing,
                                        MessageManager.GetMessage(_loginModel.LoginCulture, "CMN_E00054"))
                Return False
            End If

            Return True

        End Function

#End Region

#Region "業務規制"

        ''' <summary>
        ''' 業務規制チェック(メイン処理)
        ''' </summary>
        ''' <param name="regulationModel">業務権限・業務規制管理モデル</param>
        ''' <returns>業務規制チェック結果(チェックＯＫ時:True)</returns>
        ''' <remarks>業務側で必要に応じて上書きを行う</remarks>
        Public Function OnRegulationCheck(ByVal regulationModel As SecurityRegulationModel) As Boolean

            Dim _regulationCheckSuccess As Boolean = True

            '' SQL実行
            'Dim _da As New DataAccess.LoginDataAccess
            ''Dim _model = _da.GetRegulation()
            ' 取得件数分ループ
            'For Each gyomuKisei As Model.GyomuKiseiModel In _model
            'Dim _startDate As New DateTime(Int32.Parse(gyomuKisei.STARTYMD.Substring(0, 4)),
            'Int32.Parse(gyomuKisei.STARTYMD.Substring(4, 2)),
            'Int32.Parse(gyomuKisei.STARTYMD.Substring(6, 2)),
            'Int32.Parse(gyomuKisei.STARTHHMM.Substring(0, 2)),
            'Int32.Parse(gyomuKisei.STARTHHMM.Substring(2, 2)),
            '0)
            'Dim _endtDate As New DateTime(Int32.Parse(gyomuKisei.ENDYMD.Substring(0, 4)),
            'Int32.Parse(gyomuKisei.ENDYMD.Substring(4, 2)),
            'Int32.Parse(gyomuKisei.ENDYMD.Substring(6, 2)),
            'Int32.Parse(gyomuKisei.ENDHHMM.Substring(0, 2)),
            'Int32.Parse(gyomuKisei.ENDHHMM.Substring(2, 2)),
            '59)
            'If _startDate <= DateTime.Now AndAlso _endtDate >= DateTime.Now Then
            '' 業務規制中の場合
            '_regulationCheckSuccess = False
            'Exit For
            'End If
            'Next

            Return _regulationCheckSuccess

        End Function

#End Region

    End Class

End Namespace

