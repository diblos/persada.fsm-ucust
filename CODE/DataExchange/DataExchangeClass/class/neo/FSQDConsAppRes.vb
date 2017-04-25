Imports System.ComponentModel

Namespace FSQDConsAppRes
    Public Class FSQDConsAppRes
        Private _Header As Header.HeaderType
        Public Property Header() As Header.HeaderType
            Get
                Return Me._Header
            End Get
            Set(ByVal value As Header.HeaderType)
                Me._Header = value
            End Set
        End Property

        Private _Body As FSQDDeclarationResponse
        Public Property Body() As FSQDDeclarationResponse
            Get
                Return Me._Body
            End Get
            Set(ByVal value As FSQDDeclarationResponse)
                Me._Body = value
            End Set
        End Property
    End Class

    Public Class FSQDDeclarationResponse
        Private _MCKey As String
        Public Property MCKey() As String
            Get
                Return Me._MCKey
            End Get
            Set(ByVal value As String)
                Me._MCKey = value
            End Set
        End Property

        Private _MCValue As String
        Public Property MCValue() As String
            Get
                Return Me._MCValue
            End Get
            Set(ByVal value As String)
                Me._MCValue = value
            End Set
        End Property

        Private _CustomRegistrationNumber As String ' 1-35
        Public Property CustomRegistrationNumber() As String
            Get
                Return Me._CustomRegistrationNumber
            End Get
            Set(ByVal value As String)
                Me._CustomRegistrationNumber = value
            End Set
        End Property

        Private _CommentFromFQC As String ' 0-512
        Public Property CommentFromFQC() As String
            Get
                Return Me._CommentFromFQC
            End Get
            Set(ByVal value As String)
                Me._CommentFromFQC = value
            End Set
        End Property

        Private _ProcessDate As String ' 1-17
        Public Property ProcessDate() As String
            Get
                Return Me._ProcessDate
            End Get
            Set(ByVal value As String)
                Me._ProcessDate = value
            End Set
        End Property

        Private _InvoiceItems As InvoiceItem
        Public Property InvoiceItems() As InvoiceItem
            Get
                Return Me._InvoiceItems
            End Get
            Set(ByVal value As InvoiceItem)
                Me._InvoiceItems = value
            End Set
        End Property
    End Class

    Public Class InvoiceItem

        Enum enumApprovalStatus
            <Description("Approved")> A
            <Description("Rejection")> R
            <Description("Not applicable")> N
            <Description("Request Inspection")> I
            <Description("Conditional Release")> C
        End Enum

        Enum enumActionCode
            <Description("phyisical inspection")> I
            <Description("sampling")> S
            <Description("document inspection")> D
            <Description("Re-export")> R
        End Enum

        Private _ItemNumber As Integer
        Public Property ItemNumber() As Integer
            Get
                Return Me._ItemNumber
            End Get
            Set(ByVal value As Integer)
                Me._ItemNumber = value
            End Set
        End Property

        Private _HSCode As String ' 1-18
        Public Property HSCode() As String
            Get
                Return Me._HSCode
            End Get
            Set(ByVal value As String)
                Me._HSCode = value
            End Set
        End Property

        Private _ApprovalStatus As enumApprovalStatus
        Public Property ApprovalStatus() As enumApprovalStatus
            Get
                Return Me._ApprovalStatus
            End Get
            Set(ByVal value As enumApprovalStatus)
                Me._ApprovalStatus = value
            End Set
        End Property

        'In case of Approval status is inspection, the action is I - phyisical inspection, S - sampling, D - document inspection In case of Approval status is rejection, the action is  R - Re-export"
        Private _ActionCode As enumActionCode
        Public Property ActionCode() As enumActionCode
            Get
                Return Me._ActionCode
            End Get
            Set(ByVal value As enumActionCode)
                Me._ActionCode = value
            End Set
        End Property
    End Class

End Namespace


