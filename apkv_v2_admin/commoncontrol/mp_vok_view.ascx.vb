Imports System.Data.SqlClient
Public Class mp_vok_view1
    Inherits System.Web.UI.UserControl
    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                kpmkv_tahun_list()
                ddlTahun.Text = Now.Year

                kpmkv_semester_list()


                LoadPage()

                kpmkv_kursus_list()
            End If

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message
        End Try
    End Sub

    Private Sub kpmkv_tahun_list()
        strSQL = "SELECT Tahun FROM kpmkv_tahun  ORDER BY TahunID"
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
        strSQL = "SELECT Semester FROM kpmkv_semester ORDER BY SemesterID"
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

    Private Sub kpmkv_kursus_list()

        strSQL = "SELECT KursusID,(KodKursus + ' - ' + NamaKursus) AS Kursus FROM kpmkv_kursus "
        strSQL += " WHERE Tahun='" & ddlTahun.SelectedValue & "' and Sesi='" & ddlSesi.SelectedValue & "' AND IsDeleted='N' ORDER BY KodKursus"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)
        ' Response.Write(strSQL)
        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlKursus.DataSource = ds
            ddlKursus.DataTextField = "Kursus"
            ddlKursus.DataValueField = "KursusID"
            ddlKursus.DataBind()

        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub LoadPage()
        strSQL = "SELECT * FROM kpmkv_matapelajaran_v WHERE MPVID='" & Request.QueryString("MataPelajaranID") & "'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            Dim nRows As Integer = 0
            Dim nCount As Integer = 1
            Dim MyTable As DataTable = New DataTable
            MyTable = ds.Tables(0)
            If MyTable.Rows.Count > 0 Then


                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Tahun")) Then
                    ddlTahun.SelectedValue = ds.Tables(0).Rows(0).Item("Tahun")
                Else
                    ddlTahun.SelectedValue = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Semester")) Then
                    ddlSemester.SelectedValue = ds.Tables(0).Rows(0).Item("Semester")
                Else
                    ddlSemester.SelectedValue = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Sesi")) Then
                    ddlSesi.SelectedValue = ds.Tables(0).Rows(0).Item("Sesi")
                Else
                    ddlSesi.SelectedValue = ""
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Sesi")) Then
                    ddlSesi.SelectedValue = ds.Tables(0).Rows(0).Item("Sesi")
                Else
                    ddlSesi.SelectedValue = ""
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("KursusID")) Then
                    ddlKursus.SelectedValue = ds.Tables(0).Rows(0).Item("KursusID")
                Else
                    ddlKursus.SelectedValue = ""
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("KodMPVok")) Then
                    txtKodMpVok.Text = ds.Tables(0).Rows(0).Item("KodMpVok")
                Else
                    txtKodMpVok.Text = ""
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("NamaMPVok")) Then
                    txtNamaMpVok.Text = ds.Tables(0).Rows(0).Item("NamaMpVok")
                Else
                    txtNamaMpVok.Text = ""
                End If


            End If

        Catch ex As Exception
            divMsg.Attributes("class") = "error"
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub btnUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        lblMsg.Text = ""

        Try
            '--validate
            If ValidatePage() = False Then
                divMsg.Attributes("class") = "error"
                Exit Sub
            End If

            '--execute
            If kpmkv_mp_update() = True Then
                divMsg.Attributes("class") = "info"
                lblMsg.Text = "Berjaya mengemaskini maklumat Matapelajaran."
            Else
                divMsg.Attributes("class") = "error"
            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message
        End Try

    End Sub

    Private Function ValidatePage() As Boolean


        If ddlTahun.SelectedValue = "" Then
            lblMsg.Text = "Sila Pilih Kohort!"
            ddlTahun.Focus()
            Return False
        End If
        If ddlSemester.SelectedValue = "" Then
            lblMsg.Text = "Sila Pilih Semester!"
            ddlSemester.Focus()
            Return False
        End If
        If ddlSesi.SelectedValue = "" Then
            lblMsg.Text = "Sila Pilih Sesi!"
            ddlSesi.Focus()
            Return False
        End If

        '--txtNama
        If txtNamaMpVok.Text.Length = 0 Then
            lblMsg.Text = "Sila masukkan Nama MataPelajaran Vokasional!"
            txtNamaMpVok.Focus()
            Return False
        End If

        Return True
    End Function

    Private Function kpmkv_mp_update() As Boolean
        strSQL = "UPDATE kpmkv_matapelajaran_v WITH (UPDLOCK) "
        strSQL += " SET NamaMPVok='" & oCommon.FixSingleQuotes(txtNamaMpVok.Text.ToUpper) & "',"
        strSQL += " Tahun ='" & ddlTahun.SelectedValue & "' ,"
        strSQL += " Semester ='" & ddlSemester.SelectedValue & "' ,"
        strSQL += " Sesi ='" & ddlSesi.SelectedValue & "' "
        strSQL += " WHERE MPVID='" & Request.QueryString("MataPelajaranID") & "'"
        strRet = oCommon.ExecuteSQL(strSQL)
        If strRet = "0" Then
            Return True
        Else
            lblMsg.Text = "System Error:" & strRet
            Return False
        End If

    End Function

    Private Sub ddlSesi_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSesi.SelectedIndexChanged
        kpmkv_kursus_list()
    End Sub
    Private Sub ddlSemester_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSemester.SelectedIndexChanged
        kpmkv_kursus_list()
    End Sub
End Class