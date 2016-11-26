Public Class Errors
    Dim _code As String
    Public Property Code() As String
        Get
            Return Me._code
        End Get
        Set(ByVal value As String)
            Me._code = value
        End Set
    End Property

    Dim _desc As String
    Public Property Description() As String
        Get
            Return Me._desc
        End Get
        Set(ByVal value As String)
            Me._desc = value
        End Set
    End Property

    Dim _action As String
    Public Property Action() As String
        Get
            Return Me._action
        End Get
        Set(ByVal value As String)
            Me._action = value
        End Set
    End Property

    Public Sub New()
        Me._code = String.Empty
        Me._desc = String.Empty
        Me._action = String.Empty
    End Sub

    Public Sub New(ByVal code As String, ByVal desc As String, ByVal action As String)
        Me._code = code
        Me._desc = desc
        Me._action = action
    End Sub
End Class
