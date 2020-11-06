Imports System.Data.SqlClient
Public Class jana_keseluruhan_pngs1
    Inherits System.Web.UI.UserControl
    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""
    Dim strKursusID As String = ""
    Dim strAngkaGiliran As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                lblMsg.Text = ""

                kpmkv_tahun_list()
                ddlTahun.SelectedValue = Now.Year

                kpmkv_negeri_list()
                ddlNegeri.SelectedValue = ""

                kpmkv_jenis_list()
                ddlJenis.SelectedValue = ""

                kpmkv_kolej_list()
                ddlKolej.SelectedValue = ""

                kpmkv_kodkursus_list()
                ddlKodKursus.SelectedValue = ""

                kpmkv_semester_list()

            End If

        Catch ex As Exception
            lblMsg.Text = "Error Message:" & ex.Message
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
            ddlTahun.Items.Insert(0, "-Pilih-")

        Catch ex As Exception

        Finally
            objConn.Dispose()
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

    Private Sub kpmkv_jenis_list()
        strSQL = "SELECT Jenis FROM kpmkv_jeniskolej ORDER BY Jenis"
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
            ddlJenis.Items.Add(New ListItem("-Pilih-", ""))

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub kpmkv_kolej_list()
        strSQL = "SELECT Nama,RecordID FROM kpmkv_kolej WHERE Negeri='" & ddlNegeri.SelectedItem.Value & "' AND Jenis='" & ddlJenis.SelectedValue & "' ORDER BY Nama"
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
            ddlKolej.Items.Add(New ListItem("-Pilih-", ""))

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub kpmkv_kodkursus_list()

        strSQL = "SELECT b.KursusID as KursusID,b.KodKursus as KodKursus from kpmkv_kursus_kolej as a"
        strSQL += " LEFT JOIN kpmkv_kursus as b ON a.KursusID =b.KursusID "
        strSQL += " WHERE a.kolejRecordID='" & ddlKolej.SelectedValue & "'"

        '--tahun
        If Not ddlTahun.Text = "-Pilih-" Then
            strSQL += " AND b.Tahun ='" & ddlTahun.SelectedValue & "'"
        End If
        '--sesi
        If Not chkSesi.Text = "" Then
            strSQL += " AND b.Sesi ='" & chkSesi.Text & "'"
        End If

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
            ddlKodKursus.Items.Add(New ListItem("SEMUA", ""))

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
            ddlSemester.Items.Insert(0, "-Pilih-")

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub ddlNegeri_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlNegeri.SelectedIndexChanged


        kpmkv_jenis_list()
        ddlJenis.SelectedValue = ""

        ddlKolej.SelectedValue = ""
    End Sub

    Private Sub ddlJenis_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlJenis.SelectedIndexChanged

        kpmkv_kolej_list()
        ddlKolej.SelectedValue = ""

    End Sub

    Private Sub chkSesi_SelectedIndexChanged(sender As Object, e As EventArgs) Handles chkSesi.SelectedIndexChanged
        kpmkv_kodkursus_list()
        ddlKodKursus.SelectedValue = ""
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
        Dim strOrder As String = ""

        tmpSQL = "SELECT b.KursusID,b.KodKursus from kpmkv_kursus_kolej as a"
        tmpSQL += " LEFT JOIN kpmkv_kursus as b ON a.KursusID =b.KursusID "
        strWhere = " WHERE a.kolejRecordID='" & ddlKolej.SelectedValue & "'"

        '--tahun
        If Not ddlTahun.Text = "-Pilih-" Then
            strWhere += " AND b.Tahun ='" & ddlTahun.SelectedValue & "'"
        End If
        '--sesi
        If Not chkSesi.Text = "" Then
            strWhere += " AND b.Sesi ='" & chkSesi.Text & "'"
        End If

        '--Kod
        If Not ddlKodKursus.Text = "" Then
            strWhere += " AND b.KursusID='" & ddlKodKursus.SelectedValue & "'"
        End If

        getSQL = tmpSQL & strWhere & strOrder

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

    Protected Sub btnCari_Click(sender As Object, e As EventArgs) Handles btnCari.Click
        lblMsg.Text = ""
        strRet = BindData(datRespondent)


    End Sub

    Private Sub calculate_pngs()

        For i As Integer = 0 To datRespondent.Rows.Count - 1

            Dim cb As CheckBox = datRespondent.Rows(i).FindControl("chkSelect")

            If cb.Checked = True Then

                strKursusID = datRespondent.DataKeys(i).Value.ToString()

                strSQL = "  SELECT kpmkv_pelajar.PelajarID"
                strSQL += " FROM kpmkv_pelajar_markah "
                strSQL += " LEFT OUTER JOIN kpmkv_pelajar On kpmkv_pelajar_markah.PelajarID = kpmkv_pelajar.PelajarID"
                strSQL += " LEFT OUTER Join kpmkv_kursus On kpmkv_pelajar.KursusID = kpmkv_kursus.KursusID"
                strSQL += " WHERE kpmkv_pelajar.KolejRecordID='" & ddlKolej.SelectedValue & "' "
                strSQL += " AND kpmkv_pelajar.IsDeleted='N' AND kpmkv_pelajar.StatusID='2' AND kpmkv_pelajar.JenisCalonID='2'"
                strSQL += " AND kpmkv_pelajar.Tahun ='" & ddlTahun.Text & "'"
                strSQL += " AND kpmkv_pelajar.Sesi ='" & chkSesi.Text & "'"
                strSQL += " AND kpmkv_pelajar.Semester ='" & ddlSemester.Text & "'"
                strSQL += " AND kpmkv_pelajar.KursusID='" & strKursusID & "'"

                strRet = oCommon.ExecuteSQL(strSQL)

                Dim sqlDA As New SqlDataAdapter(strSQL, objConn)
                Dim ds As DataSet = New DataSet
                sqlDA.Fill(ds, "AnyTable")

                Dim strPelajarID As String

                Dim strCount As Integer = ds.Tables(0).Rows.Count - 1
                For iloop As Integer = 0 To strCount

                    Dim strGred As String = ""
                    Dim strNilaiGred As Double = 0.0
                    Dim strNilaiGredBM As Double = 0.0
                    Dim strTtlNilaiGredBM As Double = 0.0
                    Dim strKodBM As String = ""
                    Dim strKodBI As String = ""
                    Dim strKodMT As String = ""
                    Dim strKodSC As String = ""
                    Dim strKodSJ As String = ""
                    Dim strKodPI As String = ""
                    Dim strKodPM As String = ""

                    Dim strNamaBM As String = ""
                    Dim strNamaBI As String = ""
                    Dim strNamaMT As String = ""
                    Dim strNamaSC As String = ""
                    Dim strNamaSJ As String = ""
                    Dim strNamaPI As String = ""
                    Dim strNamaPM As String = ""

                    Dim strJamKreditBM As String = ""
                    Dim strJamKreditBI As String = ""
                    Dim strJamKreditMT As String = ""
                    Dim strJamKreditSC As String = ""
                    Dim strJamKreditSJ As String = ""
                    Dim strJamKreditPI As String = ""
                    Dim strJamKreditPM As String = ""

                    Dim strNilaiMataBM As Double = 0.0
                    Dim strNilaiMataBI As Double = 0.0
                    Dim strNilaiMataMT As Double = 0.0
                    Dim strNilaiMataSC As Double = 0.0
                    Dim strNilaiMataSJ As Double = 0.0
                    Dim strNilaiMataPI As Double = 0.0
                    Dim strNilaiMataPM As Double = 0.0

                    Dim strKompentasiBM As String = ""
                    Dim strKompentasiBI As String = ""
                    Dim strKompentasiMT As String = ""
                    Dim strKompentasiSC As String = ""
                    Dim strKompentasiSJ As String = ""
                    Dim strKompentasiPI As String = ""
                    Dim strKompentasiPM As String = ""
                    Dim strKompentasi As String = ""
                    Dim strTotalNilaiMata As Double = 0.0
                    Dim strKodMP As String = ""
                    Dim strNamaMP As String = ""
                    'vokay
                    Dim strJamKredit As String = ""
                    Dim strNilaiMataV As Double = 0.0
                    Dim strJamKreditV As Double = 0.0

                    strPelajarID = (ds.Tables(0).Rows(iloop).Item(0).ToString())

                    strSQL = "SELECT AngkaGiliran FROM kpmkv_pelajar WHERE PelajarID='" & strPelajarID & "'"
                    strAngkaGiliran = oCommon.getFieldValue(strSQL)

                    strSQL = "SELECT JenisKursus FROM kpmkv_kursus WHERE KursusID = '" & strKursusID & "'"
                    Dim strJenisKursus As String = oCommon.getFieldValue(strSQL)

                    If strJenisKursus = "TECHNOLOGY" Then
                        strJenisKursus = "TEKNOLOGI"
                    End If

                    'BahasaMelayu
                    strSQL = "SELECT  KODMatapelajaran, NamaMataPelajaran,JamKredit FROM kpmkv_matapelajaran WHERE Semester='" & ddlSemester.Text & "'"
                    strSQL += " AND NamaMataPelajaran='BAHASA MELAYU' AND Tahun='" & ddlTahun.SelectedValue & "'"
                    strRet = oCommon.getFieldValueEx(strSQL)
                    'matapelajaran info
                    Dim ar_MP As Array
                    ar_MP = strRet.Split("|")
                    strKodBM = ar_MP(0)
                    strNamaBM = ar_MP(1)
                    strJamKreditBM = ar_MP(2)
                    'markah
                    strSQL = "SELECT  GredBM FROM kpmkv_pelajar_markah WHERE PelajarID='" & strPelajarID & "'"
                    strSQL += " AND Tahun='" & ddlTahun.Text & "'"
                    strSQL += " AND Semester='" & ddlSemester.Text & "'"
                    strSQL += " AND Sesi='" & chkSesi.Text & "'"
                    Dim strGredBM As String = oCommon.getFieldValue(strSQL)
                    If Not strGredBM = "" Then
                        'gred
                        strSQL = "SELECT  Pointer, Status FROM kpmkv_gred WHERE Jenis='AKADEMIK' AND Gred='" & strGredBM & "'"
                        strRet = oCommon.getFieldValueEx(strSQL)
                        ' ''--get Gred akademik info
                        Dim ar_akademik As Array
                        ar_akademik = strRet.Split("|")

                        strNilaiGred = ar_akademik(0)
                        strNilaiGredBM = ar_akademik(0)
                        strNilaiMataBM = ar_akademik(0) * (strJamKreditBM) 'edit 0608
                        strTtlNilaiGredBM = ar_akademik(0) * (strJamKreditBM)
                        strKompentasiBM = ar_akademik(1)
                    End If

                    'bi
                    strSQL = "SELECT  KODMatapelajaran, NamaMataPelajaran,JamKredit FROM kpmkv_matapelajaran WHERE Semester='" & ddlSemester.Text & "'"
                    strSQL += " AND NamaMataPelajaran='BAHASA INGGERIS' AND Tahun='" & ddlTahun.SelectedValue & "'"
                    strRet = oCommon.getFieldValueEx(strSQL)
                    ' ''--get Gred akademik info
                    Dim ar_MPBI As Array
                    ar_MPBI = strRet.Split("|")
                    strKodBI = ar_MPBI(0)
                    strNamaBI = ar_MPBI(1)
                    strJamKreditBI = ar_MPBI(2)
                    'markah
                    strSQL = "SELECT  GredBI FROM kpmkv_pelajar_markah WHERE PelajarID='" & strPelajarID & "'"
                    strSQL += " AND Tahun='" & ddlTahun.Text & "'"
                    strSQL += " AND Semester='" & ddlSemester.Text & "'"
                    strSQL += " AND Sesi='" & chkSesi.Text & "'"
                    Dim strGredBI As String = oCommon.getFieldValue(strSQL)
                    If Not strGredBI = "" Then
                        'gred
                        strSQL = "SELECT  Pointer, Status FROM kpmkv_gred WHERE Jenis='AKADEMIK' AND Gred='" & strGredBI & "'"
                        strRet = oCommon.getFieldValueEx(strSQL)
                        ' ''--get Gred akademik info
                        Dim ar_akademikBI As Array
                        ar_akademikBI = strRet.Split("|")

                        strNilaiGred = ar_akademikBI(0)
                        strNilaiMataBI = ar_akademikBI(0) * CDbl(strJamKreditBI)
                        strKompentasiBI = ar_akademikBI(1)
                    End If

                    'MATHEMATIC
                    If ddlSemester.Text = "1" Or ddlSemester.Text = "2" Then

                        strSQL = "SELECT  KODMatapelajaran, NamaMataPelajaran,JamKredit FROM kpmkv_matapelajaran WHERE Semester='" & ddlSemester.Text & "'"
                        strSQL += " AND NamaMataPelajaran='MATEMATIK' AND Tahun='" & ddlTahun.SelectedValue & "'"
                        strRet = oCommon.getFieldValueEx(strSQL)

                    Else

                        strSQL = "SELECT  KODMatapelajaran, NamaMataPelajaran,JamKredit FROM kpmkv_matapelajaran WHERE Semester='" & ddlSemester.Text & "'"
                        strSQL += " AND NamaMataPelajaran LIKE '%MATEMATIK%' AND Jenis = '" & strJenisKursus & "' AND Tahun='" & ddlTahun.SelectedValue & "'"
                        strRet = oCommon.getFieldValueEx(strSQL)

                    End If

                    Dim ar_KODMT As Array
                    ar_KODMT = strRet.Split("|")
                    strKodMT = ar_KODMT(0)
                    strNamaMT = ar_KODMT(1)
                    strJamKreditMT = ar_KODMT(2)
                    'markah
                    strSQL = "SELECT  GredMT FROM kpmkv_pelajar_markah WHERE PelajarID='" & strPelajarID & "'"
                    strSQL += " AND Tahun='" & ddlTahun.Text & "'"
                    strSQL += " AND Semester='" & ddlSemester.Text & "'"
                    strSQL += " AND Sesi='" & chkSesi.Text & "'"
                    Dim strGredMT As String = oCommon.getFieldValue(strSQL)
                    If Not strGredMT = "" Then
                        'gred
                        strSQL = "SELECT  Pointer, Status FROM kpmkv_gred WHERE Jenis='AKADEMIK' AND Gred='" & strGredMT & "'"
                        strRet = oCommon.getFieldValueEx(strSQL)
                        ' ''--get Gred akademik info
                        Dim ar_akademikMT As Array
                        ar_akademikMT = strRet.Split("|")

                        strNilaiGred = ar_akademikMT(0)
                        strNilaiMataMT = ar_akademikMT(0) * CDbl(strJamKreditMT)
                        strKompentasiMT = ar_akademikMT(1)
                    End If

                    'SCIENCE

                    If ddlSemester.Text = "1" Or ddlSemester.Text = "2" Then

                        strSQL = "SELECT  KODMatapelajaran, NamaMataPelajaran,JamKredit FROM kpmkv_matapelajaran WHERE Semester='" & ddlSemester.Text & "'"
                        strSQL += " AND NamaMataPelajaran='SAINS' AND Tahun='" & ddlTahun.SelectedValue & "'"
                        strRet = oCommon.getFieldValueEx(strSQL)

                    Else

                        strSQL = "SELECT  KODMatapelajaran, NamaMataPelajaran,JamKredit FROM kpmkv_matapelajaran WHERE Semester='" & ddlSemester.Text & "'"
                        strSQL += " AND NamaMataPelajaran LIKE '%SAINS%' AND Jenis = '" & strJenisKursus & "' AND Tahun='" & ddlTahun.SelectedValue & "'"
                        strRet = oCommon.getFieldValueEx(strSQL)

                    End If

                    Dim ar_MPSC As Array
                    ar_MPSC = strRet.Split("|")
                    strKodSC = ar_MPSC(0)
                    strNamaSC = ar_MPSC(1)
                    strJamKreditSC = ar_MPSC(2)
                    'markah
                    strSQL = "SELECT  GredSC FROM kpmkv_pelajar_markah WHERE PelajarID='" & strPelajarID & "'"
                    strSQL += " AND Tahun='" & ddlTahun.Text & "'"
                    strSQL += " AND Semester='" & ddlSemester.Text & "'"
                    strSQL += " AND Sesi='" & chkSesi.Text & "'"
                    Dim strGredSC As String = oCommon.getFieldValue(strSQL)
                    If Not strGredSC = "" Then
                        'gred
                        strSQL = "SELECT  Pointer, Status FROM kpmkv_gred WHERE Jenis='AKADEMIK' AND Gred='" & strGredSC & "'"
                        strRet = oCommon.getFieldValueEx(strSQL)
                        ' ''--get Gred akademik info
                        Dim ar_akademikSC As Array
                        ar_akademikSC = strRet.Split("|")

                        strNilaiGred = ar_akademikSC(0)
                        strNilaiMataSC = ar_akademikSC(0) * CDbl(strJamKreditSC)
                        strKompentasiSC = ar_akademikSC(1)
                    End If
                    'SEJARAH
                    strSQL = "SELECT  KODMatapelajaran, NamaMataPelajaran,JamKredit FROM kpmkv_matapelajaran WHERE Semester='" & ddlSemester.Text & "'"
                    strSQL += " AND NamaMataPelajaran='SEJARAH' AND Tahun='" & ddlTahun.SelectedValue & "'"
                    strRet = oCommon.getFieldValueEx(strSQL)
                    Dim ar_KODSJ As Array
                    ar_KODSJ = strRet.Split("|")
                    strKodSJ = ar_KODSJ(0)
                    strNamaSJ = ar_KODSJ(1)
                    strJamKreditSJ = ar_KODSJ(2)
                    'markah
                    strSQL = "SELECT  GredSJ FROM kpmkv_pelajar_markah WHERE PelajarID='" & strPelajarID & "'"
                    strSQL += " AND Tahun='" & ddlTahun.Text & "'"
                    strSQL += " AND Semester='" & ddlSemester.Text & "'"
                    strSQL += " AND Sesi='" & chkSesi.Text & "'"
                    Dim strGredSJ As String = oCommon.getFieldValue(strSQL)
                    If Not strGredSJ = "" Then
                        'gred
                        strSQL = "SELECT  Pointer, Status FROM kpmkv_gred WHERE Jenis='AKADEMIK' AND Gred='" & strGredSJ & "'"
                        strRet = oCommon.getFieldValueEx(strSQL)
                        ' ''--get Gred akademik info
                        Dim ar_akademikSJ As Array
                        ar_akademikSJ = strRet.Split("|")

                        strNilaiGred = ar_akademikSJ(0)
                        strNilaiMataSJ = ar_akademikSJ(0) * CDbl(strJamKreditSJ)
                        strKompentasiSJ = ar_akademikSJ(1)
                    End If

                    'PENDIDIKAN ISLAM
                    strSQL = "SELECT  KODMatapelajaran, NamaMataPelajaran,JamKredit FROM kpmkv_matapelajaran WHERE Semester='" & ddlSemester.Text & "' "
                    strSQL += " AND NamaMataPelajaran='PENDIDIKAN ISLAM' AND Tahun='" & ddlTahun.SelectedValue & "'"
                    strRet = oCommon.getFieldValueEx(strSQL)
                    Dim ar_MPPI As Array
                    ar_MPPI = strRet.Split("|")
                    strKodPI = ar_MPPI(0)
                    strNamaPI = ar_MPPI(1)
                    strJamKreditPI = ar_MPPI(2)
                    'markah
                    strSQL = "SELECT  GredPI FROM kpmkv_pelajar_markah WHERE PelajarID='" & strPelajarID & "'"
                    strSQL += " AND Tahun='" & ddlTahun.Text & "'"
                    strSQL += " AND Semester='" & ddlSemester.Text & "'"
                    strSQL += " AND Sesi='" & chkSesi.Text & "'"
                    Dim strGredPI As String = oCommon.getFieldValue(strSQL)

                    'gred
                    If Not strGredPI = "" Then
                        strSQL = "SELECT  Pointer, Status FROM kpmkv_gred WHERE Jenis='AKADEMIK' AND Gred='" & strGredPI & "'"
                        strRet = oCommon.getFieldValueEx(strSQL)
                        ' ''--get Gred akademik info
                        Dim ar_akademikPI As Array
                        ar_akademikPI = strRet.Split("|")
                        strNilaiGred = ar_akademikPI(0)
                        strNilaiMataPI = ar_akademikPI(0) * CDbl(strJamKreditPI)
                        strKompentasiPI = ar_akademikPI(1)
                    End If

                    'PENDIDIKAN MORAL
                    strSQL = "SELECT  KODMatapelajaran, NamaMataPelajaran,JamKredit FROM kpmkv_matapelajaran WHERE Semester='" & ddlSemester.Text & "' "
                    strSQL += " AND NamaMataPelajaran='PENDIDIKAN MORAL' AND Tahun='" & ddlTahun.SelectedValue & "'"
                    strRet = oCommon.getFieldValueEx(strSQL)
                    Dim ar_MPPM As Array
                    ar_MPPM = strRet.Split("|")
                    strKodPM = ar_MPPM(0)
                    strNamaPM = ar_MPPM(1)
                    strJamKreditPM = ar_MPPM(2)
                    'markah
                    strSQL = "SELECT  GredPM FROM kpmkv_pelajar_markah WHERE PelajarID='" & strPelajarID & "'"
                    strSQL += " AND Tahun='" & ddlTahun.Text & "'"
                    strSQL += " AND Semester='" & ddlSemester.Text & "'"
                    strSQL += " AND Sesi='" & chkSesi.Text & "'"
                    Dim strGredPM As String = oCommon.getFieldValue(strSQL)
                    'gred
                    If Not strGredPM = "" Then
                        strSQL = "SELECT  Pointer, Status FROM kpmkv_gred WHERE Jenis='AKADEMIK' AND Gred='" & strGredPM & "'"
                        strRet = oCommon.getFieldValueEx(strSQL)
                        ' ''--get Gred akademik info
                        Dim ar_akademikPM As Array
                        ar_akademikPM = strRet.Split("|")
                        strNilaiGred = ar_akademikPM(0)
                        strNilaiMataPM = ar_akademikPM(0) * CDbl(strJamKreditPM)
                        strKompentasiPM = ar_akademikPM(1)
                    End If

                    ''*******************************************************************************************
                    '----VOKASIONAL
                    strSQL = " SELECT COUNT(kpmkv_modul.KODMODUL) AS BILMODUL FROM kpmkv_modul LEFT OUTER JOIN "
                    strSQL += " kpmkv_kursus ON kpmkv_modul.KursusID = kpmkv_kursus.KursusID"
                    strSQL += " WHERE kpmkv_modul.Tahun='" & ddlTahun.Text & "'"
                    strSQL += " AND kpmkv_modul.Semester='" & ddlSemester.Text & "'"
                    strSQL += " AND kpmkv_modul.Sesi='" & chkSesi.Text & "'"
                    strSQL += " AND kpmkv_modul.KursusID ='" & strKursusID & "'"
                    Dim strBilModul As Integer = oCommon.getFieldValue(strSQL)


                    For j As Integer = 1 To strBilModul

                        'Modul1
                        strSQL = " SELECT kpmkv_modul.KodModul, kpmkv_modul.NamaModul, kpmkv_modul.JamKredit"
                        strSQL += " FROM  kpmkv_modul LEFT OUTER JOIN kpmkv_kursus ON kpmkv_modul.KursusID = kpmkv_kursus.KursusID "
                        strSQL += " LEFT OUTER JOIN kpmkv_pelajar ON kpmkv_kursus.KursusID = kpmkv_pelajar.KursusID"
                        strSQL += " WHERE kpmkv_modul.Tahun='" & ddlTahun.Text & "'"
                        strSQL += " AND kpmkv_modul.Semester='" & ddlSemester.Text & "'"
                        strSQL += " AND kpmkv_modul.Sesi='" & chkSesi.Text & "'"
                        strSQL += " AND SUBSTRING(kpmkv_modul.KodModul,6,1)='" & j & "'"
                        strSQL += " AND kpmkv_modul.KursusID='" & strKursusID & "'"
                        strSQL += " AND kpmkv_pelajar.PelajarID='" & strPelajarID & "'"
                        strSQL += " ORDER BY  kpmkv_modul.KodModul, kpmkv_modul.NamaModul, kpmkv_modul.JamKredit ASC"
                        strRet = oCommon.getFieldValueEx(strSQL)
                        Dim ar_VokM1 As Array
                        ar_VokM1 = strRet.Split("|")
                        strKodMP = ar_VokM1(0)
                        strNamaMP = ar_VokM1(1)
                        strJamKredit = ar_VokM1(2)
                        strJamKreditV += CDbl(strJamKredit)

                        strSQL = "SELECT GredV" & j & " FROM kpmkv_pelajar_markah WHERE PelajarID='" & strPelajarID & "' AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "' AND Sesi='" & chkSesi.Text & "'"
                        strGred = oCommon.getFieldValue(strSQL)
                        If Not strGred = "" Then
                            'gred
                            strSQL = "SELECT  Pointer, Kompentasi FROM kpmkv_gred WHERE Jenis='VOKASIONAL' AND Gred='" & strGred & "'"
                            strRet = oCommon.getFieldValueEx(strSQL)
                            ' ''--get Gred akademik info
                            Dim ar_VOKGred As Array
                            ar_VOKGred = strRet.Split("|")
                            strNilaiGred = ar_VOKGred(0)
                            strNilaiMataV = ar_VOKGred(0) * CDbl(strJamKredit)
                            strKompentasi = ar_VOKGred(1)
                            strTotalNilaiMata += strNilaiMataV

                        End If

                    Next

                    ''**********************************************************************
                    Dim strJamKreditAkademik As Double

                    If Not strGredPI = "" Then
                        strJamKreditAkademik = CDbl(strJamKreditBM) + CDbl(strJamKreditBI) + CDbl(strJamKreditMT) + CDbl(strJamKreditSC) + CDbl(strJamKreditSJ) + CDbl(strJamKreditPI)
                    ElseIf Not strGredPM = "" Then
                        strJamKreditAkademik = CDbl(strJamKreditBM) + CDbl(strJamKreditBI) + CDbl(strJamKreditMT) + CDbl(strJamKreditSC) + CDbl(strJamKreditSJ) + CDbl(strJamKreditPI)
                    End If

                    'check by semester

                    Dim strTotalNilaiMataAkademik As Double = CDbl(strNilaiMataBM) + CDbl(strNilaiMataBI) + CDbl(strNilaiMataMT) + CDbl(strNilaiMataSC) + CDbl(strNilaiMataSJ) + CDbl(strNilaiMataPI) + CDbl(strNilaiMataPM)
                    Dim strTotalNilaiMataPNGA As Double = Math.Round((strTotalNilaiMataAkademik / strJamKreditAkademik), 2)
                    Dim strTotalNilaiMataPNGV As Double = Math.Round((strTotalNilaiMata / strJamKreditV), 2)
                    Dim strTotalNilaiMataPNGK As Double = Math.Round((strTotalNilaiMataAkademik + strTotalNilaiMata) / (strJamKreditAkademik + strJamKreditV), 2)
                    Dim strTotalNilaiMataPNGKBM As Double = 0.0

                    If ddlSemester.Text = 1 Then
                        strTotalNilaiMataPNGKBM = Math.Round((strNilaiMataBM / strJamKreditBM), 2)

                    ElseIf ddlSemester.Text = 2 Then
                        'checkin PelajarID for other semester
                        'change on 28July2016
                        strSQL = "SELECT PelajarID FROM kpmkv_pelajar WHERE AngkaGiliran='" & strAngkaGiliran & "'"
                        strSQL += " AND Semester='1'"
                        strSQL += " AND IsDeleted='N' AND StatusID='2'"
                        If oCommon.isExist(strSQL) = True Then
                            strSQL = "SELECT PelajarID,Tahun,Sesi FROM kpmkv_pelajar WHERE AngkaGiliran='" & strAngkaGiliran & "'"
                            strSQL += " AND Semester='1'"
                            strSQL += " AND IsDeleted='N' AND StatusID='2'"
                            strRet = oCommon.getFieldValueEx(strSQL)

                            Dim ar_HPelajar As Array
                            ar_HPelajar = strRet.Split("|")
                            Dim strP_ID As String = ar_HPelajar(0)
                            Dim strP_Tahun As String = ar_HPelajar(1)
                            Dim strP_Sesi As String = ar_HPelajar(2)

                            strSQL = "SELECT Jum_NilaiMata_Akademik_BM,Jum_JamKredit_Akademik_BM FROM kpmkv_pelajar_markah"
                            strSQL += " WHERE Tahun='" & strP_Tahun & "'"
                            strSQL += " AND Semester='1'"
                            strSQL += " AND Sesi='" & strP_Sesi & "'"
                            strSQL += " AND PelajarID='" & strP_ID & "'"
                            strRet = oCommon.getFieldValueEx(strSQL)

                            Dim ar_HMarkah2 As Array
                            ar_HMarkah2 = strRet.Split("|")
                            Dim strNilaiMata_Akademik_BM2 As Double = ar_HMarkah2(0)
                            Dim strJamKredit_Akademik_BM2 As Double = ar_HMarkah2(1)
                            strTotalNilaiMataPNGKBM = Math.Round((strNilaiMataBM + strNilaiMata_Akademik_BM2) / (strJamKreditBM + strJamKredit_Akademik_BM2), 2)
                        End If

                    ElseIf ddlSemester.Text = 3 Then
                        'change on 28July2016
                        strSQL = "SELECT PelajarID FROM kpmkv_pelajar WHERE AngkaGiliran='" & strAngkaGiliran & "'"
                        strSQL += " AND Semester='1'"
                        strSQL += " AND IsDeleted='N' AND StatusID='2'"
                        If oCommon.isExist(strSQL) = True Then
                            strSQL = "SELECT PelajarID,Tahun,Sesi FROM kpmkv_pelajar WHERE AngkaGiliran='" & strAngkaGiliran & "'"
                            strSQL += " AND Semester='1'"
                            strSQL += " AND IsDeleted='N' AND StatusID='2'"
                            strRet = oCommon.getFieldValueEx(strSQL)

                            Dim ar_HPelajar2 As Array
                            ar_HPelajar2 = strRet.Split("|")
                            Dim strP_ID2 As String = ar_HPelajar2(0)
                            Dim strP_Tahun2 As String = ar_HPelajar2(1)
                            Dim strP_Sesi2 As String = ar_HPelajar2(2)

                            strSQL = "SELECT Jum_NilaiMata_Akademik_BM,Jum_JamKredit_Akademik_BM FROM kpmkv_pelajar_markah"
                            strSQL += " WHERE Tahun='" & strP_Tahun2 & "'"
                            strSQL += " AND Semester='1'"
                            strSQL += " AND Sesi='" & strP_Sesi2 & "'"
                            strSQL += " AND PelajarID='" & strP_ID2 & "'"
                            strRet = oCommon.getFieldValueEx(strSQL)
                            Dim ar_HMarkah1_3 As Array
                            ar_HMarkah1_3 = strRet.Split("|")
                            Dim strNilaiMata_Akademik_BM1_3 As Double = ar_HMarkah1_3(0)
                            Dim strJamKredit_Akademik_BM1_3 As Double = ar_HMarkah1_3(1)

                            'if exist
                            'change on 28July2016
                            strSQL = "SELECT PelajarID FROM kpmkv_pelajar WHERE AngkaGiliran='" & strAngkaGiliran & "'"
                            strSQL += " AND Semester='2'"
                            strSQL += " AND IsDeleted='N' AND StatusID='2'"
                            If oCommon.isExist(strSQL) = True Then
                                strSQL = "SELECT PelajarID,Tahun,Sesi FROM kpmkv_pelajar WHERE AngkaGiliran='" & strAngkaGiliran & "'"
                                strSQL += " AND Semester='2'"
                                strSQL += " AND IsDeleted='N' AND StatusID='2'"
                                strRet = oCommon.getFieldValueEx(strSQL)

                                Dim ar_HPelajar3 As Array
                                ar_HPelajar3 = strRet.Split("|")
                                Dim strP_ID3 As String = ar_HPelajar3(0)
                                Dim strP_Tahun3 As String = ar_HPelajar3(1)
                                Dim strP_Sesi3 As String = ar_HPelajar3(2)

                                strSQL = "SELECT Jum_NilaiMata_Akademik_BM,Jum_JamKredit_Akademik_BM FROM kpmkv_pelajar_markah"
                                strSQL += " WHERE Tahun='" & strP_Tahun3 & "'"
                                strSQL += " AND Semester='2'"
                                strSQL += " AND Sesi='" & strP_Sesi3 & "'"
                                strSQL += " AND PelajarID='" & strP_ID3 & "'"
                                strRet = oCommon.getFieldValueEx(strSQL)

                                Dim ar_HMarkah3 As Array
                                ar_HMarkah3 = strRet.Split("|")
                                Dim strNilaiMata_Akademik_BM3 As Double = CDbl(ar_HMarkah3(0))
                                Dim strJamKredit_Akademik_BM3 As Double = ar_HMarkah3(1)
                                strTotalNilaiMataPNGKBM = Math.Round((strNilaiMataBM + strNilaiMata_Akademik_BM3 + strNilaiMata_Akademik_BM1_3) / (strJamKreditBM + strJamKredit_Akademik_BM3 + strJamKredit_Akademik_BM1_3), 2)
                            End If
                        End If

                    ElseIf ddlSemester.Text = 4 Then
                        'checkin PelajarID for other semester
                        'change on 28July2016
                        strSQL = "SELECT PelajarID FROM kpmkv_pelajar WHERE AngkaGiliran='" & strAngkaGiliran & "'"
                        strSQL += " AND Semester='1'"
                        strSQL += " AND IsDeleted='N' AND StatusID='2'"
                        If oCommon.isExist(strSQL) = True Then
                            strSQL = "SELECT PelajarID,Tahun,Sesi FROM kpmkv_pelajar WHERE AngkaGiliran='" & strAngkaGiliran & "'"
                            strSQL += " AND Semester='1'"
                            strSQL += " AND IsDeleted='N' AND StatusID='2'"
                            strRet = oCommon.getFieldValueEx(strSQL)

                            Dim ar_HPelajar4 As Array
                            ar_HPelajar4 = strRet.Split("|")
                            Dim strP_ID4 As String = ar_HPelajar4(0)
                            Dim strP_Tahun4 As String = ar_HPelajar4(1)
                            Dim strP_Sesi4 As String = ar_HPelajar4(2)

                            strSQL = "SELECT Jum_NilaiMata_Akademik_BM,Jum_JamKredit_Akademik_BM FROM kpmkv_pelajar_markah"
                            strSQL += " WHERE Tahun='" & strP_Tahun4 & "'"
                            strSQL += " AND Semester='1'"
                            strSQL += " AND Sesi='" & strP_Sesi4 & "'"
                            strSQL += " AND PelajarID='" & strP_ID4 & "'"
                            strRet = oCommon.getFieldValueEx(strSQL)

                            Dim ar_HMarkah1_4 As Array
                            ar_HMarkah1_4 = strRet.Split("|")
                            Dim strNilaiMata_Akademik_BM1_4 As Double = ar_HMarkah1_4(0)
                            Dim strJamKredit_Akademik_BM1_4 As Double = ar_HMarkah1_4(1)

                            'if exist
                            'change on 28July2016
                            strSQL = "SELECT PelajarID FROM kpmkv_pelajar WHERE AngkaGiliran='" & strAngkaGiliran & "'"
                            strSQL += " AND Semester='2'"
                            strSQL += " AND IsDeleted='N' AND StatusID='2'"
                            If oCommon.isExist(strSQL) = True Then
                                strSQL = "SELECT PelajarID,Tahun,Sesi FROM kpmkv_pelajar WHERE AngkaGiliran='" & strAngkaGiliran & "'"
                                strSQL += " AND Semester='2'"
                                strSQL += " AND IsDeleted='N' AND StatusID='2'"
                                strRet = oCommon.getFieldValueEx(strSQL)

                                Dim ar_HPelajar5 As Array
                                ar_HPelajar5 = strRet.Split("|")
                                Dim strP_ID5 As String = ar_HPelajar5(0)
                                Dim strP_Tahun5 As String = ar_HPelajar5(1)
                                Dim strP_Sesi5 As String = ar_HPelajar5(2)

                                strSQL = "SELECT Jum_NilaiMata_Akademik_BM,Jum_JamKredit_Akademik_BM FROM kpmkv_pelajar_markah"
                                strSQL += " WHERE Tahun='" & strP_Tahun5 & "'"
                                strSQL += " AND Semester='2'"
                                strSQL += " AND Sesi='" & strP_Sesi5 & "'"
                                strSQL += " AND PelajarID='" & strP_ID5 & "'"
                                strRet = oCommon.getFieldValueEx(strSQL)
                                Dim ar_HMarkah2_4 As Array
                                ar_HMarkah2_4 = strRet.Split("|")
                                Dim strNilaiMata_Akademik_BM2_4 As Double = CDbl(ar_HMarkah2_4(0))
                                Dim strJamKredit_Akademik_BM2_4 As Double = ar_HMarkah2_4(1)

                                'if exist
                                'change on 28July2016
                                strSQL = "SELECT PelajarID FROM kpmkv_pelajar WHERE AngkaGiliran='" & strAngkaGiliran & "'"
                                strSQL += " AND Semester='3'"
                                strSQL += " AND IsDeleted='N' AND StatusID='2'"
                                If oCommon.isExist(strSQL) = True Then
                                    strSQL = "SELECT PelajarID,Tahun,Sesi FROM kpmkv_pelajar WHERE AngkaGiliran='" & strAngkaGiliran & "'"
                                    strSQL += " AND Semester='3'"
                                    strSQL += " AND IsDeleted='N' AND StatusID='2'"
                                    strRet = oCommon.getFieldValueEx(strSQL)

                                    Dim ar_HPelajar6 As Array
                                    ar_HPelajar6 = strRet.Split("|")
                                    Dim strP_ID6 As String = ar_HPelajar6(0)
                                    Dim strP_Tahun6 As String = ar_HPelajar6(1)
                                    Dim strP_Sesi6 As String = ar_HPelajar6(2)

                                    strSQL = "SELECT Jum_NilaiMata_Akademik_BM,Jum_JamKredit_Akademik_BM FROM kpmkv_pelajar_markah"
                                    strSQL += " WHERE Tahun='" & strP_Tahun6 & "'"
                                    strSQL += " AND Semester='3'"
                                    strSQL += " AND Sesi='" & strP_Sesi6 & "'"
                                    strSQL += " AND PelajarID='" & strP_ID6 & "'"
                                    strRet = oCommon.getFieldValueEx(strSQL)
                                    ' ''--get Gred akademik info
                                    Dim ar_HMarkah3_4 As Array
                                    ar_HMarkah3_4 = strRet.Split("|")
                                    Dim strNilaiMata_Akademik_BM3_4 As Double = CDbl(ar_HMarkah3_4(0))
                                    Dim strJamKredit_Akademik_BM3_4 As Double = ar_HMarkah3_4(1)
                                    strTotalNilaiMataPNGKBM = Math.Round((strNilaiMataBM + strNilaiMata_Akademik_BM3_4 + strNilaiMata_Akademik_BM2_4 + strNilaiMata_Akademik_BM1_4) / (strJamKreditBM + strJamKredit_Akademik_BM3_4 + strJamKredit_Akademik_BM2_4 + strJamKredit_Akademik_BM1_4), 2)
                                End If

                            End If
                        End If

                    End If

                    Dim strNilaiMata_Akademik1 As Double = 0.0
                    Dim strNilaiMata_Vokasional1 As Double = 0.0
                    Dim strJamKredit_Akademik1 As Double = 0.0
                    Dim strJamKredit_Vokasional1 As Double = 0.0
                    Dim strTotalNilaiMataPNGKA As Double = 0.0
                    Dim strTotalNilaiMataPNGKV As Double = 0.0
                    Dim strTotalNilaiMataPNGKK As Double = 0.0
                    Dim strP_ID_P As String = ""
                    Dim strP_Tahun_P As String = ""
                    Dim strP_Sesi_P As String = ""

                    If ddlSemester.Text = 1 Then
                        strTotalNilaiMataPNGKA = Math.Round((strTotalNilaiMataAkademik / strJamKreditAkademik), 2)
                        strTotalNilaiMataPNGKV = Math.Round((strTotalNilaiMata / strJamKreditV), 2)
                        strTotalNilaiMataPNGKK = Math.Round((strTotalNilaiMataAkademik + strTotalNilaiMata) / (strJamKreditAkademik + strJamKreditV), 2)

                    ElseIf ddlSemester.Text = 2 Then
                        'if exist
                        'change on 28July2016
                        strSQL = "SELECT PelajarID FROM kpmkv_pelajar WHERE AngkaGiliran='" & strAngkaGiliran & "'"
                        strSQL += " AND Semester='1'"
                        strSQL += " AND IsDeleted='N' AND StatusID='2'"
                        If oCommon.isExist(strSQL) = True Then
                            strSQL = "SELECT PelajarID,Tahun,Sesi FROM kpmkv_pelajar WHERE AngkaGiliran='" & strAngkaGiliran & "'"
                            strSQL += " AND Semester='1'"
                            strSQL += " AND IsDeleted='N' AND StatusID='2'"
                            strRet = oCommon.getFieldValueEx(strSQL)

                            Dim ar_HPelajar As Array
                            ar_HPelajar = strRet.Split("|")
                            strP_ID_P = ar_HPelajar(0)
                            strP_Tahun_P = ar_HPelajar(1)
                            strP_Sesi_P = ar_HPelajar(2)

                            strSQL = "SELECT Jum_NilaiMata_Akademik,Jum_NilaiMata_Vokasional,Jum_JamKredit_Akademik,Jum_JamKredit_Vokasional FROM kpmkv_pelajar_markah"
                            strSQL += " WHERE Tahun='" & strP_Tahun_P & "'"
                            strSQL += " AND Semester='1'"
                            strSQL += " AND Sesi='" & strP_Sesi_P & "'"
                            strSQL += " AND PelajarID='" & strP_ID_P & "'"
                            strRet = oCommon.getFieldValueEx(strSQL)

                            ' ''--get Gred VOK info
                            Dim ar_HMarkah2 As Array
                            ar_HMarkah2 = strRet.Split("|")
                            strNilaiMata_Akademik1 = CDbl(ar_HMarkah2(0))
                            strNilaiMata_Vokasional1 = ar_HMarkah2(1)
                            strJamKredit_Akademik1 = CDbl(ar_HMarkah2(2))
                            strJamKredit_Vokasional1 = ar_HMarkah2(3)
                        End If

                    ElseIf ddlSemester.Text = 3 Then
                        'if exist
                        'change on 28July2016
                        strSQL = "SELECT PelajarID FROM kpmkv_pelajar WHERE AngkaGiliran='" & strAngkaGiliran & "'"
                        strSQL += " AND Semester='2'"
                        strSQL += " AND IsDeleted='N' AND StatusID='2'"
                        If oCommon.isExist(strSQL) = True Then
                            strSQL = "SELECT PelajarID,Tahun,Sesi FROM kpmkv_pelajar WHERE AngkaGiliran='" & strAngkaGiliran & "'"
                            strSQL += " AND Semester='2'"
                            strSQL += " AND IsDeleted='N' AND StatusID='2'"
                            strRet = oCommon.getFieldValueEx(strSQL)

                            Dim ar_HPelajar As Array
                            ar_HPelajar = strRet.Split("|")
                            strP_ID_P = ar_HPelajar(0)
                            strP_Tahun_P = ar_HPelajar(1)
                            strP_Sesi_P = ar_HPelajar(2)

                            strSQL = "SELECT Jum_NilaiMata_Akademik,Jum_NilaiMata_Vokasional,Jum_JamKredit_Akademik,Jum_JamKredit_Vokasional FROM kpmkv_pelajar_markah"
                            strSQL += " WHERE Tahun='" & strP_Tahun_P & "'"
                            strSQL += " AND Semester='2'"
                            strSQL += " AND Sesi='" & strP_Sesi_P & "'"
                            strSQL += " AND PelajarID='" & strP_ID_P & "'"
                            strRet = oCommon.getFieldValueEx(strSQL)

                            ' ''--get Gred VOK info
                            Dim ar_HMarkah3 As Array
                            ar_HMarkah3 = strRet.Split("|")
                            strNilaiMata_Akademik1 = CDbl(ar_HMarkah3(0))
                            strNilaiMata_Vokasional1 = ar_HMarkah3(1)
                            strJamKredit_Akademik1 = CDbl(ar_HMarkah3(2))
                            strJamKredit_Vokasional1 = ar_HMarkah3(3)
                        End If

                    ElseIf ddlSemester.Text = 4 Then
                        'if exist
                        'change on 28July2016
                        strSQL = "SELECT PelajarID FROM kpmkv_pelajar WHERE AngkaGiliran='" & strAngkaGiliran & "'"
                        strSQL += " AND Semester='3'"
                        strSQL += " AND IsDeleted='N' AND StatusID='2'"
                        If oCommon.isExist(strSQL) = True Then
                            strSQL = "SELECT PelajarID,Tahun,Sesi FROM kpmkv_pelajar WHERE AngkaGiliran='" & strAngkaGiliran & "'"
                            strSQL += " AND Semester='3'"
                            strSQL += " AND IsDeleted='N' AND StatusID='2'"
                            strRet = oCommon.getFieldValueEx(strSQL)
                            Dim ar_HPelajar As Array
                            ar_HPelajar = strRet.Split("|")
                            strP_ID_P = ar_HPelajar(0)
                            strP_Tahun_P = ar_HPelajar(1)
                            strP_Sesi_P = ar_HPelajar(2)

                            strSQL = "SELECT Jum_NilaiMata_Akademik,Jum_NilaiMata_Vokasional,Jum_JamKredit_Akademik,Jum_JamKredit_Vokasional FROM kpmkv_pelajar_markah"
                            strSQL += " WHERE Tahun='" & strP_Tahun_P & "'"
                            strSQL += " AND Semester='3'"
                            strSQL += " AND Sesi='" & strP_Sesi_P & "'"
                            strSQL += " AND PelajarID='" & strP_ID_P & "'"
                            strRet = oCommon.getFieldValueEx(strSQL)

                            ' ''--get Gred VOK info
                            Dim ar_HMarkah4 As Array
                            ar_HMarkah4 = strRet.Split("|")
                            strNilaiMata_Akademik1 = CDbl(ar_HMarkah4(0))
                            strNilaiMata_Vokasional1 = ar_HMarkah4(1)
                            strJamKredit_Akademik1 = CDbl(ar_HMarkah4(2))
                            strJamKredit_Vokasional1 = ar_HMarkah4(3)
                            'end for semester
                        End If
                    End If
                    strTotalNilaiMataPNGKA = Math.Round((strNilaiMata_Akademik1 + strTotalNilaiMataAkademik) / (strJamKredit_Akademik1 + strJamKreditAkademik), 2)
                    strTotalNilaiMataPNGKV = Math.Round((strNilaiMata_Vokasional1 + strTotalNilaiMata) / (strJamKredit_Vokasional1 + strJamKreditV), 2)
                    strTotalNilaiMataPNGKK = Math.Round((strNilaiMata_Akademik1 + strTotalNilaiMataAkademik + strNilaiMata_Vokasional1 + strTotalNilaiMata) / (strJamKredit_Akademik1 + strJamKreditAkademik + strJamKredit_Vokasional1 + strJamKreditV), 2)

                    ' Format(Val(txtA.Text) * 1000 / Val(txtG.Text), "0.00")
                    Dim strMata_A As Double = Format(strTotalNilaiMataAkademik + strNilaiMata_Akademik1, "0.00")
                    Dim strJamKredit_A As Double = Format(strJamKredit_Akademik1 + strJamKreditAkademik, "0.00")
                    Dim strMata_V As Double = Format(strNilaiMata_Vokasional1 + strTotalNilaiMata, "0.00")
                    Dim strJamKredit_V As Double = Format(strJamKredit_Vokasional1 + strJamKreditV, "0.00")


                    ''**************************************
                    strSQL = "UPDATE kpmkv_pelajar_markah SET Jum_NilaiMata_Akademik_BM ='" & strTtlNilaiGredBM & "', Jum_JamKredit_Akademik_BM ='" & strJamKreditBM & "', "
                    strSQL += " Jum_NilaiMata_Akademik ='" & strMata_A & "', Jum_NilaiMata_Vokasional ='" & strMata_V & "', Jum_JamKredit_Akademik ='" & strJamKredit_A & "', Jum_JamKredit_Vokasional ='" & strJamKredit_V & "',"
                    strSQL += " PNG_Akademik ='" & strTotalNilaiMataAkademik & "', PNG_Vokasional ='" & strTotalNilaiMata & "', JamKredit_Akademik ='" & strJamKreditAkademik & "', JamKredit_Vokasional ='" & strJamKreditV & "',"
                    strSQL += " PNGBM ='" & strNilaiGredBM & "', PNGKBM ='" & strTotalNilaiMataPNGKBM & "', PNGA ='" & strTotalNilaiMataPNGA & "', PNGKA ='" & strTotalNilaiMataPNGKA & "',"
                    strSQL += " PNGV ='" & strTotalNilaiMataPNGV & "', PNGKV ='" & strTotalNilaiMataPNGKV & "', PNGK ='" & strTotalNilaiMataPNGK & "', PNGKK ='" & strTotalNilaiMataPNGKK & "'"
                    strSQL += " WHERE Tahun='" & ddlTahun.Text & "'"
                    strSQL += " AND Semester='" & ddlSemester.Text & "'"
                    strSQL += " AND Sesi='" & chkSesi.Text & "'"
                    strSQL += " AND PelajarID='" & strPelajarID & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)

                Next

            End If
        Next


    End Sub


    Private Sub btnGred_Click(sender As Object, e As EventArgs) Handles btnGred.Click
        lblMsg.Text = ""

        calculate_pngs()

        If ddlSemester.Text = 4 Then
            If Not strRet = "0" Then
                divMsg.Attributes("class") = "error"
                lblMsg.Text = "Tidak Berjaya mengemaskini keseluruhan PNGS"
            Else
                divMsg.Attributes("class") = "info"
                lblMsg.Text = "Berjaya mengemaskini keseluruhan PNGS"

            End If
        End If


    End Sub

    Private Sub btnGredKeseluruhan_Click(sender As Object, e As EventArgs) Handles btnGredKeseluruhan.Click

        Try

            lblMsg.Text = ""

            calculate_pngs_keseluruhan()

            If Not strRet = "0" Then
                divMsg.Attributes("class") = "info"
                lblMsg.Text = "Berjaya mengemaskini keseluruhan PNGS"
            Else
                divMsg.Attributes("class") = "info"
                lblMsg.Text = "Berjaya mengemaskini keseluruhan PNGS"
            End If

        Catch ex As Exception

            divMsg.Attributes("class") = "info"
            lblMsg.Text = "Berjaya mengemaskini keseluruhan PNGS"

        End Try



    End Sub

    Private Sub calculate_pngs_keseluruhan()

        Try

            Dim strKodBM As String = ""
            Dim strKodBI As String = ""
            Dim strKodMT As String = ""
            Dim strKodSC As String = ""
            Dim strKodSJ As String = ""
            Dim strKodPI As String = ""
            Dim strKodPM As String = ""

            Dim strNamaBM As String = ""
            Dim strNamaBI As String = ""
            Dim strNamaMT As String = ""
            Dim strNamaSC As String = ""
            Dim strNamaSJ As String = ""
            Dim strNamaPI As String = ""
            Dim strNamaPM As String = ""

            Dim strJamKreditBM As String = ""
            Dim strJamKreditBI As String = ""
            Dim strJamKreditMT As String = ""
            Dim strJamKreditSC As String = ""
            Dim strJamKreditSJ As String = ""
            Dim strJamKreditPI As String = ""
            Dim strJamKreditPM As String = ""

            strSQL = "SELECT  KODMatapelajaran, NamaMataPelajaran,JamKredit FROM kpmkv_matapelajaran WHERE Semester = '" & ddlSemester.Text & "'"
            strSQL += " AND Tahun='" & ddlTahun.SelectedValue & "'"
            strRet = oCommon.ExecuteSQL(strSQL)

            Dim sqlDA As New SqlDataAdapter(strSQL, objConn)
            Dim dsMataPelajaran As DataSet = New DataSet
            sqlDA.Fill(dsMataPelajaran, "AnyTable")

            For a As Integer = 0 To dsMataPelajaran.Tables(0).Rows.Count - 1

                If dsMataPelajaran.Tables(0).Rows(a).Item(1).ToString = "BAHASA MELAYU" Then

                    strKodBM = dsMataPelajaran.Tables(0).Rows(a).Item(0).ToString
                    strJamKreditBM = dsMataPelajaran.Tables(0).Rows(a).Item(2).ToString

                ElseIf dsMataPelajaran.Tables(0).Rows(a).Item(1).ToString = "BAHASA INGGERIS" Then

                    strKodBI = dsMataPelajaran.Tables(0).Rows(a).Item(0).ToString
                    strJamKreditBI = dsMataPelajaran.Tables(0).Rows(a).Item(2).ToString

                ElseIf dsMataPelajaran.Tables(0).Rows(a).Item(1).ToString = "SAINS" Then

                    strKodSC = dsMataPelajaran.Tables(0).Rows(a).Item(0).ToString
                    strJamKreditSC = dsMataPelajaran.Tables(0).Rows(a).Item(2).ToString

                ElseIf dsMataPelajaran.Tables(0).Rows(a).Item(1).ToString = "SAINS UNTUK TEKNOLOGI" Then

                    strKodSC = dsMataPelajaran.Tables(0).Rows(a).Item(0).ToString
                    strJamKreditSC = dsMataPelajaran.Tables(0).Rows(a).Item(2).ToString

                ElseIf dsMataPelajaran.Tables(0).Rows(a).Item(1).ToString = "SAINS UNTUK PENGAJIAN SOSIAL" Then

                    strKodSC = dsMataPelajaran.Tables(0).Rows(a).Item(0).ToString
                    strJamKreditSC = dsMataPelajaran.Tables(0).Rows(a).Item(2).ToString

                ElseIf dsMataPelajaran.Tables(0).Rows(a).Item(1).ToString = "SEJARAH" Then

                    strKodSJ = dsMataPelajaran.Tables(0).Rows(a).Item(0).ToString
                    strJamKreditSJ = dsMataPelajaran.Tables(0).Rows(a).Item(2).ToString

                ElseIf dsMataPelajaran.Tables(0).Rows(a).Item(1).ToString = "MATEMATIK" Then

                    strKodMT = dsMataPelajaran.Tables(0).Rows(a).Item(0).ToString
                    strJamKreditMT = dsMataPelajaran.Tables(0).Rows(a).Item(2).ToString

                ElseIf dsMataPelajaran.Tables(0).Rows(a).Item(1).ToString = "MATEMATIK UNTUK TEKNOLOGI" Then

                    strKodMT = dsMataPelajaran.Tables(0).Rows(a).Item(0).ToString
                    strJamKreditMT = dsMataPelajaran.Tables(0).Rows(a).Item(2).ToString

                ElseIf dsMataPelajaran.Tables(0).Rows(a).Item(1).ToString = "MATEMATIK UNTUK PENGAJIAN SOSIAL" Then

                    strKodMT = dsMataPelajaran.Tables(0).Rows(a).Item(0).ToString
                    strJamKreditMT = dsMataPelajaran.Tables(0).Rows(a).Item(2).ToString

                ElseIf dsMataPelajaran.Tables(0).Rows(a).Item(1).ToString = "PENDIDIKAN ISLAM" Then

                    strKodPI = dsMataPelajaran.Tables(0).Rows(a).Item(0).ToString
                    strJamKreditPI = dsMataPelajaran.Tables(0).Rows(a).Item(2).ToString

                ElseIf dsMataPelajaran.Tables(0).Rows(a).Item(1).ToString = "PENDIDIKAN MORAL" Then

                    strKodPM = dsMataPelajaran.Tables(0).Rows(a).Item(0).ToString
                    strJamKreditPM = dsMataPelajaran.Tables(0).Rows(a).Item(2).ToString

                End If

            Next

            strSQL = "SELECT RecordID FROM kpmkv_kolej WHERE Negeri IS NOT NULL"

            If Not ddlNegeri.SelectedValue = "" Then

                strSQL += " AND Negeri = '" & ddlNegeri.SelectedValue & "'"

            End If

            strRet = oCommon.ExecuteSQL(strSQL)

            sqlDA = New SqlDataAdapter(strSQL, objConn)
            Dim dsRecordID As DataSet = New DataSet
            sqlDA.Fill(dsRecordID, "AnyTable")

            For a As Integer = 0 To dsRecordID.Tables(0).Rows.Count - 1

                Dim strRecordID As String = dsRecordID.Tables(0).Rows(a).Item(0).ToString

                strSQL = "  SELECT kpmkv_pelajar.PelajarID, kpmkv_pelajar.AngkaGiliran, GredBM, GredBI, GredMT, GredSC, GredSJ, GredPI, GredPM, JenisKursus, kpmkv_pelajar.KursusID"
                strSQL += " FROM kpmkv_pelajar_markah "
                strSQL += " LEFT OUTER JOIN kpmkv_pelajar On kpmkv_pelajar_markah.PelajarID = kpmkv_pelajar.PelajarID"
                strSQL += " LEFT OUTER Join kpmkv_kursus On kpmkv_pelajar.KursusID = kpmkv_kursus.KursusID"
                strSQL += " WHERE kpmkv_pelajar.KolejRecordID='" & strRecordID & "'"
                strSQL += " AND kpmkv_pelajar.IsDeleted='N' AND kpmkv_pelajar.StatusID='2' AND kpmkv_pelajar.JenisCalonID='2'"

                If Not ddlTahun.Text = "" Then
                    strSQL += " AND kpmkv_pelajar.Tahun ='" & ddlTahun.Text & "'"
                End If

                If Not ddlSemester.Text = "" Then
                    strSQL += " AND kpmkv_pelajar.Semester ='" & ddlSemester.Text & "'"
                End If

                strRet = oCommon.ExecuteSQL(strSQL)

                sqlDA = New SqlDataAdapter(strSQL, objConn)
                Dim dsPelajarID As DataSet = New DataSet
                sqlDA.Fill(dsPelajarID, "AnyTable")

                For b As Integer = 0 To dsPelajarID.Tables(0).Rows.Count - 1

                    Dim strJamKredit As String = ""
                    Dim strNilaiMataV As Double = 0.0
                    Dim strJamKreditV As Double = 0.0
                    Dim strTotalNilaiMata As Double = 0.0


                    Dim strGred As String = ""

                    Dim strNilaiGred As Double = 0.0
                    Dim strNilaiGredBM As Double = 0.0
                    Dim strTtlNilaiGredBM As Double = 0.0

                    Dim strNilaiMataBM As Double = 0.0
                    Dim strNilaiMataBI As Double = 0.0
                    Dim strNilaiMataMT As Double = 0.0
                    Dim strNilaiMataSC As Double = 0.0
                    Dim strNilaiMataSJ As Double = 0.0
                    Dim strNilaiMataPI As Double = 0.0
                    Dim strNilaiMataPM As Double = 0.0

                    Dim strPelajarID As String = dsPelajarID.Tables(0).Rows(b).Item(0).ToString
                    Dim strAngkaGiliran As String = dsPelajarID.Tables(0).Rows(b).Item(1).ToString
                    Dim strGredBM As String = dsPelajarID.Tables(0).Rows(b).Item(2).ToString
                    Dim strGredBI As String = dsPelajarID.Tables(0).Rows(b).Item(3).ToString
                    Dim strGredMT As String = dsPelajarID.Tables(0).Rows(b).Item(4).ToString
                    Dim strGredSC As String = dsPelajarID.Tables(0).Rows(b).Item(5).ToString
                    Dim strGredSJ As String = dsPelajarID.Tables(0).Rows(b).Item(6).ToString
                    Dim strGredPI As String = dsPelajarID.Tables(0).Rows(b).Item(7).ToString
                    Dim strGredPM As String = dsPelajarID.Tables(0).Rows(b).Item(8).ToString
                    Dim strJenisKursus As String = dsPelajarID.Tables(0).Rows(b).Item(9).ToString
                    Dim strKursusID As String = dsPelajarID.Tables(0).Rows(b).Item(10).ToString

                    If strJenisKursus = "TECHNOLOGY" Then
                        strJenisKursus = "TEKNOLOGI"
                    End If

                    If Not strGredBM = "" Then
                        strSQL = "SELECT  Pointer, Status FROM kpmkv_gred WHERE Jenis='AKADEMIK' AND Gred='" & strGredBM & "'"
                        strRet = oCommon.getFieldValueEx(strSQL)
                        Dim ar_akademik As Array
                        ar_akademik = strRet.Split("|")

                        strNilaiGredBM = ar_akademik(0)
                        strNilaiMataBM = ar_akademik(0) * (strJamKreditBM) 'edit 0608
                        strTtlNilaiGredBM = ar_akademik(0) * (strJamKreditBM)
                        'strKompentasiBM = ar_akademik(1)
                    End If

                    If Not strGredBI = "" Then
                        strSQL = "SELECT  Pointer, Status FROM kpmkv_gred WHERE Jenis='AKADEMIK' AND Gred='" & strGredBI & "'"
                        strRet = oCommon.getFieldValueEx(strSQL)
                        Dim ar_akademikBI As Array
                        ar_akademikBI = strRet.Split("|")

                        'strNilaiGred = ar_akademikBI(0)
                        strNilaiMataBI = ar_akademikBI(0) * CDbl(strJamKreditBI)
                        'strKompentasiBI = ar_akademikBI(1)
                    End If

                    If Not strGredMT = "" Then
                        strSQL = "SELECT  Pointer, Status FROM kpmkv_gred WHERE Jenis='AKADEMIK' AND Gred='" & strGredMT & "'"
                        strRet = oCommon.getFieldValueEx(strSQL)
                        Dim ar_akademikMT As Array
                        ar_akademikMT = strRet.Split("|")

                        'strNilaiGred = ar_akademikMT(0)
                        strNilaiMataMT = ar_akademikMT(0) * CDbl(strJamKreditMT)
                        'strKompentasiMT = ar_akademikMT(1)
                    End If

                    If Not strGredSC = "" Then
                        strSQL = "SELECT  Pointer, Status FROM kpmkv_gred WHERE Jenis='AKADEMIK' AND Gred='" & strGredSC & "'"
                        strRet = oCommon.getFieldValueEx(strSQL)
                        Dim ar_akademikSC As Array
                        ar_akademikSC = strRet.Split("|")

                        'strNilaiGred = ar_akademikSC(0)
                        strNilaiMataSC = ar_akademikSC(0) * CDbl(strJamKreditSC)
                        'strKompentasiSC = ar_akademikSC(1)
                    End If

                    If Not strGredSJ = "" Then
                        strSQL = "SELECT  Pointer, Status FROM kpmkv_gred WHERE Jenis='AKADEMIK' AND Gred='" & strGredSJ & "'"
                        strRet = oCommon.getFieldValueEx(strSQL)
                        Dim ar_akademikSJ As Array
                        ar_akademikSJ = strRet.Split("|")

                        'strNilaiGred = ar_akademikSJ(0)
                        strNilaiMataSJ = ar_akademikSJ(0) * CDbl(strJamKreditSJ)
                        'strKompentasiSJ = ar_akademikSJ(1)
                    End If

                    If Not strGredPI = "" Then
                        strSQL = "SELECT  Pointer, Status FROM kpmkv_gred WHERE Jenis='AKADEMIK' AND Gred='" & strGredPI & "'"
                        strRet = oCommon.getFieldValueEx(strSQL)
                        Dim ar_akademikPI As Array
                        ar_akademikPI = strRet.Split("|")

                        'strNilaiGred = ar_akademikPI(0)
                        strNilaiMataPI = ar_akademikPI(0) * CDbl(strJamKreditPI)
                        'strKompentasiPI = ar_akademikPI(1)
                    End If

                    If Not strGredPM = "" Then
                        strSQL = "SELECT  Pointer, Status FROM kpmkv_gred WHERE Jenis='AKADEMIK' AND Gred='" & strGredPM & "'"
                        strRet = oCommon.getFieldValueEx(strSQL)
                        Dim ar_akademikPM As Array
                        ar_akademikPM = strRet.Split("|")

                        'strNilaiGred = ar_akademikPM(0)
                        strNilaiMataPM = ar_akademikPM(0) * CDbl(strJamKreditPM)
                        'strKompentasiPM = ar_akademikPM(1)
                    End If

                    strSQL = " SELECT COUNT(kpmkv_modul.KODMODUL) AS BILMODUL FROM kpmkv_modul LEFT OUTER JOIN "
                    strSQL += " kpmkv_kursus ON kpmkv_modul.KursusID = kpmkv_kursus.KursusID"
                    strSQL += " WHERE kpmkv_modul.Tahun='" & ddlTahun.Text & "'"
                    strSQL += " AND kpmkv_modul.Semester='" & ddlSemester.Text & "'"
                    strSQL += " AND kpmkv_modul.Sesi='" & chkSesi.Text & "'"
                    strSQL += " AND kpmkv_modul.KursusID ='" & strKursusID & "'"
                    Dim strBilModul As Integer = oCommon.getFieldValue(strSQL)

                    For j As Integer = 1 To strBilModul

                        strSQL = " SELECT kpmkv_modul.KodModul, kpmkv_modul.NamaModul, kpmkv_modul.JamKredit"
                        strSQL += " FROM  kpmkv_modul LEFT OUTER JOIN kpmkv_kursus ON kpmkv_modul.KursusID = kpmkv_kursus.KursusID "
                        strSQL += " LEFT OUTER JOIN kpmkv_pelajar ON kpmkv_kursus.KursusID = kpmkv_pelajar.KursusID"
                        strSQL += " WHERE kpmkv_modul.Tahun='" & ddlTahun.Text & "'"
                        strSQL += " AND kpmkv_modul.Semester='" & ddlSemester.Text & "'"
                        strSQL += " AND kpmkv_modul.Sesi='" & chkSesi.Text & "'"
                        strSQL += " AND SUBSTRING(kpmkv_modul.KodModul,6,1)='" & j & "'"
                        strSQL += " AND kpmkv_modul.KursusID='" & strKursusID & "'"
                        strSQL += " AND kpmkv_pelajar.PelajarID='" & strPelajarID & "'"
                        strSQL += " ORDER BY  kpmkv_modul.KodModul, kpmkv_modul.NamaModul, kpmkv_modul.JamKredit ASC"
                        strRet = oCommon.getFieldValueEx(strSQL)
                        Dim ar_VokM1 As Array
                        ar_VokM1 = strRet.Split("|")
                        'strKodMP = ar_VokM1(0)
                        'strNamaMP = ar_VokM1(1)
                        strJamKredit = ar_VokM1(2)
                        strJamKreditV += CDbl(strJamKredit)

                        strSQL = "SELECT GredV" & j & " FROM kpmkv_pelajar_markah WHERE PelajarID='" & strPelajarID & "' AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "' AND Sesi='" & chkSesi.Text & "'"
                        strGred = oCommon.getFieldValue(strSQL)
                        If Not strGred = "" Then
                            'gred
                            strSQL = "SELECT  Pointer, Kompentasi FROM kpmkv_gred WHERE Jenis='VOKASIONAL' AND Gred='" & strGred & "'"
                            strRet = oCommon.getFieldValueEx(strSQL)
                            ' ''--get Gred akademik info
                            Dim ar_VOKGred As Array
                            ar_VOKGred = strRet.Split("|")
                            strNilaiGred = ar_VOKGred(0)
                            strNilaiMataV = ar_VOKGred(0) * CDbl(strJamKredit)
                            'strKompentasi = ar_VOKGred(1)
                            strTotalNilaiMata += strNilaiMataV

                        End If

                    Next

                    Dim strJamKreditAkademik As Double

                    If Not strGredPI = "" Then
                        strJamKreditAkademik = CDbl(strJamKreditBM) + CDbl(strJamKreditBI) + CDbl(strJamKreditMT) + CDbl(strJamKreditSC) + CDbl(strJamKreditSJ) + CDbl(strJamKreditPI)
                    ElseIf Not strGredPM = "" Then
                        strJamKreditAkademik = CDbl(strJamKreditBM) + CDbl(strJamKreditBI) + CDbl(strJamKreditMT) + CDbl(strJamKreditSC) + CDbl(strJamKreditSJ) + CDbl(strJamKreditPI)
                    End If

                    'check by semester

                    Dim strTotalNilaiMataAkademik As Double = CDbl(strNilaiMataBM) + CDbl(strNilaiMataBI) + CDbl(strNilaiMataMT) + CDbl(strNilaiMataSC) + CDbl(strNilaiMataSJ) + CDbl(strNilaiMataPI) + CDbl(strNilaiMataPM)
                    Dim strTotalNilaiMataPNGA As Double = Math.Round((strTotalNilaiMataAkademik / strJamKreditAkademik), 2)
                    Dim strTotalNilaiMataPNGV As Double = Math.Round((strTotalNilaiMata / strJamKreditV), 2)
                    Dim strTotalNilaiMataPNGK As Double = Math.Round((strTotalNilaiMataAkademik + strTotalNilaiMata) / (strJamKreditAkademik + strJamKreditV), 2)
                    Dim strTotalNilaiMataPNGKBM As Double = 0.0

                    If ddlSemester.Text = 1 Then

                        strTotalNilaiMataPNGKBM = Math.Round((strNilaiMataBM / strJamKreditBM), 2)

                    ElseIf ddlSemester.Text = 2 Then
                        'checkin PelajarID for other semester
                        'change on 28July2016
                        strSQL = "SELECT PelajarID FROM kpmkv_pelajar WHERE AngkaGiliran='" & strAngkaGiliran & "'"
                        strSQL += " AND Semester='1'"
                        strSQL += " AND IsDeleted='N' AND StatusID='2'"
                        If oCommon.isExist(strSQL) = True Then
                            strSQL = "SELECT PelajarID,Tahun,Sesi FROM kpmkv_pelajar WHERE AngkaGiliran='" & strAngkaGiliran & "'"
                            strSQL += " AND Semester='1'"
                            strSQL += " AND IsDeleted='N' AND StatusID='2'"
                            strRet = oCommon.getFieldValueEx(strSQL)

                            Dim ar_HPelajar As Array
                            ar_HPelajar = strRet.Split("|")
                            Dim strP_ID As String = ar_HPelajar(0)
                            Dim strP_Tahun As String = ar_HPelajar(1)
                            Dim strP_Sesi As String = ar_HPelajar(2)

                            strSQL = "SELECT Jum_NilaiMata_Akademik_BM,Jum_JamKredit_Akademik_BM FROM kpmkv_pelajar_markah"
                            strSQL += " WHERE Tahun='" & strP_Tahun & "'"
                            strSQL += " AND Semester='1'"
                            strSQL += " AND Sesi='" & strP_Sesi & "'"
                            strSQL += " AND PelajarID='" & strP_ID & "'"
                            strRet = oCommon.getFieldValueEx(strSQL)

                            Dim ar_HMarkah2 As Array
                            ar_HMarkah2 = strRet.Split("|")
                            Dim strNilaiMata_Akademik_BM2 As Double = ar_HMarkah2(0)
                            Dim strJamKredit_Akademik_BM2 As Double = ar_HMarkah2(1)
                            strTotalNilaiMataPNGKBM = Math.Round((strNilaiMataBM + strNilaiMata_Akademik_BM2) / (strJamKreditBM + strJamKredit_Akademik_BM2), 2)
                        End If

                    ElseIf ddlSemester.Text = 3 Then
                        'change on 28July2016
                        strSQL = "SELECT PelajarID FROM kpmkv_pelajar WHERE AngkaGiliran='" & strAngkaGiliran & "'"
                        strSQL += " AND Semester='1'"
                        strSQL += " AND IsDeleted='N' AND StatusID='2'"
                        If oCommon.isExist(strSQL) = True Then
                            strSQL = "SELECT PelajarID,Tahun,Sesi FROM kpmkv_pelajar WHERE AngkaGiliran='" & strAngkaGiliran & "'"
                            strSQL += " AND Semester='1'"
                            strSQL += " AND IsDeleted='N' AND StatusID='2'"
                            strRet = oCommon.getFieldValueEx(strSQL)

                            Dim ar_HPelajar2 As Array
                            ar_HPelajar2 = strRet.Split("|")
                            Dim strP_ID2 As String = ar_HPelajar2(0)
                            Dim strP_Tahun2 As String = ar_HPelajar2(1)
                            Dim strP_Sesi2 As String = ar_HPelajar2(2)

                            strSQL = "SELECT Jum_NilaiMata_Akademik_BM,Jum_JamKredit_Akademik_BM FROM kpmkv_pelajar_markah"
                            strSQL += " WHERE Tahun='" & strP_Tahun2 & "'"
                            strSQL += " AND Semester='1'"
                            strSQL += " AND Sesi='" & strP_Sesi2 & "'"
                            strSQL += " AND PelajarID='" & strP_ID2 & "'"
                            strRet = oCommon.getFieldValueEx(strSQL)
                            Dim ar_HMarkah1_3 As Array
                            ar_HMarkah1_3 = strRet.Split("|")
                            Dim strNilaiMata_Akademik_BM1_3 As Double = ar_HMarkah1_3(0)
                            Dim strJamKredit_Akademik_BM1_3 As Double = ar_HMarkah1_3(1)

                            'if exist
                            'change on 28July2016
                            strSQL = "SELECT PelajarID FROM kpmkv_pelajar WHERE AngkaGiliran='" & strAngkaGiliran & "'"
                            strSQL += " AND Semester='2'"
                            strSQL += " AND IsDeleted='N' AND StatusID='2'"
                            If oCommon.isExist(strSQL) = True Then
                                strSQL = "SELECT PelajarID,Tahun,Sesi FROM kpmkv_pelajar WHERE AngkaGiliran='" & strAngkaGiliran & "'"
                                strSQL += " AND Semester='2'"
                                strSQL += " AND IsDeleted='N' AND StatusID='2'"
                                strRet = oCommon.getFieldValueEx(strSQL)

                                Dim ar_HPelajar3 As Array
                                ar_HPelajar3 = strRet.Split("|")
                                Dim strP_ID3 As String = ar_HPelajar3(0)
                                Dim strP_Tahun3 As String = ar_HPelajar3(1)
                                Dim strP_Sesi3 As String = ar_HPelajar3(2)

                                strSQL = "SELECT Jum_NilaiMata_Akademik_BM,Jum_JamKredit_Akademik_BM FROM kpmkv_pelajar_markah"
                                strSQL += " WHERE Tahun='" & strP_Tahun3 & "'"
                                strSQL += " AND Semester='2'"
                                strSQL += " AND Sesi='" & strP_Sesi3 & "'"
                                strSQL += " AND PelajarID='" & strP_ID3 & "'"
                                strRet = oCommon.getFieldValueEx(strSQL)

                                Dim ar_HMarkah3 As Array
                                ar_HMarkah3 = strRet.Split("|")
                                Dim strNilaiMata_Akademik_BM3 As Double = CDbl(ar_HMarkah3(0))
                                Dim strJamKredit_Akademik_BM3 As Double = ar_HMarkah3(1)
                                strTotalNilaiMataPNGKBM = Math.Round((strNilaiMataBM + strNilaiMata_Akademik_BM3 + strNilaiMata_Akademik_BM1_3) / (strJamKreditBM + strJamKredit_Akademik_BM3 + strJamKredit_Akademik_BM1_3), 2)
                            End If
                        End If

                    ElseIf ddlSemester.Text = 4 Then
                        'checkin PelajarID for other semester
                        'change on 28July2016
                        strSQL = "SELECT PelajarID FROM kpmkv_pelajar WHERE AngkaGiliran='" & strAngkaGiliran & "'"
                        strSQL += " AND Semester='1'"
                        strSQL += " AND IsDeleted='N' AND StatusID='2'"
                        If oCommon.isExist(strSQL) = True Then
                            strSQL = "SELECT PelajarID,Tahun,Sesi FROM kpmkv_pelajar WHERE AngkaGiliran='" & strAngkaGiliran & "'"
                            strSQL += " AND Semester='1'"
                            strSQL += " AND IsDeleted='N' AND StatusID='2'"
                            strRet = oCommon.getFieldValueEx(strSQL)

                            Dim ar_HPelajar4 As Array
                            ar_HPelajar4 = strRet.Split("|")
                            Dim strP_ID4 As String = ar_HPelajar4(0)
                            Dim strP_Tahun4 As String = ar_HPelajar4(1)
                            Dim strP_Sesi4 As String = ar_HPelajar4(2)

                            strSQL = "SELECT Jum_NilaiMata_Akademik_BM,Jum_JamKredit_Akademik_BM FROM kpmkv_pelajar_markah"
                            strSQL += " WHERE Tahun='" & strP_Tahun4 & "'"
                            strSQL += " AND Semester='1'"
                            strSQL += " AND Sesi='" & strP_Sesi4 & "'"
                            strSQL += " AND PelajarID='" & strP_ID4 & "'"
                            strRet = oCommon.getFieldValueEx(strSQL)

                            Dim ar_HMarkah1_4 As Array
                            ar_HMarkah1_4 = strRet.Split("|")
                            Dim strNilaiMata_Akademik_BM1_4 As Double = ar_HMarkah1_4(0)
                            Dim strJamKredit_Akademik_BM1_4 As Double = ar_HMarkah1_4(1)

                            'if exist
                            'change on 28July2016
                            strSQL = "SELECT PelajarID FROM kpmkv_pelajar WHERE AngkaGiliran='" & strAngkaGiliran & "'"
                            strSQL += " AND Semester='2'"
                            strSQL += " AND IsDeleted='N' AND StatusID='2'"
                            If oCommon.isExist(strSQL) = True Then
                                strSQL = "SELECT PelajarID,Tahun,Sesi FROM kpmkv_pelajar WHERE AngkaGiliran='" & strAngkaGiliran & "'"
                                strSQL += " AND Semester='2'"
                                strSQL += " AND IsDeleted='N' AND StatusID='2'"
                                strRet = oCommon.getFieldValueEx(strSQL)

                                Dim ar_HPelajar5 As Array
                                ar_HPelajar5 = strRet.Split("|")
                                Dim strP_ID5 As String = ar_HPelajar5(0)
                                Dim strP_Tahun5 As String = ar_HPelajar5(1)
                                Dim strP_Sesi5 As String = ar_HPelajar5(2)

                                strSQL = "SELECT Jum_NilaiMata_Akademik_BM,Jum_JamKredit_Akademik_BM FROM kpmkv_pelajar_markah"
                                strSQL += " WHERE Tahun='" & strP_Tahun5 & "'"
                                strSQL += " AND Semester='2'"
                                strSQL += " AND Sesi='" & strP_Sesi5 & "'"
                                strSQL += " AND PelajarID='" & strP_ID5 & "'"
                                strRet = oCommon.getFieldValueEx(strSQL)
                                Dim ar_HMarkah2_4 As Array
                                ar_HMarkah2_4 = strRet.Split("|")
                                Dim strNilaiMata_Akademik_BM2_4 As Double = CDbl(ar_HMarkah2_4(0))
                                Dim strJamKredit_Akademik_BM2_4 As Double = ar_HMarkah2_4(1)

                                'if exist
                                'change on 28July2016
                                strSQL = "SELECT PelajarID FROM kpmkv_pelajar WHERE AngkaGiliran='" & strAngkaGiliran & "'"
                                strSQL += " AND Semester='3'"
                                strSQL += " AND IsDeleted='N' AND StatusID='2'"
                                If oCommon.isExist(strSQL) = True Then
                                    strSQL = "SELECT PelajarID,Tahun,Sesi FROM kpmkv_pelajar WHERE AngkaGiliran='" & strAngkaGiliran & "'"
                                    strSQL += " AND Semester='3'"
                                    strSQL += " AND IsDeleted='N' AND StatusID='2'"
                                    strRet = oCommon.getFieldValueEx(strSQL)

                                    Dim ar_HPelajar6 As Array
                                    ar_HPelajar6 = strRet.Split("|")
                                    Dim strP_ID6 As String = ar_HPelajar6(0)
                                    Dim strP_Tahun6 As String = ar_HPelajar6(1)
                                    Dim strP_Sesi6 As String = ar_HPelajar6(2)

                                    strSQL = "SELECT Jum_NilaiMata_Akademik_BM,Jum_JamKredit_Akademik_BM FROM kpmkv_pelajar_markah"
                                    strSQL += " WHERE Tahun='" & strP_Tahun6 & "'"
                                    strSQL += " AND Semester='3'"
                                    strSQL += " AND Sesi='" & strP_Sesi6 & "'"
                                    strSQL += " AND PelajarID='" & strP_ID6 & "'"
                                    strRet = oCommon.getFieldValueEx(strSQL)
                                    ' ''--get Gred akademik info
                                    Dim ar_HMarkah3_4 As Array
                                    ar_HMarkah3_4 = strRet.Split("|")
                                    Dim strNilaiMata_Akademik_BM3_4 As Double = CDbl(ar_HMarkah3_4(0))
                                    Dim strJamKredit_Akademik_BM3_4 As Double = ar_HMarkah3_4(1)
                                    strTotalNilaiMataPNGKBM = Math.Round((strNilaiMataBM + strNilaiMata_Akademik_BM3_4 + strNilaiMata_Akademik_BM2_4 + strNilaiMata_Akademik_BM1_4) / (strJamKreditBM + strJamKredit_Akademik_BM3_4 + strJamKredit_Akademik_BM2_4 + strJamKredit_Akademik_BM1_4), 2)
                                End If

                            End If
                        End If

                    End If

                    Dim strNilaiMata_Akademik1 As Double = 0.0
                    Dim strNilaiMata_Vokasional1 As Double = 0.0
                    Dim strJamKredit_Akademik1 As Double = 0.0
                    Dim strJamKredit_Vokasional1 As Double = 0.0
                    Dim strTotalNilaiMataPNGKA As Double = 0.0
                    Dim strTotalNilaiMataPNGKV As Double = 0.0
                    Dim strTotalNilaiMataPNGKK As Double = 0.0
                    Dim strP_ID_P As String = ""
                    Dim strP_Tahun_P As String = ""
                    Dim strP_Sesi_P As String = ""

                    If ddlSemester.Text = 1 Then
                        strTotalNilaiMataPNGKA = Math.Round((strTotalNilaiMataAkademik / strJamKreditAkademik), 2)
                        strTotalNilaiMataPNGKV = Math.Round((strTotalNilaiMata / strJamKreditV), 2)
                        strTotalNilaiMataPNGKK = Math.Round((strTotalNilaiMataAkademik + strTotalNilaiMata) / (strJamKreditAkademik + strJamKreditV), 2)

                    ElseIf ddlSemester.Text = 2 Then
                        'if exist
                        'change on 28July2016
                        strSQL = "SELECT PelajarID FROM kpmkv_pelajar WHERE AngkaGiliran='" & strAngkaGiliran & "'"
                        strSQL += " AND Semester='1'"
                        strSQL += " AND IsDeleted='N' AND StatusID='2'"
                        If oCommon.isExist(strSQL) = True Then
                            strSQL = "SELECT PelajarID,Tahun,Sesi FROM kpmkv_pelajar WHERE AngkaGiliran='" & strAngkaGiliran & "'"
                            strSQL += " AND Semester='1'"
                            strSQL += " AND IsDeleted='N' AND StatusID='2'"
                            strRet = oCommon.getFieldValueEx(strSQL)

                            Dim ar_HPelajar As Array
                            ar_HPelajar = strRet.Split("|")
                            strP_ID_P = ar_HPelajar(0)
                            strP_Tahun_P = ar_HPelajar(1)
                            strP_Sesi_P = ar_HPelajar(2)

                            strSQL = "SELECT Jum_NilaiMata_Akademik,Jum_NilaiMata_Vokasional,Jum_JamKredit_Akademik,Jum_JamKredit_Vokasional FROM kpmkv_pelajar_markah"
                            strSQL += " WHERE Tahun='" & strP_Tahun_P & "'"
                            strSQL += " AND Semester='1'"
                            strSQL += " AND Sesi='" & strP_Sesi_P & "'"
                            strSQL += " AND PelajarID='" & strP_ID_P & "'"
                            strRet = oCommon.getFieldValueEx(strSQL)

                            ' ''--get Gred VOK info
                            Dim ar_HMarkah2 As Array
                            ar_HMarkah2 = strRet.Split("|")
                            strNilaiMata_Akademik1 = CDbl(ar_HMarkah2(0))
                            strNilaiMata_Vokasional1 = ar_HMarkah2(1)
                            strJamKredit_Akademik1 = CDbl(ar_HMarkah2(2))
                            strJamKredit_Vokasional1 = ar_HMarkah2(3)
                        End If

                    ElseIf ddlSemester.Text = 3 Then
                        'if exist
                        'change on 28July2016
                        strSQL = "SELECT PelajarID FROM kpmkv_pelajar WHERE AngkaGiliran='" & strAngkaGiliran & "'"
                        strSQL += " AND Semester='2'"
                        strSQL += " AND IsDeleted='N' AND StatusID='2'"
                        If oCommon.isExist(strSQL) = True Then
                            strSQL = "SELECT PelajarID,Tahun,Sesi FROM kpmkv_pelajar WHERE AngkaGiliran='" & strAngkaGiliran & "'"
                            strSQL += " AND Semester='2'"
                            strSQL += " AND IsDeleted='N' AND StatusID='2'"
                            strRet = oCommon.getFieldValueEx(strSQL)

                            Dim ar_HPelajar As Array
                            ar_HPelajar = strRet.Split("|")
                            strP_ID_P = ar_HPelajar(0)
                            strP_Tahun_P = ar_HPelajar(1)
                            strP_Sesi_P = ar_HPelajar(2)

                            strSQL = "SELECT Jum_NilaiMata_Akademik,Jum_NilaiMata_Vokasional,Jum_JamKredit_Akademik,Jum_JamKredit_Vokasional FROM kpmkv_pelajar_markah"
                            strSQL += " WHERE Tahun='" & strP_Tahun_P & "'"
                            strSQL += " AND Semester='2'"
                            strSQL += " AND Sesi='" & strP_Sesi_P & "'"
                            strSQL += " AND PelajarID='" & strP_ID_P & "'"
                            strRet = oCommon.getFieldValueEx(strSQL)

                            ' ''--get Gred VOK info
                            Dim ar_HMarkah3 As Array
                            ar_HMarkah3 = strRet.Split("|")
                            strNilaiMata_Akademik1 = CDbl(ar_HMarkah3(0))
                            strNilaiMata_Vokasional1 = ar_HMarkah3(1)
                            strJamKredit_Akademik1 = CDbl(ar_HMarkah3(2))
                            strJamKredit_Vokasional1 = ar_HMarkah3(3)
                        End If

                    ElseIf ddlSemester.Text = 4 Then
                        'if exist
                        'change on 28July2016
                        strSQL = "SELECT PelajarID FROM kpmkv_pelajar WHERE AngkaGiliran='" & strAngkaGiliran & "'"
                        strSQL += " AND Semester='3'"
                        strSQL += " AND IsDeleted='N' AND StatusID='2'"
                        If oCommon.isExist(strSQL) = True Then
                            strSQL = "SELECT PelajarID,Tahun,Sesi FROM kpmkv_pelajar WHERE AngkaGiliran='" & strAngkaGiliran & "'"
                            strSQL += " AND Semester='3'"
                            strSQL += " AND IsDeleted='N' AND StatusID='2'"
                            strRet = oCommon.getFieldValueEx(strSQL)
                            Dim ar_HPelajar As Array
                            ar_HPelajar = strRet.Split("|")
                            strP_ID_P = ar_HPelajar(0)
                            strP_Tahun_P = ar_HPelajar(1)
                            strP_Sesi_P = ar_HPelajar(2)

                            strSQL = "SELECT Jum_NilaiMata_Akademik,Jum_NilaiMata_Vokasional,Jum_JamKredit_Akademik,Jum_JamKredit_Vokasional FROM kpmkv_pelajar_markah"
                            strSQL += " WHERE Tahun='" & strP_Tahun_P & "'"
                            strSQL += " AND Semester='3'"
                            strSQL += " AND Sesi='" & strP_Sesi_P & "'"
                            strSQL += " AND PelajarID='" & strP_ID_P & "'"
                            strRet = oCommon.getFieldValueEx(strSQL)

                            ' ''--get Gred VOK info
                            Dim ar_HMarkah4 As Array
                            ar_HMarkah4 = strRet.Split("|")
                            strNilaiMata_Akademik1 = CDbl(ar_HMarkah4(0))
                            strNilaiMata_Vokasional1 = ar_HMarkah4(1)
                            strJamKredit_Akademik1 = CDbl(ar_HMarkah4(2))
                            strJamKredit_Vokasional1 = ar_HMarkah4(3)
                            'end for semester
                        End If
                    End If
                    strTotalNilaiMataPNGKA = Math.Round((strNilaiMata_Akademik1 + strTotalNilaiMataAkademik) / (strJamKredit_Akademik1 + strJamKreditAkademik), 2)
                    strTotalNilaiMataPNGKV = Math.Round((strNilaiMata_Vokasional1 + strTotalNilaiMata) / (strJamKredit_Vokasional1 + strJamKreditV), 2)
                    strTotalNilaiMataPNGKK = Math.Round((strNilaiMata_Akademik1 + strTotalNilaiMataAkademik + strNilaiMata_Vokasional1 + strTotalNilaiMata) / (strJamKredit_Akademik1 + strJamKreditAkademik + strJamKredit_Vokasional1 + strJamKreditV), 2)

                    ' Format(Val(txtA.Text) * 1000 / Val(txtG.Text), "0.00")
                    Dim strMata_A As Double = Format(strTotalNilaiMataAkademik + strNilaiMata_Akademik1, "0.00")
                    Dim strJamKredit_A As Double = Format(strJamKredit_Akademik1 + strJamKreditAkademik, "0.00")
                    Dim strMata_V As Double = Format(strNilaiMata_Vokasional1 + strTotalNilaiMata, "0.00")
                    Dim strJamKredit_V As Double = Format(strJamKredit_Vokasional1 + strJamKreditV, "0.00")


                    ''**************************************
                    strSQL = "UPDATE kpmkv_pelajar_markah SET Jum_NilaiMata_Akademik_BM ='" & strTtlNilaiGredBM & "', Jum_JamKredit_Akademik_BM ='" & strJamKreditBM & "', "
                    strSQL += " Jum_NilaiMata_Akademik ='" & strMata_A & "', Jum_NilaiMata_Vokasional ='" & strMata_V & "', Jum_JamKredit_Akademik ='" & strJamKredit_A & "', Jum_JamKredit_Vokasional ='" & strJamKredit_V & "',"
                    strSQL += " PNG_Akademik ='" & strTotalNilaiMataAkademik & "', PNG_Vokasional ='" & strTotalNilaiMata & "', JamKredit_Akademik ='" & strJamKreditAkademik & "', JamKredit_Vokasional ='" & strJamKreditV & "',"
                    strSQL += " PNGBM ='" & strNilaiGredBM & "', PNGKBM ='" & strTotalNilaiMataPNGKBM & "', PNGA ='" & strTotalNilaiMataPNGA & "', PNGKA ='" & strTotalNilaiMataPNGKA & "',"
                    strSQL += " PNGV ='" & strTotalNilaiMataPNGV & "', PNGKV ='" & strTotalNilaiMataPNGKV & "', PNGK ='" & strTotalNilaiMataPNGK & "', PNGKK ='" & strTotalNilaiMataPNGKK & "'"
                    strSQL += " WHERE Tahun='" & ddlTahun.Text & "'"
                    strSQL += " AND Semester='" & ddlSemester.Text & "'"
                    strSQL += " AND Sesi='" & chkSesi.Text & "'"
                    strSQL += " AND PelajarID='" & strPelajarID & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)

                Next

            Next

        Catch ex As Exception
            lblMsg.Text = ex.Message
        End Try

    End Sub

End Class