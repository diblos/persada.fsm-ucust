Public Class Acknowledge
    Dim _indicator
    Public Property Indicator() As Object
        Get
            Return Me._indicator
        End Get
        Set(ByVal value As Object)
            Me._indicator = value
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

    Dim _timestamp
    Public Property Timestamp() As Object
        Get
            Return Me._timestamp
        End Get
        Set(ByVal value As Object)
            Me._timestamp = value
        End Set
    End Property

End Class



