Imports System.Xml.Serialization
Imports System.Xml
Imports System.Text
Imports System.IO

Public Class Common

    Public Const DEFAULT_SQL_DATA_FORMAT As String = "yyyy-MM-dd HH:mm:ss"

    Public Enum Functions
        JobSender
        JobReceiver
    End Enum

    Public Enum ResponseStatus
        AUTHOTHIZE_FAIL = 100
        VALIDATE_DATA_FAIL = 101
        PROCESS_DATA_FAIL = 102
        SUCCESS = 103
    End Enum

    Public Enum ErrorCode
        EC00 = 0
        EV00 = 100
        ET00 = 200
    End Enum

    Public Enum JobStatus
        [NEW]
        [PROGRESS]
        [COMPLETE]
        [PENDING]
    End Enum

    Public Enum LoggerType
        [Event]
        [Error]
    End Enum

    Public Sub log(ByVal Path As String, ByVal message As String)
        Try
            Dim nLog As New Itac.Logger.ErrorLogger
            If isValidPath(PerfectPath(Path)) = False Then CreatePath(PerfectPath(Path))
            nLog.WriteToLog(PerfectPath(Path) & "Log_" & Now.ToString("yyyyMMdd_HH") & ".txt", message)
            'nLog.WriteToLog(System.IO.Path.Combine(Path, "Log.txt"), message)
        Catch ex As Exception
            'do nothing
        End Try
    End Sub

    Public Shared Function getFreshTrxTable() As DataTable
        Dim tmpTable As New DataTable
        Dim col As DataColumn

        col = New DataColumn()
        col.DataType = System.Type.GetType("System.Int64")
        col.ColumnName = "JOBLIST"
        col.DefaultValue = 0
        col.Unique = False
        tmpTable.Columns.Add(col)

        col = New DataColumn()
        col.DataType = System.Type.GetType("System.Int16")
        col.ColumnName = "SEQ"
        col.DefaultValue = 0
        col.Unique = False
        tmpTable.Columns.Add(col)

        col = New DataColumn()
        col.DataType = System.Type.GetType("System.String")
        col.ColumnName = "SENT"
        col.DefaultValue = ""
        col.Unique = False
        tmpTable.Columns.Add(col)

        col = New DataColumn()
        col.DataType = System.Type.GetType("System.String")
        col.ColumnName = "RECEIVED"
        col.DefaultValue = ""
        col.Unique = False
        tmpTable.Columns.Add(col)

        Return tmpTable

    End Function

    Public Shared Function DeserializeIT(ByVal Xml As String, ByVal ObjType As System.Type) As Object

        Dim ser As XmlSerializer
        ser = New XmlSerializer(ObjType)
        Dim stringReader As IO.StringReader
        stringReader = New IO.StringReader(Xml)
        Dim xmlReader As XmlTextReader
        xmlReader = New XmlTextReader(stringReader)
        Dim obj As Object
        obj = ser.Deserialize(xmlReader)
        xmlReader.Close()
        stringReader.Close()
        Return obj

    End Function

    Public Shared Function SerializeIT(ByVal nObject As Object) As String
        Dim builder As New StringBuilder()
        Try
            Dim s As New XmlSerializer(nObject.[GetType]())
            Using writer As New StringWriter(builder)
                s.Serialize(writer, nObject)
            End Using
            Return builder.ToString
        Catch ex As Exception
            Throw ex
        Finally
            builder = Nothing
        End Try
    End Function

    Public Shared Function IsList(value As Object) As Boolean
        Return TypeOf value Is IList OrElse IsGenericList(value)
    End Function

    Private Shared Function IsGenericList(value As Object) As Boolean
        Dim type As Object = value.[GetType]()
        Return type.IsGenericType AndAlso GetType(List(Of )) = type.GetGenericTypeDefinition()
    End Function

    Public Shared Function Compare(ByVal str1 As String, ByVal str2 As String) As Double
        Dim count As Integer = If(str1.Length > str2.Length, str1.Length, str2.Length)
        Dim hits As Integer = 0
        Dim i, j As Integer : i = 0 : j = 0
        For i = 0 To str1.Length - 1
            If str1.Chars(i) = " " Then i += 1 : j = str2.IndexOf(" "c, j) + 1 : hits += 1
            While j < str2.Length AndAlso str2.Chars(j) <> " "c
                If str1.Chars(i) = str2.Chars(j) Then
                    hits += 1
                    j += 1
                    Exit While
                Else
                    j += 1
                End If
            End While
            If Not (j < str2.Length AndAlso str2.Chars(j) <> " "c) Then
                j -= 1
            End If
        Next
        Return Math.Round((hits / count), 2)
    End Function

    Public Shared Function SelectDistinct(ByVal SourceTable As DataTable, ByVal ParamArray FieldNames() As String) As DataTable
        Dim lastValues() As Object
        Dim newTable As DataTable

        If FieldNames Is Nothing OrElse FieldNames.Length = 0 Then
            Throw New ArgumentNullException("FieldNames")
        End If

        lastValues = New Object(FieldNames.Length - 1) {}
        newTable = New DataTable

        For Each field As String In FieldNames
            newTable.Columns.Add(field, SourceTable.Columns(field).DataType)
        Next

        For Each Row As DataRow In SourceTable.Select("", String.Join(", ", FieldNames))
            If Not fieldValuesAreEqual(lastValues, Row, FieldNames) Then
                newTable.Rows.Add(createRowClone(Row, newTable.NewRow(), FieldNames))

                setLastValues(lastValues, Row, FieldNames)
            End If
        Next

        Return newTable
    End Function

    Private Shared Function fieldValuesAreEqual(ByVal lastValues() As Object, ByVal currentRow As DataRow, ByVal fieldNames() As String) As Boolean
        Dim areEqual As Boolean = True

        For i As Integer = 0 To fieldNames.Length - 1
            If lastValues(i) Is Nothing OrElse Not lastValues(i).Equals(currentRow(fieldNames(i))) Then
                areEqual = False
                Exit For
            End If
        Next

        Return areEqual
    End Function

    Private Shared Function createRowClone(ByVal sourceRow As DataRow, ByVal newRow As DataRow, ByVal fieldNames() As String) As DataRow
        For Each field As String In fieldNames
            newRow(field) = sourceRow(field)
        Next

        Return newRow
    End Function

    Private Shared Sub setLastValues(ByVal lastValues() As Object, ByVal sourceRow As DataRow, ByVal fieldNames() As String)
        For i As Integer = 0 To fieldNames.Length - 1
            lastValues(i) = sourceRow(fieldNames(i))
        Next
    End Sub

    Public Shared Function CleanSerializedData(ByVal XMLStr As String) As String
        Dim xml_read As New XmlTextReader(New System.IO.StringReader(XMLStr))
        Dim xml_doc As New XmlDocument
        Try
            xml_doc.Load(xml_read)
            Dim Node As XmlNode = xml_doc.DocumentElement

            If (xml_doc.FirstChild.NodeType = XmlNodeType.XmlDeclaration) Then xml_doc.RemoveChild(xml_doc.FirstChild)

            Node.Attributes.RemoveAll()

            Select Case UCase(Node.Name)
                Case "ALARMDATA"
                    Return xml_doc.InnerXml.Replace("AlarmData", "alarmData")
                Case Else
                    Return xml_doc.InnerXml
            End Select

        Catch ex As Exception
            'RaiseEvent OnError(Now, New Exception("CleanSerializedData:" & ex.Message))
            Return XMLStr
        End Try
    End Function

    Public Shared Function ConsignmentApproval2SMKCForm(ByVal CAR As deprecating.ConsigmentApprovalRequest) As deprecating.SMKCForm_DTO
        Return New deprecating.SMKCForm_DTO
    End Function

End Class
