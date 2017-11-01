Imports DataExchangeClass.Common
'Imports System.Net
Imports System.Xml
Imports System.IO

'APLIKASI POST DATA KE UCUSTOM

Public Class MainForm

    Public Const AgencyName As String = "FSQD"
    Public Const WINDOWS_TEXT_TITLE As String = "Fosim Data Post"
    Dim nSize As Size

    Dim dummyDataCA As DataExchangeClass.deprecating.ConsigmentApprovalResponse
    Dim dummyDataFC As DataExchangeClass.deprecating.FoodCodeMaster

    Dim dummyDataCAb As DataExchangeClass.FSQDConsAppRes.FSQDDeclarationResponse
    Dim dummyDataFCb As DataExchangeClass.FSQDFoodCodeMaster.FSQDFoodCodeMaster

    Dim routineTimer As System.Timers.Timer

    Dim tmpData As DataTable 'for flag update

#Region "Form Action"
    Public Delegate Sub AddItemsToListBoxDelegate( _
           ByVal ToListBox As ListBox, _
           ByVal AddText As String)

    Private Sub AddItemsToListBox(ByVal ToListBox As ListBox, _
                                 ByVal AddText As String)
        If ToListBox.Items.Count > 1000 Then
            ToListBox.Items.Clear()
        End If

        ToListBox.Items.Add(AddText)
        ToListBox.SetSelected(ListBox1.Items.Count - 1, True)
        ToListBox.SetSelected(ListBox1.Items.Count - 1, False)
    End Sub

    Public Sub lstMsgs(ByVal item As Object)

        If (ListBox1.InvokeRequired) Then
            ListBox1.Invoke( _
                    New AddItemsToListBoxDelegate(AddressOf AddItemsToListBox), _
                    New Object() {ListBox1, CStr(item)})

        Else
            If Me.ListBox1.Items.Count > 1000 Then
                ListBox1.Items.Clear()
            End If

            Me.ListBox1.Items.Add(CStr(item))
            Me.ListBox1.SetSelected(ListBox1.Items.Count - 1, True)
            Me.ListBox1.SetSelected(ListBox1.Items.Count - 1, False)
        End If

    End Sub

    Private Sub ListBox1_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles ListBox1.MouseDoubleClick
        Dim listbox As ListBox = DirectCast(sender, ListBox)
        If listbox.Items.Count > 0 Then
            MsgBox(listbox.SelectedItem.ToString)
        End If
    End Sub

    Private Sub ButtonSend_Click(sender As Object, e As EventArgs) Handles ButtonSend.Click
        InitializeDummyData()

        lstMsgs("Started at " & Now)
        ButtonSend.Enabled = False
        Me.Cursor = Cursors.WaitCursor
        If RadioWS.Checked Then
            '=================================================================================================
            'SOAP
            '=================================================================================================
            If RadioCA.Checked Then
                lstMsgs("Sending Consigment Approval Response Data to uCustom Webservice")
                If CheckBoxDB.Checked Then
                    tmpData = Nothing
                    For Each item As DataExchangeClass.deprecating.ConsigmentApprovalResponse In LoadCAData()
                        SOAPCall("CADATA", GetCA(item)) 'from DB
                    Next
                    'UPDATE HERE
                    If Not IsNothing(tmpData) Then
                        lstMsgs("Updating flags...")
                        Dim k As New DataExchangeClass.Data
                        For Each x As DataRow In tmpData.Rows
                            k.UpdateSMKCFlag(x("IMG_ID"), x("flag")) 'Should be moved to SOAP CallBack for Accurate Result
                            Application.DoEvents()
                        Next
                    End If
                Else
                    SOAPCall("CADATA", GetCA(dummyDataCA)) '1 dummy
                End If
            Else
                lstMsgs("This service isn't available!")
                'lstMsgs("Sending Food Code Master File Data to uCustom Webservice")
                'SOAPCall("FCDATA", GetFC)
            End If
        Else
            '=================================================================================================
            'BATCH
            '=================================================================================================
            Dim fileName As New List(Of String)
            If RadioCA.Checked Then
                'CONSIGNMENT APPROVAL RESPONSE [FOSIM TO UCUSTOM]
                '=================================================================================================
                'The batch file will pick up every 5 minutes by uCustoms Integration Server. 
                'The batch file will then upload into uCustoms sFTP Server for the use of the system.
                '-------------------------------------------------------------------------------------------------
                lstMsgs("Sending Consigment Approval Response Data to uCustom FTP Folder")
                If CheckBoxDB.Checked Then
                    tmpData = Nothing
                    'fileName = GenerateCVS(LoadCAData, "RESCA", True) 'from DB
                    Dim CADatabList As List(Of DataExchangeClass.FSQDConsAppRes.FSQDDeclarationResponse) = LoadCADatab()
                    For Each item In CADatabList
                        fileName.Add(GenerateTextFile(item, "RESCA"))
                    Next
                Else
                    'fileName = GenerateCVS(dummyDataCA, "RESCA", True) '1 dummy
                    fileName.Add(GenerateTextFile(dummyDataCAb, "RESCA"))
                End If

                For Each File In fileName
                    Try
                        lstMsgs("Created: " & File)
                        'UploadFtpFile("FoSIM_CA_OUTBOUND", fileName)
                        MoveAfile(File, PerfectPath(CA_FTP_FOLDER_PATH) & Path.GetFileName(File))
                        lstMsgs("Uploaded: " & File & " to " & Path.GetFileName(CA_FTP_FOLDER_PATH))
                        If Not IsNothing(tmpData) Then
                            lstMsgs("Updating flags...")
                            Dim k As New DataExchangeClass.Data
                            For Each x As DataRow In tmpData.Rows
                                'k.UpdateSMKCFlag(x("IMG_ID"), x("flag"))
                                k.SetICMImportFlag(x("IMH_ID"))
                                k.SetICMImportGoodsFlag(x("IMH_ID"))
                                Application.DoEvents()
                            Next
                        End If
                    Catch ex As Exception
                        lstMsgs(ex.Message)
                    End Try
                Next

            Else
                'FOOD CODE MASTER FILE [FOSIM TO UCUSTOM]
                '=================================================================================================
                'The batch file will generate by uCustoms System once a month if there have new/updated food code. 
                'The batch file will then upload into sFTP Server for the use of the system.
                '-------------------------------------------------------------------------------------------------
                tmpData = Nothing
                lstMsgs("Sending Food Master File Data to uCustom FTP Folder")
                'If CheckBoxDB.Checked Then
                '    fileName = GenerateCVS(LoadFCData, "FC") 'from DB
                'Else
                '    fileName = GenerateCVS(dummyDataFC, "FC") '1 dummy
                'End If

                fileName.Add(GenerateTextFile(dummyDataFCb, "FC"))

                For Each File In fileName
                    Try
                        lstMsgs("Created: " & File)
                        'UploadFtpFile("UC_CD_INBOUND", fileName)
                        MoveAfile(File, PerfectPath(FC_FTP_FOLDER_PATH) & Path.GetFileName(File))
                        lstMsgs("Uploaded: " & File & " to " & Path.GetFileName(FC_FTP_FOLDER_PATH))
                        If Not IsNothing(tmpData) Then
                            lstMsgs("Updating flags...")
                            Dim k As New DataExchangeClass.Data
                            For Each x As DataRow In tmpData.Rows
                                k.UpdateFoodFlag(x("FCO_ID"))
                                Application.DoEvents()
                            Next
                        End If
                    Catch ex As Exception
                        lstMsgs(ex.Message)
                    End Try
                Next
                '=================================================================================================
            End If
            
        End If
        lstMsgs("Finished at " & Now)
        Me.Cursor = Cursors.Default
        ButtonSend.Enabled = True
        GC.Collect()
    End Sub

    Public Enum WritingOption
        FSQDDeclarationResponse
        FSQDFoodCodeMaster
    End Enum

    Function GenerateTextFile(ByVal data As Object, ByVal ServiceCode As String, Optional isResponse As Boolean = False) As String
        Dim FilenameStr As String = String.Empty

        Dim Writing As WritingOption
        If ServiceCode = "FC" Then
            Writing = WritingOption.FSQDFoodCodeMaster
        Else
            Writing = WritingOption.FSQDDeclarationResponse
        End If

        Try
            Dim lineIndent As String = vbTab 'StrDup(2, " ")
            Dim str As New System.Text.StringBuilder

            Dim g As Guid = Guid.NewGuid()

            Select Case Writing
                Case WritingOption.FSQDDeclarationResponse
                    With data
                        str.AppendLine("FSQDConsAppRes:")
                        str.AppendLine("Header:")

                        str.AppendLine("<empty>") '1
                        str.AppendLine("<empty>") '2
                        str.AppendLine("<empty>") '3
                        str.AppendLine("<empty>") '4
                        str.AppendLine("<empty>") '5
                        str.AppendLine("<empty>") '6
                        str.AppendLine("<empty>") '7
                        str.AppendLine(g.ToString.ToUpper) 'Guid / other unique identifier per file
                        str.AppendLine(.PrevGUID) 'refers to previous file batchID
                        str.AppendLine("<empty>")
                        str.AppendLine(Now.ToString("yyyy-MM-ddTHH:mm:ss")) 'yyyyMMddTHHmmss
                        str.AppendLine("<empty>")
                        str.AppendLine("false") 'hardcode doe
                        str.AppendLine("<empty>")
                        str.AppendLine("FSQDConsAppRes")
                        str.AppendLine("RES")

                        str.AppendLine("Body:")
                        str.AppendLine("FSQDDeclarationResponse:")
                        'str.AppendLine(lineIndent & "MCKey_" & .MCKey.ToString)
                        'str.AppendLine(lineIndent & "MCValue_" & .MCValue.ToString)

                        str.AppendLine(lineIndent & .CustomRegistrationNumber)
                        str.AppendLine(lineIndent & .CommentFromFQC)
                        str.AppendLine(lineIndent & .ProcessDate)

                        str.AppendLine("InvoiceItems:")
                        For Each item In .InvoiceItems
                            str.AppendLine("InvoiceItem:")

                            str.AppendLine(lineIndent & item.ItemNumber)
                            str.AppendLine(lineIndent & item.HSCode)

                            str.AppendLine(lineIndent & item.ApprovalStatus.ToString)
                            'str.Append(lineIndent & item.ActionCode.ToString)
                            str.Append(lineIndent & IIf(item.ActionCode.ToString = DataExchangeClass.FSQDConsAppRes.InvoiceItem.enumActionCode.X.ToString, "", item.ActionCode.ToString))

                            str.AppendLine(String.Empty) 'for multiple invoice item separator
                        Next

                    End With

                Case WritingOption.FSQDFoodCodeMaster
                    With data
                        str.AppendLine("FSQDFoodCodeMaster:")
                        str.AppendLine("Body:")
                        str.AppendLine("FoodCodeMaster:")
                        For Each item In .Body
                            str.AppendLine("FoodCode:")
                            With item.FoodCode

                                str.AppendLine(lineIndent & .FCOCode)
                                str.AppendLine(lineIndent & .FCODescription)
                                str.AppendLine(lineIndent & .RStatus)
                                str.AppendLine(lineIndent & .Category)
                                str.Append(lineIndent & .ProductType)

                            End With

                        Next

                    End With

            End Select

            Console.Write(str.ToString())

            str.Replace(vbNewLine, "|")
            str.Replace("<empty>", "")
            str.Replace(vbTab, "")

            Select Case Writing
                Case WritingOption.FSQDDeclarationResponse

                    FilenameStr = AgencyName & "_" & "RESCA" & "_" & Now.ToString("yyyyMMddTHHmmss") & "_RES" & ".txt"

                Case WritingOption.FSQDFoodCodeMaster

                    FilenameStr = AgencyName & "_" & "FC" & "_" & Now.ToString("yyyyMMddTHHmmss") & ".txt"

            End Select

            If Not FilenameStr = String.Empty Then
                Dim file As System.IO.StreamWriter
                Dim path As String = My.Application.Info.DirectoryPath
                file = My.Computer.FileSystem.OpenTextFileWriter(System.IO.Path.Combine(path, FilenameStr), False)
                file.WriteLine(str.ToString())
                file.Close()
            End If
        Catch ex As Exception
            lstMsgs(ex.Message)
        End Try
        Return FilenameStr
    End Function

    Private Sub MainForm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If AUTO_MODE = True Then
            routineTimer.Stop()
            routineTimer.Dispose()
        End If
    End Sub

    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        nSize = Me.Size
        Me.MaximumSize = nSize

        Me.Text = WINDOWS_TEXT_TITLE
        ReadConfiguration()


        RadioFTP.Checked = True
        RadioWS.Enabled = False

        If AUTO_MODE = True Then
            GroupBox1.Enabled = False
            GroupBox3.Enabled = False
            CheckBoxDB.Checked = True
            RadioCA.Checked = True
            routineTimer = New System.Timers.Timer(60000)
            AddHandler routineTimer.Elapsed, AddressOf RoutineElapsed
            routineTimer.Start()
        End If

    End Sub

#End Region



    Private Sub RoutineElapsed(sender As Object, e As System.Timers.ElapsedEventArgs)
        'lstMsgs(Now.Second)
        ButtonSend_Click(Nothing, Nothing)
    End Sub

    Private Function LoadCAData() As List(Of DataExchangeClass.deprecating.ConsigmentApprovalResponse)
        Dim k As New DataExchangeClass.Data

        Dim newlist As New List(Of DataExchangeClass.deprecating.ConsigmentApprovalResponse)
        'Dim newlist As New List(Of DataExchangeClass.FSQDConsAppRes.FSQDDeclarationResponse)
        Dim dtCA As DataTable = k.GetResponseData()

        For Each row As DataRow In dtCA.Rows
            Dim tmpObj As New DataExchangeClass.deprecating.ConsigmentApprovalResponse
            With tmpObj
                .Data_Header = row("DataHeader")
                .UCustomRegistrationID = IIf(IsDBNull(row("customreg")), "", row("customreg"))
                .FQC_Preassigned_control_no = row("FQC")
                .Item_number = row("IMGLine")
                .HS_code = IIf(IsDBNull(row("HScode")), "", row("HScode"))
                .Food_Code = row("IMGFoodCode")
                .exam_level = IIf(IsDBNull(row("IMGCurrLvl")), "", row("IMGCurrLvl"))
                .Process_date = CDate(row("LMDT")).ToString("yyyy-MM-dd")
                .Process_date_2 = ""
                .Process_time = CDate(row("LMDT")).ToString("HH:mm:ss")
                .Processing_officer_name = row("LMBY")
                .Processing_officer_name_2 = ""
                .Officer_Designation = IIf(IsDBNull(row("IMHReplyDesign")), "", row("IMHReplyDesign"))
                .Comment_from_FQC = row("IMGAGNotes")
                'HERE, CHECKING & UPDATE FLAG
                'A-Approved, R-Rejection, N-Not applicable,I-Request Inspection ( will update the status code after inspection done)
                'C- Conditional Release
                .Action_code = IIf(IsDBNull(row("IMGStatusPurpose")), "", row("IMGStatusPurpose"))

                Select Case IIf(IsDBNull(row("IMGpstatus")), "", row("IMGpstatus"))
                    Case "R"
                        .Approval_Status = "A"
                        row("flag") = "C"
                    Case "D"
                        If .Action_code = "CR" Then
                            .Approval_Status = "C"
                        Else
                            .Approval_Status = "I"
                        End If
                        row("flag") = "1"
                    Case "J"
                        .Approval_Status = "R"
                        row("flag") = "C"
                    Case Else
                        .Approval_Status = "N"
                        row("flag") = "C"
                End Select

                .Remarks = IIf(IsDBNull(row("IMHReplyRemarks")), "", row("IMHReplyRemarks"))
                .Approval_reference_no = ""
                .Approval_reference_no_2 = ""
            End With
            newlist.Add(tmpObj)
        Next

        tmpData = dtCA.Copy

        Return newlist

    End Function

    Private Function LoadCADatab() As List(Of DataExchangeClass.FSQDConsAppRes.FSQDDeclarationResponse)
        Dim DEC As New DataExchangeClass.Data
        Dim newlist As New List(Of DataExchangeClass.FSQDConsAppRes.FSQDDeclarationResponse)

        Dim dtICMImport As DataTable = DEC.GetICMImport

        For Each CARow As DataRow In dtICMImport.Rows

            Dim LoadDataCAb As New DataExchangeClass.FSQDConsAppRes.FSQDDeclarationResponse
            With LoadDataCAb

                .MCKey = 0
                .MCValue = 0
                .PrevGUID = CARow.Item("BATCHID")
                .CustomRegistrationNumber = CARow.Item("IMHK1RefNum")
                '.CommentFromFQC = "0709.60.1000"
                .CommentFromFQC = CARow.Item("IMHReplyRemarks")
                .ProcessDate = CARow.Item("LMDT")
                .InvoiceItems = New List(Of DataExchangeClass.FSQDConsAppRes.InvoiceItem)
                Dim icmimportgood As DataTable = DEC.GetICMImportGoods(CARow.Item("IMH_ID"))
                If icmimportgood.Rows.Count > 0 Then

                    For Each rowY As DataRow In icmimportgood.Rows

                        Dim InvoiceItem As New DataExchangeClass.FSQDConsAppRes.InvoiceItem
                        With InvoiceItem
                            .ItemNumber = rowY.Item("IMGLine")
                            .HSCode = rowY.Item("IMGTariffCode")
                            .ApprovalStatus = rowY.Item("ApprovalStatus")
                            .ActionCode = rowY.Item("ActionCode")
                        End With

                        .InvoiceItems.Add(InvoiceItem)

                    Next

                End If
            End With

            newlist.Add(LoadDataCAb)
        Next

        tmpData = dtICMImport.Copy

        Return newlist

    End Function

    Private Function LoadFCData() As List(Of DataExchangeClass.deprecating.FoodCodeMaster)
        Dim k As New DataExchangeClass.Data

        Dim newlist As New List(Of DataExchangeClass.deprecating.FoodCodeMaster)
        Dim dtFC As DataTable = k.GetFoodCodeData()

        For Each row As DataRow In dtFC.Rows
            Dim tmpObj As New DataExchangeClass.deprecating.FoodCodeMaster
            With tmpObj
                .FCOCode = row("FCOCode")
                .FCODescription = row("FCODescription")
                .HS_ID = row("HS_ID")
                .RStatus = row("RStatus")
                .LastModifiedBy = row("LMBY")
                .LastModifiedDate = row("LMDT")
            End With
            newlist.Add(tmpObj)
        Next

        tmpData = dtFC.Copy

        Return newlist

    End Function

    Private Function LoadFCDataB() As List(Of DataExchangeClass.FSQDFoodCodeMaster.FSQDFoodCodeMaster)
        Dim k As New DataExchangeClass.Data

        Dim newlist As New List(Of DataExchangeClass.FSQDFoodCodeMaster.FSQDFoodCodeMaster)
        Dim dtFC As DataTable = k.GetFoodCodeData()

        For Each row As DataRow In dtFC.Rows
            Dim tmpObj As New DataExchangeClass.FSQDFoodCodeMaster.FSQDFoodCodeMaster
            With tmpObj
                '.FCOCode = row("FCOCode")
                '.FCODescription = row("FCODescription")
                '.HS_ID = row("HS_ID")
                '.RStatus = row("RStatus")
                '.LastModifiedBy = row("LMBY")
                '.LastModifiedDate = row("LMDT")



            End With
            newlist.Add(tmpObj)
        Next

        tmpData = dtFC.Copy

        Return newlist

    End Function

    Private Sub InitializeDummyData()

        '=============================================================================================================================
        'DataHeader	customreg	FQC	IMGLine	HScode	IMGCurrLvl	IMGFoodCode	LMBY	LMDT	IMHReplyDesign	IMGAGNotes	IMGPStatus	IMGStatusPurpose	IMHReplyRemarks
        'FQC001	K122015101004808	0709.60.1000 	3	NULL	5	V0103523	MOHD SUHAIMIE B. NOH	2015-01-21 19:57:23.647			R	R	Konsaimen diperiksa dan dilepaskan
        dummyDataCA = New DataExchangeClass.deprecating.ConsigmentApprovalResponse
        With dummyDataCA

            .Data_Header = "FQC001"
            .UCustomRegistrationID = "K122015101004808"
            .FQC_Preassigned_control_no = "" '"0709.60.1000"
            .Item_number = "1"
            .HS_code = "1902.19.900"
            .Food_Code = "V0103523"
            .exam_level = "5"
            .Process_date = "2015-01-21"
            .Process_date_2 = ""
            .Process_time = "19:57:23"
            .Processing_officer_name = "MOHD SUHAIMIE B. NOH"
            .Processing_officer_name_2 = ""
            .Officer_Designation = "PPKP U29"
            .Comment_from_FQC = ""
            .Approval_Status = "N"
            .Action_code = "R"
            .Remarks = "Konsaimen diperiksa dan dilepaskan"
            .Approval_reference_no = ""
            .Approval_reference_no_2 = ""

        End With
        '-------------------------------------------------------------------------------------------------------------------------------
        dummyDataCAb = New DataExchangeClass.FSQDConsAppRes.FSQDDeclarationResponse
        With dummyDataCAb
            .MCKey = 0
            .MCValue = 0
            .PrevGUID = "320AE4CA-17DF-495E-B402-A789D278C33C"
            .CustomRegistrationNumber = "B1FE00169662015"
            '.CommentFromFQC = "0709.60.1000"
            .CommentFromFQC = "diperiksa dan dilepaskan"
            .ProcessDate = "2017-05-12"
            .InvoiceItems = New List(Of DataExchangeClass.FSQDConsAppRes.InvoiceItem)
            Dim InvoiceItem As New DataExchangeClass.FSQDConsAppRes.InvoiceItem
            With InvoiceItem
                .ItemNumber = 1
                .HSCode = "040410910"
                .ApprovalStatus = DataExchangeClass.FSQDConsAppRes.InvoiceItem.enumApprovalStatus.R.ToString   'N is no more, R instead
                .ActionCode = DataExchangeClass.FSQDConsAppRes.InvoiceItem.enumActionCode.R.ToString
            End With
            .InvoiceItems.Add(InvoiceItem)
        End With

        '=============================================================================================================================
        'FCOCode	FCODescription	HS_ID	RStatus	LMBY	LMDT
        'A0301001	Food Colouring Substance - Allura Red Ac (16035)	2413	2	ADMIN	2005-11-17 13:15:04.000
        dummyDataFC = New DataExchangeClass.deprecating.FoodCodeMaster
        With dummyDataFC
            .FCOCode = "A0301502"
            .FCODescription = "Food Colouring Substance - Amaranth (16185)"
            .HS_ID = "2412"
            .RStatus = 2
            .LastModifiedBy = "ADMIN"
            .LastModifiedDate = "2005-11-14 14:31:29.727"
        End With
        '-------------------------------------------------------------------------------------------------------------------------------
        dummyDataFCb = New DataExchangeClass.FSQDFoodCodeMaster.FSQDFoodCodeMaster
        With dummyDataFCb
            .Body = New List(Of DataExchangeClass.FSQDFoodCodeMaster.FoodCodeMaster)
            Dim FC As New DataExchangeClass.FSQDFoodCodeMaster.FoodCodeMaster
            FC.FoodCode = New DataExchangeClass.FSQDFoodCodeMaster.FoodCode
            With FC.FoodCode
                .FCOCode = "A0301502"
                .FCODescription = "Food Colouring Substance - Amaranth (16185)"
                .RStatus = 2
                .Category = Nothing
                .ProductType = Nothing
            End With
            .Body.Add(FC)
        End With
        '=============================================================================================================================

    End Sub

    Private Function GenerateCVS(ByVal data As Object, ByVal ServiceCode As String, Optional isResponse As Boolean = False) As String

        Dim fileName As String = AgencyName & "_" & ServiceCode & "_" & Now.ToString("yyyyMMddTHHmmss") & IIf(isResponse, "_RES", "") & ".txt"

        Try
            Dim textWriter As StreamWriter = File.CreateText(fileName)
            Dim csv = New CsvHelper.CsvWriter(textWriter)
            Dim list As Object

            csv.Configuration.HasHeaderRecord = False
            csv.Configuration.QuoteAllFields = True

            If IsList(data) Then
                list = data
                If list.count = 0 Then Throw New Exception("No Data")
            Else
                list = New List(Of Object)
                list.Add(data)
            End If

            csv.WriteRecords(list)
            csv.Dispose()
            textWriter.Dispose()

            Return fileName
        Catch ex As Exception
            lstMsgs(ex.Message)
            Return Nothing
        Finally

        End Try
    End Function

#Region "Get XML Data"

    Public Function GetCA(ByVal CAData As DataExchangeClass.deprecating.ConsigmentApprovalResponse, Optional ByVal AsXMLString As Boolean = True) As Object
        If AsXMLString = True Then
            Return SOAP_ENVELOPE.Replace("@DATA", CA_ENV.Replace("@DATA", CleanSerializedData(SerializeIT(CAData))))
        Else
            Return CAData
        End If
    End Function

    Public Function GetFC(Optional ByVal AsXMLString As Boolean = True) As Object
        If AsXMLString = True Then
            Return SOAP_ENVELOPE.Replace("@DATA", FC_ENV.Replace("@DATA", CleanSerializedData(SerializeIT(dummyDataFC))))
        Else
            Return dummyDataFC
        End If
    End Function

#End Region

    Sub SOAPCall(ByVal Type As String, ByVal XMLPath As String)
        Dim objHTTP As New MSXML2.XMLHTTP60
        Dim strEnvelope As String
        Dim strReturn As String
        Dim objReturn As New MSXML2.DOMDocument60
        'Dim dblTax As String
        Dim strQuery As String
        Dim cmn As New DataExchangeClass.Common

        If Microsoft.VisualBasic.Right(XMLPath, 3) = "xml" Then
            Dim k As New XmlDocument
            k.Load(XMLPath)
            strEnvelope = k.InnerXml
        Else
            strEnvelope = XMLPath
            'strEnvelope = strEnvelope.Replace("@LOGIN", LOGIN).Replace("@PWD", PWD)
            lstMsgs("Sending: " & vbCrLf & strEnvelope)
            cmn.log("Logs", "Sending: " & vbCrLf & strEnvelope)
        End If

        'Set up to post to our local server
        objHTTP.open("post", WEB_SERVICE_ASMX_URL, False)
        'objHTTP.open("post", WEB_SERVICE_ASMX_URL, False, LOGIN, PWD)

        'Set a standard SOAP/ XML header for the content-type
        objHTTP.setRequestHeader("Content-Type", "text/xml")

        'Set a header for the method to be called
        'objHTTP.setRequestHeader("SOAPAction", _
        '"urn:localhost/soap:ItramasTMCWS#MSG_AlarmData")

        'Make the SOAP call
        objHTTP.send(strEnvelope)

        'Get the return envelope
        strReturn = objHTTP.responseText

        'If TESTMODE = True Then
        '    MsgBox(strReturn)
        '    Exit Sub
        'End If

        'Load the return envelope into a DOM
        objReturn.loadXML(strReturn)

        'Query the return envelope
        Select Case Type
            Case "CADATA"
                strQuery = "soap:Envelope/soap:Body/MSG_AlarmDataResponse/MSG_AlarmDataResult"
            Case "FCDATA"
                strQuery = "soap:Envelope/soap:Body/getDataResponse/getDataResult"
            Case Else
                strQuery = Nothing
        End Select

        'lstMsgs(objReturn.text)
        lstMsgs("Response:" & vbCrLf & strReturn)
        cmn.log("Logs", "Response:" & vbCrLf & strReturn)

        'SUCCESS POST WILL BE UPDATED INTO DATABASE TABLE

        'Try
        '    If Not strQuery = Nothing Then
        '        'dblTax = objReturn.selectSingleNode(strQuery).text
        '        'Select Case dblTax
        '        '    Case ResponseStatus.AUTHOTHIZE_FAIL
        '        '        lstMsgs(Type & " process: AUTHORIZATION FAIL")
        '        '    Case ResponseStatus.VALIDATE_DATA_FAIL
        '        '        lstMsgs(Type & " process: DATA VALIDATION FAIL")
        '        '    Case ResponseStatus.PROCESS_DATA_FAIL
        '        '        lstMsgs(Type & " process: DATA PROCESSING FAIL")
        '        '    Case ResponseStatus.SUCCESS
        '        '        lstMsgs(Type & " process: SUCCESS")
        '        'End Select
        '    Else
        '        lstMsgs(Type & ": Error retrive result!")
        '    End If
        'Catch ex As Exception
        '    lstMsgs(ex.Message)
        '    'Dim objRtrn As New XmlDocument
        '    'objRtrn.LoadXml(strReturn)
        '    'Dim XQuery As XmlNode = objReturn.documentElement
        '    'lstMsgs(XQuery.InnerText)
        'End Try

    End Sub

End Class
