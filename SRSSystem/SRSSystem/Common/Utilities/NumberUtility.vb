Imports System.Globalization
Imports SRS.Standards.MvcFramework.Core.Utilities
Imports SRS.Standards.MvcFramework.Core.Information

Namespace Utilities

    ''' <userName>SRS</userName>
    ''' <sysName>トレーニング</sysName>
    ''' <subSysName>共通ライブラリ</subSysName>
    ''' <summary>
    ''' 数値に関係するユーティリティ関数を定義します。
    ''' </summary>
    ''' <author>SRSTaro</author>
    ''' <Version>
    ''' 001, 2018-01-11, 新規作成
    ''' </Version>
    Public NotInheritable Class NumberUtility

        ''' <summary>
        ''' ダミーコンストラクタ
        ''' </summary>
        Private Sub New()
        End Sub

        ''' <summary>
        ''' ユーザの言語設定から数値整形情報を作成します。
        ''' </summary>
        ''' <returns>数値整形情報。</returns>
        Private Shared Function GetNumberFormatInfo() As System.Globalization.NumberFormatInfo
            Dim _formatInfo As New System.Globalization.NumberFormatInfo()
            Try
                If InformationManager.UserInfo.UserAttribute.ContainsKey("DecimalPoint") Then
                    Dim _point = InformationManager.UserInfo.UserAttribute("DecimalPoint")
                    _formatInfo.NumberDecimalSeparator = _point
                    _formatInfo.PercentDecimalSeparator = _point
                    _formatInfo.CurrencyDecimalSeparator = _point
                End If
                If InformationManager.UserInfo.UserAttribute.ContainsKey("NumberDelimiter") Then
                    Dim _delimiter = InformationManager.UserInfo.UserAttribute("NumberDelimiter")
                    _formatInfo.NumberGroupSeparator = _delimiter
                    _formatInfo.PercentGroupSeparator = _delimiter
                    _formatInfo.CurrencyGroupSeparator = _delimiter
                End If
            Catch ex As InvalidOperationException
                System.Diagnostics.Debug.Print(ex.Message)
                'SRS.Standards.MvcFramework.Core.Logging.LogManager.BusinessLog.Error(ex.Message, ex)
            End Try
            Return _formatInfo
        End Function

#Region "ユーザ言語設定に従って数値から文字列に変換"

        ''' <summary>
        ''' 数値をユーザの言語設定に従って文字列に変換します。
        ''' </summary>
        ''' <param name="value">変換対象の数値。</param>
        ''' <returns>変換後文字列。</returns>
        Public Shared Function ToLocaleString(value As String) As String
            If String.IsNullOrWhiteSpace(value) Then
                Return Nothing
            End If
            Return Decimal.Parse(value, CultureInfo.InvariantCulture).ToString(GetNumberFormatInfo())
        End Function

        ''' <summary>
        ''' 数値をユーザの言語設定に従って文字列に変換します。
        ''' </summary>
        ''' <param name="value">変換対象の数値。</param>
        ''' <param name="format">フォーマット文字列。</param>
        ''' <returns>変換後文字列。</returns>
        Public Shared Function ToLocaleString(value As String, format As String) As String
            If String.IsNullOrWhiteSpace(value) Then
                Return Nothing
            End If
            Return Decimal.Parse(value, CultureInfo.InvariantCulture).ToString(format, GetNumberFormatInfo())
        End Function

        ''' <summary>
        ''' 数値をユーザの言語設定に従って文字列に変換します。
        ''' </summary>
        ''' <param name="value">変換対象の数値。</param>
        ''' <returns>変換後文字列。</returns>
        Public Shared Function ToLocaleString(value As Decimal) As String
            Return value.ToString(GetNumberFormatInfo())
        End Function

        ''' <summary>
        ''' 数値をユーザの言語設定に従って文字列に変換します。
        ''' </summary>
        ''' <param name="value">変換対象の数値。</param>
        ''' <param name="decimalLength">小数点以下の桁数。</param>
        ''' <param name="delimiter">3桁毎の桁区切り記号を入れる場合 <c>True</c>。</param>
        ''' <returns>変換後文字列。</returns>
        Public Shared Function ToLocaleString(value As Decimal, decimalLength As Integer, delimiter As Boolean) As String
            Dim _format = "0"
            If delimiter Then
                _format = "#," & _format
            End If
            If decimalLength > 0 Then
                _format &= "."
                For i = 1 To decimalLength
                    _format &= "0"
                Next
            End If

            Return value.ToString(_format, GetNumberFormatInfo())
        End Function

        ''' <summary>
        ''' 数値をユーザの言語設定に従って文字列に変換します。
        ''' </summary>
        ''' <param name="value">変換対象の数値。</param>
        ''' <param name="format">フォーマット文字列。</param>
        ''' <returns>変換後文字列。</returns>
        Public Shared Function ToLocaleString(value As Decimal, format As String) As String
            Return value.ToString(format, GetNumberFormatInfo())
        End Function

        ''' <summary>
        ''' 数値をユーザの言語設定に従って文字列に変換します。
        ''' </summary>
        ''' <param name="value">変換対象の数値。</param>
        ''' <returns>変換後文字列。</returns>
        Public Shared Function ToLocaleString(value As Integer) As String
            Return value.ToString(GetNumberFormatInfo())
        End Function

        ''' <summary>
        ''' 数値をユーザの言語設定に従って文字列に変換します。
        ''' </summary>
        ''' <param name="value">変換対象の数値。</param>
        ''' <param name="format">フォーマット文字列。</param>
        ''' <returns>変換後文字列。</returns>
        Public Shared Function ToLocaleString(value As Integer, format As String) As String
            Return value.ToString(format, GetNumberFormatInfo())
        End Function

#End Region

#Region "ユーザ言語設定に従って文字列から数値に変換"

        ''' <summary>
        ''' 数値文字列を数値に変換できるかどうかを判定します。
        ''' </summary>
        ''' <param name="value">変換対象の数値文字列。</param>
        ''' <returns>変換可能な場合 <c>True</c>。</returns>
        Public Shared Function CanConvertToDecimal(value As String) As Boolean
            Return Decimal.TryParse(value, NumberStyles.Number, GetNumberFormatInfo(), Nothing)
        End Function

        ''' <summary>
        ''' 数値文字列をユーザの言語設定に従って数値に変換します。
        ''' </summary>
        ''' <param name="value">変換対象の数値文字列。</param>
        ''' <returns>変換後の数値文字列。</returns>
        Public Shared Function ParseDecimal(value As String) As Decimal?
            If CanConvertToDecimal(value) Then
                Return Decimal.Parse(value, GetNumberFormatInfo())
            Else
                Return Nothing
            End If
        End Function

#End Region

#Region "文字列のまま数値操作"

        ''' <summary>
        ''' 文字列数値の切り上げ (0から遠いほうに丸める)
        ''' </summary>
        ''' <param name="value">変換対象の文字列数値。</param>
        ''' <param name="decimalLength">小数点以下の桁数。</param>
        ''' <param name="delimiter">3桁毎の桁区切り記号を入れる場合 <c>True</c>。</param>
        ''' <returns>処理後の文字列数値</returns>
        Public Shared Function RoundUpString(value As String, decimalLength As Integer, delimiter As Boolean) As String
            Dim _decimal = ParseDecimal(value)
            If Not _decimal.HasValue Then
                Return Nothing
            End If
            Return ToLocaleString(_decimal.Value.RoundUpAtAnyPlace(decimalLength), decimalLength, delimiter)
        End Function

        ''' <summary>
        ''' 文字列数値の切り捨て (0に近いほうに丸める)
        ''' </summary>
        ''' <param name="value">変換対象の文字列数値。</param>
        ''' <param name="decimalLength">小数点以下の桁数。</param>
        ''' <param name="delimiter">3桁毎の桁区切り記号を入れる場合 <c>True</c>。</param>
        ''' <returns>処理後の文字列数値</returns>
        Public Shared Function RoundDownString(value As String, decimalLength As Integer, delimiter As Boolean) As String
            Dim _decimal = ParseDecimal(value)
            If Not _decimal.HasValue Then
                Return Nothing
            End If
            Return ToLocaleString(_decimal.Value.RoundDownAtAnyPlace(decimalLength), decimalLength, delimiter)
        End Function

        ''' <summary>
        ''' 文字列数値の四捨五入 (銀行丸め)
        ''' </summary>
        ''' <param name="value">変換対象の文字列数値。</param>
        ''' <param name="decimalLength">小数点以下の桁数。</param>
        ''' <param name="delimiter">3桁毎の桁区切り記号を入れる場合 <c>True</c>。</param>
        ''' <returns>処理後の文字列数値</returns>
        Public Shared Function RoundHalfEvenString(value As String, decimalLength As Integer, delimiter As Boolean) As String
            Dim _decimal = ParseDecimal(value)
            If Not _decimal.HasValue Then
                Return Nothing
            End If
            Return ToLocaleString(_decimal.Value.RoundHalfEvenAtAnyPlace(decimalLength), decimalLength, delimiter)
        End Function

        ''' <summary>
        ''' 文字列数値の四捨五入 (中間値は0から遠いほうに丸める)
        ''' </summary>
        ''' <param name="value">変換対象の文字列数値。</param>
        ''' <param name="decimalLength">小数点以下の桁数。</param>
        ''' <param name="delimiter">3桁毎の桁区切り記号を入れる場合 <c>True</c>。</param>
        ''' <returns>処理後の文字列数値</returns>
        Public Shared Function RoundHalfUpString(value As String, decimalLength As Integer, delimiter As Boolean) As String
            Dim _decimal = ParseDecimal(value)
            If Not _decimal.HasValue Then
                Return Nothing
            End If
            Return ToLocaleString(_decimal.Value.RoundHalfUpAtAnyPlace(decimalLength), decimalLength, delimiter)
        End Function

#End Region

    End Class

End Namespace
