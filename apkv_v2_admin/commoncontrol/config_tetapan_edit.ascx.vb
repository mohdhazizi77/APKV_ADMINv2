Imports System.Data.SqlClient

Public Class config_tetapan_edit
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try

            If Not IsPostBack Then

                strSQL = "SELECT configName FROM kpmkv_config_tetapan WHERE configID = '" & Request.QueryString("id") & "'"
                txtConfigName.Text = oCommon.getFieldValue(strSQL)

                strSQL = "SELECT configValue FROM kpmkv_config_tetapan WHERE configID = '" & Request.QueryString("id") & "'"
                txtConfigValue.Text = oCommon.getFieldValue(strSQL)

                kpmkv_menuUtama_list()
                strSQL = "SELECT configMenuUtama FROM kpmkv_config_tetapan WHERE configID = '" & Request.QueryString("id") & "'"
                ddlConfigMenuUtama.SelectedItem.Text = oCommon.getFieldValue(strSQL)

                kpmkv_menu_list()
                strSQL = "SELECT configMenu FROM kpmkv_config_tetapan WHERE configID = '" & Request.QueryString("id") & "'"
                ddlConfigMenu.SelectedItem.Text = oCommon.getFieldValue(strSQL)

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

    Private Sub btnKemaskini_Click(sender As Object, e As EventArgs) Handles btnKemaskini.Click

        lblMsg.Text = ""

        strSQL = "  UPDATE 
                    kpmkv_config_tetapan
                    SET
                    configName = '" & txtConfigName.Text & "', 
                    configValue = '" & txtConfigValue.Text & "', 
                    configMenuUtama = '" & ddlConfigMenuUtama.SelectedItem.Text & "', 
                    configMenu = '" & ddlConfigMenu.Text & "', 
                    configDateTime = GETDATE()
                    WHERE
                    configID = '" & Request.QueryString("id") & "'"

        strRet = oCommon.ExecuteSQL(strSQL)

        If strRet = "0" Then

            lblMsg.Text = "Berjaya Mengemaskini Tetapan!"

        End If

    End Sub

    Private Sub ddlConfigMenuUtama_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlConfigMenuUtama.SelectedIndexChanged

        kpmkv_menu_list()
        ddlConfigMenu.SelectedIndex = 0

    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click

        strSQL = "DELETE FROM kpmkv_config_tetapan WHERE configID = '" & Request.QueryString("id") & "'"
        strRet = oCommon.ExecuteSQL(strSQL)

        If strRet = "0" Then

            lblMsg.Text = "Tetapan berjaya dipadam"

        Else

            lblMsg.Text = "Tetapan tidak berjaya dipadam"

        End If

    End Sub

End Class