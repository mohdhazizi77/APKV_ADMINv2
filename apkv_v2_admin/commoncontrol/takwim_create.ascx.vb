Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Public Class takwim_create
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                lblMsg.Text = ""
                '--refresh
                kpmkv_negeri_list()
                ddlNegeri.Text = "0"
                kpmkv_jenis_list()
                ddlJenis.Text = "0"
                kpmkv_kolej_list()

                menu_list()

                submenu_list()
                ddlSubMenu.Text = "0"
                kpmkv_tahun_list()
                ddlTahun.Text = Now.Year

                kpmkv_tahun_notifikasi_list()
                ddlTahunNotifikasi.Text = Now.Year

                kpmkv_semester_list()

                txtDate.Text = Format(CDate(Date.Now), "dd-MM-yyyy")
                txtDateTo.Text = Format(CDate(Date.Now), "dd-MM-yyyy")

                txtTarikhMulaNotifikasi.Text = Format(CDate(Date.Now), "dd-MM-yyyy")
                txtTarikhAkhirNotifikasi.Text = Format(CDate(Date.Now), "dd-MM-yyyy")

            End If

        Catch ex As Exception
            lblMsg.Text = "System error:" & ex.Message

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

            chkBLKolej.DataSource = ds
            chkBLKolej.DataTextField = "Nama"
            chkBLKolej.DataValueField = "RecordID"
            chkBLKolej.DataBind()

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub
    Private Sub menu_list()

        strSQL = "SELECT * FROM tbl_menuheader_kolej ORDER BY HeaderCode"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try

            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            Dim nCount As Integer = 1
            Dim MyTable As DataTable = New DataTable
            MyTable = ds.Tables(0)

            ddlMenu.DataSource = ds
            ddlMenu.DataTextField = "HeaderText"
            ddlMenu.DataValueField = "HeaderCode"
            ddlMenu.DataBind()

            ''--add blank row
            ddlMenu.Items.Insert(0, New ListItem("-PILIH-", "0"))


        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
        Finally
            objConn.Dispose()
        End Try

    End Sub
    Private Sub submenu_list()

        strSQL = "Select SubMenuText FROM tbl_submenu_kolej WHERE HeaderCode='" & ddlMenu.SelectedValue & "' ORDER BY HeaderCode"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try

            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            Dim nCount As Integer = 1
            Dim MyTable As DataTable = New DataTable
            MyTable = ds.Tables(0)

            ddlSubMenu.DataSource = ds
            ddlSubMenu.DataTextField = "SubMenuText"
            ddlSubMenu.DataValueField = "SubMenuText"
            ddlSubMenu.DataBind()

            ''--add blank row
            ddlSubMenu.Items.Insert(0, New ListItem("-PILIH-", "0"))


        Catch ex As Exception
            lblMsg.Text = "Database error!" & ex.Message
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

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub kpmkv_tahun_notifikasi_list()
        strSQL = "SELECT Tahun FROM kpmkv_tahun  ORDER BY Tahun"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlTahunNotifikasi.DataSource = ds
            ddlTahunNotifikasi.DataTextField = "Tahun"
            ddlTahunNotifikasi.DataValueField = "Tahun"
            ddlTahunNotifikasi.DataBind()

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

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try

    End Sub
    Private Sub ClearScreen()
        lblMsg.Text = ""

        txtTitle.Text = ""
        txtCatatan.Text = ""

    End Sub

    Protected Sub btnadd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnadd.Click
        Try
            '-insert into course list
            strSQL = "INSERT INTO kpmkv_takwim(Tahun,Kohort,Sesi,Semester,HeaderCode,SubMenuText,TarikhMula,TarikhAkhir,Title,Catatan,Aktif) "
            strSQL += " VALUES ('" & Now.Year & "','" & ddlTahun.Text & "','" & chkSesi.Text & "','" & ddlSemester.Text & "','" & ddlMenu.SelectedItem.Text & "','" & ddlSubMenu.Text & "','" & Request.Form(txtDate.UniqueID) & "','" & Request.Form(txtDateTo.UniqueID) & "','" & txtTitle.Text & "','" & txtCatatan.Text & "','1')"
            strRet = oCommon.ExecuteSQL(strSQL)

            If strRet = "0" Then

                lblMsg.Text = "Penambahan berjaya!"

                strSQL = "SELECT MAX(TakwimID) FROM kpmkv_takwim" ' SELECT LATEST INSERTED TAKWIMID - 11 NOV 2021
                Dim TakwimID As String = oCommon.getFieldValue(strSQL)

                If chkSelectAll.Checked = False Then

                    For i = 0 To chkBLKolej.Items.Count - 1

                        If chkBLKolej.Items(i).Selected = True Then

                            strSQL = "INSERT INTO kpmkv_takwim_kv (TakwimID, KolejRecordID) "
                            strSQL += " VALUES ('" & TakwimID & "', '" & chkBLKolej.Items(i).Value & "')"
                            strRet = oCommon.ExecuteSQL(strSQL)

                        End If

                    Next

                End If

            Else

                lblMsg.Text = "Tidak berjaya. " & strRet

            End If



        Catch ex As Exception
            lblMsg.Text = "System error:" & ex.Message
        End Try

    End Sub

    Protected Sub lnkList_Click(sender As Object, e As EventArgs) Handles lnkList.Click
        Response.Redirect("admin.takwim.list.aspx?tahun=" & ddlTahun.Text)
    End Sub

    Private Sub ddlMenu_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlMenu.SelectedIndexChanged
        submenu_list()
        ddlSubMenu.Text = "0"
    End Sub

    Private Sub btnTambahNotifikasi_Click(sender As Object, e As EventArgs) Handles btnTambahNotifikasi.Click

        Try
            '-insert into course list
            strSQL = "INSERT INTO kpmkv_takwim(Tahun,SubMenuText,TarikhMula,TarikhAkhir,Title,Catatan,Aktif) "
            strSQL += " VALUES ('" & ddlTahun.Text & "', 'Notifikasi Kemasukan Markah','" & Request.Form(txtTarikhMulaNotifikasi.UniqueID) & "','" & Request.Form(txtTarikhAkhirNotifikasi.UniqueID) & "','" & txtTajukNotifikasi.Text & "','" & txtCatatanNotifikasi.Text & "','1')"
            strRet = oCommon.ExecuteSQL(strSQL)
            If strRet = "0" Then
                lblMsg.Text = "Penambahan berjaya!"
            Else
                lblMsg.Text = "Tidak berjaya. " & strRet
            End If

        Catch ex As Exception
            lblMsg.Text = "System error:" & ex.Message
        End Try

    End Sub

    Private Sub ddlNegeri_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlNegeri.SelectedIndexChanged
        kpmkv_kolej_list()
    End Sub

    Private Sub ddlJenis_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlJenis.SelectedIndexChanged
        kpmkv_kolej_list()
    End Sub

    Private Sub chkSelectAll_CheckedChanged(sender As Object, e As EventArgs) Handles chkSelectAll.CheckedChanged
        If chkSelectAll.Checked = True Then

            ddlNegeri.Enabled = False
            ddlJenis.Enabled = False
            chkBLKolej.Enabled = False

        Else

            ddlNegeri.Enabled = True
            ddlJenis.Enabled = True
            chkBLKolej.Enabled = True

        End If
    End Sub
End Class