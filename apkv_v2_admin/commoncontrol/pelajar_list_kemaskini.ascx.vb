Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization

Public Class pelajar_list_kemaskini
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        lblMsg.Text = ""
        lblMsg2.Text = ""
        Try
            If Not IsPostBack Then

                Dim PelajarID As String = Request.QueryString("id")

                strSQL = "SELECT KolejRecordID FROM kpmkv_pelajar WHERE PelajarID = '" & PelajarID & "'"
                Dim KolejRecordID As String = oCommon.getFieldValue(strSQL)

                strSQL = "SELECT Nama FROM kpmkv_kolej WHERE RecordID = '" & KolejRecordID & "'"
                Dim Nama As String = oCommon.getFieldValue(strSQL)

                strSQL = "SELECT Jenis FROM kpmkv_kolej WHERE RecordID = '" & KolejRecordID & "'"
                Dim Jenis As String = oCommon.getFieldValue(strSQL)

                strSQL = "SELECT Negeri FROM kpmkv_kolej WHERE RecordID = '" & KolejRecordID & "'"
                Dim Negeri As String = oCommon.getFieldValue(strSQL)

                kpmkv_negeri_list()
                ddlNegeri.Text = Negeri

                kpmkv_jenis_list()
                ddlJenis.Text = Jenis

                kpmkv_kolej_list()
                ddlKolej.SelectedValue = KolejRecordID

                strSQL = "SELECT Tahun FROM kpmkv_pelajar WHERE PelajarID = '" & PelajarID & "'"
                Dim Tahun As String = oCommon.getFieldValue(strSQL)

                kpmkv_tahun_list()
                ddlTahun.Text = Tahun

                strSQL = "SELECT Semester FROM kpmkv_pelajar WHERE PelajarID = '" & PelajarID & "'"
                Dim Semester As String = oCommon.getFieldValue(strSQL)

                kpmkv_semester_list()
                ddlSemester.Text = Semester

                chkSesi.SelectedIndex = 0

                strSQL = "SELECT Kaum FROM kpmkv_pelajar WHERE PelajarID = '" & PelajarID & "'"
                Dim Kaum As String = oCommon.getFieldValue(strSQL)

                kpmkv_kaum_list()
                ddlKaum.Text = Kaum

                strSQL = "SELECT KursusID FROM kpmkv_pelajar WHERE PelajarID = '" & PelajarID & "'"
                Dim KursusID As String = oCommon.getFieldValue(strSQL)

                strSQL = "SELECT KlusterID FROM kpmkv_kursus WHERE KursusID = '" & KursusID & "'"
                Dim KlusterID As String = oCommon.getFieldValue(strSQL)

                kpmkv_kluster_list()
                ddlKluster.SelectedValue = KlusterID

                kpmkv_kodkursus_list()
                ddlKodKursus.SelectedValue = KursusID

                strSQL = "SELECT KelasID FROM kpmkv_pelajar WHERE PelajarID = '" & PelajarID & "'"
                Dim KelasID As String = oCommon.getFieldValue(strSQL)

                kpmkv_kelas_list()
                ddlNamaKelas.SelectedValue = KelasID

                strSQL = "SELECT Nama FROM kpmkv_pelajar WHERE PelajarID = '" & PelajarID & "'"
                Dim NamaCalon As String = oCommon.getFieldValue(strSQL)

                txtNama.Text = NamaCalon

                strSQL = "SELECT MYKAD FROM kpmkv_pelajar WHERE PelajarID = '" & PelajarID & "'"
                Dim MYKAD As String = oCommon.getFieldValue(strSQL)

                txtMYKAD.Text = MYKAD

                strSQL = "SELECT Jantina FROM kpmkv_pelajar WHERE PelajarID = '" & PelajarID & "'"
                Dim Jantina As String = oCommon.getFieldValue(strSQL)

                If Jantina = "LELAKI" Then

                    chkJantina.SelectedIndex = 0

                Else

                    chkJantina.SelectedIndex = 1

                End If

                strSQL = "SELECT Agama FROM kpmkv_pelajar WHERE PelajarID = '" & PelajarID & "'"
                Dim Agama As String = oCommon.getFieldValue(strSQL)

                Agama = Agama.Trim

                If Agama = "ISLAM" Then

                    chkAgama.SelectedIndex = 0

                Else

                    chkAgama.SelectedIndex = 1

                End If

                strSQL = "SELECT Email FROM kpmkv_pelajar WHERE PelajarID = '" & PelajarID & "'"
                Dim Email As String = oCommon.getFieldValue(strSQL)

                txtEmail.Text = Email

                strSQL = "SELECT Catatan FROM kpmkv_pelajar WHERE PelajarID = '" & PelajarID & "'"
                Dim Catatan As String = oCommon.getFieldValue(strSQL)

                txtCatatan.Text = Catatan

            End If

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message
            lblMsg2.Text = "System Error:" & ex.Message
        End Try
    End Sub
    '-----list----'

    Private Sub kpmkv_negeri_list()
        strSQL = "SELECT Negeri FROM kpmkv_negeri  ORDER BY Negeri"
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
            lblMsg.Text = "System Error:" & ex.Message
            lblMsg2.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub
    Private Sub kpmkv_jenis_list()
        strSQL = "SELECT Jenis FROM kpmkv_jeniskolej  ORDER BY Jenis"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlJenis.DataSource = ds
            ddlJenis.DataTextField = "Jenis"
            ddlJenis.DataValueField = "Jenis"
            ddlJenis.DataBind()

            '--ALL
            ddlJenis.Items.Add(New ListItem("-Pilih-", "0"))

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message
            lblMsg2.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub
    Private Sub kpmkv_kolej_list()
        strSQL = "SELECT Nama,RecordID FROM kpmkv_kolej  ORDER BY Nama ASC"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlKolej.DataSource = ds
            ddlKolej.DataTextField = "Nama"
            ddlKolej.DataValueField = "RecordID"
            ddlKolej.DataBind()




            '--ALL
            ddlKolej.Items.Add(New ListItem("-Pilih-", "0"))

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message
            lblMsg2.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub kpmkv_tahun_list()
        strSQL = "SELECT Tahun FROM kpmkv_tahun  ORDER BY Tahun"
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
            lblMsg.Text = "System Error:" & ex.Message
            lblMsg2.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub
    Private Sub kpmkv_semester_list()
        strSQL = "SELECT Semester FROM kpmkv_semester  WHERE Semester='1'"
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

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message
            lblMsg2.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub
    Private Sub kpmkv_kaum_list()
        strSQL = "SELECT Kaum FROM kpmkv_kaum  ORDER BY Kaum"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlKaum.DataSource = ds
            ddlKaum.DataTextField = "Kaum"
            ddlKaum.DataValueField = "Kaum"
            ddlKaum.DataBind()

            ddlKaum.Items.Add(New ListItem("-Pilih-", "0"))


        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message
            lblMsg2.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub kpmkv_kluster_list()
        strSQL = "SELECT kpmkv_kluster.NamaKluster, kpmkv_kluster.KlusterID FROM  kpmkv_kursus_kolej"
        strSQL += " LEFT OUTER JOIN kpmkv_kursus ON kpmkv_kursus_kolej.KursusID = kpmkv_kursus.KursusID LEFT OUTER JOIN"
        strSQL += " kpmkv_kluster ON kpmkv_kursus.KlusterID = kpmkv_kluster.KlusterID"
        strSQL += " WHERE kpmkv_kursus.Tahun ='" & ddlTahun.SelectedValue & "' AND kpmkv_kursus.Sesi='" & chkSesi.Text & "' GROUP BY kpmkv_kluster.NamaKluster,  kpmkv_kluster.KlusterID"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlKluster.DataSource = ds
            ddlKluster.DataTextField = "NamaKluster"
            ddlKluster.DataValueField = "KlusterID"
            ddlKluster.DataBind()

            '--ALL
            ddlKluster.Items.Add(New ListItem("-Pilih-", "0"))

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message
            lblMsg2.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub
    Private Sub kpmkv_kodkursus_list()

        strSQL = "SELECT kpmkv_kursus.KodKursus, kpmkv_kursus.KursusID FROM kpmkv_kursus_kolej LEFT OUTER JOIN"
        strSQL += " kpmkv_kursus ON kpmkv_kursus_kolej.KursusID = kpmkv_kursus.KursusID"
        strSQL += " WHERE kpmkv_kursus_kolej.KolejRecordID='" & ddlKolej.Text & "' AND kpmkv_kursus.Tahun='" & ddlTahun.SelectedValue & "' AND kpmkv_kursus.Sesi='" & chkSesi.SelectedValue & "' AND KlusterID='" & ddlKluster.SelectedValue & "'"
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

            '--ALL
            ddlKodKursus.Items.Add(New ListItem("-Pilih-", "0"))

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message
            lblMsg2.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub
    Private Sub kpmkv_kelas_list()
        strSQL = " SELECT kpmkv_kelas.NamaKelas, kpmkv_kelas.KelasID FROM kpmkv_kelas_kursus LEFT OUTER JOIN kpmkv_kelas ON kpmkv_kelas_kursus.KelasID = kpmkv_kelas.KelasID LEFT OUTER JOIN"
        strSQL += " kpmkv_kursus ON kpmkv_kelas_kursus.KursusID = kpmkv_kursus.KursusID"
        strSQL += " WHERE kpmkv_kelas.KolejRecordID='" & ddlKolej.Text & "' AND kpmkv_kelas_kursus.KursusID= '" & ddlKodKursus.SelectedValue & "' AND kpmkv_kursus.Tahun= '" & ddlTahun.SelectedValue & "' ORDER BY  kpmkv_kelas.NamaKelas"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)
        'Response.Write(strSQL)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlNamaKelas.DataSource = ds
            ddlNamaKelas.DataTextField = "NamaKelas"
            ddlNamaKelas.DataValueField = "KelasID"
            ddlNamaKelas.DataBind()

            '--ALL
            ddlNamaKelas.Items.Add(New ListItem("-Pilih-", "0"))

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message
            lblMsg2.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub
    Protected Sub btnCreate_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCreate.Click
        lblMsg.Text = ""
        lblMsg2.Text = ""

        Try
            '--validate
            If ValidatePage() = False Then
                divMsg1.Attributes("class") = "error"
                divMsg2.Attributes("class") = "error"
                Exit Sub
            End If

            '--execute
            If kpmkv_pelajar_create() = True Then
                divMsg1.Attributes("class") = "info"
                divMsg1.Attributes("class") = "info"
                lblMsg.Text = "Calon berjaya dikemaskini"
                lblMsg2.Text = "Calon berjaya dikemaskini"
            Else
                divMsg1.Attributes("class") = "error"
                divMsg2.Attributes("class") = "error"
                lblMsg.Text = "Calon tidak berjaya dikemaskini"
                lblMsg2.Text = "Calon tidak berjaya dikemaskini"
            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message
            lblMsg2.Text = ex.Message
        End Try

    End Sub
    Private Function ValidatePage() As Boolean

        '--ddlTahun
        If ddlNegeri.SelectedItem.Text = "-Pilih-" Then
            lblMsg.Text = "Sila pilih Negeri!"
            lblMsg2.Text = "Sila pilih Negeri!"
            ddlNegeri.Focus()
            Return False
        End If

        '--ddlJenisKolej
        If ddlJenis.SelectedItem.Text = "-Pilih-" Then
            lblMsg.Text = "Sila pilih Jenis Kolej!"
            lblMsg2.Text = "Sila pilih Jenis Kolej!"
            ddlJenis.Focus()
            Return False
        End If

        '--ddlNamaKolej
        If ddlKolej.SelectedItem.Text = "-Pilih-" Then
            lblMsg.Text = "Sila pilih Nama Kolej!"
            lblMsg2.Text = "Sila pilih Nama Kolej!"
            ddlKolej.Focus()
            Return False
        End If

        '--ddlsesi
        If chkSesi.Text = "" Then
            lblMsg.Text = "Sila pilih Sesi!"
            lblMsg2.Text = "Sila pilih Sesi!"
            chkSesi.Focus()
            Return False
        End If

        '--ddlKluster
        If ddlKluster.SelectedItem.Text = "-Pilih-" Then
            lblMsg.Text = "Sila Pilih Kluster!"
            lblMsg2.Text = "Sila Pilih Kluster!"
            ddlKluster.Focus()
            Return False
        End If

        '--ddlKodProgram
        If ddlKodKursus.SelectedItem.Text = "-Pilih-" Then
            lblMsg.Text = "Sila Pilih Kod Program!"
            lblMsg2.Text = "Sila Pilih Kod Program!"
            ddlKodKursus.Focus()
            Return False
        End If

        '--ddlNamaKelas
        If ddlNamaKelas.SelectedItem.Text = "-Pilih-" Then
            lblMsg.Text = "Sila Pilih Nama Kelas!"
            lblMsg2.Text = "Sila Pilih Nama Kelas!"
            ddlNamaKelas.Focus()
            Return False
        End If

        '--txtNama
        If txtNama.Text.Length = 0 Then
            lblMsg.Text = "Sila masukkan Nama Calon!"
            lblMsg2.Text = "Sila masukkan Nama Calon!"
            txtNama.Focus()
            Return False
        End If

        '--txtMYKAD
        If txtMYKAD.Text.Length = 0 Then
            lblMsg.Text = "Sila masukkan MYKAD Calon!"
            lblMsg2.Text = "Sila masukkan MYKAD Calon!"
            txtMYKAD.Focus()
            Return False
        ElseIf oCommon.isNumeric(txtMYKAD.Text) = False Then
            lblMsg.Text = "Huruf tidak dibenarkan .Sila masukkan no MYKAD [############]"
            lblMsg2.Text = "Huruf tidak dibenarkan .Sila masukkan no MYKAD [############]"
            txtMYKAD.Focus()
            Return False
        End If

        'strSQL = "SELECT Mykad FROM kpmkv_pelajar Where Mykad='" & txtMYKAD.Text & "'"
        'If oCommon.isExist(strSQL) = True Then
        '    lblMsg.Text = "MYKAD telah digunakan. Pendaftaran Pelajar tidak berjaya"
        '    lblMsg2.Text = "MYKAD telah digunakan. Pendaftaran Pelajar tidak berjaya"
        '    Return False
        'End If

        '--ddlJantina
        If chkJantina.Text = "" Then
            lblMsg.Text = "Sila pilih jenis Jantina!"
            lblMsg2.Text = "Sila pilih jenis Jantina!"
            chkJantina.Focus()
            Return False
        End If

        '--ddlKaum
        If ddlKaum.SelectedItem.Text = "-Pilih-" Then
            lblMsg.Text = "Sila Pilih Kaum!"
            lblMsg2.Text = "Sila Pilih Kaum!"
            ddlNamaKelas.Focus()
            Return False
        End If

        '--ddlAgama
        If chkAgama.Text = "" Then
            lblMsg.Text = "Sila pilih jenis Agama!"
            lblMsg2.Text = "Sila pilih jenis Agama!"
            chkAgama.Focus()
            Return False
        End If

        '--txtEmail
        If txtEmail.Text.Length = 0 Then
            lblMsg.Text = "Sila masukkan Email Calon!"
            lblMsg2.Text = "Sila masukkan Email Calon!"
        ElseIf oCommon.isEmail(txtEmail.Text) = False Then
            lblMsg.Text = "Emel Calon tidak sah. (Contoh: Emel@contoh.com)"
            lblMsg2.Text = "Emel Calon tidak sah. (Contoh: Emel@contoh.com)"
            Return False
        End If

        Return True

    End Function

    Private Function kpmkv_pelajar_create() As Boolean
        Dim strRecordID As String = oCommon.getGUID
        'create with isApproved='Y'

        strSQL = "UPDATE kpmkv_pelajar SET "
        strSQL += "KolejRecordID = '" & ddlKolej.SelectedValue & "',"
        strSQL += "KursusID = '" & ddlKodKursus.SelectedValue & "',"
        strSQL += "KelasID = '" & ddlNamaKelas.SelectedValue & "',"
        strSQL += "Tahun = '" & ddlTahun.Text & "',"
        strSQL += "Semester = '" & ddlSemester.Text & "',"
        strSQL += "Sesi = '" & chkSesi.SelectedValue & "',"
        strSQL += "Nama = '" & txtNama.Text.ToUpper & "',"
        strSQL += "MYKAD = '" & txtMYKAD.Text & "',"
        strSQL += "Jantina = '" & chkJantina.SelectedValue & "',"
        strSQL += "Kaum = '" & ddlKaum.Text & "',"
        strSQL += "Agama = '" & chkAgama.SelectedValue & "',"
        strSQL += "Email = '" & txtEmail.Text & "',"
        strSQL += "Catatan = '" & txtCatatan.Text & "'"
        strSQL += "WHERE PelajarID = '" & Request.QueryString("id") & "'"

        strRet = oCommon.ExecuteSQL(strSQL)

        If strRet = "0" Then

            'strSQL = "SELECT PelajarID FROM kpmkv_pelajar WHERE Mykad='" & txtMYKAD.Text & "'"
            'Dim strPelajarID As Integer = oCommon.getFieldValue(strSQL)
            'strSQL = "INSERT INTO kpmkv_pelajar_markah (PelajarID,KolejRecordID,KursusID,Tahun,Semester,Sesi)"
            'strSQL += " VALUES ('" & strPelajarID & "','" & ddlKolej.SelectedValue & "','" & ddlKodKursus.SelectedValue & "','" & ddlTahun.SelectedValue & "','" & ddlSemester.SelectedValue & "','" & chkSesi.Text & "')"
            'strRet = oCommon.ExecuteSQL(strSQL)

            Return True

        Else
            lblMsg.Text = "System Error:" & strRet
            lblMsg2.Text = "System Error:" & strRet
            Return False
        End If

    End Function

    Protected Sub ddlJenis_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlJenis.SelectedIndexChanged

        If ddlNegeri.SelectedItem.Value <> "ALL" Then
            strSQL = "SELECT Nama,RecordID FROM kpmkv_kolej WHERE Negeri='" & ddlNegeri.SelectedItem.Value & "' AND Jenis='" & ddlJenis.SelectedValue & "'"
        Else
            strSQL = "SELECT Nama,RecordID FROM kpmkv_kolej ORDER BY Nama ASC"
        End If

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlKolej.DataSource = ds
            ddlKolej.DataTextField = "Nama"
            ddlKolej.DataValueField = "RecordID"
            ddlKolej.DataBind()

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message
            lblMsg2.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub chkSesi_SelectedIndexChanged(sender As Object, e As EventArgs) Handles chkSesi.SelectedIndexChanged
        kpmkv_kluster_list()
        kpmkv_kodkursus_list()
        kpmkv_kelas_list()
    End Sub
    Protected Sub ddlKluster_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlKluster.SelectedIndexChanged
        kpmkv_kodkursus_list()
        kpmkv_kelas_list()
    End Sub
    Private Sub ddlKodKursus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlKodKursus.SelectedIndexChanged
        kpmkv_kelas_list()
    End Sub


End Class