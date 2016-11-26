Imports System.IO

Module fileman

    Public Function isExtensionRight(ByVal fPath As String, ByVal ext As String) As Boolean

        Dim extension As String = Path.GetExtension(fPath)
        If extension.ToUpper = ext.Trim().ToUpper Then
            Return True
        Else
            Return False
        End If

    End Function

    Public Sub DeleteFilesFromFolders(ByVal sourcePath As String)
        Try
            If (System.IO.Directory.Exists(sourcePath)) Then
                For Each fName As String In System.IO.Directory.GetFiles(sourcePath)
                    If System.IO.File.Exists(fName) Then
                        System.IO.File.Delete(fName)
                    End If
                Next
            End If
        Catch ex As Exception
            'MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub CreatePath(ByVal path As String)
        If Not System.IO.Directory.Exists(path) Then
            System.IO.Directory.CreateDirectory(path)
        End If
    End Sub

    Public Function GetFilePathList(ByVal DIR As String, ByVal EXT As String) As String()
        Dim strFileSize As String = ""
        Dim di As New IO.DirectoryInfo(DIR)
        Dim aryFi As IO.FileInfo() = di.GetFiles(EXT)
        Dim fi As IO.FileInfo

        Dim counter = 0
        Dim k As String()

        For Each fi In aryFi
            ReDim Preserve k(counter)
            k(counter) = fi.FullName
            counter += 1
        Next
        Return k

        di = Nothing
        aryFi = Nothing
        fi = Nothing
    End Function

    Public Function GetFilenameList(ByVal DIR As String, ByVal EXT As String) As String()
        Dim strFileSize As String = ""
        Dim di As New IO.DirectoryInfo(DIR)
        Dim aryFi As IO.FileInfo() = di.GetFiles(EXT)
        Dim fi As IO.FileInfo

        Dim counter = 0
        Dim k As String()

        For Each fi In aryFi
            ReDim Preserve k(counter)
            k(counter) = fi.Name

            counter += 1

        Next
        Return k
    End Function

    Public Function GetFileList0(ByVal DIR As String, ByVal EXT As String)
        Dim strFileSize As String = ""
        Dim di As New IO.DirectoryInfo(DIR)
        Dim aryFi As IO.FileInfo() = di.GetFiles(EXT)
        Dim fi As IO.FileInfo

        For Each fi In aryFi
            strFileSize = (Math.Round(fi.Length / 1024)).ToString()
            Debug.Print("File Name: {0}", fi.Name)
            Debug.Print("File Full Name: {0}", fi.FullName)
            Debug.Print("File Size (KB): {0}", strFileSize)
            Debug.Print("File Extension: {0}", fi.Extension)
            Debug.Print("Last Accessed: {0}", fi.LastAccessTime)
        Next
        Return Nothing
    End Function

    Public Sub deleteFile(ByVal filename As String)
        System.IO.File.Delete(filename)
    End Sub

    Public Sub WTF(ByVal path As String)
        Dim _Open As Boolean = True
        While _Open
            Try
                Dim fs As System.IO.FileStream = System.IO.File.Open(path, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.None)
                fs.Close()
                fs.Dispose()
                _Open = False
                Exit Try
            Catch
                Dim fullSizeImg As System.Drawing.Image = System.Drawing.Image.FromFile(path)
                fullSizeImg.Dispose()
                _Open = True
            End Try
        End While
    End Sub

#Region "Java based zip utility"

    'Public Function zipFile(ByVal filename As String, Optional ByVal prefix As String = Nothing)

    '    Dim Files() As String = {filename}
    '    Dim ZipName As String = ReplaceFileExt(filename) & ".zip"

    '    If prefix = Nothing Then
    '        ZipName = ReplaceFileExt(filename) & ".zip"
    '    Else
    '        ZipName = ReplaceFileExt(PrefixInsertion(filename, prefix)) & ".zip"
    '    End If

    '    Try
    '        If ZipUtility.CreateZip(Files, ZipName) = True Then
    '            'lstMsgs("Zip Created")
    '            Return True
    '        Else
    '            'lstMsgs("Zip Failed")
    '            Return False
    '        End If
    '    Catch ex As Exception
    '        'lstMsgs("An error has occured: " & ex.Message)
    '        Return False
    '    End Try
    'End Function

    'Public Function unzipFile(ByVal filename As String, Optional ByVal prefix As String = Nothing)

    '    Dim Files() As String = {filename}
    '    Dim ZipName As String = ReplaceFileExt(filename) & ".zip"

    '    If prefix = Nothing Then
    '        ZipName = ReplaceFileExt(filename) & ".zip"
    '    Else
    '        ZipName = ReplaceFileExt(PrefixInsertion(filename, prefix)) & ".zip"
    '    End If

    '    Try



    '        If ZipUtility.CreateZip(Files, ZipName) = True Then
    '            'lstMsgs("Zip Created")
    '            Return True
    '        Else
    '            'lstMsgs("Zip Failed")
    '            Return False
    '        End If
    '    Catch ex As Exception
    '        'lstMsgs("An error has occured: " & ex.Message)
    '        Return False
    '    End Try
    'End Function

#End Region

    Private Function PrefixInsertion(ByVal filename As String, ByVal prefix As String) As String
        Dim tmp = filename.Split("\")
        For i = 0 To UBound(tmp)
            Select Case i
                Case 0
                    filename = tmp(0) & "\"
                Case UBound(tmp)
                    filename &= prefix & "_" & tmp(i)
                Case Else
                    filename &= tmp(i) & "\"
            End Select
        Next
        Return filename
    End Function

    Private Function ReplaceFileExt(ByVal filename As String) As String
        Dim tmp = filename.Split(".")
        Return tmp(0)
    End Function

    Public Function writeFile(ByVal str As Object, Optional ByVal fileNameStr As String = Nothing)
        Dim currentfileName As String = "temp.vbs"
        If fileNameStr <> Nothing Then
            currentfileName = fileNameStr
        End If
        'Check for existence of logger file
        If System.IO.File.Exists(currentfileName) Then
            Try
                'Dim fs As FileStream = New FileStream(currentfileName, FileMode.Append, FileAccess.Write)
                Dim fs As IO.FileStream = New System.IO.FileStream(currentfileName, IO.FileMode.Open, IO.FileAccess.Write)
                Dim sw As IO.StreamWriter = New IO.StreamWriter(fs)
                sw.WriteLine(str.ToString)
                sw.Close()
                fs.Close()
            Catch Ex As Exception
                'LogInfo(Ex)
            End Try
        Else
            'If file doesn't exist create one
            Try
                Dim fileStream As IO.FileStream = IO.File.Create(currentfileName)
                Dim sw As IO.StreamWriter = New IO.StreamWriter(fileStream)
                sw.WriteLine(str.ToString)
                sw.Close()
                fileStream.Close()
            Catch ex As Exception
                'LogInfo(ex)
            End Try
        End If
    End Function

    Public Sub MoveFiles(ByVal sourcePath As String, ByVal DestinationPath As String)
        If (IO.Directory.Exists(sourcePath)) Then
            For Each fName As String In IO.Directory.GetFiles(sourcePath)
                If IO.File.Exists(fName) Then
                    Dim dFile As String = String.Empty
                    dFile = IO.Path.GetFileName(fName)
                    Dim dFilePath As String = String.Empty
                    dFilePath = DestinationPath + dFile
                    IO.File.Move(fName, dFilePath)
                End If
            Next
        End If
    End Sub

    Public Sub CopyFiles(ByVal sourcePath As String, ByVal DestinationPath As String)
        If (IO.Directory.Exists(sourcePath)) Then
            For Each fName As String In IO.Directory.GetFiles(sourcePath)
                If IO.File.Exists(fName) Then
                    Dim dFile As String = String.Empty
                    dFile = IO.Path.GetFileName(fName)
                    Dim dFilePath As String = String.Empty
                    dFilePath = DestinationPath + dFile
                    IO.File.Copy(fName, dFilePath, True)
                End If
            Next
        End If
    End Sub

    Public Function MoveAfile(ByVal FileToCopy As String, ByVal NewCopy As String)
        Try
            If System.IO.File.Exists(FileToCopy) = True Then
                System.IO.File.Move(FileToCopy, NewCopy)
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Debug.Print(ex.Message)
            Return False
        End Try
    End Function

    Public Function CopyAfile(ByVal FileToCopy As String, ByVal NewCopy As String)
        Try
            If System.IO.File.Exists(FileToCopy) = True Then
                System.IO.File.Copy(FileToCopy, NewCopy)
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Debug.Print(ex.Message)
            Return False
        End Try
    End Function

    ' TEST *************************************************************
    Public Function PerfectPath(ByVal sPath As String)
        If Right(sPath, 1) <> "\" Then
            sPath = sPath & "\"
        End If
        Return sPath
    End Function

    Public Function RemoveFileExt(ByVal filename As String) As String
        Dim tmp = filename.Split(".")
        Return tmp(0)
    End Function

    Public Function GetFileExt(ByVal filename As String) As String
        Dim tmp = filename.Split(".")
        Return tmp(1)
    End Function

    Public Function GetFileName(ByVal fullpath As String) As String
        Dim tmp = fullpath.Split("\")
        Return tmp(tmp.Length - 1)
    End Function

    Public Function GetWorkingDir(ByVal fullpath As String) As String
        Dim tmp = fullpath.Split("\")
        If tmp.Length = 1 Then
            Return System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly.Location)
        Else
            Return System.IO.Path.GetDirectoryName(fullpath)
        End If
    End Function

    Public Function FileExists(ByVal FileFullPath As String) _
     As Boolean

        Dim f As New IO.FileInfo(FileFullPath)
        Return f.Exists

    End Function

    Public Function FolderExists(ByVal FolderPath As String) _
   As Boolean

        Dim f As New IO.DirectoryInfo(FolderPath)
        Return f.Exists

    End Function

End Module
