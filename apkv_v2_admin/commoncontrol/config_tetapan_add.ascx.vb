Imports System.Data.SqlClient

Public Class config_tetapan_add1

    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try

            If Not IsPostBack Then

                kpmkv_menuUtama_list()
                ddlConfigMenuUtama.SelectedIndex = 0

                kpmkv_menu_list()
                ddlConfigMenu.SelectedIndex = 0

            End If

        Catch ex As Exception

            lblMsg.Text = "System Error:" & ex.Message

        End Try

    End Sub

    Private Sub kpmkv_menuUtama_list()

        strSQL = "SELECT HeaderCode, HeaderText FROM tbl_menuheader ORDER BY Idx"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlConfigMenuUtama.DataSource = ds
            ddlConfigMenuUtama.DataTextField = "HeaderText"
            ddlConfigMenuUtama.DataValueField = "HeaderCode"
            ddlConfigMenuUtama.DataBind()

            ddlConfigMenuUtama.Items.Insert(0, "-PILIH-")

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub kpmkv_menu_list()

        strSQL = "SELECT SubMenuText FROM tbl_submenu WHERE HeaderCode = '" & ddlConfigMenuUtama.SelectedValue & "'"

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlConfigMenu.DataSource = ds
            ddlConfigMenu.DataTextField = "SubMenuText"
            ddlConfigMenu.DataValueField = "SubMenuText"
            ddlConfigMenu.DataBind()

            ddlConfigMenu.Items.Insert(0, "-PILIH-")

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub btnDaftar_Click(sender As Object, e As EventArgs) Handles btnDaftar.Click

        lblMsg.Text = ""

        strSQL = "  INSERT INTO 
                    kpmkv_config_tetapan 
                    (configName, configValue, configMenuUtama, configMenu, configDateTime)
                    VALUES
                    ('" & txtConfigName.Text & "', '" & txtConfigValue.Text & "', '" & ddlConfigMenuUtama.SelectedItem.Text & "', '" & ddlConfigMenu.Text & "', GETDATE())"
        strRet = oCommon.ExecuteSQL(strSQL)

        If strRet = "0" Then

            lblMsg.Text = "Berjaya Mendaftar Baru Tetapan!"

        End If

    End Sub

    Private Sub ddlConfigMenuUtama_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlConfigMenuUtama.SelectedIndexChanged

        kpmkv_menu_list()
        ddlConfigMenu.SelectedIndex = 0

    End Sub


End Class