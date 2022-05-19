Imports System.Net.Mail

Public Class pentaksir_pendaftaran
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub btnSend_Click(sender As Object, e As EventArgs) Handles btnSend.Click
        Try
            Dim Smtp_Server As New SmtpClient
            Dim e_mail As New MailMessage()
            Smtp_Server.UseDefaultCredentials = False
            Smtp_Server.Credentials = New Net.NetworkCredential("mohdhaziziak@gmail.com", "MH14126gg")
            Smtp_Server.Port = 587
            Smtp_Server.EnableSsl = True
            Smtp_Server.Host = "smtp.gmail.com"

            e_mail = New MailMessage()
            e_mail.From = New MailAddress("mohdhaziziak@gmail.com")
            e_mail.To.Add(txtMykad.Text)
            e_mail.Subject = "Pendaftaran Pentaksir"
            e_mail.IsBodyHtml = False
            e_mail.Body = txtMykad.Text
            Smtp_Server.Send(e_mail)
            MsgBox("Mail Sent")

        Catch error_t As Exception
            lblMsg.Text = error_t.ToString
        End Try

    End Sub

End Class