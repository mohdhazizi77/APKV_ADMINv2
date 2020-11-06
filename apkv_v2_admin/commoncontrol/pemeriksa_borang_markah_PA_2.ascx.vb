Imports System.Data.SqlClient

Public Class pemeriksa_borang_markah_PA_2
    Inherits System.Web.UI.UserControl
    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                lblmsg.Text = ""
                divMsgTop.Visible = False

                kpmkv_kodpusat_list()
                kpmkv_tahun_list()
                ddlTahun.Text = Now.Year
                kpmkv_semester_list()
                kpmkv_kodkursus_list()
                kpmkv_pemeriksa_list()

                listItem()

            End If
        Catch ex As Exception

            divMsgTop.Visible = True
            divMsgTop.Attributes("class") = "error"
            lblMsgTop.Text = "System Error:" & ex.Message
        End Try

    End Sub

    '-----------DropDownlist------------'

    Private Sub listItem()
        Dim item As ListItem
        For Each item In chkSesi.Items
            If item.Value = "" Then
                item.Attributes.Add("style", "display:none")
            End If
        Next
    End Sub

    Private Sub kpmkv_kodpusat_list()
        strSQL = "SELECT Kod, RecordID FROM kpmkv_kolej ORDER BY Kod ASC "

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlKodPusat.DataSource = ds
            ddlKodPusat.DataTextField = "Kod"
            ddlKodPusat.DataValueField = "RecordID"
            ddlKodPusat.DataBind()

            ddlKodPusat.Items.Insert(0, "-PILIH-")

        Catch ex As Exception
            divMsgTop.Visible = True
            divMsgTop.Attributes("class") = "error"
            lblMsgTop.Text = "System Error:" & ex.Message & "<br>"
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

            '--ALL
            ddlSemester.Items.Add(New ListItem("PILIH", ""))

        Catch ex As Exception
            divMsgTop.Visible = True
            divMsgTop.Attributes("class") = "error"
            lblMsgTop.Text = "System Error:" & ex.Message & "<br>"

        Finally
            objConn.Dispose()
        End Try

    End Sub
    Private Sub kpmkv_kodkursus_list()
        strSQL = "SELECT Distinct kpmkv_kursus.KodKursus, kpmkv_kursus.KursusID"
        strSQL += " FROM kpmkv_kursus INNER JOIN kpmkv_kursus_kolej ON kpmkv_kursus_kolej.KursusID=kpmkv_kursus.KursusID"
        strSQL += " WHERE 
                    kpmkv_kursus.Tahun='" & ddlTahun.Text & "' 
                    AND kpmkv_kursus.Sesi='" & chkSesi.SelectedValue & "' "

        If Not ddlKodPusat.SelectedValue = "-PILIH-" Then
            strSQL += " AND kpmkv_kursus_kolej.KolejRecordID='" & ddlKodPusat.SelectedValue & "' "
        End If

        strSQL += " ORDER BY kpmkv_kursus.KodKursus ASC"
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
            ddlKodKursus.Items.Insert(0, "-PILIH-")

        Catch ex As Exception
            divMsgTop.Visible = True
            divMsgTop.Attributes("class") = "error"
            lblMsgTop.Text = "System Error:" & ex.Message & "<br>"

        Finally
            objConn.Dispose()
        End Try

    End Sub
    Private Sub kpmkv_pemeriksa_list()
        strSQL = "SELECT Nama FROM kpmkv_users WHERE UserType='VOKASIONAL-PEMERIKSA' ORDER BY Nama"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlPemeriksa.DataSource = ds
            ddlPemeriksa.DataTextField = "Nama"
            ddlPemeriksa.DataValueField = "Nama"
            ddlPemeriksa.DataBind()


        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub ddlKodPusat_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlKodPusat.SelectedIndexChanged
        strSQL = "SELECT Nama FROM kpmkv_kolej WHERE RecordID='" & ddlKodPusat.Text & "'"
        lblNamaKolej.Text = oCommon.getFieldValue(strSQL)
        chkSesi.SelectedValue = ""
        listItem()
        ddlKodKursus.SelectedValue = "-PILIH-"
    End Sub
    Private Sub chkSesi_SelectedIndexChanged(sender As Object, e As EventArgs) Handles chkSesi.SelectedIndexChanged
        kpmkv_kodkursus_list()

        listItem()
    End Sub


    '-------Senarai Pemeriksa--------'

    Private Function getSQL() As String


        Dim tmpSQL As String = ""
        Dim strWhere As String = ""
        Dim strOrder As String = " ORDER BY kpmkv_pemeriksa.PemeriksaID, kpmkv_pemeriksa.Tahun, kpmkv_pemeriksa.Sesi ASC"

        '--not deleted
        tmpSQL = "SELECT kpmkv_pemeriksa.PemeriksaID, kpmkv_pemeriksa.Tahun, kpmkv_pemeriksa.Semester, kpmkv_pemeriksa.Sesi, kpmkv_pemeriksa.NamaPemeriksa,"
        tmpSQL += " kpmkv_pemeriksa.KodKolej, kpmkv_pemeriksa.KodKursus FROM kpmkv_pemeriksa"
        strWhere = " WHERE kpmkv_pemeriksa.PemeriksaID IS NOT NULL AND Jenis='VOK'"

        '--tahun
        If Not ddlTahun.Text = "PILIH" Then
            strWhere += " AND kpmkv_pemeriksa.Tahun ='" & ddlTahun.Text & "'"
        End If
        '--sesi
        If Not chkSesi.Text = "" Then
            strWhere += " AND kpmkv_pemeriksa.Sesi ='" & chkSesi.Text & "'"
        End If
        If Not ddlKodKursus.SelectedValue = "-PILIH-" Then

            strSQL = "SELECT KodKursus FROM kpmkv_kursus WHERE KursusID='" & ddlKodKursus.SelectedValue & "'"
            Dim strKodKursus As String = oCommon.getFieldValue(strSQL)

            strWhere += " AND kpmkv_pemeriksa.KodKursus='" & strKodKursus & " '"
        End If
        If Not ddlKodPusat.SelectedValue = "-PILIH-" Then
            strSQL = "SELECT Kod FROM kpmkv_kolej WHERE RecordID='" & ddlKodPusat.SelectedValue & "'"
            Dim strPusat As String = oCommon.getFieldValue(strSQL)

            strWhere += " AND kpmkv_pemeriksa.KodKolej='" & strPusat & " '"

        End If

        getSQL = tmpSQL & strWhere & strOrder


        Return getSQL

    End Function
    Private Function BindData(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 120

        Try
            myDataAdapter.Fill(myDataSet, "myaccount")

            If myDataSet.Tables(0).Rows.Count = 0 Then

                divMsg.Attributes("class") = "error"
                lblmsg.Text = "Jumlah Rekod Pemeriksa yang didaftarkan#: Tiada rekod dijumpai! <br>"

            Else

                divMsg.Attributes("class") = "info"
                lblmsg.Text += "Jumlah Rekod Pemeriksa yang didaftarkan#:" & myDataSet.Tables(0).Rows.Count & "</br>"
            End If

            gvTable.DataSource = myDataSet
            gvTable.DataBind()
            objConn.Close()
        Catch ex As Exception
            divMsgTop.Visible = True
            divMsgTop.Attributes("class") = "error"
            lblMsgTop.Text = "System Error:" & ex.Message & "<br>"
            Return False
        End Try

        Return True

    End Function


    '------Senarai Pusat-------------'
    Private Sub Pusat()

        Dim tmpSQL As String = ""
        Dim strWhere As String = ""
        Dim strOrder As String = " ORDER BY kpmkv_Kolej.Kod,kpmkv_kursus.KodKursus ASC"


        tmpSQL = "SELECT kpmkv_kursus_kolej.TxnKursusKolejID,kpmkv_kursus.KodKursus, "
        tmpSQL += " kpmkv_Kolej.Kod,kpmkv_Kolej.Nama"
        tmpSQL += " FROM kpmkv_kursus_kolej"
        tmpSQL += " LEFT JOIN kpmkv_kursus ON kpmkv_kursus.kursusID=kpmkv_kursus_kolej.KursusID"
        tmpSQL += " LEFT JOIN kpmkv_Kolej ON kpmkv_kolej.RecordID= kpmkv_kursus_kolej.KolejRecordID"
        strWhere = " WHERE kpmkv_kursus_kolej.TxnKursusKolejID Is NOT NULL AND kpmkv_kolej.Kod IS NOT NULL "

        '--tahun
        If Not ddlTahun.Text = "PILIH" Then
            strWhere += " AND kpmkv_kursus.Tahun='" & ddlTahun.SelectedValue & "'"
        End If
        '--sesi
        If Not chkSesi.Text = "" Then
            strWhere += " AND kpmkv_kursus.Sesi='" & chkSesi.SelectedValue & "'"
        End If
        If Not ddlKodKursus.SelectedValue = "-PILIH-" Then
            strWhere += " AND kpmkv_kursus_kolej.KursusID='" & ddlKodKursus.SelectedValue & "'"
        End If

        If Not ddlKodPusat.SelectedValue = "-PILIH-" Then
            strWhere += " AND kpmkv_Kolej.RecordID='" & ddlKodPusat.SelectedValue & "'"
        End If

        Dim getSQLPusat As String
        getSQLPusat = tmpSQL & strWhere & strOrder


        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQLPusat, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 120

        Try
            myDataAdapter.Fill(myDataSet, "myaccount")

            If myDataSet.Tables(0).Rows.Count = 0 Then

                divMsg.Attributes("class") = "error"
                lblmsg.Text = "Jumlah Rekod Kod Pusat#: Tiada rekod dijumpai! <br>"
            Else
                divMsg.Attributes("class") = "info"
                lblmsg.Text += "Jumlah Rekod Kod Pusat #:" & myDataSet.Tables(0).Rows.Count & "<br>"
            End If

            datRespondent2.DataSource = myDataSet
            datRespondent2.DataBind()
            objConn.Close()
        Catch ex As Exception
            divMsgTop.Visible = True
            divMsgTop.Attributes("class") = "error"
            lblMsgTop.Text = "System Error:" & ex.Message & "<br>"

        End Try

    End Sub

    '------Fungsi Carian-------------'

    Private Sub btnCari_Click(sender As Object, e As EventArgs) Handles btnCari.Click
        lblmsg.Text = ""
        lblMsgTop.Text = ""
        divMsgTop.Visible = False

        '--validate--
        If ValidatePage() = False Then
            divMsgTop.Visible = True
            divMsgTop.Attributes("class") = "error"
            Exit Sub
        End If

        listItem()
        strRet = BindData(datRespondent)
        Pusat()

    End Sub

    Private Function ValidatePage() As Boolean


        If chkSesi.Text = "" Then
            lblMsgTop.Text = "Sila Pilih Sesi <br>"
            chkSesi.Focus()
            listItem()

            Return False
        End If



        Return True
    End Function

    '-------Fungsi Button Simpan------------'
    Protected Sub btnSimpan_Click(sender As Object, e As EventArgs) Handles btnSimpan.Click
        lblmsg.Text = ""
        divMsgTop.Visible = False

        '--validate--
        If ValidatePage() = False Then
            divMsgTop.Visible = True
            Exit Sub
        End If

        strSQL = "Select UserID FROM kpmkv_users WHERE Nama='" & oCommon.FixSingleQuotes(ddlPemeriksa.Text) & "'"
        Dim StrUserID As Integer = oCommon.getFieldValue(strSQL)

        strSQL = "SELECT Kod FROM kpmkv_kolej WHERE RecordID='" & ddlKodPusat.SelectedValue & "'"
        Dim strPusat As String = oCommon.getFieldValue(strSQL)

        Try

            strSQL = "SELECT kpmkv_kursus_kolej.TxnKursusKolejID, kpmkv_kursus.KodKursus, "
            strSQL += " kpmkv_Kolej.Kod, kpmkv_Kolej.Nama"
            strSQL += " FROM kpmkv_kursus_kolej"
            strSQL += " LEFT JOIN kpmkv_kursus ON kpmkv_kursus.kursusID=kpmkv_kursus_kolej.KursusID"
            strSQL += " LEFT JOIN kpmkv_Kolej ON kpmkv_kolej.RecordID= kpmkv_kursus_kolej.KolejRecordID"
            strSQL += " WHERE kpmkv_kursus_kolej.TxnKursusKolejID IS NOT NULL"
            strSQL += " AND kpmkv_Kolej.Kod IS NOT NULL"
            strSQL += " AND kpmkv_Kolej.Nama IS NOT NULL"

            '--tahun
            If Not ddlTahun.Text = "PILIH" Then
                strSQL += " AND kpmkv_kursus.Tahun='" & ddlTahun.SelectedValue & "'"
            End If
            '--sesi
            If Not chkSesi.Text = "" Then
                strSQL += " AND kpmkv_kursus.Sesi='" & chkSesi.SelectedValue & "'"
            End If
            If Not ddlKodKursus.SelectedValue = "-PILIH-" Then
                strSQL += " AND kpmkv_kursus_kolej.KursusID='" & ddlKodKursus.SelectedValue & "'"
            End If

            If Not ddlKodPusat.SelectedValue = "-PILIH-" Then
                strSQL += " AND kpmkv_Kolej.RecordID='" & ddlKodPusat.SelectedValue & "'"
            End If

            strSQL += " ORDER BY kpmkv_Kolej.Kod ASC"

            Dim sqlDA As New SqlDataAdapter(strSQL, objConn)
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            '--validate checked checkbox--'
            Dim strcount As Integer = 0
            For j As Integer = 0 To ds.Tables(0).Rows.Count - 1

                Dim k As CheckBox = datRespondent2.Rows(j).FindControl("chkSelect")

                If k.Checked Then
                    strcount = strcount + 1
                End If
            Next

            Dim strTotal As Integer = strcount

            If strTotal = 0 Then
                divMsgTop.Visible = True
                lblMsgTop.Text = "Sila Pilih/Klik pada Kod Pusat"
                listItem()
                Exit Sub
            End If
            '----------------------------------INSERT----------------------------------------'
            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1

                Dim cb As CheckBox = datRespondent2.Rows(i).FindControl("chkSelect")

                If cb.Checked = True Then
                    Dim strKodPusat As String = ds.Tables(0).Rows(i).Item(2).ToString
                    Dim strKodkursus As String = ds.Tables(0).Rows(i).Item(1).ToString

                    strSQL = "INSERT INTO kpmkv_pemeriksa(NamaPemeriksa, UserID, Tahun, Semester, Sesi, 
                              KodKolej, KodKursus,Jenis) "
                    strSQL += " VALUES ('" & oCommon.FixSingleQuotes(ddlPemeriksa.Text) & "','" & StrUserID & "',
                              '" & ddlTahun.Text & "','" & ddlSemester.Text & "','" & chkSesi.Text & "','" & strKodPusat & "',
                              '" & strKodKursus & "','VOK')"

                    strRet = oCommon.ExecuteSQL(strSQL)
                End If
            Next

            If strRet = "0" Then
                divMsgTop.Visible = True
                divMsg.Attributes("class") = "info"
                divMsgTop.Attributes("class") = "info"
                lblmsg.Text += "Status Pendaftaran Pemeriksa : Berjaya <br>"
                lblMsgTop.Text += "Status Pendaftaran Pemeriksa : Berjaya <br>"

                listItem()

            Else
                divMsgTop.Visible = True
                divMsg.Attributes("class") = "error"
                lblmsg.Text += "Status Pendaftaran Pemeriksa : Tidak Berjaya! <br>"
                lblMsgTop.Text += "Status Pendaftaran Pemeriksa : Tidak Berjaya! <br>"
            End If

            strRet = BindData(datRespondent)
        Catch ex As Exception
            divMsgTop.Visible = True
            divMsgTop.Attributes("class") = "error"
            lblMsgTop.Text = "System Error:" & ex.Message & "<br>"
        End Try

    End Sub

    '----------Fungsi Batal------------'
    Protected Sub datRespondent_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles datRespondent.RowCommand
        lblmsg.Text = ""
        divMsgTop.Visible = False
        If (e.CommandName = "Batal") = True Then

            Dim PemeriksaID = Int32.Parse(e.CommandArgument.ToString())

            strSQL = "DELETE FROM kpmkv_pemeriksa WHERE PemeriksaID='" & PemeriksaID & "'"
            strRet = oCommon.ExecuteSQL(strSQL)
            If strRet = "0" Then
                divMsgTop.Visible = True
                divMsg.Attributes("class") = "Info"
                divMsgTop.Attributes("class") = "Info"
                lblmsg.Text = " Status Pembatalan Rekod Pemeriksa : Berjaya <br>"
                lblMsgTop.Text = " Status Pembatalan Rekod Pemeriksa : Berjaya <br>"
            Else
                divMsgTop.Visible = True
                divMsgTop.Attributes("class") = "error"
                divMsg.Attributes("class") = "error"
                lblmsg.Text = "Status Pembatalan Rekod Pemeriksa : Tidak Berjaya! <br>"
                lblMsgTop.Text = "Status Pembatalan Rekod Pemeriksa : Tidak Berjaya! <br>"
            End If

        End If
        listItem()

        strRet = BindData(datRespondent)

    End Sub

    Private Sub datRespondent_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles datRespondent.PageIndexChanging
        datRespondent.PageIndex = e.NewPageIndex
        strRet = BindData(datRespondent)
    End Sub
End Class