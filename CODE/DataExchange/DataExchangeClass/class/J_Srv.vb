Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Text
Imports System.Data.Common
Imports DataExchangeClass.Common

Public Class J_Srv

    Dim _selectedDate As Date
    Dim _selectedJob As Integer
    Dim _gotDate As Boolean

    Dim SMSJOB As DataTable
    Dim _db As Database

    Public ReadOnly Property GetSMSCode(ByVal Command As String, Optional ByVal Sequence As Integer = 1) As String
        Get
            Dim foundRows() As DataRow
            If Command = "UNKNOWN" Or Command = "" Then
                Return String.Empty
            Else
                foundRows = SMSJOB.Select("COMMAND_HANDLER='" & Command & "' AND SEQUENCE=" & Sequence & "")
                If foundRows.Length > 0 Then
                    Return foundRows(0)("SMS_CODE")
                Else
                    Return String.Empty
                End If
            End If
        End Get
    End Property

    Public ReadOnly Property GetCommandHandlerList() As DataTable
        Get

            Return SelectDistinct(SMSJOB, "COMMAND_HANDLER")

        End Get
    End Property

    Public ReadOnly Property GetTable() As DataTable
        Get
            Try

                Return SMSJOB.Copy

            Catch ex As Exception
                Return New DataTable
            End Try
        End Get
    End Property

    Sub New(ByVal Connection As Database)

        _selectedDate = Nothing
        _selectedJob = Nothing
        _gotDate = False
        _db = Connection
        GetData()

    End Sub

    Sub New(ByVal Connection As Database, ByVal SelectedDate As Date)

        _selectedDate = SelectedDate
        _selectedJob = Nothing
        _gotDate = True
        _db = Connection
        GetData()

    End Sub

    Sub New(ByVal Connection As Database, ByVal SelectedJob As Integer)

        _selectedDate = Nothing
        _selectedJob = SelectedJob
        _gotDate = False
        _db = Connection
        GetData()

    End Sub

    Sub New(ByVal Connection As Database, ByVal SelectedDate As Date, ByVal SelectedJob As Integer)

        _selectedDate = SelectedDate
        _selectedJob = SelectedJob
        _gotDate = True
        _db = Connection
        GetData()

    End Sub

    Sub Dispose()
        SMSJOB.Dispose()
        Database.ClearParameterCache()
        GC.Collect()
    End Sub

    Protected Sub GetData()

        Dim strSQL As String = _
        " SELECT * FROM SMS_JOBLIST "

        Dim command As System.Data.Common.DbCommand = _db.GetSqlStringCommand(strSQL)

        Try
            SMSJOB = _db.ExecuteDataSet(command).Tables(0).Copy

        Catch ex As Exception
            '
        Finally
            strSQL = Nothing
            command.Dispose()
        End Try

    End Sub

End Class