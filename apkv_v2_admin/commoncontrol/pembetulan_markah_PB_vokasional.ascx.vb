Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Public Class pembetulan_markah_PB_vokasional
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                kpmkv_negeri_list()
                ddlNegeri.Text = "0"

                kpmkv_jenis_list()
                ddlJenis.Text = "0"

                kpmkv_kolej_list()
                ddlKolej.Text = "0"

                kpmkv_tahun_list()
                ddlTahun.Text = "0"

                kpmkv_semester_list()

                kpmkv_kodkursus_list()



                kpmkv_kelas_list()
                lblMsg.Text = ""

            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message
        End Try
    End Sub
    Private Sub kpmkv_negeri_list()
        strSQL = "SELECT Negeri FROM kpmkv_negeri ORDER BY Negeri"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlNegeri.DataSource = ds
            ddlNegeri.DataTextField = "Negeri"
            ddlNegeri.DataValueField = "Negeri"
            ddlNegeri.DataBind()

            '--ALL
            ddlNegeri.Items.Add(New ListItem("-Pilih-", "0"))

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub
    Private Sub kpmkv_jenis_list()
        strSQL = "SELECT Jenis FROM kpmkv_jeniskolej  ORDER BY Jenis"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlJenis.DataSource = ds
            ddlJenis.DataTextField = "Jenis"
            ddlJenis.DataValueField = "Jenis"
            ddlJenis.DataBind()

            '--ALL
            ddlJenis.Items.Add(New ListItem("-Pilih-", "0"))

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub
    Private Sub kpmkv_kolej_list()
        strSQL = "SELECT Nama,RecordID FROM kpmkv_kolej WHERE Negeri='" & ddlNegeri.SelectedItem.Value & "' AND Jenis='" & ddlJenis.SelectedValue & "'"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlKolej.DataSource = ds
            ddlKolej.DataTextField = "Nama"
            ddlKolej.DataValueField = "RecordID"
            ddlKolej.DataBind()

            '--ALL
            ddlKolej.Items.Add(New ListItem("-Pilih-", "0"))

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub
    Private Sub hiddencolumn()
        strSQL = "SELECT COUNT(kpmkv_modul.KodModul) as CModul "
        strSQL += " FROM kpmkv_modul LEFT OUTER JOIN"
        strSQL += " kpmkv_kursus ON kpmkv_modul.KursusID = kpmkv_kursus.KursusID"
        strSQL += " WHERE kpmkv_modul.Tahun='" & ddlTahun.Text & "'"
        strSQL += " AND kpmkv_modul.Semester='" & ddlSemester.Text & "'"
        strSQL += " AND kpmkv_modul.Sesi='" & chkSesi.Text & "'"
        strSQL += " AND kpmkv_modul.KursusID='" & ddlKodKursus.SelectedValue & "'"
        strRet = oCommon.getFieldValue(strSQL)

        Select Case strRet
            Case "2"
                datRespondent.Columns.Item("9").Visible = False
                datRespondent.Columns.Item("10").Visible = False
                datRespondent.Columns.Item("11").Visible = False
                datRespondent.Columns.Item("12").Visible = False
                datRespondent.Columns.Item("13").Visible = False
                datRespondent.Columns.Item("14").Visible = False
                datRespondent.Columns.Item("15").Visible = False
                datRespondent.Columns.Item("16").Visible = False
                datRespondent.Columns.Item("17").Visible = False
                datRespondent.Columns.Item("18").Visible = False
                datRespondent.Columns.Item("19").Visible = False
                datRespondent.Columns.Item("20").Visible = False

            Case "3"
                datRespondent.Columns.Item("11").Visible = False
                datRespondent.Columns.Item("12").Visible = False
                datRespondent.Columns.Item("13").Visible = False
                datRespondent.Columns.Item("14").Visible = False
                datRespondent.Columns.Item("15").Visible = False
                datRespondent.Columns.Item("16").Visible = False
                datRespondent.Columns.Item("17").Visible = False
                datRespondent.Columns.Item("18").Visible = False
                datRespondent.Columns.Item("19").Visible = False
                datRespondent.Columns.Item("20").Visible = False

            Case "4"
                datRespondent.Columns.Item("13").Visible = False
                datRespondent.Columns.Item("14").Visible = False
                datRespondent.Columns.Item("15").Visible = False
                datRespondent.Columns.Item("16").Visible = False
                datRespondent.Columns.Item("17").Visible = False
                datRespondent.Columns.Item("18").Visible = False
                datRespondent.Columns.Item("19").Visible = False
                datRespondent.Columns.Item("20").Visible = False

            Case "5"
                datRespondent.Columns.Item("15").Visible = False
                datRespondent.Columns.Item("16").Visible = False
                datRespondent.Columns.Item("17").Visible = False
                datRespondent.Columns.Item("18").Visible = False
                datRespondent.Columns.Item("19").Visible = False
                datRespondent.Columns.Item("20").Visible = False

            Case "6"
                datRespondent.Columns.Item("17").Visible = False
                datRespondent.Columns.Item("18").Visible = False
                datRespondent.Columns.Item("19").Visible = False
                datRespondent.Columns.Item("20").Visible = False

            Case "7"
                datRespondent.Columns.Item("17").Visible = False
                datRespondent.Columns.Item("18").Visible = False
                datRespondent.Columns.Item("19").Visible = False
                datRespondent.Columns.Item("20").Visible = False
        End Select


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
            ddlTahun.Items.Add(New ListItem("-Pilih-", "0"))

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub
    Private Sub kpmkv_semester_list()
        strSQL = "SELECT Semester FROM kpmkv_semester  ORDER BY Semester"
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

    End Sub
    Private Sub kpmkv_kodkursus_list()

        strSQL = "SELECT kpmkv_kursus.KodKursus, kpmkv_kursus.KursusID"
        strSQL += " FROM kpmkv_kelas_kursus INNER JOIN kpmkv_kursus ON kpmkv_kelas_kursus.KursusID = kpmkv_kursus.KursusID INNER JOIN"
        strSQL += " kpmkv_kelas ON kpmkv_kelas_kursus.KelasID = kpmkv_kelas.KelasID"
        strSQL += " WHERE kpmkv_kelas.KolejRecordID='" & ddlKolej.SelectedValue & "' AND kpmkv_kursus.Tahun='" & ddlTahun.Text & "' AND kpmkv_kursus.Sesi='" & chkSesi.SelectedValue & "' ORDER BY kpmkv_kursus.KodKursus"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlKodKursus.DataSource = ds
            ddlKodKursus.DataTextField = "KodKursus"
            ddlKodKursus.DataValueField = "KursusID"
            ddlKodKursus.DataBind()

            '--ALL
            ' ddlKodKursus.Items.Add(New ListItem("PILIH", "PILIH"))

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub
    Private Sub kpmkv_kelas_list()
        strSQL = " SELECT kpmkv_kelas.NamaKelas, kpmkv_kelas.KelasID"
        strSQL += " FROM  kpmkv_kelas_kursus LEFT OUTER JOIN kpmkv_kelas ON kpmkv_kelas_kursus.KelasID = kpmkv_kelas.KelasID LEFT OUTER JOIN"
        strSQL += " kpmkv_kursus ON kpmkv_kelas_kursus.KursusID = kpmkv_kursus.KursusID"
        strSQL += " WHERE kpmkv_kelas.KolejRecordID='" & ddlKolej.SelectedValue & "' AND kpmkv_kelas_kursus.KursusID= '" & ddlKodKursus.SelectedValue & "' ORDER BY KelasID"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlKelas.DataSource = ds
            ddlKelas.DataTextField = "NamaKelas"
            ddlKelas.DataValueField = "KelasID"
            ddlKelas.DataBind()

            '--ALL
            'ddlNamaKelas.Items.Add(New ListItem("PILIH", "PILIH"))

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub kpmkv_modul_list()

        If datRespondent.Rows.Count = 1 Then

            For i = 0 To datRespondent.Rows.Count - 1

                strSQL = "SELECT KursusID FROM kpmkv_pelajar WHERE PelajarID = '" & datRespondent.DataKeys(i).Value.ToString & "'"
                Dim KursusID As String = oCommon.getFieldValue(strSQL)

                strSQL = "  SELECT KodModul FROM kpmkv_modul WHERE Semester = '" & ddlSemester.Text & "' AND Sesi = '" & chkSesi.Text & "' AND KursusID = '" & KursusID & "'"

                Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
                Dim objConn As SqlConnection = New SqlConnection(strConn)
                Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

                Try
                    Dim ds As DataSet = New DataSet
                    sqlDA.Fill(ds, "AnyTable")

                    ddlModul.DataSource = ds
                    ddlModul.DataTextField = "KodModul"
                    ddlModul.DataValueField = "KodModul"
                    ddlModul.DataBind()

                    '--ALL

                Catch ex As Exception
                    lblMsg.Text = "System Error:" & ex.Message

                Finally
                    objConn.Dispose()
                End Try

            Next

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

    Private Function BindDataAudit(ByVal gvTable As GridView) As Boolean
        Dim myDataSet As New DataSet
        Dim myDataAdapter As New SqlDataAdapter(getSQLAudit, strConn)
        myDataAdapter.SelectCommand.CommandTimeout = 120
        Try
            myDataAdapter.Fill(myDataSet, "myaccount")

            If myDataSet.Tables(0).Rows.Count = 0 Then
                divMsgAudit.Attributes("class") = "error"
                lblMsgAudit.Text = "Tiada rekod."
            Else
                divMsgAudit.Attributes("class") = "info"
                lblMsgAudit.Text = "Jumlah rekod#:" & myDataSet.Tables(0).Rows.Count
            End If

            gvTable.DataSource = myDataSet
            gvTable.DataBind()
            objConn.Close()

        Catch ex As Exception
            lblMsgAudit.Text = "Error:" & ex.Message
            Return False
        End Try

        Return True

    End Function

    Private Function getSQL() As String

        Dim tmpSQL As String = ""
        Dim strWhere As String = ""
        Dim strOrder As String = " ORDER BY kpmkv_pelajar.Nama ASC"

        tmpSQL = "SELECT kpmkv_pelajar.PelajarID,  kpmkv_pelajar.Nama, kpmkv_pelajar.MYKAD, kpmkv_pelajar.AngkaGiliran, kpmkv_kursus.KodKursus, "
        tmpSQL += " kpmkv_pelajar_markah.B_Amali1, kpmkv_pelajar_markah.B_Amali2, kpmkv_pelajar_markah.B_Amali3, kpmkv_pelajar_markah.B_Amali4,"
        tmpSQL += " kpmkv_pelajar_markah.B_Amali5, kpmkv_pelajar_markah.B_Amali6, kpmkv_pelajar_markah.B_Amali7, kpmkv_pelajar_markah.B_Amali8,"
        tmpSQL += " kpmkv_pelajar_markah.B_Teori1, kpmkv_pelajar_markah.B_Teori2, kpmkv_pelajar_markah.B_Teori3, kpmkv_pelajar_markah.B_Teori4,"
        tmpSQL += " kpmkv_pelajar_markah.B_Teori5, kpmkv_pelajar_markah.B_Teori6, kpmkv_pelajar_markah.B_Teori7, kpmkv_pelajar_markah.B_Teori8"
        tmpSQL += " FROM  kpmkv_pelajar_markah LEFT OUTER JOIN kpmkv_pelajar ON kpmkv_pelajar_markah.PelajarID = kpmkv_pelajar.PelajarID"
        tmpSQL += " LEFT OUTER JOIN kpmkv_kursus ON kpmkv_pelajar.KursusID = kpmkv_kursus.KursusID"
        tmpSQL += " LEFT OUTER Join kpmkv_kolej ON kpmkv_kolej.RecordID = kpmkv_pelajar.KolejRecordID"

        strWhere = " WHERE kpmkv_pelajar.IsDeleted='N' AND kpmkv_pelajar.StatusID='2'"

        '--negeri
        If Not ddlNegeri.SelectedItem.Text = "-Pilih-" Then
            strWhere += " AND kpmkv_kolej.Negeri='" & ddlNegeri.SelectedItem.Text & "'"
        End If
        '--kolej
        If Not ddlKolej.SelectedItem.Text = "-Pilih-" Then
            strWhere += " AND kpmkv_pelajar.KolejRecordID='" & ddlKolej.SelectedValue & "'"
        End If
        '--tahun
        If Not ddlTahun.Text = "0" Then
            strWhere += " AND kpmkv_pelajar.Tahun ='" & ddlTahun.Text & "'"
        End If
        '--semester
        If Not ddlSemester.Text = "-Pilih-" Then
            strWhere += " AND kpmkv_pelajar.Semester ='" & ddlSemester.Text & "'"
        End If
        '--sesi
        If Not chkSesi.Text = "" Then
            strWhere += " AND kpmkv_pelajar.Sesi ='" & chkSesi.Text & "'"
        End If
        '--kursus
        If Not ddlKodKursus.Text = "" Then
            strWhere += " AND kpmkv_pelajar.KursusID ='" & ddlKodKursus.SelectedValue & "'"
        End If
        '--jantina
        If Not ddlKelas.Text = "" Then
            strWhere += " AND kpmkv_pelajar.KelasID ='" & ddlKelas.SelectedValue & "'"
        End If
        If Not txtMykad.Text = "" Then
            strWhere += " AND kpmkv_pelajar.Mykad ='" & txtMykad.Text & "'"
        End If
        If Not txtAngkaGiliran.Text = "" Then
            strWhere += " AND kpmkv_pelajar.AngkaGiliran ='" & txtAngkaGiliran.Text & "'"
        End If

        getSQL = tmpSQL & strWhere & strOrder
        ''--debug
        ''Response.Write(getSQL)

        Return getSQL

    End Function

    Private Function getSQLAudit() As String

        Dim tmpSQL As String = ""

        strSQL = "  SELECT PelajarID FROM kpmkv_pelajar 
                        WHERE MYKAD = '" & txtMykad.Text & "' 
                        AND Sesi = '" & chkSesi.Text & "'
                        AND Semester = '" & ddlSemester.Text & "'"


        If Not txtAngkaGiliran.Text = "" Then
            strSQL += " And AngkaGiliran = '" & txtAngkaGiliran.Text & "'"
        End If

        Dim strPelajarID As String = oCommon.getFieldValue(strSQL)


        tmpSQL = "  SELECT TOP(10)
                    A.markahAuditID, A.dateTime,
                    B.LoginID, 
                    C.Nama, C.AngkaGiliran, 
                    A.Tahun, A.Semester, A.Sesi, A.Menu, 
                    E.KodModul, E.NamaModul,
                    A.MarkahSebelum, A.MarkahSelepas, A.Catatan, A.Jenis
                    FROM kpmkv_pelajar_markah_audit A
                    LEFT JOIN kpmkv_users B ON B.UserID = A.UserID
                    LEFT JOIN kpmkv_pelajar C ON C.PelajarID = A.PelajarID
					LEFT JOIN kpmkv_kursus D ON D.KursusID = D.KursusID
                    LEFT JOIN kpmkv_modul E ON E.ModulID = A.MataPelajaranId                    
                    WHERE
                    A.PelajarID = '" & strPelajarID & "'
                    AND A.Menu = 'PB VOKASIONAL'
					GROUP BY
					A.markahAuditID, A.dateTime,
                    B.LoginID, 
                    C.Nama, C.AngkaGiliran, 
                    A.Tahun, A.Semester, A.Sesi, A.Menu, 
                    E.KodModul, E.NamaModul,
                    A.MarkahSebelum, A.MarkahSelepas, A.Catatan, A.Jenis
                    ORDER BY A.dateTime DESC"

        getSQLAudit = tmpSQL
        Debug.WriteLine(getSQLAudit)

        Return getSQLAudit

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

    Protected Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click

        lblMsg.Text = ""
        If ValidateForm() = False Then
            lblMsg.Text = "Sila masukkan NOMBOR SAHAJA. 0-100"
            Exit Sub
        End If

        Dim markahSebelum As String = ""
        Dim markahSelepas As String = ""
        Dim querySebelum As String = ""

        If datRespondent.Rows.Count = 1 Then

            For i As Integer = 0 To datRespondent.Rows.Count - 1

                '-- get marks before update

                strSQL = " SELECT "

                If ddlModul.SelectedIndex = 0 Then

                    If ddlKertas.SelectedValue = 1 Then

                        strSQL += " B_Amali1 "

                    Else

                        strSQL += " B_Teori1 "

                    End If

                ElseIf ddlModul.SelectedIndex = 1 Then

                    If ddlKertas.SelectedValue = 1 Then

                        strSQL += " B_Amali2 "

                    Else

                        strSQL += " B_Teori2 "

                    End If

                ElseIf ddlModul.SelectedIndex = 2 Then

                    If ddlKertas.SelectedValue = 1 Then

                        strSQL += " B_Amali3 "

                    Else

                        strSQL += " B_Teori3 "

                    End If

                ElseIf ddlModul.SelectedIndex = 3 Then

                    If ddlKertas.SelectedValue = 1 Then

                        strSQL += " B_Amali4 "

                    Else

                        strSQL += " B_Teori4 "

                    End If

                ElseIf ddlModul.SelectedIndex = 4 Then

                    If ddlKertas.SelectedValue = 1 Then

                        strSQL += " B_Amali5 "

                    Else

                        strSQL += " B_Teori5 "

                    End If

                ElseIf ddlModul.SelectedIndex = 5 Then

                    If ddlKertas.SelectedValue = 1 Then

                        strSQL += " B_Amali6 "

                    Else

                        strSQL += " B_Teori6 "

                    End If

                ElseIf ddlModul.SelectedIndex = 6 Then

                    If ddlKertas.SelectedValue = 1 Then

                        strSQL += " B_Amali7 "

                    Else

                        strSQL += " B_Teori7 "

                    End If

                ElseIf ddlModul.SelectedIndex = 7 Then

                    If ddlKertas.SelectedValue = 1 Then

                        strSQL += " B_Amali8 "

                    Else

                        strSQL += " B_Teori8 "

                    End If

                End If

                strSQL += " FROM kpmkv_pelajar_markah
                            WHERE PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"

                markahSebelum = oCommon.getFieldValue(strSQL)
                querySebelum = strSQL

                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

                '-- update marks
                strSQL = "UPDATE kpmkv_pelajar_markah SET"

                If ddlModul.SelectedIndex = 0 Then

                    If ddlKertas.SelectedValue = 1 Then

                        strSQL += " B_Amali1='" & txtMarkah.Text & "'"

                    Else

                        strSQL += " B_Teori1='" & txtMarkah.Text & "'"

                    End If

                ElseIf ddlModul.SelectedIndex = 1 Then

                    If ddlKertas.SelectedValue = 1 Then

                        strSQL += " B_Amali2='" & txtMarkah.Text & "'"

                    Else

                        strSQL += " B_Teori2='" & txtMarkah.Text & "'"

                    End If

                ElseIf ddlModul.SelectedIndex = 2 Then

                    If ddlKertas.SelectedValue = 1 Then

                        strSQL += " B_Amali3='" & txtMarkah.Text & "'"

                    Else

                        strSQL += " B_Teori3='" & txtMarkah.Text & "'"

                    End If

                ElseIf ddlModul.SelectedIndex = 3 Then

                    If ddlKertas.SelectedValue = 1 Then

                        strSQL += " B_Amali4='" & txtMarkah.Text & "'"

                    Else

                        strSQL += " B_Teori4='" & txtMarkah.Text & "'"

                    End If

                ElseIf ddlModul.SelectedIndex = 4 Then

                    If ddlKertas.SelectedValue = 1 Then

                        strSQL += " B_Amali5='" & txtMarkah.Text & "'"

                    Else

                        strSQL += " B_Teori5='" & txtMarkah.Text & "'"

                    End If

                ElseIf ddlModul.SelectedIndex = 5 Then

                    If ddlKertas.SelectedValue = 1 Then

                        strSQL += " B_Amali6='" & txtMarkah.Text & "'"

                    Else

                        strSQL += " B_Teori6='" & txtMarkah.Text & "'"

                    End If

                ElseIf ddlModul.SelectedIndex = 6 Then

                    If ddlKertas.SelectedValue = 1 Then

                        strSQL += " B_Amali7='" & txtMarkah.Text & "'"

                    Else

                        strSQL += " B_Teori7='" & txtMarkah.Text & "'"

                    End If

                ElseIf ddlModul.SelectedIndex = 7 Then

                    If ddlKertas.SelectedValue = 1 Then

                        strSQL += " B_Amali8='" & txtMarkah.Text & "'"

                    Else

                        strSQL += " B_Teori8='" & txtMarkah.Text & "'"

                    End If

                End If

                strSQL += " WHERE PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"

                strRet = oCommon.ExecuteSQL(strSQL)

                '-- get marks after update
                markahSelepas = txtMarkah.Text

                If Not strRet = "0" Then
                    divMsgResult.Attributes("class") = "error"
                    lblMsgResult.Text = "Tidak Berjaya mengemaskini markah Pentaksiran Berterusan Vokasional"

                Else

                    divMsgResult.Attributes("class") = "info"
                    lblMsgResult.Text = "Berjaya mengemaskini markah Pentaksiran Berterusan Vokasional"

                    '-- new function to insert audit trails PBV
                    auditTrailsInsert(datRespondent.DataKeys(i).Value.ToString, markahSebelum, markahSelepas, "KEMASKINI")

                    strRet = BindData(datRespondent)
                    strRet = BindDataAudit(datRespondentAudit)

                End If

            Next

        Else

            divMsgResult.Attributes("class") = "error"
            lblMsgResult.Text = "Tidak Berjaya mengemaskini markah Pentaksiran Berterusan Vokasional"

        End If


    End Sub

    Private Sub auditTrailsInsert(ByVal PelajarID As String, ByVal markahSebelum As String, ByVal markahSelepas As String, ByVal Jenis As String)

        If datRespondent.Rows.Count = 1 Then

            For i = 0 To datRespondent.Rows.Count - 1

                Dim Tahun As String
                Dim Semester As String

                strSQL = "SELECT Tahun, Semester FROM kpmkv_pelajar WHERE PelajarID = '" & datRespondent.DataKeys(i).Value.ToString & "'"
                strRet = oCommon.getFieldValueEx(strSQL)

                Dim ar_TahunSemester As Array
                ar_TahunSemester = strRet.Split("|")

                Tahun = ar_TahunSemester(0)
                Semester = ar_TahunSemester(1)

                strSQL = "SELECT UserID FROM kpmkv_users WHERE LoginID='" & Session("LoginID") & "'"
                Dim UserID As String = oCommon.getFieldValue(strSQL)

                strSQL = "SELECT ModulID FROM kpmkv_modul WHERE KodModul = '" & ddlModul.SelectedValue & "' AND Tahun = '" & Tahun & "' AND Semester = '" & Semester & "'"
                Dim strModulID As String = oCommon.getFieldValue(strSQL)

                strSQL = "SELECT KursusID FROM kpmkv_pelajar_markah WHERE PelajarID = '" & PelajarID & "' AND Tahun = '" & Tahun & "' AND Semester = '" & Semester & "'"
                Dim strKursusID As String = oCommon.getFieldValue(strSQL)

                If Jenis = "KEMASKINI" Then

                    If ddlKertas.SelectedValue = 1 Then

                        strSQL = "  INSERT INTO kpmkv_pelajar_markah_audit 
                    ( UserID, PelajarID, KursusID, MataPelajaranID, dateTime, Tahun, Semester, Sesi, Menu, MarkahSebelum, MarkahSelepas, Catatan, Jenis )
                    VALUES
                    ( '" & UserID & "', '" & PelajarID & "', '" & strKursusID & "', '" & strModulID & "', GETDATE(), '" & Tahun & "', '" & Semester & "', '" & chkSesi.Text & "', 'PB VOKASIONAL', '" & markahSebelum & "', '" & markahSelepas & "', '" & Jenis & "', 'AMALI')"
                        strRet = oCommon.ExecuteSQL(strSQL)

                    Else

                        strSQL = "  INSERT INTO kpmkv_pelajar_markah_audit 
                    ( UserID, PelajarID, KursusID, MataPelajaranID, dateTime, Tahun, Semester, Sesi, Menu, MarkahSebelum, MarkahSelepas, Catatan, Jenis )
                    VALUES
                    ( '" & UserID & "', '" & PelajarID & "', '" & strKursusID & "', '" & strModulID & "', GETDATE(), '" & Tahun & "', '" & Semester & "', '" & chkSesi.Text & "', 'PB VOKASIONAL', '" & markahSebelum & "', '" & markahSelepas & "', '" & Jenis & "', 'TEORI')"
                        strRet = oCommon.ExecuteSQL(strSQL)

                    End If

                Else

                    strSQL = "  INSERT INTO kpmkv_pelajar_markah_audit 
                    ( UserID, PelajarID, KursusID, dateTime, Tahun, Semester, Sesi, Menu, Catatan )
                    VALUES
                    ( '" & UserID & "', '" & PelajarID & "', '" & strKursusID & "', GETDATE(), '" & Tahun & "', '" & Semester & "', '" & chkSesi.Text & "', 'PB VOKASIONAL', '" & Jenis & "')"
                    strRet = oCommon.ExecuteSQL(strSQL)

                End If

            Next

        End If



    End Sub

    Private Function ValidateForm() As Boolean

        For i As Integer = 0 To datRespondent.Rows.Count - 1

            If Not txtMarkah.Text.Length = 0 Then
                If oCommon.IsCurrency(txtMarkah.Text) = False Then
                    Return False
                End If
                If CInt(txtMarkah.Text) > 100 Then
                    Return False
                End If
            Else
                txtMarkah.Text = "0"
            End If

        Next

        Return True

    End Function

    Protected Sub ddlTahun_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTahun.SelectedIndexChanged
        kpmkv_kodkursus_list()
        kpmkv_kelas_list()
    End Sub

    Private Sub ddlSemester_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSemester.SelectedIndexChanged
        kpmkv_kodkursus_list()
        kpmkv_kelas_list()
    End Sub

    Private Sub ddlKolej_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlKolej.SelectedIndexChanged
        kpmkv_kodkursus_list()
        kpmkv_kelas_list()
    End Sub

    Protected Sub btnCari_Click(sender As Object, e As EventArgs) Handles btnCari.Click
        lblMsg.Text = ""
        strRet = BindData(datRespondent)
        If datRespondent.Rows.Count = 1 Then
            strRet = BindDataAudit(datRespondentAudit)
            kpmkv_modul_list()
        End If
        hiddencolumn()

    End Sub

    Private Sub ddlKodKursus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlKodKursus.SelectedIndexChanged
        kpmkv_kelas_list()
    End Sub
    Protected Sub ddlJenis_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlJenis.SelectedIndexChanged

        kpmkv_kolej_list()
        kpmkv_kodkursus_list()
        kpmkv_kelas_list()

    End Sub
    Private Sub chkSesi_SelectedIndexChanged(sender As Object, e As EventArgs) Handles chkSesi.SelectedIndexChanged
        kpmkv_kodkursus_list()
        kpmkv_kelas_list()
    End Sub

    Private Sub btnGred_Click(sender As Object, e As EventArgs) Handles btnGred.Click
        lblMsg.Text = ""

        If datRespondent.Rows.Count = 1 Then

            For i = 0 To datRespondent.Rows.Count - 1

                Vokasional_markah()
                Vokasional_gred()
                Vokasional_SMP_PB()
                Vokasional_SMP_PA()
                Vokasional_gredKompeten()
                Vokasional_gredSMP()
                Vokasional_MPVOK()

                If Not strRet = "0" Then
                    divMsgResult.Attributes("class") = "error"
                    lblMsgResult.Text = "Tidak Berjaya mengemaskini gred Pentaksiran Akhir Vokasional"
                Else
                    divMsgResult.Attributes("class") = "info"
                    lblMsgResult.Text = "Berjaya mengemaskini gred Pentaksiran Akhir Vokasional"
                    '-- new function to insert audit trails PBV
                    auditTrailsInsert(datRespondent.DataKeys(i).Value.ToString, "", "", "JANA GRED")
                    strRet = BindData(datRespondent)
                    If datRespondent.Rows.Count = 1 Then
                        strRet = BindDataAudit(datRespondentAudit)
                    End If

                End If

            Next

        End If


    End Sub

    Private Sub Vokasional_markah()

        If datRespondent.Rows.Count = 1 Then

            For i As Integer = 0 To datRespondent.Rows.Count - 1

                Dim Tahun As String
                Dim Semester As String

                strSQL = "SELECT Tahun, Semester FROM kpmkv_pelajar WHERE PelajarID = '" & datRespondent.DataKeys(i).Value.ToString & "'"
                strRet = oCommon.getFieldValueEx(strSQL)

                Dim ar_TahunSemester As Array
                ar_TahunSemester = strRet.Split("|")

                Tahun = ar_TahunSemester(0)
                Semester = ar_TahunSemester(1)

                strSQL = "SELECT KursusID FROM kpmkv_pelajar_markah WHERE PelajarID = '" & datRespondent.DataKeys(i).Value.ToString & "'"
                Dim KursusID As String = oCommon.getFieldValue(strSQL)

                strSQL = "SELECT KodModul from kpmkv_modul WHERE KursusID='" & KursusID & "' AND Tahun='" & Tahun & "' AND Semester='" & Semester & "' AND Sesi='" & chkSesi.Text & "' AND KodModul Like '%01%' "
                Dim strModul1 As String = oCommon.getFieldValue(strSQL) '1

                strSQL = "SELECT KodModul from kpmkv_modul WHERE KursusID='" & KursusID & "' AND Tahun='" & Tahun & "' AND Semester='" & Semester & "' AND Sesi='" & chkSesi.Text & "' AND KodModul Like '%02%' "
                Dim strModul2 As String = oCommon.getFieldValue(strSQL) '2

                strSQL = "SELECT KodModul from kpmkv_modul WHERE KursusID='" & KursusID & "' AND Tahun='" & Tahun & "' AND Semester='" & Semester & "' AND Sesi='" & chkSesi.Text & "' AND KodModul Like '%03%' "
                Dim strModul3 As String = oCommon.getFieldValue(strSQL) '3

                strSQL = "SELECT KodModul from kpmkv_modul WHERE KursusID='" & KursusID & "' AND Tahun='" & Tahun & "' AND Semester='" & Semester & "' AND Sesi='" & chkSesi.Text & "' AND KodModul Like '%04%' "
                Dim strModul4 As String = oCommon.getFieldValue(strSQL) '4

                strSQL = "SELECT KodModul from kpmkv_modul WHERE KursusID='" & KursusID & "' AND Tahun='" & Tahun & "' AND Semester='" & Semester & "' AND Sesi='" & chkSesi.Text & "' AND KodModul Like '%05%' "
                Dim strModul5 As String = oCommon.getFieldValue(strSQL) '5

                strSQL = "SELECT KodModul from kpmkv_modul WHERE KursusID='" & KursusID & "' AND Tahun='" & Tahun & "' AND Semester='" & Semester & "' AND Sesi='" & chkSesi.Text & "' AND KodModul Like '%06%' "
                Dim strModul6 As String = oCommon.getFieldValue(strSQL) '6

                strSQL = "SELECT KodModul from kpmkv_modul WHERE KursusID='" & KursusID & "' AND Tahun='" & Tahun & "' AND Semester='" & Semester & "' AND Sesi='" & chkSesi.Text & "' AND KodModul Like '%07%' "
                Dim strModul7 As String = oCommon.getFieldValue(strSQL) '7

                strSQL = "SELECT KodModul from kpmkv_modul WHERE KursusID='" & KursusID & "' AND Tahun='" & Tahun & "' AND Semester='" & Semester & "' AND Sesi='" & chkSesi.Text & "' AND KodModul Like '%08%' "
                Dim strModul8 As String = oCommon.getFieldValue(strSQL) '8

                Dim PBAmali1 As Integer
                Dim PBTeori1 As Integer
                Dim PAAmali1 As Integer
                Dim PATeori1 As Integer

                Dim B_Amali1 As Double
                Dim B_Teori1 As Double
                Dim A_Amali1 As Double
                Dim A_Teori1 As Double

                Dim PBA1 As Double
                Dim PBT1 As Double
                Dim PAA1 As Double
                Dim PAT1 As Double
                Dim PointerM1 As Integer



                'B_Amali1, B_Amali2, B_Amali3,B_Amali4, B_Amali5, B_Amali6, B_Amali7, B_Amali8, 
                'B_Teori1, B_Teori2, B_Teori3, B_Teori4, B_Teori5, B_Teori6, B_Teori7, B_Teori8, 
                'A_Amali1, A_Amali2, A_Amali3, A_Amali4, A_Amali5, A_Amali6, A_Amali7, A_Amali8,
                'A_Teori1, A_Teori2, A_Teori3, A_Teori4, A_Teori5, A_Teori6, A_Teori7, A_Teori8,
                strSQL = "Select PBAmali from kpmkv_modul Where KodModul= '" & strModul1 & "' AND KursusID='" & KursusID & "' AND Tahun='" & Tahun & "' AND Semester='" & Semester & "' AND Sesi='" & chkSesi.Text & "'"
                PBAmali1 = oCommon.getFieldValue(strSQL)

                strSQL = "Select PBTeori from kpmkv_modul Where KodModul= '" & strModul1 & "' AND KursusID='" & KursusID & "' AND Tahun='" & Tahun & "' AND Semester='" & Semester & "' AND Sesi='" & chkSesi.Text & "'"
                PBTeori1 = oCommon.getFieldValue(strSQL)

                strSQL = "Select PAAmali from kpmkv_modul Where KodModul= '" & strModul1 & "' AND KursusID='" & KursusID & "' AND Tahun='" & Tahun & "' AND Semester='" & Semester & "' AND Sesi='" & chkSesi.Text & "'"
                PAAmali1 = oCommon.getFieldValue(strSQL)

                strSQL = "Select PATeori from kpmkv_modul Where KodModul= '" & strModul1 & "' AND KursusID='" & KursusID & "' AND Tahun='" & Tahun & "' AND Semester='" & Semester & "' AND Sesi='" & chkSesi.Text & "'"
                PATeori1 = oCommon.getFieldValue(strSQL)

                strSQL = "Select B_Amali1 from kpmkv_pelajar_markah Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                'strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & KursusID & "'"
                B_Amali1 = oCommon.getFieldValue(strSQL)
                'round up
                B_Amali1 = (B_Amali1)

                strSQL = "Select B_Teori1 from kpmkv_pelajar_markah Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                'strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & KursusID & "'"
                B_Teori1 = oCommon.getFieldValue(strSQL)
                'round up
                B_Teori1 = (B_Teori1)

                strSQL = "Select A_Amali1 from kpmkv_pelajar_markah Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                'strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & KursusID & "'"
                A_Amali1 = oCommon.getFieldValue(strSQL)
                'round up
                A_Amali1 = (A_Amali1)

                strSQL = "Select A_Teori1 from kpmkv_pelajar_markah Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                'strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & KursusID & "'"
                A_Teori1 = oCommon.getFieldValueInt(strSQL)
                'round up
                A_Teori1 = (A_Teori1)

                'convert 0 if null
                If (String.IsNullOrEmpty(B_Amali1.ToString())) Then
                    B_Amali1 = 0
                End If

                If (String.IsNullOrEmpty(B_Teori1.ToString())) Then
                    B_Teori1 = 0
                End If

                If (String.IsNullOrEmpty(A_Amali1.ToString())) Then
                    A_Amali1 = 0
                End If

                If (String.IsNullOrEmpty(A_Teori1.ToString())) Then
                    A_Teori1 = 0
                End If

                If B_Amali1 = "-1" Or B_Teori1 = "-1" Or A_Amali1 = "-1" Or A_Teori1 = "-1" Then
                    strSQL = "UPDATE kpmkv_pelajar_markah SET PBPAM1='-1',"
                    strSQL += "PBAV1='-1',PBTV1='-1',PAAV1='-1',PATV1='-1'"
                    strSQL += " Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                    'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                    'strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & KursusID & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)
                Else
                    'PBM1 = (((B_Amali1 / 100) * PBAmali1) + ((B_Teori1 / 100) * PBTeori1))
                    'PAM1 = (((A_Amali1 / 100) * PAAmali1) + ((A_Teori1 / 100) * PATeori1))
                    PBA1 = ((B_Amali1 / 100) * PBAmali1)
                    PBT1 = ((B_Teori1 / 100) * PBTeori1)
                    PAA1 = ((A_Amali1 / 100) * PAAmali1)
                    PAT1 = ((A_Teori1 / 100) * PATeori1)

                    'change on 31/7/2017 PBAV1,PBTV1,PAAV1,PATV1
                    PointerM1 = Math.Ceiling(PBA1 + PBT1 + PAA1 + PAT1)
                    strSQL = "UPDATE kpmkv_pelajar_markah SET PBPAM1='" & PointerM1 & "',"
                    strSQL += "PBAV1='" & PBA1 & "',PBTV1='" & PBT1 & "',PAAV1='" & PAA1 & "',PATV1='" & PAT1 & "'"
                    strSQL += " Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                    'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                    'strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & KursusID & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)
                End If
                'Modu1------------------------
                If Not String.IsNullOrEmpty(strModul2) Then
                    Dim PBAmali2 As Integer
                    Dim PBTeori2 As Integer
                    Dim PAAmali2 As Integer
                    Dim PATeori2 As Integer

                    Dim B_Amali2 As Double
                    Dim B_Teori2 As Double
                    Dim A_Amali2 As Double
                    Dim A_Teori2 As Double
                    Dim PBA2 As Double
                    Dim PBT2 As Double
                    Dim PAA2 As Double
                    Dim PAT2 As Double
                    Dim PointerM2 As Integer

                    strSQL = "Select PBAmali from kpmkv_modul Where KodModul= '" & strModul2 & "' AND KursusID='" & KursusID & "' AND Tahun='" & Tahun & "' AND Semester='" & Semester & "' AND Sesi='" & chkSesi.Text & "'"
                    PBAmali2 = oCommon.getFieldValue(strSQL)

                    strSQL = "Select PBTeori from kpmkv_modul Where KodModul= '" & strModul2 & "' AND KursusID='" & KursusID & "' AND Tahun='" & Tahun & "' AND Semester='" & Semester & "' AND Sesi='" & chkSesi.Text & "'"
                    PBTeori2 = oCommon.getFieldValue(strSQL)

                    strSQL = "Select PAAmali from kpmkv_modul Where KodModul= '" & strModul2 & "' AND KursusID='" & KursusID & "' AND Tahun='" & Tahun & "' AND Semester='" & Semester & "' AND Sesi='" & chkSesi.Text & "'"
                    PAAmali2 = oCommon.getFieldValue(strSQL)

                    strSQL = "Select PATeori from kpmkv_modul Where KodModul= '" & strModul2 & "' AND KursusID='" & KursusID & "' AND Tahun='" & Tahun & "' AND Semester='" & Semester & "' AND Sesi='" & chkSesi.Text & "'"
                    PATeori2 = oCommon.getFieldValue(strSQL)

                    strSQL = "Select B_Amali2 from kpmkv_pelajar_markah Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                    'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                    'strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & KursusID & "'"
                    B_Amali2 = oCommon.getFieldValue(strSQL)
                    'round up
                    B_Amali2 = (B_Amali2)

                    strSQL = "Select B_Teori2 from kpmkv_pelajar_markah Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                    'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                    'strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & KursusID & "'"
                    B_Teori2 = oCommon.getFieldValue(strSQL)
                    'round up
                    B_Teori2 = (B_Teori2)

                    strSQL = "Select A_Amali2 from kpmkv_pelajar_markah Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                    'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                    'strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & KursusID & "'"
                    A_Amali2 = oCommon.getFieldValue(strSQL)
                    'round up
                    A_Amali2 = (A_Amali2)

                    strSQL = "Select A_Teori2 from kpmkv_pelajar_markah Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                    'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                    'strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & KursusID & "'"
                    A_Teori2 = oCommon.getFieldValue(strSQL)
                    'round up
                    A_Teori2 = (A_Teori2)

                    'convert 0 if null
                    If (String.IsNullOrEmpty(B_Amali2.ToString())) Then
                        B_Amali2 = 0
                    End If

                    If (String.IsNullOrEmpty(B_Teori2.ToString())) Then
                        B_Teori2 = 0
                    End If

                    If (String.IsNullOrEmpty(A_Amali2.ToString())) Then
                        A_Amali2 = 0
                    End If

                    If (String.IsNullOrEmpty(A_Teori2.ToString())) Then
                        A_Teori2 = 0
                    End If

                    If B_Amali2 = "-1" Or B_Teori2 = "-1" Or A_Amali2 = "-1" Or A_Teori2 = "-1" Then
                        strSQL = "UPDATE kpmkv_pelajar_markah SET PBPAM2='-1',"
                        strSQL += "PBAV2='-1',PBTV2='-1',PAAV2='-1',PATV2='-1'"
                        strSQL += " Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                        'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                        'strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & KursusID & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)

                    Else
                        PBA2 = ((B_Amali2 / 100) * PBAmali2)
                        PBT2 = ((B_Teori2 / 100) * PBTeori2)
                        PAA2 = ((A_Amali2 / 100) * PAAmali2)
                        PAT2 = ((A_Teori2 / 100) * PATeori2)

                        'change on 31/7/2017 PBAV1,PBTV1,PAAV1,PATV1
                        PointerM2 = Math.Ceiling(PBA2 + PBT2 + PAA2 + PAT2)
                        strSQL = "UPDATE kpmkv_pelajar_markah SET PBPAM2='" & PointerM2 & "',"
                        strSQL += "PBAV2='" & PBA2 & "',PBTV2='" & PBT2 & "',PAAV2='" & PAA2 & "',PATV2='" & PAT2 & "'"
                        strSQL += " Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                        'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                        'strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & KursusID & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)
                    End If
                End If
                'Modul2---------------------------------
                If Not String.IsNullOrEmpty(strModul3) Then
                    Dim PBAmali3 As Integer
                    Dim PBTeori3 As Integer
                    Dim PAAmali3 As Integer
                    Dim PATeori3 As Integer

                    Dim B_Amali3 As Double
                    Dim B_Teori3 As Double
                    Dim A_Amali3 As Double
                    Dim A_Teori3 As Double
                    Dim PBA3 As Double
                    Dim PBT3 As Double
                    Dim PAA3 As Double
                    Dim PAT3 As Double
                    Dim PointerM3 As Integer

                    strSQL = "Select PBAmali from kpmkv_modul Where KodModul= '" & strModul3 & "' AND KursusID='" & KursusID & "' AND Tahun='" & Tahun & "' AND Semester='" & Semester & "' AND Sesi='" & chkSesi.Text & "'"
                    PBAmali3 = oCommon.getFieldValue(strSQL)

                    strSQL = "Select PBTeori from kpmkv_modul Where KodModul= '" & strModul3 & "' AND KursusID='" & KursusID & "' AND Tahun='" & Tahun & "' AND Semester='" & Semester & "' AND Sesi='" & chkSesi.Text & "'"
                    PBTeori3 = oCommon.getFieldValue(strSQL)

                    strSQL = "Select PAAmali from kpmkv_modul Where KodModul= '" & strModul3 & "' AND KursusID='" & KursusID & "' AND Tahun='" & Tahun & "' AND Semester='" & Semester & "' AND Sesi='" & chkSesi.Text & "'"
                    PAAmali3 = oCommon.getFieldValue(strSQL)

                    strSQL = "Select PATeori from kpmkv_modul Where KodModul= '" & strModul3 & "' AND KursusID='" & KursusID & "' AND Tahun='" & Tahun & "' AND Semester='" & Semester & "' AND Sesi='" & chkSesi.Text & "'"
                    PATeori3 = oCommon.getFieldValue(strSQL)

                    strSQL = "Select B_Amali3 from kpmkv_pelajar_markah Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                    'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                    'strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & KursusID & "'"
                    B_Amali3 = oCommon.getFieldValue(strSQL)
                    'round up
                    B_Amali3 = (B_Amali3)

                    strSQL = "Select B_Teori3 from kpmkv_pelajar_markah Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                    'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                    'strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & KursusID & "'"
                    B_Teori3 = oCommon.getFieldValue(strSQL)
                    'round up
                    B_Teori3 = (B_Teori3)

                    strSQL = "Select A_Amali3 from kpmkv_pelajar_markah Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                    'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                    'strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & KursusID & "'"
                    A_Amali3 = oCommon.getFieldValue(strSQL)
                    'round up
                    A_Amali3 = (A_Amali3)

                    strSQL = "Select A_Teori3 from kpmkv_pelajar_markah Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                    'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                    'strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & KursusID & "'"
                    A_Teori3 = oCommon.getFieldValue(strSQL)
                    'round up
                    A_Teori3 = (A_Teori3)

                    'convert 0 if null
                    If (String.IsNullOrEmpty(B_Amali3.ToString())) Then
                        B_Amali3 = 0
                    End If

                    If (String.IsNullOrEmpty(B_Teori3.ToString())) Then
                        B_Teori3 = 0
                    End If

                    If (String.IsNullOrEmpty(A_Amali3.ToString())) Then
                        A_Amali3 = 0
                    End If

                    If (String.IsNullOrEmpty(A_Teori3.ToString())) Then
                        A_Teori3 = 0
                    End If

                    If B_Amali3 = "-1" Or B_Teori3 = "-1" Or A_Amali3 = "-1" Or A_Teori3 = "-1" Then
                        strSQL = "UPDATE kpmkv_pelajar_markah SET PBPAM3='-1',"
                        strSQL += "PBAV3='-1',PBTV3='-1',PAAV3='-1',PATV3='-1'"
                        strSQL += " Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                        'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                        'strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & KursusID & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)
                    Else

                        PBA3 = ((B_Amali3 / 100) * PBAmali3)
                        PBT3 = ((B_Teori3 / 100) * PBTeori3)
                        PAA3 = ((A_Amali3 / 100) * PAAmali3)
                        PAT3 = ((A_Teori3 / 100) * PATeori3)

                        'change on 31/7/2017 PBAV1,PBTV1,PAAV1,PATV1
                        PointerM3 = Math.Ceiling(PBA3 + PBT3 + PAA3 + PAT3)
                        strSQL = "UPDATE kpmkv_pelajar_markah SET PBPAM3='" & PointerM3 & "',"
                        strSQL += "PBAV3='" & PBA3 & "',PBTV3='" & PBT3 & "',PAAV3='" & PAA3 & "',PATV3='" & PAT3 & "'"
                        strSQL += " Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                        'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                        'strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & KursusID & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)
                    End If
                End If
                'Modul3---------------------------------
                If Not String.IsNullOrEmpty(strModul4) Then
                    Dim PBAmali4 As Integer
                    Dim PBTeori4 As Integer
                    Dim PAAmali4 As Integer
                    Dim PATeori4 As Integer

                    Dim B_Amali4 As Double
                    Dim B_Teori4 As Double
                    Dim A_Amali4 As Double
                    Dim A_Teori4 As Double
                    Dim PBA4 As Double
                    Dim PBT4 As Double
                    Dim PAA4 As Double
                    Dim PAT4 As Double
                    Dim PointerM4 As Integer

                    strSQL = "Select PBAmali from kpmkv_modul Where KodModul= '" & strModul4 & "' AND KursusID='" & KursusID & "' AND Tahun='" & Tahun & "' AND Semester='" & Semester & "' AND Sesi='" & chkSesi.Text & "'"
                    PBAmali4 = oCommon.getFieldValue(strSQL)

                    strSQL = "Select PBTeori from kpmkv_modul Where KodModul= '" & strModul4 & "' AND KursusID='" & KursusID & "' AND Tahun='" & Tahun & "' AND Semester='" & Semester & "' AND Sesi='" & chkSesi.Text & "'"
                    PBTeori4 = oCommon.getFieldValue(strSQL)

                    strSQL = "Select PAAmali from kpmkv_modul Where KodModul= '" & strModul4 & "' AND KursusID='" & KursusID & "' AND Tahun='" & Tahun & "' AND Semester='" & Semester & "' AND Sesi='" & chkSesi.Text & "'"
                    PAAmali4 = oCommon.getFieldValue(strSQL)

                    strSQL = "Select PATeori from kpmkv_modul Where KodModul= '" & strModul4 & "' AND KursusID='" & KursusID & "' AND Tahun='" & Tahun & "' AND Semester='" & Semester & "' AND Sesi='" & chkSesi.Text & "'"
                    PATeori4 = oCommon.getFieldValue(strSQL)

                    strSQL = "Select B_Amali4 from kpmkv_pelajar_markah Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                    'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                    'strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & KursusID & "'"
                    B_Amali4 = oCommon.getFieldValue(strSQL)
                    'round up
                    B_Amali4 = (B_Amali4)

                    strSQL = "Select B_Teori4 from kpmkv_pelajar_markah Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                    'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                    'strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & KursusID & "'"
                    B_Teori4 = oCommon.getFieldValue(strSQL)
                    'round up
                    B_Teori4 = (B_Teori4)

                    strSQL = "Select A_Amali4 from kpmkv_pelajar_markah Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                    'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                    'strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & KursusID & "'"
                    A_Amali4 = oCommon.getFieldValue(strSQL)
                    'round up
                    A_Amali4 = (A_Amali4)

                    strSQL = "Select A_Teori4 from kpmkv_pelajar_markah Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                    'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                    'strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & KursusID & "'"
                    A_Teori4 = oCommon.getFieldValue(strSQL)
                    'round up
                    A_Teori4 = (A_Teori4)

                    'convert 0 if null
                    If (String.IsNullOrEmpty(B_Amali4.ToString())) Then
                        B_Amali4 = 0
                    End If

                    If (String.IsNullOrEmpty(B_Teori4.ToString())) Then
                        B_Teori4 = 0
                    End If

                    If (String.IsNullOrEmpty(A_Amali4.ToString())) Then
                        A_Amali4 = 0
                    End If

                    If (String.IsNullOrEmpty(A_Teori4.ToString())) Then
                        A_Teori4 = 0
                    End If

                    If B_Amali4 = "-1" Or B_Teori4 = "-1" Or A_Amali4 = "-1" Or A_Teori4 = "-1" Then
                        strSQL = "UPDATE kpmkv_pelajar_markah SET PBPAM4='-1',"
                        strSQL += "PBAV4='-1',PBTV4='-1',PAAV4='-1',PATV4='-1'"
                        strSQL += " Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                        'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                        'strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & KursusID & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)
                    Else

                        PBA4 = ((B_Amali4 / 100) * PBAmali4)
                        PBT4 = ((B_Teori4 / 100) * PBTeori4)
                        PAA4 = ((A_Amali4 / 100) * PAAmali4)
                        PAT4 = ((A_Teori4 / 100) * PATeori4)

                        'change on 31/7/2017 PBAV1,PBTV1,PAAV1,PATV1
                        PointerM4 = Math.Ceiling(PBA4 + PBT4 + PAA4 + PAT4)
                        strSQL = "UPDATE kpmkv_pelajar_markah SET PBPAM4='" & PointerM4 & "',"
                        strSQL += "PBAV4='" & PBA4 & "',PBTV4='" & PBT4 & "',PAAV4='" & PAA4 & "',PATV4='" & PAT4 & "'"
                        strSQL += " Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                        'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                        'strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & KursusID & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)
                    End If
                End If
                'Modul4---------------------------------
                If Not String.IsNullOrEmpty(strModul5) Then

                    Dim PBAmali5 As Integer
                    Dim PBTeori5 As Integer
                    Dim PAAmali5 As Integer
                    Dim PATeori5 As Integer

                    Dim B_Amali5 As Double
                    Dim B_Teori5 As Double
                    Dim A_Amali5 As Double
                    Dim A_Teori5 As Double
                    Dim PBA5 As Double
                    Dim PBT5 As Double
                    Dim PAA5 As Double
                    Dim PAT5 As Double
                    Dim PointerM5 As Integer

                    strSQL = "Select PBAmali from kpmkv_modul Where KodModul= '" & strModul5 & "' AND KursusID='" & KursusID & "' AND Tahun='" & Tahun & "' AND Semester='" & Semester & "' AND Sesi='" & chkSesi.Text & "'"
                    PBAmali5 = oCommon.getFieldValue(strSQL)

                    strSQL = "Select PBTeori from kpmkv_modul Where KodModul= '" & strModul5 & "' AND KursusID='" & KursusID & "' AND Tahun='" & Tahun & "' AND Semester='" & Semester & "' AND Sesi='" & chkSesi.Text & "'"
                    PBTeori5 = oCommon.getFieldValue(strSQL)

                    strSQL = "Select PAAmali from kpmkv_modul Where KodModul= '" & strModul5 & "' AND KursusID='" & KursusID & "' AND Tahun='" & Tahun & "' AND Semester='" & Semester & "' AND Sesi='" & chkSesi.Text & "'"
                    PAAmali5 = oCommon.getFieldValue(strSQL)

                    strSQL = "Select PATeori from kpmkv_modul Where KodModul= '" & strModul5 & "' AND KursusID='" & KursusID & "' AND Tahun='" & Tahun & "' AND Semester='" & Semester & "' AND Sesi='" & chkSesi.Text & "'"
                    PATeori5 = oCommon.getFieldValue(strSQL)

                    strSQL = "Select B_Amali5 from kpmkv_pelajar_markah Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                    'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                    'strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & KursusID & "'"
                    B_Amali5 = oCommon.getFieldValue(strSQL)
                    'round up
                    B_Amali5 = (B_Amali5)

                    strSQL = "Select B_Teori5 from kpmkv_pelajar_markah Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                    'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                    'strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & KursusID & "'"
                    B_Teori5 = oCommon.getFieldValue(strSQL)
                    'round up
                    B_Teori5 = (B_Teori5)

                    strSQL = "Select A_Amali5 from kpmkv_pelajar_markah Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                    'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                    'strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & KursusID & "'"
                    A_Amali5 = oCommon.getFieldValue(strSQL)
                    'round up
                    A_Amali5 = (A_Amali5)

                    strSQL = "Select A_Teori5 from kpmkv_pelajar_markah Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                    'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                    'strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & KursusID & "'"
                    A_Teori5 = oCommon.getFieldValue(strSQL)
                    'round up
                    A_Teori5 = (A_Teori5)

                    'convert 0 if null
                    If (String.IsNullOrEmpty(B_Amali5.ToString())) Then
                        B_Amali5 = 0
                    End If

                    If (String.IsNullOrEmpty(B_Teori5.ToString())) Then
                        B_Teori5 = 0
                    End If

                    If (String.IsNullOrEmpty(A_Amali5.ToString())) Then
                        A_Amali5 = 0
                    End If

                    If (String.IsNullOrEmpty(A_Teori5.ToString())) Then
                        A_Teori5 = 0
                    End If

                    If B_Amali5 = "-1" Or B_Teori5 = "-1" Or A_Amali5 = "-1" Or A_Teori5 = "-1" Then
                        strSQL = "UPDATE kpmkv_pelajar_markah SET PBPAM5='-1',"
                        strSQL += "PBAV5='-1',PBTV5='-1',PAAV5='-1',PATV5='-1'"
                        strSQL += " Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                        'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                        'strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & KursusID & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)
                    Else

                        PBA5 = ((B_Amali5 / 100) * PBAmali5)
                        PBT5 = ((B_Teori5 / 100) * PBTeori5)
                        PAA5 = ((A_Amali5 / 100) * PAAmali5)
                        PAT5 = ((A_Teori5 / 100) * PATeori5)

                        'change on 31/7/2017 PBAV1,PBTV1,PAAV1,PATV1
                        PointerM5 = Math.Ceiling(PBA5 + PBT5 + PAA5 + PAT5)
                        strSQL = "UPDATE kpmkv_pelajar_markah SET PBPAM5='" & PointerM5 & "',"
                        strSQL += "PBAV5='" & PBA5 & "',PBTV5='" & PBT5 & "',PAAV5='" & PAA5 & "',PATV5='" & PAT5 & "'"
                        strSQL += " Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                        'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                        'strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & KursusID & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)
                    End If
                End If
                'Modul6---------------------------------
                If Not String.IsNullOrEmpty(strModul6) Then

                    Dim PBAmali6 As Integer
                    Dim PBTeori6 As Integer
                    Dim PAAmali6 As Integer
                    Dim PATeori6 As Integer

                    Dim B_Amali6 As Double
                    Dim B_Teori6 As Double
                    Dim A_Amali6 As Double
                    Dim A_Teori6 As Double
                    Dim PBA6 As Double
                    Dim PBT6 As Double
                    Dim PAA6 As Double
                    Dim PAT6 As Double
                    Dim PointerM6 As Integer

                    strSQL = "Select PBAmali from kpmkv_modul Where KodModul= '" & strModul6 & "' AND KursusID='" & KursusID & "' AND Tahun='" & Tahun & "' AND Semester='" & Semester & "' AND Sesi='" & chkSesi.Text & "'"
                    PBAmali6 = oCommon.getFieldValue(strSQL)

                    strSQL = "Select PBTeori from kpmkv_modul Where KodModul= '" & strModul6 & "' AND KursusID='" & KursusID & "' AND Tahun='" & Tahun & "' AND Semester='" & Semester & "' AND Sesi='" & chkSesi.Text & "'"
                    PBTeori6 = oCommon.getFieldValue(strSQL)

                    strSQL = "Select PAAmali from kpmkv_modul Where KodModul= '" & strModul6 & "' AND KursusID='" & KursusID & "' AND Tahun='" & Tahun & "' AND Semester='" & Semester & "' AND Sesi='" & chkSesi.Text & "'"
                    PAAmali6 = oCommon.getFieldValue(strSQL)

                    strSQL = "Select PATeori from kpmkv_modul Where KodModul= '" & strModul6 & "' AND KursusID='" & KursusID & "' AND Tahun='" & Tahun & "' AND Semester='" & Semester & "' AND Sesi='" & chkSesi.Text & "'"
                    PATeori6 = oCommon.getFieldValue(strSQL)

                    strSQL = "Select B_Amali6 from kpmkv_pelajar_markah Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                    'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                    'strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & KursusID & "'"
                    B_Amali6 = oCommon.getFieldValue(strSQL)
                    'round up
                    B_Amali6 = (B_Amali6)

                    strSQL = "Select B_Teori6 from kpmkv_pelajar_markah Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                    'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                    'strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & KursusID & "'"
                    B_Teori6 = oCommon.getFieldValue(strSQL)
                    'round up
                    B_Teori6 = (B_Teori6)

                    strSQL = "Select A_Amali6 from kpmkv_pelajar_markah Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                    'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                    'strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & KursusID & "'"
                    A_Amali6 = oCommon.getFieldValue(strSQL)
                    'round up
                    A_Amali6 = (A_Amali6)

                    strSQL = "Select A_Teori6 from kpmkv_pelajar_markah Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                    'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                    'strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & KursusID & "'"
                    A_Teori6 = oCommon.getFieldValue(strSQL)
                    'round up
                    A_Teori6 = (A_Teori6)

                    'convert 0 if null
                    If (String.IsNullOrEmpty(B_Amali6.ToString())) Then
                        B_Amali6 = 0
                    End If

                    If (String.IsNullOrEmpty(B_Teori6.ToString())) Then
                        B_Teori6 = 0
                    End If

                    If (String.IsNullOrEmpty(A_Amali6.ToString())) Then
                        A_Amali6 = 0
                    End If

                    If (String.IsNullOrEmpty(A_Teori6.ToString())) Then
                        A_Teori6 = 0
                    End If

                    If B_Amali6 = "-1" Or B_Teori6 = "-1" Or A_Amali6 = "-1" Or A_Teori6 = "-1" Then
                        strSQL = "UPDATE kpmkv_pelajar_markah SET PBPAM6='-1',"
                        strSQL += "PBAV6='-1',PBTV6='-1',PAAV6='-1',PATV6='-1'"
                        strSQL += " Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                        'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                        'strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & KursusID & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)
                    Else

                        PBA6 = ((B_Amali6 / 100) * PBAmali6)
                        PBT6 = ((B_Teori6 / 100) * PBTeori6)
                        PAA6 = ((A_Amali6 / 100) * PAAmali6)
                        PAT6 = ((A_Teori6 / 100) * PATeori6)

                        'change on 31/7/2017 PBAV1,PBTV1,PAAV1,PATV1
                        PointerM6 = Math.Ceiling(PBA6 + PBT6 + PAA6 + PAT6)
                        strSQL = "UPDATE kpmkv_pelajar_markah SET PBPAM6='" & PointerM6 & "',"
                        strSQL += "PBAV6='" & PBA6 & "',PBTV6='" & PBT6 & "',PAAV6='" & PAA6 & "',PATV6='" & PAT6 & "'"
                        strSQL += " Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                        'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                        'strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & KursusID & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)
                    End If
                End If
                'Modul7---------------------------------
                If Not String.IsNullOrEmpty(strModul7) Then

                    Dim PBAmali7 As Integer
                    Dim PBTeori7 As Integer
                    Dim PAAmali7 As Integer
                    Dim PATeori7 As Integer

                    Dim B_Amali7 As Double
                    Dim B_Teori7 As Double
                    Dim A_Amali7 As Double
                    Dim A_Teori7 As Double
                    Dim PBA7 As Double
                    Dim PBT7 As Double
                    Dim PAA7 As Double
                    Dim PAT7 As Double
                    Dim PointerM7 As Integer

                    strSQL = "Select PBAmali from kpmkv_modul Where KodModul= '" & strModul7 & "' AND KursusID='" & KursusID & "' AND Tahun='" & Tahun & "' AND Semester='" & Semester & "' AND Sesi='" & chkSesi.Text & "'"
                    PBAmali7 = oCommon.getFieldValue(strSQL)

                    strSQL = "Select PBTeori from kpmkv_modul Where KodModul= '" & strModul7 & "' AND KursusID='" & KursusID & "' AND Tahun='" & Tahun & "' AND Semester='" & Semester & "' AND Sesi='" & chkSesi.Text & "'"
                    PBTeori7 = oCommon.getFieldValue(strSQL)

                    strSQL = "Select PAAmali from kpmkv_modul Where KodModul= '" & strModul7 & "' AND KursusID='" & KursusID & "' AND Tahun='" & Tahun & "' AND Semester='" & Semester & "' AND Sesi='" & chkSesi.Text & "'"
                    PAAmali7 = oCommon.getFieldValue(strSQL)

                    strSQL = "Select PATeori from kpmkv_modul Where KodModul= '" & strModul7 & "' AND KursusID='" & KursusID & "' AND Tahun='" & Tahun & "' AND Semester='" & Semester & "' AND Sesi='" & chkSesi.Text & "'"
                    PATeori7 = oCommon.getFieldValue(strSQL)

                    strSQL = "Select B_Amali7 from kpmkv_pelajar_markah Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                    'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                    'strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & KursusID & "'"
                    B_Amali7 = oCommon.getFieldValue(strSQL)
                    'round up
                    B_Amali7 = (B_Amali7)

                    strSQL = "Select B_Teori7 from kpmkv_pelajar_markah Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                    'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                    'strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & KursusID & "'"
                    B_Teori7 = oCommon.getFieldValue(strSQL)
                    'round up
                    B_Teori7 = (B_Teori7)

                    strSQL = "Select A_Amali7 from kpmkv_pelajar_markah Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                    'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                    'strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & KursusID & "'"
                    A_Amali7 = oCommon.getFieldValue(strSQL)
                    'round up
                    A_Amali7 = (A_Amali7)

                    strSQL = "Select A_Teori7 from kpmkv_pelajar_markah Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                    'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                    'strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & KursusID & "'"
                    A_Teori7 = oCommon.getFieldValue(strSQL)
                    'round up
                    A_Teori7 = (A_Teori7)

                    'convert 0 if null
                    If (String.IsNullOrEmpty(B_Amali7.ToString())) Then
                        B_Amali7 = 0
                    End If

                    If (String.IsNullOrEmpty(B_Teori7.ToString())) Then
                        B_Teori7 = 0
                    End If

                    If (String.IsNullOrEmpty(A_Amali7.ToString())) Then
                        A_Amali7 = 0
                    End If

                    If (String.IsNullOrEmpty(A_Teori7.ToString())) Then
                        A_Teori7 = 0
                    End If

                    If B_Amali7 = "-1" Or B_Teori7 = "-1" Or A_Amali7 = "-1" Or A_Teori7 = "-1" Then
                        strSQL = "UPDATE kpmkv_pelajar_markah SET PBPAM7='-1',"
                        strSQL += "PBAV7='-1',PBTV7='-1',PAAV7='-1',PATV7='-1'"
                        strSQL += " Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                        'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                        'strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & KursusID & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)
                    Else

                        PBA7 = ((B_Amali7 / 100) * PBAmali7)
                        PBT7 = ((B_Teori7 / 100) * PBTeori7)
                        PAA7 = ((A_Amali7 / 100) * PAAmali7)
                        PAT7 = ((A_Teori7 / 100) * PATeori7)

                        'change on 31/7/2017 PBAV1,PBTV1,PAAV1,PATV1
                        PointerM7 = Math.Ceiling(PBA7 + PBT7 + PAA7 + PAT7)
                        strSQL = "UPDATE kpmkv_pelajar_markah SET PBPAM7='" & PointerM7 & "',"
                        strSQL += "PBAV7='" & PBA7 & "',PBTV7='" & PBT7 & "',PAAV7='" & PAA7 & "',PATV7='" & PAT7 & "'"
                        strSQL += " Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                        'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                        'strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & KursusID & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)
                    End If
                End If
                'Modul8---------------------------------
                If Not String.IsNullOrEmpty(strModul8) Then

                    Dim PBAmali8 As Integer
                    Dim PBTeori8 As Integer
                    Dim PAAmali8 As Integer
                    Dim PATeori8 As Integer

                    Dim B_Amali8 As Double
                    Dim B_Teori8 As Double
                    Dim A_Amali8 As Double
                    Dim A_Teori8 As Double
                    Dim PBA8 As Double
                    Dim PBT8 As Double
                    Dim PAA8 As Double
                    Dim PAT8 As Double
                    Dim PointerM8 As Integer

                    strSQL = "Select PBAmali from kpmkv_modul Where KodModul= '" & strModul8 & "' AND KursusID='" & KursusID & "' AND Tahun='" & Tahun & "' AND Semester='" & Semester & "' AND Sesi='" & chkSesi.Text & "'"
                    PBAmali8 = oCommon.getFieldValue(strSQL)

                    strSQL = "Select PBTeori from kpmkv_modul Where KodModul= '" & strModul8 & "' AND KursusID='" & KursusID & "' AND Tahun='" & Tahun & "' AND Semester='" & Semester & "' AND Sesi='" & chkSesi.Text & "'"
                    PBTeori8 = oCommon.getFieldValue(strSQL)

                    strSQL = "Select PAAmali from kpmkv_modul Where KodModul= '" & strModul8 & "' AND KursusID='" & KursusID & "' AND Tahun='" & Tahun & "' AND Semester='" & Semester & "' AND Sesi='" & chkSesi.Text & "'"
                    PAAmali8 = oCommon.getFieldValue(strSQL)

                    strSQL = "Select PATeori from kpmkv_modul Where KodModul= '" & strModul8 & "' AND KursusID='" & KursusID & "' AND Tahun='" & Tahun & "' AND Semester='" & Semester & "' AND Sesi='" & chkSesi.Text & "'"
                    PATeori8 = oCommon.getFieldValue(strSQL)

                    strSQL = "Select B_Amali8 from kpmkv_pelajar_markah Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                    'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                    'strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & KursusID & "'"
                    B_Amali8 = oCommon.getFieldValue(strSQL)
                    'round up
                    B_Amali8 = (B_Amali8)

                    strSQL = "Select B_Teori8 from kpmkv_pelajar_markah Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                    'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                    'strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & KursusID & "'"
                    B_Teori8 = oCommon.getFieldValue(strSQL)
                    'round up
                    B_Teori8 = (B_Teori8)

                    strSQL = "Select A_Amali8 from kpmkv_pelajar_markah Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                    'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                    'strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & KursusID & "'"
                    A_Amali8 = oCommon.getFieldValue(strSQL)
                    'round up
                    A_Amali8 = (A_Amali8)

                    strSQL = "Select A_Teori8 from kpmkv_pelajar_markah Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                    'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                    'strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & KursusID & "'"
                    A_Teori8 = oCommon.getFieldValue(strSQL)
                    'round up
                    A_Teori8 = (A_Teori8)

                    'convert 0 if null
                    If (String.IsNullOrEmpty(B_Amali8.ToString())) Then
                        B_Amali8 = 0
                    End If

                    If (String.IsNullOrEmpty(B_Teori8.ToString())) Then
                        B_Teori8 = 0
                    End If

                    If (String.IsNullOrEmpty(A_Amali8.ToString())) Then
                        A_Amali8 = 0
                    End If

                    If (String.IsNullOrEmpty(A_Teori8.ToString())) Then
                        A_Teori8 = 0
                    End If

                    If B_Amali8 = "-1" Or B_Teori8 = "-1" Or A_Amali8 = "-1" Or A_Teori8 = "-1" Then
                        strSQL = "UPDATE kpmkv_pelajar_markah SET PBPAM8='-1',"
                        strSQL += "PBAV8='-1',PBTV8='-1',PAAV8='-1',PATV8='-1'"
                        strSQL += " Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                        'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                        'strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & KursusID & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)
                    Else

                        PBA8 = ((B_Amali8 / 100) * PBAmali8)
                        PBT8 = ((B_Teori8 / 100) * PBTeori8)
                        PAA8 = ((A_Amali8 / 100) * PAAmali8)
                        PAT8 = ((A_Teori8 / 100) * PATeori8)

                        'change on 31/8/2018 PBAV1,PBTV1,PAAV1,PATV1
                        PointerM8 = Math.Ceiling(PBA8 + PBT8 + PAA8 + PAT8)
                        strSQL = "UPDATE kpmkv_pelajar_markah SET PBPAM8='" & PointerM8 & "',"
                        strSQL += "PBAV8='" & PBA8 & "',PBTV8='" & PBT8 & "',PAAV8='" & PAA8 & "',PATV8='" & PAT8 & "'"
                        strSQL += " Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                        'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                        'strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & KursusID & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)
                    End If
                End If
            Next

        End If

    End Sub

    Private Sub Vokasional_gred()
        strRet = BindData(datRespondent)

        If datRespondent.Rows.Count = 1 Then

            For i As Integer = 0 To datRespondent.Rows.Count - 1
                Dim PBPAM1 As String
                Dim GredPBPAM1 As String

                strSQL = "SELECT PBPAM1 FROM kpmkv_pelajar_markah"
                strSQL += " Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                PBPAM1 = oCommon.getFieldValueInt(strSQL)

                If String.IsNullOrEmpty(PBPAM1) Then
                    strSQL = "UPDATE kpmkv_pelajar_markah SET GredV1='' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                    'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                    'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)
                ElseIf PBPAM1 = 0 Then
                    strSQL = "UPDATE kpmkv_pelajar_markah SET GredV1='E' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                    'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                    'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)
                ElseIf PBPAM1 = -1 Then
                    strSQL = "UPDATE kpmkv_pelajar_markah SET GredV1='T' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                    'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                    'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)
                Else
                    strSQL = "SELECT TOP ( 1 ) Gred FROM  kpmkv_gred WHERE '" & PBPAM1 & "' BETWEEN MarkahFrom AND MarkahTo AND Jenis='VOKASIONAL'"
                    GredPBPAM1 = oCommon.getFieldValue(strSQL)

                    strSQL = "UPDATE kpmkv_pelajar_markah SET GredV1='" & GredPBPAM1 & "' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                    'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                    'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)

                End If

                '-----------------------------------------------------------------
                Dim PBPAM2 As String
                Dim GredPBPAM2 As String

                strSQL = "SELECT PBPAM2 FROM kpmkv_pelajar_markah"
                strSQL += " Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                PBPAM2 = oCommon.getFieldValueInt(strSQL)

                If String.IsNullOrEmpty(PBPAM2) Then
                    strSQL = "UPDATE kpmkv_pelajar_markah SET GredV2='' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                    'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                    'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)
                ElseIf PBPAM2 = 0 Then
                    strSQL = "UPDATE kpmkv_pelajar_markah SET GredV2='E' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                    'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                    'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)
                ElseIf PBPAM2 = -1 Then
                    strSQL = "UPDATE kpmkv_pelajar_markah SET GredV2='T' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                    'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                    'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)
                Else
                    strSQL = "SELECT TOP ( 1 ) Gred FROM  kpmkv_gred WHERE '" & PBPAM2 & "' BETWEEN MarkahFrom AND MarkahTo AND Jenis='VOKASIONAL'"
                    GredPBPAM2 = oCommon.getFieldValue(strSQL)

                    strSQL = "UPDATE kpmkv_pelajar_markah SET GredV2='" & GredPBPAM2 & "' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                    'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                    'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)

                End If
                '------------------------------------------------------------------------------------------------------------------------
                Dim PBPAM3 As String
                Dim GredPBPAM3 As String

                strSQL = "SELECT PBPAM3 FROM kpmkv_pelajar_markah"
                strSQL += " Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                PBPAM3 = oCommon.getFieldValueInt(strSQL)

                If String.IsNullOrEmpty(PBPAM3) Then
                    strSQL = "UPDATE kpmkv_pelajar_markah SET GredV3='' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                    'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                    'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)
                ElseIf PBPAM3 = 0 Then
                    strSQL = "UPDATE kpmkv_pelajar_markah SET GredV3='E' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                    'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                    'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)
                ElseIf PBPAM3 = -1 Then
                    strSQL = "UPDATE kpmkv_pelajar_markah SET GredV3='T' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                    'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                    'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)
                Else
                    strSQL = "SELECT TOP ( 1 ) Gred FROM  kpmkv_gred WHERE '" & PBPAM3 & "' BETWEEN MarkahFrom AND MarkahTo AND Jenis='VOKASIONAL'"
                    GredPBPAM3 = oCommon.getFieldValue(strSQL)

                    strSQL = "UPDATE kpmkv_pelajar_markah SET GredV3='" & GredPBPAM3 & "' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                    'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                    'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)

                End If
                '------------------------------------------------------------------------------------------------------------

                Dim PBPAM4 As String
                Dim GredPBPAM4 As String

                strSQL = "SELECT PBPAM4 FROM kpmkv_pelajar_markah"
                strSQL += " Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                PBPAM4 = oCommon.getFieldValueInt(strSQL)

                If String.IsNullOrEmpty(PBPAM4) Then
                    strSQL = "UPDATE kpmkv_pelajar_markah SET GredV4='' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                    'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                    'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)
                ElseIf PBPAM4 = 0 Then
                    strSQL = "UPDATE kpmkv_pelajar_markah SET GredV4='E' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                    'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                    'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)
                ElseIf PBPAM4 = -1 Then
                    strSQL = "UPDATE kpmkv_pelajar_markah SET GredV4='T' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                    'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                    'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)
                Else
                    strSQL = "SELECT TOP ( 1 ) Gred FROM  kpmkv_gred WHERE '" & PBPAM4 & "' BETWEEN MarkahFrom AND MarkahTo AND Jenis='VOKASIONAL'"
                    GredPBPAM4 = oCommon.getFieldValue(strSQL)

                    strSQL = "UPDATE kpmkv_pelajar_markah SET GredV4='" & GredPBPAM4 & "' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                    'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                    'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)

                End If
                '-------------------------------------------------------------------------------------------------------------

                Dim PBPAM5 As String
                Dim GredPBPAM5 As String

                strSQL = "SELECT PBPAM5 FROM kpmkv_pelajar_markah"
                strSQL += " Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                PBPAM5 = oCommon.getFieldValueInt(strSQL)

                If String.IsNullOrEmpty(PBPAM5) Then
                    strSQL = "UPDATE kpmkv_pelajar_markah SET GredV5='' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                    'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                    'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)
                ElseIf PBPAM5 = 0 Then
                    strSQL = "UPDATE kpmkv_pelajar_markah SET GredV5='E' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                    'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                    'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)
                ElseIf PBPAM5 = -1 Then
                    strSQL = "UPDATE kpmkv_pelajar_markah SET GredV5='T' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                    'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                    'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)
                Else
                    strSQL = "SELECT TOP ( 1 ) Gred FROM  kpmkv_gred WHERE '" & PBPAM5 & "' BETWEEN MarkahFrom AND MarkahTo AND Jenis='VOKASIONAL'"
                    GredPBPAM5 = oCommon.getFieldValue(strSQL)

                    strSQL = "UPDATE kpmkv_pelajar_markah SET GredV5='" & GredPBPAM5 & "' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                    'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                    'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)
                End If
                '-------------------------------------------------------------------------------------------------------------

                Dim PBPAM6 As String
                Dim GredPBPAM6 As String

                strSQL = "SELECT PBPAM6 FROM kpmkv_pelajar_markah"
                strSQL += " Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                PBPAM6 = oCommon.getFieldValueInt(strSQL)

                If String.IsNullOrEmpty(PBPAM6) Then
                    strSQL = "UPDATE kpmkv_pelajar_markah SET GredV6='' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                    'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                    'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)
                ElseIf PBPAM6 = 0 Then
                    strSQL = "UPDATE kpmkv_pelajar_markah SET GredV6='E' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                    'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                    'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)
                ElseIf PBPAM6 = -1 Then
                    strSQL = "UPDATE kpmkv_pelajar_markah SET GredV6='T' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                    'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                    'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)
                Else
                    strSQL = "SELECT TOP ( 1 ) Gred FROM  kpmkv_gred WHERE '" & PBPAM6 & "' BETWEEN MarkahFrom AND MarkahTo AND Jenis='VOKASIONAL'"
                    GredPBPAM6 = oCommon.getFieldValue(strSQL)

                    strSQL = "UPDATE kpmkv_pelajar_markah SET GredV6='" & GredPBPAM6 & "' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                    'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                    'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)
                End If                '-------------------------------------------------------------------------------------------------------------

            Next

        End If

    End Sub

    Private Sub Vokasional_gredKompeten()

        Try

            If datRespondent.Rows.Count = 1 Then

                For i As Integer = 0 To datRespondent.Rows.Count - 1

                    Dim Tahun As String
                    Dim Semester As String

                    strSQL = "SELECT Tahun, Semester FROM kpmkv_pelajar WHERE PelajarID = '" & datRespondent.DataKeys(i).Value.ToString & "'"
                    strRet = oCommon.getFieldValueEx(strSQL)

                    Dim ar_TahunSemester As Array
                    ar_TahunSemester = strRet.Split("|")

                    Tahun = ar_TahunSemester(0)
                    Semester = ar_TahunSemester(1)

                    strSQL = "SELECT KursusID FROM kpmkv_pelajar_markah WHERE PelajarID = '" & datRespondent.DataKeys(i).Value.ToString & "'"
                    Dim KursusID As String = oCommon.getFieldValue(strSQL)

                    '--count no of modul
                    Dim nCount As Integer = 0
                    strSQL = "SELECT COUNT(kpmkv_modul.KodModul) as CModul "
                    strSQL += " FROM kpmkv_modul LEFT OUTER JOIN"
                    strSQL += " kpmkv_kursus ON kpmkv_modul.KursusID = kpmkv_kursus.KursusID"
                    strSQL += " WHERE kpmkv_modul.KursusID='" & KursusID & "'"
                    strSQL += " AND  kpmkv_modul.Sesi='" & chkSesi.Text & "'"
                    strSQL += " AND  kpmkv_modul.Semester='" & Semester & "'"
                    strSQL += " AND  kpmkv_modul.Tahun='" & Tahun & "'"
                    nCount = oCommon.getFieldValueInt(strSQL)

                    strRet = BindData(datRespondent)

                    Dim strGredV1 As String
                    Dim strGredV2 As String
                    Dim strGredV3 As String
                    Dim strGredV4 As String
                    Dim strGredV5 As String
                    Dim strGredV6 As String
                    Dim strGredV7 As String
                    Dim strGredV8 As String

                    Select Case nCount
                        Case "2"
                            strSQL = "SELECT GredV1,GredV2 FROM kpmkv_pelajar_markah"
                            strSQL += " Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                            'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                            'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                            strRet = oCommon.getFieldValueEx(strSQL)

                            Dim ar_total As Array
                            ar_total = strRet.Split("|")
                            strGredV1 = ar_total(0)
                            strGredV2 = ar_total(1)


                            If (strGredV1 = "C+" Or strGredV1 = "C" Or strGredV1 = "C-" Or strGredV1 = "D+" Or strGredV1 = "D-" Or strGredV1 = "E" Or strGredV1 = "G" Or strGredV1 = "T") Then
                                strSQL = "UPDATE kpmkv_pelajar_markah SET Gred_Kompeten='0' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                                'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                                'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                                strRet = oCommon.ExecuteSQL(strSQL)

                            ElseIf (strGredV2 = "C+" Or strGredV2 = "C" Or strGredV2 = "C-" Or strGredV2 = "D+" Or strGredV2 = "D-" Or strGredV2 = "E" Or strGredV2 = "G" Or strGredV2 = "T") Then
                                strSQL = "UPDATE kpmkv_pelajar_markah SET Gred_Kompeten='0' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                                'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                                'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                                strRet = oCommon.ExecuteSQL(strSQL)
                            Else

                                strSQL = "UPDATE kpmkv_pelajar_markah SET Gred_Kompeten='1' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                                'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                                'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                                strRet = oCommon.ExecuteSQL(strSQL)
                            End If

                        Case "3"

                            strSQL = "SELECT GredV1,GredV2,GredV3 FROM kpmkv_pelajar_markah"
                            strSQL += " Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                            'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                            'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                            strRet = oCommon.getFieldValueEx(strSQL)

                            Dim ar_total As Array
                            ar_total = strRet.Split("|")
                            strGredV1 = ar_total(0)
                            strGredV2 = ar_total(1)
                            strGredV3 = ar_total(2)

                            If (strGredV1 = "C+" Or strGredV1 = "C" Or strGredV1 = "C-" Or strGredV1 = "D+" Or strGredV1 = "D-" Or strGredV1 = "E" Or strGredV1 = "G" Or strGredV1 = "T") Then
                                strSQL = "UPDATE kpmkv_pelajar_markah SET Gred_Kompeten='0' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                                'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                                'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                                strRet = oCommon.ExecuteSQL(strSQL)

                            ElseIf (strGredV2 = "C+" Or strGredV2 = "C" Or strGredV2 = "C-" Or strGredV2 = "D+" Or strGredV2 = "D-" Or strGredV2 = "E" Or strGredV2 = "G" Or strGredV2 = "T") Then
                                strSQL = "UPDATE kpmkv_pelajar_markah SET Gred_Kompeten='0' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                                'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                                'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                                strRet = oCommon.ExecuteSQL(strSQL)

                            ElseIf (strGredV3 = "C+" Or strGredV3 = "C" Or strGredV3 = "C-" Or strGredV3 = "D+" Or strGredV3 = "D-" Or strGredV3 = "E" Or strGredV3 = "G" Or strGredV3 = "T") Then
                                strSQL = "UPDATE kpmkv_pelajar_markah SET Gred_Kompeten='0' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                                'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                                'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                                strRet = oCommon.ExecuteSQL(strSQL)
                            Else

                                strSQL = "UPDATE kpmkv_pelajar_markah SET Gred_Kompeten='1' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                                'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                                'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                                strRet = oCommon.ExecuteSQL(strSQL)
                            End If

                        Case "4"

                            strSQL = "SELECT GredV1,GredV2,GredV3,GredV4 FROM kpmkv_pelajar_markah"
                            strSQL += " Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                            'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                            'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                            strRet = oCommon.getFieldValueEx(strSQL)

                            Dim ar_total As Array
                            ar_total = strRet.Split("|")
                            strGredV1 = ar_total(0)
                            strGredV2 = ar_total(1)
                            strGredV3 = ar_total(2)
                            strGredV4 = ar_total(3)


                            If (strGredV1 = "C+" Or strGredV1 = "C" Or strGredV1 = "C-" Or strGredV1 = "D+" Or strGredV1 = "D-" Or strGredV1 = "E" Or strGredV1 = "G" Or strGredV1 = "T") Then
                                strSQL = "UPDATE kpmkv_pelajar_markah SET Gred_Kompeten='0' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                                'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                                'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                                strRet = oCommon.ExecuteSQL(strSQL)

                            ElseIf (strGredV2 = "C+" Or strGredV2 = "C" Or strGredV2 = "C-" Or strGredV2 = "D+" Or strGredV2 = "D-" Or strGredV2 = "E" Or strGredV2 = "G" Or strGredV2 = "T") Then
                                strSQL = "UPDATE kpmkv_pelajar_markah SET Gred_Kompeten='0' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                                'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                                'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                                strRet = oCommon.ExecuteSQL(strSQL)

                            ElseIf (strGredV3 = "C+" Or strGredV3 = "C" Or strGredV3 = "C-" Or strGredV3 = "D+" Or strGredV3 = "D-" Or strGredV3 = "E" Or strGredV3 = "G" Or strGredV3 = "T") Then
                                strSQL = "UPDATE kpmkv_pelajar_markah SET Gred_Kompeten='0' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                                'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                                'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                                strRet = oCommon.ExecuteSQL(strSQL)

                            ElseIf (strGredV4 = "C+" Or strGredV4 = "C" Or strGredV4 = "C-" Or strGredV4 = "D+" Or strGredV4 = "D-" Or strGredV4 = "E" Or strGredV4 = "G" Or strGredV4 = "T") Then
                                strSQL = "UPDATE kpmkv_pelajar_markah SET Gred_Kompeten='0' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                                'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                                'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                                strRet = oCommon.ExecuteSQL(strSQL)
                            Else

                                strSQL = "UPDATE kpmkv_pelajar_markah SET Gred_Kompeten='1' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                                'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                                'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                                strRet = oCommon.ExecuteSQL(strSQL)
                            End If


                        Case "5"
                            strSQL = "SELECT GredV1,GredV2,GredV3,GredV4,GredV5 FROM kpmkv_pelajar_markah"
                            strSQL += " Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                            'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                            'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                            strRet = oCommon.getFieldValueEx(strSQL)

                            Dim ar_total As Array
                            ar_total = strRet.Split("|")
                            strGredV1 = ar_total(0)
                            strGredV2 = ar_total(1)
                            strGredV3 = ar_total(2)
                            strGredV4 = ar_total(3)
                            strGredV5 = ar_total(4)


                            If (strGredV1 = "C+" Or strGredV1 = "C" Or strGredV1 = "C-" Or strGredV1 = "D+" Or strGredV1 = "D-" Or strGredV1 = "E" Or strGredV1 = "G" Or strGredV1 = "T") Then
                                strSQL = "UPDATE kpmkv_pelajar_markah SET Gred_Kompeten='0' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                                'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                                'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                                strRet = oCommon.ExecuteSQL(strSQL)

                            ElseIf (strGredV2 = "C+" Or strGredV2 = "C" Or strGredV2 = "C-" Or strGredV2 = "D+" Or strGredV2 = "D-" Or strGredV2 = "E" Or strGredV2 = "G" Or strGredV2 = "T") Then
                                strSQL = "UPDATE kpmkv_pelajar_markah SET Gred_Kompeten='0' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                                'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                                'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                                strRet = oCommon.ExecuteSQL(strSQL)

                            ElseIf (strGredV3 = "C+" Or strGredV3 = "C" Or strGredV3 = "C-" Or strGredV3 = "D+" Or strGredV3 = "D-" Or strGredV3 = "E" Or strGredV3 = "G" Or strGredV3 = "T") Then
                                strSQL = "UPDATE kpmkv_pelajar_markah SET Gred_Kompeten='0' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                                'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                                'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                                strRet = oCommon.ExecuteSQL(strSQL)

                            ElseIf (strGredV4 = "C+" Or strGredV4 = "C" Or strGredV4 = "C-" Or strGredV4 = "D+" Or strGredV4 = "D-" Or strGredV4 = "E" Or strGredV4 = "G" Or strGredV4 = "T") Then
                                strSQL = "UPDATE kpmkv_pelajar_markah SET Gred_Kompeten='0' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                                'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                                'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                                strRet = oCommon.ExecuteSQL(strSQL)

                            ElseIf (strGredV5 = "C+" Or strGredV5 = "C" Or strGredV5 = "C-" Or strGredV5 = "D+" Or strGredV5 = "D-" Or strGredV5 = "E" Or strGredV5 = "G" Or strGredV5 = "T") Then
                                strSQL = "UPDATE kpmkv_pelajar_markah SET Gred_Kompeten='0' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                                'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                                'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                                strRet = oCommon.ExecuteSQL(strSQL)

                            Else
                                strSQL = "UPDATE kpmkv_pelajar_markah SET Gred_Kompeten='1' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                                'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                                'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                                strRet = oCommon.ExecuteSQL(strSQL)
                            End If


                        Case "6"

                            strSQL = "SELECT GredV1,GredV2,GredV3,GredV4,GredV5,GredV6 FROM kpmkv_pelajar_markah"
                            strSQL += " Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                            'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                            'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                            strRet = oCommon.getFieldValueEx(strSQL)

                            Dim ar_total As Array
                            ar_total = strRet.Split("|")
                            strGredV1 = ar_total(0)
                            strGredV2 = ar_total(1)
                            strGredV3 = ar_total(2)
                            strGredV4 = ar_total(3)
                            strGredV5 = ar_total(4)
                            strGredV6 = ar_total(5)


                            If (strGredV1 = "C+" Or strGredV1 = "C" Or strGredV1 = "C-" Or strGredV1 = "D+" Or strGredV1 = "D-" Or strGredV1 = "E" Or strGredV1 = "G" Or strGredV1 = "T") Then
                                strSQL = "UPDATE kpmkv_pelajar_markah SET Gred_Kompeten='0' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                                'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                                'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                                strRet = oCommon.ExecuteSQL(strSQL)

                            ElseIf (strGredV2 = "C+" Or strGredV2 = "C" Or strGredV2 = "C-" Or strGredV2 = "D+" Or strGredV2 = "D-" Or strGredV2 = "E" Or strGredV2 = "G" Or strGredV2 = "T") Then
                                strSQL = "UPDATE kpmkv_pelajar_markah SET Gred_Kompeten='0' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                                'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                                'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                                strRet = oCommon.ExecuteSQL(strSQL)

                            ElseIf (strGredV3 = "C+" Or strGredV3 = "C" Or strGredV3 = "C-" Or strGredV3 = "D+" Or strGredV3 = "D-" Or strGredV3 = "E" Or strGredV3 = "G" Or strGredV3 = "T") Then
                                strSQL = "UPDATE kpmkv_pelajar_markah SET Gred_Kompeten='0' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                                'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                                'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                                strRet = oCommon.ExecuteSQL(strSQL)

                            ElseIf (strGredV4 = "C+" Or strGredV4 = "C" Or strGredV4 = "C-" Or strGredV4 = "D+" Or strGredV4 = "D-" Or strGredV4 = "E" Or strGredV4 = "G" Or strGredV4 = "T") Then
                                strSQL = "UPDATE kpmkv_pelajar_markah SET Gred_Kompeten='0' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                                'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                                'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                                strRet = oCommon.ExecuteSQL(strSQL)

                            ElseIf (strGredV5 = "C+" Or strGredV5 = "C" Or strGredV5 = "C-" Or strGredV5 = "D+" Or strGredV5 = "D-" Or strGredV5 = "E" Or strGredV5 = "G" Or strGredV5 = "T") Then
                                strSQL = "UPDATE kpmkv_pelajar_markah SET Gred_Kompeten='0' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                                'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                                'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                                strRet = oCommon.ExecuteSQL(strSQL)

                            ElseIf (strGredV6 = "C+" Or strGredV6 = "C" Or strGredV6 = "C-" Or strGredV6 = "D+" Or strGredV6 = "D-" Or strGredV6 = "E" Or strGredV6 = "G" Or strGredV6 = "T") Then
                                strSQL = "UPDATE kpmkv_pelajar_markah SET Gred_Kompeten='0' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                                'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                                'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                                strRet = oCommon.ExecuteSQL(strSQL)
                            Else
                                strSQL = "UPDATE kpmkv_pelajar_markah SET Gred_Kompeten='1' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                                'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                                'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                                strRet = oCommon.ExecuteSQL(strSQL)
                            End If

                        Case "7"

                            strSQL = "SELECT GredV1,GredV2,GredV3,GredV4,GredV5,GredV6,GredV7 FROM kpmkv_pelajar_markah"
                            strSQL += " Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                            'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                            'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                            strRet = oCommon.getFieldValueEx(strSQL)

                            Dim ar_total As Array
                            ar_total = strRet.Split("|")
                            strGredV1 = ar_total(0)
                            strGredV2 = ar_total(1)
                            strGredV3 = ar_total(2)
                            strGredV4 = ar_total(3)
                            strGredV5 = ar_total(4)
                            strGredV6 = ar_total(5)
                            strGredV7 = ar_total(6)

                            If (strGredV1 = "C+" Or strGredV1 = "C" Or strGredV1 = "C-" Or strGredV1 = "D+" Or strGredV1 = "D-" Or strGredV1 = "E" Or strGredV1 = "G" Or strGredV1 = "T") Then
                                strSQL = "UPDATE kpmkv_pelajar_markah SET Gred_Kompeten='0' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                                'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                                'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                                strRet = oCommon.ExecuteSQL(strSQL)

                            ElseIf (strGredV2 = "C+" Or strGredV2 = "C" Or strGredV2 = "C-" Or strGredV2 = "D+" Or strGredV2 = "D-" Or strGredV2 = "E" Or strGredV2 = "G" Or strGredV2 = "T") Then
                                strSQL = "UPDATE kpmkv_pelajar_markah SET Gred_Kompeten='0' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                                'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                                'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                                strRet = oCommon.ExecuteSQL(strSQL)

                            ElseIf (strGredV3 = "C+" Or strGredV3 = "C" Or strGredV3 = "C-" Or strGredV3 = "D+" Or strGredV3 = "D-" Or strGredV3 = "E" Or strGredV3 = "G" Or strGredV3 = "T") Then
                                strSQL = "UPDATE kpmkv_pelajar_markah SET Gred_Kompeten='0' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                                'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                                'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                                strRet = oCommon.ExecuteSQL(strSQL)

                            ElseIf (strGredV4 = "C+" Or strGredV4 = "C" Or strGredV4 = "C-" Or strGredV4 = "D+" Or strGredV4 = "D-" Or strGredV4 = "E" Or strGredV4 = "G" Or strGredV4 = "T") Then
                                strSQL = "UPDATE kpmkv_pelajar_markah SET Gred_Kompeten='0' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                                'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                                'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                                strRet = oCommon.ExecuteSQL(strSQL)

                            ElseIf (strGredV5 = "C+" Or strGredV5 = "C" Or strGredV5 = "C-" Or strGredV5 = "D+" Or strGredV5 = "D-" Or strGredV5 = "E" Or strGredV5 = "G" Or strGredV5 = "T") Then
                                strSQL = "UPDATE kpmkv_pelajar_markah SET Gred_Kompeten='0' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                                'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                                'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                                strRet = oCommon.ExecuteSQL(strSQL)

                            ElseIf (strGredV6 = "C+" Or strGredV6 = "C" Or strGredV6 = "C-" Or strGredV6 = "D+" Or strGredV6 = "D-" Or strGredV6 = "E" Or strGredV6 = "G" Or strGredV6 = "T") Then
                                strSQL = "UPDATE kpmkv_pelajar_markah SET Gred_Kompeten='0' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                                'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                                'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                                strRet = oCommon.ExecuteSQL(strSQL)

                            ElseIf (strGredV7 = "C+" Or strGredV7 = "C" Or strGredV7 = "C-" Or strGredV7 = "D+" Or strGredV7 = "D-" Or strGredV7 = "E" Or strGredV7 = "G" Or strGredV7 = "T") Then
                                strSQL = "UPDATE kpmkv_pelajar_markah SET Gred_Kompeten='0' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                                'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                                'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                                strRet = oCommon.ExecuteSQL(strSQL)
                            Else
                                strSQL = "UPDATE kpmkv_pelajar_markah SET Gred_Kompeten='1' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                                'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                                'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                                strRet = oCommon.ExecuteSQL(strSQL)
                            End If


                        Case "8"
                            strSQL = "SELECT GredV1,GredV2,GredV3,GredV4,GredV5,GredV6,GredV7,GredV8 FROM kpmkv_pelajar_markah"
                            strSQL += " Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                            'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                            'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                            strRet = oCommon.getFieldValueEx(strSQL)

                            Dim ar_total As Array
                            ar_total = strRet.Split("|")
                            strGredV1 = ar_total(0)
                            strGredV2 = ar_total(1)
                            strGredV3 = ar_total(2)
                            strGredV4 = ar_total(3)
                            strGredV5 = ar_total(4)
                            strGredV6 = ar_total(5)
                            strGredV7 = ar_total(6)
                            strGredV8 = ar_total(7)

                            If (strGredV1 = "C+" Or strGredV1 = "C" Or strGredV1 = "C-" Or strGredV1 = "D+" Or strGredV1 = "D-" Or strGredV1 = "E" Or strGredV1 = "G" Or strGredV1 = "T") Then
                                strSQL = "UPDATE kpmkv_pelajar_markah SET Gred_Kompeten='0' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                                'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                                'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                                strRet = oCommon.ExecuteSQL(strSQL)

                            ElseIf (strGredV2 = "C+" Or strGredV2 = "C" Or strGredV2 = "C-" Or strGredV2 = "D+" Or strGredV2 = "D-" Or strGredV2 = "E" Or strGredV2 = "G" Or strGredV2 = "T") Then
                                strSQL = "UPDATE kpmkv_pelajar_markah SET Gred_Kompeten='0' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                                'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                                'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                                strRet = oCommon.ExecuteSQL(strSQL)

                            ElseIf (strGredV3 = "C+" Or strGredV3 = "C" Or strGredV3 = "C-" Or strGredV3 = "D+" Or strGredV3 = "D-" Or strGredV3 = "E" Or strGredV3 = "G" Or strGredV3 = "T") Then
                                strSQL = "UPDATE kpmkv_pelajar_markah SET Gred_Kompeten='0' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                                'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                                'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                                strRet = oCommon.ExecuteSQL(strSQL)

                            ElseIf (strGredV4 = "C+" Or strGredV4 = "C" Or strGredV4 = "C-" Or strGredV4 = "D+" Or strGredV4 = "D-" Or strGredV4 = "E" Or strGredV4 = "G" Or strGredV4 = "T") Then
                                strSQL = "UPDATE kpmkv_pelajar_markah SET Gred_Kompeten='0' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                                'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                                'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                                strRet = oCommon.ExecuteSQL(strSQL)

                            ElseIf (strGredV5 = "C+" Or strGredV5 = "C" Or strGredV5 = "C-" Or strGredV5 = "D+" Or strGredV5 = "D-" Or strGredV5 = "E" Or strGredV5 = "G" Or strGredV5 = "T") Then
                                strSQL = "UPDATE kpmkv_pelajar_markah SET Gred_Kompeten='0' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                                'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                                'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                                strRet = oCommon.ExecuteSQL(strSQL)

                            ElseIf (strGredV6 = "C+" Or strGredV6 = "C" Or strGredV6 = "C-" Or strGredV6 = "D+" Or strGredV6 = "D-" Or strGredV6 = "E" Or strGredV6 = "G" Or strGredV6 = "T") Then
                                strSQL = "UPDATE kpmkv_pelajar_markah SET Gred_Kompeten='0' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                                'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                                'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                                strRet = oCommon.ExecuteSQL(strSQL)

                            ElseIf (strGredV7 = "C+" Or strGredV7 = "C" Or strGredV7 = "C-" Or strGredV7 = "D+" Or strGredV7 = "D-" Or strGredV7 = "E" Or strGredV7 = "G" Or strGredV7 = "T") Then
                                strSQL = "UPDATE kpmkv_pelajar_markah SET Gred_Kompeten='0' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                                'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                                'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                                strRet = oCommon.ExecuteSQL(strSQL)

                            ElseIf (strGredV8 = "C+" Or strGredV8 = "C" Or strGredV8 = "C-" Or strGredV8 = "D+" Or strGredV8 = "D-" Or strGredV8 = "E" Or strGredV8 = "G" Or strGredV8 = "T") Then
                                strSQL = "UPDATE kpmkv_pelajar_markah SET Gred_Kompeten='0' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                                'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                                'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                                strRet = oCommon.ExecuteSQL(strSQL)

                            Else

                                strSQL = "UPDATE kpmkv_pelajar_markah SET Gred_Kompeten='1' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                                'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                                'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                                strRet = oCommon.ExecuteSQL(strSQL)
                            End If

                    End Select

                Next

            End If

        Catch ex As Exception
            lblMsg.Text = "System Error.Gred:" & ex.Message
        End Try

    End Sub

    Private Sub Vokasional_gredSMP()
        Try

            If datRespondent.Rows.Count = 1 Then

                For i As Integer = 0 To datRespondent.Rows.Count - 1

                    Dim Tahun As String
                    Dim Semester As String

                    strSQL = "SELECT Tahun, Semester FROM kpmkv_pelajar WHERE PelajarID = '" & datRespondent.DataKeys(i).Value.ToString & "'"
                    strRet = oCommon.getFieldValueEx(strSQL)

                    Dim ar_TahunSemester As Array
                    ar_TahunSemester = strRet.Split("|")

                    Tahun = ar_TahunSemester(0)
                    Semester = ar_TahunSemester(1)

                    strSQL = "SELECT KursusID FROM kpmkv_pelajar_markah WHERE PelajarID = '" & datRespondent.DataKeys(i).Value.ToString & "'"
                    Dim KursusID As String = oCommon.getFieldValue(strSQL)

                    '--count no of modul
                    Dim nCount As Integer = 0
                    strSQL = "SELECT COUNT(kpmkv_modul.KodModul) as CModul "
                    strSQL += " FROM kpmkv_modul LEFT OUTER JOIN"
                    strSQL += " kpmkv_kursus ON kpmkv_modul.KursusID = kpmkv_kursus.KursusID"
                    strSQL += " WHERE kpmkv_modul.KursusID='" & KursusID & "'"
                    strSQL += " AND  kpmkv_modul.Sesi='" & chkSesi.Text & "'"
                    strSQL += " AND  kpmkv_modul.Semester='" & Semester & "'"
                    strSQL += " AND  kpmkv_modul.Tahun='" & Tahun & "'"
                    nCount = oCommon.getFieldValueInt(strSQL)

                    strRet = BindData(datRespondent)

                    'PB
                    Dim PBAV1 As String
                    Dim PBTV1 As String
                    Dim PAAV1 As String
                    Dim PATV1 As String

                    Dim PBAV2 As String
                    Dim PBTV2 As String
                    Dim PAAV2 As String
                    Dim PATV2 As String

                    Dim PBAV3 As String
                    Dim PBTV3 As String
                    Dim PAAV3 As String
                    Dim PATV3 As String

                    Dim PBAV4 As String
                    Dim PBTV4 As String
                    Dim PAAV4 As String
                    Dim PATV4 As String

                    Dim PBAV5 As String
                    Dim PBTV5 As String
                    Dim PAAV5 As String
                    Dim PATV5 As String

                    Dim PBAV6 As String
                    Dim PBTV6 As String
                    Dim PAAV6 As String
                    Dim PATV6 As String

                    Dim PBAV7 As String
                    Dim PBTV7 As String
                    Dim PAAV7 As String
                    Dim PATV7 As String

                    'Dim PBAV8 As String
                    'Dim PBTV8 As String
                    'Dim PAAV8 As String
                    'Dim PATV8 As String

                    Dim SMP_PB As Double
                    Dim SMP_PA As Double

                    Dim SMP_Total As Double

                    Select Case nCount
                        Case "2"
                            strSQL = "SELECT PBAV1,PBTV1,PAAV1,PATV1,PBAV2,PBTV2,PAAV2,PATV2 FROM kpmkv_pelajar_markah"
                            strSQL += " Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                            'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                            'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                            strRet = oCommon.getFieldValueEx(strSQL)
                            'Response.Write(strSQL)
                            ''--get total
                            Dim ar_total As Array
                            ar_total = strRet.Split("|")
                            PBAV1 = ar_total(0)
                            PBTV1 = ar_total(1)
                            PAAV1 = ar_total(2)
                            PATV1 = ar_total(3)
                            PBAV2 = ar_total(4)
                            PBTV2 = ar_total(5)
                            PAAV2 = ar_total(6)
                            PATV2 = ar_total(7)

                            If String.IsNullOrEmpty(PBAV1) Then PBAV1 = 0
                            If String.IsNullOrEmpty(PBTV1) Then PBTV1 = 0
                            If String.IsNullOrEmpty(PAAV1) Then PAAV1 = 0
                            If String.IsNullOrEmpty(PATV1) Then PATV1 = 0
                            If String.IsNullOrEmpty(PBAV2) Then PBAV2 = 0
                            If String.IsNullOrEmpty(PBTV2) Then PBTV2 = 0
                            If String.IsNullOrEmpty(PAAV2) Then PAAV2 = 0
                            If String.IsNullOrEmpty(PATV2) Then PATV2 = 0

                            If (PBAV1) = -1 Or (PBTV1) = -1 Or (PAAV1) = -1 Or (PATV1) = -1 Or (PBAV2) = -1 Or (PBTV2) = -1 Or (PAAV2) = -1 Or (PATV2) = -1 Then
                                strSQL = "UPDATE kpmkv_pelajar_markah SET SMP_Total='-1' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                                'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                                'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                                strRet = oCommon.ExecuteSQL(strSQL)
                            Else
                                SMP_PA = (Convert.ToDouble(PAAV1) + CDbl(PATV1) + CDbl(PAAV2) + CDbl(PATV2)) / 2
                                SMP_PB = (CDbl(PBAV1) + CDbl(PBTV1) + CDbl(PBAV2) + CDbl(PBTV2)) / 2
                                SMP_Total = Math.Ceiling(SMP_PB + SMP_PA)

                                strSQL = "UPDATE kpmkv_pelajar_markah SET SMP_Total='" & SMP_Total & "' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                                'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                                'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                                strRet = oCommon.ExecuteSQL(strSQL)
                            End If

                        Case "3"
                            strSQL = "SELECT PBAV1,PBTV1,PAAV1,PATV1,PBAV2,PBTV2,PAAV2,PATV2,PBAV3,PBTV3,PAAV3,PATV3 FROM kpmkv_pelajar_markah"
                            strSQL += " Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                            'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                            'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                            strRet = oCommon.getFieldValueEx(strSQL)
                            'Response.Write(strSQL)
                            ''--get total
                            Dim ar_total As Array
                            ar_total = strRet.Split("|")
                            PBAV1 = ar_total(0)
                            PBTV1 = ar_total(1)
                            PAAV1 = ar_total(2)
                            PATV1 = ar_total(3)
                            PBAV2 = ar_total(4)
                            PBTV2 = ar_total(5)
                            PAAV2 = ar_total(6)
                            PATV2 = ar_total(7)
                            PBAV3 = ar_total(8)
                            PBTV3 = ar_total(9)
                            PAAV3 = ar_total(10)
                            PATV3 = ar_total(11)

                            If String.IsNullOrEmpty(PBAV1) Then PBAV1 = 0
                            If String.IsNullOrEmpty(PBTV1) Then PBTV1 = 0
                            If String.IsNullOrEmpty(PAAV1) Then PAAV1 = 0
                            If String.IsNullOrEmpty(PATV1) Then PATV1 = 0
                            If String.IsNullOrEmpty(PBAV2) Then PBAV2 = 0
                            If String.IsNullOrEmpty(PBTV2) Then PBTV2 = 0
                            If String.IsNullOrEmpty(PAAV2) Then PAAV2 = 0
                            If String.IsNullOrEmpty(PATV2) Then PATV2 = 0
                            If String.IsNullOrEmpty(PBAV3) Then PBAV3 = 0
                            If String.IsNullOrEmpty(PBTV3) Then PBTV3 = 0
                            If String.IsNullOrEmpty(PAAV3) Then PAAV3 = 0
                            If String.IsNullOrEmpty(PATV3) Then PATV3 = 0

                            If (PBAV1) = -1 Or (PBTV1) = -1 Or (PAAV1) = -1 Or (PATV1) = -1 Or (PBAV2) = -1 Or (PBTV2) = -1 Or (PAAV2) = -1 Or (PATV2) = -1 Or (PBAV3) = -1 Or (PBTV3) = -1 Or (PAAV3) = -1 Or (PATV3) = -1 Then
                                strSQL = "UPDATE kpmkv_pelajar_markah SET SMP_Total='-1' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                                'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                                'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                                strRet = oCommon.ExecuteSQL(strSQL)
                            Else

                                SMP_PA = (CDbl(PAAV1) + CDbl(PATV1) + CDbl(PAAV2) + CDbl(PATV2) + CDbl(PAAV3) + CDbl(PATV3)) / 3
                                SMP_PB = (CDbl(PBAV1) + CDbl(PBTV1) + CDbl(PBAV2) + CDbl(PBTV2) + CDbl(PBAV3) + CDbl(PBTV3)) / 3
                                SMP_Total = Math.Ceiling(SMP_PB + SMP_PA)

                                strSQL = "UPDATE kpmkv_pelajar_markah SET SMP_Total='" & SMP_Total & "' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                                'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                                'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                                strRet = oCommon.ExecuteSQL(strSQL)
                            End If

                        Case "4"
                            strSQL = "SELECT PBAV1,PBTV1,PAAV1,PATV1,PBAV2,PBTV2,PAAV2,PATV2,PBAV3,PBTV3,PAAV3,PATV3,PBAV4,PBTV4,PAAV4,PATV4 FROM kpmkv_pelajar_markah"
                            strSQL += " Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                            'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                            'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                            strRet = oCommon.getFieldValueEx(strSQL)
                            'Response.Write(strSQL)
                            ''--get total
                            Dim ar_total As Array
                            ar_total = strRet.Split("|")
                            PBAV1 = ar_total(0)
                            PBTV1 = ar_total(1)
                            PAAV1 = ar_total(2)
                            PATV1 = ar_total(3)
                            PBAV2 = ar_total(4)
                            PBTV2 = ar_total(5)
                            PAAV2 = ar_total(6)
                            PATV2 = ar_total(7)
                            PBAV3 = ar_total(8)
                            PBTV3 = ar_total(9)
                            PAAV3 = ar_total(10)
                            PATV3 = ar_total(11)
                            PBAV4 = ar_total(12)
                            PBTV4 = ar_total(13)
                            PAAV4 = ar_total(14)
                            PATV4 = ar_total(15)

                            If String.IsNullOrEmpty(PBAV1) Then PBAV1 = 0
                            If String.IsNullOrEmpty(PBTV1) Then PBTV1 = 0
                            If String.IsNullOrEmpty(PAAV1) Then PAAV1 = 0
                            If String.IsNullOrEmpty(PATV1) Then PATV1 = 0
                            If String.IsNullOrEmpty(PBAV2) Then PBAV2 = 0
                            If String.IsNullOrEmpty(PBTV2) Then PBTV2 = 0
                            If String.IsNullOrEmpty(PAAV2) Then PAAV2 = 0
                            If String.IsNullOrEmpty(PATV2) Then PATV2 = 0
                            If String.IsNullOrEmpty(PBAV3) Then PBAV3 = 0
                            If String.IsNullOrEmpty(PBTV3) Then PBTV3 = 0
                            If String.IsNullOrEmpty(PAAV3) Then PAAV3 = 0
                            If String.IsNullOrEmpty(PATV3) Then PATV3 = 0
                            If String.IsNullOrEmpty(PBAV4) Then PBAV4 = 0
                            If String.IsNullOrEmpty(PBTV4) Then PBTV4 = 0
                            If String.IsNullOrEmpty(PAAV4) Then PAAV4 = 0
                            If String.IsNullOrEmpty(PATV4) Then PATV4 = 0


                            If (PBAV1) = -1 Or (PBTV1) = -1 Or (PAAV1) = -1 Or (PATV1) = -1 Or (PBAV2) = -1 Or (PBTV2) = -1 Or (PAAV2) = -1 Or (PATV2) = -1 Or (PBAV3) = -1 Or (PBTV3) = -1 Or (PAAV3) = -1 Or (PATV3) = -1 Or (PBAV4) = -1 Or (PBTV4) = -1 Or (PAAV4) = -1 Or (PATV4) = -1 Then
                                strSQL = "UPDATE kpmkv_pelajar_markah SET SMP_Total='-1' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                                'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                                'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                                strRet = oCommon.ExecuteSQL(strSQL)
                            Else
                                'If String.IsNullOrEmpty(PBAV1) Then PBAV1 = 0
                                'If String.IsNullOrEmpty(PBTV1) Then PBTV1 = 0
                                'If String.IsNullOrEmpty(PAAV1) Then PAAV1 = 0
                                'If String.IsNullOrEmpty(PATV1) Then PATV1 = 0
                                'If String.IsNullOrEmpty(PBAV2) Then PBAV2 = 0
                                'If String.IsNullOrEmpty(PBTV2) Then PBTV2 = 0
                                'If String.IsNullOrEmpty(PAAV2) Then PAAV2 = 0
                                'If String.IsNullOrEmpty(PATV2) Then PATV2 = 0
                                'If String.IsNullOrEmpty(PBAV3) Then PBAV3 = 0
                                'If String.IsNullOrEmpty(PBTV3) Then PBTV3 = 0
                                'If String.IsNullOrEmpty(PAAV3) Then PAAV3 = 0
                                'If String.IsNullOrEmpty(PATV3) Then PATV3 = 0
                                'If String.IsNullOrEmpty(PBAV4) Then PBAV4 = 0
                                'If String.IsNullOrEmpty(PBTV4) Then PBTV4 = 0
                                'If String.IsNullOrEmpty(PAAV4) Then PAAV4 = 0
                                'If String.IsNullOrEmpty(PATV4) Then PATV4 = 0

                                SMP_PA = (Convert.ToDouble(PAAV1) + Convert.ToDouble(PATV1) + Convert.ToDouble(PAAV2) + Convert.ToDouble(PATV2) + Convert.ToDouble(PAAV3) + Convert.ToDouble(PATV3) + Convert.ToDouble(PAAV4) + Convert.ToDouble(PATV4)) / 4
                                SMP_PB = (Convert.ToDouble(PBAV1) + Convert.ToDouble(PBTV1) + Convert.ToDouble(PBAV2) + Convert.ToDouble(PBTV2) + Convert.ToDouble(PBAV3) + Convert.ToDouble(PBTV3) + Convert.ToDouble(PBAV4) + Convert.ToDouble(PBTV4)) / 4
                                SMP_Total = Math.Ceiling(SMP_PB + SMP_PA)

                                strSQL = "UPDATE kpmkv_pelajar_markah SET SMP_Total='" & SMP_Total & "' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                                'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                                'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                                strRet = oCommon.ExecuteSQL(strSQL)
                            End If

                        Case "5"
                            strSQL = "SELECT PBAV1,PBTV1,PAAV1,PATV1,PBAV2,PBTV2,PAAV2,PATV2,PBAV3,PBTV3,PAAV3,PATV3,PBAV4,PBTV4,PAAV4,PATV4,PBAV5,PBTV5,PAAV5,PATV5 FROM kpmkv_pelajar_markah"
                            strSQL += " Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                            'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                            'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                            strRet = oCommon.getFieldValueEx(strSQL)
                            'Response.Write(strSQL)
                            ''--get total
                            Dim ar_total As Array
                            ar_total = strRet.Split("|")
                            PBAV1 = ar_total(0)
                            PBTV1 = ar_total(1)
                            PAAV1 = ar_total(2)
                            PATV1 = ar_total(3)
                            PBAV2 = ar_total(4)
                            PBTV2 = ar_total(5)
                            PAAV2 = ar_total(6)
                            PATV2 = ar_total(7)
                            PBAV3 = ar_total(8)
                            PBTV3 = ar_total(9)
                            PAAV3 = ar_total(10)
                            PATV3 = ar_total(11)
                            PBAV4 = ar_total(12)
                            PBTV4 = ar_total(13)
                            PAAV4 = ar_total(14)
                            PATV4 = ar_total(15)
                            PBAV5 = ar_total(16)
                            PBTV5 = ar_total(17)
                            PAAV5 = ar_total(18)
                            PATV5 = ar_total(19)


                            If String.IsNullOrEmpty(PBAV1) Then PBAV1 = 0
                            If String.IsNullOrEmpty(PBTV1) Then PBTV1 = 0
                            If String.IsNullOrEmpty(PAAV1) Then PAAV1 = 0
                            If String.IsNullOrEmpty(PATV1) Then PATV1 = 0
                            If String.IsNullOrEmpty(PBAV2) Then PBAV2 = 0
                            If String.IsNullOrEmpty(PBTV2) Then PBTV2 = 0
                            If String.IsNullOrEmpty(PAAV2) Then PAAV2 = 0
                            If String.IsNullOrEmpty(PATV2) Then PATV2 = 0
                            If String.IsNullOrEmpty(PBAV3) Then PBAV3 = 0
                            If String.IsNullOrEmpty(PBTV3) Then PBTV3 = 0
                            If String.IsNullOrEmpty(PAAV3) Then PAAV3 = 0
                            If String.IsNullOrEmpty(PATV3) Then PATV3 = 0
                            If String.IsNullOrEmpty(PBAV4) Then PBAV4 = 0
                            If String.IsNullOrEmpty(PBTV4) Then PBTV4 = 0
                            If String.IsNullOrEmpty(PAAV4) Then PAAV4 = 0
                            If String.IsNullOrEmpty(PATV4) Then PATV4 = 0
                            If String.IsNullOrEmpty(PBAV5) Then PBAV5 = 0
                            If String.IsNullOrEmpty(PBTV5) Then PBTV5 = 0
                            If String.IsNullOrEmpty(PAAV5) Then PAAV5 = 0
                            If String.IsNullOrEmpty(PATV5) Then PATV5 = 0


                            If (PBAV1) = -1 Or (PBTV1) = -1 Or (PAAV1) = -1 Or (PATV1) = -1 Or (PBAV2) = -1 Or (PBTV2) = -1 Or (PAAV2) = -1 Or (PATV2) = -1 Or (PBAV3) = -1 Or (PBTV3) = -1 Or (PAAV3) = -1 Or (PATV3) = -1 Or (PBAV4) = -1 Or (PBTV4) = -1 Or (PAAV4) = -1 Or (PATV4) = -1 Or (PBAV5) = -1 Or (PBTV5) = -1 Or (PAAV5) = -1 Or (PATV5) = -1 Then
                                strSQL = "UPDATE kpmkv_pelajar_markah SET SMP_Total='-1' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                                'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                                'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                                strRet = oCommon.ExecuteSQL(strSQL)
                            Else

                                SMP_PA = (CDbl(PAAV1) + CDbl(PATV1) + CDbl(PAAV2) + CDbl(PATV2) + CDbl(PAAV3) + CDbl(PATV3) + CDbl(PAAV4) + CDbl(PATV4) + CDbl(PAAV5) + CDbl(PATV5)) / 5
                                SMP_PB = (CDbl(PBAV1) + CDbl(PBTV1) + CDbl(PBAV2) + CDbl(PBTV2) + CDbl(PBAV3) + CDbl(PBTV3) + CDbl(PBAV4) + CDbl(PBTV4) + CDbl(PBAV5) + CDbl(PBTV5) / 5)
                                SMP_Total = Math.Ceiling(SMP_PB + SMP_PA)

                                strSQL = "UPDATE kpmkv_pelajar_markah SET SMP_Total='" & SMP_Total & "' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                                'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                                'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                                strRet = oCommon.ExecuteSQL(strSQL)
                            End If

                        Case "6"
                            strSQL = "SELECT PBAV1,PBTV1,PAAV1,PATV1,PBAV2,PBTV2,PAAV2,PATV2,PBAV3,PBTV3,PAAV3,PATV3,PBAV4,PBTV4,PAAV4,PATV4,PBAV5,PBTV5,PAAV5,PATV5,PBAV6,PBTV6,PAAV6,PATV6 FROM kpmkv_pelajar_markah"
                            strSQL += " Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                            'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                            'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                            strRet = oCommon.getFieldValueEx(strSQL)
                            'Response.Write(strSQL)
                            ''--get total
                            Dim ar_total As Array
                            ar_total = strRet.Split("|")
                            PBAV1 = ar_total(0)
                            PBTV1 = ar_total(1)
                            PAAV1 = ar_total(2)
                            PATV1 = ar_total(3)
                            PBAV2 = ar_total(4)
                            PBTV2 = ar_total(5)
                            PAAV2 = ar_total(6)
                            PATV2 = ar_total(7)
                            PBAV3 = ar_total(8)
                            PBTV3 = ar_total(9)
                            PAAV3 = ar_total(10)
                            PATV3 = ar_total(11)
                            PBAV4 = ar_total(12)
                            PBTV4 = ar_total(13)
                            PAAV4 = ar_total(14)
                            PATV4 = ar_total(15)
                            PBAV5 = ar_total(16)
                            PBTV5 = ar_total(17)
                            PAAV5 = ar_total(18)
                            PATV5 = ar_total(19)
                            PBAV6 = ar_total(20)
                            PBTV6 = ar_total(21)
                            PAAV6 = ar_total(22)
                            PATV6 = ar_total(23)

                            If String.IsNullOrEmpty(PBAV1) Then PBAV1 = 0
                            If String.IsNullOrEmpty(PBTV1) Then PBTV1 = 0
                            If String.IsNullOrEmpty(PAAV1) Then PAAV1 = 0
                            If String.IsNullOrEmpty(PATV1) Then PATV1 = 0
                            If String.IsNullOrEmpty(PBAV2) Then PBAV2 = 0
                            If String.IsNullOrEmpty(PBTV2) Then PBTV2 = 0
                            If String.IsNullOrEmpty(PAAV2) Then PAAV2 = 0
                            If String.IsNullOrEmpty(PATV2) Then PATV2 = 0
                            If String.IsNullOrEmpty(PBAV3) Then PBAV3 = 0
                            If String.IsNullOrEmpty(PBTV3) Then PBTV3 = 0
                            If String.IsNullOrEmpty(PAAV3) Then PAAV3 = 0
                            If String.IsNullOrEmpty(PATV3) Then PATV3 = 0
                            If String.IsNullOrEmpty(PBAV4) Then PBAV4 = 0
                            If String.IsNullOrEmpty(PBTV4) Then PBTV4 = 0
                            If String.IsNullOrEmpty(PAAV4) Then PAAV4 = 0
                            If String.IsNullOrEmpty(PATV4) Then PATV4 = 0
                            If String.IsNullOrEmpty(PBAV5) Then PBAV5 = 0
                            If String.IsNullOrEmpty(PBTV5) Then PBTV5 = 0
                            If String.IsNullOrEmpty(PAAV5) Then PAAV5 = 0
                            If String.IsNullOrEmpty(PATV5) Then PATV5 = 0
                            If String.IsNullOrEmpty(PBAV6) Then PBAV6 = 0
                            If String.IsNullOrEmpty(PBTV6) Then PBTV6 = 0
                            If String.IsNullOrEmpty(PAAV6) Then PAAV6 = 0
                            If String.IsNullOrEmpty(PATV6) Then PATV6 = 0

                            If (PBAV1) = -1 Or (PBTV1) = -1 Or (PAAV1) = -1 Or (PATV1) = -1 Or (PBAV2) = -1 Or (PBTV2) = -1 Or (PAAV2) = -1 Or (PATV2) = -1 Or (PBAV3) = -1 Or (PBTV3) = -1 Or (PAAV3) = -1 Or (PATV3) = -1 Or (PBAV4) = -1 Or (PBTV4) = -1 Or (PAAV4) = -1 Or (PATV4) = -1 Or (PBAV5) = -1 Or (PBTV5) = -1 Or (PAAV5) = -1 Or (PATV5) = -1 Or (PBAV6) = -1 Or (PBTV6) = -1 Or (PAAV6) = -1 Or (PATV6) = -1 Then
                                strSQL = "UPDATE kpmkv_pelajar_markah SET SMP_Total='-1' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                                'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                                'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                                strRet = oCommon.ExecuteSQL(strSQL)
                            Else



                                SMP_PA = (CDbl(PAAV1) + CDbl(PATV1) + CDbl(PAAV2) + CDbl(PATV2) + CDbl(PAAV3) + CDbl(PATV3) + CDbl(PAAV4) + CDbl(PATV4) + CDbl(PAAV5) + CDbl(PATV5) + CDbl(PAAV6) + CDbl(PATV6)) / 6
                                SMP_PB = (CDbl(PBAV1) + CDbl(PBTV1) + CDbl(PBAV2) + CDbl(PBTV2) + CDbl(PBAV3) + CDbl(PBTV3) + CDbl(PBAV4) + CDbl(PBTV4) + CDbl(PBAV5) + CDbl(PBTV5) + CDbl(PBAV6) + CDbl(PBTV6)) / 6
                                SMP_Total = Math.Ceiling(SMP_PB + SMP_PA)

                                strSQL = "UPDATE kpmkv_pelajar_markah SET SMP_Total='" & SMP_Total & "' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                                'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                                'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                                strRet = oCommon.ExecuteSQL(strSQL)
                            End If

                        Case "7"
                            strSQL = "SELECT PBAV1,PBTV1,PAAV1,PATV1,PBAV2,PBTV2,PAAV2,PATV2,PBAV3,PBTV3,PAAV3,PATV3,PBAV4,PBTV4,PAAV4,PATV4,PBAV5,PBTV5,PAAV5,PATV5,PBAV6,PBTV6,PAAV6,PATV6,PBAV7,PBTV7,PAAV7,PATV7 FROM kpmkv_pelajar_markah"
                            strSQL += " Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                            'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                            'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                            strRet = oCommon.getFieldValueEx(strSQL)
                            'Response.Write(strSQL)
                            ''--get total
                            Dim ar_total As Array
                            ar_total = strRet.Split("|")
                            PBAV1 = ar_total(0)
                            PBTV1 = ar_total(1)
                            PAAV1 = ar_total(2)
                            PATV1 = ar_total(3)
                            PBAV2 = ar_total(4)
                            PBTV2 = ar_total(5)
                            PAAV2 = ar_total(6)
                            PATV2 = ar_total(7)
                            PBAV3 = ar_total(8)
                            PBTV3 = ar_total(9)
                            PAAV3 = ar_total(10)
                            PATV3 = ar_total(11)
                            PBAV4 = ar_total(12)
                            PBTV4 = ar_total(13)
                            PAAV4 = ar_total(14)
                            PATV4 = ar_total(15)
                            PBAV5 = ar_total(16)
                            PBTV5 = ar_total(17)
                            PAAV5 = ar_total(18)
                            PATV5 = ar_total(19)
                            PBAV6 = ar_total(20)
                            PBTV6 = ar_total(21)
                            PAAV6 = ar_total(22)
                            PATV6 = ar_total(23)
                            PBAV7 = ar_total(24)
                            PBTV7 = ar_total(25)
                            PAAV7 = ar_total(26)
                            PATV7 = ar_total(27)


                            If String.IsNullOrEmpty(PBAV1) Then PBAV1 = 0
                            If String.IsNullOrEmpty(PBTV1) Then PBTV1 = 0
                            If String.IsNullOrEmpty(PAAV1) Then PAAV1 = 0
                            If String.IsNullOrEmpty(PATV1) Then PATV1 = 0
                            If String.IsNullOrEmpty(PBAV2) Then PBAV2 = 0
                            If String.IsNullOrEmpty(PBTV2) Then PBTV2 = 0
                            If String.IsNullOrEmpty(PAAV2) Then PAAV2 = 0
                            If String.IsNullOrEmpty(PATV2) Then PATV2 = 0
                            If String.IsNullOrEmpty(PBAV3) Then PBAV3 = 0
                            If String.IsNullOrEmpty(PBTV3) Then PBTV3 = 0
                            If String.IsNullOrEmpty(PAAV3) Then PAAV3 = 0
                            If String.IsNullOrEmpty(PATV3) Then PATV3 = 0
                            If String.IsNullOrEmpty(PBAV4) Then PBAV4 = 0
                            If String.IsNullOrEmpty(PBTV4) Then PBTV4 = 0
                            If String.IsNullOrEmpty(PAAV4) Then PAAV4 = 0
                            If String.IsNullOrEmpty(PATV4) Then PATV4 = 0
                            If String.IsNullOrEmpty(PBAV5) Then PBAV5 = 0
                            If String.IsNullOrEmpty(PBTV5) Then PBTV5 = 0
                            If String.IsNullOrEmpty(PAAV5) Then PAAV5 = 0
                            If String.IsNullOrEmpty(PATV5) Then PATV5 = 0
                            If String.IsNullOrEmpty(PBAV6) Then PBAV6 = 0
                            If String.IsNullOrEmpty(PBTV6) Then PBTV6 = 0
                            If String.IsNullOrEmpty(PAAV6) Then PAAV6 = 0
                            If String.IsNullOrEmpty(PATV6) Then PATV6 = 0
                            If String.IsNullOrEmpty(PBAV7) Then PBAV7 = 0
                            If String.IsNullOrEmpty(PBTV7) Then PBTV7 = 0
                            If String.IsNullOrEmpty(PAAV7) Then PAAV7 = 0
                            If String.IsNullOrEmpty(PATV7) Then PATV7 = 0

                            If (PBAV1) = -1 Or (PBTV1) = -1 Or (PAAV1) = -1 Or (PATV1) = -1 Or (PBAV2) = -1 Or (PBTV2) = -1 Or (PAAV2) = -1 Or (PATV2) = -1 Or (PBAV3) = -1 Or (PBTV3) = -1 Or (PAAV3) = -1 Or (PATV3) = -1 Or (PBAV4) = -1 Or (PBTV4) = -1 Or (PAAV4) = -1 Or (PATV4) = -1 Or (PBAV5) = -1 Or (PBTV5) = -1 Or (PAAV5) = -1 Or (PATV5) = -1 Or (PBAV6) = -1 Or (PBTV6) = -1 Or (PAAV6) = -1 Or (PATV6) = -1 Or (PBAV7) = -1 Or (PBTV7) = -1 Or (PAAV7) = -1 Or (PATV7) = -1 Then
                                strSQL = "UPDATE kpmkv_pelajar_markah SET SMP_Total='-1' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                                'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                                'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                                strRet = oCommon.ExecuteSQL(strSQL)
                            Else


                                SMP_PA = (CDbl(PAAV1) + CDbl(PATV1) + CDbl(PAAV2) + CDbl(PATV2) + CDbl(PAAV3) + CDbl(PATV3) + CDbl(PAAV4) + CDbl(PATV4) + CDbl(PAAV5) + CDbl(PATV5) + CDbl(PAAV6) + CDbl(PATV6) + CDbl(PAAV7) + CDbl(PATV7)) / 7
                                SMP_PB = (CDbl(PBAV1) + CDbl(PBTV1) + CDbl(PBAV2) + CDbl(PBTV2) + CDbl(PBAV3) + CDbl(PBTV3) + CDbl(PBAV4) + CDbl(PBTV4) + CDbl(PBAV5) + CDbl(PBTV5) + CDbl(PBAV6) + CDbl(PBTV6) + CDbl(PBAV7) + CDbl(PBTV7)) / 7
                                SMP_Total = Math.Ceiling(SMP_PB + SMP_PA)

                                strSQL = "UPDATE kpmkv_pelajar_markah SET SMP_Total='" & SMP_Total & "' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                                'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                                'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                                strRet = oCommon.ExecuteSQL(strSQL)
                            End If

                            'Case "8"
                            '    strSQL = "SELECT PBAV1,PBTV1,PAAV1,PATV1,PBAV2,PBTV2,PAAV2,PATV2,PBAV3,PBTV3,PAAV3,PATV3,PBAV4,PBTV4,PAAV4,PATV4,PBAV5,PBTV5,PAAV5,PATV5,PBAV6,PBTV6,PAAV6,PATV6,PBAV7,PBTV7,PAAV7,PATV7,PBAV8,PBTV8,PAAV8,PATV8 FROM kpmkv_pelajar_markah"
                            '    strSQL += " Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                            '    strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                            '    strSQL += " AND Sesi='" & chkSesi.Text & "'"
                            '    strRet = oCommon.getFieldValueEx(strSQL)
                            '    'Response.Write(strSQL)
                            '    ''--get total
                            '    Dim ar_total As Array
                            '    ar_total = strRet.Split("|")
                            '    PBAV1 = ar_total(0)
                            '    PBTV1 = ar_total(1)
                            '    PAAV1 = ar_total(2)
                            '    PATV1 = ar_total(3)
                            '    PBAV2 = ar_total(4)
                            '    PBTV2 = ar_total(5)
                            '    PAAV2 = ar_total(6)
                            '    PATV2 = ar_total(7)
                            '    PBAV3 = ar_total(8)
                            '    PBTV3 = ar_total(9)
                            '    PAAV3 = ar_total(10)
                            '    PATV3 = ar_total(11)
                            '    PBAV4 = ar_total(12)
                            '    PBTV4 = ar_total(13)
                            '    PAAV4 = ar_total(14)
                            '    PATV4 = ar_total(15)
                            '    PBAV5 = ar_total(16)
                            '    PBTV5 = ar_total(17)
                            '    PAAV5 = ar_total(18)
                            '    PATV5 = ar_total(19)
                            '    PBAV6 = ar_total(20)
                            '    PBTV6 = ar_total(21)
                            '    PAAV6 = ar_total(22)
                            '    PATV6 = ar_total(23)
                            '    PBAV7 = ar_total(24)
                            '    PBTV7 = ar_total(25)
                            '    PAAV7 = ar_total(26)
                            '    PATV7 = ar_total(27)
                            '    PBAV8 = ar_total(28)
                            '    PBTV8 = ar_total(29)
                            '    PAAV8 = ar_total(30)
                            '    PATV8 = ar_total(31)


                            '    If String.IsNullOrEmpty(PBAV1) Then PBAV1 = 0
                            '    If String.IsNullOrEmpty(PBTV1) Then PBTV1 = 0
                            '    If String.IsNullOrEmpty(PAAV1) Then PAAV1 = 0
                            '    If String.IsNullOrEmpty(PATV1) Then PATV1 = 0
                            '    If String.IsNullOrEmpty(PBAV2) Then PBAV2 = 0
                            '    If String.IsNullOrEmpty(PBTV2) Then PBTV2 = 0
                            '    If String.IsNullOrEmpty(PAAV2) Then PAAV2 = 0
                            '    If String.IsNullOrEmpty(PATV2) Then PATV2 = 0
                            '    If String.IsNullOrEmpty(PBAV3) Then PBAV3 = 0
                            '    If String.IsNullOrEmpty(PBTV3) Then PBTV3 = 0
                            '    If String.IsNullOrEmpty(PAAV3) Then PAAV3 = 0
                            '    If String.IsNullOrEmpty(PATV3) Then PATV3 = 0
                            '    If String.IsNullOrEmpty(PBAV4) Then PBAV4 = 0
                            '    If String.IsNullOrEmpty(PBTV4) Then PBTV4 = 0
                            '    If String.IsNullOrEmpty(PAAV4) Then PAAV4 = 0
                            '    If String.IsNullOrEmpty(PATV4) Then PATV4 = 0
                            '    If String.IsNullOrEmpty(PBAV5) Then PBAV5 = 0
                            '    If String.IsNullOrEmpty(PBTV5) Then PBTV5 = 0
                            '    If String.IsNullOrEmpty(PAAV5) Then PAAV5 = 0
                            '    If String.IsNullOrEmpty(PATV5) Then PATV5 = 0
                            '    If String.IsNullOrEmpty(PBAV6) Then PBAV6 = 0
                            '    If String.IsNullOrEmpty(PBTV6) Then PBTV6 = 0
                            '    If String.IsNullOrEmpty(PAAV6) Then PAAV6 = 0
                            '    If String.IsNullOrEmpty(PATV6) Then PATV6 = 0
                            '    If String.IsNullOrEmpty(PBAV7) Then PBAV7 = 0
                            '    If String.IsNullOrEmpty(PBTV7) Then PBTV7 = 0
                            '    If String.IsNullOrEmpty(PAAV7) Then PAAV7 = 0
                            '    If String.IsNullOrEmpty(PATV7) Then PATV7 = 0
                            '    If String.IsNullOrEmpty(PBAV8) Then PBAV8 = 0
                            '    If String.IsNullOrEmpty(PBTV8) Then PBTV8 = 0
                            '    If String.IsNullOrEmpty(PAAV8) Then PAAV8 = 0
                            '    If String.IsNullOrEmpty(PATV8) Then PATV8 = 0

                            '    If (PBAV1) = -1 Or (PBTV1) = -1 Or (PAAV1) = -1 Or (PATV1) = -1 Or (PBAV2) = -1 Or (PBTV2) = -1 Or (PAAV2) = -1 Or (PATV2) = -1 Or (PBAV3) = -1 Or (PBTV3) = -1 Or (PAAV3) = -1 Or (PATV3) = -1 Or (PBAV4) = -1 Or (PBTV4) = -1 Or (PAAV4) = -1 Or (PATV4) = -1 Or (PBAV5) = -1 Or (PBTV5) = -1 Or (PAAV5) = -1 Or (PATV5) = -1 Or (PBAV6) = -1 Or (PBTV6) = -1 Or (PAAV6) = -1 Or (PATV6) = -1 Or (PBAV7) = -1 Or (PBTV7) = -1 Or (PAAV7) = -1 Or (PATV7) = -1 Or (PBAV8) = -1 Or (PBTV8) = -1 Or (PAAV8) = -1 Or (PATV8) = -1 Then
                            '        strSQL = "UPDATE kpmkv_pelajar_markah SET SMP_Total='-1' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                            '        strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                            '        strSQL += " AND Sesi='" & chkSesi.Text & "'"
                            '        strRet = oCommon.ExecuteSQL(strSQL)
                            '    Else
                            '        SMP_PB = ((PBAV1 + PBTV1 + PBAV2 + PBTV2 + PBAV3 + PBTV3 + PBAV4 + PBTV4 + PBAV5 + PBTV5 + PBAV6 + PBTV6 + PBAV7 + PBTV7 + PBAV8 + PBTV8) / 8)
                            '        SMP_PA = ((PAAV1 + PATV1 + PAAV2 + PATV2 + PAAV3 + PATV3 + PAAV4 + PATV4 + PAAV5 + PATV5 + PAAV6 + PATV6 + PAAV7 + PATV7 + PAAV8 + PATV8) / 8)
                            '        SMP_Total = Math.Ceiling(SMP_PB + SMP_PA)

                            '        strSQL = "UPDATE kpmkv_pelajar_markah SET SMP_Total='" & SMP_Total & "' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                            '        strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                            '        strSQL += " AND Sesi='" & chkSesi.Text & "'"
                            '        strRet = oCommon.ExecuteSQL(strSQL)
                            '    End If

                    End Select
                Next

            End If

        Catch ex As Exception
            lblMsg.Text = "System Error.GredSMP:" & ex.Message
        End Try

    End Sub

    Private Sub Vokasional_SMP_PB()
        Try

            If datRespondent.Rows.Count = 1 Then

                For i As Integer = 0 To datRespondent.Rows.Count - 1

                    Dim Tahun As String
                    Dim Semester As String

                    strSQL = "SELECT Tahun, Semester FROM kpmkv_pelajar WHERE PelajarID = '" & datRespondent.DataKeys(i).Value.ToString & "'"
                    strRet = oCommon.getFieldValueEx(strSQL)

                    Dim ar_TahunSemester As Array
                    ar_TahunSemester = strRet.Split("|")

                    Tahun = ar_TahunSemester(0)
                    Semester = ar_TahunSemester(1)

                    strSQL = "SELECT KursusID FROM kpmkv_pelajar_markah WHERE PelajarID = '" & datRespondent.DataKeys(i).Value.ToString & "'"
                    Dim KursusID As String = oCommon.getFieldValue(strSQL)

                    '--count no of modul
                    Dim nCount As Integer = 0
                    strSQL = "SELECT COUNT(kpmkv_modul.KodModul) as CModul "
                    strSQL += " FROM kpmkv_modul LEFT OUTER JOIN"
                    strSQL += " kpmkv_kursus ON kpmkv_modul.KursusID = kpmkv_kursus.KursusID"
                    strSQL += " WHERE kpmkv_modul.KursusID='" & KursusID & "'"
                    strSQL += " AND  kpmkv_modul.Sesi='" & chkSesi.Text & "'"
                    strSQL += " AND  kpmkv_modul.Semester='" & Semester & "'"
                    strSQL += " AND  kpmkv_modul.Tahun='" & Tahun & "'"
                    nCount = oCommon.getFieldValueInt(strSQL)
                    'PB
                    Dim PBA1 As String
                    Dim PBA2 As String
                    Dim PBA3 As String
                    Dim PBA4 As String
                    Dim PBA5 As String
                    Dim PBA6 As String
                    Dim PBA7 As String
                    Dim PBA8 As String
                    'PB
                    Dim PBT1 As String
                    Dim PBT2 As String
                    Dim PBT3 As String
                    Dim PBT4 As String
                    Dim PBT5 As String
                    Dim PBT6 As String
                    Dim PBT7 As String
                    Dim PBT8 As String

                    Dim PB_Amali As Double
                    Dim PB_Amali_K As Double
                    Dim PB_Teori As Double
                    Dim PB_Teori_K As Double
                    Dim SMP_PB As Double

                    Dim Skor As String

                    'get score SMP_PB
                    strSQL = "SELECT SMP_PB FROM kpmkv_skor_svm"
                    strSQL += " WHERE Tahun='" & Tahun & "' AND Semester='" & Semester & "' AND Sesi='" & chkSesi.Text & "'"
                    Skor = oCommon.getFieldValue(strSQL)

                    strRet = BindData(datRespondent)

                    'PBAV1,PBTV1
                    'PBAV2,PBTV2
                    Select Case nCount
                        Case "2"
                            strSQL = "SELECT PBAV1,PBAV2,PBTV1,PBTV2 FROM kpmkv_pelajar_markah"
                            strSQL += " Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                            'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                            'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                            strRet = oCommon.getFieldValueEx(strSQL)
                            'Response.Write(strSQL)
                            ''--get total
                            Dim ar_total As Array
                            ar_total = strRet.Split("|")
                            PBA1 = ar_total(0)
                            PBA2 = ar_total(1)
                            PBT1 = ar_total(2)
                            PBT2 = ar_total(3)

                            'check pb
                            If (PBA1) = -1 Or (PBA2) = -1 Or (PBT1) = -1 Or (PBT2) = -1 Then
                                strSQL = "UPDATE kpmkv_pelajar_markah SET SMP_PB='0' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                                'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                                'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                                strRet = oCommon.ExecuteSQL(strSQL)
                            Else
                                PB_Amali = CDbl(PBA1) + CDbl(PBA2)
                                PB_Teori = CDbl(PBT1) + CDbl(PBT2)

                                PB_Amali_K = (PB_Amali / 2)
                                PB_Teori_K = (PB_Teori / 2)

                                SMP_PB = (PB_Amali_K + PB_Teori_K)

                                If SMP_PB >= CDbl(Skor) Then
                                    strSQL = "UPDATE kpmkv_pelajar_markah SET SMP_PB='1' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                                    'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                                    'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                                    strRet = oCommon.ExecuteSQL(strSQL)
                                Else
                                    strSQL = "UPDATE kpmkv_pelajar_markah SET SMP_PB='0' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                                    'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                                    'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                                    strRet = oCommon.ExecuteSQL(strSQL)
                                End If
                            End If

                        Case "3"
                            strSQL = "SELECT PBAV1,PBAV2,PBAV3,PBTV1,PBTV2,PBTV3 FROM kpmkv_pelajar_markah"
                            strSQL += " Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                            'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                            'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                            strRet = oCommon.getFieldValueEx(strSQL)
                            'Response.Write(strSQL)
                            ''--get total
                            Dim ar_total As Array
                            ar_total = strRet.Split("|")
                            PBA1 = ar_total(0)
                            PBA2 = ar_total(1)
                            PBA3 = ar_total(2)
                            PBT1 = ar_total(3)
                            PBT2 = ar_total(4)
                            PBT3 = ar_total(5)

                            If (PBA1) = -1 Or (PBA2) = -1 Or (PBA3) = -1 Or (PBT1) = -1 Or (PBT2) = -1 Or (PBT3) = -1 Then
                                strSQL = "UPDATE kpmkv_pelajar_markah SET SMP_PB='0' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                                'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                                'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                                strRet = oCommon.ExecuteSQL(strSQL)
                            Else

                                PB_Amali = CDbl(PBA1) + CDbl(PBA2) + CDbl(PBA3)
                                PB_Teori = CDbl(PBT1) + CDbl(PBT2) + CDbl(PBT3)

                                PB_Amali_K = (PB_Amali / 3)
                                PB_Teori_K = (PB_Teori / 3)

                                SMP_PB = (PB_Amali_K + PB_Teori_K)

                                If SMP_PB >= CDbl(Skor) Then
                                    strSQL = "UPDATE kpmkv_pelajar_markah SET SMP_PB='1' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                                    'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                                    'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                                    strRet = oCommon.ExecuteSQL(strSQL)
                                Else
                                    strSQL = "UPDATE kpmkv_pelajar_markah SET SMP_PB='0' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                                    'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                                    'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                                    strRet = oCommon.ExecuteSQL(strSQL)
                                End If
                            End If
                        Case "4"
                            strSQL = "SELECT PBAV1,PBAV2,PBAV3,PBAV4,PBTV1,PBTV2,PBTV3,PBTV4 FROM kpmkv_pelajar_markah"
                            strSQL += " Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                            'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                            'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                            strRet = oCommon.getFieldValueEx(strSQL)
                            'Response.Write(strSQL)
                            ''--get total
                            Dim ar_total As Array
                            ar_total = strRet.Split("|")
                            PBA1 = ar_total(0)
                            PBA2 = ar_total(1)
                            PBA3 = ar_total(2)
                            PBA4 = ar_total(3)
                            PBT1 = ar_total(4)
                            PBT2 = ar_total(5)
                            PBT3 = ar_total(6)
                            PBT4 = ar_total(7)

                            If (PBA1) = -1 Or (PBA2) = -1 Or (PBA3) = -1 Or (PBA4) = -1 Or (PBT1) = -1 Or (PBT2) = -1 Or (PBT3) = -1 Or (PBT4) = -1 Then
                                strSQL = "UPDATE kpmkv_pelajar_markah SET SMP_PB='0' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                                'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                                'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                                strRet = oCommon.ExecuteSQL(strSQL)
                            Else

                                PB_Amali = CDbl(PBA1) + CDbl(PBA2) + CDbl(PBA3) + CDbl(PBA4)
                                PB_Teori = CDbl(PBT1) + CDbl(PBT2) + CDbl(PBT3) + CDbl(PBT4)

                                PB_Amali_K = (PB_Amali / 4)
                                PB_Teori_K = (PB_Teori / 4)

                                SMP_PB = (PB_Amali_K + PB_Teori_K)

                                If SMP_PB >= CDbl(Skor) Then
                                    strSQL = "UPDATE kpmkv_pelajar_markah SET SMP_PB='1' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                                    'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                                    'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                                    strRet = oCommon.ExecuteSQL(strSQL)
                                Else
                                    strSQL = "UPDATE kpmkv_pelajar_markah SET SMP_PB='0' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                                    'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                                    'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                                    strRet = oCommon.ExecuteSQL(strSQL)
                                End If

                            End If
                        Case "5"
                            strSQL = "SELECT PBAV1,PBAV2,PBAV3,PBAV4,PBAV5,PBTV1,PBTV2,PBTV3,PBTV4,PBTV5 FROM kpmkv_pelajar_markah"
                            strSQL += " Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                            'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                            'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                            strRet = oCommon.getFieldValueEx(strSQL)
                            'Response.Write(strSQL)
                            ''--get total
                            Dim ar_total As Array
                            ar_total = strRet.Split("|")
                            PBA1 = ar_total(0)
                            PBA2 = ar_total(1)
                            PBA3 = ar_total(2)
                            PBA4 = ar_total(3)
                            PBA5 = ar_total(4)
                            PBT1 = ar_total(5)
                            PBT2 = ar_total(6)
                            PBT3 = ar_total(7)
                            PBT4 = ar_total(8)
                            PBT5 = ar_total(9)

                            If (PBA1) = -1 Or (PBA2) = -1 Or (PBA3) = -1 Or (PBA4) = -1 Or (PBA5) = -1 Or (PBT1) = -1 Or (PBT2) = -1 Or (PBT3) = -1 Or (PBT4) = -1 Or (PBT5) = -1 Then
                                strSQL = "UPDATE kpmkv_pelajar_markah SET SMP_PB='0' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                                'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                                'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                                strRet = oCommon.ExecuteSQL(strSQL)
                            Else

                                PB_Amali = CDbl(PBA1) + CDbl(PBA2) + CDbl(PBA3) + CDbl(PBA4) + CDbl(PBA5)
                                PB_Teori = CDbl(PBT1) + CDbl(PBT2) + CDbl(PBT3) + CDbl(PBT4) + CDbl(PBT5)

                                PB_Amali_K = (PB_Amali / 5)
                                PB_Teori_K = (PB_Teori / 5)

                                SMP_PB = (PB_Amali_K + PB_Teori_K)

                                If SMP_PB >= CDbl(Skor) Then
                                    strSQL = "UPDATE kpmkv_pelajar_markah SET SMP_PB='1' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                                    'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                                    'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                                    strRet = oCommon.ExecuteSQL(strSQL)
                                Else
                                    strSQL = "UPDATE kpmkv_pelajar_markah SET SMP_PB='0' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                                    'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                                    'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                                    strRet = oCommon.ExecuteSQL(strSQL)
                                End If
                            End If
                        Case "6"
                            strSQL = "SELECT PBAV1,PBAV2,PBAV3,PBAV4,PBAV5,PBAV6,PBTV1,PBTV2,PBTV3,PBTV4,PBTV5,PBTV6 FROM kpmkv_pelajar_markah"
                            strSQL += " Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                            'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                            'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                            strRet = oCommon.getFieldValueEx(strSQL)
                            'Response.Write(strSQL)
                            ''--get total
                            Dim ar_total As Array
                            ar_total = strRet.Split("|")
                            PBA1 = ar_total(0)
                            PBA2 = ar_total(1)
                            PBA3 = ar_total(2)
                            PBA4 = ar_total(3)
                            PBA5 = ar_total(4)
                            PBA6 = ar_total(5)
                            PBT1 = ar_total(6)
                            PBT2 = ar_total(7)
                            PBT3 = ar_total(8)
                            PBT4 = ar_total(9)
                            PBT5 = ar_total(10)
                            PBT6 = ar_total(11)

                            If (PBA1) = -1 Or (PBA2) = -1 Or (PBA3) = -1 Or (PBA4) = -1 Or (PBA5) = -1 Or (PBA6) = -1 Or (PBT1) = -1 Or (PBT2) = -1 Or (PBT3) = -1 Or (PBT4) = -1 Or (PBT5) = -1 Or (PBT6) = -1 Then
                                strSQL = "UPDATE kpmkv_pelajar_markah SET SMP_PB='0' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                                'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                                'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                                strRet = oCommon.ExecuteSQL(strSQL)
                            Else

                                PB_Amali = CDbl(PBA1) + CDbl(PBA2) + CDbl(PBA3) + CDbl(PBA4) + CDbl(PBA5) + CDbl(PBA6)
                                PB_Teori = CDbl(PBT1) + CDbl(PBT2) + CDbl(PBT3) + CDbl(PBT4) + CDbl(PBT5) + CDbl(PBT6)

                                PB_Amali_K = (PB_Amali / 6)
                                PB_Teori_K = (PB_Teori / 6)

                                SMP_PB = (PB_Amali_K + PB_Teori_K)

                                If SMP_PB >= CDbl(Skor) Then
                                    strSQL = "UPDATE kpmkv_pelajar_markah SET SMP_PB='1' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                                    'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                                    'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                                    strRet = oCommon.ExecuteSQL(strSQL)
                                Else
                                    strSQL = "UPDATE kpmkv_pelajar_markah SET SMP_PB='0' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                                    'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                                    'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                                    strRet = oCommon.ExecuteSQL(strSQL)
                                End If
                            End If
                        Case "7"
                            strSQL = "SELECT PBAV1,PBAV2,PBAV3,PBAV4,PBAV5,PBAV6,PBAV7,PBTV1,PBTV2,PBTV3,PBTV4,PBTV5,PBTV6,PBTV7 FROM kpmkv_pelajar_markah"
                            strSQL += " Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                            'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                            'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                            strRet = oCommon.getFieldValueEx(strSQL)
                            'Response.Write(strSQL)
                            ''--get total
                            Dim ar_total As Array
                            ar_total = strRet.Split("|")
                            PBA1 = ar_total(0)
                            PBA2 = ar_total(1)
                            PBA3 = ar_total(2)
                            PBA4 = ar_total(3)
                            PBA5 = ar_total(4)
                            PBA6 = ar_total(5)
                            PBA7 = ar_total(6)
                            PBT1 = ar_total(7)
                            PBT2 = ar_total(8)
                            PBT3 = ar_total(9)
                            PBT4 = ar_total(10)
                            PBT5 = ar_total(11)
                            PBT6 = ar_total(12)
                            PBT7 = ar_total(13)

                            If (PBA1) = -1 Or (PBA2) = -1 Or (PBA3) = -1 Or (PBA4) = -1 Or (PBA5) = -1 Or (PBA6) = -1 Or (PBA7) = -1 Or (PBT1) = -1 Or (PBT2) = -1 Or (PBT3) = -1 Or (PBT4) = -1 Or (PBT5) = -1 Or (PBT6) = -1 Or (PBT7) = -1 Then
                                strSQL = "UPDATE kpmkv_pelajar_markah SET SMP_PB='0' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                                'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                                'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                                strRet = oCommon.ExecuteSQL(strSQL)
                            Else

                                PB_Amali = CDbl(PBA1) + CDbl(PBA2) + CDbl(PBA3) + CDbl(PBA4) + CDbl(PBA5) + CDbl(PBA6) + CDbl(PBA7)
                                PB_Teori = CDbl(PBT1) + CDbl(PBT2) + CDbl(PBT3) + CDbl(PBT4) + CDbl(PBT5) + CDbl(PBT6) + CDbl(PBT7)

                                PB_Amali_K = (PB_Amali / 7)
                                PB_Teori_K = (PB_Teori / 7)

                                SMP_PB = (PB_Amali_K + PB_Teori_K)

                                If SMP_PB >= CDbl(Skor) Then
                                    strSQL = "UPDATE kpmkv_pelajar_markah SET SMP_PB='1' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                                    'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                                    'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                                    strRet = oCommon.ExecuteSQL(strSQL)
                                Else
                                    strSQL = "UPDATE kpmkv_pelajar_markah SET SMP_PB='0' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                                    'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                                    'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                                    strRet = oCommon.ExecuteSQL(strSQL)
                                End If
                            End If

                        Case "8"
                            strSQL = "SELECT PBAV1,PBAV2,PBAV3,PBAV4,PBAV5,PBAV6,PBAV7,PBAV8,PBTV1,PBTV2,PBTV3,PBTV4,PBTV5,PBTV6,PBTV7,PBTV8 FROM kpmkv_pelajar_markah"
                            strSQL += " Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                            'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                            'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                            strRet = oCommon.getFieldValueEx(strSQL)
                            'Response.Write(strSQL)
                            ''--get total
                            Dim ar_total As Array
                            ar_total = strRet.Split("|")
                            PBA1 = ar_total(0)
                            PBA2 = ar_total(1)
                            PBA3 = ar_total(2)
                            PBA4 = ar_total(3)
                            PBA5 = ar_total(4)
                            PBA6 = ar_total(5)
                            PBA7 = ar_total(6)
                            PBA8 = ar_total(7)
                            PBT1 = ar_total(8)
                            PBT2 = ar_total(9)
                            PBT3 = ar_total(10)
                            PBT4 = ar_total(11)
                            PBT5 = ar_total(12)
                            PBT6 = ar_total(13)
                            PBT7 = ar_total(14)
                            PBT8 = ar_total(15)

                            If (PBA1) = -1 Or (PBA2) = -1 Or (PBA3) = -1 Or (PBA4) = -1 Or (PBA5) = -1 Or (PBA6) = -1 Or (PBA7) = -1 Or (PBA8) = -1 Or (PBT1) = -1 Or (PBT2) = -1 Or (PBT3) = -1 Or (PBT4) = -1 Or (PBT5) = -1 Or (PBT6) = -1 Or (PBT7) = -1 Or (PBT8) = -1 Then
                                strSQL = "UPDATE kpmkv_pelajar_markah SET SMP_PB='0' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                                'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                                'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                                strRet = oCommon.ExecuteSQL(strSQL)
                            Else

                                PB_Amali = CDbl(PBA1) + CDbl(PBA2) + CDbl(PBA3) + CDbl(PBA4) + CDbl(PBA5) + CDbl(PBA6) + CDbl(PBA7) + CDbl(PBA8)
                                PB_Teori = CDbl(PBT1) + CDbl(PBT2) + CDbl(PBT3) + CDbl(PBT4) + CDbl(PBT5) + CDbl(PBT6) + CDbl(PBT7) + CDbl(PBT8)

                                PB_Amali_K = (PB_Amali / 8)
                                PB_Teori_K = (PB_Teori / 8)

                                SMP_PB = (PB_Amali_K + PB_Teori_K)

                                If SMP_PB >= CDbl(Skor) Then
                                    strSQL = "UPDATE kpmkv_pelajar_markah SET SMP_PB='1' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                                    'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                                    'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                                    strRet = oCommon.ExecuteSQL(strSQL)
                                Else
                                    strSQL = "UPDATE kpmkv_pelajar_markah SET SMP_PB='0' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                                    'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                                    'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                                    strRet = oCommon.ExecuteSQL(strSQL)
                                End If
                            End If

                    End Select
                Next

            End If



        Catch ex As Exception
            lblMsg.Text = "System Error.PB:" & ex.Message
        End Try

    End Sub

    Private Sub Vokasional_SMP_PA()
        Try

            If datRespondent.Rows.Count = 1 Then

                For i As Integer = 0 To datRespondent.Rows.Count - 1

                    Dim Tahun As String
                    Dim Semester As String

                    strSQL = "SELECT Tahun, Semester FROM kpmkv_pelajar WHERE PelajarID = '" & datRespondent.DataKeys(i).Value.ToString & "'"
                    strRet = oCommon.getFieldValueEx(strSQL)

                    Dim ar_TahunSemester As Array
                    ar_TahunSemester = strRet.Split("|")

                    Tahun = ar_TahunSemester(0)
                    Semester = ar_TahunSemester(1)

                    'get score SMP_PAA
                    strSQL = "SELECT SMP_PAA FROM kpmkv_skor_svm"
                    strSQL += " WHERE Tahun='" & Tahun & "' AND Semester='" & Semester & "' AND Sesi='" & chkSesi.Text & "'"
                    Dim Skor As String = oCommon.getFieldValue(strSQL)

                    'get score SMP_PAA
                    strSQL = "SELECT SMP_PAT FROM kpmkv_skor_svm"
                    strSQL += " WHERE Tahun='" & Tahun & "' AND Semester='" & Semester & "' AND Sesi='" & chkSesi.Text & "'"
                    Dim SkorT As String = oCommon.getFieldValue(strSQL)

                    'PA A
                    Dim PAA As String
                    'PA T
                    Dim PAT As String

                    strRet = BindData(datRespondent)

                    strSQL = "SELECT PAAV1,PATV1 FROM kpmkv_pelajar_markah"
                    strSQL += " Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                    'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                    'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                    strRet = oCommon.getFieldValueEx(strSQL)
                    'Response.Write(strSQL)
                    ''--get total
                    Dim ar_total As Array
                    ar_total = strRet.Split("|")
                    PAA = ar_total(0)
                    PAT = ar_total(1)

                    'check pa
                    If PAA = -1 Then
                        strSQL = "UPDATE kpmkv_pelajar_markah SET SMP_PAA='0' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                        'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                        'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)
                    Else
                        If Not PAA = "" Then
                            If CDbl(PAA) >= CDbl(Skor) Then
                                strSQL = "UPDATE kpmkv_pelajar_markah SET SMP_PAA='1' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                                'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                                'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                                strRet = oCommon.ExecuteSQL(strSQL)
                            Else
                                strSQL = "UPDATE kpmkv_pelajar_markah SET SMP_PAA='0' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                                'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                                'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                                strRet = oCommon.ExecuteSQL(strSQL)
                            End If
                        End If

                    End If

                    If PAT = -1 Then
                        strSQL = "UPDATE kpmkv_pelajar_markah SET SMP_PAT='0' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                        'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                        'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)
                    Else
                        If Not PAT = "" Then
                            If CDbl(PAT) >= CDbl(SkorT) Then
                                strSQL = "UPDATE kpmkv_pelajar_markah SET SMP_PAT='1' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                                'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                                'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                                strRet = oCommon.ExecuteSQL(strSQL)
                            Else
                                strSQL = "UPDATE kpmkv_pelajar_markah SET SMP_PAT='0' Where PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                                'strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                                'strSQL += " AND Sesi='" & chkSesi.Text & "'"
                                strRet = oCommon.ExecuteSQL(strSQL)
                            End If
                        End If
                    End If

                Next

            End If

        Catch ex As Exception
            lblMsg.Text = "System Error.PA:" & ex.Message
        End Try

    End Sub

    Private Sub Vokasional_MPVOK()

        Try

            If datRespondent.Rows.Count = 1 Then

                For i As Integer = 0 To datRespondent.Rows.Count - 1

                    lblMsg.Text = ""

                    Dim SMP_Total As String
                    Dim SMP_Grade As String
                    Dim SMP_PB As String
                    Dim SMP_PAA As String
                    Dim SMP_PAT As String
                    Dim Gred_Kompeten As String

                    'Get the data from database into datatable 
                    Dim cmd As New SqlCommand(getSQL)
                    Dim dt As DataTable = GetData(cmd)

                    strSQL = "SELECT SMP_Total,SMP_PB,SMP_PAA,SMP_PAT,Gred_Kompeten FROM kpmkv_pelajar_markah WHERE PelajarID ='" & datRespondent.DataKeys(i).Value.ToString & "'"

                    strRet = oCommon.getFieldValueEx(strSQL)
                    ' ''--get Pelajar info
                    Dim ar_Detail As Array
                    ar_Detail = strRet.Split("|")
                    SMP_Total = ar_Detail(0)
                    SMP_PB = ar_Detail(1)
                    SMP_PAA = ar_Detail(2)
                    SMP_PAT = ar_Detail(3)
                    Gred_Kompeten = ar_Detail(4)

                    If (SMP_PB = "0" Or SMP_PAA = "0" Or SMP_PAT = "0" Or Gred_Kompeten = "0") Then
                        strSQL = " UPDATE kpmkv_pelajar_markah SET SMP_Grade='G' WHERE PelajarID ='" & datRespondent.DataKeys(i).Value.ToString & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)
                    Else

                        strSQL = "SELECT TOP ( 1 ) Gred FROM  kpmkv_gred_vokasional WHERE '" & CInt(SMP_Total) & "' BETWEEN MarkahFrom AND MarkahTo AND Jenis='VOKASIONAL'"
                        strSQL += " AND Tahun ='" & ddlTahun.Text & "'"
                        strSQL += " AND Semester='" & ddlSemester.Text & "'"
                        strSQL += " AND Sesi ='" & chkSesi.Text & "'"
                        SMP_Grade = oCommon.getFieldValue(strSQL)

                        strSQL = " UPDATE kpmkv_pelajar_markah SET SMP_Grade='" & SMP_Grade & "' WHERE PelajarID ='" & datRespondent.DataKeys(i).Value.ToString & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)
                    End If


                    '--Debug
                    'Response.Write(strSQL)
                    If strRet = "0" Then
                    Else
                        lblMsg.Text = "System Error: Check PelajarID" & datRespondent.DataKeys(i).Value.ToString

                    End If

                Next

            End If

        Catch ex As Exception
            lblMsg.Text = "System Error.PA:" & ex.Message
        End Try

    End Sub

End Class