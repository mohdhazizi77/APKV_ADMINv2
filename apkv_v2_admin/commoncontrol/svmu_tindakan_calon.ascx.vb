Imports System.Net
Imports System.IO
Imports System.Web.Script.Serialization
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports System.Data.SqlClient
Imports iTextSharp.text
Imports iTextSharp.text.pdf

Public Class svmu_tindakan_calon1
    Inherits System.Web.UI.UserControl
    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

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

        If Not IsPostBack Then

            getTahun()

            getPaymentStatus()

            getNama()

            getMYKAD()

            getStatusPermohonan()

            getTarikhSemakanPermohonan()

        End If

    End Sub

    Private Sub getStatusPermohonan()

        strSQL = "SELECT svmu_id FROM kpmkv_svmu_payment_request WHERE RefNo = '" & RefNo & "'"
        Dim svmuID As String = oCommon.getFieldValue(strSQL)

        strSQL = "SELECT Status FROM kpmkv_svmu_calon WHERE svmu_id = '" & svmuID & "'"

        If oCommon.getFieldValue(strSQL) = "BELUM DISAHKAN" Then

            lblStatusPermohonan.Text = "SEDANG DIPROSES"

        Else

            lblStatusPermohonan.Text = "BERJAYA"

        End If

    End Sub

    Private Sub getNama()

        strSQL = "SELECT svmu_id FROM kpmkv_svmu_payment_request WHERE RefNo = '" & RefNo & "'"
        Dim svmuID As String = oCommon.getFieldValue(strSQL)

        strSQL = "SELECT Nama FROM kpmkv_svmu_calon WHERE svmu_id = '" & svmuID & "'"
        lblNama.Text = oCommon.getFieldValue(strSQL)

    End Sub

    Private Sub getTahun()

        strSQL = "SELECT setting_value_int FROM kpmkv_svmu_setting WHERE setting_parameter = 'TAHUN_PEPERIKSAAN'"
        lblTahun.Text = oCommon.getFieldValue(strSQL)

    End Sub

    Private Sub getPaymentStatus()

        Dim paramName As String() = New String(0) {"RefNo"}
        Dim paramVal As String() = New String(0) {Request.QueryString("RefNo")}
        Dim result As String

        ''STAGING
        result = HttpPost("https://elp-lab.moe.gov.my/eportal/api/payment/elpportal607e54a7d74bf/apigetformrequest", paramName, paramVal)

        ''LIVE
        ''result = HttpPost("https://elp.moe.gov.my/eportal/api/payment/elpportal607e54a7d74bf/apigetformrequest", paramName, paramVal)

        DeserializeAndDump(result)

        strSQL = "SELECT svmu_id FROM kpmkv_svmu_payment_request WHERE RefNo = '" & RefNo & "'"
        Dim svmuID As String = oCommon.getFieldValue(strSQL)

        strSQL = "SELECT PelajarID FROM kpmkv_svmu WHERE svmu_id = '" & svmuID & "'"
        Dim PelajarID As String = oCommon.getFieldValue(strSQL)

        strSQL = "UPDATE kpmkv_svmu_calon SET RefNo = '" & RefNo & "', StatusMP = '1' WHERE svmu_id = '" & svmuID & "' AND TahunPeperiksaan = '" & lblTahun.Text & "'  AND StatusMP = '0'"
        strRet = oCommon.ExecuteSQL(strSQL)

        If Not PaymentStatus = "success" Then

            Response.Redirect("svmu_pembayaran_pendaftaran.aspx?ID=" & AsciiSwitchWithMod(PelajarID, 19, 7) & "&NO=" & AsciiSwitchWithMod(svmuID, 19, 7))
        Else

            lblStatusBayaran.Text = "TELAH BAYAR (ONLINE)"

            link.HRef = "https://elp-lab.moe.gov.my/eportal/receipt/" + Request.QueryString("RefNo") + "/asalpdf"

        End If


    End Sub

    Private Sub getMYKAD()

        strSQL = "SELECT svmu_id FROM kpmkv_svmu_payment_request WHERE RefNo = '" & RefNo & "'"
        Dim svmuID As String = oCommon.getFieldValue(strSQL)

        strSQL = "SELECT MYKAD FROM kpmkv_svmu WHERE svmu_id = '" & svmuID & "'"
        lblMYKAD.Text = oCommon.getFieldValue(strSQL)

        strSQL = "SELECT setting_value_int FROM kpmkv_svmu_setting WHERE setting_parameter = 'TAHUN_PEPERIKSAAN'"
        Dim TahunPeperiksaan As String = oCommon.getFieldValue(strSQL)

        strSQL = "  SELECT svmu_no_permohonan FROM kpmkv_svmu_calon WHERE svmu_id = '" & svmuID & "' AND TahunPeperiksaan = '" & TahunPeperiksaan & "'  AND RefNo = '" & Request.QueryString("RefNo") & "'"
        lblNoPermohonan.Text = oCommon.getFieldValue(strSQL)

    End Sub

    Private Sub getTarikhSemakanPermohonan()



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

    Sub DeserializeAndDump(json As String)

        Dim detail As JSONdata = JsonConvert.DeserializeObject(Of JSONdata)(json)

        strSQL = "SELECT svmu_id FROM kpmkv_svmu_payment_request WHERE RefNo = '" & Request.QueryString("RefNo") & "'"
        Dim svmuID As String = oCommon.getFieldValue(strSQL)

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

            strSQL = "  SELECT payment_status_id FROM kpmkv_svmu_payment_status WHERE RefNo = '" & Request.QueryString("RefNo") & "'"
            Dim paymentStatusID As String = oCommon.getFieldValue(strSQL)

            If paymentStatusID = "" Then

                strSQL = "  INSERT INTO kpmkv_svmu_payment_status
                            (RefNo, Amount, Email, PaymentStatus, FPXTxnTime, FPXPaymentStatus, FPXFinalAmount, FPXTxnId, FPXOrderNo, created_at, updated_at)
                            VALUES
                            ('" & RefNo & "', '" & Amount & "', '" & Email & "', '" & PaymentStatus & "', '" & FPXTxnTime & "', '" & FPXPaymentStatus & "', '" & FPXFinalAmount & "', '" & FPXTxnId & "', '" & FPXOrderNo & "', '" & created_at & "', '" & updated_at & "')"
                strRet = oCommon.ExecuteSQL(strSQL)

            Else

                strSQL = "DELETE FROM kpmkv_svmu_payment_status WHERE RefNo = '" & RefNo & "'"
                strRet = oCommon.ExecuteSQL(strSQL)

                strSQL = "  INSERT INTO kpmkv_svmu_payment_status
                            (RefNo, Amount, Email, PaymentStatus, FPXTxnTime, FPXPaymentStatus, FPXFinalAmount, FPXTxnId, FPXOrderNo, created_at, updated_at)
                            VALUES
                            ('" & RefNo & "', '" & Amount & "', '" & Email & "', '" & PaymentStatus & "', '" & FPXTxnTime & "', '" & FPXPaymentStatus & "', '" & FPXFinalAmount & "', '" & FPXTxnId & "', '" & FPXOrderNo & "', '" & created_at & "', '" & updated_at & "')"
                strRet = oCommon.ExecuteSQL(strSQL)

            End If

        Next

    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Response.Redirect("pendaftaran_calon_ulang_online.aspx")
    End Sub

    Private Sub KenyataanSemakan_ServerClick(sender As Object, e As EventArgs) Handles KenyataanSemakan.ServerClick

        Dim myDocument As New Document(PageSize.A4)

        Try
            HttpContext.Current.Response.ContentType = "application/pdf"
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=KenyataanSemakan.pdf")
            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache)

            PdfWriter.GetInstance(myDocument, HttpContext.Current.Response.OutputStream)

            myDocument.Open()

            ''--draw spacing
            Dim imgdrawSpacing As String = Server.MapPath("~/img/spacing.png")
            Dim imgSpacing As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(imgdrawSpacing)
            imgSpacing.Alignment = iTextSharp.text.Image.LEFT_ALIGN  'left
            imgSpacing.Border = 0

            Dim table As New PdfPTable(1)
            Dim myPara001 As New Paragraph()
            table.WidthPercentage = 100
            table.SetWidths({100})
            table.DefaultCell.Border = 0

            Dim cell = New PdfPCell()
            Dim cetak = "Kenyataan Semakan"
            myPara001 = New Paragraph(cetak, FontFactory.GetFont("Arial", 12, Font.BOLD))
            myPara001.Alignment = Element.ALIGN_CENTER
            cell.AddElement(myPara001)
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
            cell.Border = 0
            cell.Padding = 0
            table.AddCell(cell)
            myDocument.Add(table)

            table = New PdfPTable(1)
            myPara001 = New Paragraph()
            table.WidthPercentage = 100
            table.SetWidths({100})
            table.DefaultCell.Border = 0

            cell = New PdfPCell()
            cetak = "Sijil Vokasional Malaysia (Ulangan)"
            myPara001 = New Paragraph(cetak, FontFactory.GetFont("Arial", 12, Font.BOLD))
            myPara001.Alignment = Element.ALIGN_CENTER
            cell.AddElement(myPara001)
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
            cell.Border = 0
            cell.Padding = 0
            table.AddCell(cell)
            myDocument.Add(table)

            strSQL = "SELECT setting_value_int FROM kpmkv_svmu_setting WHERE setting_parameter = 'TAHUN_PEPERIKSAAN'"
            Dim TahunPeperiksaan As String = oCommon.getFieldValue(strSQL)

            table = New PdfPTable(1)
            myPara001 = New Paragraph()
            table.WidthPercentage = 100
            table.SetWidths({100})
            table.DefaultCell.Border = 0

            cell = New PdfPCell()
            cetak = "Tahun " & TahunPeperiksaan
            myPara001 = New Paragraph(cetak, FontFactory.GetFont("Arial", 12, Font.BOLD))
            myPara001.Alignment = Element.ALIGN_CENTER
            cell.AddElement(myPara001)
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
            cell.Border = 0
            cell.Padding = 0
            table.AddCell(cell)
            myDocument.Add(table)

            myDocument.Add(imgSpacing)

            ''NO PERMOHONAN

            table = New PdfPTable(3)
            table.WidthPercentage = 100
            table.SetWidths({20, 5, 75})

            cell = New PdfPCell()
            cetak = "No. Permohonan"
            cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 10, Font.BOLD)))
            cell.Border = 0
            table.AddCell(cell)

            cell = New PdfPCell()
            cetak = ":"
            cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 10)))
            cell.Border = 0
            table.AddCell(cell)

            cell = New PdfPCell()
            cetak = lblNoPermohonan.Text
            cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 10)))
            cell.Border = 0
            table.AddCell(cell)

            myDocument.Add(table)

            ''NO PERMOHONAN

            ''NAMA

            table = New PdfPTable(3)
            table.WidthPercentage = 100
            table.SetWidths({20, 5, 75})

            cell = New PdfPCell()
            cetak = "Nama Calon"
            cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 10, Font.BOLD)))
            cell.Border = 0
            table.AddCell(cell)

            cell = New PdfPCell()
            cetak = ":"
            cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 10)))
            cell.Border = 0
            table.AddCell(cell)

            cell = New PdfPCell()
            cetak = lblNama.Text
            cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 10)))
            cell.Border = 0
            table.AddCell(cell)

            myDocument.Add(table)

            ''NAMA

            ''MYKAD DAN ANGKA GILIRAN

            table = New PdfPTable(6)
            table.WidthPercentage = 100
            table.SetWidths({20, 5, 25, 20, 5, 25})

            cell = New PdfPCell()
            cetak = "No. Kad Pengenalan"
            cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 10, Font.BOLD)))
            cell.Border = 0
            table.AddCell(cell)

            cell = New PdfPCell()
            cetak = ":"
            cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 10)))
            cell.Border = 0
            table.AddCell(cell)

            cell = New PdfPCell()
            cetak = lblMYKAD.Text
            cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 10)))
            cell.Border = 0
            table.AddCell(cell)

            cell = New PdfPCell()
            cetak = "Angka Giliran"
            cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 10, Font.BOLD)))
            cell.Border = 0
            table.AddCell(cell)

            cell = New PdfPCell()
            cetak = ":"
            cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 10)))
            cell.Border = 0
            table.AddCell(cell)

            strSQL = "SELECT AngkaGiliran FROM kpmkv_svmu WHERE Mykad = '" & lblMYKAD.Text & "' AND Tahun = '" & TahunPeperiksaan & "'"

            cell = New PdfPCell()
            cetak = oCommon.getFieldValue(strSQL)
            cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 10)))
            cell.Border = 0
            table.AddCell(cell)

            myDocument.Add(table)

            ''MYKAD DAN ANGKA GILIRAN

            ''TARIKH LAHIR DAN JANTINA

            table = New PdfPTable(6)
            table.WidthPercentage = 100
            table.SetWidths({20, 5, 25, 20, 5, 25})

            cell = New PdfPCell()
            cetak = "Tarikh Lahir"
            cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 10, Font.BOLD)))
            cell.Border = 0
            table.AddCell(cell)

            cell = New PdfPCell()
            cetak = ":"
            cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 10)))
            cell.Border = 0
            table.AddCell(cell)

            strSQL = "SELECT TarikhLahir FROM kpmkv_svmu_calon WHERE svmu_no_permohonan = '" & lblNoPermohonan.Text & "'"

            cell = New PdfPCell()
            cetak = oCommon.getFieldValue(strSQL)
            cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 10)))
            cell.Border = 0
            table.AddCell(cell)

            cell = New PdfPCell()
            cetak = "Jantina"
            cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 10, Font.BOLD)))
            cell.Border = 0
            table.AddCell(cell)

            cell = New PdfPCell()
            cetak = ":"
            cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 10)))
            cell.Border = 0
            table.AddCell(cell)

            strSQL = "SELECT Jantina FROM kpmkv_svmu_calon WHERE svmu_no_permohonan = '" & lblNoPermohonan.Text & "'"

            cell = New PdfPCell()
            cetak = oCommon.getFieldValue(strSQL)
            cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 10)))
            cell.Border = 0
            table.AddCell(cell)

            myDocument.Add(table)

            ''TARIKH LAHIR DAN JANTINA

            ''BANGSA DAN AGAMA

            table = New PdfPTable(6)
            table.WidthPercentage = 100
            table.SetWidths({20, 5, 25, 20, 5, 25})

            cell = New PdfPCell()
            cetak = "Bangsa"
            cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 10, Font.BOLD)))
            cell.Border = 0
            table.AddCell(cell)

            cell = New PdfPCell()
            cetak = ":"
            cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 10)))
            cell.Border = 0
            table.AddCell(cell)

            strSQL = "SELECT Bangsa FROM kpmkv_svmu_calon WHERE svmu_no_permohonan = '" & lblNoPermohonan.Text & "'"

            cell = New PdfPCell()
            cetak = oCommon.getFieldValue(strSQL)
            cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 10)))
            cell.Border = 0
            table.AddCell(cell)

            cell = New PdfPCell()
            cetak = "Agama"
            cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 10, Font.BOLD)))
            cell.Border = 0
            table.AddCell(cell)

            cell = New PdfPCell()
            cetak = ":"
            cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 10)))
            cell.Border = 0
            table.AddCell(cell)

            strSQL = "SELECT Agama FROM kpmkv_svmu_calon WHERE svmu_no_permohonan = '" & lblNoPermohonan.Text & "'"

            cell = New PdfPCell()
            cetak = oCommon.getFieldValue(strSQL)
            cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 10)))
            cell.Border = 0
            table.AddCell(cell)

            myDocument.Add(table)

            ''BANGSA DAN AGAMA

            ''KOLEJ

            table = New PdfPTable(3)
            table.WidthPercentage = 100
            table.SetWidths({20, 5, 75})

            cell = New PdfPCell()
            cetak = "Kolej"
            cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 10, Font.BOLD)))
            cell.Border = 0
            table.AddCell(cell)

            cell = New PdfPCell()
            cetak = ":"
            cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 10)))
            cell.Border = 0
            table.AddCell(cell)

            strSQL = "SELECT KolejNama FROM kpmkv_svmu_calon WHERE svmu_no_permohonan = '" & lblNoPermohonan.Text & "'"

            cell = New PdfPCell()
            cetak = oCommon.getFieldValue(strSQL)
            cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 10)))
            cell.Border = 0
            table.AddCell(cell)

            myDocument.Add(table)

            ''KOLEJ

            ''KOHORT

            table = New PdfPTable(3)
            table.WidthPercentage = 100
            table.SetWidths({20, 5, 75})

            cell = New PdfPCell()
            cetak = "Kohort"
            cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 10, Font.BOLD)))
            cell.Border = 0
            table.AddCell(cell)

            cell = New PdfPCell()
            cetak = ":"
            cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 10)))
            cell.Border = 0
            table.AddCell(cell)

            strSQL = "SELECT Kohort FROM kpmkv_svmu_calon WHERE svmu_no_permohonan = '" & lblNoPermohonan.Text & "'"

            cell = New PdfPCell()
            cetak = oCommon.getFieldValue(strSQL)
            cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 10)))
            cell.Border = 0
            table.AddCell(cell)

            myDocument.Add(table)

            ''KOHORT

            ''ALAMAT

            table = New PdfPTable(3)
            table.WidthPercentage = 100
            table.SetWidths({20, 5, 75})

            cell = New PdfPCell()
            cetak = "Alamat"
            cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 10, Font.BOLD)))
            cell.Border = 0
            table.AddCell(cell)

            cell = New PdfPCell()
            cetak = ":"
            cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 10)))
            cell.Border = 0
            table.AddCell(cell)

            strSQL = "SELECT Alamat FROM kpmkv_svmu_calon WHERE svmu_no_permohonan = '" & lblNoPermohonan.Text & "'"

            cell = New PdfPCell()
            cetak = oCommon.getFieldValue(strSQL)
            cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 10)))
            cell.Border = 0
            table.AddCell(cell)

            myDocument.Add(table)

            ''ALAMAT

            ''POSKOD DAN BANDAR

            table = New PdfPTable(6)
            table.WidthPercentage = 100
            table.SetWidths({20, 5, 25, 20, 5, 25})

            cell = New PdfPCell()
            cetak = "Poskod"
            cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 10, Font.BOLD)))
            cell.Border = 0
            table.AddCell(cell)

            cell = New PdfPCell()
            cetak = ":"
            cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 10)))
            cell.Border = 0
            table.AddCell(cell)

            strSQL = "SELECT Poskod FROM kpmkv_svmu_calon WHERE svmu_no_permohonan = '" & lblNoPermohonan.Text & "'"

            cell = New PdfPCell()
            cetak = oCommon.getFieldValue(strSQL)
            cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 10)))
            cell.Border = 0
            table.AddCell(cell)

            cell = New PdfPCell()
            cetak = "Bandar"
            cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 10, Font.BOLD)))
            cell.Border = 0
            table.AddCell(cell)

            cell = New PdfPCell()
            cetak = ":"
            cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 10)))
            cell.Border = 0
            table.AddCell(cell)

            strSQL = "SELECT Bandar FROM kpmkv_svmu_calon WHERE svmu_no_permohonan = '" & lblNoPermohonan.Text & "'"

            cell = New PdfPCell()
            cetak = oCommon.getFieldValue(strSQL)
            cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 10)))
            cell.Border = 0
            table.AddCell(cell)

            myDocument.Add(table)

            ''POSKOD DAN BANDAR

            ''NEGERI DAN NO TELEFON

            table = New PdfPTable(6)
            table.WidthPercentage = 100
            table.SetWidths({20, 5, 25, 20, 5, 25})

            cell = New PdfPCell()
            cetak = "Negeri"
            cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 10, Font.BOLD)))
            cell.Border = 0
            table.AddCell(cell)

            cell = New PdfPCell()
            cetak = ":"
            cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 10)))
            cell.Border = 0
            table.AddCell(cell)

            strSQL = "SELECT Negeri FROM kpmkv_svmu_calon WHERE svmu_no_permohonan = '" & lblNoPermohonan.Text & "'"

            cell = New PdfPCell()
            cetak = oCommon.getFieldValue(strSQL)
            cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 10)))
            cell.Border = 0
            table.AddCell(cell)

            cell = New PdfPCell()
            cetak = "No. Telefon"
            cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 10, Font.BOLD)))
            cell.Border = 0
            table.AddCell(cell)

            cell = New PdfPCell()
            cetak = ":"
            cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 10)))
            cell.Border = 0
            table.AddCell(cell)

            strSQL = "SELECT Telefon FROM kpmkv_svmu_calon WHERE svmu_no_permohonan = '" & lblNoPermohonan.Text & "'"

            cell = New PdfPCell()
            cetak = oCommon.getFieldValue(strSQL)
            cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 10)))
            cell.Border = 0
            table.AddCell(cell)

            myDocument.Add(table)

            ''NEGERI DAN NO TELEFON

            ''EMAIL

            table = New PdfPTable(3)
            table.WidthPercentage = 100
            table.SetWidths({20, 5, 75})

            cell = New PdfPCell()
            cetak = "Alamat E-mel"
            cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 10, Font.BOLD)))
            cell.Border = 0
            table.AddCell(cell)

            cell = New PdfPCell()
            cetak = ":"
            cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 10)))
            cell.Border = 0
            table.AddCell(cell)

            strSQL = "SELECT Email FROM kpmkv_svmu_calon WHERE svmu_no_permohonan = '" & lblNoPermohonan.Text & "'"

            cell = New PdfPCell()
            cetak = oCommon.getFieldValue(strSQL)
            cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 10)))
            cell.Border = 0
            table.AddCell(cell)

            myDocument.Add(table)

            ''EMAIL

            ''PUSAT PEPERIKSAAN PILIHAN

            table = New PdfPTable(3)
            table.WidthPercentage = 100
            table.SetWidths({20, 5, 75})

            cell = New PdfPCell()
            cetak = "Pusat Peperiksaan"
            cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 10, Font.BOLD)))
            cell.Border = 0
            table.AddCell(cell)

            cell = New PdfPCell()
            cetak = ":"
            cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 10)))
            cell.Border = 0
            table.AddCell(cell)

            strSQL = "SELECT PusatPeperiksaanID FROM kpmkv_svmu_calon WHERE svmu_no_permohonan = '" & lblNoPermohonan.Text & "'"
            Dim ppID As String = oCommon.getFieldValue(strSQL)

            strSQL = "SELECT Kod FROM kpmkv_kolej WHERE RecordID = '" & ppID & "'"
            Dim KodPusat As String = oCommon.getFieldValue(strSQL)

            strSQL = "SELECT Nama FROM kpmkv_kolej WHERE RecordID = '" & ppID & "'"
            Dim NamaPusat As String = oCommon.getFieldValue(strSQL)

            cell = New PdfPCell()
            cetak = KodPusat & " - " & NamaPusat
            cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 10)))
            cell.Border = 0
            table.AddCell(cell)

            myDocument.Add(table)

            ''PUSAT PEPERIKSAAN PILIHAN

            myDocument.Add(imgSpacing)

            ''MATAPELAJARAN

            table = New PdfPTable(1)
            table.WidthPercentage = 100
            table.SetWidths({100})

            cell = New PdfPCell()
            cetak = "Mata Pelajaran Yang Didaftar"
            cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 10, Font.BOLD)))
            cell.Border = 0
            table.AddCell(cell)
            myDocument.Add(table)

            table = New PdfPTable(3)
            table.WidthPercentage = 100
            table.SetWidths({10, 30, 60})

            cell = New PdfPCell()
            cetak = "No."
            cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 10, Font.BOLD)))
            cell.Border = 0
            table.AddCell(cell)

            cell = New PdfPCell()
            cetak = "Kod M/P"
            cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 10, Font.BOLD)))
            cell.Border = 0
            table.AddCell(cell)

            cell = New PdfPCell()
            cetak = "Mata Pelajaran"
            cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 10, Font.BOLD)))
            cell.Border = 0
            table.AddCell(cell)

            myDocument.Add(table)

            Dim count As Integer = 0

            strSQL = "SELECT MataPelajaran FROM kpmkv_svmu_calon WHERE svmu_no_permohonan = '" & lblNoPermohonan.Text & "' AND MataPelajaran = 'BM' AND StatusMP = '1'"

            If Not oCommon.getFieldValue(strSQL) = "" Then

                count = count + 1

                table = New PdfPTable(3)
                table.WidthPercentage = 100
                table.SetWidths({10, 30, 60})

                cell = New PdfPCell()
                cetak = count & "."
                cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 10)))
                cell.Border = 0
                table.AddCell(cell)

                cell = New PdfPCell()
                cetak = "1104"
                cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 10)))
                cell.Border = 0
                table.AddCell(cell)

                cell = New PdfPCell()
                cetak = "Bahasa Melayu"
                cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 10)))
                cell.Border = 0
                table.AddCell(cell)

                myDocument.Add(table)

            End If

            strSQL = "SELECT MataPelajaran FROM kpmkv_svmu_calon WHERE svmu_no_permohonan = '" & lblNoPermohonan.Text & "' AND MataPelajaran = 'SJ' AND StatusMP = '1'"

            If Not oCommon.getFieldValue(strSQL) = "" Then

                count = count + 1

                table = New PdfPTable(3)
                table.WidthPercentage = 100
                table.SetWidths({10, 30, 60})

                cell = New PdfPCell()
                cetak = count & "."
                cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 10)))
                cell.Border = 0
                table.AddCell(cell)

                cell = New PdfPCell()
                cetak = "1251"
                cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 10)))
                cell.Border = 0
                table.AddCell(cell)

                cell = New PdfPCell()
                cetak = "Sejarah"
                cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 10)))
                cell.Border = 0
                table.AddCell(cell)

                myDocument.Add(table)

            End If

            myDocument.Add(imgSpacing)
            myDocument.Add(imgSpacing)
            myDocument.Add(imgSpacing)
            myDocument.Add(imgSpacing)
            myDocument.Add(imgSpacing)
            myDocument.Add(imgSpacing)
            myDocument.Add(imgSpacing)
            myDocument.Add(imgSpacing)
            myDocument.Add(imgSpacing)
            myDocument.Add(imgSpacing)
            myDocument.Add(imgSpacing)
            myDocument.Add(imgSpacing)
            myDocument.Add(imgSpacing)
            myDocument.Add(imgSpacing)
            myDocument.Add(imgSpacing)

            ''BAYARAN

            table = New PdfPTable(3)
            table.WidthPercentage = 100
            table.SetWidths({30, 5, 65})

            cell = New PdfPCell()
            cetak = "Bayaran yang dikenakan"
            cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 10, Font.BOLD)))
            cell.Border = 0
            table.AddCell(cell)

            cell = New PdfPCell()
            cetak = ":"
            cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 10)))
            cell.Border = 0
            table.AddCell(cell)

            Dim rmAsas As Double
            Dim rmBM As Double
            Dim rmSJ As Double
            Dim rmTotal As Decimal

            ''getRMAsas
            strSQL = "SELECT setting_value_double FROM kpmkv_svmu_setting WHERE setting_parameter = 'BAYARAN' AND setting_value_string = 'ASAS'"
            rmAsas = oCommon.getFieldValue(strSQL)

            ''getRMBM
            strSQL = "SELECT setting_value_double FROM kpmkv_svmu_setting WHERE setting_parameter = 'BAYARAN' AND setting_value_string = 'BM'"
            rmBM = oCommon.getFieldValue(strSQL)

            ''getRMSJ
            strSQL = "SELECT setting_value_double FROM kpmkv_svmu_setting WHERE setting_parameter = 'BAYARAN' AND setting_value_string = 'SJ'"
            rmSJ = oCommon.getFieldValue(strSQL)

            ''getMPBM
            strSQL = "SELECT svmu_calon_id FROM kpmkv_svmu_calon WHERE svmu_no_permohonan = '" & lblNoPermohonan.Text & "' AND MataPelajaran = 'BM' AND StatusMP = '1'"
            strRet = oCommon.getFieldValue(strSQL)

            If strRet = "" Then

                rmBM = 0

            End If

            ''getMPSJ
            strSQL = "SELECT svmu_calon_id FROM kpmkv_svmu_calon WHERE svmu_no_permohonan = '" & lblNoPermohonan.Text & "' AND MataPelajaran = 'SJ' AND StatusMP = '1'"
            strRet = oCommon.getFieldValue(strSQL)

            If strRet = "" Then

                rmSJ = 0

            End If

            rmTotal = Format(rmAsas + rmBM + rmSJ, "0.00")

            cell = New PdfPCell()
            cetak = "RM" & rmTotal
            cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 10)))
            cell.Border = 0
            table.AddCell(cell)

            myDocument.Add(table)

            ''BAYARAN

            table = New PdfPTable(1)
            table.WidthPercentage = 100
            table.SetWidths({100})

            cell = New PdfPCell()
            cetak = "Saya telah menyemak maklumat peribadi dan maklumat mata pelajaran yang didaftar. saya mengesahkan maklumat berkenaan adalah betul."
            cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 10)))
            cell.Border = 0
            table.AddCell(cell)
            myDocument.Add(table)

            myDocument.Add(imgSpacing)
            myDocument.Add(imgSpacing)
            myDocument.Add(imgSpacing)
            myDocument.Add(imgSpacing)
            myDocument.Add(imgSpacing)

            ''TANDATANGAN DAN TARIKH

            table = New PdfPTable(3)
            table.WidthPercentage = 100
            table.SetWidths({30, 40, 30})

            cell = New PdfPCell()
            cetak = "____________________"
            myPara001 = New Paragraph(cetak, FontFactory.GetFont("Arial", 12))
            myPara001.Alignment = Element.ALIGN_CENTER
            cell.AddElement(myPara001)
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
            cell.Border = 0
            cell.Padding = 0
            table.AddCell(cell)

            cell = New PdfPCell()
            cetak = ""
            myPara001 = New Paragraph(cetak, FontFactory.GetFont("Arial", 12))
            myPara001.Alignment = Element.ALIGN_LEFT
            cell.AddElement(myPara001)
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
            cell.Border = 0
            cell.Padding = 0
            table.AddCell(cell)

            cell = New PdfPCell()
            cetak = "____________________"
            myPara001 = New Paragraph(cetak, FontFactory.GetFont("Arial", 12))
            myPara001.Alignment = Element.ALIGN_CENTER
            cell.AddElement(myPara001)
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
            cell.Border = 0
            cell.Padding = 0
            table.AddCell(cell)

            myDocument.Add(table)

            table = New PdfPTable(3)
            table.WidthPercentage = 100
            table.SetWidths({30, 40, 30})

            cell = New PdfPCell()
            cetak = "Tandatangan Calon"
            myPara001 = New Paragraph(cetak, FontFactory.GetFont("Arial", 10))
            myPara001.Alignment = Element.ALIGN_CENTER
            cell.AddElement(myPara001)
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
            cell.Border = 0
            cell.Padding = 0
            table.AddCell(cell)

            cell = New PdfPCell()
            cetak = ""
            myPara001 = New Paragraph(cetak, FontFactory.GetFont("Arial", 12))
            myPara001.Alignment = Element.ALIGN_LEFT
            cell.AddElement(myPara001)
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
            cell.Border = 0
            cell.Padding = 0
            table.AddCell(cell)

            cell = New PdfPCell()
            cetak = "Tarikh Permohonan"
            myPara001 = New Paragraph(cetak, FontFactory.GetFont("Arial", 10))
            myPara001.Alignment = Element.ALIGN_CENTER
            cell.AddElement(myPara001)
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
            cell.Border = 0
            cell.Padding = 0
            table.AddCell(cell)

            myDocument.Add(table)

            ''TANDATANGAN DAN TARIKH

            myDocument.Add(imgSpacing)
            myDocument.Add(imgSpacing)


            table = New PdfPTable(1)
            myPara001 = New Paragraph()
            table.WidthPercentage = 100
            table.SetWidths({100})
            table.DefaultCell.Border = 0

            cell = New PdfPCell()
            cetak = "Lembaga Peperiksaan" & Environment.NewLine
            cetak += "Kementerian Pendidikan Malaysia"
            myPara001 = New Paragraph(cetak, FontFactory.GetFont("Arial", 12, Font.BOLD))
            myPara001.Alignment = Element.ALIGN_CENTER
            cell.AddElement(myPara001)
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
            cell.Border = 0
            cell.Padding = 0
            table.AddCell(cell)
            myDocument.Add(table)

            ''PERINGATAN KEPADA CALON

            myDocument.NewPage()

            table = New PdfPTable(1)
            myPara001 = New Paragraph()
            table.WidthPercentage = 100
            table.SetWidths({100})
            table.DefaultCell.Border = 0

            cell = New PdfPCell()
            cetak = "Peringatan Kepada Calon"
            myPara001 = New Paragraph(cetak, FontFactory.GetFont("Arial", 12, Font.BOLD))
            myPara001.Alignment = Element.ALIGN_CENTER
            cell.AddElement(myPara001)
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
            cell.Border = 0
            cell.Padding = 0
            table.AddCell(cell)
            myDocument.Add(table)

            myDocument.Add(imgSpacing)
            myDocument.Add(imgSpacing)

            table = New PdfPTable(2)
            table.WidthPercentage = 100
            table.SetWidths({5, 95})

            cell = New PdfPCell()
            cetak = "1."
            cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 10)))
            cell.Border = 0
            table.AddCell(cell)

            cell = New PdfPCell()
            cetak = "Perlaksanaan Ujian Lisan Khas Calon Persendirian (ULKCP) bagi calon persendirian akan dikendalikan oleh Jabatan Pendidikan Negeri (JPN). Tarikh dan tempat akan ditentukan oleh pihak JPN."
            cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 10, Font.BOLD)))
            cell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED
            cell.Border = 0
            table.AddCell(cell)

            myDocument.Add(table)

            myDocument.Add(imgSpacing)

            table = New PdfPTable(2)
            table.WidthPercentage = 100
            table.SetWidths({5, 95})

            cell = New PdfPCell()
            cetak = "2."
            cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 10)))
            cell.Border = 0
            table.AddCell(cell)

            cell = New PdfPCell()
            cetak = "Calon hendaklah mencetak dan menghantar/mengepos dokumen berikut dalam tempoh 7 hari selepas bayaran pendaftaran diselesaikan ke Jabatan Pendidikan Negeri (JPN) tempat calon memilih untuk menduduki peperiksaan:" & Environment.NewLine
            cetak += "i. Salinan Kenyataan Semakan;" & Environment.NewLine
            cetak += "ii. Salinan Resit bayaran (perbankan elektronik); dan" & Environment.NewLine
            cetak += "iii. 2 keping sampul Pos Laju Prabayar atau Pos Ekspres bersaiz A3 beralamat sendiri."
            cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 10)))
            cell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED
            cell.Border = 0
            table.AddCell(cell)

            myDocument.Add(table)

            myDocument.Add(imgSpacing)

            table = New PdfPTable(2)
            table.WidthPercentage = 100
            table.SetWidths({5, 95})

            cell = New PdfPCell()
            cetak = ""
            cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 10)))
            cell.Border = 0
            table.AddCell(cell)

            cell = New PdfPCell()
            cetak = "Kegagalan calon mengemukakan dokumen di perkara 1, akan menyebabkan calon tidak menerima maklumat penting berkaitan peperiksaan." & Environment.NewLine & Environment.NewLine
            cetak += "Alamat JPN boleh didapati di portal Lembaga Peperiksaan (http://lp.moe.gov.my)"
            cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 10, Font.BOLD)))
            cell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED
            cell.Border = 0
            table.AddCell(cell)

            myDocument.Add(table)

            myDocument.Add(imgSpacing)

            table = New PdfPTable(2)
            table.WidthPercentage = 100
            table.SetWidths({5, 95})

            cell = New PdfPCell()
            cetak = "3."
            cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 10)))
            cell.Border = 0
            table.AddCell(cell)

            cell = New PdfPCell()
            cetak = "Proses pendaftaran peperiksaan oleh calon hanya lengkap selepas JPN mengesahkan pendaftaran. Calon boleh menyemak status pengesahan pendaftaran peperiksaan dan mencetak Pernyataan Pendaftaran Peperiksaan (PPP) selepas satu (1) bulan dari tarikh tutup pendaftaran."
            cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 10)))
            cell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_JUSTIFIED
            cell.Border = 0
            table.AddCell(cell)

            myDocument.Add(table)

            ''PERINGATAN KEPADA CALON

            myDocument.Close()

            HttpContext.Current.Response.Write(myDocument)
            HttpContext.Current.Response.End()

        Catch ex As Exception

        End Try

    End Sub

    Public Class JSONdata

        <JsonConverter(GetType(SingleOrArrayConverter(Of JSONdetails)))>
        Public Property data As List(Of JSONdetails)

    End Class

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

End Class