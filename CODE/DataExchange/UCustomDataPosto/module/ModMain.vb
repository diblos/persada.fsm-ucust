Imports System.Configuration.ConfigurationManager
Imports System.IO
Imports System.Net

Module ModMain
    Public WEB_SERVICE_ASMX_URL As String = "http://localhost:57970/Receiver.asmx"

    Public Const SOAP_ENVELOPE As String = _
"<soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">" & _
"<soap:Body>@DATA</soap:Body></soap:Envelope>"

    Public Const CA_ENV As String = "<ConsignmentApprovalData xmlns=""http://tempuri.org/"">@DATA</ConsignmentApprovalData>"
    Public Const K1_ENV As String = "<K1Data xmlns=""http://tempuri.org/"">@DATA</K1Data>"

    Public ftpServ As String = "127.0.0.1"
    Public UID As String = "ucustom"
    Public PWD As String = "ucustom"

    Public Sub ReadConfiguration()
        If Not AppSettings("WEB.SERVICE.ASMX.URL") = "" Then WEB_SERVICE_ASMX_URL = AppSettings("WEB.SERVICE.ASMX.URL")
        If Not AppSettings("FTP.SERVER") = "" Then ftpServ = AppSettings("FTP.SERVER")
        If Not AppSettings("FTP.UID") = "" Then UID = AppSettings("FTP.UID")
        If Not AppSettings("FTP.PWD") = "" Then PWD = AppSettings("FTP.PWD")
    End Sub

    Public Sub UploadFtpFile(folderName As String, fileName As String)

        Dim request As FtpWebRequest
        Try
            Dim absoluteFileName As String = Path.GetFileName(fileName)

            request = TryCast(WebRequest.Create(New Uri(String.Format("ftp://{0}/{1}/{2}", ftpServ, folderName, absoluteFileName))), FtpWebRequest)
            request.Method = WebRequestMethods.Ftp.UploadFile
            request.UseBinary = 1
            request.UsePassive = 1
            request.KeepAlive = False
            request.Credentials = New NetworkCredential(UID, PWD)
            request.ConnectionGroupName = "group"

            Using fs As FileStream = File.OpenRead(fileName)
                Dim buffer As Byte() = New Byte(fs.Length - 1) {}
                fs.Read(buffer, 0, buffer.Length)
                fs.Close()
                Dim requestStream As Stream = request.GetRequestStream()
                requestStream.Write(buffer, 0, buffer.Length)
                requestStream.Flush()
                requestStream.Close()
            End Using

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ftpfolder(ByVal request As FtpWebRequest)
        request = TryCast(WebRequest.Create(New Uri(String.Format("ftp://{0}/{1}/", "127.0.0.1", "testFolder"))), FtpWebRequest)
        request.Method = WebRequestMethods.Ftp.MakeDirectory
        Dim ftpResponse As FtpWebResponse = DirectCast(request.GetResponse(), FtpWebResponse)
    End Sub

End Module
