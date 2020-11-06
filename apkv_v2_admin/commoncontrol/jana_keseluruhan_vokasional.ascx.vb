Imports System.Data.SqlClient
Public Class jana_keseluruhan_vokasional1
    Inherits System.Web.UI.UserControl
    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strKursusID As String = ""
    Dim strPelajarID As String = ""
    Dim strSemester As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                lblMsg.Text = ""

                kpmkv_tahun_list()
                ddlTahun.SelectedValue = Now.Year

                kpmkv_negeri_list()
                ddlNegeri.SelectedValue = ""

                kpmkv_jenis_list()
                ddlJenis.SelectedValue = ""

                kpmkv_kolej_list()
                ddlKolej.SelectedValue = ""

                kpmkv_kodkursus_list()
                ddlKodKursus.SelectedValue = ""

                kpmkv_semester_list()

            End If

        Catch ex As Exception
            lblMsg.Text = "Error Message:" & ex.Message
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
            ddlNegeri.Items.Add(New ListItem("-Pilih-", ""))

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub kpmkv_jenis_list()
        strSQL = "SELECT Jenis FROM kpmkv_jeniskolej ORDER BY Jenis"
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
            ddlJenis.Items.Add(New ListItem("-Pilih-", ""))

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub kpmkv_kolej_list()
        strSQL = "SELECT Nama,RecordID FROM kpmkv_kolej WHERE Negeri='" & ddlNegeri.SelectedItem.Value & "' AND Jenis='" & ddlJenis.SelectedValue & "' ORDER BY Nama"
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
            ddlKolej.Items.Add(New ListItem("-Pilih-", ""))

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub kpmkv_kodkursus_list()

        strSQL = "SELECT b.KursusID as KursusID,b.KodKursus as KodKursus from kpmkv_kursus_kolej as a"
        strSQL += " LEFT JOIN kpmkv_kursus as b ON a.KursusID =b.KursusID "
        strSQL += " WHERE a.kolejRecordID='" & ddlKolej.SelectedValue & "'"

        '--tahun
        If Not ddlTahun.Text = "" Then
            strSQL += " AND b.Tahun ='" & ddlTahun.SelectedValue & "'"
        End If
        '--sesi
        If Not chkSesi.Text = "" Then
            strSQL += " AND b.Sesi ='" & chkSesi.Text & "'"
        End If

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
            ddlKodKursus.Items.Add(New ListItem("SEMUA", ""))

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

            ddlSemester.Items.Add(New ListItem("-Pilih-", ""))


        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub ddlNegeri_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlNegeri.SelectedIndexChanged


        kpmkv_jenis_list()
        ddlJenis.SelectedValue = ""

        ddlKolej.SelectedValue = ""
    End Sub

    Private Sub ddlJenis_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlJenis.SelectedIndexChanged

        kpmkv_kolej_list()
        ddlKolej.SelectedValue = ""

    End Sub

    Private Sub chkSesi_SelectedIndexChanged(sender As Object, e As EventArgs) Handles chkSesi.SelectedIndexChanged
        kpmkv_kodkursus_list()
        ddlKodKursus.SelectedValue = ""
    End Sub

    Protected Sub CheckUncheckAll(sender As Object, e As System.EventArgs)
        Dim chk1 As CheckBox
        chk1 = DirectCast(datRespondent.HeaderRow.Cells(0).FindControl("chkAll"), CheckBox)
        For Each row As GridViewRow In datRespondent.Rows
            Dim chk As CheckBox
            chk = DirectCast(row.Cells(0).FindControl("chkSelect"), CheckBox)
            chk.Checked = chk1.Checked
        Next
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
        Dim strOrder As String = ""


        tmpSQL = "SELECT b.KursusID,b.KodKursus from kpmkv_kursus_kolej as a"
        tmpSQL += " LEFT JOIN kpmkv_kursus as b ON a.KursusID =b.KursusID "
        strWhere = " WHERE a.kolejRecordID='" & ddlKolej.SelectedValue & "'"

        '--tahun
        If Not ddlTahun.Text = "" Then
            strWhere += " AND b.Tahun ='" & ddlTahun.SelectedValue & "'"
        End If
        '--sesi
        If Not chkSesi.Text = "" Then
            strWhere += " AND b.Sesi ='" & chkSesi.Text & "'"
        End If

        '--Kod
        If Not ddlKodKursus.Text = "" Then
            strWhere += " AND b.KursusID='" & ddlKodKursus.SelectedValue & "'"
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

    Protected Sub btnCari_Click(sender As Object, e As EventArgs) Handles btnCari.Click
        lblMsg.Text = ""
        strRet = BindData(datRespondent)
    End Sub


    Private Sub Vokasional_markah()

        'Dim strKursusID As String = ""
        'For i As Integer = 0 To datRespondent.Rows.Count - 1

        '    Dim cb As CheckBox = datRespondent.Rows(i).FindControl("chkSelect")

        '    If cb.Checked = True Then

        'strKursusID = datRespondent.DataKeys(i).Value.ToString()

        strSQL = "SELECT KodModul from kpmkv_modul WHERE KursusID='" & strKursusID & "' AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "' AND Sesi='" & chkSesi.Text & "' AND KodModul Like '%01%' "
        Dim strModul1 As String = oCommon.getFieldValue(strSQL) '1

        strSQL = "SELECT KodModul from kpmkv_modul WHERE KursusID='" & strKursusID & "' AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "' AND Sesi='" & chkSesi.Text & "' AND KodModul Like '%02%' "
        Dim strModul2 As String = oCommon.getFieldValue(strSQL) '2

        strSQL = "SELECT KodModul from kpmkv_modul WHERE KursusID='" & strKursusID & "' AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "' AND Sesi='" & chkSesi.Text & "' AND KodModul Like '%03%' "
        Dim strModul3 As String = oCommon.getFieldValue(strSQL) '3

        strSQL = "SELECT KodModul from kpmkv_modul WHERE KursusID='" & strKursusID & "' AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "' AND Sesi='" & chkSesi.Text & "' AND KodModul Like '%04%' "
        Dim strModul4 As String = oCommon.getFieldValue(strSQL) '4

        strSQL = "SELECT KodModul from kpmkv_modul WHERE KursusID='" & strKursusID & "' AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "' AND Sesi='" & chkSesi.Text & "' AND KodModul Like '%05%' "
        Dim strModul5 As String = oCommon.getFieldValue(strSQL) '5

        strSQL = "SELECT KodModul from kpmkv_modul WHERE KursusID='" & strKursusID & "' AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "' AND Sesi='" & chkSesi.Text & "' AND KodModul Like '%06%' "
        Dim strModul6 As String = oCommon.getFieldValue(strSQL) '6

        strSQL = "SELECT KodModul from kpmkv_modul WHERE KursusID='" & strKursusID & "' AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "' AND Sesi='" & chkSesi.Text & "' AND KodModul Like '%07%' "
        Dim strModul7 As String = oCommon.getFieldValue(strSQL) '7

        strSQL = "SELECT KodModul from kpmkv_modul WHERE KursusID='" & strKursusID & "' AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "' AND Sesi='" & chkSesi.Text & "' AND KodModul Like '%08%' "
        Dim strModul8 As String = oCommon.getFieldValue(strSQL) '8

        strSQL = "  SELECT kpmkv_pelajar.PelajarID"
        strSQL += " FROM kpmkv_pelajar_markah "
        strSQL += " LEFT OUTER JOIN kpmkv_pelajar On kpmkv_pelajar_markah.PelajarID = kpmkv_pelajar.PelajarID"
        strSQL += " LEFT OUTER Join kpmkv_kursus On kpmkv_pelajar.KursusID = kpmkv_kursus.KursusID"
        strSQL += " WHERE kpmkv_pelajar.KolejRecordID='" & ddlKolej.SelectedValue & "' "
        strSQL += " AND kpmkv_pelajar.IsDeleted='N' AND kpmkv_pelajar.StatusID='2' AND kpmkv_pelajar.JenisCalonID='2'"
        strSQL += " AND kpmkv_pelajar.Tahun ='" & ddlTahun.Text & "'"
        strSQL += " AND kpmkv_pelajar.Sesi ='" & chkSesi.Text & "'"
        strSQL += " AND kpmkv_pelajar.Semester ='" & ddlSemester.Text & "'"
        strSQL += " AND kpmkv_pelajar.KursusID='" & strKursusID & "'"

        strRet = oCommon.ExecuteSQL(strSQL)

        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)
        Dim ds As DataSet = New DataSet
        sqlDA.Fill(ds, "AnyTable")

        Dim strPelajarID As String = ""

        Dim strCount As Integer = ds.Tables(0).Rows.Count - 1
        For iloop As Integer = 0 To strCount
            strPelajarID = (ds.Tables(0).Rows(iloop).Item(0).ToString())

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

            strSQL = "Select PBAmali from kpmkv_modul Where KodModul= '" & strModul1 & "' AND KursusID='" & strKursusID & "' AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "' AND Sesi='" & chkSesi.Text & "'"
            PBAmali1 = oCommon.getFieldValue(strSQL)

            strSQL = "Select PBTeori from kpmkv_modul Where KodModul= '" & strModul1 & "' AND KursusID='" & strKursusID & "' AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "' AND Sesi='" & chkSesi.Text & "'"
            PBTeori1 = oCommon.getFieldValue(strSQL)

            strSQL = "Select PAAmali from kpmkv_modul Where KodModul= '" & strModul1 & "' AND KursusID='" & strKursusID & "' AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "' AND Sesi='" & chkSesi.Text & "'"
            PAAmali1 = oCommon.getFieldValue(strSQL)

            strSQL = "Select PATeori from kpmkv_modul Where KodModul= '" & strModul1 & "' AND KursusID='" & strKursusID & "' AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "' AND Sesi='" & chkSesi.Text & "'"
            PATeori1 = oCommon.getFieldValue(strSQL)

            strSQL = "Select B_Amali1 from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
            strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
            strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
            B_Amali1 = oCommon.getFieldValue(strSQL)
            'round up
            B_Amali1 = (B_Amali1)

            strSQL = "Select B_Teori1 from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
            strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
            strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
            B_Teori1 = oCommon.getFieldValue(strSQL)
            'round up
            B_Teori1 = (B_Teori1)

            strSQL = "Select A_Amali1 from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
            strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
            strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
            A_Amali1 = oCommon.getFieldValue(strSQL)
            'round up
            A_Amali1 = (A_Amali1)

            strSQL = "Select A_Teori1 from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
            strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
            strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
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
                strSQL += " Where PelajarID='" & strPelajarID & "'"
                strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                strRet = oCommon.ExecuteSQL(strSQL)
            Else

                PBA1 = ((B_Amali1 / 100) * PBAmali1)
                PBT1 = ((B_Teori1 / 100) * PBTeori1)
                PAA1 = ((A_Amali1 / 100) * PAAmali1)
                PAT1 = ((A_Teori1 / 100) * PATeori1)

                'change on 31/7/2017 PBAV1,PBTV1,PAAV1,PATV1
                PointerM1 = Math.Ceiling(PBA1 + PBT1 + PAA1 + PAT1)
                strSQL = "UPDATE kpmkv_pelajar_markah SET PBPAM1='" & PointerM1 & "',"
                strSQL += "PBAV1='" & PBA1 & "',PBTV1='" & PBT1 & "',PAAV1='" & PAA1 & "',PATV1='" & PAT1 & "'"
                strSQL += " Where PelajarID='" & strPelajarID & "'"
                strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
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

                strSQL = "Select PBAmali from kpmkv_modul Where KodModul= '" & strModul2 & "' AND KursusID='" & strKursusID & "' AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "' AND Sesi='" & chkSesi.Text & "'"
                PBAmali2 = oCommon.getFieldValue(strSQL)

                strSQL = "Select PBTeori from kpmkv_modul Where KodModul= '" & strModul2 & "' AND KursusID='" & strKursusID & "' AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "' AND Sesi='" & chkSesi.Text & "'"
                PBTeori2 = oCommon.getFieldValue(strSQL)

                strSQL = "Select PAAmali from kpmkv_modul Where KodModul= '" & strModul2 & "' AND KursusID='" & strKursusID & "' AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "' AND Sesi='" & chkSesi.Text & "'"
                PAAmali2 = oCommon.getFieldValue(strSQL)

                strSQL = "Select PATeori from kpmkv_modul Where KodModul= '" & strModul2 & "' AND KursusID='" & strKursusID & "' AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "' AND Sesi='" & chkSesi.Text & "'"
                PATeori2 = oCommon.getFieldValue(strSQL)

                strSQL = "Select B_Amali2 from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
                strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                B_Amali2 = oCommon.getFieldValue(strSQL)
                'round up
                B_Amali2 = (B_Amali2)

                strSQL = "Select B_Teori2 from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
                strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                B_Teori2 = oCommon.getFieldValue(strSQL)
                'round up
                B_Teori2 = (B_Teori2)

                strSQL = "Select A_Amali2 from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
                strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                A_Amali2 = oCommon.getFieldValue(strSQL)
                'round up
                A_Amali2 = (A_Amali2)

                strSQL = "Select A_Teori2 from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
                strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
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
                    strSQL += " Where PelajarID='" & strPelajarID & "'"
                    strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                    strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
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
                    strSQL += " Where PelajarID='" & strPelajarID & "'"
                    strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                    strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
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

                strSQL = "Select PBAmali from kpmkv_modul Where KodModul= '" & strModul3 & "' AND KursusID='" & strKursusID & "' AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "' AND Sesi='" & chkSesi.Text & "'"
                PBAmali3 = oCommon.getFieldValue(strSQL)

                strSQL = "Select PBTeori from kpmkv_modul Where KodModul= '" & strModul3 & "' AND KursusID='" & strKursusID & "' AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "' AND Sesi='" & chkSesi.Text & "'"
                PBTeori3 = oCommon.getFieldValue(strSQL)

                strSQL = "Select PAAmali from kpmkv_modul Where KodModul= '" & strModul3 & "' AND KursusID='" & strKursusID & "' AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "' AND Sesi='" & chkSesi.Text & "'"
                PAAmali3 = oCommon.getFieldValue(strSQL)

                strSQL = "Select PATeori from kpmkv_modul Where KodModul= '" & strModul3 & "' AND KursusID='" & strKursusID & "' AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "' AND Sesi='" & chkSesi.Text & "'"
                PATeori3 = oCommon.getFieldValue(strSQL)

                strSQL = "Select B_Amali3 from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
                strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                B_Amali3 = oCommon.getFieldValue(strSQL)
                'round up
                B_Amali3 = (B_Amali3)

                strSQL = "Select B_Teori3 from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
                strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                B_Teori3 = oCommon.getFieldValue(strSQL)
                'round up
                B_Teori3 = (B_Teori3)

                strSQL = "Select A_Amali3 from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
                strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                A_Amali3 = oCommon.getFieldValue(strSQL)
                'round up
                A_Amali3 = (A_Amali3)

                strSQL = "Select A_Teori3 from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
                strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
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
                    strSQL += " Where PelajarID='" & strPelajarID & "'"
                    strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                    strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
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
                    strSQL += " Where PelajarID='" & strPelajarID & "'"
                    strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                    strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
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

                strSQL = "Select PBAmali from kpmkv_modul Where KodModul= '" & strModul4 & "' AND KursusID='" & strKursusID & "' AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "' AND Sesi='" & chkSesi.Text & "'"
                PBAmali4 = oCommon.getFieldValue(strSQL)

                strSQL = "Select PBTeori from kpmkv_modul Where KodModul= '" & strModul4 & "' AND KursusID='" & strKursusID & "' AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "' AND Sesi='" & chkSesi.Text & "'"
                PBTeori4 = oCommon.getFieldValue(strSQL)

                strSQL = "Select PAAmali from kpmkv_modul Where KodModul= '" & strModul4 & "' AND KursusID='" & strKursusID & "' AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "' AND Sesi='" & chkSesi.Text & "'"
                PAAmali4 = oCommon.getFieldValue(strSQL)

                strSQL = "Select PATeori from kpmkv_modul Where KodModul= '" & strModul4 & "' AND KursusID='" & strKursusID & "' AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "' AND Sesi='" & chkSesi.Text & "'"
                PATeori4 = oCommon.getFieldValue(strSQL)

                strSQL = "Select B_Amali4 from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
                strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                B_Amali4 = oCommon.getFieldValue(strSQL)
                'round up
                B_Amali4 = (B_Amali4)

                strSQL = "Select B_Teori4 from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
                strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                B_Teori4 = oCommon.getFieldValue(strSQL)
                'round up
                B_Teori4 = (B_Teori4)

                strSQL = "Select A_Amali4 from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
                strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                A_Amali4 = oCommon.getFieldValue(strSQL)
                'round up
                A_Amali4 = (A_Amali4)

                strSQL = "Select A_Teori4 from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
                strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
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
                    strSQL += " Where PelajarID='" & strPelajarID & "'"
                    strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                    strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
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
                    strSQL += " Where PelajarID='" & strPelajarID & "'"
                    strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                    strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
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

                strSQL = "Select PBAmali from kpmkv_modul Where KodModul= '" & strModul5 & "' AND KursusID='" & strKursusID & "' AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "' AND Sesi='" & chkSesi.Text & "'"
                PBAmali5 = oCommon.getFieldValue(strSQL)

                strSQL = "Select PBTeori from kpmkv_modul Where KodModul= '" & strModul5 & "' AND KursusID='" & strKursusID & "' AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "' AND Sesi='" & chkSesi.Text & "'"
                PBTeori5 = oCommon.getFieldValue(strSQL)

                strSQL = "Select PAAmali from kpmkv_modul Where KodModul= '" & strModul5 & "' AND KursusID='" & strKursusID & "' AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "' AND Sesi='" & chkSesi.Text & "'"
                PAAmali5 = oCommon.getFieldValue(strSQL)

                strSQL = "Select PATeori from kpmkv_modul Where KodModul= '" & strModul5 & "' AND KursusID='" & strKursusID & "' AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "' AND Sesi='" & chkSesi.Text & "'"
                PATeori5 = oCommon.getFieldValue(strSQL)

                strSQL = "Select B_Amali5 from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
                strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                B_Amali5 = oCommon.getFieldValue(strSQL)
                'round up
                B_Amali5 = (B_Amali5)

                strSQL = "Select B_Teori5 from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
                strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                B_Teori5 = oCommon.getFieldValue(strSQL)
                'round up
                B_Teori5 = (B_Teori5)

                strSQL = "Select A_Amali5 from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
                strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                A_Amali5 = oCommon.getFieldValue(strSQL)
                'round up
                A_Amali5 = (A_Amali5)

                strSQL = "Select A_Teori5 from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
                strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
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
                    strSQL += " Where PelajarID='" & strPelajarID & "'"
                    strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                    strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
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
                    strSQL += " Where PelajarID='" & strPelajarID & "'"
                    strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                    strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
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

                strSQL = "Select PBAmali from kpmkv_modul Where KodModul= '" & strModul6 & "' AND KursusID='" & strKursusID & "' AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "' AND Sesi='" & chkSesi.Text & "'"
                PBAmali6 = oCommon.getFieldValue(strSQL)

                strSQL = "Select PBTeori from kpmkv_modul Where KodModul= '" & strModul6 & "' AND KursusID='" & strKursusID & "' AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "' AND Sesi='" & chkSesi.Text & "'"
                PBTeori6 = oCommon.getFieldValue(strSQL)

                strSQL = "Select PAAmali from kpmkv_modul Where KodModul= '" & strModul6 & "' AND KursusID='" & strKursusID & "' AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "' AND Sesi='" & chkSesi.Text & "'"
                PAAmali6 = oCommon.getFieldValue(strSQL)

                strSQL = "Select PATeori from kpmkv_modul Where KodModul= '" & strModul6 & "' AND KursusID='" & strKursusID & "' AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "' AND Sesi='" & chkSesi.Text & "'"
                PATeori6 = oCommon.getFieldValue(strSQL)

                strSQL = "Select B_Amali6 from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
                strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                B_Amali6 = oCommon.getFieldValue(strSQL)
                'round up
                B_Amali6 = (B_Amali6)

                strSQL = "Select B_Teori6 from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
                strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                B_Teori6 = oCommon.getFieldValue(strSQL)
                'round up
                B_Teori6 = (B_Teori6)

                strSQL = "Select A_Amali6 from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
                strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                A_Amali6 = oCommon.getFieldValue(strSQL)
                'round up
                A_Amali6 = (A_Amali6)

                strSQL = "Select A_Teori6 from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
                strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
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
                    strSQL += " Where PelajarID='" & strPelajarID & "'"
                    strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                    strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
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
                    strSQL += " Where PelajarID='" & strPelajarID & "'"
                    strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                    strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
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

                strSQL = "Select PBAmali from kpmkv_modul Where KodModul= '" & strModul7 & "' AND KursusID='" & strKursusID & "' AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "' AND Sesi='" & chkSesi.Text & "'"
                PBAmali7 = oCommon.getFieldValue(strSQL)

                strSQL = "Select PBTeori from kpmkv_modul Where KodModul= '" & strModul7 & "' AND KursusID='" & strKursusID & "' AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "' AND Sesi='" & chkSesi.Text & "'"
                PBTeori7 = oCommon.getFieldValue(strSQL)

                strSQL = "Select PAAmali from kpmkv_modul Where KodModul= '" & strModul7 & "' AND KursusID='" & strKursusID & "' AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "' AND Sesi='" & chkSesi.Text & "'"
                PAAmali7 = oCommon.getFieldValue(strSQL)

                strSQL = "Select PATeori from kpmkv_modul Where KodModul= '" & strModul7 & "' AND KursusID='" & strKursusID & "' AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "' AND Sesi='" & chkSesi.Text & "'"
                PATeori7 = oCommon.getFieldValue(strSQL)

                strSQL = "Select B_Amali7 from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
                strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                B_Amali7 = oCommon.getFieldValue(strSQL)
                'round up
                B_Amali7 = (B_Amali7)

                strSQL = "Select B_Teori7 from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
                strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                B_Teori7 = oCommon.getFieldValue(strSQL)
                'round up
                B_Teori7 = (B_Teori7)

                strSQL = "Select A_Amali7 from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
                strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                A_Amali7 = oCommon.getFieldValue(strSQL)
                'round up
                A_Amali7 = (A_Amali7)

                strSQL = "Select A_Teori7 from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
                strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
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
                    strSQL += " Where PelajarID='" & strPelajarID & "'"
                    strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                    strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
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
                    strSQL += " Where PelajarID='" & strPelajarID & "'"
                    strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                    strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
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

                strSQL = "Select PBAmali from kpmkv_modul Where KodModul= '" & strModul8 & "' AND KursusID='" & strKursusID & "' AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "' AND Sesi='" & chkSesi.Text & "'"
                PBAmali8 = oCommon.getFieldValue(strSQL)

                strSQL = "Select PBTeori from kpmkv_modul Where KodModul= '" & strModul8 & "' AND KursusID='" & strKursusID & "' AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "' AND Sesi='" & chkSesi.Text & "'"
                PBTeori8 = oCommon.getFieldValue(strSQL)

                strSQL = "Select PAAmali from kpmkv_modul Where KodModul= '" & strModul8 & "' AND KursusID='" & strKursusID & "' AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "' AND Sesi='" & chkSesi.Text & "'"
                PAAmali8 = oCommon.getFieldValue(strSQL)

                strSQL = "Select PATeori from kpmkv_modul Where KodModul= '" & strModul8 & "' AND KursusID='" & strKursusID & "' AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "' AND Sesi='" & chkSesi.Text & "'"
                PATeori8 = oCommon.getFieldValue(strSQL)

                strSQL = "Select B_Amali8 from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
                strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                B_Amali8 = oCommon.getFieldValue(strSQL)
                'round up
                B_Amali8 = (B_Amali8)

                strSQL = "Select B_Teori8 from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
                strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                B_Teori8 = oCommon.getFieldValue(strSQL)
                'round up
                B_Teori8 = (B_Teori8)

                strSQL = "Select A_Amali8 from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
                strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                A_Amali8 = oCommon.getFieldValue(strSQL)
                'round up
                A_Amali8 = (A_Amali8)

                strSQL = "Select A_Teori8 from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
                strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
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
                    strSQL += " Where PelajarID='" & strPelajarID & "'"
                    strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                    strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
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
                    strSQL += " Where PelajarID='" & strPelajarID & "'"
                    strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                    strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)
                End If
            End If
        Next

        '    End If
        'Next


    End Sub

    Private Sub Vokasional_gred()
        'Dim strKursusID As String = ""
        'strRet = BindData(datRespondent)
        'For i As Integer = 0 To datRespondent.Rows.Count - 1

        '    Dim cb As CheckBox = datRespondent.Rows(i).FindControl("chkSelect")

        '    If cb.Checked = True Then
        Dim PBPAM1 As String
        Dim GredPBPAM1 As String

        strSQL = "  SELECT kpmkv_pelajar.PelajarID"
        strSQL += " FROM kpmkv_pelajar_markah "
        strSQL += " LEFT OUTER JOIN kpmkv_pelajar On kpmkv_pelajar_markah.PelajarID = kpmkv_pelajar.PelajarID"
        strSQL += " LEFT OUTER Join kpmkv_kursus On kpmkv_pelajar.KursusID = kpmkv_kursus.KursusID"
        strSQL += " WHERE kpmkv_pelajar.KolejRecordID='" & ddlKolej.SelectedValue & "' "
        strSQL += " AND kpmkv_pelajar.IsDeleted='N' AND kpmkv_pelajar.StatusID='2' AND kpmkv_pelajar.JenisCalonID='2'"
        strSQL += " AND kpmkv_pelajar.Tahun ='" & ddlTahun.Text & "'"
        strSQL += " AND kpmkv_pelajar.Sesi ='" & chkSesi.Text & "'"
        strSQL += " AND kpmkv_pelajar.Semester ='" & ddlSemester.Text & "'"
        strSQL += " AND kpmkv_pelajar.KursusID='" & strKursusID & "'"

        strRet = oCommon.ExecuteSQL(strSQL)

        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)
        Dim ds As DataSet = New DataSet
        sqlDA.Fill(ds, "AnyTable")

        Dim strPelajarID As String = ""


        Dim strCount As Integer = ds.Tables(0).Rows.Count - 1
        For iloop As Integer = 0 To strCount
            strPelajarID = (ds.Tables(0).Rows(iloop).Item(0).ToString())

            strSQL = "SELECT PBPAM1 FROM kpmkv_pelajar_markah"
            strSQL += " Where PelajarID='" & strPelajarID & "'"
            strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
            strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
            PBPAM1 = oCommon.getFieldValueInt(strSQL)

            If String.IsNullOrEmpty(PBPAM1) Then
                strSQL = "UPDATE kpmkv_pelajar_markah SET GredV1='' Where PelajarID='" & strPelajarID & "'"
                strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                strRet = oCommon.ExecuteSQL(strSQL)
            ElseIf PBPAM1 = 0 Then
                strSQL = "UPDATE kpmkv_pelajar_markah SET GredV1='E' Where PelajarID='" & strPelajarID & "'"
                strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                strRet = oCommon.ExecuteSQL(strSQL)
            ElseIf PBPAM1 = -1 Then
                strSQL = "UPDATE kpmkv_pelajar_markah SET GredV1='T' Where PelajarID='" & strPelajarID & "'"
                strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                strRet = oCommon.ExecuteSQL(strSQL)
            Else
                strSQL = "SELECT TOP ( 1 ) Gred FROM  kpmkv_gred WHERE '" & PBPAM1 & "' BETWEEN MarkahFrom AND MarkahTo AND Jenis='VOKASIONAL'"
                GredPBPAM1 = oCommon.getFieldValue(strSQL)

                strSQL = "UPDATE kpmkv_pelajar_markah SET GredV1='" & GredPBPAM1 & "' Where PelajarID='" & strPelajarID & "'"
                strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                strRet = oCommon.ExecuteSQL(strSQL)

            End If

            '-----------------------------------------------------------------
            Dim PBPAM2 As String
            Dim GredPBPAM2 As String

            strSQL = "SELECT PBPAM2 FROM kpmkv_pelajar_markah"
            strSQL += " Where PelajarID='" & strPelajarID & "'"
            strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
            strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
            PBPAM2 = oCommon.getFieldValueInt(strSQL)

            If String.IsNullOrEmpty(PBPAM2) Then
                strSQL = "UPDATE kpmkv_pelajar_markah SET GredV2='' Where PelajarID='" & strPelajarID & "'"
                strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                strRet = oCommon.ExecuteSQL(strSQL)
            ElseIf PBPAM2 = 0 Then
                strSQL = "UPDATE kpmkv_pelajar_markah SET GredV2='E' Where PelajarID='" & strPelajarID & "'"
                strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                strRet = oCommon.ExecuteSQL(strSQL)
            ElseIf PBPAM2 = -1 Then
                strSQL = "UPDATE kpmkv_pelajar_markah SET GredV2='T' Where PelajarID='" & strPelajarID & "'"
                strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                strRet = oCommon.ExecuteSQL(strSQL)
            Else
                strSQL = "SELECT TOP ( 1 ) Gred FROM  kpmkv_gred WHERE '" & PBPAM2 & "' BETWEEN MarkahFrom AND MarkahTo AND Jenis='VOKASIONAL'"
                GredPBPAM2 = oCommon.getFieldValue(strSQL)

                strSQL = "UPDATE kpmkv_pelajar_markah SET GredV2='" & GredPBPAM2 & "' Where PelajarID='" & strPelajarID & "'"
                strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                strRet = oCommon.ExecuteSQL(strSQL)

            End If
            '------------------------------------------------------------------------------------------------------------------------
            Dim PBPAM3 As String
            Dim GredPBPAM3 As String

            strSQL = "SELECT PBPAM3 FROM kpmkv_pelajar_markah"
            strSQL += " Where PelajarID='" & strPelajarID & "'"
            strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
            strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
            PBPAM3 = oCommon.getFieldValueInt(strSQL)

            If String.IsNullOrEmpty(PBPAM3) Then
                strSQL = "UPDATE kpmkv_pelajar_markah SET GredV3='' Where PelajarID='" & strPelajarID & "'"
                strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                strRet = oCommon.ExecuteSQL(strSQL)
            ElseIf PBPAM3 = 0 Then
                strSQL = "UPDATE kpmkv_pelajar_markah SET GredV3='E' Where PelajarID='" & strPelajarID & "'"
                strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                strRet = oCommon.ExecuteSQL(strSQL)
            ElseIf PBPAM3 = -1 Then
                strSQL = "UPDATE kpmkv_pelajar_markah SET GredV3='T' Where PelajarID='" & strPelajarID & "'"
                strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                strRet = oCommon.ExecuteSQL(strSQL)
            Else
                strSQL = "SELECT TOP ( 1 ) Gred FROM  kpmkv_gred WHERE '" & PBPAM3 & "' BETWEEN MarkahFrom AND MarkahTo AND Jenis='VOKASIONAL'"
                GredPBPAM3 = oCommon.getFieldValue(strSQL)

                strSQL = "UPDATE kpmkv_pelajar_markah SET GredV3='" & GredPBPAM3 & "' Where PelajarID='" & strPelajarID & "'"
                strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                strRet = oCommon.ExecuteSQL(strSQL)

            End If
            '------------------------------------------------------------------------------------------------------------

            Dim PBPAM4 As String
            Dim GredPBPAM4 As String

            strSQL = "SELECT PBPAM4 FROM kpmkv_pelajar_markah"
            strSQL += " Where PelajarID='" & strPelajarID & "'"
            strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
            strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
            PBPAM4 = oCommon.getFieldValueInt(strSQL)

            If String.IsNullOrEmpty(PBPAM4) Then
                strSQL = "UPDATE kpmkv_pelajar_markah SET GredV4='' Where PelajarID='" & strPelajarID & "'"
                strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                strRet = oCommon.ExecuteSQL(strSQL)
            ElseIf PBPAM4 = 0 Then
                strSQL = "UPDATE kpmkv_pelajar_markah SET GredV4='E' Where PelajarID='" & strPelajarID & "'"
                strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                strRet = oCommon.ExecuteSQL(strSQL)
            ElseIf PBPAM4 = -1 Then
                strSQL = "UPDATE kpmkv_pelajar_markah SET GredV4='T' Where PelajarID='" & strPelajarID & "'"
                strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                strRet = oCommon.ExecuteSQL(strSQL)
            Else
                strSQL = "SELECT TOP ( 1 ) Gred FROM  kpmkv_gred WHERE '" & PBPAM4 & "' BETWEEN MarkahFrom AND MarkahTo AND Jenis='VOKASIONAL'"
                GredPBPAM4 = oCommon.getFieldValue(strSQL)

                strSQL = "UPDATE kpmkv_pelajar_markah SET GredV4='" & GredPBPAM4 & "' Where PelajarID='" & strPelajarID & "'"
                strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                strRet = oCommon.ExecuteSQL(strSQL)

            End If
            '-------------------------------------------------------------------------------------------------------------

            Dim PBPAM5 As String
            Dim GredPBPAM5 As String

            strSQL = "SELECT PBPAM5 FROM kpmkv_pelajar_markah"
            strSQL += " Where PelajarID='" & strPelajarID & "'"
            strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
            strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
            PBPAM5 = oCommon.getFieldValueInt(strSQL)

            If String.IsNullOrEmpty(PBPAM5) Then
                strSQL = "UPDATE kpmkv_pelajar_markah SET GredV5='' Where PelajarID='" & strPelajarID & "'"
                strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                strRet = oCommon.ExecuteSQL(strSQL)
            ElseIf PBPAM5 = 0 Then
                strSQL = "UPDATE kpmkv_pelajar_markah SET GredV5='E' Where PelajarID='" & strPelajarID & "'"
                strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                strRet = oCommon.ExecuteSQL(strSQL)
            ElseIf PBPAM5 = -1 Then
                strSQL = "UPDATE kpmkv_pelajar_markah SET GredV5='T' Where PelajarID='" & strPelajarID & "'"
                strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                strRet = oCommon.ExecuteSQL(strSQL)
            Else
                strSQL = "SELECT TOP ( 1 ) Gred FROM  kpmkv_gred WHERE '" & PBPAM5 & "' BETWEEN MarkahFrom AND MarkahTo AND Jenis='VOKASIONAL'"
                GredPBPAM5 = oCommon.getFieldValue(strSQL)

                strSQL = "UPDATE kpmkv_pelajar_markah SET GredV5='" & GredPBPAM5 & "' Where PelajarID='" & strPelajarID & "'"
                strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                strRet = oCommon.ExecuteSQL(strSQL)
            End If
            '-------------------------------------------------------------------------------------------------------------

            Dim PBPAM6 As String
            Dim GredPBPAM6 As String

            strSQL = "SELECT PBPAM6 FROM kpmkv_pelajar_markah"
            strSQL += " Where PelajarID='" & strPelajarID & "'"
            strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
            strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
            PBPAM6 = oCommon.getFieldValueInt(strSQL)

            If String.IsNullOrEmpty(PBPAM6) Then
                strSQL = "UPDATE kpmkv_pelajar_markah SET GredV6='' Where PelajarID='" & strPelajarID & "'"
                strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                strRet = oCommon.ExecuteSQL(strSQL)
            ElseIf PBPAM6 = 0 Then
                strSQL = "UPDATE kpmkv_pelajar_markah SET GredV6='E' Where PelajarID='" & strPelajarID & "'"
                strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                strRet = oCommon.ExecuteSQL(strSQL)
            ElseIf PBPAM6 = -1 Then
                strSQL = "UPDATE kpmkv_pelajar_markah SET GredV6='T' Where PelajarID='" & strPelajarID & "'"
                strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                strRet = oCommon.ExecuteSQL(strSQL)
            Else
                strSQL = "SELECT TOP ( 1 ) Gred FROM  kpmkv_gred WHERE '" & PBPAM6 & "' BETWEEN MarkahFrom AND MarkahTo AND Jenis='VOKASIONAL'"
                GredPBPAM6 = oCommon.getFieldValue(strSQL)

                strSQL = "UPDATE kpmkv_pelajar_markah SET GredV6='" & GredPBPAM6 & "' Where PelajarID='" & strPelajarID & "'"
                strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                strRet = oCommon.ExecuteSQL(strSQL)
            End If
            '-------------------------------------------------------------------------------------------------------------
        Next
        '    End If

        'Next
    End Sub


    Private Sub Vokasional_gredKompeten()
        'Dim strKursusID As String = ""
        'strRet = BindData(datRespondent)
        'For i As Integer = 0 To datRespondent.Rows.Count - 1

        '    Dim cb As CheckBox = datRespondent.Rows(i).FindControl("chkSelect")

        '    If cb.Checked = True Then

        Try

            '--count no of modul
            Dim nCount As Integer = 0
            strSQL = "SELECT COUNT(kpmkv_modul.KodModul) as CModul "
            strSQL += " FROM kpmkv_modul LEFT OUTER JOIN"
            strSQL += " kpmkv_kursus ON kpmkv_modul.KursusID = kpmkv_kursus.KursusID"
            strSQL += " WHERE kpmkv_modul.KursusID='" & strKursusID & "'"
            strSQL += " AND  kpmkv_modul.Sesi='" & chkSesi.Text & "'"
            strSQL += " AND  kpmkv_modul.Semester='" & ddlSemester.Text & "'"
            strSQL += " AND  kpmkv_modul.Tahun='" & ddlTahun.Text & "'"
            nCount = oCommon.getFieldValueInt(strSQL)

            strSQL = "  SELECT kpmkv_pelajar.PelajarID"
            strSQL += " FROM kpmkv_pelajar_markah "
            strSQL += " LEFT OUTER JOIN kpmkv_pelajar On kpmkv_pelajar_markah.PelajarID = kpmkv_pelajar.PelajarID"
            strSQL += " LEFT OUTER Join kpmkv_kursus On kpmkv_pelajar.KursusID = kpmkv_kursus.KursusID"
            strSQL += " WHERE kpmkv_pelajar.KolejRecordID='" & ddlKolej.SelectedValue & "' "
            strSQL += " AND kpmkv_pelajar.IsDeleted='N' AND kpmkv_pelajar.StatusID='2' AND kpmkv_pelajar.JenisCalonID='2'"
            strSQL += " AND kpmkv_pelajar.Tahun ='" & ddlTahun.Text & "'"
            strSQL += " AND kpmkv_pelajar.Sesi ='" & chkSesi.Text & "'"
            strSQL += " AND kpmkv_pelajar.Semester ='" & ddlSemester.Text & "'"
            strSQL += " AND kpmkv_pelajar.KursusID='" & strKursusID & "'"

            strRet = oCommon.ExecuteSQL(strSQL)

            Dim sqlDA As New SqlDataAdapter(strSQL, objConn)
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            Dim strPelajarID As String = ""


            Dim strCount As Integer = ds.Tables(0).Rows.Count - 1
            For iloop As Integer = 0 To strCount
                strPelajarID = (ds.Tables(0).Rows(iloop).Item(0).ToString())

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
                        strSQL += " Where PelajarID='" & strPelajarID & "'"
                        strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                        strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                        strRet = oCommon.getFieldValueEx(strSQL)

                        Dim ar_total As Array
                        ar_total = strRet.Split("|")
                        strGredV1 = ar_total(0)
                        strGredV2 = ar_total(1)


                        If (strGredV1 = "C+" Or strGredV1 = "C" Or strGredV1 = "C-" Or strGredV1 = "D+" Or strGredV1 = "D" Or strGredV1 = "D-" Or strGredV1 = "E" Or strGredV1 = "G" Or strGredV1 = "T") Then
                            strSQL = "UPDATE kpmkv_pelajar_markah SET Gred_Kompeten='0' Where PelajarID='" & strPelajarID & "'"
                            strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                            strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)

                        ElseIf (strGredV2 = "C+" Or strGredV2 = "C" Or strGredV2 = "C-" Or strGredV2 = "D+" Or strGredV2 = "D" Or strGredV2 = "D-" Or strGredV2 = "E" Or strGredV2 = "G" Or strGredV2 = "T") Then
                            strSQL = "UPDATE kpmkv_pelajar_markah SET Gred_Kompeten='0' Where PelajarID='" & strPelajarID & "'"
                            strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                            strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)
                        Else

                            strSQL = "UPDATE kpmkv_pelajar_markah SET Gred_Kompeten='1' Where PelajarID='" & strKursusID & "'"
                            strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                            strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)
                        End If

                    Case "3"

                        strSQL = "SELECT GredV1,GredV2,GredV3 FROM kpmkv_pelajar_markah"
                        strSQL += " Where PelajarID='" & strPelajarID & "'"
                        strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                        strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                        strRet = oCommon.getFieldValueEx(strSQL)

                        Dim ar_total As Array
                        ar_total = strRet.Split("|")
                        strGredV1 = ar_total(0)
                        strGredV2 = ar_total(1)
                        strGredV3 = ar_total(2)


                        If (strGredV1 = "C+" Or strGredV1 = "C" Or strGredV1 = "C-" Or strGredV1 = "D+" Or strGredV1 = "D" Or strGredV1 = "D-" Or strGredV1 = "E" Or strGredV1 = "G" Or strGredV1 = "T") Then
                            strSQL = "UPDATE kpmkv_pelajar_markah SET Gred_Kompeten='0' Where PelajarID='" & strPelajarID & "'"
                            strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                            strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)

                        ElseIf (strGredV2 = "C+" Or strGredV2 = "C" Or strGredV2 = "C-" Or strGredV2 = "D+" Or strGredV2 = "D" Or strGredV2 = "D-" Or strGredV2 = "E" Or strGredV2 = "G" Or strGredV2 = "T") Then
                            strSQL = "UPDATE kpmkv_pelajar_markah SET Gred_Kompeten='0' Where PelajarID='" & strPelajarID & "'"
                            strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                            strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)

                        ElseIf (strGredV3 = "C+" Or strGredV3 = "C" Or strGredV3 = "C-" Or strGredV3 = "D+" Or strGredV3 = "D" Or strGredV3 = "D-" Or strGredV3 = "E" Or strGredV3 = "G" Or strGredV3 = "T") Then
                            strSQL = "UPDATE kpmkv_pelajar_markah SET Gred_Kompeten='0' Where PelajarID='" & strPelajarID & "'"
                            strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                            strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)
                        Else

                            strSQL = "UPDATE kpmkv_pelajar_markah SET Gred_Kompeten='1' Where PelajarID='" & strPelajarID & "'"
                            strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                            strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)
                        End If

                    Case "4"

                        strSQL = "SELECT GredV1,GredV2,GredV3,GredV4 FROM kpmkv_pelajar_markah"
                        strSQL += " Where PelajarID='" & strPelajarID & "'"
                        strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                        strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                        strRet = oCommon.getFieldValueEx(strSQL)

                        Dim ar_total As Array
                        ar_total = strRet.Split("|")
                        strGredV1 = ar_total(0)
                        strGredV2 = ar_total(1)
                        strGredV3 = ar_total(2)
                        strGredV4 = ar_total(3)


                        If (strGredV1 = "C+" Or strGredV1 = "C" Or strGredV1 = "C-" Or strGredV1 = "D+" Or strGredV1 = "D" Or strGredV1 = "D-" Or strGredV1 = "E" Or strGredV1 = "G" Or strGredV1 = "T") Then
                            strSQL = "UPDATE kpmkv_pelajar_markah SET Gred_Kompeten='0' Where PelajarID='" & strPelajarID & "'"
                            strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                            strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)

                        ElseIf (strGredV2 = "C+" Or strGredV2 = "C" Or strGredV2 = "C-" Or strGredV2 = "D+" Or strGredV2 = "D" Or strGredV2 = "D-" Or strGredV2 = "E" Or strGredV2 = "G" Or strGredV2 = "T") Then
                            strSQL = "UPDATE kpmkv_pelajar_markah SET Gred_Kompeten='0' Where PelajarID='" & strPelajarID & "'"
                            strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                            strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)

                        ElseIf (strGredV3 = "C+" Or strGredV3 = "C" Or strGredV3 = "C-" Or strGredV3 = "D+" Or strGredV3 = "D" Or strGredV3 = "D-" Or strGredV3 = "E" Or strGredV3 = "G" Or strGredV3 = "T") Then
                            strSQL = "UPDATE kpmkv_pelajar_markah SET Gred_Kompeten='0' Where PelajarID='" & strPelajarID & "'"
                            strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                            strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)

                        ElseIf (strGredV4 = "C+" Or strGredV4 = "C" Or strGredV4 = "C-" Or strGredV4 = "D+" Or strGredV4 = "D" Or strGredV4 = "D-" Or strGredV4 = "E" Or strGredV4 = "G" Or strGredV4 = "T") Then
                            strSQL = "UPDATE kpmkv_pelajar_markah SET Gred_Kompeten='0' Where PelajarID='" & strPelajarID & "'"
                            strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                            strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)
                        Else

                            strSQL = "UPDATE kpmkv_pelajar_markah SET Gred_Kompeten='1' Where PelajarID='" & strPelajarID & "'"
                            strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                            strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)
                        End If


                    Case "5"
                        strSQL = "SELECT GredV1,GredV2,GredV3,GredV4,GredV5 FROM kpmkv_pelajar_markah"
                        strSQL += " Where PelajarID='" & strPelajarID & "'"
                        strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                        strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                        strRet = oCommon.getFieldValueEx(strSQL)

                        Dim ar_total As Array
                        ar_total = strRet.Split("|")
                        strGredV1 = ar_total(0)
                        strGredV2 = ar_total(1)
                        strGredV3 = ar_total(2)
                        strGredV4 = ar_total(3)
                        strGredV5 = ar_total(4)


                        If (strGredV1 = "C+" Or strGredV1 = "C" Or strGredV1 = "C-" Or strGredV1 = "D+" Or strGredV1 = "D" Or strGredV1 = "D-" Or strGredV1 = "E" Or strGredV1 = "G" Or strGredV1 = "T") Then
                            strSQL = "UPDATE kpmkv_pelajar_markah SET Gred_Kompeten='0' Where PelajarID='" & strPelajarID & "'"
                            strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                            strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)

                        ElseIf (strGredV2 = "C+" Or strGredV2 = "C" Or strGredV2 = "C-" Or strGredV2 = "D+" Or strGredV2 = "D" Or strGredV2 = "D-" Or strGredV2 = "E" Or strGredV2 = "G" Or strGredV2 = "T") Then
                            strSQL = "UPDATE kpmkv_pelajar_markah SET Gred_Kompeten='0' Where PelajarID='" & strPelajarID & "'"
                            strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                            strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)

                        ElseIf (strGredV3 = "C+" Or strGredV3 = "C" Or strGredV3 = "C-" Or strGredV3 = "D+" Or strGredV3 = "D" Or strGredV3 = "D-" Or strGredV3 = "E" Or strGredV3 = "G" Or strGredV3 = "T") Then
                            strSQL = "UPDATE kpmkv_pelajar_markah SET Gred_Kompeten='0' Where PelajarID='" & strPelajarID & "'"
                            strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                            strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)

                        ElseIf (strGredV4 = "C+" Or strGredV4 = "C" Or strGredV4 = "C-" Or strGredV4 = "D+" Or strGredV4 = "D" Or strGredV4 = "D-" Or strGredV4 = "E" Or strGredV4 = "G" Or strGredV4 = "T") Then
                            strSQL = "UPDATE kpmkv_pelajar_markah SET Gred_Kompeten='0' Where PelajarID='" & strPelajarID & "'"
                            strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                            strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)

                        ElseIf (strGredV5 = "C+" Or strGredV5 = "C" Or strGredV5 = "C-" Or strGredV5 = "D+" Or strGredV5 = "D" Or strGredV5 = "D-" Or strGredV5 = "E" Or strGredV5 = "G" Or strGredV5 = "T") Then
                            strSQL = "UPDATE kpmkv_pelajar_markah SET Gred_Kompeten='0' Where PelajarID='" & strPelajarID & "'"
                            strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                            strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)

                        Else
                            strSQL = "UPDATE kpmkv_pelajar_markah SET Gred_Kompeten='1' Where PelajarID='" & strPelajarID & "'"
                            strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                            strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)
                        End If


                    Case "6"

                        strSQL = "SELECT GredV1,GredV2,GredV3,GredV4,GredV5,GredV6 FROM kpmkv_pelajar_markah"
                        strSQL += " Where PelajarID='" & strPelajarID & "'"
                        strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                        strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                        strRet = oCommon.getFieldValueEx(strSQL)

                        Dim ar_total As Array
                        ar_total = strRet.Split("|")
                        strGredV1 = ar_total(0)
                        strGredV2 = ar_total(1)
                        strGredV3 = ar_total(2)
                        strGredV4 = ar_total(3)
                        strGredV5 = ar_total(4)
                        strGredV6 = ar_total(5)


                        If (strGredV1 = "C+" Or strGredV1 = "C" Or strGredV1 = "C-" Or strGredV1 = "D+" Or strGredV1 = "D" Or strGredV1 = "D-" Or strGredV1 = "E" Or strGredV1 = "G" Or strGredV1 = "T") Then
                            strSQL = "UPDATE kpmkv_pelajar_markah SET Gred_Kompeten='0' Where PelajarID='" & strPelajarID & "'"
                            strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                            strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)

                        ElseIf (strGredV2 = "C+" Or strGredV2 = "C" Or strGredV2 = "C-" Or strGredV2 = "D+" Or strGredV2 = "D" Or strGredV2 = "D-" Or strGredV2 = "E" Or strGredV2 = "G" Or strGredV2 = "T") Then
                            strSQL = "UPDATE kpmkv_pelajar_markah SET Gred_Kompeten='0' Where PelajarID='" & strPelajarID & "'"
                            strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                            strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)

                        ElseIf (strGredV3 = "C+" Or strGredV3 = "C" Or strGredV3 = "C-" Or strGredV3 = "D+" Or strGredV3 = "D" Or strGredV3 = "D-" Or strGredV3 = "E" Or strGredV3 = "G" Or strGredV3 = "T") Then
                            strSQL = "UPDATE kpmkv_pelajar_markah SET Gred_Kompeten='0' Where PelajarID='" & strPelajarID & "'"
                            strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                            strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)

                        ElseIf (strGredV4 = "C+" Or strGredV4 = "C" Or strGredV4 = "C-" Or strGredV4 = "D+" Or strGredV4 = "D" Or strGredV4 = "D-" Or strGredV4 = "E" Or strGredV4 = "G" Or strGredV4 = "T") Then
                            strSQL = "UPDATE kpmkv_pelajar_markah SET Gred_Kompeten='0' Where PelajarID='" & strPelajarID & "'"
                            strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                            strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)

                        ElseIf (strGredV5 = "C+" Or strGredV5 = "C" Or strGredV5 = "C-" Or strGredV5 = "D+" Or strGredV5 = "D" Or strGredV5 = "D-" Or strGredV5 = "E" Or strGredV5 = "G" Or strGredV5 = "T") Then
                            strSQL = "UPDATE kpmkv_pelajar_markah SET Gred_Kompeten='0' Where PelajarID='" & strPelajarID & "'"
                            strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                            strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)

                        ElseIf (strGredV6 = "C+" Or strGredV6 = "C" Or strGredV6 = "C-" Or strGredV6 = "D+" Or strGredV6 = "D" Or strGredV6 = "D-" Or strGredV6 = "E" Or strGredV6 = "G" Or strGredV6 = "T") Then
                            strSQL = "UPDATE kpmkv_pelajar_markah SET Gred_Kompeten='0' Where PelajarID='" & strPelajarID & "'"
                            strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                            strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)
                        Else
                            strSQL = "UPDATE kpmkv_pelajar_markah SET Gred_Kompeten='1' Where PelajarID='" & strPelajarID & "'"
                            strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                            strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)
                        End If

                    Case "7"

                        strSQL = "SELECT GredV1,GredV2,GredV3,GredV4,GredV5,GredV6,GredV7 FROM kpmkv_pelajar_markah"
                        strSQL += " Where PelajarID='" & strPelajarID & "'"
                        strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                        strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
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

                        If (strGredV1 = "C+" Or strGredV1 = "C" Or strGredV1 = "C-" Or strGredV1 = "D+" Or strGredV1 = "D" Or strGredV1 = "D-" Or strGredV1 = "E" Or strGredV1 = "G" Or strGredV1 = "T") Then
                            strSQL = "UPDATE kpmkv_pelajar_markah SET Gred_Kompeten='0' Where PelajarID='" & strPelajarID & "'"
                            strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                            strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)

                        ElseIf (strGredV2 = "C+" Or strGredV2 = "C" Or strGredV2 = "C-" Or strGredV2 = "D+" Or strGredV2 = "D" Or strGredV2 = "D-" Or strGredV2 = "E" Or strGredV2 = "G" Or strGredV2 = "T") Then
                            strSQL = "UPDATE kpmkv_pelajar_markah SET Gred_Kompeten='0' Where PelajarID='" & strPelajarID & "'"
                            strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                            strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)

                        ElseIf (strGredV3 = "C+" Or strGredV3 = "C" Or strGredV3 = "C-" Or strGredV3 = "D+" Or strGredV3 = "D" Or strGredV3 = "D-" Or strGredV3 = "E" Or strGredV3 = "G" Or strGredV3 = "T") Then
                            strSQL = "UPDATE kpmkv_pelajar_markah SET Gred_Kompeten='0' Where PelajarID='" & strPelajarID & "'"
                            strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                            strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)

                        ElseIf (strGredV4 = "C+" Or strGredV4 = "C" Or strGredV4 = "C-" Or strGredV4 = "D+" Or strGredV4 = "D" Or strGredV4 = "D-" Or strGredV4 = "E" Or strGredV4 = "G" Or strGredV4 = "T") Then
                            strSQL = "UPDATE kpmkv_pelajar_markah SET Gred_Kompeten='0' Where PelajarID='" & strPelajarID & "'"
                            strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                            strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)

                        ElseIf (strGredV5 = "C+" Or strGredV5 = "C" Or strGredV5 = "C-" Or strGredV5 = "D+" Or strGredV5 = "D" Or strGredV5 = "D-" Or strGredV5 = "E" Or strGredV5 = "G" Or strGredV5 = "T") Then
                            strSQL = "UPDATE kpmkv_pelajar_markah SET Gred_Kompeten='0' Where PelajarID='" & strPelajarID & "'"
                            strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                            strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)

                        ElseIf (strGredV6 = "C+" Or strGredV6 = "C" Or strGredV6 = "C-" Or strGredV6 = "D+" Or strGredV6 = "D" Or strGredV6 = "D-" Or strGredV6 = "E" Or strGredV6 = "G" Or strGredV6 = "T") Then
                            strSQL = "UPDATE kpmkv_pelajar_markah SET Gred_Kompeten='0' Where PelajarID='" & strPelajarID & "'"
                            strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                            strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)

                        ElseIf (strGredV7 = "C+" Or strGredV7 = "C" Or strGredV7 = "C-" Or strGredV7 = "D+" Or strGredV7 = "D" Or strGredV7 = "D-" Or strGredV7 = "E" Or strGredV7 = "G" Or strGredV7 = "T") Then
                            strSQL = "UPDATE kpmkv_pelajar_markah SET Gred_Kompeten='0' Where PelajarID='" & strPelajarID & "'"
                            strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                            strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)
                        Else
                            strSQL = "UPDATE kpmkv_pelajar_markah SET Gred_Kompeten='1' Where PelajarID='" & strPelajarID & "'"
                            strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                            strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)
                        End If


                    Case "8"
                        strSQL = "SELECT GredV1,GredV2,GredV3,GredV4,GredV5,GredV6,GredV7,GredV8 FROM kpmkv_pelajar_markah"
                        strSQL += " Where PelajarID='" & strPelajarID & "'"
                        strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                        strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
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

                        If (strGredV1 = "C+" Or strGredV1 = "C" Or strGredV1 = "C-" Or strGredV1 = "D+" Or strGredV1 = "D" Or strGredV1 = "D-" Or strGredV1 = "E" Or strGredV1 = "G" Or strGredV1 = "T") Then
                            strSQL = "UPDATE kpmkv_pelajar_markah SET Gred_Kompeten='0' Where PelajarID='" & strPelajarID & "'"
                            strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                            strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)

                        ElseIf (strGredV2 = "C+" Or strGredV2 = "C" Or strGredV2 = "C-" Or strGredV2 = "D+" Or strGredV2 = "D" Or strGredV2 = "D-" Or strGredV2 = "E" Or strGredV2 = "G" Or strGredV2 = "T") Then
                            strSQL = "UPDATE kpmkv_pelajar_markah SET Gred_Kompeten='0' Where PelajarID='" & strPelajarID & "'"
                            strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                            strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)

                        ElseIf (strGredV3 = "C+" Or strGredV3 = "C" Or strGredV3 = "C-" Or strGredV3 = "D+" Or strGredV3 = "D" Or strGredV3 = "D-" Or strGredV3 = "E" Or strGredV3 = "G" Or strGredV3 = "T") Then
                            strSQL = "UPDATE kpmkv_pelajar_markah SET Gred_Kompeten='0' Where PelajarID='" & strPelajarID & "'"
                            strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                            strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)

                        ElseIf (strGredV4 = "C+" Or strGredV4 = "C" Or strGredV4 = "C-" Or strGredV4 = "D+" Or strGredV4 = "D" Or strGredV4 = "D-" Or strGredV4 = "E" Or strGredV4 = "G" Or strGredV4 = "T") Then
                            strSQL = "UPDATE kpmkv_pelajar_markah SET Gred_Kompeten='0' Where PelajarID='" & strPelajarID & "'"
                            strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                            strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)

                        ElseIf (strGredV5 = "C+" Or strGredV5 = "C" Or strGredV5 = "C-" Or strGredV5 = "D+" Or strGredV5 = "D" Or strGredV5 = "D-" Or strGredV5 = "E" Or strGredV5 = "G" Or strGredV5 = "T") Then
                            strSQL = "UPDATE kpmkv_pelajar_markah SET Gred_Kompeten='0' Where PelajarID='" & strPelajarID & "'"
                            strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                            strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)

                        ElseIf (strGredV6 = "C+" Or strGredV6 = "C" Or strGredV6 = "C-" Or strGredV6 = "D+" Or strGredV6 = "D" Or strGredV6 = "D-" Or strGredV6 = "E" Or strGredV6 = "G" Or strGredV6 = "T") Then
                            strSQL = "UPDATE kpmkv_pelajar_markah SET Gred_Kompeten='0' Where PelajarID='" & strPelajarID & "'"
                            strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                            strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)

                        ElseIf (strGredV7 = "C+" Or strGredV7 = "C" Or strGredV7 = "C-" Or strGredV7 = "D+" Or strGredV7 = "D" Or strGredV7 = "D-" Or strGredV7 = "E" Or strGredV7 = "G" Or strGredV7 = "T") Then
                            strSQL = "UPDATE kpmkv_pelajar_markah SET Gred_Kompeten='0' Where PelajarID='" & strPelajarID & "'"
                            strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                            strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)

                        ElseIf (strGredV8 = "C+" Or strGredV8 = "C" Or strGredV8 = "C-" Or strGredV8 = "D+" Or strGredV8 = "D" Or strGredV8 = "D-" Or strGredV8 = "E" Or strGredV8 = "G" Or strGredV8 = "T") Then
                            strSQL = "UPDATE kpmkv_pelajar_markah SET Gred_Kompeten='0' Where PelajarID='" & strPelajarID & "'"
                            strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                            strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)

                        Else

                            strSQL = "UPDATE kpmkv_pelajar_markah SET Gred_Kompeten='1' Where PelajarID='" & strPelajarID & "'"
                            strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                            strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)
                        End If



                End Select
            Next
        Catch ex As Exception
            lblMsg.Text = "System Error.Gred:" & ex.Message
        End Try
        '    End If
        'Next



    End Sub

    Private Sub Vokasional_gredSMP()
        'Dim strKursusID As String = ""
        'strRet = BindData(datRespondent)

        'For i As Integer = 0 To datRespondent.Rows.Count - 1

        '    Dim cb As CheckBox = datRespondent.Rows(i).FindControl("chkSelect")

        '    If cb.Checked = True Then
        Try

            '--count no of modul
            Dim nCount As Integer = 0
            strSQL = "SELECT COUNT(kpmkv_modul.KodModul) as CModul "
            strSQL += " FROM kpmkv_modul LEFT OUTER JOIN"
            strSQL += " kpmkv_kursus ON kpmkv_modul.KursusID = kpmkv_kursus.KursusID"
            strSQL += " WHERE kpmkv_modul.KursusID='" & strKursusID & "'"
            strSQL += " AND  kpmkv_modul.Sesi='" & chkSesi.Text & "'"
            strSQL += " AND  kpmkv_modul.Semester='" & ddlSemester.Text & "'"
            strSQL += " AND  kpmkv_modul.Tahun='" & ddlTahun.Text & "'"
            nCount = oCommon.getFieldValueInt(strSQL)

            strSQL = "  SELECT kpmkv_pelajar.PelajarID"
            strSQL += " FROM kpmkv_pelajar_markah "
            strSQL += " LEFT OUTER JOIN kpmkv_pelajar On kpmkv_pelajar_markah.PelajarID = kpmkv_pelajar.PelajarID"
            strSQL += " LEFT OUTER Join kpmkv_kursus On kpmkv_pelajar.KursusID = kpmkv_kursus.KursusID"
            strSQL += " WHERE kpmkv_pelajar.KolejRecordID='" & ddlKolej.SelectedValue & "' "
            strSQL += " AND kpmkv_pelajar.IsDeleted='N' AND kpmkv_pelajar.StatusID='2' AND kpmkv_pelajar.JenisCalonID='2'"
            strSQL += " AND kpmkv_pelajar.Tahun ='" & ddlTahun.Text & "'"
            strSQL += " AND kpmkv_pelajar.Sesi ='" & chkSesi.Text & "'"
            strSQL += " AND kpmkv_pelajar.Semester ='" & ddlSemester.Text & "'"
            strSQL += " AND kpmkv_pelajar.KursusID='" & strKursusID & "'"

            strRet = oCommon.ExecuteSQL(strSQL)

            Dim sqlDA As New SqlDataAdapter(strSQL, objConn)
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            Dim strPelajarID As String = ""


            Dim strCount As Integer = ds.Tables(0).Rows.Count - 1
            For iloop As Integer = 0 To strCount
                strPelajarID = (ds.Tables(0).Rows(iloop).Item(0).ToString())
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


                Dim SMP_PB As Double
                Dim SMP_PA As Double

                Dim SMP_Total As Double

                Select Case nCount
                    Case "2"
                        strSQL = "SELECT PBAV1,PBTV1,PAAV1,PATV1,PBAV2,PBTV2,PAAV2,PATV2 FROM kpmkv_pelajar_markah"
                        strSQL += " Where PelajarID='" & strPelajarID & "'"
                        strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                        strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
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
                            strSQL = "UPDATE kpmkv_pelajar_markah SET SMP_Total='-1' Where PelajarID='" & strPelajarID & "'"
                            strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                            strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)
                        Else
                            SMP_PA = (Convert.ToDouble(PAAV1) + CDbl(PATV1) + CDbl(PAAV2) + CDbl(PATV2)) / 2
                            SMP_PB = (CDbl(PBAV1) + CDbl(PBTV1) + CDbl(PBAV2) + CDbl(PBTV2)) / 2
                            SMP_Total = Math.Ceiling(SMP_PB + SMP_PA)

                            strSQL = "UPDATE kpmkv_pelajar_markah SET SMP_Total='" & SMP_Total & "' Where PelajarID='" & strPelajarID & "'"
                            strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                            strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)
                        End If

                    Case "3"
                        strSQL = "SELECT PBAV1,PBTV1,PAAV1,PATV1,PBAV2,PBTV2,PAAV2,PATV2,PBAV3,PBTV3,PAAV3,PATV3 FROM kpmkv_pelajar_markah"
                        strSQL += " Where PelajarID='" & strPelajarID & "'"
                        strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                        strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
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
                            strSQL = "UPDATE kpmkv_pelajar_markah SET SMP_Total='-1' Where PelajarID='" & strPelajarID & "'"
                            strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                            strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)
                        Else

                            SMP_PA = (CDbl(PAAV1) + CDbl(PATV1) + CDbl(PAAV2) + CDbl(PATV2) + CDbl(PAAV3) + CDbl(PATV3)) / 3
                            SMP_PB = (CDbl(PBAV1) + CDbl(PBTV1) + CDbl(PBAV2) + CDbl(PBTV2) + CDbl(PBAV3) + CDbl(PBTV3)) / 3
                            SMP_Total = Math.Ceiling(SMP_PB + SMP_PA)

                            strSQL = "UPDATE kpmkv_pelajar_markah SET SMP_Total='" & SMP_Total & "' Where PelajarID='" & strPelajarID & "'"
                            strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                            strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)
                        End If

                    Case "4"
                        strSQL = "SELECT PBAV1,PBTV1,PAAV1,PATV1,PBAV2,PBTV2,PAAV2,PATV2,PBAV3,PBTV3,PAAV3,PATV3,PBAV4,PBTV4,PAAV4,PATV4 FROM kpmkv_pelajar_markah"
                        strSQL += " Where PelajarID='" & strPelajarID & "'"
                        strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                        strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
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
                            strSQL = "UPDATE kpmkv_pelajar_markah SET SMP_Total='-1' Where PelajarID='" & strPelajarID & "'"
                            strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                            strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)
                        Else


                            SMP_PA = (Convert.ToDouble(PAAV1) + Convert.ToDouble(PATV1) + Convert.ToDouble(PAAV2) + Convert.ToDouble(PATV2) + Convert.ToDouble(PAAV3) + Convert.ToDouble(PATV3) + Convert.ToDouble(PAAV4) + Convert.ToDouble(PATV4)) / 4
                            SMP_PB = (Convert.ToDouble(PBAV1) + Convert.ToDouble(PBTV1) + Convert.ToDouble(PBAV2) + Convert.ToDouble(PBTV2) + Convert.ToDouble(PBAV3) + Convert.ToDouble(PBTV3) + Convert.ToDouble(PBAV4) + Convert.ToDouble(PBTV4)) / 4
                            SMP_Total = Math.Ceiling(SMP_PB + SMP_PA)

                            strSQL = "UPDATE kpmkv_pelajar_markah SET SMP_Total='" & SMP_Total & "' Where PelajarID='" & strPelajarID & "'"
                            strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                            strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)
                        End If

                    Case "5"
                        strSQL = "SELECT PBAV1,PBTV1,PAAV1,PATV1,PBAV2,PBTV2,PAAV2,PATV2,PBAV3,PBTV3,PAAV3,PATV3,PBAV4,PBTV4,PAAV4,PATV4,PBAV5,PBTV5,PAAV5,PATV5 FROM kpmkv_pelajar_markah"
                        strSQL += " Where PelajarID='" & strPelajarID & "'"
                        strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                        strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
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
                            strSQL = "UPDATE kpmkv_pelajar_markah SET SMP_Total='-1' Where PelajarID='" & strPelajarID & "'"
                            strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                            strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)
                        Else

                            SMP_PA = (CDbl(PAAV1) + CDbl(PATV1) + CDbl(PAAV2) + CDbl(PATV2) + CDbl(PAAV3) + CDbl(PATV3) + CDbl(PAAV4) + CDbl(PATV4) + CDbl(PAAV5) + CDbl(PATV5)) / 5
                            SMP_PB = (CDbl(PBAV1) + CDbl(PBTV1) + CDbl(PBAV2) + CDbl(PBTV2) + CDbl(PBAV3) + CDbl(PBTV3) + CDbl(PBAV4) + CDbl(PBTV4) + CDbl(PBAV5) + CDbl(PBTV5) / 5)
                            SMP_Total = Math.Ceiling(SMP_PB + SMP_PA)

                            strSQL = "UPDATE kpmkv_pelajar_markah SET SMP_Total='" & SMP_Total & "' Where PelajarID='" & strPelajarID & "'"
                            strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                            strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)
                        End If

                    Case "6"
                        strSQL = "SELECT PBAV1,PBTV1,PAAV1,PATV1,PBAV2,PBTV2,PAAV2,PATV2,PBAV3,PBTV3,PAAV3,PATV3,PBAV4,PBTV4,PAAV4,PATV4,PBAV5,PBTV5,PAAV5,PATV5,PBAV6,PBTV6,PAAV6,PATV6 FROM kpmkv_pelajar_markah"
                        strSQL += " Where PelajarID='" & strPelajarID & "'"
                        strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                        strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
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
                            strSQL = "UPDATE kpmkv_pelajar_markah SET SMP_Total='-1' Where PelajarID='" & strPelajarID & "'"
                            strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                            strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)
                        Else



                            SMP_PA = (CDbl(PAAV1) + CDbl(PATV1) + CDbl(PAAV2) + CDbl(PATV2) + CDbl(PAAV3) + CDbl(PATV3) + CDbl(PAAV4) + CDbl(PATV4) + CDbl(PAAV5) + CDbl(PATV5) + CDbl(PAAV6) + CDbl(PATV6)) / 6
                            SMP_PB = (CDbl(PBAV1) + CDbl(PBTV1) + CDbl(PBAV2) + CDbl(PBTV2) + CDbl(PBAV3) + CDbl(PBTV3) + CDbl(PBAV4) + CDbl(PBTV4) + CDbl(PBAV5) + CDbl(PBTV5) + CDbl(PBAV6) + CDbl(PBTV6)) / 6
                            SMP_Total = Math.Ceiling(SMP_PB + SMP_PA)

                            strSQL = "UPDATE kpmkv_pelajar_markah SET SMP_Total='" & SMP_Total & "' Where PelajarID='" & strPelajarID & "'"
                            strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                            strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)
                        End If

                    Case "7"
                        strSQL = "SELECT PBAV1,PBTV1,PAAV1,PATV1,PBAV2,PBTV2,PAAV2,PATV2,PBAV3,PBTV3,PAAV3,PATV3,PBAV4,PBTV4,PAAV4,PATV4,PBAV5,PBTV5,PAAV5,PATV5,PBAV6,PBTV6,PAAV6,PATV6,PBAV7,PBTV7,PAAV7,PATV7 FROM kpmkv_pelajar_markah"
                        strSQL += " Where PelajarID='" & strPelajarID & "'"
                        strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                        strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
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
                            strSQL = "UPDATE kpmkv_pelajar_markah SET SMP_Total='-1' Where PelajarID='" & strPelajarID & "'"
                            strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                            strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)
                        Else


                            SMP_PA = (CDbl(PAAV1) + CDbl(PATV1) + CDbl(PAAV2) + CDbl(PATV2) + CDbl(PAAV3) + CDbl(PATV3) + CDbl(PAAV4) + CDbl(PATV4) + CDbl(PAAV5) + CDbl(PATV5) + CDbl(PAAV6) + CDbl(PATV6) + CDbl(PAAV7) + CDbl(PATV7)) / 7
                            SMP_PB = (CDbl(PBAV1) + CDbl(PBTV1) + CDbl(PBAV2) + CDbl(PBTV2) + CDbl(PBAV3) + CDbl(PBTV3) + CDbl(PBAV4) + CDbl(PBTV4) + CDbl(PBAV5) + CDbl(PBTV5) + CDbl(PBAV6) + CDbl(PBTV6) + CDbl(PBAV7) + CDbl(PBTV7)) / 7
                            SMP_Total = Math.Ceiling(SMP_PB + SMP_PA)

                            strSQL = "UPDATE kpmkv_pelajar_markah SET SMP_Total='" & SMP_Total & "' Where PelajarID='" & strPelajarID & "'"
                            strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                            strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)
                        End If


                End Select
            Next
        Catch ex As Exception
            lblMsg.Text = "System Error.GredSMP:" & ex.Message
        End Try
        '    End If
        'Next
    End Sub

    Private Sub Vokasional_SMP_PB()

        'strRet = BindData(datRespondent)
        'For i As Integer = 0 To datRespondent.Rows.Count - 1

        '    Dim cb As CheckBox = datRespondent.Rows(i).FindControl("chkSelect")

        '    If cb.Checked = True Then
        Try

            '--count no of modul
            Dim nCount As Integer = 0
            strSQL = "SELECT COUNT(kpmkv_modul.KodModul) as CModul "
            strSQL += " FROM kpmkv_modul LEFT OUTER JOIN"
            strSQL += " kpmkv_kursus ON kpmkv_modul.KursusID = kpmkv_kursus.KursusID"
            strSQL += " WHERE kpmkv_modul.KursusID='" & strKursusID & "'"
            strSQL += " AND  kpmkv_modul.Sesi='" & chkSesi.Text & "'"
            strSQL += " AND  kpmkv_modul.Semester='" & ddlSemester.Text & "'"
            strSQL += " AND  kpmkv_modul.Tahun='" & ddlTahun.Text & "'"
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
            strSQL += " WHERE Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "' AND Sesi='" & chkSesi.Text & "'"
            Skor = oCommon.getFieldValue(strSQL)

            strSQL = "  SELECT kpmkv_pelajar.PelajarID"
            strSQL += " FROM kpmkv_pelajar_markah "
            strSQL += " LEFT OUTER JOIN kpmkv_pelajar On kpmkv_pelajar_markah.PelajarID = kpmkv_pelajar.PelajarID"
            strSQL += " LEFT OUTER Join kpmkv_kursus On kpmkv_pelajar.KursusID = kpmkv_kursus.KursusID"
            strSQL += " WHERE kpmkv_pelajar.KolejRecordID='" & ddlKolej.SelectedValue & "' "
            strSQL += " AND kpmkv_pelajar.IsDeleted='N' AND kpmkv_pelajar.StatusID='2' AND kpmkv_pelajar.JenisCalonID='2'"
            strSQL += " AND kpmkv_pelajar.Tahun ='" & ddlTahun.Text & "'"
            strSQL += " AND kpmkv_pelajar.Sesi ='" & chkSesi.Text & "'"
            strSQL += " AND kpmkv_pelajar.Semester ='" & ddlSemester.Text & "'"
            strSQL += " AND kpmkv_pelajar.KursusID='" & strKursusID & "'"

            strRet = oCommon.ExecuteSQL(strSQL)

            Dim sqlDA As New SqlDataAdapter(strSQL, objConn)
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            Dim strPelajarID As String = ""


            Dim strCount As Integer = ds.Tables(0).Rows.Count - 1
            For iloop As Integer = 0 To strCount
                strPelajarID = (ds.Tables(0).Rows(iloop).Item(0).ToString())


                'PBAV1,PBTV1
                'PBAV2,PBTV2
                Select Case nCount
                    Case "2"
                        strSQL = "SELECT PBAV1,PBAV2,PBTV1,PBTV2 FROM kpmkv_pelajar_markah"
                        strSQL += " Where PelajarID='" & strPelajarID & "'"
                        strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                        strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
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
                            strSQL = "UPDATE kpmkv_pelajar_markah SET SMP_PB='0' Where PelajarID='" & strPelajarID & "'"
                            strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                            strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)
                        Else
                            PB_Amali = CDbl(PBA1) + CDbl(PBA2)
                            PB_Teori = CDbl(PBT1) + CDbl(PBT2)

                            PB_Amali_K = (PB_Amali / 2)
                            PB_Teori_K = (PB_Teori / 2)

                            SMP_PB = (PB_Amali_K + PB_Teori_K)

                            If SMP_PB >= CDbl(Skor) Then
                                strSQL = "UPDATE kpmkv_pelajar_markah SET SMP_PB='1' Where PelajarID='" & strPelajarID & "'"
                                strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                                strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                                strRet = oCommon.ExecuteSQL(strSQL)
                            Else
                                strSQL = "UPDATE kpmkv_pelajar_markah SET SMP_PB='0' Where PelajarID='" & strPelajarID & "'"
                                strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                                strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                                strRet = oCommon.ExecuteSQL(strSQL)
                            End If
                        End If

                    Case "3"
                        strSQL = "SELECT PBAV1,PBAV2,PBAV3,PBTV1,PBTV2,PBTV3 FROM kpmkv_pelajar_markah"
                        strSQL += " Where PelajarID='" & strPelajarID & "'"
                        strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                        strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
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
                            strSQL = "UPDATE kpmkv_pelajar_markah SET SMP_PB='0' Where PelajarID='" & strPelajarID & "'"
                            strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                            strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)
                        Else

                            PB_Amali = CDbl(PBA1) + CDbl(PBA2) + CDbl(PBA3)
                            PB_Teori = CDbl(PBT1) + CDbl(PBT2) + CDbl(PBT3)

                            PB_Amali_K = (PB_Amali / 3)
                            PB_Teori_K = (PB_Teori / 3)

                            SMP_PB = (PB_Amali_K + PB_Teori_K)

                            If SMP_PB >= CDbl(Skor) Then
                                strSQL = "UPDATE kpmkv_pelajar_markah SET SMP_PB='1' Where PelajarID='" & strPelajarID & "'"
                                strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                                strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                                strRet = oCommon.ExecuteSQL(strSQL)
                            Else
                                strSQL = "UPDATE kpmkv_pelajar_markah SET SMP_PB='0' Where PelajarID='" & strPelajarID & "'"
                                strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                                strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                                strRet = oCommon.ExecuteSQL(strSQL)
                            End If
                        End If
                    Case "4"
                        strSQL = "SELECT PBAV1,PBAV2,PBAV3,PBAV4,PBTV1,PBTV2,PBTV3,PBTV4 FROM kpmkv_pelajar_markah"
                        strSQL += " Where PelajarID='" & strPelajarID & "'"
                        strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                        strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
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
                            strSQL = "UPDATE kpmkv_pelajar_markah SET SMP_PB='0' Where PelajarID='" & strPelajarID & "'"
                            strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                            strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)
                        Else

                            PB_Amali = CDbl(PBA1) + CDbl(PBA2) + CDbl(PBA3) + CDbl(PBA4)
                            PB_Teori = CDbl(PBT1) + CDbl(PBT2) + CDbl(PBT3) + CDbl(PBT4)

                            PB_Amali_K = (PB_Amali / 4)
                            PB_Teori_K = (PB_Teori / 4)

                            SMP_PB = (PB_Amali_K + PB_Teori_K)

                            If SMP_PB >= CDbl(Skor) Then
                                strSQL = "UPDATE kpmkv_pelajar_markah SET SMP_PB='1' Where PelajarID='" & strPelajarID & "'"
                                strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                                strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                                strRet = oCommon.ExecuteSQL(strSQL)
                            Else
                                strSQL = "UPDATE kpmkv_pelajar_markah SET SMP_PB='0' Where PelajarID='" & strPelajarID & "'"
                                strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                                strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                                strRet = oCommon.ExecuteSQL(strSQL)
                            End If

                        End If
                    Case "5"
                        strSQL = "SELECT PBAV1,PBAV2,PBAV3,PBAV4,PBAV5,PBTV1,PBTV2,PBTV3,PBTV4,PBTV5 FROM kpmkv_pelajar_markah"
                        strSQL += " Where PelajarID='" & strPelajarID & "'"
                        strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                        strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
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
                            strSQL = "UPDATE kpmkv_pelajar_markah SET SMP_PB='0' Where PelajarID='" & strPelajarID & "'"
                            strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                            strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)
                        Else

                            PB_Amali = CDbl(PBA1) + CDbl(PBA2) + CDbl(PBA3) + CDbl(PBA4) + CDbl(PBA5)
                            PB_Teori = CDbl(PBT1) + CDbl(PBT2) + CDbl(PBT3) + CDbl(PBT4) + CDbl(PBT5)

                            PB_Amali_K = (PB_Amali / 5)
                            PB_Teori_K = (PB_Teori / 5)

                            SMP_PB = (PB_Amali_K + PB_Teori_K)

                            If SMP_PB >= CDbl(Skor) Then
                                strSQL = "UPDATE kpmkv_pelajar_markah SET SMP_PB='1' Where PelajarID='" & strPelajarID & "'"
                                strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                                strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                                strRet = oCommon.ExecuteSQL(strSQL)
                            Else
                                strSQL = "UPDATE kpmkv_pelajar_markah SET SMP_PB='0' Where PelajarID='" & strPelajarID & "'"
                                strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                                strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                                strRet = oCommon.ExecuteSQL(strSQL)
                            End If
                        End If
                    Case "6"
                        strSQL = "SELECT PBAV1,PBAV2,PBAV3,PBAV4,PBAV5,PBAV6,PBTV1,PBTV2,PBTV3,PBTV4,PBTV5,PBTV6 FROM kpmkv_pelajar_markah"
                        strSQL += " Where PelajarID='" & strPelajarID & "'"
                        strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                        strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
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
                            strSQL = "UPDATE kpmkv_pelajar_markah SET SMP_PB='0' Where PelajarID='" & strPelajarID & "'"
                            strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                            strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)
                        Else

                            PB_Amali = CDbl(PBA1) + CDbl(PBA2) + CDbl(PBA3) + CDbl(PBA4) + CDbl(PBA5) + CDbl(PBA6)
                            PB_Teori = CDbl(PBT1) + CDbl(PBT2) + CDbl(PBT3) + CDbl(PBT4) + CDbl(PBT5) + CDbl(PBT6)

                            PB_Amali_K = (PB_Amali / 6)
                            PB_Teori_K = (PB_Teori / 6)

                            SMP_PB = (PB_Amali_K + PB_Teori_K)

                            If SMP_PB >= CDbl(Skor) Then
                                strSQL = "UPDATE kpmkv_pelajar_markah SET SMP_PB='1' Where PelajarID='" & strPelajarID & "'"
                                strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                                strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                                strRet = oCommon.ExecuteSQL(strSQL)
                            Else
                                strSQL = "UPDATE kpmkv_pelajar_markah SET SMP_PB='0' Where PelajarID='" & strPelajarID & "'"
                                strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                                strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                                strRet = oCommon.ExecuteSQL(strSQL)
                            End If
                        End If
                    Case "7"
                        strSQL = "SELECT PBAV1,PBAV2,PBAV3,PBAV4,PBAV5,PBAV6,PBAV7,PBTV1,PBTV2,PBTV3,PBTV4,PBTV5,PBTV6,PBTV7 FROM kpmkv_pelajar_markah"
                        strSQL += " Where PelajarID='" & strPelajarID & "'"
                        strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                        strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
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
                            strSQL = "UPDATE kpmkv_pelajar_markah SET SMP_PB='0' Where PelajarID='" & strPelajarID & "'"
                            strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                            strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)
                        Else

                            PB_Amali = CDbl(PBA1) + CDbl(PBA2) + CDbl(PBA3) + CDbl(PBA4) + CDbl(PBA5) + CDbl(PBA6) + CDbl(PBA7)
                            PB_Teori = CDbl(PBT1) + CDbl(PBT2) + CDbl(PBT3) + CDbl(PBT4) + CDbl(PBT5) + CDbl(PBT6) + CDbl(PBT7)

                            PB_Amali_K = (PB_Amali / 7)
                            PB_Teori_K = (PB_Teori / 7)

                            SMP_PB = (PB_Amali_K + PB_Teori_K)

                            If SMP_PB >= CDbl(Skor) Then
                                strSQL = "UPDATE kpmkv_pelajar_markah SET SMP_PB='1' Where PelajarID='" & strPelajarID & "'"
                                strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                                strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                                strRet = oCommon.ExecuteSQL(strSQL)
                            Else
                                strSQL = "UPDATE kpmkv_pelajar_markah SET SMP_PB='0' Where PelajarID='" & strPelajarID & "'"
                                strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                                strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                                strRet = oCommon.ExecuteSQL(strSQL)
                            End If
                        End If

                    Case "8"
                        strSQL = "SELECT PBAV1,PBAV2,PBAV3,PBAV4,PBAV5,PBAV6,PBAV7,PBAV8,PBTV1,PBTV2,PBTV3,PBTV4,PBTV5,PBTV6,PBTV7,PBTV8 FROM kpmkv_pelajar_markah"
                        strSQL += " Where PelajarID='" & strPelajarID & "'"
                        strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                        strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
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
                            strSQL = "UPDATE kpmkv_pelajar_markah SET SMP_PB='0' Where PelajarID='" & strPelajarID & "'"
                            strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                            strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)
                        Else

                            PB_Amali = CDbl(PBA1) + CDbl(PBA2) + CDbl(PBA3) + CDbl(PBA4) + CDbl(PBA5) + CDbl(PBA6) + CDbl(PBA7) + CDbl(PBA8)
                            PB_Teori = CDbl(PBT1) + CDbl(PBT2) + CDbl(PBT3) + CDbl(PBT4) + CDbl(PBT5) + CDbl(PBT6) + CDbl(PBT7) + CDbl(PBT8)

                            PB_Amali_K = (PB_Amali / 8)
                            PB_Teori_K = (PB_Teori / 8)

                            SMP_PB = (PB_Amali_K + PB_Teori_K)

                            If SMP_PB >= CDbl(Skor) Then
                                strSQL = "UPDATE kpmkv_pelajar_markah SET SMP_PB='1' Where PelajarID='" & strPelajarID & "'"
                                strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                                strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                                strRet = oCommon.ExecuteSQL(strSQL)
                            Else
                                strSQL = "UPDATE kpmkv_pelajar_markah SET SMP_PB='0' Where PelajarID='" & strPelajarID & "'"
                                strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                                strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                                strRet = oCommon.ExecuteSQL(strSQL)
                            End If
                        End If

                End Select
            Next

        Catch ex As Exception
            lblMsg.Text = "System Error.PB:" & ex.Message
        End Try
        '    End If
        'Next
    End Sub

    Private Sub Vokasional_SMP_PA()
        'Dim strKursusID As String = ""
        'strRet = BindData(datRespondent)
        'For i As Integer = 0 To datRespondent.Rows.Count - 1

        '    Dim cb As CheckBox = datRespondent.Rows(i).FindControl("chkSelect")

        '    If cb.Checked = True Then
        Try
            'get score SMP_PAA
            strSQL = "SELECT SMP_PAA FROM kpmkv_skor_svm"
            strSQL += " WHERE Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "' AND Sesi='" & chkSesi.Text & "'"
            Dim Skor As String = oCommon.getFieldValue(strSQL)

            'get score SMP_PAA
            strSQL = "SELECT SMP_PAT FROM kpmkv_skor_svm"
            strSQL += " WHERE Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "' AND Sesi='" & chkSesi.Text & "'"
            Dim SkorT As String = oCommon.getFieldValue(strSQL)

            'PA A
            Dim PAA As String
            'PA T
            Dim PAT As String

            strSQL = "  SELECT kpmkv_pelajar.PelajarID"
            strSQL += " FROM kpmkv_pelajar_markah "
            strSQL += " LEFT OUTER JOIN kpmkv_pelajar On kpmkv_pelajar_markah.PelajarID = kpmkv_pelajar.PelajarID"
            strSQL += " LEFT OUTER Join kpmkv_kursus On kpmkv_pelajar.KursusID = kpmkv_kursus.KursusID"
            strSQL += " WHERE kpmkv_pelajar.KolejRecordID='" & ddlKolej.SelectedValue & "' "
            strSQL += " AND kpmkv_pelajar.IsDeleted='N' AND kpmkv_pelajar.StatusID='2' AND kpmkv_pelajar.JenisCalonID='2'"
            strSQL += " AND kpmkv_pelajar.Tahun ='" & ddlTahun.Text & "'"
            strSQL += " AND kpmkv_pelajar.Sesi ='" & chkSesi.Text & "'"
            strSQL += " AND kpmkv_pelajar.Semester ='" & ddlSemester.Text & "'"
            strSQL += " AND kpmkv_pelajar.KursusID='" & strKursusID & "'"

            strRet = oCommon.ExecuteSQL(strSQL)

            Dim sqlDA As New SqlDataAdapter(strSQL, objConn)
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            Dim strPelajarID As String = ""


            Dim strCount As Integer = ds.Tables(0).Rows.Count - 1
            For iloop As Integer = 0 To strCount
                strPelajarID = (ds.Tables(0).Rows(iloop).Item(0).ToString())

                strSQL = "SELECT PAAV1,PATV1 FROM kpmkv_pelajar_markah"
                strSQL += " Where PelajarID='" & strPelajarID & "'"
                strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                strRet = oCommon.getFieldValueEx(strSQL)
                'Response.Write(strSQL)
                ''--get total
                Dim ar_total As Array
                ar_total = strRet.Split("|")
                PAA = ar_total(0)
                PAT = ar_total(1)

                'check pa
                If PAA = -1 Then
                    strSQL = "UPDATE kpmkv_pelajar_markah SET SMP_PAA='0' Where PelajarID='" & strPelajarID & "'"
                    strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                    strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)
                Else
                    If Not PAA = "" Then
                        If CDbl(PAA) >= CDbl(Skor) Then
                            strSQL = "UPDATE kpmkv_pelajar_markah SET SMP_PAA='1' Where PelajarID='" & strPelajarID & "'"
                            strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                            strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)
                        Else
                            strSQL = "UPDATE kpmkv_pelajar_markah SET SMP_PAA='0' Where PelajarID='" & strPelajarID & "'"
                            strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                            strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)
                        End If
                    End If

                End If

                If PAT = -1 Then
                    strSQL = "UPDATE kpmkv_pelajar_markah SET SMP_PAT='0' Where PelajarID='" & strPelajarID & "'"
                    strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                    strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                    strRet = oCommon.ExecuteSQL(strSQL)
                Else
                    If Not PAT = "" Then
                        If CDbl(PAT) >= CDbl(SkorT) Then
                            strSQL = "UPDATE kpmkv_pelajar_markah SET SMP_PAT='1' Where PelajarID='" & strPelajarID & "'"
                            strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                            strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)
                        Else
                            strSQL = "UPDATE kpmkv_pelajar_markah SET SMP_PAT='0' Where PelajarID='" & strPelajarID & "'"
                            strSQL += " AND Tahun='" & ddlTahun.Text & "' AND Semester='" & ddlSemester.Text & "'"
                            strSQL += " AND Sesi='" & chkSesi.Text & "' AND KursusID='" & strKursusID & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)
                        End If
                    End If
                End If

            Next

        Catch ex As Exception
            lblMsg.Text = "System Error.PA:" & ex.Message
        End Try
        '    End If
        'Next
    End Sub

    Private Sub btnGred_Click(sender As Object, e As EventArgs) Handles btnGred.Click
        lblMsg.Text = ""

        For i As Integer = 0 To datRespondent.Rows.Count - 1

            Dim cb As CheckBox = datRespondent.Rows(i).FindControl("chkSelect")

            If cb.Checked = True Then
                strKursusID = datRespondent.DataKeys(i).Value.ToString()

                Vokasional_markah()
                Vokasional_gred()
                Vokasional_SMP_PB()
                Vokasional_SMP_PA()
                Vokasional_gredKompeten()
                Vokasional_gredSMP()

            End If
        Next

        If Not strRet = "0" Then
            divMsg.Attributes("class") = "error"
            lblMsg.Text = "Tidak Berjaya mengemaskini gred Pentaksiran Akhir Vokasional"
        Else
            divMsg.Attributes("class") = "info"
            lblMsg.Text = "Berjaya mengemaskini gred Pentaksiran Akhir Vokasional"


        End If
    End Sub

    Private Sub btnJanaKeseluruhan_Click(sender As Object, e As EventArgs) Handles btnJanaKeseluruhan.Click

        Try

            lblMsg.Text = ""

            strSQL = "SELECT RecordID FROM kpmkv_kolej"

            strRet = oCommon.ExecuteSQL(strSQL)

            Dim sqlDA As New SqlDataAdapter(strSQL, objConn)
            Dim dsRecordID As DataSet = New DataSet
            sqlDA.Fill(dsRecordID, "AnyTable")

            For a As Integer = 0 To dsRecordID.Tables(0).Rows.Count - 1

                Dim strRecordID As String = dsRecordID.Tables(0).Rows(a).Item(0).ToString

                strSQL = "  SELECT kpmkv_pelajar.PelajarID, kpmkv_pelajar.KursusID, kpmkv_pelajar.Semester"
                strSQL += " FROM kpmkv_pelajar_markah "
                strSQL += " LEFT OUTER JOIN kpmkv_pelajar On kpmkv_pelajar_markah.PelajarID = kpmkv_pelajar.PelajarID"
                strSQL += " LEFT OUTER Join kpmkv_kursus On kpmkv_pelajar.KursusID = kpmkv_kursus.KursusID"
                strSQL += " WHERE kpmkv_pelajar.KolejRecordID='" & strRecordID & "' "
                strSQL += " AND kpmkv_pelajar.IsDeleted='N' AND kpmkv_pelajar.StatusID='2' AND kpmkv_pelajar.JenisCalonID='2'"
                strSQL += " AND kpmkv_pelajar.Tahun ='" & ddlTahun.Text & "'"

                If Not ddlSemester.Text = "-Pilih-" Then

                    strSQL += " AND kpmkv_pelajar.Semester ='" & ddlSemester.Text & "'"

                End If

                strRet = oCommon.ExecuteSQL(strSQL)

                sqlDA = New SqlDataAdapter(strSQL, objConn)
                Dim dsPelajar As DataSet = New DataSet
                sqlDA.Fill(dsPelajar, "AnyTable")

                For b As Integer = 0 To dsPelajar.Tables(0).Rows.Count - 1

                    strPelajarID = dsPelajar.Tables(0).Rows(b).Item(0).ToString
                    strKursusID = dsPelajar.Tables(0).Rows(b).Item(1).ToString
                    strSemester = dsPelajar.Tables(0).Rows(b).Item(2).ToString

                    Vokasional_markah_keseluruhan()
                    Vokasional_gred_keseluruhan()
                    Vokasional_SMP_PB_keseluruhan()
                    Vokasional_SMP_PA_keseluruhan()
                    Vokasional_gredKompeten_keseluruhan()
                    Vokasional_gredSMP_keseluruhan()

                Next

            Next

            If Not strRet = "0" Then
                divMsg.Attributes("class") = "info"
                lblMsg.Text = "Berjaya mengemaskini gred Pentaksiran Akhir Vokasional"
            Else
                divMsg.Attributes("class") = "info"
                lblMsg.Text = "Berjaya mengemaskini gred Pentaksiran Akhir Vokasional"
            End If



        Catch ex As Exception

            divMsg.Attributes("class") = "info"
            lblMsg.Text = "Berjaya mengemaskini gred Pentaksiran Akhir Vokasional"

        End Try

    End Sub

    Private Sub Vokasional_markah_keseluruhan()

        Dim tempSkipIfNull As String = ""

        tempSkipIfNull = oCommon.getFieldValue(strSQL)
        If Not tempSkipIfNull = "" Then



        Else

            Exit Sub

        End If

        strSQL = "SELECT KodModul from kpmkv_modul WHERE KursusID='" & strKursusID & "' AND Tahun='" & ddlTahun.Text & "' AND Semester='" & strSemester & "' AND KodModul Like '%01%' "
        Dim strModul1 As String = oCommon.getFieldValue(strSQL) '1

        strSQL = "SELECT KodModul from kpmkv_modul WHERE KursusID='" & strKursusID & "' AND Tahun='" & ddlTahun.Text & "' AND Semester='" & strSemester & "' AND KodModul Like '%02%' "
        Dim strModul2 As String = oCommon.getFieldValue(strSQL) '2

        strSQL = "SELECT KodModul from kpmkv_modul WHERE KursusID='" & strKursusID & "' AND Tahun='" & ddlTahun.Text & "' AND Semester='" & strSemester & "' AND KodModul Like '%03%' "
        Dim strModul3 As String = oCommon.getFieldValue(strSQL) '3

        strSQL = "SELECT KodModul from kpmkv_modul WHERE KursusID='" & strKursusID & "' AND Tahun='" & ddlTahun.Text & "' AND Semester='" & strSemester & "' AND KodModul Like '%04%' "
        Dim strModul4 As String = oCommon.getFieldValue(strSQL) '4

        strSQL = "SELECT KodModul from kpmkv_modul WHERE KursusID='" & strKursusID & "' AND Tahun='" & ddlTahun.Text & "' AND Semester='" & strSemester & "' AND KodModul Like '%05%' "
        Dim strModul5 As String = oCommon.getFieldValue(strSQL) '5

        strSQL = "SELECT KodModul from kpmkv_modul WHERE KursusID='" & strKursusID & "' AND Tahun='" & ddlTahun.Text & "' AND Semester='" & strSemester & "' AND KodModul Like '%06%' "
        Dim strModul6 As String = oCommon.getFieldValue(strSQL) '6

        strSQL = "SELECT KodModul from kpmkv_modul WHERE KursusID='" & strKursusID & "' AND Tahun='" & ddlTahun.Text & "' AND Semester='" & strSemester & "' AND KodModul Like '%07%' "
        Dim strModul7 As String = oCommon.getFieldValue(strSQL) '7

        strSQL = "SELECT KodModul from kpmkv_modul WHERE KursusID='" & strKursusID & "' AND Tahun='" & ddlTahun.Text & "' AND Semester='" & strSemester & "' AND KodModul Like '%08%' "
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

        strSQL = "Select PBAmali from kpmkv_modul Where KodModul= '" & strModul1 & "' AND KursusID='" & strKursusID & "' AND Tahun='" & ddlTahun.Text & "' AND Semester='" & strSemester & "' AND Sesi='" & chkSesi.Text & "'"
        PBAmali1 = oCommon.getFieldValue(strSQL)

        strSQL = "Select PBTeori from kpmkv_modul Where KodModul= '" & strModul1 & "' AND KursusID='" & strKursusID & "' AND Tahun='" & ddlTahun.Text & "' AND Semester='" & strSemester & "' AND Sesi='" & chkSesi.Text & "'"
        PBTeori1 = oCommon.getFieldValue(strSQL)

        strSQL = "Select PAAmali from kpmkv_modul Where KodModul= '" & strModul1 & "' AND KursusID='" & strKursusID & "' AND Tahun='" & ddlTahun.Text & "' AND Semester='" & strSemester & "' AND Sesi='" & chkSesi.Text & "'"
        PAAmali1 = oCommon.getFieldValue(strSQL)

        strSQL = "Select PATeori from kpmkv_modul Where KodModul= '" & strModul1 & "' AND KursusID='" & strKursusID & "' AND Tahun='" & ddlTahun.Text & "' AND Semester='" & strSemester & "' AND Sesi='" & chkSesi.Text & "'"
        PATeori1 = oCommon.getFieldValue(strSQL)

        strSQL = "Select B_Amali1 from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
        tempSkipIfNull = oCommon.getFieldValue(strSQL)

        If Not tempSkipIfNull = "" Then

            B_Amali1 = oCommon.getFieldValue(strSQL)
            'round up
            B_Amali1 = (B_Amali1)

        Else

            B_Amali1 = 0

        End If

        strSQL = "Select B_Teori1 from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
        tempSkipIfNull = oCommon.getFieldValue(strSQL)
        If Not tempSkipIfNull = "" Then

            B_Teori1 = oCommon.getFieldValue(strSQL)
            'round up
            B_Teori1 = (B_Teori1)

        Else

            B_Teori1 = 0

        End If

        strSQL = "Select A_Amali1 from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
        tempSkipIfNull = oCommon.getFieldValue(strSQL)
        If Not tempSkipIfNull = "" Then

            A_Amali1 = oCommon.getFieldValue(strSQL)
            'round up
            A_Amali1 = (A_Amali1)

        Else

            A_Amali1 = 0

        End If

        strSQL = "Select A_Teori1 from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
        tempSkipIfNull = oCommon.getFieldValue(strSQL)
        If Not tempSkipIfNull = "" Then

            A_Teori1 = oCommon.getFieldValueInt(strSQL)
            'round up
            A_Teori1 = (A_Teori1)

        Else

            A_Teori1 = 0

        End If

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
            strSQL += " Where PelajarID='" & strPelajarID & "'"
            strRet = oCommon.ExecuteSQL(strSQL)
        Else

            PBA1 = ((B_Amali1 / 100) * PBAmali1)
            PBT1 = ((B_Teori1 / 100) * PBTeori1)
            PAA1 = ((A_Amali1 / 100) * PAAmali1)
            PAT1 = ((A_Teori1 / 100) * PATeori1)

            'change on 31/7/2017 PBAV1,PBTV1,PAAV1,PATV1
            PointerM1 = Math.Ceiling(PBA1 + PBT1 + PAA1 + PAT1)
            strSQL = "UPDATE kpmkv_pelajar_markah SET PBPAM1='" & PointerM1 & "',"
            strSQL += "PBAV1='" & PBA1 & "',PBTV1='" & PBT1 & "',PAAV1='" & PAA1 & "',PATV1='" & PAT1 & "'"
            strSQL += " Where PelajarID='" & strPelajarID & "'"
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

            strSQL = "Select PBAmali from kpmkv_modul Where KodModul= '" & strModul2 & "' AND KursusID='" & strKursusID & "' AND Tahun='" & ddlTahun.Text & "' AND Semester='" & strSemester & "'"
            PBAmali2 = oCommon.getFieldValue(strSQL)

            strSQL = "Select PBTeori from kpmkv_modul Where KodModul= '" & strModul2 & "' AND KursusID='" & strKursusID & "' AND Tahun='" & ddlTahun.Text & "' AND Semester='" & strSemester & "'"
            PBTeori2 = oCommon.getFieldValue(strSQL)

            strSQL = "Select PAAmali from kpmkv_modul Where KodModul= '" & strModul2 & "' AND KursusID='" & strKursusID & "' AND Tahun='" & ddlTahun.Text & "' AND Semester='" & strSemester & "'"
            PAAmali2 = oCommon.getFieldValue(strSQL)

            strSQL = "Select PATeori from kpmkv_modul Where KodModul= '" & strModul2 & "' AND KursusID='" & strKursusID & "' AND Tahun='" & ddlTahun.Text & "' AND Semester='" & strSemester & "'"
            PATeori2 = oCommon.getFieldValue(strSQL)

            strSQL = "Select B_Amali2 from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
            tempSkipIfNull = oCommon.getFieldValue(strSQL)
            If Not tempSkipIfNull = "" Then

                B_Amali2 = oCommon.getFieldValue(strSQL)
                'round up
                B_Amali2 = (B_Amali2)

            Else

                B_Amali2 = 0

            End If

            strSQL = "Select B_Teori2 from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
            tempSkipIfNull = oCommon.getFieldValue(strSQL)
            If Not tempSkipIfNull = "" Then

                B_Teori2 = oCommon.getFieldValue(strSQL)
                'round up
                B_Teori2 = (B_Teori2)

            Else

                B_Teori2 = 0

            End If

            strSQL = "Select A_Amali2 from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
            tempSkipIfNull = oCommon.getFieldValue(strSQL)
            If Not tempSkipIfNull = "" Then

                A_Amali2 = oCommon.getFieldValue(strSQL)
                'round up
                A_Amali2 = (A_Amali2)

            Else

                A_Amali2 = 0
            End If

            strSQL = "Select A_Teori2 from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
            tempSkipIfNull = oCommon.getFieldValue(strSQL)
            If Not tempSkipIfNull = "" Then

                A_Teori2 = oCommon.getFieldValue(strSQL)
                'round up
                A_Teori2 = (A_Teori2)

            Else

                A_Teori2 = 0
            End If

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
                strSQL += " Where PelajarID='" & strPelajarID & "'"
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
                strSQL += " Where PelajarID='" & strPelajarID & "'"
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

            strSQL = "Select PBAmali from kpmkv_modul Where KodModul= '" & strModul3 & "' AND KursusID='" & strKursusID & "' AND Tahun='" & ddlTahun.Text & "' AND Semester='" & strSemester & "'"
            PBAmali3 = oCommon.getFieldValue(strSQL)

            strSQL = "Select PBTeori from kpmkv_modul Where KodModul= '" & strModul3 & "' AND KursusID='" & strKursusID & "' AND Tahun='" & ddlTahun.Text & "' AND Semester='" & strSemester & "'"
            PBTeori3 = oCommon.getFieldValue(strSQL)

            strSQL = "Select PAAmali from kpmkv_modul Where KodModul= '" & strModul3 & "' AND KursusID='" & strKursusID & "' AND Tahun='" & ddlTahun.Text & "' AND Semester='" & strSemester & "'"
            PAAmali3 = oCommon.getFieldValue(strSQL)

            strSQL = "Select PATeori from kpmkv_modul Where KodModul= '" & strModul3 & "' AND KursusID='" & strKursusID & "' AND Tahun='" & ddlTahun.Text & "' AND Semester='" & strSemester & "'"
            PATeori3 = oCommon.getFieldValue(strSQL)

            strSQL = "Select B_Amali3 from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
            tempSkipIfNull = oCommon.getFieldValue(strSQL)
            If Not tempSkipIfNull = "" Then

                B_Amali3 = oCommon.getFieldValue(strSQL)
                'round up
                B_Amali3 = (B_Amali3)

            Else

                B_Amali3 = 0
            End If

            strSQL = "Select B_Teori3 from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
            tempSkipIfNull = oCommon.getFieldValue(strSQL)
            If Not tempSkipIfNull = "" Then

                B_Teori3 = oCommon.getFieldValue(strSQL)
                'round up
                B_Teori3 = (B_Teori3)

            Else

                B_Teori3 = 0
            End If

            strSQL = "Select A_Amali3 from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
            tempSkipIfNull = oCommon.getFieldValue(strSQL)
            If Not tempSkipIfNull = "" Then

                A_Amali3 = oCommon.getFieldValue(strSQL)
                'round up
                A_Amali3 = (A_Amali3)

            Else

                A_Amali3 = 0
            End If

            strSQL = "Select A_Teori3 from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
            tempSkipIfNull = oCommon.getFieldValue(strSQL)
            If Not tempSkipIfNull = "" Then

                A_Teori3 = oCommon.getFieldValue(strSQL)
                'round up
                A_Teori3 = (A_Teori3)

            Else

                A_Teori3 = 0
            End If

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
                strSQL += " Where PelajarID='" & strPelajarID & "'"
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
                strSQL += " Where PelajarID='" & strPelajarID & "'"
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

            strSQL = "Select PBAmali from kpmkv_modul Where KodModul= '" & strModul4 & "' AND KursusID='" & strKursusID & "' AND Tahun='" & ddlTahun.Text & "' AND Semester='" & strSemester & "'"
            PBAmali4 = oCommon.getFieldValue(strSQL)

            strSQL = "Select PBTeori from kpmkv_modul Where KodModul= '" & strModul4 & "' AND KursusID='" & strKursusID & "' AND Tahun='" & ddlTahun.Text & "' AND Semester='" & strSemester & "'"
            PBTeori4 = oCommon.getFieldValue(strSQL)

            strSQL = "Select PAAmali from kpmkv_modul Where KodModul= '" & strModul4 & "' AND KursusID='" & strKursusID & "' AND Tahun='" & ddlTahun.Text & "' AND Semester='" & strSemester & "'"
            PAAmali4 = oCommon.getFieldValue(strSQL)

            strSQL = "Select PATeori from kpmkv_modul Where KodModul= '" & strModul4 & "' AND KursusID='" & strKursusID & "' AND Tahun='" & ddlTahun.Text & "' AND Semester='" & strSemester & "'"
            PATeori4 = oCommon.getFieldValue(strSQL)

            strSQL = "Select B_Amali4 from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
            tempSkipIfNull = oCommon.getFieldValue(strSQL)
            If Not tempSkipIfNull = "" Then

                B_Amali4 = oCommon.getFieldValue(strSQL)
                'round up
                B_Amali4 = (B_Amali4)

            Else

                B_Amali4 = 0
            End If

            strSQL = "Select B_Teori4 from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
            tempSkipIfNull = oCommon.getFieldValue(strSQL)
            If Not tempSkipIfNull = "" Then

                B_Teori4 = oCommon.getFieldValue(strSQL)
                'round up
                B_Teori4 = (B_Teori4)

            Else

                B_Teori4 = 0
            End If

            strSQL = "Select A_Amali4 from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
            tempSkipIfNull = oCommon.getFieldValue(strSQL)
            If Not tempSkipIfNull = "" Then

                A_Amali4 = oCommon.getFieldValue(strSQL)
                'round up
                A_Amali4 = (A_Amali4)

            Else

                A_Amali4 = 0
            End If

            strSQL = "Select A_Teori4 from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
            tempSkipIfNull = oCommon.getFieldValue(strSQL)
            If Not tempSkipIfNull = "" Then

                A_Teori4 = oCommon.getFieldValue(strSQL)
                'round up
                A_Teori4 = (A_Teori4)

            Else

                A_Teori4 = 0
            End If

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
                strSQL += " Where PelajarID='" & strPelajarID & "'"
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
                strSQL += " Where PelajarID='" & strPelajarID & "'"
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

            strSQL = "Select PBAmali from kpmkv_modul Where KodModul= '" & strModul5 & "' AND KursusID='" & strKursusID & "' AND Tahun='" & ddlTahun.Text & "' AND Semester='" & strSemester & "'"
            PBAmali5 = oCommon.getFieldValue(strSQL)

            strSQL = "Select PBTeori from kpmkv_modul Where KodModul= '" & strModul5 & "' AND KursusID='" & strKursusID & "' AND Tahun='" & ddlTahun.Text & "' AND Semester='" & strSemester & "'"
            PBTeori5 = oCommon.getFieldValue(strSQL)

            strSQL = "Select PAAmali from kpmkv_modul Where KodModul= '" & strModul5 & "' AND KursusID='" & strKursusID & "' AND Tahun='" & ddlTahun.Text & "' AND Semester='" & strSemester & "'"
            PAAmali5 = oCommon.getFieldValue(strSQL)

            strSQL = "Select PATeori from kpmkv_modul Where KodModul= '" & strModul5 & "' AND KursusID='" & strKursusID & "' AND Tahun='" & ddlTahun.Text & "' AND Semester='" & strSemester & "'"
            PATeori5 = oCommon.getFieldValue(strSQL)

            strSQL = "Select B_Amali5 from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
            tempSkipIfNull = oCommon.getFieldValue(strSQL)
            If Not tempSkipIfNull = "" Then

                B_Amali5 = oCommon.getFieldValue(strSQL)
                'round up
                B_Amali5 = (B_Amali5)

            Else

                B_Amali5 = 0
            End If

            strSQL = "Select B_Teori5 from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
            tempSkipIfNull = oCommon.getFieldValue(strSQL)
            If Not tempSkipIfNull = "" Then

                B_Teori5 = oCommon.getFieldValue(strSQL)
                'round up
                B_Teori5 = (B_Teori5)

            Else

                B_Teori5 = 0
            End If

            strSQL = "Select A_Amali5 from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
            tempSkipIfNull = oCommon.getFieldValue(strSQL)
            If Not tempSkipIfNull = "" Then

                A_Amali5 = oCommon.getFieldValue(strSQL)
                'round up
                A_Amali5 = (A_Amali5)

            Else

                A_Amali5 = 0
            End If

            strSQL = "Select A_Teori5 from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
            tempSkipIfNull = oCommon.getFieldValue(strSQL)
            If Not tempSkipIfNull = "" Then

                A_Teori5 = oCommon.getFieldValue(strSQL)
                'round up
                A_Teori5 = (A_Teori5)

            Else

                A_Teori5 = 0
            End If

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
                strSQL += " Where PelajarID='" & strPelajarID & "'"
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
                strSQL += " Where PelajarID='" & strPelajarID & "'"
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

            strSQL = "Select PBAmali from kpmkv_modul Where KodModul= '" & strModul6 & "' AND KursusID='" & strKursusID & "' AND Tahun='" & ddlTahun.Text & "' AND Semester='" & strSemester & "'"
            PBAmali6 = oCommon.getFieldValue(strSQL)

            strSQL = "Select PBTeori from kpmkv_modul Where KodModul= '" & strModul6 & "' AND KursusID='" & strKursusID & "' AND Tahun='" & ddlTahun.Text & "' AND Semester='" & strSemester & "'"
            PBTeori6 = oCommon.getFieldValue(strSQL)

            strSQL = "Select PAAmali from kpmkv_modul Where KodModul= '" & strModul6 & "' AND KursusID='" & strKursusID & "' AND Tahun='" & ddlTahun.Text & "' AND Semester='" & strSemester & "'"
            PAAmali6 = oCommon.getFieldValue(strSQL)

            strSQL = "Select PATeori from kpmkv_modul Where KodModul= '" & strModul6 & "' AND KursusID='" & strKursusID & "' AND Tahun='" & ddlTahun.Text & "' AND Semester='" & strSemester & "'"
            PATeori6 = oCommon.getFieldValue(strSQL)

            strSQL = "Select B_Amali6 from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
            tempSkipIfNull = oCommon.getFieldValue(strSQL)
            If Not tempSkipIfNull = "" Then

                B_Amali6 = oCommon.getFieldValue(strSQL)
                'round up
                B_Amali6 = (B_Amali6)

            Else

                B_Amali6 = 0
            End If

            strSQL = "Select B_Teori6 from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
            tempSkipIfNull = oCommon.getFieldValue(strSQL)
            If Not tempSkipIfNull = "" Then

                B_Teori6 = oCommon.getFieldValue(strSQL)
                'round up
                B_Teori6 = (B_Teori6)

            Else

                B_Teori6 = 0
            End If

            strSQL = "Select A_Amali6 from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
            tempSkipIfNull = oCommon.getFieldValue(strSQL)
            If Not tempSkipIfNull = "" Then

                A_Amali6 = oCommon.getFieldValue(strSQL)
                'round up
                A_Amali6 = (A_Amali6)

            Else

                A_Amali6 = 0
            End If

            strSQL = "Select A_Teori6 from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
            tempSkipIfNull = oCommon.getFieldValue(strSQL)
            If Not tempSkipIfNull = "" Then

                A_Teori6 = oCommon.getFieldValue(strSQL)
                'round up
                A_Teori6 = (A_Teori6)

            Else

                A_Teori6 = 0
            End If

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
                strSQL += " Where PelajarID='" & strPelajarID & "'"
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
                strSQL += " Where PelajarID='" & strPelajarID & "'"
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

            strSQL = "Select PBAmali from kpmkv_modul Where KodModul= '" & strModul7 & "' AND KursusID='" & strKursusID & "' AND Tahun='" & ddlTahun.Text & "' AND Semester='" & strSemester & "'"
            PBAmali7 = oCommon.getFieldValue(strSQL)

            strSQL = "Select PBTeori from kpmkv_modul Where KodModul= '" & strModul7 & "' AND KursusID='" & strKursusID & "' AND Tahun='" & ddlTahun.Text & "' AND Semester='" & strSemester & "'"
            PBTeori7 = oCommon.getFieldValue(strSQL)

            strSQL = "Select PAAmali from kpmkv_modul Where KodModul= '" & strModul7 & "' AND KursusID='" & strKursusID & "' AND Tahun='" & ddlTahun.Text & "' AND Semester='" & strSemester & "'"
            PAAmali7 = oCommon.getFieldValue(strSQL)

            strSQL = "Select PATeori from kpmkv_modul Where KodModul= '" & strModul7 & "' AND KursusID='" & strKursusID & "' AND Tahun='" & ddlTahun.Text & "' AND Semester='" & strSemester & "'"
            PATeori7 = oCommon.getFieldValue(strSQL)

            strSQL = "Select B_Amali7 from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
            tempSkipIfNull = oCommon.getFieldValue(strSQL)
            If Not tempSkipIfNull = "" Then

                B_Amali7 = oCommon.getFieldValue(strSQL)
                'round up
                B_Amali7 = (B_Amali7)

            Else

                B_Amali7 = 0
            End If

            strSQL = "Select B_Teori7 from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
            tempSkipIfNull = oCommon.getFieldValue(strSQL)
            If Not tempSkipIfNull = "" Then

                B_Teori7 = oCommon.getFieldValue(strSQL)
                'round up
                B_Teori7 = (B_Teori7)

            Else

                B_Teori7 = 0
            End If

            strSQL = "Select A_Amali7 from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
            tempSkipIfNull = oCommon.getFieldValue(strSQL)
            If Not tempSkipIfNull = "" Then

                A_Amali7 = oCommon.getFieldValue(strSQL)
                'round up
                A_Amali7 = (A_Amali7)

            Else

                A_Amali7 = 0
            End If

            strSQL = "Select A_Teori7 from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
            tempSkipIfNull = oCommon.getFieldValue(strSQL)
            If Not tempSkipIfNull = "" Then

                A_Teori7 = oCommon.getFieldValue(strSQL)
                'round up
                A_Teori7 = (A_Teori7)

            Else

                A_Teori7 = 0
            End If

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
                strSQL += " Where PelajarID='" & strPelajarID & "'"
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
                strSQL += " Where PelajarID='" & strPelajarID & "'"
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

            strSQL = "Select PBAmali from kpmkv_modul Where KodModul= '" & strModul8 & "' AND KursusID='" & strKursusID & "' AND Tahun='" & ddlTahun.Text & "' AND Semester='" & strSemester & "'"
            PBAmali8 = oCommon.getFieldValue(strSQL)

            strSQL = "Select PBTeori from kpmkv_modul Where KodModul= '" & strModul8 & "' AND KursusID='" & strKursusID & "' AND Tahun='" & ddlTahun.Text & "' AND Semester='" & strSemester & "'"
            PBTeori8 = oCommon.getFieldValue(strSQL)

            strSQL = "Select PAAmali from kpmkv_modul Where KodModul= '" & strModul8 & "' AND KursusID='" & strKursusID & "' AND Tahun='" & ddlTahun.Text & "' AND Semester='" & strSemester & "'"
            PAAmali8 = oCommon.getFieldValue(strSQL)

            strSQL = "Select PATeori from kpmkv_modul Where KodModul= '" & strModul8 & "' AND KursusID='" & strKursusID & "' AND Tahun='" & ddlTahun.Text & "' AND Semester='" & strSemester & "'"
            PATeori8 = oCommon.getFieldValue(strSQL)

            strSQL = "Select B_Amali8 from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
            tempSkipIfNull = oCommon.getFieldValue(strSQL)
            If Not tempSkipIfNull = "" Then

                B_Amali8 = oCommon.getFieldValue(strSQL)
                'round up
                B_Amali8 = (B_Amali8)

            Else

                B_Amali8 = 0
            End If

            strSQL = "Select B_Teori8 from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
            tempSkipIfNull = oCommon.getFieldValue(strSQL)
            If Not tempSkipIfNull = "" Then

                B_Teori8 = oCommon.getFieldValue(strSQL)
                'round up
                B_Teori8 = (B_Teori8)

            Else

                B_Teori8 = 0
            End If

            strSQL = "Select A_Amali8 from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
            tempSkipIfNull = oCommon.getFieldValue(strSQL)
            If Not tempSkipIfNull = "" Then

                A_Amali8 = oCommon.getFieldValue(strSQL)
                'round up
                A_Amali8 = (A_Amali8)

            Else

                A_Amali8 = 0
            End If

            strSQL = "Select A_Teori8 from kpmkv_pelajar_markah Where PelajarID='" & strPelajarID & "'"
            tempSkipIfNull = oCommon.getFieldValue(strSQL)
            If Not tempSkipIfNull = "" Then

                A_Teori8 = oCommon.getFieldValue(strSQL)
                'round up
                A_Teori8 = (A_Teori8)

            Else

                A_Teori8 = 0
            End If

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
                strSQL += " Where PelajarID='" & strPelajarID & "'"
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
                strSQL += " Where PelajarID='" & strPelajarID & "'"
                strRet = oCommon.ExecuteSQL(strSQL)
            End If

        End If

    End Sub

    Private Sub Vokasional_gred_keseluruhan()

        Dim PBPAM1 As String
        Dim GredPBPAM1 As String

        strSQL = "SELECT PBPAM1 FROM kpmkv_pelajar_markah"
        strSQL += " Where PelajarID='" & strPelajarID & "'"
        PBPAM1 = oCommon.getFieldValueInt(strSQL)

        If String.IsNullOrEmpty(PBPAM1) Then
            strSQL = "UPDATE kpmkv_pelajar_markah SET GredV1='' Where PelajarID='" & strPelajarID & "'"
            strRet = oCommon.ExecuteSQL(strSQL)
        ElseIf PBPAM1 = 0 Then
            strSQL = "UPDATE kpmkv_pelajar_markah SET GredV1='E' Where PelajarID='" & strPelajarID & "'"
            strRet = oCommon.ExecuteSQL(strSQL)
        ElseIf PBPAM1 = -1 Then
            strSQL = "UPDATE kpmkv_pelajar_markah SET GredV1='T' Where PelajarID='" & strPelajarID & "'"
            strRet = oCommon.ExecuteSQL(strSQL)
        Else
            strSQL = "SELECT TOP ( 1 ) Gred FROM  kpmkv_gred WHERE '" & PBPAM1 & "' BETWEEN MarkahFrom AND MarkahTo AND Jenis='VOKASIONAL'"
            GredPBPAM1 = oCommon.getFieldValue(strSQL)

            strSQL = "UPDATE kpmkv_pelajar_markah SET GredV1='" & GredPBPAM1 & "' Where PelajarID='" & strPelajarID & "'"
            strRet = oCommon.ExecuteSQL(strSQL)

        End If

        '-----------------------------------------------------------------
        Dim PBPAM2 As String
        Dim GredPBPAM2 As String

        strSQL = "SELECT PBPAM2 FROM kpmkv_pelajar_markah"
        strSQL += " Where PelajarID='" & strPelajarID & "'"
        PBPAM2 = oCommon.getFieldValueInt(strSQL)

        If String.IsNullOrEmpty(PBPAM2) Then
            strSQL = "UPDATE kpmkv_pelajar_markah SET GredV2='' Where PelajarID='" & strPelajarID & "'"
            strRet = oCommon.ExecuteSQL(strSQL)
        ElseIf PBPAM2 = 0 Then
            strSQL = "UPDATE kpmkv_pelajar_markah SET GredV2='E' Where PelajarID='" & strPelajarID & "'"
            strRet = oCommon.ExecuteSQL(strSQL)
        ElseIf PBPAM2 = -1 Then
            strSQL = "UPDATE kpmkv_pelajar_markah SET GredV2='T' Where PelajarID='" & strPelajarID & "'"
            strRet = oCommon.ExecuteSQL(strSQL)
        Else
            strSQL = "SELECT TOP ( 1 ) Gred FROM  kpmkv_gred WHERE '" & PBPAM2 & "' BETWEEN MarkahFrom AND MarkahTo AND Jenis='VOKASIONAL'"
            GredPBPAM2 = oCommon.getFieldValue(strSQL)

            strSQL = "UPDATE kpmkv_pelajar_markah SET GredV2='" & GredPBPAM2 & "' Where PelajarID='" & strPelajarID & "'"
            strRet = oCommon.ExecuteSQL(strSQL)

        End If
        '------------------------------------------------------------------------------------------------------------------------
        Dim PBPAM3 As String
        Dim GredPBPAM3 As String

        strSQL = "SELECT PBPAM3 FROM kpmkv_pelajar_markah"
        strSQL += " Where PelajarID='" & strPelajarID & "'"
        PBPAM3 = oCommon.getFieldValueInt(strSQL)

        If String.IsNullOrEmpty(PBPAM3) Then
            strSQL = "UPDATE kpmkv_pelajar_markah SET GredV3='' Where PelajarID='" & strPelajarID & "'"
            strRet = oCommon.ExecuteSQL(strSQL)
        ElseIf PBPAM3 = 0 Then
            strSQL = "UPDATE kpmkv_pelajar_markah SET GredV3='E' Where PelajarID='" & strPelajarID & "'"
            strRet = oCommon.ExecuteSQL(strSQL)
        ElseIf PBPAM3 = -1 Then
            strSQL = "UPDATE kpmkv_pelajar_markah SET GredV3='T' Where PelajarID='" & strPelajarID & "'"
            strRet = oCommon.ExecuteSQL(strSQL)
        Else
            strSQL = "SELECT TOP ( 1 ) Gred FROM  kpmkv_gred WHERE '" & PBPAM3 & "' BETWEEN MarkahFrom AND MarkahTo AND Jenis='VOKASIONAL'"
            GredPBPAM3 = oCommon.getFieldValue(strSQL)

            strSQL = "UPDATE kpmkv_pelajar_markah SET GredV3='" & GredPBPAM3 & "' Where PelajarID='" & strPelajarID & "'"
            strRet = oCommon.ExecuteSQL(strSQL)

        End If
        '------------------------------------------------------------------------------------------------------------

        Dim PBPAM4 As String
        Dim GredPBPAM4 As String

        strSQL = "SELECT PBPAM4 FROM kpmkv_pelajar_markah"
        strSQL += " Where PelajarID='" & strPelajarID & "'"
        PBPAM4 = oCommon.getFieldValueInt(strSQL)

        If String.IsNullOrEmpty(PBPAM4) Then
            strSQL = "UPDATE kpmkv_pelajar_markah SET GredV4='' Where PelajarID='" & strPelajarID & "'"
            strRet = oCommon.ExecuteSQL(strSQL)
        ElseIf PBPAM4 = 0 Then
            strSQL = "UPDATE kpmkv_pelajar_markah SET GredV4='E' Where PelajarID='" & strPelajarID & "'"
            strRet = oCommon.ExecuteSQL(strSQL)
        ElseIf PBPAM4 = -1 Then
            strSQL = "UPDATE kpmkv_pelajar_markah SET GredV4='T' Where PelajarID='" & strPelajarID & "'"
            strRet = oCommon.ExecuteSQL(strSQL)
        Else
            strSQL = "SELECT TOP ( 1 ) Gred FROM  kpmkv_gred WHERE '" & PBPAM4 & "' BETWEEN MarkahFrom AND MarkahTo AND Jenis='VOKASIONAL'"
            GredPBPAM4 = oCommon.getFieldValue(strSQL)

            strSQL = "UPDATE kpmkv_pelajar_markah SET GredV4='" & GredPBPAM4 & "' Where PelajarID='" & strPelajarID & "'"
            strRet = oCommon.ExecuteSQL(strSQL)

        End If
        '-------------------------------------------------------------------------------------------------------------

        Dim PBPAM5 As String
        Dim GredPBPAM5 As String

        strSQL = "SELECT PBPAM5 FROM kpmkv_pelajar_markah"
        strSQL += " Where PelajarID='" & strPelajarID & "'"
        PBPAM5 = oCommon.getFieldValueInt(strSQL)

        If String.IsNullOrEmpty(PBPAM5) Then
            strSQL = "UPDATE kpmkv_pelajar_markah SET GredV5='' Where PelajarID='" & strPelajarID & "'"
            strRet = oCommon.ExecuteSQL(strSQL)
        ElseIf PBPAM5 = 0 Then
            strSQL = "UPDATE kpmkv_pelajar_markah SET GredV5='E' Where PelajarID='" & strPelajarID & "'"
            strRet = oCommon.ExecuteSQL(strSQL)
        ElseIf PBPAM5 = -1 Then
            strSQL = "UPDATE kpmkv_pelajar_markah SET GredV5='T' Where PelajarID='" & strPelajarID & "'"
            strRet = oCommon.ExecuteSQL(strSQL)
        Else
            strSQL = "SELECT TOP ( 1 ) Gred FROM  kpmkv_gred WHERE '" & PBPAM5 & "' BETWEEN MarkahFrom AND MarkahTo AND Jenis='VOKASIONAL'"
            GredPBPAM5 = oCommon.getFieldValue(strSQL)

            strSQL = "UPDATE kpmkv_pelajar_markah SET GredV5='" & GredPBPAM5 & "' Where PelajarID='" & strPelajarID & "'"
            strRet = oCommon.ExecuteSQL(strSQL)
        End If
        '-------------------------------------------------------------------------------------------------------------

        Dim PBPAM6 As String
        Dim GredPBPAM6 As String

        strSQL = "SELECT PBPAM6 FROM kpmkv_pelajar_markah"
        strSQL += " Where PelajarID='" & strPelajarID & "'"
        PBPAM6 = oCommon.getFieldValueInt(strSQL)

        If String.IsNullOrEmpty(PBPAM6) Then
            strSQL = "UPDATE kpmkv_pelajar_markah SET GredV6='' Where PelajarID='" & strPelajarID & "'"
            strRet = oCommon.ExecuteSQL(strSQL)
        ElseIf PBPAM6 = 0 Then
            strSQL = "UPDATE kpmkv_pelajar_markah SET GredV6='E' Where PelajarID='" & strPelajarID & "'"
            strRet = oCommon.ExecuteSQL(strSQL)
        ElseIf PBPAM6 = -1 Then
            strSQL = "UPDATE kpmkv_pelajar_markah SET GredV6='T' Where PelajarID='" & strPelajarID & "'"
            strRet = oCommon.ExecuteSQL(strSQL)
        Else
            strSQL = "SELECT TOP ( 1 ) Gred FROM  kpmkv_gred WHERE '" & PBPAM6 & "' BETWEEN MarkahFrom AND MarkahTo AND Jenis='VOKASIONAL'"
            GredPBPAM6 = oCommon.getFieldValue(strSQL)

            strSQL = "UPDATE kpmkv_pelajar_markah SET GredV6='" & GredPBPAM6 & "' Where PelajarID='" & strPelajarID & "'"
            strRet = oCommon.ExecuteSQL(strSQL)
        End If
        '----
    End Sub

    Private Sub Vokasional_SMP_PB_keseluruhan()

        Try

            Dim nCount As Integer = 0
            strSQL = "SELECT COUNT(kpmkv_modul.KodModul) as CModul "
            strSQL += " FROM kpmkv_modul LEFT OUTER JOIN"
            strSQL += " kpmkv_kursus ON kpmkv_modul.KursusID = kpmkv_kursus.KursusID"
            strSQL += " WHERE kpmkv_modul.KursusID='" & strKursusID & "'"
            strSQL += " AND  kpmkv_modul.Semester='" & strSemester & "'"
            strSQL += " AND  kpmkv_modul.Tahun='" & ddlTahun.Text & "'"
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

            strSQL = "SELECT SMP_PB FROM kpmkv_skor_svm"
            strSQL += " WHERE Tahun='" & ddlTahun.Text & "' AND Semester='" & strSemester & "'"
            Skor = oCommon.getFieldValue(strSQL)

            Select Case nCount
                Case "2"
                    strSQL = "SELECT PBAV1,PBAV2,PBTV1,PBTV2 FROM kpmkv_pelajar_markah"
                    strSQL += " Where PelajarID='" & strPelajarID & "'"
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
                        strSQL = "UPDATE kpmkv_pelajar_markah SET SMP_PB='0' Where PelajarID='" & strPelajarID & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)
                    Else
                        PB_Amali = CDbl(PBA1) + CDbl(PBA2)
                        PB_Teori = CDbl(PBT1) + CDbl(PBT2)

                        PB_Amali_K = (PB_Amali / 2)
                        PB_Teori_K = (PB_Teori / 2)

                        SMP_PB = (PB_Amali_K + PB_Teori_K)

                        If SMP_PB >= CDbl(Skor) Then
                            strSQL = "UPDATE kpmkv_pelajar_markah SET SMP_PB='1' Where PelajarID='" & strPelajarID & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)
                        Else
                            strSQL = "UPDATE kpmkv_pelajar_markah SET SMP_PB='0' Where PelajarID='" & strPelajarID & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)
                        End If
                    End If

                Case "3"
                    strSQL = "SELECT PBAV1,PBAV2,PBAV3,PBTV1,PBTV2,PBTV3 FROM kpmkv_pelajar_markah"
                    strSQL += " Where PelajarID='" & strPelajarID & "'"
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
                        strSQL = "UPDATE kpmkv_pelajar_markah SET SMP_PB='0' Where PelajarID='" & strPelajarID & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)
                    Else

                        PB_Amali = CDbl(PBA1) + CDbl(PBA2) + CDbl(PBA3)
                        PB_Teori = CDbl(PBT1) + CDbl(PBT2) + CDbl(PBT3)

                        PB_Amali_K = (PB_Amali / 3)
                        PB_Teori_K = (PB_Teori / 3)

                        SMP_PB = (PB_Amali_K + PB_Teori_K)

                        If SMP_PB >= CDbl(Skor) Then
                            strSQL = "UPDATE kpmkv_pelajar_markah SET SMP_PB='1' Where PelajarID='" & strPelajarID & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)
                        Else
                            strSQL = "UPDATE kpmkv_pelajar_markah SET SMP_PB='0' Where PelajarID='" & strPelajarID & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)
                        End If
                    End If
                Case "4"
                    strSQL = "SELECT PBAV1,PBAV2,PBAV3,PBAV4,PBTV1,PBTV2,PBTV3,PBTV4 FROM kpmkv_pelajar_markah"
                    strSQL += " Where PelajarID='" & strPelajarID & "'"
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
                        strSQL = "UPDATE kpmkv_pelajar_markah SET SMP_PB='0' Where PelajarID='" & strPelajarID & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)
                    Else

                        PB_Amali = CDbl(PBA1) + CDbl(PBA2) + CDbl(PBA3) + CDbl(PBA4)
                        PB_Teori = CDbl(PBT1) + CDbl(PBT2) + CDbl(PBT3) + CDbl(PBT4)

                        PB_Amali_K = (PB_Amali / 4)
                        PB_Teori_K = (PB_Teori / 4)

                        SMP_PB = (PB_Amali_K + PB_Teori_K)

                        If SMP_PB >= CDbl(Skor) Then
                            strSQL = "UPDATE kpmkv_pelajar_markah SET SMP_PB='1' Where PelajarID='" & strPelajarID & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)
                        Else
                            strSQL = "UPDATE kpmkv_pelajar_markah SET SMP_PB='0' Where PelajarID='" & strPelajarID & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)
                        End If

                    End If
                Case "5"
                    strSQL = "SELECT PBAV1,PBAV2,PBAV3,PBAV4,PBAV5,PBTV1,PBTV2,PBTV3,PBTV4,PBTV5 FROM kpmkv_pelajar_markah"
                    strSQL += " Where PelajarID='" & strPelajarID & "'"
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
                        strSQL = "UPDATE kpmkv_pelajar_markah SET SMP_PB='0' Where PelajarID='" & strPelajarID & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)
                    Else

                        PB_Amali = CDbl(PBA1) + CDbl(PBA2) + CDbl(PBA3) + CDbl(PBA4) + CDbl(PBA5)
                        PB_Teori = CDbl(PBT1) + CDbl(PBT2) + CDbl(PBT3) + CDbl(PBT4) + CDbl(PBT5)

                        PB_Amali_K = (PB_Amali / 5)
                        PB_Teori_K = (PB_Teori / 5)

                        SMP_PB = (PB_Amali_K + PB_Teori_K)

                        If SMP_PB >= CDbl(Skor) Then
                            strSQL = "UPDATE kpmkv_pelajar_markah SET SMP_PB='1' Where PelajarID='" & strPelajarID & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)
                        Else
                            strSQL = "UPDATE kpmkv_pelajar_markah SET SMP_PB='0' Where PelajarID='" & strPelajarID & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)
                        End If
                    End If
                Case "6"
                    strSQL = "SELECT PBAV1,PBAV2,PBAV3,PBAV4,PBAV5,PBAV6,PBTV1,PBTV2,PBTV3,PBTV4,PBTV5,PBTV6 FROM kpmkv_pelajar_markah"
                    strSQL += " Where PelajarID='" & strPelajarID & "'"
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
                        strSQL = "UPDATE kpmkv_pelajar_markah SET SMP_PB='0' Where PelajarID='" & strPelajarID & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)
                    Else

                        PB_Amali = CDbl(PBA1) + CDbl(PBA2) + CDbl(PBA3) + CDbl(PBA4) + CDbl(PBA5) + CDbl(PBA6)
                        PB_Teori = CDbl(PBT1) + CDbl(PBT2) + CDbl(PBT3) + CDbl(PBT4) + CDbl(PBT5) + CDbl(PBT6)

                        PB_Amali_K = (PB_Amali / 6)
                        PB_Teori_K = (PB_Teori / 6)

                        SMP_PB = (PB_Amali_K + PB_Teori_K)

                        If SMP_PB >= CDbl(Skor) Then
                            strSQL = "UPDATE kpmkv_pelajar_markah SET SMP_PB='1' Where PelajarID='" & strPelajarID & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)
                        Else
                            strSQL = "UPDATE kpmkv_pelajar_markah SET SMP_PB='0' Where PelajarID='" & strPelajarID & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)
                        End If
                    End If
                Case "7"
                    strSQL = "SELECT PBAV1,PBAV2,PBAV3,PBAV4,PBAV5,PBAV6,PBAV7,PBTV1,PBTV2,PBTV3,PBTV4,PBTV5,PBTV6,PBTV7 FROM kpmkv_pelajar_markah"
                    strSQL += " Where PelajarID='" & strPelajarID & "'"
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
                        strSQL = "UPDATE kpmkv_pelajar_markah SET SMP_PB='0' Where PelajarID='" & strPelajarID & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)
                    Else

                        PB_Amali = CDbl(PBA1) + CDbl(PBA2) + CDbl(PBA3) + CDbl(PBA4) + CDbl(PBA5) + CDbl(PBA6) + CDbl(PBA7)
                        PB_Teori = CDbl(PBT1) + CDbl(PBT2) + CDbl(PBT3) + CDbl(PBT4) + CDbl(PBT5) + CDbl(PBT6) + CDbl(PBT7)

                        PB_Amali_K = (PB_Amali / 7)
                        PB_Teori_K = (PB_Teori / 7)

                        SMP_PB = (PB_Amali_K + PB_Teori_K)

                        If SMP_PB >= CDbl(Skor) Then
                            strSQL = "UPDATE kpmkv_pelajar_markah SET SMP_PB='1' Where PelajarID='" & strPelajarID & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)
                        Else
                            strSQL = "UPDATE kpmkv_pelajar_markah SET SMP_PB='0' Where PelajarID='" & strPelajarID & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)
                        End If
                    End If

                Case "8"
                    strSQL = "SELECT PBAV1,PBAV2,PBAV3,PBAV4,PBAV5,PBAV6,PBAV7,PBAV8,PBTV1,PBTV2,PBTV3,PBTV4,PBTV5,PBTV6,PBTV7,PBTV8 FROM kpmkv_pelajar_markah"
                    strSQL += " Where PelajarID='" & strPelajarID & "'"
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
                        strSQL = "UPDATE kpmkv_pelajar_markah SET SMP_PB='0' Where PelajarID='" & strPelajarID & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)
                    Else

                        PB_Amali = CDbl(PBA1) + CDbl(PBA2) + CDbl(PBA3) + CDbl(PBA4) + CDbl(PBA5) + CDbl(PBA6) + CDbl(PBA7) + CDbl(PBA8)
                        PB_Teori = CDbl(PBT1) + CDbl(PBT2) + CDbl(PBT3) + CDbl(PBT4) + CDbl(PBT5) + CDbl(PBT6) + CDbl(PBT7) + CDbl(PBT8)

                        PB_Amali_K = (PB_Amali / 8)
                        PB_Teori_K = (PB_Teori / 8)

                        SMP_PB = (PB_Amali_K + PB_Teori_K)

                        If SMP_PB >= CDbl(Skor) Then
                            strSQL = "UPDATE kpmkv_pelajar_markah SET SMP_PB='1' Where PelajarID='" & strPelajarID & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)
                        Else
                            strSQL = "UPDATE kpmkv_pelajar_markah SET SMP_PB='0' Where PelajarID='" & strPelajarID & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)
                        End If
                    End If

            End Select

        Catch ex As Exception
            lblMsg.Text = "System Error.PB:" & ex.Message
        End Try

    End Sub

    Private Sub Vokasional_SMP_PA_keseluruhan()

        Try
            'get score SMP_PAA
            strSQL = "SELECT SMP_PAA FROM kpmkv_skor_svm"
            strSQL += " WHERE Tahun='" & ddlTahun.Text & "' AND Semester='" & strSemester & "'"
            Dim Skor As String = oCommon.getFieldValue(strSQL)

            'get score SMP_PAA
            strSQL = "SELECT SMP_PAT FROM kpmkv_skor_svm"
            strSQL += " WHERE Tahun='" & ddlTahun.Text & "' AND Semester='" & strSemester & "'"
            Dim SkorT As String = oCommon.getFieldValue(strSQL)

            'PA A
            Dim PAA As String
            'PA T
            Dim PAT As String

            strSQL = "SELECT PAAV1,PATV1 FROM kpmkv_pelajar_markah"
            strSQL += " Where PelajarID='" & strPelajarID & "'"
            strRet = oCommon.getFieldValueEx(strSQL)
            'Response.Write(strSQL)
            ''--get total
            Dim ar_total As Array
            ar_total = strRet.Split("|")
            PAA = ar_total(0)
            PAT = ar_total(1)

            'check pa
            If PAA = -1 Then
                strSQL = "UPDATE kpmkv_pelajar_markah SET SMP_PAA='0' Where PelajarID='" & strPelajarID & "'"
                strRet = oCommon.ExecuteSQL(strSQL)
            Else
                If Not PAA = "" Then
                    If CDbl(PAA) >= CDbl(Skor) Then
                        strSQL = "UPDATE kpmkv_pelajar_markah SET SMP_PAA='1' Where PelajarID='" & strPelajarID & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)
                    Else
                        strSQL = "UPDATE kpmkv_pelajar_markah SET SMP_PAA='0' Where PelajarID='" & strPelajarID & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)
                    End If
                End If

            End If

            If PAT = -1 Then
                strSQL = "UPDATE kpmkv_pelajar_markah SET SMP_PAT='0' Where PelajarID='" & strPelajarID & "'"
                strRet = oCommon.ExecuteSQL(strSQL)
            Else
                If Not PAT = "" Then
                    If CDbl(PAT) >= CDbl(SkorT) Then
                        strSQL = "UPDATE kpmkv_pelajar_markah SET SMP_PAT='1' Where PelajarID='" & strPelajarID & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)
                    Else
                        strSQL = "UPDATE kpmkv_pelajar_markah SET SMP_PAT='0' Where PelajarID='" & strPelajarID & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)
                    End If
                End If
            End If

        Catch ex As Exception
            lblMsg.Text = "System Error.PA:" & ex.Message
        End Try
        '    End If
        'Next
    End Sub

    Private Sub Vokasional_gredKompeten_keseluruhan()

        Try

            Dim nCount As Integer = 0
            strSQL = "SELECT COUNT(kpmkv_modul.KodModul) as CModul "
            strSQL += " FROM kpmkv_modul LEFT OUTER JOIN"
            strSQL += " kpmkv_kursus ON kpmkv_modul.KursusID = kpmkv_kursus.KursusID"
            strSQL += " WHERE kpmkv_modul.KursusID='" & strKursusID & "'"
            strSQL += " AND  kpmkv_modul.Semester='" & strSemester & "'"
            strSQL += " AND  kpmkv_modul.Tahun='" & ddlTahun.Text & "'"
            nCount = oCommon.getFieldValueInt(strSQL)

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
                    strSQL += " Where PelajarID='" & strPelajarID & "'"
                    strRet = oCommon.getFieldValueEx(strSQL)

                    Dim ar_total As Array
                    ar_total = strRet.Split("|")
                    strGredV1 = ar_total(0)
                    strGredV2 = ar_total(1)


                    If (strGredV1 = "C+" Or strGredV1 = "C" Or strGredV1 = "C-" Or strGredV1 = "D+" Or strGredV1 = "D" Or strGredV1 = "D-" Or strGredV1 = "E" Or strGredV1 = "G" Or strGredV1 = "T") Then
                        strSQL = "UPDATE kpmkv_pelajar_markah SET Gred_Kompeten='0' Where PelajarID='" & strPelajarID & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)

                    ElseIf (strGredV2 = "C+" Or strGredV2 = "C" Or strGredV2 = "C-" Or strGredV2 = "D+" Or strGredV2 = "D" Or strGredV2 = "D-" Or strGredV2 = "E" Or strGredV2 = "G" Or strGredV2 = "T") Then
                        strSQL = "UPDATE kpmkv_pelajar_markah SET Gred_Kompeten='0' Where PelajarID='" & strPelajarID & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)
                    Else

                        strSQL = "UPDATE kpmkv_pelajar_markah SET Gred_Kompeten='1' Where PelajarID='" & strPelajarID & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)
                    End If

                Case "3"

                    strSQL = "SELECT GredV1,GredV2,GredV3 FROM kpmkv_pelajar_markah"
                    strSQL += " Where PelajarID='" & strPelajarID & "'"
                    strRet = oCommon.getFieldValueEx(strSQL)

                    Dim ar_total As Array
                    ar_total = strRet.Split("|")
                    strGredV1 = ar_total(0)
                    strGredV2 = ar_total(1)
                    strGredV3 = ar_total(2)

                    If (strGredV1 = "C+" Or strGredV1 = "C" Or strGredV1 = "C-" Or strGredV1 = "D+" Or strGredV1 = "D" Or strGredV1 = "D-" Or strGredV1 = "E" Or strGredV1 = "G" Or strGredV1 = "T") Then
                        strSQL = "UPDATE kpmkv_pelajar_markah SET Gred_Kompeten='0' Where PelajarID='" & strPelajarID & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)

                    ElseIf (strGredV2 = "C+" Or strGredV2 = "C" Or strGredV2 = "C-" Or strGredV2 = "D+" Or strGredV2 = "D" Or strGredV2 = "D-" Or strGredV2 = "E" Or strGredV2 = "G" Or strGredV2 = "T") Then
                        strSQL = "UPDATE kpmkv_pelajar_markah SET Gred_Kompeten='0' Where PelajarID='" & strPelajarID & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)

                    ElseIf (strGredV3 = "C+" Or strGredV3 = "C" Or strGredV3 = "C-" Or strGredV3 = "D+" Or strGredV3 = "D" Or strGredV3 = "D-" Or strGredV3 = "E" Or strGredV3 = "G" Or strGredV3 = "T") Then
                        strSQL = "UPDATE kpmkv_pelajar_markah SET Gred_Kompeten='0' Where PelajarID='" & strPelajarID & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)
                    Else

                        strSQL = "UPDATE kpmkv_pelajar_markah SET Gred_Kompeten='1' Where PelajarID='" & strPelajarID & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)
                    End If

                Case "4"

                    strSQL = "SELECT GredV1,GredV2,GredV3,GredV4 FROM kpmkv_pelajar_markah"
                    strSQL += " Where PelajarID='" & strPelajarID & "'"
                    strRet = oCommon.getFieldValueEx(strSQL)

                    Dim ar_total As Array
                    ar_total = strRet.Split("|")
                    strGredV1 = ar_total(0)
                    strGredV2 = ar_total(1)
                    strGredV3 = ar_total(2)
                    strGredV4 = ar_total(3)


                    If (strGredV1 = "C+" Or strGredV1 = "C" Or strGredV1 = "C-" Or strGredV1 = "D+" Or strGredV1 = "D" Or strGredV1 = "D-" Or strGredV1 = "E" Or strGredV1 = "G" Or strGredV1 = "T") Then
                        strSQL = "UPDATE kpmkv_pelajar_markah SET Gred_Kompeten='0' Where PelajarID='" & strPelajarID & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)

                    ElseIf (strGredV2 = "C+" Or strGredV2 = "C" Or strGredV2 = "C-" Or strGredV2 = "D+" Or strGredV2 = "D" Or strGredV2 = "D-" Or strGredV2 = "E" Or strGredV2 = "G" Or strGredV2 = "T") Then
                        strSQL = "UPDATE kpmkv_pelajar_markah SET Gred_Kompeten='0' Where PelajarID='" & strPelajarID & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)

                    ElseIf (strGredV3 = "C+" Or strGredV3 = "C" Or strGredV3 = "C-" Or strGredV3 = "D+" Or strGredV3 = "D" Or strGredV3 = "D-" Or strGredV3 = "E" Or strGredV3 = "G" Or strGredV3 = "T") Then
                        strSQL = "UPDATE kpmkv_pelajar_markah SET Gred_Kompeten='0' Where PelajarID='" & strPelajarID & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)

                    ElseIf (strGredV4 = "C+" Or strGredV4 = "C" Or strGredV4 = "C-" Or strGredV4 = "D+" Or strGredV4 = "D" Or strGredV4 = "D-" Or strGredV4 = "E" Or strGredV4 = "G" Or strGredV4 = "T") Then
                        strSQL = "UPDATE kpmkv_pelajar_markah SET Gred_Kompeten='0' Where PelajarID='" & strPelajarID & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)
                    Else

                        strSQL = "UPDATE kpmkv_pelajar_markah SET Gred_Kompeten='1' Where PelajarID='" & strPelajarID & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)
                    End If


                Case "5"
                    strSQL = "SELECT GredV1,GredV2,GredV3,GredV4,GredV5 FROM kpmkv_pelajar_markah"
                    strSQL += " Where PelajarID='" & strPelajarID & "'"
                    strRet = oCommon.getFieldValueEx(strSQL)

                    Dim ar_total As Array
                    ar_total = strRet.Split("|")
                    strGredV1 = ar_total(0)
                    strGredV2 = ar_total(1)
                    strGredV3 = ar_total(2)
                    strGredV4 = ar_total(3)
                    strGredV5 = ar_total(4)


                    If (strGredV1 = "C+" Or strGredV1 = "C" Or strGredV1 = "C-" Or strGredV1 = "D+" Or strGredV1 = "D" Or strGredV1 = "D-" Or strGredV1 = "E" Or strGredV1 = "G" Or strGredV1 = "T") Then
                        strSQL = "UPDATE kpmkv_pelajar_markah SET Gred_Kompeten='0' Where PelajarID='" & strPelajarID & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)

                    ElseIf (strGredV2 = "C+" Or strGredV2 = "C" Or strGredV2 = "C-" Or strGredV2 = "D+" Or strGredV2 = "D" Or strGredV2 = "D-" Or strGredV2 = "E" Or strGredV2 = "G" Or strGredV2 = "T") Then
                        strSQL = "UPDATE kpmkv_pelajar_markah SET Gred_Kompeten='0' Where PelajarID='" & strPelajarID & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)

                    ElseIf (strGredV3 = "C+" Or strGredV3 = "C" Or strGredV3 = "C-" Or strGredV3 = "D+" Or strGredV3 = "D" Or strGredV3 = "D-" Or strGredV3 = "E" Or strGredV3 = "G" Or strGredV3 = "T") Then
                        strSQL = "UPDATE kpmkv_pelajar_markah SET Gred_Kompeten='0' Where PelajarID='" & strPelajarID & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)

                    ElseIf (strGredV4 = "C+" Or strGredV4 = "C" Or strGredV4 = "C-" Or strGredV4 = "D+" Or strGredV4 = "D" Or strGredV4 = "D-" Or strGredV4 = "E" Or strGredV4 = "G" Or strGredV4 = "T") Then
                        strSQL = "UPDATE kpmkv_pelajar_markah SET Gred_Kompeten='0' Where PelajarID='" & strPelajarID & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)

                    ElseIf (strGredV5 = "C+" Or strGredV5 = "C" Or strGredV5 = "C-" Or strGredV5 = "D+" Or strGredV5 = "D" Or strGredV5 = "D-" Or strGredV5 = "E" Or strGredV5 = "G" Or strGredV5 = "T") Then
                        strSQL = "UPDATE kpmkv_pelajar_markah SET Gred_Kompeten='0' Where PelajarID='" & strPelajarID & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)

                    Else
                        strSQL = "UPDATE kpmkv_pelajar_markah SET Gred_Kompeten='1' Where PelajarID='" & strPelajarID & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)
                    End If


                Case "6"

                    strSQL = "SELECT GredV1,GredV2,GredV3,GredV4,GredV5,GredV6 FROM kpmkv_pelajar_markah"
                    strSQL += " Where PelajarID='" & strPelajarID & "'"
                    strRet = oCommon.getFieldValueEx(strSQL)

                    Dim ar_total As Array
                    ar_total = strRet.Split("|")
                    strGredV1 = ar_total(0)
                    strGredV2 = ar_total(1)
                    strGredV3 = ar_total(2)
                    strGredV4 = ar_total(3)
                    strGredV5 = ar_total(4)
                    strGredV6 = ar_total(5)


                    If (strGredV1 = "C+" Or strGredV1 = "C" Or strGredV1 = "C-" Or strGredV1 = "D+" Or strGredV1 = "D" Or strGredV1 = "D-" Or strGredV1 = "E" Or strGredV1 = "G" Or strGredV1 = "T") Then
                        strSQL = "UPDATE kpmkv_pelajar_markah SET Gred_Kompeten='0' Where PelajarID='" & strPelajarID & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)

                    ElseIf (strGredV2 = "C+" Or strGredV2 = "C" Or strGredV2 = "C-" Or strGredV2 = "D+" Or strGredV2 = "D" Or strGredV2 = "D-" Or strGredV2 = "E" Or strGredV2 = "G" Or strGredV2 = "T") Then
                        strSQL = "UPDATE kpmkv_pelajar_markah SET Gred_Kompeten='0' Where PelajarID='" & strPelajarID & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)

                    ElseIf (strGredV3 = "C+" Or strGredV3 = "C" Or strGredV3 = "C-" Or strGredV3 = "D+" Or strGredV3 = "D" Or strGredV3 = "D-" Or strGredV3 = "E" Or strGredV3 = "G" Or strGredV3 = "T") Then
                        strSQL = "UPDATE kpmkv_pelajar_markah SET Gred_Kompeten='0' Where PelajarID='" & strPelajarID & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)

                    ElseIf (strGredV4 = "C+" Or strGredV4 = "C" Or strGredV4 = "C-" Or strGredV4 = "D+" Or strGredV4 = "D" Or strGredV4 = "D-" Or strGredV4 = "E" Or strGredV4 = "G" Or strGredV4 = "T") Then
                        strSQL = "UPDATE kpmkv_pelajar_markah SET Gred_Kompeten='0' Where PelajarID='" & strPelajarID & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)

                    ElseIf (strGredV5 = "C+" Or strGredV5 = "C" Or strGredV5 = "C-" Or strGredV5 = "D+" Or strGredV5 = "D" Or strGredV5 = "D-" Or strGredV5 = "E" Or strGredV5 = "G" Or strGredV5 = "T") Then
                        strSQL = "UPDATE kpmkv_pelajar_markah SET Gred_Kompeten='0' Where PelajarID='" & strPelajarID & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)

                    ElseIf (strGredV6 = "C+" Or strGredV6 = "C" Or strGredV6 = "C-" Or strGredV6 = "D+" Or strGredV6 = "D" Or strGredV6 = "D-" Or strGredV6 = "E" Or strGredV6 = "G" Or strGredV6 = "T") Then
                        strSQL = "UPDATE kpmkv_pelajar_markah SET Gred_Kompeten='0' Where PelajarID='" & strPelajarID & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)
                    Else
                        strSQL = "UPDATE kpmkv_pelajar_markah SET Gred_Kompeten='1' Where PelajarID='" & strPelajarID & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)
                    End If

                Case "7"

                    strSQL = "SELECT GredV1,GredV2,GredV3,GredV4,GredV5,GredV6,GredV7 FROM kpmkv_pelajar_markah"
                    strSQL += " Where PelajarID='" & strPelajarID & "'"
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

                    If (strGredV1 = "C+" Or strGredV1 = "C" Or strGredV1 = "C-" Or strGredV1 = "D+" Or strGredV1 = "D" Or strGredV1 = "D-" Or strGredV1 = "E" Or strGredV1 = "G" Or strGredV1 = "T") Then
                        strSQL = "UPDATE kpmkv_pelajar_markah SET Gred_Kompeten='0' Where PelajarID='" & strPelajarID & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)

                    ElseIf (strGredV2 = "C+" Or strGredV2 = "C" Or strGredV2 = "C-" Or strGredV2 = "D+" Or strGredV2 = "D" Or strGredV2 = "D-" Or strGredV2 = "E" Or strGredV2 = "G" Or strGredV2 = "T") Then
                        strSQL = "UPDATE kpmkv_pelajar_markah SET Gred_Kompeten='0' Where PelajarID='" & strPelajarID & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)

                    ElseIf (strGredV3 = "C+" Or strGredV3 = "C" Or strGredV3 = "C-" Or strGredV3 = "D+" Or strGredV3 = "D" Or strGredV3 = "D-" Or strGredV3 = "E" Or strGredV3 = "G" Or strGredV3 = "T") Then
                        strSQL = "UPDATE kpmkv_pelajar_markah SET Gred_Kompeten='0' Where PelajarID='" & strPelajarID & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)

                    ElseIf (strGredV4 = "C+" Or strGredV4 = "C" Or strGredV4 = "C-" Or strGredV4 = "D+" Or strGredV4 = "D" Or strGredV4 = "D-" Or strGredV4 = "E" Or strGredV4 = "G" Or strGredV4 = "T") Then
                        strSQL = "UPDATE kpmkv_pelajar_markah SET Gred_Kompeten='0' Where PelajarID='" & strPelajarID & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)

                    ElseIf (strGredV5 = "C+" Or strGredV5 = "C" Or strGredV5 = "C-" Or strGredV5 = "D+" Or strGredV5 = "D" Or strGredV5 = "D-" Or strGredV5 = "E" Or strGredV5 = "G" Or strGredV5 = "T") Then
                        strSQL = "UPDATE kpmkv_pelajar_markah SET Gred_Kompeten='0' Where PelajarID='" & strPelajarID & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)

                    ElseIf (strGredV6 = "C+" Or strGredV6 = "C" Or strGredV6 = "C-" Or strGredV6 = "D+" Or strGredV6 = "D" Or strGredV6 = "D-" Or strGredV6 = "E" Or strGredV6 = "G" Or strGredV6 = "T") Then
                        strSQL = "UPDATE kpmkv_pelajar_markah SET Gred_Kompeten='0' Where PelajarID='" & strPelajarID & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)

                    ElseIf (strGredV7 = "C+" Or strGredV7 = "C" Or strGredV7 = "C-" Or strGredV7 = "D+" Or strGredV7 = "D" Or strGredV7 = "D-" Or strGredV7 = "E" Or strGredV7 = "G" Or strGredV7 = "T") Then
                        strSQL = "UPDATE kpmkv_pelajar_markah SET Gred_Kompeten='0' Where PelajarID='" & strPelajarID & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)
                    Else
                        strSQL = "UPDATE kpmkv_pelajar_markah SET Gred_Kompeten='1' Where PelajarID='" & strPelajarID & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)
                    End If
                Case "8"
                    strSQL = "SELECT GredV1,GredV2,GredV3,GredV4,GredV5,GredV6,GredV7,GredV8 FROM kpmkv_pelajar_markah"
                    strSQL += " Where PelajarID='" & strPelajarID & "'"
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

                    If (strGredV1 = "C+" Or strGredV1 = "C" Or strGredV1 = "C-" Or strGredV1 = "D+" Or strGredV1 = "D" Or strGredV1 = "D-" Or strGredV1 = "E" Or strGredV1 = "G" Or strGredV1 = "T") Then
                        strSQL = "UPDATE kpmkv_pelajar_markah SET Gred_Kompeten='0' Where PelajarID='" & strPelajarID & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)

                    ElseIf (strGredV2 = "C+" Or strGredV2 = "C" Or strGredV2 = "C-" Or strGredV2 = "D+" Or strGredV2 = "D" Or strGredV2 = "D-" Or strGredV2 = "E" Or strGredV2 = "G" Or strGredV2 = "T") Then
                        strSQL = "UPDATE kpmkv_pelajar_markah SET Gred_Kompeten='0' Where PelajarID='" & strPelajarID & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)

                    ElseIf (strGredV3 = "C+" Or strGredV3 = "C" Or strGredV3 = "C-" Or strGredV3 = "D+" Or strGredV3 = "D" Or strGredV3 = "D-" Or strGredV3 = "E" Or strGredV3 = "G" Or strGredV3 = "T") Then
                        strSQL = "UPDATE kpmkv_pelajar_markah SET Gred_Kompeten='0' Where PelajarID='" & strPelajarID & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)

                    ElseIf (strGredV4 = "C+" Or strGredV4 = "C" Or strGredV4 = "C-" Or strGredV4 = "D+" Or strGredV4 = "D" Or strGredV4 = "D-" Or strGredV4 = "E" Or strGredV4 = "G" Or strGredV4 = "T") Then
                        strSQL = "UPDATE kpmkv_pelajar_markah SET Gred_Kompeten='0' Where PelajarID='" & strPelajarID & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)

                    ElseIf (strGredV5 = "C+" Or strGredV5 = "C" Or strGredV5 = "C-" Or strGredV5 = "D+" Or strGredV5 = "D" Or strGredV5 = "D-" Or strGredV5 = "E" Or strGredV5 = "G" Or strGredV5 = "T") Then
                        strSQL = "UPDATE kpmkv_pelajar_markah SET Gred_Kompeten='0' Where PelajarID='" & strPelajarID & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)

                    ElseIf (strGredV6 = "C+" Or strGredV6 = "C" Or strGredV6 = "C-" Or strGredV6 = "D+" Or strGredV6 = "D" Or strGredV6 = "D-" Or strGredV6 = "E" Or strGredV6 = "G" Or strGredV6 = "T") Then
                        strSQL = "UPDATE kpmkv_pelajar_markah SET Gred_Kompeten='0' Where PelajarID='" & strPelajarID & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)

                    ElseIf (strGredV7 = "C+" Or strGredV7 = "C" Or strGredV7 = "C-" Or strGredV7 = "D+" Or strGredV7 = "D" Or strGredV7 = "D-" Or strGredV7 = "E" Or strGredV7 = "G" Or strGredV7 = "T") Then
                        strSQL = "UPDATE kpmkv_pelajar_markah SET Gred_Kompeten='0' Where PelajarID='" & strPelajarID & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)

                    ElseIf (strGredV8 = "C+" Or strGredV8 = "C" Or strGredV8 = "C-" Or strGredV8 = "D+" Or strGredV8 = "D" Or strGredV8 = "D-" Or strGredV8 = "E" Or strGredV8 = "G" Or strGredV8 = "T") Then
                        strSQL = "UPDATE kpmkv_pelajar_markah SET Gred_Kompeten='0' Where PelajarID='" & strPelajarID & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)

                    Else

                        strSQL = "UPDATE kpmkv_pelajar_markah SET Gred_Kompeten='1' Where PelajarID='" & strPelajarID & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)
                    End If

            End Select

        Catch ex As Exception
            lblMsg.Text = "System Error.Gred:" & ex.Message
        End Try

    End Sub

    Private Sub Vokasional_gredSMP_keseluruhan()

        Try

            '--count no of modul
            Dim nCount As Integer = 0
            strSQL = "SELECT COUNT(kpmkv_modul.KodModul) as CModul "
            strSQL += " FROM kpmkv_modul LEFT OUTER JOIN"
            strSQL += " kpmkv_kursus ON kpmkv_modul.KursusID = kpmkv_kursus.KursusID"
            strSQL += " WHERE kpmkv_modul.KursusID='" & strKursusID & "'"
            strSQL += " AND  kpmkv_modul.Semester='" & strSemester & "'"
            strSQL += " AND  kpmkv_modul.Tahun='" & ddlTahun.Text & "'"
            nCount = oCommon.getFieldValueInt(strSQL)

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


            Dim SMP_PB As Double
            Dim SMP_PA As Double

            Dim SMP_Total As Double

            Select Case nCount
                Case "2"
                    strSQL = "SELECT PBAV1,PBTV1,PAAV1,PATV1,PBAV2,PBTV2,PAAV2,PATV2 FROM kpmkv_pelajar_markah"
                    strSQL += " Where PelajarID='" & strPelajarID & "'"
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
                        strSQL = "UPDATE kpmkv_pelajar_markah SET SMP_Total='-1' Where PelajarID='" & strPelajarID & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)
                    Else
                        SMP_PA = (Convert.ToDouble(PAAV1) + CDbl(PATV1) + CDbl(PAAV2) + CDbl(PATV2)) / 2
                        SMP_PB = (CDbl(PBAV1) + CDbl(PBTV1) + CDbl(PBAV2) + CDbl(PBTV2)) / 2
                        SMP_Total = Math.Ceiling(SMP_PB + SMP_PA)

                        strSQL = "UPDATE kpmkv_pelajar_markah SET SMP_Total='" & SMP_Total & "' Where PelajarID='" & strPelajarID & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)
                    End If

                Case "3"
                    strSQL = "SELECT PBAV1,PBTV1,PAAV1,PATV1,PBAV2,PBTV2,PAAV2,PATV2,PBAV3,PBTV3,PAAV3,PATV3 FROM kpmkv_pelajar_markah"
                    strSQL += " Where PelajarID='" & strPelajarID & "'"
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
                        strSQL = "UPDATE kpmkv_pelajar_markah SET SMP_Total='-1' Where PelajarID='" & strPelajarID & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)
                    Else

                        SMP_PA = (CDbl(PAAV1) + CDbl(PATV1) + CDbl(PAAV2) + CDbl(PATV2) + CDbl(PAAV3) + CDbl(PATV3)) / 3
                        SMP_PB = (CDbl(PBAV1) + CDbl(PBTV1) + CDbl(PBAV2) + CDbl(PBTV2) + CDbl(PBAV3) + CDbl(PBTV3)) / 3
                        SMP_Total = Math.Ceiling(SMP_PB + SMP_PA)

                        strSQL = "UPDATE kpmkv_pelajar_markah SET SMP_Total='" & SMP_Total & "' Where PelajarID='" & strPelajarID & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)
                    End If

                Case "4"
                    strSQL = "SELECT PBAV1,PBTV1,PAAV1,PATV1,PBAV2,PBTV2,PAAV2,PATV2,PBAV3,PBTV3,PAAV3,PATV3,PBAV4,PBTV4,PAAV4,PATV4 FROM kpmkv_pelajar_markah"
                    strSQL += " Where PelajarID='" & strPelajarID & "'"
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
                        strSQL = "UPDATE kpmkv_pelajar_markah SET SMP_Total='-1' Where PelajarID='" & strPelajarID & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)
                    Else


                        SMP_PA = (Convert.ToDouble(PAAV1) + Convert.ToDouble(PATV1) + Convert.ToDouble(PAAV2) + Convert.ToDouble(PATV2) + Convert.ToDouble(PAAV3) + Convert.ToDouble(PATV3) + Convert.ToDouble(PAAV4) + Convert.ToDouble(PATV4)) / 4
                        SMP_PB = (Convert.ToDouble(PBAV1) + Convert.ToDouble(PBTV1) + Convert.ToDouble(PBAV2) + Convert.ToDouble(PBTV2) + Convert.ToDouble(PBAV3) + Convert.ToDouble(PBTV3) + Convert.ToDouble(PBAV4) + Convert.ToDouble(PBTV4)) / 4
                        SMP_Total = Math.Ceiling(SMP_PB + SMP_PA)

                        strSQL = "UPDATE kpmkv_pelajar_markah SET SMP_Total='" & SMP_Total & "' Where PelajarID='" & strPelajarID & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)
                    End If

                Case "5"

                    strSQL = "SELECT PBAV1,PBTV1,PAAV1,PATV1,PBAV2,PBTV2,PAAV2,PATV2,PBAV3,PBTV3,PAAV3,PATV3,PBAV4,PBTV4,PAAV4,PATV4,PBAV5,PBTV5,PAAV5,PATV5 FROM kpmkv_pelajar_markah"
                    strSQL += " Where PelajarID='" & strPelajarID & "'"
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
                        strSQL = "UPDATE kpmkv_pelajar_markah SET SMP_Total='-1' Where PelajarID='" & strPelajarID & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)
                    Else

                        SMP_PA = (CDbl(PAAV1) + CDbl(PATV1) + CDbl(PAAV2) + CDbl(PATV2) + CDbl(PAAV3) + CDbl(PATV3) + CDbl(PAAV4) + CDbl(PATV4) + CDbl(PAAV5) + CDbl(PATV5)) / 5
                        SMP_PB = (CDbl(PBAV1) + CDbl(PBTV1) + CDbl(PBAV2) + CDbl(PBTV2) + CDbl(PBAV3) + CDbl(PBTV3) + CDbl(PBAV4) + CDbl(PBTV4) + CDbl(PBAV5) + CDbl(PBTV5) / 5)
                        SMP_Total = Math.Ceiling(SMP_PB + SMP_PA)

                        strSQL = "UPDATE kpmkv_pelajar_markah SET SMP_Total='" & SMP_Total & "' Where PelajarID='" & strPelajarID & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)
                    End If

                Case "6"
                    strSQL = "SELECT PBAV1,PBTV1,PAAV1,PATV1,PBAV2,PBTV2,PAAV2,PATV2,PBAV3,PBTV3,PAAV3,PATV3,PBAV4,PBTV4,PAAV4,PATV4,PBAV5,PBTV5,PAAV5,PATV5,PBAV6,PBTV6,PAAV6,PATV6 FROM kpmkv_pelajar_markah"
                    strSQL += " Where PelajarID='" & strPelajarID & "'"
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
                        strSQL = "UPDATE kpmkv_pelajar_markah SET SMP_Total='-1' Where PelajarID='" & strPelajarID & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)
                    Else
                        SMP_PA = (CDbl(PAAV1) + CDbl(PATV1) + CDbl(PAAV2) + CDbl(PATV2) + CDbl(PAAV3) + CDbl(PATV3) + CDbl(PAAV4) + CDbl(PATV4) + CDbl(PAAV5) + CDbl(PATV5) + CDbl(PAAV6) + CDbl(PATV6)) / 6
                        SMP_PB = (CDbl(PBAV1) + CDbl(PBTV1) + CDbl(PBAV2) + CDbl(PBTV2) + CDbl(PBAV3) + CDbl(PBTV3) + CDbl(PBAV4) + CDbl(PBTV4) + CDbl(PBAV5) + CDbl(PBTV5) + CDbl(PBAV6) + CDbl(PBTV6)) / 6
                        SMP_Total = Math.Ceiling(SMP_PB + SMP_PA)

                        strSQL = "UPDATE kpmkv_pelajar_markah SET SMP_Total='" & SMP_Total & "' Where PelajarID='" & strPelajarID & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)
                    End If

                Case "7"
                    strSQL = "SELECT PBAV1,PBTV1,PAAV1,PATV1,PBAV2,PBTV2,PAAV2,PATV2,PBAV3,PBTV3,PAAV3,PATV3,PBAV4,PBTV4,PAAV4,PATV4,PBAV5,PBTV5,PAAV5,PATV5,PBAV6,PBTV6,PAAV6,PATV6,PBAV7,PBTV7,PAAV7,PATV7 FROM kpmkv_pelajar_markah"
                    strSQL += " Where PelajarID='" & strPelajarID & "'"
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
                        strSQL = "UPDATE kpmkv_pelajar_markah SET SMP_Total='-1' Where PelajarID='" & strPelajarID & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)
                    Else


                        SMP_PA = (CDbl(PAAV1) + CDbl(PATV1) + CDbl(PAAV2) + CDbl(PATV2) + CDbl(PAAV3) + CDbl(PATV3) + CDbl(PAAV4) + CDbl(PATV4) + CDbl(PAAV5) + CDbl(PATV5) + CDbl(PAAV6) + CDbl(PATV6) + CDbl(PAAV7) + CDbl(PATV7)) / 7
                        SMP_PB = (CDbl(PBAV1) + CDbl(PBTV1) + CDbl(PBAV2) + CDbl(PBTV2) + CDbl(PBAV3) + CDbl(PBTV3) + CDbl(PBAV4) + CDbl(PBTV4) + CDbl(PBAV5) + CDbl(PBTV5) + CDbl(PBAV6) + CDbl(PBTV6) + CDbl(PBAV7) + CDbl(PBTV7)) / 7
                        SMP_Total = Math.Ceiling(SMP_PB + SMP_PA)

                        strSQL = "UPDATE kpmkv_pelajar_markah SET SMP_Total='" & SMP_Total & "' Where PelajarID='" & strPelajarID & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)
                    End If

            End Select

        Catch ex As Exception
            lblMsg.Text = "System Error.GredSMP:" & ex.Message
        End Try
        '    End If
        'Next
    End Sub

    Private Sub btnJanaKeseluruhanPeringkat1_Click(sender As Object, e As EventArgs) Handles btnJanaKeseluruhanPeringkat1.Click

        Try

            lblMsg.Text = ""

            strSQL = "SELECT RecordID FROM kpmkv_kolej"

            strRet = oCommon.ExecuteSQL(strSQL)

            Dim sqlDA As New SqlDataAdapter(strSQL, objConn)
            Dim dsRecordID As DataSet = New DataSet
            sqlDA.Fill(dsRecordID, "AnyTable")

            For a As Integer = 0 To dsRecordID.Tables(0).Rows.Count - 1

                Dim strRecordID As String = dsRecordID.Tables(0).Rows(a).Item(0).ToString

                strSQL = "  SELECT kpmkv_pelajar.PelajarID, kpmkv_pelajar.KursusID, kpmkv_pelajar.Semester"
                strSQL += " FROM kpmkv_pelajar_markah "
                strSQL += " LEFT OUTER JOIN kpmkv_pelajar On kpmkv_pelajar_markah.PelajarID = kpmkv_pelajar.PelajarID"
                strSQL += " LEFT OUTER Join kpmkv_kursus On kpmkv_pelajar.KursusID = kpmkv_kursus.KursusID"
                strSQL += " WHERE kpmkv_pelajar.KolejRecordID='" & strRecordID & "' "
                strSQL += " AND kpmkv_pelajar.IsDeleted='N' AND kpmkv_pelajar.StatusID='2' AND kpmkv_pelajar.JenisCalonID='2'"
                strSQL += " AND kpmkv_pelajar.Tahun ='" & ddlTahun.Text & "'"

                If Not ddlSemester.Text = "-Pilih-" Then

                    strSQL += " AND kpmkv_pelajar.Semester ='" & ddlSemester.Text & "'"

                End If

                strRet = oCommon.ExecuteSQL(strSQL)

                sqlDA = New SqlDataAdapter(strSQL, objConn)
                Dim dsPelajar As DataSet = New DataSet
                sqlDA.Fill(dsPelajar, "AnyTable")

                For b As Integer = 0 To dsPelajar.Tables(0).Rows.Count - 1

                    strPelajarID = dsPelajar.Tables(0).Rows(b).Item(0).ToString
                    strKursusID = dsPelajar.Tables(0).Rows(b).Item(1).ToString
                    strSemester = dsPelajar.Tables(0).Rows(b).Item(2).ToString

                    Vokasional_markah_keseluruhan()

                Next

            Next

            If Not strRet = "0" Then
                divMsg.Attributes("class") = "error"
                lblJanaKeseluruhanPeringkat1.Text = "Tidak Berjaya mengemaskini markah keseluruhan Pentaksiran Akhir Vokasional"
            Else
                divMsg.Attributes("class") = "info"
                lblJanaKeseluruhanPeringkat1.Text = "Berjaya mengemaskini markah keseluruhan Pentaksiran Akhir Vokasional"
            End If



        Catch ex As Exception

            divMsg.Attributes("class") = "error"
            lblJanaKeseluruhanPeringkat1.Text = "Tidak Berjaya mengemaskini markah keseluruhan Pentaksiran Akhir Vokasional. " & ex.Message & " PelajarID : " & strPelajarID

        End Try

    End Sub

    Private Sub btnJanaKeseluruhanPeringkat2_Click(sender As Object, e As EventArgs) Handles btnJanaKeseluruhanPeringkat2.Click

        Try

            lblMsg.Text = ""

            strSQL = "SELECT RecordID FROM kpmkv_kolej"

            strRet = oCommon.ExecuteSQL(strSQL)

            Dim sqlDA As New SqlDataAdapter(strSQL, objConn)
            Dim dsRecordID As DataSet = New DataSet
            sqlDA.Fill(dsRecordID, "AnyTable")

            For a As Integer = 0 To dsRecordID.Tables(0).Rows.Count - 1

                Dim strRecordID As String = dsRecordID.Tables(0).Rows(a).Item(0).ToString

                strSQL = "  SELECT kpmkv_pelajar.PelajarID, kpmkv_pelajar.KursusID, kpmkv_pelajar.Semester"
                strSQL += " FROM kpmkv_pelajar_markah "
                strSQL += " LEFT OUTER JOIN kpmkv_pelajar On kpmkv_pelajar_markah.PelajarID = kpmkv_pelajar.PelajarID"
                strSQL += " LEFT OUTER Join kpmkv_kursus On kpmkv_pelajar.KursusID = kpmkv_kursus.KursusID"
                strSQL += " WHERE kpmkv_pelajar.KolejRecordID='" & strRecordID & "' "
                strSQL += " AND kpmkv_pelajar.IsDeleted='N' AND kpmkv_pelajar.StatusID='2' AND kpmkv_pelajar.JenisCalonID='2'"
                strSQL += " AND kpmkv_pelajar.Tahun ='" & ddlTahun.Text & "'"

                If Not ddlSemester.Text = "-Pilih-" Then

                    strSQL += " AND kpmkv_pelajar.Semester ='" & ddlSemester.Text & "'"

                End If

                strRet = oCommon.ExecuteSQL(strSQL)

                sqlDA = New SqlDataAdapter(strSQL, objConn)
                Dim dsPelajar As DataSet = New DataSet
                sqlDA.Fill(dsPelajar, "AnyTable")

                For b As Integer = 0 To dsPelajar.Tables(0).Rows.Count - 1

                    strPelajarID = dsPelajar.Tables(0).Rows(b).Item(0).ToString
                    strKursusID = dsPelajar.Tables(0).Rows(b).Item(1).ToString
                    strSemester = dsPelajar.Tables(0).Rows(b).Item(2).ToString

                    Vokasional_gred_keseluruhan()

                Next

            Next

            If Not strRet = "0" Then
                divMsg.Attributes("class") = "error"
                lblJanaKeseluruhanPeringkat2.Text = "Tidak Berjaya mengemaskini gred keseluruhan Pentaksiran Akhir Vokasional"
            Else
                divMsg.Attributes("class") = "info"
                lblJanaKeseluruhanPeringkat2.Text = "Berjaya mengemaskini gred keseluruhan Pentaksiran Akhir Vokasional"
            End If



        Catch ex As Exception

            divMsg.Attributes("class") = "error"
            lblJanaKeseluruhanPeringkat2.Text = "Tidak Berjaya mengemaskini gred keseluruhan Pentaksiran Akhir Vokasional. " & ex.Message & " PelajarID : " & strPelajarID

        End Try

    End Sub

    Private Sub btnJanaKeseluruhanPeringkat3_Click(sender As Object, e As EventArgs) Handles btnJanaKeseluruhanPeringkat3.Click

        Try

            lblMsg.Text = ""

            strSQL = "SELECT RecordID FROM kpmkv_kolej"

            strRet = oCommon.ExecuteSQL(strSQL)

            Dim sqlDA As New SqlDataAdapter(strSQL, objConn)
            Dim dsRecordID As DataSet = New DataSet
            sqlDA.Fill(dsRecordID, "AnyTable")

            For a As Integer = 0 To dsRecordID.Tables(0).Rows.Count - 1

                Dim strRecordID As String = dsRecordID.Tables(0).Rows(a).Item(0).ToString

                strSQL = "  SELECT kpmkv_pelajar.PelajarID, kpmkv_pelajar.KursusID, kpmkv_pelajar.Semester"
                strSQL += " FROM kpmkv_pelajar_markah "
                strSQL += " LEFT OUTER JOIN kpmkv_pelajar On kpmkv_pelajar_markah.PelajarID = kpmkv_pelajar.PelajarID"
                strSQL += " LEFT OUTER Join kpmkv_kursus On kpmkv_pelajar.KursusID = kpmkv_kursus.KursusID"
                strSQL += " WHERE kpmkv_pelajar.KolejRecordID='" & strRecordID & "' "
                strSQL += " AND kpmkv_pelajar.IsDeleted='N' AND kpmkv_pelajar.StatusID='2' AND kpmkv_pelajar.JenisCalonID='2'"
                strSQL += " AND kpmkv_pelajar.Tahun ='" & ddlTahun.Text & "'"

                If Not ddlSemester.Text = "-Pilih-" Then

                    strSQL += " AND kpmkv_pelajar.Semester ='" & ddlSemester.Text & "'"

                End If

                strRet = oCommon.ExecuteSQL(strSQL)

                sqlDA = New SqlDataAdapter(strSQL, objConn)
                Dim dsPelajar As DataSet = New DataSet
                sqlDA.Fill(dsPelajar, "AnyTable")

                For b As Integer = 0 To dsPelajar.Tables(0).Rows.Count - 1

                    strPelajarID = dsPelajar.Tables(0).Rows(b).Item(0).ToString
                    strKursusID = dsPelajar.Tables(0).Rows(b).Item(1).ToString
                    strSemester = dsPelajar.Tables(0).Rows(b).Item(2).ToString

                    Vokasional_SMP_PB_keseluruhan()

                Next

            Next

            If Not strRet = "0" Then
                divMsg.Attributes("class") = "error"
                lblJanaKeseluruhanPeringkat3.Text = "Tidak Berjaya mengemaskini SMP_PB keseluruhan Pentaksiran Akhir Vokasional"
            Else
                divMsg.Attributes("class") = "info"
                lblJanaKeseluruhanPeringkat3.Text = "Berjaya mengemaskini SMP_PB keseluruhan Pentaksiran Akhir Vokasional"
            End If



        Catch ex As Exception

            divMsg.Attributes("class") = "error"
            lblJanaKeseluruhanPeringkat3.Text = "Tidak Berjaya mengemaskini SMP_PB keseluruhan Pentaksiran Akhir Vokasional." & ex.Message & " PelajarID : " & strPelajarID

        End Try

    End Sub

    Private Sub btnJanaKeseluruhanPeringkat4_Click(sender As Object, e As EventArgs) Handles btnJanaKeseluruhanPeringkat4.Click

        Try

            lblMsg.Text = ""

            strSQL = "SELECT RecordID FROM kpmkv_kolej"

            strRet = oCommon.ExecuteSQL(strSQL)

            Dim sqlDA As New SqlDataAdapter(strSQL, objConn)
            Dim dsRecordID As DataSet = New DataSet
            sqlDA.Fill(dsRecordID, "AnyTable")

            For a As Integer = 0 To dsRecordID.Tables(0).Rows.Count - 1

                Dim strRecordID As String = dsRecordID.Tables(0).Rows(a).Item(0).ToString

                strSQL = "  SELECT kpmkv_pelajar.PelajarID, kpmkv_pelajar.KursusID, kpmkv_pelajar.Semester"
                strSQL += " FROM kpmkv_pelajar_markah "
                strSQL += " LEFT OUTER JOIN kpmkv_pelajar On kpmkv_pelajar_markah.PelajarID = kpmkv_pelajar.PelajarID"
                strSQL += " LEFT OUTER Join kpmkv_kursus On kpmkv_pelajar.KursusID = kpmkv_kursus.KursusID"
                strSQL += " WHERE kpmkv_pelajar.KolejRecordID='" & strRecordID & "' "
                strSQL += " AND kpmkv_pelajar.IsDeleted='N' AND kpmkv_pelajar.StatusID='2' AND kpmkv_pelajar.JenisCalonID='2'"
                strSQL += " AND kpmkv_pelajar.Tahun ='" & ddlTahun.Text & "'"

                If Not ddlSemester.Text = "-Pilih-" Then

                    strSQL += " AND kpmkv_pelajar.Semester ='" & ddlSemester.Text & "'"

                End If

                strRet = oCommon.ExecuteSQL(strSQL)

                sqlDA = New SqlDataAdapter(strSQL, objConn)
                Dim dsPelajar As DataSet = New DataSet
                sqlDA.Fill(dsPelajar, "AnyTable")

                For b As Integer = 0 To dsPelajar.Tables(0).Rows.Count - 1

                    strPelajarID = dsPelajar.Tables(0).Rows(b).Item(0).ToString
                    strKursusID = dsPelajar.Tables(0).Rows(b).Item(1).ToString
                    strSemester = dsPelajar.Tables(0).Rows(b).Item(2).ToString

                    Vokasional_SMP_PA_keseluruhan()

                Next

            Next

            If Not strRet = "0" Then
                divMsg.Attributes("class") = "error"
                lblJanaKeseluruhanPeringkat4.Text = "Tidak Berjaya mengemaskini SMP_PA keseluruhan Pentaksiran Akhir Vokasional"
            Else
                divMsg.Attributes("class") = "info"
                lblJanaKeseluruhanPeringkat4.Text = "Berjaya mengemaskini SMP_PA keseluruhan Pentaksiran Akhir Vokasional"
            End If



        Catch ex As Exception

            divMsg.Attributes("class") = "error"
            lblJanaKeseluruhanPeringkat4.Text = "Tidak Berjaya mengemaskini SMP_PA keseluruhan Pentaksiran Akhir Vokasional. " & ex.Message & " PelajarID : " & strPelajarID

        End Try

    End Sub

    Private Sub btnJanaKeseluruhanPeringkat5_Click(sender As Object, e As EventArgs) Handles btnJanaKeseluruhanPeringkat5.Click

        Try

            lblMsg.Text = ""

            strSQL = "SELECT RecordID FROM kpmkv_kolej"

            strRet = oCommon.ExecuteSQL(strSQL)

            Dim sqlDA As New SqlDataAdapter(strSQL, objConn)
            Dim dsRecordID As DataSet = New DataSet
            sqlDA.Fill(dsRecordID, "AnyTable")

            For a As Integer = 0 To dsRecordID.Tables(0).Rows.Count - 1

                Dim strRecordID As String = dsRecordID.Tables(0).Rows(a).Item(0).ToString

                strSQL = "  SELECT kpmkv_pelajar.PelajarID, kpmkv_pelajar.KursusID, kpmkv_pelajar.Semester"
                strSQL += " FROM kpmkv_pelajar_markah "
                strSQL += " LEFT OUTER JOIN kpmkv_pelajar On kpmkv_pelajar_markah.PelajarID = kpmkv_pelajar.PelajarID"
                strSQL += " LEFT OUTER Join kpmkv_kursus On kpmkv_pelajar.KursusID = kpmkv_kursus.KursusID"
                strSQL += " WHERE kpmkv_pelajar.KolejRecordID='" & strRecordID & "' "
                strSQL += " AND kpmkv_pelajar.IsDeleted='N' AND kpmkv_pelajar.StatusID='2' AND kpmkv_pelajar.JenisCalonID='2'"
                strSQL += " AND kpmkv_pelajar.Tahun ='" & ddlTahun.Text & "'"

                If Not ddlSemester.Text = "-Pilih-" Then

                    strSQL += " AND kpmkv_pelajar.Semester ='" & ddlSemester.Text & "'"

                End If

                strRet = oCommon.ExecuteSQL(strSQL)

                sqlDA = New SqlDataAdapter(strSQL, objConn)
                Dim dsPelajar As DataSet = New DataSet
                sqlDA.Fill(dsPelajar, "AnyTable")

                For b As Integer = 0 To dsPelajar.Tables(0).Rows.Count - 1

                    strPelajarID = dsPelajar.Tables(0).Rows(b).Item(0).ToString
                    strKursusID = dsPelajar.Tables(0).Rows(b).Item(1).ToString
                    strSemester = dsPelajar.Tables(0).Rows(b).Item(2).ToString

                    Vokasional_gredKompeten_keseluruhan()

                Next

            Next

            If Not strRet = "0" Then
                divMsg.Attributes("class") = "error"
                lblJanaKeseluruhanPeringkat5.Text = "Tidak Berjaya mengemaskini gred kompeten Pentaksiran Akhir Vokasional"
            Else
                divMsg.Attributes("class") = "info"
                lblJanaKeseluruhanPeringkat5.Text = "Berjaya mengemaskini gred kompeten Pentaksiran Akhir Vokasional"
            End If



        Catch ex As Exception

            divMsg.Attributes("class") = "error"
            lblJanaKeseluruhanPeringkat5.Text = "Tidak Berjaya mengemaskini gred kompeten Pentaksiran Akhir Vokasional. " & ex.Message & " PelajarID : " & strPelajarID

        End Try

    End Sub

    Private Sub btnJanaKeseluruhanPeringkat6_Click(sender As Object, e As EventArgs) Handles btnJanaKeseluruhanPeringkat6.Click

        Try

            lblMsg.Text = ""

            strSQL = "SELECT RecordID FROM kpmkv_kolej"

            strRet = oCommon.ExecuteSQL(strSQL)

            Dim sqlDA As New SqlDataAdapter(strSQL, objConn)
            Dim dsRecordID As DataSet = New DataSet
            sqlDA.Fill(dsRecordID, "AnyTable")

            For a As Integer = 0 To dsRecordID.Tables(0).Rows.Count - 1

                Dim strRecordID As String = dsRecordID.Tables(0).Rows(a).Item(0).ToString

                strSQL = "  SELECT kpmkv_pelajar.PelajarID, kpmkv_pelajar.KursusID, kpmkv_pelajar.Semester"
                strSQL += " FROM kpmkv_pelajar_markah "
                strSQL += " LEFT OUTER JOIN kpmkv_pelajar On kpmkv_pelajar_markah.PelajarID = kpmkv_pelajar.PelajarID"
                strSQL += " LEFT OUTER Join kpmkv_kursus On kpmkv_pelajar.KursusID = kpmkv_kursus.KursusID"
                strSQL += " WHERE kpmkv_pelajar.KolejRecordID='" & strRecordID & "' "
                strSQL += " AND kpmkv_pelajar.IsDeleted='N' AND kpmkv_pelajar.StatusID='2' AND kpmkv_pelajar.JenisCalonID='2'"
                strSQL += " AND kpmkv_pelajar.Tahun ='" & ddlTahun.Text & "'"

                If Not ddlSemester.Text = "-Pilih-" Then

                    strSQL += " AND kpmkv_pelajar.Semester ='" & ddlSemester.Text & "'"

                End If

                strRet = oCommon.ExecuteSQL(strSQL)

                sqlDA = New SqlDataAdapter(strSQL, objConn)
                Dim dsPelajar As DataSet = New DataSet
                sqlDA.Fill(dsPelajar, "AnyTable")

                For b As Integer = 0 To dsPelajar.Tables(0).Rows.Count - 1

                    strPelajarID = dsPelajar.Tables(0).Rows(b).Item(0).ToString
                    strKursusID = dsPelajar.Tables(0).Rows(b).Item(1).ToString
                    strSemester = dsPelajar.Tables(0).Rows(b).Item(2).ToString

                    Vokasional_gredSMP_keseluruhan()

                Next

            Next

            If Not strRet = "0" Then
                divMsg.Attributes("class") = "error"
                lblJanaKeseluruhanPeringkat6.Text = "Tidak Berjaya mengemaskini gred SMP Pentaksiran Akhir Vokasional"
            Else
                divMsg.Attributes("class") = "info"
                lblJanaKeseluruhanPeringkat6.Text = "Berjaya mengemaskini gred SMP Pentaksiran Akhir Vokasional"
            End If



        Catch ex As Exception

            divMsg.Attributes("class") = "error"
            lblJanaKeseluruhanPeringkat6.Text = "Tidak Berjaya mengemaskini gred SMP Pentaksiran Akhir Vokasional. " & ex.Message & " PelajarID : " & strPelajarID

        End Try

    End Sub
End Class