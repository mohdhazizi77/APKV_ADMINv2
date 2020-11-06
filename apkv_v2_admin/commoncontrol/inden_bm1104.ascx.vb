Imports System.Data.SqlClient
Public Class inden_bm1104
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
            '--ALL
            ddlTahun.Items.Add(New ListItem("-Pilih-", ""))
        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try

    End Sub



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
        Dim strGroup As String = ""
        Dim strOrder As String = " ORDER BY kpmkv_kolej.Kod"

        tmpSQL = "  SELECT
			        kpmkv_kolej.Kod, kpmkv_kolej.Negeri, kpmkv_kolej.Nama,
			        kpmkv_pelajar.Tahun, kpmkv_pelajar.Sesi,
			        COUNT(kpmkv_pelajar.PelajarID) AS 'Sem4',
			        COUNT(CASE WHEN kpmkv_pelajar_ulang.IsCalon = '1' THEN 1 ELSE NULL END) AS 'Ulang',
			        COUNT(kpmkv_pelajar.PelajarID) + COUNT(CASE WHEN kpmkv_pelajar_ulang.IsCalon = '1' THEN 1 ELSE NULL END) AS 'JumlahCalon',
			        (COUNT(kpmkv_pelajar.PelajarID) + COUNT(CASE WHEN kpmkv_pelajar_ulang.IsCalon = '1' THEN 1 ELSE NULL END)) + (5-((COUNT(kpmkv_pelajar.PelajarID) + COUNT(CASE WHEN kpmkv_pelajar_ulang.IsCalon = '1' THEN 1 ELSE NULL END)) % 5)) AS 'Near5',
			        (COUNT(kpmkv_pelajar.PelajarID) + COUNT(CASE WHEN kpmkv_pelajar_ulang.IsCalon = '1' THEN 1 ELSE NULL END)) + (5-((COUNT(kpmkv_pelajar.PelajarID) + COUNT(CASE WHEN kpmkv_pelajar_ulang.IsCalon = '1' THEN 1 ELSE NULL END)) % 5)) + 5 AS 'Plus5'
			        FROM 
			        kpmkv_pelajar
			        LEFT JOIN kpmkv_kolej ON kpmkv_kolej.RecordID = kpmkv_pelajar.KolejRecordID
			        LEFT JOIN kpmkv_pelajar_ulang ON kpmkv_pelajar_ulang.PelajarID = kpmkv_pelajar.PelajarID"

        strWhere = "    WHERE
			            kpmkv_pelajar.IsCalon = '1'
			            AND kpmkv_pelajar.Semester ='4'
                        AND kpmkv_pelajar.StatusID = '2'"

        '--tahun
        If Not ddlTahun.Text = "" Then
            strWhere += " AND kpmkv_pelajar.IsBMTahun = '" & ddlTahun.Text & "'"
        End If

        If Not chkSesi.Text = "" Then
            strWhere += " AND kpmkv_pelajar.Sesi ='" & chkSesi.Text & "'"
        End If

        strGroup = "    GROUP BY 
                        kpmkv_kolej.Kod, kpmkv_kolej.Negeri, kpmkv_kolej.Nama,
			            kpmkv_pelajar.Tahun, kpmkv_pelajar.Sesi, kpmkv_pelajar.Semester"

        getSQL = tmpSQL & strWhere & strGroup & strOrder

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

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        lblMsg.Text = ""
        strRet = BindData(datRespondent)
    End Sub

    Private Sub datRespondent_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles datRespondent.PageIndexChanging
        datRespondent.PageIndex = e.NewPageIndex
        strRet = BindData(datRespondent)
    End Sub

    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnExport.Click
        Try
            lblMsg.Text = ""

            ExportToCSV(getSQL)

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message
        End Try

    End Sub

    Private Sub ExportToCSV(ByVal strQuery As String)
        'Get the data from database into datatable 
        Dim cmd As New SqlCommand(strQuery)
        Dim dt As DataTable = GetData(cmd)

        Response.Clear()
        Response.Buffer = True
        Response.AddHeader("content-disposition", "attachment;filename=INDEN BM1104 BAGI TAHUN " & ddlTahun.Text & ".csv")
        Response.Charset = ""
        Response.ContentType = "application/text"


        Dim sb As New StringBuilder()
        For k As Integer = 0 To dt.Columns.Count - 1
            'add separator 
            sb.Append(dt.Columns(k).ColumnName + ","c)
        Next

        'append new line 
        sb.Append(vbCr & vbLf)
        For i As Integer = 0 To dt.Rows.Count - 1
            For k As Integer = 0 To dt.Columns.Count - 1
                '--add separator 
                'sb.Append(dt.Rows(i)(k).ToString().Replace(",", ";") + ","c)

                'cleanup here
                If k <> 0 Then
                    sb.Append(",")
                End If

                Dim columnValue As Object = dt.Rows(i)(k).ToString()
                If columnValue Is Nothing Then
                    sb.Append("")
                Else
                    Dim columnStringValue As String = columnValue.ToString()

                    Dim cleanedColumnValue As String = CleanCSVString(columnStringValue)

                    If columnValue.[GetType]() Is GetType(String) AndAlso Not columnStringValue.Contains(",") Then
                        ' Prevents a number stored in a string from being shown as 8888E+24 in Excel. Example use is the AccountNum field in CI that looks like a number but is really a string.
                        cleanedColumnValue = "=" & cleanedColumnValue
                    End If
                    sb.Append(cleanedColumnValue)
                End If

            Next
            'append new line 
            sb.Append(vbCr & vbLf)
        Next
        Response.Output.Write(sb.ToString())
        Response.Flush()
        Response.End()

    End Sub

    Protected Function CleanCSVString(ByVal input As String) As String
        Dim output As String = """" & input.Replace("""", """""").Replace(vbCr & vbLf, " ").Replace(vbCr, " ").Replace(vbLf, "") & """"
        Return output
    End Function

    Private Sub chkSesi_SelectedIndexChanged(sender As Object, e As EventArgs) Handles chkSesi.SelectedIndexChanged

    End Sub
End Class
