Imports System.Data.SqlClient
Public Class svmu_daftar_calon_ulang1

    Inherits System.Web.UI.UserControl
    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        strSQL = "SELECT setting_value_string FROM kpmkv_svmu_setting WHERE setting_parameter = 'TARIKH_MULA'"
        lblTarikhMula.Text = oCommon.getFieldValue(strSQL)

        strSQL = "SELECT setting_value_int FROM kpmkv_svmu_setting WHERE setting_parameter = 'TARIKH_AKHIR'"
        lblTarikhAkhir.Text = oCommon.getFieldValue(strSQL)

    End Sub

    Private Sub btnApply_Click(sender As Object, e As EventArgs) Handles btnApply.Click

        Try

            '--validate
            If ValidatePage() = False Then
                Exit Sub
            End If

            Dim strMYKAD As String = txtMYKAD.Text
            Dim strAG As String = txtAngkaGiliran.Text

            strSQL = "SELECT PelajarID FROM kpmkv_pelajar WHERE MYKAD = '" & strMYKAD & "' AND AngkaGiliran = '" & strAG & "' AND Semester = '4'"
            Dim strPelajarID As String = oCommon.getFieldValue(strSQL)

            If strPelajarID.Length = 0 Then

                lblMsgMYKAD.Text = "Sila masukkan No. Kad Pengenalan yang betul!"
                lblMsgAngkaGiliran.Text = "Sila masukkan Angka Giliran yang betul!"

            Else

                Dim svmuID As String

                strSQL = "SELECT setting_value_int FROM kpmkv_svmu_setting WHERE setting_parameter = 'TAHUN_PEPERIKSAAN'"
                Dim TahunPeperiksaan As String = oCommon.getFieldValue(strSQL)

                strSQL = "SELECT svmu_id FROM kpmkv_svmu WHERE PelajarID = '" & strPelajarID & "' AND Tahun = '" & TahunPeperiksaan & "'"
                svmuID = oCommon.getFieldValue(strSQL)

                If svmuID = "" Then

                    strSQL = "INSERT INTO kpmkv_svmu
                          (DatabaseName, Tahun,  PelajarID, MYKAD, AngkaGiliran, create_timestamp)
                          VALUES
                          ('APKV', '" & TahunPeperiksaan & "', '" & strPelajarID & "', '" & strMYKAD & "', '" & strAG & "', CURRENT_TIMESTAMP)"
                    strRet = oCommon.ExecuteSQL(strSQL)

                    strSQL = "SELECT svmu_id FROM kpmkv_svmu WHERE PelajarID = '" & strPelajarID & "'"
                    svmuID = oCommon.getFieldValue(strSQL)
                    Response.Redirect("svmu_kemaskini_calon_ulang.aspx?ID=" & AsciiSwitchWithMod(strPelajarID, 19, 7) & "&NO=" & AsciiSwitchWithMod(svmuID, 19, 7))

                Else

                    strSQL = "SELECT svmu_id FROM kpmkv_svmu WHERE PelajarID = '" & strPelajarID & "'"
                    svmuID = oCommon.getFieldValue(strSQL)

                    strSQL = "SELECT svmu_calon_id FROM kpmkv_svmu_calon WHERE svmu_id = '" & svmuID & "' AND TahunPeperiksaan = '" & TahunPeperiksaan & "' AND MataPelajaran = 'BM'"
                    Dim BM As String = oCommon.getFieldValue(strSQL)

                    strSQL = "SELECT svmu_calon_id FROM kpmkv_svmu_calon WHERE svmu_id = '" & svmuID & "' AND TahunPeperiksaan = '" & TahunPeperiksaan & "' AND MataPelajaran = 'SJ'"
                    Dim SJ As String = oCommon.getFieldValue(strSQL)

                    If Not BM = "" And Not SJ = "" Then

                        lblMsg1.Visible = True

                    Else

                        strSQL = "SELECT svmu_id FROM kpmkv_svmu WHERE PelajarID = '" & strPelajarID & "' AND Tahun = '" & TahunPeperiksaan & "'"
                        svmuID = oCommon.getFieldValue(strSQL)
                        Response.Redirect("svmu_kemaskini_calon_ulang.aspx?ID=" & AsciiSwitchWithMod(strPelajarID, 19, 7) & "&NO=" & AsciiSwitchWithMod(svmuID, 19, 7))

                    End If

                End If

            End If

        Catch ex As Exception

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


    Private Function ValidatePage() As Boolean

        Dim errorCode As String = "FALSE"

        lblMsgMYKAD.Text = ""
        lblMsgAngkaGiliran.Text = ""

        '--txtMYKAD
        If txtMYKAD.Text.Length > 12 Or txtMYKAD.Text.Length < 12 Then
            lblMsgMYKAD.Text = "Sila masukkan No. Kad Pengenalan yang betul!"
            errorCode = "TRUE"
        End If

        If txtMYKAD.Text.Length = 0 Then
            lblMsgMYKAD.Text = "Sila masukkan No. Kad Pengenalan!"
            errorCode = "TRUE"
        End If

        If txtAngkaGiliran.Text.Length > 11 Or txtAngkaGiliran.Text.Length < 11 Then
            lblMsgAngkaGiliran.Text = "Sila masukkan Angka Giliran yang betul!"
            errorCode = "TRUE"
        End If

        If txtAngkaGiliran.Text.Length = 0 Then
            lblMsgAngkaGiliran.Text = "Sila masukkan Angka Giliran!"
            errorCode = "TRUE"
        End If

        If errorCode = "TRUE" Then
            Return False
        Else
            Return True
        End If

    End Function

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click

        Response.Redirect("pendaftaran_calon_ulang_online.aspx")

    End Sub

End Class