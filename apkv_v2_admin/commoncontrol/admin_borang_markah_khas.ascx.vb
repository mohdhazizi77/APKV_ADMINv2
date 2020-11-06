
Imports System.Data.SqlClient
Imports iTextSharp.text
Imports iTextSharp.text.pdf

Public Class admin_borang_markah_khas
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


                strSQL = "SELECT UserID FROM kpmkv_users WHERE LoginID='" & Session("LoginID") & "'"
                lblPemeriksa.Text = oCommon.getFieldValue(strSQL)

                If lblPemeriksa.Text = "1" Then
                Else
                    strSQL = " SELECT KertasNo FROM  kpmkv_pemeriksa "
                    strSQL += " WHERE UserID='" & lblPemeriksa.Text & "' AND Tahun='" & Now.Year & "'"
                    strRet = oCommon.getFieldValueEx(strSQL)

                    Dim ar_pemeriksa As Array
                    ar_pemeriksa = strRet.Split("|")
                    lblKertas.Text = ar_pemeriksa(0)
                End If

                loadStores()
            End If
        Catch ex As Exception
            lblMsg.Text = ex.Message
        End Try
    End Sub
    Protected Sub loadStores()
        objConn.Open()
        If lblPemeriksa.Text = "1" Then
            strSQL = "SELECT * from kpmkv_markah_khas WHERE Tahun='" & Now.Year & "'"
        Else
            strSQL = "SELECT * from kpmkv_markah_khas WHERE PemeriksaID='" & lblPemeriksa.Text & "' AND Tahun='" & Now.Year & "' AND KertasNo='" & lblKertas.Text & "'"
        End If

        Dim cmd As New SqlCommand(strSQL, objConn)

        Dim da As New SqlDataAdapter(cmd)
        Dim ds As New DataSet()
        da.Fill(ds)
        Dim count As Integer = ds.Tables(0).Rows.Count
        objConn.Close()
        If ds.Tables(0).Rows.Count > 0 Then
            gridView.DataSource = ds
            gridView.DataBind()
        Else
            ds.Tables(0).Rows.Add(ds.Tables(0).NewRow())
            gridView.DataSource = ds
            gridView.DataBind()
            Dim columncount As Integer = gridView.Rows(0).Cells.Count
            lblMsg.Text = " Tiada Rekod !!!"
        End If
        hiddencolumn()
    End Sub
    Private Sub hiddencolumn()

        Select Case lblKertas.Text
            Case "1"
                gridView.Columns.Item("3").Visible = True
                gridView.Columns.Item("4").Visible = True
                gridView.Columns.Item("5").Visible = False
                gridView.Columns.Item("6").Visible = False

            Case "2"
                gridView.Columns.Item("3").Visible = True
                gridView.Columns.Item("4").Visible = False
                gridView.Columns.Item("5").Visible = False
                gridView.Columns.Item("6").Visible = True
        End Select

    End Sub
    Protected Sub gridView_RowEditing(ByVal sender As Object, ByVal e As GridViewEditEventArgs)
        gridView.EditIndex = e.NewEditIndex
        loadStores()
    End Sub
    Protected Sub gridView_RowDeleting(ByVal sender As Object, ByVal e As GridViewDeleteEventArgs)
        Dim KhasID As String = gridView.DataKeys(e.RowIndex).Values("KhasID").ToString()
        objConn.Open()
        Dim cmd As New SqlCommand("DELETE FROM kpmkv_markah_khas WHERE KhasID=" + KhasID, objConn)
        Dim result As Integer = cmd.ExecuteNonQuery()
        objConn.Close()
        If result = 1 Then
            loadStores()
            lblMsg.Text = "      Berjaya Padam.......    "
        End If
    End Sub
    Protected Sub gridView_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim KhasID As String = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "KhasID"))
            Dim lnkbtnresult As Button = DirectCast(e.Row.FindControl("ButtonDelete"), Button)
            If lnkbtnresult IsNot Nothing Then
                lnkbtnresult.Attributes.Add("onclick", (Convert.ToString("javascript:return deleteConfirm('") & KhasID) + "')")
            End If
        End If
    End Sub
    Protected Sub gridView_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs)
        If e.CommandName.Equals("AddNew") Then
            Dim inMykad As TextBox = gridView.FooterRow.FindControl("inMykad")
            Dim inKod As TextBox = gridView.FooterRow.FindControl("inKod")
            Dim inAngkaGiliran As TextBox = gridView.FooterRow.FindControl("inAngkaGiliran")
            Dim inKertas1 As TextBox = gridView.FooterRow.FindControl("inKertas1")
            Dim inKertas2 As TextBox = gridView.FooterRow.FindControl("inKertas2")
            Dim inCatatan As TextBox = gridView.FooterRow.FindControl("inCatatan")
            Dim inCatatan2 As TextBox = gridView.FooterRow.FindControl("inCatatan2")

            'assign value to integer
            Dim strMykad As String = inMykad.Text
            Dim strKod As String = inKod.Text
            Dim strAngkaGiliran As String = inAngkaGiliran.Text
            Dim strKertas1 As String = inKertas1.Text
            Dim strKertas2 As String = inKertas2.Text
            Dim strCatatan As String = inCatatan.Text
            Dim strCatatan2 As String = inCatatan2.Text

            objConn.Open()
            If lblPemeriksa.Text = "1" Then
                strSQL = "INSERT into kpmkv_markah_khas(Mykad,Kod,AngkaGiliran,KertasNo,Kertas1,Kertas2,Catatan,Catatan2,PemeriksaID,Tahun) values('" & strMykad & "','" & strKod & "','" & strAngkaGiliran & "','0','" & strKertas1 & "','" & strKertas2 & "','" & strCatatan & "','" & strCatatan2 & "','1','" & Now.Year & "')"
            Else
                strSQL = "INSERT into kpmkv_markah_khas(Mykad,Kod,AngkaGiliran,KertasNo,Kertas1,Kertas2,Catatan,Catatan2,PemeriksaID,Tahun) values('" & strMykad & "','" & strKod & "','" & strAngkaGiliran & "','" & lblKertas.Text & "','" & strKertas1 & "','" & strKertas2 & "','" & strCatatan & "','" & strCatatan2 & "','" & lblPemeriksa.Text & "','" & Now.Year & "')"
            End If

            Dim cmd As New SqlCommand(strSQL, objConn)

            Dim result As Integer = cmd.ExecuteNonQuery()
            objConn.Close()
            If result = 1 Then
                loadStores()

                lblMsg.Text = inMykad.Text + "      Berjaya......    "
            Else

                lblMsg.Text = inMykad.Text + " Tidak Berjaya....."
            End If
        End If
    End Sub


    Protected Sub btnCetak_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCetak.Click
        Dim myDocument As New Document(PageSize.A4)

        Try
            HttpContext.Current.Response.ContentType = "application/pdf"
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=BorangMarkahKhas.pdf")
            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache)

            PdfWriter.GetInstance(myDocument, HttpContext.Current.Response.OutputStream)

            myDocument.Open()

            ''--draw spacing
            Dim imgdrawSpacing As String = Server.MapPath("~/img/spacing.png")
            Dim imgSpacing As Image = Image.GetInstance(imgdrawSpacing)
            imgSpacing.Alignment = Image.LEFT_ALIGN  'left
            imgSpacing.Border = 0

            Dim table As New PdfPTable(1)
            table.WidthPercentage = 100
            table.SetWidths({100})
            table.DefaultCell.Border = 0

            Dim cell = New PdfPCell()
            Dim cetak As String = ""

            cetak = "BORANG MARKAH KHAS" & Environment.NewLine
            cetak += Environment.NewLine
            cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 7, Font.UNDERLINE)))
            cell.Border = 0
            table.AddCell(cell)

            myDocument.Add(table)

            table = New PdfPTable(7)
            table.WidthPercentage = 100
            table.SetWidths({10, 5, 10, 12, 26, 12, 26})
            table.DefaultCell.Border = 0

            ''Mykad
            cell = New PdfPCell()
            cetak = ""

            cetak = "Mykad"
            cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 7, Font.BOLD)))
            cell.Border = 0
            table.AddCell(cell)
            ''kod
            cell = New PdfPCell()
            cetak = ""

            cetak = "Kod Pusat"
            cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 7, Font.BOLD)))
            cell.Border = 0
            table.AddCell(cell)

            ''AngkaGiliran
            cell = New PdfPCell()
            cetak = ""

            cetak = "AngkaGiliran"
            cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 7, Font.BOLD)))
            cell.Border = 0
            table.AddCell(cell)

            ''Markah Kertas 1
            cell = New PdfPCell()
            cetak = ""

            cetak = "Markah Kertas 1"
            cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 7, Font.BOLD)))
            cell.Border = 0
            table.AddCell(cell)

            ''Catatan
            cell = New PdfPCell()
            cetak = ""

            cetak = "Catatan"
            cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 7, Font.BOLD)))
            cell.Border = 0
            table.AddCell(cell)

            ''Markah Kertas 2
            cell = New PdfPCell()
            cetak = ""

            cetak = "Markah Kertas 2"
            cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 7, Font.BOLD)))
            cell.Border = 0
            table.AddCell(cell)

            ''Catatan
            cell = New PdfPCell()
            cetak = ""

            cetak = "Catatan"
            cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 7, Font.BOLD)))
            cell.Border = 0
            table.AddCell(cell)

            myDocument.Add(table)

            Dim i As Integer

            For i = 0 To gridView.Rows.Count - 1

                Dim strMykad As Label = CType(gridView.Rows(i).FindControl("lblMykad"), Label)
                Dim strKod As Label = CType(gridView.Rows(i).FindControl("lblKod"), Label)
                Dim strAngkaGiliran As Label = CType(gridView.Rows(i).FindControl("lblAngkaGiliran"), Label)
                Dim strKertas1 As Label = CType(gridView.Rows(i).FindControl("lblKertas1"), Label)
                Dim strCatatan1 As Label = CType(gridView.Rows(i).FindControl("lblCatatan"), Label)
                Dim strKertas2 As Label = CType(gridView.Rows(i).FindControl("lblKertas2"), Label)
                Dim strCatatan2 As Label = CType(gridView.Rows(i).FindControl("lblCatatan2"), Label)

                table = New PdfPTable(7)
                table.WidthPercentage = 100
                table.SetWidths({10, 5, 10, 12, 26, 12, 26})
                table.DefaultCell.Border = 0

                ''Mykad
                cell = New PdfPCell()
                cetak = ""

                cetak = strMykad.Text
                cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 7)))
                cell.Border = 0
                table.AddCell(cell)

                ''Kod
                cell = New PdfPCell()
                cetak = ""

                cetak = strKod.Text
                cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 7)))
                cell.Border = 0
                table.AddCell(cell)

                ''AngkaGiliran
                cell = New PdfPCell()
                cetak = ""

                cetak = strAngkaGiliran.Text
                cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 7)))
                cell.Border = 0
                table.AddCell(cell)

                ''Markah Kertas 1
                cell = New PdfPCell()
                cetak = ""

                cetak = strKertas1.Text
                cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 7)))
                cell.Border = 0
                table.AddCell(cell)

                ''Catatan
                cell = New PdfPCell()
                cetak = ""

                cetak = strCatatan1.Text
                cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 7)))
                cell.Border = 0
                table.AddCell(cell)

                ''Markah Kertas 2
                cell = New PdfPCell()
                cetak = ""

                cetak = strKertas2.Text
                cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 7)))
                cell.Border = 0
                table.AddCell(cell)

                ''Catatan
                cell = New PdfPCell()
                cetak = ""

                cetak = strCatatan2.Text
                cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 7)))
                cell.Border = 0
                table.AddCell(cell)

                myDocument.Add(table)

            Next

            myDocument.Close()

            HttpContext.Current.Response.Write(myDocument)
            HttpContext.Current.Response.End()

        Catch ex As Exception

        End Try
    End Sub


End Class