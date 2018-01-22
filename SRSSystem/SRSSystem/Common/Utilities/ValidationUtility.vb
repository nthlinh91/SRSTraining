Imports System.ComponentModel
Imports System.Reflection
Imports SRS.Standards.MvcFramework.Core.Facade
Imports SRS.Standards.MvcFramework.Core.Information
Imports SRS.Standards.MvcFramework.Core.Message
Imports SRS.Standards.MvcFramework.Core.MVC
Imports SRS.Standards.MvcFramework.Core.Utilities

Namespace Utilities

    ''' <userName>SRS</userName>
    ''' <sysName>トレーニング</sysName>
    ''' <subSysName>共通ライブラリ</subSysName>
    ''' <summary>
    ''' 出力チェックに関係するユーティリティ関数を定義します。
    ''' </summary>
    ''' <author>SRSTaro</author>
    ''' <Version>
    ''' 001, 2018-01-11, 新規作成
    ''' </Version>
    Public NotInheritable Class ValidationUtility

        ''' <summary>
        ''' ダミーコンストラクタ
        ''' </summary>
        Private Sub New()
        End Sub

#Region "モデルチェック"

        ''' <summary>
        ''' モデルをモデル定義に設定した属性に従っているかどうかを検査します。
        ''' </summary>
        ''' <param name="model">エラーを設定するモデル。</param>
        ''' <param name="targetObject">検査対象オブジェクト。</param>
        ''' <param name="objectPath">ルート要素から検査対象オブジェクトに至るまでのパス。エラーに設定するプロパティ名に使用されます。</param>
        ''' <returns>問題がないとき <c>True</c>。</returns>
        Public Shared Function ValidateModel(model As IViewModel, targetObject As Object, objectPath As String) As Boolean

            Dim _type = targetObject.GetType()

            ' 一覧明細のとき、削除済フラグが True の場合か、選択対象でないときは抜ける (無条件でチェック成功)
            If GetType(ViewElementModelBase).IsAssignableFrom(_type) Then
                If DirectCast(targetObject, ViewElementModelBase).DeleteFlag = True OrElse
                    DirectCast(targetObject, ViewElementModelBase).SelectFlag = False Then
                    Return True
                End If
            End If

            Dim _properties = _type.GetProperties(BindingFlags.Public Or BindingFlags.Instance Or BindingFlags.FlattenHierarchy)

            For Each _property In _properties

                ' 無引数の値でない場合は抜ける
                If _property.GetIndexParameters().Count > 0 Then
                    Continue For
                End If

                ' 文字列型の場合のみ、チェックを行う
                If _property.PropertyType Is GetType(String) Then

                    Dim _attrs = _property.GetCustomAttributes(GetType(SRSValidationAttribute), True)

                    If _attrs Is Nothing OrElse _attrs.Count = 0 Then
                        Continue For
                    End If

                    ' 表示名を取得
                    Dim _displayName = ""
                    Dim _nameAttr = DirectCast(_property.GetCustomAttributes(GetType(DisplayNameAttribute), False), DisplayNameAttribute())
                    If _nameAttr.Count > 0 Then
                        _displayName = MessageManager.GetMessage(_nameAttr(0).DisplayName)
                    Else
                        _displayName = _property.Name
                    End If

                    ' プロパティ名を取得 (計算)
                    Dim _propertyName = If(objectPath.IsNullOrEmpty, "", objectPath) & _property.Name
                    ' 値を取得
                    Dim _value = DBNullToEmptyString(_property.GetValue(targetObject, Nothing))

                    For Each _attr In _attrs

                        Dim _attrType = _attr.GetType()

                        '' 必須チェックが成功し、かつ値が空でないときだけ他のチェックを行う
                        If ValidateRequired(model, DirectCast(_attr, SRSValidationAttribute), _value, _displayName, _propertyName) AndAlso
                             Not _value.IsNullOrWhiteSpace Then

                            If _attrType Is GetType(SRSStringValidationAttribute) Then
                                ValidateString(model, DirectCast(_attr, SRSStringValidationAttribute), _value, _displayName, _propertyName)
                            ElseIf _attrType Is GetType(SRSNumberValidationAttribute) Then
                                ValidateNumber(model, DirectCast(_attr, SRSNumberValidationAttribute), _value, _displayName, _propertyName)
                            ElseIf _attrType Is GetType(SRSYMDValidationAttribute) Then
                                ValidateYMD(model, DirectCast(_attr, SRSYMDValidationAttribute), _value, _displayName, _propertyName)
                            ElseIf _attrType Is GetType(SRSYMValidationAttribute) Then
                                ValidateYM(model, DirectCast(_attr, SRSYMValidationAttribute), _value, _displayName, _propertyName)
                            ElseIf _attrType Is GetType(SRSHMSValidationAttribute) Then
                                ValidateHMS(model, DirectCast(_attr, SRSHMSValidationAttribute), _value, _displayName, _propertyName)
                            End If
                        End If

                    Next

                ElseIf GetType(IEnumerable).IsAssignableFrom(_property.PropertyType) Then
                    ' モデル型で列挙可能な場合、再帰処理を行う

                    Dim _newTargets = DirectCast(_property.GetValue(targetObject, Nothing), IEnumerable)

                    If _newTargets IsNot Nothing Then
                        Dim _index = 0
                        For Each _newTarget In _newTargets
                            ValidateModel(model, _newTarget, objectPath & _property.Name & "[" & _index.ToString & "].")
                            _index += 1
                        Next
                    End If

                ElseIf _property.PropertyType.IsClass AndAlso _property.PropertyType IsNot GetType(Object) Then
                    ' クラスの場合再帰処理
                    Dim _newTarget = _property.GetValue(targetObject, Nothing)
                    If _newTarget IsNot Nothing Then
                        ValidateModel(model, _newTarget, objectPath & _property.Name & ".")
                    End If
                End If

            Next

            If model.ValidLevel = ValidLevelType.Error OrElse model.ValidLevel = ValidLevelType.Fatal Then
                Return False
            End If

            Return True

        End Function

        ''' <summary>
        ''' 必須かどうかをチェックします。
        ''' </summary>
        ''' <param name="model">エラーを設定するモデル。</param>
        ''' <param name="attr">バリデーション属性。</param>
        ''' <param name="value">値。</param>
        ''' <param name="displayName">表示名。</param>
        ''' <param name="propertyName">プロパティ名。</param>
        ''' <returns>問題がないとき <c>True</c>。</returns>
        Public Shared Function ValidateRequired(model As IViewModel, attr As SRSValidationAttribute,
                                                value As String, displayName As String, propertyName As String) As Boolean

            If attr.Required And value.IsNullOrWhiteSpace Then
                '' {0}は入力必須項目です。入力してください。
                model.SetValidateMember(ValidLevelType.Error, propertyName, "CMN_E00018",
                                        displayName)
                Return False
            End If
            Return True
        End Function

        ''' <summary>
        ''' 文字列要素としてプロパティをチェックします。
        ''' </summary>
        ''' <param name="model">エラーを設定するモデル。</param>
        ''' <param name="attr">バリデーション属性。</param>
        ''' <param name="value">値。</param>
        ''' <param name="displayName">表示名。</param>
        ''' <param name="propertyName">プロパティ名。</param>
        ''' <returns>問題がないとき <c>True</c>。</returns>
        Public Shared Function ValidateString(model As IViewModel, attr As SRSStringValidationAttribute,
                                              value As String, displayName As String, propertyName As String) As Boolean

            Dim _result = True

            '' 文字種別毎のチェック
            Select Case attr.Type
                Case CharacterType.Alphabet
                    If Not value.IsAlphabetOnly Then
                        '' {0}には半角の英字を入力してください。
                        model.SetValidateMember(ValidLevelType.Error, propertyName, "CMN_E00050",
                                                displayName)
                        _result = False
                    End If
                Case CharacterType.AlphaNumeric
                    If Not value.IsNumericOrAlphabetOnly Then
                        '' {0}には半角の英数文字を入力してください。
                        model.SetValidateMember(ValidLevelType.Error, propertyName, "CMN_E00019",
                                                displayName)
                        _result = False
                    End If
                Case CharacterType.Numeric
                    If Not value.IsNumericOnly Then
                        '' {0}には半角の数字を入力してください。
                        model.SetValidateMember(ValidLevelType.Error, propertyName, "CMN_E00022",
                                                displayName)
                        _result = False
                    End If
                Case CharacterType.HalfWidth
                    If Not value.IsHalfWidthCharOnly Then
                        '' {0}には半角の文字を入力してください。
                        model.SetValidateMember(ValidLevelType.Error, propertyName, "CMN_E00026",
                                                displayName)
                        _result = False
                    End If
                Case CharacterType.FullWidth
                    If Not value.IsFullWidthCharOnly Then
                        '' {0}には全角の文字を入力してください。
                        model.SetValidateMember(ValidLevelType.Error, propertyName, "CMN_E00025",
                                                displayName)
                        _result = False
                    End If
                Case CharacterType.HalfWidthKatakana
                    If Not value.IsHalfwidthKatakanaOnly Then
                        '' {0}には半角カタカナを入力してください。
                        model.SetValidateMember(ValidLevelType.Error, propertyName, "CMN_E00051",
                                                displayName)
                        _result = False
                    End If
                Case CharacterType.ASCII
                    If value.IsIncludeFullwidthChar OrElse value.IsIncludeHalfwidthKatakana OrElse value.IsIncludeNonSjis OrElse value.IsIncludeNonAsciiSymbol Then
                        '' {0}には半角の英数字、または記号を入力してください。
                        model.SetValidateMember(ValidLevelType.Error, propertyName, "MSG_E00384",
                                                displayName)
                        _result = False
                    End If
                Case CharacterType.AlphaNumericKantakana
                    If value.IsIncludeFullwidthChar OrElse value.IsIncludeSymbol OrElse value.IsIncludeNonSjis OrElse value.IsIncludeNonAsciiSymbol Then
                        '' {0}には英数字か半角カタカナを入力してください。
                        model.SetValidateMember(ValidLevelType.Error, propertyName, "CMN_E00062",
                                                    displayName)
                        _result = False
                    End If
                Case CharacterType.ShiftJIS
                    If value.IsIncludeNonSjis Then
                        '' {0}にはShiftJISを入力してください。
                        model.SetValidateMember(ValidLevelType.Error, propertyName, "CMN_E00063",
                                                displayName)
                        _result = False
                    End If
                Case CharacterType.Symbol
                    If value.IsIncludeNonSjis OrElse value.IsIncludeFullwidthChar OrElse value.IsIncludeDigit OrElse
                        value.IsIncludeAlphabet OrElse value.IsIncludeHalfwidthKatakana Then
                        '' {0}には半角の記号を入力してください。
                        model.SetValidateMember(ValidLevelType.Error, propertyName, "MSG_E00383",
                                                displayName)
                        _result = False
                    End If
            End Select

            If attr.MinLength > 0 AndAlso attr.MaxLength > 0 AndAlso attr.MinLength = attr.MaxLength Then
                '' 総桁チェック
                If value.Length <> attr.MinLength Then
                    '' {0}には{1}桁で入力してください。
                    model.SetValidateMember(ValidLevelType.Error, propertyName, "CMN_E00027",
                                            displayName, attr.MinLength.ToString)
                    _result = False
                End If
            Else
                If attr.MinLength > 0 Then
                    '' 最小桁チェック
                    If value.Length < attr.MinLength Then
                        '' {0}が短すぎます。{1}文字以上で入力してください。
                        model.SetValidateMember(ValidLevelType.Error, propertyName, "CMN_E00020",
                                                displayName, attr.MinLength.ToString)
                        _result = False
                    End If
                End If

                If attr.MaxLength > 0 Then
                    '' 最大桁チェック
                    If value.Length > attr.MaxLength Then
                        '' {0}が長すぎます。{1}文字以下で入力してください。
                        model.SetValidateMember(ValidLevelType.Error, propertyName, "CMN_E00021",
                                                displayName, attr.MaxLength.ToString)
                        _result = False
                    End If
                End If
            End If

            Return _result

        End Function

        ''' <summary>
        ''' 数値要素としてプロパティをチェックします。
        ''' </summary>
        ''' <param name="model">エラーを設定するモデル。</param>
        ''' <param name="attr">バリデーション属性。</param>
        ''' <param name="value">値。</param>
        ''' <param name="displayName">表示名。</param>
        ''' <param name="propertyName">プロパティ名。</param>
        ''' <returns>問題がないとき <c>True</c>。</returns>
        Public Shared Function ValidateNumber(model As IViewModel, attr As SRSNumberValidationAttribute,
                                              value As String, displayName As String, propertyName As String) As Boolean

            Dim _result = True

            ' 区切り文字の除去
            value = value.Replace(",", "")

            ' 小数点記号の置換 (ユーザ表現 → 内部表現)
            'value = value.Replace(InformationUtility.GetUserDecimalPoint, My.Application.Culture.NumberFormat.NumberDecimalSeparator)

            '' 小数のパース
            If Not NumberUtility.CanConvertToDecimal(value) Then
                If attr.DecimalLength = 0 Then
                    '' {0}には半角の数字を入力してください。
                    model.SetValidateMember(ValidLevelType.Error, propertyName, "CMN_E00022",
                                            displayName)
                    _result = False
                Else
                    '' {0}には半角の小数を入力してください。
                    model.SetValidateMember(ValidLevelType.Error, propertyName, "CMN_E00048",
                                            displayName)
                    _result = False
                End If
            Else
                '' 小数桁の検査
                Dim _targetDecimalIndex = value.IndexOf(".")
                Dim _targetDecimalLength = 0
                If _targetDecimalIndex >= 0 Then
                    _targetDecimalLength = value.Length - _targetDecimalIndex - 1
                End If

                If _targetDecimalLength > attr.DecimalLength OrElse (_targetDecimalIndex >= 0 AndAlso attr.DecimalLength = 0) Then
                    '' {0}には小数桁{1}で入力してください。
                    model.SetValidateMember(ValidLevelType.Error, propertyName, "CMN_E00049",
                                            displayName, attr.DecimalLength.ToString())
                    _result = False
                End If


                Dim _number = NumberUtility.ParseDecimal(value)

                ' 最小値チェック
                If _number < attr.MinValue Then
                    '' {0}には{1}以上の値を入力してください。
                    model.SetValidateMember(ValidLevelType.Error, propertyName, "CMN_E00023",
                                            displayName, NumberUtility.ToLocaleString(CDec(attr.MinValue)))
                    _result = False
                End If

                ' 最大値チェック
                If _number > attr.MaxValue Then
                    '' {0}には{1}以下の値を入力してください。
                    model.SetValidateMember(ValidLevelType.Error, propertyName, "CMN_E00024",
                                            displayName, NumberUtility.ToLocaleString(CDec(attr.MaxValue)))
                    _result = False
                End If
            End If

            Return _result

        End Function

        ''' <summary>
        ''' 年月日要素としてプロパティをチェックします。
        ''' </summary>
        ''' <param name="model">エラーを設定するモデル。</param>
        ''' <param name="attr">バリデーション属性。</param>
        ''' <param name="value">値。</param>
        ''' <param name="displayName">表示名。</param>
        ''' <param name="propertyName">プロパティ名。</param>
        ''' <returns>問題がないとき <c>True</c>。</returns>
        Public Shared Function ValidateYMD(model As IViewModel, attr As SRSYMDValidationAttribute,
                                           value As String, displayName As String, propertyName As String) As Boolean

            Dim _result = True

            If Not DateTimeUtility.IsValidYMD(value) Then
                '' {0}に有効な日付を yyyyMMdd 形式で入力してください。
                model.SetValidateMember(ValidLevelType.Error, propertyName, "CMN_E00052",
                                        displayName)
                _result = False
            End If

            Return _result

        End Function

        ''' <summary>
        ''' 年月要素としてプロパティをチェックします。
        ''' </summary>
        ''' <param name="model">エラーを設定するモデル。</param>
        ''' <param name="attr">バリデーション属性。</param>
        ''' <param name="value">値。</param>
        ''' <param name="displayName">表示名。</param>
        ''' <param name="propertyName">プロパティ名。</param>
        ''' <returns>問題がないとき <c>True</c>。</returns>
        Public Shared Function ValidateYM(model As IViewModel, attr As SRSYMValidationAttribute,
                                          value As String, displayName As String, propertyName As String) As Boolean

            Dim _result = True

            If Not DateTimeUtility.IsValidYM(value) Then
                '' {0}に有効な年月を yyyyMM 形式で入力してください。
                model.SetValidateMember(ValidLevelType.Error, propertyName, "CMN_E00053",
                                        displayName)
                _result = False
            End If

            Return _result

        End Function

        ''' <summary>
        ''' 時分秒要素としてプロパティをチェックします。
        ''' </summary>
        ''' <param name="model">エラーを設定するモデル。</param>
        ''' <param name="attr">バリデーション属性。</param>
        ''' <param name="value">値。</param>
        ''' <param name="displayName">表示名。</param>
        ''' <param name="propertyName">プロパティ名。</param>
        ''' <returns>問題がないとき <c>True</c>。</returns>
        Public Shared Function ValidateHMS(model As IViewModel, attr As SRSHMSValidationAttribute,
                                           value As String, displayName As String, propertyName As String) As Boolean

            Dim _result = True

            If Not DateTimeUtility.IsValidHMS(value) Then
                '' {0}に有効な時刻を HHmmss 形式で入力してください。
                model.SetValidateMember(ValidLevelType.Error, propertyName, "CMN_E00065",
                                        displayName)
                _result = False
            End If

            Return _result

        End Function

#End Region

#Region "エラーリスト作成"

        ''' <summary>
        ''' エラーリストを作成します。
        ''' </summary>
        ''' <param name="models">エラーを含んだモデルの集合</param>
        ''' <param name="fileName">出力ファイル名 (指定されない場合、 ErrorList_yyyyMMddHHmmss.xlsx となります)</param>
        ''' <param name="sheetName">出力シート名</param>
        ''' <returns>エラーリストのファイルパス</returns>
        ''' <remarks>モデル内容を自動的に全て出力します。</remarks>
        Public Shared Function CreateErrorList(models As IEnumerable(Of IViewModel),
                                               Optional fileName As String = Nothing,
                                               Optional sheetName As String = "ErrorList") As String

            '' FileIDを採番
            Dim _fileId = FileUtilities.CreateTempDirectory()
            Dim _tempPath = FileUtilities.GetTempDirectory(_fileId)

            If fileName Is Nothing Then
                fileName = "ErrorList_" & Now.ToString("yyyyMMddHHmmss",
                                                       System.Globalization.CultureInfo.InvariantCulture) & ".xlsx"
            End If

            '' ファイル名(フルパス)を定義
            Dim _filePath = System.IO.Path.Combine(_tempPath, fileName)

            '' エラーリスト生成
            Using _package As New OfficeOpenXml.ExcelPackage(New System.IO.FileInfo(_filePath))

                Dim _sheet = _package.Workbook.Worksheets.Add(sheetName) ' シート名

                '' 型情報から、プロパティ名とラベルの一覧を取得
                Dim _itemType = models.GetType().GetGenericArguments()(0)
                Dim _nameDic = GetPropertyDisplayName(_itemType)

                _sheet.Cells(1, 1).Value = MessageManager.GetMessage("lbl_ZZ_ERRORITEM") ' エラー項目
                _sheet.Cells(1, 2).Value = MessageManager.GetMessage("lbl_ZZ_ERRORDESC") ' エラー内容

                Dim _columnIndex = 3
                '' ヘッダ出力
                For Each _item In _nameDic
                    _sheet.Cells(1, _columnIndex).Value = _item.Value
                    _columnIndex += 1
                Next

                Dim _culture As New System.Globalization.CultureInfo(InformationManager.BusinessInfo.CultureInfoName)
                Dim _ymdFormat As String = _culture.DateTimeFormat.ShortDatePattern & " " & _culture.DateTimeFormat.ShortTimePattern

                ' 内容出力
                Dim _rowIndex = 2
                For Each _item In models
                    If _item.ValidLevel = ValidLevelType.Normal Then
                        '' エラーがなければ飛ばす
                        Continue For
                    End If

                    '' 1つのデータに複数エラーが設定されている場合があるので、全て出力する
                    For Each _errorProperty In _item.ValidateMembers
                        For Each _error In _errorProperty.Value

                            ' エラー項目名を出力
                            Dim _key = _errorProperty.Key
                            Dim _match = _errorProperty.Key.Match("^([^\[]*)\[[0-9]+\]", Text.RegularExpressions.RegexOptions.Compiled)
                            Dim _pathOriginal As String = Nothing
                            Dim _pathNormalize As String = Nothing
                            If _match.Success Then
                                Dim _replace As New Text.RegularExpressions.Regex("\[[0-9]+\]", Text.RegularExpressions.RegexOptions.Compiled)

                                _key = _replace.Replace(_key, "[0]", Text.RegularExpressions.RegexOptions.Compiled)
                                _pathOriginal = _match.Groups(0).Value
                                _pathNormalize = _replace.Replace(_pathOriginal, "[0]", Text.RegularExpressions.RegexOptions.Compiled)
                            End If

                            _sheet.Cells(_rowIndex, 1).Value = _nameDic(_key)
                            ' エラー内容を出力 (リソースIDから実際の文字列に変換)
                            _sheet.Cells(_rowIndex, 2).Value = MessageManager.GetMessage(_error.Message, _error.MessageParams)

                            ' 項目内容を出力
                            _columnIndex = 3
                            For Each _name In _nameDic
                                ' エラープロパティと同じツリー位置のものは、インデックスをあわせる
                                Dim _nameKey = _name.Key
                                If _pathNormalize IsNot Nothing Then
                                    _nameKey = _nameKey.Replace(_pathNormalize, _pathOriginal)
                                End If

                                Dim _value = GetModelValue(_item, _nameKey)
                                _sheet.Cells(_rowIndex, _columnIndex).Value = _value
                                If TypeOf _value Is Date Then
                                    _sheet.Cells(_rowIndex, _columnIndex).Style.Numberformat.Format = _ymdFormat
                                End If
                                _columnIndex += 1
                            Next
                            _rowIndex += 1
                        Next
                    Next
                Next

                '' 列内容にあわせて列幅を自動調整
                _sheet.Cells().AutoFitColumns()

                _package.Save()
            End Using

            Return _filePath

        End Function

        ''' <summary>
        ''' 型情報から、プロパティフルネームとプロパティ表示名の辞書を取得します。
        ''' </summary>
        ''' <param name="type">型情報</param>
        ''' <param name="prefix">作成するフルネームの前に付けるプレフィックス。</param>
        ''' <param name="container">結果を返すためのオブジェクト。指定されなければ新規作成します。</param>
        ''' <returns>プロパティの表示名</returns>
        ''' <remarks>型の分割に対応</remarks>
        Private Shared Function GetPropertyDisplayName(type As Type,
                                                       Optional prefix As String = "",
                                                       Optional container As Dictionary(Of String, String) = Nothing) As Dictionary(Of String, String)

            Dim _result As Dictionary(Of String, String) = container
            If _result Is Nothing Then
                _result = New Dictionary(Of String, String)
            End If

            Dim _properties = type.GetProperties(Reflection.BindingFlags.Instance Or Reflection.BindingFlags.Public Or Reflection.BindingFlags.DeclaredOnly)

            For Each _property In _properties

                ' 無引数の値でない場合は抜ける
                If _property.GetIndexParameters().Count > 0 Then
                    Continue For
                End If

                If _property.PropertyType Is GetType(String) OrElse _property.PropertyType Is GetType(Decimal) OrElse
                    _property.PropertyType Is GetType(Integer) OrElse _property.PropertyType Is GetType(Short) OrElse
                    _property.PropertyType Is GetType(Double) OrElse _property.PropertyType Is GetType(Single) OrElse
                    _property.PropertyType Is GetType(Long) OrElse
                    _property.PropertyType Is GetType(Decimal?) OrElse
                    _property.PropertyType Is GetType(Integer?) OrElse _property.PropertyType Is GetType(Short?) OrElse
                    _property.PropertyType Is GetType(Double?) OrElse _property.PropertyType Is GetType(Single?) OrElse
                    _property.PropertyType Is GetType(Long?) Then
                    ' 文字列かDecimal/Integer/Short/Long/Single/Doubleの場合対象

                    Dim _attr = DirectCast(_property.GetCustomAttributes(GetType(DisplayNameAttribute), False), DisplayNameAttribute())

                    If _attr.Count > 0 Then
                        _result.Add(prefix & _property.Name, MessageManager.GetMessage(_attr(0).DisplayName))
                    Else
                        _result.Add(prefix & _property.Name, _property.Name)
                    End If

                ElseIf GetType(IEnumerable).IsAssignableFrom(_property.PropertyType) Then
                    ' モデル型で列挙可能な場合、再帰処理を行う

                    Dim _elementTypes = _property.PropertyType.GetGenericArguments()

                    If _elementTypes IsNot Nothing AndAlso _elementTypes.Length > 0 Then
                        GetPropertyDisplayName(_elementTypes(0), prefix & _property.Name & "[0].", _result)
                    End If

                ElseIf _property.PropertyType.IsClass Then
                    ' クラスの場合再帰処理
                    GetPropertyDisplayName(_property.PropertyType, _property.Name & ".", _result)
                End If
            Next

            Return _result

        End Function

        ''' <summary>
        ''' モデルから、プロパティフルネームを用いて値を取得する
        ''' </summary>
        ''' <param name="model">取得対象のモデル</param>
        ''' <param name="propertyName">プロパティフルネーム</param>
        ''' <returns>プロパティ値</returns>
        Private Shared Function GetModelValue(model As Object, propertyName As String) As Object

            If model Is Nothing Then
                Return Nothing
            End If

            Dim _names = propertyName.Split("."c)

            Dim _currentObject = model
            Dim _currentType = model.GetType()

            For Each _name In _names

                Dim _index As Integer? = Nothing

                ' IEnumerableかどうか判定して、Index値を取得(そうでなければindex=Nothing)
                Dim _match = _name.Match("^([^\[]+)\[([0-9]+)\]$", Text.RegularExpressions.RegexOptions.Compiled)
                If _match IsNot Nothing AndAlso _match.Success Then
                    _name = _match.Groups(1).Value
                    _index = Integer.Parse(_match.Groups(2).Value)
                End If

                Dim _property = _currentType.GetProperty(_name)
                If _property Is Nothing Then
                    Return Nothing
                End If

                _currentObject = _property.GetValue(_currentObject, Nothing)
                If _currentObject Is Nothing Then
                    Return Nothing
                End If
                ' IEnumerableの場合は、要素を取得
                If _index.HasValue Then
                    _currentObject = DirectCast(_currentObject, IEnumerable)(_index.Value)
                End If

                _currentType = _currentObject.GetType()

            Next

            Return _currentObject

        End Function

        ''' <summary>
        ''' モデルから、プロパティフルネームを用いて値を設定する
        ''' </summary>
        ''' <param name="model">取得対象のモデル</param>
        ''' <param name="propertyName">プロパティフルネーム</param>
        ''' <param name="value">値</param>
        Private Shared Sub SetModelValue(model As Object, propertyName As String, value As Object)

            If model Is Nothing Then
                Return
            End If

            Dim _names = propertyName.Split("."c)

            Dim _currentObject = model
            Dim _currentType = model.GetType()
            Dim _propertyObject As Object = Nothing
            Dim _property As PropertyInfo = Nothing

            For Each _name In _names

                Dim _index As Integer? = Nothing

                ' IEnumerableかどうか判定して、Index値を取得(そうでなければindex=Nothing)
                Dim _match = _name.Match("^([^\[]+)\[([0-9]+)\]$", Text.RegularExpressions.RegexOptions.Compiled)
                If _match IsNot Nothing AndAlso _match.Success Then
                    _name = _match.Groups(1).Value
                    _index = Integer.Parse(_match.Groups(2).Value)
                End If

                _propertyObject = _currentObject
                _property = _currentType.GetProperty(_name)
                If _property Is Nothing Then
                    Return
                End If

                _currentObject = _property.GetValue(_currentObject, Nothing)
                If _currentObject Is Nothing Then
                    Return
                End If
                ' IEnumerableの場合は、要素を取得
                If _index.HasValue Then
                    _currentObject = DirectCast(_currentObject, IEnumerable)(_index.Value)
                End If

                _currentType = _currentObject.GetType()
            Next

            If _property IsNot Nothing Then
                _property.SetValue(_propertyObject, value, Nothing)
            End If

        End Sub

#End Region

    End Class

End Namespace
