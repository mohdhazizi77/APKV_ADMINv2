Imports System.Data.SqlClient

Public Class svmu_pengakuan_calon
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click

        Response.Redirect("svmu_kemaskini_calon_ulang.aspx?ID=" & Request.QueryString("ID") & "&NO=" & Request.QueryString("NO"))

    End Sub

    Private Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click

        strSQL = "SELECT setting_value_int FROM kpmkv_svmu_setting WHERE setting_parameter = 'TAHUN_PEPERIKSAAN'"
        Dim TahunPeperiksaan As String = oCommon.getFieldValue(strSQL)

        strSQL = "UPDATE kpmkv_svmu_calon SET Status = 'BELUM DISAHKAN' WHERE svmu_id = '" & AsciiSwitchWithMod(Request.QueryString("NO"), -19, -7) & "' AND Status = 'BARU' AND TahunPeperiksaan = '" & TahunPeperiksaan & "' AND StatusMP = '0'"
        strRet = oCommon.ExecuteSQL(strSQL)



        ''formatKekal
        strSQL = " SELECT setting_value_string FROM kpmkv_svmu_setting WHERE setting_parameter = 'FORMAT_NP'"
        Dim formatNPKekal As String = oCommon.getFieldValue(strSQL)

        ''TahunPeperiksaan
        strSQL = " SELECT RIGHT(setting_value_int,2) FROM kpmkv_svmu_setting WHERE setting_parameter = 'TAHUN_PEPERIKSAAN'"
        Dim formatNPTahunPeperiksaan As String = oCommon.getFieldValue(strSQL)

        ''npAkhir
        strSQL = " SELECT np_akhir FROM kpmkv_svmu_setting_np WHERE np_tahun = '" & TahunPeperiksaan & "'"
        Dim npAkhir As Integer = oCommon.getFieldValue(strSQL)
        npAkhir = npAkhir + 1

        ''INSERT 
        strSQL = " UPDATE kpmkv_svmu_calon SET svmu_no_permohonan = '" & formatNPKekal + formatNPTahunPeperiksaan + npAkhir.ToString("D" & 6) & "' WHERE svmu_id = '" & AsciiSwitchWithMod(Request.QueryString("NO"), -19, -7) & "'  AND StatusMP = '0'"
        strRet = oCommon.ExecuteSQL(strSQL)

        ''UPDATE
        strSQL = " UPDATE kpmkv_svmu_setting_np SET np_akhir = '" & npAkhir & "' WHERE np_tahun = '" & TahunPeperiksaan & "'"
        strRet = oCommon.ExecuteSQL(strSQL)


        Response.Redirect("svmu_pembayaran_pendaftaran.aspx?ID=" & Request.QueryString("ID") & "&NO=" & Request.QueryString("NO"))

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

    Private Sub chkPengakuan_CheckedChanged(sender As Object, e As EventArgs) Handles chkPengakuan.CheckedChanged

        If chkPengakuan.Checked = True Then

            btnSubmit.Enabled = True

        Else

            btnSubmit.Enabled = False

        End If

    End Sub
End Class