Imports System.Data.SqlClient
Public Class mp_vok_search
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
                ddlTahun.Text = ""

                kpmkv_kod_list()
                ddlKodMataPelajaran.Text = ""

                kpmkv_semester_list()
                ddlSemester.Text = ""

                kpmkv_kodkursus_list()
                ddlKodKursus.SelectedValue = ""


                lblMsg.Text = ""

            End If
        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message
        End Try
    End Sub
    Private Sub kpmkv_tahun_list()
        strSQL = "SELECT Tahun FROM kpmkv_tahun  ORDER BY Tahun"
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

            '--ALL
            ddlTahun.Items.Add(New ListItem("-Pilih-", ""))

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub kpmkv_semester_list()
        strSQL = "SELECT Semester FROM kpmkv_semester  ORDER BY Semester"
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
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub kpmkv_kodkursus_list()

        strSQL = "SELECT KodKursus, KursusID "
        strSQL += " FROM kpmkv_kursus WHERE Sesi='" & chkSesi.SelectedValue & "'"
        strSQL += " AND Tahun='" & ddlTahun.SelectedValue & "' ORDER BY KodKursus ASC"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlKodKursus.DataSource = ds
            ddlKodKursus.DataTextField = "KodKursus"
            ddlKodKursus.DataValueField = "KursusID"
            ddlKodKursus.DataBind()

            '--ALL
            ddlKodKursus.Items.Add(New ListItem("-Pilih-", ""))

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub kpmkv_kod_list()
        strSQL = "SELECT Distinct KodMPVok FROM kpmkv_matapelajaran_v ORDER BY KodMPVok"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlKodMataPelajaran.DataSource = ds
            ddlKodMataPelajaran.DataTextField = "KodMPVok"
            ddlKodMataPelajaran.DataValueField = "KodMPVok"
            ddlKodMataPelajaran.DataBind()

            '--ALL
            ddlKodMataPelajaran.Items.Add(New ListItem("-Pilih-", ""))

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
        Dim strOrder As String = " ORDER BY v.Tahun DESC"


        tmpSQL = "SELECT * FROM kpmkv_matapelajaran_v as v "
        tmpSQL += " LEFT JOIN kpmkv_kursus as k on v.KursusID =k.KursusID "
        strWhere += " WHERE v.IsDeleted ='N'"

        '--Tahun
        If Not ddlTahun.Text = "" Then
            strWhere += " AND v.Tahun ='" & ddlTahun.Text & "'"
        End If

        If Not ddlSemester.SelectedValue = "" Then
            strWhere += " AND v.semester ='" & ddlSemester.SelectedValue & "' "
        End If

        If Not ddlKodKursus.SelectedValue = "" Then
            strWhere += " AND v.kursusID ='" & ddlKodKursus.SelectedValue & "' "
        End If

        '--Kod
        If Not ddlKodMataPelajaran.Text = "" Then
            strWhere += " AND v.KodMPVok ='" & ddlKodMataPelajaran.Text & "'"
        End If

        '--Nama
        If Not txtNamaMataPelajaran.Text.Length = 0 Then
            strWhere += " AND v.NamaMPVok LIKE '%" & oCommon.FixSingleQuotes(txtNamaMataPelajaran.Text) & "%'"
        End If

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
    Private Sub datRespondent_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles datRespondent.PageIndexChanging
        datRespondent.PageIndex = e.NewPageIndex
        strRet = BindData(datRespondent)

    End Sub
    Private Sub datRespondent_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles datRespondent.RowDeleting

        lblMsg.Text = ""
        lblMsgTop.Text = ""

        Dim strMataPelajaranID As Integer = datRespondent.DataKeys(e.RowIndex).Values("MPVID")
        Try
            If Not strMataPelajaranID = Session("MPVID") Then
                strSQL = "DELETE FROM kpmkv_matapelajaran_v WHERE MPVID='" & strMataPelajaranID & "'"
                If strRet = oCommon.ExecuteSQL(strSQL) = 0 Then
                    divMsgTop.Attributes("class") = "error"
                    lblMsgTop.Text = "Matapelajaran berjaya dipadamkan"
                    divMsg.Attributes("class") = "error"
                    lblMsg.Text = "KeMatapelajaran  berjaya dipadamkan"
                    Session("MPVID") = ""
                Else
                    divMsgTop.Attributes("class") = "error"
                    lblMsgTop.Text = "Matapelajaran tidak berjaya dipadamkan"
                    divMsg.Attributes("class") = "error"
                    lblMsg.Text = "Matapelajaran tidak berjaya dipadamkan"
                    Session("MPVID") = ""
                End If

            Else
                divMsgTop.Attributes("class") = "error"
                lblMsgTop.Text = "Matapelajaran tidak berjaya dipadamkan"
                divMsg.Attributes("class") = "error"
                lblMsg.Text = "Matapelajaran tidak berjaya dipadamkan"
                Session("MPVID") = ""
            End If

        Catch ex As Exception
            divMsg.Attributes("class") = "error"
        End Try

        strRet = BindData(datRespondent)
    End Sub

    Private Sub datRespondent_SelectedIndexChanging(sender As Object, e As GridViewSelectEventArgs) Handles datRespondent.SelectedIndexChanging
        Dim strKeyID As String = datRespondent.DataKeys(e.NewSelectedIndex).Value.ToString
        Response.Redirect("mp.vok.view.aspx?MataPelajaranID=" & strKeyID)
    End Sub

    Protected Sub chkSesi_SelectedIndexChanged(sender As Object, e As EventArgs) Handles chkSesi.SelectedIndexChanged
        kpmkv_kodkursus_list()
        ddlKodKursus.Text = ""
    End Sub
End Class