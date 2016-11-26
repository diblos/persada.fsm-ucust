Imports System.Configuration.ConfigurationManager
Imports System.Drawing
Imports System.IO

Module common
    Public Const DEVELOPMENT_MODE_KEY As String = "DEVELOPMENT.MODE"
    Public Const TIME_FORMAT_HACK_KEY As String = "TIME.FORMAT.HACK"
    Public Const DATE_FORMAT_DISPLAY As String = "yyyy-MM-dd HH:mm:ss"
    Public Const SEPARATOR As String = "|"
    Public myName As String = System.Reflection.Assembly.GetExecutingAssembly.GetName.Name
    'Public RESTART_APPLICATION_PATH As String = "D:\workspace\_codes\POLRI\_SERVICE\AID_VDS\Itac.RestartApplication\bin\Debug\Itac.RestartApplication.exe"
    Public DEV_MODE As Boolean = False
    Public TIME_FORMAT_HACK As Boolean = False
    Public NO_OF_APP As Integer = AppSettings("NO.OF.APP")
    Public INACTIVE_THRESHOLD_MINUTE As Integer = 1
    Public APP_STATUS() As ApplicationObject

    Public LOG_STRING_CHECK As String = ".seq"
    Public LOG_SEPARATOR As String = "-->"
    Public LOG_TIME_FORMAT As String = "M/d/yyyy H:mm:ss tt"
    Public LOG_STRING_BEFORE_FILE As String = "("
    Public LOG_STRING_AFTER_FILE As String = ")"
    Public LOG_STRING_BEFORE_NAME As String = "to display at"
    Public LOG_STRING_AFTER_NAME As String = "."

    Public DATA_SEQUENCE_FOLDER As String = "C:\Users\lenovo\Desktop\AKLEH\VMS_Server\Data\Sequence\"
    Public BLANK_BITMAP_PATH As String = "C:\Users\lenovo\Desktop\AKLEH\VMS_Server\System\Blank.bmp"

    'Public db As Microsoft.Practices.EnterpriseLibrary.Data.Database = Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase("DB")

    Public Sub InitAPP()
        Dim tmpStr As String
        ReDim APP_STATUS(NO_OF_APP - 1)

        If Not AppSettings(DEVELOPMENT_MODE_KEY) = "" Then
            Select Case UCase(AppSettings(DEVELOPMENT_MODE_KEY))
                Case "ON"
                    DEV_MODE = True
                Case "OFF"
                    DEV_MODE = False
                Case Else
                    DEV_MODE = False
            End Select
        End If

        If Not AppSettings(TIME_FORMAT_HACK_KEY) = "" Then
            If AppSettings(TIME_FORMAT_HACK_KEY).ToUpper = "ON" Then
                TIME_FORMAT_HACK = True
            Else
                TIME_FORMAT_HACK = False
            End If
        End If

        For x = 0 To NO_OF_APP - 1

            tmpStr = AppSettings("APP." & x + 1)

            APP_STATUS(x) = New ApplicationObject

            'SET APPLICATION OBJECT VALUES
            '=============================
            APP_STATUS(x).EXEPath = Nothing
            APP_STATUS(x).Time = Now
            APP_STATUS(x).WatchFolder = tmpStr
            tmpStr = AppSettings("ARCHIVE." & x + 1)
            APP_STATUS(x).ArchivePath = tmpStr
            APP_STATUS(x).Status = AppStatus.Active

        Next
    End Sub

    Public Function ImageToBase64(ByVal image As System.Drawing.Image, ByVal format As System.Drawing.Imaging.ImageFormat) As String
        Using ms As New MemoryStream()
            ' Convert Image to byte[]
            image.Save(ms, format)
            Dim imageBytes As Byte() = ms.ToArray()

            ' Convert byte[] to Base64 String
            Dim base64String As String = Convert.ToBase64String(imageBytes)
            Return base64String
        End Using
    End Function

#Region "Respawn"
    'Same function as in frmTest
    Public Function RestartApp(ByVal App As ApplicationObject)
        Dim path As String = App.EXEPath
        Dim processName As String = RemoveFileExt(GetFileName(path))
        Dim result As Boolean = False
        Try
            For Each proc As Process In Process.GetProcesses
                'Kill process by  ProcessID
                If (proc.ProcessName = processName) Then
                    proc.Kill()
                End If
            Next

            Process.Start(path)
            result = True
        Catch ex As Exception
            WriteEventLog(EventLogEntryType.Error, "RestartApp: " & ex.ToString, myName, myName)
            result = False
            'Finally
        End Try
        RestartApp = result
    End Function
#End Region
End Module

Public Class ApplicationObject
    Private _Status As AppStatus
    Private _Time As DateTime
    Private _WatchFolder As String
    Private _Path As String
    Private _ArchivePath As String

    Public Property Status() As AppStatus
        Get
            Return Me._Status
        End Get
        Set(ByVal value As AppStatus)
            Me._Status = value
        End Set
    End Property

    Public Property Time() As DateTime
        Get
            Return Me._Time
        End Get
        Set(ByVal value As DateTime)
            Me._Time = value
        End Set
    End Property

    Public Property WatchFolder() As String
        Get
            Return Me._WatchFolder
        End Get
        Set(ByVal value As String)
            Me._WatchFolder = value
        End Set
    End Property

    Public Property ArchivePath() As String
        Get
            Return Me._ArchivePath
        End Get
        Set(ByVal value As String)
            Me._ArchivePath = value
        End Set
    End Property

    Public Property EXEPath() As String
        Get
            Return Me._Path
        End Get
        Set(ByVal value As String)
            Me._Path = value
        End Set
    End Property

End Class

Public Enum AppStatus
    Active = 1
    InActive = 2
End Enum

Public Class VMS
    Private _timestamp As String
    Public Property Timestamp() As String
        Get
            Return Me._timestamp
        End Get
        Set(ByVal value As String)
            Me._timestamp = value
        End Set
    End Property

    Private _vmsname As String
    Public Property VMSName() As String
        Get
            Return Me._vmsname
        End Get
        Set(ByVal value As String)
            Me._vmsname = value
        End Set
    End Property

    Private _seqfile As String
    Public Property SeqFile() As String
        Get
            Return Me._seqfile
        End Get
        Set(ByVal value As String)
            Me._seqfile = value
        End Set
    End Property

    Private _vmspages As List(Of VMSPage)
    Public Property VMSPages() As List(Of VMSPage)
        Get
            Return Me._vmspages
        End Get
        Set(ByVal value As List(Of VMSPage))
            Me._vmspages = value
        End Set
    End Property

End Class

Public Class VMSPage
    Private _page As Object
    Public Property Page() As Object
        Get
            Return Me._page
        End Get
        Set(ByVal value As Object)
            Me._page = value
        End Set
    End Property

    Public Sub New(ByVal vmspage As Object)
        Me._page = vmspage
    End Sub
End Class