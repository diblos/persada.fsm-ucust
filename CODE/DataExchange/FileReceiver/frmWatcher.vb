Imports System.Windows.Forms
Imports System.Drawing
Imports System.Threading
Imports System.ServiceProcess
Imports System.IO

Public Class frmWatcher

#Region "Declarations"
    Public watchfolder() As FileSystemWatcher
    Public IntervalGuard As New System.Timers.Timer(500)

    Dim myIcon As New System.Drawing.Icon(vmmwatcher.My.Resources.Resources.earthwatcher, vmmwatcher.My.Resources.Resources.earthwatcher.Size)
    Dim dService As New DataExchangeClass.Data

#End Region
#Region "Form / Development"

    Public Delegate Sub AddItemsToListBoxDelegate( _
                          ByVal ToListBox As ListBox, _
                          ByVal AddText As String)

    Private Sub AddItemsToListBox(ByVal ToListBox As ListBox, _
                                 ByVal AddText As String)
        ToListBox.Items.Add(AddText)
        ToListBox.SelectedIndex = ToListBox.Items.Count - 1
    End Sub

    Public Sub lstMsgs(ByVal item As Object, ByVal source As Object)
        'If Me.ListBox1.Items.Count > 1000 Then
        '    ListBox1.Items.Clear()
        'End If
        'If source.ToString = Nothing Then
        '    Me.ListBox1.Items.Add(item)
        '    Me.ListBox1.SetSelected(ListBox1.Items.Count - 1, True)
        '    Me.ListBox1.SetSelected(ListBox1.Items.Count - 1, False)
        'Else
        '    If (source.invokerequired) Then
        source.invoke( _
                New AddItemsToListBoxDelegate(AddressOf AddItemsToListBox), _
                New Object() {source, item})

        '    End If
        'End If
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

    Private Function getArchivePath(ByVal FilePath As Object) As String
        Dim arcPath As String = String.Empty
        For x = 0 To NO_OF_APP - 1
            If PerfectPath(Path.GetDirectoryName(FilePath)) = PerfectPath(APP_STATUS(x).WatchFolder) Then
                arcPath = APP_STATUS(x).ArchivePath
            End If
        Next
        Return arcPath
    End Function

    Private Sub logchange(ByVal source As Object, ByVal e As  _
                    System.IO.FileSystemEventArgs)
        If e.ChangeType = IO.WatcherChangeTypes.Changed Then
            If DEV_MODE = True Then
                lstMsgs(Now.ToString("yyyy-MM-dd HH:mm:ss") & " File " & Path.GetFileName(e.FullPath) & _
                        " has been arrived", Me.ListBox1)
            End If
        End If
        'If e.ChangeType = IO.WatcherChangeTypes.Created Then
        '    If DEV_MODE = True Then
        '        lstMsgs(Now.ToString("yyyy-MM-dd HH:mm:ss") & " File " & e.FullPath & _
        '                " has been created" & vbCrLf, Me.ListBox1)
        '    End If
        'End If
        'If e.ChangeType = IO.WatcherChangeTypes.Deleted Then
        '    If DEV_MODE = True Then
        '        lstMsgs(Now.ToString("yyyy-MM-dd HH:mm:ss") & " File " & e.FullPath & _
        '                " has been deleted" & vbCrLf, Me.ListBox1)
        '    End If
        'End If

        'lstMsgs(source.ToString)
        'lstMsgs(e.FullPath)

        'setStatus(GetWorkingDir(e.FullPath))

        If (isExtensionRight(e.FullPath, ".txt")) Then ParseFileName(e.FullPath) 'readBatchFile(e.FullPath) 'TO BE AMENDED

    End Sub

    Public Sub logrename(ByVal source As Object, ByVal e As  _
                            System.IO.RenamedEventArgs)
        If DEV_MODE = True Then

            'If Me.InvokeRequired Then
            '    Me.Invoke(New MethodInvoker(AddressOf logrename))
            'Else
            '    lstMsgs("using textbox from another thread")
            'End If

            'lstResults.BeginInvoke((MethodInvoker)delegate() { lstResults.Columns.Clear(); }, null);

            lstMsgs(Now.ToString("yyyy-MM-dd HH:mm:ss") & " File" & e.OldName & _
                    " has been renamed to " & e.Name & vbCrLf, Me.ListBox1)
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
            'watchfolder(x).NotifyFilter = IO.NotifyFilters.DirectoryName
            'watchfolder(x).NotifyFilter = watchfolder(x).NotifyFilter Or _
            '                              IO.NotifyFilters.FileName
            'watchfolder(x).NotifyFilter = watchfolder(x).NotifyFilter Or _
            '                              IO.NotifyFilters.Attributes
            '-------------------------------------------------------------
            'watchfolder(x).NotifyFilter = (NotifyFilters.LastAccess Or _
            '             NotifyFilters.LastWrite Or _
            '             NotifyFilters.FileName Or _
            '             NotifyFilters.DirectoryName)
            watchfolder(x).NotifyFilter = IO.NotifyFilters.DirectoryName
            'watchfolder(x).NotifyFilter = watchfolder(x).NotifyFilter Or _
            '                              IO.NotifyFilters.LastWrite
            watchfolder(x).NotifyFilter = (NotifyFilters.LastAccess Or _
             NotifyFilters.LastWrite Or _
             NotifyFilters.FileName Or _
             NotifyFilters.DirectoryName)
            '-------------------------------------------------------------
            ' add the handler to each event

            'AddHandler watchfolder(x).Changed, AddressOf logchange
            AddHandler watchfolder(x).Created, AddressOf logchange
            'AddHandler watchfolder(x).Deleted, AddressOf logchange

            ' add the rename handler as the signature is different
            'AddHandler watchfolder(x).Renamed, AddressOf logrename

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
            lstMsgs("TIME CHECK: " & e.SignalTime, Me.ListBox1)
        End If
        For Each x In APP_STATUS
            If DEV_MODE = True Then
                lstMsgs(vbTab & x.EXEPath & " : " & x.Time, Me.ListBox1)
            End If
            'If DateDiff(DateInterval.Minute, e.SignalTime, x.Time) Then
            'End If

            If DateDiff(DateInterval.Minute, x.Time, e.SignalTime) >= INACTIVE_THRESHOLD_MINUTE Then
                If FileExists(x.EXEPath) = True Then
                    RestartApp(x)
                    lstMsgs(vbTab & e.SignalTime.ToString(DATE_FORMAT_DISPLAY) & " RE-SPAWNED: " & GetFileName(x.EXEPath), Me.ListBox1)
                Else
                    lstMsgs(vbTab & e.SignalTime.ToString(DATE_FORMAT_DISPLAY) & " RE-SPAWNED FAILED: " & GetFileName(x.EXEPath) & " isn't exists", Me.ListBox1)
                End If
            End If
        Next x
        IntervalGuard.Start()
    End Sub
#End Region
#Region "Start / Stop"
    Private Sub frmWatcher_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        StopWatching()
    End Sub

    Private Sub frmWatcher_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If ProcessesRunning(myName) > 1 Then
            lstMsgs("Me is running (one instance)", Me.ListBox1)
            End
        Else
            'INITIALIZE VALUES
            '--------------------------------------------------------
            Dim minToTray As New clsMinToTray(Me, myName & " is watching...", myIcon)
            InitAPP()

            'INIT TIMER
            '--------------------------------------------------------
            'AddHandler IntervalGuard.Elapsed, AddressOf CheckStatus
            'IntervalGuard.Interval = 60000 * INACTIVE_THRESHOLD_MINUTE
            'IntervalGuard.Start()

            'START WATCHER
            '--------------------------------------------------------
            StartWatching()

            'FORM ATTRIBUTES
            '--------------------------------------------------------
            Me.Icon = myIcon
            Me.Text = myName
            Me.Size = New Size(700, 400)
        End If
    End Sub
#End Region

#Region "Read Files"
    Private Sub readBatchFile(ByVal FILE_NAME As String)

        Dim TextLine As String = String.Empty
        Dim tmpTextLine As String = String.Empty

        If System.IO.File.Exists(FILE_NAME) = True Then

            Dim objReader As New System.IO.StreamReader(FILE_NAME)

            Dim startSTR As Integer
            Dim stopSTR As Integer
            Dim vmstime As String
            Dim seqfile As String
            Dim vmsname As String

            Dim vmslist As New List(Of VMS)

            Try
                Do While objReader.Peek() <> -1

                    tmpTextLine = objReader.ReadLine()
                    If tmpTextLine.Contains(LOG_STRING_CHECK) Then
                        'TextLine = TextLine & tmpTextLine & vbNewLine

                        'TextLine = TextLine & DateTime.ParseExact(tmpTextLine.Split(LOG_SEPARATOR)(0).Trim, LOG_TIME_FORMAT, System.Globalization.DateTimeFormatInfo.InvariantInfo, Globalization.DateTimeStyles.None).ToString & vbNewLine

                        'TextLine = TextLine & tmpTextLine.Split(LOG_SEPARATOR)(0).Trim & vbNewLine

                        'TextLine = TextLine & CDate(reassembleString("8/18/2015 1:35:25 PM")).ToString(LOG_TIME_FORMAT) & vbNewLine

                        'TextLine = TextLine & tmpTextLine.Split(LOG_SEPARATOR)(2) & vbNewLine

                        '===================================================================
                        vmstime = reassembleString(tmpTextLine.Split(LOG_SEPARATOR)(0).Trim)

                        startSTR = tmpTextLine.Split(LOG_SEPARATOR)(2).IndexOf(LOG_STRING_BEFORE_FILE)
                        stopSTR = tmpTextLine.Split(LOG_SEPARATOR)(2).IndexOf(LOG_STRING_AFTER_FILE)
                        seqfile = tmpTextLine.Split(LOG_SEPARATOR)(2).Substring(startSTR + 1, stopSTR - startSTR - 1)

                        startSTR = tmpTextLine.Split(LOG_SEPARATOR)(2).IndexOf(LOG_STRING_BEFORE_NAME)
                        stopSTR = tmpTextLine.Split(LOG_SEPARATOR)(2).Length - 1
                        vmsname = tmpTextLine.Split(LOG_SEPARATOR)(2).Substring(startSTR, stopSTR - startSTR).Replace(LOG_STRING_BEFORE_NAME, "").Trim

                        Dim k As New VMS
                        k.Timestamp = vmstime
                        k.VMSName = vmsname
                        k.SeqFile = seqfile

                        addVMS(vmslist, k)

                        'MsgBox(seqfile & "~" & vmsname)
                        '===================================================================


                    End If

                Loop

                If TextLine.Length > 0 Then MsgBox(TextLine)

            Catch ex As Exception
                lstMsgs(ex.Message, Me.ListBox1)
            Finally
                objReader.Dispose()
            End Try

            processVMS(vmslist)

        Else

            MsgBox("File Does Not Exist")

        End If
    End Sub

    Private Sub ParseFileName(ByVal FILENAME As String)
        Dim tmpName = System.IO.Path.GetFileNameWithoutExtension(FILENAME)
        Dim ServiceCode As String = tmpName.Split("_")(1)

        Select Case ServiceCode
            Case "RQCA"
                'ReadCAFile_OLD(FILENAME)
                ReadCAFile(FILENAME)

            Case "K1BTC"
                ReadK1File(FILENAME)
            Case Else
                lstMsgs("unrecognised file: " & FILENAME, Me.ListBox1)
        End Select

    End Sub

    Private Sub ReadCAFile(ByVal FILE_NAME As String)
        lstMsgs(FILE_NAME, Me.ListBox1)
        'Dim textReader As New StreamReader(FILE_NAME)
        'Dim csv = New CsvHelper.CsvReader(textReader)
        ''Dim records = csv.GetRecords(Of [MyClass])().ToList()
        'csv.Configuration.HasHeaderRecord = False
        'csv.Configuration.IgnoreBlankLines = True

        'While csv.Read
        '    Dim data As New DataExchangeClass.deprecating.ConsigmentApprovalRequest

        '    With data

        '        .Data_Header = csv.GetField(Of String)(0)
        '        .Message_Type = csv.GetField(Of String)(1)
        '        .Message_Mode = csv.GetField(Of String)(2)
        '        .Import_Indicator = csv.GetField(Of String)(3)
        '        .Custom_Form_number = csv.GetField(Of String)(4)
        '        .Custom_Form_number_2 = csv.GetField(Of String)(5)
        '        .FQC_Preassigned_control_number = csv.GetField(Of String)(6)
        '        .Registration_Date = csv.GetField(Of String)(7)
        '        .Resistration_Time = csv.GetField(Of String)(8)
        '        .Total_number_of_Item = csv.GetField(Of String)(9)
        '        .Commodity_Status = csv.GetField(Of String)(10)
        '        .Transaction_Type = csv.GetField(Of String)(11)
        '        .Exporter_name = csv.GetField(Of String)(12)
        '        .Exporter_Address = csv.GetField(Of String)(13)
        '        .Exporter_Address_2 = csv.GetField(Of String)(14)
        '        .Exporter_Address_3 = csv.GetField(Of String)(15)
        '        .Exporter_Address_4 = csv.GetField(Of String)(16)
        '        .Exporter_Address_5 = csv.GetField(Of String)(17)
        '        .Importer_Code = csv.GetField(Of String)(18)
        '        .Importer_Name = csv.GetField(Of String)(19)
        '        .Importer_Address = csv.GetField(Of String)(20)
        '        .Importer_Address_2 = csv.GetField(Of String)(21)
        '        .Importer_Address_3 = csv.GetField(Of String)(22)
        '        .Importer_Address_4 = csv.GetField(Of String)(23)
        '        .Importer_Address_5 = csv.GetField(Of String)(24)
        '        .Agent_Code = csv.GetField(Of String)(25)
        '        .Agent_Name = csv.GetField(Of String)(26)
        '        .Agent_Address = csv.GetField(Of String)(27)
        '        .Agent_Address_2 = csv.GetField(Of String)(28)
        '        .Agent_Address_3 = csv.GetField(Of String)(29)
        '        .Agent_Address_4 = csv.GetField(Of String)(30)
        '        .Agent_Address_5 = csv.GetField(Of String)(31)
        '        .Mode_of_Transport = csv.GetField(Of String)(32)
        '        .Date_of_Import = csv.GetField(Of String)(33)
        '        .Vessel_Registration = csv.GetField(Of String)(34)
        '        .Voyage_number = csv.GetField(Of String)(35)
        '        .Vessel_name = csv.GetField(Of String)(36)
        '        .Flight_number = csv.GetField(Of String)(37)
        '        .Flight_Date = csv.GetField(Of String)(38)
        '        .Vehicle_Lorry_number = csv.GetField(Of String)(39)
        '        .Trailer_number = csv.GetField(Of String)(40)
        '        .Place_of_Import = csv.GetField(Of String)(41)
        '        .Place_of_Loading = csv.GetField(Of String)(42)
        '        .Port_of_Transhipment = csv.GetField(Of String)(43)
        '        .Pay_To = csv.GetField(Of String)(44)
        '        .Insurance = csv.GetField(Of String)(45)
        '        .Insurance_2 = csv.GetField(Of String)(46)
        '        .Other_Charges = csv.GetField(Of String)(47)
        '        .Other_Charges_2 = csv.GetField(Of String)(48)
        '        .CIF = csv.GetField(Of String)(49)
        '        .CIF_2 = csv.GetField(Of String)(50)
        '        .FOB = csv.GetField(Of String)(51)
        '        .FOB_2 = csv.GetField(Of String)(52)
        '        .Freight = csv.GetField(Of String)(53)
        '        .Freight_2 = csv.GetField(Of String)(54)
        '        .Gross_Weight = csv.GetField(Of String)(55)
        '        .number_of_Packages = csv.GetField(Of String)(56)
        '        .Type_of_Packages = csv.GetField(Of String)(57)
        '        .Measurement = csv.GetField(Of String)(58)
        '        .Consignment_Note = csv.GetField(Of String)(59)
        '        .General_description_of_Goods = csv.GetField(Of String)(60)
        '        .Marks = csv.GetField(Of String)(61)
        '        .Manifest_Registration_number = csv.GetField(Of String)(62)
        '        .Import_Permit_number = csv.GetField(Of String)(63)
        '        .Import_Permit_number_2 = csv.GetField(Of String)(64)
        '        .Special_Treatement = csv.GetField(Of String)(65)
        '        .Total_Duty_Payable = csv.GetField(Of String)(66)
        '        .Declarant_IC_number = csv.GetField(Of String)(67)
        '        .Declarant_Name = csv.GetField(Of String)(68)
        '        .Declarant_Status = csv.GetField(Of String)(69)

        '        '====================================

        '        .Data_Header_B = csv.GetField(Of String)(70)
        '        .Custom_Form_number_B = csv.GetField(Of String)(71)
        '        .Custom_Form_number_B_2 = csv.GetField(Of String)(72)
        '        .Item_number = csv.GetField(Of String)(73)
        '        .Item_Description = csv.GetField(Of String)(74)
        '        .HS_code = csv.GetField(Of String)(75)
        '        .Declared_Quantity_1 = csv.GetField(Of String)(76)
        '        .Declared_unit_1 = csv.GetField(Of String)(77)
        '        .Unit_price = csv.GetField(Of String)(78)
        '        .Total_price = csv.GetField(Of String)(79)
        '        .Duty_Amount_B = csv.GetField(Of String)(80)
        '        .Duty_Amount_B_2 = csv.GetField(Of String)(81)
        '        .number_of_Packages_B = csv.GetField(Of String)(82)
        '        .Type_of_Packages_B = csv.GetField(Of String)(83)
        '        .Country_of_Origin = csv.GetField(Of String)(84)
        '        .Declared_Quantity_2 = csv.GetField(Of String)(85)
        '        .Declared_unit_2 = csv.GetField(Of String)(86)
        '        .Purpose_of_import = csv.GetField(Of String)(87)
        '        .Warehouse_Code = csv.GetField(Of String)(88)
        '        .Warehouse_Code_2 = csv.GetField(Of String)(89)
        '        .Warehouse_Name = csv.GetField(Of String)(90)
        '        .Warehouse_Address = csv.GetField(Of String)(91)
        '        .Warehouse_Address_2 = csv.GetField(Of String)(92)
        '        .Warehouse_Address_3 = csv.GetField(Of String)(93)
        '        .Warehouse_Address_4 = csv.GetField(Of String)(94)
        '        .Warehouse_Address_5 = csv.GetField(Of String)(95)
        '        .Remarks_and_Accident = csv.GetField(Of String)(96)
        '        .Exporter_Code = csv.GetField(Of String)(97)
        '        .Food_Code = csv.GetField(Of String)(98)
        '        .Food_Code_2 = csv.GetField(Of String)(99)
        '        .Brand = csv.GetField(Of String)(100)
        '        .Brand_2 = csv.GetField(Of String)(101)

        '        .Date_of_production = csv.GetField(Of String)(102)
        '        .Date_of_expire = csv.GetField(Of String)(103)

        '        .Treatment = csv.GetField(Of String)(104)

        '        .Manufacturer_code = csv.GetField(Of String)(105)
        '        .Manufacturer_name = csv.GetField(Of String)(106)
        '        .Manufacturer_address = csv.GetField(Of String)(107)
        '        .Manufacturer_address_2 = csv.GetField(Of String)(108)
        '        .Manufacturer_address_3 = csv.GetField(Of String)(109)
        '        .Manufacturer_address_4 = csv.GetField(Of String)(110)
        '        .Manufacturer_address_5 = csv.GetField(Of String)(111)

        '    End With

        '    dService.CAInsert(data)

        'End While

        'csv.Dispose()
        'textReader.Close()
        'textReader.Dispose()

        Try
            If Not MoveAfile(FILE_NAME, PerfectPath(getArchivePath(FILE_NAME)) & Path.GetFileName(FILE_NAME)) Then Throw New Exception("Move file error!")
            lstMsgs("Moved file " & Path.GetFileName(FILE_NAME) & " to " & getArchivePath(FILE_NAME), Me.ListBox1)
        Catch ex As Exception
            lstMsgs("Archiving file " & FILE_NAME & " failed!", Me.ListBox1)
        End Try

    End Sub

    Private Sub ReadCAFile_OLD(ByVal FILE_NAME As String)
        'lstMsgs(FILE_NAME, Me.ListBox1)
        Dim textReader As New StreamReader(FILE_NAME)
        Dim csv = New CsvHelper.CsvReader(textReader)
        'Dim records = csv.GetRecords(Of [MyClass])().ToList()
        csv.Configuration.HasHeaderRecord = False
        csv.Configuration.IgnoreBlankLines = True

        While csv.Read
            Dim data As New DataExchangeClass.deprecating.ConsigmentApprovalRequest

            With data

                .Data_Header = csv.GetField(Of String)(0)
                .Message_Type = csv.GetField(Of String)(1)
                .Message_Mode = csv.GetField(Of String)(2)
                .Import_Indicator = csv.GetField(Of String)(3)
                .Custom_Form_number = csv.GetField(Of String)(4)
                .Custom_Form_number_2 = csv.GetField(Of String)(5)
                .FQC_Preassigned_control_number = csv.GetField(Of String)(6)
                .Registration_Date = csv.GetField(Of String)(7)
                .Resistration_Time = csv.GetField(Of String)(8)
                .Total_number_of_Item = csv.GetField(Of String)(9)
                .Commodity_Status = csv.GetField(Of String)(10)
                .Transaction_Type = csv.GetField(Of String)(11)
                .Exporter_name = csv.GetField(Of String)(12)
                .Exporter_Address = csv.GetField(Of String)(13)
                .Exporter_Address_2 = csv.GetField(Of String)(14)
                .Exporter_Address_3 = csv.GetField(Of String)(15)
                .Exporter_Address_4 = csv.GetField(Of String)(16)
                .Exporter_Address_5 = csv.GetField(Of String)(17)
                .Importer_Code = csv.GetField(Of String)(18)
                .Importer_Name = csv.GetField(Of String)(19)
                .Importer_Address = csv.GetField(Of String)(20)
                .Importer_Address_2 = csv.GetField(Of String)(21)
                .Importer_Address_3 = csv.GetField(Of String)(22)
                .Importer_Address_4 = csv.GetField(Of String)(23)
                .Importer_Address_5 = csv.GetField(Of String)(24)
                .Agent_Code = csv.GetField(Of String)(25)
                .Agent_Name = csv.GetField(Of String)(26)
                .Agent_Address = csv.GetField(Of String)(27)
                .Agent_Address_2 = csv.GetField(Of String)(28)
                .Agent_Address_3 = csv.GetField(Of String)(29)
                .Agent_Address_4 = csv.GetField(Of String)(30)
                .Agent_Address_5 = csv.GetField(Of String)(31)
                .Mode_of_Transport = csv.GetField(Of String)(32)
                .Date_of_Import = csv.GetField(Of String)(33)
                .Vessel_Registration = csv.GetField(Of String)(34)
                .Voyage_number = csv.GetField(Of String)(35)
                .Vessel_name = csv.GetField(Of String)(36)
                .Flight_number = csv.GetField(Of String)(37)
                .Flight_Date = csv.GetField(Of String)(38)
                .Vehicle_Lorry_number = csv.GetField(Of String)(39)
                .Trailer_number = csv.GetField(Of String)(40)
                .Place_of_Import = csv.GetField(Of String)(41)
                .Place_of_Loading = csv.GetField(Of String)(42)
                .Port_of_Transhipment = csv.GetField(Of String)(43)
                .Pay_To = csv.GetField(Of String)(44)
                .Insurance = csv.GetField(Of String)(45)
                .Insurance_2 = csv.GetField(Of String)(46)
                .Other_Charges = csv.GetField(Of String)(47)
                .Other_Charges_2 = csv.GetField(Of String)(48)
                .CIF = csv.GetField(Of String)(49)
                .CIF_2 = csv.GetField(Of String)(50)
                .FOB = csv.GetField(Of String)(51)
                .FOB_2 = csv.GetField(Of String)(52)
                .Freight = csv.GetField(Of String)(53)
                .Freight_2 = csv.GetField(Of String)(54)
                .Gross_Weight = csv.GetField(Of String)(55)
                .number_of_Packages = csv.GetField(Of String)(56)
                .Type_of_Packages = csv.GetField(Of String)(57)
                .Measurement = csv.GetField(Of String)(58)
                .Consignment_Note = csv.GetField(Of String)(59)
                .General_description_of_Goods = csv.GetField(Of String)(60)
                .Marks = csv.GetField(Of String)(61)
                .Manifest_Registration_number = csv.GetField(Of String)(62)
                .Import_Permit_number = csv.GetField(Of String)(63)
                .Import_Permit_number_2 = csv.GetField(Of String)(64)
                .Special_Treatement = csv.GetField(Of String)(65)
                .Total_Duty_Payable = csv.GetField(Of String)(66)
                .Declarant_IC_number = csv.GetField(Of String)(67)
                .Declarant_Name = csv.GetField(Of String)(68)
                .Declarant_Status = csv.GetField(Of String)(69)

                '====================================

                .Data_Header_B = csv.GetField(Of String)(70)
                .Custom_Form_number_B = csv.GetField(Of String)(71)
                .Custom_Form_number_B_2 = csv.GetField(Of String)(72)
                .Item_number = csv.GetField(Of String)(73)
                .Item_Description = csv.GetField(Of String)(74)
                .HS_code = csv.GetField(Of String)(75)
                .Declared_Quantity_1 = csv.GetField(Of String)(76)
                .Declared_unit_1 = csv.GetField(Of String)(77)
                .Unit_price = csv.GetField(Of String)(78)
                .Total_price = csv.GetField(Of String)(79)
                .Duty_Amount_B = csv.GetField(Of String)(80)
                .Duty_Amount_B_2 = csv.GetField(Of String)(81)
                .number_of_Packages_B = csv.GetField(Of String)(82)
                .Type_of_Packages_B = csv.GetField(Of String)(83)
                .Country_of_Origin = csv.GetField(Of String)(84)
                .Declared_Quantity_2 = csv.GetField(Of String)(85)
                .Declared_unit_2 = csv.GetField(Of String)(86)
                .Purpose_of_import = csv.GetField(Of String)(87)
                .Warehouse_Code = csv.GetField(Of String)(88)
                .Warehouse_Code_2 = csv.GetField(Of String)(89)
                .Warehouse_Name = csv.GetField(Of String)(90)
                .Warehouse_Address = csv.GetField(Of String)(91)
                .Warehouse_Address_2 = csv.GetField(Of String)(92)
                .Warehouse_Address_3 = csv.GetField(Of String)(93)
                .Warehouse_Address_4 = csv.GetField(Of String)(94)
                .Warehouse_Address_5 = csv.GetField(Of String)(95)
                .Remarks_and_Accident = csv.GetField(Of String)(96)
                .Exporter_Code = csv.GetField(Of String)(97)
                .Food_Code = csv.GetField(Of String)(98)
                .Food_Code_2 = csv.GetField(Of String)(99)
                .Brand = csv.GetField(Of String)(100)
                .Brand_2 = csv.GetField(Of String)(101)

                .Date_of_production = csv.GetField(Of String)(102)
                .Date_of_expire = csv.GetField(Of String)(103)

                .Treatment = csv.GetField(Of String)(104)

                .Manufacturer_code = csv.GetField(Of String)(105)
                .Manufacturer_name = csv.GetField(Of String)(106)
                .Manufacturer_address = csv.GetField(Of String)(107)
                .Manufacturer_address_2 = csv.GetField(Of String)(108)
                .Manufacturer_address_3 = csv.GetField(Of String)(109)
                .Manufacturer_address_4 = csv.GetField(Of String)(110)
                .Manufacturer_address_5 = csv.GetField(Of String)(111)

            End With

            dService.CAInsert(data)

        End While

        csv.Dispose()
        textReader.Close()
        textReader.Dispose()

        Try
            If Not MoveAfile(FILE_NAME, PerfectPath(getArchivePath(FILE_NAME)) & Path.GetFileName(FILE_NAME)) Then Throw New Exception("Move file error!")
            lstMsgs("Moved file " & Path.GetFileName(FILE_NAME) & " to " & getArchivePath(FILE_NAME), Me.ListBox1)
        Catch ex As Exception
            lstMsgs("Archiving file " & FILE_NAME & " failed!", Me.ListBox1)
        End Try

    End Sub

    Private Sub ReadK1File(ByVal FILE_NAME As String)
        lstMsgs(FILE_NAME, Me.ListBox1)
    End Sub

    Private Function reassembleString(ByVal dateStr As String) As String

        If TIME_FORMAT_HACK = True Then
            Dim tmpTime As String() = dateStr.Split(" ")
            Dim tmp As String() = tmpTime(0).Split("/")
            Return tmp(2) & "-" & IIf(tmp(0).Length = 1, "0" & tmp(0), tmp(0)) & "-" & IIf(tmp(1).Length = 1, "0" & tmp(1), tmp(1)) & " " & tmpTime(1) & " " & tmpTime(2)
        Else
            Return dateStr
        End If

    End Function

    Private Sub addVMS(ByRef vmslist As List(Of VMS), ByVal vms As VMS)
        Dim result As Boolean = False
        For Each item As VMS In vmslist
            If (item.SeqFile = vms.SeqFile) And (item.VMSName = vms.VMSName) Then
                item.Timestamp = vms.Timestamp
                result = True
            ElseIf (item.SeqFile <> vms.SeqFile) And (item.VMSName = vms.VMSName) Then
                item.Timestamp = vms.Timestamp
                item.SeqFile = vms.SeqFile
                result = True
            End If
        Next

        If result = False Then vmslist.Add(vms)

    End Sub

    Private Sub processVMS(ByRef vmslist As List(Of VMS))
        For Each item As VMS In vmslist
            readSeqFile(item)
        Next
    End Sub

    Private Function readSeqFile(ByRef vmsObj As VMS) As Object
        Dim path As String = DATA_SEQUENCE_FOLDER
        If System.IO.File.Exists(path & vmsObj.SeqFile) = True Then
            Dim objReader As New System.IO.StreamReader(path & vmsObj.SeqFile)

            Dim TextLine As String = String.Empty
            Dim tmpTextLine As String
            Dim fname As String = String.Empty
            Dim vmspageList As New List(Of VMSPage)
            Try
                Do While objReader.Peek() <> -1

                    tmpTextLine = objReader.ReadLine()

                    'TextLine = TextLine & tmpTextLine & vbNewLine

                    '===================================================================
                    'TextLine = TextLine & tmpTextLine.Split(";")(1) & vbNewLine
                    fname = tmpTextLine.Split(";")(1)
                    Dim vmspageObj As New VMSPage(getImage(System.IO.Path.GetFileNameWithoutExtension(vmsObj.SeqFile) & "\" & fname))
                    vmspageList.Add(vmspageObj)
                    vmsObj.VMSPages = vmspageList
                    '===================================================================


                Loop

                If TextLine.Length > 0 Then MsgBox(TextLine)

            Catch ex As Exception
                lstMsgs(ex.Message, Me.ListBox1)
            Finally
                objReader.Dispose()
            End Try

        Else
            'do nothing
        End If
        Return False
    End Function

    Private Function getImage(ByVal filename As String) As Object 'massive coding will be placed here.
        Dim nPath As String = DATA_SEQUENCE_FOLDER
        Try
            If (isExtensionRight(filename, ".csc")) Then

                If System.IO.File.Exists(nPath & "\" & getCSCPath(filename)) = True Then
                    'lstMsgs("CSC PAGE YES", Me.ListBox1)
                    'Return "CSC PAGE YES"
                    Return Load64Bit(System.IO.Path.Combine(nPath, getCSCPath(filename)))
                Else
                    'lstMsgs("CSC PAGE NO", Me.ListBox1
                    Return Load64Bit(BLANK_BITMAP_PATH)
                    Return "CSC PAGE NO"
                End If

            ElseIf (isExtensionRight(filename, ".bmp")) Then

                Dim dPath As String = System.IO.Path.Combine(nPath, System.IO.Path.GetDirectoryName(filename))
                Dim dFiles As String() = GetFilenameList(PerfectPath(dPath), System.IO.Path.GetFileNameWithoutExtension(filename) & "*.bmp")
                If dFiles.Length <> 0 Then
                    Return Load64Bit(System.IO.Path.Combine(System.IO.Path.Combine(nPath, System.IO.Path.GetDirectoryName(filename)), dFiles(0)))
                Else
                    Return Load64Bit(BLANK_BITMAP_PATH)
                End If

            End If
        Catch ex As Exception
            'Return "INVALID!"
            Return BLANK_BITMAP_PATH
        End Try
    End Function

    Private Function getCSCPath(ByVal fname As String) As String
        Return System.IO.Path.Combine(System.IO.Path.GetDirectoryName(fname), System.IO.Path.Combine(System.IO.Path.GetFileNameWithoutExtension(fname), System.IO.Path.GetFileNameWithoutExtension(fname) & ".bmp"))
    End Function

    Private Function LoadBitmap(ByVal filePath As String) As Bitmap
        Try
            Return New Bitmap(filePath)
        Catch ex As Exception
            Return New Bitmap(BLANK_BITMAP_PATH)
        End Try
    End Function

    Private Function Load64Bit(ByVal filePath As String) As String
        Return ImageToBase64(New Bitmap(filePath), System.Drawing.Imaging.ImageFormat.Bmp)
    End Function

#End Region

End Class