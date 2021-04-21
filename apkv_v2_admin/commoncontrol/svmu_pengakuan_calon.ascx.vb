Public Class svmu_pengakuan_calon
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click

        Response.Redirect("svmu_kemaskini_calon_ulang.aspx")

    End Sub

    Private Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click

        Response.Redirect("svmu_pembayaran_pendaftaran.aspx")

    End Sub
End Class