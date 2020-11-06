Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization
Public Class muatnaik_bahan
    Inherits System.Web.UI.UserControl
    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim fileSavePath As String = ConfigurationManager.AppSettings("FolderPath")
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            If Not IsPostBack Then

                lblAction.Text = Request.QueryString("Action")

                Session("strTypeid") = ""

                kpmkv_kategori_list()
                ddlKategory.SelectedValue = ""


                kpmkv_kohort_list()
                ddlKohort.Text = Now.Year

                kpmkv_semester_list()
                ddlSemester.SelectedValue = ""

                kpmkv_kodkursus_list()
                ddlKodkursus.Text = ""


                ddlKodkursus.Enabled = False
                ddlJenisKursus.Enabled = False

                strRet = BindData(datRespondent)

            End If

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message
        End Try
    End Sub

    Private Sub kpmkv_kategori_list()
        strSQL = "SELECT ID,Parameter FROM tbl_Settings WHERE type='KATEGORIMUATNAIKBAHAN'  ORDER BY idx ASC"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlKategory.DataSource = ds
            ddlKategory.DataTextField = "Parameter"
            ddlKategory.DataValueField = "ID"
            ddlKategory.DataBind()


            ddlKategory.Items.Add(New ListItem("-Pilih-", ""))

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try
    End Sub

    Private Sub kpmkv_kohort_list()
        strSQL = "SELECT Tahun FROM kpmkv_tahun  ORDER BY TahunID"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlKohort.DataSource = ds
            ddlKohort.DataTextField = "Tahun"
            ddlKohort.DataValueField = "Tahun"
            ddlKohort.DataBind()

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

            ddlSemester.Items.Add(New ListItem("-Pilih-", ""))

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub kpmkv_kodkursus_list()

        strSQL = "SELECT kpmkv_kursus.KodKursus, kpmkv_kursus.KursusID "
        strSQL += " FROM kpmkv_kursus_kolej LEFT OUTER JOIN"
        strSQL += " kpmkv_kursus ON kpmkv_kursus_kolej.KursusID = kpmkv_kursus.KursusID"
        strSQL += " WHERE kpmkv_kursus.Tahun='" & ddlKohort.SelectedValue & "' "
        strSQL += " And kpmkv_kursus.Sesi ='" & ddlSesi.SelectedValue & "'"
        strSQL += " GROUP BY kpmkv_kursus.KodKursus, kpmkv_kursus.KursusID"
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
            ddlKodkursus.Items.Add(New ListItem("-KodKursus-", ""))

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub ddlKomponen_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlKomponen.SelectedIndexChanged
        If ddlKomponen.SelectedValue = "VOKASIONAL" Then
            ddlKodkursus.Enabled = True
            ddlJenisKursus.Enabled = False
            kpmkv_kodkursus_list()
            ddlKodkursus.SelectedValue = ""
        ElseIf ddlKomponen.SelectedValue = "AKADEMIK" Then
            ddlKodkursus.Enabled = False
            ddlJenisKursus.Enabled = True
            ddlKodkursus.SelectedValue = ""
        Else
            ddlKodkursus.Enabled = False
            ddlJenisKursus.Enabled = False
            ddlKodkursus.SelectedValue = ""
            ddlJenisKursus.SelectedValue = ""
        End If

    End Sub

    Private Sub ddlSemester_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSemester.SelectedIndexChanged
        If ddlKodkursus.Enabled = True Then
            kpmkv_kodkursus_list()
            ddlKodkursus.SelectedValue = ""
        End If
    End Sub

    Private Sub ddlSesi_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSesi.SelectedIndexChanged
        If ddlKodkursus.Enabled = True Then
            kpmkv_kodkursus_list()
            ddlKodkursus.SelectedValue = ""
        End If

    End Sub


    Private Function ValidatePage() As Boolean

        If ddlKategory.SelectedValue = "" Then
            lblMsg.Text = "Sila pilih Kategori!"
            ddlKategory.Focus()
            Return False
        End If
        If ddlKohort.SelectedValue = "" Then
            lblMsg.Text = "Sila pilih Kohort!"
            ddlKohort.Focus()
            Return False
        End If

        If ddlKomponen.SelectedValue = "" Then
            lblMsg.Text = "Sila pilih Komponen!"
            ddlKomponen.Focus()
            Return False
        End If

        If Not ddlKomponen.SelectedValue = "UMUM" Then
            If ddlSemester.SelectedValue = "" Then
                lblMsg.Text = "Sila pilih Semester!"
                ddlSemester.Focus()
                Return False
            End If
        End If

        If Not ddlKomponen.SelectedValue = "UMUM" Then
            If ddlSesi.SelectedValue = "" Then
                lblMsg.Text = "Sila pilih Sesi!"
                ddlKategory.Focus()
                Return False
            End If
        End If

        If (ddlKomponen.SelectedValue = "VOKASIONAL" And ddlKodkursus.SelectedValue = "") Then
            lblMsg.Text = "Sila pilih Kod Kursus!"
            ddlKodkursus.Focus()
            Return False
        End If
        If txttitle.Text = "" Then
            lblMsg.Text = "Sila isi Tajuk!"
            txttitle.Focus()
            Return False
        End If
        If txtDateStart.Text = "" Then
            lblMsg.Text = "Sila isi Tarikh Mula!"
            txtDateStart.Focus()
            Return False
        End If
        If txtDateEnd.Text = "" Then
            lblMsg.Text = "Sila isi Tarikh Tamat!"
            txtDateEnd.Focus()
            Return False
        End If

        Dim fileName = Path.GetFileName(FlUploadpdf.PostedFile.FileName)
        If fileName = String.Empty Then
            lblMsg.Text = "Sila pilih Fail!"
            txtDateEnd.Focus()
             Return False
        End If

        Return True
    End Function

    Protected Sub btnCreate_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnUpload.Click
        lblMsg.Text = ""

        Try
            '--validate
            If ValidatePage() = False Then
                divMsg.Attributes("class") = "error"
                Exit Sub
            End If

            '--execute
            If InsertData() = True Then
                divMsg.Attributes("class") = "info"
                lblMsg.Text = "Muatnaik Bahan Berjaya"

                strRet = BindData(datRespondent)


            Else
                divMsg.Attributes("class") = "error"
                lblMsg.Text = "Muatnaik Bahan Tidak Berjaya"

            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message
        End Try

    End Sub

    Private Function InsertData() As Boolean
        Dim fileName As String = ""
        Dim fileExtension As String = ""
        Dim fullFileName As String = ""

        If FlUploadpdf.PostedFile IsNot Nothing Then
            fileName = Path.GetFileName(FlUploadpdf.PostedFile.FileName)
            fileExtension = Path.GetExtension(FlUploadpdf.PostedFile.FileName)


            'first check if "uploads" folder exist or not, if not create it

            If Not Directory.Exists(fileSavePath) Then
                Directory.CreateDirectory(fileSavePath)
            End If

            'after checking or creating folder it's time to save the file
            Dim rand As Random = New Random()
            Dim randNum = rand.Next(1000)
            fullFileName = oCommon.getRandom + "-" + fileName
            fileSavePath = fileSavePath & "//" & fullFileName
            FlUploadpdf.PostedFile.SaveAs(fileSavePath)
            Dim fileInfo As New FileInfo(fileSavePath)
        End If

        'kalau perlukan pengesahan mesti setkan kepada N sbb Y untuk yg dah disahkan 
        Dim strchk As String = ""
        Dim strver As String = ""
        If chkVerified.Checked = True Then
            strchk = "Y"
            strver = "N"
        Else
            strchk = "N"
            strver = "Y"
        End If

        strSQL = "INSERT INTO kpmkv_bahan(Kategori,Kohort,Semester,Sesi,Komponen,KursusID,Tajuk,Catatan,STarikh,"
        strSQL += "ETarikh,FilePath,JenisKursus,NeedVerify,isVerified)"
        strSQL += " VALUES ('" & ddlKategory.SelectedValue & "','" & ddlKohort.SelectedValue & "',"
        strSQL += " '" & ddlSemester.SelectedValue & "','" & ddlSesi.SelectedValue & "',"
        strSQL += " '" & ddlKomponen.SelectedValue & "','" & ddlKodkursus.SelectedValue & "',"
        strSQL += " '" & txttitle.Text & "','" & txtDesc.Text & "',"
        strSQL += " '" & txtDateStart.Text & "','" & txtDateEnd.Text & "','" & fullFileName & "',"
        strSQL += " '" & ddlJenisKursus.SelectedValue & "','" & strchk & "','" & strver & "'"
        strSQL += " )"
        strRet = oCommon.ExecuteSQL(strSQL)
        If Not strRet = 0 Then
            divMsg.Attributes("class") = "error"
            Return False
        Else
            Return True
        End If

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

    Private Function getSQL() As String
        strSQL = " SELECT bahan.ID,bahan.Kohort ,bahan.Semester ,bahan.Sesi ,bahan.Komponen ,"
        strSQL += " kursus.KodKursus ,bahan.Tajuk ,bahan.Catatan ,bahan.FilePath ,"
        strSQL += " bahan.STarikh ,bahan.ETarikh,bahan.Komen"

        strSQL += " FROM kpmkv_bahan AS bahan"
        strSQL += " LEFT JOIN tbl_Settings AS setting ON bahan.kategori=setting.ID"
        strSQL += " LEFT JOIN kpmkv_kursus AS kursus ON bahan.KursusID=kursus.KursusID"

        If lblAction.Text = "" Then

            If Not ddlKohort.SelectedValue = "" Then
                strSQL += "  WHERE bahan.Kohort='" & ddlKohort.SelectedValue & "'"
            End If

            If Not ddlKategory.SelectedValue = "" Then
                strSQL += "AND bahan.kategori='" & ddlKategory.SelectedValue & "'"
            End If


            If Not ddlKomponen.SelectedValue = "UMUM" Then
                If Not ddlSemester.SelectedValue = "" Then
                    strSQL += " AND bahan.Semester='" & ddlSemester.SelectedValue & "'"
                End If
                If Not ddlSesi.SelectedValue = "" Then
                    strSQL += " AND bahan.Sesi='" & ddlSesi.SelectedValue & "'"
                End If
            End If

            If Not ddlKomponen.SelectedValue = "" Then
                strSQL += " AND bahan.Komponen='" & ddlKomponen.SelectedValue & "'"
            End If

            If Not ddlKodkursus.SelectedValue = "" Then
                strSQL += " AND bahan.KursusID='" & ddlKodkursus.SelectedValue & "'"
            End If

        Else

            strSQL += " WHERE (Komen IS NOT NULL AND Komen <>'')"

        End If

        strSQL += " ORDER BY STarikh ASC"

        getSQL = strSQL

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

    Private Sub datRespondent_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSelectEventArgs) Handles datRespondent.SelectedIndexChanging
        Dim strKeyID As String = datRespondent.DataKeys(e.NewSelectedIndex).Value.ToString
    End Sub


    Private Sub datRespondent_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles datRespondent.RowDeleting
        Dim strTid = datRespondent.DataKeys(e.RowIndex).Values("ID").ToString

        'chk session to prevent postback
        If Not strTid = Session("strTypeid") Then
            strSQL = "SELECT FilePath FROM kpmkv_bahan WHERE ID='" & oCommon.FixSingleQuotes(strTid) & "'"
            Dim fullFileName As String = oCommon.getFieldValue(strSQL)

            strSQL = "DELETE FROM kpmkv_bahan WHERE ID='" & oCommon.FixSingleQuotes(strTid) & "'"
            strRet = oCommon.ExecuteSQL(strSQL)

            fileSavePath = fileSavePath & "//" & fullFileName

            If System.IO.File.Exists(fileSavePath) = True Then

                System.IO.File.Delete(fileSavePath)
                lblMsg.Text = "Rekod telah dipadamkan."

            End If

            Session("strDid") = ""
        End If
        strRet = BindData(datRespondent)
    End Sub

    Protected Sub datRespondent_RowCommand(sender As Object, e As GridViewCommandEventArgs)
        If e.CommandName = "DownloadBahan" Then
            'Determine the RowIndex of the Row whose Button was clicked.
            Dim rowIndex As Integer = Convert.ToInt32(e.CommandArgument)

            'Reference the GridView Row.
            Dim row As GridViewRow = datRespondent.Rows(rowIndex)

            'Fetch value of Name.
            Dim strID As String = TryCast(row.FindControl("lblID"), Label).Text

            strSQL = "SELECT FilePath FROM kpmkv_bahan WHERE ID='" & strID & "'"
            Dim fullFileName As String = oCommon.getFieldValue(strSQL)

            fileSavePath = fileSavePath & "//" & fullFileName


            lblMsg.Text = ""
            Response.ContentType = "Application/octet-stream"
            Response.AppendHeader("Content-Disposition", "attachment; filename=" & fullFileName & "")
            Response.TransmitFile(fileSavePath)
            Response.End()
        End If
    End Sub

    Private Sub Refresh_ServerClick(sender As Object, e As EventArgs) Handles Refresh.ServerClick
        Response.Redirect("muatnaik.bahan.aspx")
    End Sub

    Protected Sub datRespondent_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then

            If Not (Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Komen")) = "") Then

                e.Row.ForeColor = System.Drawing.Color.Red
            End If
        End If
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        lblAction.Text = ""
        lblMsg.Text = ""
        strRet = BindData(datRespondent)


    End Sub





End Class