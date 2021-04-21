Public Class svmu_rumusan_pendaftaran
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub btnProceed_Click(sender As Object, e As EventArgs) Handles btnProceed.Click

        Response.Redirect("svmu_pengakuan_calon.aspx")

    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click

        Response.Redirect("svmu_kemaskini_calon_ulang.aspx")
    End Sub
End Class