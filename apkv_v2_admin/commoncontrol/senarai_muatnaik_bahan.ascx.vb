Imports System.Data.SqlClient
Public Class senarai_muatnaik_bahan1
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

                'kolejnama
                strSQL = "SELECT Nama FROM kpmkv_users WHERE LoginID='" & Session("LoginID") & "'"
                Dim strKolejnama As String = oCommon.getFieldValue(strSQL)
                'kolejid
                strSQL = "SELECT RecordID FROM kpmkv_kolej WHERE Nama='" & strKolejnama & "'"
                lblKolejID.Text = oCommon.getFieldValue(strSQL)



                kpmkv_kategori_list()
                ddlKategory.SelectedValue = ""


                kpmkv_kohort_list()
                ddlKohort.Text = Now.Year

                kpmkv_semester_list()
                ddlSemester.SelectedValue = ""

                kpmkv_kodkursus_list()
                ddlKodkursus.Text = ""

                ddlKodkursus.Enabled = False

                btnSave.Visible = False
                lblSta.Visible = False
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

            ddlKodkursus.DataSource = ds
            ddlKodkursus.DataTextField = "KodKursus"
            ddlKodkursus.DataValueField = "KursusID"
            ddlKodkursus.DataBind()

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
            kpmkv_kodkursus_list()
            ddlKodkursus.SelectedValue = ""
        ElseIf ddlKomponen.SelectedValue = "SOCIAL" Then
            ddlKodkursus.Enabled = False
            ddlKodkursus.SelectedValue = ""
        Else
            ddlKodkursus.Enabled = False
            ddlKodkursus.SelectedValue = ""
        End If

        strRet = BindData(datRespondent)
    End Sub

    Private Sub ddlkodkursus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlKodkursus.SelectedIndexChanged
        strRet = BindData(datRespondent)
    End Sub

    Protected Sub CheckUncheckAll(sender As Object, e As System.EventArgs)
        Dim chk1 As CheckBox
        chk1 = DirectCast(datRespondent.HeaderRow.Cells(0).FindControl("chkAll"), CheckBox)
        For Each row As GridViewRow In datRespondent.Rows
            Dim chk As CheckBox
            chk = DirectCast(row.Cells(0).FindControl("chkSelect"), CheckBox)
            chk.Checked = chk1.Checked
        Next
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

                btnSave.Visible = False
                lblSta.Visible = False
            Else
                divMsg.Attributes("class") = "info"
                lblMsg.Text = "Jumlah Rekod#:" & myDataSet.Tables(0).Rows.Count

                btnSave.Visible = True
                lblSta.Visible = True
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
        strSQL += " bahan.STarikh ,bahan.ETarikh, "
        strSQL += " bahan.Komen,bahan.isVerified"

        strSQL += " FROM kpmkv_bahan AS bahan"
        strSQL += " LEFT JOIN tbl_Settings AS setting ON bahan.kategori=setting.ID"
        strSQL += " LEFT JOIN kpmkv_kursus AS kursus ON bahan.KursusID=kursus.KursusID"
        strSQL += " WHERE bahan.Kohort='" & ddlKohort.SelectedValue & "'"

        If chkstatus.Checked = False Then
            strSQL += " AND bahan.NeedVerify='Y'"
        End If

        If Not ddlKategory.SelectedValue = "" Then
            strSQL += " AND bahan.kategori='" & ddlKategory.SelectedValue & "'"

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

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        lblMsg.Text = ""
        strRet = BindData(datRespondent)
    End Sub


    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSave.Click
        Try
            For i As Integer = 0 To datRespondent.Rows.Count - 1

                Dim cb As CheckBox = datRespondent.Rows(i).FindControl("chkSelect")
                Dim comment As TextBox = datRespondent.Rows(i).FindControl("Komen")

                Dim strkey As String = datRespondent.DataKeys(i).Value.ToString

                Dim strcb As String = ""
                Dim strVer As String = ""
                If cb.Checked = True Then
                    strcb = "Y"
                    strVer = "N"
                Else
                    strcb = "N"
                    strVer = "Y"
                End If


                strSQL = " UPDATE kpmkv_bahan SET "
                strSQL += " isVerified ='" & strcb & "', Komen='" & comment.Text & "', NeedVerify='" & strVer & "'"
                strSQL += " WHERE ID='" & strkey & "'"
                strRet = oCommon.ExecuteSQL(strSQL)

                If Not strRet = 0 Then
                    divMsg.Attributes("class") = "error"
                    lblMsg.Text = "Rekod Bahan tidak berjaya dikemaskini"
                End If

            Next

            strRet = BindData(datRespondent)
            divMsg.Attributes("class") = "info"
            lblMsg.Text = "Rekod Bahan berjaya dikemaskini"
        Catch ex As Exception
            divMsg.Attributes("class") = "error"
            lblMsg.Text = "System Error. " & ex.Message
        End Try


    End Sub
End Class