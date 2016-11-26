Public Class ProcessInfo
    Dim _datetime
    Public Property Datetime() As Object
        Get
            Return Me._datetime
        End Get
        Set(ByVal value As Object)
            Me._datetime = value
        End Set
    End Property
End Class
