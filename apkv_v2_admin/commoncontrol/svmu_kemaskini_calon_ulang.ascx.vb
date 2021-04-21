Imports System.Data.SqlClient
Public Class svmu_kemaskini_calon_ulang1
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim strPelajarID As String

        Dim strNama As String
        Dim strMYKAD As String
        Dim strJantina As String
        Dim strBangsa As String
        Dim strAgama As String
        Dim strAngkaGiliran As String
        Dim strKolejRecordID As String
        Dim strNamaKolej As String
        Dim strTahun As String

        strPelajarID = AsciiSwitchWithMod(Request.QueryString("PelajarID"), -19, -7)

        strSQL = "SELECT kpmkv_pelajar.Nama, kpmkv_pelajar.MYKAD, kpmkv_pelajar.Jantina, kpmkv_pelajar.Bangsa, kpmkv_pelajar.Agama, kpmkv_pelajar.AngkaGiliran, kpmkv_pelajar.KolejRecordID, kpmkv_kolej.Nama, kpmkv_pelajar.Tahun FROM kpmkv_pelajar 
LEFT JOIN kpmkv_kolej ON kpmkv_pelajar.KolejRecordID = kpmkv_kolej.RecordID WHERE kpmkv_pelajar.PelajarID = '" & strPelajarID & "'"
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

        txtNama.Text = strNama
        txtMYKAD.Text = strMYKAD
        txtJantina.Text = strJantina
        txtKeturunan.Text = strBangsa
        txtAgama.Text = strAgama
        txtAngkaGiliran.Text = strAngkaGiliran
        ''txtNama.Text = strKolejRecordID
        ''txtNama.Text = strNamaKolej
        txtKohort.Text = strTahun

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

        Response.Redirect("svmu_rumusan_pendaftaran.aspx")

    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click

        Response.Redirect("svmu_daftar_calon_ulang.aspx")

    End Sub
End Class