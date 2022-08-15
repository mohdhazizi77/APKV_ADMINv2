﻿Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Public Class markah_ulang_BMSetara1
    Inherits System.Web.UI.UserControl
    Dim oCommon As New Commonfunction
    Dim oCommon2 As New Commonfunction2
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim strConn2 As String = ConfigurationManager.AppSettings("ConnectionString2")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim objConn2 As SqlConnection = New SqlConnection(strConn2)

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
                ddlTahun.Text = Now.Year

                kpmkv_tahunSem()

                kpmkv_bmtahun_list()
                ddlBMTahun.Text = Now.Year

                kpmkv_kodkursus_list()

                kpmkv_kelas_list()

                lblMsg.Text = ""
                strRet = BindData(datRespondent)

                kemaskini_markahulang()

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

    Private Sub kpmkv_tahunSem()
        strSQL = "SELECT DISTINCT TahunSem FROM kpmkv_pelajar WHERE TahunSem IS NOT NULL ORDER BY TahunSem"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlTahunSem.DataSource = ds
            ddlTahunSem.DataTextField = "TahunSem"
            ddlTahunSem.DataValueField = "TahunSem"
            ddlTahunSem.DataBind()

            '--ALL
            ddlTahunSem.Items.Add(New ListItem("-Pilih-", "0"))

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub kpmkv_bmtahun_list()
        strSQL = "SELECT Tahun FROM kpmkv_tahun  ORDER BY Tahun"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlBMTahun.DataSource = ds
            ddlBMTahun.DataTextField = "Tahun"
            ddlBMTahun.DataValueField = "Tahun"
            ddlBMTahun.DataBind()

            '--ALL
            ddlTahun.Items.Add(New ListItem("-Pilih-", "0"))

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
        strSQL += " WHERE kpmkv_kelas.KolejRecordID='" & ddlKolej.SelectedValue & "' AND kpmkv_kelas.Tahun = '" & ddlTahun.SelectedValue & "'"
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
            ' ddlKodKursus.Items.Add(New ListItem("PILIH", "PILIH"))

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
            'ddlNamaKelas.Items.Add(New ListItem("PILIH", "PILIH"))

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

    Private Sub datRespondent_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles datRespondent.RowCommand
        lblMsg.Text = ""

        If (e.CommandName = "Batal") Then
            Dim strPelajarUID = Int32.Parse(e.CommandArgument.ToString())
            Try

                strSQL = "DELETE FROM kpmkv_pelajar_ulang_vokasional WHERE PelajarUlangVID='" & strPelajarUID & "'"
                strRet = oCommon.ExecuteSQL(strSQL)
                If strRet = "0" Then
                    divMsg.Attributes("class") = "info"
                    lblMsg.Text = "Pelajar berjaya dipadamkan"
                Else
                    divMsg.Attributes("class") = "error"
                    lblMsg.Text = "Pelajar tidak berjaya dipadam"

                End If
            Catch ex As Exception
                divMsg.Attributes("class") = "error"
            End Try
        End If
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

        '--not deleted
        tmpSQL = "  SELECT
                    kpmkv_pelajar.Nama,
                    kpmkv_pelajar.PelajarID,
                    kpmkv_pelajar.MYKAD,
                    kpmkv_pelajar.AngkaGiliran,
                    kpmkv_pelajar_markah.BahasaMelayu,
                    kpmkv_pelajar_markah.GredBM,
                    kpmkv_pelajar_ulang.MarkahBMUlang AS 'BahasaMelayuUlang',
                    kpmkv_pelajar_ulang.Gred AS 'GredBMUlang',
                    kpmkv_pelajar_ulang.PointerBMUlang,
                    kpmkv_pelajar_ulang.GredPointerBMUlang
                    FROM
                    kpmkv_pelajar
                    LEFT JOIN kpmkv_pelajar_markah ON kpmkv_pelajar_markah.PelajarID = kpmkv_pelajar.PelajarID
                    LEFT JOIN kpmkv_pelajar_ulang ON kpmkv_pelajar_ulang.PelajarID = kpmkv_pelajar.PelajarID
                    WHERE
                    kpmkv_pelajar_ulang.NamaMataPelajaran = 'BAHASA MELAYU'
                    AND kpmkv_pelajar.IsBMTahun = '" & ddlBMTahun.Text & "'
                    AND kpmkv_pelajar.Semester = '4'
                    AND kpmkv_pelajar.Sesi = '" & chkSesi.Text & "'
                    AND kpmkv_pelajar_ulang.KursusID ='" & ddlKodKursus.SelectedValue & "'
                    AND kpmkv_pelajar_ulang.KelasID ='" & ddlKelas.SelectedValue & "'
                    ORDER BY
                    kpmkv_pelajar.Nama"


        getSQL = tmpSQL
        ''--debug
        ''Response.Write(getSQL)


        Return getSQL

    End Function

    Private Sub datRespondent_SelectedIndexChanging(sender As Object, e As GridViewSelectEventArgs) Handles datRespondent.SelectedIndexChanging
        Dim strKeyID As String = datRespondent.DataKeys(e.NewSelectedIndex).Value.ToString
        'Response.Redirect("pelajar.ulang.akademik.view.aspx?PelajarID=" & strKeyID)

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

    Private Sub ddlKodKursus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlKodKursus.SelectedIndexChanged
        kpmkv_kelas_list()
    End Sub

    Private Sub kemaskini_markahulang()

        For i As Integer = 0 To datRespondent.Rows.Count - 1

            'assign value to integer
            Dim strGredNama As String = ""
            Dim strpointerNama As String = ""
            Dim strGredMarkah As String = ""
            Dim strMP As String = "BAHASA MELAYU"

            Dim strPelajarID As String = datRespondent.DataKeys(i).Value.ToString
            Dim PB As String
            Dim PA As String
            Dim PBMarkah As Integer
            Dim PAMarkah As Integer
            Dim Pointer As Integer

            strGredNama = "BM"
            strpointerNama = "BahasaMelayu"

            Dim txtMarkahBMUlang As Label = CType(datRespondent.Rows(i).FindControl("MarkahBMUlang"), Label)

            strSQL = "SELECT MarkahPB FROM kpmkv_pelajar_ulang WHERE PelajarID = '" & strPelajarID & "' AND NamaMataPelajaran = 'BAHASA MELAYU'"
            Dim strPB As String = oCommon.getFieldValue(strSQL)

            strSQL = "SELECT MarkahPA FROM kpmkv_pelajar_ulang WHERE PelajarID = '" & strPelajarID & "' AND NamaMataPelajaran = 'BAHASA MELAYU'"
            Dim strPA As String = oCommon.getFieldValue(strSQL)

            Dim PB1 As Integer = Integer.Parse(strPB)
            Dim PA2 As Integer = Integer.Parse(strPA)

            'get gred from kpmkv_pelajar_markah
            If Not String.IsNullOrEmpty(strGredNama) Then
                strSQL = "SELECT  Gred" & strGredNama & "  FROM kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
                strGredMarkah = oCommon.getFieldValue(strSQL)
            End If

            strSQL = "SELECT PB FROM kpmkv_aka_weightage WHERE Tahun='" & ddlBMTahun.Text & "'"
            PB = oCommon.getFieldValue(strSQL)

            strSQL = "SELECT PA FROM kpmkv_aka_weightage WHERE Tahun='" & ddlBMTahun.Text & "'"
            PA = oCommon.getFieldValue(strSQL)

            If Not String.IsNullOrEmpty(PB1) Then
                PBMarkah = oCommon.DoConvertN((PB1 / 100) * CInt(PB), 2)
            End If
            If Not String.IsNullOrEmpty(PA2) Then
                PAMarkah = oCommon.DoConvertN((PA2 / 100) * CInt(PA), 2)
            End If

            Pointer = PBMarkah + PAMarkah

            txtMarkahBMUlang.Text = Pointer

        Next

    End Sub

    Private Sub Gred_markah()

        For i As Integer = 0 To datRespondent.Rows.Count - 1
            'assign value to integer
            Dim strGredNama As String = ""
            Dim strpointerNama As String = ""
            Dim strGredMarkah As String = ""
            Dim strMP As String = "BAHASA MELAYU"

            Dim strPelajarID As String = datRespondent.DataKeys(i).Value.ToString
            Dim PB As String
            Dim PA As String
            Dim PBMarkah As Integer
            Dim PAMarkah As Integer
            Dim Pointer As Integer

            strGredNama = "BM"
            strpointerNama = "BahasaMelayu"

            Dim txtMarkahBMUlang As Label = CType(datRespondent.Rows(i).FindControl("MarkahBMUlang"), Label)

            strSQL = "SELECT MarkahPB FROM kpmkv_pelajar_ulang WHERE PelajarID = '" & strPelajarID & "' AND NamaMataPelajaran = 'BAHASA MELAYU'"
            Dim strPB As String = oCommon.getFieldValue(strSQL)

            strSQL = "SELECT MarkahPA FROM kpmkv_pelajar_ulang WHERE PelajarID = '" & strPelajarID & "' AND NamaMataPelajaran = 'BAHASA MELAYU'"
            Dim strPA As String = oCommon.getFieldValue(strSQL)

            Dim PB1 As Integer = Integer.Parse(strPB)
            Dim PA2 As Integer = Integer.Parse(strPA)

            'get gred from kpmkv_pelajar_markah
            If Not String.IsNullOrEmpty(strGredNama) Then
                strSQL = "SELECT  Gred" & strGredNama & "  FROM kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
                strGredMarkah = oCommon.getFieldValue(strSQL)
            End If

            strSQL = "SELECT PB FROM kpmkv_aka_weightage WHERE Tahun='" & ddlBMTahun.Text & "'"
            PB = oCommon.getFieldValue(strSQL)

            strSQL = "SELECT PA FROM kpmkv_aka_weightage WHERE Tahun='" & ddlBMTahun.Text & "'"
            PA = oCommon.getFieldValue(strSQL)

            If Not String.IsNullOrEmpty(PB1) Then
                PBMarkah = oCommon.DoConvertN((PB1 / 100) * CInt(PB), 2)
            End If
            If Not String.IsNullOrEmpty(PA2) Then
                PAMarkah = oCommon.DoConvertN((PA2 / 100) * CInt(PA), 2)
            End If

            Pointer = PBMarkah + PAMarkah

            strSQL = "UPDATE kpmkv_pelajar_ulang SET MarkahBMUlang = '" & Pointer & "' WHERE PelajarID = '" & strPelajarID & "' AND NamaMataPelajaran = 'BAHASA MELAYU'"
            strRet = oCommon.ExecuteSQL(strSQL)

            'untuk tidak hadir sahaja
            If PB1 = -1 Then
                strSQL = "UPDATE kpmkv_pelajar_ulang SET Gred='T' Where PelajarUlangID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='4'"
                strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & ddlKodKursus.SelectedValue & "' AND NamaMataPelajaran='" & strMP & "'"
                strRet = oCommon.ExecuteSQL(strSQL)

                strSQL = "UPDATE kpmkv_pelajar_markah SET Gred" & strGredNama & "='" & strGredMarkah & "' Where PelajarID='" & strPelajarID & "'"
                strRet = oCommon.ExecuteSQL(strSQL)

            ElseIf PA2 = -1 Then
                strSQL = "UPDATE kpmkv_pelajar_ulang SET Gred='T' Where PelajarUlangID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='4'"
                strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & ddlKodKursus.SelectedValue & "' AND NamaMataPelajaran='" & strMP & "'"
                strRet = oCommon.ExecuteSQL(strSQL)

                strSQL = "UPDATE kpmkv_pelajar_markah SET Gred" & strGredNama & "='" & strGredMarkah & "' Where PelajarID='" & strPelajarID & "'"
                strRet = oCommon.ExecuteSQL(strSQL)

            Else
                strSQL = "SELECT TOP ( 1 ) Gred FROM  kpmkv_gred WHERE '" & Pointer & "' BETWEEN MarkahFrom AND MarkahTo AND Jenis='AKADEMIK_ULANG'"
                Dim Gred As String = oCommon.getFieldValue(strSQL)

                If Not String.IsNullOrEmpty(Pointer) Then
                    strSQL = "UPDATE kpmkv_pelajar_ulang SET Gred='" & Gred & "' Where PelajarID = '" & datRespondent.DataKeys(i).Value.ToString & "'"
                    strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='4'"
                    strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & ddlKodKursus.SelectedValue & "' AND NamaMataPelajaran='" & strMP & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)
                End If
            End If
        Next

    End Sub
    Private Sub Pointer_markah()

        For i As Integer = 0 To datRespondent.Rows.Count - 1

            Dim strPelajarID As String = datRespondent.DataKeys(i).Value.ToString

            '------BM SETARA ULANG 09112016
            Dim BerterusanBM As Integer
            Dim AkhiranBM As Integer
            Dim PB4 As Integer
            Dim PABmSetara As Integer
            Dim PAPB4 As Integer

            Dim B_BahasaMelayuSem1 As Integer
            Dim B_BahasaMelayuSem2 As Integer
            Dim B_BahasaMelayuSem3 As Integer
            Dim B_BahasaMelayuSem4 As Integer
            Dim A_BahasaMelayuSem4 As Integer
            Dim PointerBMUlang As Integer

            strSQL = "SELECT PB FROM kpmkv_aka_weightage WHERE Tahun='" & ddlTahun.Text & "'"
            BerterusanBM = oCommon.getFieldValue(strSQL)

            strSQL = "SELECT PA FROM kpmkv_aka_weightage WHERE Tahun='" & ddlTahun.Text & "'"
            AkhiranBM = oCommon.getFieldValue(strSQL)

            'get mykad
            strSQL = " SELECT Mykad FROM kpmkv_pelajar"
            strSQL += " WHERE PelajarID='" & strPelajarID & "'"
            Dim strMYKAD1 As String = oCommon.getFieldValue(strSQL)

            'get pelajarid
            strSQL = " SELECT PelajarID FROM kpmkv_pelajar"
            strSQL += " WHERE Semester='1'"
            strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & ddlKodKursus.SelectedValue & "'"
            strSQL += " AND Mykad='" & strMYKAD1 & "'"
            Dim strPelajarID1 As String = oCommon.getFieldValue(strSQL)

            'get bm sem 1
            strSQL = "Select BahasaMelayu from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID1 & "'"
            strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='1'"
            strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & ddlKodKursus.SelectedValue & "'"
            B_BahasaMelayuSem1 = oCommon.getFieldValue(strSQL)
            'round up
            B_BahasaMelayuSem1 = Math.Ceiling(B_BahasaMelayuSem1)
            '----------------------------------------------------------------------------

            'get pelajarid
            strSQL = " SELECT PelajarID FROM kpmkv_pelajar"
            strSQL += " WHERE Semester='2'"
            strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & ddlKodKursus.SelectedValue & "'"
            strSQL += " AND Mykad='" & strMYKAD1 & "'"
            Dim strPelajarID2 As String = oCommon.getFieldValue(strSQL)
            'get Bm sem 2
            strSQL = "Select BahasaMelayu from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID2 & "'"
            strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='2'"
            strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & ddlKodKursus.SelectedValue & "'"
            B_BahasaMelayuSem2 = oCommon.getFieldValue(strSQL)
            'round up
            B_BahasaMelayuSem2 = Math.Ceiling(B_BahasaMelayuSem2)

            'get pelajarid
            strSQL = " SELECT PelajarID FROM kpmkv_pelajar"
            strSQL += " WHERE Semester='3'"
            strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & ddlKodKursus.SelectedValue & "'"
            strSQL += " AND Mykad='" & strMYKAD1 & "'"
            Dim strPelajarID3 As String = oCommon.getFieldValue(strSQL)
            'get bm sem 3
            strSQL = "Select BahasaMelayu from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID3 & "'"
            strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='3'"
            strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & ddlKodKursus.SelectedValue & "'"
            B_BahasaMelayuSem3 = oCommon.getFieldValue(strSQL)
            'round up
            B_BahasaMelayuSem3 = Math.Ceiling(B_BahasaMelayuSem3)

            'get bm sem 4 PB
            strSQL = "Select B_BahasaMelayu from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
            strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='4'"
            strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & ddlKodKursus.SelectedValue & "'"
            B_BahasaMelayuSem4 = oCommon.getFieldValue(strSQL)

            'get bm sem 4 PA
            strSQL = "Select MarkahBMUlang from kpmkv_pelajar_ulang Where PelajarID='" & strPelajarID & "'"
            strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='4' AND NamaMataPelajaran = 'BAHASA MELAYU'"
            strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & ddlKodKursus.SelectedValue & "'"
            A_BahasaMelayuSem4 = oCommon.getFieldValue(strSQL)

            Dim Kertas1 As Integer = 0
            Dim Kertas2 As Integer = 0

            strSQL = "SELECT A_BahasaMelayu1, A_BahasaMelayu2 FROM kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
            strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='4'"
            strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & ddlKodKursus.SelectedValue & "'"
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

            If Not (B_BahasaMelayuSem4) = "-1" Then

                PB4 = Math.Ceiling((B_BahasaMelayuSem4 / 100) * BerterusanBM)
                PABmSetara = Math.Ceiling((A_BahasaMelayuSem4 / 100) * 40)
                PAPB4 = Math.Ceiling(((Kertas1 + Kertas2 + PABmSetara) / 280) * AkhiranBM)
                'PAPB4 = Math.Ceiling(PAPB * AkhiranBM)

                'gred sem 4 
                Dim PointSem4 As Integer = Math.Ceiling(PB4 + PAPB4)
                strSQL = "UPDATE kpmkv_pelajar_markah SET BahasaMelayu='" & PointSem4 & "' Where PelajarID='" & strPelajarID & "'"
                strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='4'"
                strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & ddlKodKursus.SelectedValue & "'"
                strRet = oCommon.ExecuteSQL(strSQL)

                PointerBMUlang = Math.Ceiling((((B_BahasaMelayuSem1 / 100) * 10) + ((B_BahasaMelayuSem2 / 100) * 10) + ((B_BahasaMelayuSem3 / 100) * 10) + ((PointSem4 / 100) * 70)))

                strSQL = "UPDATE kpmkv_pelajar_ulang SET PointerBMUlang = '" & PointerBMUlang & "' WHERE PelajarID = '" & strPelajarID & "' AND NamaMataPelajaran = 'BAHASA MELAYU'"
                strRet = oCommon.ExecuteSQL(strSQL)

                strSQL = "SELECT TOP ( 1 ) Gred FROM  kpmkv_gred_bmsetara WHERE '" & PointerBMUlang & "' BETWEEN MarkahFrom AND MarkahTo AND Jenis='BMSETARA' AND Tahun = '" & ddlTahun.SelectedValue & "'"
                Dim GredUlang As String = oCommon.getFieldValue(strSQL)

                strSQL = "UPDATE kpmkv_pelajar_ulang SET GredPointerBMUlang = '" & GredUlang & "' WHERE PelajarID = '" & strPelajarID & "' AND NamaMataPelajaran = 'BAHASA MELAYU'"
                strRet = oCommon.ExecuteSQL(strSQL)

            End If

        Next

    End Sub
    Protected Sub ddlJenis_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlJenis.SelectedIndexChanged
        kpmkv_kolej_list()
        kpmkv_kodkursus_list()
        kpmkv_kelas_list()

    End Sub

    Private Sub btnPointer_Click(sender As Object, e As EventArgs) Handles btnPointer.Click
        Pointer_markah()
        BindData(datRespondent)
    End Sub

    Private Sub btnGred_Click(sender As Object, e As EventArgs) Handles btnGred.Click
        Gred_markah()
        BindData(datRespondent)
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click

        For i As Integer = 0 To datRespondent.Rows.Count - 1

            Dim PelajarID As String = datRespondent.DataKeys(i).Value.ToString

            strSQL = "SELECT PointerBMSetara FROM kpmkv_pelajar_markah WHERE PelajarID = '" & PelajarID & "'"
            Dim strPointerLama As String = oCommon.getFieldValue(strSQL)

            strSQL = "SELECT PointerBMUlang FROM kpmkv_pelajar_ulang WHERE PelajarID = '" & PelajarID & "' AND NamaMataPelajaran = 'BAHASA MELAYU'"
            Dim strPointerBaru As String = oCommon.getFieldValue(strSQL)

            If strPointerBaru > strPointerLama Then

                strSQL = "SELECT MarkahBMUlang FROM kpmkv_pelajar_ulang WHERE PelajarID = '" & PelajarID & "' AND NamaMataPelajaran = 'BAHASA MELAYU'"
                Dim markahBaru As String = oCommon.getFieldValue(strSQL)

                strSQL = "SELECT Gred FROM kpmkv_pelajar_ulang WHERE PelajarID = '" & PelajarID & "' AND NamaMataPelajaran = 'BAHASA MELAYU'"
                Dim gredBaru As String = oCommon.getFieldValue(strSQL)

                strSQL = "SELECT GredPointerBMUlang FROM kpmkv_pelajar_ulang WHERE PelajarID = '" & PelajarID & "' AND NamaMataPelajaran = 'BAHASA MELAYU'"
                Dim gredSetaraBaru As String = oCommon.getFieldValue(strSQL)

                strSQL = "  UPDATE kpmkv_pelajar_markah 
                            SET
                            BahasaMelayu = '" & markahBaru & "',
                            GredBM = '" & gredBaru & "',
                            PointerBMSetara = '" & strPointerBaru & "',
                            GredBMSetara = '" & gredSetaraBaru & "'"
                strRet = oCommon.ExecuteSQL(strSQL)

                lblMsg.Text = "Berjaya Kemaskini Markah Baru"

            End If



        Next

    End Sub

    Private Sub ddlNegeri_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlNegeri.SelectedIndexChanged

        kpmkv_kolej_list()
        kpmkv_kodkursus_list()
        kpmkv_kelas_list()

    End Sub

    Private Sub ddlKolej_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlKolej.SelectedIndexChanged

        kpmkv_kodkursus_list()
        kpmkv_kelas_list()

    End Sub

    Private Sub btnJanaKohortLama_Click(sender As Object, e As EventArgs) Handles btnJanaKohortLama.Click

        strSQL = "SELECT DISTINCT  A.MYKAD, B.PusatPeperiksaanID, A.PelajarID FROM kpmkv_svmu A
LEFT JOIN kpmkv_svmu_calon B ON A.svmu_id = B.svmu_id
WHERE 
A.Tahun = '" & ddlTahunSem.SelectedValue & "'
AND A.DatabaseName = 'KPMKV'
AND B.MataPelajaran = 'SJ'
AND B.Status = 'DISAHKAN'"

        Dim sqlMYKAD As New SqlDataAdapter(strSQL, objConn)
        Dim dsMYKAD As DataSet = New DataSet
        sqlMYKAD.Fill(dsMYKAD, "AnyTable")

        For i As Integer = 0 To dsMYKAD.Tables(0).Rows.Count - 1

            Dim BerterusanBM As Integer
            Dim AkhiranBM1 As Integer
            Dim AkhiranBM2 As Integer
            Dim PointerBMSetara As Integer


            strSQL = "SELECT Berterusan FROM kpmkv_wajaran_a WHERE Subjek = 'BM' AND Kohort = '" & ddlTahun.SelectedValue & "' AND TahunPeperiksaan = '" & ddltahunSem.SelectedValue & "' AND Semester = '4'"
            BerterusanBM = oCommon.getFieldValue(strSQL)

            strSQL = "SELECT Akhir1 FROM kpmkv_wajaran_a WHERE Subjek = 'BM' AND Kohort = '" & ddlTahun.SelectedValue & "' AND TahunPeperiksaan = '" & ddltahunSem.SelectedValue & "' AND Semester = '4'"
            AkhiranBM1 = oCommon.getFieldValue(strSQL)

            strSQL = "SELECT Akhir2 FROM kpmkv_wajaran_a WHERE Subjek = 'BM' AND Kohort = '" & ddlTahun.SelectedValue & "' AND TahunPeperiksaan = '" & ddltahunSem.SelectedValue & "' AND Semester = '4'"
            AkhiranBM2 = oCommon.getFieldValue(strSQL)

            Dim MYKAD As String = dsMYKAD.Tables(0).Rows(i).Item(0).ToString
            Dim RecordID As String = dsMYKAD.Tables(0).Rows(i).Item(1).ToString

            strSQL = "SELECT PelajarID FROM kpmkv_pelajar WHERE MYKAD = '" & MYKAD & "' AND Semester = '1'"
            Dim PelajarID_sem1 As String = oCommon2.getFieldValue(strSQL)

            strSQL = "SELECT PelajarID FROM kpmkv_pelajar WHERE MYKAD = '" & MYKAD & "' AND Semester = '2'"
            Dim PelajarID_sem2 As String = oCommon2.getFieldValue(strSQL)

            strSQL = "SELECT PelajarID FROM kpmkv_pelajar WHERE MYKAD = '" & MYKAD & "' AND Semester = '3'"
            Dim PelajarID_sem3 As String = oCommon2.getFieldValue(strSQL)

            strSQL = "SELECT PelajarID FROM kpmkv_pelajar WHERE MYKAD = '" & MYKAD & "' AND Semester = '4'"
            Dim PelajarID_sem4 As String = oCommon2.getFieldValue(strSQL)

            '' BM sem1
            strSQL = "Select BahasaMelayu from kpmkv_pelajar_markah Where PelajarID='" & PelajarID_sem1 & "'"
            Dim BahasaMelayuSem1 As String = oCommon2.getFieldValue(strSQL)

            '' BM sem2
            strSQL = "Select BahasaMelayu from kpmkv_pelajar_markah Where PelajarID='" & PelajarID_sem2 & "'"
            Dim BahasaMelayuSem2 As String = oCommon2.getFieldValue(strSQL)

            '' BM sem3
            strSQL = "Select BahasaMelayu from kpmkv_pelajar_markah Where PelajarID='" & PelajarID_sem3 & "'"
            Dim BahasaMelayuSem3 As String = oCommon2.getFieldValue(strSQL)

            '' B_BM sem4
            strSQL = "Select B_BahasaMelayu from kpmkv_pelajar_markah Where PelajarID='" & PelajarID_sem4 & "'"
            Dim B_BahasaMelayuSem4 As String = oCommon2.getFieldValue(strSQL)

            strSQL = "SELECT KolejRecordID FROM kpmkv_pelajar WHERE PelajarID = '" & PelajarID_sem4 & "'"


            '' A_BM sem4
            Dim BM1 As Integer
            Dim BM2 As Integer
            Dim BM3 As Integer
            Dim BM4 As Integer

            Dim checkBM1 As Integer = -1
            Dim checkBM2 As Integer = -1
            Dim checkBM3 As Integer = -1
            Dim checkBM4 As Integer = -1

            strSQL = "  SELECT BM1 FROM kpmkv_pelajar_markah_import_bm1 WHERE MYKAD = '" & MYKAD & "'"
            strRet = oCommon.getFieldValue(strSQL)

            If Not strRet = "" Then
                BM1 = oCommon.getFieldValue(strSQL)
                checkBM1 = 0
            Else
                BM1 = 0
            End If


            strSQL = "  SELECT BM2 FROM kpmkv_pelajar_markah_import_bm2 WHERE MYKAD = '" & MYKAD & "'"
            strRet = oCommon.getFieldValue(strSQL)

            If Not strRet = "" Then
                BM2 = oCommon.getFieldValue(strSQL)
                checkBM2 = 0
            Else
                BM2 = 0
            End If

            strSQL = "  SELECT BM3_Total FROM kpmkv_pentaksir_bmsetara_calon WHERE MYKAD = '" & MYKAD & "' AND Tahun = '" & ddlBMTahun.SelectedValue & "'"
            strRet = oCommon.getFieldValue(strSQL)

            If Not strRet = "" Then
                BM3 = oCommon.getFieldValue(strSQL)
                checkBM3 = 0
            Else
                BM3 = 0
            End If

            strSQL = "  SELECT BM4_Total FROM kpmkv_pentaksir_bmsetara_calon WHERE MYKAD = '" & MYKAD & "' AND Tahun = '" & ddlBMTahun.SelectedValue & "'"
            strRet = oCommon.getFieldValue(strSQL)

            If Not strRet = "" Then
                BM4 = oCommon.getFieldValue(strSQL)
                checkBM4 = 0
            Else
                BM4 = 0
            End If

            strSQL = "SELECT Tahun FROM kpmkv_pelajar WHERE PelajarID = '" & PelajarID_sem4 & "'"
            Dim Tahun As String = oCommon2.getFieldValue(strSQL)

            strSQL = "SELECT Sesi FROM kpmkv_pelajar WHERE PelajarID = '" & PelajarID_sem4 & "'"
            Dim Sesi As String = oCommon2.getFieldValue(strSQL)

            strSQL = "SELECT KursusID FROM kpmkv_pelajar WHERE PelajarID = '" & PelajarID_sem4 & "'"
            Dim KursusID As String = oCommon2.getFieldValue(strSQL)

            strSQL = "SELECT KodKursus FROM kpmkv_kursus WHERE KursusID = '" & KursusID & "'"
            Dim KodKursus As String = oCommon2.getFieldValue(strSQL)

            If checkBM1 = -1 Or checkBM2 = -1 Or checkBM3 = -1 Or checkBM4 = -1 Then

                strSQL = "SELECT PelajarID FROM kpmkv_markah_bmsj_setara WHERE PelajarID = '" & PelajarID_sem4 & "'"
                Dim checkID As String = oCommon.getFieldValue(strSQL)

                If checkID = "" Then
                    strSQL = "  INSERT INTO kpmkv_markah_bmsj_setara 
                               (PelajarID, KolejRecordID, Tahun, Sesi, Kodkursus, MataPelajaran, IsCalon, IsAKATahun, IsAKASesi, IsAKADated, Markah, Gred, Catatan1)
                                VALUES
                               ('" & PelajarID_sem4 & "', '" & RecordID & "', '" & Tahun & "', '" & Sesi & "', '" & KodKursus & "', 'BAHASA MELAYU', '1', '" & ddlBMTahun.SelectedValue & "', '1',  GETDATE(), '-1', 'T', 'KPMKV')"
                Else
                    strSQL = " UPDATE kpmkv_markah_bmsj_setara SET Markah = '-1', Gred = 'T', isAKATahun = '" & ddlBMTahun.SelectedValue & "', Catatan1 = 'KPMKV' WHERE PelajarID = '" & PelajarID_sem4 & "'"
                End If

                strRet = oCommon.ExecuteSQL(strSQL)

            Else

                Dim PB4 As Integer
                Dim PAPB4 As Integer

                PB4 = Math.Ceiling((B_BahasaMelayuSem4 / 100) * BerterusanBM)

                PAPB4 = Math.Ceiling(((BM1 + BM2 + BM3 + BM4) / 300) * AkhiranBM1)

                Dim PointSem4 As Integer = Math.Ceiling(PB4 + PAPB4)
                'strSQL = "UPDATE kpmkv_pelajar_markah SET BahasaMelayu='" & PointSem4 & "' Where PelajarID='" & strPelajarID & "'"
                'strRet = oCommon.ExecuteSQL(strSQL)

                strSQL = "SELECT TOP ( 1 ) Gred FROM  kpmkv_gred WHERE '" & Integer.Parse(PointSem4) & "' BETWEEN MarkahFrom AND MarkahTo AND Jenis='AKADEMIK'"
                Dim GredBM As String = oCommon.getFieldValue(strSQL)


                'If (B_BahasaMelayuSem1 = "-1" Or B_BahasaMelayuSem2 = "-1" Or B_BahasaMelayuSem3 = "-1") Then
                '    PointerBMSetara = "-1"
                'Else
                PointerBMSetara = Math.Ceiling(((BahasaMelayuSem1 / 100) * 10) + ((BahasaMelayuSem2 / 100) * 10) + ((BahasaMelayuSem3 / 100) * 10)) + Math.Ceiling((PointSem4 / 100) * 70)
                'End If

                'strSQL = "UPDATE kpmkv_pelajar_markah SET PointerBMSetara='" & PointerBMSetara & "' Where PelajarID='" & strPelajarID & "'"
                'strRet = oCommon.ExecuteSQL(strSQL)
                strSQL = "SELECT TOP ( 1 ) Status FROM kpmkv_gred WHERE '" & BM3 + BM4 & "' BETWEEN MarkahFrom AND MarkahTo AND Jenis='BMLISAN'"
                Dim GredBMLisan As String = oCommon.getFieldValue(strSQL)

                strSQL = "UPDATE kpmkv_pelajar_markah SET MarkahBMLisan='" & BM3 + BM4 & "', GredBMLisan = '" & GredBMLisan & "' Where PelajarID='" & PelajarID_sem4 & "'"
                strRet = oCommon2.ExecuteSQL(strSQL)

                strSQL = "SELECT PelajarID FROM kpmkv_markah_bmsj_setara WHERE PelajarID = '" & PelajarID_sem4 & "'"
                Dim checkID As String = oCommon.getFieldValue(strSQL)

                strSQL = "SELECT TOP ( 1 ) Gred FROM  kpmkv_gred_bmsetara WHERE '" & Integer.Parse(PointerBMSetara) & "' BETWEEN MarkahFrom AND MarkahTo AND Tahun='" & ddlBMTahun.SelectedValue & "'"
                Dim kpmkv_gred_bmsetara As String = oCommon.getFieldValue(strSQL)



                If checkID = "" Then

                    strSQL = "  INSERT INTO kpmkv_markah_bmsj_setara 
                               (PelajarID, KolejRecordID, Tahun, Sesi, Kodkursus, MataPelajaran, IsCalon, IsAKATahun, IsAKASesi, IsAKADated, Markah, Gred, Catatan1)
                                VALUES
                               ('" & PelajarID_sem4 & "', '" & RecordID & "', '" & Tahun & "', '" & Sesi & "', '" & KodKursus & "', 'BAHASA MELAYU', '1', '" & ddlBMTahun.SelectedValue & "', '1',  GETDATE(), '" & PointerBMSetara & "', '" & kpmkv_gred_bmsetara & "', 'KPMKV')"
                Else
                    strSQL = " UPDATE kpmkv_markah_bmsj_setara SET Markah = '" & PointerBMSetara & "', Gred = '" & kpmkv_gred_bmsetara & "', isAKATahun = '" & ddlBMTahun.SelectedValue & "' WHERE PelajarID = '" & PelajarID_sem4 & "'"

                End If
                strRet = oCommon.ExecuteSQL(strSQL)

                strSQL = "SELECT PointerBMSetara FROM kpmkv_pelajar_markah WHERE PelajarID = '" & PelajarID_sem4 & "'"
                Dim strPointerLama As String = oCommon2.getFieldValue(strSQL)

                If PointerBMSetara > strPointerLama Then
                    strSQL = "  UPDATE kpmkv_pelajar_markah 
                            SET
                            BahasaMelayu = '" & PointSem4 & "',
                            GredBM = '" & GredBM & "',
                            PointerBMSetara = '" & PointerBMSetara & "',
                            GredBMSetara = '" & kpmkv_gred_bmsetara & "'
                            WHERE PelajarID = '" & PelajarID_sem4 & "'"

                    strRet = oCommon2.ExecuteSQL(strSQL)
                End If

            End If


        Next


    End Sub

End Class
