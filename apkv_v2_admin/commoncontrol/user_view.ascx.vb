Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization
Public Class user_view
    Inherits System.Web.UI.UserControl
    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                lblUserID.Text = Request.QueryString("UserID")
                '-get  ic-'
                strSQL = "SELECT MYKAD FROM kpmkv_users WHERE UserID='" & lblUserID.Text & "'"
                Dim strMykad As String = oCommon.getFieldValue(strSQL)
                '-get kumpulan pengguna pensyrah-'
                strSQL = "SELECT UserType FROM kpmkv_users WHERE LoginID='" & oCommon.FixSingleQuotes(strMykad) & "'"
                Dim strUserType As String = oCommon.getFieldValue(strSQL)


                kpmkv_Kumpulan_Pengguna()
                If Not strUserType = "" Then
                    ddlKumpulan.SelectedValue = strUserType
                Else
                    ddlKumpulan.Text = "0"
                End If



                LoadPage()

            End If

        Catch ex As Exception
            'lblMsg.Text = "System Error:" & ex.Message
        End Try
    End Sub
    Private Sub kpmkv_Kumpulan_Pengguna()
        strSQL = "SELECT UserGroup FROM tbl_ctrl_usergroup ORDER BY UserGroup ASC"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlKumpulan.DataSource = ds
            ddlKumpulan.DataTextField = "UserGroup"
            ddlKumpulan.DataValueField = "UserGroup"
            ddlKumpulan.DataBind()

            '--ALL
            ddlKumpulan.Items.Add(New ListItem("-Pilih", "0"))

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message


        Finally
            objConn.Dispose()
        End Try

    End Sub
    Private Sub LoadPage()
        strSQL = "SELECT kpmkv_users.*,kpmkv_status.Status FROM kpmkv_users "
        strSQL += " LEFT OUTER JOIN kpmkv_status ON kpmkv_users.StatusID=kpmkv_status.StatusID"
        strSQL += " WHERE kpmkv_users.UserID='" & oCommon.FixSingleQuotes(lblUserID.Text) & "'"
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
                '--Account Details 
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Nama")) Then
                    lblNama.Text = ds.Tables(0).Rows(0).Item("Nama")
                Else
                    lblNama.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("MYKAD")) Then
                    lblMYKAD.Text = ds.Tables(0).Rows(0).Item("MYKAD")
                Else
                    lblMYKAD.Text = ""
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Tel")) Then
                    lblTel.Text = ds.Tables(0).Rows(0).Item("Tel")
                Else
                    lblTel.Text = ""
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Email")) Then
                    lblEmail.Text = ds.Tables(0).Rows(0).Item("Email")
                Else
                    lblEmail.Text = ""
                End If


                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Status")) Then
                    lblStatus2.Text = ds.Tables(0).Rows(0).Item("Status")
                Else
                    lblStatus2.Text = ""
                End If


            End If

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub
    Private Sub btnExecute_Click(sender As Object, e As EventArgs) Handles btnExecute.Click
        Response.Redirect("user.update.aspx?UserID=" & lblUserID.Text)
    End Sub
    '------ kemaskini konfigurasi sistem-------------'

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click

        lblMsg.Text = ""

        Try
            '--validate
            If ValidatePage() = False Then
                divMsg.Attributes("class") = "error"
                Exit Sub
            End If

            '--execute
            If kpmkv_pensyarah_update() = True Then
                divMsg.Attributes("class") = "info"
                lblMsg.Text = "Berjaya mengemaskini konfigurasi sistem pengguna"
            Else
                divMsg.Attributes("class") = "error"
            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message
        End Try

    End Sub

    Private Function ValidatePage() As Boolean
        '--ddlkumpulan
        If ddlKumpulan.Text = "0" Then
            lblMsg.Text = "Sila pilih Kumpulan Pengguna!"
            ddlKumpulan.Focus()
            Return False
        End If


        Return True
    End Function

    Private Function kpmkv_pensyarah_update() As Boolean

        '-get pensyarah ic-'
        strSQL = "SELECT MYKAD FROM kpmkv_users WHERE userID='" & oCommon.FixSingleQuotes(lblUserID.Text) & "'"
        Dim strMykad As String = oCommon.getFieldValue(strSQL)

        '-1.Check user existance in tbl user-'
        strSQL = "SELECT LoginID FROM kpmkv_users WHERE LoginID='" & oCommon.FixSingleQuotes(strMykad) & "'"
        Dim strLoginID As String = oCommon.isExist(strSQL)

        '-2.get nama Kolej in tbl kolej-'
        strSQL = "SELECT Nama FROM kpmkv_kolej WHERE RecordID='" & oCommon.FixSingleQuotes(lblKolejID.Text) & "'"
        Dim strNamaKolej As String = oCommon.getFieldValue(strSQL)
        '-3.get negeri Kolej in tbl kolej-'
        strSQL = "SELECT Negeri FROM kpmkv_kolej WHERE RecordID='" & oCommon.FixSingleQuotes(lblKolejID.Text) & "'"
        Dim strNegeriKolej As String = oCommon.getFieldValue(strSQL)



        If strLoginID = True Then
            If txtKatalaluan.Text = "" Then

                strSQL = "UPDATE kpmkv_users SET UserType= '" & oCommon.FixSingleQuotes(ddlKumpulan.SelectedValue) & "'"
                strSQL += " WHERE LoginID='" & oCommon.FixSingleQuotes(strMykad) & "'"
            Else

                strSQL = "UPDATE kpmkv_users SET UserType= '" & oCommon.FixSingleQuotes(ddlKumpulan.SelectedValue) & "',"
                strSQL += " Pwd='" & oCommon.FixSingleQuotes(txtKatalaluan.Text) & "'"
                strSQL += " WHERE LoginID='" & oCommon.FixSingleQuotes(strMykad) & "'"
            End If

        ElseIf strLoginID = False Then

            If txtKatalaluan.Text = "" Then
                '-- create if user not exist in tbl user-'
                strSQL = " INSERT INTO kpmkv_users(RecordID,LoginID,Pwd,UserType,Nama,Negeri)"
                strSQL += " VALUES('" & oCommon.FixSingleQuotes(lblKolejID.Text) & "','" & oCommon.FixSingleQuotes(lblMYKAD.Text) & "','pwd',"
                strSQL += " '" & oCommon.FixSingleQuotes(ddlKumpulan.SelectedValue) & "','" & oCommon.FixSingleQuotes(strNamaKolej) & "',"
                strSQL += " '" & oCommon.FixSingleQuotes(strNegeriKolej) & "')"
            Else

                strSQL = " INSERT INTO kpmkv_users(RecordID,LoginID,Pwd,UserType,Nama,Negeri)"
                strSQL += " VALUES('" & oCommon.FixSingleQuotes(lblKolejID.Text) & "','" & oCommon.FixSingleQuotes(lblMYKAD.Text) & "','" & oCommon.FixSingleQuotes(txtKatalaluan.Text) & "',"
                strSQL += " '" & oCommon.FixSingleQuotes(ddlKumpulan.SelectedValue) & "','" & oCommon.FixSingleQuotes(strNamaKolej) & "',"
                strSQL += " '" & oCommon.FixSingleQuotes(strNegeriKolej) & "')"

            End If


        End If
        'Response.Write(strSQL)
        strRet = oCommon.ExecuteSQL(strSQL)
        If strRet = "0" Then
            Return True
        Else
            lblMsg.Text = "System Error:" & strRet
            Return False
        End If
        Return True
    End Function
End Class