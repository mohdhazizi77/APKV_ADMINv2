Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Public Class takwim_update

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

                strSQL = "SELECT TakwimKVID FROM kpmkv_takwim_kv WHERE TakwimID = '" & Request.QueryString("TakwimID") & "'"
                Dim countTakwimKVID As Integer = oCommon.getCount(strSQL)

                If countTakwimKVID = 0 Then

                    chkSelectAll.Checked = True

                    ddlNegeri.Text = "0"
                    ddlNegeri.Enabled = False
                    ddlJenis.Text = "0"
                    ddlJenis.Enabled = False
                    chkBLKolej.Enabled = False

                Else

                    chkSelectAll.Checked = False

                    ddlNegeri.Text = "0"
                    ddlNegeri.Enabled = True
                    ddlJenis.Text = "0"
                    ddlJenis.Enabled = True
                    chkBLKolej.Enabled = True

                End If



                menu_list()

                kpmkv_tahun_list()

                kpmkv_semester_list()

                strSQL = "SELECT TarikhMula FROM kpmkv_takwim WHERE TakwimID = '" & Request.QueryString("TakwimID") & "'"
                txtDate.Text = oCommon.getFieldValue(strSQL)

                strSQL = "SELECT TarikhAkhir FROM kpmkv_takwim WHERE TakwimID = '" & Request.QueryString("TakwimID") & "'"
                txtDateTo.Text = oCommon.getFieldValue(strSQL)

                strSQL = "SELECT HeaderCode FROM kpmkv_takwim WHERE TakwimID = '" & Request.QueryString("TakwimID") & "'"
                Dim HeaderText As String = oCommon.getFieldValue(strSQL)

                strSQL = "SELECT HeaderCode FROM tbl_menuheader_kolej WHERE HeaderText = '" & HeaderText & "'"
                Dim HeaderCode As String = oCommon.getFieldValue(strSQL)

                ddlMenu.SelectedValue = HeaderCode

                submenu_list()

                strSQL = "SELECT SubMenuText FROM kpmkv_takwim WHERE TakwimID = '" & Request.QueryString("TakwimID") & "'"
                Dim SubMenuText As String = oCommon.getFieldValue(strSQL)

                ddlSubMenu.SelectedValue = SubMenuText

                strSQL = "SELECT Kohort FROM kpmkv_takwim WHERE TakwimID = '" & Request.QueryString("TakwimID") & "'"
                Dim Kohort As String = oCommon.getFieldValue(strSQL)

                ddlTahun.Text = Kohort

                strSQL = "SELECT Sesi FROM kpmkv_takwim WHERE TakwimID = '" & Request.QueryString("TakwimID") & "'"
                Dim Sesi As String = oCommon.getFieldValue(strSQL)

                chkSesi.Text = Sesi

                strSQL = "SELECT Semester FROM kpmkv_takwim WHERE TakwimID = '" & Request.QueryString("TakwimID") & "'"
                Dim Semester As String = oCommon.getFieldValue(strSQL)

                ddlSemester.Text = Semester

                strSQL = "SELECT Title FROM kpmkv_takwim WHERE TakwimID = '" & Request.QueryString("TakwimID") & "'"
                Dim Title As String = oCommon.getFieldValue(strSQL)

                txtTitle.Text = Title

                strSQL = "SELECT Catatan FROM kpmkv_takwim WHERE TakwimID = '" & Request.QueryString("TakwimID") & "'"
                Dim Catatan As String = oCommon.getFieldValue(strSQL)

                txtCatatan.Text = Catatan


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

            If Not ddlNegeri.Text = "0" And Not ddlJenis.Text = "0" Then

                For i = 0 To chkBLKolej.Items.Count - 1

                    strSQL = "  SELECT kpmkv_takwim_kv.KolejRecordID FROM kpmkv_takwim_kv
                                LEFT JOIN kpmkv_kolej ON kpmkv_kolej.RecordID = kpmkv_takwim_kv.KolejRecordID
                                WHERE kpmkv_kolej.Negeri = '" & ddlNegeri.SelectedItem.Value & "'
                                AND kpmkv_kolej.Jenis = '" & ddlJenis.SelectedValue & "'
                                AND kpmkv_takwim_kv.TakwimID = '" & Request.QueryString("TakwimID") & "'
                                AND KolejRecordID LIKE '%" & chkBLKolej.Items(i).Value & "%'"

                    If oCommon.isExist(strSQL) = True Then

                        chkBLKolej.Items(i).Selected = True

                    Else

                        chkBLKolej.Items(i).Selected = False

                    End If

                Next

            End If

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

    Protected Sub lnkList_Click(sender As Object, e As EventArgs) Handles lnkList.Click
        Response.Redirect("admin.takwim.list.aspx")
    End Sub

    Private Sub ddlMenu_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlMenu.SelectedIndexChanged
        submenu_list()
        ddlSubMenu.Text = "0"
    End Sub

    Private Sub btnKemaskini_Click(sender As Object, e As EventArgs) Handles btnKemaskini.Click

        Try
            '-insert into course list
            strSQL = "  UPDATE kpmkv_takwim SET
                        Tahun = '" & Now.Year & "',
                        Kohort = '" & ddlTahun.Text & "',
                        Sesi = '" & chkSesi.Text & "',
                        Semester = '" & ddlSemester.Text & "',
                        HeaderCode = '" & ddlMenu.SelectedItem.Text & "',
                        SubMenuText = '" & ddlSubMenu.Text & "',
                        TarikhMula = '" & Request.Form(txtDate.UniqueID) & "',
                        TarikhAkhir = '" & Request.Form(txtDateTo.UniqueID) & "',
                        Title = '" & txtTitle.Text & "',
                        Catatan = '" & txtCatatan.Text & "',
                        Aktif = '1'
                        WHERE TakwimID = '" & Request.QueryString("TakwimID") & "'"
            strRet = oCommon.ExecuteSQL(strSQL)
            If strRet = "0" Then

                If chkSelectAll.Checked = False Then



                    For i = 0 To chkBLKolej.Items.Count - 1

                        If chkBLKolej.Items(i).Selected = True Then

                            strSQL = "SELECT TakwimKVID FROM kpmkv_takwim_kv WHERE TakwimID = '" & Request.QueryString("TakwimID") & "' AND KolejRecordID = '" & chkBLKolej.Items(i).Value & "'"
                            If Not oCommon.isExist(strSQL) = True Then
                                strSQL = "INSERT INTO kpmkv_takwim_kv (TakwimID, KolejRecordID) "
                                strSQL += " VALUES ('" & Request.QueryString("TakwimID") & "', '" & chkBLKolej.Items(i).Value & "')"
                                strRet = oCommon.ExecuteSQL(strSQL)
                            End If

                        Else

                            strSQL = "SELECT TakwimKVID FROM kpmkv_takwim_kv WHERE TakwimID = '" & Request.QueryString("TakwimID") & "' AND KolejRecordID = '" & chkBLKolej.Items(i).Value & "'"
                            Dim TakwimKVID As String = oCommon.getFieldValue(strSQL)

                            If oCommon.isExist(strSQL) = True Then
                                strSQL = "DELETE FROM kpmkv_takwim_kv WHERE TakwimKVID = '" & TakwimKVID & "'"
                                strRet = oCommon.ExecuteSQL(strSQL)
                            End If

                        End If

                    Next

                Else

                    strSQL = "DELETE FROM kpmkv_takwim_kv WHERE TakwimID = '" & Request.QueryString("TakwimID") & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)

                End If

                lblMsg.Text = "Kemaskini berjaya!"



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
            ddlNegeri.Text = "0"
            ddlJenis.Enabled = False
            ddlJenis.Text = "0"
            kpmkv_kolej_list()
            chkBLKolej.Enabled = False

        Else

            ddlNegeri.Enabled = True
            ddlNegeri.Text = "0"
            ddlJenis.Enabled = True
            ddlJenis.Text = "0"
            kpmkv_kolej_list()
            chkBLKolej.Enabled = True

        End If
    End Sub

End Class