Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization

Public Class wajaran_akademik_list
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                kpmkv_kohort_list()
                ddlKohort.Text = "-PILIH-"
                kpmkv_tahunpeperiksaan_list()
                ddlTahunPeperiksaan.Text = "-PILIH-"
                kpmkv_semester_list()
                ddlSemester.Text = "-PILIH-"

                strRet = BindData(datRespondent)
            End If

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message
        End Try
    End Sub

    Private Sub kpmkv_kohort_list()
        strSQL = "SELECT DISTINCT Kohort FROM kpmkv_wajaran_a ORDER BY Kohort"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlKohort.DataSource = ds
            ddlKohort.DataTextField = "Kohort"
            ddlKohort.DataValueField = "Kohort"
            ddlKohort.DataBind()

            '--ALL
            ddlKohort.Items.Add(New ListItem("-PILIH-", "-PILIH-"))

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub kpmkv_tahunpeperiksaan_list()
        strSQL = "SELECT DISTINCT TahunPeperiksaan FROM kpmkv_wajaran_a ORDER BY TahunPeperiksaan"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlTahunPeperiksaan.DataSource = ds
            ddlTahunPeperiksaan.DataTextField = "TahunPeperiksaan"
            ddlTahunPeperiksaan.DataValueField = "TahunPeperiksaan"
            ddlTahunPeperiksaan.DataBind()

            '--ALL
            ddlTahunPeperiksaan.Items.Add(New ListItem("-PILIH-", "-PILIH-"))

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub kpmkv_semester_list()
        strSQL = "SELECT DISTINCT Semester FROM kpmkv_wajaran_a ORDER BY Semester"
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
            ddlSemester.Items.Add(New ListItem("-PILIH-", "-PILIH-"))

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        lblMsg.Text = ""
        strRet = BindData(datRespondent)

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
        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrder As String = " ORDER BY Kohort, TahunPeperiksaan, Semester"

        tmpSQL = "SELECT * FROM kpmkv_wajaran_a"
        strWhere = " WHERE WajaranID IS NOT NULL"

        If Not ddlKohort.Text = "-PILIH-" Then
            strWhere += " AND Kohort ='" & ddlKohort.Text & "'"
        End If

        If Not ddlTahunPeperiksaan.Text = "-PILIH-" Then
            strWhere += " AND TahunPeperiksaan ='" & ddlTahunPeperiksaan.Text & "'"
        End If

        If Not ddlSemester.Text = "-PILIH-" Then
            strWhere += " AND Semester ='" & ddlSemester.Text & "'"
        End If

        getSQL = tmpSQL & strWhere & strOrder
        ''--debug
        'Response.Write(getSQL)

        Return getSQL

    End Function

    Private Sub datRespondent_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles datRespondent.PageIndexChanging
        datRespondent.PageIndex = e.NewPageIndex
        strRet = BindData(datRespondent)
    End Sub

    Private Sub datRespondent_SelectedIndexChanging(sender As Object, e As GridViewSelectEventArgs) Handles datRespondent.SelectedIndexChanging
        Dim strKeyID As String = datRespondent.DataKeys(e.NewSelectedIndex).Value.ToString
        Response.Redirect("wajaran_akademik_update.aspx?WajaranID = " & strKeyID)

    End Sub

    Protected Sub btnDaftar_Click(sender As Object, e As EventArgs) Handles btnDaftar.Click
        Response.Redirect("wajaran_akademik_create.aspx")
    End Sub
End Class