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

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class uc
    Inherits System.Web.Services.WebService

    <WebMethod(Description:="Post a Consignment Approval Response Data")> _
    Function ConsignmentApprovalData(ByVal ConsigmentApprovalResponse As DataExchangeClass.deprecating.ConsigmentApprovalResponse) As DataExchangeClass.Acknowledge
        Dim response As New DataExchangeClass.Acknowledge
        Try
            nEventLOG("Logs", SerializeIT(ConsigmentApprovalResponse))
            With response
                .Timestamp = Now
                .Status = "Success"
                .Indicator = "S"
            End With
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

    '<WebMethod(Description:="Post an array of Consignment Approval Response Data")> _
    'Function ConsignmentApprovalData(ByVal ArrayOfConsigmentApprovalResponse() As DataExchangeClass.ConsigmentApprovalResponse) As DataExchangeClass.Acknowledge
    '    Dim response As New DataExchangeClass.Acknowledge
    '    Try
    '        nEventLOG("Logs", SerializeIT(ArrayOfConsigmentApprovalResponse))
    '        With response
    '            .Timestamp = Now
    '            .Status = "Success"
    '            .Indicator = "S"
    '        End With
    '        Return response
    '    Catch ex As Exception
    '        With response
    '            .Timestamp = Now
    '            .Status = "Fail"
    '            .Indicator = "F"
    '        End With
    '        Return response
    '    End Try
    'End Function

End Class