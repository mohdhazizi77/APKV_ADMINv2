Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization
Public Class create_user
    Inherits System.Web.UI.UserControl
    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Dim No As String = ""
    Dim NamaKolej As String = ""
    Dim LoginID As String = ""
    Dim Password As String = ""
    Dim UserType As String = ""
    Dim Nama As String = ""
    Dim MYKAD As String = ""
    Dim Negeri As String = ""
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                kpmkv_Kumpulan_Pengguna()
                ddlKumpulan.Text = "0"

                kpmkv_negeri_list()
                ddlNegeri.Text = "0"
            End If

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message
        End Try
    End Sub
    Private Sub kpmkv_negeri_list()
        strSQL = "SELECT Negeri FROM kpmkv_negeri ORDER BY Negeri"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlNegeri.DataSource = ds
            ddlNegeri.DataTextField = "Negeri"
            ddlNegeri.DataValueField = "Negeri"
            ddlNegeri.DataBind()

            '--ALL
            ddlNegeri.Items.Add(New ListItem("-Pilih-", "0"))

        Catch ex As Exception
            'lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub
    Private Sub kpmkv_Kumpulan_Pengguna()

        If Session("LoginID") = "sppmaln" Then

            strSQL = "SELECT UserGroup FROM tbl_ctrl_usergroup WHERE UserGroup = 'AKADEMIK-PEMERIKSA' OR UserGroup = 'VOKASIONAL-PEMERIKSA' ORDER BY UserGroup ASC"

        Else

            strSQL = "SELECT UserGroup FROM tbl_ctrl_usergroup WHERE UserGroup NOT IN ('PENSYARAH', 'PELAJAR', 'SYSADMIN') ORDER BY UserGroup ASC"

        End If

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlKumpulan.DataSource = ds
            ddlKumpulan.DataTextField = "UserGroup"
            ddlKumpulan.DataValueField = "UserGroup"
            ddlKumpulan.DataBind()

            '--ALL
            ddlKumpulan.Items.Add(New ListItem("-Pilih", "0"))

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message


        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Function ValidatePage() As Boolean
        '--txtNama
        If txtNama.Text.Length = 0 Then
            lblMsg.Text = "Sila masukkan Nama!"
            txtNama.Focus()
            Return False
        End If

        '--txtMYKAD
        If txtMYKAD.Text.Length = 0 Then
            lblMsg.Text = "Sila masukkan MYKAD!"
            txtMYKAD.Focus()
            Return False
        ElseIf oCommon.isMyKad2(txtMYKAD.Text) = False Then
            lblMsg.Text = "Huruf tidak dibenarkan .Sila masukkan no MYKAD [######06####]"
            txtMYKAD.Focus()
            Return False
        End If

        strSQL = "SELECT MYKAD FROM kpmkv_users WHERE MYKAD='" & txtMYKAD.Text & "'"
        If oCommon.isExist(strSQL) = True Then
            lblMsg.Text = "MYKAD telah digunakan. Pendaftaran Pengguna tidak berjaya."
            Return False
        End If


        '--txtJawatan
        If ddlKumpulan.SelectedValue = "0" Then
            lblMsg.Text = "Sila pilih Kumpulan Pengguna!"
            ddlKumpulan.Focus()
            Return False
        End If


        '--txtNegeri
        If ddlKumpulan.SelectedValue = "0" Then
            lblMsg.Text = "Sila pilih Negeri!"
            ddlKumpulan.Focus()
            Return False
        End If


        '--txtEmail
        If txtEmail.Text.Length = 0 Then
            lblMsg.Text = "Sila masukkan Email!"
        ElseIf oCommon.isEmail(txtEmail.Text) = False Then
            lblMsg.Text = "Emel Pengguna tidak sah. (Contoh: user@mail.com)"
            txtEmail.Focus()
            Return False
        End If

        ''--txtlogin
        'If txtLogin.Text.Length = 0 Then
        '    lblMsg.Text = "Sila masukkan Login ID!"
        '    Return False
        'End If

        ''--txtpwd
        'If txtPwd.Text.Length = 0 Then
        '    Return False
        'End If

        Return True
    End Function

    Protected Sub btnCreate_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCreate.Click
        lblMsg.Text = ""

        Try
            '--validate
            If ValidatePage() = False Then
                divMsg.Attributes("class") = "error"
                Exit Sub
            End If

            '--execute
            If kpmkv_user_create() = True Then
                divMsg.Attributes("class") = "info"
                lblMsg.Text = "Pengguna baru berjaya didaftarkan"
            Else
                divMsg.Attributes("class") = "error"
                lblMsg.Text = "Pengguna baru tidak berjaya didaftarkan"

            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message
        End Try

    End Sub

    Private Function kpmkv_user_create() As Boolean

        Dim strRecordID As String = oCommon.getGUID

        strSQL = "INSERT INTO kpmkv_users (RecordID,LoginID,Pwd,UserType,Negeri,Nama,Mykad,Tel,Email,StatusID) "
        strSQL += "VALUES ('" & strRecordID & "','" & oCommon.FixSingleQuotes(txtLogin.Text) & "','" & oCommon.FixSingleQuotes(txtPwd.Text) & "','" & ddlKumpulan.Text & "','" & ddlNegeri.Text & "','" & oCommon.FixSingleQuotes(txtNama.Text.ToUpper) & "','" & oCommon.FixSingleQuotes(txtMYKAD.Text) & "','" & oCommon.FixSingleQuotes(txtTel.Text) & "','" & oCommon.FixSingleQuotes(txtEmail.Text) & "','2')"
        strRet = oCommon.ExecuteSQL(strSQL)
        If Not strRet = 0 Then
            divMsg.Attributes("class") = "error"
            Return False
        Else
            Return True
        End If

    End Function

    Private Sub btnFile_Click(sender As Object, e As EventArgs) Handles btnFile.Click
        lblMsg.Text = ""
        Response.ContentType = "Application/xlsx"
        Response.AppendHeader("Content-Disposition", "attachment; filename=IMPORT_PENGGUNA.xlsx")
        Response.TransmitFile(Server.MapPath("~/sample_data/IMPORT_PENGGUNA.xlsx"))
        Response.End()
    End Sub

    Private Sub btnUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpload.Click
        lblMsg.Text = ""
        Try
            '--upload excel
            If ImportExcel() = True Then
                divMsg.Attributes("class") = "info"
            Else
            End If
        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        End Try

    End Sub

    Private Function ImportExcel() As Boolean
        Dim path As String = String.Concat(Server.MapPath("~/inbox/"))

        If FlUploadcsv.HasFile Then
            Dim rand As Random = New Random()
            Dim randNum = rand.Next(1000)
            Dim fullFileName As String = path + oCommon.getRandom + "-" + FlUploadcsv.FileName
            FlUploadcsv.PostedFile.SaveAs(fullFileName)

            '--required ms access engine
            Dim excelConnectionString As String = ("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & fullFileName & ";Extended Properties=Excel 12.0;")
            Dim connection As OleDbConnection = New OleDbConnection(excelConnectionString)
            Dim command As OleDbCommand = New OleDbCommand("SELECT * FROM [PENGGUNA$]", connection)
            Dim da As OleDbDataAdapter = New OleDbDataAdapter(command)
            Dim ds As DataSet = New DataSet

            Try
                connection.Open()
                da.Fill(ds)
                Dim validationMessage As String = ValidateSiteData(ds)
                If validationMessage = "" Then
                    SaveSiteData(ds)

                Else
                    'lblMsgTop.Text = "Muatnaik GAGAL!. Lihat mesej dibawah."
                    divMsg.Attributes("class") = "error"
                    lblMsg.Text = "Kesalahan Kemasukkan Maklumat Pengguna:<br />" & validationMessage
                    Return False
                End If

                da.Dispose()
                connection.Close()
                command.Dispose()

            Catch ex As Exception
                lblMsg.Text = "System Error:" & ex.Message
                Return False
            Finally
                If connection.State = ConnectionState.Open Then
                    connection.Close()
                End If
            End Try

        Else
            divMsg.Attributes("class") = "error"
            lblMsg.Text = "Please select file to upload!"
            Return False
        End If

        Return True

    End Function

    Private Sub refreshVar()

        No = ""
        NamaKolej = ""
        LoginID = ""
        Password = ""
        UserType = ""
        Nama = ""
        MYKAD = ""
        Negeri = ""

    End Sub

    Protected Function ValidateSiteData(ByVal SiteData As DataSet) As String
        Try
            'Loop through DataSet and validate data
            'If data is bad, bail out, otherwise continue on with the bulk copy

            Dim strMsg As String = ""
            Dim sb As StringBuilder = New StringBuilder()
            For i As Integer = 0 To SiteData.Tables(0).Rows.Count - SiteData.Tables(0).Rows(i).Item("No")

                refreshVar()
                strMsg = ""

                'No
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("No")) Then
                    No = SiteData.Tables(0).Rows(i).Item("No")
                Else
                    strMsg += " Sila isi No|"
                End If

                'Nama Kolej
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("Nama Kolej")) Then

                    NamaKolej = SiteData.Tables(0).Rows(i).Item("Nama Kolej")

                    strSQL = " SELECT RecordID FROM kpmkv_kolej WHERE Nama = '" & NamaKolej & "'"
                    Dim RecordID As String = oCommon.getFieldValue(strSQL)

                    If RecordID = "" Then

                        strMsg += " Sila masukkan Nama Kolej yang betul|"

                    End If

                Else

                    strMsg += " Sila isi Nama Kolej|"

                End If

                'Login ID
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("ID Pengguna")) Then
                    LoginID = SiteData.Tables(0).Rows(i).Item("ID Pengguna")
                Else
                    strMsg += " Sila isi ID Pengguna|"
                End If

                'Password
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("Kata Laluan Pengguna")) Then
                    Password = SiteData.Tables(0).Rows(i).Item("Kata Laluan Pengguna")
                Else
                    strMsg += " Sila isi Kata Laluan Pengguna|"
                End If

                'UserType
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("Jenis Pengguna")) Then
                    UserType = SiteData.Tables(0).Rows(i).Item("Jenis Pengguna")
                Else
                    strMsg += " Sila isi Jenis Pengguna|"
                End If

                'Nama
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("Nama")) Then
                    Nama = SiteData.Tables(0).Rows(i).Item("Nama")
                Else
                    strMsg += " Sila isi Nama|"
                End If

                'MYKAD
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("MYKAD")) Then
                    MYKAD = SiteData.Tables(0).Rows(i).Item("MYKAD")

                    If Not MYKAD.Length = 12 Then

                        strMsg += " Sila isi MYKAD dengan format: ############|"

                    End If

                Else
                    strMsg += " Sila isi MYKAD dengan format: ############|"
                End If

                'Jantina
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("Negeri")) Then
                    Negeri = SiteData.Tables(0).Rows(i).Item("Negeri")

                    strSQL = "SELECT NegeriID FROM kpmkv_negeri WHERE Negeri = '" & Negeri & "'"
                    Dim NegeriID As String = oCommon.getFieldValue(strSQL)

                    If NegeriID = "" Then

                        strMsg += " Sila masukkan Negeri yang betul|"

                    End If

                Else
                    strMsg += " Sila isi Negeri|"
                End If

                If Not strMsg.Length = 0 Then
                    strMsg = "No. " & No & " :" & strMsg
                    strMsg += "<br/>"
                End If

                sb.Append(strMsg)

            Next
            Return sb.ToString()
        Catch ex As Exception
            Return ex.Message
        End Try

    End Function

    Private Function SaveSiteData(ByVal SiteData As DataSet) As String
        lblMsg.Text = ""

        'Dim str As String
        Try

            Dim sb As StringBuilder = New StringBuilder()

            For i As Integer = 0 To SiteData.Tables(0).Rows.Count - SiteData.Tables(0).Rows(i).Item("No")

                No = SiteData.Tables(0).Rows(i).Item("No")
                NamaKolej = SiteData.Tables(0).Rows(i).Item("Nama Kolej")
                LoginID = SiteData.Tables(0).Rows(i).Item("ID Pengguna")
                Password = SiteData.Tables(0).Rows(i).Item("Kata Laluan Pengguna")
                UserType = SiteData.Tables(0).Rows(i).Item("Jenis Pengguna")
                Nama = SiteData.Tables(0).Rows(i).Item("Nama")
                MYKAD = SiteData.Tables(0).Rows(i).Item("MYKAD")
                Negeri = SiteData.Tables(0).Rows(i).Item("Negeri")

                strSQL = "SELECT RecordID FROM kpmkv_kolej WHERE Nama = '" & NamaKolej & "'"
                Dim RecordID As String = oCommon.getFieldValue(strSQL)

                strSQL = "SELECT UserID FROM kpmkv_users WHERE Nama = '" & Nama & "'"
                Dim UserID As String = oCommon.getFieldValue(strSQL)

                If UserID = "" Then

                    strSQL = "INSERT INTO kpmkv_users (RecordID, LoginID, Pwd, UserType, Nama, Mykad, Negeri)"
                    strSQL += " VALUES ('" & RecordID & "', '" & LoginID & "', '" & Password & "', '" & UserType & "', '" & Nama & "', '" & MYKAD & "', '" & Negeri & "')"

                Else

                    strSQL = "  UPDATE kpmkv_users SET 
                                RecordID = '" & RecordID & "'
                                LoginID = '" & LoginID & "'
                                Pwd = '" & Password & "'
                                UserType = '" & UserType & "'
                                Nama = '" & Nama & "'
                                Mykad = '" & MYKAD & "'
                                Negeri = '" & Negeri & "'
                                WHERE UserID = '" & UserID & "'"

                End If

                strRet = oCommon.ExecuteSQL(strSQL)

                If strRet = "0" Then

                    divMsg.Attributes("class") = "info"
                    lblMsg.Text = "Pengguna berjaya didaftarkan"

                Else

                    divMsg.Attributes("class") = "error"
                    lblMsg.Text = "Pengguna tidak berjaya didaftarkan"

                End If

            Next

        Catch ex As Exception
            divMsg.Attributes("class") = "error"
            lblMsg.Text = "Pengguna tidak berjaya didaftarkan"
            Return False
        End Try

        Return True

    End Function

    Public Function FileIsLocked(ByVal strFullFileName As String) As Boolean
        Dim blnReturn As Boolean = False
        Dim fs As System.IO.FileStream

        Try
            fs = System.IO.File.Open(strFullFileName, IO.FileMode.OpenOrCreate, IO.FileAccess.Read, IO.FileShare.None)
            fs.Close()
        Catch ex As System.IO.IOException
            divMsg.Attributes("class") = "error"
            lblMsg.Text = "Error Message FileIsLocked:" & ex.Message
            blnReturn = True
        End Try

        Return blnReturn
    End Function

End Class