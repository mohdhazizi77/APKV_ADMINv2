Imports System.Data.SqlClient
Imports System.IO
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports System.Security.Cryptography

Public Class borang_kawalan_svm1

    Inherits System.Web.UI.UserControl
    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""


    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                kpmkv_negeri_list()


                kpmkv_jenis_list()


                kpmkv_kolej_list()


                kpmkv_tahun_list()
                ddlTahun.Text = Now.Year

                kpmkv_kelas_list()

                kpmkv_kodkursus_list()

            End If

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message
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



        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
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

    Private Sub kpmkv_kodkursus_list()

        strSQL = "SELECT kpmkv_kursus.KodKursus, kpmkv_kursus.KursusID FROM kpmkv_kursus_kolej LEFT OUTER JOIN"
        strSQL += " kpmkv_kursus ON kpmkv_kursus_kolej.KursusID = kpmkv_kursus.KursusID"
        strSQL += " WHERE kpmkv_kursus_kolej.KolejRecordID='" & ddlKolej.SelectedValue & "' AND kpmkv_kursus.Tahun='" & ddlTahun.SelectedValue & "' "
        strSQL += " AND kpmkv_kursus.Sesi='" & chkSesi.SelectedValue & "' GROUP BY kpmkv_kursus.KodKursus,kpmkv_kursus.KursusID"
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

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub kpmkv_kelas_list()
        strSQL = " SELECT kpmkv_kelas.NamaKelas, kpmkv_kelas.KelasID FROM kpmkv_kelas_kursus LEFT OUTER JOIN kpmkv_kelas ON kpmkv_kelas_kursus.KelasID = kpmkv_kelas.KelasID LEFT OUTER JOIN"
        strSQL += " kpmkv_kursus ON kpmkv_kelas_kursus.KursusID = kpmkv_kursus.KursusID"
        strSQL += " WHERE kpmkv_kelas.KolejRecordID='" & ddlKolej.SelectedValue & "' AND kpmkv_kelas_kursus.KursusID= '" & ddlKodKursus.SelectedValue & "' AND kpmkv_kursus.Tahun= '" & ddlTahun.SelectedValue & "' ORDER BY  kpmkv_kelas.NamaKelas"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlNamaKelas.DataSource = ds
            ddlNamaKelas.DataTextField = "NamaKelas"
            ddlNamaKelas.DataValueField = "KelasID"
            ddlNamaKelas.DataBind()

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        lblMsg.Text = ""
        strRet = BindData(datRespondent)
        status_cetakan()
    End Sub

    Private Sub status_cetakan()

        For i As Integer = 0 To datRespondent.Rows.Count - 1

            Dim strkey As String = datRespondent.DataKeys(i).Value.ToString

            strSQL = "SELECT statusID FROM kpmkv_status_cetak WHERE PelajarID = '" & strkey & "'"
            Dim IDExist As String = oCommon.getFieldValue(strSQL)

            Dim lblStatusCetak As Label = datRespondent.Rows(i).FindControl("lblStatusCetak")

            If Not IDExist = "" Then

                strSQL = "SELECT svm FROM kpmkv_status_cetak WHERE statusID = '" & IDExist & "'"
                Dim svm As String = oCommon.getFieldValue(strSQL)

                If svm = "1" Then

                    lblStatusCetak.Text = "Telah Dicetak"

                End If

            End If

        Next

    End Sub

    Private Sub btnKemaskini_Click(sender As Object, e As EventArgs) Handles btnKemaskini.Click

        lblMsg.Text = ""

        For i As Integer = 0 To datRespondent.Rows.Count - 1

            Dim strkey As String = datRespondent.DataKeys(i).Value.ToString

            strSQL = " SELECT RunningNo FROM kpmkv_sijil_RunningNo WHERE PelajarID = '" & strkey & "'"
            Dim RunNo As String = oCommon.getFieldValue(strSQL)

            If RunNo = "" Then

                ''isbmtahun
                strSQL = " SELECT isBMTahun FROM kpmkv_pelajar WHERE PelajarID = '" & strkey & "'"
                Dim strIsBMTahun As String = oCommon.getFieldValue(strSQL)

                ''kod
                strSQL = " SELECT Kod FROM kpmkv_tahun WHERE Tahun = '" & strIsBMTahun & "'"
                Dim strKod As String = oCommon.getFieldValue(strSQL)

                ''digit
                strSQL = " SELECT RunningNoDigit FROM kpmkv_tahun WHERE Tahun = '" & strIsBMTahun & "'"
                Dim strRunNoDigit As String = oCommon.getFieldValue(strSQL)

                ''lastrunningno
                strSQL = " SELECT LastRunningNo FROM kpmkv_tahun WHERE Tahun = '" & strIsBMTahun & "'"
                Dim strLastRunNo As Integer = oCommon.getFieldValue(strSQL)
                strLastRunNo = strLastRunNo + 1

                ''INSERT 
                strSQL = " INSERT INTO kpmkv_sijil_RunningNo (PelajarID, RunningNo, TotalPrint) VALUES ('" & strkey & "', '" & strKod & "   " & strLastRunNo.ToString("D" & strRunNoDigit) & "', 0)"
                strRet = oCommon.ExecuteSQL(strSQL)

                ''UPDATE
                strSQL = " UPDATE kpmkv_tahun SET LastRunningNo = '" & strLastRunNo & "' WHERE Tahun = '" & strIsBMTahun & "'"
                strRet = oCommon.ExecuteSQL(strSQL)

            End If

        Next

        strRet = BindData(datRespondent)

    End Sub

    Private Sub tbl_menu_check()

        Dim str As String
        For i As Integer = 0 To datRespondent.Rows.Count - 1
            Dim row As GridViewRow = datRespondent.Rows(0)
            Dim cb As CheckBox = datRespondent.Rows(i).FindControl("chkSelect")

            str = datRespondent.DataKeys(i).Value.ToString
            Dim strMykad As String = CType(datRespondent.Rows(i).FindControl("Mykad"), Label).Text

            strSQL = "SELECT KelasID FROM kpmkv_pelajar Where Mykad='" & strMykad & "' AND IsDeleted='N' AND KelasID IS NOT NULL"
            If oCommon.isExist(strSQL) = False Then
                cb.Checked = True
            End If
        Next

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
                lblMsg.Text = "Rekod tidak dijumpai!"
            Else
                divMsg.Attributes("class") = "info"
                lblMsg.Text = "Jumlah Rekod#: " & myDataSet.Tables(0).Rows.Count
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
        Dim strWhere As String = ""
        Dim strOrder As String = " ORDER BY kpmkv_pelajar.AngkaGiliran ASC"

        '--not deleted
        tmpSQL = "SELECT kpmkv_pelajar.PelajarID, kpmkv_pelajar.Tahun, kpmkv_pelajar.Semester, kpmkv_pelajar.Sesi, kpmkv_pelajar.Nama, kpmkv_pelajar.MYKAD, kpmkv_pelajar.AngkaGiliran, "
        tmpSQL += " kpmkv_kluster.NamaKluster, kpmkv_kursus.NamaKursus, kpmkv_pelajar.Kaum, kpmkv_pelajar.Jantina, kpmkv_pelajar.Agama, kpmkv_status.Status, kpmkv_kelas.NamaKelas,"
        tmpSQL += " kpmkv_svm.PNGKA ,kpmkv_svm.PNGKV,"
        tmpSQL += " kpmkv_sijil_RunningNo.RunningNo"
        tmpSQL += " FROM  kpmkv_pelajar LEFT OUTER JOIN kpmkv_kursus ON kpmkv_pelajar.KursusID = kpmkv_kursus.KursusID LEFT OUTER JOIN kpmkv_kluster ON kpmkv_kursus.KlusterID=kpmkv_kluster.KlusterID"
        tmpSQL += " LEFT OUTER JOIN kpmkv_status ON kpmkv_pelajar.StatusID = kpmkv_status.StatusID LEFT OUTER JOIN kpmkv_kelas ON kpmkv_pelajar.KelasID = kpmkv_kelas.KelasID"
        tmpSQL += " LEFT OUTER JOIN kpmkv_svm ON kpmkv_svm.PelajarID = kpmkv_pelajar.PelajarID"
        tmpSQL += " LEFT OUTER JOIN kpmkv_sijil_RunningNo ON kpmkv_sijil_RunningNo.PelajarID = kpmkv_pelajar.PelajarID"
        strWhere = " WHERE kpmkv_pelajar.IsDeleted='N' AND kpmkv_pelajar.StatusID='2' AND kpmkv_pelajar.KolejRecordID='" & ddlKolej.SelectedValue & "' AND kpmkv_pelajar.Semester ='4'"

        '--tahun
        If Not ddlTahun.Text = "" Then
            strWhere += " AND kpmkv_pelajar.Tahun ='" & ddlTahun.Text & "'"
        End If

        '--sesi
        If Not chkSesi.Text = "" Then
            strWhere += " AND kpmkv_pelajar.Sesi ='" & chkSesi.Text & "'"
        End If
        '--kodkursus
        If Not ddlKodKursus.Text = "" Then
            strWhere += " AND kpmkv_pelajar.KursusID ='" & ddlKodKursus.SelectedValue & "'"
        End If
        '--NamaKelas
        If Not ddlNamaKelas.Text = "" Then
            strWhere += " AND kpmkv_pelajar.KelasID ='" & ddlNamaKelas.SelectedValue & "'"
        End If
        '--txtNama
        If Not txtNama.Text.Length = 0 Then
            strWhere += " AND kpmkv_pelajar.Nama LIKE '%" & oCommon.FixSingleQuotes(txtNama.Text) & "%'"
        End If

        '--txtMYKAD
        If Not txtMYKAD.Text.Length = 0 Then
            strWhere += " AND kpmkv_pelajar.MYKAD='" & oCommon.FixSingleQuotes(txtMYKAD.Text) & "'"
        End If

        '--txtAngkaGiliran
        If Not txtAngkaGiliran.Text.Length = 0 Then
            strWhere += " AND kpmkv_pelajar.AngkaGiliran LIKE '%" & oCommon.FixSingleQuotes(txtAngkaGiliran.Text) & "%'"
        End If
        If Not ddlStatus.SelectedValue = "SEMUA" Then

            If ddlStatus.SelectedValue = "SETARA" Then
                strWhere += " AND kpmkv_SVM.LayakSVM = '1' 
                            AND kpmkv_SVM.GredBMSetara IN ('C', 'C+', 'B-', 'B', 'B+', 'A-', 'A', 'A+')"

            ElseIf ddlStatus.SelectedValue = "TIDAK SETARA" Then
                strWhere += " AND kpmkv_SVM.LayakSVM = '1' 
                            AND kpmkv_SVM.GredBMSetara IN ('D', 'E', 'G')"

            ElseIf ddlStatus.SelectedValue = "LAYAK" Then
                strWhere += " AND kpmkv_SVM.LayakSVM = '1'"

            ElseIf ddlStatus.SelectedValue = "TIDAK LAYAK" Then
                strWhere += " AND kpmkv_SVM.LayakSVM IS NULL"
            End If

        End If
        getSQL = tmpSQL & strWhere & strOrder
        ''--debug
        'Response.Write(getSQL)

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

    Protected Sub chkSesi_SelectedIndexChanged(sender As Object, e As EventArgs) Handles chkSesi.SelectedIndexChanged
        kpmkv_kodkursus_list()
        kpmkv_kelas_list()
    End Sub

    Private Sub ddlKodKursus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlKodKursus.SelectedIndexChanged
        kpmkv_kelas_list()
    End Sub

    Private Function Encrypt(qrString As String) As String

        Dim encryptionKey As String = "MAKV2SPBNI99212"
        Dim clearBytes As Byte() = Encoding.Unicode.GetBytes(qrString)

        Using encryptor As Aes = Aes.Create()

            Dim rfc As New Rfc2898DeriveBytes(encryptionKey, New Byte() {&H49, &H76, &H61, &H6E, &H20, &H4D, &H65, &H64, &H76, &H65, &H64, &H65, &H76})
            encryptor.Key = rfc.GetBytes(32)
            encryptor.IV = rfc.GetBytes(16)

            Using ms As New MemoryStream()

                Using cs As New CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write)

                    cs.Write(clearBytes, 0, clearBytes.Length)
                    cs.Close()

                End Using

                qrString = Convert.ToString(ms.ToArray())

            End Using

        End Using

        Return qrString

    End Function

    Private Sub ddlNegeri_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlNegeri.SelectedIndexChanged
        kpmkv_jenis_list()
        kpmkv_kolej_list()
    End Sub

    Private Sub ddlJenis_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlJenis.SelectedIndexChanged
        kpmkv_kolej_list()

    End Sub

    Protected Sub btnBorangKawalan_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnBorangKawalan.Click
        Dim myDocument As New Document(PageSize.A4.Rotate)

        Try
            HttpContext.Current.Response.ContentType = "application/pdf"
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=BorangKawalanSijilVokMalaysia.pdf")
            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache)

            PdfWriter.GetInstance(myDocument, HttpContext.Current.Response.OutputStream)

            myDocument.Open()

            ''--draw spacing
            Dim imgdrawSpacing As String = Server.MapPath("~/img/spacing.png")
            Dim imgSpacing As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(imgdrawSpacing)
            imgSpacing.Alignment = iTextSharp.text.Image.LEFT_ALIGN  'left
            imgSpacing.Border = 0

            strSQL = "SELECT nama FROM kpmkv_kolej WHERE RecordID='" & ddlKolej.SelectedValue & "'"
            Dim strNamaKolej = oCommon.getFieldValue(strSQL)

            Dim table As New PdfPTable(1)
            Dim myPara001 As New Paragraph()
            table.WidthPercentage = 103
            table.SetWidths({100})
            table.DefaultCell.Border = 0

            Dim cell = New PdfPCell()
            Dim cetak = "LEMBAGA PEPERIKSAAN"
            myPara001 = New Paragraph(cetak, FontFactory.GetFont("Arial", 9))
            myPara001.Alignment = Element.ALIGN_CENTER
            cell.AddElement(myPara001)
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
            cell.Border = 0
            cell.Padding = 0
            table.AddCell(cell)
            myDocument.Add(table)

            table = New PdfPTable(1)
            myPara001 = New Paragraph()
            table.WidthPercentage = 103
            table.SetWidths({100})
            table.DefaultCell.Border = 0

            cell = New PdfPCell()
            cetak = "KEMENTERIAN PENDIDIKAN MALAYSIA"
            myPara001 = New Paragraph(cetak, FontFactory.GetFont("Arial", 9))
            myPara001.Alignment = Element.ALIGN_CENTER
            cell.AddElement(myPara001)
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
            cell.Border = 0
            cell.Padding = 0
            table.AddCell(cell)
            myDocument.Add(table)

            table = New PdfPTable(1)
            myPara001 = New Paragraph()
            table.WidthPercentage = 103
            table.SetWidths({100})
            table.DefaultCell.Border = 0

            cell = New PdfPCell()

            If ddlStatus.Text = "TIDAK LAYAK" Then
                cetak = "SIJIL VOKASIONAL MALAYSIA (PERNYATAAN KEPUTUSAN)"
            Else
                cetak = "SIJIL VOKASIONAL MALAYSIA"
            End If

            myPara001 = New Paragraph(cetak, FontFactory.GetFont("Arial", 9))
            myPara001.Alignment = Element.ALIGN_CENTER
            cell.AddElement(myPara001)
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
            cell.Border = 0
            cell.Padding = 0
            table.AddCell(cell)
            myDocument.Add(table)

            table = New PdfPTable(1)
            myPara001 = New Paragraph()
            table.WidthPercentage = 103
            table.SetWidths({100})
            table.DefaultCell.Border = 0

            cell = New PdfPCell()
            cetak = "TAHUN " & Now.Year
            myPara001 = New Paragraph(cetak, FontFactory.GetFont("Arial", 9))
            myPara001.Alignment = Element.ALIGN_CENTER
            cell.AddElement(myPara001)
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
            cell.Border = 0
            cell.Padding = 0
            table.AddCell(cell)
            myDocument.Add(table)

            table = New PdfPTable(1)
            myPara001 = New Paragraph()
            table.WidthPercentage = 103
            table.SetWidths({100})
            table.DefaultCell.Border = 0

            cell = New PdfPCell()
            cetak = "INSTITUSI   : " & strNamaKolej & "," & ddlNegeri.SelectedValue
            myPara001 = New Paragraph(cetak, FontFactory.GetFont("Arial", 9))
            myPara001.Alignment = Element.ALIGN_LEFT
            cell.AddElement(myPara001)
            cell.VerticalAlignment = PdfPCell.ALIGN_LEFT
            cell.Border = 0
            table.AddCell(cell)
            myDocument.Add(table)

            myDocument.Add(imgSpacing)
            myDocument.Add(imgSpacing)
            '*********************************Student list*********************************************'

            table = New PdfPTable(7)
            myPara001 = New Paragraph()
            table.WidthPercentage = 103
            table.SetWidths({3, 30, 10, 10, 30, 10, 7})
            table.DefaultCell.Border = 1

            cell = New PdfPCell()
            cetak = "BIL"
            myPara001 = New Paragraph(cetak, FontFactory.GetFont("Arial", 9))
            myPara001.Alignment = Element.ALIGN_CENTER
            cell.AddElement(myPara001)
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
            table.AddCell(cell)
            myDocument.Add(table)

            cell = New PdfPCell()
            cetak = "NAMA"
            myPara001 = New Paragraph(cetak, FontFactory.GetFont("Arial", 9))
            myPara001.Alignment = Element.ALIGN_CENTER
            cell.AddElement(myPara001)
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
            table.AddCell(cell)
            myDocument.Add(table)

            cell = New PdfPCell()
            cetak = "NO KP"
            myPara001 = New Paragraph(cetak, FontFactory.GetFont("Arial", 9))
            myPara001.Alignment = Element.ALIGN_CENTER
            cell.AddElement(myPara001)
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
            table.AddCell(cell)
            myDocument.Add(table)

            cell = New PdfPCell()
            cetak = "ANGKA GILIRAN"
            myPara001 = New Paragraph(cetak, FontFactory.GetFont("Arial", 9))
            myPara001.Alignment = Element.ALIGN_CENTER
            cell.AddElement(myPara001)
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
            table.AddCell(cell)
            myDocument.Add(table)

            cell = New PdfPCell()
            cetak = "KURSUS"
            myPara001 = New Paragraph(cetak, FontFactory.GetFont("Arial", 9))
            myPara001.Alignment = Element.ALIGN_CENTER
            cell.AddElement(myPara001)
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
            table.AddCell(cell)
            myDocument.Add(table)

            cell = New PdfPCell()
            cetak = "NO SIRI"
            myPara001 = New Paragraph(cetak, FontFactory.GetFont("Arial", 9))
            myPara001.Alignment = Element.ALIGN_CENTER
            cell.AddElement(myPara001)
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
            table.AddCell(cell)
            myDocument.Add(table)

            cell = New PdfPCell()
            cetak = "TANDA TANGAN"
            myPara001 = New Paragraph(cetak, FontFactory.GetFont("Arial", 9))
            myPara001.Alignment = Element.ALIGN_CENTER
            cell.AddElement(myPara001)
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
            table.AddCell(cell)
            myDocument.Add(table)


            '***************get student data*******************'

            strSQL = "SELECT kpmkv_pelajar.PelajarID,kpmkv_pelajar.MYKAD,kpmkv_pelajar.Nama, kpmkv_pelajar.AngkaGiliran, "
            strSQL += " kpmkv_kursus.NamaKursus,kpmkv_sijil_runningNo.runningNo"
            strSQL += " FROM  kpmkv_pelajar LEFT OUTER JOIN kpmkv_kursus On kpmkv_pelajar.KursusID = kpmkv_kursus.KursusID "
            strSQL += " LEFT OUTER JOIN kpmkv_svm On kpmkv_svm.PelajarID = kpmkv_pelajar.PelajarID"
            strSQL += " LEFT OUTER JOIN kpmkv_sijil_runningNo ON kpmkv_pelajar.PelajarID=kpmkv_sijil_runningNo.PelajarID "
            strSQL += " WHERE kpmkv_pelajar.IsDeleted='N' AND kpmkv_pelajar.StatusID='2' "
            strSQL += " And kpmkv_pelajar.KolejRecordID ='" & ddlKolej.SelectedValue & "' "
            strSQL += " AND kpmkv_pelajar.Semester ='4'"


            If Not ddlTahun.Text = "" Then
                strSQL += " AND kpmkv_pelajar.Tahun ='" & ddlTahun.Text & "'"
            End If

            If Not chkSesi.Text = "" Then
                strSQL += " AND kpmkv_pelajar.Sesi ='" & chkSesi.Text & "'"
            End If

            If Not ddlKodKursus.Text = "" Then
                strSQL += " AND kpmkv_pelajar.KursusID ='" & ddlKodKursus.SelectedValue & "'"
            End If

            If Not ddlNamaKelas.Text = "" Then
                strSQL += " AND kpmkv_pelajar.KelasID ='" & ddlNamaKelas.SelectedValue & "'"
            End If

            If Not txtNama.Text.Length = 0 Then
                strSQL += " AND kpmkv_pelajar.Nama LIKE '%" & oCommon.FixSingleQuotes(txtNama.Text) & "%'"
            End If

            If Not txtMYKAD.Text.Length = 0 Then
                strSQL += " AND kpmkv_pelajar.MYKAD='" & oCommon.FixSingleQuotes(txtMYKAD.Text) & "'"
            End If

            If Not txtAngkaGiliran.Text.Length = 0 Then
                strSQL += " AND kpmkv_pelajar.AngkaGiliran LIKE '%" & oCommon.FixSingleQuotes(txtAngkaGiliran.Text) & "%'"
            End If
            If Not ddlStatus.SelectedValue = "SEMUA" Then

                If ddlStatus.SelectedValue = "SETARA" Then
                    strSQL += " AND kpmkv_SVM.LayakSVM = '1' 
                            AND kpmkv_SVM.GredBMSetara IN ('C', 'C+', 'B-', 'B', 'B+', 'A-', 'A', 'A+')"

                ElseIf ddlStatus.SelectedValue = "TIDAK SETARA" Then
                    strSQL += " AND kpmkv_SVM.LayakSVM = '1' 
                            AND kpmkv_SVM.GredBMSetara IN ('D', 'E', 'G')"

                ElseIf ddlStatus.SelectedValue = "LAYAK" Then
                    strSQL += " AND kpmkv_SVM.LayakSVM = '1'"

                ElseIf ddlStatus.SelectedValue = "TIDAK LAYAK" Then
                    strSQL += " AND kpmkv_SVM.LayakSVM IS NULL"
                End If

            End If

            strSQL += " ORDER BY kpmkv_pelajar.AngkaGiliran ASC"

            strRet = oCommon.ExecuteSQL(strSQL)

            Dim sqlDA As New SqlDataAdapter(strSQL, objConn)
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1

                Dim cb As CheckBox = datRespondent.Rows(i).FindControl("chkSelect")

                If cb.Checked = True Then

                    Dim strkey As String = ds.Tables(0).Rows(i).Item(0).ToString

                    Dim strMykad As String = ds.Tables(0).Rows(i).Item(1).ToString
                    Dim strName As String = ds.Tables(0).Rows(i).Item(2).ToString
                    Dim strAg As String = ds.Tables(0).Rows(i).Item(3).ToString
                    Dim strKursus As String = ds.Tables(0).Rows(i).Item(4).ToString
                    Dim strNoSiri As String = ds.Tables(0).Rows(i).Item(5).ToString

                    table = New PdfPTable(7)
                    myPara001 = New Paragraph()
                    table.WidthPercentage = 103
                    table.SetWidths({3, 30, 10, 10, 30, 10, 7})
                    table.DefaultCell.Border = 1

                    cell = New PdfPCell()
                    cetak = i + 1
                    cetak += Environment.NewLine & " "
                    myPara001 = New Paragraph(cetak, FontFactory.GetFont("Arial", 9))
                    myPara001.Alignment = Element.ALIGN_CENTER
                    cell.AddElement(myPara001)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    table.AddCell(cell)
                    myDocument.Add(table)

                    cell = New PdfPCell()
                    cetak = strName
                    cetak += Environment.NewLine & " "
                    myPara001 = New Paragraph(cetak, FontFactory.GetFont("Arial", 9))
                    myPara001.Alignment = Element.ALIGN_LEFT
                    cell.AddElement(myPara001)
                    cell.VerticalAlignment = PdfPCell.ALIGN_LEFT
                    table.AddCell(cell)
                    myDocument.Add(table)

                    cell = New PdfPCell()
                    cetak = strMykad
                    cetak += Environment.NewLine & " "
                    myPara001 = New Paragraph(cetak, FontFactory.GetFont("Arial", 9))
                    myPara001.Alignment = Element.ALIGN_CENTER
                    cell.AddElement(myPara001)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    table.AddCell(cell)
                    myDocument.Add(table)

                    cell = New PdfPCell()
                    cetak = strAg
                    cetak += Environment.NewLine & " "
                    myPara001 = New Paragraph(cetak, FontFactory.GetFont("Arial", 9))
                    myPara001.Alignment = Element.ALIGN_CENTER
                    cell.AddElement(myPara001)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    table.AddCell(cell)
                    myDocument.Add(table)

                    cell = New PdfPCell()
                    cetak = strKursus
                    cetak += Environment.NewLine & " "
                    myPara001 = New Paragraph(cetak, FontFactory.GetFont("Arial", 9))
                    myPara001.Alignment = Element.ALIGN_LEFT
                    cell.AddElement(myPara001)
                    cell.VerticalAlignment = PdfPCell.ALIGN_LEFT
                    table.AddCell(cell)
                    myDocument.Add(table)

                    cell = New PdfPCell()
                    cetak = strNoSiri
                    cetak += Environment.NewLine & " "
                    myPara001 = New Paragraph(cetak, FontFactory.GetFont("Arial", 9))
                    myPara001.Alignment = Element.ALIGN_CENTER
                    cell.AddElement(myPara001)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    table.AddCell(cell)
                    myDocument.Add(table)

                    cell = New PdfPCell()
                    cetak = ""
                    myPara001 = New Paragraph(cetak, FontFactory.GetFont("Arial", 9))
                    myPara001.Alignment = Element.ALIGN_CENTER
                    cell.AddElement(myPara001)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    table.AddCell(cell)
                    myDocument.Add(table)

                End If
            Next

            myDocument.Close()

            HttpContext.Current.Response.Write(myDocument)
            HttpContext.Current.Response.End()

        Catch ex As Exception

        End Try

    End Sub

End Class