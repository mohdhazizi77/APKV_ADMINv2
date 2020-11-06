Imports System.Data.SqlClient
Public Class matapelajaran_vokasional_create
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

                chkSesi.SelectedValue = 1

                kpmkv_kursus_list()



                lblMsg.Text = ""


            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message
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
        strSQL += " WHERE Tahun='" & ddlTahun.Text & "' and Sesi='" & chkSesi.Text & "' AND IsDeleted='N' ORDER BY KodKursus"
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

    Protected Sub btnCreate_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCreate.Click
        lblMsg.Text = ""

        Try
            '--validate
            If ValidatePage() = False Then
                divMsg.Attributes("class") = "error"
                Exit Sub
            End If

            '--execute
            If kpmkv_matapelajaran_create() = True Then
                divMsg.Attributes("class") = "info"
                lblMsg.Text = "Berjaya mendaftarkan MataPelajaran Vokasional baru"
            Else
                divMsg.Attributes("class") = "error"
                lblMsg.Text = "Tidak Berjaya mendaftarkan MataPelajaran Vokasional baru"
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
        If chkSesi.SelectedValue = "" Then
            lblMsg.Text = "Sila Pilih Sesi!"
            chkSesi.Focus()
            Return False
        End If

        '--txtKod
        If txtKodMpVok.Text.Length = 0 Then
            lblMsg.Text = "Sila masukkan Kod MataPelajaran Vokasional!"
            txtKodMpVok.Focus()
            Return False
        End If

        '--txtNama
        If txtNamaMpVok.Text.Length = 0 Then
            lblMsg.Text = "Sila masukkan Nama MataPelajaran Vokasional!"
            txtNamaMpVok.Focus()
            Return False
        End If



        strSQL = "SELECT * FROM kpmkv_matapelajaran_v WHERE KodMpVok='" & txtKodMpVok.Text & "' "
        strSQL += " and NamaMpVok='" & txtNamaMpVok.Text & "' and IsDeleted='N' and Tahun='" & ddlTahun.Text & "'"
        If oCommon.isExist(strSQL) = True Then
            lblMsg.Text = "Kod MataPelajaran Vokasional telah digunakan. Sila masukkan kod yang baru."
            Return False
        Else
            Return True
        End If

        Return True
    End Function

    Private Function kpmkv_matapelajaran_create() As Boolean

        strSQL = "INSERT INTO kpmkv_matapelajaran_v (Tahun,KursusID,kodMPVOK,NamaMPVOK,Semester,Sesi,IsDeleted) "
        strSQL += " VALUES ('" & ddlTahun.SelectedValue & "',"
        strSQL += " '" & oCommon.FixSingleQuotes(ddlKursus.SelectedValue) & "',"
        strSQL += " '" & oCommon.FixSingleQuotes(txtKodMpVok.Text.ToUpper) & "',"
        strSQL += " '" & oCommon.FixSingleQuotes(txtNamaMpVok.Text.ToUpper) & "',"
        strSQL += " '" & ddlSemester.SelectedValue & "','" & chkSesi.Text & "','N')"
        strRet = oCommon.ExecuteSQL(strSQL)
        If strRet = "0" Then
            Return True
        Else
            Return False

        End If

    End Function
    Private Sub chkSesi_SelectedIndexChanged(sender As Object, e As EventArgs) Handles chkSesi.SelectedIndexChanged
        kpmkv_kursus_list()
    End Sub

    Private Sub ddlSemester_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSemester.SelectedIndexChanged
        chkSesi.SelectedValue = 1
        kpmkv_kursus_list()
    End Sub
End Class