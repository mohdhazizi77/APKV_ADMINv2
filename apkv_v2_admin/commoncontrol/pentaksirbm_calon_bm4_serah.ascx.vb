Imports System.Data.SqlClient


Public Class pentaksirbm_calon_bm4_serah1
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                strSQL = "SELECT MataPelajaran FROM kpmkv_pentaksir_bmsetara WHERE id = '" & Request.QueryString("id") & "'"
                lblMP.Text = oCommon.getFieldValue(strSQL)

                strSQL = "SELECT KolejRecordID FROM kpmkv_pentaksir_bmsetara WHERE id = '" & Request.QueryString("id") & "'"
                Dim KolejRecordID As String = oCommon.getFieldValue(strSQL)

                strSQL = "SELECT Nama FROM kpmkv_kolej WHERE RecordID = '" & KolejRecordID & "'"
                lblPP.Text = oCommon.getFieldValue(strSQL)

                lblMsg.Text = ""
                strRet = BindData(datRespondent)

            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message
        End Try
    End Sub

    Private Sub datRespondent_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles datRespondent.PageIndexChanging
        datRespondent.PageIndex = e.NewPageIndex
        strRet = BindData(datRespondent)
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click

        Dim id As String = Request.QueryString("id")

        Response.Redirect("pentaksirbm_calon_bm4.aspx?id=" & id)

    End Sub

    Private Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click

        Try

            strSQL = "SELECT UserID FROM kpmkv_users WHERE LoginID = '" & Session("LoginID") & "' AND Pwd = '" & Session("Password") & "'"
            Dim UserID As String = oCommon.getFieldValue(strSQL)

            For i As Integer = 0 To datRespondent.Rows.Count - 1

                strSQL = "  UPDATE kpmkv_pentaksir_bmsetara_calon
                            SET
                            StatusHantarBM4_Pentaksir = 'TELAH HANTAR',
                            StatusHantarBM4_Pentaksir_timestamp = CURRENT_TIMESTAMP
                            WHERE id = '" & datRespondent.DataKeys(i).Value.ToString & "'"

                strRet = oCommon.ExecuteSQL(strSQL)

                If strRet = "0" Then
                    lblMsg.Attributes("class") = "info"
                    lblMsg.Text = "Markah berjaya dihantar"
                Else
                    lblMsg.Attributes("class") = "error"
                    lblMsg.Text = "Markah tidak berjaya dihantar"
                End If

            Next

        Catch ex As Exception
            lblMsg.Text = "Error:" & ex.Message
        End Try

        strRet = BindData(datRespondent)

    End Sub

    Private Function BindData(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 120
        Try
            myDataAdapter.Fill(myDataSet, "myaccount")

            If myDataSet.Tables(0).Rows.Count = 0 Then
                'divMsg.Attributes("class") = "error"
                'lblMsg.Text = "Tiada rekod pelajar."
            Else
                divMsg.Attributes("class") = "info"
                lblMsg.Text = "Jumlah rekod#:" & myDataSet.Tables(0).Rows.Count
            End If

            gvTable.DataSource = myDataSet
            gvTable.DataBind()
            objConn.Close()

        Catch ex As Exception
            lblMsg.Text = "Error:" & ex.Message
            Return False
        End Try

        Return True

    End Function
    Private Function getSQL() As String

        strSQL = "SELECT KolejRecordID FROM kpmkv_pentaksir_bmsetara WHERE id = '" & Request.QueryString("id") & "'"
        Dim KolejRecordID As String = oCommon.getFieldValue(strSQL)

        strSQL = "SELECT Kohort FROM kpmkv_pentaksir_bmsetara WHERE id = '" & Request.QueryString("id") & "'"
        Dim Kohort As String = oCommon.getFieldValue(strSQL)

        strSQL = "SELECT Semester FROM kpmkv_pentaksir_bmsetara WHERE id = '" & Request.QueryString("id") & "'"
        Dim Semester As String = oCommon.getFieldValue(strSQL)


        Dim tmpSQL As String = ""
        Dim strWhere As String = ""
        Dim strOrder As String = " ORDER BY AngkaGiliran, Nama ASC"

        '--not deleted
        tmpSQL = "SELECT id, KolejRecordID, Tahun, Nama, MYKAD, AngkaGiliran, BM4, BM4_Total, StatusHantarBM4_Pentaksir FROM kpmkv_pentaksir_bmsetara_calon"
        strWhere = " WHERE KolejRecordID='" & KolejRecordID & "' AND Tahun ='" & Kohort & "' AND StatusBM4 = '1' AND StatusHantarBM4_Pentaksir = 'BELUM HANTAR'"

        getSQL = tmpSQL & strWhere & strOrder
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

End Class

