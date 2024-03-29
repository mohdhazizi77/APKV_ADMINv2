﻿Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Public Class markah_ulang_vokasional
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
                ddlTahun.Text = Now.Year

                kpmkv_semester_list()

                kpmkv_kodkursus_list()

                kpmkv_kelas_list()

                lblMsg.Text = ""
                'strRet = BindData(datRespondent)

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
            ddlTahun.Items.Add(New ListItem("PILIH", "PILIH"))

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

            '--ALL
            ddlSemester.Items.Add(New ListItem("PILIH", "PILIH"))

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
        strSQL += " WHERE kpmkv_kelas.KolejRecordID='" & ddlKolej.SelectedValue & "' AND kpmkv_kursus.Tahun='" & ddlTahun.Text & "' "
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
        Dim strOrder As String = " ORDER BY kpmkv_pelajar.Tahun, kpmkv_pelajar.Semester, kpmkv_pelajar.Sesi ASC"

        '--not deleted
        tmpSQL = "SELECT kpmkv_pelajar_ulang_vokasional.PelajarUlangVID, kpmkv_pelajar_ulang_vokasional.Tahun, kpmkv_pelajar_ulang_vokasional.Semester, kpmkv_pelajar_ulang_vokasional.Sesi, kpmkv_pelajar_ulang_vokasional.NamaModul, "
        tmpSQL += " kpmkv_pelajar_ulang_vokasional.MarkahPBTeori,kpmkv_pelajar_ulang_vokasional.MarkahPBAmali, kpmkv_pelajar_ulang_vokasional.MarkahPATeori, kpmkv_pelajar_ulang_vokasional.MarkahPAAmali,"
        tmpSQL += " kpmkv_pelajar_ulang_vokasional.Gred,kpmkv_pelajar.Tahun, kpmkv_pelajar.Nama, kpmkv_pelajar.Mykad, kpmkv_pelajar.AngkaGiliran, kpmkv_kursus.KodKursus, kpmkv_kelas.NamaKelas FROM  kpmkv_pelajar_ulang_vokasional LEFT OUTER JOIN"
        tmpSQL += " kpmkv_pelajar ON kpmkv_pelajar_ulang_vokasional.PelajarID = kpmkv_pelajar.PelajarID LEFT OUTER JOIN"
        tmpSQL += " kpmkv_kursus ON kpmkv_pelajar_ulang_vokasional.KursusID = kpmkv_kursus.KursusID LEFT OUTER JOIN"
        tmpSQL += " kpmkv_kelas ON kpmkv_pelajar_ulang_vokasional.KelasID = kpmkv_kelas.KelasID "
        strWhere = " WHERE kpmkv_pelajar_ulang_vokasional.KolejRecordID='" & ddlKolej.SelectedValue & "' AND kpmkv_pelajar.IsDeleted='N' AND kpmkv_pelajar.StatusID='2' "

        '--tahun
        If Not ddlTahun.Text = "PILIH" Then
            strWhere += " AND kpmkv_pelajar_ulang_vokasional.Tahun ='" & ddlTahun.Text & "'"
        End If
        '--semester
        If Not ddlSemester.Text = "PILIH" Then
            strWhere += " AND kpmkv_pelajar_ulang_vokasional.Semester ='" & ddlSemester.Text & "'"
        End If
        '--sesi
        If Not chkSesi.Text = "" Then
            strWhere += " AND kpmkv_pelajar_ulang_vokasional.Sesi ='" & chkSesi.Text & "'"
        End If
        '--kursus
        If Not ddlKodKursus.Text = "" Then
            strWhere += " AND kpmkv_pelajar_ulang_vokasional.KursusID ='" & ddlKodKursus.SelectedValue & "'"
        End If
        '--jantina
        If Not ddlKelas.Text = "" Then
            strWhere += " AND kpmkv_pelajar_ulang_vokasional.KelasID ='" & ddlKelas.SelectedValue & "'"
        End If

        getSQL = tmpSQL & strWhere & strOrder
        ''--debug
        ''Response.Write(getSQL)


        Return getSQL

    End Function
    Private Sub datRespondent_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles datRespondent.RowDeleting
        lblMsg.Text = ""
        lblMsgTop.Text = ""

        Dim strPelajarUID As Integer = datRespondent.DataKeys(e.RowIndex).Values("PelajarUlangVID")
        Try
            If Not strPelajarUID = Session("strPelajarUID") Then
                strSQL = "Select Gred FROM kpmkv_pelajar_ulang_vokasional WHERE PelajarUlangVID='" & strPelajarUID & "'"
                Dim strGred As String = oCommon.getFieldValue(strSQL)

                If strGred = "NULL" Or strGred = "" Then
                    strSQL = "DELETE FROM kpmkv_pelajar_ulang_vokasional WHERE PelajarUlangVID='" & strPelajarUID & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)
                    If strRet = "0" Then
                        divTop.Attributes("class") = "info"
                        lblMsgTop.Text = "Calon berjaya dipadamkan"
                        Session("strPelajarUID") = ""
                    Else
                        divTop.Attributes("class") = "error"
                        lblMsgTop.Text = "Calon tidak berjaya dipadam"
                        Session("strPelajarUID") = ""
                    End If
                Else
                    divTop.Attributes("class") = "error"
                    lblMsgTop.Text = "Calon tidak berjaya dipadam. Gred Ulang telah dijana"
                    Session("strPelajarUID") = ""
                End If
            Else
                Session("strPelajarUID") = ""
            End If
        Catch ex As Exception
            divTop.Attributes("class") = "error"
        End Try

        strRet = BindData(datRespondent)
    End Sub
    Private Sub datRespondent_SelectedIndexChanging(sender As Object, e As GridViewSelectEventArgs) Handles datRespondent.SelectedIndexChanging
        Dim strKeyID As String = datRespondent.DataKeys(e.NewSelectedIndex).Value.ToString
        'Response.Redirect("pelajar.ulang.akademik.view.aspx?PelajarID=" & strKeyID)

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
        lblMsgTop.Text = ""
        strRet = BindData(datRespondent)
        hiddencolumn()
    End Sub

    Private Sub chkSesi_SelectedIndexChanged(sender As Object, e As EventArgs) Handles chkSesi.SelectedIndexChanged
        kpmkv_kodkursus_list()
        kpmkv_kelas_list()
    End Sub

    Private Sub ddlKodKursus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlKodKursus.SelectedIndexChanged
        kpmkv_kelas_list()
    End Sub

    Private Sub hiddencolumn()

        For i As Integer = 0 To datRespondent.Rows.Count - 1
            Dim row As GridViewRow = datRespondent.Rows(i)
            Dim strPBT As TextBox = datRespondent.Rows(i).FindControl("MarkahPBTeori")
            Dim strPBA As TextBox = datRespondent.Rows(i).FindControl("MarkahPBAmali")
            Dim strPAT As TextBox = datRespondent.Rows(i).FindControl("MarkahPATeori")
            Dim strPAA As TextBox = datRespondent.Rows(i).FindControl("MarkahPAAmali")

            strSQL = "SELECT PBTeori FROM kpmkv_pelajar_ulang_vokasional WHERE PelajarUlangVID='" & datRespondent.DataKeys(i).Value.ToString & "'"
            Dim strPBTHide As String = oCommon.getFieldValue(strSQL)

            If strPBTHide = "1" Then
                strPBT.Enabled = True
                strPBT.Text = ""
            Else
                strPBT.Enabled = False
            End If

            strSQL = "SELECT PBAmali FROM kpmkv_pelajar_ulang_vokasional WHERE PelajarUlangVID='" & datRespondent.DataKeys(i).Value.ToString & "'"
            Dim strPBAHide As String = oCommon.getFieldValue(strSQL)

            If strPBAHide = "1" Then
                strPBA.Enabled = True
                strPBA.Text = ""
            Else
                strPBA.Enabled = False
            End If

            strSQL = "SELECT PATeori FROM kpmkv_pelajar_ulang_vokasional WHERE PelajarUlangVID='" & datRespondent.DataKeys(i).Value.ToString & "'"
            Dim strPATHide As String = oCommon.getFieldValue(strSQL)

            If strPATHide = "1" Then
                strPAT.Enabled = True
                strPAT.Text = ""
            Else
                strPAT.Enabled = False
            End If

            strSQL = "SELECT PAAmali FROM kpmkv_pelajar_ulang_vokasional WHERE PelajarUlangVID='" & datRespondent.DataKeys(i).Value.ToString & "'"
            Dim strPAAHide As String = oCommon.getFieldValue(strSQL)

            If strPAAHide = "1" Then
                strPAA.Enabled = True
                strPAA.Text = ""
            Else
                strPAA.Enabled = False
            End If
        Next
    End Sub
    Private Sub hiddencolumnNext()

        For i As Integer = 0 To datRespondent.Rows.Count - 1
            Dim row As GridViewRow = datRespondent.Rows(i)
            Dim strPBT As TextBox = datRespondent.Rows(i).FindControl("MarkahPBTeori")
            Dim strPBA As TextBox = datRespondent.Rows(i).FindControl("MarkahPBAmali")
            Dim strPAT As TextBox = datRespondent.Rows(i).FindControl("MarkahPATeori")
            Dim strPAA As TextBox = datRespondent.Rows(i).FindControl("MarkahPAAmali")

            strSQL = "SELECT PBTeori FROM kpmkv_pelajar_ulang_vokasional WHERE PelajarUlangVID='" & datRespondent.DataKeys(i).Value.ToString & "'"
            Dim strPBTHide As String = oCommon.getFieldValue(strSQL)

            If strPBTHide = "1" Then
                strPBT.Enabled = True
            Else
                strPBT.Enabled = False
            End If

            strSQL = "SELECT PBAmali FROM kpmkv_pelajar_ulang_vokasional WHERE PelajarUlangVID='" & datRespondent.DataKeys(i).Value.ToString & "'"
            Dim strPBAHide As String = oCommon.getFieldValue(strSQL)

            If strPBAHide = "1" Then
                strPBA.Enabled = True
            Else
                strPBA.Enabled = False
            End If

            strSQL = "SELECT PATeori FROM kpmkv_pelajar_ulang_vokasional WHERE PelajarUlangVID='" & datRespondent.DataKeys(i).Value.ToString & "'"
            Dim strPATHide As String = oCommon.getFieldValue(strSQL)

            If strPATHide = "1" Then
                strPAT.Enabled = True
            Else
                strPAT.Enabled = False
            End If

            strSQL = "SELECT PAAmali FROM kpmkv_pelajar_ulang_vokasional WHERE PelajarUlangVID='" & datRespondent.DataKeys(i).Value.ToString & "'"
            Dim strPAAHide As String = oCommon.getFieldValue(strSQL)

            If strPAAHide = "1" Then
                strPAA.Enabled = True
            Else
                strPAA.Enabled = False
            End If
        Next
    End Sub
    Protected Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        lblMsg.Text = ""

        If ValidateForm() = False Then
            lblMsg.Text = "Sila masukkan NOMBOR 0-100 SAHAJA"
            Exit Sub
        End If

        Try
            For i As Integer = 0 To datRespondent.Rows.Count - 1
                Dim row As GridViewRow = datRespondent.Rows(i)
                Dim strPBT As TextBox = datRespondent.Rows(i).FindControl("MarkahPBTeori")
                Dim strPBA As TextBox = datRespondent.Rows(i).FindControl("MarkahPBAmali")
                Dim strPAT As TextBox = datRespondent.Rows(i).FindControl("MarkahPATeori")
                Dim strPAA As TextBox = datRespondent.Rows(i).FindControl("MarkahPAAmali")


                'assign value to integer
                Dim strPBT1 As Integer = strPBT.Text
                Dim strPBA1 As Integer = strPBA.Text
                Dim strPAT1 As Integer = strPAT.Text
                Dim strPAA1 As Integer = strPAA.Text


                strSQL = "UPDATE kpmkv_pelajar_ulang_vokasional SET MarkahPBTeori='" & strPBT1 & "', MarkahPBAmali='" & strPBA1 & "', MarkahPATeori='" & strPAT1 & "', MarkahPAAmali='" & strPAA1 & "' WHERE PelajarUlangVID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                strRet = oCommon.ExecuteSQL(strSQL)
                If strRet = "0" Then
                    divMsg.Attributes("class") = "info"
                    lblMsg.Text = "Berjaya!.Kemaskini markah Ulang Vokasional"
                Else
                    divMsg.Attributes("class") = "error"
                    lblMsg.Text = "Tidak Berjaya!.Kemaskini markah Ulang Vokasional"
                End If
            Next

        Catch ex As Exception
            lblMsg.Text = "Error:" & ex.Message
        End Try
        Vokasional_markah()
        strRet = BindData(datRespondent)
        hiddencolumnNext()
    End Sub
    Private Function ValidateForm() As Boolean
        For i As Integer = 0 To datRespondent.Rows.Count - 1
            Dim row As GridViewRow = datRespondent.Rows(i)
            Dim strPBT As TextBox = CType(row.FindControl("MarkahPBTeori"), TextBox)
            Dim strPBA As TextBox = CType(row.FindControl("MarkahPBAmali"), TextBox)

            Dim strPAT As TextBox = CType(row.FindControl("MarkahPATeori"), TextBox)
            Dim strPAA As TextBox = CType(row.FindControl("MarkahPAAmali"), TextBox)
            '--validate NUMBER and less than 100
            '--strBahasaMelayu

            If Not strPBT.Text.Length = 0 Then
                If oCommon.IsCurrency(strPBT.Text) = False Then
                    Return False
                End If
                If CInt(strPBT.Text) > 100 Then
                    Return False
                End If
            Else
                strPBT.Text = "0"
            End If

            '--strBahasaMelayu1
            If Not strPBA.Text.Length = 0 Then
                If oCommon.IsCurrency(strPBA.Text) = False Then
                    Return False
                End If
                If CInt(strPBA.Text) > 100 Then
                    Return False
                End If
            Else
                strPBA.Text = "0"
            End If

            If Not strPAT.Text.Length = 0 Then
                If oCommon.IsCurrency(strPAT.Text) = False Then
                    Return False
                End If
                If CInt(strPAT.Text) > 100 Then
                    Return False
                End If
            Else
                strPAT.Text = "0"
            End If

            '--strBahasaMelayu1
            If Not strPAA.Text.Length = 0 Then
                If oCommon.IsCurrency(strPAA.Text) = False Then
                    Return False
                End If
                If CInt(strPAA.Text) > 100 Then
                    Return False
                End If
            Else
                strPAA.Text = "0"
            End If

        Next

        Return True
    End Function

    Private Sub Vokasional_markah()


        For i As Integer = 0 To datRespondent.Rows.Count - 1
            Dim row As GridViewRow = datRespondent.Rows(i)
            Dim strPBT As TextBox = CType(row.FindControl("MarkahPBTeori"), TextBox)
            Dim strPBA As TextBox = CType(row.FindControl("MarkahPBAmali"), TextBox)

            Dim strPAT As TextBox = CType(row.FindControl("MarkahPATeori"), TextBox)
            Dim strPAA As TextBox = CType(row.FindControl("MarkahPAAmali"), TextBox)

            Dim strNamaModul As Label = CType(row.FindControl("NamaModul"), Label)
            'assign value to integer
            Dim PB1 As Integer = strPBT.Text
            Dim PB2 As Integer = strPBA.Text

            Dim PA1 As Integer = strPAT.Text
            Dim PA2 As Integer = strPAA.Text
            Dim strModul As String = strNamaModul.Text.Substring(6, 1)

            Dim strKodModul As String = strModul
            Dim strNamaModulKV As String = strNamaModul.Text.Substring(8)

            Dim strGredMarkah As String = ""
            Dim strPelajarID As Integer
            Dim PBAmali As String
            Dim PBTeori As String
            Dim PAAmali As String
            Dim PATeori As String

            Dim PATotal As Integer
            Dim PATotal1 As Integer
            Dim PBTotal As Integer
            Dim PBTotal1 As Integer
            Dim Pointer As Integer
            ' ''get kodmodul
            'strSQL = "SELECT NamaModul FROM kpmkv_modul WHERE NamaModul='" & strModul & "' AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "' AND Sesi='" & chkSesi.Text & "'"
            'strKodModul = oCommon.getFieldValue(strSQL)

            'get pelajarid
            strSQL = "SELECT PelajarID FROM kpmkv_pelajar_ulang_vokasional Where PelajarUlangVID='" & datRespondent.DataKeys(i).Value.ToString & "'"
            strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
            strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & ddlKodKursus.SelectedValue & "' AND NamaModul='" & strNamaModul.Text & "'"
            strPelajarID = oCommon.getFieldValue(strSQL)

            'get gred from kpmkv_pelajar_markah
            If Not String.IsNullOrEmpty(strKodModul) Then
                strSQL = "SELECT  GredV" & strKodModul & "  FROM kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
                strGredMarkah = oCommon.getFieldValue(strSQL)
            End If

            strSQL = "SELECT PBAmali FROM kpmkv_modul WHERE NamaModul='" & strNamaModulKV & "' AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "' AND Sesi='" & chkSesi.Text & "'"
            PBAmali = oCommon.getFieldValue(strSQL)

            strSQL = "SELECT PBTeori FROM kpmkv_modul WHERE NamaModul='" & strNamaModulKV & "' AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "' AND Sesi='" & chkSesi.Text & "'"
            PBTeori = oCommon.getFieldValue(strSQL)

            strSQL = "SELECT PAAmali FROM kpmkv_modul WHERE NamaModul='" & strNamaModulKV & "' AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "' AND Sesi='" & chkSesi.Text & "'"
            PAAmali = oCommon.getFieldValue(strSQL)

            strSQL = "SELECT PATeori FROM kpmkv_modul WHERE NamaModul='" & strNamaModulKV & "' AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "' AND Sesi='" & chkSesi.Text & "'"
            PATeori = oCommon.getFieldValue(strSQL)

            'change on 16082016
            'convert 0 if null
            If (String.IsNullOrEmpty(PBAmali.ToString())) Then
                PBAmali = 0
            End If

            If (String.IsNullOrEmpty(PBTeori.ToString())) Then
                PBTeori = 0
            End If

            If (String.IsNullOrEmpty(PAAmali.ToString())) Then
                PAAmali = 0
            End If

            If (String.IsNullOrEmpty(PATeori.ToString())) Then
                PATeori = 0
            End If

            If PB1 = "-1" Or PB2 = "-1" Or PA1 = "-1" Or PA2 = "-1" Then
                strSQL = "UPDATE kpmkv_pelajar_ulang_vokasional SET Gred='T' Where PelajarUlangVID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & ddlKodKursus.SelectedValue & "' AND NamaModul='" & strNamaModul.Text & "'"
                strRet = oCommon.ExecuteSQL(strSQL)

                strSQL = "UPDATE kpmkv_pelajar_markah SET GredV" & strKodModul & "='" & strGredMarkah & "' Where PelajarID='" & strPelajarID & "'"
                strRet = oCommon.ExecuteSQL(strSQL)
            Else
                PBTotal = Math.Ceiling((PB2 / 100) * PBAmali)
                PBTotal1 = Math.Ceiling((PB1 / 100) * PBTeori)
                PATotal = Math.Ceiling((PA2 / 100) * PAAmali)
                PATotal1 = Math.Ceiling((PA1 / 100) * PATeori)


                Pointer = CInt(PBTotal) + CInt(PBTotal1) + CInt(PATotal) + CInt(PATotal1)

                strSQL = "SELECT TOP ( 1 ) Gred FROM  kpmkv_gred WHERE '" & Pointer & "' BETWEEN MarkahFrom AND MarkahTo AND Jenis='VOKASIONAL_ULANG'"
                Dim Gred As String = oCommon.getFieldValue(strSQL)

                strSQL = "UPDATE kpmkv_pelajar_ulang_vokasional SET Gred='" & Gred & "' Where PelajarUlangVID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & ddlKodKursus.SelectedValue & "' AND NamaModul='" & strNamaModul.Text & "'"
                strRet = oCommon.ExecuteSQL(strSQL)

                If Gred = strGredMarkah Then
                    strSQL = "UPDATE kpmkv_pelajar_markah SET GredV" & strKodModul & "='" & strGredMarkah & "' Where PelajarID='" & strPelajarID & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)
                ElseIf Gred = "B-" Then
                    strSQL = "UPDATE kpmkv_pelajar_markah SET GredV" & strKodModul & "='B-' Where PelajarID='" & strPelajarID & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)
                Else
                    strSQL = "UPDATE kpmkv_pelajar_markah SET GredV" & strKodModul & "='" & strGredMarkah & "' Where PelajarID='" & strPelajarID & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)
                End If
            End If


        Next
    End Sub
    Protected Sub ddlJenis_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlJenis.SelectedIndexChanged
        kpmkv_kolej_list()
    End Sub
End Class
