Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient


Public Class rawmarkah_SJSetara

    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                kpmkv_tahun_list()
                ddlTahun.Text = Now.Year

                lblMsg.Text = ""
                strRet = BindData(datRespondent)

            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message
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

            '--ALL
            ddlTahun.Items.Add(New ListItem("PILIH", "PILIH"))

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub datRespondent_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles datRespondent.PageIndexChanging

        datRespondent.PageIndex = e.NewPageIndex
        strRet = BindData(datRespondent)

    End Sub

    Private Function BindData(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQL, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 120
        Try
            myDataAdapter.Fill(myDataSet, "myaccount")

            If myDataSet.Tables(0).Rows.Count = 0 Then
                divMsg.Attributes("class") = "error"
                lblMsg.Text = "Tiada rekod pelajar."
            Else
                divMsg.Attributes("class") = "info"
                lblMsg.Text = "Jumlah rekod#:" & myDataSet.Tables(0).Rows.Count
            End If

            gvTable.DataSource = myDataSet
            gvTable.DataBind()
            objConn.Close()

        Catch ex As Exception
            lblMsg.Text = "Error:" & ex.Message
            Return False
        End Try

        Return True

    End Function

    Private Function getSQL() As String

        Dim tmpSQL As String = ""
        Dim strWhere As String = ""
        Dim strOrder As String = " ORDER BY kpmkv_pelajar.Nama ASC"

        tmpSQL = "  SELECT
                    kpmkv_pelajar.Nama,
                    kpmkv_pelajar.PelajarID,
                    kpmkv_pelajar.MYKAD,
                    kpmkv_pelajar.AngkaGiliran,
                    kpmkv_kolej.Nama AS 'KV'
                    FROM
                    kpmkv_pelajar
                    LEFT JOIN kpmkv_pelajar_markah ON kpmkv_pelajar_markah.PelajarID = kpmkv_pelajar.PelajarID
                    LEFT JOIN kpmkv_kolej ON kpmkv_kolej.RecordID = kpmkv_pelajar.KolejRecordID
                    WHERE
                    kpmkv_pelajar_markah.Tahun = '" & ddlTahun.SelectedValue & "'
                    AND kpmkv_pelajar_markah.Semester = '4'                    
                    ORDER BY 
                    kpmkv_kolej.Nama,
                    kpmkv_pelajar.Nama"

        getSQL = tmpSQL

        Return getSQL

    End Function

    Private Sub datRespondent_SelectedIndexChanging(sender As Object, e As GridViewSelectEventArgs) Handles datRespondent.SelectedIndexChanging
        Dim strKeyID As String = datRespondent.DataKeys(e.NewSelectedIndex).Value.ToString

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

    Protected Sub btnCari_Click(sender As Object, e As EventArgs) Handles btnCari.Click
        lblMsg.Text = ""
        strRet = BindData(datRespondent)
        calculateBMSetara()
        printBMSetara(datRespondent)
    End Sub

    Private Sub calculateBMSetara()

        Dim strPelajarID As String

        Dim strPelajarID1 As String
        Dim strPelajarID2 As String
        Dim strPelajarID3 As String

        Dim lblMyKad As Label
        Dim lblSem1 As Label
        Dim lblSem2 As Label
        Dim lblSem3 As Label
        Dim lblSem4 As Label
        Dim lblK1 As Label
        Dim lblK2 As Label
        Dim lblK3_System As Label
        Dim lblK3 As Label
        Dim Total As Label
        Dim lblSem4_PA As Label
        Dim lblSem4_PB As Label
        Dim lblPB_PA As Label
        Dim lblGred_Sem4 As Label
        Dim Sem_1 As Label
        Dim Sem_2 As Label
        Dim Sem_3 As Label
        Dim Sem_4 As Label
        Dim lblSum As Label
        Dim lblGred_SJ1251 As Label


        For i As Integer = 0 To 10 - 1

            Dim countKV As Integer = 1

            strPelajarID = datRespondent.DataKeys(i).Value.ToString
            lblMyKad = datRespondent.Rows(i).FindControl("MYKAD")
            lblSem1 = datRespondent.Rows(i).FindControl("SEM1")
            lblSem2 = datRespondent.Rows(i).FindControl("SEM2")
            lblSem3 = datRespondent.Rows(i).FindControl("SEM3")
            lblSem4 = datRespondent.Rows(i).FindControl("SEM4")
            lblK1 = datRespondent.Rows(i).FindControl("K1")
            lblK2 = datRespondent.Rows(i).FindControl("K2")
            lblK3_System = datRespondent.Rows(i).FindControl("k3_SYSTEM")
            lblK3 = datRespondent.Rows(i).FindControl("K3")
            Total = datRespondent.Rows(i).FindControl("TOTAL")
            lblSem4_PA = datRespondent.Rows(i).FindControl("SEM4_PA")
            lblSem4_PB = datRespondent.Rows(i).FindControl("SEM4_PB")
            lblPB_PA = datRespondent.Rows(i).FindControl("PB_PA")
            lblGred_Sem4 = datRespondent.Rows(i).FindControl("GRED_SEM4")
            Sem_1 = datRespondent.Rows(i).FindControl("SEM_1")
            Sem_2 = datRespondent.Rows(i).FindControl("SEM_2")
            Sem_3 = datRespondent.Rows(i).FindControl("SEM_3")
            Sem_4 = datRespondent.Rows(i).FindControl("SEM_4")
            lblSum = datRespondent.Rows(i).FindControl("SUM")
            lblGred_SJ1251 = datRespondent.Rows(i).FindControl("GRED_SJ1251")


            ''PelajarID Sem1
            strSQL = "SELECT PelajarID FROM kpmkv_pelajar WHERE MYKAD = '" & lblMyKad.Text & "' AND Semester = '1'"
            strPelajarID1 = oCommon.getFieldValue(strSQL)

            ''PelajarID Sem2
            strSQL = "SELECT PelajarID FROM kpmkv_pelajar WHERE MYKAD = '" & lblMyKad.Text & "' AND Semester = '2'"
            strPelajarID2 = oCommon.getFieldValue(strSQL)

            ''PelajarID Sem3
            strSQL = "SELECT PelajarID FROM kpmkv_pelajar WHERE MYKAD = '" & lblMyKad.Text & "' AND Semester = '3'"
            strPelajarID3 = oCommon.getFieldValue(strSQL)

            ''Sejarah Sem1
            strSQL = "SELECT Sejarah FROM kpmkv_pelajar_markah WHERE PelajarID = '" & strPelajarID1 & "'"
            lblSem1.Text = oCommon.getFieldValue(strSQL)

            If lblSem1.Text = "" Then

                lblSem1.Text = "-"

            End If

            ''Sejarah Sem2
            strSQL = "SELECT Sejarah FROM kpmkv_pelajar_markah WHERE PelajarID = '" & strPelajarID2 & "'"
            lblSem2.Text = oCommon.getFieldValue(strSQL)

            If lblSem2.Text = "" Then

                lblSem2.Text = "-"

            End If

            ''Sejarah Sem3
            strSQL = "SELECT Sejarah FROM kpmkv_pelajar_markah WHERE PelajarID = '" & strPelajarID3 & "'"
            lblSem3.Text = oCommon.getFieldValue(strSQL)

            If lblSem3.Text = "" Then

                lblSem3.Text = "-"

            End If

            ''B_Sejarah
            strSQL = "SELECT B_Sejarah FROM kpmkv_pelajar_markah WHERE PelajarID = '" & strPelajarID & "'"
            lblSem4.Text = oCommon.getFieldValue(strSQL)

            If lblSem4.Text = "" Then

                lblSem4.Text = "-"

            End If

            ''A_Sejarah
            strSQL = "SELECT A_Sejarah FROM kpmkv_pelajar_markah WHERE PelajarID = '" & strPelajarID & "'"
            lblSem4_PA.Text = oCommon.getFieldValue(strSQL)

            If lblSem4_PA.Text = "" Then

                lblSem4_PA.Text = "-"

            End If

            If Not lblSem4_PA.Text = "-" Then

                lblSem4_PA.Text = Math.Round(lblSem4_PA.Text * 0.7)

            End If

            If Not lblSem4.Text = "-" Then

                lblSem4_PB.Text = Math.Round(lblSem4.Text * 0.3)

            Else

                lblSem4_PB.Text = "-"

            End If

            If Not lblSem4_PA.Text = "-" And Not lblSem4_PB.Text = "-" Then

                lblPB_PA.Text = Convert.ToInt32(lblSem4_PA.Text) + Convert.ToInt32(lblSem4_PB.Text)

            Else

                lblPB_PA.Text = "-"

            End If

            strSQL = "SELECT TOP ( 1 ) Gred FROM  kpmkv_gred_sejarah WHERE '" & lblPB_PA.Text & "' BETWEEN MarkahFrom AND MarkahTo AND Jenis='SJSETARA'"
            strSQL += " AND Tahun ='" & ddlTahun.Text & "'"
            lblGred_Sem4.Text = oCommon.getFieldValue(strSQL)

            If lblGred_Sem4.Text = "" Then

                lblGred_Sem4.Text = "-"

            End If

            If Not lblSem1.Text = "-" And Not lblSem2.Text = "-" And Not lblSem3.Text = "-" And Not lblPB_PA.Text = "-" Then

                Sem_1.Text = lblSem1.Text * 0.1
                Sem_2.Text = lblSem2.Text * 0.1
                Sem_3.Text = lblSem3.Text * 0.1
                Sem_4.Text = lblPB_PA.Text * 0.7

                lblSum.Text = Convert.ToDouble(Sem_1.Text) + Convert.ToDouble(Sem_2.Text) + Convert.ToDouble(Sem_3.Text) + Convert.ToDouble(Sem_4.Text)

            Else

                Sem_1.Text = "-"
                Sem_2.Text = "-"
                Sem_3.Text = "-"
                Sem_4.Text = "-"

                lblSum.Text = "-"

            End If

        Next

    End Sub

    Private Sub printBMSetara(ByVal gv As GridView)

        'Get the data from database into datatable 
        Response.Clear()
        Response.Buffer = True
        Response.AddHeader("content-disposition", "attachment;filename=RawMarkahSJSetara-Tahun" & ddlTahun.Text & ".csv")
        Response.Charset = ""
        Response.ContentType = "application/text"

        Dim sb As New StringBuilder()

        For k As Integer = 0 To gv.Columns.Count - 1
            'add separator 
            sb.Append(gv.Columns(k).HeaderText + ","c)
        Next

        'append new line 
        sb.Append(vbCr & vbLf)
        For i As Integer = 0 To gv.Rows.Count - 1
            For k As Integer = 0 To gv.Columns.Count - 1
                '--add separator 
                'sb.Append(dt.Rows(i)(k).ToString().Replace(",", ";") + ","c)

                'cleanup here
                If k <> 0 Then
                    sb.Append(",")
                End If

                Dim colValue As Label = gv.Rows(i).FindControl(gv.Columns(k).HeaderText)

                If colValue.Text Is Nothing Then
                    sb.Append("0")
                Else
                    sb.Append(colValue.Text)
                End If

            Next
            'append new line 
            sb.Append(vbCr & vbLf)
        Next
        Response.Output.Write(sb.ToString())
        Response.Flush()
        Response.End()

    End Sub

End Class