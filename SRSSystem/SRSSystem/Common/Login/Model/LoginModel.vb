Imports SRS.Standards.MvcFramework.Core.Security

Namespace Login.Model

    ''' <userName>SRS</userName>
    ''' <sysName>トレーニング</sysName>
    ''' <subSysName>共通ライブラリ</subSysName>
    ''' <summary>
    ''' ログインID情報モデル
    ''' </summary>
    ''' <author>SRSTaro</author>
    ''' <Version>
    ''' 001, 2018-01-11, 新規作成
    ''' </Version>
    Public Class LoginModel
        Inherits AuthenticationModel

        ''' <summary>
        ''' 表示言語。
        ''' </summary>
        Public Property Locale As String

        ''' <summary>
        ''' 日付フォーマット。
        ''' </summary>
        Public Property YmdFormat As String

        ''' <summary>
        ''' 小数点記号。
        ''' </summary>
        Public Property DecimalPoint As String

        ''' <summary>
        ''' 数値の桁区切り記号。
        ''' </summary>
        Public Property NumberDelimiter As String

        ''' <summary>ログイン画面の表示言語(内部値)</summary>
        Private _loginCulture As System.Globalization.CultureInfo

        ''' <summary>ログイン画面の表示言語</summary>
        Public ReadOnly Property LoginCulture As System.Globalization.CultureInfo
            Get
                If Me._loginCulture Is Nothing Then
                    Dim _cookies = System.Web.HttpContext.Current.Request.Cookies
                    Dim _localeValue As String = Nothing

                    For Each _cookie As String In _cookies.Keys
                        If _cookie = "Login_Culture" Then
                            _localeValue = _cookies.Item(_cookie).Value
                        End If
                    Next
                    If _localeValue Is Nothing Then
                        _localeValue = "en-US"
                    End If
                    Me._loginCulture = New System.Globalization.CultureInfo(_localeValue)
                End If

                Return Me._loginCulture
            End Get
        End Property

    End Class

End Namespace
