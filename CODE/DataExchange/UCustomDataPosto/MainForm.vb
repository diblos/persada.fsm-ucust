Imports DataExchangeClass.Common
'Imports System.Net
Imports System.Xml
Imports System.IO

'APLIKASI POST DATA KE FOSIM - PROOF OF CONCEPT

Public Class MainForm

    Dim r As New Random
    Public Const AgencyName As String = "FSQD"
    Public Const WINDOWS_TEXT_TITLE As String = "Custom Data Post"
    Dim nSize As Size

    Dim dummyDataCA As DataExchangeClass.deprecating.ConsigmentApprovalRequest
    Dim dummyDataCD As DataExchangeClass.deprecating.CUSDECInfo

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

    Private Sub ButtonSend_Click(sender As Object, e As EventArgs) Handles ButtonSend.Click
        InitializeDummyData()
        If RadioWS.Checked Then
            '=============================================================================================
            'SOAP
            '=============================================================================================
            lstMsgs("Sending Data to Webservice")
            SOAPCall("RQCA", GetCA)
        Else
            '=============================================================================================
            'BATCH
            '=============================================================================================
            Dim fileName As String
            lstMsgs("Sending Data to FTP Service")
            'REQUEST CONSIGNMENT APPROVAL INFORMATION EXCHANGE
            fileName = GenerateCVS(dummyDataCA, "RQCA")
            If Not fileName = Nothing Then
                Try
                    lstMsgs("Created: " & fileName)
                    UploadFtpFile("CA_INBOUND", fileName)
                    lstMsgs("Uploaded: " & fileName)
                Catch ex As Exception
                    lstMsgs(ex.Message)
                Finally

                End Try
            End If

            'CUSTOM DECLARATION K1 [UCUSTOM TO FOSIM] - KIV
            'fileName = GenerateCVS(dummyDataCD, "K1BTC")
            'If Not fileName = Nothing Then
            '    Try
            '        UploadFtpFile("UC_CD_INBOUND", fileName)
            '    Catch ex As Exception
            '        lstMsgs(ex.Message)
            '    End Try
            'End If
            '=============================================================================================
        End If
    End Sub

    Private Sub ListBox1_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles ListBox1.MouseDoubleClick
        Dim listbox As ListBox = DirectCast(sender, ListBox)
        If listbox.Items.Count > 0 Then
            MsgBox(listbox.SelectedItem.ToString)
        End If
    End Sub

#End Region

    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        nSize = Me.Size
        Me.MaximumSize = nSize

        Me.Text = WINDOWS_TEXT_TITLE
        ReadConfiguration()
    End Sub

    Private Sub InitializeDummyData()

        dummyDataCA = New DataExchangeClass.deprecating.ConsigmentApprovalRequest
        With dummyDataCA
            .Data_Header = "SMK001"
            .Message_Type = "A"
            .Message_Mode = "C"
            .Import_Indicator = "I"
            '.Custom_Form_number = "W202014112058276" '>>>
            .Custom_Form_number = "W20201411205" & r.Next(1000, 2000)
            .Custom_Form_number_2 = ""
            .FQC_Preassigned_control_number = ""
            .Registration_Date = "2014-12-19"
            .Resistration_Time = "04:10:00"
            .Total_number_of_Item = 1
            .Commodity_Status = "D"
            .Transaction_Type = "I"
            .Exporter_name = "YOUNG LIVING ESSENTIAL OILS"
            .Exporter_Address = "142 EAST 3450 NORTH"
            .Exporter_Address_2 = "SPANISH FORK"
            .Exporter_Address_3 = "84660"
            .Exporter_Address_4 = ""
            .Exporter_Address_5 = ""
            .Importer_Code = "S115809P"
            .Importer_Name = "CHEN SEONG FOOK"
            .Importer_Address = "F3-6 BLK F NEO DAMANSARA"
            .Importer_Address_2 = "JALAN PJU 8/1"
            .Importer_Address_3 = "BANDAR DAMANSARA PERDANA"
            .Importer_Address_4 = "47820"
            .Importer_Address_5 = "MALAYSIA"
            .Agent_Code = "WF0100"
            .Agent_Name = "FEDERAL EXPRESS BROKERAGE SDN BHD"
            .Agent_Address = "801B,LEVEL 8, UPTOWN 5"
            .Agent_Address_2 = "5, JLN. SS 21/39"
            .Agent_Address_3 = "DAMANSARA UPTOWN 47400"
            .Agent_Address_4 = "PETALING JAYA"
            .Agent_Address_5 = "SELANGOR"
            .Mode_of_Transport = 4
            .Date_of_Import = "2014-12-20"
            .Vessel_Registration = ""
            .Voyage_number = ""
            .Vessel_name = ""
            .Flight_number = "FX5193 "
            .Flight_Date = "2014-12-20"
            .Vehicle_Lorry_number = ""
            .Trailer_number = ""
            .Place_of_Import = "KULMY"
            .Place_of_Loading = "ANCUS"
            .Port_of_Transhipment = ""
            .Pay_To = "MY"
            .Insurance = "12.14"
            .Insurance_2 = ""
            .Other_Charges = 0
            .Other_Charges_2 = 0
            .CIF = "1628.99"
            .CIF_2 = ""
            .FOB = "1213.77"
            .FOB_2 = ""
            .Freight = "403.08"
            .Freight_2 = ""
            .Gross_Weight = "3.3"
            .number_of_Packages = 1
            .Type_of_Packages = "PK"
            .Measurement = 0
            .Consignment_Note = "023-65848411 625075973330"
            .General_description_of_Goods = "KH251"
            .Marks = " *SM* NIL"
            .Manifest_Registration_number = "W200000"
            .Import_Permit_number = "0000"
            .Import_Permit_number_2 = "0000"
            .Special_Treatement = 1
            .Total_Duty_Payable = 1
            .Declarant_IC_number = "791210015723"
            .Declarant_Name = "MUHAMMAD SHAH BIN SHAHARI"
            .Declarant_Status = "SENIOR CUSTOMS AGENT"

            '====================================

            .Data_Header_B = "SMK002"
            .Custom_Form_number_B = "B182015101029628"
            .Custom_Form_number_B_2 = ""
            .Item_number = 1
            .Item_Description = "PENNE ZITI RIGATE (PASTA DI SEMOLA)"
            .HS_code = "1902.19.900"
            .Declared_Quantity_1 = 648
            .Declared_unit_1 = "KGM"
            .Unit_price = 4.12516
            .Total_price = 2673.1
            .Duty_Amount_B = 302.06
            .Duty_Amount_B_2 = 0
            .number_of_Packages_B = 0
            .Type_of_Packages_B = ""
            .Country_of_Origin = "IT"
            .Declared_Quantity_2 = 648
            .Declared_unit_2 = "KGM"
            .Purpose_of_import = 78
            .Warehouse_Code = ""
            .Warehouse_Code_2 = ""
            .Warehouse_Name = ""
            .Warehouse_Address = ""
            .Warehouse_Address_2 = ""
            .Warehouse_Address_3 = ""
            .Warehouse_Address_4 = ""
            .Warehouse_Address_5 = ""
            .Remarks_and_Accident = ""
            .Exporter_Code = ""
            .Food_Code = "C0402003"
            .Food_Code_2 = ""
            .Brand = "COCOE"
            .Brand_2 = ""

            .Date_of_production = "2014-12-12"
            .Date_of_expire = "2016-12-12"

            .Treatment = ""

            .Manufacturer_code = "6204"
            .Manufacturer_name = "JB COCOA SDN BHD"
            .Manufacturer_address = "NO.8(1 FLOOR)"
            .Manufacturer_address_2 = "JALAN PESTA 1/1"
            .Manufacturer_address_3 = "TAMAN TUN DR ISMAIL 1"
            .Manufacturer_address_4 = "JALAN BAKRI 84000"
            .Manufacturer_address_5 = "MALAYSIA"

        End With

        dummyDataCD = New DataExchangeClass.deprecating.CUSDECInfo
        With dummyDataCD

        End With

    End Sub

    Private Function GenerateCVS(ByVal data As Object, ByVal ServiceCode As String) As String

        Dim fileName As String = AgencyName & "_" & ServiceCode & "_" & Now.ToString("yyyyMMddTHHmmss") & ".txt"

        Try
            Dim textWriter As StreamWriter = File.CreateText(fileName)
            Dim csv = New CsvHelper.CsvWriter(textWriter)
            Dim list As New List(Of Object)

            csv.Configuration.HasHeaderRecord = False

            'For i = 1 To 10
            list.Add(data)
            'Next

            csv.WriteRecords(list)
            csv.Dispose()
            textWriter.Dispose()

            Return fileName
        Catch ex As Exception
            Return Nothing
        Finally

        End Try
    End Function

#Region "Get XML Data"

    Public Function GetCA(Optional ByVal AsXMLString As Boolean = True) As Object
        If AsXMLString = True Then
            Return SOAP_ENVELOPE.Replace("@DATA", CA_ENV.Replace("@DATA", CleanSerializedData(SerializeIT(dummyDataCA))))
        Else
            Return dummyDataCA
        End If
    End Function

    Public Function GetALM(Optional ByVal AsXMLString As Boolean = True) As Object
        If AsXMLString = True Then
            Return SOAP_ENVELOPE.Replace("@DATA", CA_ENV.Replace("@DATA", CleanSerializedData(SerializeIT(GetAlarmData))))
        Else
            Return GetAlarmData()
        End If
    End Function

    Private Function GetAlarmData() As DataExchangeClass.deprecating.Officer
        Try
            Dim test As New DataExchangeClass.deprecating.Officer

            With test
                .Name = "ABU"
                .Designation = "Clerk"
            End With

            Return test
        Catch ex As Exception
            'RaiseEvent OnError(Now, New Exception("GetAlarmData:" & ex.Message))
            Return New DataExchangeClass.deprecating.Officer
        End Try
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
            Case "RQCA"
                strQuery = "soap:Envelope/soap:Body/ConsignmentApprovalDataResponse/ConsignmentApprovalDataResult"
            Case Else
                strQuery = Nothing
        End Select

        lstMsgs("Response:" & vbCrLf & strReturn)
        cmn.log("Logs", "Response:" & vbCrLf & strReturn)

        'Try
        '    Dim dblTax

        '    If Not strQuery = Nothing Then
        '        dblTax = objReturn.selectSingleNode(strQuery).text
        '        Select Case dblTax
        '            Case ResponseStatus.AUTHOTHIZE_FAIL
        '                lstMsgs(Type & " process: AUTHORIZATION FAIL")
        '            Case ResponseStatus.VALIDATE_DATA_FAIL
        '                lstMsgs(Type & " process: DATA VALIDATION FAIL")
        '            Case ResponseStatus.PROCESS_DATA_FAIL
        '                lstMsgs(Type & " process: DATA PROCESSING FAIL")
        '            Case ResponseStatus.SUCCESS
        '                lstMsgs(Type & " process: SUCCESS")
        '        End Select
        '    Else
        '        lstMsgs(Type & ": Error retrive result!")
        '    End If
        'Catch ex As Exception
        '    lstMsgs(ex.Message)
        'End Try

    End Sub

End Class
