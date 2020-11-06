Imports System.Data.SqlClient
Public Class prm_muatnaik_bahan1
    Inherits System.Web.UI.UserControl
    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                Session("strTypeid") = ""


                If Not Request.QueryString("edit") = "" Then
                    Load_page()
                Else
                    lblConfig.Text = Request.QueryString("p")
                End If

                strRet = BindData(datRespondent)
            End If

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message
        End Try
    End Sub

    '--load for edit--'
    Private Sub Load_page()

        strSQL = "SELECT Type FROM tbl_Settings WHERE ID='" & Request.QueryString("edit") & "'"
        lblConfig.Text = oCommon.getFieldValue(strSQL)

        strSQL = " SELECT Parameter,Value,idx,Description FROM tbl_Settings"
        strSQL += " WHERE ID='" & Request.QueryString("edit") & "'"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            Dim nRows As Integer = 0
            Dim nCount As Integer = 1
            Dim MyTable As DataTable = New DataTable
            MyTable = ds.Tables(0)
            If MyTable.Rows.Count > 0 Then


                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Parameter")) Then
                    txtCategory.Text = ds.Tables(0).Rows(0).Item("Parameter")
                Else
                    txtCategory.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Value")) Then
                    txtStatus.Text = ds.Tables(0).Rows(0).Item("Value")
                Else
                    txtStatus.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("idx")) Then
                    txtidx.Text = ds.Tables(0).Rows(0).Item("idx")
                Else
                    txtidx.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Description")) Then
                    txtDescription.Text = ds.Tables(0).Rows(0).Item("Description")
                Else
                    txtDescription.Text = ""
                End If

            End If
        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub


    '--list--'
    Private Function getSQL() As String
        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim Action As String = lblConfig.Text


        Dim strOrder As String = " ORDER BY idx,Parameter ASC"

        tmpSQL = "SELECT id,Parameter,Value,Description,idx FROM tbl_Settings"

        strWhere += " WHERE Type='" & Action & "'"

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

    Private Function BindData(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 120

        Try
            myDataAdapter.Fill(myDataSet, "myaccount")

            If myDataSet.Tables(0).Rows.Count = 0 Then
                divMsg.Attributes("class") = "error"
                lblMsg.Text = "No Record Found!"
            Else
                divMsg.Attributes("class") = "info"
                lblMsg.Text = "Total Records#:" & myDataSet.Tables(0).Rows.Count
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

    '-----add function-----'
    Private Function Save() As Boolean
        If Not Request.QueryString("edit") = "" Then

            strSQL = "UPDATE tbl_Settings SET "

            strSQL += " Parameter='" & oCommon.FixSingleQuotes(txtCategory.Text) & "',"
            strSQL += " Value='" & oCommon.FixSingleQuotes(txtStatus.Text) & "',"
            strSQL += " Type='" & oCommon.FixSingleQuotes(lblConfig.Text) & "',"
            strSQL += " idx='" & oCommon.FixSingleQuotes(txtidx.Text) & "',"
            strSQL += " Description='" & oCommon.FixSingleQuotes(txtDescription.Text) & "'"

            strSQL += " WHERE id='" & Request.QueryString("edit") & "'"

        Else
            strSQL = "INSERT INTO tbl_Settings(Parameter,Value,Type,idx,Description)"

            strSQL += " VALUES("
            strSQL += " '" & oCommon.FixSingleQuotes(txtCategory.Text) & " ',"
            strSQL += " '" & oCommon.FixSingleQuotes(txtStatus.Text) & "',"
            strSQL += " '" & oCommon.FixSingleQuotes(lblConfig.Text) & "',"
            strSQL += " '" & oCommon.FixSingleQuotes(txtidx.Text) & "',"
            strSQL += " '" & oCommon.FixSingleQuotes(txtDescription.Text) & "')"

        End If

        strRet = oCommon.ExecuteSQL(strSQL)
        '--Debug
        'Response.Write(strSQL)

        If strRet = "0" Then
            Return True
        Else
            lblMsg.Text = "System Error:" & strRet

            Return False
        End If
    End Function

    Private Sub SaveFunction_ServerClick(sender As Object, e As EventArgs) Handles SaveFunction.ServerClick


        lblMsg.Text = ""

        Try

            '--execute
            If Save() = True Then
                divMsg.Attributes("class") = "info"
                lblMsg.Text = "Record Successfully Save"
            Else
                divMsg.Attributes("class") = "error"
            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message
        End Try

        strRet = BindData(datRespondent)
    End Sub


    Private Sub Refresh_ServerClick(sender As Object, e As EventArgs) Handles Refresh.ServerClick
        Response.Redirect("prm.muatnaik.bahan.aspx?p=" + lblConfig.Text)
    End Sub


    Private Sub datRespondent_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles datRespondent.RowDeleting
        Dim strTid = datRespondent.DataKeys(e.RowIndex).Values("ID").ToString

        'chk session to prevent postback
        If Not strTid = Session("strTypeid") Then
            strSQL = "DELETE FROM tbl_Settings WHERE ID='" & oCommon.FixSingleQuotes(strTid) & "'"
            strRet = oCommon.ExecuteSQL(strSQL)

            Session("strDid") = ""
        End If
        strRet = BindData(datRespondent)
    End Sub


End Class