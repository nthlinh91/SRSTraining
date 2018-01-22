Imports System.Globalization
Imports System.Web
Imports System.ComponentModel
Imports System.Linq.Expressions
Imports System.Runtime.CompilerServices
Imports System.Text

Imports SRS.Standards.MvcFramework.Core.Business
Imports SRS.Standards.MvcFramework.Core.Configuration
Imports SRS.Standards.MvcFramework.Core.Message
Imports SRS.Standards.MvcFramework.Core.Utilities
Imports SRS.Standards.MvcFramework.Core.MVC
Imports SRS.Standards.MvcFramework.Core.Constant
Imports SRS.Standards.MvcFramework.Core.Facade
Imports SRS.Standards.MvcFramework.Core.Information

Namespace Utilities

    ''' <userName>SRS</userName>
    ''' <sysName>トレーニング</sysName>
    ''' <subSysName>共通ライブラリ</subSysName>
    ''' <summary>
    ''' 各型の Extension を定義します。
    ''' </summary>
    ''' <author>SRSTaro</author>
    ''' <Version>
    ''' 001, 2018-01-11, 新規作成
    ''' </Version>
    Public Module Extensions

#Region "Model"

        ''' <summary>
        ''' モデルをモデル定義に設定した属性に従っているかどうかを検査します。
        ''' </summary>
        ''' <param name="model">検査対象モデル。</param>
        ''' <returns>問題がないとき <c>True</c>。</returns>
        <System.Runtime.CompilerServices.Extension()>
        Public Function Validate(model As IViewModel) As Boolean

            Return ValidationUtility.ValidateModel(model, model, "")

        End Function

        ''' <summary>
        ''' 対象モデルのメンバを文字列要素として検査します。
        ''' </summary>
        ''' <typeparam name="T">対象モデルの型。</typeparam>
        ''' <typeparam name="TValue">対象メンバの型。</typeparam>
        ''' <param name="model">検査対象モデル。</param>
        ''' <param name="memberFunc">対象メンバを取得するための関数表現。</param>
        ''' <param name="required">必須項目かどうか。</param>
        ''' <param name="type">文字列の種類。</param>
        ''' <param name="minLength">最小桁数。指定しない場合は 0。</param>
        ''' <param name="maxLength">最大桁数。指定しない場合は 0。</param>
        ''' <returns>問題がないとき <c>True</c>。</returns>
        <System.Runtime.CompilerServices.Extension()>
        Public Function ValidateString(Of T As IViewModel, TValue)(model As T,
                                                                   memberFunc As System.Linq.Expressions.Expression(Of Func(Of T, TValue)),
                                                                   required As Boolean, type As CharacterType, minLength As Integer, maxLength As Integer) As Boolean

            ' 値を取得
            Dim _func = memberFunc.Compile

            Dim _value = DBNullToEmptyString(_func.Invoke(model))
            Dim _displayName = model.GetDisplayName(memberFunc)
            Dim _propertyName = model.GetPropertyFullName(memberFunc)
            Dim _attr As New SRSStringValidationAttribute(required, type, minLength, maxLength)

            If ValidationUtility.ValidateRequired(model, _attr, _value, _displayName, _propertyName) Then
                If _value.IsNullOrWhiteSpace Then
                    ' 必須がFalseで値が空の場合は常に成功
                    Return True
                End If
                Return ValidationUtility.ValidateString(model, _attr, _value, _displayName, _propertyName)
            End If

            Return False

        End Function

        ''' <summary>
        ''' 対象モデルのメンバを数値要素として検査します。
        ''' </summary>
        ''' <typeparam name="T">対象モデルの型。</typeparam>
        ''' <typeparam name="TValue">対象メンバの型。</typeparam>
        ''' <param name="model">検査対象モデル。</param>
        ''' <param name="memberFunc">対象メンバを取得するための関数表現。</param>
        ''' <param name="required">必須項目かどうか。</param>
        ''' <param name="minValue">最小値。</param>
        ''' <param name="maxValue">最大値。</param>
        ''' <param name="decimalLength">最大小数桁。</param>
        ''' <returns>問題がないとき <c>True</c>。</returns>
        <System.Runtime.CompilerServices.Extension()>
        Public Function ValidateNumber(Of T As IViewModel, TValue)(model As T, memberFunc As System.Linq.Expressions.Expression(Of Func(Of T, TValue)),
                                                                   required As Boolean, minValue As Double, maxValue As Double, decimalLength As Integer) As Boolean

            ' 値を取得
            Dim _func = memberFunc.Compile

            Dim _value = DBNullToEmptyString(_func.Invoke(model))
            Dim _displayName = model.GetDisplayName(memberFunc)
            Dim _propertyName = model.GetPropertyFullName(memberFunc)
            Dim _attr As New SRSNumberValidationAttribute(required, minValue, maxValue, decimalLength)

            If ValidationUtility.ValidateRequired(model, _attr, _value, _displayName, _propertyName) Then
                If _value.IsNullOrWhiteSpace Then
                    ' 必須がFalseで値が空の場合は常に成功
                    Return True
                End If
                Return ValidationUtility.ValidateNumber(model, _attr, _value, _displayName, _propertyName)
            End If

            Return False

        End Function

        ''' <summary>
        ''' 対象モデルのメンバを年月日要素として検査します。
        ''' </summary>
        ''' <typeparam name="T">対象モデルの型。</typeparam>
        ''' <typeparam name="TValue">対象メンバの型。</typeparam>
        ''' <param name="model">検査対象モデル。</param>
        ''' <param name="memberFunc">対象メンバを取得するための関数表現。</param>
        ''' <param name="required">必須項目かどうか。</param>
        ''' <returns>問題がないとき <c>True</c>。</returns>
        <System.Runtime.CompilerServices.Extension()>
        Public Function ValidateYMD(Of T As IViewModel, TValue)(model As T,
                                                                memberFunc As System.Linq.Expressions.Expression(Of Func(Of T, TValue)),
                                                                required As Boolean) As Boolean

            ' 値を取得
            Dim _func = memberFunc.Compile

            Dim _value = DBNullToEmptyString(_func.Invoke(model))
            Dim _displayName = model.GetDisplayName(memberFunc)
            Dim _propertyName = model.GetPropertyFullName(memberFunc)
            Dim _attr As New SRSYMDValidationAttribute(required)

            If ValidationUtility.ValidateRequired(model, _attr, _value, _displayName, _propertyName) Then
                If _value.IsNullOrWhiteSpace Then
                    ' 必須がFalseで値が空の場合は常に成功
                    Return True
                End If
                Return ValidationUtility.ValidateYMD(model, _attr, _value, _displayName, _propertyName)
            End If

            Return False

        End Function

        ''' <summary>
        ''' 対象モデルのメンバを年月要素として検査します。
        ''' </summary>
        ''' <typeparam name="T">対象モデルの型。</typeparam>
        ''' <typeparam name="TValue">対象メンバの型。</typeparam>
        ''' <param name="model">検査対象モデル。</param>
        ''' <param name="memberFunc">対象メンバを取得するための関数表現。</param>
        ''' <param name="required">必須項目かどうか。</param>
        ''' <returns>問題がないとき <c>True</c>。</returns>
        <System.Runtime.CompilerServices.Extension()>
        Public Function ValidateYM(Of T As IViewModel, TValue)(model As T,
                                                               memberFunc As System.Linq.Expressions.Expression(Of Func(Of T, TValue)),
                                                               required As Boolean) As Boolean

            ' 値を取得
            Dim _func = memberFunc.Compile

            Dim _value = DBNullToEmptyString(_func.Invoke(model))
            Dim _displayName = model.GetDisplayName(memberFunc)
            Dim _propertyName = model.GetPropertyFullName(memberFunc)
            Dim _attr As New SRSYMValidationAttribute(required)

            If ValidationUtility.ValidateRequired(model, _attr, _value, _displayName, _propertyName) Then
                If _value.IsNullOrWhiteSpace Then
                    ' 必須がFalseで値が空の場合は常に成功
                    Return True
                End If
                Return ValidationUtility.ValidateYM(model, _attr, _value, _displayName, _propertyName)
            End If

            Return False

        End Function

        ''' <summary>
        ''' 対象モデルのメンバを年月要素として検査します。
        ''' </summary>
        ''' <typeparam name="T">対象モデルの型。</typeparam>
        ''' <typeparam name="TValue">対象メンバの型。</typeparam>
        ''' <param name="model">検査対象モデル。</param>
        ''' <param name="memberFunc">対象メンバを取得するための関数表現。</param>
        ''' <param name="required">必須項目かどうか。</param>
        ''' <returns>問題がないとき <c>True</c>。</returns>
        <System.Runtime.CompilerServices.Extension()>
        Public Function ValidateHMS(Of T As IViewModel, TValue)(model As T,
                                                                memberFunc As System.Linq.Expressions.Expression(Of Func(Of T, TValue)),
                                                                required As Boolean) As Boolean

            ' 値を取得
            Dim _func = memberFunc.Compile

            Dim _value = DBNullToEmptyString(_func.Invoke(model))
            Dim _displayName = model.GetDisplayName(memberFunc)
            Dim _propertyName = model.GetPropertyFullName(memberFunc)
            Dim _attr As New SRSHMSValidationAttribute(required)

            If ValidationUtility.ValidateRequired(model, _attr, _value, _displayName, _propertyName) Then
                If _value.IsNullOrWhiteSpace Then
                    ' 必須がFalseで値が空の場合は常に成功
                    Return True
                End If
                Return ValidationUtility.ValidateHMS(model, _attr, _value, _displayName, _propertyName)
            End If

            Return False

        End Function

        ''' <summary>
        ''' モデルにメッセージを設定します。
        ''' メッセージIDの5文字目を見てエラーレベルを判定します。(N: 通常、W: 警告、E: エラー、F: 致命的)
        ''' </summary>
        ''' <param name="model">モデル</param>
        ''' <param name="propertyName">対象のプロパティ名(階層込で設定する)</param>
        ''' <param name="messageId">メッセージID</param>
        ''' <param name="messageParams">メッセージ置換文字列(メッセージIDも可)</param>
        <Extension()>
        Public Sub SetMessage(model As IViewModel, propertyName As String, messageId As String, ParamArray messageParams As String())

            Dim _validLevel = ValidLevelType.Normal

            Select Case messageId.Substring(4, 1)
                Case "F"
                    _validLevel = ValidLevelType.Fatal
                    model.SetValidateMember(_validLevel, propertyName, messageId, messageParams)
                Case "E"
                    _validLevel = ValidLevelType.Error
                    model.SetValidateMember(_validLevel, propertyName, messageId, messageParams)
                Case "W"
                    _validLevel = ValidLevelType.Warn
                    model.SetValidateMember(_validLevel, propertyName, messageId, messageParams)
                Case "N"
                    '_validLevel = ValidLevelType.Normal
                    model.Message = MessageManager.GetMessage(messageId, messageParams)
                Case Else
                    Throw New RuleException()
            End Select

        End Sub

        ''' <summary>
        ''' モデルにエラーとして項目エラー及びメッセージを設定します。
        ''' </summary>
        ''' <param name="model">モデル</param>
        ''' <param name="propertyName">対象のプロパティ名(階層込で設定する)</param>
        ''' <param name="messageId">メッセージID</param>
        ''' <param name="messageParams">メッセージ置換文字列(メッセージIDも可)</param>
        <Extension()>
        Public Sub SetError(model As IViewModel, propertyName As String, messageId As String, ParamArray messageParams As String())
            Dim _validLevel = ValidLevelType.Error
            model.SetValidateMember(_validLevel, propertyName, messageId, messageParams)
        End Sub

        ''' <summary>
        ''' モデルに警告として項目エラー及びメッセージを設定します。
        ''' </summary>
        ''' <param name="model">モデル</param>
        ''' <param name="propertyName">対象のプロパティ名(階層込で設定する)</param>
        ''' <param name="messageId">メッセージID</param>
        ''' <param name="messageParams">メッセージ置換文字列(メッセージIDも可)</param>
        <Extension()>
        Public Sub SetWarn(model As IViewModel, propertyName As String, messageId As String, ParamArray messageParams As String())
            Dim _validLevel = ValidLevelType.Warn
            model.SetValidateMember(_validLevel, propertyName, messageId, messageParams)
        End Sub

        ''' <summary>
        ''' モデルに致命的として項目エラー及びメッセージを設定します。
        ''' </summary>
        ''' <param name="model">モデル</param>
        ''' <param name="propertyName">対象のプロパティ名(階層込で設定する)</param>
        ''' <param name="messageId">メッセージID</param>
        ''' <param name="messageParams">メッセージ置換文字列(メッセージIDも可)</param>
        <Extension()>
        Public Sub SetFatal(model As IViewModel, propertyName As String, messageId As String, ParamArray messageParams As String())
            Dim _validLevel = ValidLevelType.Fatal
            model.SetValidateMember(_validLevel, propertyName, messageId, messageParams)
        End Sub

#End Region

    End Module

End Namespace
