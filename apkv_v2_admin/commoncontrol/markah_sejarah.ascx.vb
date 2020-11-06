Imports System.Data.SqlClient
Public Class markah_sejarah1
    Inherits System.Web.UI.UserControl
    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            If Not IsPostBack Then

                strSQL = "SELECT UserID FROM kpmkv_users WHERE LoginID='" & Session("LoginID") & "'"
                lblUserId.Text = oCommon.getFieldValue(strSQL)

                strSQL = "SELECT UserType FROM kpmkv_users WHERE LoginID='" & Session("LoginID") & "'"
                lblUserType.Text = oCommon.getFieldValue(strSQL)

                kpmkv_Kohort()
                ddlTahun.SelectedValue = ""

                kpmkv_kolej_list()

                'kpmkv_kodkursus_list()


                kpmkv_semester_list()
                ddlSemester.SelectedValue = ""

            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message
        End Try
    End Sub
    Private Sub kpmkv_Kohort()

        ddlTahun.Items.Insert(0, Now.Year)
        ddlTahun.SelectedItem.Text = Now.Year

        'If lblUserType.Text = "ADMIN" Then
        '    strSQL = " SELECT Distinct Tahun FROM kpmkv_pemeriksa ORDER BY Tahun DESC"
        'Else
        '    strSQL = "SELECT Distinct Tahun FROM kpmkv_pemeriksa WHERE UserID='" & lblUserId.Text & "'  ORDER BY Tahun DESC"

        'End If

        'Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        'Dim objConn As SqlConnection = New SqlConnection(strConn)
        'Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        'Try
        '    Dim ds As DataSet = New DataSet
        '    sqlDA.Fill(ds, "AnyTable")

        '    ddlTahun.DataSource = ds
        '    ddlTahun.DataTextField = "Tahun"
        '    ddlTahun.DataValueField = "Tahun"
        '    ddlTahun.DataBind()

        '    '--ALL
        '    ddlTahun.Items.Add(New ListItem("-Pilih-", ""))

        'Catch ex As Exception
        '    lblMsg.Text = "System Error:" & ex.Message

        'Finally
        '    objConn.Dispose()
        'End Try

    End Sub

    Private Sub kpmkv_kolej_list()
        If lblUserType.Text = "ADMIN" Then
            strSQL = " SELECT Distinct KodKolej FROM kpmkv_pemeriksa WHERE Tahun='" & Now.Year & "' ORDER By KodKolej"
        Else
            strSQL = " SELECT  Distinct KodKolej FROM kpmkv_pemeriksa WHERE UserID='" & lblUserId.Text & "' AND Tahun='" & ddlTahun.SelectedValue & "'"

        End If

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlKodPusat.DataSource = ds
            ddlKodPusat.DataTextField = "KodKolej"
            ddlKodPusat.DataValueField = "KodKolej"
            ddlKodPusat.DataBind()

            '--ALL
            'ddlKolej.Items.Add(New ListItem("-Pilih-", "0"))

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub kpmkv_semester_list()

        If lblUserType.Text = "ADMIN" Then
            strSQL = " SELECT Distinct Semester FROM kpmkv_pemeriksa "
            strSQL += " ORDER BY Semester ASC"
        Else
            strSQL = "SELECT Distinct Semester FROM kpmkv_pemeriksa WHERE UserID='" & lblUserId.Text & "' AND Tahun='" & ddlTahun.SelectedValue & "'"
            strSQL += " ORDER BY Semester ASC"
        End If

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
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub

    'Private Sub kpmkv_kodkursus_list()

    '    strSQL = "SELECT RecordID FROM kpmkv_kolej WHERE Kod = '" & ddlKodPusat.SelectedValue & "'"
    '    Dim RecordID As String = oCommon.getFieldValue(strSQL)

    '    If lblUserType.Text = "ADMIN" Then
    '        strSQL = " SELECT DISTINCT kpmkv_kursus.KodKursus, kpmkv_markah_bmsj_setara.KodKursus  AS KursusID FROM kpmkv_markah_bmsj_setara
    '                   LEFT JOIN kpmkv_kursus ON kpmkv_kursus.KursusID = kpmkv_markah_bmsj_setara.KodKursus"
    '        strSQL += " ORDER BY KodKursus ASC"
    '    Else
    '        strSQL = " SELECT DISTINCT kpmkv_kursus.KodKursus, kpmkv_markah_bmsj_setara.KodKursus AS KursusID FROM kpmkv_markah_bmsj_setara
    '                   LEFT JOIN kpmkv_kursus ON kpmkv_kursus.KursusID = kpmkv_markah_bmsj_setara.KodKursus 
    '                   WHERE KolejRecordID = '" & RecordID & "' AND IsAKATahun = '" & ddlTahun.SelectedValue & "'"
    '        strSQL += " ORDER BY KodKursus ASC"
    '    End If

    '    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    '    Dim objConn As SqlConnection = New SqlConnection(strConn)
    '    Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

    '    Try
    '        Dim ds As DataSet = New DataSet
    '        sqlDA.Fill(ds, "AnyTable")

    '        ddlKodKursus.DataSource = ds
    '        ddlKodKursus.DataTextField = "KodKursus"
    '        ddlKodKursus.DataValueField = "KursusID"
    '        ddlKodKursus.DataBind()

    '    Catch ex As Exception
    '        lblMsg.Text = "System Error:" & ex.Message

    '    Finally
    '        objConn.Dispose()
    '    End Try

    'End Sub

    Private Sub datRespondent_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles datRespondent.PageIndexChanging
        datRespondent.PageIndex = e.NewPageIndex
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
                lblMsg.Text = "Tiada rekod pemeriksa."
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

        Dim tmpSQL As String = ""
        Dim strWhere As String = ""
        Dim strOrder As String = " ORDER BY kpmkv_pemeriksa.Tahun, kpmkv_pemeriksa.Semester, kpmkv_pemeriksa.Sesi ASC"

        If lblUserType.Text = "ADMIN" Then
            tmpSQL = " SELECT PemeriksaID,Tahun,Semester,Sesi,KodKolej,KodKursus FROM kpmkv_pemeriksa "
            strWhere = " WHERE KodKolej='" & ddlKodPusat.Text & "'"
            strWhere += " AND Sesi='" & chkSesi.Text & "' AND Tahun='" & Now.Year & "' AND Semester='" & ddlSemester.Text & "'"
            ''--debug
            ''Response.Write(getSQL)
        Else
            tmpSQL = " SELECT PemeriksaID,Tahun,Semester,Sesi,KodKolej,KodKursus FROM  kpmkv_pemeriksa "
            strWhere = " WHERE KodKolej='" & ddlKodPusat.Text & "' AND kpmkv_pemeriksa.UserID='" & lblUserId.Text & "'"
            strWhere += " AND Sesi='" & chkSesi.Text & "' AND Tahun='" & ddlTahun.SelectedValue & "' AND Semester='" & ddlSemester.Text & "'"

        End If
        getSQL = tmpSQL & strWhere & strOrder
        Return getSQL

    End Function

    Protected Sub datRespondent_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles datRespondent.RowCommand
        lblMsg.Text = ""
        If (e.CommandName = "Pilih") = True Then


            Dim strkeyID = Int32.Parse(e.CommandArgument.ToString())

            Response.Redirect("markah.PA.sejarah.aspx?PemeriksaID=" & strkeyID)
        End If
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

    Protected Sub btnCari_Click(sender As Object, e As EventArgs) Handles btnCari.Click
        lblMsg.Text = ""
        strRet = BindData(datRespondent)
    End Sub

    Private Sub ddlTahun_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTahun.SelectedIndexChanged
        kpmkv_kolej_list()
        kpmkv_semester_list()
        'kpmkv_kodkursus_list()

    End Sub

    Private Sub ddlKodPusat_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlKodPusat.SelectedIndexChanged
        'kpmkv_kodkursus_list()

    End Sub
End Class