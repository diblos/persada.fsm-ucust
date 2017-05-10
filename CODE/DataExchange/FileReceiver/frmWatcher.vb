Imports System.Windows.Forms
Imports System.Drawing
Imports System.Threading
Imports System.ServiceProcess
Imports System.IO

Public Class frmWatcher

#Region "Declarations"
    Public watchfolder() As FileSystemWatcher
    Public IntervalGuard As New System.Timers.Timer(500)

    Public Const LOGTIMEFORMAT As String = "yyyy-MM-dd HH:mm:ss"
    Public Const INDENT As String = "  "

    Dim myIcon As New System.Drawing.Icon(vmmwatcher.My.Resources.Resources.earthwatcher, vmmwatcher.My.Resources.Resources.earthwatcher.Size)
    Dim WithEvents dService As New DataExchangeClass.Data

#End Region
#Region "Form / Development"

    Public Delegate Sub AddItemsToListBoxDelegate( _
                          ByVal ToListBox As ListBox, _
                          ByVal AddText As String)

    Private Sub AddItemsToListBox(ByVal ToListBox As ListBox, _
                                 ByVal AddText As String)
        If ToListBox.Items.Count > 1000 Then
            ToListBox.Items.Clear()
        End If
        ToListBox.Items.Add(AddText)
        ToListBox.SetSelected(ToListBox.Items.Count - 1, True)
        ToListBox.SetSelected(ToListBox.Items.Count - 1, False)

    End Sub

    Public Sub lstMsgs(ByVal item As Object)
        If Me.ListBox1.Items.Count > 1000 Then
            ListBox1.Items.Clear()
        End If
        If Me.ListBox1.ToString = Nothing Then
            Me.ListBox1.Items.Add(item)
            Me.ListBox1.SetSelected(ListBox1.Items.Count - 1, True)
            Me.ListBox1.SetSelected(ListBox1.Items.Count - 1, False)
        Else
            If (Me.ListBox1.InvokeRequired) Then
                Me.ListBox1.Invoke( _
                        New AddItemsToListBoxDelegate(AddressOf AddItemsToListBox), _
                        New Object() {Me.ListBox1, item})

            End If
        End If
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
                        " has been arrived")
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
            lstMsgs("TIME CHECK: " & e.SignalTime)
        End If
        For Each x In APP_STATUS
            If DEV_MODE = True Then
                lstMsgs(vbTab & x.EXEPath & " : " & x.Time)
            End If
            'If DateDiff(DateInterval.Minute, e.SignalTime, x.Time) Then
            'End If

            If DateDiff(DateInterval.Minute, x.Time, e.SignalTime) >= INACTIVE_THRESHOLD_MINUTE Then
                If FileExists(x.EXEPath) = True Then
                    RestartApp(x)
                    lstMsgs(vbTab & e.SignalTime.ToString(DATE_FORMAT_DISPLAY) & " RE-SPAWNED: " & GetFileName(x.EXEPath))
                Else
                    lstMsgs(vbTab & e.SignalTime.ToString(DATE_FORMAT_DISPLAY) & " RE-SPAWNED FAILED: " & GetFileName(x.EXEPath) & " isn't exists")
                End If
            End If
        Next x
        IntervalGuard.Start()
    End Sub

    Private Sub DataInsertEvent(ByVal eventname As String, ByVal value As String)
        lstMsgs(Now.ToString(LOGTIMEFORMAT) & INDENT & eventname & ": " & value)
    End Sub

    Private Sub DataInsertError(ByVal timestamp As Date, ByVal Err As System.Exception)
        lstMsgs(Now.ToString(LOGTIMEFORMAT) & INDENT & "DataInsertError: " & Err.Message)
    End Sub

    Private Sub DoubleClickList(sender As Object, e As EventArgs)
        Dim List As ListBox = DirectCast(sender, ListBox)
        My.Computer.Clipboard.SetText(List.SelectedItem)
    End Sub

#End Region
#Region "Start / Stop"
    Private Sub frmWatcher_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        StopWatching()
    End Sub

    Private Sub frmWatcher_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If ProcessesRunning(myName) > 1 Then
            lstMsgs("Me is running (one instance)")
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

            AddHandler dService.OnEvent, AddressOf DataInsertEvent
            AddHandler dService.OnError, AddressOf DataInsertError
            AddHandler ListBox1.DoubleClick, AddressOf DoubleClickList

            'FORM ATTRIBUTES
            '--------------------------------------------------------
            Me.Icon = myIcon
            Me.Text = myName
            Me.Size = New Size(700, 400)

            ListBox1.ForeColor = Color.LimeGreen
            ListBox1.BackColor = Color.Black

        End If
    End Sub
#End Region

#Region "Read Files"

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
                lstMsgs("unrecognised file: " & FILENAME)
        End Select

    End Sub

    Private Sub ReadCAFile(ByVal FILE_NAME As String)
        lstMsgs(FILE_NAME)

        Try
            ' Open the file using a stream reader.
            Using sr As New StreamReader(FILE_NAME, System.Text.Encoding.GetEncoding("UCS-2"))
                Dim line As String
                ' Read the stream to a string and write the string to the console.
                line = sr.ReadToEnd()

                Dim tmp = line.Split("|")
                'Console.WriteLine("COUNT " & tmp.Length)
                'For Each x In tmp
                '    If x.IndexOf(":") >= 0 Then
                '        lstMsgs("[" & x & "]")
                '    Else
                '        lstMsgs(vbTab & ">>" & Trim(x) & "<<")
                '    End If
                'Next

                Dim NewData As DataExchangeClass.FSQDConsAppReq.FSQDDeclaration = Arr2Object(tmp)

                dService.FSQDInsert(NewData)

            End Using
        Catch e As Exception
            lstMsgs(Now.ToString(LOGTIMEFORMAT) & INDENT & e.Message)
        End Try

        Try
            If Not MoveAfile(FILE_NAME, PerfectPath(getArchivePath(FILE_NAME)) & Path.GetFileName(FILE_NAME)) Then Throw New Exception("Move file error!")
            lstMsgs(Now.ToString(LOGTIMEFORMAT) & INDENT & "Moved file " & Path.GetFileName(FILE_NAME) & " to " & getArchivePath(FILE_NAME))
        Catch ex As Exception
            lstMsgs(Now.ToString(LOGTIMEFORMAT) & INDENT & "Archiving file " & FILE_NAME & " failed!")
        End Try

    End Sub

    Private Function Arr2Object(ByVal arrString() As String) As DataExchangeClass.FSQDConsAppReq.FSQDDeclaration

        Dim FSQD As New DataExchangeClass.FSQDConsAppReq.FSQDDeclaration
        Dim InvoiceItem As DataExchangeClass.FSQDConsAppReq.InvoiceItem = Nothing
        Dim Permit As DataExchangeClass.FSQDConsAppReq.Permit = Nothing
        Dim Spec As DataExchangeClass.FSQDConsAppReq.Specification = Nothing
        Dim Attachment As DataExchangeClass.FSQDConsAppReq.Attachment = Nothing

        Try

            Dim validTag As String = String.Empty
            Dim currTag As String = String.Empty

            Console.WriteLine(StrDup(5, "=") & " Arr2Object " & StrDup(5, "="))
            Dim counter As Integer = 0, invoiceCount As Integer = 0, invoiceItemCount As Integer = 0, permitCount As Integer = 0, attachementCount As Integer = 0
            For Each item In arrString
                If (item.IndexOf("FSQDConsAppReq") >= 0) And (currTag = String.Empty) Then
                    currTag = "FSQDConsAppReq"
                    validTag &= currTag
                ElseIf (item.IndexOf("Body") >= 0) And (currTag = "FSQDConsAppReq") Then
                    currTag = "Body"
                    validTag &= ">" & currTag
                ElseIf (item.IndexOf("FSQDDeclaration") >= 0) And (currTag = "Body") Then
                    currTag = "FSQDDeclaration"
                    validTag &= ">" & currTag
                ElseIf (item.IndexOf("Invoice") >= 0) And (currTag = "FSQDDeclaration") Then
                    currTag = "Invoice"
                    counter = 0
                ElseIf (item.IndexOf("InvoiceItems") >= 0) And (currTag = "Invoice") Then
                    invoiceCount += 1
                    currTag = "InvoiceItems"
                ElseIf (item.IndexOf("InvoiceItem") >= 0) And (currTag = "InvoiceItems") Then
                    invoiceItemCount += 1
                    currTag = "InvoiceItem"
                    counter = 0
                    'NEW OBJECT
                    InvoiceItem = New DataExchangeClass.FSQDConsAppReq.InvoiceItem

                ElseIf (item.IndexOf("Permits") >= 0) And (currTag = "InvoiceItem") Then
                    currTag = "Permits"
                ElseIf (item.IndexOf("Permit") >= 0) And (currTag = "Permits") Then
                    permitCount += 1
                    currTag = "Permit"
                    counter = 0
                    'NEW OBJECT
                    Permit = New DataExchangeClass.FSQDConsAppReq.Permit
                ElseIf (item.IndexOf("Specifications") >= 0) And (currTag = "Permit") Then
                    currTag = "Specifications"
                ElseIf (item.IndexOf("Specification") >= 0) And (currTag = "Specifications") Then
                    currTag = "Specification"
                    counter = 0
                    'NEW OBJECT
                    Spec = New DataExchangeClass.FSQDConsAppReq.Specification
                ElseIf (item.IndexOf("Attachments") >= 0) And (currTag = "Permit") Then
                    currTag = "Attachments"
                ElseIf (item.IndexOf("Attachment") >= 0) And (currTag = "Attachments") Then
                    attachementCount += 1
                    currTag = "Attachment"
                    counter = 0
                    'NEW OBJECT
                    Attachment = New DataExchangeClass.FSQDConsAppReq.Attachment
                Else

                    'Console.WriteLine(validTag)

                    If validTag = "FSQDConsAppReq>Body>FSQDDeclaration" Then
                        Select Case currTag
                            Case "FSQDDeclaration"
                                counter += 1
                                Dim key As String = String.Empty

                                Select Case counter
                                    Case 1 'MCKey
                                        key = "MCKey"
                                        FSQD.MCKey = item
                                    Case 2 'MCValue
                                        key = "MCValue"
                                        FSQD.MCValue = item
                                    Case 3 'CustomFormNumber
                                        key = "CustomFormNumber"
                                        FSQD.CustomFormNumber = item
                                    Case 4 'TransactionType
                                        key = "TransactionType"
                                        FSQD.TransactionType = item
                                    Case 5 'RegistrationDate
                                        key = "RegistrationDate"
                                        FSQD.RegistrationDate = item
                                    Case 6 'RegistrationTime
                                        key = "RegistrationTime"
                                        FSQD.RegistrationTime = item
                                    Case 7 'DeclarantName
                                        key = "DeclarantName"
                                        FSQD.DeclarantName = item
                                    Case 8 'DeclarantICNumber
                                        key = "DeclarantICNumber"
                                        FSQD.DeclarantICNumber = item
                                    Case 9 'DeclarantStatus
                                        key = "DeclarantStatus"
                                        FSQD.DeclarantStatus = item
                                    Case 10 'TotalNumberOfItem
                                        key = "TotalNumberOfItem"
                                        FSQD.TotalNumberOfItem = item
                                    Case 11 'ExporterName
                                        key = "ExporterName"
                                        FSQD.ExporterName = item
                                    Case 12 'ExporterAddressStreetAndNumberPObox
                                        key = "ExporterAddressStreetAndNumberPObox"
                                        FSQD.ExporterAddressStreetAndNumberPObox = item
                                    Case 13 'ExporterAddressCountry
                                        key = "ExporterAddressCountry"
                                        FSQD.ExporterAddressCountry = item
                                    Case 14 'ImporterCode
                                        key = "ImporterCode"
                                        FSQD.ImporterCode = item
                                    Case 15 'ImporterName
                                        key = "ImporterName"
                                        FSQD.ImporterName = item
                                    Case 16 'ImporterAddressStreetAndNumberPObox
                                        key = "ImporterAddressStreetAndNumberPObox"
                                        FSQD.ImporterAddressStreetAndNumberPObox = item
                                    Case 17 'ImporterAddressCity
                                        key = "ImporterAddressCity"
                                        FSQD.ImporterAddressCity = item
                                    Case 18 'ImporterAddressCountry
                                        key = "ImporterAddressCountry"
                                        FSQD.ImporterAddressCountry = item
                                    Case 19 'ImporterAddressCountrySubEntityName
                                        key = "ImporterAddressCountrySubEntityName"
                                        FSQD.ImporterAddressCountrySubEntityName = item
                                    Case 20 'ImporterAddressPostcodeIdentification
                                        key = "ImporterAddressPostcodeIdentification"
                                        FSQD.ImporterAddressPostcodeIdentification = item

                                    Case 21 'AgentCode
                                        key = "AgentCode"
                                        FSQD.AgentCode = item
                                    Case 21 'AgentName 'HACKS : Missing AgnetCode
                                        key = "AgentName"
                                        FSQD.AgentCode = item

                                    Case 23 'AgentAddressStreetAndNumberPObox
                                        key = "AgentAddressStreetAndNumberPObox"
                                        FSQD.AgentAddressStreetAndNumberPObox = item
                                    Case 24 'AgentAddressCity
                                        key = "AgentAddressCity"
                                        FSQD.AgentAddressCity = item
                                    Case 25 'AgentAddressCountry
                                        key = "AgentAddressCountry"
                                        FSQD.AgentAddressCountry = item
                                    Case 26 'AgentAddressCountrySubEntityName
                                        key = "AgentAddressCountrySubEntityName"
                                        FSQD.AgentAddressCountrySubEntityName = item
                                    Case 27 'AgentAddressPostcodeIdentification
                                        key = "AgentAddressPostcodeIdentification"
                                        FSQD.AgentAddressPostcodeIdentification = item
                                    Case 28 'ConsignmentNote
                                        key = "ConsignmentNote"
                                        FSQD.ConsignmentNote = item
                                    Case 29 'GeneralDescriptionOfGoods
                                        key = "GeneralDescriptionOfGoods"
                                        FSQD.GeneralDescriptionOfGoods = item
                                    Case 30 'Marks
                                        key = "Marks"
                                        FSQD.Marks = item
                                    Case 31 'ManifestRegistrationNumber
                                        key = "ManifestRegistrationNumber"
                                        FSQD.ManifestRegistrationNumber = item
                                    Case 32 'ModeOfTransport
                                        key = "ModeOfTransport"
                                        FSQD.ModeOfTransport = item
                                    Case 33 'DateOfImport
                                        key = "DateOfImport"
                                        FSQD.DateOfImport = item
                                    Case 34 'VesselRegistration
                                        key = "VesselRegistration"
                                        FSQD.VesselRegistration = item
                                    Case 35 'VoyageNumber
                                        key = "VoyageNumber"
                                        FSQD.VoyageNumber = item
                                    Case 36 'VesselName
                                        key = "VesselName"
                                        FSQD.VesselName = item
                                    Case 37 'FlightNumber
                                        key = "FlightNumber"
                                        FSQD.FlightNumber = item
                                    Case 38 'FlightDate
                                        key = "FlightDate"
                                        FSQD.FlightDate = item
                                    Case 39 'PlaceOfImport
                                        key = "PlaceOfImport"
                                        FSQD.PlaceOfImport = item
                                    Case 40 'PlaceOfLoading
                                        key = "PlaceOfLoading"
                                        FSQD.PlaceOfLoading = item
                                    Case 41 'PortOfTransshipment
                                        key = "PortOfTransshipment"
                                        FSQD.PortOfTransshipment = item

                                End Select

                                'Console.WriteLine(currTag & vbTab & counter & " : " & item)
                                Console.WriteLine(key & " : " & item)

                            Case "Invoice" ' GROUP TAG
                                counter += 1
                                Dim key As String = String.Empty
                                Select Case counter
                                    Case 1 'PayTo
                                        key = "PayTo"
                                        FSQD.Invoice.PayTo = item
                                    Case 2 'Insurance
                                        key = "Insurance"
                                        FSQD.Invoice.Insurance = item
                                    Case 3 'OtherCharges
                                        key = "OtherCharges"
                                        FSQD.Invoice.OtherCharges = item
                                    Case 4 'CIF
                                        key = "CIF"
                                        FSQD.Invoice.CIF = item
                                    Case 5 'FOB
                                        key = "FOB"
                                        FSQD.Invoice.FOB = item
                                    Case 6 'Freight
                                        key = "Freight"
                                        FSQD.Invoice.Freight = item
                                    Case 7 'TotalDutyPayable
                                        key = "TotalDutyPayable"
                                        FSQD.Invoice.TotalDutyPayable = item

                                End Select
                                'Console.WriteLine(currTag & " : " & item)
                                Console.WriteLine(key & " : " & item)

                            Case "InvoiceItems"
                                Console.WriteLine(currTag & " : " & item)
                            Case "InvoiceItem"
                                counter += 1
                                Dim key As String = String.Empty
                                Select Case counter
                                    Case 1 'ItemNumber
                                        key = "ItemNumber"
                                        InvoiceItem.ItemNumber = item
                                    Case 2 'ItemDescription
                                        key = "ItemDescription"
                                        InvoiceItem.ItemDescription = item
                                    Case 3 'HSCode
                                        key = "HSCode"
                                        InvoiceItem.HSCode = item
                                    Case 4 'GrossWeightInKGS
                                        key = "GrossWeightInKGS"
                                        InvoiceItem.GrossWeightInKGS = item
                                    Case 5 'DeclaredQuantity
                                        key = "DeclaredQuantity"
                                        InvoiceItem.DeclaredQuantity = item
                                    Case 6 'DeclaredUnit
                                        key = "DeclaredUnit"
                                        InvoiceItem.DeclaredUnit = item
                                    Case 7 'UnitPrice
                                        key = "UnitPrice"
                                        InvoiceItem.UnitPrice = item
                                    Case 8 'TotalPrice
                                        key = "TotalPrice"
                                        InvoiceItem.TotalPrice = item
                                    Case 9 'DutyAmount
                                        key = "DutyAmount"
                                        InvoiceItem.DutyAmount = item
                                    Case 10 'CountryOfOrigin
                                        key = "CountryOfOrigin"
                                        InvoiceItem.CountryOfOrigin = item
                                    Case 11 'CommodityStatus
                                        key = "CommodityStatus"
                                        InvoiceItem.CommodityStatus = IIf(item = DataExchangeClass.FSQDConsAppReq.InvoiceItem.enumCommodityStatus.D.ToString, _
                                                                          DataExchangeClass.FSQDConsAppReq.InvoiceItem.enumCommodityStatus.D, _
                                                                          DataExchangeClass.FSQDConsAppReq.InvoiceItem.enumCommodityStatus.ND)

                                        FSQD.Invoice.InvoiceItems.Add(InvoiceItem)
                                End Select
                                'Console.WriteLine(currTag & " : " & item)
                                Console.WriteLine(key & " : " & item)

                            Case "Permits" ' GROUP TAG
                                Console.WriteLine(currTag & " : " & item)
                            Case "Permit"
                                counter += 1
                                Dim key As String = String.Empty
                                Select Case counter
                                    Case 1 'ImportPermitNumber
                                        key = "ImportPermitNumber"
                                        Permit.ImportPermitNumber = item

                                        InvoiceItem.Permits.Add(Permit)

                                    Case 2
                                    Case 3
                                    Case 4
                                    Case 5
                                End Select
                                'Console.WriteLine(currTag & " : " & item)
                                Console.WriteLine(key & " : " & item)

                            Case "Specifications" ' GROUP TAG
                                Console.WriteLine(currTag & " : " & item)
                            Case "Specification"
                                counter += 1
                                Dim key As String = String.Empty
                                Select Case counter
                                    Case 1 'PurposeOfImport
                                        key = "PurposeOfImport"
                                        Spec.PurposeOfImport = item
                                    Case 2 'WarehouseCode
                                        key = "WarehouseCode"
                                        Spec.WarehouseCode = item
                                    Case 3 'WarehouseName
                                        key = "WarehouseName"
                                        Spec.WarehouseName = item
                                    Case 4 'WarehouseAddress
                                        key = "WarehouseAddress"
                                        Spec.WarehouseAddress = item
                                    Case 5 'ExporterCode
                                        key = "ExporterCode"
                                        Spec.ExporterCode = item
                                    Case 6 'FoodCode
                                        key = "FoodCode"
                                        Spec.FoodCode = item
                                    Case 7 'Brand
                                        key = "Brand"
                                        Spec.Brand = item
                                    Case 8 'DateOfProduction
                                        key = "DateOfProduction"
                                        Spec.DateOfProduction = item
                                    Case 9 'DateOfExpire
                                        key = "DateOfExpire"
                                        Spec.DateOfExpire = item
                                    Case 10 'Treatment
                                        key = "Treatment"
                                        Spec.Treatment = item
                                    Case 11 'ManufacturerCode
                                        key = "ManufacturerCode"
                                        Spec.ManufacturerCode = item
                                    Case 12 'ManufacturerName
                                        key = "ManufacturerName"
                                        Spec.ManufacturerName = item
                                    Case 13 'ManufacturerAddress
                                        key = "ManufacturerAddress"
                                        Spec.ManufacturerAddress = item
                                    Case 14 'PreImportRegistrationNo
                                        key = "PreImportRegistrationNo"
                                        Spec.PreImportRegistrationNo = item

                                        InvoiceItem.Specifications.Add(Spec)

                                End Select
                                'Console.WriteLine(currTag & " : " & item)
                                Console.WriteLine(key & " : " & item)

                            Case "Attachments" ' GROUP TAG
                                Console.WriteLine(currTag & " : " & item)
                            Case "Attachment"
                                counter += 1
                                Dim key As String = String.Empty
                                Select Case counter
                                    Case 1 'FilePath
                                        key = "FilePath"
                                        Attachment.FilePath = item
                                    Case 2 'FileContent
                                        key = "FileContent"
                                        Attachment.FileContent = item
                                        FSQD.Attachments.Add(Attachment)
                                End Select
                                'Console.WriteLine(currTag & vbTab & attachementCount & " : " & item)
                                Console.WriteLine(key & " : " & item)
                            Case Else
                                Console.WriteLine("X" & currTag & " : " & item)
                        End Select
                    Else
                        Throw New Exception("Invalid FSQDConsAppReq Text")
                    End If



                End If

                'Invoice InvoiceItems InvoiceItem
                'Permits Permit
                'Attachments Attachment

            Next

        Catch ex As Exception
            lstMsgs(Now.ToString(LOGTIMEFORMAT) & INDENT & ex.Message)
        End Try

        Return FSQD

    End Function

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
            lstMsgs("Moved file " & Path.GetFileName(FILE_NAME) & " to " & getArchivePath(FILE_NAME))
        Catch ex As Exception
            lstMsgs("Archiving file " & FILE_NAME & " failed!")
        End Try

    End Sub

    Private Sub ReadK1File(ByVal FILE_NAME As String)
        lstMsgs(FILE_NAME)
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

#End Region

End Class