Imports System.Data.SqlClient
Imports System.Data.OleDb

Public Class pemeriksa_borang_markah_PA_2
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

                lblmsg.Text = ""
                divMsgTop.Visible = False

                kpmkv_kodpusat_list()
                kpmkv_tahun_list()
                ddlTahun.Text = Now.Year
                kpmkv_semester_list()
                kpmkv_kodkursus_list()
                kpmkv_pemeriksa_list()

                listItem()

            End If
        Catch ex As Exception

            divMsgTop.Visible = True
            divMsgTop.Attributes("class") = "error"
            lblMsgTop.Text = "System Error:" & ex.Message
        End Try

    End Sub

    '-----------DropDownlist------------'

    Private Sub listItem()
        Dim item As ListItem
        For Each item In chkSesi.Items
            If item.Value = "" Then
                item.Attributes.Add("style", "display:none")
            End If
        Next
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
            divMsgTop.Visible = True
            divMsgTop.Attributes("class") = "error"
            lblMsgTop.Text = "System Error:" & ex.Message & "<br>"
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
    Private Sub kpmkv_semester_list()
        strSQL = "SELECT Semester FROM kpmkv_semester  ORDER BY SemesterID"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlSemester.DataSource = ds
            ddlSemester.DataTextField = "Semester"
            ddlSemester.DataValueField = "Semester"
            ddlSemester.DataBind()

            '--ALL
            ddlSemester.Items.Add(New ListItem("PILIH", ""))

        Catch ex As Exception
            divMsgTop.Visible = True
            divMsgTop.Attributes("class") = "error"
            lblMsgTop.Text = "System Error:" & ex.Message & "<br>"

        Finally
            objConn.Dispose()
        End Try

    End Sub
    Private Sub kpmkv_kodkursus_list()
        strSQL = "SELECT Distinct kpmkv_kursus.KodKursus, kpmkv_kursus.KursusID"
        strSQL += " FROM kpmkv_kursus INNER JOIN kpmkv_kursus_kolej ON kpmkv_kursus_kolej.KursusID=kpmkv_kursus.KursusID"
        strSQL += " WHERE 
                    kpmkv_kursus.Tahun='" & ddlTahun.Text & "' 
                    AND kpmkv_kursus.Sesi='" & chkSesi.SelectedValue & "' "

        If Not ddlKodPusat.SelectedValue = "-PILIH-" Then
            strSQL += " AND kpmkv_kursus_kolej.KolejRecordID='" & ddlKodPusat.SelectedValue & "' "
        End If

        strSQL += " ORDER BY kpmkv_kursus.KodKursus ASC"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlKodKursus.DataSource = ds
            ddlKodKursus.DataTextField = "KodKursus"
            ddlKodKursus.DataValueField = "KursusID"
            ddlKodKursus.DataBind()
            ddlKodKursus.Items.Insert(0, "-PILIH-")

        Catch ex As Exception
            divMsgTop.Visible = True
            divMsgTop.Attributes("class") = "error"
            lblMsgTop.Text = "System Error:" & ex.Message & "<br>"

        Finally
            objConn.Dispose()
        End Try

    End Sub
    Private Sub kpmkv_pemeriksa_list()
        strSQL = "SELECT Nama FROM kpmkv_users WHERE UserType='VOKASIONAL-PEMERIKSA' ORDER BY Nama"
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

    Private Sub ddlKodPusat_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlKodPusat.SelectedIndexChanged
        strSQL = "SELECT Nama FROM kpmkv_kolej WHERE RecordID='" & ddlKodPusat.Text & "'"
        lblNamaKolej.Text = oCommon.getFieldValue(strSQL)
        chkSesi.SelectedValue = ""
        listItem()
        ddlKodKursus.SelectedValue = "-PILIH-"
    End Sub
    Private Sub chkSesi_SelectedIndexChanged(sender As Object, e As EventArgs) Handles chkSesi.SelectedIndexChanged
        kpmkv_kodkursus_list()

        listItem()
    End Sub


    '-------Senarai Pemeriksa--------'

    Private Function getSQL() As String


        Dim tmpSQL As String = ""
        Dim strWhere As String = ""
        Dim strOrder As String = " ORDER BY kpmkv_pemeriksa.PemeriksaID, kpmkv_pemeriksa.Tahun, kpmkv_pemeriksa.Sesi ASC"

        '--not deleted
        tmpSQL = "SELECT kpmkv_pemeriksa.PemeriksaID, kpmkv_pemeriksa.Tahun, kpmkv_pemeriksa.Semester, kpmkv_pemeriksa.Sesi, kpmkv_pemeriksa.NamaPemeriksa,"
        tmpSQL += " kpmkv_pemeriksa.KodKolej, kpmkv_pemeriksa.KodKursus FROM kpmkv_pemeriksa"
        strWhere = " WHERE kpmkv_pemeriksa.PemeriksaID IS NOT NULL AND Jenis='VOK'"

        '--tahun
        If Not ddlTahun.Text = "PILIH" Then
            strWhere += " AND kpmkv_pemeriksa.Tahun ='" & ddlTahun.Text & "'"
        End If
        '--sesi
        If Not chkSesi.Text = "" Then
            strWhere += " AND kpmkv_pemeriksa.Sesi ='" & chkSesi.Text & "'"
        End If
        If Not ddlKodKursus.SelectedValue = "-PILIH-" Then

            strSQL = "SELECT KodKursus FROM kpmkv_kursus WHERE KursusID='" & ddlKodKursus.SelectedValue & "'"
            Dim strKodKursus As String = oCommon.getFieldValue(strSQL)

            strWhere += " AND kpmkv_pemeriksa.KodKursus='" & strKodKursus & " '"
        End If
        If Not ddlKodPusat.SelectedValue = "-PILIH-" Then
            strSQL = "SELECT Kod FROM kpmkv_kolej WHERE RecordID='" & ddlKodPusat.SelectedValue & "'"
            Dim strPusat As String = oCommon.getFieldValue(strSQL)

            strWhere += " AND kpmkv_pemeriksa.KodKolej='" & strPusat & " '"

        End If

        getSQL = tmpSQL & strWhere & strOrder


        Return getSQL

    End Function
    Private Function BindData(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 120

        Try
            myDataAdapter.Fill(myDataSet, "myaccount")

            If myDataSet.Tables(0).Rows.Count = 0 Then

                divMsg.Attributes("class") = "error"
                lblmsg.Text = "Jumlah Rekod Pemeriksa yang didaftarkan#: Tiada rekod dijumpai! <br>"

            Else

                divMsg.Attributes("class") = "info"
                lblmsg.Text += "Jumlah Rekod Pemeriksa yang didaftarkan#:" & myDataSet.Tables(0).Rows.Count & "</br>"
            End If

            gvTable.DataSource = myDataSet
            gvTable.DataBind()
            objConn.Close()
        Catch ex As Exception
            divMsgTop.Visible = True
            divMsgTop.Attributes("class") = "error"
            lblMsgTop.Text = "System Error:" & ex.Message & "<br>"
            Return False
        End Try

        Return True

    End Function


    '------Senarai Pusat-------------'
    Private Sub Pusat()

        Dim tmpSQL As String = ""
        Dim strWhere As String = ""
        Dim strOrder As String = " ORDER BY kpmkv_Kolej.Kod,kpmkv_kursus.KodKursus ASC"


        tmpSQL = "SELECT kpmkv_kursus_kolej.TxnKursusKolejID,kpmkv_kursus.KodKursus, "
        tmpSQL += " kpmkv_Kolej.Kod,kpmkv_Kolej.Nama"
        tmpSQL += " FROM kpmkv_kursus_kolej"
        tmpSQL += " LEFT JOIN kpmkv_kursus ON kpmkv_kursus.kursusID=kpmkv_kursus_kolej.KursusID"
        tmpSQL += " LEFT JOIN kpmkv_Kolej ON kpmkv_kolej.RecordID= kpmkv_kursus_kolej.KolejRecordID"
        strWhere = " WHERE kpmkv_kursus_kolej.TxnKursusKolejID Is NOT NULL AND kpmkv_kolej.Kod IS NOT NULL "

        '--tahun
        If Not ddlTahun.Text = "PILIH" Then
            strWhere += " AND kpmkv_kursus.Tahun='" & ddlTahun.SelectedValue & "'"
        End If
        '--sesi
        If Not chkSesi.Text = "" Then
            strWhere += " AND kpmkv_kursus.Sesi='" & chkSesi.SelectedValue & "'"
        End If
        If Not ddlKodKursus.SelectedValue = "-PILIH-" Then
            strWhere += " AND kpmkv_kursus_kolej.KursusID='" & ddlKodKursus.SelectedValue & "'"
        End If

        If Not ddlKodPusat.SelectedValue = "-PILIH-" Then
            strWhere += " AND kpmkv_Kolej.RecordID='" & ddlKodPusat.SelectedValue & "'"
        End If

        Dim getSQLPusat As String
        getSQLPusat = tmpSQL & strWhere & strOrder


        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQLPusat, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 120

        Try
            myDataAdapter.Fill(myDataSet, "myaccount")

            If myDataSet.Tables(0).Rows.Count = 0 Then

                divMsg.Attributes("class") = "error"
                lblmsg.Text = "Jumlah Rekod Kod Pusat#: Tiada rekod dijumpai! <br>"
            Else
                divMsg.Attributes("class") = "info"
                lblmsg.Text += "Jumlah Rekod Kod Pusat #:" & myDataSet.Tables(0).Rows.Count & "<br>"
            End If

            datRespondent2.DataSource = myDataSet
            datRespondent2.DataBind()
            objConn.Close()
        Catch ex As Exception
            divMsgTop.Visible = True
            divMsgTop.Attributes("class") = "error"
            lblMsgTop.Text = "System Error:" & ex.Message & "<br>"

        End Try

    End Sub

    '------Fungsi Carian-------------'

    Private Sub btnCari_Click(sender As Object, e As EventArgs) Handles btnCari.Click
        lblmsg.Text = ""
        lblMsgTop.Text = ""
        divMsgTop.Visible = False

        '--validate--
        If ValidatePage() = False Then
            divMsgTop.Visible = True
            divMsgTop.Attributes("class") = "error"
            Exit Sub
        End If

        listItem()
        strRet = BindData(datRespondent)
        Pusat()

    End Sub

    Private Function ValidatePage() As Boolean


        If chkSesi.Text = "" Then
            lblMsgTop.Text = "Sila Pilih Sesi <br>"
            chkSesi.Focus()
            listItem()

            Return False
        End If



        Return True
    End Function

    '-------Fungsi Button Simpan------------'
    Protected Sub btnSimpan_Click(sender As Object, e As EventArgs) Handles btnSimpan.Click
        lblmsg.Text = ""
        divMsgTop.Visible = False

        '--validate--
        If ValidatePage() = False Then
            divMsgTop.Visible = True
            Exit Sub
        End If

        strSQL = "Select UserID FROM kpmkv_users WHERE Nama='" & oCommon.FixSingleQuotes(ddlPemeriksa.Text) & "'"
        Dim StrUserID As Integer = oCommon.getFieldValue(strSQL)

        strSQL = "SELECT Kod FROM kpmkv_kolej WHERE RecordID='" & ddlKodPusat.SelectedValue & "'"
        Dim strPusat As String = oCommon.getFieldValue(strSQL)

        Try

            strSQL = "SELECT kpmkv_kursus_kolej.TxnKursusKolejID, kpmkv_kursus.KodKursus, "
            strSQL += " kpmkv_Kolej.Kod, kpmkv_Kolej.Nama"
            strSQL += " FROM kpmkv_kursus_kolej"
            strSQL += " LEFT JOIN kpmkv_kursus ON kpmkv_kursus.kursusID=kpmkv_kursus_kolej.KursusID"
            strSQL += " LEFT JOIN kpmkv_Kolej ON kpmkv_kolej.RecordID= kpmkv_kursus_kolej.KolejRecordID"
            strSQL += " WHERE kpmkv_kursus_kolej.TxnKursusKolejID IS NOT NULL"
            strSQL += " AND kpmkv_Kolej.Kod IS NOT NULL"
            strSQL += " AND kpmkv_Kolej.Nama IS NOT NULL"

            '--tahun
            If Not ddlTahun.Text = "PILIH" Then
                strSQL += " AND kpmkv_kursus.Tahun='" & ddlTahun.SelectedValue & "'"
            End If
            '--sesi
            If Not chkSesi.Text = "" Then
                strSQL += " AND kpmkv_kursus.Sesi='" & chkSesi.SelectedValue & "'"
            End If
            If Not ddlKodKursus.SelectedValue = "-PILIH-" Then
                strSQL += " AND kpmkv_kursus_kolej.KursusID='" & ddlKodKursus.SelectedValue & "'"
            End If

            If Not ddlKodPusat.SelectedValue = "-PILIH-" Then
                strSQL += " AND kpmkv_Kolej.RecordID='" & ddlKodPusat.SelectedValue & "'"
            End If

            strSQL += " ORDER BY kpmkv_Kolej.Kod ASC"

            Dim sqlDA As New SqlDataAdapter(strSQL, objConn)
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            '--validate checked checkbox--'
            Dim strcount As Integer = 0
            For j As Integer = 0 To ds.Tables(0).Rows.Count - 1

                Dim k As CheckBox = datRespondent2.Rows(j).FindControl("chkSelect")

                If k.Checked Then
                    strcount = strcount + 1
                End If
            Next

            Dim strTotal As Integer = strcount

            If strTotal = 0 Then
                divMsgTop.Visible = True
                lblMsgTop.Text = "Sila Pilih/Klik pada Kod Pusat"
                listItem()
                Exit Sub
            End If
            '----------------------------------INSERT----------------------------------------'
            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1

                Dim cb As CheckBox = datRespondent2.Rows(i).FindControl("chkSelect")

                If cb.Checked = True Then
                    Dim strKodPusat As String = ds.Tables(0).Rows(i).Item(2).ToString
                    Dim strKodkursus As String = ds.Tables(0).Rows(i).Item(1).ToString

                    strSQL = "INSERT INTO kpmkv_pemeriksa(NamaPemeriksa, UserID, Tahun, Semester, Sesi, 
                              KodKolej, KodKursus,Jenis) "
                    strSQL += " VALUES ('" & oCommon.FixSingleQuotes(ddlPemeriksa.Text) & "','" & StrUserID & "',
                              '" & ddlTahun.Text & "','" & ddlSemester.Text & "','" & chkSesi.Text & "','" & strKodPusat & "',
                              '" & strKodkursus & "','VOK')"

                    strRet = oCommon.ExecuteSQL(strSQL)
                End If
            Next

            If strRet = "0" Then
                divMsgTop.Visible = True
                divMsg.Attributes("class") = "info"
                divMsgTop.Attributes("class") = "info"
                lblmsg.Text += "Status Pendaftaran Pemeriksa : Berjaya <br>"
                lblMsgTop.Text += "Status Pendaftaran Pemeriksa : Berjaya <br>"

                listItem()

            Else
                divMsgTop.Visible = True
                divMsg.Attributes("class") = "error"
                lblmsg.Text += "Status Pendaftaran Pemeriksa : Tidak Berjaya! <br>"
                lblMsgTop.Text += "Status Pendaftaran Pemeriksa : Tidak Berjaya! <br>"
            End If

            strRet = BindData(datRespondent)
        Catch ex As Exception
            divMsgTop.Visible = True
            divMsgTop.Attributes("class") = "error"
            lblMsgTop.Text = "System Error:" & ex.Message & "<br>"
        End Try

    End Sub

    '----------Fungsi Batal------------'
    Protected Sub datRespondent_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles datRespondent.RowCommand
        lblmsg.Text = ""
        divMsgTop.Visible = False
        If (e.CommandName = "Batal") = True Then

            Dim PemeriksaID = Int32.Parse(e.CommandArgument.ToString())

            strSQL = "DELETE FROM kpmkv_pemeriksa WHERE PemeriksaID='" & PemeriksaID & "'"
            strRet = oCommon.ExecuteSQL(strSQL)
            If strRet = "0" Then
                divMsgTop.Visible = True
                divMsg.Attributes("class") = "Info"
                divMsgTop.Attributes("class") = "Info"
                lblmsg.Text = " Status Pembatalan Rekod Pemeriksa : Berjaya <br>"
                lblMsgTop.Text = " Status Pembatalan Rekod Pemeriksa : Berjaya <br>"
            Else
                divMsgTop.Visible = True
                divMsgTop.Attributes("class") = "error"
                divMsg.Attributes("class") = "error"
                lblmsg.Text = "Status Pembatalan Rekod Pemeriksa : Tidak Berjaya! <br>"
                lblMsgTop.Text = "Status Pembatalan Rekod Pemeriksa : Tidak Berjaya! <br>"
            End If

        End If
        listItem()

        strRet = BindData(datRespondent)

    End Sub

    Private Sub datRespondent_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles datRespondent.PageIndexChanging
        datRespondent.PageIndex = e.NewPageIndex
        strRet = BindData(datRespondent)
    End Sub

    Private Sub btnFile_Click(sender As Object, e As EventArgs) Handles btnFile.Click
        lblmsg.Text = ""
        Response.ContentType = "Application/xlsx"
        Response.AppendHeader("Content-Disposition", "attachment; filename=IMPORT_PEMERIKSAVOKASIONAL.xlsx")
        Response.TransmitFile(Server.MapPath("~/sample_data/IMPORT_PEMERIKSAVOKASIONAL.xlsx"))
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

                strSQL = "SELECT UserID FROM kpmkv_users WHERE Nama = '" & Nama & "' AND UserType = 'VOKASIONAL-PEMERIKSA'"
                Dim UserID As String = oCommon.getFieldValue(strSQL)

                If UserID = "" Then

                    strSQL = "INSERT INTO kpmkv_users (RecordID, LoginID, Pwd, UserType, Nama, Mykad, Negeri)"
                    strSQL += " VALUES ('" & RecordID & "', '" & LoginID & "', '" & Password & "', 'VOKASIONAL-PEMERIKSA', '" & Nama & "', '" & MYKAD & "', '" & Negeri & "')"

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

                    strSQL = " SELECT UserID FROM kpmkv_users WHERE Nama = '" & NamaPemeriksa & "' AND UserType = 'VOKASIONAL-PEMERIKSA'"
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

                strSQL = "SELECT PemeriksaID FROM kpmkv_pemeriksa WHERE NamaPemeriksa = '" & NamaPemeriksa & "' AND UserID = '" & UserID & "' AND Tahun = '" & Tahun & "' AND  Semester = '" & Semester & "' AND Sesi = '" & Sesi & "' AND KodKolej = '" & KodKolej & "' AND KodKursus = '" & KodKursus & "' AND Jenis = 'VOK'"

                If oCommon.getFieldValue(strSQL) = "" Then

                    strSQL = "INSERT INTO kpmkv_pemeriksa (NamaPemeriksa, UserID, Tahun, Semester, Sesi, KodKolej, KodKursus,Jenis) "
                    strSQL += " VALUES ('" & NamaPemeriksa & "', '" & UserID & "', '" & Tahun & "','" & Semester & "','" & Sesi & "','" & KodKolej & "', '" & KodKursus & "', 'VOK')"
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