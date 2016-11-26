Imports System.Diagnostics
'Bakhtiar write event log
Module EventLogger
    Public Function WriteEventLog(ByVal EventType As EventLogEntryType, ByVal Entry As String, _
                                    Optional ByVal AppName As String = "VB.NET Application", _
                                    Optional ByVal LogName As String = "Application") As Boolean
        Dim objEventLog As New EventLog
        Try
            'Register the App as an Event Source
            If Not EventLog.SourceExists(AppName) Then
                EventLog.CreateEventSource(AppName, LogName)
            End If

            objEventLog.Source = AppName

            'WriteEntry is overloaded; this is one
            'of 10 ways to call it
            objEventLog.WriteEntry(Entry, EventType)
            Return True
        Catch Ex As Exception
            Return False
        End Try
    End Function

    Public Sub DeleteEventLog(ByVal LogName As String)
        If EventLog.Exists(LogName) Then
            EventLog.Delete(LogName)
        End If
    End Sub
End Module
