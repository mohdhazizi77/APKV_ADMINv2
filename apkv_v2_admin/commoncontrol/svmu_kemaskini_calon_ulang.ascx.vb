Imports System.Data.SqlClient
Public Class svmu_kemaskini_calon_ulang1
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        If Not IsPostBack Then

            getDetailsBayaran()

            getBangsa()

            getNegeri()

            getNegeriKV()
            getKV()
            getDetails()


        End If

    End Sub

    Private Sub getDetails()

        Dim strPelajarID As String
        Dim strsvmuID As String

        Dim strNama As String
        Dim strMYKAD As String
        Dim strJantina As String
        Dim strBangsa As String
        Dim strAgama As String
        Dim strAngkaGiliran As String
        Dim strKolejRecordID As String
        Dim strNamaKolej As String
        Dim strTahun As String
        Dim strGredBM As String
        Dim strGredSJ As String

        strPelajarID = AsciiSwitchWithMod(Request.QueryString("ID"), -19, -7)
        strsvmuID = AsciiSwitchWithMod(Request.QueryString("NO"), -19, -7)

        strSQL = "SELECT kpmkv_pelajar.Nama, kpmkv_pelajar.MYKAD, kpmkv_pelajar.Jantina, kpmkv_pelajar.Bangsa, kpmkv_pelajar.Agama, kpmkv_pelajar.AngkaGiliran, kpmkv_pelajar.KolejRecordID, kpmkv_kolej.Nama, kpmkv_pelajar.Tahun, kpmkv_pelajar_markah.GredBMSetara, kpmkv_pelajar_markah.GredSJSetara FROM kpmkv_pelajar 
LEFT JOIN kpmkv_kolej ON kpmkv_pelajar.KolejRecordID = kpmkv_kolej.RecordID
LEFT JOIN kpmkv_pelajar_markah ON kpmkv_pelajar.PelajarID = kpmkv_pelajar_markah.pelajarID 
WHERE kpmkv_pelajar.PelajarID = '" & strPelajarID & "'
AND kpmkv_pelajar.Semester = '4'"
        strRet = oCommon.getFieldValueEx(strSQL)

        ''--get user info
        Dim ar_Calon As Array
        ar_Calon = strRet.Split("|")

        strNama = ar_Calon(0)
        strMYKAD = ar_Calon(1)
        strJantina = ar_Calon(2)
        strBangsa = ar_Calon(3)
        strAgama = ar_Calon(4)
        strAngkaGiliran = ar_Calon(5)
        strKolejRecordID = ar_Calon(6)
        strNamaKolej = ar_Calon(7)
        strTahun = ar_Calon(8)
        strGredBM = ar_Calon(9)
        strGredSJ = ar_Calon(10)

        txtNama.Text = strNama
        txtMYKAD.Text = strMYKAD
        txtJantina.Text = strJantina
        txtAgama.Text = strAgama
        txtAngkaGiliran.Text = strAngkaGiliran
        ''txtNama.Text = strKolejRecordID
        txtKolej.Text = strNamaKolej
        txtKohort.Text = strTahun

        strSQL = "SELECT setting_value_int FROM kpmkv_svmu_setting WHERE setting_parameter = 'TAHUN_PEPERIKSAAN'"
        Dim TahunPeperiksaan As String = oCommon.getFieldValue(strSQL)

        strSQL = "SELECT svmu_calon_id FROM kpmkv_svmu_calon WHERE svmu_id = '" & AsciiSwitchWithMod(Request.QueryString("NO"), -19, -7) & "' AND TahunPeperiksaan = '" & TahunPeperiksaan & "' AND MataPelajaran = 'BM'"
        strRet = oCommon.getFieldValue(strSQL)

        If Not strRet = "" Then

            strSQL = "SELECT Bangsa, Alamat, Poskod, Bandar, Negeri, Telefon, Email, PusatPeperiksaanID
FROM kpmkv_svmu_calon WHERE svmu_calon_id = '" & strRet & "'"
            strRet = oCommon.getFieldValueEx(strSQL)

            Dim ar_maklumat As Array
            ar_maklumat = strRet.Split("|")

            ddlBangsa.SelectedValue = ar_maklumat(0)
            txtAlamat.Text = ar_maklumat(1)
            txtPoskod.Text = ar_maklumat(2)
            txtBandar.Text = ar_maklumat(3)
            ddlNegeri.SelectedValue = ar_maklumat(4)
            txtTelefon.Text = ar_maklumat(5)
            txtEmail.Text = ar_maklumat(6)
            ddlNegeriKV.Text = ar_maklumat(7)
            strSQL = "SELECT Negeri FROM kpmkv_kolej WHERE RecordID = '" & ar_maklumat(7) & "'"
            ddlNegeriKV.Text = oCommon.getFieldValue(strSQL)
            strSQL = "SELECT Nama FROM kpmkv_kolej WHERE RecordID = '" & ar_maklumat(7) & "'"
            ddlKolej.SelectedItem.Text = oCommon.getFieldValue(strSQL)

            ddlBangsa.Enabled = False
            txtAlamat.Enabled = False
            txtPoskod.Enabled = False
            txtBandar.Enabled = False
            ddlNegeri.Enabled = False
            txtTelefon.Enabled = False
            txtEmail.Enabled = False
            ddlNegeriKV.Enabled = False
            ddlNegeriKV.Enabled = False
            ddlKolej.Enabled = False

        End If

        strSQL = "SELECT svmu_calon_id FROM kpmkv_svmu_calon WHERE svmu_id = '" & AsciiSwitchWithMod(Request.QueryString("NO"), -19, -7) & "' AND TahunPeperiksaan = '" & TahunPeperiksaan & "' AND MataPelajaran = 'SJ'"
        strRet = oCommon.getFieldValue(strSQL)

        If Not strRet = "" Then

            strSQL = "SELECT Bangsa, Alamat, Poskod, Bandar, Negeri, Telefon, Email, PusatPeperiksaanID
FROM kpmkv_svmu_calon WHERE svmu_calon_id = '" & strRet & "'"
            strRet = oCommon.getFieldValueEx(strSQL)

            Dim ar_maklumat As Array
            ar_maklumat = strRet.Split("|")

            ddlBangsa.SelectedValue = ar_maklumat(0)
            txtAlamat.Text = ar_maklumat(1)
            txtPoskod.Text = ar_maklumat(2)
            txtBandar.Text = ar_maklumat(3)
            ddlNegeri.SelectedValue = ar_maklumat(4)
            txtTelefon.Text = ar_maklumat(5)
            txtEmail.Text = ar_maklumat(6)
            ddlNegeriKV.Text = ar_maklumat(7)
            strSQL = "SELECT Negeri FROM kpmkv_kolej WHERE RecordID = '" & ar_maklumat(7) & "'"
            ddlNegeriKV.Text = oCommon.getFieldValue(strSQL)
            strSQL = "SELECT Nama FROM kpmkv_kolej WHERE RecordID = '" & ar_maklumat(7) & "'"
            ddlKolej.SelectedItem.Text = oCommon.getFieldValue(strSQL)

            ddlBangsa.Enabled = False
            txtAlamat.Enabled = False
            txtPoskod.Enabled = False
            txtBandar.Enabled = False
            ddlNegeri.Enabled = False
            txtTelefon.Enabled = False
            txtEmail.Enabled = False
            ddlNegeriKV.Enabled = False
            ddlNegeriKV.Enabled = False
            ddlKolej.Enabled = False

        End If

        lblGredBM.Text = strGredBM
        lblGredSJ.Text = strGredSJ

        ''get DOB calon
        Dim strYear As String
        Dim strMonth As String
        Dim strDay As String
        Dim strDOB As String

        If txtMYKAD.Text.Substring(0, 1) < Now.Year.ToString.Substring(2, 2) Then
            strYear = 20 & txtMYKAD.Text.Substring(0, 2)
        Else
            strYear = 19 & txtMYKAD.Text.Substring(0, 2)
        End If

        strMonth = txtMYKAD.Text.Substring(2, 2)
        strDay = txtMYKAD.Text.Substring(4, 2)

        strDOB = strDay & "-" & strMonth & "-" & strYear
        txtDate.Text = strDOB

        strSQL = "SELECT svmu_calon_id FROM kpmkv_svmu_calon WHERE svmu_id = '" & strsvmuID & "' AND MataPelajaran = 'BM'"

        If oCommon.getFieldValue(strSQL) = "" Then

            chkBM.Enabled = True

        Else

            chkBM.Enabled = False

        End If

        strSQL = "SELECT svmu_calon_id FROM kpmkv_svmu_calon WHERE svmu_id = '" & strsvmuID & "' AND MataPelajaran = 'SJ'"

        If oCommon.getFieldValue(strSQL) = "" Then

            chkSJ.Enabled = True

        Else

            chkSJ.Enabled = False

        End If

    End Sub

    Private Sub getDetailsBayaran()

        strSQL = "SELECT setting_value_double FROM kpmkv_svmu_setting WHERE setting_parameter = 'BAYARAN' AND setting_value_string = 'ASAS'"
        lblRMAsas.Text = oCommon.getFieldValue(strSQL)

        strSQL = "SELECT setting_value_double FROM kpmkv_svmu_setting WHERE setting_parameter = 'BAYARAN' AND setting_value_string = 'BM'"
        lblRMBM.Text = oCommon.getFieldValue(strSQL)

        strSQL = "SELECT setting_value_double FROM kpmkv_svmu_setting WHERE setting_parameter = 'BAYARAN' AND setting_value_string = 'SJ'"
        lblRMSJ.Text = oCommon.getFieldValue(strSQL)

    End Sub

    Private Sub getBangsa()

        strSQL = "SELECT setting_value_string FROM kpmkv_svmu_setting WHERE setting_parameter = 'BANGSA' ORDER BY setting_index"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlBangsa.DataSource = ds
            ddlBangsa.DataTextField = "setting_value_string"
            ddlBangsa.DataValueField = "setting_value_string"
            ddlBangsa.DataBind()

            '--ALL
            ddlBangsa.Items.Add(New ListItem("-Pilih-", "0"))
            ddlBangsa.Text = "0"

        Catch ex As Exception

        End Try

    End Sub

    Private Sub getNegeri()

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
            ddlNegeri.Text = "0"

        Catch ex As Exception

        End Try

    End Sub

    Private Sub getNegeriKV()

        strSQL = "SELECT Negeri FROM kpmkv_negeri  ORDER BY Negeri"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlNegeriKV.DataSource = ds
            ddlNegeriKV.DataTextField = "Negeri"
            ddlNegeriKV.DataValueField = "Negeri"
            ddlNegeriKV.DataBind()

            '--ALL
            ddlNegeriKV.Items.Add(New ListItem("-Pilih-", "0"))
            ddlNegeriKV.Text = "0"

        Catch ex As Exception

        End Try

    End Sub

    Private Sub getKV()

        strSQL = "SELECT Nama,RecordID FROM kpmkv_kolej WHERE Negeri='" & ddlNegeriKV.Text & "' ORDER BY Nama ASC"
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
            ddlKolej.Text = "0"

        Catch ex As Exception
            'lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub

    Function AsciiSwitchWithMod(InputString As String, ValueToAdd As Integer, ModValue As Integer) As String
        Dim OutputString As String = String.Empty
        Dim c As Char
        For i = 0 To Len(InputString) - 1
            c = InputString.Substring(i, 1)
            If i Mod 5 = 0 Then
                OutputString += Chr(Asc(c) + ValueToAdd + ModValue)
            Else
                OutputString += Chr(Asc(c) + ValueToAdd)
            End If
        Next

        Return OutputString
    End Function


    Private Sub btnProceed_Click(sender As Object, e As EventArgs) Handles btnProceed.Click

        If ValidatePage() = False Then
            Exit Sub
        End If



        Dim strsvmuCalonID As String
        Dim strPelajarID As String
        Dim strsvmuID As String

        strPelajarID = AsciiSwitchWithMod(Request.QueryString("ID"), -19, -7)
        strsvmuID = AsciiSwitchWithMod(Request.QueryString("NO"), -19, -7)

        strSQL = "SELECT setting_value_int FROM kpmkv_svmu_setting WHERE setting_parameter = 'TAHUN_PEPERIKSAAN'"
        Dim TahunPeperiksaan As String = oCommon.getFieldValue(strSQL)

        strSQL = "SELECT svmu_calon_id FROM kpmkv_svmu_calon WHERE svmu_id = '" & strsvmuID & "' AND TahunPeperiksaan = '" & TahunPeperiksaan & "'"
        strsvmuCalonID = oCommon.getFieldValue(strSQL)

        If chkBM.Checked = True Then

            strSQL = "  INSERT INTO kpmkv_svmu_calon
                        (svmu_id, Nama, TarikhLahir, Jantina, Bangsa, Agama, KolejID, KolejNama, Kohort, Alamat, Poskod, Bandar, Negeri, Telefon, Email, MataPelajaran, StatusMP, PusatPeperiksaanID, TahunPeperiksaan, Status, create_timestamp)
                        VALUES
                        ('" & strsvmuID & "', '" & txtNama.Text & "', '" & txtDate.Text & "', '" & txtJantina.Text & "', '" & ddlBangsa.Text & "', '" & txtAgama.Text & "', '" & txtKolej.Text & "', '" & txtKolej.Text & "', '" & txtKohort.Text & "', '" & txtAlamat.Text & "', '" & txtPoskod.Text & "', '" & txtBandar.Text & "', '" & ddlNegeri.Text & "', '" & txtTelefon.Text & "', '" & txtEmail.Text & "', 'BM', '0', '" & ddlKolej.SelectedValue & "', '" & TahunPeperiksaan & "', 'BARU', CURRENT_TIMESTAMP)"
            strRet = oCommon.ExecuteSQL(strSQL)

        End If

        If chkSJ.Checked = True Then

            strSQL = "  INSERT INTO kpmkv_svmu_calon
                        (svmu_id, Nama, TarikhLahir, Jantina, Bangsa, Agama, KolejID, KolejNama, Kohort, Alamat, Poskod, Bandar, Negeri, Telefon, Email, MataPelajaran, StatusMP, PusatPeperiksaanID, TahunPeperiksaan, Status, create_timestamp)
                        VALUES
                        ('" & strsvmuID & "', '" & txtNama.Text & "', '" & txtDate.Text & "', '" & txtJantina.Text & "', '" & ddlBangsa.Text & "', '" & txtAgama.Text & "', '" & txtKolej.Text & "', '" & txtKolej.Text & "', '" & txtKohort.Text & "', '" & txtAlamat.Text & "', '" & txtPoskod.Text & "', '" & txtBandar.Text & "', '" & ddlNegeri.Text & "', '" & txtTelefon.Text & "', '" & txtEmail.Text & "', 'SJ', '0', '" & ddlKolej.SelectedValue & "', '" & TahunPeperiksaan & "', 'BARU', CURRENT_TIMESTAMP)"
            strRet = oCommon.ExecuteSQL(strSQL)

        End If

        'If strsvmuCalonID = "" Then

        '    strSQL = "  INSERT INTO kpmkv_svmu_calon
        '                (svmu_id, Nama, TarikhLahir, Jantina, Bangsa, Agama, KolejID, KolejNama, Kohort, Alamat, Poskod, Bandar, Negeri, Telefon, Email, PusatPeperiksaanID, TahunPeperiksaan, Status, create_timestamp)
        '                VALUES
        '                ('" & strsvmuID & "', '" & txtNama.Text & "', '" & txtDate.Text & "', '" & txtJantina.Text & "', '" & ddlBangsa.Text & "', '" & txtAgama.Text & "', '" & txtKolej.Text & "', '" & txtKolej.Text & "', '" & txtKohort.Text & "', '" & txtAlamat.Text & "', '" & txtPoskod.Text & "', '" & txtBandar.Text & "', '" & ddlNegeri.Text & "', '" & txtTelefon.Text & "', '" & txtEmail.Text & "', '" & ddlKolej.SelectedValue & "', '" & TahunPeperiksaan & "', 'BARU', CURRENT_TIMESTAMP)"
        '    strRet = oCommon.ExecuteSQL(strSQL)

        '    If chkBM.Checked = True Then

        '        strSQL = "  INSERT INTO kpmkv_svmu_matapelajaran
        '                    (svmu_id, mp_jenis, create_timestamp)
        '                    VALUES
        '                    ('" & strsvmuID & "', 'BM', CURRENT_TIMESTAMP)"
        '        strRet = oCommon.ExecuteSQL(strSQL)


        '    End If

        '    If chkSJ.Checked = True Then

        '        strSQL = "  INSERT INTO kpmkv_svmu_matapelajaran
        '                    (svmu_id, mp_jenis, create_timestamp)
        '                    VALUES
        '                    ('" & strsvmuID & "', 'SJ', CURRENT_TIMESTAMP)"
        '        strRet = oCommon.ExecuteSQL(strSQL)


        '    End If

        '    Response.Redirect("svmu_rumusan_pendaftaran.aspx?ID=" & Request.QueryString("ID") & "&NO=" & AsciiSwitchWithMod(strsvmuID, 19, 7))

        'Else

        '    strSQL = "  UPDATE kpmkv_svmu_calon
        '                SET
        '                Nama = '" & txtNama.Text & "',
        '                TarikhLahir = '" & txtDate.Text & "',
        '                Jantina= '" & txtJantina.Text & "',
        '                Bangsa = '" & ddlBangsa.Text & "',
        '                Agama = '" & txtAgama.Text & "',
        '                KolejID = '" & txtKolej.Text & "',
        '                KolejNama = '" & txtKolej.Text & "',
        '                Kohort = '" & txtKohort.Text & "',
        '                Alamat = '" & txtAlamat.Text & "',
        '                Poskod = '" & txtPoskod.Text & "',
        '                Bandar = '" & txtBandar.Text & "', 
        '                Negeri = '" & ddlNegeri.Text & "',
        '                Telefon =  '" & txtTelefon.Text & "',
        '                Email = '" & txtEmail.Text & "',
        '                PusatPeperiksaanID = '" & ddlKolej.SelectedValue & "',
        '                TahunPeperiksaan = '" & TahunPeperiksaan & "'
        '                WHERE
        '                svmu_calon_id = '" & strsvmuCalonID & "'"
        '    strRet = oCommon.ExecuteSQL(strSQL)

        '    ''DELETE MATAPELAJARAN
        '    strSQL = "DELETE FROM kpmkv_svmu_matapelajaran WHERE svmu_id = '" & strsvmuID & "'"
        '    strRet = oCommon.ExecuteSQL(strSQL)

        '    If chkBM.Checked = True Then

        '        strSQL = "  INSERT INTO kpmkv_svmu_matapelajaran
        '                        (svmu_id, mp_jenis, create_timestamp)
        '                        VALUES
        '                        ('" & strsvmuID & "', 'BM', CURRENT_TIMESTAMP)"
        '        strRet = oCommon.ExecuteSQL(strSQL)

        '    End If

        '    If chkSJ.Checked = True Then

        '        strSQL = "  INSERT INTO kpmkv_svmu_matapelajaran
        '                        (svmu_id, mp_jenis, create_timestamp)
        '                        VALUES
        '                        ('" & strsvmuID & "', 'SJ', CURRENT_TIMESTAMP)"
        '        strRet = oCommon.ExecuteSQL(strSQL)

        '    End If

        Response.Redirect("svmu_rumusan_pendaftaran.aspx?ID=" & Request.QueryString("ID") & "&NO=" & AsciiSwitchWithMod(strsvmuID, 19, 7))

        'End If

    End Sub

    Private Function ValidatePage() As Boolean

        lblErrAlamat.Text = ""
        lblErrBandar.Text = ""
        lblErrEmail.Text = ""
        lblErrNegeri.Text = ""
        lblErrPoskod.Text = ""
        lblErrTelefon.Text = ""
        lblErrBangsa.Text = ""
        lblErrMataPelajaran.Text = ""
        lblErrNegeriKV.Text = ""
        lblErrKolej.Text = ""

        Dim errorCode As String = "FALSE"

        If ddlBangsa.SelectedItem.Text = "-Pilih-" Then
            lblErrBangsa.Text = "Sila pilih Bangsa!"
            errorCode = "TRUE"
        End If

        '--txtAlamat
        If txtAlamat.Text.Length = 0 Then
            lblErrAlamat.Text = "Sila masukkan Alamat!"
            errorCode = "TRUE"
        End If

        If txtPoskod.Text.Length = 0 Then

            lblErrPoskod.Text = "Sila masukkan Poskod!"
            errorCode = "TRUE"

        Else

            If IsNumeric(txtPoskod.Text) = False Then
                lblErrPoskod.Text = "Sila masukkan Poskod!"
                errorCode = "TRUE"
            End If

            If txtPoskod.Text.Length < 5 Or txtPoskod.Text.Length > 5 Then
                lblErrPoskod.Text = "Sila masukkan Poskod!"
                errorCode = "TRUE"
            End If

        End If

        If txtBandar.Text.Length = 0 Then
            lblErrBandar.Text = "Sila masukkan Bandar!"
            errorCode = "TRUE"
        End If

        If ddlNegeri.SelectedItem.Text = "-Pilih-" Then
            lblErrNegeri.Text = "Sila pilih Negeri!"
            errorCode = "TRUE"
        End If

        If txtTelefon.Text.Length = 0 Then

            lblErrTelefon.Text = "Sila masukkan No. Telefon!"
            errorCode = "TRUE"

        Else

            If IsNumeric(txtTelefon.Text) = False Then
                lblErrTelefon.Text = "Sila masukkan No. Telefon!"
                errorCode = "TRUE"
            End If

        End If

        If txtEmail.Text.Length = 0 Then
            lblErrEmail.Text = "Sila masukkan Alamat E-mel!"
            errorCode = "TRUE"
        End If

        Dim regex As Regex = New Regex("^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$")
        Dim isValid As Boolean = regex.IsMatch(txtEmail.Text.Trim)
        If Not isValid Then
            lblErrEmail.Text = "Sila masukkan Alamat E-mel!"
            errorCode = "TRUE"
        End If

        If chkBM.Checked = False And chkSJ.Checked = False Then
            lblErrMataPelajaran.Text = "Sila tanda salah satu atau kedua-dua mata pelajaran untuk mengulang"
            errorCode = "TRUE"
        End If

        If ddlNegeriKV.SelectedItem.Text = "-Pilih-" Then
            lblErrNegeriKV.Text = "Sila pilih Negeri!"
            errorCode = "TRUE"
        End If

        If ddlKolej.SelectedItem.Text = "-Pilih-" Then
            lblErrKolej.Text = "Sila pilih Pusat Peperiksaan!"
            errorCode = "TRUE"
        End If

        If errorCode = "TRUE" Then
            Return False
        Else
            Return True
        End If

    End Function

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click

        Response.Redirect("svmu_daftar_calon_ulang.aspx")

    End Sub

    Private Sub ddlNegeriKV_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlNegeriKV.SelectedIndexChanged
        getKV()
        ddlKolej.Text = "0"
        ddlKolej.Focus()
    End Sub
End Class