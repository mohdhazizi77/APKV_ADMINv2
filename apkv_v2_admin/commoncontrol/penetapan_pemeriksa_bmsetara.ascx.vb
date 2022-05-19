Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization
Public Class penetapan_pemeriksa_bmsetara
    Inherits System.Web.UI.UserControl
    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Dim No As String = ""
    Dim NamaKolej As String = ""
    Dim LoginID As String = ""
    Dim Password As String = ""
    Dim Nama As String = ""
    Dim MYKAD As String = ""
    Dim Negeri As String = ""

    Dim NamaPemeriksa As String = ""
    Dim Tahun As String = ""
    Dim Semester As String = ""
    Dim Sesi As String = ""
    Dim KodKolej As String = ""
    Dim KodKursus As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                kpmkv_kodpusat_list()

                kpmkv_tahun_list()
                ddlTahun.Text = Now.Year

                kpmkv_pemeriksa_list()
            End If

        Catch ex As Exception
            lblmsg.Text = "System Error:" & ex.Message
        End Try

    End Sub

    Private Sub kpmkv_kodpusat_list()
        strSQL = "SELECT Kod, RecordID FROM kpmkv_kolej ORDER BY Kod ASC "

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlKodPusat.DataSource = ds
            ddlKodPusat.DataTextField = "Kod"
            ddlKodPusat.DataValueField = "RecordID"
            ddlKodPusat.DataBind()

            ddlKodPusat.Items.Insert(0, "-PILIH-")

        Catch ex As Exception
            lblmsg.Text = "System Error:" & ex.Message
        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub kpmkv_tahun_list()
        strSQL = "SELECT Tahun FROM kpmkv_tahun ORDER BY TahunID"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlTahun.DataSource = ds
            ddlTahun.DataTextField = "Tahun"
            ddlTahun.DataValueField = "Tahun"
            ddlTahun.DataBind()

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try

    End Sub
    Private Sub kpmkv_pemeriksa_list()
        strSQL = "SELECT Nama FROM kpmkv_users WHERE UserType='PEMERIKSA' ORDER BY Nama"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlPemeriksa.DataSource = ds
            ddlPemeriksa.DataTextField = "Nama"
            ddlPemeriksa.DataValueField = "Nama"
            ddlPemeriksa.DataBind()

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try

    End Sub
    Protected Sub btnSimpan_Click(sender As Object, e As EventArgs) Handles btnSimpan.Click
        lblmsg.Text = ""

        strSQL = "SELECT UserID FROM kpmkv_users WHERE Nama='" & oCommon.FixSingleQuotes(ddlPemeriksa.Text) & "'"
        Dim StrUserID As Integer = oCommon.getFieldValue(strSQL)

        strSQL = "SELECT Kod FROM kpmkv_kolej WHERE RecordID='" & ddlKodPusat.SelectedValue & "'"
        Dim strPusat As String = oCommon.getFieldValue(strSQL)

        Try
            strSQL = "INSERT INTO kpmkv_pemeriksa(NamaPemeriksa, UserID, Tahun, Semester, Sesi, KodKolej, KertasNo,Jenis) "
            strSQL += "VALUES ('" & oCommon.FixSingleQuotes(ddlPemeriksa.Text) & "','" & StrUserID & "','" & ddlTahun.Text & "','4','" & chkSesi.Text & "','" & strPusat & "','" & chkKertas.Text & "','BM')"
            strRet = oCommon.ExecuteSQL(strSQL)
            If strRet = "0" Then
                divMsg.Attributes("class") = "info"
                lblmsg.Text = "Berjaya mendaftarkan Pemeriksa."
            Else
                divMsg.Attributes("class") = "error"
            End If

            strRet = BindData(datRespondent)
        Catch ex As Exception
            lblmsg.Text = ex.Message
        End Try

    End Sub
    Private Function BindData(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 120

        Try
            myDataAdapter.Fill(myDataSet, "myaccount")

            If myDataSet.Tables(0).Rows.Count = 0 Then
                divMsg.Attributes("class") = "error"
                lblmsg.Text = "Rekod tidak dijumpai!"
            Else
                divMsg.Attributes("class") = "info"
                lblmsg.Text = "Jumlah Rekod#:" & myDataSet.Tables(0).Rows.Count
            End If

            gvTable.DataSource = myDataSet
            gvTable.DataBind()
            objConn.Close()
        Catch ex As Exception
            lblmsg.Text = "System Error:" & ex.Message
            Return False
        End Try

        Return True

    End Function
    Private Function getSQL() As String
        Dim tmpSQL As String = ""
        Dim strWhere As String = ""
        Dim strOrder As String = " ORDER BY kpmkv_pemeriksa.PemeriksaID, kpmkv_pemeriksa.Tahun, kpmkv_pemeriksa.Sesi ASC"
        'FileID,Tahun,Sesi,NamaKolej,KolejID
        '--not deleted
        tmpSQL = "SELECT kpmkv_pemeriksa.PemeriksaID, kpmkv_pemeriksa.Tahun,  kpmkv_pemeriksa.Semester, kpmkv_pemeriksa.Sesi, kpmkv_pemeriksa.NamaPemeriksa,"
        tmpSQL += " kpmkv_pemeriksa.KodKolej, kpmkv_pemeriksa.KertasNo FROM kpmkv_pemeriksa"
        strWhere = " WHERE kpmkv_pemeriksa.PemeriksaID IS NOT NULL AND kpmkv_pemeriksa.Semester='4'"
        strWhere += " AND Jenis='BM'"

        '--tahun
        If Not ddlTahun.Text = "PILIH" Then
            strWhere += " AND kpmkv_pemeriksa.Tahun ='" & ddlTahun.Text & "'"
        End If
        '--sesi
        If Not chkSesi.Text = "" Then
            strWhere += " AND kpmkv_pemeriksa.Sesi ='" & chkSesi.Text & "'"
        End If

        getSQL = tmpSQL & strWhere & strOrder
        ''--debug
        'Response.Write(getSQL)

        Return getSQL

    End Function
    Private Sub datRespondent_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles datRespondent.PageIndexChanging
        datRespondent.PageIndex = e.NewPageIndex
        strRet = BindData(datRespondent)

    End Sub

    Private Sub datRespondent_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSelectEventArgs) Handles datRespondent.SelectedIndexChanging
        Dim strKeyID As String = datRespondent.DataKeys(e.NewSelectedIndex).Value.ToString
        ' Response.Redirect("pelajar.view.aspx?PelajarID=" & strKeyID)

    End Sub

    Protected Sub datRespondent_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles datRespondent.RowCommand
        lblmsg.Text = ""
        If (e.CommandName = "Batal") = True Then

            Dim PemeriksaID = Int32.Parse(e.CommandArgument.ToString())

            strSQL = "DELETE FROM kpmkv_pemeriksa WHERE PemeriksaID='" & PemeriksaID & "'"
            strRet = oCommon.ExecuteSQL(strSQL)
            If strRet = "0" Then
                divMsg.Attributes("class") = "error"
                lblmsg.Text = "Pemeriksa berjaya dipadamkan"
            Else
                divMsg.Attributes("class") = "error"
                lblmsg.Text = "Pemeriksa tidak berjaya dipadamkan"
            End If

        End If

        strRet = BindData(datRespondent)

    End Sub
    Private Function GetData(ByVal cmd As SqlCommand) As DataTable
        Dim dt As New DataTable()
        Dim strConnString As [String] = ConfigurationManager.AppSettings("ConnectionString")
        Dim con As New SqlConnection(strConnString)
        Dim sda As New SqlDataAdapter()
        cmd.CommandType = CommandType.Text
        cmd.Connection = con
        Try
            con.Open()
            sda.SelectCommand = cmd
            sda.Fill(dt)
            Return dt
        Catch ex As Exception
            Throw ex
        Finally
            con.Close()
            sda.Dispose()
            con.Dispose()
        End Try
    End Function

    Private Sub btnCari_Click(sender As Object, e As EventArgs) Handles btnCari.Click
        lblmsg.Text = ""
        strRet = BindData(datRespondent)

    End Sub

    Private Sub ddlKodPusat_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlKodPusat.SelectedIndexChanged
        strSQL = "SELECT Nama FROM kpmkv_kolej WHERE RecordID='" & ddlKodPusat.Text & "'"
        lblNamaKolej.Text = oCommon.getFieldValue(strSQL)
    End Sub

    Private Sub btnFile_Click(sender As Object, e As EventArgs) Handles btnFile.Click
        lblmsg.Text = ""
        Response.ContentType = "Application/xlsx"
        Response.AppendHeader("Content-Disposition", "attachment; filename=IMPORT_PEMERIKSA.xlsx")
        Response.TransmitFile(Server.MapPath("~/sample_data/IMPORT_PEMERIKSA.xlsx"))
        Response.End()
    End Sub

    Private Sub btnUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpload.Click
        lblmsg.Text = ""
        Try
            '--upload excel
            If ImportExcel1() = True Then
                divMsg.Attributes("class") = "info"
            Else
            End If

            If ImportExcel2() = True Then
                divMsg.Attributes("class") = "info"
            Else
            End If

        Catch ex As Exception
            lblmsg.Text = "System Error:" & ex.Message

        End Try

    End Sub

    Private Function ImportExcel1() As Boolean
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
                Dim validationMessage As String = ValidateSiteData1(ds)
                If validationMessage = "" Then
                    SaveSiteData1(ds)

                Else
                    'lblMsgTop.Text = "Muatnaik GAGAL!. Lihat mesej dibawah."
                    divMsg.Attributes("class") = "error"
                    lblmsg.Text = "Kesalahan Kemasukkan Maklumat Pengguna:<br />" & validationMessage
                    Return False
                End If

                da.Dispose()
                connection.Close()
                command.Dispose()

            Catch ex As Exception
                lblmsg.Text = "System Error:" & ex.Message
                Return False
            Finally
                If connection.State = ConnectionState.Open Then
                    connection.Close()
                End If
            End Try

        Else
            divMsg.Attributes("class") = "error"
            lblmsg.Text = "Please select file to upload!"
            Return False
        End If

        Return True

    End Function

    Private Sub refreshVar1()

        No = ""
        NamaKolej = ""
        LoginID = ""
        Password = ""
        Nama = ""
        MYKAD = ""
        Negeri = ""

    End Sub

    Protected Function ValidateSiteData1(ByVal SiteData As DataSet) As String
        Try
            'Loop through DataSet and validate data
            'If data is bad, bail out, otherwise continue on with the bulk copy

            Dim strMsg As String = ""
            Dim sb As StringBuilder = New StringBuilder()
            For i As Integer = 0 To SiteData.Tables(0).Rows.Count - SiteData.Tables(0).Rows(i).Item("No")

                refreshVar1()
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

    Private Function SaveSiteData1(ByVal SiteData As DataSet) As String
        lblmsg.Text = ""

        'Dim str As String
        Try

            Dim sb As StringBuilder = New StringBuilder()

            For i As Integer = 0 To SiteData.Tables(0).Rows.Count - SiteData.Tables(0).Rows(i).Item("No")

                No = SiteData.Tables(0).Rows(i).Item("No")
                NamaKolej = SiteData.Tables(0).Rows(i).Item("Nama Kolej")
                LoginID = SiteData.Tables(0).Rows(i).Item("ID Pengguna")
                Password = SiteData.Tables(0).Rows(i).Item("Kata Laluan Pengguna")
                Nama = UCase(SiteData.Tables(0).Rows(i).Item("Nama"))
                MYKAD = SiteData.Tables(0).Rows(i).Item("MYKAD")
                Negeri = SiteData.Tables(0).Rows(i).Item("Negeri")

                strSQL = "SELECT RecordID FROM kpmkv_kolej WHERE Nama = '" & NamaKolej & "'"
                Dim RecordID As String = oCommon.getFieldValue(strSQL)

                strSQL = "SELECT UserID FROM kpmkv_users WHERE Nama = '" & Nama & "' AND UserType = 'PEMERIKSA'"
                Dim UserID As String = oCommon.getFieldValue(strSQL)

                If UserID = "" Then

                    strSQL = "INSERT INTO kpmkv_users (RecordID, LoginID, Pwd, UserType, Nama, Mykad, Negeri)"
                    strSQL += " VALUES ('" & RecordID & "', '" & LoginID & "', '" & Password & "', 'PEMERIKSA', '" & Nama & "', '" & MYKAD & "', '" & Negeri & "')"

                Else

                    strSQL = "  UPDATE kpmkv_users SET 
                                RecordID = '" & RecordID & "',
                                LoginID = '" & LoginID & "',
                                Pwd = '" & Password & "',
                                Nama = '" & Nama & "',
                                Mykad = '" & MYKAD & "',
                                Negeri = '" & Negeri & "'
                                WHERE UserID = '" & UserID & "'"

                End If

                strRet = oCommon.ExecuteSQL(strSQL)

                If strRet = "0" Then

                    divMsg.Attributes("class") = "info"
                    lblmsg.Text = "Pengguna berjaya didaftarkan"

                Else

                    divMsg.Attributes("class") = "error"
                    lblmsg.Text = "Pengguna tidak berjaya didaftarkan"

                End If

            Next

        Catch ex As Exception
            divMsg.Attributes("class") = "error"
            lblmsg.Text = "Pengguna tidak berjaya didaftarkan"
            Return False
        End Try

        Return True

    End Function

    Private Function ImportExcel2() As Boolean
        Dim path As String = String.Concat(Server.MapPath("~/inbox/"))

        If FlUploadcsv.HasFile Then
            Dim rand As Random = New Random()
            Dim randNum = rand.Next(1000)
            Dim fullFileName As String = path + oCommon.getRandom + "-" + FlUploadcsv.FileName
            FlUploadcsv.PostedFile.SaveAs(fullFileName)

            '--required ms access engine
            Dim excelConnectionString As String = ("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & fullFileName & ";Extended Properties=Excel 12.0;")
            Dim connection As OleDbConnection = New OleDbConnection(excelConnectionString)
            Dim command As OleDbCommand = New OleDbCommand("SELECT * FROM [PEMERIKSA$]", connection)
            Dim da As OleDbDataAdapter = New OleDbDataAdapter(command)
            Dim ds As DataSet = New DataSet

            Try
                connection.Open()
                da.Fill(ds)
                Dim validationMessage As String = ValidateSiteData2(ds)
                If validationMessage = "" Then
                    SaveSiteData2(ds)

                Else
                    'lblMsgTop.Text = "Muatnaik GAGAL!. Lihat mesej dibawah."
                    divMsg.Attributes("class") = "error"
                    lblmsg.Text = "Kesalahan Kemasukkan Maklumat Pemeriksa:<br />" & validationMessage
                    Return False
                End If

                da.Dispose()
                connection.Close()
                command.Dispose()

            Catch ex As Exception
                lblmsg.Text = "System Error:" & ex.Message
                Return False
            Finally
                If connection.State = ConnectionState.Open Then
                    connection.Close()
                End If
            End Try

        Else
            divMsg.Attributes("class") = "error"
            lblmsg.Text = "Please select file to upload!"
            Return False
        End If

        Return True

    End Function

    Private Sub refreshVar2()

        No = ""
        NamaPemeriksa = ""
        Tahun = ""
        Semester = ""
        Sesi = ""
        KodKolej = ""
        KodKursus = ""

    End Sub

    Protected Function ValidateSiteData2(ByVal SiteData As DataSet) As String
        Try
            'Loop through DataSet and validate data
            'If data is bad, bail out, otherwise continue on with the bulk copy

            No = ""
            NamaPemeriksa = ""
            Tahun = ""
            Semester = ""
            Sesi = ""
            KodKolej = ""
            KodKursus = ""

            Dim strMsg As String = ""
            Dim sb As StringBuilder = New StringBuilder()
            For i As Integer = 0 To SiteData.Tables(0).Rows.Count - SiteData.Tables(0).Rows(i).Item("No")

                refreshVar2()
                strMsg = ""

                'No
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("No")) Then
                    No = SiteData.Tables(0).Rows(i).Item("No")
                Else
                    strMsg += " Sila isi No|"
                End If

                'Nama Kolej
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("Nama Pemeriksa")) Then

                    NamaPemeriksa = SiteData.Tables(0).Rows(i).Item("Nama Pemeriksa")

                    strSQL = " SELECT UserID FROM kpmkv_users WHERE Nama = '" & NamaPemeriksa & "' AND UserType = 'PEMERIKSA'"
                    Dim UserID As String = oCommon.getFieldValue(strSQL)

                    If UserID = "" Then

                        strMsg += " Sila masukkan Nama Pemeriksa yang betul|"

                    End If

                Else

                    strMsg += " Sila isi Nama Pemeriksa|"

                End If

                'Login ID
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("Tahun")) Then
                    Tahun = SiteData.Tables(0).Rows(i).Item("Tahun")
                Else
                    strMsg += " Sila isi Tahun|"
                End If

                'Password
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("Semester")) Then
                    Semester = SiteData.Tables(0).Rows(i).Item("Semester")
                Else
                    strMsg += " Sila isi Semester|"
                End If

                'UserType
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("Sesi")) Then
                    Sesi = SiteData.Tables(0).Rows(i).Item("Sesi")
                Else
                    strMsg += " Sila isi Sesi|"
                End If

                'Nama
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("Kod Kolej")) Then
                    KodKolej = SiteData.Tables(0).Rows(i).Item("Kod Kolej")
                    strSQL = "SELECT KolejID FROM kpmkv_kolej WHERE Kod = '" & KodKolej & "'"
                    If oCommon.getFieldValue(strSQL) = "" Then
                        strMsg += " Sila isi Kod Kolej yang betul|"
                    End If
                Else
                    strMsg += " Sila isi Kod Kolej|"
                End If

                'MYKAD
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("Kod Kursus")) Then
                    KodKursus = SiteData.Tables(0).Rows(i).Item("Kod Kursus")
                    strSQL = "SELECT KursusID FROM kpmkv_kursus WHERE KodKursus = '" & KodKursus & "' AND Tahun = '" & Tahun & "'"
                    If oCommon.getFieldValue(strSQL) = "" Then
                        strMsg += " Sila isi Kod Kursus yang betul|"
                    End If
                Else
                    strMsg += " Sila isi Kod Kursus|"
                End If

                If Not strMsg.Length = 0 Then
                    strMsg = "No " & No & " :" & strMsg
                    strMsg += "<br/>"
                End If

                sb.Append(strMsg)

            Next
            Return sb.ToString()
        Catch ex As Exception
            Return ex.Message
        End Try

    End Function

    Private Function SaveSiteData2(ByVal SiteData As DataSet) As String
        lblmsg.Text = ""

        'Dim str As String
        Try

            Dim sb As StringBuilder = New StringBuilder()

            No = ""
            NamaPemeriksa = ""
            Tahun = ""
            Semester = ""
            Sesi = ""
            KodKolej = ""
            KodKursus = ""

            For i As Integer = 0 To SiteData.Tables(0).Rows.Count - SiteData.Tables(0).Rows(i).Item("No")

                No = SiteData.Tables(0).Rows(i).Item("No")
                NamaPemeriksa = UCase(SiteData.Tables(0).Rows(i).Item("Nama Pemeriksa"))
                Tahun = SiteData.Tables(0).Rows(i).Item("Tahun")
                Semester = SiteData.Tables(0).Rows(i).Item("Semester")
                Sesi = SiteData.Tables(0).Rows(i).Item("Sesi")
                KodKolej = SiteData.Tables(0).Rows(i).Item("Kod Kolej")
                KodKursus = SiteData.Tables(0).Rows(i).Item("Kod Kursus")

                strSQL = "SELECT UserID FROM kpmkv_users WHERE Nama = '" & NamaPemeriksa & "'"
                Dim UserID As String = oCommon.getFieldValue(strSQL)

                strSQL = "SELECT PemeriksaID FROM kpmkv_pemeriksa WHERE NamaPemeriksa = '" & NamaPemeriksa & "' AND UserID = '" & UserID & "' AND Tahun = '" & Tahun & "' AND  Semester = '" & Semester & "' AND Sesi = '" & Sesi & "' AND KodKolej = '" & KodKolej & "' AND KodKursus = '" & KodKursus & "' AND Jenis = 'BM'"

                If oCommon.getFieldValue(strSQL) = "" Then

                    strSQL = "INSERT INTO kpmkv_pemeriksa (NamaPemeriksa, UserID, Tahun, Semester, Sesi, KodKolej, KodKursus,Jenis) "
                    strSQL += " VALUES ('" & NamaPemeriksa & "', '" & UserID & "', '" & Tahun & "','" & Semester & "','" & Sesi & "','" & KodKolej & "', '" & KodKursus & "', 'BM')"
                    strRet = oCommon.ExecuteSQL(strSQL)

                    If strRet = "0" Then

                        divMsg.Attributes("class") = "info"
                        lblmsg.Text = "Pemeriksa berjaya didaftarkan"

                    Else

                        divMsg.Attributes("class") = "error"
                        lblmsg.Text = "Pemeriksa tidak berjaya didaftarkan"

                    End If

                Else

                    divMsg.Attributes("class") = "info"
                    lblmsg.Text = "Pemeriksa berjaya didaftarkan"

                End If

            Next

        Catch ex As Exception
            divMsg.Attributes("class") = "error"
            lblmsg.Text = "Pemeriksa tidak berjaya didaftarkan"
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
            lblmsg.Text = "Error Message FileIsLocked:" & ex.Message
            blnReturn = True
        End Try

        Return blnReturn
    End Function

End Class