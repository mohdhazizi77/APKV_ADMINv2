Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization

Public Class svmu_senarai_calon

    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Dim strNegeri As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                kpmkv_tahun_list()

                getNegeriKV()

                getKV()

                strRet = BindData(datRespondent)

            End If

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message
        End Try
    End Sub
    Private Sub kpmkv_tahun_list()
        strSQL = "SELECT DISTINCT TahunPeperiksaan FROM kpmkv_svmu_calon ORDER BY TahunPeperiksaan ASC"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlTahunPeperiksaanSVMU.DataSource = ds
            ddlTahunPeperiksaanSVMU.DataTextField = "TahunPeperiksaan"
            ddlTahunPeperiksaanSVMU.DataValueField = "TahunPeperiksaan"
            ddlTahunPeperiksaanSVMU.DataBind()

            ddlTahunPeperiksaanSVMU.Items.Add(New ListItem("-Pilih-", "0"))
            ddlTahunPeperiksaanSVMU.Text = "0"

        Catch ex As Exception

            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub getNegeriKV()

        strSQL = "SELECT UserID FROM kpmkv_users WHERE LoginID='" & Session("LoginID") & "'"
        Dim UserID As String = oCommon.getFieldValue(strSQL)

        strSQL = "SELECT UserType FROM kpmkv_users WHERE UserID = '" & UserID & "'"

        If oCommon.getFieldValue(strSQL) = "JPN" Then

            strSQL = "SELECT Negeri FROM kpmkv_users WHERE UserID = '" & UserID & "'"

        Else

            strSQL = "SELECT Negeri FROM kpmkv_negeri ORDER BY Negeri"

        End If

        strNegeri = oCommon.getFieldValue(strSQL)
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

            strSQL = "SELECT UserType FROM kpmkv_users WHERE UserID = '" & UserID & "'"


            If oCommon.getFieldValue(strSQL) = "JPN" Then

            Else

                ddlNegeri.Items.Add(New ListItem("-Pilih-", "0"))
                ddlNegeri.Text = "0"

            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub getKV()

        strSQL = "SELECT Kod, Nama, RecordID FROM kpmkv_kolej WHERE Negeri = '" & ddlNegeri.Text & "' ORDER BY Kod ASC"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlKodPusat.DataSource = ds
            ddlKodPusat.DataTextField = "Kod"
            ddlKodPusat.DataValueField = "RecordID"
            ddlKodPusat.DataBind()
            '--ALL
            ddlKodPusat.Items.Add(New ListItem("-Pilih-", "0"))
            ddlKodPusat.Text = "0"

        Catch ex As Exception
            'lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        lblMsg.Text = ""
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

        strSQL = "
                    SELECT 
                    kpmkv_svmu.svmu_id, kpmkv_svmu.MYKAD, kpmkv_svmu.AngkaGiliran,
                    kpmkv_svmu_calon.svmu_calon_id, kpmkv_svmu_calon.Nama, kpmkv_svmu_calon.Status, kpmkv_svmu_calon.MataPelajaran,
                    kpmkv_kolej.Kod,
                    kpmkv_svmu_payment_status.PaymentStatus
                    FROM kpmkv_svmu
                    LEFT JOIN kpmkv_svmu_calon ON kpmkv_svmu_calon.svmu_id = kpmkv_svmu.svmu_id
                    LEFT JOIN kpmkv_kolej ON kpmkv_kolej.RecordID = kpmkv_svmu_calon.PusatPeperiksaanID
                    LEFT JOIN kpmkv_svmu_payment_status ON kpmkv_svmu_payment_status.RefNo = kpmkv_svmu_calon.RefNo
                    WHERE
                    kpmkv_kolej.Negeri IS NOT NULL
                    AND kpmkv_svmu_payment_status.PaymentStatus = 'success'"

        If Not ddlTahunPeperiksaanSVMU.SelectedValue = "0" Then

            strSQL += " AND kpmkv_svmu_calon.TahunPeperiksaan = '" & ddlTahunPeperiksaanSVMU.SelectedValue & "'"

        End If

        If Not ddlNegeri.SelectedValue = "0" Then

            strSQL += " AND kpmkv_kolej.Negeri = '" & ddlNegeri.SelectedValue & "'"

        End If

        If Not ddlKodPusat.SelectedValue = "0" Then

            strSQL += " AND kpmkv_svmu_calon.PusatPeperiksaanID = '" & ddlKodPusat.SelectedValue & "'"

        End If

        strSQL += " UNION
                    SELECT
                    kpmkv_svmu.svmu_id, kpmkv_svmu.MYKAD, kpmkv_svmu.AngkaGiliran,
                    kpmkv_svmu_calon.svmu_calon_id, kpmkv_svmu_calon.Nama, kpmkv_svmu_calon.Status, kpmkv_svmu_calon.MataPelajaran,
                    kpmkv_kolej.Kod,
                    kpmkv_svmu_payment_status.PaymentStatus
                    FROM kpmkv_svmu
                    LEFT JOIN kpmkv_svmu_calon ON kpmkv_svmu_calon.svmu_id = kpmkv_svmu.svmu_id
                    LEFT JOIN kpmkv_kolej ON kpmkv_kolej.RecordID = kpmkv_svmu_calon.PusatPeperiksaanID
                    LEFT JOIN kpmkv_svmu_payment_status ON kpmkv_svmu_payment_status.RefNo = kpmkv_svmu_calon.RefNo
                    WHERE
                    kpmkv_kolej.Negeri IS NOT NULL
                    AND kpmkv_svmu_calon.JenisDaftar = 'MANUAL'"

        If Not ddlTahunPeperiksaanSVMU.SelectedValue = "0" Then

            strSQL += " AND kpmkv_svmu_calon.TahunPeperiksaan = '" & ddlTahunPeperiksaanSVMU.SelectedValue & "'"

        End If

        If Not ddlNegeri.SelectedValue = "0" Then

            strSQL += " AND kpmkv_kolej.Negeri = '" & ddlNegeri.SelectedValue & "'"

        End If

        If Not ddlKodPusat.SelectedValue = "0" Then

            strSQL += " AND kpmkv_svmu_calon.PusatPeperiksaanID = '" & ddlKodPusat.SelectedValue & "'"

        End If

        strSQL += " ORDER BY kpmkv_kolej.Kod, kpmkv_svmu_calon.Nama"

        getSQL = strSQL

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

    Private Sub datRespondent_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSelectEventArgs) Handles datRespondent.SelectedIndexChanging

        Dim strKeyID As String = datRespondent.DataKeys(e.NewSelectedIndex).Value.ToString
        Response.Redirect("svmu_pengesahan_kodpusat.aspx?ID=" & AsciiSwitchWithMod(strKeyID, 19, 7))

    End Sub
    Function AsciiSwitchWithMod(InputString As String, ValueToAdd As Integer, ModValue As Integer) As String

        Dim OutputString As String = String.Empty
        Dim c As Char
        For i = 0 To Len(InputString) - 1
            c = InputString.Substring(i, 1)
            If i Mod 5 = 0 Then
                OutputString += Chr(Asc(c) + ValueToAdd + ModValue)
            Else
                OutputString += Chr(Asc(c) + ValueToAdd)
            End If
        Next

        Return OutputString

    End Function

    Private Sub ddlNegeri_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlNegeri.SelectedIndexChanged

        getKV()
        lblNamaPusat.Text = ""

    End Sub

    Private Sub ddlKodPusat_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlKodPusat.SelectedIndexChanged

        strSQL = "SELECT Nama FROM kpmkv_kolej WHERE Kod = '" & ddlKodPusat.SelectedItem.Text & "'"
        lblNamaPusat.Text = oCommon.getFieldValue(strSQL)

    End Sub

End Class