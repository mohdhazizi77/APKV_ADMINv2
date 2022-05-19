Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization

Public Class pentaksirbm_main1
    Inherits System.Web.UI.UserControl
    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                strRet = BindData(datRespondent)

            End If

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message
        End Try
    End Sub

    Private Sub datRespondent_SelectedIndexChanging(sender As Object, e As GridViewSelectEventArgs) Handles datRespondent.SelectedIndexChanging
        Dim strKey As String = datRespondent.DataKeys(e.NewSelectedIndex).Value.ToString

        strSQL = "SELECT MataPelajaran FROM kpmkv_pentaksir_bmsetara WHERE id = '" & strKey & "'"
        Dim MataPelajaran As String = oCommon.getFieldValue(strSQL)

        If MataPelajaran = "BAHASA MELAYU 3" Then

            Response.Redirect("pentaksirbm_calon_bm3.aspx?id=" & strKey)

        Else

            Response.Redirect("pentaksirbm_calon_bm4.aspx?id=" & strKey)

        End If


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

        strSQL = "SELECT UserID FROM kpmkv_users WHERE LoginID = '" & Session("LoginID") & "' AND Pwd = '" & Session("Password") & "'"
        Dim UserID As String = oCommon.getFieldValue(strSQL)

        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrder As String = " ORDER BY UserGroupCode ASC"

        tmpSQL = "  SELECT kpmkv_pentaksir_bmsetara.id,
                    kpmkv_pentaksir_bmsetara.UserID,
                    kpmkv_pentaksir_bmsetara.MataPelajaran,
                    kpmkv_pentaksir_bmsetara.Kohort,
                    kpmkv_pentaksir_bmsetara.Semester,
                    kpmkv_kolej.Nama
                    FROM kpmkv_pentaksir_bmsetara
                    LEFT JOIN kpmkv_kolej ON kpmkv_kolej.RecordID = kpmkv_pentaksir_bmsetara.KolejRecordID
                    WHERE kpmkv_pentaksir_bmsetara.UserID = '" & UserID & "'"

        getSQL = tmpSQL
        ''--debug
        'Response.Write(getSQL)

        Return getSQL

    End Function

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

    'Private Sub datRespondent_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles datRespondent.RowDeleting
    '    Dim strKeyID As String = datRespondent.DataKeys(e.RowIndex).Value
    '    Try
    '        strSQL = "DELETE FROM tbl_ctrl_usergroup WHERE UserGroupCode='" & strKeyID & "'"
    '        strRet = oCommon.ExecuteSQL(strSQL)
    '        If strRet = "0" Then
    '            divMsg.Attributes("class") = "info"
    '            lblMsg.Text = "Berjaya membatalkan parameter"
    '        Else
    '            divMsg.Attributes("class") = "error"
    '            lblMsg.Text = "Tidak Berjaya membatalkan parameter"
    '        End If

    '        ''debug
    '        'Response.Write(strSQL)
    '        strRet = BindData(datRespondent)

    '    Catch ex As Exception
    '        divMsg.Attributes("class") = "error"

    '    End Try
    'End Sub
End Class