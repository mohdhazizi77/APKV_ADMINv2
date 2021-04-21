Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Globalization

Public Class markah_PA
    Inherits System.Web.UI.UserControl
    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim IntTakwim As Integer = 0

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                '------exist takwim
                strSQL = "SELECT * FROM kpmkv_takwim WHERE Tahun='" & Now.Year & "' AND SubMenuText='Pemeriksa Borang Markah PA' AND Aktif='1'"
                If oCommon.isExist(strSQL) = True Then

                    'count data takwim
                    'Get the data from database into datatable
                    Dim cmd As New SqlCommand("SELECT TakwimID FROM kpmkv_takwim WHERE Tahun='" & Now.Year & "' AND SubMenuText='Pemeriksa Borang Markah PA' AND Aktif='1'")
                    Dim dt As DataTable = GetData(cmd)

                    For i As Integer = 0 To dt.Rows.Count - 1
                        IntTakwim = dt.Rows(i)("TakwimID")

                        strSQL = "SELECT TarikhMula,TarikhAkhir FROM kpmkv_takwim WHERE TakwimID='" & IntTakwim & "'"
                        strRet = oCommon.getFieldValueEx(strSQL)

                        Dim ar_user_login As Array
                        ar_user_login = strRet.Split("|")
                        Dim strMula As String = ar_user_login(0)
                        Dim strAkhir As String = ar_user_login(1)

                        Dim strdateNow As Date = Date.Now.Date
                        Dim startDate = DateTime.ParseExact(strMula, "dd-MM-yyyy", CultureInfo.InvariantCulture)
                        Dim endDate = DateTime.ParseExact(strAkhir, "dd-MM-yyyy", CultureInfo.InvariantCulture)

                        Dim ts As New TimeSpan
                        ts = startDate.Subtract(strdateNow)
                        Dim dayDiffMula = ts.Days
                        ts = endDate.Subtract(strdateNow)
                        Dim dayDiffAkhir = ts.Days

                        If strMula IsNot Nothing And dayDiffMula <= 0 Then
                            If strAkhir IsNot Nothing And dayDiffAkhir >= 0 Then

                                strSQL = "SELECT UserID FROM kpmkv_users WHERE LoginID='" & Session("LoginID") & "' AND Pwd = '" & Session("Password") & "'"
                                lblUserId.Text = oCommon.getFieldValue(strSQL)

                                strSQL = "SELECT UserType FROM kpmkv_users WHERE LoginID='" & Session("LoginID") & "' AND Pwd = '" & Session("Password") & "'"
                                lblUserType.Text = oCommon.getFieldValue(strSQL)

                                kpmkv_Kohort()

                                kpmkv_kolej_list()


                                kpmkv_semester_list()

                                'checkinbox
                                strSQL = "SELECT Sesi FROM kpmkv_takwim WHERE TakwimId='" & IntTakwim & "'ORDER BY Kohort ASC"
                                strRet = oCommon.getFieldValue(strSQL)

                                If strRet = 1 Then
                                    chkSesi.Items(0).Enabled = True
                                    chkSesi.Items(0).Selected = True
                                    ' chkSesi.Items(1).Enabled = False
                                Else
                                    ' chkSesi.Items(0).Enabled = False
                                    chkSesi.Items(1).Enabled = True
                                End If
                                btnCari.Enabled = True
                                ddlTahun.Enabled = True
                                ddlKodPusat.Enabled = True
                                ddlSemester.Enabled = True
                                lblMsg.Text = ""

                            End If
                        Else
                            btnCari.Enabled = False
                            ddlTahun.Enabled = False
                            ddlKodPusat.Enabled = False
                            ddlSemester.Enabled = False
                            lblMsg.Text = "Pemeriksa Borang Markah PA telah ditutup!"
                        End If
                    Next
                Else
                    btnCari.Enabled = False
                    ddlTahun.Enabled = False
                    ddlKodPusat.Enabled = False
                    ddlSemester.Enabled = False
                    lblMsg.Text = "Pemeriksa Borang Markah PA telah ditutup!"
                End If
                RepoveDuplicate(ddlTahun)
                RepoveDuplicate(ddlKodPusat)
                RepoveDuplicate(ddlSemester)
            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message
        End Try
    End Sub

    Private Shared Function RepoveDuplicate(ByVal ddl As DropDownList) As DropDownList
        For Row As Int16 = 0 To ddl.Items.Count - 2
            For RowAgain As Int16 = ddl.Items.Count - 1 To Row + 1 Step -1
                If ddl.Items(Row).ToString = ddl.Items(RowAgain).ToString Then
                    ddl.Items.RemoveAt(RowAgain)
                End If
            Next
        Next
        Return ddl
    End Function

    Private Sub kpmkv_Kohort()
        strSQL = "SELECT Kohort FROM kpmkv_takwim WHERE TakwimId='" & IntTakwim & "'ORDER BY Kohort ASC"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlTahun.DataSource = ds
            ddlTahun.DataTextField = "Kohort"
            ddlTahun.DataValueField = "Kohort"
            ddlTahun.DataBind()

            '--ALL
            ddlTahun.Items.Add(New ListItem("-Pilih-", ""))

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub kpmkv_kolej_list()
        If lblUserType.Text = "ADMIN" Then
            strSQL = " SELECT KodKolej FROM kpmkv_pemeriksa WHERE Tahun='" & Now.Year & "' ORDER By KodKolej"
        Else
            strSQL = " SELECT  Distinct KodKolej FROM kpmkv_pemeriksa WHERE UserID='" & lblUserId.Text & "' AND Tahun='" & ddlTahun.SelectedValue & "'"

        End If

        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlKodPusat.DataSource = ds
            ddlKodPusat.DataTextField = "KodKolej"
            ddlKodPusat.DataValueField = "KodKolej"
            ddlKodPusat.DataBind()

            '--ALL
            'ddlKolej.Items.Add(New ListItem("-Pilih-", "0"))

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub kpmkv_semester_list()

        strSQL = "SELECT * FROM kpmkv_takwim WHERE Tahun='" & Now.Year & "' AND SubMenuText='Pemeriksa Borang Markah PA' AND Aktif='1'"
        If oCommon.isExist(strSQL) = True Then

            'count data takwim
            'Get the data from database into datatable
            Dim cmd As New SqlCommand("SELECT TakwimID FROM kpmkv_takwim WHERE Tahun='" & Now.Year & "' AND SubMenuText='Pemeriksa Borang Markah PA' AND Aktif='1'")
            Dim dt As DataTable = GetData(cmd)

            For i As Integer = 0 To dt.Rows.Count - 1
                IntTakwim = dt.Rows(i)("TakwimID")

                strSQL = "SELECT TarikhMula,TarikhAkhir FROM kpmkv_takwim WHERE TakwimID='" & IntTakwim & "'"
                strRet = oCommon.getFieldValueEx(strSQL)

                Dim ar_user_login As Array
                ar_user_login = strRet.Split("|")
                Dim strMula As String = ar_user_login(0)
                Dim strAkhir As String = ar_user_login(1)

                Dim strdateNow As Date = Date.Now.Date
                Dim startDate = DateTime.ParseExact(strMula, "dd-MM-yyyy", CultureInfo.InvariantCulture)
                Dim endDate = DateTime.ParseExact(strAkhir, "dd-MM-yyyy", CultureInfo.InvariantCulture)

                Dim ts As New TimeSpan
                ts = startDate.Subtract(strdateNow)
                Dim dayDiffMula = ts.Days
                ts = endDate.Subtract(strdateNow)
                Dim dayDiffAkhir = ts.Days

                If strMula IsNot Nothing And dayDiffMula <= 0 Then
                    If strAkhir IsNot Nothing And dayDiffAkhir >= 0 Then

                        strSQL = "SELECT UserID FROM kpmkv_users WHERE LoginID='" & Session("LoginID") & "' AND Pwd = '" & Session("Password") & "'"
                        lblUserId.Text = oCommon.getFieldValue(strSQL)

                        strSQL = "SELECT UserType FROM kpmkv_users WHERE LoginID='" & Session("LoginID") & "' AND Pwd = '" & Session("Password") & "'"
                        lblUserType.Text = oCommon.getFieldValue(strSQL)

                        strSQL = "SELECT Semester FROM kpmkv_takwim WHERE TakwimId='" & IntTakwim & "'ORDER BY Semester ASC"
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
                            lblMsg.Text = "System Error:" & ex.Message

                        Finally
                            objConn.Dispose()
                        End Try

                        'checkinbox
                        strSQL = "SELECT Sesi FROM kpmkv_takwim WHERE TakwimId='" & IntTakwim & "'ORDER BY Kohort ASC"
                        strRet = oCommon.getFieldValue(strSQL)

                        If strRet = 1 Then
                            chkSesi.Items(0).Enabled = True
                            chkSesi.Items(0).Selected = True
                            ' chkSesi.Items(1).Enabled = False
                        Else
                            ' chkSesi.Items(0).Enabled = False
                            chkSesi.Items(1).Enabled = True
                        End If
                        btnCari.Enabled = True
                        ddlTahun.Enabled = True
                        ddlKodPusat.Enabled = True
                        ddlSemester.Enabled = True
                        lblMsg.Text = ""

                    End If
                Else
                    btnCari.Enabled = False
                    ddlTahun.Enabled = False
                    ddlKodPusat.Enabled = False
                    ddlSemester.Enabled = False
                    lblMsg.Text = "Pemeriksa Borang Markah PA telah ditutup!"
                End If
            Next
        Else
            btnCari.Enabled = False
            ddlTahun.Enabled = False
            ddlKodPusat.Enabled = False
            ddlSemester.Enabled = False
            lblMsg.Text = "Pemeriksa Borang Markah PA telah ditutup!"
        End If

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
                lblMsg.Text = "Tiada rekod pemeriksa."
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
        Dim strOrder As String = " ORDER BY kpmkv_pemeriksa.Tahun, kpmkv_pemeriksa.Semester, kpmkv_pemeriksa.Sesi ASC"

        If lblUserType.Text = "ADMIN" Then
            tmpSQL = " SELECT PemeriksaID,Tahun,Semester,Sesi,KodKolej,KodKursus FROM kpmkv_pemeriksa "
            strWhere = " WHERE KodKolej='" & ddlKodPusat.Text & "'"
            strWhere += " AND Sesi='" & chkSesi.Text & "' AND Tahun='" & Now.Year & "' AND Semester='" & ddlSemester.Text & "'"
            ''--debug
            ''Response.Write(getSQL)
        Else
            tmpSQL = " SELECT PemeriksaID,Tahun,Semester,Sesi,KodKolej,KodKursus FROM  kpmkv_pemeriksa "
            strWhere = " WHERE KodKolej='" & ddlKodPusat.Text & "' AND kpmkv_pemeriksa.UserID='" & lblUserId.Text & "'"
            strWhere += " AND Sesi='" & chkSesi.Text & "' AND Tahun='" & ddlTahun.SelectedValue & "' AND Semester='" & ddlSemester.Text & "'"

        End If
        getSQL = tmpSQL & strWhere & strOrder
        Return getSQL

    End Function

    Protected Sub datRespondent_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles datRespondent.RowCommand
        lblMsg.Text = ""
        If (e.CommandName = "Pilih") = True Then


            Dim strkeyID = Int32.Parse(e.CommandArgument.ToString())

            Response.Redirect("apkv.borang.markah.PA.kertas.aspx?PemeriksaID=" & strkeyID)
        End If
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

    Protected Sub btnCari_Click(sender As Object, e As EventArgs) Handles btnCari.Click
        lblMsg.Text = ""
        strRet = BindData(datRespondent)
    End Sub

    Private Sub ddlTahun_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTahun.SelectedIndexChanged
        kpmkv_kolej_list()
        kpmkv_semester_list()

    End Sub

    Private Sub ddlKodPusat_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlKodPusat.SelectedIndexChanged
        kpmkv_semester_list()
    End Sub
End Class