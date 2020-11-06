Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization
Imports RKLib.ExportData

Imports iTextSharp.text
Imports iTextSharp.text.pdf
Public Class markah_PA_kertas
    Inherits System.Web.UI.UserControl
    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                lblMsg.Text = ""
                lblMsg2.Text = ""

                lblPemeriksa.Text = Request.QueryString("PemeriksaID")

                strSQL = " SELECT Tahun, Sesi, Semester, KodKolej, KodKursus FROM  kpmkv_pemeriksa "
                strSQL += " WHERE kpmkv_pemeriksa.PemeriksaID='" & lblPemeriksa.Text & "'"
                strRet = oCommon.getFieldValueEx(strSQL)
                'Response.Write(strSQL)
                ''--get pemeriksa info
                Dim ar_pemeriksa As Array
                ar_pemeriksa = strRet.Split("|")

                lblTahun.Text = ar_pemeriksa(0)
                lblSesi.Text = ar_pemeriksa(1)
                lblSemester.Text = ar_pemeriksa(2)
                lblKodPusat.Text = ar_pemeriksa(3)
                lblKodKursus.Text = ar_pemeriksa(4)

                strSQL = "SELECT RecordID FROM kpmkv_kolej WHERE Kod='" & lblKodPusat.Text & "'"
                lblKolejRecorID.Text = oCommon.getFieldValue(strSQL)

                strRet = BindData(datRespondent)
                ' ValidateGridView()
            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message
            lblMsg2.Text = ex.Message

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
                lblMsg.Text = "Tiada rekod pelajar."
                lblMsg2.Text = "Tiada rekod pelajar."

            Else
                divMsg.Attributes("class") = "info"
                lblMsg.Text = "Jumlah rekod#:" & myDataSet.Tables(0).Rows.Count
            End If

            gvTable.DataSource = myDataSet
            gvTable.DataBind()
            objConn.Close()

        Catch ex As Exception
            lblMsg.Text = "Error:" & ex.Message
            lblMsg2.Text = "Error." & ex.Message
            Return False
        End Try

        Return True

    End Function
    Private Function getSQL() As String
        Dim tmpSQL As String = ""
        Dim strWhere As String = ""
        Dim strOrder As String = " ORDER BY kpmkv_pelajar.Nama ASC"

        tmpSQL = "SELECT kpmkv_pelajar.PelajarID,  kpmkv_pelajar.Nama, kpmkv_pelajar.MYKAD, kpmkv_pelajar.AngkaGiliran, kpmkv_kursus.KodKursus, "
        tmpSQL += " kpmkv_pelajar_markah.A_Teori1,kpmkv_pelajar_markah.CatatanPA"
        tmpSQL += " FROM  kpmkv_pelajar_markah LEFT OUTER JOIN kpmkv_pelajar ON kpmkv_pelajar_markah.PelajarID = kpmkv_pelajar.PelajarID"
        tmpSQL += " LEFT OUTER Join kpmkv_kursus ON kpmkv_pelajar.KursusID = kpmkv_kursus.KursusID"
        strWhere = " WHERE kpmkv_pelajar.IsDeleted='N' AND kpmkv_pelajar.StatusID='2' AND kpmkv_pelajar.Semester ='" & lblSemester.Text & "'"
        strWhere += " AND kpmkv_pelajar.KolejRecordID ='" & lblKolejRecorID.Text & "'"
        strWhere += " AND kpmkv_kursus.KodKursus ='" & lblKodKursus.Text & "'"
        strWhere += " AND kpmkv_pelajar.Sesi ='" & lblSesi.Text & "'"
        strWhere += " AND kpmkv_pelajar.Tahun ='" & lblTahun.Text & "'"

        getSQL = tmpSQL & strWhere & strOrder
        ''--debug
        'Response.Write(getSQL)

        Return getSQL

    End Function
    Private Sub datRespondent_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles datRespondent.PageIndexChanging
        datRespondent.PageIndex = e.NewPageIndex
        strRet = BindData(datRespondent)
        ' ValidateGridView()
    End Sub
    Private Sub datRespondent_SelectedIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSelectEventArgs) Handles datRespondent.SelectedIndexChanging
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
        lblMsg2.Text = ""

        Try
            If ValidateForm() = False Then
                lblMsg.Text = "Sila masukkan NOMBOR SAHAJA. 0-100"
                lblMsg2.Text = "Sila masukkan NOMBOR SAHAJA. 0-100"

                Exit Sub
            End If


            '--count no of modul
            Dim nCount As Integer = 0
            strSQL = "SELECT COUNT(kpmkv_modul.KodModul) as CModul "
            strSQL += " FROM kpmkv_modul LEFT OUTER JOIN"
            strSQL += " kpmkv_kursus ON kpmkv_modul.KursusID = kpmkv_kursus.KursusID"
            strSQL += " WHERE kpmkv_kursus.KodKursus='" & lblKodKursus.Text & "' AND kpmkv_modul.Tahun='" & lblTahun.Text & "'"
            strSQL += " AND kpmkv_modul.Sesi='" & lblSesi.Text & "' AND kpmkv_modul.semester='" & lblSemester.Text & "'"
            nCount = oCommon.getFieldValueInt(strSQL)

            For i As Integer = 0 To datRespondent.Rows.Count - 1
                Dim row As GridViewRow = datRespondent.Rows(i)
                Dim strPelajarID As Integer = datRespondent.DataKeys(i).Value.ToString
                Dim Kertas1 As TextBox = datRespondent.Rows(i).FindControl("A_Teori1")
                Dim CatatanPA As TextBox = datRespondent.Rows(i).FindControl("CatatanPA")

                'assign value to integer
                Dim strCatatan As String = ""
                Dim BM3 As String = Kertas1.Text

                If CatatanPA Is Nothing Then
                    strCatatan = ""
                Else
                    strCatatan = CatatanPA.Text
                End If



                If nCount = 2 Then
                    strSQL = "UPDATE kpmkv_pelajar_markah SET  A_Teori1='" & BM3 & "', A_Teori2='" & BM3 & "', CatatanPA='" & strCatatan & "'"
                    strSQL += " WHERE KolejRecordID='" & lblKolejRecorID.Text & "' AND PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                ElseIf nCount = 3 Then
                    strSQL = "UPDATE kpmkv_pelajar_markah SET  A_Teori1='" & BM3 & "', A_Teori2='" & BM3 & "',A_Teori3='" & BM3 & "', CatatanPA='" & strCatatan & "'"
                    strSQL += " WHERE KolejRecordID='" & lblKolejRecorID.Text & "' AND PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                ElseIf nCount = 4 Then
                    strSQL = "UPDATE kpmkv_pelajar_markah SET  A_Teori1='" & BM3 & "', A_Teori2='" & BM3 & "', A_Teori3='" & BM3 & "', A_Teori4='" & BM3 & "', CatatanPA='" & strCatatan & "'"
                    strSQL += " WHERE KolejRecordID='" & lblKolejRecorID.Text & "' AND PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                ElseIf nCount = 5 Then
                    strSQL = "UPDATE kpmkv_pelajar_markah SET  A_Teori1='" & BM3 & "', A_Teori2='" & BM3 & "',A_Teori3='" & BM3 & "', A_Teori4='" & BM3 & "',"
                    strSQL += " A_Teori5='" & BM3 & "', CatatanPA='" & strCatatan & "'"
                    strSQL += " WHERE KolejRecordID='" & lblKolejRecorID.Text & "' AND PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                ElseIf nCount = 6 Then
                    strSQL = "UPDATE kpmkv_pelajar_markah SET  A_Teori1='" & BM3 & "', A_Teori2='" & BM3 & "',A_Teori3='" & BM3 & "', A_Teori4='" & BM3 & "',"
                    strSQL += " A_Teori5='" & BM3 & "', A_Teori6='" & BM3 & "', CatatanPA='" & strCatatan & "'"
                    strSQL += " WHERE KolejRecordID='" & lblKolejRecorID.Text & "' AND PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                ElseIf nCount = 7 Then
                    strSQL = "UPDATE kpmkv_pelajar_markah SET  A_Teori1='" & BM3 & "', A_Teori2='" & BM3 & "',A_Teori3='" & BM3 & "', A_Teori4='" & BM3 & "',"
                    strSQL += " A_Teori5='" & BM3 & "', A_Teori6='" & BM3 & "',A_Teori7='" & BM3 & "', CatatanPA='" & strCatatan & "'"
                    strSQL += " WHERE KolejRecordID='" & lblKolejRecorID.Text & "' AND PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                ElseIf nCount = 8 Then
                    strSQL = "UPDATE kpmkv_pelajar_markah SET  A_Teori1='" & BM3 & "', A_Teori2='" & BM3 & "',A_Teori3='" & BM3 & "', A_Teori4='" & BM3 & "',"
                    strSQL += " A_Teori5='" & BM3 & "', A_Teori6='" & BM3 & "', A_Teori7='" & BM3 & "', A_Teori8='" & BM3 & "', CatatanPA='" & strCatatan & "'"
                    strSQL += " WHERE KolejRecordID='" & lblKolejRecorID.Text & "' AND PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"
                End If
                strRet = oCommon.ExecuteSQL(strSQL)

            Next
            divMsg2.Attributes("class") = "info"
            lblMsg2.Text = "Berjaya!.Kemaskini markah Vokasional."

            divMsg.Attributes("class") = "info"
            lblMsg.Text = "Berjaya!.Kemaskini markah Vokasional."

            strRet = BindData(datRespondent)

        Catch ex As Exception
            divMsg2.Attributes("class") = "Error:" & ex.Message
            lblMsg2.Text = "Tidak Berjaya!.Kemaskini markah Vokasional."

            divMsg.Attributes("class") = "Error:" & ex.Message
            lblMsg.Text = "Tidak Berjaya!.Kemaskini markah Vokasional."
        End Try


        ' Akademik_markah()
        'Akademik_gred()
    End Sub
    Private Function ValidateForm() As Boolean
        For i As Integer = 0 To datRespondent.Rows.Count - 1
            Dim row As GridViewRow = datRespondent.Rows(i)

            Dim Kertas1 As TextBox = CType(row.FindControl("A_Teori1"), TextBox)

            '--validate NUMBER and less than 100
            '--strBahasaMelayu

            '--strBahasaMelayu3
            If Not Kertas1.Text.Length = 0 Then
                'If oCommon.IsCurrency(Kertas1.Text) = False Then
                '    Return False
                'End If
                If CInt(Kertas1.Text) > 100 Then
                    Return False
                End If
            Else
                Kertas1.Text = ""
            End If

        Next

        Return True
    End Function
    Protected Sub btnEksport_Click(sender As Object, e As EventArgs) Handles btnEksport.Click
        Try

            Dim tableColumn As DataColumnCollection
            Dim tableRows As DataRowCollection

            Dim myDataSet As New DataSet
            Dim myDataAdapter As New SqlDataAdapter(getSQL, strConn)
            myDataAdapter.Fill(myDataSet, "Borang Markah PA")
            myDataAdapter.SelectCommand.CommandTimeout = 80000
            objConn.Close()

            '--transfer to an object
            tableColumn = myDataSet.Tables(0).Columns
            tableRows = myDataSet.Tables(0).Rows

            CreatePDF(tableColumn, tableRows)

        Catch ex As Exception
            '--display on screen
            lblMsg.Text = "System Error." & ex.Message
            lblMsg2.Text = "System Error." & ex.Message
            ''--write to file
            'Dim strMsg As String = Now.ToString & ":" & Request.UserHostAddress & ":" & ex.Message
            'Dim strPath As String = Server.MapPath(".") & "\log\Error" & oCommon.getToday & ".log"
            'oCommon.WriteLogFile(strPath, strMsg)

        End Try
    End Sub
    Private Sub CreatePDF(ByVal tableColumns As DataColumnCollection, ByVal tableRows As DataRowCollection)

        Dim msFileName As String
        Dim msFilePath As String
        Dim KodBM As String
        'Step 1: First create an instance of document object
        Dim myDocument As New Document(PageSize.A4)
        ' Document.SetPageSize(iTextSharp.text.PageSize.A4.Rotate())
        Try
            ''--msFileName = Guid.NewGuid.ToString & ".pdf"
            msFileName = "BorangMarkahPA" & oCommon.getRandom & ".pdf"
            KodBM = lblKodKursus.Text

            msFilePath = Server.MapPath("borang\")

            HttpContext.Current.Response.ContentType = "application/pdf"
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" & msFileName)
            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache)

            PdfWriter.GetInstance(myDocument, HttpContext.Current.Response.OutputStream)

            'Step 3: Open the document now using
            myDocument.Open()

            ''--draw spacing
            Dim imgdrawSpacing As String = Server.MapPath("~/img/spacing2.png")
            Dim imgSpacing As Image = Image.GetInstance(imgdrawSpacing)
            imgSpacing.Alignment = Image.ALIGN_LEFT  'ALIGN_LEFT
            imgSpacing.Border = 0


            Dim strPusat As String = ""
            Dim strBmSetara As String = ""
            Dim strMykad As String = ""
            Dim strAngkaGiliran As String = ""
            Dim strKertas1 As String = ""
            Dim strCatatan As String = ""

            'strSQL = "SELECT Kod FROM kpmkv_kolej WHERE RecordID='" & lblKodPusat.Text & "' "
            strPusat = lblKodPusat.Text

            'strSQL = "SELECT KodKursus FROM kpmkv_kursus WHERE KursusID='" & lblKodKursus.Text & "'"
            strBmSetara = lblKodKursus.Text

            ''--page header

            Dim myPara03 As New Paragraph("LEMBAGA PEPERIKSAAN", FontFactory.GetFont("Arial", 10, Font.BOLD))
            myPara03.Alignment = Element.ALIGN_CENTER
            myDocument.Add(myPara03)

            Dim myPara04 As New Paragraph("KEMENTERIAN PENDIDIKAN MALAYSIA", FontFactory.GetFont("Arial", 10, Font.BOLD))
            myPara04.Alignment = Element.ALIGN_CENTER
            myDocument.Add(myPara04)

            Dim myPara05 As New Paragraph("PENTAKSIRAN PUSAT SEMESTER " & lblSemester.Text & ", " & "SESI, " & lblSesi.Text & ", " & lblTahun.Text, FontFactory.GetFont("Arial", 10, Font.BOLD))
            myPara05.Alignment = Element.ALIGN_CENTER
            myDocument.Add(myPara05)

            Dim myPara06 As New Paragraph("BORANG PENGISIAN MARKAH BERTULIS (K15)", FontFactory.GetFont("Arial", 10, Font.BOLD))
            myPara06.Alignment = Element.ALIGN_CENTER
            myDocument.Add(myPara06)

            myDocument.Add(imgSpacing)
            myDocument.Add(imgSpacing)


            Dim myTableTitle As New PdfPTable(8)
            myTableTitle.WidthPercentage = 100 ' Table size is set to 100% of the page
            myTableTitle.HorizontalAlignment = 1 '//0=Left, 1=Centre, 2=Right
            'myTable.HorizontalAlignment = Rectangle.NO_BORDER
            Dim intTblWidthTitle() As Integer = {36, 12, 36, 12, 36, 12, 36, 12}
            myTableTitle.SetWidths(intTblWidthTitle)


            Dim Cell1TTL As New PdfPCell(New Phrase("NOMBOR PUSAT ", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
            Cell1TTL.Border = Rectangle.NO_BORDER
            myTableTitle.AddCell(Cell1TTL)

            Dim Cell2TTL As New PdfPCell(New Phrase("" & strPusat, FontFactory.GetFont("Arial", 9, Font.NORMAL)))
            Cell2TTL.Border = Rectangle.NO_BORDER
            myTableTitle.AddCell(Cell2TTL)

            Dim Cell3TTL As New PdfPCell(New Phrase("NOMBOR MATA PELAJARAN", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
            Cell3TTL.Border = Rectangle.NO_BORDER
            myTableTitle.AddCell(Cell3TTL)

            Dim Cell4TTL As New PdfPCell(New Phrase("" & strBmSetara, FontFactory.GetFont("Arial", 9, Font.NORMAL)))
            Cell4TTL.Border = Rectangle.NO_BORDER
            myTableTitle.AddCell(Cell4TTL)

            Dim Cell5TTL As New PdfPCell(New Phrase("NOMBOR KETUA PEMERIKSA", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
            Cell5TTL.Border = Rectangle.NO_BORDER
            myTableTitle.AddCell(Cell5TTL)

            Dim Cell6TTL As New PdfPCell(New Phrase("", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
            Cell6TTL.Border = Rectangle.NO_BORDER
            myTableTitle.AddCell(Cell6TTL)

            Dim Cell7TTL As New PdfPCell(New Phrase("NOMBOR PEMERIKSA", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
            Cell7TTL.Border = Rectangle.NO_BORDER
            myTableTitle.AddCell(Cell7TTL)

            Dim Cell8TTL As New PdfPCell(New Phrase("", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
            Cell8TTL.Border = Rectangle.NO_BORDER
            myTableTitle.AddCell(Cell8TTL)
            myDocument.Add(myTableTitle)

            myDocument.Add(imgSpacing)
            myDocument.Add(imgSpacing)

            Dim myTable As New PdfPTable(5)
            myTable.WidthPercentage = 100 ' Table size is set to 100% of the page
            myTable.HorizontalAlignment = 1 '//0=Left, 1=Centre, 2=Right
            'myTable.HorizontalAlignment = Rectangle.NO_BORDER
            Dim intTblWidth() As Integer = {10, 30, 30, 16, 36}
            myTable.SetWidths(intTblWidth)

            Dim Cell1Hdr As New PdfPCell(New Phrase("BIL ", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
            Cell1Hdr.Border = Rectangle.NO_BORDER
            myTable.AddCell(Cell1Hdr)

            Dim Cell2Hdr As New PdfPCell(New Phrase("ANGKA GILIRAN", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
            Cell2Hdr.Border = Rectangle.NO_BORDER
            myTable.AddCell(Cell2Hdr)

            Dim Cell3Hdr As New PdfPCell(New Phrase("NO KP", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
            Cell3Hdr.Border = Rectangle.NO_BORDER
            myTable.AddCell(Cell3Hdr)

            Dim Cell4Hdr As New PdfPCell(New Phrase("MARKAH", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
            Cell4Hdr.Border = Rectangle.NO_BORDER
            myTable.AddCell(Cell4Hdr)

            Dim Cell5Hdr As New PdfPCell(New Phrase("CATATAN(1,2 ATAU 3)", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
            Cell5Hdr.Border = Rectangle.NO_BORDER
            myTable.AddCell(Cell5Hdr)
            myDocument.Add(myTable)

            myDocument.Add(imgSpacing)


            Dim k As Integer = 1
            Dim intRPN As Integer = 1
            'Get the data from database into datatable 
            Dim cmd As New SqlCommand(getSQL)
            Dim dt As DataTable = GetData(cmd)

            'append new line 
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim intCountPelajar As Integer = dt.Rows.Count
                Dim strStudentID As String = ""
                'set RPN


                strSQL = "SELECT Mykad FROM kpmkv_pelajar WHERE PelajarID='" & dt.Rows(i)("PelajarID").ToString() & "'"
                strMykad = oCommon.getFieldValue(strSQL)

                strSQL = "SELECT AngkaGiliran FROM kpmkv_pelajar WHERE PelajarID='" & dt.Rows(i)("PelajarID").ToString() & "'"
                strAngkaGiliran = oCommon.getFieldValue(strSQL)

                strSQL = "SELECT A_Teori1 FROM kpmkv_pelajar_markah WHERE PelajarID='" & dt.Rows(i)("PelajarID").ToString() & "'"
                strKertas1 = oCommon.getFieldValue(strSQL)

                strSQL = "SELECT CatatanPA FROM kpmkv_pelajar_markah WHERE PelajarID='" & dt.Rows(i)("PelajarID").ToString() & "'"
                strCatatan = oCommon.getFieldValue(strSQL)


                Dim myTableMP8 As New PdfPTable(5)
                myTableMP8.WidthPercentage = 100 ' Table size is set to 100% of the page
                myTableMP8.HorizontalAlignment = 1 '//0=Left, 1=Centre, 2=Right
                myTableMP8.DefaultCell.BorderWidth = Rectangle.NO_BORDER
                Dim intTblCell8() As Integer = {10, 30, 30, 16, 36}
                myTableMP8.SetWidths(intTblCell8)

                myTableMP8.AddCell(New Phrase(intRPN, FontFactory.GetFont("Arial", 9, Font.NORMAL)))
                myTableMP8.AddCell(New Phrase(strAngkaGiliran, FontFactory.GetFont("Arial", 9, Font.NORMAL)))
                myTableMP8.AddCell(New Phrase(strMykad, FontFactory.GetFont("Arial", 9, Font.NORMAL)))
                myTableMP8.AddCell(New Phrase(strKertas1, FontFactory.GetFont("Arial", 9, Font.NORMAL)))
                myTableMP8.AddCell(New Phrase(strCatatan, FontFactory.GetFont("Arial", 9, Font.NORMAL)))
                myDocument.Add(myTableMP8)
                myDocument.Add(imgSpacing)

                If Not intRPN = intCountPelajar Then
                    If Not k = 20 Then
                        k = k + 1
                    Else
                        k = 1
                        myDocument.Add(imgSpacing)

                        Dim myTableSubTitle As New PdfPTable(3)
                        myTableSubTitle.WidthPercentage = 100 ' Table size is set to 100% of the page
                        myTableSubTitle.HorizontalAlignment = 1 '//0=Left, 1=Centre, 2=Right
                        'myTable.HorizontalAlignment = Rectangle.NO_BORDER
                        Dim intTblWidthSubTitle() As Integer = {36, 36, 36}
                        myTableSubTitle.SetWidths(intTblWidthSubTitle)


                        Dim Cell1Sub As New PdfPCell(New Phrase("PENGESAHAN OLEH ", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
                        Cell1Sub.Border = Rectangle.NO_BORDER
                        myTableSubTitle.AddCell(Cell1Sub)

                        Dim Cell2Sub As New PdfPCell(New Phrase("", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
                        Cell2Sub.Border = Rectangle.NO_BORDER
                        myTableSubTitle.AddCell(Cell2Sub)

                        Dim Cell3Sub As New PdfPCell(New Phrase("PETUNJUK :", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
                        Cell3Sub.Border = Rectangle.NO_BORDER
                        myTableSubTitle.AddCell(Cell3Sub)
                        myDocument.Add(myTableSubTitle)

                        Dim myTableSubTitle1 As New PdfPTable(3)
                        myTableSubTitle1.WidthPercentage = 100 ' Table size is set to 100% of the page
                        myTableSubTitle1.HorizontalAlignment = 1 '//0=Left, 1=Centre, 2=Right
                        'myTable.HorizontalAlignment = Rectangle.NO_BORDER
                        Dim intTblWidthSubTitle1() As Integer = {36, 36, 36}
                        myTableSubTitle1.SetWidths(intTblWidthSubTitle1)

                        Dim Cell4Sub As New PdfPCell(New Phrase("NAMA", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
                        Cell4Sub.Border = Rectangle.NO_BORDER
                        myTableSubTitle1.AddCell(Cell4Sub)

                        Dim Cell5Sub As New PdfPCell(New Phrase(":", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
                        Cell5Sub.Border = Rectangle.NO_BORDER
                        myTableSubTitle1.AddCell(Cell5Sub)

                        Dim Cell6Sub As New PdfPCell(New Phrase("HADIR TIDAK MENJAWAB:1", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
                        Cell6Sub.Border = Rectangle.NO_BORDER
                        myTableSubTitle1.AddCell(Cell6Sub)
                        myDocument.Add(myTableSubTitle1)

                        Dim myTableSubTitle2 As New PdfPTable(3)
                        myTableSubTitle2.WidthPercentage = 100 ' Table size is set to 100% of the page
                        myTableSubTitle2.HorizontalAlignment = 1 '//0=Left, 1=Centre, 2=Right
                        'myTable.HorizontalAlignment = Rectangle.NO_BORDER
                        Dim intTblWidthSubTitle2() As Integer = {36, 36, 36}
                        myTableSubTitle2.SetWidths(intTblWidthSubTitle2)

                        Dim Cell7Sub As New PdfPCell(New Phrase("TARIKH", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
                        Cell7Sub.Border = Rectangle.NO_BORDER
                        myTableSubTitle2.AddCell(Cell7Sub)

                        Dim Cell8Sub As New PdfPCell(New Phrase(":", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
                        Cell8Sub.Border = Rectangle.NO_BORDER
                        myTableSubTitle2.AddCell(Cell8Sub)

                        Dim Cell9Sub As New PdfPCell(New Phrase("TIADA SKRIP:2", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
                        Cell9Sub.Border = Rectangle.NO_BORDER
                        myTableSubTitle2.AddCell(Cell9Sub)
                        myDocument.Add(myTableSubTitle2)

                        Dim myTableSubTitle3 As New PdfPTable(3)
                        myTableSubTitle3.WidthPercentage = 100 ' Table size is set to 100% of the page
                        myTableSubTitle3.HorizontalAlignment = 1 '//0=Left, 1=Centre, 2=Right
                        'myTable.HorizontalAlignment = Rectangle.NO_BORDER
                        Dim intTblWidthSubTitle3() As Integer = {36, 36, 36}
                        myTableSubTitle3.SetWidths(intTblWidthSubTitle3)

                        Dim Cell10Sub As New PdfPCell(New Phrase("", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
                        Cell10Sub.Border = Rectangle.NO_BORDER
                        myTableSubTitle3.AddCell(Cell10Sub)

                        Dim Cell11Sub As New PdfPCell(New Phrase("", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
                        Cell11Sub.Border = Rectangle.NO_BORDER
                        myTableSubTitle3.AddCell(Cell11Sub)

                        Dim Cell12Sub As New PdfPCell(New Phrase("TIDAK HADIR:3", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
                        Cell12Sub.Border = Rectangle.NO_BORDER
                        myTableSubTitle3.AddCell(Cell12Sub)
                        myDocument.Add(myTableSubTitle3)


                        Dim myTableSubTitle4 As New PdfPTable(3)
                        myTableSubTitle4.WidthPercentage = 100 ' Table size is set to 100% of the page
                        myTableSubTitle4.HorizontalAlignment = 1 '//0=Left, 1=Centre, 2=Right
                        'myTable.HorizontalAlignment = Rectangle.NO_BORDER
                        Dim intTblWidthSubTitle4() As Integer = {36, 36, 36}
                        myTableSubTitle4.SetWidths(intTblWidthSubTitle4)

                        Dim Cell13Sub As New PdfPCell(New Phrase("DISEMAK OLEH", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
                        Cell13Sub.Border = Rectangle.NO_BORDER
                        myTableSubTitle4.AddCell(Cell13Sub)

                        Dim Cell14Sub As New PdfPCell(New Phrase(":", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
                        Cell14Sub.Border = Rectangle.NO_BORDER
                        myTableSubTitle4.AddCell(Cell14Sub)

                        Dim Cell15Sub As New PdfPCell(New Phrase("", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
                        Cell15Sub.Border = Rectangle.NO_BORDER
                        myTableSubTitle4.AddCell(Cell15Sub)
                        myDocument.Add(myTableSubTitle4)

                        Dim myTableSubTitle5 As New PdfPTable(3)
                        myTableSubTitle5.WidthPercentage = 100 ' Table size is set to 100% of the page
                        myTableSubTitle5.HorizontalAlignment = 1 '//0=Left, 1=Centre, 2=Right
                        'myTable.HorizontalAlignment = Rectangle.NO_BORDER
                        Dim intTblWidthSubTitle5() As Integer = {36, 36, 36}
                        myTableSubTitle5.SetWidths(intTblWidthSubTitle5)

                        Dim Cell16Sub As New PdfPCell(New Phrase("NAMA", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
                        Cell16Sub.Border = Rectangle.NO_BORDER
                        myTableSubTitle5.AddCell(Cell16Sub)

                        Dim Cell17Sub As New PdfPCell(New Phrase(":", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
                        Cell17Sub.Border = Rectangle.NO_BORDER
                        myTableSubTitle5.AddCell(Cell17Sub)

                        Dim Cell18Sub As New PdfPCell(New Phrase("", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
                        Cell18Sub.Border = Rectangle.NO_BORDER
                        myTableSubTitle5.AddCell(Cell18Sub)
                        myDocument.Add(myTableSubTitle5)

                        Dim myTableSubTitle6 As New PdfPTable(3)
                        myTableSubTitle6.WidthPercentage = 100 ' Table size is set to 100% of the page
                        myTableSubTitle6.HorizontalAlignment = 1 '//0=Left, 1=Centre, 2=Right
                        'myTable.HorizontalAlignment = Rectangle.NO_BORDER
                        Dim intTblWidthSubTitle6() As Integer = {36, 36, 36}
                        myTableSubTitle6.SetWidths(intTblWidthSubTitle6)

                        Dim Cell19Sub As New PdfPCell(New Phrase("TARIKH", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
                        Cell19Sub.Border = Rectangle.NO_BORDER
                        myTableSubTitle6.AddCell(Cell19Sub)

                        Dim Cell20Sub As New PdfPCell(New Phrase(":", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
                        Cell20Sub.Border = Rectangle.NO_BORDER
                        myTableSubTitle6.AddCell(Cell20Sub)

                        Dim Cell21Sub As New PdfPCell(New Phrase("", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
                        Cell21Sub.Border = Rectangle.NO_BORDER
                        myTableSubTitle6.AddCell(Cell21Sub)
                        myDocument.Add(myTableSubTitle6)

                        myDocument.NewPage()
                        myDocument.Add(myPara03)
                        myDocument.Add(myPara04)
                        myDocument.Add(myPara05)
                        myDocument.Add(myPara06)

                        myDocument.Add(imgSpacing)
                        myDocument.Add(imgSpacing)

                        myDocument.Add(myTableTitle)

                        myDocument.Add(imgSpacing)
                        myDocument.Add(imgSpacing)

                        myDocument.Add(myTable)

                        myDocument.Add(imgSpacing)
                    End If
                Else
                    myDocument.Add(imgSpacing)

                    Dim myTableSubTitle As New PdfPTable(3)
                    myTableSubTitle.WidthPercentage = 100 ' Table size is set to 100% of the page
                    myTableSubTitle.HorizontalAlignment = 1 '//0=Left, 1=Centre, 2=Right
                    'myTable.HorizontalAlignment = Rectangle.NO_BORDER
                    Dim intTblWidthSubTitle() As Integer = {36, 36, 36}
                    myTableSubTitle.SetWidths(intTblWidthSubTitle)


                    Dim Cell1Sub As New PdfPCell(New Phrase("PENGESAHAN OLEH ", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
                    Cell1Sub.Border = Rectangle.NO_BORDER
                    myTableSubTitle.AddCell(Cell1Sub)

                    Dim Cell2Sub As New PdfPCell(New Phrase("", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
                    Cell2Sub.Border = Rectangle.NO_BORDER
                    myTableSubTitle.AddCell(Cell2Sub)

                    Dim Cell3Sub As New PdfPCell(New Phrase("PETUNJUK :", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
                    Cell3Sub.Border = Rectangle.NO_BORDER
                    myTableSubTitle.AddCell(Cell3Sub)
                    myDocument.Add(myTableSubTitle)

                    Dim myTableSubTitle1 As New PdfPTable(3)
                    myTableSubTitle1.WidthPercentage = 100 ' Table size is set to 100% of the page
                    myTableSubTitle1.HorizontalAlignment = 1 '//0=Left, 1=Centre, 2=Right
                    'myTable.HorizontalAlignment = Rectangle.NO_BORDER
                    Dim intTblWidthSubTitle1() As Integer = {36, 36, 36}
                    myTableSubTitle1.SetWidths(intTblWidthSubTitle1)

                    Dim Cell4Sub As New PdfPCell(New Phrase("NAMA", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
                    Cell4Sub.Border = Rectangle.NO_BORDER
                    myTableSubTitle1.AddCell(Cell4Sub)

                    Dim Cell5Sub As New PdfPCell(New Phrase(":", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
                    Cell5Sub.Border = Rectangle.NO_BORDER
                    myTableSubTitle1.AddCell(Cell5Sub)

                    Dim Cell6Sub As New PdfPCell(New Phrase("HADIR TIDAK MENJAWAB:1", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
                    Cell6Sub.Border = Rectangle.NO_BORDER
                    myTableSubTitle1.AddCell(Cell6Sub)
                    myDocument.Add(myTableSubTitle1)

                    Dim myTableSubTitle2 As New PdfPTable(3)
                    myTableSubTitle2.WidthPercentage = 100 ' Table size is set to 100% of the page
                    myTableSubTitle2.HorizontalAlignment = 1 '//0=Left, 1=Centre, 2=Right
                    'myTable.HorizontalAlignment = Rectangle.NO_BORDER
                    Dim intTblWidthSubTitle2() As Integer = {36, 36, 36}
                    myTableSubTitle2.SetWidths(intTblWidthSubTitle2)

                    Dim Cell7Sub As New PdfPCell(New Phrase("TARIKH", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
                    Cell7Sub.Border = Rectangle.NO_BORDER
                    myTableSubTitle2.AddCell(Cell7Sub)

                    Dim Cell8Sub As New PdfPCell(New Phrase(":", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
                    Cell8Sub.Border = Rectangle.NO_BORDER
                    myTableSubTitle2.AddCell(Cell8Sub)

                    Dim Cell9Sub As New PdfPCell(New Phrase("TIADA SKRIP:2", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
                    Cell9Sub.Border = Rectangle.NO_BORDER
                    myTableSubTitle2.AddCell(Cell9Sub)
                    myDocument.Add(myTableSubTitle2)

                    Dim myTableSubTitle3 As New PdfPTable(3)
                    myTableSubTitle3.WidthPercentage = 100 ' Table size is set to 100% of the page
                    myTableSubTitle3.HorizontalAlignment = 1 '//0=Left, 1=Centre, 2=Right
                    'myTable.HorizontalAlignment = Rectangle.NO_BORDER
                    Dim intTblWidthSubTitle3() As Integer = {36, 36, 36}
                    myTableSubTitle3.SetWidths(intTblWidthSubTitle3)

                    Dim Cell10Sub As New PdfPCell(New Phrase("", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
                    Cell10Sub.Border = Rectangle.NO_BORDER
                    myTableSubTitle3.AddCell(Cell10Sub)

                    Dim Cell11Sub As New PdfPCell(New Phrase("", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
                    Cell11Sub.Border = Rectangle.NO_BORDER
                    myTableSubTitle3.AddCell(Cell11Sub)

                    Dim Cell12Sub As New PdfPCell(New Phrase("TIDAK HADIR:3", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
                    Cell12Sub.Border = Rectangle.NO_BORDER
                    myTableSubTitle3.AddCell(Cell12Sub)
                    myDocument.Add(myTableSubTitle3)


                    Dim myTableSubTitle4 As New PdfPTable(3)
                    myTableSubTitle4.WidthPercentage = 100 ' Table size is set to 100% of the page
                    myTableSubTitle4.HorizontalAlignment = 1 '//0=Left, 1=Centre, 2=Right
                    'myTable.HorizontalAlignment = Rectangle.NO_BORDER
                    Dim intTblWidthSubTitle4() As Integer = {36, 36, 36}
                    myTableSubTitle4.SetWidths(intTblWidthSubTitle4)

                    Dim Cell13Sub As New PdfPCell(New Phrase("DISEMAK OLEH", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
                    Cell13Sub.Border = Rectangle.NO_BORDER
                    myTableSubTitle4.AddCell(Cell13Sub)

                    Dim Cell14Sub As New PdfPCell(New Phrase(":", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
                    Cell14Sub.Border = Rectangle.NO_BORDER
                    myTableSubTitle4.AddCell(Cell14Sub)

                    Dim Cell15Sub As New PdfPCell(New Phrase("", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
                    Cell15Sub.Border = Rectangle.NO_BORDER
                    myTableSubTitle4.AddCell(Cell15Sub)
                    myDocument.Add(myTableSubTitle4)

                    Dim myTableSubTitle5 As New PdfPTable(3)
                    myTableSubTitle5.WidthPercentage = 100 ' Table size is set to 100% of the page
                    myTableSubTitle5.HorizontalAlignment = 1 '//0=Left, 1=Centre, 2=Right
                    'myTable.HorizontalAlignment = Rectangle.NO_BORDER
                    Dim intTblWidthSubTitle5() As Integer = {36, 36, 36}
                    myTableSubTitle5.SetWidths(intTblWidthSubTitle5)

                    Dim Cell16Sub As New PdfPCell(New Phrase("NAMA", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
                    Cell16Sub.Border = Rectangle.NO_BORDER
                    myTableSubTitle5.AddCell(Cell16Sub)

                    Dim Cell17Sub As New PdfPCell(New Phrase(":", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
                    Cell17Sub.Border = Rectangle.NO_BORDER
                    myTableSubTitle5.AddCell(Cell17Sub)

                    Dim Cell18Sub As New PdfPCell(New Phrase("", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
                    Cell18Sub.Border = Rectangle.NO_BORDER
                    myTableSubTitle5.AddCell(Cell18Sub)
                    myDocument.Add(myTableSubTitle5)

                    Dim myTableSubTitle6 As New PdfPTable(3)
                    myTableSubTitle6.WidthPercentage = 100 ' Table size is set to 100% of the page
                    myTableSubTitle6.HorizontalAlignment = 1 '//0=Left, 1=Centre, 2=Right
                    'myTable.HorizontalAlignment = Rectangle.NO_BORDER
                    Dim intTblWidthSubTitle6() As Integer = {36, 36, 36}
                    myTableSubTitle6.SetWidths(intTblWidthSubTitle6)

                    Dim Cell19Sub As New PdfPCell(New Phrase("TARIKH", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
                    Cell19Sub.Border = Rectangle.NO_BORDER
                    myTableSubTitle6.AddCell(Cell19Sub)

                    Dim Cell20Sub As New PdfPCell(New Phrase(":", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
                    Cell20Sub.Border = Rectangle.NO_BORDER
                    myTableSubTitle6.AddCell(Cell20Sub)

                    Dim Cell21Sub As New PdfPCell(New Phrase("", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
                    Cell21Sub.Border = Rectangle.NO_BORDER
                    myTableSubTitle6.AddCell(Cell21Sub)
                    myDocument.Add(myTableSubTitle6)

                End If


                intRPN = intRPN + 1

            Next



            lblMsg.Text = "PDF siap dijana."

        Catch ex As DocumentException
            '--display on screen
            lblMsg.Text = "System Error. Contact system admin. " & ex.Message
            lblMsg2.Text = "System Error. Contact system admin. " & ex.Message

        Catch ioe As IOException
            '--display on screen
            lblMsg.Text = "System Error. Contact system admin. " & ioe.Message.ToString
            lblMsg2.Text = "System Error. Contact system admin. " & ioe.Message.ToString

        Finally
            'Step 5: Remember to close the documnet
            myDocument.Close()

        End Try

    End Sub

End Class