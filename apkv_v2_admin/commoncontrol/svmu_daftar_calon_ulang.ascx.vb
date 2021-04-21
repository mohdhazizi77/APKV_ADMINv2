Imports System.Data.SqlClient
Public Class svmu_daftar_calon_ulang1

    Inherits System.Web.UI.UserControl
    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub btnApply_Click(sender As Object, e As EventArgs) Handles btnApply.Click

        Try
            '--validate
            If ValidatePage() = False Then
                Exit Sub
            End If

            Dim strMYKAD As String = txtMYKAD.Text
            Dim strAG As String = txtAngkaGiliran.Text

            strSQL = "SELECT PelajarID FROM kpmkv_pelajar WHERE MYKAD = '" & strMYKAD & "' AND AngkaGiliran = '" & strAG & "'"
            Dim strPelajarID As String = oCommon.getFieldValue(strSQL)

            If strPelajarID.Length = 0 Then

                lblMsgMYKAD.Text = "Sila masukkan No. Kad Pengenalan yang betul!"
                lblMsgAngkaGiliran.Text = "Sila masukkan Angka Giliran yang betul!"

            Else

                strPelajarID = AsciiSwitchWithMod(strPelajarID, 19, 7)


                Response.Redirect("svmu_kemaskini_calon_ulang.aspx?PelajarID=" & strPelajarID)

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