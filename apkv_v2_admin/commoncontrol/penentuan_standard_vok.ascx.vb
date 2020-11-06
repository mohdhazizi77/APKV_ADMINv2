Imports System.Data.SqlClient
Public Class penentuan_standard_vok
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

                kpmkv_tahun_list()
                ddlTahun.Text = Now.Year

                kpmkv_semester_list()
                ddlSemester.Text = ""

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

    Private Sub kpmkv_tahun_list()
        strSQL = "SELECT Tahun FROM kpmkv_tahun ORDER BY TahunID"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlTahun.DataSource = ds
            ddlTahun.DataTextField = "Tahun"
            ddlTahun.DataValueField = "Tahun"
            ddlTahun.DataBind()

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try

    End Sub
    Private Sub kpmkv_semester_list()
        strSQL = "SELECT Semester FROM kpmkv_semester  ORDER BY SemesterID"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlSemester.DataSource = ds
            ddlSemester.DataTextField = "Semester"
            ddlSemester.DataValueField = "Semester"
            ddlSemester.DataBind()
            '--ALL
            ddlSemester.Items.Add(New ListItem("-Pilih-", ""))
        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try

    End Sub

    '--load for edit--'
    Private Sub Load_page()

        strSQL = " SELECT Tahun,Semester,Sesi,SMP_PB,SMP_PAA,SMP_PAT FROM kpmkv_skor_Svm"
        strSQL += " WHERE SkorID='" & Request.QueryString("edit") & "'"

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


                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Tahun")) Then
                    ddlTahun.SelectedValue = ds.Tables(0).Rows(0).Item("Tahun")
                Else
                    ddlTahun.SelectedValue = Now.Year
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Semester")) Then
                    ddlSemester.SelectedValue = ds.Tables(0).Rows(0).Item("Semester")
                Else
                    ddlSemester.SelectedValue = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Sesi")) Then
                    chkSesi.Text = ds.Tables(0).Rows(0).Item("Sesi")
                Else
                    chkSesi.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("SMP_PB")) Then
                    txtSmpPB.Text = ds.Tables(0).Rows(0).Item("SMP_PB")
                Else
                    txtSmpPB.Text = ""
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("SMP_PAA")) Then
                    txtSmpPAA.Text = ds.Tables(0).Rows(0).Item("SMP_PAA")
                Else
                    txtSmpPAA.Text = ""
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("SMP_PAT")) Then
                    txtSmpPAT.Text = ds.Tables(0).Rows(0).Item("SMP_PAT")
                Else
                    txtSmpPAT.Text = ""
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


        Dim strOrder As String = " ORDER BY SkorID ASC"

        tmpSQL = "SELECT SkorID,Tahun,Semester,Sesi,SMP_PB,SMP_PAA,SMP_PAT FROM kpmkv_skor_svm"
        tmpSQL += " WHERE Tahun='" & ddlTahun.SelectedValue & "'"



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

            strSQL = "UPDATE kpmkv_skor_svm SET "

            strSQL += " Tahun='" & oCommon.FixSingleQuotes(ddlTahun.SelectedValue) & "',"
            strSQL += " Semester='" & oCommon.FixSingleQuotes(ddlSemester.SelectedValue) & "',"
            strSQL += " Sesi='" & oCommon.FixSingleQuotes(chkSesi.Text) & "',"
            strSQL += " SMP_PB='" & oCommon.FixSingleQuotes(txtSmpPB.Text) & "',"
            strSQL += " SMP_PAA='" & oCommon.FixSingleQuotes(txtSmpPAA.Text) & "',"
            strSQL += " SMP_PAT='" & oCommon.FixSingleQuotes(txtSmpPAT.Text) & "'"

            strSQL += " WHERE SkorID='" & Request.QueryString("edit") & "'"

        Else
            strSQL = "INSERT INTO kpmkv_skor_svm(Tahun,Semester,Sesi,SMP_PB,SMP_PAA,SMP_PAT)"

            strSQL += " VALUES("
            strSQL += " '" & oCommon.FixSingleQuotes(ddlTahun.SelectedValue) & " ',"
            strSQL += " '" & oCommon.FixSingleQuotes(ddlSemester.SelectedValue) & "',"
            strSQL += " '" & oCommon.FixSingleQuotes(chkSesi.Text) & "',"
            strSQL += " '" & oCommon.FixSingleQuotes(txtSmpPB.Text) & "',"
            strSQL += " '" & oCommon.FixSingleQuotes(txtSmpPAA.Text) & "',"
            strSQL += " '" & oCommon.FixSingleQuotes(txtSmpPAT.Text) & "')"

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
                lblMsg.Text = "Rekod Berjaya Disimpan"
            Else
                divMsg.Attributes("class") = "error"
            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message
        End Try

        strRet = BindData(datRespondent)
    End Sub


    Private Sub Refresh_ServerClick(sender As Object, e As EventArgs) Handles Refresh.ServerClick
        Response.Redirect("penentuan.standard.vok.aspx?p=" + lblConfig.Text)
    End Sub


    Private Sub datRespondent_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles datRespondent.RowDeleting
        Dim strTid = datRespondent.DataKeys(e.RowIndex).Values("SkorID").ToString

        'chk session to prevent postback
        If Not strTid = Session("strTypeid") Then
            strSQL = "DELETE FROM kpmkv_skor_svm WHERE SkorID='" & oCommon.FixSingleQuotes(strTid) & "'"
            strRet = oCommon.ExecuteSQL(strSQL)

            Session("strDid") = ""
        End If
        strRet = BindData(datRespondent)
    End Sub
    Private Sub ddlTahun_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTahun.SelectedIndexChanged
        strRet = BindData(datRespondent)
    End Sub


End Class