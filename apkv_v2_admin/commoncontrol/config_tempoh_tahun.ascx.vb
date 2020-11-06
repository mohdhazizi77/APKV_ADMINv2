Imports System.Data.SqlClient
Public Class config_tempoh_tahun
    Inherits System.Web.UI.UserControl
    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                LoadPage()
                strRet = BindData(datRespondent)
            End If

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message
        End Try
    End Sub

    Private Sub LoadPage()
        strSQL = "SELECT YearFrom,YearEnd FROM kpmkv_tempoh_tahun "
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
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("YearFrom")) Then
                    txtYearFrom.Text = ds.Tables(0).Rows(0).Item("YearFrom")
                Else
                    txtYearFrom.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("YearEnd")) Then
                    txtYearEnd.Text = ds.Tables(0).Rows(0).Item("YearEnd")
                Else
                    txtYearEnd.Text = ""
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
            If kpmkv_tempohtahun_update() = True Then
                divMsg.Attributes("class") = "info"
                lblMsg.Text = "Berjaya mengemaskini maklumat konfigurasi tempoh tahun"
            Else
                divMsg.Attributes("class") = "error"
            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message
        End Try
        strRet = BindData(datRespondent)

    End Sub
    Private Function ValidatePage() As Boolean

        '--txtTahun
        If txtYearFrom.Text.Length < 4 Then
            lblMsg.Text = "Tahun Tidak Sah!"
            txtYearFrom.Focus()
            Return False
        End If
        If txtYearEnd.Text.Length < 4 Then
            lblMsg.Text = "Tahun Tidak Sah!"
            txtYearEnd.Focus()
            Return False
        End If

        If txtYearEnd.Text < txtYearFrom.Text Then
            lblMsg.Text = "Tahun tidak sah!"
            txtYearEnd.Focus()
            Return False
        End If

        Return True
    End Function
    Private Function kpmkv_tempohtahun_update() As Boolean

        strSQL = "SELECT * FROM kpmkv_tempoh_tahun"
        strRet = oCommon.isExist(strSQL)

        If strRet = True Then

            strSQL = "UPDATE kpmkv_tempoh_tahun SET YearFrom ='" & txtYearFrom.Text & "', YearEnd='" & txtYearEnd.Text & "'"
        Else
            strSQL = "INSERT kpmkv_tempoh_tahun(YearFrom,YearEnd) VALUES ('" & oCommon.FixSingleQuotes(txtYearFrom.Text) & "','" & oCommon.FixSingleQuotes(txtYearFrom.Text) & "')"
        End If

        strRet = oCommon.ExecuteSQL(strSQL)

            If strRet = "0" Then
            Return True
        Else
            lblMsg.Text = "System Error:" & strRet

            Return False
        End If

    End Function

    Private Function BindData(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 120

        Try
            myDataAdapter.Fill(myDataSet, "myaccount")

            If myDataSet.Tables(0).Rows.Count = 0 Then
                divMsg.Attributes("class") = "error"
                lblMsg.Text = "Rekod tidak dijumpai!"
            Else
                divMsg.Attributes("class") = "info"
                lblMsg.Text = "Jumlah Rekod#:" & myDataSet.Tables(0).Rows.Count
            End If

            gvTable.DataSource = myDataSet
            gvTable.DataBind()
            objConn.Close()
        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message
            Return False
        End Try

        Return True

    End Function

    Private Function getSQL() As String
        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrder As String = ""

        tmpSQL = "SELECT ID,YearFrom,YearEnd FROM kpmkv_tempoh_tahun"
        strWhere = " WHERE ID IS NOT NULL"

        getSQL = tmpSQL & strWhere & strOrder

        Return getSQL

    End Function


    Private Sub datRespondent_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles datRespondent.PageIndexChanging
        datRespondent.PageIndex = e.NewPageIndex
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

    Private Sub datRespondent_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles datRespondent.RowDeleting
        Dim strKeyID As String = datRespondent.DataKeys(e.RowIndex).Value
        Try
            strSQL = "DELETE FROM kpmkv_tempoh_tahun WHERE ID='" & strKeyID & "'"
            strRet = oCommon.ExecuteSQL(strSQL)
            If strRet = "0" Then
                divMsg.Attributes("class") = "info"
                lblMsg.Text = "Rekod berjaya dipadam"
            Else
                divMsg.Attributes("class") = "error"
                lblMsg.Text = "Rekod tidak berjaya dipadam"
            End If
            strRet = BindData(datRespondent)

        Catch ex As Exception
            divMsg.Attributes("class") = "error"

        End Try
    End Sub



End Class