Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization
Public Class config_penskoran_svm
    Inherits System.Web.UI.UserControl
    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                strSQL = "SELECT DISTINCT Jenis FROM kpmkv_gred_vokasional ORDER BY Jenis"
                txtJenis.Text = oCommon.getFieldValue(strSQL)

                kpmkv_tahun_list()
                ddlTahun.Text = Now.Year

                kpmkv_semester_list()
                kpmkv_status_list()
                kpmkv_kompentensi_list()

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
        strSQL = "SELECT Semester FROM kpmkv_semester ORDER BY SemesterID"
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

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub
    Private Sub kpmkv_status_list()
        strSQL = "SELECT Distinct Status FROM kpmkv_gred_vokasional ORDER BY Status"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlStatus.DataSource = ds
            ddlStatus.DataTextField = "Status"
            ddlStatus.DataValueField = "Status"
            ddlStatus.DataBind()

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub
    Private Sub kpmkv_kompentensi_list()
        strSQL = "SELECT Distinct Kompetensi FROM kpmkv_gred_vokasional ORDER BY Kompentasi"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlKompenten.DataSource = ds
            ddlKompenten.DataTextField = "Kompetensi"
            ddlKompenten.DataValueField = "Kompetensi"
            ddlKompenten.DataBind()

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try
    End Sub
    Private Function ValidatePage() As Boolean

        '--txtGed
        If txtGred.Text.Length = 0 Then
            lblMsg.Text = "Sila masukkan Gred!"
            txtGred.Focus()
            Return False
        End If

        If Not lblGred.Text = txtGred.Text Then       '--changes made to the Gred
            strSQL = "SELECT Gred FROM kpmkv_gred_vokasional WHERE Gred='" & oCommon.FixSingleQuotes(txtGred.Text) & "'"
            If oCommon.isExist(strSQL) = True Then
                lblMsg.Text = "Gred telah digunakan. Sila masukkan gred yang baru."
                Return False
            End If
        End If

        ''--txtMarkah
        If txtMarkahFrom.Text.Length = 0 Then
            lblMsg.Text = "Sila masukkan Markah Mula!"
            txtMarkahFrom.Focus()
            Return False
        End If

        ''--txtMarkah
        If txtMarkahTo.Text.Length = 0 Then
            lblMsg.Text = "Sila masukkan Markah Akhir!"
            txtMarkahTo.Focus()
            Return False
        End If

        If CInt(txtMarkahFrom.Text) > CInt(txtMarkahTo.Text) Then
            lblMsg.Text = "Markah Mula mesti lebih Kecik dari Markah Akhir"
            txtMarkahFrom.Focus()
            Return False
        End If


        Return True
    End Function

    Private Function insert() As Boolean

        strSQL = "INSERT INTO kpmkv_gred_vokasional(MarkahFrom, MarkahTo, Gred, Status, Kompetensi, Tahun, Jenis, Sesi, Semester) "
        strSQL += " VALUES ('" & oCommon.FixSingleQuotes(txtMarkahFrom.Text) & "','" & oCommon.FixSingleQuotes(txtMarkahTo.Text) & "',"
        strSQL += " '" & oCommon.FixSingleQuotes(txtGred.Text.ToUpper) & "','" & ddlStatus.Text & "','" & ddlKompenten.Text & "',"
        strSQL += " '" & ddlTahun.Text & "','" & chkSesi.Text & "','" & ddlSemester.Text & "')"
        strRet = oCommon.ExecuteSQL(strSQL)
        '--Debug
        'Response.Write(strSQL)
        If strRet = "0" Then
            lblMsg.Text = "Berjaya daftar baru Gred"
            Return True
        Else
            lblMsg.Text = "System Error:" & strRet

            Return False
        End If
    End Function
    Protected Sub btnDaftar_Click(sender As Object, e As EventArgs) Handles btnDaftar.Click
        lblMsg.Text = ""
        If ValidatePage() = False Then
            divMsg.Attributes("class") = "error"
            Exit Sub
        Else
            insert()
            strRet = BindData(datRespondent)

        End If
    End Sub
    Private Function getSQL() As String
        Dim tmpSQL As String
        Dim strOrder As String = " ORDER BY GredbsID ASC"

        '--not deleted
        tmpSQL = "SELECT * FROM kpmkv_gred_vokasional"
        tmpSQL += " WHERE Tahun ='" & ddlTahun.Text & "'"
        tmpSQL += " AND Semester='" & ddlSemester.Text & "'"
        tmpSQL += " AND Sesi ='" & chkSesi.Text & "'"

        getSQL = tmpSQL & strOrder

        Return getSQL

    End Function
    Private Sub datRespondent_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles datRespondent.RowDeleting
        lblMsg.Text = ""
        Dim IntGredbsID As Integer = datRespondent.DataKeys(e.RowIndex).Values("GredbsID")
        Try
            If Not IntGredbsID = Session("GredbsID") Then
                'kpmkv_pelajar
                strSQL = "Delete GredbsID FROM kpmkv_gred_vokasional WHERE GredbsID='" & IntGredbsID & "'"
                If strRet = oCommon.ExecuteSQL(strSQL) = 0 Then
                    divMsg.Attributes("class") = "info"
                    lblMsg.Text = "Gred berjaya dipadamkan"
                    Session("GredbsID") = ""
                Else
                    divMsg.Attributes("class") = "error"
                    lblMsg.Text = "Gred tidak berjaya dipadamkan"
                    Session("GredbsID") = ""
                End If
            Else
                Session("GredbsID") = ""
            End If
        Catch ex As Exception
            divMsg.Attributes("class") = "error"
        End Try

        strRet = BindData(datRespondent)
    End Sub
    Private Sub datRespondent_SelectedIndexChanging(sender As Object, e As GridViewSelectEventArgs) Handles datRespondent.SelectedIndexChanging
        Dim strKeyID As String = datRespondent.DataKeys(e.NewSelectedIndex).Value
        strRet = BindData(datRespondent)
    End Sub

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

    Private Sub ddlTahun_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTahun.SelectedIndexChanged
        strRet = BindData(datRespondent)
    End Sub

    Private Sub chkSesi_SelectedIndexChanged(sender As Object, e As EventArgs) Handles chkSesi.SelectedIndexChanged
        strRet = BindData(datRespondent)
    End Sub

    Private Sub ddlSemester_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSemester.SelectedIndexChanged
        strRet = BindData(datRespondent)
    End Sub
End Class