Imports System.IO
Imports System.Data
Imports System.Drawing
Imports System.Data.SqlClient
Imports System.Configuration

Public Class inden_matapelajaran_vok
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

            End If

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message
        End Try
    End Sub
    Private Sub kpmkv_tahun_list()
        strSQL = "SELECT DISTINCT Tahun FROM kpmkv_matapelajaran_v ORDER BY Tahun ASC"
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



    Private Sub kpmkv_semester_list()
        strSQL = "SELECT DISTINCT Semester FROM kpmkv_matapelajaran_v WHERE Tahun = '" & ddlTahun.SelectedValue & "'  ORDER BY Semester ASC"
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
            '--ALL
            ddlSemester.Items.Add(New ListItem("-Pilih-", ""))
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
        Dim i As Integer
        Dim tableEnd As Integer
        Dim strKodName As String

        strSQL = " SELECT KodMPVOK FROM kpmkv_matapelajaran_v WHERE Tahun = '" & ddlTahun.Text & "' AND Semester = '" & ddlSemester.Text & "' GROUP BY KodMPVOK ORDER BY KodMPVOK"

        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)
        Dim ds As DataSet = New DataSet
        sqlDA.Fill(ds, "AnyTable")

        Dim strWhere As String = ""
        Dim strGroup As String = ""
        Dim strOrder As String = " ORDER BY kpmkv_kolej.Negeri"

        tmpSQL = "  SELECT
		            kpmkv_kolej.Negeri,
		            kpmkv_pelajar.Tahun, kpmkv_pelajar.Semester, kpmkv_pelajar.Sesi,"

        tableEnd = ds.Tables(0).Rows.Count - 1

        For i = 0 To ds.Tables(0).Rows.Count - 1

            strKodName = ds.Tables(0).Rows(i).Item(0).ToString()

            tmpSQL += " (COUNT(CASE WHEN kpmkv_matapelajaran_v.KodMPVOK = '" & strKodName & "' THEN 1 ELSE NULL END)) AS '" & strKodName & "',"

        Next



        For i = 0 To ds.Tables(0).Rows.Count - 1

            If Not i = tableEnd Then

                strKodName = ds.Tables(0).Rows(i).Item(0).ToString()

                tmpSQL += " (COUNT(CASE WHEN kpmkv_matapelajaran_v.KodMPVOK = '" & strKodName & "' THEN 1 ELSE NULL END)) + "

            Else

                strKodName = ds.Tables(0).Rows(i).Item(0).ToString()

                tmpSQL += " (COUNT(CASE WHEN kpmkv_matapelajaran_v.KodMPVOK = '" & strKodName & "' THEN 1 ELSE NULL END)) As 'Jumlah_Calon'"

            End If

        Next

        tmpSQL += " FROM
                    kpmkv_pelajar
                    LEFT JOIN kpmkv_kolej ON kpmkv_kolej.RecordID = kpmkv_pelajar.KolejRecordID
                    LEFT JOIN kpmkv_matapelajaran_v ON kpmkv_matapelajaran_v.KursusID = kpmkv_pelajar.KursusID"

        strWhere = "    WHERE kpmkv_pelajar.StatusID = '2' AND kpmkv_kolej.Negeri IS NOT NULL "

        '--tahun
        If Not ddlTahun.Text = "" Then
            strWhere += " AND kpmkv_pelajar.Tahun = '" & ddlTahun.Text & "'"
        End If
        '--semester
        If Not ddlSemester.Text = "" Then
            strWhere += " AND kpmkv_pelajar.Semester = '" & ddlSemester.Text & "'"
        End If

        If Not chkSesi.Text = "" Then
            strWhere += " AND kpmkv_pelajar.Sesi = '" & chkSesi.Text & "'"
        End If

        strGroup = "    GROUP BY
		                kpmkv_kolej.Negeri,
		                kpmkv_pelajar.Tahun, kpmkv_pelajar.Semester, kpmkv_pelajar.Sesi"

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

    'Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
    '    lblMsg.Text = ""

    '    'Dim col0 As Label
    '    'Dim col1 As Label
    '    'Dim col2 As Label
    '    'Dim col3 As Label
    '    'Dim col4 As Label
    '    'Dim col5 As Label
    '    'Dim col6 As Label
    '    'Dim col7 As Label
    '    'Dim col8 As Label
    '    'Dim col9 As Label
    '    'Dim col10 As Label
    '    'Dim col11 As Label
    '    'Dim col12 As Label
    '    'Dim col13 As Label
    '    'Dim col14 As Label
    '    'Dim col15 As Label
    '    'Dim col16 As Label
    '    'Dim col17 As Label
    '    'Dim col18 As Label
    '    'Dim col19 As Label
    '    'Dim col20 As Label
    '    'Dim col21 As Label
    '    'Dim col22 As Label
    '    'Dim col23 As Label
    '    'Dim col24 As Label
    '    'Dim col25 As Label
    '    'Dim col26 As Label
    '    'Dim col27 As Label
    '    'Dim col28 As Label
    '    'Dim col29 As Label
    '    'Dim col30 As Label
    '    'Dim col31 As Label
    '    'Dim col32 As Label
    '    'Dim col33 As Label
    '    'Dim col34 As Label
    '    'Dim col35 As Label
    '    'Dim col36 As Label
    '    'Dim col37 As Label
    '    'Dim col38 As Label
    '    'Dim col39 As Label

    '    'Dim Jumlah As Label

    '    If ddlSemester.Text = "1" Then

    '        strRet = BindData(datRespondent)
    '        datRespondent.Visible = True
    '        datRespondent2.Visible = False
    '        datRespondent3.Visible = False
    '        datRespondent4.Visible = False

    '        'For i = 0 To datRespondent.Rows.Count - 1

    '        '    col0 = datRespondent.Rows(i).FindControl("V0101")
    '        '    col1 = datRespondent.Rows(i).FindControl("V0201")
    '        '    col2 = datRespondent.Rows(i).FindControl("V0301")
    '        '    col3 = datRespondent.Rows(i).FindControl("V0401")
    '        '    col4 = datRespondent.Rows(i).FindControl("V0501")
    '        '    col5 = datRespondent.Rows(i).FindControl("V0601")
    '        '    col6 = datRespondent.Rows(i).FindControl("V0701")
    '        '    col7 = datRespondent.Rows(i).FindControl("V0801")
    '        '    col8 = datRespondent.Rows(i).FindControl("V0901")
    '        '    col9 = datRespondent.Rows(i).FindControl("V1001")
    '        '    col10 = datRespondent.Rows(i).FindControl("V1101")
    '        '    col11 = datRespondent.Rows(i).FindControl("V1201")
    '        '    col12 = datRespondent.Rows(i).FindControl("V1301")
    '        '    col13 = datRespondent.Rows(i).FindControl("V1401")
    '        '    col14 = datRespondent.Rows(i).FindControl("V1501")
    '        '    col15 = datRespondent.Rows(i).FindControl("V1601")
    '        '    col16 = datRespondent.Rows(i).FindControl("V1701")
    '        '    col17 = datRespondent.Rows(i).FindControl("V1801")
    '        '    col18 = datRespondent.Rows(i).FindControl("V1901")
    '        '    col19 = datRespondent.Rows(i).FindControl("V2001")
    '        '    col20 = datRespondent.Rows(i).FindControl("V2101")
    '        '    col21 = datRespondent.Rows(i).FindControl("V2201")
    '        '    col22 = datRespondent.Rows(i).FindControl("V2301")
    '        '    col23 = datRespondent.Rows(i).FindControl("V2401")
    '        '    col24 = datRespondent.Rows(i).FindControl("V2501")
    '        '    col25 = datRespondent.Rows(i).FindControl("V2601")
    '        '    col26 = datRespondent.Rows(i).FindControl("V2701")
    '        '    col27 = datRespondent.Rows(i).FindControl("V2801")
    '        '    col28 = datRespondent.Rows(i).FindControl("V2901")
    '        '    col29 = datRespondent.Rows(i).FindControl("V3001")
    '        '    col30 = datRespondent.Rows(i).FindControl("V3101")
    '        '    col31 = datRespondent.Rows(i).FindControl("V3201")
    '        '    col32 = datRespondent.Rows(i).FindControl("V3301")
    '        '    col33 = datRespondent.Rows(i).FindControl("V3401")
    '        '    col34 = datRespondent.Rows(i).FindControl("V3501")
    '        '    col35 = datRespondent.Rows(i).FindControl("V3601")
    '        '    col36 = datRespondent.Rows(i).FindControl("V3701")
    '        '    col37 = datRespondent.Rows(i).FindControl("V3801")
    '        '    col38 = datRespondent.Rows(i).FindControl("V3901")
    '        '    col39 = datRespondent.Rows(i).FindControl("V4001")

    '        '    Jumlah = datRespondent.Rows(i).FindControl("Jumlah_Calon")

    '        '    If Not col0.Text = 0 Then
    '        '        col0.Text = Math.Round(col0.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col1.Text = 0 Then
    '        '        col1.Text = Math.Round(col1.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col2.Text = 0 Then
    '        '        col2.Text = Math.Round(col2.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col3.Text = 0 Then
    '        '        col3.Text = Math.Round(col3.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col4.Text = 0 Then
    '        '        col4.Text = Math.Round(col4.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col5.Text = 0 Then
    '        '        col5.Text = Math.Round(col5.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col6.Text = 0 Then
    '        '        col6.Text = Math.Round(col6.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col7.Text = 0 Then
    '        '        col7.Text = Math.Round(col7.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col8.Text = 0 Then
    '        '        col8.Text = Math.Round(col8.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col9.Text = 0 Then
    '        '        col9.Text = Math.Round(col9.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col10.Text = 0 Then
    '        '        col10.Text = Math.Round(col10.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col11.Text = 0 Then
    '        '        col11.Text = Math.Round(col11.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col12.Text = 0 Then
    '        '        col12.Text = Math.Round(col12.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col13.Text = 0 Then
    '        '        col13.Text = Math.Round(col13.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col14.Text = 0 Then
    '        '        col14.Text = Math.Round(col14.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col15.Text = 0 Then
    '        '        col15.Text = Math.Round(col15.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col16.Text = 0 Then
    '        '        col16.Text = Math.Round(col16.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col17.Text = 0 Then
    '        '        col17.Text = Math.Round(col17.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col18.Text = 0 Then
    '        '        col18.Text = Math.Round(col18.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col19.Text = 0 Then
    '        '        col19.Text = Math.Round(col19.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col20.Text = 0 Then
    '        '        col20.Text = Math.Round(col20.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col21.Text = 0 Then
    '        '        col21.Text = Math.Round(col21.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col22.Text = 0 Then
    '        '        col22.Text = Math.Round(col22.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col23.Text = 0 Then
    '        '        col23.Text = Math.Round(col23.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col24.Text = 0 Then
    '        '        col24.Text = Math.Round(col24.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col25.Text = 0 Then
    '        '        col25.Text = Math.Round(col25.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col26.Text = 0 Then
    '        '        col26.Text = Math.Round(col26.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col27.Text = 0 Then
    '        '        col27.Text = Math.Round(col27.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col28.Text = 0 Then
    '        '        col28.Text = Math.Round(col28.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col29.Text = 0 Then
    '        '        col29.Text = Math.Round(col29.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col30.Text = 0 Then
    '        '        col30.Text = Math.Round(col30.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col31.Text = 0 Then
    '        '        col31.Text = Math.Round(col31.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col32.Text = 0 Then
    '        '        col32.Text = Math.Round(col32.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col33.Text = 0 Then
    '        '        col33.Text = Math.Round(col33.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col34.Text = 0 Then
    '        '        col34.Text = Math.Round(col34.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col35.Text = 0 Then
    '        '        col35.Text = Math.Round(col35.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col36.Text = 0 Then
    '        '        col36.Text = Math.Round(col36.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col37.Text = 0 Then
    '        '        col37.Text = Math.Round(col37.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col38.Text = 0 Then
    '        '        col38.Text = Math.Round(col38.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col39.Text = 0 Then
    '        '        col39.Text = Math.Round(col39.Text / 5) * 5 + 5
    '        '    End If

    '        '    Jumlah.Text = 0

    '        '    Jumlah.Text = Jumlah.Text + Convert.ToInt32(col0.Text) + Convert.ToInt32(col1.Text) + Convert.ToInt32(col2.Text) + Convert.ToInt32(col3.Text) + Convert.ToInt32(col4.Text) + Convert.ToInt32(col5.Text) + Convert.ToInt32(col6.Text) + Convert.ToInt32(col7.Text) + Convert.ToInt32(col8.Text) + Convert.ToInt32(col9.Text)
    '        '    Jumlah.Text = Jumlah.Text + Convert.ToInt32(col10.Text) + Convert.ToInt32(col11.Text) + Convert.ToInt32(col12.Text) + Convert.ToInt32(col13.Text) + Convert.ToInt32(col14.Text) + Convert.ToInt32(col15.Text) + Convert.ToInt32(col16.Text) + Convert.ToInt32(col17.Text) + Convert.ToInt32(col18.Text) + Convert.ToInt32(col19.Text)
    '        '    Jumlah.Text = Jumlah.Text + Convert.ToInt32(col20.Text) + Convert.ToInt32(col21.Text) + Convert.ToInt32(col22.Text) + Convert.ToInt32(col23.Text) + Convert.ToInt32(col24.Text) + Convert.ToInt32(col25.Text) + Convert.ToInt32(col26.Text) + Convert.ToInt32(col27.Text) + Convert.ToInt32(col28.Text) + Convert.ToInt32(col29.Text)
    '        '    Jumlah.Text = Jumlah.Text + Convert.ToInt32(col30.Text) + Convert.ToInt32(col31.Text) + Convert.ToInt32(col32.Text) + Convert.ToInt32(col33.Text) + Convert.ToInt32(col34.Text) + Convert.ToInt32(col35.Text) + Convert.ToInt32(col36.Text) + Convert.ToInt32(col37.Text) + Convert.ToInt32(col38.Text) + Convert.ToInt32(col39.Text)

    '        'Next


    '    ElseIf ddlSemester.Text = "2" Then

    '        strRet = BindData(datRespondent2)
    '        datRespondent.Visible = False
    '        datRespondent2.Visible = True
    '        datRespondent3.Visible = False
    '        datRespondent4.Visible = False

    '        'For i = 0 To datRespondent2.Rows.Count - 1

    '        '    col0 = datRespondent2.Rows(i).FindControl("V0102")
    '        '    col1 = datRespondent2.Rows(i).FindControl("V0202")
    '        '    col2 = datRespondent2.Rows(i).FindControl("V0302")
    '        '    col3 = datRespondent2.Rows(i).FindControl("V0402")
    '        '    col4 = datRespondent2.Rows(i).FindControl("V0502")
    '        '    col5 = datRespondent2.Rows(i).FindControl("V0602")
    '        '    col6 = datRespondent2.Rows(i).FindControl("V0702")
    '        '    col7 = datRespondent2.Rows(i).FindControl("V0802")
    '        '    col8 = datRespondent2.Rows(i).FindControl("V0902")
    '        '    col9 = datRespondent2.Rows(i).FindControl("V1002")
    '        '    col10 = datRespondent2.Rows(i).FindControl("V1102")
    '        '    col11 = datRespondent2.Rows(i).FindControl("V1202")
    '        '    col12 = datRespondent2.Rows(i).FindControl("V1302")
    '        '    col13 = datRespondent2.Rows(i).FindControl("V1402")
    '        '    col14 = datRespondent2.Rows(i).FindControl("V1502")
    '        '    col15 = datRespondent2.Rows(i).FindControl("V1602")
    '        '    col16 = datRespondent2.Rows(i).FindControl("V1702")
    '        '    col17 = datRespondent2.Rows(i).FindControl("V1802")
    '        '    col18 = datRespondent2.Rows(i).FindControl("V1902")
    '        '    col19 = datRespondent2.Rows(i).FindControl("V2002")
    '        '    col20 = datRespondent2.Rows(i).FindControl("V2102")
    '        '    col21 = datRespondent2.Rows(i).FindControl("V2202")
    '        '    col22 = datRespondent2.Rows(i).FindControl("V2302")
    '        '    col23 = datRespondent2.Rows(i).FindControl("V2402")
    '        '    col24 = datRespondent2.Rows(i).FindControl("V2502")
    '        '    col25 = datRespondent2.Rows(i).FindControl("V2602")
    '        '    col26 = datRespondent2.Rows(i).FindControl("V2702")
    '        '    col27 = datRespondent2.Rows(i).FindControl("V2802")
    '        '    col28 = datRespondent2.Rows(i).FindControl("V2902")
    '        '    col29 = datRespondent2.Rows(i).FindControl("V3002")
    '        '    col30 = datRespondent2.Rows(i).FindControl("V3102")
    '        '    col31 = datRespondent2.Rows(i).FindControl("V3202")
    '        '    col32 = datRespondent2.Rows(i).FindControl("V3302")
    '        '    col33 = datRespondent2.Rows(i).FindControl("V3402")
    '        '    col34 = datRespondent2.Rows(i).FindControl("V3502")
    '        '    col35 = datRespondent2.Rows(i).FindControl("V3602")
    '        '    col36 = datRespondent2.Rows(i).FindControl("V3702")
    '        '    col37 = datRespondent2.Rows(i).FindControl("V3802")
    '        '    col38 = datRespondent2.Rows(i).FindControl("V3902")
    '        '    col39 = datRespondent2.Rows(i).FindControl("V4002")

    '        '    Jumlah = datRespondent2.Rows(i).FindControl("Jumlah_Calon")

    '        '    If Not col0.Text = 0 Then
    '        '        col0.Text = Math.Round(col0.Text / 5) * 5 + 5
    '        '        Convert.ToInt32(col0.Text)
    '        '    End If
    '        '    If Not col1.Text = 0 Then
    '        '        col1.Text = Math.Round(col1.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col2.Text = 0 Then
    '        '        col2.Text = Math.Round(col2.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col3.Text = 0 Then
    '        '        col3.Text = Math.Round(col3.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col4.Text = 0 Then
    '        '        col4.Text = Math.Round(col4.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col5.Text = 0 Then
    '        '        col5.Text = Math.Round(col5.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col6.Text = 0 Then
    '        '        col6.Text = Math.Round(col6.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col7.Text = 0 Then
    '        '        col7.Text = Math.Round(col7.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col8.Text = 0 Then
    '        '        col8.Text = Math.Round(col8.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col9.Text = 0 Then
    '        '        col9.Text = Math.Round(col9.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col10.Text = 0 Then
    '        '        col10.Text = Math.Round(col10.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col11.Text = 0 Then
    '        '        col11.Text = Math.Round(col11.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col12.Text = 0 Then
    '        '        col12.Text = Math.Round(col12.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col13.Text = 0 Then
    '        '        col13.Text = Math.Round(col13.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col14.Text = 0 Then
    '        '        col14.Text = Math.Round(col14.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col15.Text = 0 Then
    '        '        col15.Text = Math.Round(col15.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col16.Text = 0 Then
    '        '        col16.Text = Math.Round(col16.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col17.Text = 0 Then
    '        '        col17.Text = Math.Round(col17.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col18.Text = 0 Then
    '        '        col18.Text = Math.Round(col18.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col19.Text = 0 Then
    '        '        col19.Text = Math.Round(col19.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col20.Text = 0 Then
    '        '        col20.Text = Math.Round(col20.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col21.Text = 0 Then
    '        '        col21.Text = Math.Round(col21.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col22.Text = 0 Then
    '        '        col22.Text = Math.Round(col22.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col23.Text = 0 Then
    '        '        col23.Text = Math.Round(col23.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col24.Text = 0 Then
    '        '        col24.Text = Math.Round(col24.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col25.Text = 0 Then
    '        '        col25.Text = Math.Round(col25.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col26.Text = 0 Then
    '        '        col26.Text = Math.Round(col26.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col27.Text = 0 Then
    '        '        col27.Text = Math.Round(col27.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col28.Text = 0 Then
    '        '        col28.Text = Math.Round(col28.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col29.Text = 0 Then
    '        '        col29.Text = Math.Round(col29.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col30.Text = 0 Then
    '        '        col30.Text = Math.Round(col30.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col31.Text = 0 Then
    '        '        col31.Text = Math.Round(col31.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col32.Text = 0 Then
    '        '        col32.Text = Math.Round(col32.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col33.Text = 0 Then
    '        '        col33.Text = Math.Round(col33.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col34.Text = 0 Then
    '        '        col34.Text = Math.Round(col34.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col35.Text = 0 Then
    '        '        col35.Text = Math.Round(col35.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col36.Text = 0 Then
    '        '        col36.Text = Math.Round(col36.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col37.Text = 0 Then
    '        '        col37.Text = Math.Round(col37.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col38.Text = 0 Then
    '        '        col38.Text = Math.Round(col38.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col39.Text = 0 Then
    '        '        col39.Text = Math.Round(col39.Text / 5) * 5 + 5
    '        '    End If

    '        '    Jumlah.Text = 0

    '        '    Jumlah.Text = Jumlah.Text + Convert.ToInt32(col0.Text) + Convert.ToInt32(col1.Text) + Convert.ToInt32(col2.Text) + Convert.ToInt32(col3.Text) + Convert.ToInt32(col4.Text) + Convert.ToInt32(col5.Text) + Convert.ToInt32(col6.Text) + Convert.ToInt32(col7.Text) + Convert.ToInt32(col8.Text) + Convert.ToInt32(col9.Text)
    '        '    Jumlah.Text = Jumlah.Text + Convert.ToInt32(col10.Text) + Convert.ToInt32(col11.Text) + Convert.ToInt32(col12.Text) + Convert.ToInt32(col13.Text) + Convert.ToInt32(col14.Text) + Convert.ToInt32(col15.Text) + Convert.ToInt32(col16.Text) + Convert.ToInt32(col17.Text) + Convert.ToInt32(col18.Text) + Convert.ToInt32(col19.Text)
    '        '    Jumlah.Text = Jumlah.Text + Convert.ToInt32(col20.Text) + Convert.ToInt32(col21.Text) + Convert.ToInt32(col22.Text) + Convert.ToInt32(col23.Text) + Convert.ToInt32(col24.Text) + Convert.ToInt32(col25.Text) + Convert.ToInt32(col26.Text) + Convert.ToInt32(col27.Text) + Convert.ToInt32(col28.Text) + Convert.ToInt32(col29.Text)
    '        '    Jumlah.Text = Jumlah.Text + Convert.ToInt32(col30.Text) + Convert.ToInt32(col31.Text) + Convert.ToInt32(col32.Text) + Convert.ToInt32(col33.Text) + Convert.ToInt32(col34.Text) + Convert.ToInt32(col35.Text) + Convert.ToInt32(col36.Text) + Convert.ToInt32(col37.Text) + Convert.ToInt32(col38.Text) + Convert.ToInt32(col39.Text)


    '        'Next

    '    ElseIf ddlSemester.Text = "3" Then

    '        strRet = BindData(datRespondent3)
    '        datRespondent.Visible = False
    '        datRespondent2.Visible = False
    '        datRespondent3.Visible = True
    '        datRespondent4.Visible = False

    '        'For i = 0 To datRespondent3.Rows.Count - 1

    '        '    col0 = datRespondent3.Rows(i).FindControl("V0103")
    '        '    col1 = datRespondent3.Rows(i).FindControl("V0203")
    '        '    col2 = datRespondent3.Rows(i).FindControl("V0303")
    '        '    col3 = datRespondent3.Rows(i).FindControl("V0403")
    '        '    col4 = datRespondent3.Rows(i).FindControl("V0503")
    '        '    col5 = datRespondent3.Rows(i).FindControl("V0603")
    '        '    col6 = datRespondent3.Rows(i).FindControl("V0703")
    '        '    col7 = datRespondent3.Rows(i).FindControl("V0803")
    '        '    col8 = datRespondent3.Rows(i).FindControl("V0903")
    '        '    col9 = datRespondent3.Rows(i).FindControl("V1003")
    '        '    col10 = datRespondent3.Rows(i).FindControl("V1103")
    '        '    col11 = datRespondent3.Rows(i).FindControl("V1203")
    '        '    col12 = datRespondent3.Rows(i).FindControl("V1303")
    '        '    col13 = datRespondent3.Rows(i).FindControl("V1403")
    '        '    col14 = datRespondent3.Rows(i).FindControl("V1503")
    '        '    col15 = datRespondent3.Rows(i).FindControl("V1603")
    '        '    col16 = datRespondent3.Rows(i).FindControl("V1703")
    '        '    col17 = datRespondent3.Rows(i).FindControl("V1803")
    '        '    col18 = datRespondent3.Rows(i).FindControl("V1903")
    '        '    col19 = datRespondent3.Rows(i).FindControl("V2003")
    '        '    col20 = datRespondent3.Rows(i).FindControl("V2103")
    '        '    col21 = datRespondent3.Rows(i).FindControl("V2203")
    '        '    col22 = datRespondent3.Rows(i).FindControl("V2303")
    '        '    col23 = datRespondent3.Rows(i).FindControl("V2403")
    '        '    col24 = datRespondent3.Rows(i).FindControl("V2503")
    '        '    col25 = datRespondent3.Rows(i).FindControl("V2603")
    '        '    col26 = datRespondent3.Rows(i).FindControl("V2703")
    '        '    col27 = datRespondent3.Rows(i).FindControl("V2803")
    '        '    col28 = datRespondent3.Rows(i).FindControl("V2903")
    '        '    col29 = datRespondent3.Rows(i).FindControl("V3003")
    '        '    col30 = datRespondent3.Rows(i).FindControl("V3103")
    '        '    col31 = datRespondent3.Rows(i).FindControl("V3203")
    '        '    col32 = datRespondent3.Rows(i).FindControl("V3303")
    '        '    col33 = datRespondent3.Rows(i).FindControl("V3403")
    '        '    col34 = datRespondent3.Rows(i).FindControl("V3503")
    '        '    col35 = datRespondent3.Rows(i).FindControl("V3603")
    '        '    col36 = datRespondent3.Rows(i).FindControl("V3703")
    '        '    col37 = datRespondent3.Rows(i).FindControl("V3803")
    '        '    col38 = datRespondent3.Rows(i).FindControl("V3903")
    '        '    col39 = datRespondent3.Rows(i).FindControl("V4003")

    '        '    Jumlah = datRespondent3.Rows(i).FindControl("Jumlah_Calon")

    '        '    If Not col0.Text = 0 Then
    '        '        col0.Text = Math.Round(col0.Text / 5) * 5 + 5
    '        '        Convert.ToInt32(col0.Text)
    '        '    End If
    '        '    If Not col1.Text = 0 Then
    '        '        col1.Text = Math.Round(col1.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col2.Text = 0 Then
    '        '        col2.Text = Math.Round(col2.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col3.Text = 0 Then
    '        '        col3.Text = Math.Round(col3.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col4.Text = 0 Then
    '        '        col4.Text = Math.Round(col4.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col5.Text = 0 Then
    '        '        col5.Text = Math.Round(col5.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col6.Text = 0 Then
    '        '        col6.Text = Math.Round(col6.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col7.Text = 0 Then
    '        '        col7.Text = Math.Round(col7.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col8.Text = 0 Then
    '        '        col8.Text = Math.Round(col8.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col9.Text = 0 Then
    '        '        col9.Text = Math.Round(col9.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col10.Text = 0 Then
    '        '        col10.Text = Math.Round(col10.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col11.Text = 0 Then
    '        '        col11.Text = Math.Round(col11.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col12.Text = 0 Then
    '        '        col12.Text = Math.Round(col12.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col13.Text = 0 Then
    '        '        col13.Text = Math.Round(col13.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col14.Text = 0 Then
    '        '        col14.Text = Math.Round(col14.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col15.Text = 0 Then
    '        '        col15.Text = Math.Round(col15.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col16.Text = 0 Then
    '        '        col16.Text = Math.Round(col16.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col17.Text = 0 Then
    '        '        col17.Text = Math.Round(col17.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col18.Text = 0 Then
    '        '        col18.Text = Math.Round(col18.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col19.Text = 0 Then
    '        '        col19.Text = Math.Round(col19.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col20.Text = 0 Then
    '        '        col20.Text = Math.Round(col20.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col21.Text = 0 Then
    '        '        col21.Text = Math.Round(col21.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col22.Text = 0 Then
    '        '        col22.Text = Math.Round(col22.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col23.Text = 0 Then
    '        '        col23.Text = Math.Round(col23.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col24.Text = 0 Then
    '        '        col24.Text = Math.Round(col24.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col25.Text = 0 Then
    '        '        col25.Text = Math.Round(col25.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col26.Text = 0 Then
    '        '        col26.Text = Math.Round(col26.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col27.Text = 0 Then
    '        '        col27.Text = Math.Round(col27.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col28.Text = 0 Then
    '        '        col28.Text = Math.Round(col28.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col29.Text = 0 Then
    '        '        col29.Text = Math.Round(col29.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col30.Text = 0 Then
    '        '        col30.Text = Math.Round(col30.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col31.Text = 0 Then
    '        '        col31.Text = Math.Round(col31.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col32.Text = 0 Then
    '        '        col32.Text = Math.Round(col32.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col33.Text = 0 Then
    '        '        col33.Text = Math.Round(col33.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col34.Text = 0 Then
    '        '        col34.Text = Math.Round(col34.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col35.Text = 0 Then
    '        '        col35.Text = Math.Round(col35.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col36.Text = 0 Then
    '        '        col36.Text = Math.Round(col36.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col37.Text = 0 Then
    '        '        col37.Text = Math.Round(col37.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col38.Text = 0 Then
    '        '        col38.Text = Math.Round(col38.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col39.Text = 0 Then
    '        '        col39.Text = Math.Round(col39.Text / 5) * 5 + 5
    '        '    End If

    '        '    Jumlah.Text = 0

    '        '    Jumlah.Text = Jumlah.Text + Convert.ToInt32(col0.Text) + Convert.ToInt32(col1.Text) + Convert.ToInt32(col2.Text) + Convert.ToInt32(col3.Text) + Convert.ToInt32(col4.Text) + Convert.ToInt32(col5.Text) + Convert.ToInt32(col6.Text) + Convert.ToInt32(col7.Text) + Convert.ToInt32(col8.Text) + Convert.ToInt32(col9.Text)
    '        '    Jumlah.Text = Jumlah.Text + Convert.ToInt32(col10.Text) + Convert.ToInt32(col11.Text) + Convert.ToInt32(col12.Text) + Convert.ToInt32(col13.Text) + Convert.ToInt32(col14.Text) + Convert.ToInt32(col15.Text) + Convert.ToInt32(col16.Text) + Convert.ToInt32(col17.Text) + Convert.ToInt32(col18.Text) + Convert.ToInt32(col19.Text)
    '        '    Jumlah.Text = Jumlah.Text + Convert.ToInt32(col20.Text) + Convert.ToInt32(col21.Text) + Convert.ToInt32(col22.Text) + Convert.ToInt32(col23.Text) + Convert.ToInt32(col24.Text) + Convert.ToInt32(col25.Text) + Convert.ToInt32(col26.Text) + Convert.ToInt32(col27.Text) + Convert.ToInt32(col28.Text) + Convert.ToInt32(col29.Text)
    '        '    Jumlah.Text = Jumlah.Text + Convert.ToInt32(col30.Text) + Convert.ToInt32(col31.Text) + Convert.ToInt32(col32.Text) + Convert.ToInt32(col33.Text) + Convert.ToInt32(col34.Text) + Convert.ToInt32(col35.Text) + Convert.ToInt32(col36.Text) + Convert.ToInt32(col37.Text) + Convert.ToInt32(col38.Text) + Convert.ToInt32(col39.Text)

    '        'Next

    '    ElseIf ddlSemester.Text = "4" Then

    '        strRet = BindData(datRespondent4)
    '        datRespondent.Visible = False
    '        datRespondent2.Visible = False
    '        datRespondent3.Visible = False
    '        datRespondent4.Visible = True

    '        'For i = 0 To datRespondent4.Rows.Count - 1

    '        '    col0 = datRespondent4.Rows(i).FindControl("V0104")
    '        '    col1 = datRespondent4.Rows(i).FindControl("V0204")
    '        '    col2 = datRespondent4.Rows(i).FindControl("V0304")
    '        '    col3 = datRespondent4.Rows(i).FindControl("V0404")
    '        '    col4 = datRespondent4.Rows(i).FindControl("V0504")
    '        '    col5 = datRespondent4.Rows(i).FindControl("V0604")
    '        '    col6 = datRespondent4.Rows(i).FindControl("V0704")
    '        '    col7 = datRespondent4.Rows(i).FindControl("V0804")
    '        '    col8 = datRespondent4.Rows(i).FindControl("V0904")
    '        '    col9 = datRespondent4.Rows(i).FindControl("V1004")
    '        '    col10 = datRespondent4.Rows(i).FindControl("V1104")
    '        '    col11 = datRespondent4.Rows(i).FindControl("V1204")
    '        '    col12 = datRespondent4.Rows(i).FindControl("V1304")
    '        '    col13 = datRespondent4.Rows(i).FindControl("V1404")
    '        '    col14 = datRespondent4.Rows(i).FindControl("V1504")
    '        '    col15 = datRespondent4.Rows(i).FindControl("V1604")
    '        '    col16 = datRespondent4.Rows(i).FindControl("V1704")
    '        '    col17 = datRespondent4.Rows(i).FindControl("V1804")
    '        '    col18 = datRespondent4.Rows(i).FindControl("V1904")
    '        '    col19 = datRespondent4.Rows(i).FindControl("V2004")
    '        '    col20 = datRespondent4.Rows(i).FindControl("V2104")
    '        '    col21 = datRespondent4.Rows(i).FindControl("V2204")
    '        '    col22 = datRespondent4.Rows(i).FindControl("V2304")
    '        '    col23 = datRespondent4.Rows(i).FindControl("V2404")
    '        '    col24 = datRespondent4.Rows(i).FindControl("V2504")
    '        '    col25 = datRespondent4.Rows(i).FindControl("V2604")
    '        '    col26 = datRespondent4.Rows(i).FindControl("V2704")
    '        '    col27 = datRespondent4.Rows(i).FindControl("V2804")
    '        '    col28 = datRespondent4.Rows(i).FindControl("V2904")
    '        '    col29 = datRespondent4.Rows(i).FindControl("V3004")
    '        '    col30 = datRespondent4.Rows(i).FindControl("V3104")
    '        '    col31 = datRespondent4.Rows(i).FindControl("V3204")
    '        '    col32 = datRespondent4.Rows(i).FindControl("V3304")
    '        '    col33 = datRespondent4.Rows(i).FindControl("V3404")
    '        '    col34 = datRespondent4.Rows(i).FindControl("V3504")
    '        '    col35 = datRespondent4.Rows(i).FindControl("V3604")
    '        '    col36 = datRespondent4.Rows(i).FindControl("V3704")
    '        '    col37 = datRespondent4.Rows(i).FindControl("V3804")
    '        '    col38 = datRespondent4.Rows(i).FindControl("V3904")
    '        '    col39 = datRespondent4.Rows(i).FindControl("V4004")

    '        '    Jumlah = datRespondent4.Rows(i).FindControl("Jumlah_Calon")

    '        '    If Not col0.Text = 0 Then
    '        '        col0.Text = Math.Round(col0.Text / 5) * 5 + 5
    '        '        Convert.ToInt32(col0.Text)
    '        '    End If
    '        '    If Not col1.Text = 0 Then
    '        '        col1.Text = Math.Round(col1.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col2.Text = 0 Then
    '        '        col2.Text = Math.Round(col2.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col3.Text = 0 Then
    '        '        col3.Text = Math.Round(col3.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col4.Text = 0 Then
    '        '        col4.Text = Math.Round(col4.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col5.Text = 0 Then
    '        '        col5.Text = Math.Round(col5.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col6.Text = 0 Then
    '        '        col6.Text = Math.Round(col6.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col7.Text = 0 Then
    '        '        col7.Text = Math.Round(col7.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col8.Text = 0 Then
    '        '        col8.Text = Math.Round(col8.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col9.Text = 0 Then
    '        '        col9.Text = Math.Round(col9.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col10.Text = 0 Then
    '        '        col10.Text = Math.Round(col10.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col11.Text = 0 Then
    '        '        col11.Text = Math.Round(col11.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col12.Text = 0 Then
    '        '        col12.Text = Math.Round(col12.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col13.Text = 0 Then
    '        '        col13.Text = Math.Round(col13.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col14.Text = 0 Then
    '        '        col14.Text = Math.Round(col14.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col15.Text = 0 Then
    '        '        col15.Text = Math.Round(col15.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col16.Text = 0 Then
    '        '        col16.Text = Math.Round(col16.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col17.Text = 0 Then
    '        '        col17.Text = Math.Round(col17.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col18.Text = 0 Then
    '        '        col18.Text = Math.Round(col18.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col19.Text = 0 Then
    '        '        col19.Text = Math.Round(col19.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col20.Text = 0 Then
    '        '        col20.Text = Math.Round(col20.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col21.Text = 0 Then
    '        '        col21.Text = Math.Round(col21.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col22.Text = 0 Then
    '        '        col22.Text = Math.Round(col22.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col23.Text = 0 Then
    '        '        col23.Text = Math.Round(col23.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col24.Text = 0 Then
    '        '        col24.Text = Math.Round(col24.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col25.Text = 0 Then
    '        '        col25.Text = Math.Round(col25.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col26.Text = 0 Then
    '        '        col26.Text = Math.Round(col26.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col27.Text = 0 Then
    '        '        col27.Text = Math.Round(col27.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col28.Text = 0 Then
    '        '        col28.Text = Math.Round(col28.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col29.Text = 0 Then
    '        '        col29.Text = Math.Round(col29.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col30.Text = 0 Then
    '        '        col30.Text = Math.Round(col30.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col31.Text = 0 Then
    '        '        col31.Text = Math.Round(col31.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col32.Text = 0 Then
    '        '        col32.Text = Math.Round(col32.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col33.Text = 0 Then
    '        '        col33.Text = Math.Round(col33.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col34.Text = 0 Then
    '        '        col34.Text = Math.Round(col34.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col35.Text = 0 Then
    '        '        col35.Text = Math.Round(col35.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col36.Text = 0 Then
    '        '        col36.Text = Math.Round(col36.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col37.Text = 0 Then
    '        '        col37.Text = Math.Round(col37.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col38.Text = 0 Then
    '        '        col38.Text = Math.Round(col38.Text / 5) * 5 + 5
    '        '    End If
    '        '    If Not col39.Text = 0 Then
    '        '        col39.Text = Math.Round(col39.Text / 5) * 5 + 5
    '        '    End If

    '        '    Jumlah.Text = 0

    '        '    Jumlah.Text = Jumlah.Text + Convert.ToInt32(col0.Text) + Convert.ToInt32(col1.Text) + Convert.ToInt32(col2.Text) + Convert.ToInt32(col3.Text) + Convert.ToInt32(col4.Text) + Convert.ToInt32(col5.Text) + Convert.ToInt32(col6.Text) + Convert.ToInt32(col7.Text) + Convert.ToInt32(col8.Text) + Convert.ToInt32(col9.Text)
    '        '    Jumlah.Text = Jumlah.Text + Convert.ToInt32(col10.Text) + Convert.ToInt32(col11.Text) + Convert.ToInt32(col12.Text) + Convert.ToInt32(col13.Text) + Convert.ToInt32(col14.Text) + Convert.ToInt32(col15.Text) + Convert.ToInt32(col16.Text) + Convert.ToInt32(col17.Text) + Convert.ToInt32(col18.Text) + Convert.ToInt32(col19.Text)
    '        '    Jumlah.Text = Jumlah.Text + Convert.ToInt32(col20.Text) + Convert.ToInt32(col21.Text) + Convert.ToInt32(col22.Text) + Convert.ToInt32(col23.Text) + Convert.ToInt32(col24.Text) + Convert.ToInt32(col25.Text) + Convert.ToInt32(col26.Text) + Convert.ToInt32(col27.Text) + Convert.ToInt32(col28.Text) + Convert.ToInt32(col29.Text)
    '        '    Jumlah.Text = Jumlah.Text + Convert.ToInt32(col30.Text) + Convert.ToInt32(col31.Text) + Convert.ToInt32(col32.Text) + Convert.ToInt32(col33.Text) + Convert.ToInt32(col34.Text) + Convert.ToInt32(col35.Text) + Convert.ToInt32(col36.Text) + Convert.ToInt32(col37.Text) + Convert.ToInt32(col38.Text) + Convert.ToInt32(col39.Text)

    '        'Next

    '    End If





    'End Sub



    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnExport.Click
        Try
            lblMsg.Text = ""

            If ddlSemester.Text = "1" Then
                ExportToCSV(datRespondent)
            ElseIf ddlSemester.Text = "2" Then
                ExportToCSV(datRespondent2)
            ElseIf ddlSemester.Text = "3" Then
                ExportToCSV(datRespondent3)
            ElseIf ddlSemester.Text = "4" Then
                ExportToCSV(datRespondent4)
            End If

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message
        End Try

    End Sub

    Private Sub ExportToCSV(ByVal gv As GridView)
        'Get the data from database into datatable 
        Response.Clear()
        Response.Buffer = True
        Response.AddHeader("content-disposition", "attachment;filename=Matapelajaran_Vokasional-Tahun" & ddlTahun.Text & "Sem" & ddlSemester.Text & ".csv")
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

                Dim columnValue As Label = gv.Rows(i).FindControl(gv.Columns(k).HeaderText)
                If columnValue Is Nothing Then
                    sb.Append("")
                Else
                    sb.Append(columnValue.Text)
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

    Private Sub ddlTahun_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTahun.SelectedIndexChanged
        kpmkv_semester_list()
    End Sub



    Private Sub ExportInden(ByVal strQuery As String)
        'Get the data from database into datatable 
        Dim cmd As New SqlCommand(strQuery)
        Dim dt As DataTable = GetData(cmd)

        Response.Clear()
        Response.Buffer = True
        Response.AddHeader("content-disposition", "attachment;filename=janaFIKsemasa_Tahun" & ddlTahun.Text & "_Semester" & ddlSemester.Text & ".csv")
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

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        ExportInden(getSQL)
    End Sub
End Class