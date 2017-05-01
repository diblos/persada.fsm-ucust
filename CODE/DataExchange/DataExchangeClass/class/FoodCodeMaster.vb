'=====================================================
'FOOD CODE MASTER FILE [FOSIM TO UCUSTOM]
'=====================================================

Namespace deprecating
    Public Class FoodCodeMaster

        Dim _fcocode As String 'FoodCode
        Public Property FCOCode() As String
            Get
                Return Me._fcocode
            End Get
            Set(ByVal value As String)
                Me._fcocode = value
            End Set
        End Property

        Dim _fcodesc As String 'FoodCode description
        Public Property FCODescription() As String
            Get
                Return Me._fcodesc
            End Get
            Set(ByVal value As String)
                Me._fcodesc = value
            End Set
        End Property

        Dim _hs_id As String
        Public Property HS_ID() As String
            Get
                Return Me._hs_id
            End Get
            Set(ByVal value As String)
                Me._hs_id = value
            End Set
        End Property

        Dim _rstatus As Int16 'Food Code status
        Public Property RStatus() As Int16
            Get
                Return Me._rstatus
            End Get
            Set(ByVal value As Int16)
                Me._rstatus = value
            End Set
        End Property

        Dim _lmby As String 'Last modified person
        Public Property LastModifiedBy() As String
            Get
                Return Me._lmby
            End Get
            Set(ByVal value As String)
                Me._lmby = value
            End Set
        End Property

        Dim _lmdt As Date 'Last modified date time
        Public Property LastModifiedDate() As Date
            Get
                Return Me._lmdt
            End Get
            Set(ByVal value As Date)
                Me._lmdt = value
            End Set
        End Property

    End Class
End Namespace


