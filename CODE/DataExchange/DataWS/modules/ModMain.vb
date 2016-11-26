Imports System.Web.HttpUtility
Imports Itac.Logger

Module ModMain
    Private _EventLogPath As String = "Logs"
    Private _ErrorLogPath As String = "Logs"
    Private errorLOG As New ErrorLogger

    Public ReadOnly Property GetDefaultLogPath() As String
        Get
            Return _EventLogPath
        End Get
    End Property

    Public Sub nEventLOG(ByVal path As Object, ByVal str As Object)
        Try
            If isValidPath(PerfectPath(GetWorkingDir("")) & PerfectPath(_EventLogPath)) = False Then CreatePath(PerfectPath(GetWorkingDir("")) & PerfectPath(_EventLogPath))
            errorLOG.WriteToLog(PerfectPath(_EventLogPath) & "Log_" & Now.ToString("yyyyMMdd_HH"), str)
        Catch ex As Exception

        End Try
    End Sub

    Public Sub nEventLOG(ByVal str As Object)
        Try
            If isValidPath(PerfectPath(GetWorkingDir("")) & PerfectPath(_EventLogPath)) = False Then CreatePath(PerfectPath(GetWorkingDir("")) & PerfectPath(_EventLogPath))
            errorLOG.WriteToLog(PerfectPath(_EventLogPath) & "Log_" & Now.ToString("yyyyMMdd_HH"), str)
        Catch ex As Exception

        End Try
    End Sub

    Public Sub nErrorLOG(ByVal str As Object)
        Try
            If isValidPath(PerfectPath(GetWorkingDir("")) & PerfectPath(_ErrorLogPath)) = False Then CreatePath(PerfectPath(GetWorkingDir("")) & PerfectPath(_ErrorLogPath))
            errorLOG.WriteToLog(PerfectPath(_ErrorLogPath) & "Log_" & Now.ToString("yyyyMMdd_HH"), str)
        Catch ex As Exception

        End Try
    End Sub

    Public Sub nErrorLOG(ByVal nMsg, ByVal nTrace, ByVal nTitle)
        Try
            errorLOG.WriteToErrorLog(nMsg, nTrace, nTitle, ErrorLogger.LOG_TYPE.ERROR_LOG)
        Catch ex As Exception

        End Try
    End Sub

    Public Function isValidPath(ByVal filepath As String) As Boolean

        If System.IO.File.Exists(filepath) = True Then
            Return True
        ElseIf (IO.Directory.Exists(filepath) = True) Then
            Return True
        Else
            Return False
        End If

    End Function

    Public Function PerfectPath(ByVal sPath As String)
        If Right(sPath, 1) <> "\" Then
            sPath = sPath & "\"
        End If
        Return sPath
    End Function

    Public Sub CreatePath(ByVal path As String)
        If Not System.IO.Directory.Exists(path) Then
            System.IO.Directory.CreateDirectory(path)
        End If
    End Sub

    Public Function GetWorkingDir(ByVal fullpath As String) As String
        Dim tmp = fullpath.Split("\")
        If tmp.Length = 1 Then
            Return System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly.Location)
        Else
            Return System.IO.Path.GetDirectoryName(fullpath)
        End If
    End Function

End Module
