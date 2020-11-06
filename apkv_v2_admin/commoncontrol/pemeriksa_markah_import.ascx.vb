Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Globalization
Public Class pemeriksa_markah_import
    Inherits System.Web.UI.UserControl
    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String

    Dim strTahun As String = ""
    Dim strSemester As String = ""
    Dim strSesi As String = ""
    Dim strKodKursus As String = ""
    Dim strNama As String = ""
    Dim strMyKad As String = ""

    Dim strAmali As String = ""
    Dim strCatatan2 As String = ""
    Dim strPelajarID As String = ""
    Dim strKursusID As String = ""
    Dim IntTakwim As Integer = 0

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            lblMsg.Text = ""
            kpmkv_tahun_list()
            kpmkv_semester_list()

            btnFile.Enabled = True
            btnUpload.Enabled = True
        End If
    End Sub
    Private Sub kpmkv_tahun_list()
        strSQL = "SELECT Tahun FROM kpmkv_tahun  ORDER BY TahunID"
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
            lblMsg.Text = "System Error:" & ex.Message

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

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub
    Private Sub kpmkv_kodkursus_list()

        strSQL = "SELECT kpmkv_kursus.KodKursus, kpmkv_kursus.KursusID"
        strSQL += " FROM kpmkv_kelas_kursus INNER JOIN kpmkv_kursus ON kpmkv_kelas_kursus.KursusID = kpmkv_kursus.KursusID INNER JOIN"
        strSQL += " kpmkv_kelas ON kpmkv_kelas_kursus.KelasID = kpmkv_kelas.KelasID"
        strSQL += " WHERE kpmkv_kursus.Tahun='" & ddlTahun.Text & "' AND kpmkv_kursus.Sesi='" & chkSesi.SelectedValue & "' GROUP BY kpmkv_kursus.KodKursus, kpmkv_kursus.KursusID"
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

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Function BindData(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 120
        Try
            myDataAdapter.Fill(myDataSet, "myaccount")

            If myDataSet.Tables(0).Rows.Count = 0 Then
                divMsg.Attributes("class") = "error"
                lblMsg.Text = "Tiada rekod pelajar."
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

        Dim strModul As String = ""
        Dim strModul2 As String = ""
        Dim strModul3 As String = ""
        Dim strModul4 As String = ""
        Dim tmpSQL As String = ""
        Dim strWhere As String = ""
        Dim strOrder As String = " ORDER BY kpmkv_pelajar.Nama ASC"

        tmpSQL = "SELECT kpmkv_pelajar.Nama,kpmkv_pelajar.MYKAD, kpmkv_kursus.KodKursus AS [KodProgram],"
        tmpSQL += " kpmkv_pelajar_markah.A_Teori1 AS Teori,kpmkv_pelajar_markah.CatatanPA AS Catatan"
        tmpSQL += " FROM kpmkv_pelajar_markah LEFT OUTER JOIN kpmkv_pelajar ON kpmkv_pelajar_markah.PelajarID = kpmkv_pelajar.PelajarID"
        tmpSQL += " LEFT OUTER Join kpmkv_kursus ON kpmkv_pelajar.KursusID = kpmkv_kursus.KursusID"
        strWhere = " WHERE kpmkv_pelajar.IsDeleted='N' AND kpmkv_pelajar.StatusID='2' AND kpmkv_pelajar.JenisCalonID='2'"

        '--tahun
        If Not ddlTahun.Text = "PILIH" Then
            strWhere += " AND kpmkv_pelajar.Tahun ='" & ddlTahun.Text & "'"
        End If
        '--sesi
        If Not chkSesi.Text = "" Then
            strWhere += " AND kpmkv_pelajar.Sesi ='" & chkSesi.Text & "'"
        End If
        '--semester
        If Not ddlSemester.Text = "" Then
            strWhere += " AND kpmkv_pelajar.Semester ='" & ddlSemester.Text & "'"
        End If
        '--Kod
        If Not ddlKodKursus.Text = "" Then
            strWhere += " AND kpmkv_pelajar.KursusID='" & ddlKodKursus.SelectedValue & "'"
        End If
       
        getSQL = tmpSQL & strWhere & strOrder
        ''--debug
        ' Response.Write(getSQL)

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
    Private Sub btnFile_Click(sender As Object, e As EventArgs) Handles btnFile.Click

        lblMsg.Text = ""
        ExportToCSV(getSQL)
        'Response.ContentType = "Application/xlsx"
        'Response.AppendHeader("Content-Disposition", "attachment; filename=IMPORTMARKAHSKORMATAPELAJARAN.xlsx")
        'Response.TransmitFile(Server.MapPath("~/sample_data/IMPORTMARKAHSKORMATAPELAJARAN.xlsx"))
        'Response.End()

    End Sub
    Private Sub ExportToCSV(ByVal strQuery As String)
        'Get the data from database into datatable 
        Dim cmd As New SqlCommand(strQuery)
        Dim dt As DataTable = GetData(cmd)

        Response.Clear()
        Response.Buffer = True
        Response.AddHeader("content-disposition", "attachment;filename=markahskormatapelajaran.csv")
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
    Private Sub btnUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpload.Click
        lblMsg.Text = ""
        Try
            '--upload excel
            If ImportExcel() = True Then
                divMsg.Attributes("class") = "info"

            Else

            End If
        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        End Try

    End Sub

    Private Function ImportExcel() As Boolean
        Dim path As String = String.Concat(Server.MapPath("~/inbox/"))

        If FlUploadcsv.HasFile Then
            Dim rand As Random = New Random()
            Dim randNum = rand.Next(1000)
            Dim fullFileName As String = path + oCommon.getRandom + "-" + FlUploadcsv.FileName
            FlUploadcsv.PostedFile.SaveAs(fullFileName)

            '--required ms access engine
            Dim excelConnectionString As String = ("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & fullFileName & ";Extended Properties=Excel 12.0;")
            'Dim excelConnectionString As String = ("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & fullFileName & ";Extended Properties=Excel 8.0;HDR=YES;")
            'Response.Write("excelConnectionString:" & excelConnectionString)

            Dim connection As OleDbConnection = New OleDbConnection(excelConnectionString)
            Dim command As OleDbCommand = New OleDbCommand("SELECT * FROM [markahskormatapelajaran$]", connection)
            Dim da As OleDbDataAdapter = New OleDbDataAdapter(command)
            Dim ds As DataSet = New DataSet

            Try
                connection.Open()
                da.Fill(ds)
                Dim validationMessage As String = ValidateSiteData(ds)
                If validationMessage = "" Then
                    SaveSiteData(ds)

                Else
                    'lblMsgTop.Text = "Muatnaik GAGAL!. Lihat mesej dibawah."
                    divMsg.Attributes("class") = "error"
                    lblMsg.Text = "Kesalahan Kemasukkan Maklumat Markah Calon:<br />" & validationMessage
                    Return False
                End If

                da.Dispose()
                connection.Close()
                command.Dispose()

            Catch ex As Exception
                lblMsg.Text = "System Error:" & ex.Message
                Return False
            Finally
                If connection.State = ConnectionState.Open Then
                    connection.Close()
                End If
            End Try

        Else
            divMsg.Attributes("class") = "error"
            lblMsg.Text = "Please select file to upload!"
            Return False
        End If

        Return True

    End Function
    Public Function FileIsLocked(ByVal strFullFileName As String) As Boolean
        Dim blnReturn As Boolean = False
        Dim fs As System.IO.FileStream

        Try
            fs = System.IO.File.Open(strFullFileName, IO.FileMode.OpenOrCreate, IO.FileAccess.Read, IO.FileShare.None)
            fs.Close()
        Catch ex As System.IO.IOException
            divMsg.Attributes("class") = "error"
            lblMsg.Text = "Error Message FileIsLocked:" & ex.Message
            blnReturn = True
        End Try

        Return blnReturn
    End Function

    Protected Function ValidateSiteData(ByVal SiteData As DataSet) As String
        Try
            'Loop through DataSet and validate data
            'If data is bad, bail out, otherwise continue on with the bulk copy
            Dim strMsg As String = ""
            Dim sb As StringBuilder = New StringBuilder()
            For i As Integer = 0 To SiteData.Tables(0).Rows.Count - 1

                refreshVar()
                strMsg = ""

                'bil
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("Nama")) Then
                    strNama = SiteData.Tables(0).Rows(i).Item("Nama")
                Else
                    strMsg += "Sila isi Nama|"
                End If

                'Tahun
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("MYKAD")) Then
                    strMyKad = SiteData.Tables(0).Rows(i).Item("MYKAD")
                Else
                    strMsg += "Sila isi Mykad|"
                End If

                'Kod Kursus
                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("KodProgram")) Then
                    strKodKursus = SiteData.Tables(0).Rows(i).Item("KodProgram")
                Else
                    strMsg += "Sila isi Kod Program|"
                End If



                If strMsg.Length = 0 Then
                    'strMsg = "Record#:" & i.ToString & "OK"
                    'strMsg += "<br/>"
                Else
                    strMsg = " & strMsg"
                    strMsg += "<br/>"
                End If

                sb.Append(strMsg)
                'disp bil

            Next

            Return sb.ToString()
        Catch ex As Exception
            Return ex.Message
        End Try

    End Function
    Private Function SaveSiteData(ByVal SiteData As DataSet) As String
        lblMsg.Text = ""
        strAmali = 0

        Try

            Dim sb As StringBuilder = New StringBuilder()

            '--count no of modul
            Dim nCount As Integer = 0
            strSQL = "SELECT COUNT(kpmkv_modul.KodModul) as CModul "
            strSQL += " FROM kpmkv_modul LEFT OUTER JOIN"
            strSQL += " kpmkv_kursus ON kpmkv_modul.KursusID = kpmkv_kursus.KursusID"
            strSQL += " WHERE kpmkv_modul.Tahun='" & ddlTahun.Text & "'"
            strSQL += " AND kpmkv_modul.Semester='" & ddlSemester.Text & "'"
            strSQL += " AND kpmkv_modul.Sesi='" & chkSesi.Text & "'"
            strSQL += " AND kpmkv_modul.KursusID='" & ddlKodKursus.SelectedValue & "'"
            nCount = oCommon.getFieldValueInt(strSQL)


            '***KursusID
            strSQL = "SELECT KursusID FROM kpmkv_kursus WHERE KodKursus='" & strKodKursus & "' AND Tahun='" & ddlTahun.Text & "' AND Sesi='" & chkSesi.SelectedValue & "'"
            Dim strKursusID As String = oCommon.getFieldValue(strSQL)

            For i As Integer = 0 To SiteData.Tables(0).Rows.Count - 1

                'strNama = SiteData.Tables(0).Rows(i).Item("Nama")
                strKodKursus = SiteData.Tables(0).Rows(i).Item("KodProgram")
                strMyKad = SiteData.Tables(0).Rows(i).Item("MyKad")

                If IsNumeric(SiteData.Tables(0).Rows(i).Item("Teori")) Then
                    strAmali = SiteData.Tables(0).Rows(i).Item("Teori")
                Else
                    strAmali = 0
                End If

                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("Catatan")) Then
                    strCatatan2 = SiteData.Tables(0).Rows(i).Item("Catatan")
                Else
                    strCatatan2 = ""
                End If

                '*****pelajarid
                strSQL = "SELECT PelajarID FROM kpmkv_pelajar WHERE MYKAD='" & strMyKad & "' AND KursusID='" & strKursusID & "' AND Tahun='" & ddlTahun.Text & "' AND Sesi='" & chkSesi.SelectedValue & "' AND Semester='" & ddlSemester.SelectedValue & "'"
                Dim strPelajarID As String = oCommon.getFieldValue(strSQL)
                If Not String.IsNullOrEmpty(strPelajarID) Then

                    If nCount = 2 Then
                        strSQL = "UPDATE kpmkv_pelajar_markah Set A_Teori1='" & oCommon.FixSingleQuotes(strAmali) & "',A_Teori2='" & oCommon.FixSingleQuotes(strAmali) & "',CatatanPA='" & oCommon.FixSingleQuotes(strCatatan2) & "'"
                        strSQL += " WHERE Sesi='" & chkSesi.Text & "' AND Semester='" & ddlSemester.SelectedValue & "' AND Tahun='" & ddlTahun.SelectedValue & "' AND KursusID='" & strKursusID & "' AND PelajarID='" & strPelajarID & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)
                    ElseIf nCount = 3 Then
                        strSQL = "UPDATE kpmkv_pelajar_markah Set A_Teori1='" & oCommon.FixSingleQuotes(strAmali) & "',A_Teori2='" & oCommon.FixSingleQuotes(strAmali) & "',A_Teori3='" & oCommon.FixSingleQuotes(strAmali) & "',CatatanPA='" & oCommon.FixSingleQuotes(strCatatan2) & "'"
                        strSQL += " WHERE Sesi='" & chkSesi.Text & "' AND Semester='" & ddlSemester.SelectedValue & "' AND Tahun='" & ddlTahun.SelectedValue & "' AND KursusID='" & strKursusID & "' AND PelajarID='" & strPelajarID & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)
                    ElseIf nCount = 4 Then
                        strSQL = "UPDATE kpmkv_pelajar_markah Set A_Teori1='" & oCommon.FixSingleQuotes(strAmali) & "',A_Teori2='" & oCommon.FixSingleQuotes(strAmali) & "',A_Teori3='" & oCommon.FixSingleQuotes(strAmali) & "',CatatanPA='" & oCommon.FixSingleQuotes(strCatatan2) & "',"
                        strSQL += " A_Teori4 ='" & oCommon.FixSingleQuotes(strAmali) & "'"
                        strSQL += " WHERE Sesi='" & chkSesi.Text & "' AND Semester='" & ddlSemester.SelectedValue & "' AND Tahun='" & ddlTahun.SelectedValue & "' AND KursusID='" & strKursusID & "' AND PelajarID='" & strPelajarID & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)
                    ElseIf nCount = 5 Then
                        strSQL = "UPDATE kpmkv_pelajar_markah Set A_Teori1='" & oCommon.FixSingleQuotes(strAmali) & "',A_Teori2='" & oCommon.FixSingleQuotes(strAmali) & "',A_Teori3='" & oCommon.FixSingleQuotes(strAmali) & "',"
                        strSQL += " A_Teori4 ='" & oCommon.FixSingleQuotes(strAmali) & "',A_Teori5='" & oCommon.FixSingleQuotes(strAmali) & "',CatatanPA='" & oCommon.FixSingleQuotes(strCatatan2) & "'"
                        strSQL += " WHERE Sesi='" & chkSesi.Text & "' AND Semester='" & ddlSemester.SelectedValue & "' AND Tahun='" & ddlTahun.SelectedValue & "' AND KursusID='" & strKursusID & "' AND PelajarID='" & strPelajarID & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)
                    ElseIf nCount = 6 Then
                        strSQL = "UPDATE kpmkv_pelajar_markah Set A_Teori1='" & oCommon.FixSingleQuotes(strAmali) & "',A_Teori2='" & oCommon.FixSingleQuotes(strAmali) & "',A_Teori3='" & oCommon.FixSingleQuotes(strAmali) & "',"
                        strSQL += " A_Teori4 ='" & oCommon.FixSingleQuotes(strAmali) & "',A_Teori5='" & oCommon.FixSingleQuotes(strAmali) & "',A_Amali6='" & oCommon.FixSingleQuotes(strAmali) & "',CatatanPA='" & oCommon.FixSingleQuotes(strCatatan2) & "'"
                        strSQL += " WHERE Sesi='" & chkSesi.Text & "' AND Semester='" & ddlSemester.SelectedValue & "' AND Tahun='" & ddlTahun.SelectedValue & "' AND KursusID='" & strKursusID & "' AND PelajarID='" & strPelajarID & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)
                    ElseIf nCount = 7 Then
                        strSQL = "UPDATE kpmkv_pelajar_markah Set A_Teori1='" & oCommon.FixSingleQuotes(strAmali) & "',A_Teori2='" & oCommon.FixSingleQuotes(strAmali) & "',A_Teori3='" & oCommon.FixSingleQuotes(strAmali) & "',"
                        strSQL += " A_Teori4 ='" & oCommon.FixSingleQuotes(strAmali) & "',A_Teori5='" & oCommon.FixSingleQuotes(strAmali) & "',A_Teori6='" & oCommon.FixSingleQuotes(strAmali) & "',A_Teori7='" & oCommon.FixSingleQuotes(strAmali) & "',CatatanPA='" & oCommon.FixSingleQuotes(strCatatan2) & "'"
                        strSQL += " WHERE Sesi='" & chkSesi.Text & "' AND Semester='" & ddlSemester.SelectedValue & "' AND Tahun='" & ddlTahun.SelectedValue & "' AND KursusID='" & strKursusID & "' AND PelajarID='" & strPelajarID & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)
                    ElseIf nCount = 8 Then
                        strSQL = "UPDATE kpmkv_pelajar_markah Set A_Teori1='" & oCommon.FixSingleQuotes(strAmali) & "',A_Teori2='" & oCommon.FixSingleQuotes(strAmali) & "',A_Teori3='" & oCommon.FixSingleQuotes(strAmali) & "',"
                        strSQL += " A_Teori4 ='" & oCommon.FixSingleQuotes(strAmali) & "',A_Teori5='" & oCommon.FixSingleQuotes(strAmali) & "',A_Teori6='" & oCommon.FixSingleQuotes(strAmali) & "',A_Teori7='" & oCommon.FixSingleQuotes(strAmali) & "' ,A_Teori8='" & oCommon.FixSingleQuotes(strAmali) & "',CatatanPA='" & oCommon.FixSingleQuotes(strCatatan2) & "'"
                        strSQL += " WHERE Sesi='" & chkSesi.Text & "' AND Semester='" & ddlSemester.SelectedValue & "' AND Tahun='" & ddlTahun.SelectedValue & "' AND KursusID='" & strKursusID & "' AND PelajarID='" & strPelajarID & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)
                    End If
                End If
            Next

            'Response.Write(strSQL)
            If strRet = "0" Then

                divMsg.Attributes("class") = "info"
                lblMsg.Text = "Markah berjaya dimasukkan"
            Else
                divMsg.Attributes("class") = "error"
                lblMsg.Text = "Markah tidak berjaya dimasukkan"

            End If

        Catch ex As Exception
            divMsg.Attributes("class") = "error"

        End Try
        Return True

    End Function
    Private Sub refreshVar()

        strNama = ""
        strMyKad = ""
        strKodKursus = ""

        strAmali = ""
        strCatatan2 = ""
    End Sub

    Private Sub chkSesi_SelectedIndexChanged(sender As Object, e As EventArgs) Handles chkSesi.SelectedIndexChanged
        kpmkv_kodkursus_list()
    End Sub

    Protected Sub lnkList_Click(sender As Object, e As EventArgs) Handles lnkList.Click
        Response.Redirect("apkv.pemeriksa.markah.pa.aspx")
    End Sub
End Class