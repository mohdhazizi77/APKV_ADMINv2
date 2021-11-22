Imports System.Data.SqlClient

Public Class svmu_pengesahan_kodpusat
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Dim svmu_id As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then

            getDetails()
            getPusatPeperiksaan()
            'getStatusPembayaran()

            getKolejList()

        End If

    End Sub

    Private Sub getDetails()

        Dim ar_Calon As Array

        strSQL = "SELECT svmu_id FROM kpmkv_svmu_calon WHERE svmu_calon_id = '" & AsciiSwitchWithMod(Request.QueryString("ID"), -19, -7) & "'"
        svmu_id = oCommon.getFieldValue(strSQL)


        strSQL = "  SELECT PelajarID
                    ,MYKAD
                    ,AngkaGiliran
                    FROM kpmkv_svmu
                    WHERE
                    svmu_id = '" & svmu_id & "'"
        strRet = oCommon.getFieldValueEx(strSQL)

        ar_Calon = strRet.Split("|")

        Dim strPelajarID As String = ar_Calon(0)
        Dim strMYKAD As String = ar_Calon(1)
        Dim strAG As String = ar_Calon(2)

        strSQL = "  SELECT 
                    svmu_calon_id, svmu_id, Nama, TarikhLahir, Jantina, Bangsa, Agama, KolejID, KolejNama, Kohort, Alamat, Poskod, Bandar, Negeri, Telefon, Email, create_timestamp
                    FROM 
                    kpmkv_svmu_calon
                    WHERE
                    svmu_calon_id = '" & AsciiSwitchWithMod(Request.QueryString("ID"), -19, -7) & "'"
        strRet = oCommon.getFieldValueEx(strSQL)

        ar_Calon = strRet.Split("|")

        txtNama.Text = ar_Calon(2)
        txtMYKAD.Text = strMYKAD
        txtAngkaGiliran.Text = strAG
        txtDate.Text = ar_Calon(3)
        txtJantina.Text = ar_Calon(4)
        txtBangsa.Text = ar_Calon(5)
        txtAgama.Text = ar_Calon(6)
        txtKolej.Text = ar_Calon(8)
        txtKohort.Text = ar_Calon(9)
        txtAlamat.Text = ar_Calon(10)
        txtPoskod.Text = ar_Calon(11)
        txtBandar.Text = ar_Calon(12)
        txtNegeri.Text = ar_Calon(13)
        txtTelefon.Text = ar_Calon(14)
        txtEmail.Text = ar_Calon(15)


    End Sub

    Private Sub getKolejList()

        strSQL = "SELECT Nama,RecordID FROM kpmkv_kolej WHERE Negeri='" & lblNegeri.Text & "' ORDER BY Nama ASC"
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
            'ddlKolej.Items.Add(New ListItem("-Pilih-", "0"))

            strSQL = "SELECT PusatPeperiksaanID FROM kpmkv_svmu_calon WHERE svmu_calon_id = '" & AsciiSwitchWithMod(Request.QueryString("ID"), -19, -7) & "'"
            Dim PusatPeperiksaanID As String = oCommon.getFieldValue(strSQL)

            ddlKolej.SelectedValue = PusatPeperiksaanID

            'ddlKolej.Text = "0"

        Catch ex As Exception
            lblKodPusat.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub getPusatPeperiksaan()

        strSQL = "SELECT PusatPeperiksaanID FROM kpmkv_svmu_calon WHERE svmu_calon_id = '" & AsciiSwitchWithMod(Request.QueryString("ID"), -19, -7) & "'"
        Dim PusatPeperiksaanID As String = oCommon.getFieldValue(strSQL)

        strSQL = "SELECT Kod, Nama, Negeri FROM kpmkv_kolej WHERE RecordID = '" & PusatPeperiksaanID & "'"
        strRet = oCommon.getFieldValueEx(strSQL)

        ''--get user info
        Dim kolej_Details As Array
        kolej_Details = strRet.Split("|")

        lblKodPusat.Text = kolej_Details(0)
        lblNegeri.Text = kolej_Details(2)

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

    'Private Sub getStatusPembayaran()

    '    strSQL = "SELECT PaymentStatus FROM kpmkv_svmu_payment_status WHERE svmu_id = '" & svmu_id & "'"
    '    If Not oCommon.getFieldValue(strSQL) = "Success" Then

    '        lblStatusPembayaran.Text = "BELUM BAYAR"
    '        lblStatusPembayaran.ForeColor = Drawing.Color.Red

    '    Else

    '        lblStatusPembayaran.Text = "TELAH BAYAR"
    '        lblStatusPembayaran.ForeColor = Drawing.Color.Green

    '    End If

    'End Sub

    Private Sub btnProceed_Click(sender As Object, e As EventArgs) Handles btnProceed.Click

        strSQL = "  UPDATE kpmkv_svmu_calon
                    SET Status = 'DISAHKAN',
                    PusatPeperiksaanJPN = '" & ddlKolej.SelectedValue & "'                    
                    WHERE svmu_calon_id = '" & AsciiSwitchWithMod(Request.QueryString("ID"), -19, -7) & "'"
        strRet = oCommon.ExecuteSQL(strSQL)

        Response.Redirect("svmu_senarai_calon.aspx")

    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click

        Response.Redirect("svmu_senarai_calon.aspx")

    End Sub

    Private Sub btnKemaskini_Click(sender As Object, e As EventArgs) Handles btnKemaskini.Click

        strSQL = "  UPDATE kpmkv_svmu_calon
                    SET PusatPeperiksaanJPN = '" & ddlKolej.SelectedValue & "'
                    WHERE svmu_calon_id = '" & AsciiSwitchWithMod(Request.QueryString("ID"), -19, -7) & "'"
        strRet = oCommon.ExecuteSQL(strSQL)

        If Not strRet = "0" Then

            lblKemaskini.Text = "Kemaskini Tidak Berjaya!"

        Else

            lblKemaskini.Text = "Kemaskini Berjaya!"

        End If

        getPusatPeperiksaan()

    End Sub
End Class