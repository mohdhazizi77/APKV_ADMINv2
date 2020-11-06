Imports System.Data.SqlClient

Public Class pembetulan_markah_PB_akademik

    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                kpmkv_negeri_list()
                ddlNegeri.Text = "0"

                kpmkv_jenis_list()
                ddlJenis.Text = "0"

                kpmkv_kolej_list()
                ddlKolej.Text = "0"

                kpmkv_tahun_list()
                ddlTahun.Text = "-Pilih-"

                kpmkv_semester_list()

                kpmkv_kodkursus_list()

                kpmkv_kelas_list()

                lblMsg.Text = ""

            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message
        End Try
    End Sub
    Private Sub kpmkv_negeri_list()
        strSQL = "SELECT Negeri FROM kpmkv_negeri ORDER BY Negeri"
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

            '--ALL
            ddlNegeri.Items.Add(New ListItem("-Pilih-", "0"))

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub
    Private Sub kpmkv_jenis_list()
        strSQL = "SELECT Jenis FROM kpmkv_jeniskolej  ORDER BY Jenis"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlJenis.DataSource = ds
            ddlJenis.DataTextField = "Jenis"
            ddlJenis.DataValueField = "Jenis"
            ddlJenis.DataBind()

            '--ALL
            ddlJenis.Items.Add(New ListItem("-Pilih-", "0"))

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub
    Private Sub kpmkv_kolej_list()
        strSQL = "SELECT Nama,RecordID FROM kpmkv_kolej WHERE Negeri='" & ddlNegeri.SelectedItem.Value & "' AND Jenis='" & ddlJenis.SelectedValue & "'"
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

            '--ALL
            ddlKolej.Items.Add(New ListItem("-Pilih-", "0"))

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
        strSQL += " WHERE kpmkv_kelas.KolejRecordID='" & ddlKolej.SelectedValue & "' AND kpmkv_kursus.Tahun='" & ddlTahun.Text & "' AND kpmkv_kursus.Sesi='" & chkSesi.SelectedValue & "'"
        strSQL += " GROUP BY kpmkv_kursus.KodKursus, kpmkv_kursus.KursusID  ORDER BY kpmkv_kursus.KodKursus"
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
            ddlKodKursus.Items.Add(New ListItem("-Pilih-", "0"))

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub kpmkv_kelas_list()
        strSQL = " SELECT kpmkv_kelas.NamaKelas, kpmkv_kelas.KelasID"
        strSQL += " FROM  kpmkv_kelas_kursus LEFT OUTER JOIN kpmkv_kelas ON kpmkv_kelas_kursus.KelasID = kpmkv_kelas.KelasID LEFT OUTER JOIN"
        strSQL += " kpmkv_kursus ON kpmkv_kelas_kursus.KursusID = kpmkv_kursus.KursusID"
        strSQL += " WHERE kpmkv_kelas.KolejRecordID='" & ddlKolej.SelectedValue & "' AND kpmkv_kelas_kursus.KursusID= '" & ddlKodKursus.SelectedValue & "' ORDER BY KelasID"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlKelas.DataSource = ds
            ddlKelas.DataTextField = "NamaKelas"
            ddlKelas.DataValueField = "KelasID"
            ddlKelas.DataBind()

            '--ALL
            ddlKelas.Items.Add(New ListItem("-Pilih-", "0"))

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub hiddencolumn()

        Select Case ddlSemester.Text
            Case "1"
                datRespondent.Columns.Item("5").Visible = True
                datRespondent.Columns.Item("6").Visible = False
                datRespondent.Columns.Item("7").Visible = False
                datRespondent.Columns.Item("8").Visible = False 'bm3
                datRespondent.Columns.Item("9").Visible = True 'bi
                datRespondent.Columns.Item("10").Visible = True 'sc
                datRespondent.Columns.Item("11").Visible = True
                datRespondent.Columns.Item("12").Visible = True
                datRespondent.Columns.Item("13").Visible = True
                datRespondent.Columns.Item("14").Visible = True
            Case "2"
                datRespondent.Columns.Item("5").Visible = True
                datRespondent.Columns.Item("6").Visible = False
                datRespondent.Columns.Item("7").Visible = False
                datRespondent.Columns.Item("8").Visible = False 'bm3
                datRespondent.Columns.Item("9").Visible = True
                datRespondent.Columns.Item("10").Visible = True
                datRespondent.Columns.Item("11").Visible = True
                datRespondent.Columns.Item("12").Visible = True
                datRespondent.Columns.Item("13").Visible = True
                datRespondent.Columns.Item("14").Visible = True

            Case "3"
                datRespondent.Columns.Item("5").Visible = True
                datRespondent.Columns.Item("6").Visible = False
                datRespondent.Columns.Item("7").Visible = False
                datRespondent.Columns.Item("8").Visible = False 'bm3
                datRespondent.Columns.Item("9").Visible = True
                datRespondent.Columns.Item("10").Visible = True
                datRespondent.Columns.Item("11").Visible = True
                datRespondent.Columns.Item("12").Visible = True
                datRespondent.Columns.Item("13").Visible = True
                datRespondent.Columns.Item("14").Visible = True

            Case "4"
                datRespondent.Columns.Item("5").Visible = True
                datRespondent.Columns.Item("6").Visible = False
                datRespondent.Columns.Item("7").Visible = False
                datRespondent.Columns.Item("8").Visible = False 'bm3
                datRespondent.Columns.Item("9").Visible = True
                datRespondent.Columns.Item("10").Visible = True
                datRespondent.Columns.Item("11").Visible = True
                datRespondent.Columns.Item("12").Visible = True
                datRespondent.Columns.Item("13").Visible = True
                datRespondent.Columns.Item("14").Visible = True

        End Select

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
            ddlTahun.Items.Add(New ListItem("-Pilih-", "-Pilih-"))

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
            ddlSemester.Items.Add(New ListItem("-Pilih-", "-Pilih-"))

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub kpmkv_matapelajaran_list()

        If Not txtMykad.Text = "" Or Not txtAngkaGiliran.Text = "" Then

            strSQL = "  SELECT PelajarID FROM kpmkv_pelajar 
                        WHERE MYKAD = '" & txtMykad.Text & "' 
                        AND Semester = '" & ddlSemester.Text & "'"

            If Not txtAngkaGiliran.Text = "" Then
                strSQL += " AND AngkaGiliran = '" & txtAngkaGiliran.Text & "'"
            End If

            Dim strPelajarID As String = oCommon.getFieldValue(strSQL)

            Dim Tahun As String
            Dim Semester As String

            strSQL = "SELECT Tahun, Semester FROM kpmkv_pelajar WHERE PelajarID = '" & strPelajarID & "'"
            strRet = oCommon.getFieldValueEx(strSQL)

            Dim ar_TahunSemester As Array
            ar_TahunSemester = strRet.Split("|")

            Tahun = ar_TahunSemester(0)
            Semester = ar_TahunSemester(1)

            strSQL = "  SELECT NamaMataPelajaran, KodMataPelajaran FROM  kpmkv_matapelajaran WHERE Tahun = '" & Tahun & "' AND Semester = '" & Semester & "'"

        Else

            strSQL = "  SELECT NamaMataPelajaran, KodMataPelajaran FROM  kpmkv_matapelajaran WHERE Tahun = '" & ddlTahun.Text & "' AND Semester = '" & ddlSemester.Text & "'"

        End If

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlMatapelajaran.DataSource = ds
            ddlMatapelajaran.DataTextField = "NamaMataPelajaran"
            ddlMatapelajaran.DataValueField = "KodMataPelajaran"
            ddlMatapelajaran.DataBind()

            '--ALL
            ddlMatapelajaran.Items.Insert(0, "-Pilih-")

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub datRespondent_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles datRespondent.PageIndexChanging
        datRespondent.PageIndex = e.NewPageIndex
        strRet = BindData(datRespondent)
        If datRespondent.Rows.Count = 1 Then
            strRet = BindDataAudit(datRespondentAudit)
        End If

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

    Private Function BindDataAudit(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQLAudit, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 120
        Try
            myDataAdapter.Fill(myDataSet, "myaccount")

            If myDataSet.Tables(0).Rows.Count = 0 Then
                divMsgAudit.Attributes("class") = "error"
                lblMsgAudit.Text = "Tiada rekod."
            Else
                divMsgAudit.Attributes("class") = "info"
                lblMsgAudit.Text = "Jumlah rekod#:" & myDataSet.Tables(0).Rows.Count
            End If

            gvTable.DataSource = myDataSet
            gvTable.DataBind()
            objConn.Close()

        Catch ex As Exception
            lblMsgAudit.Text = "Error:" & ex.Message
            Return False
        End Try

        Return True

    End Function

    Private Function getSQLImport() As String

        Dim tmpSQL As String = ""
        Dim strWhere As String = ""
        Dim strOrder As String = " ORDER BY kpmkv_pelajar.Nama ASC"

        '--not deleted
        tmpSQL = "SELECT kpmkv_pelajar.PelajarID, kpmkv_pelajar.Nama, kpmkv_pelajar.AngkaGiliran, "
        tmpSQL += " kpmkv_pelajar.MYKAD, kpmkv_kursus.KodKursus, kpmkv_pelajar_markah.B_BahasaMelayu1, kpmkv_pelajar_markah.B_BahasaMelayu2, kpmkv_pelajar_markah.B_BahasaMelayu3, "
        tmpSQL += " kpmkv_pelajar_markah.B_BahasaInggeris, kpmkv_pelajar_markah.B_Science1, kpmkv_pelajar_markah.B_Sejarah, "
        tmpSQL += " kpmkv_pelajar_markah.B_PendidikanIslam1, kpmkv_pelajar_markah.B_PendidikanMoral, kpmkv_pelajar_markah.B_Mathematics, kpmkv_pelajar_markah.B_BahasaMelayu"
        tmpSQL += " FROM kpmkv_pelajar_markah LEFT OUTER JOIN kpmkv_pelajar ON kpmkv_pelajar_markah.PelajarID = kpmkv_pelajar.PelajarID"
        tmpSQL += " LEFT OUTER Join kpmkv_kursus ON kpmkv_pelajar.KursusID = kpmkv_kursus.KursusID"
        tmpSQL += " LEFT OUTER Join kpmkv_kolej ON kpmkv_kolej.RecordID = kpmkv_pelajar.KolejRecordID"
        strWhere = " WHERE kpmkv_pelajar.IsDeleted='N' AND kpmkv_pelajar.StatusID='2' AND kpmkv_pelajar.JenisCalonID='2'"

        '--negeri
        If Not ddlNegeri.SelectedItem.Text = "-Pilih-" Then
            strWhere += " AND kpmkv_kolej.Negeri='" & ddlNegeri.SelectedItem.Text & "'"
        End If
        '--kolej
        If Not ddlKolej.SelectedItem.Text = "-Pilih-" Then
            strWhere += " AND kpmkv_pelajar.KolejRecordID='" & ddlKolej.SelectedValue & "'"
        End If
        '--tahun
        If Not ddlTahun.Text = "-Pilih-" Then
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

        If Not ddlKodKursus.SelectedItem.Text = "-Pilih-" Then
            strWhere += " AND kpmkv_kursus.KursusID  ='" & ddlKodKursus.SelectedValue & "'"
        End If

        If Not ddlKelas.SelectedItem.Text = "-Pilih-" Then
            strWhere += " AND kpmkv_pelajar.KelasID ='" & ddlKelas.SelectedValue & "'"
        End If

        If Not txtMykad.Text = "" Then
            strWhere += " AND kpmkv_pelajar.Mykad ='" & txtMykad.Text & "'"
        End If

        If Not txtAngkaGiliran.Text = "" Then
            strWhere += " AND kpmkv_pelajar.AngkaGiliran ='" & txtAngkaGiliran.Text & "'"
        End If


        getSQLImport = tmpSQL & strWhere & strOrder
        ''--debug
        ''Response.Write(getSQL)

        Return getSQLImport

    End Function
    Private Function getSQL() As String

        Dim tmpSQL As String = ""
        Dim strWhere As String = ""
        Dim strOrder As String = " ORDER BY kpmkv_pelajar.Nama ASC"

        '--not deleted
        tmpSQL = "SELECT kpmkv_pelajar.PelajarID, kpmkv_pelajar.Nama, kpmkv_pelajar.AngkaGiliran, "
        tmpSQL += " kpmkv_pelajar.MYKAD, kpmkv_kursus.KodKursus, kpmkv_pelajar_markah.B_BahasaMelayu1, kpmkv_pelajar_markah.B_BahasaMelayu2, kpmkv_pelajar_markah.B_BahasaMelayu3, "
        tmpSQL += " kpmkv_pelajar_markah.B_BahasaInggeris, kpmkv_pelajar_markah.B_Science1, kpmkv_pelajar_markah.B_Sejarah, "
        tmpSQL += " kpmkv_pelajar_markah.B_PendidikanIslam1, kpmkv_pelajar_markah.B_PendidikanMoral, kpmkv_pelajar_markah.B_Mathematics, kpmkv_pelajar_markah.B_BahasaMelayu"
        tmpSQL += " FROM kpmkv_pelajar_markah LEFT OUTER JOIN kpmkv_pelajar ON kpmkv_pelajar_markah.PelajarID = kpmkv_pelajar.PelajarID"
        tmpSQL += " LEFT OUTER Join kpmkv_kursus ON kpmkv_pelajar.KursusID = kpmkv_kursus.KursusID"
        tmpSQL += " LEFT OUTER Join kpmkv_kolej ON kpmkv_kolej.RecordID = kpmkv_pelajar.KolejRecordID"
        strWhere = " WHERE kpmkv_pelajar.IsDeleted='N' AND kpmkv_pelajar.StatusID='2' AND kpmkv_pelajar.JenisCalonID='2'"

        '--negeri
        If Not ddlNegeri.SelectedItem.Text = "-Pilih-" Then
            strWhere += " AND kpmkv_kolej.Negeri='" & ddlNegeri.SelectedItem.Text & "'"
        End If
        '--kolej
        If Not ddlKolej.SelectedItem.Text = "-Pilih-" Then
            strWhere += " AND kpmkv_pelajar.KolejRecordID='" & ddlKolej.SelectedValue & "'"
        End If
        '--tahun
        If Not ddlTahun.Text = "-Pilih-" Then
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

        If Not ddlKodKursus.SelectedItem.Text = "-Pilih-" Then
            strWhere += " AND kpmkv_kursus.KursusID  ='" & ddlKodKursus.SelectedValue & "'"
        End If

        If Not ddlKelas.SelectedItem.Text = "-Pilih-" Then
            strWhere += " AND kpmkv_pelajar.KelasID ='" & ddlKelas.SelectedValue & "'"
        End If

        If Not txtMykad.Text = "" Then
            strWhere += " AND kpmkv_pelajar.Mykad ='" & txtMykad.Text & "'"
        End If

        If Not txtAngkaGiliran.Text = "" Then
            strWhere += " AND kpmkv_pelajar.AngkaGiliran ='" & txtAngkaGiliran.Text & "'"
        End If

        getSQL = tmpSQL & strWhere & strOrder
        ''--debug
        'Response.Write(getSQL)

        Return getSQL

    End Function

    Private Function getSQLAudit() As String

        Dim tmpSQL As String = ""

        strSQL = "  SELECT PelajarID FROM kpmkv_pelajar 
                        WHERE MYKAD = '" & txtMykad.Text & "'                          
                        AND Semester = '" & ddlSemester.Text & "'"

        If Not txtAngkaGiliran.Text = "" Then
            strSQL += " AND AngkaGiliran = '" & txtAngkaGiliran.Text & "'"
        End If

        Dim strPelajarID As String = oCommon.getFieldValue(strSQL)


        tmpSQL = "  SELECT 
                    A.markahAuditID, A.dateTime,
                    B.LoginID, 
                    C.Nama, C.AngkaGiliran, 
                    A.Tahun, A.Semester, A.Sesi, A.Menu, 
                    D.KodMataPelajaran, D.NamaMataPelajaran,
                    A.MarkahSebelum, A.MarkahSelepas, A.Catatan, A.Jenis,
                    E.KodKursus
                    FROM kpmkv_pelajar_markah_audit A
                    LEFT JOIN kpmkv_users B ON B.UserID = A.UserID
                    LEFT JOIN kpmkv_pelajar C ON C.PelajarID = A.PelajarID
                    LEFT JOIN kpmkv_matapelajaran D ON D.MataPelajaranId = A.MataPelajaranId
                    LEFT JOIN kpmkv_kursus E ON E.KursusID = C.KursusID
                    WHERE
                    A.PelajarID = '" & strPelajarID & "'
                    AND A.Menu = 'PB AKADEMIK'
                    ORDER BY A.dateTime DESC"

        getSQLAudit = tmpSQL
        Debug.WriteLine(getSQLAudit)

        Return getSQLAudit

    End Function

    Private Sub datRespondent_SelectedIndexChanging(sender As Object, e As GridViewSelectEventArgs) Handles datRespondent.SelectedIndexChanging
        Dim strKeyID As String = datRespondent.DataKeys(e.NewSelectedIndex).Value.ToString

    End Sub

    Protected Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        Try
            ExportToCSV(getSQL)

            'strSQL = "  INSERT INTO kpmkv_pelajar_markah_audit 
            '        ( UserID, PelajarID, KursusID, MataPelajaranID, dateTime, Tahun, Semester, Sesi, Menu, MarkahSebelum, MarkahSelepas, Catatan, Jenis )
            '        VALUES
            '        ( '" & UserID & "', '" & PelajarID & "', '" & strKursusID & "', '" & strMataPelajaranID & "', GETDATE(), '" & ddlTahun.Text & "', '" & ddlSemester.Text & "', '" & chkSesi.Text & "', 'PB AKADEMIK', '" & markahSebelum & "', '" & markahSelepas & "', 'KEMASKINI', 'MARKAH')"
            'strRet = oCommon.ExecuteSQL(strSQL)

        Catch ex As Exception
            lblMsg.Text = "Error:" & ex.Message
        End Try
    End Sub

    Private Sub ExportToCSV(ByVal strQuery As String)
        'Get the data from database into datatable 
        Dim cmd As New SqlCommand(strQuery)
        Dim dt As DataTable = GetData(cmd)

        Response.Clear()
        Response.Buffer = True
        Response.AddHeader("content-disposition", "attachment;filename=KOKO_File.csv")
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

    Protected Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        lblMsg.Text = ""

        Try
            If ValidateForm() = False Then
                lblMsgResult.Text = "Sila masukkan NOMBOR SAHAJA"
                lblMsg.Text = "Sila masukkan NOMBOR 0-100 SAHAJA"
                Exit Sub
            End If

            If datRespondent.Rows.Count = 1 Then

                For i As Integer = 0 To datRespondent.Rows.Count - 1

                    '-- ENHANCEMENT AUDIT TRAILS 18072019 start
                    strSQL = "SELECT NamaMatapelajaran FROM kpmkv_matapelajaran WHERE KodMataPelajaran = '" & ddlMatapelajaran.SelectedValue & "'"
                    Dim strNamaMP As String = oCommon.getFieldValue(strSQL)

                    strSQL = "SELECT "

                    If strNamaMP = "BAHASA MELAYU" Then

                        strSQL += " B_BahasaMelayu"

                    ElseIf strNamaMP = "BAHASA INGGERIS" Then

                        strSQL += " B_BahasaInggeris"

                    ElseIf strNamaMP = "SAINS" Or strNamaMP = "SAINS UNTUK TEKNOLOGI" Or strNamaMP = "SAINS UNTUK PENGAJIAN SOSIAL" Then

                        strSQL += " B_Science1"

                    ElseIf strNamaMP = "SEJARAH" Then

                        strSQL += " B_Sejarah"

                    ElseIf strNamaMP = "PENDIDIKAN ISLAM" Then

                        strSQL += " B_PendidikanIslam1"

                    ElseIf strNamaMP = "PENDIDIKAN MORAL" Then

                        strSQL += " B_PendidikanMoral"

                    ElseIf strNamaMP = "MATEMATIK" Or strNamaMP = "MATEMATIK UNTUK TEKNOLOGI" Or strNamaMP = "MATEMATIK UNTUK PENGAJIAN SOSIAL" Then

                        strSQL += " B_Mathematics"

                    End If

                    strSQL += " FROM kpmkv_pelajar_markah
                                WHERE PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"

                    Dim MarkahSebelum As String = FormatNumber(CDbl(oCommon.getFieldValue(strSQL)), 2)
                    Dim MarkahSelepas As String = txtMarkah.Text
                    '-- ENHANCEMENT AUDIT TRAILS 18072019 end

                    strSQL = "UPDATE kpmkv_pelajar_markah SET"

                    If strNamaMP = "BAHASA MELAYU" Then

                        strSQL += " B_BahasaMelayu = '" & txtMarkah.Text & "'"

                    ElseIf strNamaMP = "BAHASA INGGERIS" Then

                        strSQL += " B_BahasaInggeris = '" & txtMarkah.Text & "'"

                    ElseIf strNamaMP = "SAINS" Or strNamaMP = "SAINS UNTUK TEKNOLOGI" Or strNamaMP = "SAINS UNTUK PENGAJIAN SOSIAL" Then

                        strSQL += " B_Science1 = '" & txtMarkah.Text & "'"

                    ElseIf strNamaMP = "SEJARAH" Then

                        strSQL += " B_Sejarah = '" & txtMarkah.Text & "'"

                    ElseIf strNamaMP = "PENDIDIKAN ISLAM" Then

                        strSQL += " B_PendidikanIslam1 = '" & txtMarkah.Text & "'"

                    ElseIf strNamaMP = "PENDIDIKAN MORAL" Then

                        strSQL += " B_PendidikanMoral = '" & txtMarkah.Text & "'"

                    ElseIf strNamaMP = "MATEMATIK" Or strNamaMP = "MATEMATIK UNTUK TEKNOLOGI" Or strNamaMP = "MATEMATIK UNTUK PENGAJIAN SOSIAL" Then

                        strSQL += " B_Mathematics = '" & txtMarkah.Text & "'"

                    End If

                    strSQL += " WHERE PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)
                    If strRet = "0" Then
                        divMsgResult.Attributes("class") = "info"
                        lblMsgResult.Text = "Kemaskini Berjaya!"
                        '-- ENHANCEMENT 16072019 AUDIT TRAILS start
                        AuditTrailInsert(datRespondent.DataKeys(i).Value.ToString, MarkahSebelum, txtMarkah.Text, "KEMASKINI")
                        '-- ENHANCEMENT 16072019 AUDIT TRAILS end

                        strRet = BindData(datRespondent)

                        If datRespondent.Rows.Count = 1 Then
                            strRet = BindDataAudit(datRespondentAudit)
                        End If
                    Else
                        divMsgResult.Attributes("class") = "error"
                        lblMsgResult.Text = "Tidak Berjaya Mengemaskini Markah"
                    End If

                Next

            Else

                divMsgResult.Attributes("class") = "error"
                lblMsgResult.Text = "Tidak Berjaya Mengemaskini Markah!"

            End If

        Catch ex As Exception
            lblMsg.Text = "Error:" & ex.Message
        End Try




    End Sub

    Private Function ValidateForm() As Boolean
        For i As Integer = 0 To datRespondent.Rows.Count - 1

            If Not txtMarkah.Text.Length = 0 Then
                If oCommon.IsCurrency(txtMarkah.Text) = False Then
                    Return False
                End If
                If CInt(txtMarkah.Text) > 100 Then
                    Return False
                End If
                If CInt(txtMarkah.Text) = -1 Then
                    Return False
                End If
            Else
                txtMarkah.Text = "0"
            End If

        Next

        Return True

    End Function

    Protected Sub btnCari_Click(sender As Object, e As EventArgs) Handles btnCari.Click

        lblMsg.Text = ""

        strRet = BindData(datRespondent)
        strRet = BindDataAudit(datRespondentAudit)


        kpmkv_matapelajaran_list()
        ddlMatapelajaran.SelectedValue = "-Pilih-"

    End Sub

    Private Sub chkSesi_SelectedIndexChanged(sender As Object, e As EventArgs) Handles chkSesi.SelectedIndexChanged

        kpmkv_kodkursus_list()
        kpmkv_kelas_list()

    End Sub

    Private Sub ddlKodKursus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlKodKursus.SelectedIndexChanged

        kpmkv_kelas_list()

    End Sub

    Protected Sub ddlJenis_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlJenis.SelectedIndexChanged

        kpmkv_kolej_list()
        ddlKolej.Text = "0"

    End Sub

    Private Sub btnGred_Click(sender As Object, e As EventArgs) Handles btnGred.Click

        lblMsg.Text = ""

        Dim arrayMarkah As Array
        Dim MarkahSebelum1 As String = ""
        Dim MarkahSebelum2 As String = ""
        Dim MarkahSelepas1 As String = ""
        Dim MarkahSelepas2 As String = ""
        Dim GredSebelum As String = ""
        Dim GredSelepas As String = ""

        If datRespondent.Rows.Count = 1 Then

            For i As Integer = 0 To datRespondent.Rows.Count - 1

                '-- ENHANCEMENT AUDIT TRAILS 18072019 start
                strSQL = "SELECT NamaMatapelajaran FROM kpmkv_matapelajaran WHERE KodMataPelajaran = '" & ddlMatapelajaran.SelectedValue & "'"
                Dim strNamaMP As String = oCommon.getFieldValue(strSQL)

                strSQL = "SELECT "

                If strNamaMP = "BAHASA MELAYU" Then

                    strSQL += " BahasaMelayu, PointerBMSetara"

                ElseIf strNamaMP = "BAHASA INGGERIS" Then

                    strSQL += " BahasaInggeris"

                ElseIf strNamaMP = "SAINS" Or strNamaMP = "SAINS UNTUK TEKNOLOGI" Or strNamaMP = "SAINS UNTUK PENGAJIAN SOSIAL" Then

                    strSQL += " Science"

                ElseIf strNamaMP = "SEJARAH" Then

                    strSQL += " Sejarah, PointerSJSetara"

                ElseIf strNamaMP = "PENDIDIKAN ISLAM" Then

                    strSQL += " PendidikanIslam"

                ElseIf strNamaMP = "PENDIDIKAN MORAL" Then

                    strSQL += " PendidikanMoral"

                ElseIf strNamaMP = "MATEMATIK" Or strNamaMP = "MATEMATIK UNTUK TEKNOLOGI" Or strNamaMP = "MATEMATIK UNTUK PENGAJIAN SOSIAL" Then

                    strSQL += " Mathematics"

                End If

                strSQL += " FROM kpmkv_pelajar_markah
                                WHERE PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"

                If ddlSemester.Text = "4" Then

                    If strNamaMP = "BAHASA MELAYU" Or strNamaMP = "SEJARAH" Then

                        strRet = oCommon.getFieldValueEx(strSQL)
                        arrayMarkah = strRet.Split("|")

                        MarkahSebelum1 = arrayMarkah(0)
                        MarkahSebelum2 = arrayMarkah(1)

                    Else

                        MarkahSebelum1 = oCommon.getFieldValue(strSQL)

                    End If

                Else

                    MarkahSebelum1 = oCommon.getFieldValue(strSQL)

                End If

                Dim strQuerySebelum As String = strSQL

                '-- ENHANCEMENT AUDIT TRAILS 18072019 end
                Akademik_markah()
                '-- ENHANCEMENT AUDIT TRAILS 18072019 start

                strSQL = strQuerySebelum

                If ddlSemester.Text = "4" Then

                    If strNamaMP = "BAHASA MELAYU" Or strNamaMP = "SEJARAH" Then

                        strRet = oCommon.getFieldValueEx(strSQL)
                        arrayMarkah = strRet.Split("|")

                        MarkahSelepas1 = arrayMarkah(0)
                        MarkahSelepas2 = arrayMarkah(1)

                    Else

                        MarkahSelepas1 = oCommon.getFieldValue(strSQL)

                    End If

                Else

                    MarkahSelepas1 = oCommon.getFieldValue(strSQL)

                End If

                AuditTrailInsert(datRespondent.DataKeys(i).Value.ToString, MarkahSebelum1, MarkahSelepas1, "MARKAH")

                If ddlSemester.Text = "4" Then

                    If strNamaMP = "BAHASA MELAYU" Or strNamaMP = "SEJARAH" Then

                        AuditTrailInsert(datRespondent.DataKeys(i).Value.ToString, MarkahSebelum2, MarkahSelepas2, "POINTER")

                    End If

                End If

            Next

        End If
        '-- ENHANCEMENT AUDIT TRAILS 18072019 end   

        '-- ENHANCEMENT AUDIT TRAILS 22072019 start  
        If datRespondent.Rows.Count = 1 Then

            For i As Integer = 0 To datRespondent.Rows.Count - 1

                strSQL = "SELECT NamaMatapelajaran FROM kpmkv_matapelajaran WHERE KodMataPelajaran = '" & ddlMatapelajaran.SelectedValue & "'"
                Dim strNamaMP As String = oCommon.getFieldValue(strSQL)

                strSQL = " SELECT "

                If strNamaMP = "BAHASA MELAYU" Then

                    strSQL += "GredBM"

                ElseIf strNamaMP = "BAHASA INGGERIS" Then

                    strSQL += "GredBI"

                ElseIf strNamaMP = "SAINS" Or strNamaMP = "SAINS UNTUK TEKNOLOGI" Or strNamaMP = "SAINS UNTUK PENGAJIAN SOSIAL" Then

                    strSQL += "GredSC"

                ElseIf strNamaMP = "SEJARAH" Then

                    strSQL += "GredSJ"

                ElseIf strNamaMP = "PENDIDIKAN ISLAM" Then

                    strSQL += "GredPI"

                ElseIf strNamaMP = "PENDIDIKAN MORAL" Then

                    strSQL += "GredPM"

                ElseIf strNamaMP = "MATEMATIK" Or strNamaMP = "MATEMATIK UNTUK TEKNOLOGI" Or strNamaMP = "MATEMATIK UNTUK PENGAJIAN SOSIAL" Then

                    strSQL += "GredMT"

                End If

                strSQL += " FROM kpmkv_pelajar_markah
                            WHERE PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"

                GredSebelum = oCommon.getFieldValue(strSQL)

                Dim strQuerySebelum As String = strSQL

                Akademik_gred()

                GredSelepas = oCommon.getFieldValue(strQuerySebelum)

                AuditTrailInsert(datRespondent.DataKeys(i).Value.ToString, GredSebelum, GredSelepas, "GRED")

            Next

        End If

        '-- ENHANCEMENT AUDIT TRAILS 22072019 end   

        If Not strRet = "0" Then
            divMsgResult.Attributes("class") = "error"
            lblMsgResult.Text = "Tidak Berjaya mengemaskini gred Pentaksiran Akhir Akademik"
        Else
            divMsgResult.Attributes("class") = "info"
            lblMsgResult.Text = "Berjaya mengemaskini gred Pentaksiran Akhir Akademik"
            'strRet = BindData(datRespondent)
            If datRespondent.Rows.Count = 1 Then
                strRet = BindDataAudit(datRespondentAudit)
            End If
        End If

        strRet = BindData(datRespondent)

        If datRespondent.Rows.Count = 1 Then
            strRet = BindDataAudit(datRespondentAudit)
        End If

    End Sub

    Private Sub AuditTrailInsert(ByVal PelajarID As String, ByVal MarkahSebelum As String, ByVal MarkahSelepas As String, ByVal Jenis As String)

        strSQL = "SELECT UserID FROM kpmkv_users WHERE LoginID='" & Session("LoginID") & "'"
        Dim UserID As String = oCommon.getFieldValue(strSQL)

        Dim Tahun As String
        Dim Semester As String

        strSQL = "SELECT Tahun, Semester FROM kpmkv_pelajar WHERE PelajarID = '" & PelajarID & "'"
        strRet = oCommon.getFieldValueEx(strSQL)

        Dim ar_TahunSemester As Array
        ar_TahunSemester = strRet.Split("|")

        Tahun = ar_TahunSemester(0)
        Semester = ar_TahunSemester(1)

        strSQL = "SELECT MataPelajaranID FROM kpmkv_matapelajaran WHERE KodMataPelajaran = '" & ddlMatapelajaran.SelectedValue & "' AND Tahun = '" & Tahun & "' AND Semester = '" & Semester & "'"
        Dim strMataPelajaranID As String = oCommon.getFieldValue(strSQL)

        strSQL = "SELECT KursusID FROM kpmkv_pelajar_markah WHERE PelajarID = '" & PelajarID & "' AND Tahun = '" & Tahun & "' AND Semester = '" & Semester & "'"
        Dim strKursusID As String = oCommon.getFieldValue(strSQL)

        strSQL = "SELECT NamaMatapelajaran FROM kpmkv_matapelajaran WHERE KodMataPelajaran = '" & ddlMatapelajaran.SelectedValue & "'"
        Dim strNamaMP As String = oCommon.getFieldValue(strSQL)

        If Jenis = "POINTER" Then

            If strNamaMP = "BAHASA MELAYU" Then

                strSQL = "  INSERT INTO kpmkv_pelajar_markah_audit 
                            ( UserID, PelajarID, KursusID, MataPelajaranID, dateTime, Tahun, Semester, Sesi, Menu, MarkahSebelum, MarkahSelepas, Catatan, Jenis )
                            VALUES
                            ( '" & UserID & "', '" & PelajarID & "', '" & strKursusID & "', '" & strMataPelajaranID & "', GETDATE(), '" & Tahun & "', '" & Semester & "', '" & chkSesi.Text & "', 'PB AKADEMIK', '" & MarkahSebelum & "', '" & MarkahSelepas & "', 'JANA GRED', 'POINTERBMSETARA')"
                strRet = oCommon.ExecuteSQL(strSQL)

            ElseIf strNamaMP = "SEJARAH" Then

                strSQL = "  INSERT INTO kpmkv_pelajar_markah_audit 
                            ( UserID, PelajarID, KursusID, MataPelajaranID, dateTime, Tahun, Semester, Sesi, Menu, MarkahSebelum, MarkahSelepas, Catatan, Jenis )
                            VALUES
                            ( '" & UserID & "', '" & PelajarID & "', '" & strKursusID & "', '" & strMataPelajaranID & "', GETDATE(), '" & Tahun & "', '" & Semester & "', '" & chkSesi.Text & "', 'PB AKADEMIK', '" & MarkahSebelum & "', '" & MarkahSelepas & "', 'JANA GRED', 'POINTERSJSETARA')"
                strRet = oCommon.ExecuteSQL(strSQL)

            End If

        ElseIf Jenis = "MARKAH" Then

            strSQL = "  INSERT INTO kpmkv_pelajar_markah_audit 
                        ( UserID, PelajarID, KursusID, MataPelajaranID, dateTime, Tahun, Semester, Sesi, Menu, MarkahSebelum, MarkahSelepas, Catatan, Jenis )
                        VALUES
                        ( '" & UserID & "', '" & PelajarID & "', '" & strKursusID & "', '" & strMataPelajaranID & "', GETDATE(), '" & Tahun & "', '" & Semester & "', '" & chkSesi.Text & "', 'PB AKADEMIK', '" & MarkahSebelum & "', '" & MarkahSelepas & "', 'JANA GRED', 'MARKAH')"
            strRet = oCommon.ExecuteSQL(strSQL)

        ElseIf Jenis = "GRED" Then

            strSQL = "  INSERT INTO kpmkv_pelajar_markah_audit 
                        ( UserID, PelajarID, KursusID, MataPelajaranID, dateTime, Tahun, Semester, Sesi, Menu, MarkahSebelum, MarkahSelepas, Catatan, Jenis )
                        VALUES
                        ( '" & UserID & "', '" & PelajarID & "', '" & strKursusID & "', '" & strMataPelajaranID & "', GETDATE(), '" & Tahun & "', '" & Semester & "', '" & chkSesi.Text & "', 'PB AKADEMIK', '" & MarkahSebelum & "', '" & MarkahSelepas & "', 'JANA GRED', 'GRED')"
            strRet = oCommon.ExecuteSQL(strSQL)

        Else

            strSQL = "  INSERT INTO kpmkv_pelajar_markah_audit 
                        ( UserID, PelajarID, KursusID, MataPelajaranID, dateTime, Tahun, Semester, Sesi, Menu, MarkahSebelum, MarkahSelepas, Catatan, Jenis )
                        VALUES
                        ( '" & UserID & "', '" & PelajarID & "', '" & strKursusID & "', '" & strMataPelajaranID & "', GETDATE(), '" & Tahun & "', '" & Semester & "', '" & chkSesi.Text & "', 'PB AKADEMIK', '" & MarkahSebelum & "', '" & MarkahSelepas & "', 'KEMASKINI', 'MARKAH')"
            strRet = oCommon.ExecuteSQL(strSQL)

        End If

    End Sub

    Private Sub Akademik_markah()

        Dim strKodMP As Integer

        Select Case ddlSemester.Text
            Case "1"
                strKodMP = "100"
            Case "2"
                strKodMP = "200"
            Case "3"
                strKodMP = "300"
            Case "4"
                strKodMP = "400"
        End Select

        If datRespondent.Rows.Count = 1 Then

            For i As Integer = 0 To datRespondent.Rows.Count - 1

                Dim Tahun As String
                Dim Semester As String

                strSQL = "SELECT Tahun, Semester FROM kpmkv_pelajar WHERE PelajarID = '" & datRespondent.DataKeys(i).Value.ToString & "'"
                strRet = oCommon.getFieldValueEx(strSQL)

                Dim ar_TahunSemester As Array
                ar_TahunSemester = strRet.Split("|")

                Tahun = ar_TahunSemester(0)
                Semester = ar_TahunSemester(1)

                '-- ENHANCEMENT AUDIT TRAILS 18072019 start
                strSQL = "SELECT NamaMatapelajaran FROM kpmkv_matapelajaran WHERE KodMataPelajaran = '" & ddlMatapelajaran.SelectedValue & "'"
                Dim strNamaMP As String = oCommon.getFieldValue(strSQL)

                If strNamaMP = "BAHASA MELAYU" Then

                    ' Dim GredBM As Integer
                    Dim BerterusanBM As Integer
                    Dim AkhiranBM As Integer

                    If ddlSemester.Text = "1" Then
                        Dim AM_BahasaMelayu As Integer
                        Dim BM_BahasaMelayu As Integer
                        Dim B_BahasaMelayu As Double
                        Dim A_BahasaMelayu As Double
                        Dim PointerBM As Integer

                        'strSQL = "Select PB from kpmkv_matapelajaran Where KodMataPelajaran LIKE '%A03'+'" & strKodMP & "%' AND Tahun='" & ddlTahun.Text & "'"
                        strSQL = "SELECT PB FROM kpmkv_aka_weightage WHERE Tahun='" & Tahun & "'"
                        BerterusanBM = oCommon.getFieldValue(strSQL)

                        'strSQL = "Select PA from kpmkv_matapelajaran Where KodMataPelajaran LIKE '%A03'+'" & strKodMP & "%' AND Tahun='" & ddlTahun.Text & "'"
                        strSQL = "SELECT PA FROM kpmkv_aka_weightage WHERE Tahun='" & Tahun & "'"
                        AkhiranBM = oCommon.getFieldValue(strSQL)

                        strSQL = "Select B_BahasaMelayu from kpmkv_pelajar_markah Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                        B_BahasaMelayu = oCommon.getFieldValue(strSQL)
                        'round up
                        B_BahasaMelayu = Math.Ceiling(B_BahasaMelayu)

                        strSQL = "Select A_BahasaMelayu from kpmkv_pelajar_markah Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                        A_BahasaMelayu = oCommon.getFieldValue(strSQL)
                        'round up
                        A_BahasaMelayu = Math.Ceiling(A_BahasaMelayu)

                        'checkin Markah
                        If Not (B_BahasaMelayu) = "-1" And Not (A_BahasaMelayu) = "-1" Then
                            BM_BahasaMelayu = Math.Ceiling((B_BahasaMelayu / 100) * BerterusanBM)
                            AM_BahasaMelayu = Math.Ceiling((A_BahasaMelayu / 100) * AkhiranBM)
                            PointerBM = Math.Ceiling(BM_BahasaMelayu + AM_BahasaMelayu)
                            strSQL = "UPDATE kpmkv_pelajar_markah SET BahasaMelayu='" & PointerBM & "' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)

                        ElseIf (B_BahasaMelayu) = "-1" Or (A_BahasaMelayu) = "-1" Then
                            strSQL = "UPDATE kpmkv_pelajar_markah SET BahasaMelayu='-1' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)
                        End If
                        'Semester1

                    ElseIf ddlSemester.Text = "2" Then
                        Dim AM_BahasaMelayu2 As Integer
                        Dim BM_BahasaMelayu2 As Integer
                        Dim B_BahasaMelayu2 As Double
                        Dim A_BahasaMelayu2 As Double
                        Dim PointerBM2 As Integer

                        'strSQL = "Select PB from kpmkv_matapelajaran Where KodMataPelajaran LIKE '%A03'+'" & strKodMP & "%' AND Tahun='" & ddlTahun.Text & "'"
                        strSQL = "SELECT PB FROM kpmkv_aka_weightage WHERE Tahun='" & Tahun & "'"
                        BerterusanBM = oCommon.getFieldValue(strSQL)

                        'strSQL = "Select PA from kpmkv_matapelajaran Where KodMataPelajaran LIKE '%A03'+'" & strKodMP & "%' AND Tahun='" & ddlTahun.Text & "'"
                        strSQL = "SELECT PA FROM kpmkv_aka_weightage WHERE Tahun='" & Tahun & "'"
                        AkhiranBM = oCommon.getFieldValue(strSQL)

                        strSQL = "Select B_BahasaMelayu from kpmkv_pelajar_markah Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                        B_BahasaMelayu2 = oCommon.getFieldValue(strSQL)
                        'round up
                        B_BahasaMelayu2 = Math.Ceiling(B_BahasaMelayu2)

                        strSQL = "Select A_BahasaMelayu from kpmkv_pelajar_markah Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                        A_BahasaMelayu2 = oCommon.getFieldValue(strSQL)
                        'round up
                        A_BahasaMelayu2 = Math.Ceiling(A_BahasaMelayu2)

                        'checkin Markah
                        If Not (B_BahasaMelayu2) = "-1" And Not (A_BahasaMelayu2) = "-1" Then
                            BM_BahasaMelayu2 = Math.Ceiling((B_BahasaMelayu2 / 100) * BerterusanBM)
                            AM_BahasaMelayu2 = Math.Ceiling((A_BahasaMelayu2 / 100) * AkhiranBM)
                            PointerBM2 = Math.Ceiling(BM_BahasaMelayu2 + AM_BahasaMelayu2)
                            strSQL = "UPDATE kpmkv_pelajar_markah SET BahasaMelayu='" & PointerBM2 & "' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)

                        ElseIf (B_BahasaMelayu2) = "-1" Or (A_BahasaMelayu2) = "-1" Then
                            strSQL = "UPDATE kpmkv_pelajar_markah SET BahasaMelayu='-1' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)
                        End If
                        'semester2

                    ElseIf ddlSemester.Text = "3" Then

                        Dim AM_BahasaMelayu3 As Integer
                        Dim BM_BahasaMelayu3 As Integer
                        Dim B_BahasaMelayu3 As Double
                        Dim A_BahasaMelayu3 As Double
                        Dim PointerBM3 As Integer

                        'strSQL = "Select PB from kpmkv_matapelajaran Where KodMataPelajaran LIKE '%A03'+'" & strKodMP & "%' AND Tahun='" & ddlTahun.Text & "'"
                        strSQL = "SELECT PB FROM kpmkv_aka_weightage WHERE Tahun='" & Tahun & "'"
                        BerterusanBM = oCommon.getFieldValue(strSQL)

                        'strSQL = "Select PA from kpmkv_matapelajaran Where KodMataPelajaran LIKE '%A03'+'" & strKodMP & "%' AND Tahun='" & ddlTahun.Text & "'"
                        strSQL = "SELECT PA FROM kpmkv_aka_weightage WHERE Tahun='" & Tahun & "'"
                        AkhiranBM = oCommon.getFieldValue(strSQL)

                        strSQL = "Select B_BahasaMelayu from kpmkv_pelajar_markah Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                        B_BahasaMelayu3 = oCommon.getFieldValue(strSQL)
                        'round up
                        B_BahasaMelayu3 = Math.Ceiling(B_BahasaMelayu3)

                        strSQL = "Select A_BahasaMelayu from kpmkv_pelajar_markah Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                        A_BahasaMelayu3 = oCommon.getFieldValue(strSQL)
                        'round up
                        A_BahasaMelayu3 = Math.Ceiling(A_BahasaMelayu3)

                        'checkin Markah
                        If Not (B_BahasaMelayu3) = "-1" And Not (A_BahasaMelayu3) = "-1" Then
                            BM_BahasaMelayu3 = Math.Ceiling((B_BahasaMelayu3 / 100) * BerterusanBM)
                            AM_BahasaMelayu3 = Math.Ceiling((A_BahasaMelayu3 / 100) * AkhiranBM)
                            PointerBM3 = Math.Ceiling(BM_BahasaMelayu3 + AM_BahasaMelayu3)
                            strSQL = "UPDATE kpmkv_pelajar_markah SET BahasaMelayu='" & PointerBM3 & "' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)

                        ElseIf (B_BahasaMelayu3) = "-1" Or (A_BahasaMelayu3) = "-1" Then
                            strSQL = "UPDATE kpmkv_pelajar_markah SET BahasaMelayu='-1' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)
                        End If
                        'semester3

                    ElseIf ddlSemester.Text = "4" Then
                        Dim PB4 As Integer
                        ' Dim PA4 As Integer
                        Dim PABmSetara As Integer
                        Dim PAPB4 As Integer
                        ' Dim PAPB As Integer
                        Dim B_BahasaMelayuSem1 As Integer
                        Dim B_BahasaMelayuSem2 As Integer
                        Dim B_BahasaMelayuSem3 As Integer
                        Dim B_BahasaMelayuSem4 As Integer
                        Dim A_BahasaMelayuSem4 As Integer
                        Dim PointerBMSetara As Integer

                        'strSQL = "Select PB from kpmkv_matapelajaran Where KodMataPelajaran LIKE '%A03'+'" & strKodMP & "%' AND Tahun='" & ddlTahun.Text & "'"
                        strSQL = "SELECT PB FROM kpmkv_aka_weightage WHERE Tahun='" & Tahun & "'"
                        BerterusanBM = oCommon.getFieldValue(strSQL)

                        'strSQL = "Select PA from kpmkv_matapelajaran Where KodMataPelajaran LIKE '%A03'+'" & strKodMP & "%' AND Tahun='" & ddlTahun.Text & "'"
                        strSQL = "SELECT PA FROM kpmkv_aka_weightage WHERE Tahun='" & Tahun & "'"
                        AkhiranBM = oCommon.getFieldValue(strSQL)

                        'get mykad
                        strSQL = " SELECT Mykad FROM kpmkv_pelajar"
                        strSQL += " WHERE PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                        Dim strMYKAD1 As String = oCommon.getFieldValue(strSQL)


                        'get pelajarid
                        strSQL = " SELECT PelajarID FROM kpmkv_pelajar"
                        strSQL += " WHERE StatusID='2' AND IsDeleted='N' AND Semester='1'"
                        strSQL += " AND Mykad='" & strMYKAD1 & "'"
                        Dim strPelajarID1 As String = oCommon.getFieldValue(strSQL)

                        'get bm sem 1
                        strSQL = "Select BahasaMelayu from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID1 & "'"
                        strSQL += " AND Semester='1' "
                        B_BahasaMelayuSem1 = oCommon.getFieldValue(strSQL)
                        'round up
                        B_BahasaMelayuSem1 = Math.Ceiling(B_BahasaMelayuSem1)
                        '----------------------------------------------------------------------------

                        'get pelajarid
                        strSQL = " SELECT PelajarID FROM kpmkv_pelajar"
                        strSQL += " WHERE StatusID='2' AND IsDeleted='N' AND Semester='2'"
                        strSQL += " AND Mykad='" & strMYKAD1 & "'"
                        Dim strPelajarID2 As String = oCommon.getFieldValue(strSQL)

                        'get Bm sem 2
                        strSQL = "Select BahasaMelayu from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID2 & "'"
                        strSQL += " AND Semester='2' "
                        B_BahasaMelayuSem2 = oCommon.getFieldValue(strSQL)
                        'round up
                        B_BahasaMelayuSem2 = Math.Ceiling(B_BahasaMelayuSem2)


                        'get pelajarid
                        strSQL = " SELECT PelajarID FROM kpmkv_pelajar"
                        strSQL += " WHERE StatusID='2' AND IsDeleted='N' AND Semester='3'"
                        strSQL += " AND Mykad='" & strMYKAD1 & "'"
                        Dim strPelajarID3 As String = oCommon.getFieldValue(strSQL)

                        'get bm sem 3
                        strSQL = "Select BahasaMelayu from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID3 & "'"
                        strSQL += " AND Semester='3' "
                        B_BahasaMelayuSem3 = oCommon.getFieldValue(strSQL)
                        'round up
                        B_BahasaMelayuSem3 = Math.Ceiling(B_BahasaMelayuSem3)

                        'get bm sem 4 PB
                        strSQL = "Select B_BahasaMelayu from kpmkv_pelajar_markah Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                        B_BahasaMelayuSem4 = oCommon.getFieldValue(strSQL)

                        'get bm sem 4 PA
                        strSQL = "Select A_BahasaMelayu3 from kpmkv_pelajar_markah Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                        A_BahasaMelayuSem4 = oCommon.getFieldValue(strSQL)

                        Dim Kertas1 As Integer = 0
                        Dim Kertas2 As Integer = 0

                        strSQL = "SELECT A_BahasaMelayu1, A_BahasaMelayu2 FROM kpmkv_pelajar_markah Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                        strRet = oCommon.getFieldValueEx(strSQL)

                        ''--get user info
                        Dim ar_Kertas As Array
                        ar_Kertas = strRet.Split("|")

                        If (String.IsNullOrEmpty(ar_Kertas(0).ToString())) Then
                            Kertas1 = 0
                        Else
                            Kertas1 = ar_Kertas(0)
                        End If

                        If (String.IsNullOrEmpty(ar_Kertas(1).ToString())) Then
                            Kertas2 = 0
                        Else
                            Kertas2 = ar_Kertas(1)
                        End If

                        If Not ((B_BahasaMelayuSem4) = "-1" Or (A_BahasaMelayuSem4) = "-1") Then
                            PB4 = Math.Ceiling((B_BahasaMelayuSem4 / 100) * BerterusanBM)
                            'PABmSetara = Math.Ceiling(A_BahasaMelayuSem4)

                            PABmSetara = Math.Ceiling((A_BahasaMelayuSem4 / 100) * 40)
                            PAPB4 = Math.Ceiling(((Kertas1 + Kertas2 + PABmSetara) / 280) * AkhiranBM)
                            'PAPB4 = Math.Ceiling(PAPB * AkhiranBM)

                            'gred sem 4 
                            Dim PointSem4 As Integer = Math.Ceiling(PB4 + PAPB4)
                            strSQL = "UPDATE kpmkv_pelajar_markah SET BahasaMelayu='" & PointSem4 & "' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)

                            If (B_BahasaMelayuSem1 = "-1" Or B_BahasaMelayuSem2 = "-1" Or B_BahasaMelayuSem3 = "-1") Then
                                PointerBMSetara = "-1"
                            Else
                                PointerBMSetara = Math.Ceiling((((B_BahasaMelayuSem1 / 100) * 10) + ((B_BahasaMelayuSem2 / 100) * 10) + ((B_BahasaMelayuSem3 / 100) * 10) + ((PointSem4 / 100) * 70)))
                            End If

                            strSQL = "UPDATE kpmkv_pelajar_markah SET PointerBMSetara='" & PointerBMSetara & "' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)

                        ElseIf ((B_BahasaMelayuSem4) = "-1" Or (A_BahasaMelayuSem4) = "-1") Then
                            strSQL = "UPDATE kpmkv_pelajar_markah SET BahasaMelayu='-1' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)

                            strSQL = "UPDATE kpmkv_pelajar_markah SET PointerBMSetara='-1' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)
                        End If

                    End If

                ElseIf strNamaMP = "BAHASA INGGERIS" Then

                    Dim BM_BahasaInggeris As Integer
                    Dim AM_BahasaInggeris As Integer
                    Dim BerterusanBI As Integer
                    Dim AkhiranBI As Integer
                    Dim B_BahasaInggeris As Double
                    Dim A_BahasaInggeris As Double
                    Dim PointerBI As Integer

                    'strSQL = "Select PB from kpmkv_matapelajaran Where KodMataPelajaran LIKE '%A02'+'" & strKodMP & "%' AND Tahun='" & ddlTahun.Text & "'"
                    strSQL = "SELECT PB FROM kpmkv_aka_weightage WHERE Tahun='" & Tahun & "'"
                    BerterusanBI = oCommon.getFieldValue(strSQL)

                    'strSQL = "Select PA from kpmkv_matapelajaran Where KodMataPelajaran LIKE '%A02'+'" & strKodMP & "%' AND Tahun='" & ddlTahun.Text & "'"
                    strSQL = "SELECT PA FROM kpmkv_aka_weightage WHERE Tahun='" & Tahun & "'"
                    AkhiranBI = oCommon.getFieldValue(strSQL)

                    strSQL = "Select B_BahasaInggeris from kpmkv_pelajar_markah Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                    B_BahasaInggeris = oCommon.getFieldValue(strSQL)
                    'round up
                    B_BahasaInggeris = Math.Ceiling(B_BahasaInggeris)

                    strSQL = "Select A_BahasaInggeris from kpmkv_pelajar_markah Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                    A_BahasaInggeris = oCommon.getFieldValue(strSQL)
                    'round up
                    A_BahasaInggeris = Math.Ceiling(A_BahasaInggeris)

                    'checkin Markah
                    If Not (B_BahasaInggeris) = "-1" And Not (A_BahasaInggeris) = "-1" Then
                        BM_BahasaInggeris = Math.Ceiling((B_BahasaInggeris / 100) * BerterusanBI)
                        AM_BahasaInggeris = Math.Ceiling((A_BahasaInggeris / 100) * AkhiranBI)
                        PointerBI = Math.Ceiling(BM_BahasaInggeris + AM_BahasaInggeris)
                        strSQL = "UPDATE kpmkv_pelajar_markah SET BahasaInggeris='" & PointerBI & "' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)

                    ElseIf (B_BahasaInggeris) = "-1" Or (A_BahasaInggeris) = "-1" Then
                        strSQL = "UPDATE kpmkv_pelajar_markah SET BahasaInggeris='-1' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)
                    End If

                ElseIf strNamaMP = "SAINS" Or strNamaMP = "SAINS UNTUK TEKNOLOGI" Or strNamaMP = "SAINS UNTUK PENGAJIAN SOSIAL" Then

                    Dim BM_Science1 As Integer
                    Dim AM_Science1 As Integer
                    Dim AM_Science2 As Integer
                    Dim BerterusanSc As Integer
                    Dim AkhiranSc As Integer
                    Dim B_Science1 As Double
                    Dim A_Science1 As Double
                    Dim A_Science2 As Double
                    Dim PointerSC1 As Integer
                    Dim PointerSC2 As Integer
                    Dim PointerSC As Integer
                    'Dim GredSC As Integer 

                    'strSQL = "Select PB from kpmkv_matapelajaran Where KodMataPelajaran LIKE '%A04'+'" & strKodMP & "%' AND Tahun='" & ddlTahun.Text & "'"
                    strSQL = "SELECT PB FROM kpmkv_aka_weightage WHERE Tahun='" & Tahun & "'"
                    BerterusanSc = oCommon.getFieldValue(strSQL)

                    'strSQL = "Select PA from kpmkv_matapelajaran Where KodMataPelajaran LIKE '%A04'+'" & strKodMP & "%'AND Tahun='" & ddlTahun.Text & "'"
                    strSQL = "SELECT PA FROM kpmkv_aka_weightage WHERE Tahun='" & Tahun & "'"
                    AkhiranSc = oCommon.getFieldValue(strSQL)

                    strSQL = "Select B_Science1 from kpmkv_pelajar_markah Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                    B_Science1 = oCommon.getFieldValue(strSQL)
                    'round up
                    B_Science1 = Math.Ceiling(B_Science1)

                    strSQL = "Select A_Science1 from kpmkv_pelajar_markah Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                    A_Science1 = oCommon.getFieldValue(strSQL)
                    'round up
                    A_Science1 = Math.Ceiling(A_Science1)

                    strSQL = "Select A_Science2 from kpmkv_pelajar_markah Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                    A_Science2 = oCommon.getFieldValue(strSQL)
                    'round up
                    A_Science2 = Math.Ceiling(A_Science2)

                    'check sem 3 n 4 ada  kertas 1
                    BM_Science1 = Math.Ceiling((B_Science1 / 100) * BerterusanSc)

                    If ddlSemester.Text = "1" Or ddlSemester.Text = "2" Then

                        If Not (A_Science1) = "-1" And Not (A_Science2) = "-1" Then
                            AM_Science1 = Math.Ceiling((A_Science1 / 100) * 50) '50%

                            AM_Science2 = Math.Ceiling((A_Science2 / 100) * 20) '20% 

                            PointerSC1 = Math.Ceiling(BM_Science1)
                            PointerSC2 = Math.Ceiling((AM_Science1) + (AM_Science2))
                            PointerSC = Math.Ceiling((PointerSC1) + (PointerSC2))

                            strSQL = "UPDATE kpmkv_pelajar_markah SET Science='" & PointerSC & "' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)
                        ElseIf (A_Science1) = "-1" Or (A_Science2) = "-1" Then
                            strSQL = "UPDATE kpmkv_pelajar_markah SET Science='-1' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)
                        End If
                    Else

                        If Not (A_Science1) = "-1" And Not (A_Science2) = "-1" Then
                            AM_Science1 = Math.Ceiling((A_Science1 / 100) * 70) '70%
                            AM_Science2 = Math.Ceiling((A_Science2 / 100) * 70) '70%
                            PointerSC1 = Math.Ceiling(BM_Science1)
                            PointerSC2 = Math.Ceiling((AM_Science1) + (AM_Science2))
                            PointerSC = Math.Ceiling((PointerSC1) + (PointerSC2))

                            strSQL = "UPDATE kpmkv_pelajar_markah SET Science='" & PointerSC & "' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)
                        ElseIf (A_Science1) = "-1" Or (A_Science2) = "-1" Then
                            strSQL = "UPDATE kpmkv_pelajar_markah SET Science='-1' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)
                        End If

                    End If

                ElseIf strNamaMP = "SEJARAH" Then

                    Dim BM_Sejarah As Integer
                    Dim AM_Sejarah As Integer
                    Dim BerterusanSJ As Integer
                    Dim AkhiranSJ As Integer
                    Dim B_Sejarah As Double
                    Dim A_Sejarah As Double
                    Dim PointerSJ As Integer
                    'Dim GredSJ As Integer 

                    'strSQL = "Select PB from kpmkv_matapelajaran Where KodMataPelajaran LIKE '%A05'+'" & strKodMP & "%' AND Tahun='" & ddlTahun.Text & "'"
                    strSQL = "SELECT PB FROM kpmkv_aka_weightage WHERE Tahun='" & Tahun & "'"
                    BerterusanSJ = oCommon.getFieldValue(strSQL)

                    'strSQL = "Select PA from kpmkv_matapelajaran Where KodMataPelajaran LIKE '%A05'+'" & strKodMP & "%' AND Tahun='" & ddlTahun.Text & "'"
                    strSQL = "SELECT PA FROM kpmkv_aka_weightage WHERE Tahun='" & Tahun & "'"
                    AkhiranSJ = oCommon.getFieldValue(strSQL)

                    strSQL = "Select B_Sejarah from kpmkv_pelajar_markah Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                    B_Sejarah = oCommon.getFieldValue(strSQL)
                    'round up
                    B_Sejarah = Math.Ceiling(B_Sejarah)

                    strSQL = "Select A_Sejarah from kpmkv_pelajar_markah Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                    A_Sejarah = oCommon.getFieldValue(strSQL)
                    'round up
                    A_Sejarah = Math.Ceiling(A_Sejarah)

                    'checkin Markah
                    If Not (B_Sejarah) = "-1" And Not (A_Sejarah) = "-1" Then
                        BM_Sejarah = Math.Ceiling((B_Sejarah / 100) * BerterusanSJ)
                        AM_Sejarah = Math.Ceiling((A_Sejarah / 100) * AkhiranSJ)
                        PointerSJ = Math.Ceiling(BM_Sejarah + AM_Sejarah)
                        strSQL = "UPDATE kpmkv_pelajar_markah SET Sejarah='" & PointerSJ & "' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)

                    ElseIf (B_Sejarah) = "-1" Or (A_Sejarah) = "-1" Then
                        strSQL = "UPDATE kpmkv_pelajar_markah SET Sejarah='-1' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)
                    End If

                    If ddlSemester.Text = "4" Then

                        Dim SJ1 As Integer
                        Dim SJ2 As Integer
                        Dim SJ3 As Integer
                        Dim SJ4 As Integer
                        Dim PointerSJSetara As Integer

                        ''get MYKAD pelajar
                        strSQL = "SELECT MYKAD FROM kpmkv_pelajar WHERE PelajarID = '" & datRespondent.DataKeys(i).Value.ToString & "'"
                        Dim strMYKADSJ As String = oCommon.getFieldValue(strSQL)

                        ''get pelajarid sem1
                        strSQL = "  SELECT PelajarID FROM kpmkv_pelajar WHERE MYKAD = '" & strMYKADSJ & "' AND Semester = '1'"
                        Dim strPelajarID1 As String = oCommon.getFieldValue(strSQL)

                        ''get pelajarid sem2
                        strSQL = "  SELECT PelajarID FROM kpmkv_pelajar WHERE MYKAD = '" & strMYKADSJ & "' AND Semester = '2'"
                        Dim strPelajarID2 As String = oCommon.getFieldValue(strSQL)

                        ''get pelajarid sem3
                        strSQL = "  SELECT PelajarID FROM kpmkv_pelajar WHERE MYKAD = '" & strMYKADSJ & "' AND Semester = '3'"
                        Dim strPelajarID3 As String = oCommon.getFieldValue(strSQL)

                        ''get pelajarid sem4
                        strSQL = "  SELECT PelajarID FROM kpmkv_pelajar WHERE MYKAD = '" & strMYKADSJ & "' AND Semester = '4'"
                        Dim strPelajarID4 As String = oCommon.getFieldValue(strSQL)


                        strSQL = "  SELECT Sejarah FROM kpmkv_pelajar_markah WHERE PelajarID = '" & strPelajarID1 & "' AND Semester = '1'"
                        SJ1 = oCommon.getFieldValue(strSQL)
                        SJ1 = Math.Ceiling((10 / 100) * Double.Parse(SJ1))

                        strSQL = "  SELECT Sejarah FROM kpmkv_pelajar_markah WHERE PelajarID = '" & strPelajarID2 & "' AND Semester = '2'"
                        SJ2 = oCommon.getFieldValue(strSQL)
                        SJ2 = Math.Ceiling((10 / 100) * Double.Parse(SJ2))

                        strSQL = "  SELECT Sejarah FROM kpmkv_pelajar_markah WHERE PelajarID = '" & strPelajarID3 & "' AND Semester = '3'"
                        SJ3 = oCommon.getFieldValue(strSQL)
                        SJ3 = Math.Ceiling((10 / 100) * Double.Parse(SJ3))

                        strSQL = "  SELECT Sejarah FROM kpmkv_pelajar_markah WHERE PelajarID = '" & strPelajarID4 & "' AND Semester = '4'"
                        SJ4 = oCommon.getFieldValue(strSQL)
                        SJ4 = Math.Ceiling((70 / 100) * Double.Parse(SJ4))

                        PointerSJSetara = Integer.Parse(SJ1) + Integer.Parse(SJ2) + Integer.Parse(SJ3) + Integer.Parse(SJ4)

                        strSQL = "UPDATE kpmkv_pelajar_markah SET PointerSJSetara='" & PointerSJSetara & "' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)

                    End If

                ElseIf strNamaMP = "PENDIDIKAN ISLAM" Then

                    Dim BM_PendidikanIslam1 As Integer
                    Dim BerterusanPI As Integer
                    Dim AkhiranPI As Integer
                    Dim B_PendidikanIslam1 As Integer
                    Dim A_PendidikanIslam1 As Integer
                    Dim A_PendidikanIslam2 As Integer
                    Dim PointerPI1 As Integer
                    Dim PointerPI2 As Integer
                    Dim PointerPI As Integer
                    ' Dim GredPI As Integer 

                    'strSQL = "Select PB from kpmkv_matapelajaran Where KodMataPelajaran LIKE '%A06'+'" & strKodMP & "%' AND Tahun='" & ddlTahun.Text & "'"
                    strSQL = "SELECT PB FROM kpmkv_aka_weightage WHERE Tahun='" & Tahun & "'"
                    BerterusanPI = oCommon.getFieldValue(strSQL)

                    'strSQL = "Select PA from kpmkv_matapelajaran Where KodMataPelajaran LIKE '%A06'+'" & strKodMP & "%' AND Tahun='" & ddlTahun.Text & "'"
                    strSQL = "SELECT PA FROM kpmkv_aka_weightage WHERE Tahun='" & Tahun & "'"
                    AkhiranPI = oCommon.getFieldValue(strSQL)

                    strSQL = "Select B_PendidikanIslam1 from kpmkv_pelajar_markah Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                    B_PendidikanIslam1 = oCommon.getFieldValue(strSQL)
                    'round up
                    B_PendidikanIslam1 = Math.Ceiling(B_PendidikanIslam1)

                    strSQL = "Select A_PendidikanIslam1 from kpmkv_pelajar_markah Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                    A_PendidikanIslam1 = oCommon.getFieldValue(strSQL)
                    'round up
                    A_PendidikanIslam1 = Math.Ceiling(A_PendidikanIslam1)

                    strSQL = "Select A_PendidikanIslam2 from kpmkv_pelajar_markah Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                    A_PendidikanIslam2 = oCommon.getFieldValue(strSQL)
                    'round up
                    A_PendidikanIslam2 = Math.Ceiling(A_PendidikanIslam2)

                    BM_PendidikanIslam1 = Math.Ceiling((B_PendidikanIslam1 / 100) * BerterusanPI)

                    If Not (A_PendidikanIslam1) = "-1" And Not (A_PendidikanIslam2) = "-1" Then
                        A_PendidikanIslam1 = Math.Ceiling((A_PendidikanIslam1 / 100) * 50) '50%
                        A_PendidikanIslam2 = Math.Ceiling((A_PendidikanIslam2 / 100) * 20) '20%

                        PointerPI1 = Math.Ceiling(BM_PendidikanIslam1)
                        PointerPI2 = Math.Ceiling(A_PendidikanIslam1 + A_PendidikanIslam2)
                        PointerPI = Math.Ceiling(PointerPI1 + PointerPI2)

                        strSQL = "UPDATE kpmkv_pelajar_markah SET PendidikanIslam='" & PointerPI & "' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)
                    ElseIf (A_PendidikanIslam1) = "-1" Or (A_PendidikanIslam2) = "-1" Then
                        strSQL = "UPDATE kpmkv_pelajar_markah SET PendidikanIslam='-1' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)
                    End If

                ElseIf strNamaMP = "PENDIDIKAN MORAL" Then

                    Dim BM_PendidikanMoral As Integer
                    Dim AM_PendidikanMoral As Integer
                    Dim BerterusanPM As Integer
                    Dim AkhiranPM As Integer
                    Dim B_PendidikanMoral As Integer
                    Dim A_PendidikanMoral As Integer
                    Dim PointerPM As Integer
                    'Dim GredPM As Integer 

                    'strSQL = "Select PB from kpmkv_matapelajaran Where KodMataPelajaran LIKE '%A07'+'" & strKodMP & "%' AND Tahun='" & ddlTahun.Text & "'"
                    strSQL = "SELECT PB FROM kpmkv_aka_weightage WHERE Tahun='" & Tahun & "'"
                    BerterusanPM = oCommon.getFieldValue(strSQL)

                    'strSQL = "Select PA from kpmkv_matapelajaran Where KodMataPelajaran LIKE '%A07'+'" & strKodMP & "%' AND Tahun='" & ddlTahun.Text & "'"
                    strSQL = "SELECT PA FROM kpmkv_aka_weightage WHERE Tahun='" & Tahun & "'"
                    AkhiranPM = oCommon.getFieldValue(strSQL)

                    strSQL = "Select B_PendidikanMoral from kpmkv_pelajar_markah Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                    B_PendidikanMoral = oCommon.getFieldValue(strSQL)
                    'round up
                    B_PendidikanMoral = Math.Ceiling(B_PendidikanMoral)

                    strSQL = "Select A_PendidikanMoral from kpmkv_pelajar_markah Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                    A_PendidikanMoral = oCommon.getFieldValue(strSQL)
                    'round up
                    A_PendidikanMoral = Math.Ceiling(A_PendidikanMoral)

                    'checkin Markah
                    If Not (B_PendidikanMoral) = "-1" And Not (A_PendidikanMoral) = "-1" Then
                        BM_PendidikanMoral = Math.Ceiling((B_PendidikanMoral / 100) * BerterusanPM)
                        AM_PendidikanMoral = Math.Ceiling((A_PendidikanMoral / 100) * AkhiranPM)
                        PointerPM = Math.Ceiling(BM_PendidikanMoral + AM_PendidikanMoral)
                        strSQL = "UPDATE kpmkv_pelajar_markah SET PendidikanMoral='" & PointerPM & "' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)
                    ElseIf (B_PendidikanMoral) = "-1" Or (A_PendidikanMoral) = "-1" Then
                        strSQL = "UPDATE kpmkv_pelajar_markah SET PendidikanMoral='-1' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)
                    End If

                ElseIf strNamaMP = "MATEMATIK" Or strNamaMP = "MATEMATIK UNTUK TEKNOLOGI" Or strNamaMP = "MATEMATIK UNTUK PENGAJIAN SOSIAL" Then

                    Dim BM_Mathematics As Integer
                    Dim AM_Mathematics As Integer
                    Dim BerterusanMT As Integer
                    Dim AkhiranMT As Integer
                    Dim B_Mathematics As Integer
                    Dim A_Mathematics As Integer
                    Dim PointerMT As Integer
                    'Dim GredMT As Integer 

                    'strSQL = "Select PB from kpmkv_matapelajaran Where KodMataPelajaran LIKE '%A03'+'" & strKodMP & "%' AND Tahun='" & ddlTahun.Text & "'"
                    strSQL = "SELECT PB FROM kpmkv_aka_weightage WHERE Tahun='" & Tahun & "'"
                    BerterusanMT = oCommon.getFieldValue(strSQL)

                    'strSQL = "Select PA from kpmkv_matapelajaran Where KodMataPelajaran LIKE '%A03'+'" & strKodMP & "%' AND Tahun='" & ddlTahun.Text & "'"
                    strSQL = "SELECT PA FROM kpmkv_aka_weightage WHERE Tahun='" & Tahun & "'"
                    AkhiranMT = oCommon.getFieldValue(strSQL)

                    strSQL = "Select B_Mathematics from kpmkv_pelajar_markah Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                    B_Mathematics = oCommon.getFieldValue(strSQL)
                    'round up
                    B_Mathematics = Math.Ceiling(B_Mathematics)

                    strSQL = "Select A_Mathematics from kpmkv_pelajar_markah Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                    A_Mathematics = oCommon.getFieldValue(strSQL)
                    'round up
                    A_Mathematics = Math.Ceiling(A_Mathematics)

                    'checkin Markah
                    If Not (B_Mathematics) = "-1" And Not (A_Mathematics) = "-1" Then
                        BM_Mathematics = Math.Ceiling((B_Mathematics / 100) * BerterusanMT)
                        AM_Mathematics = Math.Ceiling((A_Mathematics / 100) * AkhiranMT)
                        PointerMT = Math.Ceiling(BM_Mathematics + AM_Mathematics)
                        strSQL = "UPDATE kpmkv_pelajar_markah SET Mathematics='" & PointerMT & "' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)
                    ElseIf (B_Mathematics) = "-1" Or (A_Mathematics) = "-1" Then
                        strSQL = "UPDATE kpmkv_pelajar_markah SET Mathematics='-1' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)
                    End If

                End If
            Next

        End If


    End Sub
    Private Sub Akademik_gred()

        If datRespondent.Rows.Count = 1 Then

            For i As Integer = 0 To datRespondent.Rows.Count - 1
                strSQL = "SELECT NamaMatapelajaran FROM kpmkv_matapelajaran WHERE KodMataPelajaran = '" & ddlMatapelajaran.SelectedValue & "'"
                Dim strNamaMP As String = oCommon.getFieldValue(strSQL)

                If strNamaMP = "BAHASA MELAYU" Then

                    Dim BM As String
                    Dim GredBM As String

                    strSQL = "SELECT BahasaMelayu as BM FROM kpmkv_pelajar_markah"
                    strSQL += " Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                    BM = oCommon.getFieldValue(strSQL)

                    If String.IsNullOrEmpty(BM) Then
                        strSQL = "UPDATE kpmkv_pelajar_markah SET GredBM='' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)

                    Else
                        strSQL = "SELECT TOP ( 1 ) Gred FROM  kpmkv_gred WHERE '" & Integer.Parse(BM) & "' BETWEEN MarkahFrom AND MarkahTo AND Jenis='AKADEMIK'"
                        GredBM = oCommon.getFieldValue(strSQL)

                        strSQL = "UPDATE kpmkv_pelajar_markah SET GredBM='" & GredBM & "' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)

                    End If

                ElseIf strNamaMP = "BAHASA INGGERIS" Then

                    Dim BI As String
                    Dim GredBI As String

                    strSQL = "SELECT BahasaInggeris FROM kpmkv_pelajar_markah"
                    strSQL += " Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                    BI = oCommon.getFieldValue(strSQL)

                    'If BI = "0" Then
                    If String.IsNullOrEmpty(BI) Then
                    Else
                        strSQL = "SELECT TOP ( 1 ) Gred FROM  kpmkv_gred WHERE '" & Integer.Parse(BI) & "' BETWEEN MarkahFrom AND MarkahTo AND Jenis='AKADEMIK'"
                        GredBI = oCommon.getFieldValue(strSQL)

                        strSQL = "UPDATE kpmkv_pelajar_markah SET GredBI='" & GredBI & "' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)

                    End If

                ElseIf strNamaMP = "SAINS" Or strNamaMP = "SAINS UNTUK TEKNOLOGI" Or strNamaMP = "SAINS UNTUK PENGAJIAN SOSIAL" Then

                    Dim SC As Integer
                    Dim GredSC As String

                    strSQL = "SELECT Science FROM kpmkv_pelajar_markah"
                    strSQL += " Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                    SC = oCommon.getFieldValue(strSQL)

                    'If SC = 0 Then
                    If String.IsNullOrEmpty(SC) Then
                    Else
                        strSQL = "SELECT TOP ( 1 ) Gred FROM  kpmkv_gred WHERE '" & SC & "' BETWEEN MarkahFrom AND MarkahTo AND Jenis='AKADEMIK'"
                        GredSC = oCommon.getFieldValue(strSQL)

                        strSQL = "UPDATE kpmkv_pelajar_markah SET GredSC='" & GredSC & "' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)

                    End If

                ElseIf strNamaMP = "SEJARAH" Then

                    Dim SJ As String
                    Dim GredSJ As String

                    strSQL = "SELECT Sejarah FROM kpmkv_pelajar_markah"
                    strSQL += " Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                    SJ = oCommon.getFieldValue(strSQL)

                    'If SJ = "0" Then
                    If String.IsNullOrEmpty(SJ) Then
                    Else
                        strSQL = "SELECT TOP ( 1 ) Gred FROM  kpmkv_gred WHERE '" & Integer.Parse(SJ) & "' BETWEEN MarkahFrom AND MarkahTo AND Jenis='AKADEMIK'"
                        GredSJ = oCommon.getFieldValue(strSQL)

                        strSQL = "UPDATE kpmkv_pelajar_markah SET GredSJ='" & GredSJ & "' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)

                    End If

                ElseIf strNamaMP = "PENDIDIKAN ISLAM" Then

                    Dim PI As String
                    Dim GredPI As String

                    strSQL = "SELECT PendidikanIslam FROM kpmkv_pelajar_markah"
                    strSQL += " Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                    PI = oCommon.getFieldValue(strSQL)

                    If PI = "0" Then
                    ElseIf String.IsNullOrEmpty(PI) Then
                    Else
                        strSQL = "SELECT TOP ( 1 ) Gred FROM  kpmkv_gred WHERE '" & Integer.Parse(PI) & "' BETWEEN MarkahFrom AND MarkahTo AND Jenis='AKADEMIK'"
                        GredPI = oCommon.getFieldValue(strSQL)

                        strSQL = "UPDATE kpmkv_pelajar_markah SET GredPI='" & GredPI & "' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)

                    End If

                ElseIf strNamaMP = "PENDIDIKAN MORAL" Then

                    Dim PM As String
                    Dim GredPM As String

                    strSQL = "SELECT PendidikanMoral FROM kpmkv_pelajar_markah"
                    strSQL += " Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                    PM = oCommon.getFieldValue(strSQL)

                    If PM = "0" Then
                    ElseIf String.IsNullOrEmpty(PM) Then
                    Else
                        strSQL = "SELECT TOP ( 1 ) Gred FROM  kpmkv_gred WHERE '" & Integer.Parse(PM) & "' BETWEEN MarkahFrom AND MarkahTo AND Jenis='AKADEMIK'"
                        GredPM = oCommon.getFieldValue(strSQL)

                        strSQL = "UPDATE kpmkv_pelajar_markah SET GredPM='" & GredPM & "' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)

                    End If

                ElseIf strNamaMP = "MATEMATIK" Or strNamaMP = "MATEMATIK UNTUK TEKNOLOGI" Or strNamaMP = "MATEMATIK UNTUK PENGAJIAN SOSIAL" Then

                    Dim MT As String
                    Dim GredMT As String

                    strSQL = "SELECT Mathematics FROM kpmkv_pelajar_markah"
                    strSQL += " Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                    MT = oCommon.getFieldValue(strSQL)

                    'If MT = "0" Then
                    If String.IsNullOrEmpty(MT) Then
                    Else
                        strSQL = "SELECT TOP ( 1 ) Gred FROM  kpmkv_gred WHERE '" & Integer.Parse(MT) & "' BETWEEN MarkahFrom AND MarkahTo AND Jenis='AKADEMIK'"
                        GredMT = oCommon.getFieldValue(strSQL)

                        strSQL = "UPDATE kpmkv_pelajar_markah SET GredMT='" & GredMT & "' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)
                    End If

                End If

            Next

        End If



    End Sub

    Private Sub ddlTahun_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTahun.SelectedIndexChanged

        kpmkv_matapelajaran_list()
        ddlMatapelajaran.SelectedIndex = 0

        kpmkv_kodkursus_list()

        kpmkv_kelas_list()

    End Sub

    Private Sub ddlSemester_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSemester.SelectedIndexChanged

        kpmkv_matapelajaran_list()
        ddlMatapelajaran.SelectedIndex = 0

        kpmkv_kodkursus_list()

        kpmkv_kelas_list()

    End Sub

    Private Sub ddlNegeri_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlNegeri.SelectedIndexChanged

        'kpmkv_jenis_list()
        'ddlJenis.Text = "0"

        kpmkv_kolej_list()
        ddlKolej.Text = "0"

        'kpmkv_tahun_list()
        'ddlTahun.Text = Now.Year

        'kpmkv_semester_list()

        kpmkv_matapelajaran_list()

        kpmkv_kodkursus_list()

        kpmkv_kelas_list()

    End Sub

    Private Sub ddlKolej_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlKolej.SelectedIndexChanged

        kpmkv_matapelajaran_list()

        kpmkv_kodkursus_list()

        kpmkv_kelas_list()

    End Sub
End Class

