Imports System.Net
Imports System.IO
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports System.Data.SqlClient
Imports System.Globalization
Imports iTextSharp.text

Public Class pendaftaran_calon_ulang_online1
    Inherits System.Web.UI.UserControl
    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim fileSavePath As String = ConfigurationManager.AppSettings("FolderPath")
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        getTarikh()

        strSQL = "SELECT setting_value_int FROM kpmkv_svmu_setting WHERE setting_parameter = 'PRODUCTION'"
        Dim Live As String = oCommon.getFieldValue(strSQL)

        If Live = "3" Then

            ''MAINTENANCE
            Response.Redirect("svmu_maintenance.aspx")

        End If
    End Sub

    Private Sub getTarikh()

        strSQL = "SELECT setting_value_string FROM kpmkv_svmu_setting WHERE setting_parameter = 'TARIKH_MULA'"
        Dim TARIKH_MULA As String = oCommon.getFieldValue(strSQL)

        strSQL = "SELECT setting_value_string FROM kpmkv_svmu_setting WHERE setting_parameter = 'TARIKH_AKHIR'"
        Dim TARIKH_AKHIR As String = oCommon.getFieldValue(strSQL)

        Dim TARIKH_NOW As Date = Now.Date

        Dim startDate As DateTime = DateTime.ParseExact(TARIKH_MULA, "dd/MM/yyyy", CultureInfo.InvariantCulture)

        Dim endDate As DateTime = DateTime.ParseExact(TARIKH_AKHIR, "dd/MM/yyyy", CultureInfo.InvariantCulture)

        Dim ts As New TimeSpan
        ts = startDate.Subtract(TARIKH_NOW)
        Dim dayDiffMula = ts.Days
        ts = endDate.Subtract(TARIKH_NOW)
        Dim dayDiffAkhir = ts.Days

        If dayDiffMula <= 0 And dayDiffAkhir >= 0 Then

            table1.Visible = True
            table2.Visible = False

        Else

            table1.Visible = False
            table2.Visible = True

        End If



    End Sub

End Class