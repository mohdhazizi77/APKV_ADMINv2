Imports System.IO
Imports System.Data
Imports System.Drawing
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Globalization

Public Class jana_gred_BM_ulang1

    Inherits System.Web.UI.UserControl
    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim IntTakwim As Integer = 0

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                lblMsg.Text = ""

                kpmkv_negeri_list()
                kpmkv_jenis_list()
                kpmkv_kolej_list()

                kpmkv_tahun_list()
                ddlTahun.Text = Now.Year

            End If


        Catch ex As Exception
            lblMsg.Text = ex.Message
        End Try
    End Sub

    Private Sub kpmkv_negeri_list()
        strSQL = "SELECT Negeri FROM kpmkv_negeri  ORDER BY Negeri"
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
        strSQL = "SELECT Nama,RecordID FROM kpmkv_kolej  WHERE Negeri='" & ddlNegeri.SelectedItem.Value & "' AND Jenis='" & ddlJenis.SelectedValue & "'"
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
            ddlTahun.Items.Add(New ListItem("-Pilih-", "0"))

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Shared Function RepoveDuplicate(ByVal ddl As DropDownList) As DropDownList
        For Row As Int16 = 0 To ddl.Items.Count - 2
            For RowAgain As Int16 = ddl.Items.Count - 1 To Row + 1 Step -1
                If ddl.Items(Row).ToString = ddl.Items(RowAgain).ToString Then
                    ddl.Items.RemoveAt(RowAgain)
                End If
            Next
        Next
        Return ddl
    End Function

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
                lblMsg.Text = "Tiada rekod calon."
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

    'Private Sub statusDaftar()

    '    For i = 0 To datRespondent.Rows.Count - 1

    '        Dim strkey As String = datRespondent.DataKeys(i).Value.ToString

    '        ''get pengajian
    '        strSQL = " SELECT Pengajian FROM kpmkv_pelajar WHERE PelajarID = '" & strkey & "'"
    '        Dim strPengajian As String = oCommon.getFieldValue(strSQL)

    '        ''get namaMP
    '        strSQL = " SELECT NamaMataPelajaran FROM kpmkv_matapelajaran WHERE KodMataPelajaran = '" & ddlMatapelajaran.SelectedValue & "'"
    '        Dim strNamaMP As String = oCommon.getFieldValue(strSQL)

    '        strSQL = "  SELECT PelajarID FROM kpmkv_pelajar_ulang
    '                        WHERE
    '                        PelajarID = '" & strkey & "'
    '                        AND Tahun = '" & ddlTahun.Text & "'
    '                        AND Semester = '" & ddlSemester.Text & "'
    '                        AND Sesi = '" & chkSesi.Text & "'
    '                        AND KursusID = '" & ddlKodKursus.SelectedValue & "'
    '                        AND KelasID = '" & ddlKelas.SelectedValue & "'
    '                        AND NamaMataPelajaran = '" & strNamaMP & "'"

    '        strRet = oCommon.getFieldValue(strSQL)

    '        If Not strRet = "" Then

    '            Dim lblStatus As Label = datRespondent.Rows(i).FindControl("lblStatus")
    '            lblStatus.Text = "Telah Didaftarkan"

    '        End If

    '    Next

    'End Sub

    Private Function getSQL() As String

        Dim tmpSQL As String = ""
        Dim strWhere As String = ""
        Dim strOrder As String = " ORDER BY kpmkv_pelajar.Tahun, kpmkv_pelajar.Semester, kpmkv_pelajar.Sesi ASC"

        '--not deleted
        tmpSQL = "  SELECT
                    kpmkv_pelajar.PelajarID,
                    kpmkv_pelajar.Nama,
                    kpmkv_pelajar.MYKAD,
                    kpmkv_pelajar.AngkaGiliran,
                    kpmkv_pelajar_markah.GredBM,
                    kpmkv_pelajar_markah.BahasaMelayu,
                    kpmkv_pelajar_ulang.MarkahPA,
                    kpmkv_pelajar_ulang.Gred
                    FROM
                    kpmkv_pelajar
                    LEFT JOIN kpmkv_pelajar_markah ON kpmkv_pelajar_markah.PelajarID = kpmkv_pelajar.PelajarID
                    LEFT JOIN kpmkv_pelajar_ulang ON kpmkv_pelajar_ulang.PelajarID = kpmkv_pelajar.PelajarID
                    WHERE
                    kpmkv_pelajar_ulang.NamaMataPelajaran = 'BAHASA MELAYU'
                    AND kpmkv_pelajar.IsBMTahun = '" & ddlTahun.SelectedValue & "'"

        If Not ddlKolej.SelectedValue = "-Pilih-" Then

            tmpSQL += " AND kpmkv_pelajar.KolejRecordID = '" & ddlKolej.SelectedValue & "'"

        End If

        getSQL = tmpSQL
        ''--debug
        ''Response.Write(getSQL)

        Return getSQL

    End Function
    Private Sub datRespondent_SelectedIndexChanging(sender As Object, e As GridViewSelectEventArgs) Handles datRespondent.SelectedIndexChanging
        Dim strKeyID As String = datRespondent.DataKeys(e.NewSelectedIndex).Value.ToString

        Response.Redirect("pelajar.ulang.akademik.view.aspx?PelajarID=" & strKeyID)

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

    Private Sub tbl_menu_check()

        Dim str As String
        For i As Integer = 0 To datRespondent.Rows.Count - 1
            Dim row As GridViewRow = datRespondent.Rows(0)
            Dim cb As CheckBox = datRespondent.Rows(i).FindControl("chkSelect")

            str = datRespondent.DataKeys(i).Value.ToString
            Dim strMykad As String = CType(datRespondent.Rows(i).FindControl("Mykad"), Label).Text

            strSQL = "SELECT KelasID FROM kpmkv_pelajar Where Mykad='" & strMykad & "' AND IsDeleted='N' AND KelasID IS NOT NULL"
            If oCommon.isExist(strSQL) = False Then
                cb.Checked = True
            End If
        Next

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

    Private Sub ddlNegeri_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlNegeri.SelectedIndexChanged
        kpmkv_jenis_list()
        kpmkv_kolej_list()
        kpmkv_tahun_list()
        ddlTahun.Text = Now.Year
    End Sub

    Private Sub ddlJenis_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlJenis.SelectedIndexChanged
        kpmkv_kolej_list()
        kpmkv_tahun_list()
        ddlTahun.Text = Now.Year
    End Sub

    Private Sub ddlKolej_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlKolej.SelectedIndexChanged
        kpmkv_tahun_list()
        ddlTahun.Text = Now.Year
    End Sub







    'Private Sub btnDaftar_Click(sender As Object, e As EventArgs) Handles btnDaftar.Click

    '    ''get namaGred ex: GredBM, GredSJ
    '    strSQL = "SELECT PelajarMarkahGred FROM kpmkv_matapelajaran WHERE KodMataPelajaran = '" & ddlMatapelajaran.SelectedValue & "'"
    '    Dim namaGred As String = oCommon.getFieldValue(strSQL)

    '    '--not deleted
    '    strSQL = "SELECT kpmkv_pelajar.PelajarID, kpmkv_pelajar_markah.GredBM, kpmkv_pelajar_markah.GredBI, kpmkv_pelajar_markah.GredSC, kpmkv_pelajar_markah.GredSJ, "
    '    strSQL += " kpmkv_pelajar_markah.GredPI, kpmkv_pelajar_markah.GredPM, kpmkv_pelajar_markah.GredMT, kpmkv_pelajar.Nama, kpmkv_pelajar.MYKAD,"
    '    strSQL += " kpmkv_pelajar.AngkaGiliran, kpmkv_pelajar.Tahun, kpmkv_pelajar.Semester, kpmkv_pelajar.Sesi, kpmkv_kursus.KodKursus"
    '    strSQL += " FROM kpmkv_pelajar_markah LEFT OUTER JOIN  kpmkv_pelajar ON kpmkv_pelajar_markah.PelajarID = kpmkv_pelajar.PelajarID "
    '    strSQL += " LEFT OUTER JOIN kpmkv_kursus ON kpmkv_pelajar.KursusID = kpmkv_kursus.KursusID"
    '    strSQL += " WHERE kpmkv_pelajar.IsDeleted='N' AND kpmkv_pelajar.StatusID='2' AND kpmkv_pelajar.KolejRecordID='" & lblKolejID.Text & "'"
    '    strSQL += " AND kpmkv_pelajar_markah." & namaGred & " IN ('D','E','D-','D+','T') "

    '    '--tahun
    '    If Not ddlTahun.Text = "PILIH" Then
    '        strSQL += " AND kpmkv_pelajar.Tahun ='" & ddlTahun.Text & "'"
    '    End If
    '    '--semester
    '    If Not ddlSemester.Text = "PILIH" Then
    '        strSQL += " AND kpmkv_pelajar.Semester ='" & ddlSemester.Text & "'"
    '    End If
    '    '--sesi
    '    If Not chkSesi.Text = "" Then
    '        strSQL += " AND kpmkv_pelajar.Sesi ='" & chkSesi.Text & "'"
    '    End If
    '    '--kursus
    '    If Not ddlKodKursus.Text = "" Then
    '        strSQL += " AND kpmkv_pelajar.KursusID ='" & ddlKodKursus.SelectedValue & "'"
    '    End If
    '    '--jantina
    '    If Not ddlKelas.Text = "" Then
    '        strSQL += " AND kpmkv_pelajar.KelasID ='" & ddlKelas.SelectedValue & "'"
    '    End If

    '    strSQL += "ORDER BY kpmkv_pelajar.Tahun, kpmkv_pelajar.Semester, kpmkv_pelajar.Sesi ASC"

    '    strRet = oCommon.ExecuteSQL(strSQL)

    '    Dim sqlDA As New SqlDataAdapter(strSQL, objConn)
    '    Dim ds As DataSet = New DataSet
    '    sqlDA.Fill(ds, "AnyTable")

    '    lblMsg.Text = ""
    '    Dim count As Integer = 0

    '    For i As Integer = 0 To ds.Tables(0).Rows.Count - 1

    '        Dim cb As CheckBox = datRespondent.Rows(i).FindControl("chkSelect")

    '        If cb.Checked = True Then

    '            Dim strPelajarID As String = ds.Tables(0).Rows(i).Item(0).ToString

    '            ''get pengajian
    '            strSQL = " SELECT Pengajian FROM kpmkv_pelajar WHERE PelajarID = '" & strPelajarID & "'"
    '            Dim strPengajian As String = oCommon.getFieldValue(strSQL)

    '            ''get namaMP
    '            strSQL = " SELECT NamaMataPelajaran FROM kpmkv_matapelajaran WHERE KodMataPelajaran = '" & ddlMatapelajaran.SelectedValue & "'"
    '            Dim strNamaMP As String = oCommon.getFieldValue(strSQL)

    '            If strNamaMP = "BAHASA MELAYU" Then

    '                strSQL = "SELECT B_BahasaMelayu FROM kpmkv_pelajar_markah WHERE PelajarID = '" & strPelajarID & "' AND Tahun = '" & ddlTahun.SelectedValue & "' AND Semester = '" & ddlSemester.SelectedValue & "' AND Sesi = '" & chkSesi.Text & "'"

    '            ElseIf strNamaMP = "BAHASA INGGERIS" Then

    '                strSQL = "SELECT B_BahasaInggeris FROM kpmkv_pelajar_markah WHERE PelajarID = '" & strPelajarID & "' AND Tahun = '" & ddlTahun.SelectedValue & "' AND Semester = '" & ddlSemester.SelectedValue & "' AND Sesi = '" & chkSesi.Text & "'"

    '            ElseIf strNamaMP = "SAINS" Or strNamaMP = "SCIENCE" Or strNamaMP = "SAINS UNTUK TEKNOLOGI" Or strNamaMP = "SAINS UNTUK PENGAJIAN SOSIAL" Then

    '                strSQL = "SELECT B_Science1 FROM kpmkv_pelajar_markah WHERE PelajarID = '" & strPelajarID & "' AND Tahun = '" & ddlTahun.SelectedValue & "' AND Semester = '" & ddlSemester.SelectedValue & "' AND Sesi = '" & chkSesi.Text & "'"

    '            ElseIf strNamaMP = "SEJARAH" Then

    '                strSQL = "SELECT B_Sejarah FROM kpmkv_pelajar_markah WHERE PelajarID = '" & strPelajarID & "' AND Tahun = '" & ddlTahun.SelectedValue & "' AND Semester = '" & ddlSemester.SelectedValue & "' AND Sesi = '" & chkSesi.Text & "'"

    '            ElseIf strNamaMP = "PENDIDIKAN ISLAM" Then

    '                strSQL = "SELECT B_PendidikanIslam1 FROM kpmkv_pelajar_markah WHERE PelajarID = '" & strPelajarID & "' AND Tahun = '" & ddlTahun.SelectedValue & "' AND Semester = '" & ddlSemester.SelectedValue & "' AND Sesi = '" & chkSesi.Text & "'"

    '            ElseIf strNamaMP = "PENDIDIKAN MORAL" Then

    '                strSQL = "SELECT B_PendidikanMoral FROM kpmkv_pelajar_markah WHERE PelajarID = '" & strPelajarID & "' AND Tahun = '" & ddlTahun.SelectedValue & "' AND Semester = '" & ddlSemester.SelectedValue & "' AND Sesi = '" & chkSesi.Text & "'"

    '            ElseIf strNamaMP = "MATEMATIK" Or strNamaMP = "MATHEMATIC" Or strNamaMP = "MATEMATIK UNTUK TEKNOLOGI" Or strNamaMP = "MATEMATIK UNTUK PENGAJIAN SOSIAL" Then

    '                strSQL = "SELECT B_Mathematics FROM kpmkv_pelajar_markah WHERE PelajarID = '" & strPelajarID & "' AND Tahun = '" & ddlTahun.SelectedValue & "' AND Semester = '" & ddlSemester.SelectedValue & "' AND Sesi = '" & chkSesi.Text & "'"

    '            End If

    '            Dim markahPB As String = oCommon.getFieldValue(strSQL)

    '            strSQL = "  SELECT PelajarID FROM kpmkv_pelajar_ulang
    '                        WHERE
    '                        PelajarID = '" & strPelajarID & "'
    '                        AND Tahun = '" & ddlTahun.Text & "'
    '                        AND Semester = '" & ddlSemester.Text & "'
    '                        AND Sesi = '" & chkSesi.Text & "'
    '                        AND KursusID = '" & ddlKodKursus.SelectedValue & "'
    '                        AND KelasID = '" & ddlKelas.SelectedValue & "'
    '                        AND NamaMataPelajaran = '" & strNamaMP & "'"

    '            strRet = oCommon.getFieldValue(strSQL)

    '            If strRet = "" Then

    '                strSQL = "  INSERT INTO kpmkv_pelajar_ulang 
    '                            (PelajarID, KolejRecordID, Pengajian, Tahun, Semester, Sesi, KursusID, KelasID, NamaMataPelajaran, PB, PA, MarkahPB)
    '                            VALUES
    '                            ('" & strPelajarID & "', '" & lblKolejID.Text & "', '" & strPengajian & "', '" & ddlTahun.Text & "', '" & ddlSemester.Text & "', '" & chkSesi.Text & "', '" & ddlKodKursus.SelectedValue & "', '" & ddlKelas.SelectedValue & "', '" & strNamaMP & "', NULL, '1', '" & markahPB & "')"
    '                strRet = oCommon.ExecuteSQL(strSQL)

    '                count = count + 1

    '                Dim lblStatus As Label = datRespondent.Rows(i).FindControl("lblStatus")
    '                lblStatus.Text = "Telah Didaftarkan"

    '            End If


    '        End If

    '    Next

    '    lblMsg.Text = "Jumlah Pelajar Yang Didaftarkan : " & count

    'End Sub

End Class

