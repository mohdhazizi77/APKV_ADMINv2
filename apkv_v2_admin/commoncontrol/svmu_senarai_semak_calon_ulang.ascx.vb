Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization

Public Class svmu_senarai_semak_calon_ulang
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Dim strNegeri As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                strRet = BindData(datRespondent)

            End If

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message
        End Try
    End Sub

    Private Function BindData(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 120

        Try
            myDataAdapter.Fill(myDataSet, "myaccount")

            If myDataSet.Tables(0).Rows.Count = 0 Then
                divMsg.Attributes("class") = "error"
                lblMsg.Text = "Rekod tidak dijumpai!"
            Else
                divMsg.Attributes("class") = "info"
                lblMsg.Text = "Jumlah Rekod#:" & myDataSet.Tables(0).Rows.Count
            End If

            gvTable.DataSource = myDataSet
            gvTable.DataBind()
            objConn.Close()
        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message
            Return False
        End Try

        Return True

    End Function

    Private Function getSQL() As String

        strSQL = "SELECT DISTINCT
kpmkv_svmu.svmu_id, kpmkv_svmu_calon.svmu_calon_id, kpmkv_svmu.MYKAD, kpmkv_svmu.AngkaGiliran,
kpmkv_svmu_calon.Nama, kpmkv_svmu_calon.svmu_no_permohonan, kpmkv_svmu_calon.Status
FROM kpmkv_svmu
LEFT JOIN kpmkv_svmu_calon ON kpmkv_svmu_calon.svmu_id = kpmkv_svmu.svmu_id
WHERE
kpmkv_svmu_calon.StatusMP = '1'
AND kpmkv_svmu.svmu_id = '" & AsciiSwitchWithMod(Request.QueryString("NO"), -19, -7) & "'"

        getSQL = strSQL

        Return getSQL

    End Function


    Private Sub datRespondent_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles datRespondent.PageIndexChanging
        datRespondent.PageIndex = e.NewPageIndex
        strRet = BindData(datRespondent)

    End Sub

    Private Function GetData(ByVal cmd As SqlCommand) As DataTable
        Dim dt As New DataTable()
        Dim strConnString As [String] = ConfigurationManager.AppSettings("ConnectionString")
        Dim con As New SqlConnection(strConnString)
        Dim sda As New SqlDataAdapter()
        cmd.CommandType = CommandType.Text
        cmd.Connection = con
        Try
            con.Open()
            sda.SelectCommand = cmd
            sda.Fill(dt)
            Return dt
        Catch ex As Exception
            Throw ex
        Finally
            con.Close()
            sda.Dispose()
            con.Dispose()
        End Try
    End Function

    Private Sub datRespondent_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSelectEventArgs) Handles datRespondent.SelectedIndexChanging

        Dim strKeyID As String = datRespondent.DataKeys(e.NewSelectedIndex).Value.ToString

        strSQL = "SELECT JenisDaftar FROM kpmkv_svmu_calon WHERE svmu_calon_id = '" & strKeyID & "'"
        Dim JenisDaftar As String = oCommon.getFieldValue(strSQL)

        If JenisDaftar = "MANUAL" Then

            Response.Redirect("svmu.tindakan.calon.aspx?Type=M&ID=" & strKeyID)

        Else

            strSQL = "SELECT RefNo FROM kpmkv_svmu_calon WHERE svmu_calon_id = '" & strKeyID & "'"
            Dim RefNo As String = oCommon.getFieldValue(strSQL)

            Response.Redirect("svmu.tindakan.calon.aspx?Type=O&RefNo=" & RefNo)

        End If


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

End Class