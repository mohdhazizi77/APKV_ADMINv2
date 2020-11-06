Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization
Public Class user_update
    Inherits System.Web.UI.UserControl
    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        btnDelete.Attributes.Add("onclick", "return confirm('Pasti ingin menghapuskan rekod ini?');")
        Try
            If Not IsPostBack Then

                lblUserID.Text = Request.QueryString("UserID")

                kpmkv_status_list()
                ddlStatus.SelectedValue = ""
                LoadPage()

            End If

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message
        End Try

    End Sub

    Private Sub kpmkv_status_list()
        strSQL = "SELECT Status,StatusID FROM kpmkv_status ORDER BY Status"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlStatus.DataSource = ds
            ddlStatus.DataTextField = "Status"
            ddlStatus.DataValueField = "StatusID"
            ddlStatus.DataBind()

            '--choose
            ddlStatus.Items.Add(New ListItem("-Select-", ""))

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
                    txtNama.Text = ds.Tables(0).Rows(0).Item("Nama")
                Else
                    txtNama.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("MYKAD")) Then
                    txtMYKAD.Text = ds.Tables(0).Rows(0).Item("MYKAD")
                Else
                    txtMYKAD.Text = ""
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Tel")) Then
                    txtTel.Text = ds.Tables(0).Rows(0).Item("Tel")
                Else
                    txtTel.Text = ""
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Email")) Then
                    txtEmail.Text = ds.Tables(0).Rows(0).Item("Email")
                Else
                    txtEmail.Text = ""
                End If


                If Not IsDBNull(ds.Tables(0).Rows(0).Item("StatusID")) Then
                    ddlStatus.SelectedValue = ds.Tables(0).Rows(0).Item("StatusID")

                Else
                    ddlStatus.SelectedValue = ""

                End If


            End If

        Catch ex As Exception
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
            If kpmkv_pensyarah_update() = True Then
                divMsg.Attributes("class") = "info"
                lblMsg.Text = "Berjaya mengemaskini maklumat pengguna."
            Else
                divMsg.Attributes("class") = "error"
            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message
        End Try

    End Sub

    Private Function ValidatePage() As Boolean
        '--txtNama
        If txtNama.Text.Length = 0 Then
            lblMsg.Text = "Sila masukkan Nama!"
            txtNama.Focus()
            Return False
        End If

        '--txtMYKAD
        If txtMYKAD.Text.Length = 0 Then
            lblMsg.Text = "Sila masukkan MYKAD Pensyarah!"
            txtMYKAD.Focus()
            Return False
        ElseIf oCommon.isNumeric(txtMYKAD.Text) = False Then
            lblMsg.Text = "Huruf tidak dibenarkan .Sila masukkan no MYKAD [0###########]"
            txtMYKAD.Focus()
            Return False
        End If

        '--changes made
        If Not txtMYKAD.Text = txtMYKAD.Text Then
            strSQL = "SELECT MYKAD FROM kpmkv_pensyarah WHERE MYKAD='" & oCommon.FixSingleQuotes(txtMYKAD.Text) & "'"
            If oCommon.isExist(strSQL) = True Then
                lblMsg.Text = "MYKAD# telah digunakan. Sila masukkan MYKAD# yang baru."
                txtMYKAD.Focus()
                Return False
            End If
        End If

        '--txtTelefon
        If txtTel.Text.Length = 0 Then
            lblMsg.Text = "Sila masukkan Telefon!"
            txtTel.Focus()
            Return False
        ElseIf oCommon.isNumeric(txtTel.Text) = False Then
            lblMsg.Text = "Huruf tidak dibenarkan .Sila masukkan no telefon [0########]"
            txtTel.Focus()
            Return False
        End If

        '--txtEmail
        If txtEmail.Text.Length = 0 Then
            lblMsg.Text = "Sila masukkan Email Pengguna!"
        ElseIf oCommon.isEmail(txtEmail.Text) = False Then
            lblMsg.Text = "Emel Pengguna tidak sah. (Contoh: Pengguna@contoh.com)"
            txtEmail.Focus()
            Return False
        End If





        Return True
    End Function

    Private Function kpmkv_pensyarah_update() As Boolean
        strSQL = "UPDATE kpmkv_users SET Nama='" & oCommon.FixSingleQuotes(txtNama.Text.ToUpper) & "',"
        strSQL += " MYKAD='" & oCommon.FixSingleQuotes(txtMYKAD.Text) & "',Tel='" & oCommon.FixSingleQuotes(txtTel.Text) & "',"
        strSQL += " LoginID='" & oCommon.FixSingleQuotes(txtMYKAD.Text) & "',pwd='" & oCommon.FixSingleQuotes(txtMYKAD.Text) & "',"
        strSQL += " Email ='" & oCommon.FixSingleQuotes(txtEmail.Text) & "',StatusID='" & oCommon.FixSingleQuotes(ddlStatus.SelectedValue) & "'"
        strSQL += " WHERE UserID='" & oCommon.FixSingleQuotes(lblUserID.Text) & "'"
        strRet = oCommon.ExecuteSQL(strSQL)
        If strRet = "0" Then
            Return True
        Else
            lblMsg.Text = "System Error:" & strRet
            Return False
        End If
        Return True
    End Function

    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        strSQL = "DELETE FROM kpmkv_users WHERE UserID='" & oCommon.FixSingleQuotes(lblUserID.Text) & "'"
        strRet = oCommon.ExecuteSQL(strSQL)
        If strRet = "0" Then
            lblMsg.Text = "Berjaya meghapuskan rekod pengguna tersebut."
        Else
            lblMsg.Text = "System Error:" & strRet
        End If

    End Sub

End Class