Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Drawing

Public Class admin_gred
    Inherits System.Web.UI.UserControl
    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                lblMsgPBA.Text = ""
                lblMsgPBV.Text = ""
                lblMsgPAA.Text = ""
                lblMsgPAV.Text = ""

                kpmkv_tahun_list()
                ddlTahun.Text = Now.Year

                kpmkv_negeri_list()
                If Not Session("Negeri") = "" Then
                    ddlNegeri.Text = Session("Negeri")
                Else
                    ddlNegeri.Text = "0"
                End If

                kpmkv_kolej_list()

                kpmkv_semester_list()

            End If

        Catch ex As Exception

            lblMsgPBA.Text = "Error Message:" & ex.Message
            lblMsgPBV.Text = "Error Message:" & ex.Message
            lblMsgPAA.Text = "Error Message:" & ex.Message
            lblMsgPAV.Text = "Error Message:" & ex.Message

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

    Private Sub kpmkv_negeri_list()

        If Not Session("Negeri") = "" Then
            strSQL = "SELECT Negeri FROM kpmkv_negeri  Where Negeri='" & Session("Negeri") & "'"
        Else
            strSQL = "SELECT Negeri FROM kpmkv_negeri ORDER BY Negeri"
        End If

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try

            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlNegeri.DataSource = ds
            ddlNegeri.DataTextField = "Negeri"
            ddlNegeri.DataValueField = "Negeri"
            ddlNegeri.DataBind()

            ''--ALL
            If Session("Negeri") = "" Then
                ddlNegeri.Items.Insert(0, "-Pilih-")
            End If



        Catch ex As Exception

        End Try

    End Sub

    Private Sub kpmkv_kolej_list()

        strSQL = "SELECT RecordID, Nama FROM kpmkv_kolej WHERE Negeri = '" & ddlNegeri.SelectedValue & "' ORDER BY Nama"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try

            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlKolej.DataSource = ds
            ddlKolej.DataTextField = "Nama"
            ddlKolej.DataValueField = "RecordID"
            ddlKolej.DataBind()

            ddlKolej.Items.Insert(0, "-Pilih-")

        Catch ex As Exception

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

            ddlSemester.Items.Insert(0, "-Pilih-")

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try

    End Sub
    Private Function BindData(ByVal gvTable As GridView, ByVal SQL As String) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(SQL, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 120

        Try
            myDataAdapter.Fill(myDataSet, "myaccount")


            gvTable.DataSource = myDataSet
            gvTable.DataBind()
            objConn.Close()
        Catch ex As Exception
            Return False
        End Try

        Return True

    End Function

    Private Function getSQLSahPBA() As String

        Dim tmpSQL As String

        tmpSQL = "  SELECT KPM.KolejRecordID, KO.Nama, KU.KodKursus, KU.NamaKursus, KPM.Tahun, KPM.Sesi, KPM.Semester,
                    COUNT(CASE WHEN KPM.isSahPBA <> '1' THEN 1 ELSE NULL END ) AS sahPBA,                    
                    COUNT(*) AS totalStudent
                    FROM kpmkv_pelajar_markah KPM
                    LEFT JOIN kpmkv_kolej KO ON KO.RecordID = KPM.KolejRecordID
                    LEFT JOIN kpmkv_kursus KU ON KU.KursusID = KPM.KursusID
                    LEFT JOIN kpmkv_pelajar PE ON PE.PelajarID = KPM.PelajarID
                    WHERE 
                    PE.StatusID ='2' 
                    AND PE.JenisCalonID ='2'
                    AND PE.KelasID IS NOT NULL                    
                    AND KPM.Tahun = '" & ddlTahun.Text & "'                     
                    AND KPM.Sesi = '" & chkSesi.Text & "'"

        If Not ddlNegeri.SelectedItem.Text = "-Pilih-" Then

            tmpSQL += " AND KO.Negeri = '" & ddlNegeri.SelectedValue & "'"

        End If

        If Not ddlKolej.SelectedItem.Text = "-Pilih-" Then

            tmpSQL += " AND KPM.KolejRecordID = '" & ddlKolej.SelectedValue & "'"

        End If

        If Not ddlSemester.SelectedItem.Text = "-Pilih-" Then

            tmpSQL += " AND KPM.Semester = '" & ddlSemester.Text & "'"

        End If

        tmpSQL += " GROUP BY KPM.KolejRecordID, KO.Nama, KU.KodKursus, KU.NamaKursus, KPM.Tahun, KPM.Sesi, KPM.Semester
                    HAVING COUNT(CASE WHEN KPM.isSahPBA = '1' THEN 1 ELSE NULL END ) < COUNT(*)
                    ORDER BY KO.Nama"

        getSQLSahPBA = tmpSQL

        Debug.WriteLine(tmpSQL)

        Return getSQLSahPBA

    End Function


    Private Function getSQLSahPBV() As String

        Dim tmpSQL As String

        tmpSQL = "  SELECT KPM.KolejRecordID, KO.Nama, KU.KodKursus, KU.NamaKursus, KPM.Tahun, KPM.Sesi, KPM.Semester,
                    COUNT(CASE WHEN KPM.isSahPBV <> '1' THEN 1 ELSE NULL END ) AS sahPBV,                    
                    COUNT(*) AS totalStudent
                    FROM kpmkv_pelajar_markah KPM
                    LEFT JOIN kpmkv_kolej KO ON KO.RecordID = KPM.KolejRecordID
                    LEFT JOIN kpmkv_kursus KU ON KU.KursusID = KPM.KursusID
                    LEFT JOIN kpmkv_pelajar PE ON PE.PelajarID = KPM.PelajarID
                    WHERE 
                    PE.StatusID ='2' 
                    AND PE.JenisCalonID ='2'
                    AND PE.KelasID IS NOT NULL                    
                    AND KPM.Tahun = '" & ddlTahun.Text & "'                     
                    AND KPM.Sesi = '" & chkSesi.Text & "'"

        If Not ddlNegeri.SelectedItem.Text = "-Pilih-" Then

            tmpSQL += " AND KO.Negeri = '" & ddlNegeri.SelectedValue & "'"

        End If

        If Not ddlKolej.SelectedItem.Text = "-Pilih-" Then

            tmpSQL += " AND KPM.KolejRecordID = '" & ddlKolej.SelectedValue & "'"

        End If

        If Not ddlSemester.SelectedItem.Text = "-Pilih-" Then

            tmpSQL += " AND KPM.Semester = '" & ddlSemester.Text & "'"

        End If

        tmpSQL += " GROUP BY KPM.KolejRecordID, KO.Nama, KU.KodKursus, KU.NamaKursus, KPM.Tahun, KPM.Sesi, KPM.Semester
                    HAVING COUNT(CASE WHEN KPM.isSahPBV = '1' THEN 1 ELSE NULL END ) < COUNT(*)
                    ORDER BY KO.Nama"

        getSQLSahPBV = tmpSQL

        Debug.WriteLine(tmpSQL)

        Return getSQLSahPBV

    End Function

    Private Function getSQLSahPAA() As String

        Dim tmpSQL As String

        tmpSQL = "  SELECT KPM.KolejRecordID, KO.Nama, KU.KodKursus, KU.NamaKursus, KPM.Tahun, KPM.Sesi, KPM.Semester,
                    COUNT(CASE WHEN KPM.isSahPAA <> '1' THEN 1 ELSE NULL END ) AS sahPAA,                    
                    COUNT(*) AS totalStudent
                    FROM kpmkv_pelajar_markah KPM
                    LEFT JOIN kpmkv_kolej KO ON KO.RecordID = KPM.KolejRecordID
                    LEFT JOIN kpmkv_kursus KU ON KU.KursusID = KPM.KursusID
                    LEFT JOIN kpmkv_pelajar PE ON PE.PelajarID = KPM.PelajarID
                    WHERE 
                    PE.StatusID ='2' 
                    AND PE.JenisCalonID ='2'
                    AND PE.KelasID IS NOT NULL                    
                    AND KPM.Tahun = '" & ddlTahun.Text & "'                     
                    AND KPM.Sesi = '" & chkSesi.Text & "'"

        If Not ddlNegeri.SelectedItem.Text = "-Pilih-" Then

            tmpSQL += " AND KO.Negeri = '" & ddlNegeri.SelectedValue & "'"

        End If

        If Not ddlKolej.SelectedItem.Text = "-Pilih-" Then

            tmpSQL += " AND KPM.KolejRecordID = '" & ddlKolej.SelectedValue & "'"

        End If

        If Not ddlSemester.SelectedItem.Text = "-Pilih-" Then

            tmpSQL += " AND KPM.Semester = '" & ddlSemester.Text & "'"

        End If

        tmpSQL += " GROUP BY KPM.KolejRecordID, KO.Nama, KU.KodKursus, KU.NamaKursus, KPM.Tahun, KPM.Sesi, KPM.Semester
                    HAVING COUNT(CASE WHEN KPM.isSahPAA = '1' THEN 1 ELSE NULL END ) < COUNT(*)
                    ORDER BY KO.Nama"

        getSQLSahPAA = tmpSQL

        Debug.WriteLine(tmpSQL)

        Return getSQLSahPAA

    End Function

    Private Function getSQLSahPAV() As String

        Dim tmpSQL As String

        tmpSQL = "  SELECT KPM.KolejRecordID, KO.Nama, KU.KodKursus, KU.NamaKursus, KPM.Tahun, KPM.Sesi, KPM.Semester,
                    COUNT(CASE WHEN KPM.isSahPAV <> '1' THEN 1 ELSE NULL END ) AS sahPAV,                    
                    COUNT(*) AS totalStudent
                    FROM kpmkv_pelajar_markah KPM
                    LEFT JOIN kpmkv_kolej KO ON KO.RecordID = KPM.KolejRecordID
                    LEFT JOIN kpmkv_kursus KU ON KU.KursusID = KPM.KursusID
                    LEFT JOIN kpmkv_pelajar PE ON PE.PelajarID = KPM.PelajarID
                    WHERE 
                    PE.StatusID ='2' 
                    AND PE.JenisCalonID ='2'
                    AND PE.KelasID IS NOT NULL                    
                    AND KPM.Tahun = '" & ddlTahun.Text & "'                     
                    AND KPM.Sesi = '" & chkSesi.Text & "'"

        If Not ddlNegeri.SelectedItem.Text = "-Pilih-" Then

            tmpSQL += " AND KO.Negeri = '" & ddlNegeri.SelectedValue & "'"

        End If

        If Not ddlKolej.SelectedItem.Text = "-Pilih-" Then

            tmpSQL += " AND KPM.KolejRecordID = '" & ddlKolej.SelectedValue & "'"

        End If

        If Not ddlSemester.SelectedItem.Text = "-Pilih-" Then

            tmpSQL += " AND KPM.Semester = '" & ddlSemester.Text & "'"

        End If

        tmpSQL += " GROUP BY KPM.KolejRecordID, KO.Nama, KU.KodKursus, KU.NamaKursus, KPM.Tahun, KPM.Sesi, KPM.Semester
                    HAVING COUNT(CASE WHEN KPM.isSahPAV = '1' THEN 1 ELSE NULL END ) < COUNT(*)
                    ORDER BY KO.Nama"

        getSQLSahPAV = tmpSQL

        Debug.WriteLine(tmpSQL)

        Return getSQLSahPAV

    End Function

    Private Sub ExportToCSV(ByVal strQuery As String)
        'Get the data from database into datatable 
        Dim cmd As New SqlCommand(strQuery)
        Dim dt As DataTable = GetData(cmd)

        Response.Clear()
        Response.Buffer = True
        Response.AddHeader("content-disposition", "attachment;filename=PemantauanMarkah.csv")
        Response.Charset = ""
        Response.ContentType = "application/text"


        Dim sb As New StringBuilder()
        For k As Integer = 0 To dt.Columns.Count - 1
            'add separator 
            sb.Append(dt.Columns(k).ColumnName + ","c)
        Next

        'append new line 
        sb.Append(vbCr & vbLf)
        For i As Integer = 0 To dt.Rows.Count - 1
            For k As Integer = 0 To dt.Columns.Count - 1
                '--add separator 
                'sb.Append(dt.Rows(i)(k).ToString().Replace(",", ";") + ","c)

                'cleanup here
                If k <> 0 Then
                    sb.Append(",")
                End If

                Dim columnValue As Object = dt.Rows(i)(k).ToString()
                If columnValue Is Nothing Then
                    sb.Append("")
                Else
                    Dim columnStringValue As String = columnValue.ToString()

                    Dim cleanedColumnValue As String = CleanCSVString(columnStringValue)

                    If columnValue.[GetType]() Is GetType(String) AndAlso Not columnStringValue.Contains(",") Then
                        ' Prevents a number stored in a string from being shown as 8888E+24 in Excel. Example use is the AccountNum field in CI that looks like a number but is really a string.
                        cleanedColumnValue = "=" & cleanedColumnValue
                    End If
                    sb.Append(cleanedColumnValue)
                End If

            Next
            'append new line 
            sb.Append(vbCr & vbLf)
        Next
        Response.Output.Write(sb.ToString())
        Response.Flush()
        Response.End()

    End Sub

    Protected Function CleanCSVString(ByVal input As String) As String
        Dim output As String = """" & input.Replace("""", """""").Replace(vbCr & vbLf, " ").Replace(vbCr, " ").Replace(vbLf, "") & """"
        Return output

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

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        lblMsgPBA.Text = ""
        lblMsgPBV.Text = ""
        lblMsgPAA.Text = ""
        lblMsgPAV.Text = ""

        strRet = BindData(datRespondentPBA, getSQLSahPBA)
        labelMessage(getSQLSahPBA, lblMsgPBA, divMsgPBA)

        strRet = BindData(datRespondentPBV, getSQLSahPBV)
        labelMessage(getSQLSahPBV, lblMsgPBV, divMsgPBV)

        strRet = BindData(datRespondentPAA, getSQLSahPAA)
        labelMessage(getSQLSahPAA, lblMsgPAA, divMsgPAA)

        strRet = BindData(datRespondentPAV, getSQLSahPAV)
        labelMessage(getSQLSahPAV, lblMsgPAV, divMsgPAV)

    End Sub

    Private Sub labelMessage(ByVal sql As String, ByVal lblText As Label, ByVal divText As HtmlGenericControl)

        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(sql, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 120

        myDataAdapter.Fill(myDataSet, "myaccount")

        If myDataSet.Tables(0).Rows.Count = 0 Then
            divText.Attributes("class") = "error"
            lblText.Text = "Tiada rekod pelajar."
        Else
            divText.Attributes("class") = "info"
            lblText.Text = "Jumlah rekod#:" & myDataSet.Tables(0).Rows.Count
        End If

    End Sub

    Private Sub ddlNegeri_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlNegeri.SelectedIndexChanged
        kpmkv_kolej_list()
    End Sub

    Protected Sub btnExportPBA_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnExportPBA.Click
        Try
            lblMsgPBA.Text = ""

            ExportToCSV(getSQLSahPBA)

        Catch ex As Exception
            lblMsgPBA.Text = "System Error:" & ex.Message
        End Try

    End Sub

    Private Sub btnExportPBV_Click(sender As Object, e As EventArgs) Handles btnExportPBV.Click

        Try
            lblMsgPBV.Text = ""

            ExportToCSV(getSQLSahPBV)

        Catch ex As Exception
            lblMsgPBV.Text = "System Error:" & ex.Message
        End Try

    End Sub

    Private Sub btnExportPAA_Click(sender As Object, e As EventArgs) Handles btnExportPAA.Click
        Try
            lblMsgPAA.Text = ""

            ExportToCSV(getSQLSahPAA)

        Catch ex As Exception
            lblMsgPAA.Text = "System Error:" & ex.Message
        End Try
    End Sub

    Private Sub btnExportPAV_Click(sender As Object, e As EventArgs) Handles btnExportPAV.Click
        Try
            lblMsgPAV.Text = ""

            ExportToCSV(getSQLSahPAV)

        Catch ex As Exception
            lblMsgPAV.Text = "System Error:" & ex.Message
        End Try
    End Sub

    Private Sub datRespondentPBA_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles datRespondentPBA.PageIndexChanging
        datRespondentPBA.PageIndex = e.NewPageIndex
        strRet = BindData(datRespondentPBA, getSQLSahPBA)
    End Sub

    Private Sub datRespondentPBV_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles datRespondentPBV.PageIndexChanging
        datRespondentPBV.PageIndex = e.NewPageIndex
        strRet = BindData(datRespondentPBV, getSQLSahPBV)
    End Sub

    Private Sub datRespondentPAA_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles datRespondentPAA.PageIndexChanging
        datRespondentPAA.PageIndex = e.NewPageIndex
        strRet = BindData(datRespondentPAA, getSQLSahPAA)
    End Sub

    Private Sub datRespondentPAV_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles datRespondentPAV.PageIndexChanging
        datRespondentPAV.PageIndex = e.NewPageIndex
        strRet = BindData(datRespondentPAV, getSQLSahPAV)
    End Sub
End Class