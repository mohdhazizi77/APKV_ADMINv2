Public Class error_page1
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim error_Text As String = Request.QueryString("aspxerrorpath")

    End Sub

End Class