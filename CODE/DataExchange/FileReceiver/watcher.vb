Imports System.Threading
Imports System.ServiceProcess
Imports System.IO

Public Class watcher
#Region "Declaration"
    Public watchfolder() As FileSystemWatcher
    Public IntervalGuard As New System.Timers.Timer(500)
#End Region
#Region "Log / Development"
    Public Sub lstMsgs(ByVal item As Object)
        WriteEventLog(EventLogEntryType.Information, "SetStatus: " & item, myName, myName)
    End Sub
#End Region
#Region "Functions"
    Private Sub setStatus(ByVal obj As Object)
        For x = 0 To NO_OF_APP - 1
            If PerfectPath(obj) = PerfectPath(APP_STATUS(x).WatchFolder) Then
                APP_STATUS(x).Status = AppStatus.Active
                APP_STATUS(x).Time = Now
            End If
        Next
    End Sub

    Private Sub logchange(ByVal source As Object, ByVal e As  _
                    System.IO.FileSystemEventArgs)
        If e.ChangeType = IO.WatcherChangeTypes.Changed Then
            If DEV_MODE = True Then
                lstMsgs(Now.ToString("yyyy-MM-dd HH:mm:ss") & " File " & e.FullPath & _
                        " has been modified" & vbCrLf)
            End If
        End If
        If e.ChangeType = IO.WatcherChangeTypes.Created Then
            If DEV_MODE = True Then
                lstMsgs(Now.ToString("yyyy-MM-dd HH:mm:ss") & " File " & e.FullPath & _
                        " has been created" & vbCrLf)
            End If
        End If
        If e.ChangeType = IO.WatcherChangeTypes.Deleted Then
            If DEV_MODE = True Then
                lstMsgs(Now.ToString("yyyy-MM-dd HH:mm:ss") & " File " & e.FullPath & _
                        " has been deleted" & vbCrLf)
            End If
        End If

        'lstMsgs(source.ToString)
        'lstMsgs(e.FullPath)
        setStatus(GetWorkingDir(e.FullPath))

    End Sub

    Public Sub logrename(ByVal source As Object, ByVal e As  _
                            System.IO.RenamedEventArgs)
        If DEV_MODE = True Then
            lstMsgs(Now.ToString("yyyy-MM-dd HH:mm:ss") & " File" & e.OldName & _
                    " has been renamed to " & e.Name & vbCrLf)
        End If
    End Sub

    Private Sub StartWatching()
        ReDim watchfolder(NO_OF_APP - 1)

        For x = 0 To UBound(watchfolder)
            watchfolder(x) = New System.IO.FileSystemWatcher()

            'this is the path we want to monitor
            watchfolder(x).Path = APP_STATUS(x).WatchFolder

            'Add a list of Filter we want to specify

            'make sure you use OR for each Filter as we need to

            'all of those
            watchfolder(x).NotifyFilter = IO.NotifyFilters.DirectoryName
            watchfolder(x).NotifyFilter = watchfolder(x).NotifyFilter Or _
                                          IO.NotifyFilters.FileName
            watchfolder(x).NotifyFilter = watchfolder(x).NotifyFilter Or _
                                          IO.NotifyFilters.Attributes

            ' add the handler to each event

            AddHandler watchfolder(x).Changed, AddressOf logchange
            AddHandler watchfolder(x).Created, AddressOf logchange
            AddHandler watchfolder(x).Deleted, AddressOf logchange

            ' add the rename handler as the signature is different
            AddHandler watchfolder(x).Renamed, AddressOf logrename

            'Set this property to true to start watching

            watchfolder(x).EnableRaisingEvents = True
        Next

        'Button1.Text = "Stop Watch"
        'btn_startwatch.Enabled = False
        'btn_stop.Enabled = True

        'End of code for btn_start_click
    End Sub

    Private Sub StopWatching()
        Try
            For x = 0 To UBound(watchfolder)
                watchfolder(x).EnableRaisingEvents = False
            Next
            'Button1.Text = "Start Watch"
            'btn_startwatch.Enabled = True
            'btn_stop.Enabled = False
        Catch ex As Exception
            Exit Sub
        End Try
    End Sub

    Public Function ProcessesRunning(ByVal ProcessName As String) As Integer
        '
        Try
            Return Process.GetProcessesByName(ProcessName).GetUpperBound(0) + 1
        Catch
            Return 0
        End Try

    End Function

    Private Sub CheckStatus(ByVal sender As Object, ByVal e As System.Timers.ElapsedEventArgs)
        IntervalGuard.Stop()
        If DEV_MODE = True Then
            lstMsgs("TIME CHECK: " & e.SignalTime)
        End If
        For Each x In APP_STATUS
            If DEV_MODE = True Then
                lstMsgs(vbTab & x.EXEPath & " : " & x.Time)
            End If
            'If DateDiff(DateInterval.Minute, e.SignalTime, x.Time) Then
            'End If

            If DateDiff(DateInterval.Minute, x.Time, e.SignalTime) >= INACTIVE_THRESHOLD_MINUTE Then
                RestartApp(x)
                lstMsgs(vbTab & " RESTART: " & GetFileName(x.EXEPath))
            End If

        Next x
        IntervalGuard.Start()
    End Sub
#End Region
#Region "Start / Stop"
    Protected Overrides Sub OnStart(ByVal args() As String)
        ' Add code here to start your service. This method should set things
        ' in motion so your service can do its work.
        'INITIALIZE VALUES
        '--------------------------------------------------------
        InitAPP()

        'INIT TIMER
        '--------------------------------------------------------
        AddHandler IntervalGuard.Elapsed, AddressOf CheckStatus
        IntervalGuard.Interval = 1000 * INACTIVE_THRESHOLD_MINUTE
        IntervalGuard.Start()

        'START WATCHER
        '--------------------------------------------------------
        StartWatching()
    End Sub

    Protected Overrides Sub OnStop()
        ' Add code here to perform any tear-down necessary to stop your service.
        StopWatching()
    End Sub
#End Region
End Class
