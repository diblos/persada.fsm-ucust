Namespace deprecating
    Public Class DeclarantInfo
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

        Dim _status
        Public Property Status() As Object
            Get
                Return Me._status
            End Get
            Set(ByVal value As Object)
                Me._status = value
            End Set
        End Property

    End Class
End Namespace

