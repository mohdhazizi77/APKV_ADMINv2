Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization
Public Class admin_skor_vokasional
    Inherits System.Web.UI.UserControl
    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                kpmkv_tahun_list()
                ddlTahun.Text = Now.Year

                kpmkv_semester_list()

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
    Private Function ValidatePage() As Boolean

        '--txtPB
        If CInt(txtPB.Text) < 0 Then
            lblMsg.Text = "Sila masukkan markah Penskoran PB!"
            txtPB.Focus()
            Return False
        End If

        '--txtPAA
        If CInt(txtPAA.Text) < 0 Then
            lblMsg.Text = "Sila masukkan markah Penskoran PA Amali!"
            txtPAA.Focus()
            Return False
        End If

        '--txtPAA
        If CInt(txtPAT.Text) < 0 Then
            lblMsg.Text = "Sila masukkan markah Penskoran PA Teori!"
            txtPAT.Focus()
            Return False
        End If

        '--chkSesi
        If chkSesi.Text = "" Then
            lblMsg.Text = "Sila pilih Sesi kemasukkan!"
            chkSesi.Focus()
            Return False
        End If

        strSQL = "SELECT Tahun FROM kpmkv_skor_svm WHERE Tahun='" & ddlTahun.Text & "' AND Sesi='" & chkSesi.Text & "' AND Semester='" & ddlSemester.Text & "' "
        If oCommon.isExist(strSQL) = True Then
            lblMsg.Text = "Tahun telah digunakan. Sila masukkan Tahun yang baru."
            Return False
        End If


        Return True
    End Function
    Private Function insert() As Boolean

        strSQL = "INSERT INTO kpmkv_skor_svm(SMP_PB, SMP_PAA, SMP_PAT, Tahun, Sesi, Semester) "
        strSQL += "VALUES ('" & txtPB.Text & "','" & txtPAA.Text & "','" & txtPAT.Text & "','" & ddlTahun.Text & "','" & chkSesi.Text & "','" & ddlSemester.Text & "')"
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
        Dim strOrder As String = " ORDER BY SkorID ASC"

        '--not deleted
        tmpSQL = "SELECT * FROM kpmkv_skor_svm"
        tmpSQL += " WHERE Tahun ='" & ddlTahun.Text & "'"
        tmpSQL += " AND Semester='" & ddlSemester.Text & "'"
        tmpSQL += " AND Sesi ='" & chkSesi.Text & "'"

        getSQL = tmpSQL & strOrder

        Return getSQL

    End Function
    Private Sub datRespondent_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles datRespondent.RowDeleting
        lblMsg.Text = ""
        Dim IntSkorID As Integer = datRespondent.DataKeys(e.RowIndex).Values("SkorID")
        Try
            If Not IntSkorID = Session("SkorID") Then
                'kpmkv_pelajar
                strSQL = "Delete SkorID FROM kpmkv_skor_svm WHERE SkorID='" & IntSkorID & "'"
                If strRet = oCommon.ExecuteSQL(strSQL) = 0 Then
                    divMsg.Attributes("class") = "info"
                    lblMsg.Text = "Skor Pemarkahan berjaya dipadamkan"
                    Session("SkorID") = ""
                Else
                    divMsg.Attributes("class") = "error"
                    lblMsg.Text = "Skor Pemarkahan tidak berjaya dipadamkan"
                    Session("SkorID") = ""
                End If
            Else
                Session("SkorID") = ""
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
End Class