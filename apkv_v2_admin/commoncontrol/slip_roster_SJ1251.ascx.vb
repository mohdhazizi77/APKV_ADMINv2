Imports System.Data.SqlClient
Imports System.IO
Imports iTextSharp.text
Imports iTextSharp.text.pdf

Public Class slip_roster_SJ12511

    Inherits System.Web.UI.UserControl
    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strSQL2 As String = ""
    Dim strSQL3 As String = ""
    Dim strRet As String = ""



    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            If Not IsPostBack Then


                kpmkv_negeri_list()
                ddlNegeri.SelectedIndex = 0

                kpmkv_jenis_list()
                ddlJenis.SelectedIndex = 0

                kpmkv_kolej_list()
                ddlKolej.SelectedIndex = 0

                kpmkv_tahun_list()
                ddlTahun.Text = Now.Year

                kpmkv_kodkursus_list()
                ddlKodKursus.SelectedValue = "-SEMUA-"


            End If

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message
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

            ddlNegeri.Items.Insert(0, "-Pilih-")

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

            ddlJenis.Items.Insert(0, "-Pilih-")

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

            ddlKolej.Items.Insert(0, "-Pilih-")

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
    Private Sub kpmkv_kodkursus_list()

        strSQL = "SELECT kpmkv_kursus.KodKursus, kpmkv_kursus.KursusID FROM kpmkv_kursus_kolej LEFT OUTER JOIN"
        strSQL += " kpmkv_kursus ON kpmkv_kursus_kolej.KursusID = kpmkv_kursus.KursusID"
        strSQL += " WHERE kpmkv_kursus_kolej.KolejRecordID='" & ddlKolej.SelectedValue & "' AND kpmkv_kursus.Tahun='" & ddlTahun.SelectedValue & "' "
        strSQL += " AND kpmkv_kursus.Sesi='" & chkSesi.SelectedValue & "' GROUP BY kpmkv_kursus.KodKursus,kpmkv_kursus.KursusID"
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

            ddlKodKursus.Items.Insert(0, "-SEMUA-")

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub
    Private Sub kpmkv_kelas_list()
        strSQL = " SELECT kpmkv_kelas.NamaKelas, kpmkv_kelas.KelasID FROM kpmkv_kelas_kursus LEFT OUTER JOIN kpmkv_kelas ON kpmkv_kelas_kursus.KelasID = kpmkv_kelas.KelasID LEFT OUTER JOIN"
        strSQL += " kpmkv_kursus ON kpmkv_kelas_kursus.KursusID = kpmkv_kursus.KursusID"
        strSQL += " WHERE kpmkv_kelas.KolejRecordID='" & ddlKolej.SelectedValue & "' AND kpmkv_kelas_kursus.KursusID= '" & ddlKodKursus.SelectedValue & "' AND kpmkv_kursus.Tahun= '" & ddlTahun.SelectedValue & "' ORDER BY  kpmkv_kelas.NamaKelas"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlNamaKelas.DataSource = ds
            ddlNamaKelas.DataTextField = "NamaKelas"
            ddlNamaKelas.DataValueField = "KelasID"
            ddlNamaKelas.DataBind()

            ddlNamaKelas.Items.Insert(0, "-Pilih-")
        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub
    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        lblMsg.Text = ""
        strRet = BindData(datRespondent)
        tbl_menu_check()
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
        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrder As String = " ORDER BY kpmkv_pelajar.Nama ASC"

        '--not deleted
        tmpSQL = "SELECT kpmkv_pelajar.PelajarID, kpmkv_pelajar.Tahun, kpmkv_pelajar.Semester, kpmkv_pelajar.Sesi, kpmkv_pelajar.Nama, kpmkv_pelajar.MYKAD, kpmkv_pelajar.AngkaGiliran, "
        tmpSQL += " kpmkv_kluster.NamaKluster, kpmkv_kursus.NamaKursus, kpmkv_pelajar.Kaum, kpmkv_pelajar.Jantina, kpmkv_pelajar.Agama, kpmkv_status.Status, kpmkv_kelas.NamaKelas"
        tmpSQL += " FROM  kpmkv_pelajar LEFT OUTER JOIN kpmkv_kursus ON kpmkv_pelajar.KursusID = kpmkv_kursus.KursusID LEFT OUTER JOIN kpmkv_kluster ON kpmkv_kursus.KlusterID=kpmkv_kluster.KlusterID"
        tmpSQL += " LEFT OUTER JOIN kpmkv_status ON kpmkv_pelajar.StatusID = kpmkv_status.StatusID LEFT OUTER JOIN kpmkv_kelas ON kpmkv_pelajar.KelasID = kpmkv_kelas.KelasID"
        strWhere = " WHERE kpmkv_pelajar.IsDeleted='N' AND kpmkv_pelajar.StatusID='2' "
        strWhere += " AND kpmkv_pelajar.KolejRecordID='" & ddlKolej.SelectedValue & "' AND kpmkv_pelajar.Semester ='4'"
        strWhere += " AND kpmkv_pelajar.KelasID IS NOT NULL"

        '--tahun
        If Not ddlTahun.Text = "" Then
            strWhere += " AND kpmkv_pelajar.Tahun ='" & ddlTahun.Text & "'"
        End If

        '--sesi
        If Not chkSesi.Text = "" Then
            strWhere += " AND kpmkv_pelajar.Sesi ='" & chkSesi.Text & "'"
        End If
        '--kodkursus

        If Not ddlKodKursus.SelectedValue = "-SEMUA-" Then
            strWhere += " AND kpmkv_pelajar.KursusID ='" & ddlKodKursus.SelectedValue & "'"
        End If
        '--NamaKelas
        If Not (ddlNamaKelas.SelectedValue = "-PILIH-" Or ddlNamaKelas.SelectedValue = "") Then
            strWhere += " AND kpmkv_pelajar.KelasID ='" & ddlNamaKelas.SelectedValue & "'"
        End If
        '--txtNama
        If Not txtNama.Text.Length = 0 Then
            strWhere += " AND kpmkv_pelajar.Nama LIKE '%" & oCommon.FixSingleQuotes(txtNama.Text) & "%'"
        End If

        '--txtMYKAD
        If Not txtMYKAD.Text.Length = 0 Then
            strWhere += " AND kpmkv_pelajar.MYKAD='" & oCommon.FixSingleQuotes(txtMYKAD.Text) & "'"
        End If

        '--txtAngkaGiliran
        If Not txtAngkaGiliran.Text.Length = 0 Then
            strWhere += " AND kpmkv_pelajar.AngkaGiliran LIKE '%" & oCommon.FixSingleQuotes(txtAngkaGiliran.Text) & "%'"
        End If

        getSQL = tmpSQL & strWhere & strOrder
        ''--debug
        'Response.Write(getSQL)

        Return getSQL

    End Function

    Private Sub datRespondent_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles datRespondent.PageIndexChanging
        datRespondent.PageIndex = e.NewPageIndex
        strRet = BindData(datRespondent)

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

    Protected Sub btnPrint_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnPrint.Click

        Dim strKey As String

        '1'--start here
        strSQL = "SELECT Nama FROM kpmkv_Kolej WHERE RecordID='" & ddlKolej.SelectedValue & "'"
        Dim strKolejnama As String = oCommon.getFieldValue(strSQL)

        'kolejnegeri
        strSQL = "SELECT Negeri FROM kpmkv_kolej WHERE Nama='" & strKolejnama & "'"
        Dim strKolejnegeri As String = oCommon.getFieldValue(strSQL)
        'looping
        For i As Integer = 0 To datRespondent.Rows.Count - 1
            Dim row As GridViewRow = datRespondent.Rows(0)
            Dim cb As CheckBox = datRespondent.Rows(i).FindControl("chkSelect")

            strKey = datRespondent.DataKeys(i).Value.ToString

            If cb.Checked = True Then

                'Dim strNama As String = ""
                'Dim strMykad As String = ""
                'Dim strAngkaGiliran As String = ""
                Dim strBMTahun As String = ""
                Dim strPointer As String = ""
                'Dim strStudentID As String = ""
                Dim strGredSJ As String = ""
                ''--Tokenid,ClassCode,Q001Remarks
                'strSQL = "SELECT Nama FROM kpmkv_pelajar WHERE PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                'strNama = oCommon.getFieldValue(strSQL)

                'strSQL = "SELECT Mykad FROM kpmkv_pelajar WHERE PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                'strMykad = oCommon.getFieldValue(strSQL)

                'strSQL = "SELECT AngkaGiliran FROM kpmkv_pelajar WHERE PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                'strAngkaGiliran = oCommon.getFieldValue(strSQL)

                strSQL = "SELECT isBMTahun FROM kpmkv_pelajar WHERE PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                strBMTahun = oCommon.getFieldValue(strSQL)

                ''--Pointer SJ 
                strPointer = oCommon.getFieldValue("SELECT PointerSJSetara FROM kpmkv_pelajar_markah WHERE PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'")
                ''--Gred SJ 
                If strPointer = "-1" Then
                    strGredSJ = "T"
                ElseIf strPointer = "" Then
                    strGredSJ = ""
                Else
                    strSQL = "SELECT TOP ( 1 ) Status FROM  kpmkv_gred_sejarah WHERE '" & Math.Round(Double.Parse(strPointer), 0) & "' BETWEEN MarkahFrom AND MarkahTo AND Tahun='" & strBMTahun & "' AND Sesi='" & chkSesi.Text & "'"
                    strGredSJ = oCommon.getFieldValue(strSQL)
                End If

                'change on 17082016
                strSQL = "UPDATE kpmkv_pelajar_markah SET GredSJSetara='" & strGredSJ & "' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                strRet = oCommon.ExecuteSQL(strSQL)

            End If 'is check box

        Next

    End Sub

    Private Sub btnPrintKeseluruhan_Click(sender As Object, e As EventArgs) Handles btnPrintKeseluruhan.Click

        Dim strKey As String

        If ddlNegeri.Text = "-Pilih-" Then

            strSQL = "SELECT Negeri FROM kpmkv_negeri"
            strRet = oCommon.ExecuteSQL(strSQL)
            Dim sqlNegeri As New SqlDataAdapter(strSQL, objConn)
            Dim dsNegeri As DataSet = New DataSet
            sqlNegeri.Fill(dsNegeri, "AnyTable")

            For listNegeri As Integer = 0 To dsNegeri.Tables(0).Rows.Count - 1

                strSQL = "SELECT RecordID FROM kpmkv_kolej WHERE Negeri = '" & dsNegeri.Tables(0).Rows(listNegeri).Item(0).ToString & "'"
                strRet = oCommon.ExecuteSQL(strSQL)

                Dim sqlRecordID As New SqlDataAdapter(strSQL, objConn)
                Dim dsRecordID As DataSet = New DataSet
                sqlRecordID.Fill(dsRecordID, "AnyTable")

                For i As Integer = 0 To dsRecordID.Tables(0).Rows.Count - 1

                    Dim strRecordID As String = dsRecordID.Tables(0).Rows(i).Item(0).ToString

                    Dim tmpSQL As String
                    Dim strWhere As String = ""
                    Dim strOrder As String = " ORDER BY kpmkv_pelajar.Nama ASC"

                    '--not deleted
                    tmpSQL = "SELECT kpmkv_pelajar.PelajarID "
                    tmpSQL += " FROM  kpmkv_pelajar LEFT OUTER JOIN kpmkv_kursus ON kpmkv_pelajar.KursusID = kpmkv_kursus.KursusID LEFT OUTER JOIN kpmkv_kluster ON kpmkv_kursus.KlusterID=kpmkv_kluster.KlusterID"
                    tmpSQL += " LEFT OUTER JOIN kpmkv_status ON kpmkv_pelajar.StatusID = kpmkv_status.StatusID LEFT OUTER JOIN kpmkv_kelas ON kpmkv_pelajar.KelasID = kpmkv_kelas.KelasID"
                    strWhere = " WHERE kpmkv_pelajar.IsDeleted='N' AND kpmkv_pelajar.StatusID='2' "
                    strWhere += " AND kpmkv_pelajar.KolejRecordID='" & strRecordID & "' AND kpmkv_pelajar.Semester ='4'"
                    strWhere += " AND kpmkv_pelajar.KelasID IS NOT NULL"

                    '--tahun
                    If Not ddlTahun.Text = "" Then
                        strWhere += " AND kpmkv_pelajar.Tahun ='" & ddlTahun.Text & "'"
                    End If

                    '--sesi
                    If Not chkSesi.Text = "" Then
                        strWhere += " AND kpmkv_pelajar.Sesi ='" & chkSesi.Text & "'"
                    End If
                    '--kodkursus

                    If Not ddlKodKursus.SelectedValue = "-SEMUA-" Then
                        strWhere += " AND kpmkv_pelajar.KursusID ='" & ddlKodKursus.SelectedValue & "'"
                    End If
                    '--NamaKelas
                    If Not (ddlNamaKelas.SelectedValue = "-Pilih-" Or ddlNamaKelas.SelectedValue = "") Then
                        strWhere += " AND kpmkv_pelajar.KelasID ='" & ddlNamaKelas.SelectedValue & "'"
                    End If
                    '--txtNama
                    If Not txtNama.Text.Length = 0 Then
                        strWhere += " AND kpmkv_pelajar.Nama LIKE '%" & oCommon.FixSingleQuotes(txtNama.Text) & "%'"
                    End If

                    '--txtMYKAD
                    If Not txtMYKAD.Text.Length = 0 Then
                        strWhere += " AND kpmkv_pelajar.MYKAD='" & oCommon.FixSingleQuotes(txtMYKAD.Text) & "'"
                    End If

                    '--txtAngkaGiliran
                    If Not txtAngkaGiliran.Text.Length = 0 Then
                        strWhere += " AND kpmkv_pelajar.AngkaGiliran LIKE '%" & oCommon.FixSingleQuotes(txtAngkaGiliran.Text) & "%'"
                    End If

                    strSQL = tmpSQL + strWhere + strOrder

                    strRet = oCommon.ExecuteSQL(strSQL)

                    Dim sqlPelajarID As New SqlDataAdapter(strSQL, objConn)
                    Dim dsPelajarID As DataSet = New DataSet
                    sqlPelajarID.Fill(dsPelajarID, "AnyTable")

                    '1'--start here
                    strSQL = "SELECT Nama FROM kpmkv_Kolej WHERE RecordID='" & strRecordID & "'"
                    Dim strKolejnama As String = oCommon.getFieldValue(strSQL)

                    'kolejnegeri
                    strSQL = "SELECT Negeri FROM kpmkv_kolej WHERE Nama='" & strKolejnama & "'"
                    Dim strKolejnegeri As String = oCommon.getFieldValue(strSQL)
                    'looping
                    For j As Integer = 0 To dsPelajarID.Tables(0).Rows.Count - 1
                        'Dim row As GridViewRow = datRespondent.Rows(0)
                        'Dim cb As CheckBox = datRespondent.Rows(j).FindControl("chkSelect")

                        strKey = dsPelajarID.Tables(0).Rows(j).Item(0).ToString

                        'Dim strNama As String = ""
                        'Dim strMykad As String = ""
                        'Dim strAngkaGiliran As String = ""
                        Dim strBMTahun As String = ""
                        Dim strPointer As String = ""
                        'Dim strStudentID As String = ""
                        Dim strGredSJ As String = ""
                        ''--Tokenid,ClassCode,Q001Remarks
                        'strSQL = "SELECT Nama FROM kpmkv_pelajar WHERE PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                        'strNama = oCommon.getFieldValue(strSQL)

                        'strSQL = "SELECT Mykad FROM kpmkv_pelajar WHERE PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                        'strMykad = oCommon.getFieldValue(strSQL)

                        'strSQL = "SELECT AngkaGiliran FROM kpmkv_pelajar WHERE PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                        'strAngkaGiliran = oCommon.getFieldValue(strSQL)

                        strSQL = "SELECT isBMTahun FROM kpmkv_pelajar WHERE PelajarID='" & strKey & "'"
                        strBMTahun = oCommon.getFieldValue(strSQL)

                        ''--Pointer SJ 
                        strPointer = oCommon.getFieldValue("SELECT PointerSJSetara FROM kpmkv_pelajar_markah WHERE PelajarID='" & strKey & "'")
                        ''--Gred SJ 
                        If strPointer = "-1" Then
                            strGredSJ = "T"
                        ElseIf strPointer = "" Then
                            strGredSJ = ""
                        Else
                            strSQL = "SELECT TOP ( 1 ) Status FROM  kpmkv_gred_sejarah WHERE '" & Math.Round(Double.Parse(strPointer), 0) & "' BETWEEN MarkahFrom AND MarkahTo AND Tahun='" & strBMTahun & "' AND Sesi='" & chkSesi.Text & "'"
                            strGredSJ = oCommon.getFieldValue(strSQL)
                        End If

                        'change on 17082016
                        strSQL = "UPDATE kpmkv_pelajar_markah SET GredSJSetara='" & strGredSJ & "' Where PelajarID='" & strKey & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)

                        If Not strRet = "0" Then
                            divMsg.Attributes("class") = "error"
                            lblMsg.Text = "Tidak Berjaya menjana Gred Pelajar"

                        Else

                            divMsg.Attributes("class") = "info"
                            lblMsg.Text = "Berjaya menjana Gred Pelajar"

                        End If

                    Next

                Next

            Next

        Else

            strSQL = "SELECT RecordID FROM kpmkv_kolej WHERE Negeri = '" & ddlNegeri.Text & "'"
            strRet = oCommon.ExecuteSQL(strSQL)

            Dim sqlRecordID As New SqlDataAdapter(strSQL, objConn)
            Dim dsRecordID As DataSet = New DataSet
            sqlRecordID.Fill(dsRecordID, "AnyTable")

            For i As Integer = 0 To dsRecordID.Tables(0).Rows.Count - 1

                Dim strRecordID As String = dsRecordID.Tables(0).Rows(i).Item(0).ToString

                Dim tmpSQL As String
                Dim strWhere As String = ""
                Dim strOrder As String = " ORDER BY kpmkv_pelajar.Nama ASC"

                '--not deleted
                tmpSQL = "SELECT kpmkv_pelajar.PelajarID "
                tmpSQL += " FROM  kpmkv_pelajar LEFT OUTER JOIN kpmkv_kursus ON kpmkv_pelajar.KursusID = kpmkv_kursus.KursusID LEFT OUTER JOIN kpmkv_kluster ON kpmkv_kursus.KlusterID=kpmkv_kluster.KlusterID"
                tmpSQL += " LEFT OUTER JOIN kpmkv_status ON kpmkv_pelajar.StatusID = kpmkv_status.StatusID LEFT OUTER JOIN kpmkv_kelas ON kpmkv_pelajar.KelasID = kpmkv_kelas.KelasID"
                strWhere = " WHERE kpmkv_pelajar.IsDeleted='N' AND kpmkv_pelajar.StatusID='2' "
                strWhere += " AND kpmkv_pelajar.KolejRecordID='" & strRecordID & "' AND kpmkv_pelajar.Semester ='4'"
                strWhere += " AND kpmkv_pelajar.KelasID IS NOT NULL"

                '--tahun
                If Not ddlTahun.Text = "" Then
                    strWhere += " AND kpmkv_pelajar.Tahun ='" & ddlTahun.Text & "'"
                End If

                '--sesi
                If Not chkSesi.Text = "" Then
                    strWhere += " AND kpmkv_pelajar.Sesi ='" & chkSesi.Text & "'"
                End If
                '--kodkursus

                If Not ddlKodKursus.SelectedValue = "-SEMUA-" Then
                    strWhere += " AND kpmkv_pelajar.KursusID ='" & ddlKodKursus.SelectedValue & "'"
                End If
                '--NamaKelas
                If Not (ddlNamaKelas.SelectedValue = "-Pilih-" Or ddlNamaKelas.SelectedValue = "") Then
                    strWhere += " AND kpmkv_pelajar.KelasID ='" & ddlNamaKelas.SelectedValue & "'"
                End If
                '--txtNama
                If Not txtNama.Text.Length = 0 Then
                    strWhere += " AND kpmkv_pelajar.Nama LIKE '%" & oCommon.FixSingleQuotes(txtNama.Text) & "%'"
                End If

                '--txtMYKAD
                If Not txtMYKAD.Text.Length = 0 Then
                    strWhere += " AND kpmkv_pelajar.MYKAD='" & oCommon.FixSingleQuotes(txtMYKAD.Text) & "'"
                End If

                '--txtAngkaGiliran
                If Not txtAngkaGiliran.Text.Length = 0 Then
                    strWhere += " AND kpmkv_pelajar.AngkaGiliran LIKE '%" & oCommon.FixSingleQuotes(txtAngkaGiliran.Text) & "%'"
                End If

                strSQL = tmpSQL + strWhere + strOrder

                strRet = oCommon.ExecuteSQL(strSQL)

                Dim sqlPelajarID As New SqlDataAdapter(strSQL, objConn)
                Dim dsPelajarID As DataSet = New DataSet
                sqlPelajarID.Fill(dsPelajarID, "AnyTable")

                '1'--start here
                strSQL = "SELECT Nama FROM kpmkv_Kolej WHERE RecordID='" & strRecordID & "'"
                Dim strKolejnama As String = oCommon.getFieldValue(strSQL)

                'kolejnegeri
                strSQL = "SELECT Negeri FROM kpmkv_kolej WHERE Nama='" & strKolejnama & "'"
                Dim strKolejnegeri As String = oCommon.getFieldValue(strSQL)
                'looping
                For j As Integer = 0 To dsPelajarID.Tables(0).Rows.Count - 1
                    'Dim row As GridViewRow = datRespondent.Rows(0)
                    'Dim cb As CheckBox = datRespondent.Rows(j).FindControl("chkSelect")

                    strKey = dsPelajarID.Tables(0).Rows(j).Item(0).ToString

                    'Dim strNama As String = ""
                    'Dim strMykad As String = ""
                    'Dim strAngkaGiliran As String = ""
                    Dim strBMTahun As String = ""
                    Dim strPointer As String = ""
                    'Dim strStudentID As String = ""
                    Dim strGredSJ As String = ""
                    ''--Tokenid,ClassCode,Q001Remarks
                    'strSQL = "SELECT Nama FROM kpmkv_pelajar WHERE PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                    'strNama = oCommon.getFieldValue(strSQL)

                    'strSQL = "SELECT Mykad FROM kpmkv_pelajar WHERE PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                    'strMykad = oCommon.getFieldValue(strSQL)

                    'strSQL = "SELECT AngkaGiliran FROM kpmkv_pelajar WHERE PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                    'strAngkaGiliran = oCommon.getFieldValue(strSQL)

                    strSQL = "SELECT isBMTahun FROM kpmkv_pelajar WHERE PelajarID='" & strKey & "'"
                    strBMTahun = oCommon.getFieldValue(strSQL)

                    ''--Pointer SJ 
                    strPointer = oCommon.getFieldValue("SELECT PointerSJSetara FROM kpmkv_pelajar_markah WHERE PelajarID='" & strKey & "'")
                    ''--Gred SJ 
                    If strPointer = "-1" Then
                        strGredSJ = "T"
                    ElseIf strPointer = "" Then
                        strGredSJ = ""
                    Else
                        strSQL = "SELECT TOP ( 1 ) Status FROM  kpmkv_gred_sejarah WHERE '" & Math.Round(Double.Parse(strPointer), 0) & "' BETWEEN MarkahFrom AND MarkahTo AND Tahun='" & strBMTahun & "' AND Sesi='" & chkSesi.Text & "'"
                        strGredSJ = oCommon.getFieldValue(strSQL)
                    End If

                    'change on 17082016
                    strSQL = "UPDATE kpmkv_pelajar_markah SET GredSJSetara='" & strGredSJ & "' Where PelajarID='" & strKey & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)

                    If Not strRet = "0" Then
                        divMsg.Attributes("class") = "error"
                        lblMsg.Text = "Tidak Berjaya menjana Gred Pelajar"

                    Else

                        divMsg.Attributes("class") = "info"
                        lblMsg.Text = "Berjaya menjana Gred Pelajar"

                    End If

                Next

            Next

        End If

    End Sub

    Protected Sub chkSesi_SelectedIndexChanged(sender As Object, e As EventArgs) Handles chkSesi.SelectedIndexChanged
        kpmkv_kodkursus_list()
    End Sub

    Private Sub ddlKodKursus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlKodKursus.SelectedIndexChanged
        kpmkv_kelas_list()

    End Sub

    Private Sub ddlNegeri_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlNegeri.SelectedIndexChanged
        kpmkv_jenis_list()
        kpmkv_kolej_list()
    End Sub

    Private Sub ddlJenis_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlJenis.SelectedIndexChanged
        kpmkv_kolej_list()

    End Sub


End Class