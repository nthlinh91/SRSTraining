Imports System.IO
Imports System.Reflection
Imports System.Web.Hosting
Imports OfficeOpenXml

Namespace Utilities

    Public NotInheritable Class ExcelUtility

        '�񖼋L�ڍs
        Private Const ColumnRow As Integer = 1
        '�f�[�^�J�n�s
        Private Const StartRow As Integer = 2
        '���ڊJ�n��
        Private Const StartColumn As Integer = 1

        Private Sub New()

        End Sub

        ''' <summary>
        ''' Excel�t�@�C���̃f�[�^�����f���Ƀ}�b�v���܂��B�w�肳�ꂽ�t�@�C�����Ȃ��ȂǁA�ǂݎ�菈�����ł��Ȃ��ꍇ�́A null ���Ԃ�܂��B
        ''' </summary>
        ''' <param name="fileName">�t�@�C���p�X</param>
        ''' <returns>���f���Ƀ}�b�v�����f�[�^�̈ꗗ</returns>
        Public Shared Function MapTo(Of T)(fileName As String) As List(Of T)

            Try
                Dim workbook As ExcelWorkbook = LoadWorkbook(fileName)
                If workbook Is Nothing Then
                    Return Nothing
                End If
                WriteLog("Info�F{0} �̃f�[�^��Ǎ��݂܂��B�����J�n", fileName)

                Dim worksheet As ExcelWorksheet = LoadWorksheet(workbook)
                If worksheet Is Nothing Then
                    Return Nothing
                End If

                '�f�[�^���o�^����Ă���ő�s
                Dim rowNum As Integer = worksheet.Dimension.End.Row
                '�f�[�^���o�^����Ă���ő��
                Dim colNum As Integer = worksheet.Dimension.End.Column
                If Not CheckSheetData(Of T)(worksheet, rowNum, colNum) Then
                    Return Nothing
                End If

                Return LoadData(Of T)(worksheet, rowNum, colNum)
            Catch ex As Exception
                WriteLog("Error�F{0}", ex.Message)
                Return Nothing
            End Try

        End Function

        ''' <summary>
        ''' �w�肳�ꂽ�t�@�C�������� Excel Workbook �����[�h���܂��B
        ''' </summary>
        ''' <param name="fileName">�ǂݎ��G�N�Z���� <see cref="ExcelWorkbook"/> �C���X�^���X</param>
        ''' <returns>�ǂݎ��ꂽ�G�N�Z���� <see cref="ExcelWorkbook"/> �C���X�^���X</returns>
        Private Shared Function LoadWorkbook(fileName As String) As ExcelWorkbook
            Try
                Return New ExcelPackage(New FileInfo(Path.Combine(HostingEnvironment.ApplicationPhysicalPath, fileName))).Workbook()
            Catch ex As FileNotFoundException
                WriteLog("Error�F�w�肳�ꂽ�t�@�C��({0}) �ƈ�v����t�@�C����������܂���B", fileName)
                Return Nothing
            End Try
        End Function

        ''' <summary>
        ''' �u�b�N����擪�̃V�[�g�����[�h���܂��B
        ''' </summary>
        ''' <param name="workbook">�V�[�g���擾���邽�߂̃u�b�N</param>
        ''' <returns>�u�b�N�̐擪�ɂ���V�[�g</returns>
        Private Shared Function LoadWorksheet(workbook As ExcelWorkbook) As ExcelWorksheet
            Dim worksheet As ExcelWorksheet = workbook.Worksheets.FirstOrDefault()

            If worksheet Is Nothing Then
                WriteLog("Error�FExcel�t�@�C���ɃV�[�g��������܂���B")
                Return Nothing
            End If

            Return worksheet
        End Function
        ''' <summary>
        ''' �V�[�g�ƃ��f���̒�`�ɕs��v�����邩�ǂ������`�F�b�N���܂��B
        ''' </summary>
        ''' <typeparam name="T">���f���̌^</typeparam>
        ''' <param name="worksheet">�ǂݎ��V�[�g</param>
        ''' <param name="rowNum">�f�[�^�̍s��</param>
        ''' <param name="colNum">�f�[�^�̃J������</param>
        ''' <returns>�`�F�b�N���ʁB�s��v���Ȃ��ꍇ�� true �A�����łȂ��ꍇ�� false</returns>
        Private Shared Function CheckSheetData(Of T)(worksheet As ExcelWorksheet, rowNum As Integer, colNum As Integer) As Boolean

            '1�s�ڂ̓��͂���̗�
            Dim usedColNum = worksheet.Cells(ColumnRow, StartColumn, ColumnRow, colNum).Count
            If usedColNum = 0 Then
                WriteLog("Error�F1�s�ڂ����ׂċ󔒂ł��B�񖼂�ݒ肵�Ă��������B")
                Return False
            End If
            If colNum > usedColNum Then
                WriteLog("Error: 1�s�ڂ̗񖼂�Null���ݒ肳��Ă��܂��B�i{0}���j", colNum - usedColNum)
                Return False
            End If

            Dim modelType As Type = GetType(T)
            For j = StartColumn To colNum
                Dim key = worksheet.Cells(ColumnRow, j).GetValue(Of String)()
                Dim modelProperty As Reflection.PropertyInfo = modelType.GetProperty(key, BindingFlags.IgnoreCase Or BindingFlags.Public Or BindingFlags.Instance)
                If modelProperty Is Nothing Then
                    WriteLog("Warning�F{0} �����f��({1}) �̃v���p�e�B�ɑ��݂��܂���B", key, modelType.Name)
                End If
            Next

            Return True

        End Function

        ''' <summary>
        ''' �V�[�g����f�[�^�����[�h���āA���f���Ƀ}�b�s���O���܂��B
        ''' </summary>
        ''' <typeparam name="T">���f���̌^</typeparam>
        ''' <param name="worksheet">�ǂݎ��V�[�g</param>
        ''' <param name="rowNum">�f�[�^�̍s��</param>
        ''' <param name="colNum">�f�[�^�̃J������</param>
        ''' <returns>�V�[�g����ǂݎ�����f�[�^���}�b�s���O���ꂽ���f���̈ꗗ</returns>
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
                WriteLog("Error�F�f�[�^��0���ł��B1���ȏ�̃f�[�^��ݒ肵�Ă��������B")
                Return Nothing
            End If

            Return MappingData(Of T)(excelRows)

        End Function

        ''' <summary>
        ''' �ǂݍ��񂾃f�[�^�����f���փ}�b�v���܂��B
        ''' </summary>
        ''' <param name="excelRows">���ׂẴf�[�^�s</param>
        ''' <returns>���f���փ}�b�v�����f�[�^�̈ꗗ</returns>
        Private Shared Function MappingData(Of T)(excelRows As List(Of Dictionary(Of String, ExcelRange))) As List(Of T)

            Dim modelInstances As New List(Of T)
            Dim modelType As Type = GetType(T)
            Dim i As Integer = 0

            For Each excelRow In excelRows

                i = i + 1
                WriteLog("Info�F{0}���ڂ̃f�[�^�����f���փ}�b�v���܂��B", i)

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
                            WriteLog("Warning�F{0}���ڂ�{1}�̃f�[�^�����f���Ƀ}�b�v����Ƃ��ɃG���[���������܂����B�i{2}�^����{3}�^�֕ϊ��j �ڍ׃��b�Z�[�W�F{4}", i, key, dataType, modelPropertyType, ex.Message)
                        End Try
                    End If
                Next
                modelInstances.Add(modelInstance)
            Next

            WriteLog("Info�F{0}���̃f�[�^��ǂݍ��݂܂����B�����I��", modelInstances.Count)
            Return modelInstances

        End Function


        ''' <summary>
        ''' ���O���o�͂��܂��B
        ''' </summary>
        ''' <param name="message">���b�Z�[�W</param>
        ''' <param name="args">�u����������</param>
        Private Shared Sub WriteLog(message As String, ParamArray args As Object())
            Debug.WriteLine(message, args)
        End Sub

    End Class

End Namespace