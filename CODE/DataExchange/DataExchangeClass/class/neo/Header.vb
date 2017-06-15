Namespace Header

    Public Class HeaderType
        Private _Party As Party
        Public Property Party() As Party
            Get
                Return Me._Party
            End Get
            Set(ByVal value As Party)
                Me._Party = value
            End Set
        End Property

        Private _userName As String
        Public Property userName() As String
            Get
                Return Me._userName
            End Get
            Set(ByVal value As String)
                Me._userName = value
            End Set
        End Property

        Private _password As String
        Public Property password() As String
            Get
                Return Me._password
            End Get
            Set(ByVal value As String)
                Me._password = value
            End Set
        End Property

        Private _securityToken As String
        Public Property securityToken() As String
            Get
                Return Me._securityToken
            End Get
            Set(ByVal value As String)
                Me._securityToken = value
            End Set
        End Property

        Private _txnDateTime As String
        Public Property txnDateTime() As String
            Get
                Return Me._txnDateTime
            End Get
            Set(ByVal value As String)
                Me._txnDateTime = value
            End Set
        End Property

        Private _batchID As String
        Public Property batchID() As String
            Get
                Return Me._batchID
            End Get
            Set(ByVal value As String)
                Me._batchID = value
            End Set
        End Property

        Private _refBatchID As String
        Public Property refBatchID() As String
            Get
                Return Me._refBatchID
            End Get
            Set(ByVal value As String)
                Me._refBatchID = value
            End Set
        End Property

        Private _fileName As String
        Public Property fileName() As String
            Get
                Return Me._fileName
            End Get
            Set(ByVal value As String)
                Me._fileName = value
            End Set
        End Property

        Private _currentDateTime As String
        Public Property currentDateTime() As String
            Get
                Return Me._currentDateTime
            End Get
            Set(ByVal value As String)
                Me._currentDateTime = value
            End Set
        End Property

        Private _transportType As String
        Public Property transportType() As String
            Get
                Return Me._transportType
            End Get
            Set(ByVal value As String)
                Me._transportType = value
            End Set
        End Property

        Private _priority As Boolean
        Public Property priority() As Boolean
            Get
                Return Me._priority
            End Get
            Set(ByVal value As Boolean)
                Me._priority = value
            End Set
        End Property

        Private _btsPartyName As String
        Public Property btsPartyName() As String
            Get
                Return Me._btsPartyName
            End Get
            Set(ByVal value As String)
                Me._btsPartyName = value
            End Set
        End Property

        Private _messageType As String
        Public Property messageType() As String
            Get
                Return Me._messageType
            End Get
            Set(ByVal value As String)
                Me._messageType = value
            End Set
        End Property

        Private _requestType As String
        Public Property requestType() As String
            Get
                Return Me._requestType
            End Get
            Set(ByVal value As String)
                Me._requestType = value
            End Set
        End Property
    End Class

    Public Class Party
        Private _code As String
        Public Property code() As String
            Get
                Return Me._code
            End Get
            Set(ByVal value As String)
                Me._code = value
            End Set
        End Property

        Private _location As String
        Public Property location() As String
            Get
                Return Me._location
            End Get
            Set(ByVal value As String)
                Me._location = value
            End Set
        End Property

        Private _name As String
        Public Property name() As String
            Get
                Return Me._name
            End Get
            Set(ByVal value As String)
                Me._name = value
            End Set
        End Property

    End Class

    Public Class NewHeader
        'party code
        Private _party_code As String
        Public Property party_code() As String
            Get
                Return Me._party_code
            End Get
            Set(ByVal value As String)
                Me._party_code = value
            End Set
        End Property

        'party location
        Private _party_location As String
        Public Property party_location() As String
            Get
                Return Me._party_location
            End Get
            Set(ByVal value As String)
                Me._party_location = value
            End Set
        End Property

        'party name
        Private _party_name As String
        Public Property party_name() As String
            Get
                Return Me._party_name
            End Get
            Set(ByVal value As String)
                Me._party_name = value
            End Set
        End Property

        'userName
        Private _userName As String
        Public Property userName() As String
            Get
                Return Me._userName
            End Get
            Set(ByVal value As String)
                Me._userName = value
            End Set
        End Property

        'password
        Private _password As String
        Public Property password() As String
            Get
                Return Me._password
            End Get
            Set(ByVal value As String)
                Me._password = value
            End Set
        End Property

        'securityToken
        Private _securityToken As String
        Public Property securityToken() As String
            Get
                Return Me._securityToken
            End Get
            Set(ByVal value As String)
                Me._securityToken = value
            End Set
        End Property

        'txnDateTime
        Private _txnDateTime As String
        Public Property txnDateTime() As String
            Get
                Return Me._txnDateTime
            End Get
            Set(ByVal value As String)
                Me._txnDateTime = value
            End Set
        End Property

        'batchID
        Private _batchID As String
        Public Property batchID() As String
            Get
                Return Me._batchID
            End Get
            Set(ByVal value As String)
                Me._batchID = value
            End Set
        End Property

        'refBatchID
        Private _refBatchID As String
        Public Property refBatchID() As String
            Get
                Return Me._refBatchID
            End Get
            Set(ByVal value As String)
                Me._refBatchID = value
            End Set
        End Property

        'fileName
        Private _fileName As String
        Public Property fileName() As String
            Get
                Return Me._fileName
            End Get
            Set(ByVal value As String)
                Me._fileName = value
            End Set
        End Property

        'currentDateTime
        Private _currentDateTime As String
        Public Property currentDateTime() As String
            Get
                Return Me._currentDateTime
            End Get
            Set(ByVal value As String)
                Me._currentDateTime = value
            End Set
        End Property

        'transportType
        Private _transportType As String
        Public Property transportType() As String
            Get
                Return Me._transportType
            End Get
            Set(ByVal value As String)
                Me._transportType = value
            End Set
        End Property

        'priority
        Private _priority As String
        Public Property priority() As String
            Get
                Return Me._priority
            End Get
            Set(ByVal value As String)
                Me._priority = value
            End Set
        End Property

        'btsPartyName
        Private _btsPartyName As String
        Public Property btsPartyName() As String
            Get
                Return Me._btsPartyName
            End Get
            Set(ByVal value As String)
                Me._btsPartyName = value
            End Set
        End Property

        'messageType
        Private _messageType As String
        Public Property messageType() As String
            Get
                Return Me._messageType
            End Get
            Set(ByVal value As String)
                Me._messageType = value
            End Set
        End Property

        'requestType
        Private _requestType As String
        Public Property requestType() As String
            Get
                Return Me._requestType
            End Get
            Set(ByVal value As String)
                Me._requestType = value
            End Set
        End Property

    End Class

End Namespace

