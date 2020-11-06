Imports System.Data.SqlClient
Imports System.IO
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports System.Security.Cryptography

Public Class SlipBm1104_12_14
    Inherits System.Web.UI.UserControl
    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                kpmkv_kolej_list()
                kpmkv_tahun_list()
                kpmkv_kodkursus_list()
                kpmkv_Pengarah_list()


            End If

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message
        End Try
    End Sub
    Private Sub kpmkv_kolej_list()
        strSQL = "SELECT DISTINCT UPPER(Kolej) as Kolej from SVM12_14 ORDER BY Kolej ASC"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlKolej.DataSource = ds
            ddlKolej.DataTextField = "Kolej"
            ddlKolej.DataValueField = "Kolej"
            ddlKolej.DataBind()

            ddlKolej.Items.Insert(0, "-PILIH-")
        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub kpmkv_tahun_list()
        strSQL = "SELECT DISTINCT Kohort FROM SVM12_14 ORDER BY Kohort DESC"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlTahun.DataSource = ds
            ddlTahun.DataTextField = "Kohort"
            ddlTahun.DataValueField = "Kohort"
            ddlTahun.DataBind()
            ddlTahun.Items.Insert(0, "-PILIH-")
        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub kpmkv_kodkursus_list()

        strSQL = "SELECT DISTINCT UPPER(kursus) as Kursus FROM SVM12_14 "

        If Not ddlTahun.SelectedValue = "-PILIH-" Then
            strSQL += " WHERE Kohort = '" & ddlTahun.SelectedValue & "' "
        End If

        strSQL += " ORDER BY Kursus ASC"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlKodKursus.DataSource = ds
            ddlKodKursus.DataTextField = "Kursus"
            ddlKodKursus.DataValueField = "Kursus"
            ddlKodKursus.DataBind()

            ddlKodKursus.Items.Insert(0, "-PILIH-")
        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub kpmkv_Pengarah_list()
        strSQL = "SELECT ID,NamaPengarah FROM kpmkv_config_pengarahPeperiksaan"
        strSQL += " WHERE Status='AKTIF' ORDER BY NamaPengarah ASC"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlSign.DataSource = ds
            ddlSign.DataTextField = "NamaPengarah"
            ddlSign.DataValueField = "ID"
            ddlSign.DataBind()
        Catch ex As Exception

        Finally
            objConn.Dispose()
        End Try

    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        lblMsg.Text = ""
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
        strSQL = "SELECT ID,REPLACE(NoSiri,' ','') as NoSiri,Kohort,Sesi,Mykad,AngkaGiliran,Nama,kolej,Negeri,Kluster,Kursus,BMSetara,PNGKAKA,PNGKVOK
                  FROM SVM12_14 WHERE Mykad IS NOT NULL "

        If Not txtNama.Text = "" Then
            strSQL += " AND Nama LIKE '%" & oCommon.FixSingleQuotes(txtNama.Text) & "%'"
        End If
        If Not txtMYKAD.Text = "" Then
            strSQL += " AND Mykad LIKE '%" & oCommon.FixSingleQuotes(txtMYKAD.Text) & "%'"
        End If
        If Not txtAngkaGiliran.Text = "" Then
            strSQL += " AND AngkaGiliran LIKE '%" & oCommon.FixSingleQuotes(txtAngkaGiliran.Text) & "%'"
        End If

        If Not ddlTahun.Text = "-PILIH-" Then
            strSQL += " AND Kohort ='" & ddlTahun.Text & "'"
        End If

        If Not chkSesi.Text = "" Then
            strSQL += " AND Sesi ='" & chkSesi.Text & "'"
        End If

        If Not ddlKodKursus.Text = "-PILIH-" Then
            strSQL += " AND Kursus ='" & ddlKodKursus.SelectedValue & "'"
        End If

        If Not ddlKolej.SelectedValue = "-PILIH-" Then
            strSQL += " AND Kolej ='" & ddlKolej.SelectedValue & "'"
        End If

        strSQL += " ORDER BY Nama ASC"

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

    Protected Sub ddltahun_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTahun.SelectedIndexChanged
        kpmkv_kodkursus_list()
    End Sub

    Protected Sub btnPrintSlip_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnPrintSlip.Click
        Dim myDocument As New Document(PageSize.A4)

        Try
            HttpContext.Current.Response.ContentType = "application/pdf"
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=SlipBMSetara1104_12_14.pdf")
            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache)

            PdfWriter.GetInstance(myDocument, HttpContext.Current.Response.OutputStream)

            myDocument.Open()

            ''--draw spacing
            Dim imgdrawSpacing As String = Server.MapPath("~/img/spacing.png")
            Dim imgSpacing As Image = Image.GetInstance(imgdrawSpacing)
            imgSpacing.Alignment = Image.LEFT_ALIGN  'left
            imgSpacing.Border = 0

            strSQL = " SELECT ID,Mykad,Nama,AngkaGiliran,Kursus,Kluster,Kohort,PNGKAKA,PNGKVOK,Kolej,SVM,Negeri,BMSetara "
            strSQL += " FROM SVM12_14 WHERE ID IS NOT NULL"

            If Not txtNama.Text = "" Then
                strSQL += " AND Nama LIKE '%" & oCommon.FixSingleQuotes(txtNama.Text) & "%'"
            End If
            If Not txtMYKAD.Text = "" Then
                strSQL += " AND Mykad LIKE '%" & oCommon.FixSingleQuotes(txtMYKAD.Text) & "%'"
            End If
            If Not txtAngkaGiliran.Text = "" Then
                strSQL += " AND AngkaGiliran LIKE '%" & oCommon.FixSingleQuotes(txtAngkaGiliran.Text) & "%'"
            End If

            If Not ddlTahun.Text = "-PILIH-" Then
                strSQL += " AND Kohort ='" & ddlTahun.Text & "'"
            End If

            If Not chkSesi.Text = "" Then
                strSQL += " AND Sesi ='" & chkSesi.Text & "'"
            End If

            If Not ddlKodKursus.Text = "-PILIH-" Then
                strSQL += " AND Kursus ='" & ddlKodKursus.SelectedValue & "'"
            End If

            If Not ddlKolej.SelectedValue = "-PILIH-" Then
                strSQL += " AND Kolej ='" & ddlKolej.SelectedValue & "'"
            End If

            strSQL += " ORDER BY Nama ASC"

            strRet = oCommon.ExecuteSQL(strSQL)

            Dim sqlDA As New SqlDataAdapter(strSQL, objConn)
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1

                Dim cb As CheckBox = datRespondent.Rows(i).FindControl("chkSelect")

                If cb.Checked = True Then

                    Dim strkey As String = ds.Tables(0).Rows(i).Item(0).ToString

                    Dim strmykad As String = ds.Tables(0).Rows(i).Item(1).ToString
                    Dim strname As String = ds.Tables(0).Rows(i).Item(2).ToString
                    Dim strag As String = ds.Tables(0).Rows(i).Item(3).ToString
                    Dim strProgram As String = ds.Tables(0).Rows(i).Item(4).ToString
                    Dim strbidang As String = ds.Tables(0).Rows(i).Item(5).ToString
                    Dim strTahun As String = ds.Tables(0).Rows(i).Item(6).ToString
                    Dim strPNGKA As String = ds.Tables(0).Rows(i).Item(7).ToString
                    Dim strPNGKV As String = ds.Tables(0).Rows(i).Item(8).ToString
                    Dim strKolej As String = ds.Tables(0).Rows(i).Item(9).ToString
                    Dim strStatus As String = ds.Tables(0).Rows(i).Item(10).ToString
                    Dim strNegeri As String = ds.Tables(0).Rows(i).Item(11).ToString
                    Dim strGred As String = ds.Tables(0).Rows(i).Item(12).ToString
                    ''getting data end

                    Dim table As New PdfPTable(3)
                    table.WidthPercentage = 102
                    table.SetWidths({42, 16, 42})
                    table.DefaultCell.Border = 0

                    myDocument.Add(table)

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

                    Dim cell = New PdfPCell()
                    Dim cetak = Environment.NewLine & ""

                    table = New PdfPTable(1)
                    table.WidthPercentage = 102
                    table.SetWidths({100})

                    ''timesbd font
                    Dim fontPath As String = String.Concat(Server.MapPath("~/font/"))
                    Dim bfTimesbd As BaseFont = BaseFont.CreateFont(fontPath & "timesbd.ttf", BaseFont.CP1252, BaseFont.EMBEDDED)

                    Dim timesbdFont As iTextSharp.text.Font = New iTextSharp.text.Font(bfTimesbd, 12)

                    cell = New PdfPCell()
                    cetak = "Calon yang tersebut namanya di bawah telah mengambil peperiksaan Bahasa Melayu Kod 1104 dan " & Environment.NewLine
                    cetak += "menunjukkan prestasi seperti yang tercatat di bawah."
                    cell.AddElement(New Paragraph(cetak, timesbdFont))
                    cell.Border = 0
                    table.AddCell(cell)

                    myDocument.Add(table)

                    myDocument.Add(imgSpacing)
                    myDocument.Add(imgSpacing)

                    ''PROFILE STARTS HERE

                    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

                    ''NAMA
                    table = New PdfPTable(3)
                    table.WidthPercentage = 102
                    table.SetWidths({30, 3, 67})

                    cell = New PdfPCell()
                    cetak = "NAMA"
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 11)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = " : "
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 11)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = strname
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 11)))
                    cell.Border = 0
                    table.AddCell(cell)

                    myDocument.Add(table)




                    ''NO. KAD PENGENALAN
                    table = New PdfPTable(3)
                    table.WidthPercentage = 102
                    table.SetWidths({30, 3, 67})

                    cell = New PdfPCell()
                    cetak = "NO.KAD PENGENALAN"
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 11)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = " : "
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 11)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = strmykad
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 11)))
                    cell.Border = 0
                    table.AddCell(cell)

                    myDocument.Add(table)




                    ''ANGKA GILIRAN
                    table = New PdfPTable(3)
                    table.WidthPercentage = 102
                    table.SetWidths({30, 3, 67})

                    cell = New PdfPCell()
                    cetak = "ANGKA GILIRAN"
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 11)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = " : "
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 11)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = strag
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 11)))
                    cell.Border = 0
                    table.AddCell(cell)

                    myDocument.Add(table)




                    ''INSTITUSI
                    table = New PdfPTable(3)
                    table.WidthPercentage = 102
                    table.SetWidths({30, 3, 67})

                    cell = New PdfPCell()
                    cetak = "INSTITUSI"
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 11)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = " : "
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 11)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = strKolej & ", " & strNegeri
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 11)))
                    cell.Border = 0
                    table.AddCell(cell)

                    myDocument.Add(table)




                    ''KLUSTER
                    table = New PdfPTable(3)
                    table.WidthPercentage = 102
                    table.SetWidths({30, 3, 67})

                    cell = New PdfPCell()
                    cetak = "KLUSTER"
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 11)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = " : "
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 11)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = strbidang
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 11)))
                    cell.Border = 0
                    table.AddCell(cell)

                    myDocument.Add(table)




                    ''KURSUS
                    table = New PdfPTable(3)
                    table.WidthPercentage = 102
                    table.SetWidths({30, 3, 67})

                    cell = New PdfPCell()
                    cetak = "KURSUS"
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 11)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = " : "
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 11)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = strProgram
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 11)))
                    cell.Border = 0
                    table.AddCell(cell)

                    myDocument.Add(table)

                    myDocument.Add(imgSpacing)
                    myDocument.Add(imgSpacing)
                    myDocument.Add(imgSpacing)

                    ''profile ends here
                    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

                    table = New PdfPTable(5)
                    table.WidthPercentage = 102
                    table.SetWidths({30, 4, 50, 18, 1})

                    cell = New PdfPCell()
                    cetak = "KOD"
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 11)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = ""
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 11)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = "MATA PELAJARAN"
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 11)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = "GRED"
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 11)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = ""
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 11)))
                    cell.Border = 0
                    table.AddCell(cell)

                    myDocument.Add(table)


                    table = New PdfPTable(5)
                    table.WidthPercentage = 102
                    table.SetWidths({30, 4, 50, 18, 1})

                    cell = New PdfPCell()
                    cetak = ""
                    cetak += "1104"
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 11)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = ""
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 11)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = ""
                    cetak += "BAHASA MELAYU KOLEJ VOKASIONAL"
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 11)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = ""
                    cetak += strGred
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 11)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = ""
                    cetak += ""
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 11)))
                    cell.Border = 0
                    table.AddCell(cell)

                    Debug.WriteLine(cetak)
                    myDocument.Add(table)

                    myDocument.Add(imgSpacing)
                    myDocument.Add(imgSpacing)
                    myDocument.Add(imgSpacing)
                    myDocument.Add(imgSpacing)
                    myDocument.Add(imgSpacing)
                    myDocument.Add(imgSpacing)
                    myDocument.Add(imgSpacing)
                    myDocument.Add(imgSpacing)
                    myDocument.Add(imgSpacing)

                    ''TT
                    strSQL = " Select FileLocation FROM kpmkv_config_pengarahPeperiksaan WHERE ID='" & ddlSign.SelectedValue & "'"
                    Dim FullFileName As String = oCommon.getFieldValue(strSQL)

                    Dim imageHeader As String = String.Concat(Server.MapPath("~/signature/" + FullFileName))

                    'Dim imageHeader As String = Server.MapPath(fileSavePath)
                    Dim imgHeader As Image = Image.GetInstance(imageHeader)
                    imgHeader.ScalePercent(23)
                    imgHeader.SetAbsolutePosition(355, 75)

                    myDocument.Add(imgHeader)

                    ''Pernyataan ini dijeluarkan....

                    Dim ayatPenyataBM As String = "slipBM_ayatbawah_V1.png"

                    imageHeader = String.Concat(Server.MapPath("~/signature/" + ayatPenyataBM))

                    'Dim imageHeader As String = Server.MapPath(fileSavePath)
                    imgHeader = Image.GetInstance(imageHeader)
                    imgHeader.ScalePercent(37)
                    imgHeader.SetAbsolutePosition(35, 10)

                    myDocument.Add(imgHeader)


                    myDocument.NewPage()
                    ''--content end


                    myDocument.NewPage()

                End If

            Next

            myDocument.Close()

            HttpContext.Current.Response.Write(myDocument)
            HttpContext.Current.Response.End()

        Catch ex As Exception

        End Try
    End Sub
End Class