Imports System.Data.SqlClient
Public Class jana_keseluruhan_akademik1
    Inherits System.Web.UI.UserControl
    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""
    Dim strPelajarID As String = ""


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
                ddlKodKursus.SelectedValue = "0"

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
        If Not ddlTahun.Text = "" Then
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
            ddlKodKursus.Items.Add(New ListItem("SEMUA", "0"))

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
        If Not ddlTahun.Text = "" Then
            strWhere += " AND b.Tahun ='" & ddlTahun.SelectedValue & "'"
        End If
        '--sesi
        If Not chkSesi.Text = "" Then
            strWhere += " AND b.Sesi ='" & chkSesi.Text & "'"
        End If

        '--Kod
        If Not ddlKodKursus.Text = "0" Then
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

    Private Sub Akademik_markah()
        Dim strKursusID As String = ""
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

                Dim strPelajarID As String = ""

                Dim strCount As Integer = ds.Tables(0).Rows.Count - 1
                For iloop As Integer = 0 To strCount
                    strPelajarID = (ds.Tables(0).Rows(iloop).Item(0).ToString())

                    ' Dim GredBM As Integer
                    Dim BerterusanBM As Integer
                    Dim AkhiranBM As Integer

                    strSQL = "SELECT PB FROM kpmkv_aka_weightage WHERE Tahun='" & ddlTahun.Text & "'"
                    BerterusanBM = oCommon.getFieldValue(strSQL)

                    strSQL = "SELECT PA FROM kpmkv_aka_weightage WHERE Tahun='" & ddlTahun.Text & "'"
                    AkhiranBM = oCommon.getFieldValue(strSQL)

                    If ddlSemester.Text = "4" Then

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

                        'get mykad
                        strSQL = " SELECT Mykad FROM kpmkv_pelajar"
                        strSQL += " WHERE PelajarID='" & strPelajarID & "'"
                        Dim strMYKAD1 As String = oCommon.getFieldValue(strSQL)

                        'get pelajarid
                        strSQL = " SELECT PelajarID FROM kpmkv_pelajar"
                        strSQL += " WHERE StatusID='2' AND IsDeleted='N' AND Semester='1'"
                        strSQL += " AND Mykad='" & strMYKAD1 & "'"
                        Dim strPelajarID1 As String = oCommon.getFieldValue(strSQL)

                        'get bm sem 1
                        strSQL = "Select BahasaMelayu from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID1 & "'"
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
                        B_BahasaMelayuSem3 = oCommon.getFieldValue(strSQL)
                        'round up
                        B_BahasaMelayuSem3 = Math.Ceiling(B_BahasaMelayuSem3)

                        'get bm sem 4 PB
                        strSQL = "Select B_BahasaMelayu from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
                        B_BahasaMelayuSem4 = oCommon.getFieldValue(strSQL)

                        'get bm sem 4 PA
                        strSQL = "Select A_BahasaMelayu3 from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
                        A_BahasaMelayuSem4 = oCommon.getFieldValue(strSQL)

                        Dim Kertas1 As Integer = 0
                        Dim Kertas2 As Integer = 0

                        strSQL = "SELECT A_BahasaMelayu1, A_BahasaMelayu2 FROM kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
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

                        If Kertas1 = -1 Or Kertas2 = -1 Then

                            strSQL = "UPDATE kpmkv_pelajar_markah SET BahasaMelayu='-1' Where PelajarID='" & strPelajarID & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)

                            strSQL = "UPDATE kpmkv_pelajar_markah SET PointerBMSetara='-1' Where PelajarID='" & strPelajarID & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)

                        ElseIf Not ((B_BahasaMelayuSem4) = "-1" Or (A_BahasaMelayuSem4) = "-1") Then
                            PB4 = Math.Ceiling((B_BahasaMelayuSem4 / 100) * BerterusanBM)
                            'PABmSetara = Math.Ceiling(A_BahasaMelayuSem4)

                            PABmSetara = Math.Ceiling((A_BahasaMelayuSem4 / 100) * 40)
                            PAPB4 = Math.Ceiling(((Kertas1 + Kertas2 + PABmSetara) / 280) * AkhiranBM)
                            'PAPB4 = Math.Ceiling(PAPB * AkhiranBM)

                            'gred sem 4 
                            Dim PointSem4 As Integer = Math.Ceiling(PB4 + PAPB4)
                            strSQL = "UPDATE kpmkv_pelajar_markah SET BahasaMelayu='" & PointSem4 & "' Where PelajarID='" & strPelajarID & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)

                            If (B_BahasaMelayuSem1 = "-1" Or B_BahasaMelayuSem2 = "-1" Or B_BahasaMelayuSem3 = "-1") Then
                                PointerBMSetara = "-1"
                            Else
                                PointerBMSetara = Math.Ceiling((((B_BahasaMelayuSem1 / 100) * 10) + ((B_BahasaMelayuSem2 / 100) * 10) + ((B_BahasaMelayuSem3 / 100) * 10) + ((PointSem4 / 100) * 70)))
                            End If

                            strSQL = "UPDATE kpmkv_pelajar_markah SET PointerBMSetara='" & PointerBMSetara & "' Where PelajarID='" & strPelajarID & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)

                        ElseIf ((B_BahasaMelayuSem4) = "-1" Or (A_BahasaMelayuSem4) = "-1") Then

                            strSQL = "UPDATE kpmkv_pelajar_markah SET BahasaMelayu='-1' Where PelajarID='" & strPelajarID & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)

                            strSQL = "UPDATE kpmkv_pelajar_markah SET PointerBMSetara='-1' Where PelajarID='" & strPelajarID & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)
                        End If

                    Else

                        Dim AM_BahasaMelayu As Integer
                        Dim BM_BahasaMelayu As Integer
                        Dim B_BahasaMelayu As Double
                        Dim A_BahasaMelayu As Double
                        Dim PointerBM As Integer

                        strSQL = "Select B_BahasaMelayu from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
                        B_BahasaMelayu = oCommon.getFieldValue(strSQL)
                        'round up
                        B_BahasaMelayu = Math.Ceiling(B_BahasaMelayu)

                        strSQL = "Select A_BahasaMelayu from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
                        A_BahasaMelayu = oCommon.getFieldValue(strSQL)
                        'round up
                        A_BahasaMelayu = Math.Ceiling(A_BahasaMelayu)

                        'checkin Markah
                        If Not (B_BahasaMelayu) = "-1" And Not (A_BahasaMelayu) = "-1" Then
                            BM_BahasaMelayu = Math.Ceiling((B_BahasaMelayu / 100) * BerterusanBM)
                            AM_BahasaMelayu = Math.Ceiling((A_BahasaMelayu / 100) * AkhiranBM)
                            PointerBM = Math.Ceiling(BM_BahasaMelayu + AM_BahasaMelayu)
                            strSQL = "UPDATE kpmkv_pelajar_markah SET BahasaMelayu='" & PointerBM & "' "
                            strSQL += " WHERE PelajarID ='" & strPelajarID & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)

                        ElseIf (B_BahasaMelayu) = "-1" Or (A_BahasaMelayu) = "-1" Then
                            strSQL = "UPDATE kpmkv_pelajar_markah SET BahasaMelayu='-1' Where PelajarID='" & strPelajarID & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)
                        End If

                    End If

                    Dim BM_BahasaInggeris As Integer
                    Dim AM_BahasaInggeris As Integer
                    Dim BerterusanBI As Integer
                    Dim AkhiranBI As Integer
                    Dim B_BahasaInggeris As Double
                    Dim A_BahasaInggeris As Double
                    Dim PointerBI As Integer

                    strSQL = "SELECT PB FROM kpmkv_aka_weightage WHERE Tahun='" & ddlTahun.Text & "'"
                    BerterusanBI = oCommon.getFieldValue(strSQL)

                    strSQL = "SELECT PA FROM kpmkv_aka_weightage WHERE Tahun='" & ddlTahun.Text & "'"
                    AkhiranBI = oCommon.getFieldValue(strSQL)

                    strSQL = "Select B_BahasaInggeris from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
                    B_BahasaInggeris = oCommon.getFieldValue(strSQL)
                    'round up
                    B_BahasaInggeris = Math.Ceiling(B_BahasaInggeris)

                    strSQL = "Select A_BahasaInggeris from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
                    A_BahasaInggeris = oCommon.getFieldValue(strSQL)
                    'round up
                    A_BahasaInggeris = Math.Ceiling(A_BahasaInggeris)

                    'checkin Markah
                    If Not (B_BahasaInggeris) = "-1" And Not (A_BahasaInggeris) = "-1" Then
                        BM_BahasaInggeris = Math.Ceiling((B_BahasaInggeris / 100) * BerterusanBI)
                        AM_BahasaInggeris = Math.Ceiling((A_BahasaInggeris / 100) * AkhiranBI)
                        PointerBI = Math.Ceiling(BM_BahasaInggeris + AM_BahasaInggeris)
                        strSQL = "UPDATE kpmkv_pelajar_markah SET BahasaInggeris='" & PointerBI & "' Where PelajarID='" & strPelajarID & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)

                    ElseIf (B_BahasaInggeris) = "-1" Or (A_BahasaInggeris) = "-1" Then
                        strSQL = "UPDATE kpmkv_pelajar_markah SET BahasaInggeris='-1' Where PelajarID='" & strPelajarID & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)
                    End If

                    '------------------------------------------------------------------------------------------------------------------------

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

                    strSQL = "SELECT PB FROM kpmkv_aka_weightage WHERE Tahun='" & ddlTahun.Text & "'"
                    BerterusanSc = oCommon.getFieldValue(strSQL)

                    strSQL = "SELECT PA FROM kpmkv_aka_weightage WHERE Tahun='" & ddlTahun.Text & "'"
                    AkhiranSc = oCommon.getFieldValue(strSQL)

                    strSQL = "Select B_Science1 from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
                    B_Science1 = oCommon.getFieldValue(strSQL)
                    'round up
                    B_Science1 = Math.Ceiling(B_Science1)

                    strSQL = "Select A_Science1 from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
                    A_Science1 = oCommon.getFieldValue(strSQL)
                    'round up
                    A_Science1 = Math.Ceiling(A_Science1)

                    strSQL = "Select A_Science2 from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
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

                            strSQL = "UPDATE kpmkv_pelajar_markah SET Science='" & PointerSC & "' Where PelajarID='" & strPelajarID & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)
                        ElseIf (A_Science1) = "-1" Or (A_Science2) = "-1" Then
                            strSQL = "UPDATE kpmkv_pelajar_markah SET Science='-1' Where PelajarID='" & strPelajarID & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)
                        End If
                    Else

                        If Not (A_Science1) = "-1" And Not (A_Science2) = "-1" Then
                            AM_Science1 = Math.Ceiling((A_Science1 / 100) * 70) '70%
                            AM_Science2 = Math.Ceiling((A_Science2 / 100) * 70) '70%
                            PointerSC1 = Math.Ceiling(BM_Science1)
                            PointerSC2 = Math.Ceiling((AM_Science1) + (AM_Science2))
                            PointerSC = Math.Ceiling((PointerSC1) + (PointerSC2))

                            strSQL = "UPDATE kpmkv_pelajar_markah SET Science='" & PointerSC & "' Where PelajarID='" & strPelajarID & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)
                        ElseIf (A_Science1) = "-1" Or (A_Science2) = "-1" Then
                            strSQL = "UPDATE kpmkv_pelajar_markah SET Science='-1' Where PelajarID='" & strPelajarID & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)
                        End If

                    End If

                    'SC ------------------------------------------------------------------------------------------------------------

                    Dim BM_Sejarah As Integer
                    Dim AM_Sejarah As Integer
                    Dim BerterusanSJ As Integer
                    Dim AkhiranSJ As Integer
                    Dim B_Sejarah As Double
                    Dim A_Sejarah As Double
                    Dim PointerSJ As Integer
                    'Dim GredSJ As Integer 

                    strSQL = "SELECT PB FROM kpmkv_aka_weightage WHERE Tahun='" & ddlTahun.Text & "'"
                    BerterusanSJ = oCommon.getFieldValue(strSQL)

                    strSQL = "SELECT PA FROM kpmkv_aka_weightage WHERE Tahun='" & ddlTahun.Text & "'"
                    AkhiranSJ = oCommon.getFieldValue(strSQL)

                    strSQL = "Select B_Sejarah from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
                    B_Sejarah = oCommon.getFieldValue(strSQL)
                    'round up
                    B_Sejarah = Math.Ceiling(B_Sejarah)

                    strSQL = "Select A_Sejarah from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
                    A_Sejarah = oCommon.getFieldValue(strSQL)
                    'round up
                    A_Sejarah = Math.Ceiling(A_Sejarah)

                    'checkin Markah
                    If Not (B_Sejarah) = "-1" And Not (A_Sejarah) = "-1" Then
                        BM_Sejarah = Math.Ceiling((B_Sejarah / 100) * BerterusanSJ)
                        AM_Sejarah = Math.Ceiling((A_Sejarah / 100) * AkhiranSJ)
                        PointerSJ = Math.Ceiling(BM_Sejarah + AM_Sejarah)
                        strSQL = "UPDATE kpmkv_pelajar_markah SET Sejarah='" & PointerSJ & "' Where PelajarID='" & strPelajarID & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)

                    ElseIf (B_Sejarah) = "-1" Or (A_Sejarah) = "-1" Then
                        strSQL = "UPDATE kpmkv_pelajar_markah SET Sejarah='-1' Where PelajarID='" & strPelajarID & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)
                    End If

                    ''updated on 08102018 to calculate pointerSJsetara

                    If ddlSemester.Text = "4" Then

                        Dim SJ1 As String
                        Dim SJ2 As String
                        Dim SJ3 As String
                        Dim SJ4 As String

                        Dim SJ1Int As Integer
                        Dim SJ2Int As Integer
                        Dim SJ3Int As Integer
                        Dim SJ4Int As Integer

                        Dim SJ1Double As Double
                        Dim SJ2Double As Double
                        Dim SJ3Double As Double
                        Dim SJ4Double As Double

                        Dim PointerSJSetara As Integer
                        Dim PointerSJSetaraDouble As Double

                        ''get MYKAD pelajar
                        strSQL = "SELECT MYKAD FROM kpmkv_pelajar WHERE PelajarID = '" & strPelajarID & "'"
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
                        SJ1Double = (10 / 100) * Double.Parse(SJ1)
                        SJ1Int = Math.Ceiling((10 / 100) * Double.Parse(SJ1))

                        strSQL = "  SELECT Sejarah FROM kpmkv_pelajar_markah WHERE PelajarID = '" & strPelajarID2 & "' AND Semester = '2'"
                        SJ2 = oCommon.getFieldValue(strSQL)
                        SJ2Double = (10 / 100) * Double.Parse(SJ2)
                        SJ2Int = Math.Ceiling((10 / 100) * Double.Parse(SJ2))

                        strSQL = "  SELECT Sejarah FROM kpmkv_pelajar_markah WHERE PelajarID = '" & strPelajarID3 & "' AND Semester = '3'"
                        SJ3 = oCommon.getFieldValue(strSQL)
                        SJ3Double = (10 / 100) * Double.Parse(SJ3)
                        SJ3Int = Math.Ceiling((10 / 100) * Double.Parse(SJ3))

                        strSQL = "  SELECT Sejarah FROM kpmkv_pelajar_markah WHERE PelajarID = '" & strPelajarID4 & "' AND Semester = '4'"
                        SJ4 = oCommon.getFieldValue(strSQL)
                        SJ4Double = (70 / 100) * Double.Parse(SJ4)
                        SJ4Int = Math.Ceiling((70 / 100) * Double.Parse(SJ4))

                        If SJ1.ToString = "T" Or SJ1 = -1 Then

                            PointerSJSetara = -1
                            PointerSJSetaraDouble = -1

                        ElseIf SJ2.ToString = "T" Or SJ2 = -1 Then

                            PointerSJSetara = -1
                            PointerSJSetaraDouble = -1

                        ElseIf SJ3.ToString = "T" Or SJ3 = -1 Then

                            PointerSJSetara = -1
                            PointerSJSetaraDouble = -1

                        Else

                            PointerSJSetara = Integer.Parse(SJ1Int) + Integer.Parse(SJ2Int) + Integer.Parse(SJ3Int) + Integer.Parse(SJ4Int)
                            PointerSJSetaraDouble = SJ1Double + SJ2Double + SJ3Double + SJ4Double

                        End If

                        strSQL = "UPDATE kpmkv_pelajar_markah SET PointerSJSetara='" & PointerSJSetaraDouble & "' Where PelajarID='" & strPelajarID & "'"
                        strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='4'"
                        strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & ddlKodKursus.SelectedValue & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)

                    End If
                    '-------------------------------------------------------------------------------------------------------------

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

                    strSQL = "SELECT PB FROM kpmkv_aka_weightage WHERE Tahun='" & ddlTahun.Text & "'"
                    BerterusanPI = oCommon.getFieldValue(strSQL)

                    strSQL = "SELECT PA FROM kpmkv_aka_weightage WHERE Tahun='" & ddlTahun.Text & "'"
                    AkhiranPI = oCommon.getFieldValue(strSQL)

                    strSQL = "Select B_PendidikanIslam1 from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
                    B_PendidikanIslam1 = oCommon.getFieldValue(strSQL)
                    'round up
                    B_PendidikanIslam1 = Math.Ceiling(B_PendidikanIslam1)

                    strSQL = "Select A_PendidikanIslam1 from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
                    A_PendidikanIslam1 = oCommon.getFieldValue(strSQL)
                    'round up
                    A_PendidikanIslam1 = Math.Ceiling(A_PendidikanIslam1)

                    strSQL = "Select A_PendidikanIslam2 from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
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

                        strSQL = "UPDATE kpmkv_pelajar_markah SET PendidikanIslam='" & PointerPI & "' Where PelajarID='" & strPelajarID & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)
                    ElseIf (A_PendidikanIslam1) = "-1" Or (A_PendidikanIslam2) = "-1" Then
                        strSQL = "UPDATE kpmkv_pelajar_markah SET PendidikanIslam='-1' Where PelajarID='" & strPelajarID & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)
                    End If
                    '-------------------------------------------------------------------------------------------------------------

                    Dim BM_PendidikanMoral As Integer
                    Dim AM_PendidikanMoral As Integer
                    Dim BerterusanPM As Integer
                    Dim AkhiranPM As Integer
                    Dim B_PendidikanMoral As Integer
                    Dim A_PendidikanMoral As Integer
                    Dim PointerPM As Integer
                    'Dim GredPM As Integer 

                    strSQL = "SELECT PB FROM kpmkv_aka_weightage WHERE Tahun='" & ddlTahun.Text & "'"
                    BerterusanPM = oCommon.getFieldValue(strSQL)

                    strSQL = "SELECT PA FROM kpmkv_aka_weightage WHERE Tahun='" & ddlTahun.Text & "'"
                    AkhiranPM = oCommon.getFieldValue(strSQL)

                    strSQL = "Select B_PendidikanMoral from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
                    B_PendidikanMoral = oCommon.getFieldValue(strSQL)
                    'round up
                    B_PendidikanMoral = Math.Ceiling(B_PendidikanMoral)

                    strSQL = "Select A_PendidikanMoral from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
                    A_PendidikanMoral = oCommon.getFieldValue(strSQL)
                    'round up
                    A_PendidikanMoral = Math.Ceiling(A_PendidikanMoral)

                    'checkin Markah
                    If Not (B_PendidikanMoral) = "-1" And Not (A_PendidikanMoral) = "-1" Then
                        BM_PendidikanMoral = Math.Ceiling((B_PendidikanMoral / 100) * BerterusanPM)
                        AM_PendidikanMoral = Math.Ceiling((A_PendidikanMoral / 100) * AkhiranPM)
                        PointerPM = Math.Ceiling(BM_PendidikanMoral + AM_PendidikanMoral)
                        strSQL = "UPDATE kpmkv_pelajar_markah SET PendidikanMoral='" & PointerPM & "' Where PelajarID='" & strPelajarID & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)
                    ElseIf (B_PendidikanMoral) = "-1" Or (A_PendidikanMoral) = "-1" Then
                        strSQL = "UPDATE kpmkv_pelajar_markah SET PendidikanMoral='-1' Where PelajarID='" & strPelajarID & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)
                    End If
                    '-------------------------------------------------------------------------------------------------------------

                    Dim BM_Mathematics As Integer
                    Dim AM_Mathematics As Integer
                    Dim BerterusanMT As Integer
                    Dim AkhiranMT As Integer
                    Dim B_Mathematics As Integer
                    Dim A_Mathematics As Integer
                    Dim PointerMT As Integer
                    'Dim GredMT As Integer 

                    strSQL = "SELECT PB FROM kpmkv_aka_weightage WHERE Tahun='" & ddlTahun.Text & "'"
                    BerterusanMT = oCommon.getFieldValue(strSQL)

                    strSQL = "SELECT PA FROM kpmkv_aka_weightage WHERE Tahun='" & ddlTahun.Text & "'"
                    AkhiranMT = oCommon.getFieldValue(strSQL)

                    strSQL = "Select B_Mathematics from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
                    B_Mathematics = oCommon.getFieldValue(strSQL)
                    'round up
                    B_Mathematics = Math.Ceiling(B_Mathematics)

                    strSQL = "Select A_Mathematics from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
                    A_Mathematics = oCommon.getFieldValue(strSQL)
                    'round up
                    A_Mathematics = Math.Ceiling(A_Mathematics)

                    'checkin Markah
                    If Not (B_Mathematics) = "-1" And Not (A_Mathematics) = "-1" Then
                        BM_Mathematics = Math.Ceiling((B_Mathematics / 100) * BerterusanMT)
                        AM_Mathematics = Math.Ceiling((A_Mathematics / 100) * AkhiranMT)
                        PointerMT = Math.Ceiling(BM_Mathematics + AM_Mathematics)
                        strSQL = "UPDATE kpmkv_pelajar_markah SET Mathematics='" & PointerMT & "' Where PelajarID='" & strPelajarID & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)
                    ElseIf (B_Mathematics) = "-1" Or (A_Mathematics) = "-1" Then
                        strSQL = "UPDATE kpmkv_pelajar_markah SET Mathematics='-1' Where PelajarID='" & strPelajarID & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)
                    End If
                Next
            End If
        Next

    End Sub

    Private Sub Akademik_gred()

        Dim strKursusID As String = ""
        For i As Integer = 0 To datRespondent.Rows.Count - 1

            Dim cb As CheckBox = datRespondent.Rows(i).FindControl("chkSelect")

            If cb.Checked = True Then

                strKursusID = datRespondent.DataKeys(i).Value.ToString()

                strSQL = "  SELECT kpmkv_pelajar.PelajarID FROM kpmkv_pelajar
                    LEFT JOIN kpmkv_kolej ON kpmkv_kolej.RecordID = kpmkv_pelajar.KolejRecordID
                    LEFT JOIN kpmkv_negeri ON kpmkv_negeri.Negeri = kpmkv_kolej.Negeri
                    WHERE kpmkv_negeri.Negeri IS NOT NULL AND kpmkv_pelajar.StatusID = '2'"

                If Not ddlNegeri.SelectedValue = "" Then

                    strSQL += " AND kpmkv_negeri.Negeri = '" & ddlNegeri.SelectedValue & "'"

                End If

                If Not ddlKolej.SelectedValue = "" Then

                    strSQL += " AND kpmkv_kolej.RecordID = '" & ddlKolej.SelectedValue & "'"

                End If

                If Not ddlTahun.Text = "" Then

                    strSQL += " AND kpmkv_pelajar.Tahun = '" & ddlTahun.SelectedValue & "'"

                End If

                If Not ddlSemester.Text = "" Then

                    strSQL += " AND kpmkv_pelajar.Semester = '" & ddlSemester.SelectedValue & "'"

                End If

                If Not chkSesi.Text = "" Then

                    strSQL += " AND kpmkv_pelajar.Sesi ='" & chkSesi.Text & "'"

                End If


                strSQL += " ORDER BY kpmkv_negeri.Negeri, kpmkv_pelajar.AngkaGiliran"

                strRet = oCommon.ExecuteSQL(strSQL)

                Dim sqlDA As New SqlDataAdapter(strSQL, objConn)
                Dim ds As DataSet = New DataSet
                sqlDA.Fill(ds, "AnyTable")

                Dim strPelajarID As String = ""

                Dim strCount As Integer = ds.Tables(0).Rows.Count - 1
                For iloop As Integer = 0 To strCount
                    strPelajarID = (ds.Tables(0).Rows(iloop).Item(0).ToString())

                    Dim BM As String
                    Dim GredBM As String

                    Dim Kertas1 As Integer = 0
                    Dim Kertas2 As Integer = 0

                    If ddlSemester.SelectedValue = 4 Then

                        strSQL = "SELECT A_BahasaMelayu1, A_BahasaMelayu2 FROM kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
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

                    End If

                    strSQL = "SELECT BahasaMelayu as BM FROM kpmkv_pelajar_markah"
                    strSQL += " Where PelajarID='" & strPelajarID & "'"
                    BM = oCommon.getFieldValue(strSQL)

                    If Kertas1 = -1 Or Kertas2 = -1 Then

                        strSQL = "UPDATE kpmkv_pelajar_markah SET GredBM = 'T' Where PelajarID='" & strPelajarID & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)

                    Else

                        If String.IsNullOrEmpty(BM) Then
                            strSQL = "UPDATE kpmkv_pelajar_markah SET GredBM='' Where PelajarID='" & strPelajarID & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)
                        Else
                            strSQL = "SELECT TOP ( 1 ) Gred FROM  kpmkv_gred WHERE '" & Integer.Parse(BM) & "' BETWEEN MarkahFrom AND MarkahTo AND Jenis='AKADEMIK'"
                            GredBM = oCommon.getFieldValue(strSQL)

                            strSQL = "UPDATE kpmkv_pelajar_markah SET GredBM='" & GredBM & "' Where PelajarID='" & strPelajarID & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)
                        End If

                    End If

                    '-----------------------------------------------------------------
                    Dim BI As String
                    Dim GredBI As String

                    strSQL = "SELECT BahasaInggeris FROM kpmkv_pelajar_markah"
                    strSQL += " Where PelajarID='" & strPelajarID & "'"
                    BI = oCommon.getFieldValue(strSQL)

                    'If BI = "0" Then
                    If String.IsNullOrEmpty(BI) Then
                    Else
                        strSQL = "SELECT TOP ( 1 ) Gred FROM  kpmkv_gred WHERE '" & Integer.Parse(BI) & "' BETWEEN MarkahFrom AND MarkahTo AND Jenis='AKADEMIK'"
                        GredBI = oCommon.getFieldValue(strSQL)

                        strSQL = "UPDATE kpmkv_pelajar_markah SET GredBI='" & GredBI & "' Where PelajarID='" & strPelajarID & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)

                    End If
                    '------------------------------------------------------------------------------------------------------------------------
                    Dim SC As Integer
                    Dim GredSC As String

                    strSQL = "SELECT Science FROM kpmkv_pelajar_markah"
                    strSQL += " Where PelajarID='" & strPelajarID & "'"
                    SC = oCommon.getFieldValue(strSQL)

                    'If SC = 0 Then
                    If String.IsNullOrEmpty(SC) Then
                    Else
                        strSQL = "SELECT TOP ( 1 ) Gred FROM  kpmkv_gred WHERE '" & SC & "' BETWEEN MarkahFrom AND MarkahTo AND Jenis='AKADEMIK'"
                        GredSC = oCommon.getFieldValue(strSQL)

                        strSQL = "UPDATE kpmkv_pelajar_markah SET GredSC='" & GredSC & "' Where PelajarID='" & strPelajarID & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)

                    End If
                    '------------------------------------------------------------------------------------------------------------

                    Dim SJ As String
                    Dim GredSJ As String

                    strSQL = "SELECT Sejarah FROM kpmkv_pelajar_markah"
                    strSQL += " Where PelajarID='" & strPelajarID & "'"
                    SJ = oCommon.getFieldValue(strSQL)

                    'If SJ = "0" Then
                    If String.IsNullOrEmpty(SJ) Then
                    Else
                        strSQL = "SELECT TOP ( 1 ) Gred FROM  kpmkv_gred WHERE '" & Integer.Parse(SJ) & "' BETWEEN MarkahFrom AND MarkahTo AND Jenis='AKADEMIK'"
                        GredSJ = oCommon.getFieldValue(strSQL)

                        strSQL = "UPDATE kpmkv_pelajar_markah SET GredSJ='" & GredSJ & "' Where PelajarID='" & strPelajarID & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)

                    End If
                    '-------------------------------------------------------------------------------------------------------------

                    Dim PI As String
                    Dim GredPI As String

                    strSQL = "SELECT PendidikanIslam FROM kpmkv_pelajar_markah"
                    strSQL += " Where PelajarID='" & strPelajarID & "'"
                    PI = oCommon.getFieldValue(strSQL)

                    If PI = "0" Then
                    ElseIf String.IsNullOrEmpty(PI) Then
                    Else
                        strSQL = "SELECT TOP ( 1 ) Gred FROM  kpmkv_gred WHERE '" & Integer.Parse(PI) & "' BETWEEN MarkahFrom AND MarkahTo AND Jenis='AKADEMIK'"
                        GredPI = oCommon.getFieldValue(strSQL)

                        strSQL = "UPDATE kpmkv_pelajar_markah SET GredPI='" & GredPI & "' Where PelajarID='" & strPelajarID & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)

                    End If
                    '-------------------------------------------------------------------------------------------------------------

                    Dim PM As String
                    Dim GredPM As String

                    strSQL = "SELECT PendidikanMoral FROM kpmkv_pelajar_markah"
                    strSQL += " Where PelajarID='" & strPelajarID & "'"
                    PM = oCommon.getFieldValue(strSQL)

                    If PM = "0" Then
                    ElseIf String.IsNullOrEmpty(PM) Then
                    Else
                        strSQL = "SELECT TOP ( 1 ) Gred FROM  kpmkv_gred WHERE '" & Integer.Parse(PM) & "' BETWEEN MarkahFrom AND MarkahTo AND Jenis='AKADEMIK'"
                        GredPM = oCommon.getFieldValue(strSQL)

                        strSQL = "UPDATE kpmkv_pelajar_markah SET GredPM='" & GredPM & "' Where PelajarID='" & strPelajarID & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)

                    End If
                    '-------------------------------------------------------------------------------------------------------------

                    Dim MT As String
                    Dim GredMT As String

                    strSQL = "SELECT Mathematics FROM kpmkv_pelajar_markah"
                    strSQL += " Where PelajarID='" & strPelajarID & "'"
                    MT = oCommon.getFieldValue(strSQL)

                    'If MT = "0" Then
                    If String.IsNullOrEmpty(MT) Then
                    Else
                        strSQL = "SELECT TOP ( 1 ) Gred FROM  kpmkv_gred WHERE '" & Integer.Parse(MT) & "' BETWEEN MarkahFrom AND MarkahTo AND Jenis='AKADEMIK'"
                        GredMT = oCommon.getFieldValue(strSQL)

                        strSQL = "UPDATE kpmkv_pelajar_markah SET GredMT='" & GredMT & "' Where PelajarID='" & strPelajarID & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)
                    End If

                Next
            End If
        Next
    End Sub

    Private Sub btnGred_Click(sender As Object, e As EventArgs) Handles btnGred.Click
        lblMsg.Text = ""

        Akademik_markah()
        Akademik_gred()
        If Not strRet = "0" Then
            divMsg.Attributes("class") = "error"
            lblMsg.Text = "Tidak Berjaya mengemaskini keseluruhan gred Pentaksiran Akhir Akademik"
        Else
            divMsg.Attributes("class") = "info"
            lblMsg.Text = "Berjaya mengemaskini keseluruhan gred Pentaksiran Akhir Akademik"

        End If

    End Sub

    Private Sub btnGredNegeri_Click(sender As Object, e As EventArgs) Handles btnGredNegeri.Click

        Try

            strRet = "0"

            Debug.WriteLine(System.DateTime.Now)

            lblMsg.Text = ""

            Akademik_markah_Negeri()
            Akademik_gred_Negeri()
            If Not strRet = "0" Then
                divMsg.Attributes("class") = "error"
                lblMsg.Text = "Tidak Berjaya mengemaskini keseluruhan gred Pentaksiran Akhir Akademik"
            Else
                divMsg.Attributes("class") = "info"
                lblMsg.Text = "Berjaya mengemaskini keseluruhan gred Pentaksiran Akhir Akademik"

            End If

            Debug.WriteLine(System.DateTime.Now)

        Catch ex As Exception
            divMsg.Attributes("class") = "error"
            lblMsg.Text = "Tidak Berjaya mengemaskini keseluruhan gred Pentaksiran Akhir Akademik." & ex.Message & " PelajarID : " & strPelajarID
        End Try

    End Sub

    Private Sub Akademik_markah_Negeri()

        Dim strPelajarID As String = ""

        strRet = oCommon.ExecuteSQL(janaBerperingkat)

        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)
        Dim ds As DataSet = New DataSet
        sqlDA.Fill(ds, "AnyTable")

        For i As Integer = 0 To ds.Tables(0).Rows.Count - 1

            Dim tempSkipIfNull As String = ""

            strPelajarID = ds.Tables(0).Rows(i).Item(0).ToString

            ' Dim GredBM As Integer
            Dim BerterusanBM As Integer
            Dim AkhiranBM As Integer

            strSQL = "SELECT PB FROM kpmkv_aka_weightage WHERE Tahun='" & ddlTahun.Text & "'"
            BerterusanBM = oCommon.getFieldValue(strSQL)

            strSQL = "SELECT PA FROM kpmkv_aka_weightage WHERE Tahun='" & ddlTahun.Text & "'"
            AkhiranBM = oCommon.getFieldValue(strSQL)

            If ddlSemester.Text = "4" Then

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

                'get mykad
                strSQL = " SELECT Mykad FROM kpmkv_pelajar"
                strSQL += " WHERE PelajarID='" & strPelajarID & "'"
                Dim strMYKAD1 As String = oCommon.getFieldValue(strSQL)

                'get pelajarid
                strSQL = " SELECT PelajarID FROM kpmkv_pelajar"
                strSQL += " WHERE StatusID='2' AND IsDeleted='N' AND Semester='1'"
                strSQL += " AND Mykad='" & strMYKAD1 & "'"
                Dim strPelajarID1 As String = oCommon.getFieldValue(strSQL)

                'get bm sem 1
                strSQL = "Select BahasaMelayu from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID1 & "'"
                tempSkipIfNull = oCommon.getFieldValue(strSQL)

                If Not tempSkipIfNull = "" Then

                    B_BahasaMelayuSem1 = oCommon.getFieldValue(strSQL)
                    'round up
                    B_BahasaMelayuSem1 = Math.Ceiling(B_BahasaMelayuSem1)

                Else

                    Continue For

                End If
                '----------------------------------------------------------------------------

                'get pelajarid
                strSQL = " SELECT PelajarID FROM kpmkv_pelajar"
                strSQL += " WHERE StatusID='2' AND IsDeleted='N' AND Semester='2'"
                strSQL += " AND Mykad='" & strMYKAD1 & "'"
                Dim strPelajarID2 As String = oCommon.getFieldValue(strSQL)

                'get Bm sem 2
                strSQL = "Select BahasaMelayu from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID2 & "'"
                tempSkipIfNull = oCommon.getFieldValue(strSQL)

                If Not tempSkipIfNull = "" Then

                    B_BahasaMelayuSem2 = oCommon.getFieldValue(strSQL)
                    'round up
                    B_BahasaMelayuSem2 = Math.Ceiling(B_BahasaMelayuSem2)

                Else

                    Continue For

                End If

                'get pelajarid
                strSQL = " SELECT PelajarID FROM kpmkv_pelajar"
                strSQL += " WHERE StatusID='2' AND IsDeleted='N' AND Semester='3'"
                strSQL += " AND Mykad='" & strMYKAD1 & "'"
                Dim strPelajarID3 As String = oCommon.getFieldValue(strSQL)

                'get bm sem 3
                strSQL = "Select BahasaMelayu from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID3 & "'"
                tempSkipIfNull = oCommon.getFieldValue(strSQL)

                If Not tempSkipIfNull = "" Then

                    B_BahasaMelayuSem3 = oCommon.getFieldValue(strSQL)
                    'round up
                    B_BahasaMelayuSem3 = Math.Ceiling(B_BahasaMelayuSem3)

                Else

                    Continue For

                End If



                'get bm sem 4 PB
                strSQL = "Select B_BahasaMelayu from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
                tempSkipIfNull = oCommon.getFieldValue(strSQL)

                If Not tempSkipIfNull = "" Then

                    B_BahasaMelayuSem4 = oCommon.getFieldValue(strSQL)

                Else

                    Continue For

                End If

                'get bm sem 4 PA
                strSQL = "Select A_BahasaMelayu3 from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
                tempSkipIfNull = oCommon.getFieldValue(strSQL)

                If Not tempSkipIfNull = "" Then

                    A_BahasaMelayuSem4 = oCommon.getFieldValue(strSQL)

                Else

                    Continue For

                End If


                Dim Kertas1 As Integer = 0
                Dim Kertas2 As Integer = 0

                strSQL = "SELECT A_BahasaMelayu1, A_BahasaMelayu2 FROM kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
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

                If Kertas1 = -1 Or Kertas2 = -1 Then

                    strSQL = "UPDATE kpmkv_pelajar_markah SET BahasaMelayu='-1' Where PelajarID='" & strPelajarID & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)

                    strSQL = "UPDATE kpmkv_pelajar_markah SET PointerBMSetara='-1' Where PelajarID='" & strPelajarID & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)

                ElseIf Not ((B_BahasaMelayuSem4) = "-1" Or (A_BahasaMelayuSem4) = "-1") Then
                    PB4 = Math.Ceiling((B_BahasaMelayuSem4 / 100) * BerterusanBM)
                    'PABmSetara = Math.Ceiling(A_BahasaMelayuSem4)

                    PABmSetara = Math.Ceiling((A_BahasaMelayuSem4 / 100) * 40)
                    PAPB4 = Math.Ceiling(((Kertas1 + Kertas2 + PABmSetara) / 280) * AkhiranBM)
                    'PAPB4 = Math.Ceiling(PAPB * AkhiranBM)

                    'gred sem 4 
                    Dim PointSem4 As Integer = Math.Ceiling(PB4 + PAPB4)
                    strSQL = "UPDATE kpmkv_pelajar_markah SET BahasaMelayu='" & PointSem4 & "' Where PelajarID='" & strPelajarID & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)

                    If (B_BahasaMelayuSem1 = "-1" Or B_BahasaMelayuSem2 = "-1" Or B_BahasaMelayuSem3 = "-1") Then
                        PointerBMSetara = "-1"
                    Else
                        PointerBMSetara = Math.Ceiling((((B_BahasaMelayuSem1 / 100) * 10) + ((B_BahasaMelayuSem2 / 100) * 10) + ((B_BahasaMelayuSem3 / 100) * 10) + ((PointSem4 / 100) * 70)))
                    End If

                    strSQL = "UPDATE kpmkv_pelajar_markah SET PointerBMSetara='" & PointerBMSetara & "' Where PelajarID='" & strPelajarID & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)

                ElseIf ((B_BahasaMelayuSem4) = "-1" Or (A_BahasaMelayuSem4) = "-1") Then

                    strSQL = "UPDATE kpmkv_pelajar_markah SET BahasaMelayu='-1' Where PelajarID='" & strPelajarID & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)

                    strSQL = "UPDATE kpmkv_pelajar_markah SET PointerBMSetara='-1' Where PelajarID='" & strPelajarID & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)
                End If

            Else

                Dim AM_BahasaMelayu As Integer
                Dim BM_BahasaMelayu As Integer
                Dim B_BahasaMelayu As Double
                Dim A_BahasaMelayu As Double
                Dim PointerBM As Integer

                strSQL = "Select B_BahasaMelayu from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
                tempSkipIfNull = oCommon.getFieldValue(strSQL)

                If Not tempSkipIfNull = "" Then

                    B_BahasaMelayu = oCommon.getFieldValue(strSQL)
                    'round up
                    B_BahasaMelayu = Math.Ceiling(B_BahasaMelayu)
                Else

                    Continue For

                End If

                strSQL = "Select A_BahasaMelayu from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
                tempSkipIfNull = oCommon.getFieldValue(strSQL)

                If Not tempSkipIfNull = "" Then

                    A_BahasaMelayu = oCommon.getFieldValue(strSQL)
                    'round up
                    A_BahasaMelayu = Math.Ceiling(A_BahasaMelayu)

                Else

                    Continue For

                End If

                'checkin Markah
                If Not (B_BahasaMelayu) = "-1" And Not (A_BahasaMelayu) = "-1" Then
                    BM_BahasaMelayu = Math.Ceiling((B_BahasaMelayu / 100) * BerterusanBM)
                    AM_BahasaMelayu = Math.Ceiling((A_BahasaMelayu / 100) * AkhiranBM)
                    PointerBM = Math.Ceiling(BM_BahasaMelayu + AM_BahasaMelayu)
                    strSQL = "UPDATE kpmkv_pelajar_markah SET BahasaMelayu='" & PointerBM & "' "
                    strSQL += " WHERE PelajarID ='" & strPelajarID & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)

                ElseIf (B_BahasaMelayu) = "-1" Or (A_BahasaMelayu) = "-1" Then
                    strSQL = "UPDATE kpmkv_pelajar_markah SET BahasaMelayu='-1' Where PelajarID='" & strPelajarID & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)
                End If

            End If

            Dim BM_BahasaInggeris As Integer
            Dim AM_BahasaInggeris As Integer
            Dim BerterusanBI As Integer
            Dim AkhiranBI As Integer
            Dim B_BahasaInggeris As Double
            Dim A_BahasaInggeris As Double
            Dim PointerBI As Integer

            strSQL = "SELECT PB FROM kpmkv_aka_weightage WHERE Tahun='" & ddlTahun.Text & "'"
            BerterusanBI = oCommon.getFieldValue(strSQL)

            strSQL = "SELECT PA FROM kpmkv_aka_weightage WHERE Tahun='" & ddlTahun.Text & "'"
            AkhiranBI = oCommon.getFieldValue(strSQL)

            strSQL = "Select B_BahasaInggeris from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
            tempSkipIfNull = oCommon.getFieldValue(strSQL)

            If Not tempSkipIfNull = "" Then

                B_BahasaInggeris = oCommon.getFieldValue(strSQL)
                'round up
                B_BahasaInggeris = Math.Ceiling(B_BahasaInggeris)

            Else

                Continue For

            End If



            strSQL = "Select A_BahasaInggeris from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
            tempSkipIfNull = oCommon.getFieldValue(strSQL)

            If Not tempSkipIfNull = "" Then

                A_BahasaInggeris = oCommon.getFieldValue(strSQL)
                'round up
                A_BahasaInggeris = Math.Ceiling(A_BahasaInggeris)

            Else

                Continue For

            End If

            'checkin Markah
            If Not (B_BahasaInggeris) = "-1" And Not (A_BahasaInggeris) = "-1" Then
                BM_BahasaInggeris = Math.Ceiling((B_BahasaInggeris / 100) * BerterusanBI)
                AM_BahasaInggeris = Math.Ceiling((A_BahasaInggeris / 100) * AkhiranBI)
                PointerBI = Math.Ceiling(BM_BahasaInggeris + AM_BahasaInggeris)
                strSQL = "UPDATE kpmkv_pelajar_markah SET BahasaInggeris='" & PointerBI & "' Where PelajarID='" & strPelajarID & "'"
                strRet = oCommon.ExecuteSQL(strSQL)

            ElseIf (B_BahasaInggeris) = "-1" Or (A_BahasaInggeris) = "-1" Then
                strSQL = "UPDATE kpmkv_pelajar_markah SET BahasaInggeris='-1' Where PelajarID='" & strPelajarID & "'"
                strRet = oCommon.ExecuteSQL(strSQL)
            End If

            '------------------------------------------------------------------------------------------------------------------------

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

            strSQL = "SELECT PB FROM kpmkv_aka_weightage WHERE Tahun='" & ddlTahun.Text & "'"
            BerterusanSc = oCommon.getFieldValue(strSQL)

            strSQL = "SELECT PA FROM kpmkv_aka_weightage WHERE Tahun='" & ddlTahun.Text & "'"
            AkhiranSc = oCommon.getFieldValue(strSQL)

            strSQL = "Select B_Science1 from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
            tempSkipIfNull = oCommon.getFieldValue(strSQL)

            If Not tempSkipIfNull = "" Then

                B_Science1 = oCommon.getFieldValue(strSQL)
                'round up
                B_Science1 = Math.Ceiling(B_Science1)
            Else

                Continue For

            End If



            strSQL = "Select A_Science1 from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
            tempSkipIfNull = oCommon.getFieldValue(strSQL)

            If Not tempSkipIfNull = "" Then

                A_Science1 = oCommon.getFieldValue(strSQL)
                'round up
                A_Science1 = Math.Ceiling(A_Science1)

            Else

                Continue For

            End If



            strSQL = "Select A_Science2 from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
            tempSkipIfNull = oCommon.getFieldValue(strSQL)

            If Not tempSkipIfNull = "" Then

                A_Science2 = oCommon.getFieldValue(strSQL)
                'round up
                A_Science2 = Math.Ceiling(A_Science2)

            Else

                Continue For

            End If



            'check sem 3 n 4 ada  kertas 1
            BM_Science1 = Math.Ceiling((B_Science1 / 100) * BerterusanSc)

            If ddlSemester.Text = "1" Or ddlSemester.Text = "2" Then

                If Not (A_Science1) = "-1" And Not (A_Science2) = "-1" Then
                    AM_Science1 = Math.Ceiling((A_Science1 / 100) * 50) '50%

                    AM_Science2 = Math.Ceiling((A_Science2 / 100) * 20) '20% 

                    PointerSC1 = Math.Ceiling(BM_Science1)
                    PointerSC2 = Math.Ceiling((AM_Science1) + (AM_Science2))
                    PointerSC = Math.Ceiling((PointerSC1) + (PointerSC2))

                    strSQL = "UPDATE kpmkv_pelajar_markah SET Science='" & PointerSC & "' Where PelajarID='" & strPelajarID & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)
                ElseIf (A_Science1) = "-1" Or (A_Science2) = "-1" Then
                    strSQL = "UPDATE kpmkv_pelajar_markah SET Science='-1' Where PelajarID='" & strPelajarID & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)
                End If
            Else

                If Not (A_Science1) = "-1" And Not (A_Science2) = "-1" Then
                    AM_Science1 = Math.Ceiling((A_Science1 / 100) * 70) '70%
                    AM_Science2 = Math.Ceiling((A_Science2 / 100) * 70) '70%
                    PointerSC1 = Math.Ceiling(BM_Science1)
                    PointerSC2 = Math.Ceiling((AM_Science1) + (AM_Science2))
                    PointerSC = Math.Ceiling((PointerSC1) + (PointerSC2))

                    strSQL = "UPDATE kpmkv_pelajar_markah SET Science='" & PointerSC & "' Where PelajarID='" & strPelajarID & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)
                ElseIf (A_Science1) = "-1" Or (A_Science2) = "-1" Then
                    strSQL = "UPDATE kpmkv_pelajar_markah SET Science='-1' Where PelajarID='" & strPelajarID & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)
                End If

            End If

            'SC ------------------------------------------------------------------------------------------------------------

            Dim BM_Sejarah As Integer
            Dim AM_Sejarah As Integer
            Dim BerterusanSJ As Integer
            Dim AkhiranSJ As Integer
            Dim B_Sejarah As Double
            Dim A_Sejarah As Double
            Dim PointerSJ As Integer
            'Dim GredSJ As Integer 

            strSQL = "SELECT PB FROM kpmkv_aka_weightage WHERE Tahun='" & ddlTahun.Text & "'"
            BerterusanSJ = oCommon.getFieldValue(strSQL)

            strSQL = "SELECT PA FROM kpmkv_aka_weightage WHERE Tahun='" & ddlTahun.Text & "'"
            AkhiranSJ = oCommon.getFieldValue(strSQL)

            strSQL = "Select B_Sejarah from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
            tempSkipIfNull = oCommon.getFieldValue(strSQL)

            If Not tempSkipIfNull = "" Then

                B_Sejarah = oCommon.getFieldValue(strSQL)
                'round up
                B_Sejarah = Math.Ceiling(B_Sejarah)

            Else

                Continue For

            End If



            strSQL = "Select A_Sejarah from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
            tempSkipIfNull = oCommon.getFieldValue(strSQL)

            If Not tempSkipIfNull = "" Then

                A_Sejarah = oCommon.getFieldValue(strSQL)
                'round up
                A_Sejarah = Math.Ceiling(A_Sejarah)

            Else

                Continue For

            End If



            'checkin Markah
            If Not (B_Sejarah) = "-1" And Not (A_Sejarah) = "-1" Then
                BM_Sejarah = Math.Ceiling((B_Sejarah / 100) * BerterusanSJ)
                AM_Sejarah = Math.Ceiling((A_Sejarah / 100) * AkhiranSJ)
                PointerSJ = Math.Ceiling(BM_Sejarah + AM_Sejarah)
                strSQL = "UPDATE kpmkv_pelajar_markah SET Sejarah='" & PointerSJ & "' Where PelajarID='" & strPelajarID & "'"
                strRet = oCommon.ExecuteSQL(strSQL)

            ElseIf (B_Sejarah) = "-1" Or (A_Sejarah) = "-1" Then
                strSQL = "UPDATE kpmkv_pelajar_markah SET Sejarah='-1' Where PelajarID='" & strPelajarID & "'"
                strRet = oCommon.ExecuteSQL(strSQL)
            End If

            ''updated on 08102018 to calculate pointerSJsetara

            If ddlSemester.Text = "4" Then

                Dim SJ1 As String
                Dim SJ2 As String
                Dim SJ3 As String
                Dim SJ4 As String

                Dim SJ1Int As Integer
                Dim SJ2Int As Integer
                Dim SJ3Int As Integer
                Dim SJ4Int As Integer

                Dim SJ1Double As Double
                Dim SJ2Double As Double
                Dim SJ3Double As Double
                Dim SJ4Double As Double

                Dim PointerSJSetara As Integer
                Dim PointerSJSetaraDouble As Double

                ''get MYKAD pelajar
                strSQL = "SELECT MYKAD FROM kpmkv_pelajar WHERE PelajarID = '" & strPelajarID & "'"
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
                tempSkipIfNull = oCommon.getFieldValue(strSQL)

                If Not tempSkipIfNull = "" Then

                    SJ1 = oCommon.getFieldValue(strSQL)
                    SJ1Double = (10 / 100) * Double.Parse(SJ1)
                    SJ1Int = Math.Ceiling((10 / 100) * Double.Parse(SJ1))

                Else

                    Continue For

                End If


                strSQL = "  SELECT Sejarah FROM kpmkv_pelajar_markah WHERE PelajarID = '" & strPelajarID2 & "' AND Semester = '2'"
                tempSkipIfNull = oCommon.getFieldValue(strSQL)

                If Not tempSkipIfNull = "" Then

                    SJ2 = oCommon.getFieldValue(strSQL)
                    SJ2Double = (10 / 100) * Double.Parse(SJ2)
                    SJ2Int = Math.Ceiling((10 / 100) * Double.Parse(SJ2))

                Else

                    Continue For

                End If

                strSQL = "  SELECT Sejarah FROM kpmkv_pelajar_markah WHERE PelajarID = '" & strPelajarID3 & "' AND Semester = '3'"
                tempSkipIfNull = oCommon.getFieldValue(strSQL)

                If Not tempSkipIfNull = "" Then

                    SJ3 = oCommon.getFieldValue(strSQL)
                    SJ3Double = (10 / 100) * Double.Parse(SJ3)
                    SJ3Int = Math.Ceiling((10 / 100) * Double.Parse(SJ3))

                Else

                    Continue For

                End If

                strSQL = "  SELECT Sejarah FROM kpmkv_pelajar_markah WHERE PelajarID = '" & strPelajarID4 & "' AND Semester = '4'"
                tempSkipIfNull = oCommon.getFieldValue(strSQL)

                If Not tempSkipIfNull = "" Then

                    SJ4 = oCommon.getFieldValue(strSQL)
                    SJ4Double = (10 / 100) * Double.Parse(SJ4)
                    SJ4Int = Math.Ceiling((10 / 100) * Double.Parse(SJ4))

                Else

                    Continue For

                End If

                If SJ1.ToString = "T" Or SJ1 = -1 Then

                    PointerSJSetara = -1
                    PointerSJSetaraDouble = -1

                ElseIf SJ2.ToString = "T" Or SJ2 = -1 Then

                    PointerSJSetara = -1
                    PointerSJSetaraDouble = -1

                ElseIf SJ3.ToString = "T" Or SJ3 = -1 Then

                    PointerSJSetara = -1
                    PointerSJSetaraDouble = -1

                Else

                    PointerSJSetara = Integer.Parse(SJ1Int) + Integer.Parse(SJ2Int) + Integer.Parse(SJ3Int) + Integer.Parse(SJ4Int)
                    PointerSJSetaraDouble = SJ1Double + SJ2Double + SJ3Double + SJ4Double

                End If

                strSQL = "UPDATE kpmkv_pelajar_markah SET PointerSJSetara='" & PointerSJSetaraDouble & "' Where PelajarID='" & strPelajarID & "'"
                strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='4'"
                strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & ddlKodKursus.SelectedValue & "'"
                strRet = oCommon.ExecuteSQL(strSQL)

            End If
            '-------------------------------------------------------------------------------------------------------------

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

            strSQL = "SELECT PB FROM kpmkv_aka_weightage WHERE Tahun='" & ddlTahun.Text & "'"
            BerterusanPI = oCommon.getFieldValue(strSQL)

            strSQL = "SELECT PA FROM kpmkv_aka_weightage WHERE Tahun='" & ddlTahun.Text & "'"
            AkhiranPI = oCommon.getFieldValue(strSQL)

            strSQL = "Select B_PendidikanIslam1 from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
            If Not tempSkipIfNull = "" Then

                B_PendidikanIslam1 = oCommon.getFieldValue(strSQL)
                'round up
                B_PendidikanIslam1 = Math.Ceiling(B_PendidikanIslam1)

            Else

                Continue For

            End If



            strSQL = "Select A_PendidikanIslam1 from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
            If Not tempSkipIfNull = "" Then

                A_PendidikanIslam1 = oCommon.getFieldValue(strSQL)
                'round up
                A_PendidikanIslam1 = Math.Ceiling(A_PendidikanIslam1)

            Else

                Continue For

            End If



            strSQL = "Select A_PendidikanIslam2 from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
            If Not tempSkipIfNull = "" Then

                A_PendidikanIslam2 = oCommon.getFieldValue(strSQL)
                'round up
                A_PendidikanIslam2 = Math.Ceiling(A_PendidikanIslam2)

            Else

                Continue For

            End If



            BM_PendidikanIslam1 = Math.Ceiling((B_PendidikanIslam1 / 100) * BerterusanPI)

            If Not (A_PendidikanIslam1) = "-1" And Not (A_PendidikanIslam2) = "-1" Then
                A_PendidikanIslam1 = Math.Ceiling((A_PendidikanIslam1 / 100) * 50) '50%
                A_PendidikanIslam2 = Math.Ceiling((A_PendidikanIslam2 / 100) * 20) '20%

                PointerPI1 = Math.Ceiling(BM_PendidikanIslam1)
                PointerPI2 = Math.Ceiling(A_PendidikanIslam1 + A_PendidikanIslam2)
                PointerPI = Math.Ceiling(PointerPI1 + PointerPI2)

                strSQL = "UPDATE kpmkv_pelajar_markah SET PendidikanIslam='" & PointerPI & "' Where PelajarID='" & strPelajarID & "'"
                strRet = oCommon.ExecuteSQL(strSQL)
            ElseIf (A_PendidikanIslam1) = "-1" Or (A_PendidikanIslam2) = "-1" Then
                strSQL = "UPDATE kpmkv_pelajar_markah SET PendidikanIslam='-1' Where PelajarID='" & strPelajarID & "'"
                strRet = oCommon.ExecuteSQL(strSQL)
            End If
            '-------------------------------------------------------------------------------------------------------------

            Dim BM_PendidikanMoral As Integer
            Dim AM_PendidikanMoral As Integer
            Dim BerterusanPM As Integer
            Dim AkhiranPM As Integer
            Dim B_PendidikanMoral As Integer
            Dim A_PendidikanMoral As Integer
            Dim PointerPM As Integer
            'Dim GredPM As Integer 

            strSQL = "SELECT PB FROM kpmkv_aka_weightage WHERE Tahun='" & ddlTahun.Text & "'"
            BerterusanPM = oCommon.getFieldValue(strSQL)

            strSQL = "SELECT PA FROM kpmkv_aka_weightage WHERE Tahun='" & ddlTahun.Text & "'"
            AkhiranPM = oCommon.getFieldValue(strSQL)

            strSQL = "Select B_PendidikanMoral from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
            If Not tempSkipIfNull = "" Then

                B_PendidikanMoral = oCommon.getFieldValue(strSQL)
                'round up
                B_PendidikanMoral = Math.Ceiling(B_PendidikanMoral)

            Else

                Continue For

            End If



            strSQL = "Select A_PendidikanMoral from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
            If Not tempSkipIfNull = "" Then

                A_PendidikanMoral = oCommon.getFieldValue(strSQL)
                'round up
                A_PendidikanMoral = Math.Ceiling(A_PendidikanMoral)

            Else

                Continue For

            End If



            'checkin Markah
            If Not (B_PendidikanMoral) = "-1" And Not (A_PendidikanMoral) = "-1" Then
                BM_PendidikanMoral = Math.Ceiling((B_PendidikanMoral / 100) * BerterusanPM)
                AM_PendidikanMoral = Math.Ceiling((A_PendidikanMoral / 100) * AkhiranPM)
                PointerPM = Math.Ceiling(BM_PendidikanMoral + AM_PendidikanMoral)
                strSQL = "UPDATE kpmkv_pelajar_markah SET PendidikanMoral='" & PointerPM & "' Where PelajarID='" & strPelajarID & "'"
                strRet = oCommon.ExecuteSQL(strSQL)
            ElseIf (B_PendidikanMoral) = "-1" Or (A_PendidikanMoral) = "-1" Then
                strSQL = "UPDATE kpmkv_pelajar_markah SET PendidikanMoral='-1' Where PelajarID='" & strPelajarID & "'"
                strRet = oCommon.ExecuteSQL(strSQL)
            End If
            '-------------------------------------------------------------------------------------------------------------

            Dim BM_Mathematics As Integer
            Dim AM_Mathematics As Integer
            Dim BerterusanMT As Integer
            Dim AkhiranMT As Integer
            Dim B_Mathematics As Integer
            Dim A_Mathematics As Integer
            Dim PointerMT As Integer
            'Dim GredMT As Integer 

            strSQL = "SELECT PB FROM kpmkv_aka_weightage WHERE Tahun='" & ddlTahun.Text & "'"
            BerterusanMT = oCommon.getFieldValue(strSQL)

            strSQL = "SELECT PA FROM kpmkv_aka_weightage WHERE Tahun='" & ddlTahun.Text & "'"
            AkhiranMT = oCommon.getFieldValue(strSQL)

            strSQL = "Select B_Mathematics from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
            If Not tempSkipIfNull = "" Then

                B_Mathematics = oCommon.getFieldValue(strSQL)
                'round up
                B_Mathematics = Math.Ceiling(B_Mathematics)

            Else

                Continue For

            End If



            strSQL = "Select A_Mathematics from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
            If Not tempSkipIfNull = "" Then

                A_Mathematics = oCommon.getFieldValue(strSQL)
                'round up
                A_Mathematics = Math.Ceiling(A_Mathematics)

            Else

                Continue For

            End If



            'checkin Markah
            If Not (B_Mathematics) = "-1" And Not (A_Mathematics) = "-1" Then
                BM_Mathematics = Math.Ceiling((B_Mathematics / 100) * BerterusanMT)
                AM_Mathematics = Math.Ceiling((A_Mathematics / 100) * AkhiranMT)
                PointerMT = Math.Ceiling(BM_Mathematics + AM_Mathematics)
                strSQL = "UPDATE kpmkv_pelajar_markah SET Mathematics='" & PointerMT & "' Where PelajarID='" & strPelajarID & "'"
                strRet = oCommon.ExecuteSQL(strSQL)
            ElseIf (B_Mathematics) = "-1" Or (A_Mathematics) = "-1" Then
                strSQL = "UPDATE kpmkv_pelajar_markah SET Mathematics='-1' Where PelajarID='" & strPelajarID & "'"
                strRet = oCommon.ExecuteSQL(strSQL)
            End If

        Next

    End Sub

    Private Sub Akademik_gred_Negeri()

        Dim strPelajarID As String = ""

        strRet = oCommon.ExecuteSQL(janaBerperingkat)

        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)
        Dim ds As DataSet = New DataSet
        sqlDA.Fill(ds, "AnyTable")

        For i As Integer = 0 To ds.Tables(0).Rows.Count - 1

            strPelajarID = ds.Tables(0).Rows(i).Item(0).ToString

            Dim BM As String
            Dim GredBM As String

            Dim Kertas1 As Integer = 0
            Dim Kertas2 As Integer = 0

            If ddlSemester.SelectedValue = 4 Then

                strSQL = "SELECT A_BahasaMelayu1, A_BahasaMelayu2 FROM kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
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

            End If

            strSQL = "SELECT BahasaMelayu as BM FROM kpmkv_pelajar_markah"
            strSQL += " Where PelajarID='" & strPelajarID & "'"
            BM = oCommon.getFieldValue(strSQL)

            If Kertas1 = -1 Or Kertas2 = -1 Then

                strSQL = "UPDATE kpmkv_pelajar_markah SET GredBM = 'T' Where PelajarID='" & strPelajarID & "'"
                strRet = oCommon.ExecuteSQL(strSQL)

            Else

                If String.IsNullOrEmpty(BM) Then
                    strSQL = "UPDATE kpmkv_pelajar_markah SET GredBM='' Where PelajarID='" & strPelajarID & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)
                Else
                    strSQL = "SELECT TOP ( 1 ) Gred FROM  kpmkv_gred WHERE '" & Integer.Parse(BM) & "' BETWEEN MarkahFrom AND MarkahTo AND Jenis='AKADEMIK'"
                    GredBM = oCommon.getFieldValue(strSQL)

                    strSQL = "UPDATE kpmkv_pelajar_markah SET GredBM='" & GredBM & "' Where PelajarID='" & strPelajarID & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)
                End If

            End If

            '-----------------------------------------------------------------
            Dim BI As String
            Dim GredBI As String

            strSQL = "SELECT BahasaInggeris FROM kpmkv_pelajar_markah"
            strSQL += " Where PelajarID='" & strPelajarID & "'"
            BI = oCommon.getFieldValue(strSQL)

            'If BI = "0" Then
            If String.IsNullOrEmpty(BI) Then
            Else
                strSQL = "SELECT TOP ( 1 ) Gred FROM  kpmkv_gred WHERE '" & Integer.Parse(BI) & "' BETWEEN MarkahFrom AND MarkahTo AND Jenis='AKADEMIK'"
                GredBI = oCommon.getFieldValue(strSQL)

                strSQL = "UPDATE kpmkv_pelajar_markah SET GredBI='" & GredBI & "' Where PelajarID='" & strPelajarID & "'"
                strRet = oCommon.ExecuteSQL(strSQL)

            End If
            '------------------------------------------------------------------------------------------------------------------------
            Dim SC As String
            Dim GredSC As String

            strSQL = "SELECT Science FROM kpmkv_pelajar_markah"
            strSQL += " Where PelajarID='" & strPelajarID & "'"
            SC = oCommon.getFieldValue(strSQL)

            'If SC = 0 Then
            If String.IsNullOrEmpty(SC) Then
            Else
                strSQL = "SELECT TOP ( 1 ) Gred FROM  kpmkv_gred WHERE '" & SC & "' BETWEEN MarkahFrom AND MarkahTo AND Jenis='AKADEMIK'"
                GredSC = oCommon.getFieldValue(strSQL)

                strSQL = "UPDATE kpmkv_pelajar_markah SET GredSC='" & GredSC & "' Where PelajarID='" & strPelajarID & "'"
                strRet = oCommon.ExecuteSQL(strSQL)

            End If
            '------------------------------------------------------------------------------------------------------------

            Dim SJ As String
            Dim GredSJ As String

            strSQL = "SELECT Sejarah FROM kpmkv_pelajar_markah"
            strSQL += " Where PelajarID='" & strPelajarID & "'"
            SJ = oCommon.getFieldValue(strSQL)

            'If SJ = "0" Then
            If String.IsNullOrEmpty(SJ) Then
            Else
                strSQL = "SELECT TOP ( 1 ) Gred FROM  kpmkv_gred WHERE '" & Integer.Parse(SJ) & "' BETWEEN MarkahFrom AND MarkahTo AND Jenis='AKADEMIK'"
                GredSJ = oCommon.getFieldValue(strSQL)

                strSQL = "UPDATE kpmkv_pelajar_markah SET GredSJ='" & GredSJ & "' Where PelajarID='" & strPelajarID & "'"
                strRet = oCommon.ExecuteSQL(strSQL)

            End If
            '-------------------------------------------------------------------------------------------------------------

            Dim PI As String
            Dim GredPI As String

            strSQL = "SELECT PendidikanIslam FROM kpmkv_pelajar_markah"
            strSQL += " Where PelajarID='" & strPelajarID & "'"
            PI = oCommon.getFieldValue(strSQL)

            If PI = "0" Then
            ElseIf String.IsNullOrEmpty(PI) Then
            Else
                strSQL = "SELECT TOP ( 1 ) Gred FROM  kpmkv_gred WHERE '" & Integer.Parse(PI) & "' BETWEEN MarkahFrom AND MarkahTo AND Jenis='AKADEMIK'"
                GredPI = oCommon.getFieldValue(strSQL)

                strSQL = "UPDATE kpmkv_pelajar_markah SET GredPI='" & GredPI & "' Where PelajarID='" & strPelajarID & "'"
                strRet = oCommon.ExecuteSQL(strSQL)

            End If
            '-------------------------------------------------------------------------------------------------------------

            Dim PM As String
            Dim GredPM As String

            strSQL = "SELECT PendidikanMoral FROM kpmkv_pelajar_markah"
            strSQL += " Where PelajarID='" & strPelajarID & "'"
            PM = oCommon.getFieldValue(strSQL)

            If PM = "0" Then
            ElseIf String.IsNullOrEmpty(PM) Then
            Else
                strSQL = "SELECT TOP ( 1 ) Gred FROM  kpmkv_gred WHERE '" & Integer.Parse(PM) & "' BETWEEN MarkahFrom AND MarkahTo AND Jenis='AKADEMIK'"
                GredPM = oCommon.getFieldValue(strSQL)

                strSQL = "UPDATE kpmkv_pelajar_markah SET GredPM='" & GredPM & "' Where PelajarID='" & strPelajarID & "'"
                strRet = oCommon.ExecuteSQL(strSQL)

            End If
            '-------------------------------------------------------------------------------------------------------------

            Dim MT As String
            Dim GredMT As String

            strSQL = "SELECT Mathematics FROM kpmkv_pelajar_markah"
            strSQL += " Where PelajarID='" & strPelajarID & "'"
            MT = oCommon.getFieldValue(strSQL)

            'If MT = "0" Then
            If String.IsNullOrEmpty(MT) Then
            Else
                strSQL = "SELECT TOP ( 1 ) Gred FROM  kpmkv_gred WHERE '" & Integer.Parse(MT) & "' BETWEEN MarkahFrom AND MarkahTo AND Jenis='AKADEMIK'"
                GredMT = oCommon.getFieldValue(strSQL)

                strSQL = "UPDATE kpmkv_pelajar_markah SET GredMT='" & GredMT & "' Where PelajarID='" & strPelajarID & "'"
                strRet = oCommon.ExecuteSQL(strSQL)
            End If

        Next

    End Sub

    Private Sub ddlTahun_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTahun.SelectedIndexChanged

        kpmkv_kodkursus_list()
        ddlKodKursus.SelectedValue = "0"

    End Sub

    Private Sub ddlSemester_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSemester.SelectedIndexChanged

        kpmkv_kodkursus_list()
        ddlKodKursus.SelectedValue = "0"

    End Sub

    Private Sub ddlKolej_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlKolej.SelectedIndexChanged

        kpmkv_kodkursus_list()
        ddlKodKursus.SelectedValue = "0"

    End Sub

    Private Function janaBerperingkat()

        strSQL = "  SELECT kpmkv_pelajar.PelajarID FROM kpmkv_pelajar
                    LEFT JOIN kpmkv_kolej ON kpmkv_kolej.RecordID = kpmkv_pelajar.KolejRecordID
                    LEFT JOIN kpmkv_negeri ON kpmkv_negeri.Negeri = kpmkv_kolej.Negeri
                    WHERE kpmkv_negeri.Negeri IS NOT NULL AND kpmkv_pelajar.StatusID = '2' AND kpmkv_pelajar.PelajarID IS NOT NULL"

        If Not ddlNegeri.SelectedValue = "" Then

            strSQL += " AND kpmkv_negeri.Negeri = '" & ddlNegeri.SelectedValue & "'"

        End If

        If Not ddlKolej.SelectedValue = "" Then

            strSQL += " AND kpmkv_kolej.RecordID = '" & ddlKolej.SelectedValue & "'"

        End If

        If Not ddlTahun.Text = "" Then

            strSQL += " AND kpmkv_pelajar.Tahun = '" & ddlTahun.SelectedValue & "'"

        End If

        'If Not ddlSemester.Text = "" Then

        '    strSQL += " AND kpmkv_pelajar.Semester = '" & ddlSemester.SelectedValue & "'"

        'End If

        'If Not chkSesi.Text = "" Then

        '    strSQL += " AND kpmkv_pelajar.Sesi ='" & chkSesi.Text & "'"

        'End If


        strSQL += " ORDER BY kpmkv_negeri.Negeri, kpmkv_pelajar.AngkaGiliran"

        Return strSQL

    End Function

    Private Sub btnmarkahBM_Click(sender As Object, e As EventArgs) Handles btnmarkahBM.Click

        Dim strPelajarID As String = ""

        Try

            Dim BerterusanBM As Integer
            Dim AkhiranBM As Integer

            strSQL = "SELECT PB FROM kpmkv_aka_weightage WHERE Tahun='" & ddlTahun.Text & "'"
            BerterusanBM = oCommon.getFieldValue(strSQL)

            strSQL = "SELECT PA FROM kpmkv_aka_weightage WHERE Tahun='" & ddlTahun.Text & "'"
            AkhiranBM = oCommon.getFieldValue(strSQL)

            Dim AM_BahasaMelayu As Integer
            Dim BM_BahasaMelayu As Integer
            Dim B_BahasaMelayu As Double
            Dim A_BahasaMelayu As Double
            Dim PointerBM As Integer

            'Dim AM_BahasaMelayu2 As Integer
            'Dim BM_BahasaMelayu2 As Integer
            'Dim B_BahasaMelayu2 As Double
            'Dim A_BahasaMelayu2 As Double
            'Dim PointerBM2 As Integer

            'Dim AM_BahasaMelayu3 As Integer
            'Dim BM_BahasaMelayu3 As Integer
            'Dim B_BahasaMelayu3 As Double
            'Dim A_BahasaMelayu3 As Double
            'Dim PointerBM3 As Integer

            Dim PB4 As Integer
            Dim PABmSetara As Integer
            Dim PAPB4 As Integer
            Dim B_BahasaMelayuSem1 As Integer
            Dim B_BahasaMelayuSem2 As Integer
            Dim B_BahasaMelayuSem3 As Integer
            Dim B_BahasaMelayuSem4 As Integer
            Dim A_BahasaMelayuSem4 As Integer
            Dim PointerBMSetara As Integer

            strRet = oCommon.ExecuteSQL(janaBerperingkat)

            Dim sqlDA As New SqlDataAdapter(strSQL, objConn)
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1

                strPelajarID = ds.Tables(0).Rows(i).Item(0).ToString

                'If ddlNegeri.SelectedValue = "" Then

                'strSQL = "SELECT BahasaMelayu FROM kpmkv_pelajar_markah WHERE PelajarID = '" & strPelajarID & "'"

                '    If Not oCommon.getFieldValue(strSQL).Length = 0 Then

                If ddlSemester.Text = "4" Then

                    B_BahasaMelayuSem1 = -2
                    B_BahasaMelayuSem2 = -2
                    B_BahasaMelayuSem3 = -2
                    B_BahasaMelayuSem4 = -2
                    A_BahasaMelayuSem4 = -2

                    strSQL = " SELECT Mykad FROM kpmkv_pelajar"
                    strSQL += " WHERE PelajarID='" & strPelajarID & "'"
                    Dim strMYKAD1 As String = oCommon.getFieldValue(strSQL)

                    strSQL = " SELECT PelajarID FROM kpmkv_pelajar"
                    strSQL += " WHERE StatusID='2' AND IsDeleted='N' AND Semester='1'"
                    strSQL += " AND Mykad='" & strMYKAD1 & "'"
                    Dim strPelajarID1 As String = oCommon.getFieldValue(strSQL)

                    strSQL = "Select BahasaMelayu from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID1 & "'"
                    If Not oCommon.getFieldValue(strSQL).Length = 0 Then
                        B_BahasaMelayuSem1 = oCommon.getFieldValue(strSQL)
                        B_BahasaMelayuSem1 = Math.Ceiling(B_BahasaMelayuSem1)
                    End If

                    strSQL = " SELECT PelajarID FROM kpmkv_pelajar"
                    strSQL += " WHERE StatusID='2' AND IsDeleted='N' AND Semester='2'"
                    strSQL += " AND Mykad='" & strMYKAD1 & "'"
                    Dim strPelajarID2 As String = oCommon.getFieldValue(strSQL)

                    strSQL = "Select BahasaMelayu from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID2 & "'"
                    If Not oCommon.getFieldValue(strSQL).Length = 0 Then
                        B_BahasaMelayuSem2 = oCommon.getFieldValue(strSQL)
                        B_BahasaMelayuSem2 = Math.Ceiling(B_BahasaMelayuSem2)
                    End If

                    strSQL = " SELECT PelajarID FROM kpmkv_pelajar"
                    strSQL += " WHERE StatusID='2' AND IsDeleted='N' AND Semester='3'"
                    strSQL += " AND Mykad='" & strMYKAD1 & "'"
                    Dim strPelajarID3 As String = oCommon.getFieldValue(strSQL)

                    strSQL = "Select BahasaMelayu from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID3 & "'"
                    If Not oCommon.getFieldValue(strSQL).Length = 0 Then
                        B_BahasaMelayuSem3 = oCommon.getFieldValue(strSQL)
                        B_BahasaMelayuSem3 = Math.Ceiling(B_BahasaMelayuSem3)
                    End If

                    strSQL = "Select B_BahasaMelayu from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
                    If Not oCommon.getFieldValue(strSQL).Length = 0 Then
                        B_BahasaMelayuSem4 = oCommon.getFieldValue(strSQL)
                    End If

                    strSQL = "Select A_BahasaMelayu3 from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
                    If Not oCommon.getFieldValue(strSQL).Length = 0 Then
                        A_BahasaMelayuSem4 = oCommon.getFieldValue(strSQL)
                    End If

                    Dim Kertas1 As Integer = 0
                    Dim Kertas2 As Integer = 0

                    strSQL = "SELECT A_BahasaMelayu1, A_BahasaMelayu2 FROM kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
                    strRet = oCommon.getFieldValueEx(strSQL)

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

                    If Kertas1 = -1 Or Kertas2 = -1 Then

                        strSQL = "UPDATE kpmkv_pelajar_markah SET BahasaMelayu='-1' Where PelajarID='" & strPelajarID & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)

                        strSQL = "UPDATE kpmkv_pelajar_markah SET PointerBMSetara='-1' Where PelajarID='" & strPelajarID & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)

                    ElseIf Not ((B_BahasaMelayuSem4) = "-2" And Not (A_BahasaMelayuSem4) = "-2") Then

                        If Not ((B_BahasaMelayuSem4) = "-1" Or (A_BahasaMelayuSem4) = "-1") Then

                            PB4 = Math.Ceiling((B_BahasaMelayuSem4 / 100) * BerterusanBM)

                            PABmSetara = Math.Ceiling((A_BahasaMelayuSem4 / 100) * 40)
                            PAPB4 = Math.Ceiling(((Kertas1 + Kertas2 + PABmSetara) / 280) * AkhiranBM)

                            Dim PointSem4 As Integer = Math.Ceiling(PB4 + PAPB4)
                            strSQL = "UPDATE kpmkv_pelajar_markah SET BahasaMelayu='" & PointSem4 & "' Where PelajarID='" & strPelajarID & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)

                            If (B_BahasaMelayuSem1 = "-1" Or B_BahasaMelayuSem2 = "-1" Or B_BahasaMelayuSem3 = "-1") Then
                                PointerBMSetara = "-1"
                            Else
                                PointerBMSetara = Math.Ceiling((((B_BahasaMelayuSem1 / 100) * 10) + ((B_BahasaMelayuSem2 / 100) * 10) + ((B_BahasaMelayuSem3 / 100) * 10) + ((PointSem4 / 100) * 70)))
                            End If

                            strSQL = "UPDATE kpmkv_pelajar_markah SET PointerBMSetara='" & PointerBMSetara & "' Where PelajarID='" & strPelajarID & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)

                        ElseIf ((B_BahasaMelayuSem4) = "-1" Or (A_BahasaMelayuSem4) = "-1") Then

                            strSQL = "UPDATE kpmkv_pelajar_markah SET BahasaMelayu='-1' Where PelajarID = '" & strPelajarID & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)

                            strSQL = "UPDATE kpmkv_pelajar_markah SET PointerBMSetara='-1' Where PelajarID = '" & strPelajarID & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)

                        End If

                    End If

                Else

                    B_BahasaMelayu = -2
                    A_BahasaMelayu = -2

                    strSQL = "Select B_BahasaMelayu from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
                    If Not oCommon.getFieldValue(strSQL).Length = 0 Then
                        B_BahasaMelayu = oCommon.getFieldValue(strSQL)
                        B_BahasaMelayu = Math.Ceiling(B_BahasaMelayu)
                    End If

                    strSQL = "Select A_BahasaMelayu from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
                    If Not oCommon.getFieldValue(strSQL).Length = 0 Then
                        A_BahasaMelayu = oCommon.getFieldValue(strSQL)
                        A_BahasaMelayu = Math.Ceiling(A_BahasaMelayu)
                    End If

                    If Not (B_BahasaMelayu) = "-2" And Not (A_BahasaMelayu) = "-2" Then

                        'checkin Markah
                        If Not (B_BahasaMelayu) = "-1" And Not (A_BahasaMelayu) = "-1" Then
                            BM_BahasaMelayu = Math.Ceiling((B_BahasaMelayu / 100) * BerterusanBM)
                            AM_BahasaMelayu = Math.Ceiling((A_BahasaMelayu / 100) * AkhiranBM)
                            PointerBM = Math.Ceiling(BM_BahasaMelayu + AM_BahasaMelayu)
                            strSQL = "UPDATE kpmkv_pelajar_markah SET BahasaMelayu='" & PointerBM & "' "
                            strSQL += " PelajarID ='" & strPelajarID & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)

                        ElseIf (B_BahasaMelayu) = "-1" Or (A_BahasaMelayu) = "-1" Then
                            strSQL = "UPDATE kpmkv_pelajar_markah SET BahasaMelayu='-1' Where PelajarID='" & strPelajarID & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)
                        End If

                    End If

                End If

            Next

            lblMarkahBM.Text = "JANA BERPERINGKAT BM BERJAYA."

        Catch ex As Exception

            lblMarkahBM.Text = "System Error : " & ex.Message & " PelajarID : " & strPelajarID

        End Try

    End Sub

    Private Sub btnmarkahBI_Click(sender As Object, e As EventArgs) Handles btnmarkahBI.Click

        Dim strPelajarID As String = ""

        Try

            Dim BM_BahasaInggeris As Integer
            Dim AM_BahasaInggeris As Integer
            Dim BerterusanBI As Integer
            Dim AkhiranBI As Integer
            Dim B_BahasaInggeris As Double
            Dim A_BahasaInggeris As Double
            Dim PointerBI As Integer

            strSQL = janaBerperingkat()

            Dim sqlDA As New SqlDataAdapter(strSQL, objConn)
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1

                B_BahasaInggeris = -2
                A_BahasaInggeris = -2

                strPelajarID = ds.Tables(0).Rows(i).Item(0).ToString

                strSQL = "SELECT BahasaInggeris FROM kpmkv_pelajar_markah WHERE PelajarID = '" & strPelajarID & "'"

                If oCommon.getFieldValue(strSQL).Length = 0 Then

                    strSQL = "SELECT PB FROM kpmkv_aka_weightage WHERE Tahun='" & ddlTahun.Text & "'"
                    BerterusanBI = oCommon.getFieldValue(strSQL)

                    strSQL = "SELECT PA FROM kpmkv_aka_weightage WHERE Tahun='" & ddlTahun.Text & "'"
                    AkhiranBI = oCommon.getFieldValue(strSQL)

                    strSQL = "SELECT B_BahasaInggeris FROM kpmkv_pelajar_markah WHERE PelajarID='" & strPelajarID & "'"
                    If Not oCommon.getFieldValue(strSQL).Length = 0 Then
                        B_BahasaInggeris = oCommon.getFieldValue(strSQL)
                        B_BahasaInggeris = Math.Ceiling(B_BahasaInggeris)
                    End If

                    strSQL = "SELECT A_BahasaInggeris FROM kpmkv_pelajar_markah WHERE PelajarID='" & strPelajarID & "'"
                    If Not oCommon.getFieldValue(strSQL).Length = 0 Then
                        A_BahasaInggeris = oCommon.getFieldValue(strSQL)
                        A_BahasaInggeris = Math.Ceiling(A_BahasaInggeris)
                    End If

                    If Not B_BahasaInggeris = -2 And Not A_BahasaInggeris = -2 Then

                        'checkin Markah
                        If Not (B_BahasaInggeris) = "-1" And Not (A_BahasaInggeris) = "-1" Then
                            BM_BahasaInggeris = Math.Ceiling((B_BahasaInggeris / 100) * BerterusanBI)
                            AM_BahasaInggeris = Math.Ceiling((A_BahasaInggeris / 100) * AkhiranBI)
                            PointerBI = Math.Ceiling(BM_BahasaInggeris + AM_BahasaInggeris)
                            strSQL = "UPDATE kpmkv_pelajar_markah SET BahasaInggeris='" & PointerBI & "' WHERE PelajarID='" & strPelajarID & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)

                        ElseIf (B_BahasaInggeris) = "-1" Or (A_BahasaInggeris) = "-1" Then
                            strSQL = "UPDATE kpmkv_pelajar_markah SET BahasaInggeris='-1' Where PelajarID='" & strPelajarID & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)
                        End If

                    End If

                End If

            Next

            lblMarkahBI.Text = "JANA BERPERINGKAT BI BERJAYA."

        Catch ex As Exception

            lblMarkahBI.Text = "System Error : " & ex.Message & " PelajarID : " & strPelajarID

        End Try

    End Sub

    Private Sub btnmarkahMT_Click(sender As Object, e As EventArgs) Handles btnmarkahMT.Click

        Dim strPelajarID As String = ""

        Try

            Dim BM_Mathematics As Integer
            Dim AM_Mathematics As Integer
            Dim BerterusanMT As Integer
            Dim AkhiranMT As Integer
            Dim B_Mathematics As Integer
            Dim A_Mathematics As Integer
            Dim PointerMT As Integer
            'Dim GredMT As Integer 

            strRet = oCommon.ExecuteSQL(janaBerperingkat)

            Dim sqlDA As New SqlDataAdapter(strSQL, objConn)
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1

                B_Mathematics = -2
                A_Mathematics = -2

                strPelajarID = ds.Tables(0).Rows(i).Item(0).ToString

                strSQL = "SELECT Mathematics FROM kpmkv_pelajar_markah WHERE PelajarID = '" & strPelajarID & "'"

                If oCommon.getFieldValue(strSQL).Length = 0 Then

                    strSQL = "SELECT PB FROM kpmkv_aka_weightage WHERE Tahun='" & ddlTahun.Text & "'"
                    BerterusanMT = oCommon.getFieldValue(strSQL)

                    strSQL = "SELECT PA FROM kpmkv_aka_weightage WHERE Tahun='" & ddlTahun.Text & "'"
                    AkhiranMT = oCommon.getFieldValue(strSQL)

                    strSQL = "Select B_Mathematics from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
                    If Not oCommon.getFieldValue(strSQL).Length = 0 Then

                        B_Mathematics = oCommon.getFieldValue(strSQL)
                        B_Mathematics = Math.Ceiling(B_Mathematics)
                    End If

                    strSQL = "Select A_Mathematics from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
                    If Not oCommon.getFieldValue(strSQL).Length = 0 Then
                        A_Mathematics = oCommon.getFieldValue(strSQL)
                        A_Mathematics = Math.Ceiling(A_Mathematics)
                    End If

                    If Not B_Mathematics = -2 And Not A_Mathematics = -2 Then

                        If Not (B_Mathematics) = "-1" And Not (A_Mathematics) = "-1" Then
                            BM_Mathematics = Math.Ceiling((B_Mathematics / 100) * BerterusanMT)
                            AM_Mathematics = Math.Ceiling((A_Mathematics / 100) * AkhiranMT)
                            PointerMT = Math.Ceiling(BM_Mathematics + AM_Mathematics)
                            strSQL = "UPDATE kpmkv_pelajar_markah SET Mathematics='" & PointerMT & "' Where PelajarID='" & strPelajarID & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)
                        ElseIf (B_Mathematics) = "-1" Or (A_Mathematics) = "-1" Then
                            strSQL = "UPDATE kpmkv_pelajar_markah SET Mathematics='-1' Where PelajarID='" & strPelajarID & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)
                        End If

                    End If

                End If

            Next

            lblMarkahMT.Text = "JANA BERPERINGKAT MT BERJAYA."

        Catch ex As Exception

            lblMarkahMT.Text = "System Error : " & ex.Message & " PelajarID : " & strPelajarID

        End Try

    End Sub

    Private Sub btnmarkahSC_Click(sender As Object, e As EventArgs) Handles btnmarkahSC.Click

        Dim strPelajarID As String = ""

        Try

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

            strRet = oCommon.ExecuteSQL(janaBerperingkat)

            Dim sqlDA As New SqlDataAdapter(strSQL, objConn)
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1

                strPelajarID = ds.Tables(0).Rows(i).Item(0).ToString

                B_Science1 = -2
                A_Science1 = -2
                A_Science2 = -2

                strSQL = "SELECT Science FROM kpmkv_pelajar_markah WHERE PelajarID = '" & strPelajarID & "'"

                If oCommon.getFieldValue(strSQL).Length = 0 Then

                    strSQL = "SELECT PB FROM kpmkv_aka_weightage WHERE Tahun='" & ddlTahun.Text & "'"
                    BerterusanSc = oCommon.getFieldValue(strSQL)

                    strSQL = "SELECT PA FROM kpmkv_aka_weightage WHERE Tahun='" & ddlTahun.Text & "'"
                    AkhiranSc = oCommon.getFieldValue(strSQL)

                    strSQL = "Select B_Science1 from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
                    If Not oCommon.getFieldValue(strSQL).Length = 0 Then
                        B_Science1 = oCommon.getFieldValue(strSQL)
                        B_Science1 = Math.Ceiling(B_Science1)
                    End If

                    strSQL = "Select A_Science1 from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
                    If Not oCommon.getFieldValue(strSQL).Length = 0 Then
                        A_Science1 = oCommon.getFieldValue(strSQL)
                        A_Science1 = Math.Ceiling(A_Science1)
                    End If

                    strSQL = "Select A_Science2 from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
                    If Not oCommon.getFieldValue(strSQL).Length = 0 Then
                        A_Science2 = oCommon.getFieldValue(strSQL)
                        A_Science2 = Math.Ceiling(A_Science2)
                    End If

                    If Not B_Science1 = -2 And Not A_Science1 = -2 And Not A_Science2 = -2 Then

                        BM_Science1 = Math.Ceiling((B_Science1 / 100) * BerterusanSc)

                        If ddlSemester.Text = "1" Or ddlSemester.Text = "2" Then

                            If Not (A_Science1) = "-1" And Not (A_Science2) = "-1" Then
                                AM_Science1 = Math.Ceiling((A_Science1 / 100) * 50) '50%

                                AM_Science2 = Math.Ceiling((A_Science2 / 100) * 20) '20% 

                                PointerSC1 = Math.Ceiling(BM_Science1)
                                PointerSC2 = Math.Ceiling((AM_Science1) + (AM_Science2))
                                PointerSC = Math.Ceiling((PointerSC1) + (PointerSC2))

                                strSQL = "UPDATE kpmkv_pelajar_markah SET Science='" & PointerSC & "' Where PelajarID='" & strPelajarID & "'"
                                strRet = oCommon.ExecuteSQL(strSQL)
                            ElseIf (A_Science1) = "-1" Or (A_Science2) = "-1" Then
                                strSQL = "UPDATE kpmkv_pelajar_markah SET Science='-1' Where PelajarID='" & strPelajarID & "'"
                                strRet = oCommon.ExecuteSQL(strSQL)
                            End If
                        Else

                            If Not (A_Science1) = "-1" And Not (A_Science2) = "-1" Then
                                AM_Science1 = Math.Ceiling((A_Science1 / 100) * 70) '70%
                                AM_Science2 = Math.Ceiling((A_Science2 / 100) * 70) '70%
                                PointerSC1 = Math.Ceiling(BM_Science1)
                                PointerSC2 = Math.Ceiling((AM_Science1) + (AM_Science2))
                                PointerSC = Math.Ceiling((PointerSC1) + (PointerSC2))

                                strSQL = "UPDATE kpmkv_pelajar_markah SET Science='" & PointerSC & "' Where PelajarID='" & strPelajarID & "'"
                            ElseIf (A_Science1) = "-1" Or (A_Science2) = "-1" Then
                                strSQL = "UPDATE kpmkv_pelajar_markah SET Science='-1' Where PelajarID='" & strPelajarID & "'"
                                strRet = oCommon.ExecuteSQL(strSQL)
                            End If

                        End If

                    End If

                End If

            Next

            lblMarkahSC.Text = "JANA BERPERINGKAT SC BERJAYA."

        Catch ex As Exception

            lblMarkahSC.Text = "System Error : " & ex.Message & " PelajarID : " & strPelajarID

        End Try

    End Sub

    Private Sub btnmarkahSJ_Click(sender As Object, e As EventArgs) Handles btnmarkahSJ.Click

        Dim strPelajarID As String = ""

        Try

            Dim BM_Sejarah As Integer
            Dim AM_Sejarah As Integer
            Dim BerterusanSJ As Integer
            Dim AkhiranSJ As Integer
            Dim B_Sejarah As Double
            Dim A_Sejarah As Double
            Dim PointerSJ As Integer
            'Dim GredSJ As Integer 

            strRet = oCommon.ExecuteSQL(janaBerperingkat)

            Dim sqlDA As New SqlDataAdapter(strSQL, objConn)
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1

                B_Sejarah = -2
                A_Sejarah = -2

                strPelajarID = ds.Tables(0).Rows(i).Item(0).ToString

                strSQL = "SELECT Sejarah FROM kpmkv_pelajar_markah WHERE PelajarID = '" & strPelajarID & "'"

                If oCommon.getFieldValue(strSQL).Length = 0 Then

                    strSQL = "SELECT PB FROM kpmkv_aka_weightage WHERE Tahun='" & ddlTahun.Text & "'"
                    BerterusanSJ = oCommon.getFieldValue(strSQL)

                    strSQL = "SELECT PA FROM kpmkv_aka_weightage WHERE Tahun='" & ddlTahun.Text & "'"
                    AkhiranSJ = oCommon.getFieldValue(strSQL)

                    strSQL = "Select B_Sejarah from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
                    If Not oCommon.getFieldValue(strSQL).Length = 0 Then
                        B_Sejarah = oCommon.getFieldValue(strSQL)
                        B_Sejarah = Math.Ceiling(B_Sejarah)
                    End If

                    strSQL = "Select A_Sejarah from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
                    If Not oCommon.getFieldValue(strSQL).Length = 0 Then
                        A_Sejarah = oCommon.getFieldValue(strSQL)
                        A_Sejarah = Math.Ceiling(A_Sejarah)
                    End If

                    If Not (B_Sejarah) = "-2" And Not (A_Sejarah) = "-2" Then

                        If Not (B_Sejarah) = "-1" And Not (A_Sejarah) = "-1" Then
                            BM_Sejarah = Math.Ceiling((B_Sejarah / 100) * BerterusanSJ)
                            AM_Sejarah = Math.Ceiling((A_Sejarah / 100) * AkhiranSJ)
                            PointerSJ = Math.Ceiling(BM_Sejarah + AM_Sejarah)
                            strSQL = "UPDATE kpmkv_pelajar_markah SET Sejarah='" & PointerSJ & "' Where PelajarID='" & strPelajarID & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)

                        ElseIf (B_Sejarah) = "-1" Or (A_Sejarah) = "-1" Then
                            strSQL = "UPDATE kpmkv_pelajar_markah SET Sejarah='-1' Where PelajarID='" & strPelajarID & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)
                        End If

                    End If

                    If ddlSemester.Text = "4" Then

                        Dim SJ1 As Integer = -2
                        Dim SJ2 As Integer = -2
                        Dim SJ3 As Integer = -2
                        Dim SJ4 As Integer = -2

                        Dim SJ1Int As Integer
                        Dim SJ2Int As Integer
                        Dim SJ3Int As Integer
                        Dim SJ4Int As Integer

                        Dim SJ1Double As Double
                        Dim SJ2Double As Double
                        Dim SJ3Double As Double
                        Dim SJ4Double As Double

                        Dim PointerSJSetara As Integer
                        Dim PointerSJSetaraDouble As Double

                        ''get MYKAD pelajar
                        strSQL = "SELECT MYKAD FROM kpmkv_pelajar WHERE PelajarID = '" & strPelajarID & "'"
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
                        If Not oCommon.getFieldValue(strSQL).Length = 0 Then
                            SJ1 = oCommon.getFieldValue(strSQL)
                            SJ1Double = (10 / 100) * Double.Parse(SJ1)
                            SJ1Int = Math.Ceiling((10 / 100) * Double.Parse(SJ1))
                        End If

                        strSQL = "  SELECT Sejarah FROM kpmkv_pelajar_markah WHERE PelajarID = '" & strPelajarID2 & "' AND Semester = '2'"
                        If Not oCommon.getFieldValue(strSQL).Length = 0 Then
                            SJ2 = oCommon.getFieldValue(strSQL)
                            SJ2Double = (10 / 100) * Double.Parse(SJ2)
                            SJ2Int = Math.Ceiling((10 / 100) * Double.Parse(SJ2))
                        End If

                        strSQL = "  SELECT Sejarah FROM kpmkv_pelajar_markah WHERE PelajarID = '" & strPelajarID3 & "' AND Semester = '3'"
                        If Not oCommon.getFieldValue(strSQL).Length = 0 Then
                            SJ3 = oCommon.getFieldValue(strSQL)
                            SJ3Double = (10 / 100) * Double.Parse(SJ3)
                            SJ3Int = Math.Ceiling((10 / 100) * Double.Parse(SJ3))
                        End If

                        strSQL = "  SELECT Sejarah FROM kpmkv_pelajar_markah WHERE PelajarID = '" & strPelajarID4 & "' AND Semester = '4'"
                        If Not oCommon.getFieldValue(strSQL).Length = 0 Then
                            SJ4 = oCommon.getFieldValue(strSQL)
                            SJ4Double = (70 / 100) * Double.Parse(SJ4)
                            SJ4Int = Math.Ceiling((70 / 100) * Double.Parse(SJ4))
                        End If

                        If Not SJ1 = -2 And Not SJ2 = -2 And Not SJ3 = -2 And Not SJ4 = -2 Then

                            If SJ1.ToString = "T" Or SJ1 = -1 Then

                                PointerSJSetara = -1
                                PointerSJSetaraDouble = -1

                            ElseIf SJ2.ToString = "T" Or SJ2 = -1 Then

                                PointerSJSetara = -1
                                PointerSJSetaraDouble = -1

                            ElseIf SJ3.ToString = "T" Or SJ3 = -1 Then

                                PointerSJSetara = -1
                                PointerSJSetaraDouble = -1

                            Else

                                PointerSJSetara = Integer.Parse(SJ1Int) + Integer.Parse(SJ2Int) + Integer.Parse(SJ3Int) + Integer.Parse(SJ4Int)
                                PointerSJSetaraDouble = SJ1Double + SJ2Double + SJ3Double + SJ4Double

                            End If

                            strSQL = "UPDATE kpmkv_pelajar_markah SET PointerSJSetara='" & PointerSJSetaraDouble & "' Where PelajarID='" & strPelajarID4 & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)

                        End If

                    End If

                End If

            Next

            lblMarkahSJ.Text = "JANA BERPERINGKAT SJ BERJAYA."

        Catch ex As Exception

            lblMarkahSJ.Text = "System Error : " & ex.Message & " PelajarID : " & strPelajarID

        End Try

    End Sub

    Private Sub btnmarkahPI_Click(sender As Object, e As EventArgs) Handles btnmarkahPI.Click

        Dim strPelajarID As String = ""

        Try

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

            strRet = oCommon.ExecuteSQL(janaBerperingkat)

            Dim sqlDA As New SqlDataAdapter(strSQL, objConn)
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1

                B_PendidikanIslam1 = -2
                A_PendidikanIslam1 = -2
                A_PendidikanIslam2 = -2

                strPelajarID = ds.Tables(0).Rows(i).Item(0).ToString

                strSQL = "SELECT PendidikanIslam FROM kpmkv_pelajar_markah WHERE PelajarID = '" & strPelajarID & "'"

                If oCommon.getFieldValue(strSQL).Length = 0 Then

                    strSQL = "SELECT PB FROM kpmkv_aka_weightage WHERE Tahun='" & ddlTahun.Text & "'"
                    BerterusanPI = oCommon.getFieldValue(strSQL)

                    strSQL = "SELECT PA FROM kpmkv_aka_weightage WHERE Tahun='" & ddlTahun.Text & "'"
                    AkhiranPI = oCommon.getFieldValue(strSQL)

                    strSQL = "Select B_PendidikanIslam1 from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
                    If Not oCommon.getFieldValue(strSQL).Length = 0 Then
                        B_PendidikanIslam1 = oCommon.getFieldValue(strSQL)
                        B_PendidikanIslam1 = Math.Ceiling(B_PendidikanIslam1)
                    End If

                    strSQL = "Select A_PendidikanIslam1 from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
                    If Not oCommon.getFieldValue(strSQL).Length = 0 Then
                        A_PendidikanIslam1 = oCommon.getFieldValue(strSQL)
                        A_PendidikanIslam1 = Math.Ceiling(A_PendidikanIslam1)
                    End If

                    strSQL = "Select A_PendidikanIslam2 from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
                    If Not oCommon.getFieldValue(strSQL).Length = 0 Then
                        A_PendidikanIslam2 = oCommon.getFieldValue(strSQL)
                        A_PendidikanIslam2 = Math.Ceiling(A_PendidikanIslam2)
                    End If

                    If Not B_PendidikanIslam1 = -2 And Not A_PendidikanIslam1 = -2 And Not A_PendidikanIslam2 = -2 Then

                        BM_PendidikanIslam1 = Math.Ceiling((B_PendidikanIslam1 / 100) * BerterusanPI)

                        If Not (A_PendidikanIslam1) = "-1" And Not (A_PendidikanIslam2) = "-1" Then
                            A_PendidikanIslam1 = Math.Ceiling((A_PendidikanIslam1 / 100) * 50) '50%
                            A_PendidikanIslam2 = Math.Ceiling((A_PendidikanIslam2 / 100) * 20) '20%

                            PointerPI1 = Math.Ceiling(BM_PendidikanIslam1)
                            PointerPI2 = Math.Ceiling(A_PendidikanIslam1 + A_PendidikanIslam2)
                            PointerPI = Math.Ceiling(PointerPI1 + PointerPI2)

                            strSQL = "UPDATE kpmkv_pelajar_markah SET PendidikanIslam='" & PointerPI & "' Where PelajarID='" & strPelajarID & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)
                        ElseIf (A_PendidikanIslam1) = "-1" Or (A_PendidikanIslam2) = "-1" Then
                            strSQL = "UPDATE kpmkv_pelajar_markah SET PendidikanIslam='-1' Where PelajarID='" & strPelajarID & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)
                        End If

                    End If

                End If

            Next

            lblMarkahPI.Text = "JANA BERPERINGKAT PI BERJAYA."

        Catch ex As Exception

            lblMarkahPI.Text = "System Error : " & ex.Message & " PelajarID : " & strPelajarID

        End Try

    End Sub

    Private Sub btnmarkahPM_Click(sender As Object, e As EventArgs) Handles btnmarkahPM.Click

        Dim strPelajarID As String = ""

        Try

            Dim BM_PendidikanMoral As Integer
            Dim AM_PendidikanMoral As Integer
            Dim BerterusanPM As Integer
            Dim AkhiranPM As Integer
            Dim B_PendidikanMoral As Integer
            Dim A_PendidikanMoral As Integer
            Dim PointerPM As Integer
            'Dim GredPM As Integer 

            strRet = oCommon.ExecuteSQL(janaBerperingkat)

            Dim sqlDA As New SqlDataAdapter(strSQL, objConn)
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1

                strPelajarID = ds.Tables(0).Rows(i).Item(0).ToString

                B_PendidikanMoral = -2
                A_PendidikanMoral = -2

                strSQL = "SELECT PendidikanMoral FROM kpmkv_pelajar_markah WHERE PelajarID = '" & strPelajarID & "'"

                If oCommon.getFieldValue(strSQL).Length = 0 Then

                    strSQL = "SELECT PB FROM kpmkv_aka_weightage WHERE Tahun='" & ddlTahun.Text & "'"
                    BerterusanPM = oCommon.getFieldValue(strSQL)

                    strSQL = "SELECT PA FROM kpmkv_aka_weightage WHERE Tahun='" & ddlTahun.Text & "'"
                    AkhiranPM = oCommon.getFieldValue(strSQL)

                    strSQL = "Select B_PendidikanMoral from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
                    If Not oCommon.getFieldValue(strSQL).Length = 0 Then
                        B_PendidikanMoral = oCommon.getFieldValue(strSQL)
                        B_PendidikanMoral = Math.Ceiling(B_PendidikanMoral)
                    End If

                    strSQL = "Select A_PendidikanMoral from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
                    If Not oCommon.getFieldValue(strSQL).Length = 0 Then
                        A_PendidikanMoral = oCommon.getFieldValue(strSQL)
                        A_PendidikanMoral = Math.Ceiling(A_PendidikanMoral)
                    End If

                    If Not B_PendidikanMoral = -2 And Not A_PendidikanMoral = -2 Then

                        If Not (B_PendidikanMoral) = "-1" And Not (A_PendidikanMoral) = "-1" Then
                            BM_PendidikanMoral = Math.Ceiling((B_PendidikanMoral / 100) * BerterusanPM)
                            AM_PendidikanMoral = Math.Ceiling((A_PendidikanMoral / 100) * AkhiranPM)
                            PointerPM = Math.Ceiling(BM_PendidikanMoral + AM_PendidikanMoral)
                            strSQL = "UPDATE kpmkv_pelajar_markah SET PendidikanMoral='" & PointerPM & "' Where PelajarID='" & strPelajarID & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)
                        ElseIf (B_PendidikanMoral) = "-1" Or (A_PendidikanMoral) = "-1" Then
                            strSQL = "UPDATE kpmkv_pelajar_markah SET PendidikanMoral='-1' Where PelajarID='" & strPelajarID & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)
                        End If

                    End If

                End If

            Next

            lblMarkahPM.Text = "JANA BERPERINGKAT PM BERJAYA."

        Catch ex As Exception

            lblMarkahPM.Text = "System Error : " & ex.Message & " PelajarID : " & strPelajarID

        End Try

    End Sub

    Private Sub btnJanaGred_Click(sender As Object, e As EventArgs) Handles btnJanaGred.Click

        Dim strPelajarID As String = ""

        Try

            strRet = oCommon.ExecuteSQL(janaBerperingkat)

            Dim sqlDA As New SqlDataAdapter(strSQL, objConn)
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1

                strPelajarID = ds.Tables(0).Rows(i).Item(0).ToString

                Dim BM As String
                Dim GredBM As String

                Dim Kertas1 As Integer = 0
                Dim Kertas2 As Integer = 0

                If ddlSemester.SelectedValue = 4 Then

                    strSQL = "SELECT A_BahasaMelayu1, A_BahasaMelayu2 FROM kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
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

                End If

                strSQL = "SELECT BahasaMelayu as BM FROM kpmkv_pelajar_markah"
                strSQL += " Where PelajarID='" & strPelajarID & "'"
                BM = oCommon.getFieldValue(strSQL)

                If Kertas1 = -1 Or Kertas2 = -1 Then

                    strSQL = "UPDATE kpmkv_pelajar_markah SET GredBM = 'T' Where PelajarID='" & strPelajarID & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)

                Else

                    If String.IsNullOrEmpty(BM) Then
                        strSQL = "UPDATE kpmkv_pelajar_markah SET GredBM='' Where PelajarID='" & strPelajarID & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)
                    Else
                        strSQL = "SELECT TOP ( 1 ) Gred FROM  kpmkv_gred WHERE '" & Integer.Parse(BM) & "' BETWEEN MarkahFrom AND MarkahTo AND Jenis='AKADEMIK'"
                        GredBM = oCommon.getFieldValue(strSQL)

                        strSQL = "UPDATE kpmkv_pelajar_markah SET GredBM='" & GredBM & "' Where PelajarID='" & strPelajarID & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)
                    End If

                End If

                '-----------------------------------------------------------------
                Dim BI As String
                Dim GredBI As String

                strSQL = "SELECT BahasaInggeris FROM kpmkv_pelajar_markah"
                strSQL += " Where PelajarID='" & strPelajarID & "'"

                'If BI = "0" Then
                If String.IsNullOrEmpty(oCommon.getFieldValue(strSQL)) Then
                Else
                    BI = oCommon.getFieldValue(strSQL)

                    strSQL = "SELECT TOP ( 1 ) Gred FROM  kpmkv_gred WHERE '" & Integer.Parse(BI) & "' BETWEEN MarkahFrom AND MarkahTo AND Jenis='AKADEMIK'"
                    GredBI = oCommon.getFieldValue(strSQL)

                    strSQL = "UPDATE kpmkv_pelajar_markah SET GredBI='" & GredBI & "' Where PelajarID='" & strPelajarID & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)

                End If
                '------------------------------------------------------------------------------------------------------------------------
                Dim SC As Integer
                Dim GredSC As String

                strSQL = "SELECT Science FROM kpmkv_pelajar_markah"
                strSQL += " Where PelajarID='" & strPelajarID & "'"

                'If SC = 0 Then
                If String.IsNullOrEmpty(oCommon.getFieldValue(strSQL)) Then
                Else
                    SC = oCommon.getFieldValue(strSQL)

                    strSQL = "SELECT TOP ( 1 ) Gred FROM  kpmkv_gred WHERE '" & SC & "' BETWEEN MarkahFrom AND MarkahTo AND Jenis='AKADEMIK'"
                    GredSC = oCommon.getFieldValue(strSQL)

                    strSQL = "UPDATE kpmkv_pelajar_markah SET GredSC='" & GredSC & "' Where PelajarID='" & strPelajarID & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)

                End If
                '------------------------------------------------------------------------------------------------------------

                Dim SJ As String
                Dim GredSJ As String

                strSQL = "SELECT Sejarah FROM kpmkv_pelajar_markah"
                strSQL += " Where PelajarID='" & strPelajarID & "'"
                SJ = oCommon.getFieldValue(strSQL)

                'If SJ = "0" Then
                If String.IsNullOrEmpty(oCommon.getFieldValue(strSQL)) Then
                Else
                    SJ = oCommon.getFieldValue(strSQL)

                    strSQL = "SELECT TOP ( 1 ) Gred FROM  kpmkv_gred WHERE '" & Integer.Parse(SJ) & "' BETWEEN MarkahFrom AND MarkahTo AND Jenis='AKADEMIK'"
                    GredSJ = oCommon.getFieldValue(strSQL)

                    strSQL = "UPDATE kpmkv_pelajar_markah SET GredSJ='" & GredSJ & "' Where PelajarID='" & strPelajarID & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)

                End If
                '-------------------------------------------------------------------------------------------------------------

                Dim PI As String
                Dim GredPI As String

                strSQL = "SELECT PendidikanIslam FROM kpmkv_pelajar_markah"
                strSQL += " Where PelajarID='" & strPelajarID & "'"
                PI = oCommon.getFieldValue(strSQL)

                If PI = "0" Then
                ElseIf String.IsNullOrEmpty(oCommon.getFieldValue(strSQL)) Then
                Else
                    PI = oCommon.getFieldValue(strSQL)

                    strSQL = "SELECT TOP ( 1 ) Gred FROM  kpmkv_gred WHERE '" & Integer.Parse(PI) & "' BETWEEN MarkahFrom AND MarkahTo AND Jenis='AKADEMIK'"
                    GredPI = oCommon.getFieldValue(strSQL)

                    strSQL = "UPDATE kpmkv_pelajar_markah SET GredPI='" & GredPI & "' Where PelajarID='" & strPelajarID & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)

                End If
                '-------------------------------------------------------------------------------------------------------------

                Dim PM As String
                Dim GredPM As String

                strSQL = "SELECT PendidikanMoral FROM kpmkv_pelajar_markah"
                strSQL += " Where PelajarID='" & strPelajarID & "'"
                PM = oCommon.getFieldValue(strSQL)

                If PM = "0" Then
                ElseIf String.IsNullOrEmpty(oCommon.getFieldValue(strSQL)) Then
                Else
                    PM = oCommon.getFieldValue(strSQL)

                    strSQL = "SELECT TOP ( 1 ) Gred FROM  kpmkv_gred WHERE '" & Integer.Parse(PM) & "' BETWEEN MarkahFrom AND MarkahTo AND Jenis='AKADEMIK'"
                    GredPM = oCommon.getFieldValue(strSQL)

                    strSQL = "UPDATE kpmkv_pelajar_markah SET GredPM='" & GredPM & "' Where PelajarID='" & strPelajarID & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)

                End If
                '-------------------------------------------------------------------------------------------------------------

                Dim MT As String
                Dim GredMT As String

                strSQL = "SELECT Mathematics FROM kpmkv_pelajar_markah"
                strSQL += " Where PelajarID='" & strPelajarID & "'"
                MT = oCommon.getFieldValue(strSQL)

                'If MT = "0" Then
                If String.IsNullOrEmpty(oCommon.getFieldValue(strSQL)) Then
                Else
                    MT = oCommon.getFieldValue(strSQL)

                    strSQL = "SELECT TOP ( 1 ) Gred FROM  kpmkv_gred WHERE '" & Integer.Parse(MT) & "' BETWEEN MarkahFrom AND MarkahTo AND Jenis='AKADEMIK'"
                    GredMT = oCommon.getFieldValue(strSQL)

                    strSQL = "UPDATE kpmkv_pelajar_markah SET GredMT='" & GredMT & "' Where PelajarID='" & strPelajarID & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)
                End If

            Next

            lblJanaGred.Text = "JANA GRED BERJAYA."

        Catch ex As Exception

            lblJanaGred.Text = "System Error : " & ex.Message & " PelajarID : " & strPelajarID

        End Try

    End Sub

    Private Sub btnJanaKeseluruhanPeringkat1_Click(sender As Object, e As EventArgs) Handles btnJanaKeseluruhanPeringkat1.Click

        Try

            strRet = "0"

            Debug.WriteLine(System.DateTime.Now)

            lblMsg.Text = ""

            Akademik_markah_Negeri()

            If Not strRet = "0" Then
                divMsg.Attributes("class") = "error"
                lblJanaKeseluruhanPeringkat1.Text = "Tidak Berjaya mengemaskini markah keseluruhan Pentaksiran Akhir Akademik"
            Else
                divMsg.Attributes("class") = "info"
                lblJanaKeseluruhanPeringkat1.Text = "Berjaya mengemaskini markah keseluruhan Pentaksiran Akhir Akademik"

            End If

            Debug.WriteLine(System.DateTime.Now)

        Catch ex As Exception
            divMsg.Attributes("class") = "error"
            lblJanaKeseluruhanPeringkat1.Text = "Tidak Berjaya mengemaskini markah keseluruhan Pentaksiran Akhir Akademik." & ex.Message & " PelajarID : " & strPelajarID
        End Try

    End Sub

    Private Sub btnJanaKeseluruhanPeringkat2_Click(sender As Object, e As EventArgs) Handles btnJanaKeseluruhanPeringkat2.Click

        Try

            strRet = "0"

            Debug.WriteLine(System.DateTime.Now)

            lblMsg.Text = ""

            Akademik_gred_Negeri()

            If Not strRet = "0" Then
                divMsg.Attributes("class") = "error"
                lblJanaKeseluruhanPeringkat2.Text = "Tidak Berjaya mengemaskini gred keseluruhan Pentaksiran Akhir Akademik"
            Else
                divMsg.Attributes("class") = "info"
                lblJanaKeseluruhanPeringkat2.Text = "Berjaya mengemaskini gred keseluruhan Pentaksiran Akhir Akademik"

            End If

            Debug.WriteLine(System.DateTime.Now)

        Catch ex As Exception
            divMsg.Attributes("class") = "error"
            lblJanaKeseluruhanPeringkat2.Text = "Tidak Berjaya mengemaskini gred keseluruhan Pentaksiran Akhir Akademik." & ex.Message & " PelajarID : " & strPelajarID
        End Try

    End Sub
End Class