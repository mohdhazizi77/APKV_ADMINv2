Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Public Class pembetulan_markah_BM1

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
                If Not Session("Negeri") = "" Then
                    ddlNegeri.Text = Session("Negeri")
                Else
                    ddlNegeri.Text = "0"
                End If

                kpmkv_jenis_list()
                ddlJenis.Text = "0"

                kpmkv_kolej_list()
                ddlKolej.Text = "0"

                kpmkv_tahun_list()
                ddlTahun.Text = "-Pilih-"

                kpmkv_semester_list()

                kpmkv_kodkursus_list()

                kpmkv_kelas_list()
                'kpmkv_jeniscalon_list()
                'ddlJenisCalon.Text = "AKTIF"

                chkSesi.SelectedIndex = 0

                lblMsg.Text = ""
                'strRet = BindData(datRespondent)

            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message
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
        strSQL = "SELECT Semester FROM kpmkv_semester WHERE Semester = '4' ORDER BY Semester"
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
        strSQL += " WHERE kpmkv_kelas.KolejRecordID='" & ddlKolej.SelectedValue & "' AND kpmkv_kursus.Tahun='" & ddlTahun.Text & "' AND kpmkv_kursus.Sesi='" & chkSesi.SelectedValue & "' GROUP BY kpmkv_kursus.KodKursus, kpmkv_kursus.KursusID ORDER BY kpmkv_kursus.KodKursus"
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

            ddlKelas.Items.Add(New ListItem("-Pilih-", "0"))

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub

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

        Dim tmpSQL As String = ""
        Dim strWhere As String = ""
        Dim strOrder As String = " ORDER BY kpmkv_pelajar.Nama ASC"

        '--not deleted
        tmpSQL = "SELECT kpmkv_pelajar.PelajarID, kpmkv_pelajar.Tahun, kpmkv_pelajar.Semester, kpmkv_pelajar.Sesi, kpmkv_pelajar.Nama, kpmkv_pelajar.AngkaGiliran, "
        tmpSQL += " kpmkv_pelajar.MYKAD, kpmkv_kursus.KodKursus, kpmkv_pelajar_markah.A_BahasaMelayu1, kpmkv_pelajar_markah.A_BahasaMelayu2, kpmkv_pelajar_markah.A_BahasaMelayu3 "
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
        If Not ddlTahun.SelectedItem.Text = "-Pilih-" Then
            strWhere += " AND kpmkv_pelajar.Tahun ='" & ddlTahun.Text & "'"
        End If
        '--sesi
        If Not chkSesi.Text = "" Then
            strWhere += " AND kpmkv_pelajar.Sesi ='" & chkSesi.Text & "'"
        End If
        '--semester
        If Not ddlSemester.Text = "-Pilih-" Then
            strWhere += " AND kpmkv_pelajar.Semester ='" & ddlSemester.Text & "'"
        End If
        '--Kod
        If Not ddlKodKursus.SelectedItem.Text = "-Pilih-" Then
            strWhere += " AND kpmkv_pelajar.KursusID='" & ddlKodKursus.SelectedValue & "'"
        End If
        '--sesi
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
        ''Response.Write(getSQL)

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
                    AND A.Menu = 'PA AKADEMIK'
                    ORDER BY A.dateTime DESC"

        getSQLAudit = tmpSQL
        Debug.WriteLine(getSQLAudit)

        Return getSQLAudit

    End Function

    Private Sub datRespondent_SelectedIndexChanging(sender As Object, e As GridViewSelectEventArgs) Handles datRespondent.SelectedIndexChanging
        Dim strKeyID As String = datRespondent.DataKeys(e.NewSelectedIndex).Value.ToString

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

    Private Sub chkSesi_SelectedIndexChanged(sender As Object, e As EventArgs) Handles chkSesi.SelectedIndexChanged
        kpmkv_kodkursus_list()
        kpmkv_kelas_list()

    End Sub

    Private Sub ddlNegeri_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlNegeri.SelectedIndexChanged
        kpmkv_jenis_list()
        ddlJenis.Text = "0"
    End Sub

    Protected Sub ddlJenis_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlJenis.SelectedIndexChanged
        kpmkv_kolej_list()
        ddlKolej.Text = "0"
    End Sub

    Private Sub ddlKolej_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlKolej.SelectedIndexChanged

        kpmkv_kodkursus_list()
        ddlKodKursus.Text = "0"
    End Sub

    Private Sub ddlTahun_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTahun.SelectedIndexChanged
        kpmkv_semester_list()
        kpmkv_kodkursus_list()
        ddlKodKursus.Text = "0"
        kpmkv_kelas_list()
        ddlKelas.Text = "0"
    End Sub

    Private Sub ddlSemester_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSemester.SelectedIndexChanged
        kpmkv_kodkursus_list()
        ddlKodKursus.Text = "0"
        kpmkv_kelas_list()
        ddlKelas.Text = "0"
    End Sub
    Private Sub ddlKodKursus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlKodKursus.SelectedIndexChanged
        kpmkv_kelas_list()
        ddlKelas.Text = "0"
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click

        lblMsg.Text = ""

        Dim strNamaMP As String = ""

        Try

            If ValidateForm() = False Then
                lblMsgResult.Text = "Sila masukkan NOMBOR SAHAJA"
                lblMsg.Text = "Sila masukkan NOMBOR 0-100 SAHAJA"
                Exit Sub
            End If

            If datRespondent.Rows.Count = 1 Then

                For i As Integer = 0 To datRespondent.Rows.Count - 1

                    strSQL = "UPDATE kpmkv_pelajar_markah SET "

                    If ddlKertas.SelectedValue = 1 Then

                        strSQL += " A_BahasaMelayu1 = '" & txtMarkah.Text & "' "

                    Else

                        strSQL += " A_BahasaMelayu2 = '" & txtMarkah.Text & "' "

                    End If

                    strSQL += " WHERE PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                    strSQL += " AND Semester='" & ddlSemester.Text & "'"
                    strSQL += " AND Sesi='" & chkSesi.Text & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)
                    If strRet = "0" Then

                        divMsgResult.Attributes("class") = "info"
                        lblMsgResult.Text = "Kemaskini Berjaya!"

                        strRet = BindData(datRespondent)

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

    Private Sub btnGred_Click(sender As Object, e As EventArgs) Handles btnGred.Click

        Dim strPointer As String = ""
        Dim strGredBM As String = ""
        Dim strBMTahun As String = ""

        Try

            If datRespondent.Rows.Count = 1 Then

                For i As Integer = 0 To datRespondent.Rows.Count - 1

                    strSQL = "SELECT isBMTahun FROM kpmkv_pelajar WHERE PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                    strBMTahun = oCommon.getFieldValue(strSQL)

                    ''--Pointer BM 
                    'strSQL = "SELECT PointerBMSetara FROM kpmkv_pelajar_markah WHERE PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                    strPointer = oCommon.getFieldValue("SELECT PointerBMSetara FROM kpmkv_pelajar_markah WHERE PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'")
                    ''--Gred BM 
                    If strPointer = "-1" Then
                        strGredBM = "T"
                    ElseIf strPointer = "" Then
                        strGredBM = ""
                    Else
                        strSQL = "SELECT TOP ( 1 ) Gred FROM  kpmkv_gred_bmsetara WHERE '" & Integer.Parse(strPointer) & "' BETWEEN MarkahFrom AND MarkahTo AND Tahun='" & strBMTahun & "' AND Sesi='" & chkSesi.Text & "'"
                        strGredBM = oCommon.getFieldValue(strSQL)
                    End If

                    'change on 17082016
                    strSQL = "UPDATE kpmkv_pelajar_markah SET GredBMSetara='" & strGredBM & "' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)

                    If strRet = "0" Then

                        divMsgResult.Attributes("class") = "info"
                        lblMsgResult.Text = "Jana Gred Berjaya!"

                        strRet = BindData(datRespondent)

                    Else

                        divMsgResult.Attributes("class") = "error"
                        lblMsgResult.Text = "Tidak Berjaya Menjana Gred!"

                    End If

                Next

            Else

                divMsgResult.Attributes("class") = "error"
                lblMsgResult.Text = "Tidak Berjaya Menjana Gred!"

            End If

        Catch ex As Exception
            lblMsg.Text = "Error:" & ex.Message
        End Try

    End Sub

End Class