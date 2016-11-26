Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports DataExchangeClass.Common
Imports System.IO
Imports System.Net
Imports System.Threading
Imports System.Web.Security
Imports System.Xml.Serialization
Imports System.Text.RegularExpressions
'FOR DATABASE ACCESS
Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Data.Odbc

<System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class Receiver
    Inherits System.Web.Services.WebService

    Dim dService As New DataExchangeClass.Data

    <WebMethod(Description:="Consignment Approval Request Data")> _
    Public Function ConsignmentApprovalData(ByVal ConsigmentApprovalRequest As DataExchangeClass.ConsigmentApprovalRequest) As DataExchangeClass.Acknowledge
        Dim response As New DataExchangeClass.Acknowledge
        Try
            nEventLOG("Logs", SerializeIT(ConsigmentApprovalRequest))
            If dService.CAInsert(ConsigmentApprovalRequest) <= 0 Then
                With response
                    .Timestamp = Now
                    .Status = "Fail"
                    .Indicator = "F"
                End With
            Else
                With response
                    .Timestamp = Now
                    .Status = "Success"
                    .Indicator = "S"
                End With
            End If

            Return response
        Catch ex As Exception
            With response
                .Timestamp = Now
                .Status = "Fail"
                .Indicator = "F"
            End With
            Return response
        End Try
    End Function

   

#Region "Private Methods"

    'this method is used by the class but is not directly web-accessible. 
    'note the lack of a "web method" identifier.
    Function ConnectToDatabase() As SqlConnection
        Dim SqlConnectionString As String = _
             "Data Source=yourdatabaseserver\metrix;Initial Catalog=yourdatabasename;User Id=sa;Password=yourpassword"
        Dim cn As New SqlConnection(SqlConnectionString)
        cn.Open()
        Return cn
    End Function

    Private Sub Process(ByVal data As DataExchangeClass.Officer)
        Try

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

#End Region

End Class