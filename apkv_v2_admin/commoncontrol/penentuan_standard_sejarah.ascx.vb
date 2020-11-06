Imports System.Data.SqlClient
Public Class penentuan_standard_sejarah
    Inherits System.Web.UI.UserControl
    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                Session("strTypeid") = ""

                kpmkv_tahun_list()
                ddlTahun.Text = Now.Year


                If Not Request.QueryString("edit") = "" Then
                    Load_page()
                Else
                    lblConfig.Text = Request.QueryString("p")
                End If

                strRet = BindData(datRespondent)
            End If

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message
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


    '--load for edit--'
    Private Sub Load_page()

        strSQL = "SELECT * FROM kpmkv_gred_Sejarah WHERE GSID='" & Request.QueryString("edit") & "'"
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
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Tahun")) Then
                    ddlTahun.SelectedValue = ds.Tables(0).Rows(0).Item("Tahun")
                Else
                    ddlTahun.Text = Now.Year
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Sesi")) Then
                    chkSesi.Text = ds.Tables(0).Rows(0).Item("Sesi")
                Else
                    chkSesi.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("MarkahFrom")) Then
                    txtMarkahFrom.Text = ds.Tables(0).Rows(0).Item("MarkahFrom")
                Else
                    txtMarkahFrom.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("MarkahTo")) Then
                    txtMarkahTo.Text = ds.Tables(0).Rows(0).Item("MarkahTo")
                Else
                    txtMarkahTo.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Gred")) Then
                    txtGred.Text = ds.Tables(0).Rows(0).Item("Gred")
                Else
                    txtGred.Text = ""
                End If


                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Status")) Then
                    txtStatus.Text = ds.Tables(0).Rows(0).Item("Status")
                Else
                    txtStatus.Text = ""
                End If

                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Kompetensi")) Then
                    txtKompetensi.Text = ds.Tables(0).Rows(0).Item("Kompetensi")
                Else
                    txtKompetensi.Text = ""
                End If
                If Not IsDBNull(ds.Tables(0).Rows(0).Item("Jenis")) Then
                    txtJenis.Text = ds.Tables(0).Rows(0).Item("Jenis")
                Else
                    txtJenis.Text = ""
                End If

            End If
        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub


    '--list--'
    Private Function getSQL() As String
        Dim tmpSQL As String
        Dim strWhere As String = ""
        Dim strOrder As String = " ORDER BY Tahun,Sesi ASC"

        tmpSQL = "SELECT * FROM kpmkv_gred_Sejarah"
        strWhere = " WHERE GSID IS NOT NULL"
        strWhere += " AND Tahun='" & ddlTahun.Text & "' "

        If Not chkSesi.Text = "" Then
            strWhere += " AND Sesi='" & chkSesi.Text & "' "
        End If



        getSQL = tmpSQL & strWhere & strOrder

        Return getSQL

    End Function

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

    Private Function BindData(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 120

        Try
            myDataAdapter.Fill(myDataSet, "myaccount")

            If myDataSet.Tables(0).Rows.Count = 0 Then
                divMsg.Attributes("class") = "error"
                lblMsg.Text = "No Record Found!"
            Else
                divMsg.Attributes("class") = "info"
                lblMsg.Text = "Total Records#:" & myDataSet.Tables(0).Rows.Count
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

    '-----add function-----'
    Private Function Save() As Boolean
        If Not Request.QueryString("edit") = "" Then

            strSQL = "UPDATE kpmkv_gred_Sejarah SET Tahun='" & oCommon.FixSingleQuotes(ddlTahun.SelectedValue) & "',"
            strSQL += " Sesi ='" & oCommon.FixSingleQuotes(chkSesi.Text) & "',"
            strSQL += " MarkahFrom ='" & oCommon.FixSingleQuotes(txtMarkahFrom.Text) & "',"
            strSQL += " MarkahTo ='" & oCommon.FixSingleQuotes(txtMarkahTo.Text) & "',"
            strSQL += " Gred ='" & oCommon.FixSingleQuotes(txtGred.Text.ToUpper) & "',"
            strSQL += " Status ='" & oCommon.FixSingleQuotes(txtStatus.Text.ToUpper) & "',"
            strSQL += " Kompetensi ='" & oCommon.FixSingleQuotes(txtKompetensi.Text.ToUpper) & "' "
            strSQL += " WHERE GSID='" & Request.QueryString("edit") & "'"

        Else
            strSQL = "INSERT kpmkv_gred_Sejarah(Tahun,Sesi,MarkahFrom,MarkahTo,Gred,Status,Kompetensi,Jenis) "
            strSQL += " VALUES ('" & oCommon.FixSingleQuotes(ddlTahun.SelectedValue) & "',"
            strSQL += " '" & oCommon.FixSingleQuotes(chkSesi.Text) & "',"
            strSQL += " '" & oCommon.FixSingleQuotes(txtMarkahFrom.Text) & "',"
            strSQL += " '" & oCommon.FixSingleQuotes(txtMarkahTo.Text) & "',"
            strSQL += " '" & oCommon.FixSingleQuotes(txtGred.Text.ToUpper) & "',"
            strSQL += " '" & oCommon.FixSingleQuotes(txtStatus.Text.ToUpper) & "',"
            strSQL += " '" & oCommon.FixSingleQuotes(txtKompetensi.Text.ToUpper) & "','SEJARAH')"


        End If

        strRet = oCommon.ExecuteSQL(strSQL)
        '--Debug
        'Response.Write(strSQL)

        If strRet = "0" Then
            Return True
        Else
            lblMsg.Text = "System Error:" & strRet

            Return False
        End If
    End Function

    Private Sub SaveFunction_ServerClick(sender As Object, e As EventArgs) Handles SaveFunction.ServerClick


        lblMsg.Text = ""

        Try

            '--execute
            If Save() = True Then
                divMsg.Attributes("class") = "info"
                lblMsg.Text = "Rekod Berjaya Disimpan"
            Else
                divMsg.Attributes("class") = "error"
            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message
        End Try

        strRet = BindData(datRespondent)
    End Sub


    Private Sub Refresh_ServerClick(sender As Object, e As EventArgs) Handles Refresh.ServerClick
        Response.Redirect("penentuan.standard.sejarah.aspx?p=" + lblConfig.Text)
    End Sub


    Private Sub datRespondent_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles datRespondent.RowDeleting
        Dim strTid = datRespondent.DataKeys(e.RowIndex).Values("GSID").ToString

        'chk session to prevent postback
        If Not strTid = Session("strTypeid") Then
            strSQL = "DELETE FROM kpmkv_gred_Sejarah WHERE GSID='" & oCommon.FixSingleQuotes(strTid) & "'"
            strRet = oCommon.ExecuteSQL(strSQL)

            Session("strDid") = ""
        End If
        strRet = BindData(datRespondent)
    End Sub
    Private Sub ddlTahun_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTahun.SelectedIndexChanged
        strRet = BindData(datRespondent)
    End Sub

    Private Sub chkSesi_SelectedIndexChanged(sender As Object, e As EventArgs) Handles chkSesi.SelectedIndexChanged
        strRet = BindData(datRespondent)
    End Sub
End Class