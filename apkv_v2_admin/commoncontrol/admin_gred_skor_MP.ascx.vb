Imports System.Data.SqlClient
Public Class admin_gred_skor_MP
    Inherits System.Web.UI.UserControl
    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strSql2 As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lblMsg.Text = ""

        Try
            If Not IsPostBack Then

                btnGred.Visible = False

                kpmkv_negeri_list()
                ddlNegeri.SelectedValue = ""

                kpmkv_kolej_list()
                ddlKolej.SelectedValue = ""

                kpmkv_kodkursus_list()
                ddlKodKursus.SelectedValue = ""

                kpmkv_tahun_list()
                ddlTahun.Text = Now.Year


                kpmkv_semester_list()

            End If

        Catch ex As Exception
            lblMsg.Text = "Error Message:" & ex.Message
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
            ddlNegeri.Items.Add(New ListItem("-Pilih-", ""))

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub kpmkv_kolej_list()
        strSQL = " SELECT b.nama as nama,a.KolejRecordID as ID"
        strSQL += " FROM kpmkv_pelajar_markah a, kpmkv_kolej b"
        strSQL += " WHERE a.KolejRecordID = b.RecordID"
        strSQL += " AND b.Negeri='" & ddlNegeri.Text & "'"
        strSQL += " AND a.SMP_PB='1' AND a.SMP_PAA='1' AND a.SMP_PAT='1' "
        strSQL += " AND a.Tahun ='" & ddlTahun.Text & "' AND a.Semester='" & ddlSemester.Text & "'"
        strSQL += " AND a.Sesi ='" & chkSesi.Text & "'"
        strSQL += " AND SMP_Grade IS NULL"
        strSQL += " GROUP BY a.KolejRecordID, b.nama"
        strSQL += " ORDER BY b.nama ASC"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlKolej.DataSource = ds
            ddlKolej.DataTextField = "nama"
            ddlKolej.DataValueField = "ID"
            ddlKolej.DataBind()


            '--ALL
            ddlKolej.Items.Add(New ListItem("-Pilih-", ""))

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub kpmkv_kodkursus_list()
        strSQL = " SELECT c.KodKursus as kodKursus, a.KursusID as KursusID"
        strSQL += " FROM kpmkv_pelajar_markah a, kpmkv_kolej b, kpmkv_kursus c "
        strSQL += " WHERE a.KolejRecordID = b.RecordID AND a.kursusID = c.kursusID"
        strSQL += " AND b.Negeri='" & ddlNegeri.Text & "'"
        strSQL += " AND a.KolejRecordID='" & oCommon.FixSingleQuotes(ddlKolej.SelectedValue) & "'"
        strSQL += " AND a.SMP_PB='1' AND a.SMP_PAA='1' AND a.SMP_PAT='1' "
        strSQL += " AND a.Tahun ='" & ddlTahun.Text & "' AND a.Semester='" & ddlSemester.Text & "'"
        strSQL += " AND a.Sesi ='" & chkSesi.Text & "'"
        strSQL += " AND SMP_Grade IS NULL"
        strSQL += " GROUP BY a.KursusID,c.KodKursus "
        strSQL += " ORDER BY c.KodKursus asc"
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
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        lblMsg.Text = ""
        strRet = BindData(datRespondent)
        kpmkv_kolej_list()
        ddlKolej.SelectedValue = ""
        Try

            getSQLPelajarTtl()
            If lblCountPelajar.Text = "0" Then
                divMsg.Attributes("class") = "error"
                lblMsg.Text = "Markah Pelajar tidak dikemaskini!"
                btnGred.Visible = False
            Else
                btnGred.Visible = True
            End If
        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message
        End Try
    End Sub
    Private Sub getSQL_SMP_PB()
        lblMsg.Text = ""
        Try
            strSQL = "UPDATE kpmkv_pelajar_markah SET SMP_Grade='G' WHERE (SMP_PB ='0' OR SMP_PAA ='0' OR SMP_PAT ='0' OR Gred_Kompeten='0')"
            strSQL += " AND Tahun ='" & ddlTahun.Text & "'"
            strSQL += " AND Semester='" & ddlSemester.Text & "'"
            strSQL += " AND Sesi ='" & chkSesi.Text & "'"

            If Not ddlKolej.SelectedValue = "" And Not ddlKodKursus.SelectedValue = "" Then
                strSQL += " AND KolejRecordID='" & oCommon.FixSingleQuotes(ddlKolej.SelectedValue) & "'"
                strSQL += " AND KursusID='" & oCommon.FixSingleQuotes(ddlKodKursus.SelectedValue) & "'"
            ElseIf ddlKodKursus.SelectedValue = "" Then
                divMsg.Attributes("class") = "error"
                lblMsg.Text = "Sila Pilih Kod Kursus"
            End If
            strRet = oCommon.ExecuteSQL(strSQL)
        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message
        End Try
    End Sub
    Private Sub getSQL_SMP_PAA()
        lblMsg.Text = ""
        Try
            strSQL = "UPDATE kpmkv_pelajar_markah SET SMP_Grade='T' WHERE SMP_Total ='-1'"
            strSQL += " AND Tahun ='" & ddlTahun.Text & "'"
            strSQL += " AND Semester='" & ddlSemester.Text & "'"
            strSQL += " AND Sesi ='" & chkSesi.Text & "'"
            If Not ddlKolej.SelectedValue = "" And Not ddlKodKursus.SelectedValue = "" Then
                strSQL += " AND KolejRecordID='" & oCommon.FixSingleQuotes(ddlKolej.SelectedValue) & "'"
                strSQL += " AND KursusID='" & oCommon.FixSingleQuotes(ddlKodKursus.SelectedValue) & "'"
            ElseIf ddlKodKursus.SelectedValue = "" Then
                divMsg.Attributes("class") = "error"
                lblMsg.Text = "Sila Pilih Kod Kursus"
            End If

            strRet = oCommon.ExecuteSQL(strSQL)
        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message
        End Try
    End Sub
    'Private Sub getSQL_SMP_PAT()
    '    lblMsg.Text = ""
    '    Try
    '        strSQL = "UPDATE kpmkv_pelajar_markah SET SMP_Grade='G' WHERE SMP_Total ='0'"
    '        strSQL += " AND Tahun ='" & ddlTahun.Text & "'"
    '        strSQL += " AND Semester='" & ddlSemester.Text & "'"
    '        strSQL += " AND Sesi ='" & chkSesi.Text & "'"
    '        strRet = oCommon.ExecuteSQL(strSQL)
    '    Catch ex As Exception
    '        lblMsg.Text = "System Error:" & ex.Message
    '    End Try
    'End Sub
    Private Function getSQL() As String
        Dim tmpSQL As String
        Dim strOrder As String = " ORDER BY PelajarID ASC"

        '--not deleted
        tmpSQL = "SELECT a.PelajarID FROM kpmkv_pelajar_markah a, kpmkv_kolej b"
        tmpSQL += " WHERE a.KolejRecordID=b.RecordID AND b.Negeri='" & ddlNegeri.Text & "'"
        tmpSQL += " AND a.SMP_PB='1' AND a.SMP_PAA='1' AND a.SMP_PAT='1'"
        tmpSQL += " AND a.Tahun ='" & ddlTahun.Text & "'"
        tmpSQL += " AND a.Semester='" & ddlSemester.Text & "'"
        tmpSQL += " AND a.Sesi ='" & chkSesi.Text & "'"
        If Not ddlKolej.SelectedValue = "" And Not ddlKodKursus.SelectedValue = "" Then
            tmpSQL += " AND KolejRecordID='" & oCommon.FixSingleQuotes(ddlKolej.SelectedValue) & "'"
            tmpSQL += " AND KursusID='" & oCommon.FixSingleQuotes(ddlKodKursus.SelectedValue) & "'"
        ElseIf ddlKodKursus.SelectedValue = "" Then
            divMsg.Attributes("class") = "error"
            lblMsg.Text = "Sila Pilih Kod Kursus"
        End If

        getSQL = tmpSQL & strOrder

        Return getSQL

    End Function
    Private Function getSQL_Gred() As Boolean
        lblMsg.Text = ""

        Dim IntID As Integer
        Dim SMP_Total As String
        Dim SMP_Grade As String
        Dim SMP_PB As String
        Dim SMP_PAA As String
        Dim SMP_PAT As String
        Dim Gred_Kompeten As String

        'Get the data from database into datatable 
        Dim cmd As New SqlCommand(getSQL)
        Dim dt As DataTable = GetData(cmd)

        For i As Integer = 0 To dt.Rows.Count - 1
            IntID = dt.Rows(i)("PelajarID")


            strSQL = "SELECT SMP_Total,SMP_PB,SMP_PAA,SMP_PAT,Gred_Kompeten FROM kpmkv_pelajar_markah WHERE PelajarID ='" & IntID & "'"

            strRet = oCommon.getFieldValueEx(strSQL)
            ' ''--get Pelajar info
            Dim ar_Detail As Array
            ar_Detail = strRet.Split("|")
            SMP_Total = ar_Detail(0)
            SMP_PB = ar_Detail(1)
            SMP_PAA = ar_Detail(2)
            SMP_PAT = ar_Detail(3)
            Gred_Kompeten = ar_Detail(4)

            If (SMP_PB = "0" Or SMP_PAA = "0" Or SMP_PAT = "0" Or Gred_Kompeten = "0") Then
                strSQL = " UPDATE kpmkv_pelajar_markah SET SMP_Grade='G' WHERE PelajarID ='" & IntID & "'"
                strRet = oCommon.ExecuteSQL(strSQL)
            Else

                strSQL = "SELECT TOP ( 1 ) Gred FROM  kpmkv_gred_vokasional WHERE '" & CInt(SMP_Total) & "' BETWEEN MarkahFrom AND MarkahTo AND Jenis='VOKASIONAL'"
                strSQL += " AND Tahun ='" & ddlTahun.Text & "'"
                strSQL += " AND Semester='" & ddlSemester.Text & "'"
                strSQL += " AND Sesi ='" & chkSesi.Text & "'"
                SMP_Grade = oCommon.getFieldValue(strSQL)

                strSQL = " UPDATE kpmkv_pelajar_markah SET SMP_Grade='" & SMP_Grade & "' WHERE PelajarID ='" & IntID & "'"
                strRet = oCommon.ExecuteSQL(strSQL)
            End If


            '--Debug
            'Response.Write(strSQL)
            If strRet = "0" Then
            Else
                lblMsg.Text = "System Error: Check PelajarID" & IntID
                Return False
                Exit Function
            End If

        Next

        Return True

    End Function
    Private Sub getSQLPelajarTtl()

        strSQL = " SELECT Count(a.PelajarID) AS COUNTP"
        strSQL += " FROM kpmkv_pelajar_markah a, kpmkv_kolej b"
        strSQL += " WHERE a.KolejRecordID=b.RecordID AND b.Negeri='" & ddlNegeri.Text & "'"
        strSQL += " AND a.Tahun ='" & ddlTahun.Text & "'"
        strSQL += " AND a.Semester='" & ddlSemester.Text & "'"
        strSQL += " AND a.Sesi ='" & chkSesi.Text & "'"
        strSQL += " AND a.SMP_PB='1' AND a.SMP_PAA='1' AND a.SMP_PAT='1'"

        lblCountPelajar.Text = oCommon.getFieldValue(strSQL)

    End Sub

    Private Sub getSQLCount()

        strSQL = " SELECT Count(a.PelajarID) AS COUNTP"
        strSQL += " FROM kpmkv_pelajar_markah a, kpmkv_kolej b"
        strSQL += " WHERE a.KolejRecordID=b.RecordID AND b.Negeri='" & ddlNegeri.Text & "'"
        strSQL += " AND a.SMP_Grade IS NOT NULL"
        strSQL += " AND a.Tahun ='" & ddlTahun.Text & "'"
        strSQL += " AND a.Semester='" & ddlSemester.Text & "'"
        strSQL += " AND a.Sesi ='" & chkSesi.Text & "'"
        strSQL += " AND a.SMP_PB='1' AND a.SMP_PAA='1' AND a.SMP_PAT='1'"

        lblCOUNTP.Text = oCommon.getFieldValue(strSQL)



    End Sub

    Private Function getSQL2() As String
        strSql2 = " SELECT max(b.nama) as namaKolej,c.KodKursus as kodKursus ,count(a.pelajarID) As jumlahPelajar,"
        strSql2 += " MAX(case when a.smp_Total Is Null THEN 'Tidak Berjaya' else '' end) smpTotal,"
        strSql2 += " MAX(case when a.smp_Grade Is Null THEN 'Tidak Berjaya' else '' end) SmpGrade"
        strSql2 += " FROM kpmkv_pelajar_markah a, kpmkv_kolej b, kpmkv_kursus c "
        strSql2 += " WHERE a.KolejRecordID = b.RecordID AND a.kursusID = c.kursusID"
        strSql2 += " AND b.Negeri='" & ddlNegeri.Text & "'"
        strSql2 += " AND a.SMP_PB='1' AND a.SMP_PAA='1' AND a.SMP_PAT='1' "
        strSql2 += " AND a.Tahun ='" & ddlTahun.Text & "' AND a.Semester='" & ddlSemester.Text & "'"
        strSql2 += " AND a.Sesi ='" & chkSesi.Text & "'"
        strSql2 += " AND SMP_Grade IS NULL"
        strSql2 += " GROUP BY b.nama, c.KodKursus "
        strSql2 += " ORDER BY namaKolej,c.KodKursus asc"

        getSQL2 = strSql2

        Return getSQL2
    End Function
    Private Function BindData(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL2, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 120

        Try
            myDataAdapter.Fill(myDataSet, "myaccount")

            If myDataSet.Tables(0).Rows.Count = 0 Then
            Else
            End If

            gvTable.DataSource = myDataSet
            gvTable.DataBind()
            objConn.Close()
        Catch ex As Exception
            Return False
        End Try

        Return True

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

    Protected Sub btnGred_Click(sender As Object, e As EventArgs) Handles btnGred.Click
        lblMsg.Text = ""
        Try

            getSQL_SMP_PB()
            getSQL_SMP_PAA()
            ' getSQL_SMP_PAT()


            If getSQL_Gred() = True Then
                getSQLCount()
                divMsg.Attributes("class") = "info"
                lblMsg.Text = "Jana Gred Pelajar telah Berjaya!."


                strRet = BindData(datRespondent)
                kpmkv_kolej_list()
                ddlKolej.SelectedValue = ""
                ddlKodKursus.SelectedValue = ""
            Else
                divMsg.Attributes("class") = "error"
            End If


            ''--debug
            'Response.Write(getSQL)
        Catch ex As Exception
            divMsg.Attributes("class") = "error"
            lblMsg.Text = "System Error:" & ex.Message & "<br> Sila Kemaskini markah di bahagian Pentaksiran Akhir Vokasional Pada SMP_Total yang tidak berjaya! "
        End Try
    End Sub

    Protected Sub btnJana_Click(sender As Object, e As EventArgs) Handles btnJana.Click
        lblMsg.Text = ""
        Try

            getSQL_SMP_PB()
            getSQL_SMP_PAA()



            If getSQL_Gred() = True Then
                getSQLCount()
                divMsg.Attributes("class") = "info"
                lblMsg.Text = "Jana Gred Pelajar telah Berjaya!."

                strRet = BindData(datRespondent)
                kpmkv_kolej_list()
                ddlKolej.SelectedValue = ""
                ddlKodKursus.SelectedValue = ""

            Else
                divMsg.Attributes("class") = "error"
            End If


            ''--debug
            'Response.Write(getSQL)
        Catch ex As Exception
            divMsg.Attributes("class") = "error"
            lblMsg.Text = "System Error:" & ex.Message & "<br> Sila Kemaskini markah di bahagian Pentaksiran Akhir Vokasional Pada SMP_Total yang tidak berjaya! "
        End Try
    End Sub
    Protected Sub ddlNegeri_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlNegeri.SelectedIndexChanged
        kpmkv_kolej_list()
    End Sub
    Protected Sub ddlKolej_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlKolej.SelectedIndexChanged
        kpmkv_kodkursus_list()

    End Sub
End Class