'=====================================================
'CUSTOM DECLARATION K1 [UCUSTOM TO FOSIM]
'=====================================================
Namespace deprecating
    Public Class CUSDECInfo

        Dim _ucustom_reg_id As String
        Public Property uCustomRegistrationNo() As String
            Get
                Return Me._ucustom_reg_id
            End Get
            Set(ByVal value As String)
                Me._ucustom_reg_id = value
            End Set
        End Property

        Dim _exim_info As EximInfo
        Public Property ExporterImporterInfo() As EximInfo
            Get
                Return Me._exim_info
            End Get
            Set(ByVal value As EximInfo)
                Me._exim_info = value
            End Set
        End Property

        Dim _item_info As ItemInfo
        Public Property ItemInfo() As ItemInfo
            Get
                Return Me._item_info
            End Get
            Set(ByVal value As ItemInfo)
                Me._item_info = value
            End Set
        End Property

        Dim _permit_info As PermitInfo
        Public Property PermitInfo() As PermitInfo
            Get
                Return Me._permit_info
            End Get
            Set(ByVal value As PermitInfo)
                Me._permit_info = value
            End Set
        End Property

        Dim _declarant_info As DeclarantInfo
        Public Property DeclarantInfo() As DeclarantInfo
            Get
                Return Me._declarant_info
            End Get
            Set(ByVal value As DeclarantInfo)
                Me._declarant_info = value
            End Set
        End Property

    End Class
End Namespace


