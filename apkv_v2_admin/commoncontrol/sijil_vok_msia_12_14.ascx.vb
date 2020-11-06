Imports System.Data.SqlClient
Imports System.IO
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports System.Security.Cryptography

Public Class sijil_vok_msia_12_14
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
                lblMsg.Text = "Rekod tidak dijumpai atau pelajar tidak memenuhi syarat Layak Sijil SVM"
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
                  FROM SVM12_14 WHERE Mykad IS NOT NULL AND IsLayak = '1'"

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

    Protected Sub chkSesi_SelectedIndexChanged(sender As Object, e As EventArgs) Handles chkSesi.SelectedIndexChanged
        kpmkv_kodkursus_list()
    End Sub

    Protected Sub ddltahun_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTahun.SelectedIndexChanged
        kpmkv_kodkursus_list()
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

                qrString = Convert.ToBase64String(ms.ToArray())

            End Using

        End Using

        Return qrString

    End Function


    Protected Sub btnPrintSlip_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnPrintSlip.Click

        Dim myDocument As New Document(PageSize.A4)

        HttpContext.Current.Response.ContentType = "application/pdf"
        HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=SijilVokMsia12_14.pdf")
        HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache)

        PdfWriter.GetInstance(myDocument, HttpContext.Current.Response.OutputStream)

        myDocument.Open()

        ''--draw spacing
        Dim imgdrawSpacing As String = Server.MapPath("~/img/spacing.png")
        Dim imgSpacing As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(imgdrawSpacing)
        imgSpacing.Alignment = iTextSharp.text.Image.LEFT_ALIGN  'left
        imgSpacing.Border = 0


        strSQL = " SELECT ID,Mykad,Nama,AngkaGiliran,Kursus,Kluster,Kohort,PNGKAKA,PNGKVOK,Kolej,SVM,Negeri,BMSetara "
        strSQL += " FROM SVM12_14 WHERE ID IS NOT NULL"
        strSQL += " AND IsLayak = '1'"

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

                Dim encryptedStrkey As String = HttpUtility.UrlEncode(Encrypt(strkey.Trim()))

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
                table.WidthPercentage = 100
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
                myDocument.Add(imgSpacing)
                myDocument.Add(imgSpacing)
                myDocument.Add(imgSpacing)
                myDocument.Add(imgSpacing)

                ''PROFILE STARTS HERE

                ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

                Dim cell = New PdfPCell()
                Dim cetak = Environment.NewLine & ""

                ''NAMA
                table = New PdfPTable(4)
                table.WidthPercentage = 100
                table.SetWidths({0, 28, 3, 64})

                cell = New PdfPCell()
                cetak = ""
                cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 11)))
                cell.Border = 0
                table.AddCell(cell)

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
                table = New PdfPTable(4)
                table.WidthPercentage = 100
                table.SetWidths({0, 28, 3, 64})

                cell = New PdfPCell()
                cetak = ""
                cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 11)))
                cell.Border = 0
                table.AddCell(cell)

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
                table = New PdfPTable(4)
                table.WidthPercentage = 100
                table.SetWidths({0, 28, 3, 64})

                cell = New PdfPCell()
                cetak = ""
                cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 11)))
                cell.Border = 0
                table.AddCell(cell)

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
                table = New PdfPTable(4)
                table.WidthPercentage = 100
                table.SetWidths({0, 28, 3, 64})

                cell = New PdfPCell()
                cetak = ""
                cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 11)))
                cell.Border = 0
                table.AddCell(cell)

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
                table = New PdfPTable(4)
                table.WidthPercentage = 100
                table.SetWidths({0, 28, 3, 64})

                cell = New PdfPCell()
                cetak = ""
                cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 11)))
                cell.Border = 0
                table.AddCell(cell)

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
                table = New PdfPTable(4)
                table.WidthPercentage = 100
                table.SetWidths({0, 28, 3, 64})

                cell = New PdfPCell()
                cetak = ""
                cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 11)))
                cell.Border = 0
                table.AddCell(cell)

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

                ''profile ends here
                ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''


                table = New PdfPTable(4)
                table.WidthPercentage = 100
                table.SetWidths({0, 75, 15, 5})
                table.DefaultCell.Border = 0

                cell = New PdfPCell()
                cetak = ""
                cetak += ""
                cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 11)))
                cell.Border = 0
                table.AddCell(cell)

                cell = New PdfPCell()
                cetak = ""
                cetak += "BAHASA MELAYU KOLEJ VOKASIONAL 1104"
                cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 11)))
                cell.Border = 0
                table.AddCell(cell)

                cell = New PdfPCell()
                cetak = ""
                cetak += "GRED " & strGred
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


                'Kluster---------------------------------------------------------------------------

                table = New PdfPTable(4)
                table.WidthPercentage = 100
                table.SetWidths({0, 75, 15, 5})
                table.DefaultCell.Border = 0

                cell = New PdfPCell()
                cetak = ""
                cetak += ""
                cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 11)))
                cell.Border = 0
                table.AddCell(cell)

                cell = New PdfPCell()
                cetak = ""
                cetak += "KOMPETEN SEMUA MODUL " & strbidang
                cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 11)))
                cell.Border = 0
                table.AddCell(cell)

                cell = New PdfPCell()
                cetak = ""
                cetak += ""
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


                'pngka---------------------------------------------------------------------------

                table = New PdfPTable(4)
                table.WidthPercentage = 100
                table.SetWidths({0, 75, 15, 5})
                table.DefaultCell.Border = 0

                cell = New PdfPCell()
                cetak = ""
                cetak += ""
                cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 11)))
                cell.Border = 0
                table.AddCell(cell)

                cell = New PdfPCell()
                cetak = ""
                cetak += "PURATA NILAI GRED KUMULATIF AKADEMIK (PNGKA)"
                cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 11)))
                cell.Border = 0
                table.AddCell(cell)

                cell = New PdfPCell()
                cetak = ""
                cetak += FormatNumber(CDbl(strPNGKA), 2)
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

                'pngkv----------------------------------------------------------------------------

                table = New PdfPTable(4)
                table.WidthPercentage = 100
                table.SetWidths({0, 75, 15, 5})
                table.DefaultCell.Border = 0

                cell = New PdfPCell()
                cetak = ""
                cetak += ""
                cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 11)))
                cell.Border = 0
                table.AddCell(cell)

                cell = New PdfPCell()
                cetak = ""
                cetak += "PURATA NILAI GRED KUMULATIF VOKASIONAL (PNGKV)"
                cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 11)))
                cell.Border = 0
                table.AddCell(cell)

                cell = New PdfPCell()
                cetak = ""
                cetak += FormatNumber(CDbl(strPNGKV), 2)
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

                'footer------------------------------------------------------------------------------
                If strStatus = "SETARA" Then
                    table = New PdfPTable(3)
                    table.WidthPercentage = 100
                    table.SetWidths({0, 90, 5})
                    table.DefaultCell.Border = 0

                    cell = New PdfPCell()
                    cetak = ""
                    cetak += ""
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 11)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = ""
                    cetak += "Lembaga Peperiksaan memperakukan bahawa calon yang namanya tersebut "
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

                    table = New PdfPTable(3)
                    table.WidthPercentage = 100
                    table.SetWidths({0, 90, 5})
                    table.DefaultCell.Border = 0

                    cell = New PdfPCell()
                    cetak = ""
                    cetak += ""
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 11)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = ""
                    cetak += "di atas ini telah dianugerahkan Sijil Vokasional Malaysia yang setara dengan  "
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

                    table = New PdfPTable(3)
                    table.WidthPercentage = 100
                    table.SetWidths({0, 90, 5})
                    table.DefaultCell.Border = 0

                    cell = New PdfPCell()
                    cetak = ""
                    cetak += ""
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 11)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = ""
                    cetak += "3 kredit Sijil Pelajaran Malaysia."
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

                End If

                If printQR.SelectedValue = 1 Then

                    Dim hints As IDictionary = New Dictionary(Of qrcode.EncodeHintType, Object)
                    hints.Add(qrcode.EncodeHintType.ERROR_CORRECTION, qrcode.ErrorCorrectionLevel.Q)
                    Dim qr As BarcodeQRCode = New BarcodeQRCode("http://apkv.moe.gov.my/svm.pengesahan.aspx?id=" & encryptedStrkey, 150, 150, hints)
                    'Dim qr As BarcodeQRCode = New BarcodeQRCode("http://localhost:56105/svm.pengesahan.aspx?id=" & encryptedStrkey, 150, 150, hints)
                    Dim qrImage As iTextSharp.text.Image = qr.GetImage()
                    qrImage.SetAbsolutePosition(250, 15)
                    qrImage.ScalePercent(60)
                    myDocument.Add(qrImage)

                End If



                strSQL = " Select FileLocation FROM kpmkv_config_pengarahPeperiksaan WHERE ID='" & ddlSign.SelectedValue & "'"
                Dim FullFileName As String = oCommon.getFieldValue(strSQL)

                Dim imageHeader As String = String.Concat(Server.MapPath("~/signature/" + FullFileName))

                'Dim imageHeader As String = Server.MapPath(fileSavePath)
                Dim imgHeader As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(imageHeader)
                imgHeader.ScalePercent(22)
                imgHeader.SetAbsolutePosition(410, 32)

                myDocument.Add(imgHeader)

                ''isbmtahun
                strSQL = " SELECT isBMTahun FROM kpmkv_pelajar WHERE PelajarID = '" & strkey & "'"
                Dim strIsBMTahun As String = oCommon.getFieldValue(strSQL)

                ''kod
                strSQL = " SELECT Kod FROM kpmkv_tahun WHERE Tahun = '" & strIsBMTahun & "'"
                Dim strKod As String = oCommon.getFieldValue(strSQL)

                ''runningno
                strSQL = " SELECT REPLACE(NoSiri,' ','') as NoSiri FROM SVM12_14 WHERE ID = '" & strkey & "'"
                Dim strRunningNo As String = oCommon.getFieldValue(strSQL)


                ''agency font
                Dim fontPath As String = String.Concat(Server.MapPath("~/font/"))
                Dim bfAgency As BaseFont = BaseFont.CreateFont(fontPath & "agency.ttf", BaseFont.CP1252, BaseFont.EMBEDDED)

                Dim agencyFont As iTextSharp.text.Font = New iTextSharp.text.Font(bfAgency, 15)

                table = New PdfPTable(3)
                table.WidthPercentage = 100
                table.SetWidths({0, 90, 5})
                table.SetExtendLastRow(True, True)
                table.DefaultCell.Border = 1

                cell = New PdfPCell()
                cetak = ""
                cetak += ""
                cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 11)))
                cell.Border = 0
                table.AddCell(cell)

                cell = New PdfPCell
                cetak = strRunningNo & Environment.NewLine
                cetak += " "
                cell.AddElement(New Paragraph(cetak, agencyFont))
                cell.VerticalAlignment = Element.ALIGN_BOTTOM
                cell.Border = 0
                table.AddCell(cell)

                cell = New PdfPCell()
                cetak = ""
                cetak += ""
                cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 11)))
                cell.Border = 0
                table.AddCell(cell)

                myDocument.Add(table)

                ''--content end

            End If

        Next

        myDocument.Close()

        HttpContext.Current.Response.Write(myDocument)
        HttpContext.Current.Response.End()
    End Sub

End Class