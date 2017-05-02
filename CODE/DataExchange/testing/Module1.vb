Imports System.IO

Module Module1
    Dim textFilePath As String = "C:\Users\lenovo\Desktop\workspace\ucustom\neo\FSQD_RQCA_20161107T164010.txt"

    Dim dummyDataCA As DataExchangeClass.deprecating.ConsigmentApprovalResponse
    Dim dummyDataFC As DataExchangeClass.deprecating.FoodCodeMaster
    Dim dummyDataCAb As DataExchangeClass.FSQDConsAppRes.FSQDDeclarationResponse
    Dim dummyDataFCb As DataExchangeClass.FSQDFoodCodeMaster.FSQDFoodCodeMaster

    Public Enum WritingOption
        FSQDDeclarationResponse
        FSQDFoodCodeMaster
    End Enum

    Public Enum ModeOfTransport
        Sea = 1
        Road = 3
        Air = 4
    End Enum

    Public Enum CommodityStatus
        [D_Dutiable]
        [ND_Dutiable]
    End Enum

    Sub Main()
        'Threading.Thread.Sleep(10000)

        'Testing()

        Reading()

        'Writing(WritingOption.FSQDFoodCodeMaster)

        HappyEnd() 'Wait input to end
    End Sub

    Sub HappyEnd()
        Dim r = Console.ReadLine()
        Console.WriteLine(r)
    End Sub

    Private Sub Testing()
        Dim s As String = "|MCKey|MCValue|CustomFormNumber|TransactionType|RegistrationDate|RegistrationTime|DeclarantName|DeclarantICNumber|DeclarantStatus|TotalNumberOfItem|ExporterName|ExporterAddressStreetAndNumberPObox|ExporterAddressCountry|ImporterCode|ImporterName|ImporterAddressStreetAndNumberPObox|ImporterAddressCity|ImporterAddressCountry|ImporterAddressCountrySubEntityName|ImporterAddressPostcodeIdentification|AgentCode|AgentName|AgentAddressStreetAndNumberPObox|AgentAddressCity|AgentAddressCountry|AgentAddressCountrySubEntityName|AgentAddressPostcodeIdentification|ConsignmentNote|GeneralDescriptionOfGoods|Marks|ManifestRegistrationNumber|ModeOfTransport|DateOfImport|VesselRegistration|VoyageNumber|VesselName|FlightNumber|FlightDate|PlaceOfImport|PlaceOfLoading|PortOfTransshipment|Invoice"
        For Each x In s.Split("|")
            Console.WriteLine(x)
        Next
    End Sub

    Public Sub Reading()
        Try
            ' Open the file using a stream reader.
            Using sr As New StreamReader(textFilePath, System.Text.Encoding.GetEncoding("UCS-2"))
                Dim line As String
                ' Read the stream to a string and write the string to the console.
                line = sr.ReadToEnd()

                Dim tmp = line.Split("|")
                Console.WriteLine("COUNT " & tmp.Length)
                For Each x In tmp
                    If x.IndexOf(":") >= 0 Then
                        Console.WriteLine("[" & x & "]")
                    Else
                        Console.WriteLine(vbTab & ">>" & Trim(x) & "<<")
                    End If
                Next

                Arr2Object(tmp)

            End Using
        Catch e As Exception
            Console.WriteLine("The file could not be read:")
            Console.WriteLine(e.Message)
        End Try
    End Sub

    Public Sub Writing(ByVal Writing As WritingOption)
        Try

            InitializeDummyData(Writing)
            Dim lineIndent As String = StrDup(2, " ")
            Dim str As New System.Text.StringBuilder

            Select Case Writing
                Case WritingOption.FSQDDeclarationResponse
                    With dummyDataCAb
                        str.AppendLine("FSQDConsAppRes:")
                        str.AppendLine("Body:")
                        str.AppendLine("FSQDDeclarationResponse:")
                        str.AppendLine(lineIndent & "MCKey_" & .MCKey.ToString)
                        str.AppendLine(lineIndent & "MCValue_" & .MCValue.ToString)

                        str.AppendLine(lineIndent & .CustomRegistrationNumber)
                        str.AppendLine(lineIndent & .CommentFromFQC)
                        str.AppendLine(lineIndent & .ProcessDate)

                        str.AppendLine("InvoiceItems:")
                        For Each item In .InvoiceItems
                            str.AppendLine("InvoiceItem:")
                            str.AppendLine(lineIndent & item.HSCode)
                            str.AppendLine(lineIndent & item.ItemNumber)
                            str.AppendLine(lineIndent & item.ApprovalStatus.ToString)
                            str.AppendLine(lineIndent & item.ActionCode.ToString)
                        Next

                    End With

                Case WritingOption.FSQDFoodCodeMaster
                    With dummyDataFCb
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
                                str.AppendLine(lineIndent & .ProductType)

                            End With

                        Next

                    End With

            End Select



            Console.Write(str.ToString())

        Catch e As Exception
            Console.WriteLine(e.Message)
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
                                    Case 22 'AgentName
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
            Console.WriteLine(ex.Message)
        End Try

        Return FSQD

    End Function

    Private Sub InitializeDummyData(ByVal Writing As WritingOption)

        Select Case Writing
            Case WritingOption.FSQDDeclarationResponse
                'DataHeader	customreg	FQC	IMGLine	HScode	IMGCurrLvl	IMGFoodCode	LMBY	LMDT	IMHReplyDesign	IMGAGNotes	IMGPStatus	IMGStatusPurpose	IMHReplyRemarks
                'FQC001	K122015101004808	0709.60.1000 	3	NULL	5	V0103523	MOHD SUHAIMIE B. NOH	2015-01-21 19:57:23.647			R	R	Konsaimen diperiksa dan dilepaskan
                dummyDataCAb = New DataExchangeClass.FSQDConsAppRes.FSQDDeclarationResponse
                With dummyDataCAb

                    .MCKey = 0
                    .MCValue = 0
                    .CustomRegistrationNumber = "K122015101004808"
                    .CommentFromFQC = "0709.60.1000"
                    .ProcessDate = "2015-01-21"
                    .InvoiceItems = New List(Of DataExchangeClass.FSQDConsAppRes.InvoiceItem)
                    Dim InvoiceItem As New DataExchangeClass.FSQDConsAppRes.InvoiceItem
                    With InvoiceItem
                        .HSCode = "1902.19.900"
                        .ItemNumber = 1
                        .ApprovalStatus = DataExchangeClass.FSQDConsAppRes.InvoiceItem.enumApprovalStatus.N
                        .ActionCode = DataExchangeClass.FSQDConsAppRes.InvoiceItem.enumActionCode.R
                    End With

                    .InvoiceItems.Add(InvoiceItem)

                End With

            Case WritingOption.FSQDFoodCodeMaster
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

            Case Else
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
        End Select

    End Sub

End Module
