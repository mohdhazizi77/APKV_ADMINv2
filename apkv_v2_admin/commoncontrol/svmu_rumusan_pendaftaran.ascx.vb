Imports System.Data.SqlClient
Public Class svmu_rumusan_pendaftaran
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then

            getDetails()
            getMP()
            getPusatPeperiksaan()

        End If

    End Sub

    Private Sub getDetails()

        strSQL = "SELECT setting_value_int FROM kpmkv_svmu_setting WHERE setting_parameter = 'TAHUN_PEPERIKSAAN'"
        Dim TahunPeperiksaan As String = oCommon.getFieldValue(strSQL)

        Dim ar_Calon As Array

        strSQL = "  SELECT PelajarID
                    ,MYKAD
                    ,AngkaGiliran
                    FROM kpmkv_svmu
                    WHERE
                    svmu_id = '" & AsciiSwitchWithMod(Request.QueryString("NO"), -19, -7) & "'"
        strRet = oCommon.getFieldValueEx(strSQL)

        ar_Calon = strRet.Split("|")

        Dim strPelajarID As String = ar_Calon(0)
        Dim strMYKAD As String = ar_Calon(1)
        Dim strAG As String = ar_Calon(2)

        strSQL = "  SELECT 
                    svmu_calon_id, svmu_id, Nama, TarikhLahir, Jantina, Bangsa, Agama, KolejID, KolejNama, Kohort, Alamat, Poskod, Bandar, Negeri, Telefon, Email, create_timestamp
                    FROM 
                    kpmkv_svmu_calon
                    WHERE
                    svmu_id = '" & AsciiSwitchWithMod(Request.QueryString("NO"), -19, -7) & "' AND TahunPeperiksaan = '" & TahunPeperiksaan & "'"
        strRet = oCommon.getFieldValueEx(strSQL)

        ar_Calon = strRet.Split("|")

        txtNama.Text = ar_Calon(2)
        txtMYKAD.Text = strMYKAD
        txtAngkaGiliran.Text = strAG
        txtDate.Text = ar_Calon(3)
        txtJantina.Text = ar_Calon(4)
        txtBangsa.Text = ar_Calon(5)
        txtAgama.Text = ar_Calon(6)
        txtKolej.Text = ar_Calon(8)
        txtKohort.Text = ar_Calon(9)
        txtAlamat.Text = ar_Calon(10)
        txtPoskod.Text = ar_Calon(11)
        txtBandar.Text = ar_Calon(12)
        txtNegeri.Text = ar_Calon(13)
        txtTelefon.Text = ar_Calon(14)
        txtEmail.Text = ar_Calon(15)

    End Sub

    Private Sub getMP()

        Dim rmAsas As Double
        Dim rmBM As Double
        Dim rmSJ As Double

        ''getRMAsas
        strSQL = "SELECT setting_value_double FROM kpmkv_svmu_setting WHERE setting_parameter = 'BAYARAN' AND setting_value_string = 'ASAS'"
        rmAsas = oCommon.getFieldValue(strSQL)
        lblRMAsas.Text = oCommon.getFieldValue(strSQL)

        ''getRMBM
        strSQL = "SELECT setting_value_double FROM kpmkv_svmu_setting WHERE setting_parameter = 'BAYARAN' AND setting_value_string = 'BM'"
        rmBM = oCommon.getFieldValue(strSQL)
        lblRMBM.Text = oCommon.getFieldValue(strSQL)

        ''getRMSJ
        strSQL = "SELECT setting_value_double FROM kpmkv_svmu_setting WHERE setting_parameter = 'BAYARAN' AND setting_value_string = 'SJ'"
        rmSJ = oCommon.getFieldValue(strSQL)
        lblRMSJ.Text = oCommon.getFieldValue(strSQL)

        strSQL = "SELECT setting_value_int FROM kpmkv_svmu_setting WHERE setting_parameter = 'TAHUN_PEPERIKSAAN'"
        Dim TahunPeperiksaan As String = oCommon.getFieldValue(strSQL)

        ''getMPBM
        strSQL = "SELECT svmu_calon_id FROM kpmkv_svmu_calon WHERE svmu_id = '" & AsciiSwitchWithMod(Request.QueryString("NO"), -19, -7) & "' AND TahunPeperiksaan = '" & TahunPeperiksaan & "' AND MataPelajaran = 'BM' AND StatusMP = '0'"
        strRet = oCommon.getFieldValue(strSQL)

        If strRet = "" Then

            trBM.Visible = False
            rmBM = 0

        Else

            trBM.Visible = True

        End If

        ''getMPSJ
        strSQL = "SELECT svmu_calon_id FROM kpmkv_svmu_calon WHERE svmu_id = '" & AsciiSwitchWithMod(Request.QueryString("NO"), -19, -7) & "' AND TahunPeperiksaan = '" & TahunPeperiksaan & "' AND MataPelajaran = 'SJ' AND StatusMP = '0'"
        strRet = oCommon.getFieldValue(strSQL)

        If strRet = "" Then

            trSJ.Visible = False
            rmSJ = 0

        Else

            trSJ.Visible = True

        End If

        lblTotalRM.Text = Format(rmAsas + rmBM + rmSJ, "0.00")

    End Sub

    Private Sub getPusatPeperiksaan()

        strSQL = "SELECT PusatPeperiksaanID FROM kpmkv_svmu_calon WHERE svmu_id = '" & AsciiSwitchWithMod(Request.QueryString("NO"), -19, -7) & "'"
        Dim PusatPeperiksaanID As String = oCommon.getFieldValue(strSQL)

        strSQL = "SELECT Kod, Nama, Negeri FROM kpmkv_kolej WHERE RecordID = '" & PusatPeperiksaanID & "'"
        strRet = oCommon.getFieldValueEx(strSQL)

        ''--get user info
        Dim kolej_Details As Array
        kolej_Details = strRet.Split("|")

        lblKodPusat.Text = kolej_Details(0)
        lblKolej.Text = kolej_Details(1)
        lblNegeri.Text = kolej_Details(2)

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

        Response.Redirect("svmu_pengakuan_calon.aspx?ID=" & Request.QueryString("ID") & "&NO=" & Request.QueryString("NO"))

    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click

        Response.Redirect("svmu_kemaskini_calon_ulang.aspx?ID=" & Request.QueryString("ID") & "&NO=" & Request.QueryString("NO"))

    End Sub
End Class