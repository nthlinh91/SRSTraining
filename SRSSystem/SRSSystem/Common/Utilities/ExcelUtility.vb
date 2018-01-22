Imports System.IO
Imports System.Reflection
Imports System.Web.Hosting
Imports OfficeOpenXml

Namespace Utilities

    Public NotInheritable Class ExcelUtility

        '列名記載行
        Private Const ColumnRow As Integer = 1
        'データ開始行
        Private Const StartRow As Integer = 2
        '項目開始列
        Private Const StartColumn As Integer = 1

        Private Sub New()

        End Sub

        ''' <summary>
        ''' Excelファイルのデータをモデルにマップします。指定されたファイルがないなど、読み取り処理ができない場合は、 null が返ります。
        ''' </summary>
        ''' <param name="fileName">ファイルパス</param>
        ''' <returns>モデルにマップしたデータの一覧</returns>
        Public Shared Function MapTo(Of T)(fileName As String) As List(Of T)

            Try
                Dim workbook As ExcelWorkbook = LoadWorkbook(fileName)
                If workbook Is Nothing Then
                    Return Nothing
                End If
                WriteLog("Info：{0} のデータを読込みます。処理開始", fileName)

                Dim worksheet As ExcelWorksheet = LoadWorksheet(workbook)
                If worksheet Is Nothing Then
                    Return Nothing
                End If

                'データが登録されている最大行
                Dim rowNum As Integer = worksheet.Dimension.End.Row
                'データが登録されている最大列
                Dim colNum As Integer = worksheet.Dimension.End.Column
                If Not CheckSheetData(Of T)(worksheet, rowNum, colNum) Then
                    Return Nothing
                End If

                Return LoadData(Of T)(worksheet, rowNum, colNum)
            Catch ex As Exception
                WriteLog("Error：{0}", ex.Message)
                Return Nothing
            End Try

        End Function

        ''' <summary>
        ''' 指定されたファイル名から Excel Workbook をロードします。
        ''' </summary>
        ''' <param name="fileName">読み取るエクセルの <see cref="ExcelWorkbook"/> インスタンス</param>
        ''' <returns>読み取られたエクセルの <see cref="ExcelWorkbook"/> インスタンス</returns>
        Private Shared Function LoadWorkbook(fileName As String) As ExcelWorkbook
            Try
                Return New ExcelPackage(New FileInfo(Path.Combine(HostingEnvironment.ApplicationPhysicalPath, fileName))).Workbook()
            Catch ex As FileNotFoundException
                WriteLog("Error：指定されたファイル({0}) と一致するファイルが見つかりません。", fileName)
                Return Nothing
            End Try
        End Function

        ''' <summary>
        ''' ブックから先頭のシートをロードします。
        ''' </summary>
        ''' <param name="workbook">シートを取得するためのブック</param>
        ''' <returns>ブックの先頭にあるシート</returns>
        Private Shared Function LoadWorksheet(workbook As ExcelWorkbook) As ExcelWorksheet
            Dim worksheet As ExcelWorksheet = workbook.Worksheets.FirstOrDefault()

            If worksheet Is Nothing Then
                WriteLog("Error：Excelファイルにシートが見つかりません。")
                Return Nothing
            End If

            Return worksheet
        End Function
        ''' <summary>
        ''' シートとモデルの定義に不一致があるかどうかをチェックします。
        ''' </summary>
        ''' <typeparam name="T">モデルの型</typeparam>
        ''' <param name="worksheet">読み取るシート</param>
        ''' <param name="rowNum">データの行数</param>
        ''' <param name="colNum">データのカラム数</param>
        ''' <returns>チェック結果。不一致がない場合は true 、そうでない場合は false</returns>
        Private Shared Function CheckSheetData(Of T)(worksheet As ExcelWorksheet, rowNum As Integer, colNum As Integer) As Boolean

            '1行目の入力ありの列数
            Dim usedColNum = worksheet.Cells(ColumnRow, StartColumn, ColumnRow, colNum).Count
            If usedColNum = 0 Then
                WriteLog("Error：1行目がすべて空白です。列名を設定してください。")
                Return False
            End If
            If colNum > usedColNum Then
                WriteLog("Error: 1行目の列名にNullが設定されています。（{0}件）", colNum - usedColNum)
                Return False
            End If

            Dim modelType As Type = GetType(T)
            For j = StartColumn To colNum
                Dim key = worksheet.Cells(ColumnRow, j).GetValue(Of String)()
                Dim modelProperty As Reflection.PropertyInfo = modelType.GetProperty(key, BindingFlags.IgnoreCase Or BindingFlags.Public Or BindingFlags.Instance)
                If modelProperty Is Nothing Then
                    WriteLog("Warning：{0} がモデル({1}) のプロパティに存在しません。", key, modelType.Name)
                End If
            Next

            Return True

        End Function

        ''' <summary>
        ''' シートからデータをロードして、モデルにマッピングします。
        ''' </summary>
        ''' <typeparam name="T">モデルの型</typeparam>
        ''' <param name="worksheet">読み取るシート</param>
        ''' <param name="rowNum">データの行数</param>
        ''' <param name="colNum">データのカラム数</param>
        ''' <returns>シートから読み取ったデータがマッピングされたモデルの一覧</returns>
        Private Shared Function LoadData(Of T)(worksheet As ExcelWorksheet, rowNum As Integer, colNum As Integer) As List(Of T)
            Dim excelRows As New List(Of Dictionary(Of String, ExcelRange))

            For i = StartRow To rowNum
                Dim excelRowData = New Dictionary(Of String, ExcelRange)
                Dim rowData As ExcelRow = worksheet.Row(i)
                For j = StartColumn To colNum
                    excelRowData.Item(worksheet.Cells(ColumnRow, j).GetValue(Of String)) = worksheet.Cells(i, j)
                Next
                excelRows.Add(excelRowData)
            Next

            If excelRows.Count = 0 Then
                WriteLog("Error：データが0件です。1件以上のデータを設定してください。")
                Return Nothing
            End If

            Return MappingData(Of T)(excelRows)

        End Function

        ''' <summary>
        ''' 読み込んだデータをモデルへマップします。
        ''' </summary>
        ''' <param name="excelRows">すべてのデータ行</param>
        ''' <returns>モデルへマップしたデータの一覧</returns>
        Private Shared Function MappingData(Of T)(excelRows As List(Of Dictionary(Of String, ExcelRange))) As List(Of T)

            Dim modelInstances As New List(Of T)
            Dim modelType As Type = GetType(T)
            Dim i As Integer = 0

            For Each excelRow In excelRows

                i = i + 1
                WriteLog("Info：{0}件目のデータをモデルへマップします。", i)

                Dim modelInstance = Activator.CreateInstance(Of T)

                'Proccess each row in dictionary
                For Each key In excelRow.Keys

                    Dim modelProperty As Reflection.PropertyInfo = modelType.GetProperty(key, BindingFlags.IgnoreCase Or BindingFlags.Public Or BindingFlags.Instance)

                    If modelProperty IsNot Nothing Then
                        Dim modelPropertyType As Type = modelProperty.PropertyType
                        Dim cellData As Object = excelRow.Item(key).Value
                        Try
                            Dim cellDataTypeConvert As Object = Convert.ChangeType(cellData, modelPropertyType)
                            modelProperty.SetValue(modelInstance, cellDataTypeConvert, Nothing)
                        Catch ex As Exception
                            Dim dataType = If(cellData Is Nothing, "Null", cellData.GetType.ToString)
                            WriteLog("Warning：{0}件目の{1}のデータをモデルにマップするときにエラーが発生しました。（{2}型から{3}型へ変換） 詳細メッセージ：{4}", i, key, dataType, modelPropertyType, ex.Message)
                        End Try
                    End If
                Next
                modelInstances.Add(modelInstance)
            Next

            WriteLog("Info：{0}件のデータを読み込みました。処理終了", modelInstances.Count)
            Return modelInstances

        End Function


        ''' <summary>
        ''' ログを出力します。
        ''' </summary>
        ''' <param name="message">メッセージ</param>
        ''' <param name="args">置き換え文字</param>
        Private Shared Sub WriteLog(message As String, ParamArray args As Object())
            Debug.WriteLine(message, args)
        End Sub

    End Class

End Namespace