Imports System.Net
Imports System.IO
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports System.Data.SqlClient
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Public Class svmu_daftar_calon_ulang1

    Inherits System.Web.UI.UserControl
    Dim oCommon As New Commonfunction
    Dim oCommon2 As New Commonfunction2
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim fileSavePath As String = ConfigurationManager.AppSettings("FolderPath")
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Dim pendingcount As Integer = 0
    Dim RefNo As String
    Dim Amount As String
    Dim Email As String
    Dim PaymentStatus As String
    Dim FPXTxnTime As String
    Dim FPXPaymentStatus As String
    Dim FPXFinalAmount As String
    Dim FPXTxnId As String
    Dim FPXOrderNo As String
    Dim created_at As String
    Dim updated_at As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        strSQL = "SELECT setting_value_string FROM kpmkv_svmu_setting WHERE setting_parameter = 'TARIKH_MULA'"
        lblTarikhMula.Text = oCommon.getFieldValue(strSQL)

        strSQL = "SELECT setting_value_string FROM kpmkv_svmu_setting WHERE setting_parameter = 'TARIKH_AKHIR'"
        lblTarikhAkhir.Text = oCommon.getFieldValue(strSQL)

    End Sub

    Private Sub btnApply_Click(sender As Object, e As EventArgs) Handles btnApply.Click

        Try

            '--validate
            If ValidatePage() = False Then
                Exit Sub
            End If

            Dim strMYKAD As String = txtMYKAD.Text
            Dim strAG As String = txtAngkaGiliran.Text
            Dim svmuID As String

            strSQL = "SELECT setting_value_int FROM kpmkv_svmu_setting WHERE setting_parameter = 'TAHUN_PEPERIKSAAN'"
            Dim TahunPeperiksaan As String = oCommon.getFieldValue(strSQL)

            strSQL = "SELECT PelajarID FROM kpmkv_pelajar WHERE MYKAD = '" & strMYKAD & "' AND AngkaGiliran = '" & strAG & "' AND Semester = '4'"
            Dim strPelajarID As String = oCommon.getFieldValue(strSQL)

            If strPelajarID.Length = 0 Then

                strSQL = "SELECT PelajarID FROM kpmkv_pelajar WHERE MYKAD = '" & strMYKAD & "' AND AngkaGiliran = '" & strAG & "' AND Semester = '4'"
                strPelajarID = oCommon2.getFieldValue(strSQL)

                If strPelajarID.Length = 0 Then

                    txtMYKAD.Text = "Sila masukkan MYKAD yang betul!"
                    txtAngkaGiliran.Text = "Sila masukkan Angka Giliran yang betul!"

                Else

                    strSQL = "SELECT svmu_id FROM kpmkv_svmu WHERE PelajarID = '" & strPelajarID & "' AND Tahun = '" & TahunPeperiksaan & "'"
                    svmuID = oCommon.getFieldValue(strSQL)

                    If svmuID = "" Then

                        strSQL = "INSERT INTO kpmkv_svmu
                          (DatabaseName, Tahun,  PelajarID, MYKAD, AngkaGiliran, create_timestamp)
                          VALUES
                          ('KPMKV', '" & TahunPeperiksaan & "', '" & strPelajarID & "', '" & strMYKAD & "', '" & strAG & "', CURRENT_TIMESTAMP)"
                        strRet = oCommon.ExecuteSQL(strSQL)

                        strSQL = "SELECT svmu_id FROM kpmkv_svmu WHERE PelajarID = '" & strPelajarID & "'"
                        svmuID = oCommon.getFieldValue(strSQL)
                        Response.Redirect("svmu_kemaskini_calon_ulang.aspx?ID=" & AsciiSwitchWithMod(strPelajarID, 19, 7) & "&NO=" & AsciiSwitchWithMod(svmuID, 19, 7))

                    Else

                        strSQL = "SELECT svmu_id FROM kpmkv_svmu WHERE PelajarID = '" & strPelajarID & "'"
                        svmuID = oCommon.getFieldValue(strSQL)

                        ''payment pending

                        strSQL = "SELECT svmu_no_permohonan FROM kpmkv_svmu_calon WHERE svmu_id = '" & svmuID & "' AND TahunPeperiksaan = '" & TahunPeperiksaan & "' AND MataPelajaran = 'BM' AND StatusMP = '2' AND Status <> 'RALAT'"
                        Dim svmu_no_permohonanBM As String = oCommon.getFieldValue(strSQL)

                        strSQL = "SELECT svmu_no_permohonan FROM kpmkv_svmu_calon WHERE svmu_id = '" & svmuID & "' AND TahunPeperiksaan = '" & TahunPeperiksaan & "' AND MataPelajaran = 'SJ' AND StatusMP = '2' AND Status <> 'RALAT'"
                        Dim svmu_no_permohonanSJ As String = oCommon.getFieldValue(strSQL)

                        If Not svmu_no_permohonanBM = "" Then

                            strSQL = "SELECT RefNo FROM kpmkv_svmu_payment_request WHERE svmu_no_permohonan = '" & svmu_no_permohonanBM & "'"
                            Dim RefNoBM As String = oCommon.getFieldValue(strSQL)

                            If Not RefNoBM = "" Then

                                getPaymentStatus(RefNoBM)

                            End If

                        ElseIf Not svmu_no_permohonanSJ = "" Then

                            strSQL = "SELECT RefNo FROM kpmkv_svmu_payment_request WHERE svmu_no_permohonan = '" & svmu_no_permohonanSJ & "'"
                            Dim RefNoSJ As String = oCommon.getFieldValue(strSQL)

                            If Not RefNoSJ = "" Then

                                getPaymentStatus(RefNoSJ)

                            End If

                        End If

                        ''payment pending

                        strSQL = "SELECT svmu_calon_id FROM kpmkv_svmu_calon WHERE svmu_id = '" & svmuID & "' AND TahunPeperiksaan = '" & TahunPeperiksaan & "' AND MataPelajaran = 'BM' AND StatusMP = '1' AND Status <> 'RALAT'"
                        Dim BM As String = oCommon.getFieldValue(strSQL)

                        strSQL = "SELECT svmu_calon_id FROM kpmkv_svmu_calon WHERE svmu_id = '" & svmuID & "' AND TahunPeperiksaan = '" & TahunPeperiksaan & "' AND MataPelajaran = 'SJ' AND StatusMP = '1' AND Status <> 'RALAT'"
                        Dim SJ As String = oCommon.getFieldValue(strSQL)

                        If Not BM = "" And Not SJ = "" Then

                            lblMsg1.Visible = True

                        Else

                            strSQL = "SELECT svmu_calon_id FROM kpmkv_svmu_calon WHERE svmu_id = '" & svmuID & "' AND TahunPeperiksaan = '" & TahunPeperiksaan & "' AND MataPelajaran = 'BM'  AND StatusMP = '0' AND Status <> 'RALAT'"
                            Dim BMBaru As String = oCommon.getFieldValue(strSQL)

                            strSQL = "SELECT svmu_calon_id FROM kpmkv_svmu_calon WHERE svmu_id = '" & svmuID & "' AND TahunPeperiksaan = '" & TahunPeperiksaan & "' AND MataPelajaran = 'BM'  AND StatusMP = '2' AND RefNo IS NULL AND Status <> 'RALAT'"
                            Dim BMBaru2 As String = oCommon.getFieldValue(strSQL)

                            strSQL = "SELECT svmu_calon_id FROM kpmkv_svmu_calon WHERE svmu_id = '" & svmuID & "' AND TahunPeperiksaan = '" & TahunPeperiksaan & "' AND MataPelajaran = 'SJ'  AND StatusMP = '0' AND Status <> 'RALAT'"
                            Dim SJBaru As String = oCommon.getFieldValue(strSQL)

                            strSQL = "SELECT svmu_calon_id FROM kpmkv_svmu_calon WHERE svmu_id = '" & svmuID & "' AND TahunPeperiksaan = '" & TahunPeperiksaan & "' AND MataPelajaran = 'SJ'  AND StatusMP = '2' AND RefNo IS NULL AND Status <> 'RALAT'"
                            Dim SJBaru2 As String = oCommon.getFieldValue(strSQL)

                            If Not BMBaru = "" Then

                                strSQL = "UPDATE kpmkv_svmu_calon SET Status = 'RALAT' WHERE svmu_calon_id = '" & BMBaru & "'"
                                strRet = oCommon.ExecuteSQL(strSQL)

                            End If

                            If Not BMBaru2 = "" Then

                                strSQL = "UPDATE kpmkv_svmu_calon SET Status = 'RALAT' WHERE svmu_calon_id = '" & BMBaru2 & "'"
                                strRet = oCommon.ExecuteSQL(strSQL)

                            End If

                            If Not SJBaru = "" Then

                                strSQL = "UPDATE kpmkv_svmu_calon SET Status = 'RALAT' WHERE svmu_calon_id = '" & SJBaru & "'"
                                strRet = oCommon.ExecuteSQL(strSQL)

                            End If

                            If Not SJBaru2 = "" Then

                                strSQL = "UPDATE kpmkv_svmu_calon SET Status = 'RALAT' WHERE svmu_calon_id = '" & SJBaru2 & "'"
                                strRet = oCommon.ExecuteSQL(strSQL)

                            End If

                            strSQL = "SELECT svmu_id FROM kpmkv_svmu WHERE PelajarID = '" & strPelajarID & "' AND Tahun = '" & TahunPeperiksaan & "'"
                            svmuID = oCommon.getFieldValue(strSQL)
                            Response.Redirect("svmu_kemaskini_calon_ulang.aspx?ID=" & AsciiSwitchWithMod(strPelajarID, 19, 7) & "&NO=" & AsciiSwitchWithMod(svmuID, 19, 7))

                        End If

                    End If

                End If

            Else

                strSQL = "SELECT svmu_id FROM kpmkv_svmu WHERE PelajarID = '" & strPelajarID & "' AND Tahun = '" & TahunPeperiksaan & "'"
                svmuID = oCommon.getFieldValue(strSQL)

                If svmuID = "" Then

                    strSQL = "INSERT INTO kpmkv_svmu
                          (DatabaseName, Tahun,  PelajarID, MYKAD, AngkaGiliran, create_timestamp)
                          VALUES
                          ('APKV', '" & TahunPeperiksaan & "', '" & strPelajarID & "', '" & strMYKAD & "', '" & strAG & "', CURRENT_TIMESTAMP)"
                    strRet = oCommon.ExecuteSQL(strSQL)

                    strSQL = "SELECT svmu_id FROM kpmkv_svmu WHERE PelajarID = '" & strPelajarID & "'"
                    svmuID = oCommon.getFieldValue(strSQL)
                    Response.Redirect("svmu_kemaskini_calon_ulang.aspx?ID=" & AsciiSwitchWithMod(strPelajarID, 19, 7) & "&NO=" & AsciiSwitchWithMod(svmuID, 19, 7))

                Else

                    strSQL = "SELECT svmu_id FROM kpmkv_svmu WHERE PelajarID = '" & strPelajarID & "'"
                    svmuID = oCommon.getFieldValue(strSQL)

                    ''payment pending

                    strSQL = "SELECT svmu_no_permohonan FROM kpmkv_svmu_calon WHERE svmu_id = '" & svmuID & "' AND TahunPeperiksaan = '" & TahunPeperiksaan & "' AND MataPelajaran = 'BM' AND StatusMP = '2' AND Status <> 'RALAT'"
                    Dim svmu_no_permohonanBM As String = oCommon.getFieldValue(strSQL)

                    strSQL = "SELECT svmu_no_permohonan FROM kpmkv_svmu_calon WHERE svmu_id = '" & svmuID & "' AND TahunPeperiksaan = '" & TahunPeperiksaan & "' AND MataPelajaran = 'SJ' AND StatusMP = '2' AND Status <> 'RALAT'"
                    Dim svmu_no_permohonanSJ As String = oCommon.getFieldValue(strSQL)

                    If Not svmu_no_permohonanBM = "" Then

                        strSQL = "SELECT RefNo FROM kpmkv_svmu_payment_request WHERE svmu_no_permohonan = '" & svmu_no_permohonanBM & "'"
                        Dim RefNoBM As String = oCommon.getFieldValue(strSQL)

                        If Not RefNoBM = "" Then

                            getPaymentStatus(RefNoBM)

                        End If

                    ElseIf Not svmu_no_permohonanSJ = "" Then

                        strSQL = "SELECT RefNo FROM kpmkv_svmu_payment_request WHERE svmu_no_permohonan = '" & svmu_no_permohonanSJ & "'"
                        Dim RefNoSJ As String = oCommon.getFieldValue(strSQL)

                        If Not RefNoSJ = "" Then

                            getPaymentStatus(RefNoSJ)

                        End If

                    End If

                    ''payment pending

                    strSQL = "SELECT svmu_calon_id FROM kpmkv_svmu_calon WHERE svmu_id = '" & svmuID & "' AND TahunPeperiksaan = '" & TahunPeperiksaan & "' AND MataPelajaran = 'BM' AND StatusMP = '1' AND Status <> 'RALAT'"
                    Dim BM As String = oCommon.getFieldValue(strSQL)

                    strSQL = "SELECT svmu_calon_id FROM kpmkv_svmu_calon WHERE svmu_id = '" & svmuID & "' AND TahunPeperiksaan = '" & TahunPeperiksaan & "' AND MataPelajaran = 'SJ' AND StatusMP = '1' AND Status <> 'RALAT'"
                    Dim SJ As String = oCommon.getFieldValue(strSQL)

                    If Not BM = "" And Not SJ = "" Then

                        lblMsg1.Visible = True

                    Else

                        strSQL = "SELECT svmu_calon_id FROM kpmkv_svmu_calon WHERE svmu_id = '" & svmuID & "' AND TahunPeperiksaan = '" & TahunPeperiksaan & "' AND MataPelajaran = 'BM'  AND StatusMP = '0' AND Status <> 'RALAT'"
                        Dim BMBaru As String = oCommon.getFieldValue(strSQL)

                        strSQL = "SELECT svmu_calon_id FROM kpmkv_svmu_calon WHERE svmu_id = '" & svmuID & "' AND TahunPeperiksaan = '" & TahunPeperiksaan & "' AND MataPelajaran = 'BM'  AND StatusMP = '2' AND RefNo IS NULL AND Status <> 'RALAT'"
                        Dim BMBaru2 As String = oCommon.getFieldValue(strSQL)

                        strSQL = "SELECT svmu_calon_id FROM kpmkv_svmu_calon WHERE svmu_id = '" & svmuID & "' AND TahunPeperiksaan = '" & TahunPeperiksaan & "' AND MataPelajaran = 'SJ'  AND StatusMP = '0' AND Status <> 'RALAT'"
                        Dim SJBaru As String = oCommon.getFieldValue(strSQL)

                        strSQL = "SELECT svmu_calon_id FROM kpmkv_svmu_calon WHERE svmu_id = '" & svmuID & "' AND TahunPeperiksaan = '" & TahunPeperiksaan & "' AND MataPelajaran = 'SJ'  AND StatusMP = '2' AND RefNo IS NULL AND Status <> 'RALAT'"
                        Dim SJBaru2 As String = oCommon.getFieldValue(strSQL)

                        If Not BMBaru = "" Then

                            strSQL = "UPDATE kpmkv_svmu_calon SET Status = 'RALAT' WHERE svmu_calon_id = '" & BMBaru & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)

                        End If

                        If Not BMBaru2 = "" Then

                            strSQL = "UPDATE kpmkv_svmu_calon SET Status = 'RALAT' WHERE svmu_calon_id = '" & BMBaru2 & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)

                        End If

                        If Not SJBaru = "" Then

                            strSQL = "UPDATE kpmkv_svmu_calon SET Status = 'RALAT' WHERE svmu_calon_id = '" & SJBaru & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)

                        End If

                        If Not SJBaru2 = "" Then

                            strSQL = "UPDATE kpmkv_svmu_calon SET Status = 'RALAT' WHERE svmu_calon_id = '" & SJBaru2 & "'"
                            strRet = oCommon.ExecuteSQL(strSQL)

                        End If

                        strSQL = "SELECT svmu_id FROM kpmkv_svmu WHERE PelajarID = '" & strPelajarID & "' AND Tahun = '" & TahunPeperiksaan & "'"
                        svmuID = oCommon.getFieldValue(strSQL)
                        Response.Redirect("svmu_kemaskini_calon_ulang.aspx?ID=" & AsciiSwitchWithMod(strPelajarID, 19, 7) & "&NO=" & AsciiSwitchWithMod(svmuID, 19, 7))

                    End If

                End If

            End If

        Catch ex As Exception

        End Try

    End Sub

    Private Sub getPaymentStatus(ByVal RefNo As String)

        Dim paramName As String() = New String(0) {"RefNo"}
        Dim paramVal As String() = New String(0) {RefNo}
        Dim result As String

        strSQL = "SELECT setting_value_string FROM kpmkv_svmu_setting WHERE setting_parameter = 'TOKEN'"
        Dim Token As String = oCommon.getFieldValue(strSQL)

        strSQL = "SELECT setting_value_int FROM kpmkv_svmu_setting WHERE setting_parameter = 'PRODUCTION'"
        Dim Live As String = oCommon.getFieldValue(strSQL)

        If Live = "1" Then

            ''LIVE
            result = HttpPost("https://elp.moe.gov.my/eportal/api/payment/" & Token & "/apigetformrequest", paramName, paramVal)

        Else

            ''STAGING
            result = HttpPost("https://elp-lab.moe.gov.my/eportal/api/payment/" & Token & "/apigetformrequest", paramName, paramVal)

        End If

        DeserializeAndDump(result)

        strSQL = "SELECT svmu_no_permohonan FROM kpmkv_svmu_payment_request WHERE RefNo = '" & RefNo & "'"
        Dim svmu_no_permohonan As String = oCommon.getFieldValue(strSQL)

        strSQL = "SELECT svmu_id FROM kpmkv_svmu_calon WHERE svmu_no_permohonan = '" & svmu_no_permohonan & "' AND Status <> 'RALAT'"
        Dim svmu_id As String = oCommon.getFieldValue(strSQL)

        strSQL = "SELECT PelajarID FROM kpmkv_svmu WHERE svmu_id = '" & svmu_id & "'"
        Dim PelajarID As String = oCommon.getFieldValue(strSQL)

        If PaymentStatus = "failed" Then

            strSQL = "UPDATE kpmkv_svmu_calon SET Status = 'RALAT' WHERE svmu_no_permohonan = '" & svmu_no_permohonan & "'"
            strRet = oCommon.ExecuteSQL(strSQL)

            Response.Redirect("pendaftaran_calon_ulang_online.aspx")

        ElseIf PaymentStatus = "pending" Then

            pendingcount = pendingcount + 1

            If pendingcount = 3 Then

                strSQL = "UPDATE kpmkv_svmu_calon SET Status = 'RALAT' WHERE RefNo = '" & RefNo & "'"
                strRet = oCommon.ExecuteSQL(strSQL)

                Exit Sub

            End If

            System.Threading.Thread.Sleep(10000)

            getPaymentStatus(RefNo)

        ElseIf PaymentStatus = "success" Then

            strSQL = "UPDATE kpmkv_svmu_calon SET RefNo = '" & RefNo & "', StatusMP = '1' WHERE svmu_no_permohonan = '" & svmu_no_permohonan & "'"
            strRet = oCommon.ExecuteSQL(strSQL)

        End If


    End Sub

    Private Shared Function HttpPost(url As String, paramName As String(), paramVal As String()) As String
        Dim req As HttpWebRequest = TryCast(WebRequest.Create(New Uri(url)), HttpWebRequest)
        req.Method = "POST"
        req.ContentType = "application/x-www-form-urlencoded"

        'req.Headers("Authorization") = "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes("myusername:mypassword"))


        ' Build a string with all the params, properly encoded.
        ' We assume that the arrays paramName and paramVal are
        ' of equal length:
        Dim paramz As New StringBuilder()
        For i As Integer = 0 To paramName.Length - 1
            paramz.Append(paramName(i))
            paramz.Append("=")
            paramz.Append(HttpUtility.UrlEncode(paramVal(i)))
            If Not i = paramName.Length - 1 Then

                paramz.Append("&")

            End If
        Next

        ' Encode the parameters as form data:
        Dim formData As Byte() = UTF8Encoding.UTF8.GetBytes(paramz.ToString())
        req.ContentLength = formData.Length

        ' Send the request:
        Using post As Stream = req.GetRequestStream()
            post.Write(formData, 0, formData.Length)
        End Using

        ' Pick up the response:
        Dim result As String = Nothing
        Using resp As HttpWebResponse = TryCast(req.GetResponse(), HttpWebResponse)
            Dim reader As New StreamReader(resp.GetResponseStream())
            result = reader.ReadToEnd()
        End Using

        Return result
    End Function

    Public Class JSONdetails
        Public Property RefNo As String
        Public Property Amount As String
        Public Property Email As String
        Public Property PaymentStatus As String
        Public Property FPXTxnTime As String
        Public Property FPXPaymentStatus As String
        Public Property FPXFinalAmount As String
        Public Property FPXTxnId As String
        Public Property FPXOrderNo As String
        Public Property created_at As String
        Public Property updated_at As String

    End Class

    Public Class SingleOrArrayConverter(Of T)
        Inherits JsonConverter

        Public Overrides Function CanConvert(objectType As Type) As Boolean
            Return objectType = GetType(List(Of T))
        End Function

        Public Overrides Function ReadJson(reader As JsonReader, objectType As Type, existingValue As Object, serializer As JsonSerializer) As Object
            Dim token As JToken = JToken.Load(reader)

            If (token.Type = JTokenType.Array) Then
                Return token.ToObject(Of List(Of T))()
            End If

            Return New List(Of T) From {token.ToObject(Of T)()}
        End Function

        Public Overrides ReadOnly Property CanWrite As Boolean
            Get
                Return False
            End Get
        End Property

        Public Overrides Sub WriteJson(writer As JsonWriter, value As Object, serializer As JsonSerializer)
            Throw New NotImplementedException
        End Sub

    End Class

    Public Class JSONdata

        <JsonConverter(GetType(SingleOrArrayConverter(Of JSONdetails)))>
        Public Property data As List(Of JSONdetails)

    End Class

    Sub DeserializeAndDump(json As String)

        Dim detail As JSONdata = JsonConvert.DeserializeObject(Of JSONdata)(json)

        strSQL = "SELECT payment_request_id FROM kpmkv_svmu_payment_request WHERE RefNo = '" & RefNo & "'"
        Dim payment_request_id As String = oCommon.getFieldValue(strSQL)

        For Each ln As JSONdetails In detail.data

            RefNo = ln.RefNo
            Amount = ln.Amount
            Email = ln.Email
            PaymentStatus = ln.PaymentStatus
            FPXTxnTime = ln.FPXTxnTime
            FPXPaymentStatus = ln.FPXPaymentStatus
            FPXFinalAmount = ln.FPXFinalAmount
            FPXTxnId = ln.FPXTxnId
            FPXOrderNo = ln.FPXOrderNo
            created_at = ln.created_at
            updated_at = ln.updated_at

            strSQL = "  SELECT payment_status_id FROM kpmkv_svmu_payment_status WHERE RefNo = '" & RefNo & "'"
            Dim paymentStatusID As String = oCommon.getFieldValue(strSQL)

            If paymentStatusID = "" Then

                strSQL = "  INSERT INTO kpmkv_svmu_payment_status
                            (RefNo, Amount, Email, PaymentStatus, FPXTxnTime, FPXPaymentStatus, FPXFinalAmount, FPXTxnId, FPXOrderNo, created_at, updated_at)
                            VALUES
                            ('" & RefNo & "', '" & Amount & "', '" & Email & "', '" & PaymentStatus & "', '" & FPXTxnTime & "', '" & FPXPaymentStatus & "', '" & FPXFinalAmount & "', '" & FPXTxnId & "', '" & FPXOrderNo & "', '" & created_at & "', '" & updated_at & "')"
                strRet = oCommon.ExecuteSQL(strSQL)

            Else

                strSQL = "UPDATE kpmkv_svmu_calon SET Status = 'RALAT' WHERE RefNo = '" & RefNo & "'"
                strRet = oCommon.ExecuteSQL(strSQL)

                strSQL = "  INSERT INTO kpmkv_svmu_payment_status
                            (RefNo, Amount, Email, PaymentStatus, FPXTxnTime, FPXPaymentStatus, FPXFinalAmount, FPXTxnId, FPXOrderNo, created_at, updated_at)
                            VALUES
                            ('" & RefNo & "', '" & Amount & "', '" & Email & "', '" & PaymentStatus & "', '" & FPXTxnTime & "', '" & FPXPaymentStatus & "', '" & FPXFinalAmount & "', '" & FPXTxnId & "', '" & FPXOrderNo & "', '" & created_at & "', '" & updated_at & "')"
                strRet = oCommon.ExecuteSQL(strSQL)

            End If

        Next

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


    Private Function ValidatePage() As Boolean

        Dim errorCode As String = "FALSE"

        lblMsgMYKAD.Text = ""
        lblMsgAngkaGiliran.Text = ""

        '--txtMYKAD
        If txtMYKAD.Text.Length > 12 Or txtMYKAD.Text.Length < 12 Then
            lblMsgMYKAD.Text = "Sila masukkan No. Kad Pengenalan yang betul!"
            errorCode = "TRUE"
        End If

        If txtMYKAD.Text.Length = 0 Then
            lblMsgMYKAD.Text = "Sila masukkan No. Kad Pengenalan!"
            errorCode = "TRUE"
        End If

        If txtAngkaGiliran.Text.Length > 11 Or txtAngkaGiliran.Text.Length < 11 Then
            lblMsgAngkaGiliran.Text = "Sila masukkan Angka Giliran yang betul!"
            errorCode = "TRUE"
        End If

        If txtAngkaGiliran.Text.Length = 0 Then
            lblMsgAngkaGiliran.Text = "Sila masukkan Angka Giliran!"
            errorCode = "TRUE"
        End If

        If errorCode = "TRUE" Then
            Return False
        Else
            Return True
        End If

    End Function

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click

        Response.Redirect("pendaftaran_calon_ulang_online.aspx")

    End Sub

    Private Sub formatSampul_ServerClick(sender As Object, e As EventArgs) Handles formatSampul.ServerClick

        Dim fullFileName As String = "FORMAT SAMPUL.pdf"

        fileSavePath = fileSavePath & "//" & fullFileName

        Response.ContentType = "Application/octet-stream"
        Response.AppendHeader("Content-Disposition", "attachment; filename=" & fullFileName & "")
        Response.TransmitFile(fileSavePath)
        Response.End()

    End Sub
End Class