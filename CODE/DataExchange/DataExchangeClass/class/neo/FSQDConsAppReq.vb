'=====================================================
'CONSIGNMENT APPROVAL REQUEST [UCUSTOM TO FOSIM]
'=====================================================

Imports System.ComponentModel

Namespace FSQDConsAppReq
    Public Class FSQDConsAppReq
        Private _Header As Header.HeaderType
        Public Property Header() As Header.HeaderType
            Get
                Return Me._Header
            End Get
            Set(ByVal value As Header.HeaderType)
                Me._Header = value
            End Set
        End Property

        Private _Body As FSQDDeclaration
        Public Property Body() As FSQDDeclaration
            Get
                Return Me._Body
            End Get
            Set(ByVal value As FSQDDeclaration)
                Me._Body = value
            End Set
        End Property
    End Class

    Public Class FSQDDeclaration

        Public Enum EnumModeOfTransport
            Sea = 1
            Road = 3
            Air = 4
        End Enum

        Private _MCKey As String
        Public Property MCKey() As String
            Get
                Return Me._MCKey
            End Get
            Set(ByVal value As String)
                Me._MCKey = value
            End Set
        End Property

        Private _MCValue As String
        Public Property MCValue() As String
            Get
                Return Me._MCValue
            End Get
            Set(ByVal value As String)
                Me._MCValue = value
            End Set
        End Property

        Private _CustomFormNumber As String ' 1-35
        Public Property CustomFormNumber() As String
            Get
                Return Me._CustomFormNumber
            End Get
            Set(ByVal value As String)
                Me._CustomFormNumber = value
            End Set
        End Property

        Private _TransactionType As String ' 1-3
        Public Property TransactionType() As String
            Get
                Return Me._TransactionType
            End Get
            Set(ByVal value As String)
                Me._TransactionType = value
            End Set
        End Property

        Private _RegistrationDate As String ' 1-17
        Public Property RegistrationDate() As String
            Get
                Return Me._RegistrationDate
            End Get
            Set(ByVal value As String)
                Me._RegistrationDate = value
            End Set
        End Property

        Private _RegistrationTime As String  ' 1-17
        Public Property RegistrationTime() As String
            Get
                Return Me._RegistrationTime
            End Get
            Set(ByVal value As String)
                Me._RegistrationTime = value
            End Set
        End Property

        Private _DeclarantName As String ' 1-105
        Public Property DeclarantName() As String
            Get
                Return Me._DeclarantName
            End Get
            Set(ByVal value As String)
                Me._DeclarantName = value
            End Set
        End Property

        Private _DeclarantICNumber As String ' 1-17
        Public Property DeclarantICNumber() As String
            Get
                Return Me._DeclarantICNumber
            End Get
            Set(ByVal value As String)
                Me._DeclarantICNumber = value
            End Set
        End Property

        Private _DeclarantStatus As String ' 1-70
        Public Property DeclarantStatus() As String
            Get
                Return Me._DeclarantStatus
            End Get
            Set(ByVal value As String)
                Me._DeclarantStatus = value
            End Set
        End Property

        Private _TotalNumberOfItem As Integer
        Public Property TotalNumberOfItem() As Integer
            Get
                Return Me._TotalNumberOfItem
            End Get
            Set(ByVal value As Integer)
                Me._TotalNumberOfItem = value
            End Set
        End Property

        Private _ExporterName As String ' 1-70
        Public Property ExporterName() As String
            Get
                Return Me._ExporterName
            End Get
            Set(ByVal value As String)
                Me._ExporterName = value
            End Set
        End Property

        Private _ExporterAddressStreetAndNumberPObox As String ' 1-70
        Public Property ExporterAddressStreetAndNumberPObox() As String
            Get
                Return Me._ExporterAddressStreetAndNumberPObox
            End Get
            Set(ByVal value As String)
                Me._ExporterAddressStreetAndNumberPObox = value
            End Set
        End Property

        Private _ExporterAddressCountry As String ' 1-2
        Public Property ExporterAddressCountry() As String
            Get
                Return Me._ExporterAddressCountry
            End Get
            Set(ByVal value As String)
                Me._ExporterAddressCountry = value
            End Set
        End Property

        Private _ImporterCode As String ' 1-17
        Public Property ImporterCode() As String
            Get
                Return Me._ImporterCode
            End Get
            Set(ByVal value As String)
                Me._ImporterCode = value
            End Set
        End Property

        Private _ImporterName As String ' 1-105
        Public Property ImporterName() As String
            Get
                Return Me._ImporterName
            End Get
            Set(ByVal value As String)
                Me._ImporterName = value
            End Set
        End Property

        Private _ImporterAddressStreetAndNumberPObox As String ' 1-70
        Public Property ImporterAddressStreetAndNumberPObox() As String
            Get
                Return Me._ImporterAddressStreetAndNumberPObox
            End Get
            Set(ByVal value As String)
                Me._ImporterAddressStreetAndNumberPObox = value
            End Set
        End Property

        Private _ImporterAddressCity As String ' 1-35
        Public Property ImporterAddressCity() As String
            Get
                Return Me._ImporterAddressCity
            End Get
            Set(ByVal value As String)
                Me._ImporterAddressCity = value
            End Set
        End Property

        Private _ImporterAddressCountry As String ' 1-2
        Public Property ImporterAddressCountry() As String
            Get
                Return Me._ImporterAddressCountry
            End Get
            Set(ByVal value As String)
                Me._ImporterAddressCountry = value
            End Set
        End Property

        Private _ImporterAddressCountrySubEntityName As String ' 1-35
        Public Property ImporterAddressCountrySubEntityName() As String
            Get
                Return Me._ImporterAddressCountrySubEntityName
            End Get
            Set(ByVal value As String)
                Me._ImporterAddressCountrySubEntityName = value
            End Set
        End Property

        Private _ImporterAddressPostcodeIdentification As String ' 1-9
        Public Property ImporterAddressPostcodeIdentification() As String
            Get
                Return Me._ImporterAddressPostcodeIdentification
            End Get
            Set(ByVal value As String)
                Me._ImporterAddressPostcodeIdentification = value
            End Set
        End Property

        Private _AgentCode As String ' 0-17
        Public Property AgentCode() As String
            Get
                Return Me._AgentCode
            End Get
            Set(ByVal value As String)
                Me._AgentCode = value
            End Set
        End Property

        Private _AgentName As String ' 0-105
        Public Property AgentName() As String
            Get
                Return Me._AgentName
            End Get
            Set(ByVal value As String)
                Me._AgentName = value
            End Set
        End Property

        Private _AgentAddressStreetAndNumberPObox As String ' 0-70
        Public Property AgentAddressStreetAndNumberPObox() As String
            Get
                Return Me._AgentAddressStreetAndNumberPObox
            End Get
            Set(ByVal value As String)
                Me._AgentAddressStreetAndNumberPObox = value
            End Set
        End Property

        Private _AgentAddressCity As String ' 0-35
        Public Property AgentAddressCity() As String
            Get
                Return Me._AgentAddressCity
            End Get
            Set(ByVal value As String)
                Me._AgentAddressCity = value
            End Set
        End Property

        Private _AgentAddressCountry As String ' 0-2
        Public Property AgentAddressCountry() As String
            Get
                Return Me._AgentAddressCountry
            End Get
            Set(ByVal value As String)
                Me._AgentAddressCountry = value
            End Set
        End Property

        Private _AgentAddressCountrySubEntityName As String ' 0-35
        Public Property AgentAddressCountrySubEntityName() As String
            Get
                Return Me._AgentAddressCountrySubEntityName
            End Get
            Set(ByVal value As String)
                Me._AgentAddressCountrySubEntityName = value
            End Set
        End Property

        Private _AgentAddressPostcodeIdentification As String ' 0-9
        Public Property AgentAddressPostcodeIdentification() As String
            Get
                Return Me._AgentAddressPostcodeIdentification
            End Get
            Set(ByVal value As String)
                Me._AgentAddressPostcodeIdentification = value
            End Set
        End Property

        Private _ConsignmentNote As String ' 0-35
        Public Property ConsignmentNote() As String
            Get
                Return Me._ConsignmentNote
            End Get
            Set(ByVal value As String)
                Me._ConsignmentNote = value
            End Set
        End Property

        Private _GeneralDescriptionOfGoods As String ' 0-512
        Public Property GeneralDescriptionOfGoods() As String
            Get
                Return Me._GeneralDescriptionOfGoods
            End Get
            Set(ByVal value As String)
                Me._GeneralDescriptionOfGoods = value
            End Set
        End Property

        Private _Marks As String ' 0-512
        Public Property Marks() As String
            Get
                Return Me._Marks
            End Get
            Set(ByVal value As String)
                Me._Marks = value
            End Set
        End Property

        Private _ManifestRegistrationNumber As String ' 0-35
        Public Property ManifestRegistrationNumber() As String
            Get
                Return Me._ManifestRegistrationNumber
            End Get
            Set(ByVal value As String)
                Me._ManifestRegistrationNumber = value
            End Set
        End Property

        Private _ModeOfTransport As EnumModeOfTransport
        Public Property ModeOfTransport() As EnumModeOfTransport
            Get
                Return Me._ModeOfTransport
            End Get
            Set(ByVal value As EnumModeOfTransport)
                Me._ModeOfTransport = value
            End Set
        End Property

        Private _DateOfImport As String ' 0-17
        Public Property DateOfImport() As String
            Get
                Return Me._DateOfImport
            End Get
            Set(ByVal value As String)
                Me._DateOfImport = value
            End Set
        End Property

        Private _VesselRegistration As String ' 0-25
        Public Property VesselRegistration() As String
            Get
                Return Me._VesselRegistration
            End Get
            Set(ByVal value As String)
                Me._VesselRegistration = value
            End Set
        End Property

        Private _VoyageNumber As String ' 0-17
        Public Property VoyageNumber() As String
            Get
                Return Me._VoyageNumber
            End Get
            Set(ByVal value As String)
                Me._VoyageNumber = value
            End Set
        End Property

        Private _VesselName As String ' 0-35
        Public Property VesselName() As String
            Get
                Return Me._VesselName
            End Get
            Set(ByVal value As String)
                Me._VesselName = value
            End Set
        End Property

        Private _FlightNumber As String ' 0-17
        Public Property FlightNumber() As String
            Get
                Return Me._FlightNumber
            End Get
            Set(ByVal value As String)
                Me._FlightNumber = value
            End Set
        End Property

        Private _FlightDate As String ' 0-17
        Public Property FlightDate() As String
            Get
                Return Me._FlightDate
            End Get
            Set(ByVal value As String)
                Me._FlightDate = value
            End Set
        End Property

        Private _PlaceOfImport As String ' 0-17
        Public Property PlaceOfImport() As String
            Get
                Return Me._PlaceOfImport
            End Get
            Set(ByVal value As String)
                Me._PlaceOfImport = value
            End Set
        End Property

        Private _PlaceOfLoading As String ' 0-17
        Public Property PlaceOfLoading() As String
            Get
                Return Me._PlaceOfLoading
            End Get
            Set(ByVal value As String)
                Me._PlaceOfLoading = value
            End Set
        End Property

        Private _PortOfTransshipment As String ' 0-17
        Public Property PortOfTransshipment() As String
            Get
                Return Me._PortOfTransshipment
            End Get
            Set(ByVal value As String)
                Me._PortOfTransshipment = value
            End Set
        End Property

        Private _Invoice As Invoice
        Public Property Invoice() As Invoice
            Get
                Return Me._Invoice
            End Get
            Set(ByVal value As Invoice)
                Me._Invoice = value
            End Set
        End Property

        Private _Attachments As List(Of Attachment)
        Public Property Attachments() As List(Of Attachment)
            Get
                Return Me._Attachments
            End Get
            Set(ByVal value As List(Of Attachment))
                Me._Attachments = value
            End Set
        End Property
    End Class

    Public Class Invoice
        Private _PayTo As String ' 1-2
        Public Property PayTo() As String
            Get
                Return Me._PayTo
            End Get
            Set(ByVal value As String)
                Me._PayTo = value
            End Set
        End Property

        Private _Insurance As Decimal
        Public Property Insurance() As Decimal
            Get
                Return Me._Insurance
            End Get
            Set(ByVal value As Decimal)
                Me._Insurance = value
            End Set
        End Property

        Private _OtherCharges As Decimal
        Public Property OtherCharges() As Decimal
            Get
                Return Me._OtherCharges
            End Get
            Set(ByVal value As Decimal)
                Me._OtherCharges = value
            End Set
        End Property

        Private _CIF As Decimal
        Public Property CIF() As Decimal
            Get
                Return Me._CIF
            End Get
            Set(ByVal value As Decimal)
                Me._CIF = value
            End Set
        End Property

        Private _FOB As Decimal
        Public Property FOB() As Decimal
            Get
                Return Me._FOB
            End Get
            Set(ByVal value As Decimal)
                Me._FOB = value
            End Set
        End Property

        Private _Freight As Decimal
        Public Property Freight() As Decimal
            Get
                Return Me._Freight
            End Get
            Set(ByVal value As Decimal)
                Me._Freight = value
            End Set
        End Property

        Private _TotalDutyPayable As Decimal
        Public Property TotalDutyPayable() As Decimal
            Get
                Return Me._TotalDutyPayable
            End Get
            Set(ByVal value As Decimal)
                Me._TotalDutyPayable = value
            End Set
        End Property

        Private _InvoiceItems As List(Of InvoiceItem)
        Public Property InvoiceItems() As List(Of InvoiceItem)
            Get
                Return Me._InvoiceItems
            End Get
            Set(ByVal value As List(Of InvoiceItem))
                Me._InvoiceItems = value
            End Set
        End Property
    End Class

    Public Class InvoiceItem

        Enum enumCommodityStatus
            <Description("D-Dutiable")>
            D
            <Description("ND-Dutiable")>
            ND
        End Enum

        Private _ItemNumber As Integer
        Public Property ItemNumber() As Integer
            Get
                Return Me._ItemNumber
            End Get
            Set(ByVal value As Integer)
                Me._ItemNumber = value
            End Set
        End Property

        Private _ItemDescription As String ' 1-512
        Public Property ItemDescription() As String
            Get
                Return Me._ItemDescription
            End Get
            Set(ByVal value As String)
                Me._ItemDescription = value
            End Set
        End Property

        Private _HSCode As String ' 1-18
        Public Property HSCode() As String
            Get
                Return Me._HSCode
            End Get
            Set(ByVal value As String)
                Me._HSCode = value
            End Set
        End Property

        Private _GrossWeightInKGS As Decimal
        Public Property GrossWeightInKGS() As Decimal
            Get
                Return Me._GrossWeightInKGS
            End Get
            Set(ByVal value As Decimal)
                Me._GrossWeightInKGS = value
            End Set
        End Property

        Private _DeclaredQuantity As Decimal
        Public Property DeclaredQuantity() As Decimal
            Get
                Return Me._DeclaredQuantity
            End Get
            Set(ByVal value As Decimal)
                Me._DeclaredQuantity = value
            End Set
        End Property

        Private _DeclaredUnit As String ' 1-3
        Public Property DeclaredUnit() As String
            Get
                Return Me._DeclaredUnit
            End Get
            Set(ByVal value As String)
                Me._DeclaredUnit = value
            End Set
        End Property

        Private _UnitPrice As Decimal
        Public Property UnitPrice() As Decimal
            Get
                Return Me._UnitPrice
            End Get
            Set(ByVal value As Decimal)
                Me._UnitPrice = value
            End Set
        End Property

        Private _TotalPrice As Decimal
        Public Property TotalPrice() As Decimal
            Get
                Return Me._TotalPrice
            End Get
            Set(ByVal value As Decimal)
                Me._TotalPrice = value
            End Set
        End Property

        Private _DutyAmount As Decimal
        Public Property DutyAmount() As Decimal
            Get
                Return Me._DutyAmount
            End Get
            Set(ByVal value As Decimal)
                Me._DutyAmount = value
            End Set
        End Property

        Private _CountryOfOrigin As String ' 1-2
        Public Property CountryOfOrigin() As String
            Get
                Return Me._CountryOfOrigin
            End Get
            Set(ByVal value As String)
                Me._CountryOfOrigin = value
            End Set
        End Property

        Private _CommodityStatus As enumCommodityStatus
        Public Property CommodityStatus() As enumCommodityStatus
            Get
                Return Me._CommodityStatus
            End Get
            Set(ByVal value As enumCommodityStatus)
                Me._CommodityStatus = value
            End Set
        End Property

        Private _Permits As List(Of Permit)
        Public Property Permits() As List(Of Permit)
            Get
                Return Me._Permits
            End Get
            Set(ByVal value As List(Of Permit))
                Me._Permits = value
            End Set
        End Property

        Private _Specifications As List(Of Specification)
        Public Property Specifications() As List(Of Specification)
            Get
                Return Me._Specifications
            End Get
            Set(ByVal value As List(Of Specification))
                Me._Specifications = value
            End Set
        End Property
    End Class

    Public Class Permit
        Private _ImportPermitNumber As String ' 0-35
        Public Property ImportPermitNumber() As String
            Get
                Return Me._ImportPermitNumber
            End Get
            Set(ByVal value As String)
                Me._ImportPermitNumber = value
            End Set
        End Property
    End Class

    Public Class Specification
        Private _PurposeOfImport As String ' 0-512
        Public Property PurposeOfImport() As String
            Get
                Return Me._PurposeOfImport
            End Get
            Set(ByVal value As String)
                Me._PurposeOfImport = value
            End Set
        End Property

        Private _WarehouseCode As String ' 0-512
        Public Property WarehouseCode() As String
            Get
                Return Me._WarehouseCode
            End Get
            Set(ByVal value As String)
                Me._WarehouseCode = value
            End Set
        End Property

        Private _WarehouseName As String ' 0-512
        Public Property WarehouseName() As String
            Get
                Return Me._WarehouseName
            End Get
            Set(ByVal value As String)
                Me._WarehouseName = value
            End Set
        End Property

        Private _WarehouseAddress As String ' 0-512
        Public Property WarehouseAddress() As String
            Get
                Return Me._WarehouseAddress
            End Get
            Set(ByVal value As String)
                Me._WarehouseAddress = value
            End Set
        End Property

        Private _ExporterCode As String ' 0-512
        Public Property ExporterCode() As String
            Get
                Return Me._ExporterCode
            End Get
            Set(ByVal value As String)
                Me._ExporterCode = value
            End Set
        End Property

        Private _FoodCode As String ' 0-512
        Public Property FoodCode() As String
            Get
                Return Me._FoodCode
            End Get
            Set(ByVal value As String)
                Me._FoodCode = value
            End Set
        End Property

        Private _Brand As String ' 0-512
        Public Property Brand() As String
            Get
                Return Me._Brand
            End Get
            Set(ByVal value As String)
                Me._Brand = value
            End Set
        End Property

        Private _DateOfProduction As String ' 0-512
        Public Property DateOfProduction() As String
            Get
                Return Me._DateOfProduction
            End Get
            Set(ByVal value As String)
                Me._DateOfProduction = value
            End Set
        End Property

        Private _DateOfExpire As String ' 0-512
        Public Property DateOfExpire() As String
            Get
                Return Me._DateOfExpire
            End Get
            Set(ByVal value As String)
                Me._DateOfExpire = value
            End Set
        End Property

        Private _Treatment As String ' 0-512
        Public Property Treatment() As String
            Get
                Return Me._Treatment
            End Get
            Set(ByVal value As String)
                Me._Treatment = value
            End Set
        End Property

        Private _ManufacturerCode As String ' 0-512
        Public Property ManufacturerCode() As String
            Get
                Return Me._ManufacturerCode
            End Get
            Set(ByVal value As String)
                Me._ManufacturerCode = value
            End Set
        End Property

        Private _ManufacturerName As String ' 0-512
        Public Property ManufacturerName() As String
            Get
                Return Me._ManufacturerName
            End Get
            Set(ByVal value As String)
                Me._ManufacturerName = value
            End Set
        End Property

        Private _ManufacturerAddress As String ' 0-512
        Public Property ManufacturerAddress() As String
            Get
                Return Me._ManufacturerAddress
            End Get
            Set(ByVal value As String)
                Me._ManufacturerAddress = value
            End Set
        End Property

        Private _PreImportRegistrationNo As String ' 0-512
        Public Property PreImportRegistrationNo() As String
            Get
                Return Me._PreImportRegistrationNo
            End Get
            Set(ByVal value As String)
                Me._PreImportRegistrationNo = value
            End Set
        End Property
    End Class

    Public Class Attachment
        Private _FilePath As String
        Public Property FilePath() As String
            Get
                Return Me._FilePath
            End Get
            Set(ByVal value As String)
                Me._FilePath = value
            End Set
        End Property

        Private _FileContent As String
        Public Property FileContent() As String
            Get
                Return Me._FileContent
            End Get
            Set(ByVal value As String)
                Me._FileContent = value
            End Set
        End Property
    End Class
End Namespace

