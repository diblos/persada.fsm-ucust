Namespace deprecating
    Public Class Officer
        Dim _name
        Public Property Name() As Object
            Get
                Return Me._name
            End Get
            Set(ByVal value As Object)
                Me._name = value
            End Set
        End Property

        Dim _designation
        Public Property Designation() As Object
            Get
                Return Me._designation
            End Get
            Set(ByVal value As Object)
                Me._designation = value
            End Set
        End Property

    End Class
End Namespace


