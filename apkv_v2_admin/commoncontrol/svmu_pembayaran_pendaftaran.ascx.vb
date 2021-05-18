Imports System.Net
Imports System.IO
Imports System.Web.Script.Serialization
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports System.Data.SqlClient


Public Class svmu_pembayaran_pendaftaran

    Inherits System.Web.UI.UserControl
    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Dim NamaPenuh As String
    Dim NoKP As String
    Dim NoKPLama_Tentera As String
    Dim NoTelefon As String
    Dim Email As String
    Dim JenisPeperiksaan As String
    Dim Jumlah As String
    Dim RefNo As String
    Dim ExternalApp As String
    Dim updated_at As String
    Dim created_at As String
    Dim strid As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        strSQL = "  SELECT MYKAD FROM kpmkv_svmu WHERE svmu_id = '" & AsciiSwitchWithMod(Request.QueryString("NO"), -19, -7) & "'"
        lblMYKAD.Text = oCommon.getFieldValue(strSQL)

        strSQL = "SELECT setting_value_int FROM kpmkv_svmu_setting WHERE setting_parameter = 'TAHUN_PEPERIKSAAN'"
        Dim TahunPeperiksaan As String = oCommon.getFieldValue(strSQL)

        strSQL = "  SELECT svmu_no_permohonan FROM kpmkv_svmu_calon WHERE svmu_id = '" & AsciiSwitchWithMod(Request.QueryString("NO"), -19, -7) & "' AND TahunPeperiksaan = '" & TahunPeperiksaan & "'  AND StatusMP = '0'"
        lblNoPermohonan.Text = oCommon.getFieldValue(strSQL)

    End Sub
    Sub DeserializeAndDump(json As String)

        Dim detail As JSONdata = JsonConvert.DeserializeObject(Of JSONdata)(json)

        For Each ln As JSONdetails In detail.data

            NamaPenuh = ln.NamaPenuh
            NoKP = ln.NoKP
            NoKPLama_Tentera = ln.NoKPLama_Tentera
            NoTelefon = ln.NoTelefon
            Email = ln.Email
            JenisPeperiksaan = ln.JenisPeperiksaan
            Jumlah = ln.Jumlah
            RefNo = ln.RefNo
            ExternalApp = ln.ExternalApp
            updated_at = ln.updated_at
            created_at = ln.created_at
            strid = ln.id

            strSQL = "  SELECT PelajarID
                    ,MYKAD
                    ,AngkaGiliran
                    FROM kpmkv_svmu
                    WHERE
                    svmu_id = '" & AsciiSwitchWithMod(Request.QueryString("NO"), -19, -7) & "'"
            strRet = oCommon.getFieldValueEx(strSQL)

            Dim ar_Calon As Array
            ar_Calon = strRet.Split("|")

            Dim strPelajarID As String = ar_Calon(0)
            Dim strMYKAD As String = ar_Calon(1)
            Dim strAG As String = ar_Calon(2)

            strSQL = "  INSERT INTO kpmkv_svmu_payment_request
                        (svmu_id, Jumlah, RefNo, ExternalAPP, Updated_at, Created_at, Id)
                        VALUES
                        ('" & AsciiSwitchWithMod(Request.QueryString("NO"), -19, -7) & "', '" & Jumlah & "', '" & RefNo & "', '" & ExternalApp & "', '" & updated_at & "', '" & created_at & "', '" & strid & "')"
            strRet = oCommon.ExecuteSQL(strSQL)

        Next

    End Sub
    Public Class JSONdata

        <JsonConverter(GetType(SingleOrArrayConverter(Of JSONdetails)))>
        Public Property data As List(Of JSONdetails)

    End Class

    Public Class JSONdetails
        Public Property NamaPenuh As String
        Public Property NoKP As String
        Public Property NoKPLama_Tentera As String
        Public Property NoTelefon As String
        Public Property Email As String
        Public Property JenisPeperiksaan As String
        Public Property Jumlah As String
        Public Property RefNo As String
        Public Property ExternalApp As String
        Public Property updated_at As String
        Public Property created_at As String
        Public Property id As String

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

    Private Sub btnPay_Click(sender As Object, e As EventArgs) Handles btnPay.Click

        strSQL = "  SELECT PelajarID
                    ,MYKAD
                    ,AngkaGiliran
                    FROM kpmkv_svmu
                    WHERE
                    svmu_id = '" & AsciiSwitchWithMod(Request.QueryString("NO"), -19, -7) & "'"
        strRet = oCommon.getFieldValueEx(strSQL)

        Dim ar_Calon As Array
        ar_Calon = strRet.Split("|")

        Dim strPelajarID As String = ar_Calon(0)
        Dim strMYKAD As String = ar_Calon(1)
        Dim strAG As String = ar_Calon(2)

        strSQL = "SELECT Nama, Telefon, Email FROM kpmkv_svmu_calon WHERE svmu_id = '" & AsciiSwitchWithMod(Request.QueryString("No"), -19, -7) & "'"
        strRet = oCommon.getFieldValueEx(strSQL)

        ar_Calon = strRet.Split("|")

        Dim strNama As String = ar_Calon(0)
        Dim strTelefon As String = ar_Calon(1)
        Dim strEmail As String = ar_Calon(2)


        Dim rmAsas As Double
        Dim rmBM As Double
        Dim rmSJ As Double
        Dim rmTotal As Double

        ''getRMAsas
        strSQL = "SELECT setting_value_double FROM kpmkv_svmu_setting WHERE setting_parameter = 'BAYARAN' AND setting_value_string = 'ASAS'"
        rmAsas = oCommon.getFieldValue(strSQL)

        ''getRMBM
        strSQL = "SELECT setting_value_double FROM kpmkv_svmu_setting WHERE setting_parameter = 'BAYARAN' AND setting_value_string = 'BM'"
        rmBM = oCommon.getFieldValue(strSQL)

        ''getRMSJ
        strSQL = "SELECT setting_value_double FROM kpmkv_svmu_setting WHERE setting_parameter = 'BAYARAN' AND setting_value_string = 'SJ'"
        rmSJ = oCommon.getFieldValue(strSQL)

        strSQL = "SELECT setting_value_int FROM kpmkv_svmu_setting WHERE setting_parameter = 'TAHUN_PEPERIKSAAN'"
        Dim TahunPeperiksaan As String = oCommon.getFieldValue(strSQL)

        ''getMPBM
        strSQL = "SELECT svmu_calon_id FROM kpmkv_svmu_calon WHERE svmu_id = '" & AsciiSwitchWithMod(Request.QueryString("NO"), -19, -7) & "' AND TahunPeperiksaan = '" & TahunPeperiksaan & "' AND MataPelajaran = 'BM' AND StatusMP = '0'"
        strRet = oCommon.getFieldValue(strSQL)

        If strRet = "" Then

            rmBM = 0

        End If

        ''getMPSJ
        strSQL = "SELECT svmu_calon_id FROM kpmkv_svmu_calon WHERE svmu_id = '" & AsciiSwitchWithMod(Request.QueryString("NO"), -19, -7) & "' AND TahunPeperiksaan = '" & TahunPeperiksaan & "' AND MataPelajaran = 'SJ' AND StatusMP = '0'"
        strRet = oCommon.getFieldValue(strSQL)

        If strRet = "" Then

            rmSJ = 0

        End If

        rmTotal = Format(rmAsas + rmBM + rmSJ, "0.00")


        Dim paramName As String() = New String(6) {"NamaPenuh", "NoKP", "NoTelefon", "Email", "JenisPeperiksaan", "Jumlah", "NoKPLama_Tentera"}
        Dim paramVal As String() = New String(6) {strNama, strMYKAD, strTelefon, strEmail, "svmu", rmTotal, strMYKAD}
        Dim result As String

        ''STAGING
        result = HttpPost("https://elp-lab.moe.gov.my/eportal/api/payment/elpportal607e54a7d74bf/apisetformrequest", paramName, paramVal)

        ''LIVE
        ''result = HttpPost("https://elp.moe.gov.my/eportal/api/payment/elpportal607e54a7d74bf/apisetformrequest", paramName, paramVal)

        ''"{""data"":{""
        ''NamaPenuh"":""John"",""
        ''NoKP"":""940309125873"",""
        ''NoKPLama_Tentera"":""940309125873"",""
        ''NoTelefon"":""111223333"",""
        ''Email"":""mohdhazizi@gmail.com"",""
        ''JenisPeperiksaan"":""SVM"",""
        ''Jumlah"":""30.50"",""
        ''RefNo"":""LPPay6088f9c968fab"",""
        ''ExternalApp"":""APKV"",""
        ''updated_at"":""2021-04-28 13:59:37"",""
        ''created_at"":""2021-04-28 13:59:37"",""
        ''id"":159}}"

        DeserializeAndDump(result)

        ''STAGING
        Response.Redirect("https://elp-lab.moe.gov.my/eportal/payment/" & RefNo & "/gateway")

        ''LIVE
        ''Response.Redirect("https://elp.moe.gov.my/eportal/payment/" & RefNo & "/gateway")

    End Sub


End Class