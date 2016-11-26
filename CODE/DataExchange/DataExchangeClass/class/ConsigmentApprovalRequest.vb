'=====================================================
'CONSIGNMENT APPROVAL REQUEST [UCUSTOM TO FOSIM]
'=====================================================
Public Class ConsigmentApprovalRequest

    Dim _Data_Header
    Public Property Data_Header() As String
        Get
            Return Me._Data_Header
        End Get
        Set(ByVal value As String)
            Me._Data_Header = value
        End Set
    End Property

    Dim _Message_Type
    Public Property Message_Type() As String
        Get
            Return Me._Message_Type
        End Get
        Set(ByVal value As String)
            Me._Message_Type = value
        End Set
    End Property

    Dim _Message_Mode
    Public Property Message_Mode() As String
        Get
            Return Me._Message_Mode
        End Get
        Set(ByVal value As String)
            Me._Message_Mode = value
        End Set
    End Property

    Dim _Import_Indicator
    Public Property Import_Indicator() As String
        Get
            Return Me._Import_Indicator
        End Get
        Set(ByVal value As String)
            Me._Import_Indicator = value
        End Set
    End Property

    Dim _Custom_Form_number
    Public Property Custom_Form_number() As String
        Get
            Return Me._Custom_Form_number
        End Get
        Set(ByVal value As String)
            Me._Custom_Form_number = value
        End Set
    End Property

    Dim _Custom_Form_number_2
    Public Property Custom_Form_number_2() As String
        Get
            Return Me._Custom_Form_number_2
        End Get
        Set(ByVal value As String)
            Me._Custom_Form_number_2 = value
        End Set
    End Property

    Dim _FQC_Preassigned_control_number
    Public Property FQC_Preassigned_control_number() As String
        Get
            Return Me._FQC_Preassigned_control_number
        End Get
        Set(ByVal value As String)
            Me._FQC_Preassigned_control_number = value
        End Set
    End Property

    Dim _Registration_Date
    Public Property Registration_Date() As String
        Get
            Return Me._Registration_Date
        End Get
        Set(ByVal value As String)
            Me._Registration_Date = value
        End Set
    End Property

    Dim _Resistration_Time
    Public Property Resistration_Time() As String
        Get
            Return Me._Resistration_Time
        End Get
        Set(ByVal value As String)
            Me._Resistration_Time = value
        End Set
    End Property

    Dim _Total_number_of_Item
    Public Property Total_number_of_Item() As String
        Get
            Return Me._Total_number_of_Item
        End Get
        Set(ByVal value As String)
            Me._Total_number_of_Item = value
        End Set
    End Property

    Dim _Commodity_Status
    Public Property Commodity_Status() As String
        Get
            Return Me._Commodity_Status
        End Get
        Set(ByVal value As String)
            Me._Commodity_Status = value
        End Set
    End Property

    Dim _Transaction_Type
    Public Property Transaction_Type() As String
        Get
            Return Me._Transaction_Type
        End Get
        Set(ByVal value As String)
            Me._Transaction_Type = value
        End Set
    End Property

    Dim _Exporter_name
    Public Property Exporter_name() As String
        Get
            Return Me._Exporter_name
        End Get
        Set(ByVal value As String)
            Me._Exporter_name = value
        End Set
    End Property

    Dim _Exporter_Address
    Public Property Exporter_Address() As String
        Get
            Return Me._Exporter_Address
        End Get
        Set(ByVal value As String)
            Me._Exporter_Address = value
        End Set
    End Property

    Dim _Exporter_Address_2
    Public Property Exporter_Address_2() As String
        Get
            Return Me._Exporter_Address_2
        End Get
        Set(ByVal value As String)
            Me._Exporter_Address_2 = value
        End Set
    End Property

    Dim _Exporter_Address_3
    Public Property Exporter_Address_3() As String
        Get
            Return Me._Exporter_Address_3
        End Get
        Set(ByVal value As String)
            Me._Exporter_Address_3 = value
        End Set
    End Property

    Dim _Exporter_Address_4
    Public Property Exporter_Address_4() As String
        Get
            Return Me._Exporter_Address_4
        End Get
        Set(ByVal value As String)
            Me._Exporter_Address_4 = value
        End Set
    End Property

    Dim _Exporter_Address_5
    Public Property Exporter_Address_5() As String
        Get
            Return Me._Exporter_Address_5
        End Get
        Set(ByVal value As String)
            Me._Exporter_Address_5 = value
        End Set
    End Property

    Dim _Importer_Code
    Public Property Importer_Code() As String
        Get
            Return Me._Importer_Code
        End Get
        Set(ByVal value As String)
            Me._Importer_Code = value
        End Set
    End Property

    Dim _Importer_Name
    Public Property Importer_Name() As String
        Get
            Return Me._Importer_Name
        End Get
        Set(ByVal value As String)
            Me._Importer_Name = value
        End Set
    End Property

    Dim _Importer_Address
    Public Property Importer_Address() As String
        Get
            Return Me._Importer_Address
        End Get
        Set(ByVal value As String)
            Me._Importer_Address = value
        End Set
    End Property

    Dim _Importer_Address_2
    Public Property Importer_Address_2() As String
        Get
            Return Me._Importer_Address_2
        End Get
        Set(ByVal value As String)
            Me._Importer_Address_2 = value
        End Set
    End Property

    Dim _Importer_Address_3
    Public Property Importer_Address_3() As String
        Get
            Return Me._Importer_Address_3
        End Get
        Set(ByVal value As String)
            Me._Importer_Address_3 = value
        End Set
    End Property

    Dim _Importer_Address_4
    Public Property Importer_Address_4() As String
        Get
            Return Me._Importer_Address_4
        End Get
        Set(ByVal value As String)
            Me._Importer_Address_4 = value
        End Set
    End Property

    Dim _Importer_Address_5
    Public Property Importer_Address_5() As String
        Get
            Return Me._Importer_Address_5
        End Get
        Set(ByVal value As String)
            Me._Importer_Address_5 = value
        End Set
    End Property

    Dim _Agent_Code
    Public Property Agent_Code() As String
        Get
            Return Me._Agent_Code
        End Get
        Set(ByVal value As String)
            Me._Agent_Code = value
        End Set
    End Property

    Dim _Agent_Name
    Public Property Agent_Name() As String
        Get
            Return Me._Agent_Name
        End Get
        Set(ByVal value As String)
            Me._Agent_Name = value
        End Set
    End Property

    Dim _Agent_Address
    Public Property Agent_Address() As String
        Get
            Return Me._Agent_Address
        End Get
        Set(ByVal value As String)
            Me._Agent_Address = value
        End Set
    End Property

    Dim _Agent_Address_2
    Public Property Agent_Address_2() As String
        Get
            Return Me._Agent_Address_2
        End Get
        Set(ByVal value As String)
            Me._Agent_Address_2 = value
        End Set
    End Property

    Dim _Agent_Address_3
    Public Property Agent_Address_3() As String
        Get
            Return Me._Agent_Address_3
        End Get
        Set(ByVal value As String)
            Me._Agent_Address_3 = value
        End Set
    End Property

    Dim _Agent_Address_4
    Public Property Agent_Address_4() As String
        Get
            Return Me._Agent_Address_4
        End Get
        Set(ByVal value As String)
            Me._Agent_Address_4 = value
        End Set
    End Property

    Dim _Agent_Address_5
    Public Property Agent_Address_5() As String
        Get
            Return Me._Agent_Address_5
        End Get
        Set(ByVal value As String)
            Me._Agent_Address_5 = value
        End Set
    End Property

    Dim _Mode_of_Transport
    Public Property Mode_of_Transport() As String
        Get
            Return Me._Mode_of_Transport
        End Get
        Set(ByVal value As String)
            Me._Mode_of_Transport = value
        End Set
    End Property

    Dim _Date_of_Import
    Public Property Date_of_Import() As String
        Get
            Return Me._Date_of_Import
        End Get
        Set(ByVal value As String)
            Me._Date_of_Import = value
        End Set
    End Property

    Dim _Vessel_Registration
    Public Property Vessel_Registration() As String
        Get
            Return Me._Vessel_Registration
        End Get
        Set(ByVal value As String)
            Me._Vessel_Registration = value
        End Set
    End Property

    Dim _Voyage_number
    Public Property Voyage_number() As String
        Get
            Return Me._Voyage_number
        End Get
        Set(ByVal value As String)
            Me._Voyage_number = value
        End Set
    End Property

    Dim _Vessel_name
    Public Property Vessel_name() As String
        Get
            Return Me._Vessel_name
        End Get
        Set(ByVal value As String)
            Me._Vessel_name = value
        End Set
    End Property

    Dim _Flight_number
    Public Property Flight_number() As String
        Get
            Return Me._Flight_number
        End Get
        Set(ByVal value As String)
            Me._Flight_number = value
        End Set
    End Property

    Dim _Flight_Date
    Public Property Flight_Date() As String
        Get
            Return Me._Flight_Date
        End Get
        Set(ByVal value As String)
            Me._Flight_Date = value
        End Set
    End Property

    Dim _Vehicle_Lorry_number
    Public Property Vehicle_Lorry_number() As String
        Get
            Return Me._Vehicle_Lorry_number
        End Get
        Set(ByVal value As String)
            Me._Vehicle_Lorry_number = value
        End Set
    End Property

    Dim _Trailer_number
    Public Property Trailer_number() As String
        Get
            Return Me._Trailer_number
        End Get
        Set(ByVal value As String)
            Me._Trailer_number = value
        End Set
    End Property

    Dim _Place_of_Import
    Public Property Place_of_Import() As String
        Get
            Return Me._Place_of_Import
        End Get
        Set(ByVal value As String)
            Me._Place_of_Import = value
        End Set
    End Property

    Dim _Place_of_Loading
    Public Property Place_of_Loading() As String
        Get
            Return Me._Place_of_Loading
        End Get
        Set(ByVal value As String)
            Me._Place_of_Loading = value
        End Set
    End Property

    Dim _Port_of_Transhipment
    Public Property Port_of_Transhipment() As String
        Get
            Return Me._Port_of_Transhipment
        End Get
        Set(ByVal value As String)
            Me._Port_of_Transhipment = value
        End Set
    End Property

    Dim _Pay_To
    Public Property Pay_To() As String
        Get
            Return Me._Pay_To
        End Get
        Set(ByVal value As String)
            Me._Pay_To = value
        End Set
    End Property

    Dim _Insurance
    Public Property Insurance() As String
        Get
            Return Me._Insurance
        End Get
        Set(ByVal value As String)
            Me._Insurance = value
        End Set
    End Property

    Dim _Insurance_2
    Public Property Insurance_2() As String
        Get
            Return Me._Insurance_2
        End Get
        Set(ByVal value As String)
            Me._Insurance_2 = value
        End Set
    End Property

    Dim _Other_Charges
    Public Property Other_Charges() As String
        Get
            Return Me._Other_Charges
        End Get
        Set(ByVal value As String)
            Me._Other_Charges = value
        End Set
    End Property

    Dim _Other_Charges_2
    Public Property Other_Charges_2() As String
        Get
            Return Me._Other_Charges_2
        End Get
        Set(ByVal value As String)
            Me._Other_Charges_2 = value
        End Set
    End Property

    Dim _CIF
    Public Property CIF() As String
        Get
            Return Me._CIF
        End Get
        Set(ByVal value As String)
            Me._CIF = value
        End Set
    End Property

    Dim _CIF_2
    Public Property CIF_2() As String
        Get
            Return Me._CIF_2
        End Get
        Set(ByVal value As String)
            Me._CIF_2 = value
        End Set
    End Property

    Dim _FOB
    Public Property FOB() As String
        Get
            Return Me._FOB
        End Get
        Set(ByVal value As String)
            Me._FOB = value
        End Set
    End Property

    Dim _FOB_2
    Public Property FOB_2() As String
        Get
            Return Me._FOB_2
        End Get
        Set(ByVal value As String)
            Me._FOB_2 = value
        End Set
    End Property

    Dim _Freight
    Public Property Freight() As String
        Get
            Return Me._Freight
        End Get
        Set(ByVal value As String)
            Me._Freight = value
        End Set
    End Property

    Dim _Freight_2
    Public Property Freight_2() As String
        Get
            Return Me._Freight_2
        End Get
        Set(ByVal value As String)
            Me._Freight_2 = value
        End Set
    End Property

    Dim _Gross_Weight '(in KGS)
    Public Property Gross_Weight() As String
        Get
            Return Me._Gross_Weight
        End Get
        Set(ByVal value As String)
            Me._Gross_Weight = value
        End Set
    End Property

    Dim _number_of_Packages
    Public Property number_of_Packages() As String
        Get
            Return Me._number_of_Packages
        End Get
        Set(ByVal value As String)
            Me._number_of_Packages = value
        End Set
    End Property

    Dim _Type_of_Packages
    Public Property Type_of_Packages() As String
        Get
            Return Me._Type_of_Packages
        End Get
        Set(ByVal value As String)
            Me._Type_of_Packages = value
        End Set
    End Property

    Dim _Measurement '(M3)
    Public Property Measurement() As String
        Get
            Return Me._Measurement
        End Get
        Set(ByVal value As String)
            Me._Measurement = value
        End Set
    End Property

    Dim _Consignment_Note
    Public Property Consignment_Note() As String
        Get
            Return Me._Consignment_Note
        End Get
        Set(ByVal value As String)
            Me._Consignment_Note = value
        End Set
    End Property

    Dim _General_description_of_Goods
    Public Property General_description_of_Goods() As String
        Get
            Return Me._General_description_of_Goods
        End Get
        Set(ByVal value As String)
            Me._General_description_of_Goods = value
        End Set
    End Property

    Dim _Marks
    Public Property Marks() As String
        Get
            Return Me._Marks
        End Get
        Set(ByVal value As String)
            Me._Marks = value
        End Set
    End Property

    Dim _Manifest_Registration_number
    Public Property Manifest_Registration_number() As String
        Get
            Return Me._Manifest_Registration_number
        End Get
        Set(ByVal value As String)
            Me._Manifest_Registration_number = value
        End Set
    End Property

    Dim _Import_Permit_number '(1)
    Public Property Import_Permit_number() As String
        Get
            Return Me._Import_Permit_number
        End Get
        Set(ByVal value As String)
            Me._Import_Permit_number = value
        End Set
    End Property

    Dim _Import_Permit_number_2 '(2)
    Public Property Import_Permit_number_2() As String
        Get
            Return Me._Import_Permit_number_2
        End Get
        Set(ByVal value As String)
            Me._Import_Permit_number_2 = value
        End Set
    End Property

    Dim _Special_Treatement
    Public Property Special_Treatement() As String
        Get
            Return Me._Special_Treatement
        End Get
        Set(ByVal value As String)
            Me._Special_Treatement = value
        End Set
    End Property

    Dim _Total_Duty_Payable
    Public Property Total_Duty_Payable() As String
        Get
            Return Me._Total_Duty_Payable
        End Get
        Set(ByVal value As String)
            Me._Total_Duty_Payable = value
        End Set
    End Property

    Dim _Declarant_IC_number
    Public Property Declarant_IC_number() As String
        Get
            Return Me._Declarant_IC_number
        End Get
        Set(ByVal value As String)
            Me._Declarant_IC_number = value
        End Set
    End Property

    Dim _Declarant_Name
    Public Property Declarant_Name() As String
        Get
            Return Me._Declarant_Name
        End Get
        Set(ByVal value As String)
            Me._Declarant_Name = value
        End Set
    End Property

    Dim _Declarant_Status
    Public Property Declarant_Status() As String
        Get
            Return Me._Declarant_Status
        End Get
        Set(ByVal value As String)
            Me._Declarant_Status = value
        End Set
    End Property


    '=========================================

    Dim _Data_Header_B
    Public Property Data_Header_B() As String
        Get
            Return Me._Data_Header_B
        End Get
        Set(ByVal value As String)
            Me._Data_Header_B = value
        End Set
    End Property

    Dim _Custom_Form_number_B
    Public Property Custom_Form_number_B() As String
        Get
            Return Me._Custom_Form_number_B
        End Get
        Set(ByVal value As String)
            Me._Custom_Form_number_B = value
        End Set
    End Property

    Dim _Custom_Form_number_B_2
    Public Property Custom_Form_number_B_2() As String
        Get
            Return Me._Custom_Form_number_B_2
        End Get
        Set(ByVal value As String)
            Me._Custom_Form_number_B_2 = value
        End Set
    End Property

    Dim _Item_number
    Public Property Item_number() As String
        Get
            Return Me._Item_number
        End Get
        Set(ByVal value As String)
            Me._Item_number = value
        End Set
    End Property

    Dim _Item_Description
    Public Property Item_Description() As String
        Get
            Return Me._Item_Description
        End Get
        Set(ByVal value As String)
            Me._Item_Description = value
        End Set
    End Property

    Dim _HS_code
    Public Property HS_code() As String
        Get
            Return Me._HS_code
        End Get
        Set(ByVal value As String)
            Me._HS_code = value
        End Set
    End Property

    Dim _Declared_Quantity_1
    Public Property Declared_Quantity_1() As String
        Get
            Return Me._Declared_Quantity_1
        End Get
        Set(ByVal value As String)
            Me._Declared_Quantity_1 = value
        End Set
    End Property

    Dim _Declared_unit_1
    Public Property Declared_unit_1() As String
        Get
            Return Me._Declared_unit_1
        End Get
        Set(ByVal value As String)
            Me._Declared_unit_1 = value
        End Set
    End Property

    Dim _Unit_price
    Public Property Unit_price() As String
        Get
            Return Me._Unit_price
        End Get
        Set(ByVal value As String)
            Me._Unit_price = value
        End Set
    End Property

    Dim _Total_price
    Public Property Total_price() As String
        Get
            Return Me._Total_price
        End Get
        Set(ByVal value As String)
            Me._Total_price = value
        End Set
    End Property

    Dim _Duty_Amount_B
    Public Property Duty_Amount_B() As String
        Get
            Return Me._Duty_Amount_B
        End Get
        Set(ByVal value As String)
            Me._Duty_Amount_B = value
        End Set
    End Property

    Dim _Duty_Amount_B_2
    Public Property Duty_Amount_B_2() As String
        Get
            Return Me._Duty_Amount_B_2
        End Get
        Set(ByVal value As String)
            Me._Duty_Amount_B_2 = value
        End Set
    End Property

    Dim _number_of_Packages_B
    Public Property number_of_Packages_B() As String
        Get
            Return Me._number_of_Packages_B
        End Get
        Set(ByVal value As String)
            Me._number_of_Packages_B = value
        End Set
    End Property

    Dim _Type_of_Packages_B
    Public Property Type_of_Packages_B() As String
        Get
            Return Me._Type_of_Packages_B
        End Get
        Set(ByVal value As String)
            Me._Type_of_Packages_B = value
        End Set
    End Property

    Dim _Country_of_Origin
    Public Property Country_of_Origin() As String
        Get
            Return Me._Country_of_Origin
        End Get
        Set(ByVal value As String)
            Me._Country_of_Origin = value
        End Set
    End Property

    Dim _Declared_Quantity_2
    Public Property Declared_Quantity_2() As String
        Get
            Return Me._Declared_Quantity_2
        End Get
        Set(ByVal value As String)
            Me._Declared_Quantity_2 = value
        End Set
    End Property

    Dim _Declared_unit_2
    Public Property Declared_unit_2() As String
        Get
            Return Me._Declared_unit_2
        End Get
        Set(ByVal value As String)
            Me._Declared_unit_2 = value
        End Set
    End Property

    'Additional Data:
    Dim _Purpose_of_import
    Public Property Purpose_of_import() As String
        Get
            Return Me._Purpose_of_import
        End Get
        Set(ByVal value As String)
            Me._Purpose_of_import = value
        End Set
    End Property

    Dim _Warehouse_Code
    Public Property Warehouse_Code() As String
        Get
            Return Me._Warehouse_Code
        End Get
        Set(ByVal value As String)
            Me._Warehouse_Code = value
        End Set
    End Property

    Dim _Warehouse_Code_2
    Public Property Warehouse_Code_2() As String
        Get
            Return Me._Warehouse_Code_2
        End Get
        Set(ByVal value As String)
            Me._Warehouse_Code_2 = value
        End Set
    End Property

    Dim _Warehouse_Name
    Public Property Warehouse_Name() As String
        Get
            Return Me._Warehouse_Name
        End Get
        Set(ByVal value As String)
            Me._Warehouse_Name = value
        End Set
    End Property

    Dim _Warehouse_Address
    Public Property Warehouse_Address() As String
        Get
            Return Me._Warehouse_Address
        End Get
        Set(ByVal value As String)
            Me._Warehouse_Address = value
        End Set
    End Property

    Dim _Warehouse_Address_2
    Public Property Warehouse_Address_2() As String
        Get
            Return Me._Warehouse_Address_2
        End Get
        Set(ByVal value As String)
            Me._Warehouse_Address_2 = value
        End Set
    End Property

    Dim _Warehouse_Address_3
    Public Property Warehouse_Address_3() As String
        Get
            Return Me._Warehouse_Address_3
        End Get
        Set(ByVal value As String)
            Me._Warehouse_Address_3 = value
        End Set
    End Property

    Dim _Warehouse_Address_4
    Public Property Warehouse_Address_4() As String
        Get
            Return Me._Warehouse_Address_4
        End Get
        Set(ByVal value As String)
            Me._Warehouse_Address_4 = value
        End Set
    End Property

    Dim _Warehouse_Address_5
    Public Property Warehouse_Address_5() As String
        Get
            Return Me._Warehouse_Address_5
        End Get
        Set(ByVal value As String)
            Me._Warehouse_Address_5 = value
        End Set
    End Property

    Dim _Remarks_and_Accident
    Public Property Remarks_and_Accident() As String
        Get
            Return Me._Remarks_and_Accident
        End Get
        Set(ByVal value As String)
            Me._Remarks_and_Accident = value
        End Set
    End Property

    Dim _Exporter_Code
    Public Property Exporter_Code() As String
        Get
            Return Me._Exporter_Code
        End Get
        Set(ByVal value As String)
            Me._Exporter_Code = value
        End Set
    End Property

    Dim _Food_Code
    Public Property Food_Code() As String
        Get
            Return Me._Food_Code
        End Get
        Set(ByVal value As String)
            Me._Food_Code = value
        End Set
    End Property

    Dim _Food_Code_2
    Public Property Food_Code_2() As String
        Get
            Return Me._Food_Code_2
        End Get
        Set(ByVal value As String)
            Me._Food_Code_2 = value
        End Set
    End Property

    Dim _Brand
    Public Property Brand() As String
        Get
            Return Me._Brand
        End Get
        Set(ByVal value As String)
            Me._Brand = value
        End Set
    End Property

    Dim _Brand_2
    Public Property Brand_2() As String
        Get
            Return Me._Brand_2
        End Get
        Set(ByVal value As String)
            Me._Brand_2 = value
        End Set
    End Property

    Dim _Date_of_production
    Public Property Date_of_production() As String
        Get
            Return Me._Date_of_production
        End Get
        Set(ByVal value As String)
            Me._Date_of_production = value
        End Set
    End Property

    Dim _Date_of_expire
    Public Property Date_of_expire() As String
        Get
            Return Me._Date_of_expire
        End Get
        Set(ByVal value As String)
            Me._Date_of_expire = value
        End Set
    End Property

    Dim _Treatment
    Public Property Treatment() As String
        Get
            Return Me._Treatment
        End Get
        Set(ByVal value As String)
            Me._Treatment = value
        End Set
    End Property

    Dim _Manufacturer_code
    Public Property Manufacturer_code() As String
        Get
            Return Me._Manufacturer_code
        End Get
        Set(ByVal value As String)
            Me._Manufacturer_code = value
        End Set
    End Property

    Dim _Manufacturer_name
    Public Property Manufacturer_name() As String
        Get
            Return Me._Manufacturer_name
        End Get
        Set(ByVal value As String)
            Me._Manufacturer_name = value
        End Set
    End Property

    Dim _Manufacturer_address
    Public Property Manufacturer_address() As String
        Get
            Return Me._Manufacturer_address
        End Get
        Set(ByVal value As String)
            Me._Manufacturer_address = value
        End Set
    End Property

    Dim _Manufacturer_address_2
    Public Property Manufacturer_address_2() As String
        Get
            Return Me._Manufacturer_address_2
        End Get
        Set(ByVal value As String)
            Me._Manufacturer_address_2 = value
        End Set
    End Property

    Dim _Manufacturer_address_3
    Public Property Manufacturer_address_3() As String
        Get
            Return Me._Manufacturer_address_3
        End Get
        Set(ByVal value As String)
            Me._Manufacturer_address_3 = value
        End Set
    End Property

    Dim _Manufacturer_address_4
    Public Property Manufacturer_address_4() As String
        Get
            Return Me._Manufacturer_address_4
        End Get
        Set(ByVal value As String)
            Me._Manufacturer_address_4 = value
        End Set
    End Property

    Dim _Manufacturer_address_5
    Public Property Manufacturer_address_5() As String
        Get
            Return Me._Manufacturer_address_5
        End Get
        Set(ByVal value As String)
            Me._Manufacturer_address_5 = value
        End Set
    End Property

End Class