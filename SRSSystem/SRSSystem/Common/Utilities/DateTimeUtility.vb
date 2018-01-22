Imports System.Globalization
Imports SRS.Standards.MvcFramework.Core.Information
Imports SRS.Standards.MvcFramework.Core.Utilities

Namespace Utilities

    ''' <userName>SRS</userName>
    ''' <sysName>トレーニング</sysName>
    ''' <subSysName>共通ライブラリ</subSysName>
    ''' <summary>
    ''' 日時に関係するユーティリティ関数を定義します。
    ''' </summary>
    ''' <author>SRSTaro</author>
    ''' <Version>
    ''' 001, 2018-01-11, 新規作成
    ''' </Version>
    Public NotInheritable Class DateTimeUtility

        ''' <summary>
        ''' ダミーコンストラクタ
        ''' </summary>
        Private Sub New()

        End Sub

#Region "ユーザ言語設定に従って日付から文字列に変換"

        ''' <summary>
        ''' 文字列を日付と見做し、ユーザの言語設定に従って日付文字列に変換します。
        ''' </summary>
        ''' <param name="value">変換対象の日付。※DBから、日付を文字列として取得した場合を想定</param>
        ''' <returns>変換後文字列。</returns>
        Public Shared Function ToDateString(value As String) As String
            If String.IsNullOrWhiteSpace(value) Then
                Return Nothing
            End If
            Return ToDateString(Date.Parse(value))
        End Function

        ''' <summary>
        ''' 日付をユーザの言語設定に従って日付文字列に変換します。
        ''' </summary>
        ''' <param name="value">変換対象の日付。</param>
        ''' <returns>変換後文字列。</returns>
        Public Shared Function ToDateString(value As Date?) As String
            If Not value.HasValue Then
                Return String.Empty
            End If
            Return value.Value.ToString(InformationUtility.GetUserYmdFormat,
                                        New System.Globalization.CultureInfo(InformationManager.BusinessInfo.CultureInfoName))
        End Function

        ''' <summary>
        ''' 日付をユーザの言語設定に従って年月文字列に変換します。
        ''' </summary>
        ''' <param name="value">変換対象の日付。</param>
        ''' <returns>変換後文字列。</returns>
        Public Shared Function ToDateStringYM(value As Date?) As String
            If Not value.HasValue Then
                Return String.Empty
            End If
            Return value.Value.ToString(InformationUtility.GetUserYmFormat,
                                        New System.Globalization.CultureInfo(InformationManager.BusinessInfo.CultureInfoName))
        End Function

        ''' <summary>
        ''' 日付をユーザの言語設定に従って月日文字列に変換します。
        ''' </summary>
        ''' <param name="value">変換対象の日付。</param>
        ''' <returns>変換後文字列。</returns>
        Public Shared Function ToDateStringMD(value As Date?) As String
            If Not value.HasValue Then
                Return String.Empty
            End If

            Dim _format = InformationUtility.GetUserYmdFormat
            _format = _format.Replace("[^a-zA-Z]*y+[^a-zA-Z]*", "", Text.RegularExpressions.RegexOptions.Compiled)

            Return value.Value.ToString(_format,
                                        New System.Globalization.CultureInfo(InformationManager.BusinessInfo.CultureInfoName))
        End Function

        ''' <summary>
        ''' yyyyMMdd文字列日付をユーザの言語設定に従って日付文字列に変換します。
        ''' </summary>
        ''' <param name="ymdValue">変換対象の日付文字列。</param>
        ''' <returns>変換後文字列。</returns>
        Public Shared Function YMDToDateString(ymdValue As String) As String
            If String.IsNullOrWhiteSpace(ymdValue) Then
                Return Nothing
            End If

            '' 00000000 の場合
            If ymdValue = "00000000" Then
                Return InformationUtility.GetUserYmdFormat.Replace("[yMdhHms]", "0", Text.RegularExpressions.RegexOptions.Compiled)
            End If

            Return ToDateString(ParseYMD(ymdValue))
        End Function

        ''' <summary>
        ''' yyyyMM文字列日付をユーザの言語設定に従って日付文字列に変換します。
        ''' </summary>
        ''' <param name="ymValue">変換対象の日付文字列。</param>
        ''' <returns>変換後文字列。</returns>
        Public Shared Function YMToDateString(ymValue As String) As String
            If String.IsNullOrWhiteSpace(ymValue) Then
                Return Nothing
            End If

            '' 000000 の場合
            If ymValue = "000000" Then
                Return InformationUtility.GetUserYmFormat.Replace("[yMdhHms]", "0", Text.RegularExpressions.RegexOptions.Compiled)
            End If

            Return ToDateStringYM(ParseYM(ymValue))
        End Function

        ''' <summary>
        ''' MMdd文字列日付をユーザの言語設定に従って日付文字列に変換します。
        ''' </summary>
        ''' <param name="mdValue">変換対象の日付文字列。</param>
        ''' <returns>変換後文字列。</returns>
        Public Shared Function MDToDateString(mdValue As String) As String
            If String.IsNullOrWhiteSpace(mdValue) Then
                Return Nothing
            End If

            '' 0000 の場合
            If mdValue = "0000" Then
                Dim _format = InformationUtility.GetUserYmdFormat
                _format = _format.Replace("[^a-zA-Z]*y+[^a-zA-Z]*", "", Text.RegularExpressions.RegexOptions.Compiled)
                Return _format.Replace("[yMdhHms]", "0", Text.RegularExpressions.RegexOptions.Compiled)
            End If

            ' 閏年(2/29)を正常にパースするため、仮に閏年の年を付加する
            Return ToDateStringMD(ParseYMD("2000" & mdValue))
        End Function

        ''' <summary>
        ''' 文字列を日付と見做し、ユーザの言語設定に従って日時文字列に変換します。
        ''' </summary>
        ''' <param name="value">変換対象の日付。※DBから、日付を文字列として取得した場合を想定</param>
        ''' <returns>変換後文字列。</returns>
        Public Shared Function ToDateTimeString(value As String) As String
            If String.IsNullOrWhiteSpace(value) Then
                Return Nothing
            End If
            Return ToDateTimeString(Date.Parse(value))
        End Function

        ''' <summary>
        ''' 日付をユーザの言語設定に従って日時文字列に変換します。
        ''' </summary>
        ''' <param name="value">変換対象の日付。</param>
        ''' <returns>変換後文字列。</returns>
        Public Shared Function ToDateTimeString(value As Date?) As String
            If Not value.HasValue Then
                Return String.Empty
            End If
            If InformationManager.UserInfo.UserAttribute.ContainsKey("YmdFormat") Then
                Return value.Value.ToString(InformationManager.UserInfo.UserAttribute("YmdFormat") + " HH:mm:ss",
                                            New System.Globalization.CultureInfo(InformationManager.BusinessInfo.CultureInfoName))
            Else
                Return value.Value.ToString("yyyy/MM/dd HH:mm:ss",
                                            New System.Globalization.CultureInfo(InformationManager.BusinessInfo.CultureInfoName))
            End If
        End Function

        ''' <summary>
        ''' HHmmss文字列時刻をユーザの言語設定に従って時刻文字列に変換します。
        ''' </summary>
        ''' <param name="hmsValue">変換対象の日付文字列。</param>
        ''' <returns>変換後文字列。</returns>
        Public Shared Function HMSToTimeString(hmsValue As String) As String
            If String.IsNullOrWhiteSpace(hmsValue) OrElse Not IsValidHMS(hmsValue) Then
                Return Nothing
            End If

            '' 000000 の場合
            If hmsValue = "000000" Then
                Return "00:00:00"
            End If

            Dim _result = hmsValue.Substring(0, 2) & ":" & hmsValue.Substring(2, 2) & ":" & hmsValue.Substring(4, 2)

            Return _result
        End Function

#End Region

#Region "ユーザ言語設定に従って文字列から日付に変換"

        ''' <summary>
        ''' 文字列日付を <see cref="Date"/> に変換します。
        ''' </summary>
        ''' <param name="ymd">対象の文字列日付。</param>
        ''' <returns>変換された日付。</returns>
        Public Shared Function ParseYMD(ymd As String) As Date

            Dim _result As Date
            If Not Date.TryParseExact(ymd, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, _result) Then
                Throw New ArgumentException("Invalid Date String Value", "ymd")
            End If
            Return _result

        End Function

        ''' <summary>
        ''' yyyyMM文字列日付を <see cref="Date"/> に変換します。日付は1固定です。
        ''' </summary>
        ''' <param name="ym">対象の文字列日付。</param>
        ''' <returns>変換された日付。</returns>
        Public Shared Function ParseYM(ym As String) As Date

            Dim _result As Date
            If Not Date.TryParseExact(ym & "01", "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, _result) Then
                Throw New ArgumentException("Invalid Date String Value", "ym")
            End If
            Return _result

        End Function

        ''' <summary>
        ''' 文字列日付を <see cref="Date"/> に変換します。
        ''' </summary>
        ''' <param name="ymd">対象の文字列日付。</param>
        ''' <returns>変換された日付。</returns>
        Public Shared Function ParseYMDNullable(ymd As String) As Date?

            If ymd Is Nothing Then
                Return Nothing
            End If

            Dim _result As Date
            If Not Date.TryParseExact(ymd, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, _result) Then
                Return Nothing
            End If
            Return _result

        End Function

        ''' <summary>
        ''' 文字列日付が yyyyMMdd 形式で正常な日付かどうかを判定します。
        ''' </summary>
        ''' <param name="ymd">対象の文字列日付。</param>
        ''' <returns>正常な日付のとき <c>True</c>。</returns>
        Public Shared Function IsValidYMD(ymd As String) As Boolean

            Return ymd.CanConvertToDateTimeExact("yyyyMMdd")

        End Function

        ''' <summary>
        ''' 文字列日付が yyyyMM 形式で正常な日付かどうかを判定します。
        ''' </summary>
        ''' <param name="ym">対象の文字列日付。</param>
        ''' <returns>正常な日付のとき <c>True</c>。</returns>
        Public Shared Function IsValidYM(ym As String) As Boolean

            Return (ym & "01").CanConvertToDateTimeExact("yyyyMM01")

        End Function

        ''' <summary>
        ''' 日付を yyyyMMdd 形式の文字列に変換します。
        ''' </summary>
        ''' <param name="targetDate">対象日付。</param>
        ''' <returns>文字列日付。</returns>
        Public Shared Function ToYMDString(targetDate As Date) As String

            Return targetDate.ToString("yyyyMMdd", CultureInfo.InvariantCulture)

        End Function

        ''' <summary>
        ''' 正常なHHmmss形式かどうかを判定します。
        ''' </summary>
        ''' <param name="value">時刻を表す文字列。</param>
        ''' <returns>正常なとき <c>True</c>。不正なとき<c>False</c>。</returns>
        Public Shared Function IsValidHMS(value As String) As Boolean

            If value.Length <> 6 OrElse Not value.IsNumericOnly Then
                Return False
            End If

            Dim _hh = Integer.Parse(value.Substring(0, 2))
            Dim _mm = Integer.Parse(value.Substring(2, 2))
            Dim _ss = Integer.Parse(value.Substring(4, 2))

            ' 範囲判定
            If 0 > _hh OrElse _hh >= 24 Then
                Return False
            End If
            If 0 > _mm OrElse _mm >= 60 Then
                Return False
            End If
            If 0 > _ss OrElse _ss >= 60 Then
                Return False
            End If

            Return True

        End Function

#End Region

#Region "文字列のまま日付操作"

        ''' <summary>
        ''' 文字列日付(yyyyMMdd)を指定した日数だけずらします。
        ''' </summary>
        ''' <param name="ymd">対象の文字列日付。</param>
        ''' <param name="days">追加する日数。負数も可。</param>
        ''' <returns>調整後の文字列日付。</returns>
        Public Shared Function AddDaysToYMD(ymd As String, days As Integer) As String

            If ymd Is Nothing Then
                Return Nothing
            End If

            Return ToYMDString(ParseYMD(ymd).AddDays(days))

        End Function

        ''' <summary>
        ''' 文字列日付(yyyyMM)を指定した月数だけずらします。
        ''' </summary>
        ''' <param name="ym">対象の文字列日付。</param>
        ''' <param name="months">追加する日数。負数も可。</param>
        ''' <returns>調整後の文字列日付。</returns>
        Public Shared Function AddMonthsToYM(ym As String, months As Integer) As String

            If ym Is Nothing Then
                Return Nothing
            End If

            Return ToYMDString(ParseYMD(ym & "01").AddMonths(months)).Substring(0, 6)

        End Function

        ''' <summary>
        ''' 月初日を文字列日付で取得します。
        ''' </summary>
        ''' <param name="ymd">対象の文字列日付。</param>
        ''' <returns>調整後の文字列日付。</returns>
        Public Shared Function GetYMDFirstDateOfMonth(ymd As String) As String

            If ymd Is Nothing Then
                Return Nothing
            End If

            Return ToYMDString(ParseYMD(ymd).ToFirstDateOfMonth())

        End Function

        ''' <summary>
        ''' 月末日を文字列日付で取得します。
        ''' </summary>
        ''' <param name="ymd">対象の文字列日付。</param>
        ''' <returns>調整後の文字列日付。</returns>
        Public Shared Function GetYMDLastDateOfMonth(ymd As String) As String

            If ymd Is Nothing Then
                Return Nothing
            End If

            Return ToYMDString(ParseYMD(ymd).ToLastDateOfMonth())

        End Function

#End Region

    End Class

End Namespace
