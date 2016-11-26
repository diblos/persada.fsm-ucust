Public Class EximInfo
    Dim _id
    Public Property ID() As Object
        Get
            Return Me._id
        End Get
        Set(ByVal value As Object)
            Me._id = value
        End Set
    End Property

    Dim _name
    Public Property Name() As Object
        Get
            Return Me._name
        End Get
        Set(ByVal value As Object)
            Me._name = value
        End Set
    End Property

    Dim _address
    Public Property Address() As Object
        Get
            Return Me._address
        End Get
        Set(ByVal value As Object)
            Me._address = value
        End Set
    End Property
End Class
