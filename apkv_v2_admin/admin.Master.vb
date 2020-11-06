Imports System.Data.SqlClient
Partial Public Class admin
    Inherits System.Web.UI.MasterPage
    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                strSQL = "SELECT COUNT(*) FROM kpmkv_bahan WHERE (Komen IS NOT NULL AND Komen <>'')"
                lblTotal.InnerText = oCommon.getFieldValue(strSQL)
            End If

        Catch ex As Exception

        End Try
    End Sub

End Class