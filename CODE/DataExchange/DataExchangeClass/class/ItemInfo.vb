Public Class ItemInfo
    Dim _no
    Public Property No() As Object
        Get
            Return Me._no
        End Get
        Set(ByVal value As Object)
            Me._no = value
        End Set
    End Property

    Dim _description
    Public Property Description() As Object
        Get
            Return Me._description
        End Get
        Set(ByVal value As Object)
            Me._description = value
        End Set
    End Property

    Dim _hsCode
    Public Property HSCode() As Object
        Get
            Return Me._hsCode
        End Get
        Set(ByVal value As Object)
            Me._hsCode = value
        End Set
    End Property

    Dim _amount
    Public Property Amount() As Object
        Get
            Return Me._amount
        End Get
        Set(ByVal value As Object)
            Me._amount = value
        End Set
    End Property

    Dim _price
    Public Property Price() As Object
        Get
            Return Me._price
        End Get
        Set(ByVal value As Object)
            Me._price = value
        End Set
    End Property

End Class