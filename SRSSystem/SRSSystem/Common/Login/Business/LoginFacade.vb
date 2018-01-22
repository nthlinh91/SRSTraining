Imports System.Text
Imports System.Reflection
Imports SRS.Standards.MvcFramework.Core
Imports SRS.Standards.MvcFramework.Core.Configuration
Imports SRS.Standards.MvcFramework.Core.Facade
Imports SRS.Standards.MvcFramework.Core.Business
Imports SRS.Standards.MvcFramework.Core.Logging
Imports SRS.Standards.MvcFramework.Core.Message
Imports SRS.Standards.MvcFramework.Core.Utilities
Imports SRS.Standards.MvcFramework.Core.Information
Imports SRS.Standards.MvcFramework.Core.MVC
Imports SRS.Standards.MvcFramework.Core.Security

Imports SRSSystem.Common.Utilities

Namespace Login.Business

    ''' <userName>SRS</userName>
    ''' <sysName>トレニンーグ</sysName>
    ''' <subSysName>共通ライブラリ</subSysName>
    ''' <summary>
    ''' ログイン用業務Facadeクラス
    ''' </summary>
    ''' <author>SRSTaro</author>
    ''' <Version>
    ''' 001, 2018-01-09, 新規作成
    ''' </Version>
    ''' <remarks>必要に応じて各処理を上書きしてください</remarks>
    Public Class LoginFacade
        Inherits FacadeBase

        ''' <summary>
        ''' ログイン処理(メイン処理)
        ''' </summary>
        ''' <param name="authModel">認証管理モデル</param>
        ''' <returns>ログイン判定(ログインOKであればTrueを返す)</returns>
        ''' <remarks>業務側で必要に応じて上書きを行う</remarks>
        Protected Overrides Function OnLogin(authModel As AuthenticationModel) As Boolean
            LogManager.SysTraceLog.Log(Me.GetType().FullName, MethodBase.GetCurrentMethod().ToString(), MethodTraceType.Begin)

            Dim _loginSuccess As Boolean = False
            Dim _rule As New Business.LoginRule()

            ' ログイン失敗回数をチェック
            If Not _rule.CheckLoginFailedLockDown(authModel) Then
                ' 失敗回数が上限を越えていれば、無条件で失敗
                Return False
            End If

            ' 認証を実行し、ユーザ情報を取得
            Dim _model = _rule.GetAuthentication(authModel)

            If _model IsNot Nothing Then
                '' ハッシュ値を比較 (既定ではUTF16エンコードのSHA256)
                If authModel.Password.GetHashString = _model.Password Then
                    ' パスワードが一致した場合
                    _loginSuccess = True

                    ' ログイン失敗回数をクリア
                    _rule.ClearLoginFailedCount()

                    ' ユーザ基本情報を設定
                    _rule.SetUserInfo(authModel, _model)

                    ' メニューにリダイレクトする場合は、ReturnUrlを消す
                    authModel.ReturnUrl = ""

                End If
            End If
            If Not _loginSuccess Then

                ' ログイン失敗回数のカウント
                _rule.IncrementLoginFailedCount()

                Dim _loginModel As New Model.LoginModel
                ' ログインＮＧ
                authModel.SetValidateMember(ValidLevelType.Error,
                                            authModel.GetPropertyFullName(Function(m) m.UserId),
                                            MessageManager.GetMessage(_loginModel.LoginCulture,
                                                                      ExtensionConfigurationManager.Config.MessageLoginError))
                authModel.SetValidateMember(ValidLevelType.Error,
                                            authModel.GetPropertyFullName(Function(m) m.Password),
                                            MessageManager.GetMessage(_loginModel.LoginCulture,
                                                                      ExtensionConfigurationManager.Config.MessageLoginError))
            End If

            LogManager.SysTraceLog.Log(Me.GetType().FullName, MethodBase.GetCurrentMethod().ToString(), MethodTraceType.End)
            Return _loginSuccess
        End Function

#Region "業務規制"

        ''' <summary>
        ''' 業務規制チェック
        ''' </summary>
        ''' <param name="regulationModel">業務権限・業務規制管理モデル</param>
        ''' <returns>業務権限・業務規制管理モデル</returns>
        <TransactionScope(Enabled:=False)>
        Public Function RegulationCheck(regulationModel As SecurityRegulationModel) As SecurityRegulationModel
            LogManager.SysTraceLog.Log(Me.GetType().FullName, MethodBase.GetCurrentMethod().ToString(), MethodTraceType.Begin)

            Dim _rule As New LoginRule
            ' 業務規制チェック実施
            Dim _regulationFlg As Boolean = _rule.OnRegulationCheck(regulationModel)
            If _regulationFlg Then
                ' 業務規制チェックOK時
                regulationModel.ValidLevel = ValidLevelType.Normal
                regulationModel.SecurityCheck = BusinessSecurityType.Authority
            Else
                ' 業務規制チェックNG時
                ' セキュリティログ出力
                LogManager.SecurityLog.Log(InformationManager.AppInfo.MenuId, BusinessSecurityType.Regulation)
                regulationModel.ValidLevel = ValidLevelType.Error
                regulationModel.SecurityCheck = BusinessSecurityType.Regulation
            End If

            LogManager.SysTraceLog.Log(Me.GetType().FullName, MethodBase.GetCurrentMethod().ToString(), MethodTraceType.End)
            Return regulationModel
        End Function

#End Region

#Region "権限取得"

        ''' <summary>
        ''' 権限を取得
        ''' </summary>
        ''' <returns>業務権限・業務規制管理モデル</returns>
        <TransactionScope(Enabled:=False)>
        Public Function GetPermission() As SecurityRegulationModel

            Dim _da As New Login.DataAccess.LoginDataAccess()
            Dim _result = New SecurityRegulationModel
            'Dim _data = _da.GetPermission()

            'If _data IsNot Nothing AndAlso _data.Count > 0 Then
            'Dim _item = _data.Single
            '_result.CanInsert = (_item.CanInsertFlag = "1")
            '_result.CanUpdate = (_item.CanUpdateFlag = "1")
            '_result.CanDelete = (_item.CanDeleteFlag = "1")
            '_result.CanSearch = (_item.CanSearchFlag = "1")
            'End If

            Return _result

        End Function

#End Region

    End Class

End Namespace

