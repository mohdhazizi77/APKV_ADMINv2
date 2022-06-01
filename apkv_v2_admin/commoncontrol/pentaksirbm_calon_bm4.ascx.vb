Imports System.Data.SqlClient
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Public Class pentaksirbm_calon_bm4
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                strSQL = "SELECT MataPelajaran FROM kpmkv_pentaksir_bmsetara WHERE id = '" & Request.QueryString("id") & "'"
                lblMP.Text = oCommon.getFieldValue(strSQL)

                strSQL = "SELECT KolejRecordID FROM kpmkv_pentaksir_bmsetara WHERE id = '" & Request.QueryString("id") & "'"
                Dim KolejRecordID As String = oCommon.getFieldValue(strSQL)

                strSQL = "SELECT Nama FROM kpmkv_kolej WHERE RecordID = '" & KolejRecordID & "'"
                lblPP.Text = oCommon.getFieldValue(strSQL)


                lblMsg.Text = ""
                strRet = BindData(datRespondent)

                Hantar()


            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message
        End Try
    End Sub

    Public Sub Hantar()

        For i As Integer = 0 To datRespondent.Rows.Count - 1

            Dim row As GridViewRow = datRespondent.Rows(i)

            Dim BM4 As TextBox = CType(row.FindControl("BM4"), TextBox)

            strSQL = "SELECT StatusHantarBM4_Pentaksir FROM kpmkv_pentaksir_bmsetara_calon WHERE id = '" & datRespondent.DataKeys(i).Value.ToString & "'"
            Dim StatusHantarBM4_Pentaksir As String = oCommon.getFieldValue(strSQL)

            If StatusHantarBM4_Pentaksir = "TELAH HANTAR" Then

                BM4.Enabled = False

            Else

                BM4.Enabled = True

            End If

        Next

    End Sub

    Private Sub datRespondent_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles datRespondent.PageIndexChanging
        datRespondent.PageIndex = e.NewPageIndex
        strRet = BindData(datRespondent)
        Hantar()
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click

        Response.Redirect("pentaksirbm_main.aspx")

    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click

        strSQL = "SELECT KolejRecordID FROM kpmkv_pentaksir_bmsetara WHERE id = '" & Request.QueryString("id") & "'"
        Dim KolejRecordID As String = oCommon.getFieldValue(strSQL)

        strSQL = "  UPDATE kpmkv_pentaksir_bmsetara_calon
                    SET 
                    BM4 = NULL
                    WHERE KolejRecordID = '" & KolejRecordID & "'
                    AND StatusHantarBM4_Pentaksir = 'BELUM HANTAR'
                    AND StatusBM4 = '0'"

        strRet = oCommon.ExecuteSQL(strSQL)

        strSQL = "  UPDATE kpmkv_pentaksir_bmsetara_calon
                    SET 
                    BM4 = NULL
                    WHERE KolejRecordID = '" & KolejRecordID & "'
                    AND StatusHantarBM4_Pentaksir = 'BELUM HANTAR'
                    AND StatusBM4 = '0'"

        strRet = oCommon.ExecuteSQL(strSQL)

        strRet = BindData(datRespondent)
        Hantar()


    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

        Try

            If ValidateForm() = False Then

                divMsg.Attributes("class") = "error"
                lblMsg.Text = "Sila masukkan [0 - 30] untuk markah ujian mendengar, atau [T] untuk Tidak Hadir SAHAJA"
                Exit Sub

            End If

            For i As Integer = 0 To datRespondent.Rows.Count - 1

                Dim row As GridViewRow = datRespondent.Rows(i)

                Dim BM4 As TextBox = CType(row.FindControl("BM4"), TextBox)

                If Not BM4.Text = "" Then


                    If Not BM4.Text = "T" And Not BM4.Text = "t" Then

                        strSQL = "  UPDATE kpmkv_pentaksir_bmsetara_calon
                                SET
                                BM4 = '" & BM4.Text & "',
                                BM4_Total = '" & BM4.Text & "',
                                StatusBM4 = '1'
                                WHERE id = '" & datRespondent.DataKeys(i).Value.ToString & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)

                    Else

                        strSQL = "  UPDATE kpmkv_pentaksir_bmsetara_calon
                                SET
                                BM4 = '" & BM4.Text & "',
                                StatusBM4 = '1'
                                WHERE id = '" & datRespondent.DataKeys(i).Value.ToString & "'"
                        strRet = oCommon.ExecuteSQL(strSQL)

                    End If

                End If

                If strRet = "0" Then
                    lblMsg.Attributes("class") = "info"
                    lblMsg.Text = "Markah berjaya disimpan"
                Else
                    lblMsg.Attributes("class") = "error"
                    lblMsg.Text = "Markah tidak berjaya disimpan"
                End If

            Next

        Catch ex As Exception
            lblMsg.Text = "Error:" & ex.Message
        End Try

        strRet = BindData(datRespondent)
        Hantar()


    End Sub

    Private Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click

        Dim id As String = Request.QueryString("id")

        Response.Redirect("pentaksirbm_calon_bm4_serah.aspx?id=" & id)

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

        strSQL = "SELECT KolejRecordID FROM kpmkv_pentaksir_bmsetara WHERE id = '" & Request.QueryString("id") & "'"
        Dim KolejRecordID As String = oCommon.getFieldValue(strSQL)

        strSQL = "SELECT Kohort FROM kpmkv_pentaksir_bmsetara WHERE id = '" & Request.QueryString("id") & "'"
        Dim Kohort As String = oCommon.getFieldValue(strSQL)

        strSQL = "SELECT Semester FROM kpmkv_pentaksir_bmsetara WHERE id = '" & Request.QueryString("id") & "'"
        Dim Semester As String = oCommon.getFieldValue(strSQL)


        Dim tmpSQL As String = ""
        Dim strWhere As String = ""
        Dim strOrder As String = " ORDER BY AngkaGiliran, Nama ASC"

        '--not deleted
        tmpSQL = "SELECT id, KolejRecordID, Tahun, Nama, MYKAD, AngkaGiliran, BM4, StatusHantarBM4_Pentaksir FROM kpmkv_pentaksir_bmsetara_calon"
        strWhere = " WHERE KolejRecordID='" & KolejRecordID & "' AND Tahun ='" & Kohort & "'"

        getSQL = tmpSQL & strWhere & strOrder
        ''--debug
        'Response.Write(getSQL)

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

    Private Function ValidateForm() As Boolean

        For i As Integer = 0 To datRespondent.Rows.Count - 1

            Dim row As GridViewRow = datRespondent.Rows(i)

            Dim BM4 As TextBox = CType(row.FindControl("BM4"), TextBox)

            If Not BM4.Text.Length = 0 Then

                If Not IsNumeric(BM4.Text) = True Then

                    If Not BM4.Text = "T" And Not BM4.Text = "t" Then

                        Return False

                    End If

                Else

                    If CInt(BM4.Text) > 30 Then
                        Return False
                    End If
                    If CInt(BM4.Text) < 0 Then
                        Return False
                    End If

                End If

            End If

        Next

        Return True
    End Function

    Private Sub btnCetak_Click(sender As Object, e As EventArgs) Handles btnCetak.Click

        Dim myDocument As New Document(PageSize.A4.Rotate)

        Try
            HttpContext.Current.Response.ContentType = "application/pdf"
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=BorangMarkahUjianBertutur.pdf")
            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache)

            PdfWriter.GetInstance(myDocument, HttpContext.Current.Response.OutputStream)

            ''--draw spacing
            Dim imgdrawSpacing As String = Server.MapPath("~/img/spacing.png")
            Dim imgSpacing As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(imgdrawSpacing)
            imgSpacing.Alignment = iTextSharp.text.Image.LEFT_ALIGN  'left
            imgSpacing.Border = 0

            myDocument.Open()

            myDocument.NewPage()
            ''getting data end

            Dim table As New PdfPTable(2)
            table.WidthPercentage = 100
            table.SetWidths({80, 20})
            table.DefaultCell.Border = 0

            Dim cell As New PdfPCell()
            Dim cetak As String
            Dim myPara001 As New Paragraph()
            cell.AddElement(myPara001)
            cell.Border = 0
            table.AddCell(cell)

            cetak = ""

            cell = New PdfPCell()
            Debug.Write(cetak)
            myPara001 = New Paragraph(cetak, FontFactory.GetFont("Arial", 7))
            myPara001.Alignment = Element.ALIGN_RIGHT
            cell.AddElement(myPara001)
            cell.Border = 0
            cell.VerticalAlignment = Element.ALIGN_RIGHT
            table.AddCell(cell)

            myDocument.Add(table)

            strSQL = "SELECT value FROM kpmkv_pentaksir_bmsetara_setting WHERE Type = 'Tahun'"
            Dim Tahun As String = oCommon.getFieldValue(strSQL)

            cetak = "LEMBAGA PEPERIKSAAN"
            cetak += Environment.NewLine & "KEMENTERIAN PENDIDIKAN MALAYSIA"
            cetak += Environment.NewLine & "BORANG MARKAH UJIAN MENDENGAR BAHASA MELAYU"
            cetak += Environment.NewLine & "SIJIL VOKASIONAL MALAYSIA TAHUN  " & Tahun
            myPara001 = New Paragraph(cetak, FontFactory.GetFont("Arial", 8))
            myPara001.Alignment = Element.ALIGN_CENTER
            myDocument.Add(myPara001)
            Debug.WriteLine(cetak)

            ''PROFILE STARTS HERE

            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

            table = New PdfPTable(4)
            table.WidthPercentage = 100
            table.SetWidths({15, 5, 25, 65})

            cell = New PdfPCell()
            cetak = Environment.NewLine & "KOD PUSAT"
            cetak += Environment.NewLine & "NAMA KOLEJ"
            cetak += Environment.NewLine
            cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 8)))
            cell.Border = 0
            table.AddCell(cell)

            cell = New PdfPCell()
            cetak = Environment.NewLine & ":"
            cetak += Environment.NewLine & ":"
            cetak += Environment.NewLine
            cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 8)))
            cell.Border = 0
            table.AddCell(cell)

            strSQL = "  SELECT kpmkv_kolej.Kod
                        FROM kpmkv_kolej
                        LEFT JOIN kpmkv_pentaksir_bmsetara ON kpmkv_pentaksir_bmsetara.KolejRecordID = kpmkv_kolej.RecordID
                        WHERE kpmkv_pentaksir_bmsetara.id = '" & Request.QueryString("id") & "'"

            Dim Kod As String = oCommon.getFieldValue(strSQL)

            strSQL = "  SELECT kpmkv_kolej.Nama
                        FROM kpmkv_kolej
                        LEFT JOIN kpmkv_pentaksir_bmsetara ON kpmkv_pentaksir_bmsetara.KolejRecordID = kpmkv_kolej.RecordID
                        WHERE kpmkv_pentaksir_bmsetara.id = '" & Request.QueryString("id") & "'"

            Dim NamaKolej As String = oCommon.getFieldValue(strSQL)

            cell = New PdfPCell()
            cetak = Environment.NewLine & Kod
            cetak += Environment.NewLine & NamaKolej
            cetak += Environment.NewLine
            cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 8)))
            cell.Border = 0
            table.AddCell(cell)

            cell = New PdfPCell()
            cetak = Environment.NewLine
            cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 8)))
            cell.Border = 0
            table.AddCell(cell)

            Debug.WriteLine(cetak)

            myDocument.Add(table)

            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

            table = New PdfPTable(6)
            table.WidthPercentage = 100
            table.SetWidths({1, 15, 7, 7, 5, 5})
            table.DefaultCell.Border = Rectangle.BOX

            cell = New PdfPCell()
            cetak = "BIL"
            myPara001 = New Paragraph(cetak, FontFactory.GetFont("Arial", 8))
            myPara001.Alignment = Element.ALIGN_CENTER
            cell.AddElement(myPara001)
            cell.VerticalAlignment = Element.ALIGN_MIDDLE
            table.AddCell(cell)

            cell = New PdfPCell()
            cetak = "NAMA CALON"
            myPara001 = New Paragraph(cetak, FontFactory.GetFont("Arial", 8))
            myPara001.Alignment = Element.ALIGN_CENTER
            cell.AddElement(myPara001)
            cell.Border = Rectangle.BOX
            cell.VerticalAlignment = Element.ALIGN_MIDDLE
            table.AddCell(cell)

            cell = New PdfPCell()
            cetak = "NO K.P"
            myPara001 = New Paragraph(cetak, FontFactory.GetFont("Arial", 8))
            myPara001.Alignment = Element.ALIGN_CENTER
            cell.AddElement(myPara001)
            cell.Border = Rectangle.BOX
            cell.VerticalAlignment = Element.ALIGN_MIDDLE
            table.AddCell(cell)

            cell = New PdfPCell()
            cetak = "ANGKA GILIRAN"
            myPara001 = New Paragraph(cetak, FontFactory.GetFont("Arial", 8))
            myPara001.Alignment = Element.ALIGN_CENTER
            cell.AddElement(myPara001)
            cell.Border = Rectangle.BOX
            cell.VerticalAlignment = Element.ALIGN_MIDDLE
            table.AddCell(cell)

            cell = New PdfPCell()
            cetak = "MARKAH"
            myPara001 = New Paragraph(cetak, FontFactory.GetFont("Arial", 8))
            myPara001.Alignment = Element.ALIGN_CENTER
            cell.AddElement(myPara001)
            cell.Border = Rectangle.BOX
            cell.VerticalAlignment = Element.ALIGN_MIDDLE
            table.AddCell(cell)

            cell = New PdfPCell()
            cetak = "STATUS"
            myPara001 = New Paragraph(cetak, FontFactory.GetFont("Arial", 8))
            myPara001.Alignment = Element.ALIGN_CENTER
            cell.AddElement(myPara001)
            cell.Border = Rectangle.BOX
            cell.VerticalAlignment = Element.ALIGN_MIDDLE
            table.AddCell(cell)

            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''' MAKLUMAT CALON

            For i As Integer = 0 To datRespondent.Rows.Count - 1

                Dim dataKey As String = datRespondent.DataKeys(i).Value.ToString

                cell = New PdfPCell()
                cetak = i + 1
                myPara001 = New Paragraph(cetak, FontFactory.GetFont("Arial", 8))
                myPara001.Alignment = Element.ALIGN_CENTER
                cell.AddElement(myPara001)
                cell.Border = Rectangle.BOX
                cell.VerticalAlignment = Element.ALIGN_MIDDLE
                table.AddCell(cell)

                strSQL = "  SELECT Nama FROM kpmkv_pentaksir_bmsetara_calon WHERE id = '" & dataKey & "'"
                Dim Nama As String = oCommon.getFieldValue(strSQL)

                cell = New PdfPCell()
                cetak = Nama
                myPara001 = New Paragraph(cetak, FontFactory.GetFont("Arial", 8))
                myPara001.Alignment = Element.ALIGN_LEFT
                cell.AddElement(myPara001)
                cell.Border = Rectangle.BOX
                cell.VerticalAlignment = Element.ALIGN_MIDDLE
                table.AddCell(cell)

                strSQL = "  SELECT MYKAD FROM kpmkv_pentaksir_bmsetara_calon WHERE id = '" & dataKey & "'"
                Dim MYKAD As String = oCommon.getFieldValue(strSQL)

                cell = New PdfPCell()
                cetak = MYKAD
                myPara001 = New Paragraph(cetak, FontFactory.GetFont("Arial", 8))
                myPara001.Alignment = Element.ALIGN_CENTER
                cell.AddElement(myPara001)
                cell.Border = Rectangle.BOX
                cell.VerticalAlignment = Element.ALIGN_MIDDLE
                table.AddCell(cell)

                strSQL = "  SELECT AngkaGiliran FROM kpmkv_pentaksir_bmsetara_calon WHERE id = '" & dataKey & "'"
                Dim AngkaGiliran As String = oCommon.getFieldValue(strSQL)

                cell = New PdfPCell()
                cetak = AngkaGiliran
                myPara001 = New Paragraph(cetak, FontFactory.GetFont("Arial", 8))
                myPara001.Alignment = Element.ALIGN_CENTER
                cell.AddElement(myPara001)
                cell.Border = Rectangle.BOX
                cell.VerticalAlignment = Element.ALIGN_MIDDLE
                table.AddCell(cell)

                strSQL = "  SELECT BM4 FROM kpmkv_pentaksir_bmsetara_calon WHERE id = '" & dataKey & "'"
                Dim BM4 As String = oCommon.getFieldValue(strSQL)

                cell = New PdfPCell()
                cetak = BM4
                myPara001 = New Paragraph(cetak, FontFactory.GetFont("Arial", 8))
                myPara001.Alignment = Element.ALIGN_CENTER
                cell.AddElement(myPara001)
                cell.Border = Rectangle.BOX
                cell.VerticalAlignment = Element.ALIGN_MIDDLE
                table.AddCell(cell)

                strSQL = "  SELECT StatusHantarBM4_Pentaksir FROM kpmkv_pentaksir_bmsetara_calon WHERE id = '" & dataKey & "'"
                Dim StatusHantarBM4_Pentaksir As String = oCommon.getFieldValue(strSQL)

                cell = New PdfPCell()
                cetak = StatusHantarBM4_Pentaksir
                myPara001 = New Paragraph(cetak, FontFactory.GetFont("Arial", 8))
                myPara001.Alignment = Element.ALIGN_CENTER
                cell.AddElement(myPara001)
                cell.Border = Rectangle.BOX
                cell.VerticalAlignment = Element.ALIGN_MIDDLE
                table.AddCell(cell)

            Next

            strRet = BindData(datRespondent)

            myDocument.Add(table)


            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

            myDocument.Close()

            HttpContext.Current.Response.Write(myDocument)
            HttpContext.Current.Response.End()


        Catch ex As Exception


        End Try

    End Sub
End Class

