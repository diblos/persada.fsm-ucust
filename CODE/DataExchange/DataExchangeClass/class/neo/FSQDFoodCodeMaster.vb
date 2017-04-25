Namespace FSQDFoodCodeMaster
    Public Class FSQDFoodCodeMaster
        Private _Header As Header.HeaderType
        Public Property Header() As Header.HeaderType
            Get
                Return Me._Header
            End Get
            Set(ByVal value As Header.HeaderType)
                Me._Header = value
            End Set
        End Property

        Private _Body As FoodCodeMaster
        Public Property Body() As FoodCodeMaster
            Get
                Return Me._Body
            End Get
            Set(ByVal value As FoodCodeMaster)
                Me._Body = value
            End Set
        End Property
    End Class

    Public Class FoodCodeMaster
        Private _FoodCode As FoodCode
        Public Property FoodCode() As FoodCode
            Get
                Return Me._FoodCode
            End Get
            Set(ByVal value As FoodCode)
                Me._FoodCode = value
            End Set
        End Property
    End Class

    Public Class FoodCode
        Private _FCOCode As String ' 1-8
        Public Property FCOCode() As String
            Get
                Return Me._FCOCode
            End Get
            Set(ByVal value As String)
                Me._FCOCode = value
            End Set
        End Property

        Private _FCODescription As String ' 0-512
        Public Property FCODescription() As String
            Get
                Return Me._FCODescription
            End Get
            Set(ByVal value As String)
                Me._FCODescription = value
            End Set
        End Property

        Private _RStatus As String ' 1-3
        Public Property RStatus() As String
            Get
                Return Me._RStatus
            End Get
            Set(ByVal value As String)
                Me._RStatus = value
            End Set
        End Property

        Private _Category As String ' 1-18;N/A
        Public Property Category() As String
            Get
                Return Me._Category
            End Get
            Set(ByVal value As String)
                Me._Category = value
            End Set
        End Property

        Private _ProductType As String '1-3;P
        Public Property ProductType() As String
            Get
                Return Me._ProductType
            End Get
            Set(ByVal value As String)
                Me._ProductType = value
            End Set
        End Property
    End Class
End Namespace

