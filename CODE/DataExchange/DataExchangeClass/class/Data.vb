Public Class Data

    Private db As Microsoft.Practices.EnterpriseLibrary.Data.Database = Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase("DB")

    Public Event OnEvent(ByVal eventname As String, ByVal value As String)
    Public Event OnError(ByVal timestamp As Date, ByVal Err As System.Exception)

#Region "DATABASE"

    Public Shared SMKCFormTableName As String = "[SMKCForm]"
    Public Shared SMKCFormGoodsTableName As String = "[SMKCFormGoods]"
    Public Shared AG_REF_NO_DEFAULT As String = "BF0750"
    Public Shared IM_ID_DEFAULT As String = 3455
    Public Shared EP_ID_DEFAULT As String = 104
    Public Shared SMKTYPE As String = "K1"
    Public Shared SMKHEADER As String = "SMK001"
    Public Shared UPDATEDBY As String = "Admin"
    Public Shared UPDATEDDATE As String
    Public Shared CERT_ID_DEFAULT As String = "1,2"

    Private Const DATETIME_FORMAT As String = "yyyy-MM-dd HH:mm:ss"

    Public Function InsertParty(ByVal data As DataExchangeClass.FSQDConsAppReq.FSQDDeclaration)
        Dim result As Integer = 0
        Try
            Dim sql As New System.Text.StringBuilder
            '==================================================================================
            'sql.Append("INSERT INTO TmpParty")
            'sql.Append("(")
            'sql.Append("[CFGK1RegNum],")
            'sql.Append("[BATCHID],")
            'sql.Append("[LMDT]")
            'sql.Append(" )")
            ''----------------------------------------------------------------------------------
            'sql.Append(" VALUES ")
            'sql.Append(" (")
            'sql.Append("'" & data.CustomFormNumber & "',")
            'sql.Append("'" & data.HeaderObj.batchID & "',")
            'sql.Append("'" & Now.ToString(DATETIME_FORMAT) & "'")
            'sql.Append(")")
            '==================================================================================

            'SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
            'BEGIN TRANSACTION;
            'UPDATE dbo.table SET ... WHERE PK = @PK;
            'IF @@ROWCOUNT = 0
            '                BEGIN()
            '  INSERT dbo.table(PK, ...) SELECT @PK, ...;
            '                End
            'COMMIT TRANSACTION;

            sql.AppendLine("SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;")
            sql.AppendLine("BEGIN TRANSACTION;")

            sql.AppendLine("UPDATE [fdb].[dbo].[TmpParty] SET ")
            sql.Append("[CFGK1RegNum] = '" & data.CustomFormNumber & "',")
            sql.Append("[BATCHID] = '" & data.HeaderObj.batchID & "',")
            sql.Append("[LMDT] = '" & Now.ToString(DATETIME_FORMAT) & "' WHERE [BATCHID] = '" & data.HeaderObj.batchID & "';")

            sql.AppendLine("IF @@ROWCOUNT = 0")
            sql.AppendLine("BEGIN")

            sql.AppendLine("INSERT INTO TmpParty")
            sql.Append("(")
            sql.Append("[CFGK1RegNum],")
            sql.Append("[BATCHID],")
            sql.Append("[LMDT]")
            sql.Append(" )")
            '----------------------------------------------------------------------------------
            sql.Append(" VALUES ")
            sql.Append(" (")
            sql.Append("'" & data.CustomFormNumber & "',")
            sql.Append("'" & data.HeaderObj.batchID & "',")
            sql.Append("'" & Now.ToString(DATETIME_FORMAT) & "'")
            sql.Append(");")

            sql.AppendLine("END")
            sql.AppendLine("COMMIT TRANSACTION;")

            '==================================================================================
            result = result + ExecuteQuery(sql.ToString)
        Catch ex As Exception
            result = 0
        End Try
        Return result
    End Function

    Public Function FSQDInsert(ByVal data As DataExchangeClass.FSQDConsAppReq.FSQDDeclaration) As Integer

        Dim result As Integer = 0

        Dim test As New System.Text.StringBuilder

        Dim SMKFORM_CTRL As Boolean = False
        Dim TMPPARTY_CTRL As Boolean = False

        For Each item In data.Invoice.InvoiceItems

            UPDATEDDATE = Now.ToString(DataExchangeClass.Common.DEFAULT_SQL_DATA_FORMAT)
            Dim sql As New System.Text.StringBuilder

            sql.Append("INSERT INTO " & SMKCFormTableName)
            '----------------------------------------------------------------------------------
            sql.Append(" (")
            'sql.Append(" [SMK_ID] ") 'Table Key - check stored procedure
            'sql.Append(" [CFH_ID]")
            sql.Append(" [SMKType]")
            sql.Append(" ,[SMKDataHead]")
            sql.Append(" ,[SMKMsgType]")
            sql.Append(" ,[SMKMsgMode]")
            sql.Append(" ,[SMKImpIndicator]") '[SMKImpIndicator]
            sql.Append(" ,[SMKRefNum]")
            'sql.Append(" ,[SMKFQCRefNum]")
            sql.Append(" ,[SMKRegDatetime]")
            sql.Append(" ,[SMKTotalItems]")
            sql.Append(" ,[SMKCommodityStatus]")
            sql.Append(" ,[SMKTransacType]") '[SMKTransacType]
            'sql.Append(" ,[EXP_ID]")
            sql.Append(" ,[SMKExpName]")
            sql.Append(" ,[SMKExpAddr]")
            sql.Append(" ,[IMP_Id]")
            'sql.Append(" ,[ImpCustRefNo]")
            sql.Append(" ,[SMKImpCode]")
            sql.Append(" ,[SMKImpName]")
            sql.Append(" ,[SMKImpAddr]")
            sql.Append(" ,[AGCustRefNo]")
            sql.Append(" ,[SMKAgentCode]")
            sql.Append(" ,[SMKAgentName]")
            sql.Append(" ,[SMKAgentAddr]")
            sql.Append(" ,[SMKTransptMode]")
            sql.Append(" ,[SMKImpDate]")
            sql.Append(" ,[SMKVesselReg]")
            sql.Append(" ,[SMKVoyageNum]")
            sql.Append(" ,[SMKVesselName]")
            sql.Append(" ,[SMKFlightNum]")
            sql.Append(" ,[SMKFlightDate]")
            'sql.Append(" ,[SMKVechicleNum]")
            'sql.Append(" ,[SMKTrailerNum]")
            sql.Append(" ,[SMKImpPlace]")
            sql.Append(" ,[SMKLoadPlace]")
            sql.Append(" ,[SMKTranshipmtPort]")
            sql.Append(" ,[SMKPayTo]")
            sql.Append(" ,[SMKInsurance]")
            sql.Append(" ,[SMKOthCharges]")
            sql.Append(" ,[SMKCIF]")
            sql.Append(" ,[SMKFOB]")
            sql.Append(" ,[SMKFreight]")
            sql.Append(" ,[SMKGrossWeight]")
            'sql.Append(" ,[SMKNoOfPackages]")
            'sql.Append(" ,[SMKPackageType]")
            'sql.Append(" ,[SMKMeasurement]")
            sql.Append(" ,[SMKConsignmtNote]")
            sql.Append(" ,[SMKGenGoodsDesc]")
            sql.Append(" ,[SMKMarksCtnNum]")

            sql.Append(" ,[SMKManifestNum]")
            'sql.Append(" ,[SMKPermitNum1]")
            'sql.Append(" ,[SMKPermitNum2]")
            'sql.Append(" ,[SMKSpecialTreat]")
            'sql.Append(" ,[SMKTotalImpDutyAmt]")

            sql.Append(" ,[SMKDeclarantICPP]")
            sql.Append(" ,[SMKDeclarantName]")
            sql.Append(" ,[SMKDeclarantPost]")
            'sql.Append(" ,[SMKReplyDataHead]")
            'sql.Append(" ,[SMKReplyDatetime]")
            'sql.Append(" ,[SMKReplyOfficer]")
            'sql.Append(" ,[SMKReplyODesignt]")
            'sql.Append(" ,[SMKReplyComments]")
            'sql.Append(" ,[SMKReplyStatus]")
            'sql.Append(" ,[SMKReplyAction]")
            'sql.Append(" ,[SMKReplyApprRef]")
            'sql.Append(" ,[SMKReplyRemarks]")
            'sql.Append(" ,[AG_ID]")
            'sql.Append(" ,[AGCode]")
            'sql.Append(" ,[SMKConsRefNo]")

            'sql.Append(" ,[SMKRemarks]")
            'sql.Append(" ,[SMKAccident]")

            'sql.Append(" ,[SMKPurpose]")
            sql.Append(" ,[EP_ID]")
            'sql.Append(" ,[WH_ID]")
            'sql.Append(" ,[SMKWHName]")
            'sql.Append(" ,[SMKWHAddr]")
            sql.Append(" ,[PStatus]")
            sql.Append(" ,[RStatus]")
            sql.Append(" ,[LMBY]")
            sql.Append(" ,[LMDT]")
            sql.Append(" ,[UCUSTOM]")

            sql.Append(" ,[SMKFilePath]")
            sql.Append(" ,[SMKFileContent]")

            sql.Append(" )")
            '----------------------------------------------------------------------------------
            sql.Append(" VALUES ")
            sql.Append(" (")
            '----------------------------------------------------------------------------------
            Dim SMKCID As Integer = GetAutonumberKey(SMKCFormTableName, "[SMK_ID]")
            'sql.Append(" " & SMKCID)
            'sql.Append(" [CFH_ID]")
            sql.Append(" '" & SMKTYPE & "'")
            sql.Append(" ,'" & SMKHEADER & "'")
            sql.Append(" ,'" & "A" & "'")
            sql.Append(" ,'" & "C" & "'")

            Try
                sql.Append(" ,'" & CInt(data.TransactionType) & "'") '[SMKTransacType]
            Catch ex As Exception
                sql.Append(" ,'" & 0 & "'") '[SMKTransacType]
            End Try

            'sql.Append(" ,'" & data.CustomFormNumber.Replace("-", "") & "'")
            sql.Append(" ,'" & data.CustomFormNumber & "'")
            'sql.Append(" ,'" & data.FQC_Preassigned_control_number & "'")
            sql.Append(" ,'" & FormValidDateTime(data.RegistrationDate, data.RegistrationTime).ToString(DATETIME_FORMAT) & "'")

            sql.Append(" ,'" & data.TotalNumberOfItem & "'")
            sql.Append(" ,'" & item.CommodityStatus & "'")

            Try
                sql.Append(" ,'" & CInt(data.TransactionType) & "'") 'SMKTransacType
            Catch ex As Exception
                sql.Append(" ,'" & 0 & "'") 'SMKTransacType
            End Try

            'sql.Append(" ,'" & data.ExporterCode & "'")
            sql.Append(" ,'" & data.ExporterName.Replace("'", "''") & "'")
            sql.Append(" ,'" & data.ExporterAddressStreetAndNumberPObox.Replace("'", "''") & "'") '-> Form Inline Address
            sql.Append(" ," & IM_ID_DEFAULT)
            'sql.Append(" ,[ImpCustRefNo]")
            sql.Append(" ,'" & data.ImporterCode & "'")
            sql.Append(" ,'" & data.ImporterName.Replace("'", "''") & "'")
            sql.Append(" ,'" & data.ImporterAddressStreetAndNumberPObox.Replace("'", "''") & "'") '-> Form Inline Address
            sql.Append(" ,'" & AG_REF_NO_DEFAULT & "'")
            sql.Append(" ,'" & data.AgentCode & "'")
            sql.Append(" ,'" & data.AgentName.Replace("'", "''") & "'")
            sql.Append(" ,'" & data.AgentAddressStreetAndNumberPObox.Replace("'", "''") & "'") '-> Form Inline Address
            sql.Append(" ,'" & data.ModeOfTransport & "'")
            sql.Append(" ,'" & FormValidDateTime(data.DateOfImport, "0000").ToString(DATETIME_FORMAT) & "'") 'sql.Append(" ,'" & data.DateOfImport & "'")'
            sql.Append(" ,'" & data.VesselRegistration & "'")
            sql.Append(" ,'" & data.VoyageNumber & "'")
            sql.Append(" ,'" & data.VesselName.Replace("'", "''") & "'")
            sql.Append(" ,'" & data.FlightNumber & "'")
            sql.Append(" ,'" & data.FlightDate & "'")
            'sql.Append(" ,'" & data.Vehicle_Lorry_number & "'")
            'sql.Append(" ,'" & data.Trailer_number & "'")
            sql.Append(" ,'" & data.PlaceOfImport & "'")
            sql.Append(" ,'" & data.PlaceOfLoading & "'")
            sql.Append(" ,'" & data.PortOfTransshipment & "'")
            sql.Append(" ,'" & data.Invoice.PayTo & "'")
            sql.Append(" ,'" & data.Invoice.Insurance & "'")
            sql.Append(" ,'" & data.Invoice.OtherCharges & "'")
            sql.Append(" ,'" & data.Invoice.CIF & "'")
            sql.Append(" ,'" & data.Invoice.FOB & "'")
            sql.Append(" ,'" & data.Invoice.Freight & "'")
            sql.Append(" ,'" & item.GrossWeightInKGS & "'")
            'sql.Append(" ,'" & data.number_of_Packages & "'")
            'sql.Append(" ,'" & data.Type_of_Packages & "'")
            'sql.Append(" ,'" & data.Measurement & "'")
            sql.Append(" ,'" & data.ConsignmentNote & "'")
            sql.Append(" ,'" & data.GeneralDescriptionOfGoods.Replace("'", "''") & "'")
            sql.Append(" ,'" & data.Marks & "'")

            sql.Append(" ,'" & data.ManifestRegistrationNumber & "'")
            'sql.Append(" ,'" & data.Import_Permit_number & "'")
            'sql.Append(" ,'" & data.Import_Permit_number_2 & "'")
            'sql.Append(" ,'" & data.Special_Treatement & "'")
            'sql.Append(" ,'" & data.Total_Duty_Payable & "'")



            sql.Append(" ,'" & data.DeclarantICNumber & "'")
            sql.Append(" ,'" & data.DeclarantName.Replace("'", "''") & "'")
            sql.Append(" ,'" & data.DeclarantStatus & "'")
            'sql.Append(" ,[SMKReplyDataHead]")
            'sql.Append(" ,[SMKReplyDatetime]")
            'sql.Append(" ,[SMKReplyOfficer]")
            'sql.Append(" ,[SMKReplyODesignt]")
            'sql.Append(" ,[SMKReplyComments]")
            'sql.Append(" ,[SMKReplyStatus]")
            'sql.Append(" ,[SMKReplyAction]")
            'sql.Append(" ,[SMKReplyApprRef]")
            'sql.Append(" ,[SMKReplyRemarks]")
            'sql.Append(" ,[AG_ID]")
            'sql.Append(" ,[AGCode]")
            'sql.Append(" ,[SMKConsRefNo]")

            'sql.Append(" ,'" & data.Remarks_and_Accident & "'") ' Remarks_and_Accident
            'sql.Append(" ,[SMKAccident]")

            'sql.Append(" ,'" & data.Purpose_of_import & "'")
            sql.Append(" ,'" & GetEntryPointID(data.getCustomStation) & "'") 'sql.Append(" ,'" & EP_ID_DEFAULT & "'")
            'sql.Append(" ,'" & data.Warehouse_Code & "'")
            'sql.Append(" ,'" & data.Warehouse_Name & "'")
            'sql.Append(" ,'" & data.Warehouse_Address & "'") '-> Form Inline Address

            sql.Append(" ,'N'")
            sql.Append(" ,'2'")
            sql.Append(" ,'" & UPDATEDBY & "'")
            sql.Append(" ,'" & UPDATEDDATE & "'")
            sql.Append(" ,'Y'")

            If data.Attachments.Count > 0 Then
                For Each att In data.Attachments
                    sql.Append(" ,'" & att.FilePath & "'")
                    sql.Append(" ,'" & att.FileContent & "'")
                    Exit For 'TABLE CURRENTLY ACCEPT 1 ATTACHMENT
                Next
            End If

            '----------------------------------------------------------------------------------
            sql.Append(" )")

            '===================================================================================

            'test.AppendLine(" " & SMKCID)
            'test.AppendLine(" [CFH_ID]")
            test.AppendLine(" [SMKType] = '" & SMKTYPE & "'")
            test.AppendLine(" [SMKDataHead]='" & SMKHEADER & "'")
            test.AppendLine(" [SMKMsgType]='" & "A" & "'")
            test.AppendLine(" [SMKMsgMode]='" & "C" & "'")
            test.AppendLine(" [SMKImpIndicator]='" & data.TransactionType & "'")
            test.AppendLine(" [SMKRefNum]='" & data.CustomFormNumber & "'")
            'test.AppendLine(" ,'" & data.FQC_Preassigned_control_number & "'")
            test.AppendLine(" [SMKRegDatetime]='" & FormValidDateTime(data.RegistrationDate, data.RegistrationTime).ToString(DATETIME_FORMAT) & "'")

            test.AppendLine(" [SMKTotalItems]='" & data.TotalNumberOfItem & "'")
            test.AppendLine(" [SMKCommodityStatus]='" & item.CommodityStatus & "'")

            test.AppendLine(" [SMKTransacType]='" & data.TransactionType & "'")
            'test.AppendLine(" ,'" & data.ExporterCode & "'")
            test.AppendLine(" [SMKExpName]='" & data.ExporterName.Replace("'", "''") & "'")
            test.AppendLine(" [SMKExpAddr]='" & data.ExporterAddressStreetAndNumberPObox & "'") '-> Form Inline Address
            test.AppendLine(" [IMP_Id]" & IM_ID_DEFAULT)
            'test.AppendLine(" ,[ImpCustRefNo]")
            test.AppendLine(" [SMKImpCode]='" & data.ImporterCode & "'")
            test.AppendLine(" [SMKImpName]'" & data.ImporterName & "'")
            test.AppendLine(" [SMKImpAddr]='" & data.ImporterAddressStreetAndNumberPObox & "'") '-> Form Inline Address
            test.AppendLine(" [AGCustRefNo]='" & AG_REF_NO_DEFAULT & "'")
            test.AppendLine(" [SMKAgentCode]='" & data.AgentCode & "'")
            test.AppendLine(" [SMKAgentName]='" & data.AgentName & "'")
            test.AppendLine(" [SMKAgentAddr]='" & data.AgentAddressStreetAndNumberPObox & "'") '-> Form Inline Address
            test.AppendLine(" [SMKTransptMode]='" & data.ModeOfTransport & "'")
            test.AppendLine(" [SMKImpDate]='" & FormValidDateTime(data.DateOfImport, "0000").ToString(DATETIME_FORMAT) & "'")
            test.AppendLine(" [SMKVesselReg]='" & data.VesselRegistration & "'")
            test.AppendLine(" [SMKVoyageNum]='" & data.VoyageNumber & "'")
            test.AppendLine(" [SMKVesselName]='" & data.VesselName & "'")
            test.AppendLine(" [SMKFlightNum]='" & data.FlightNumber & "'")
            test.AppendLine(" [SMKFlightDate]='" & data.FlightDate & "'")
            'test.AppendLine(" ,'" & data.Vehicle_Lorry_number & "'")
            'test.AppendLine(" ,'" & data.Trailer_number & "'")
            test.AppendLine(" [SMKImpPlace]='" & data.PlaceOfImport & "'")
            test.AppendLine(" [SMKLoadPlace]='" & data.PlaceOfLoading & "'")
            test.AppendLine(" [SMKTranshipmtPort]='" & data.PortOfTransshipment & "'")
            test.AppendLine(" [SMKPayTo]='" & data.Invoice.PayTo & "'")
            test.AppendLine(" [SMKInsurance]='" & data.Invoice.Insurance & "'")
            test.AppendLine(" [SMKOthCharges]='" & data.Invoice.OtherCharges & "'")
            test.AppendLine(" [SMKCIF]='" & data.Invoice.CIF & "'")
            test.AppendLine(" [SMKFOB]='" & data.Invoice.FOB & "'")
            test.AppendLine(" [SMKFreight]='" & data.Invoice.Freight & "'")
            test.AppendLine(" [SMKGrossWeight]='" & item.GrossWeightInKGS & "'")
            'test.AppendLine(" ,'" & data.number_of_Packages & "'")
            'test.AppendLine(" ,'" & data.Type_of_Packages & "'")
            'test.AppendLine(" ,'" & data.Measurement & "'")
            test.AppendLine(" [SMKConsignmtNote]='" & data.ConsignmentNote & "'")
            test.AppendLine(" [SMKGenGoodsDesc]='" & data.GeneralDescriptionOfGoods.Replace("'", "''") & "'")
            test.AppendLine(" [SMKMarksCtnNum]='" & data.Marks & "'")

            test.AppendLine(" [SMKManifestNum]='" & data.ManifestRegistrationNumber & "'")
            'test.AppendLine(" ,'" & data.Import_Permit_number & "'")
            'test.AppendLine(" ,'" & data.Import_Permit_number_2 & "'")
            'test.AppendLine(" ,'" & data.Special_Treatement & "'")
            'test.AppendLine(" ,'" & data.Total_Duty_Payable & "'")



            test.AppendLine(" [SMKDeclarantICPP]='" & data.DeclarantICNumber & "'")
            test.AppendLine(" [SMKDeclarantName]='" & data.DeclarantName & "'")
            test.AppendLine(" [SMKDeclarantPost]='" & data.DeclarantStatus & "'")
            'test.AppendLine(" ,[SMKReplyDataHead]")
            'test.AppendLine(" ,[SMKReplyDatetime]")
            'test.AppendLine(" ,[SMKReplyOfficer]")
            'test.AppendLine(" ,[SMKReplyODesignt]")
            'test.AppendLine(" ,[SMKReplyComments]")
            'test.AppendLine(" ,[SMKReplyStatus]")
            'test.AppendLine(" ,[SMKReplyAction]")
            'test.AppendLine(" ,[SMKReplyApprRef]")
            'test.AppendLine(" ,[SMKReplyRemarks]")
            'test.AppendLine(" ,[AG_ID]")
            'test.AppendLine(" ,[AGCode]")
            'test.AppendLine(" ,[SMKConsRefNo]")

            'test.AppendLine(" ,'" & data.Remarks_and_Accident & "'") ' Remarks_and_Accident
            'test.AppendLine(" ,[SMKAccident]")

            'test.AppendLine(" ,'" & data.Purpose_of_import & "'")
            test.AppendLine(" [EP_ID]='" & EP_ID_DEFAULT & "'")
            'test.AppendLine(" ,'" & data.Warehouse_Code & "'")
            'test.AppendLine(" ,'" & data.Warehouse_Name & "'")
            'test.AppendLine(" ,'" & data.Warehouse_Address & "'") '-> Form Inline Address

            test.AppendLine(" [PStatus]='N'")
            test.AppendLine(" [RStatus]='2'")
            test.AppendLine(" [LMBY]='" & UPDATEDBY & "'")
            test.AppendLine(" [LMDT]='" & UPDATEDDATE & "'")
            test.AppendLine(" [UCUSTOM]='Y'")


            '====================================================================================
            Try
                'MsgBox(sql.ToString)
                'nEventLOG(sql.ToString)
                If Not SMKFORM_CTRL Then ' ONLY SINGLE ENTRY IN SMKFORM TABLE
                    SMKFORM_CTRL = True
                    result = result + ExecuteQuery(sql.ToString)
                End If

            Catch ex As Exception
                Throw ex
            End Try

            sql.Remove(0, sql.Length)
            '==================================================================================



            sql.Append("INSERT INTO " & SMKCFormGoodsTableName)
            '----------------------------------------------------------------------------------
            sql.Append(" (")
            'sql.Append(" [CFG_ID]") 'Autonumber
            sql.Append(" [SMK_ID]")
            sql.Append(" ,[CFGDataHead]")
            sql.Append(" ,[CFGK1RegNum]")
            sql.Append(" ,[CFGItemNum]")
            sql.Append(" ,[CFGItemDesc]")
            sql.Append(" ,[CFGHSCode]")
            sql.Append(" ,[CFGQuantity]")
            sql.Append(" ,[CFGUOM]")
            sql.Append(" ,[CFGUnitPrice]")
            sql.Append(" ,[CFGTotalPrice]")
            sql.Append(" ,[CFGImpDutyAmt]")
            sql.Append(" ,[CFGNoOfPackages]")
            'sql.Append(" ,[CFGPackageType]")
            sql.Append(" ,[CFGOriginCtry]")
            'sql.Append(" ,[CFGQuantity1]")
            'sql.Append(" ,[CFGUOM1]")

            'sql.Append(" ,[CFGIMGFoodCode]")
            'sql.Append(" ,[CFGIMGTreatment]")
            'sql.Append(" ,[CFGMNF_ID]")
            'sql.Append(" ,[CFGIMGmnfName]")
            'sql.Append(" ,[CFGIMGmnfAddr]")
            'sql.Append(" ,[CFGIMGBrand]")
            'sql.Append(" ,[CFGIMGDateProduce]")
            'sql.Append(" ,[CFGIMGDateExpiry]")

            'sql.Append(" ,[CFGIMGBatchNum]")
            'sql.Append(" ,[CFGIMGBarcode]")
            'sql.Append(" ,[CFGGoodsDesc]")
            sql.Append(" ,[CFGCertIds]")
            'sql.Append(" ,[CFGCertNo1]")
            'sql.Append(" ,[CFGCertNo2]")
            'sql.Append(" ,[CFGCertNo3]")
            'sql.Append(" ,[CFGCertNo4]")
            'sql.Append(" ,[CFGCertNo5]")
            'sql.Append(" ,[CFGCertNo6]")
            'sql.Append(" ,[CFGCertNo7]")
            'sql.Append(" ,[CFGCertNo8]")
            sql.Append(" ,[CFGIMGCurrLvl]")
            sql.Append(" ,[PStatus]")
            'sql.Append(" ,[CFGStatusPurpose]")
            'sql.Append(" ,[CFGAGNotes]")
            sql.Append(" ,[RStatus]")
            sql.Append(" ,[LMBY]")
            sql.Append(" ,[LMDT]")
            'sql.Append(" ,[Remarks]")

            '========
            sql.Append(" ,[CFGGrossWeight]")
            sql.Append(" ,[CFGImpPermitNo]")
            sql.Append(" ,[CFGPurposeImp]")
            sql.Append(" ,[CFGWarehouseCode]")
            sql.Append(" ,[CFGWarehouseName]")
            sql.Append(" ,[CFGWarehouseAdd]")
            sql.Append(" ,[CFGExpCode]")
            sql.Append(" ,[CFGIMGBrand]")
            sql.Append(" ,[CFGIMGDateProduce]")
            sql.Append(" ,[CFGIMGDateExpiry]")
            sql.Append(" ,[CFGTreatment]")
            sql.Append(" ,[CFGManuCode]")
            sql.Append(" ,[CFGIMGmnfName]")
            sql.Append(" ,[CFGIMGmnfAddr]")

            sql.Append(" ,[CFGPreImpRegNo]")

            sql.Append(" ,[CFGDistributorName]")
            sql.Append(" ,[CFGDistributorAdd]")

            sql.Append(" ,[CFGCommodityStatus]")
            '========
            sql.Append(" )")
            '----------------------------------------------------------------------------------
            sql.Append(" VALUES ")
            sql.Append(" (")
            '----------------------------------------------------------------------------------
            'sql.Append(" " & cmmn.GetAutonumberKey(SMKCFormGoodsTableName, "[CFG_ID]"))
            SMKCID = GetAutonumberKey(SMKCFormTableName, "[SMK_ID]", "[SMKRefNum]='" & data.CustomFormNumber & "'") '20171004
            sql.Append(" " & SMKCID)
            sql.Append(" ,'" & SMKHEADER & "'")
            sql.Append(" ,'" & data.CustomFormNumber & "'")
            sql.Append(" ,'" & item.ItemNumber & "'")
            sql.Append(" ,'" & item.ItemDescription & "'")
            sql.Append(" ,'" & item.HSCode & "'")
            sql.Append(" ,'" & item.DeclaredQuantity & "'")
            sql.Append(" ,'" & item.DeclaredUnit & "'")
            sql.Append(" ,'" & item.UnitPrice & "'")
            sql.Append(" ,'" & item.TotalPrice & "'")
            sql.Append(" ,'" & item.DutyAmount & "'")
            sql.Append(" ,'" & 0 & "'") 'CFGNoOfPackages ??

            'sql.Append(" ,'" & data.Type_of_Packages_B & "'")
            sql.Append(" ,'" & item.CountryOfOrigin & "'")
            'sql.Append(" ,'" & data.Declared_Quantity_2 & "'")
            'sql.Append(" ,'" & data.Declared_unit_2 & "'")

            'sql.Append(" ,'" & data.Food_Code & "'")
            'sql.Append(" ,'" & data.Treatment & "'")
            'sql.Append(" ,'" & data.Manufacturer_code & "'")
            'sql.Append(" ,'" & data.Manufacturer_name & "'")
            'sql.Append(" ,'" & data.Manufacturer_address & "'") '-> Form Inline Address
            'sql.Append(" ,'" & data.Brand & "'")
            'sql.Append(" ,'" & data.Date_of_production & "'")
            'sql.Append(" ,'" & data.Date_of_expire & "'")

            'sql.Append(" ,[CFGIMGBatchNum]")
            'sql.Append(" ,[CFGIMGBarcode]")
            'sql.Append(" ,[CFGGoodsDesc]")
            sql.Append(" ,'" & CERT_ID_DEFAULT & "'")
            'sql.Append(" ,[CFGCertNo1]")
            'sql.Append(" ,[CFGCertNo2]")
            'sql.Append(" ,[CFGCertNo3]")
            'sql.Append(" ,[CFGCertNo4]")
            'sql.Append(" ,[CFGCertNo5]")
            'sql.Append(" ,[CFGCertNo6]")
            'sql.Append(" ,[CFGCertNo7]")
            'sql.Append(" ,[CFGCertNo8]")
            sql.Append(" ," & 3) 'default Level
            sql.Append(" ,'N'")
            'sql.Append(" ,[CFGStatusPurpose]")
            'sql.Append(" ,[CFGAGNotes]")
            sql.Append(" ,'2'")
            sql.Append(" ,'" & UPDATEDBY & "'")
            sql.Append(" ,'" & UPDATEDDATE & "'")
            'sql.Append(" ,[Remarks]")

            '========
            'CFGGrossWeight
            sql.Append(" ,'" & item.GrossWeightInKGS & "'")
            If item.Permits.Count > 0 Then
                For Each permit In item.Permits
                    'CFGImpPermitNo
                    sql.Append(" ,'" & permit.ImportPermitNumber & "'")
                    Exit For
                Next
            Else
                sql.Append(" ,''") 'NULL
            End If
            If item.Specifications.Count > 0 Then
                For Each spec In item.Specifications
                    'CFGPurposeImp
                    sql.Append(" ,'" & spec.PurposeOfImport & "'")
                    'CFGWarehouseCode
                    sql.Append(" ,'" & spec.WarehouseCode & "'")
                    'CFGWarehouseName
                    sql.Append(" ,'" & spec.WarehouseName & "'")
                    'CFGWarehouseAdd
                    sql.Append(" ,'" & spec.WarehouseAddress & "'")
                    'CFGExpCode
                    sql.Append(" ,'" & spec.ExporterCode & "'")
                    'CFGIMGBrand
                    sql.Append(" ,'" & spec.Brand & "'")
                    'CFGIMGDateProduce
                    Dim s As Date = CDate(spec.DateOfProduction)
                    sql.Append(" ,'" & s.ToString("yyyy-MM-dd") & "'") 'dd-mm-yyyy
                    'CFGIMGDateExpiry
                    s = CDate(spec.DateOfExpire)
                    sql.Append(" ,'" & s.ToString("yyyy-MM-dd") & "'") 'dd-mm-yyyy
                    'CFGTreatment
                    sql.Append(" ,'" & spec.Treatment & "'")
                    'CFGManuCode
                    sql.Append(" ,'" & spec.ManufacturerCode & "'")
                    'CFGIMGmnfName
                    sql.Append(" ,'" & spec.ManufacturerName & "'")
                    'CFGIMGmnfAddr
                    sql.Append(" ,'" & spec.ManufacturerAddress & "'")
                    'CFGPreImpRegNo
                    sql.Append(" ,'" & spec.PreImportRegistrationNo & "'")
                    Exit For
                Next
            Else
                sql.Append(" ,''") 'NULL
                sql.Append(" ,''") 'NULL
                sql.Append(" ,''") 'NULL
                sql.Append(" ,''") 'NULL
                sql.Append(" ,''") 'NULL
                sql.Append(" ,''") 'NULL
                sql.Append(" ,'" & FormValidDateTime("19700101", "0001") & "'") 'NULL datetime
                sql.Append(" ,'" & FormValidDateTime("19700101", "0001") & "'") 'NULL datetime
                sql.Append(" ,''") 'NULL
                sql.Append(" ,''") 'NULL
                sql.Append(" ,''") 'NULL
                sql.Append(" ,''") 'NULL
                sql.Append(" ,''") 'NULL
            End If
            
            'CFGDistributorName
            sql.Append(" ,''") 'NULL
            'CFGDistributorAdd
            sql.Append(" ,''") 'NULL
            'CFGCommodityStatus
            sql.Append(" ,'" & item.CommodityStatus & "'")
            '========
            '----------------------------------------------------------------------------------
            sql.Append(" )")

            Try
                'MsgBox(sql.ToString)
                'nEventLOG(sql.ToString)
                result = result + ExecuteQuery(sql.ToString)

                'Dim command As System.Data.Common.DbCommand = db.GetSqlStringCommand(sql.ToString)

                'db.EndExecuteNonQuery(command)

                'command.Connection.Close()
                'command.Dispose()
            Catch ex As Exception
                Throw ex
            End Try

            If result > 0 And TMPPARTY_CTRL = False Then
                TMPPARTY_CTRL = True
                InsertParty(data)
            End If

            'Missing items by hazree
            ' CFGGrossWeight >
            ' CFGImpPermitNo > permit
            ' CFGPurposeImp
            ' CFGWarehouseCode
            ' CFGWarehouseName
            ' CFGWarehouseAdd
            ' CFGExpCode
            ' CFGFoodCode
            ' CFGIMGBrand
            ' CFGIMGDateProduce
            ' CFGIMGDateExpiry
            ' CFGTreatment
            ' CFGManuCode
            ' CFGIMGmnfName
            ' CFGIMGmnfAddr
            ' CFGDistributorName
            ' CFGDistributorAdd
            ' CFGPreImpRegNo
            ' CFGCommodityStatus >


        Next

        Return result

    End Function

    Public Function CheckBatchID(ByVal BatchID As String) As Boolean
        Dim Result As Boolean = False
        Try
            Dim SQL As String = "SELECT count(1) as COUNTER FROM TmpParty WHERE BATCHID='" & BatchID & "';"
            Dim dt As DataTable = db.ExecuteDataSet(System.Data.CommandType.Text, SQL).Tables(0)
            If dt.Rows.Count > 0 Then
                If dt.Rows(0).Item("COUNTER") > 0 Then
                    Result = True
                Else
                    Result = False
                End If
            Else
                Result = False
            End If
        Catch ex As Exception
            Result = False
        End Try
        Return Result
    End Function

    Public Function CAInsert(ByVal data As DataExchangeClass.deprecating.ConsigmentApprovalRequest) As Integer

        Dim result As Integer = 0

        UPDATEDDATE = Now.ToString(DataExchangeClass.Common.DEFAULT_SQL_DATA_FORMAT)
        Dim sql As New System.Text.StringBuilder

        sql.Append("INSERT INTO " & SMKCFormTableName)
        '----------------------------------------------------------------------------------
        sql.Append(" (")
        'sql.Append(" [SMK_ID] ") 'Table Key - check stored procedure
        'sql.Append(" [CFH_ID]")
        sql.Append(" [SMKType]")
        sql.Append(" ,[SMKDataHead]")
        sql.Append(" ,[SMKMsgType]")
        sql.Append(" ,[SMKMsgMode]")
        sql.Append(" ,[SMKImpIndicator]")
        sql.Append(" ,[SMKRefNum]")
        sql.Append(" ,[SMKFQCRefNum]")
        sql.Append(" ,[SMKRegDatetime]")
        sql.Append(" ,[SMKTotalItems]")
        sql.Append(" ,[SMKCommodityStatus]")
        sql.Append(" ,[SMKTransacType]")
        sql.Append(" ,[EXP_ID]")
        sql.Append(" ,[SMKExpName]")
        sql.Append(" ,[SMKExpAddr]")
        sql.Append(" ,[IMP_Id]")
        'sql.Append(" ,[ImpCustRefNo]")
        sql.Append(" ,[SMKImpCode]")
        sql.Append(" ,[SMKImpName]")
        sql.Append(" ,[SMKImpAddr]")
        sql.Append(" ,[AGCustRefNo]")
        sql.Append(" ,[SMKAgentCode]")
        sql.Append(" ,[SMKAgentName]")
        sql.Append(" ,[SMKAgentAddr]")
        sql.Append(" ,[SMKTransptMode]")
        sql.Append(" ,[SMKImpDate]")
        sql.Append(" ,[SMKVesselReg]")
        sql.Append(" ,[SMKVoyageNum]")
        sql.Append(" ,[SMKVesselName]")
        sql.Append(" ,[SMKFlightNum]")
        sql.Append(" ,[SMKFlightDate]")
        sql.Append(" ,[SMKVechicleNum]")
        sql.Append(" ,[SMKTrailerNum]")
        sql.Append(" ,[SMKImpPlace]")
        sql.Append(" ,[SMKLoadPlace]")
        sql.Append(" ,[SMKTranshipmtPort]")
        sql.Append(" ,[SMKPayTo]")
        sql.Append(" ,[SMKInsurance]")
        sql.Append(" ,[SMKOthCharges]")
        sql.Append(" ,[SMKCIF]")
        sql.Append(" ,[SMKFOB]")
        sql.Append(" ,[SMKFreight]")
        sql.Append(" ,[SMKGrossWeight]")
        sql.Append(" ,[SMKNoOfPackages]")
        sql.Append(" ,[SMKPackageType]")
        sql.Append(" ,[SMKMeasurement]")
        sql.Append(" ,[SMKConsignmtNote]")
        sql.Append(" ,[SMKGenGoodsDesc]")
        sql.Append(" ,[SMKMarksCtnNum]")
        sql.Append(" ,[SMKManifestNum]")
        sql.Append(" ,[SMKPermitNum1]")
        sql.Append(" ,[SMKPermitNum2]")
        sql.Append(" ,[SMKSpecialTreat]")
        sql.Append(" ,[SMKTotalImpDutyAmt]")
        sql.Append(" ,[SMKDeclarantICPP]")
        sql.Append(" ,[SMKDeclarantName]")
        sql.Append(" ,[SMKDeclarantPost]")
        'sql.Append(" ,[SMKReplyDataHead]")
        'sql.Append(" ,[SMKReplyDatetime]")
        'sql.Append(" ,[SMKReplyOfficer]")
        'sql.Append(" ,[SMKReplyODesignt]")
        'sql.Append(" ,[SMKReplyComments]")
        'sql.Append(" ,[SMKReplyStatus]")
        'sql.Append(" ,[SMKReplyAction]")
        'sql.Append(" ,[SMKReplyApprRef]")
        'sql.Append(" ,[SMKReplyRemarks]")
        'sql.Append(" ,[AG_ID]")
        'sql.Append(" ,[AGCode]")
        'sql.Append(" ,[SMKConsRefNo]")
        sql.Append(" ,[SMKRemarks]")
        'sql.Append(" ,[SMKAccident]")
        sql.Append(" ,[SMKPurpose]")
        sql.Append(" ,[EP_ID]")
        sql.Append(" ,[WH_ID]")
        sql.Append(" ,[SMKWHName]")
        sql.Append(" ,[SMKWHAddr]")
        sql.Append(" ,[PStatus]")
        sql.Append(" ,[RStatus]")
        sql.Append(" ,[LMBY]")
        sql.Append(" ,[LMDT]")
        sql.Append(" ,[UCUSTOM]")
        sql.Append(" )")
        '----------------------------------------------------------------------------------
        sql.Append(" VALUES ")
        sql.Append(" (")
        '----------------------------------------------------------------------------------
        Dim SMKCID As Integer = GetAutonumberKey(SMKCFormTableName, "[SMK_ID]")
        'sql.Append(" " & SMKCID)
        'sql.Append(" [CFH_ID]")
        sql.Append(" '" & SMKTYPE & "'")
        sql.Append(" ,'" & data.Data_Header & "'")
        sql.Append(" ,'" & data.Message_Type & "'")
        sql.Append(" ,'" & data.Message_Mode & "'")
        sql.Append(" ,'" & data.Import_Indicator & "'")
        sql.Append(" ,'" & data.Custom_Form_number & "'")
        sql.Append(" ,'" & data.FQC_Preassigned_control_number & "'")
        sql.Append(" ,'" & data.Registration_Date & "'")
        sql.Append(" ,'" & data.Total_number_of_Item & "'")
        sql.Append(" ,'" & data.Commodity_Status & "'")
        sql.Append(" ,'" & data.Transaction_Type & "'")
        sql.Append(" ,'" & data.Exporter_Code & "'")
        sql.Append(" ,'" & data.Exporter_name & "'")
        sql.Append(" ,'" & data.Exporter_Address & "'") '-> Form Inline Address
        sql.Append(" ," & IM_ID_DEFAULT)
        'sql.Append(" ,[ImpCustRefNo]")
        sql.Append(" ,'" & data.Importer_Code & "'")
        sql.Append(" ,'" & data.Importer_Name & "'")
        sql.Append(" ,'" & data.Importer_Address & "'") '-> Form Inline Address
        sql.Append(" ,'" & AG_REF_NO_DEFAULT & "'")
        sql.Append(" ,'" & data.Agent_Code & "'")
        sql.Append(" ,'" & data.Agent_Name & "'")
        sql.Append(" ,'" & data.Agent_Address & "'") '-> Form Inline Address
        sql.Append(" ,'" & data.Mode_of_Transport & "'")
        sql.Append(" ,'" & data.Date_of_Import & "'")
        sql.Append(" ,'" & data.Vessel_Registration & "'")
        sql.Append(" ,'" & data.Voyage_number & "'")
        sql.Append(" ,'" & data.Vessel_name & "'")
        sql.Append(" ,'" & data.Flight_number & "'")
        sql.Append(" ,'" & data.Flight_Date & "'")
        sql.Append(" ,'" & data.Vehicle_Lorry_number & "'")
        sql.Append(" ,'" & data.Trailer_number & "'")
        sql.Append(" ,'" & data.Place_of_Import & "'")
        sql.Append(" ,'" & data.Place_of_Loading & "'")
        sql.Append(" ,'" & data.Port_of_Transhipment & "'")
        sql.Append(" ,'" & data.Pay_To & "'")
        sql.Append(" ,'" & data.Insurance & "'")
        sql.Append(" ,'" & data.Other_Charges & "'")
        sql.Append(" ,'" & data.CIF & "'")
        sql.Append(" ,'" & data.FOB & "'")
        sql.Append(" ,'" & data.Freight & "'")
        sql.Append(" ,'" & data.Gross_Weight & "'")
        sql.Append(" ,'" & data.number_of_Packages & "'")
        sql.Append(" ,'" & data.Type_of_Packages & "'")
        sql.Append(" ,'" & data.Measurement & "'")
        sql.Append(" ,'" & data.Consignment_Note & "'")
        sql.Append(" ,'" & data.General_description_of_Goods & "'")
        sql.Append(" ,'" & data.Marks & "'")
        sql.Append(" ,'" & data.Manifest_Registration_number & "'")
        sql.Append(" ,'" & data.Import_Permit_number & "'")
        sql.Append(" ,'" & data.Import_Permit_number_2 & "'")
        sql.Append(" ,'" & data.Special_Treatement & "'")
        sql.Append(" ,'" & data.Total_Duty_Payable & "'")
        sql.Append(" ,'" & data.Declarant_IC_number & "'")
        sql.Append(" ,'" & data.Declarant_Name & "'")
        sql.Append(" ,'" & data.Declarant_Status & "'")
        'sql.Append(" ,[SMKReplyDataHead]")
        'sql.Append(" ,[SMKReplyDatetime]")
        'sql.Append(" ,[SMKReplyOfficer]")
        'sql.Append(" ,[SMKReplyODesignt]")
        'sql.Append(" ,[SMKReplyComments]")
        'sql.Append(" ,[SMKReplyStatus]")
        'sql.Append(" ,[SMKReplyAction]")
        'sql.Append(" ,[SMKReplyApprRef]")
        'sql.Append(" ,[SMKReplyRemarks]")
        'sql.Append(" ,[AG_ID]")
        'sql.Append(" ,[AGCode]")
        'sql.Append(" ,[SMKConsRefNo]")
        sql.Append(" ,'" & data.Remarks_and_Accident & "'")
        'sql.Append(" ,[SMKAccident]")
        sql.Append(" ,'" & data.Purpose_of_import & "'")
        sql.Append(" ,'" & EP_ID_DEFAULT & "'")
        sql.Append(" ,'" & data.Warehouse_Code & "'")
        sql.Append(" ,'" & data.Warehouse_Name & "'")
        sql.Append(" ,'" & data.Warehouse_Address & "'") '-> Form Inline Address
        sql.Append(" ,'N'")
        sql.Append(" ,'2'")
        sql.Append(" ,'" & UPDATEDBY & "'")
        sql.Append(" ,'" & UPDATEDDATE & "'")
        sql.Append(" ,'Y'")
        '----------------------------------------------------------------------------------
        sql.Append(" )")

        'MsgBox(sql.ToString)
        nEventLOG(sql.ToString)
        result = result + ExecuteQuery(sql.ToString)

        sql.Remove(0, sql.Length)
        '==================================================================================

        sql.Append("INSERT INTO " & SMKCFormGoodsTableName)
        '----------------------------------------------------------------------------------
        sql.Append(" (")
        'sql.Append(" [CFG_ID]") 'Autonumber
        sql.Append(" [SMK_ID]")
        sql.Append(" ,[CFGDataHead]")
        sql.Append(" ,[CFGK1RegNum]")
        sql.Append(" ,[CFGItemNum]")
        sql.Append(" ,[CFGItemDesc]")
        sql.Append(" ,[CFGHSCode]")
        sql.Append(" ,[CFGQuantity]")
        sql.Append(" ,[CFGUOM]")
        sql.Append(" ,[CFGUnitPrice]")
        sql.Append(" ,[CFGTotalPrice]")
        sql.Append(" ,[CFGImpDutyAmt]")
        sql.Append(" ,[CFGNoOfPackages]")
        sql.Append(" ,[CFGPackageType]")
        sql.Append(" ,[CFGOriginCtry]")
        sql.Append(" ,[CFGQuantity1]")
        sql.Append(" ,[CFGUOM1]")
        sql.Append(" ,[CFGIMGFoodCode]")
        sql.Append(" ,[CFGIMGTreatment]")
        sql.Append(" ,[CFGMNF_ID]")
        sql.Append(" ,[CFGIMGmnfName]")
        sql.Append(" ,[CFGIMGmnfAddr]")
        sql.Append(" ,[CFGIMGBrand]")
        sql.Append(" ,[CFGIMGDateProduce]")
        sql.Append(" ,[CFGIMGDateExpiry]")
        'sql.Append(" ,[CFGIMGBatchNum]")
        'sql.Append(" ,[CFGIMGBarcode]")
        'sql.Append(" ,[CFGGoodsDesc]")
        sql.Append(" ,[CFGCertIds]")
        'sql.Append(" ,[CFGCertNo1]")
        'sql.Append(" ,[CFGCertNo2]")
        'sql.Append(" ,[CFGCertNo3]")
        'sql.Append(" ,[CFGCertNo4]")
        'sql.Append(" ,[CFGCertNo5]")
        'sql.Append(" ,[CFGCertNo6]")
        'sql.Append(" ,[CFGCertNo7]")
        'sql.Append(" ,[CFGCertNo8]")
        sql.Append(" ,[CFGIMGCurrLvl]")
        sql.Append(" ,[PStatus]")
        'sql.Append(" ,[CFGStatusPurpose]")
        'sql.Append(" ,[CFGAGNotes]")
        sql.Append(" ,[RStatus]")
        sql.Append(" ,[LMBY]")
        sql.Append(" ,[LMDT]")
        'sql.Append(" ,[Remarks]")
        sql.Append(" )")
        '----------------------------------------------------------------------------------
        sql.Append(" VALUES ")
        sql.Append(" (")
        '----------------------------------------------------------------------------------
        'sql.Append(" " & cmmn.GetAutonumberKey(SMKCFormGoodsTableName, "[CFG_ID]"))
        sql.Append(" " & SMKCID)
        sql.Append(" ,'" & data.Data_Header_B & "'")
        sql.Append(" ,'" & data.Custom_Form_number_B & "'")
        sql.Append(" ,'" & data.Item_number & "'")
        sql.Append(" ,'" & data.Item_Description & "'")
        sql.Append(" ,'" & data.HS_code & "'")
        sql.Append(" ,'" & data.Declared_Quantity_1 & "'")
        sql.Append(" ,'" & data.Declared_unit_1 & "'")
        sql.Append(" ,'" & data.Unit_price & "'")
        sql.Append(" ,'" & data.Total_price & "'")
        sql.Append(" ,'" & data.Duty_Amount_B & "'")
        sql.Append(" ,'" & data.number_of_Packages_B & "'")
        sql.Append(" ,'" & data.Type_of_Packages_B & "'")
        sql.Append(" ,'" & data.Country_of_Origin & "'")
        sql.Append(" ,'" & data.Declared_Quantity_2 & "'")
        sql.Append(" ,'" & data.Declared_unit_2 & "'")
        sql.Append(" ,'" & data.Food_Code & "'")
        sql.Append(" ,'" & data.Treatment & "'")
        sql.Append(" ,'" & data.Manufacturer_code & "'")
        sql.Append(" ,'" & data.Manufacturer_name & "'")
        sql.Append(" ,'" & data.Manufacturer_address & "'") '-> Form Inline Address
        sql.Append(" ,'" & data.Brand & "'")
        sql.Append(" ,'" & data.Date_of_production & "'")
        sql.Append(" ,'" & data.Date_of_expire & "'")
        'sql.Append(" ,[CFGIMGBatchNum]")
        'sql.Append(" ,[CFGIMGBarcode]")
        'sql.Append(" ,[CFGGoodsDesc]")
        sql.Append(" ,'" & CERT_ID_DEFAULT & "'")
        'sql.Append(" ,[CFGCertNo1]")
        'sql.Append(" ,[CFGCertNo2]")
        'sql.Append(" ,[CFGCertNo3]")
        'sql.Append(" ,[CFGCertNo4]")
        'sql.Append(" ,[CFGCertNo5]")
        'sql.Append(" ,[CFGCertNo6]")
        'sql.Append(" ,[CFGCertNo7]")
        'sql.Append(" ,[CFGCertNo8]")
        sql.Append(" ," & 3) 'default Level
        sql.Append(" ,'N'")
        'sql.Append(" ,[CFGStatusPurpose]")
        'sql.Append(" ,[CFGAGNotes]")
        sql.Append(" ,'2'")
        sql.Append(" ,'" & UPDATEDBY & "'")
        sql.Append(" ,'" & UPDATEDDATE & "'")
        'sql.Append(" ,[Remarks]")
        '----------------------------------------------------------------------------------
        sql.Append(" )")


        'MsgBox(sql.ToString)
        nEventLOG(sql.ToString)
        result = result + ExecuteQuery(sql.ToString)

        'Dim command As System.Data.Common.DbCommand = db.GetSqlStringCommand(sql.ToString)

        'db.EndExecuteNonQuery(command)

        'command.Connection.Close()
        'command.Dispose()

        Return result

    End Function

    Private Function GetAutonumberKey(ByVal TableName As String, ByVal TableCol As String, Optional Filter As String = Nothing) As Integer
        Dim number As Integer = 1
        Dim sql As String = _
        "SELECT MAX(" & TableCol & ")  AS [MAX] FROM " & TableName & IIf(Filter = Nothing, "", " WHERE " & Filter)

        Dim command As System.Data.Common.DbCommand = db.GetSqlStringCommand(sql)
        Try
            number = db.ExecuteDataSet(command).Tables(0).Rows(0).Item("MAX")

            Return IIf(Filter = Nothing, number + 1, number)

        Catch ex As Exception
            RaiseEvent OnError(Now, New Exception("GetAutonumberKey: " & ex.Message))
            Return number
        Finally
            command.Connection.Close()
            command.Dispose()
        End Try
    End Function

    Private Function GetEntryPointID(ByVal CustomStation As String) As Integer
        Dim number As Integer = 999
        Dim sql As String = _
        "SELECT EP_ID FROM ADMEntryPoint WHERE STT_ID = (SELECT STT_ID FROM CODStation WHERE STTCode = '" & CustomStation & "' AND RStatus = '2') AND RStatus = '2';"

        Dim command As System.Data.Common.DbCommand = db.GetSqlStringCommand(sql)
        Try
            number = db.ExecuteDataSet(command).Tables(0).Rows(0).Item("EP_ID")

            Return number

        Catch ex As Exception
            RaiseEvent OnError(Now, New Exception("GetAutonumberKey: " & ex.Message))
            Return number
        Finally
            command.Connection.Close()
            command.Dispose()
        End Try
    End Function

    Public Function GetResponseData() As DataTable
        Dim sql As New System.Text.StringBuilder
        sql.Append(" SELECT TOP 100 ")
        sql.Append(" [IMG_ID]")
        sql.Append(" ,'FQC001' AS DataHeader ")
        sql.Append(" ,(select [IMHK1RefNum]  from [ICMImport] where IMH_ID=a.IMH_ID ) customreg ")
        sql.Append(" ,[IMGTariffCode] FQC ")
        sql.Append(" ,IMGLine ")
        sql.Append(" ,(select CFGHSCode from SMKCFormGoods where SMK_ID in (select SMK_ID  from [ICMImport] where IMH_ID=a.IMH_ID) ) HScode ")
        sql.Append(" ,IMGCurrLvl ")
        sql.Append(" ,IMGFoodCode")
        sql.Append(" , LMBY")
        sql.Append(" ,LMDT ")
        sql.Append(" ,(select [IMHReplyODesignt] from [ICMImport] where IMH_ID=a.IMH_ID ) IMHReplyDesign ")
        sql.Append(" ,[IMGAGNotes] ")
        sql.Append(" ,IMGPStatus ")
        sql.Append(" ,[IMGStatusPurpose] ")
        sql.Append(" ,(select [IMHReplyRemarks] from [ICMImport] where IMH_ID=a.IMH_ID ) IMHReplyRemarks ")
        sql.Append(" ,flag ")
        sql.Append(" ,'F' as bool ") 'dummy for process
        sql.Append("  FROM [ICMImportGoods] a ")
        sql.Append("  where IMGpstatus in ('R','NA','D','J') ")
        sql.Append("  and (flag is null or flag<>'C') ")
        sql.Append(" order by [IMG_ID] ")

        Try
            Return db.ExecuteDataSet(System.Data.CommandType.Text, sql.ToString).Tables(0)
        Catch ex As Exception
            RaiseEvent OnError(Now, New Exception("GetResponseData: " & sql.ToString & " : " & ex.Message))
            Return New DataTable
        End Try
    End Function

    Public Function GetICMImport() As DataTable
        Dim sql As New System.Text.StringBuilder
        'sql.Append(" SELECT TOP 100 ")
        'sql.Append(" [IMG_ID]")
        'sql.Append(" ,'FQC001' AS DataHeader ")
        'sql.Append(" ,(select [IMHK1RefNum]  from [ICMImport] where IMH_ID=a.IMH_ID ) customreg ")
        'sql.Append(" ,[IMGTariffCode] FQC ")
        'sql.Append(" ,IMGLine ")
        'sql.Append(" ,(select CFGHSCode from SMKCFormGoods where SMK_ID in (select SMK_ID  from [ICMImport] where IMH_ID=a.IMH_ID) ) HScode ")
        'sql.Append(" ,IMGCurrLvl ")
        'sql.Append(" ,IMGFoodCode")
        'sql.Append(" , LMBY")
        'sql.Append(" ,LMDT ")
        'sql.Append(" ,(select [IMHReplyODesignt] from [ICMImport] where IMH_ID=a.IMH_ID ) IMHReplyDesign ")
        'sql.Append(" ,[IMGAGNotes] ")
        'sql.Append(" ,IMGPStatus ")
        'sql.Append(" ,[IMGStatusPurpose] ")
        'sql.Append(" ,(select [IMHReplyRemarks] from [ICMImport] where IMH_ID=a.IMH_ID ) IMHReplyRemarks ")
        'sql.Append(" ,flag ")
        'sql.Append(" ,'F' as bool ") 'dummy for process
        'sql.Append("  FROM [ICMImportGoods] a ")
        'sql.Append("  where IMGpstatus in ('R','NA','D','J') ")
        'sql.Append("  and (flag is null or flag<>'C') ")
        'sql.Append(" order by [IMG_ID] ")


        sql.Append(" SELECT IMH_ID, SMK_ID, IMHK1RefNum,BATCHID, IMHReplyRemarks, IMHPStatus, CONVERT(char(10), B.LMDT,126) LMDT FROM ICMImport B, ")
        sql.Append(" (SELECT TOP 1 CFGK1RegNum,BATCHID FROM TmpParty ORDER BY TP_ID DESC) H ")
        sql.Append(" WHERE H.CFGK1RegNum = B.IMHK1RefNum ")
        sql.Append(" AND (B.UCUSTOM = 'Y' AND B.flag IS NULL AND B.IMHPStatus = 'A') ")

        Try
            Return db.ExecuteDataSet(System.Data.CommandType.Text, sql.ToString).Tables(0)
        Catch ex As Exception
            RaiseEvent OnError(Now, New Exception("GetResponseData: " & sql.ToString & " : " & ex.Message))
            Return New DataTable
        End Try
    End Function

    Public Function GetICMImportGoods(ByVal Key As Integer) As DataTable
        Dim sql As New System.Text.StringBuilder

        sql.Append(" SELECT IMGLine, IMGTariffCode, CASE WHEN IMGPStatus = 'R' THEN 'A' WHEN IMGPStatus = 'J' THEN 'R' ELSE IMGPStatus END AS ApprovalStatus, ")
        sql.Append(" CASE WHEN IMGStatusPurpose = 'PI' THEN 'I' WHEN IMGStatusPurpose = 'PIS' THEN 'I' WHEN IMGStatusPurpose = 'S' THEN 'S' ELSE IMGStatusPurpose END ")
        sql.Append(" AS ActionCode FROM ICMImportGoods WHERE IMH_ID=" & Key)
        sql.Append(" ORDER BY IMGLine ASC ")

        Try
            Return db.ExecuteDataSet(System.Data.CommandType.Text, sql.ToString).Tables(0)
        Catch ex As Exception
            RaiseEvent OnError(Now, New Exception("GetResponseData: " & sql.ToString & " : " & ex.Message))
            Return New DataTable
        End Try
    End Function

    Public Function SetICMImportFlag(ByVal Key As Integer) As Integer
        Dim sql As New System.Text.StringBuilder

        sql.Append(" UPDATE ICMImport SET flag='C' WHERE IMH_ID='" & Key & "'; ")
        Return ExecuteQuery(sql.ToString)
      
    End Function

    Public Function SetICMImportGoodsFlag(ByVal Key As Integer) As Integer
        Dim sql As New System.Text.StringBuilder

        sql.Append(" UPDATE ICMImportGoods SET flag='C' WHERE IMH_ID='" & Key & "'; ")
        Return ExecuteQuery(sql.ToString)

    End Function

    Public Function GetFoodCodeData() As DataTable
        Dim sql As New System.Text.StringBuilder
        
        sql.Append(" SELECT TOP 1000 [FCO_ID] ")
        'sql.Append(" SELECT [FCO_ID] ")
        sql.Append(" ,[FCOCode] ")
        sql.Append(" ,[FCODescription] ")
        sql.Append(" ,[HS_ID] ")
        sql.Append(" ,[RStatus] ")
        sql.Append(" ,[LMBY] ")
        sql.Append(" ,[LMDT] ")
        sql.Append(" FROM [CODFoodCode] ")
        sql.Append(" where flag is null ")
        sql.Append(" order by [FCO_ID]")

        Try
            Return db.ExecuteDataSet(System.Data.CommandType.Text, sql.ToString).Tables(0)
        Catch ex As Exception
            RaiseEvent OnError(Now, New Exception("GetFoodCodeData: " & sql.ToString & " : " & ex.Message))
            Return New DataTable
        End Try
    End Function

    Public Function UpdateSMKCFlag(ByVal ID As String, ByVal Val As String) As Integer
        Try
            Dim SQL As String = "UPDATE [ICMImportGoods] SET flag='" & Val & "' WHERE [IMG_ID]='" & ID & "'"
            nEventLOG(SQL.ToString)
            Return ExecuteQuery(SQL)
        Catch ex As Exception
            Return 0
        End Try
    End Function

    Public Function UpdateFoodFlag(ByVal ID As String) As Integer
        Try
            Dim SQL As String = "UPDATE [CODFoodCode] SET flag='Y' WHERE [FCO_ID]='" & ID & "'"
            nEventLOG(SQL.ToString)
            Return ExecuteQuery(SQL)
        Catch ex As Exception
            Return 0
        End Try
    End Function

    Private Function ExecuteQuery(ByVal SQLStr As String) As Integer
        Try
            RaiseEvent OnEvent("ExecuteQuery", SQLStr)
            Return db.ExecuteNonQuery(System.Data.CommandType.Text, SQLStr)
        Catch ex As Exception
            RaiseEvent OnError(Now, New Exception("ExecuteQuery: " & SQLStr & " : " & ex.Message))
            Return 0
        End Try
    End Function

    Private Function FormValidDateTime(ByVal DateString As String, ByVal TimeString As String) As Date
        Try
            '20130102:
            '1657:

            Dim newDate As String = DateString.Insert(6, "-").Insert(4, "-")
            Dim newTime As String = TimeString.Insert(2, ":")

            Return CDate(newDate & " " & newTime)
        Catch ex As Exception
            Return New Date(1970, 1, 1, 0, 0, 1) '1970-01-01 00:00:01
        End Try
    End Function

#End Region

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
End Class
