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
    Public Shared UPDATEDBY As String = "Admin"
    Public Shared UPDATEDDATE As String
    Public Shared CERT_ID_DEFAULT As String = "1,2"

    Public Function CAInsert(ByVal data As DataExchangeClass.ConsigmentApprovalRequest) As Integer

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

    Private Function GetAutonumberKey(ByVal TableName As String, ByVal TableCol As String) As Integer
        Dim number As Integer = 1
        Dim sql As String = _
        "SELECT MAX(" & TableCol & ")  AS [MAX] FROM " & TableName

        Dim command As System.Data.Common.DbCommand = db.GetSqlStringCommand(sql)
        Try
            number = db.ExecuteDataSet(command).Tables(0).Rows(0).Item("MAX")
            Return number + 1
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
            Return db.ExecuteNonQuery(System.Data.CommandType.Text, SQLStr)
        Catch ex As Exception
            RaiseEvent OnError(Now, New Exception("ExecuteQuery: " & SQLStr & " : " & ex.Message))
            Return 0
        End Try
    End Function

#End Region

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
End Class
