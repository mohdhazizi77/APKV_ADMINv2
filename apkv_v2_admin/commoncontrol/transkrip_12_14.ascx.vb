Imports System.Data.SqlClient
Imports System.IO
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports System.Security.Cryptography

Public Class transkrip_12_141

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
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=TranskripSijilVokasional_12_14.pdf")
            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache)

            PdfWriter.GetInstance(myDocument, HttpContext.Current.Response.OutputStream)

            myDocument.Open()

            ''--draw spacing
            Dim imgdrawSpacing As String = Server.MapPath("~/img/spacing.png")
            Dim imgSpacing As Image = Image.GetInstance(imgdrawSpacing)
            imgSpacing.Alignment = Image.LEFT_ALIGN  'left
            imgSpacing.Border = 0

            strSQL = "  SELECT ID,Mykad,Nama,AngkaGiliran,Kursus,Kluster,Kohort,PNGKAKA,PNGKVOK,Kolej,SVM,Negeri,BMSetara, JUMLAH_AKA, JUMLAH_VOK, PNGK,

                        KOD1SEM1, NAM1SEM1, JK1SEM1, MOD1SEM1,
                        KOD2SEM1, NAM2SEM1, JK2SEM1, MOD2SEM1,
                        KOD3SEM1, NAM3SEM1, JK23SEM1, MOD3SEM1,
                        KOD4SEM1, NAM4SEM1, JK4SEM1, MOD4SEM1,
                        KOD5SEM1, NAM5SEM1, JK5SEM1, MOD5SEM1,
                        KOD6SEM1, NAM6SEM1, JK6SEM1, MOD6SEM1,
                        KOD7SEM1, NAM7SEM1, JK7SEM1, MOD7SEM1,
                        KOD8SEM1, NAM8SEM1, JK8SEM1, MOD8SEM1,
                        KOD9SEM1, NAM9SEM1, JK9SEM1, MOD9SEM1,
                        KOD10SEM1, NAM10SEM1, JK10SEM1, MOD10SEM1,
                        KOD11SEM1, NAM11SEM1, JK11SEM1, MOD11SEM1,
                        KOD12SEM1, NAM12SEM1, JK12SEM1, MOD12SEM1,

                        KOD1SEM2, NAM1SEM2, JK1SEM2, MOD1SEM2,
                        KOD2SEM2, NAM2SEM2, JK2SEM2, MOD2SEM2,
                        KOD3SEM2, NAM3SEM2, JK23SEM2, MOD3SEM2,
                        KOD4SEM2, NAM4SEM2, JK4SEM2, MOD4SEM2,
                        KOD5SEM2, NAM5SEM2, JK5SEM2, MOD5SEM2,
                        KOD6SEM2, NAM6SEM2, JK6SEM2, MOD6SEM2,
                        KOD7SEM2, NAM7SEM2, JK7SEM2, MOD7SEM2,
                        KOD8SEM2, NAM8SEM2, JK8SEM2, MOD8SEM2,
                        KOD9SEM2, NAM9SEM2, JK9SEM2, MOD9SEM2,
                        KOD10SEM2, NAM10SEM2, JK10SEM2, MOD10SEM2,
                        KOD11SEM2, NAM11SEM2, JK11SEM2, MOD11SEM2,
                        KOD12SEM2, NAM12SEM2, JK12SEM2, MOD12SEM2,
                        KOD13SEM2, NAM13SEM2, JK13SEM2, MOD13SEM2,

                        KOD1SEM3, NAM1SEM3, JK1SEM3, MOD1SEM3,
                        KOD2SEM3, NAM2SEM3, JK2SEM3, MOD2SEM3,
                        KOD3SEM3, NAM3SEM3, JK23SEM3, MOD3SEM3,
                        KOD4SEM3, NAM4SEM3, JK4SEM3, MOD4SEM3,
                        KOD5SEM3, NAM5SEM3, JK5SEM3, MOD5SEM3,
                        KOD6SEM3, NAM6SEM3, JK6SEM3, MOD6SEM3,
                        KOD7SEM3, NAM7SEM3, JK7SEM3, MOD7SEM3,
                        KOD8SEM3, NAM8SEM3, JK8SEM3, MOD8SEM3,
                        KOD9SEM3, NAM9SEM3, JK9SEM3, MOD9SEM3,
                        KOD10SEM3, NAM10SEM3, JK10SEM3, MOD10SEM3,
                        KOD11SEM3, NAM11SEM3, JK11SEM3, MOD11SEM3,
                        KOD12SEM3, NAM12SEM3, JK12SEM3, MOD12SEM3,

                        KOD1SEM4, NAM1SEM4, JK1SEM4, MOD1SEM4,
                        KOD2SEM4, NAM2SEM4, JK2SEM4, MOD2SEM4,
                        KOD3SEM4, NAM3SEM4, JK23SEM4, MOD3SEM4,
                        KOD4SEM4, NAM4SEM4, JK4SEM4, MOD4SEM4,
                        KOD5SEM4, NAM5SEM4, JK5SEM4, MOD5SEM4,
                        KOD6SEM4, NAM6SEM4, JK6SEM4, MOD6SEM4,
                        KOD7SEM4, NAM7SEM4, JK7SEM4, MOD7SEM4,
                        KOD8SEM4, NAM8SEM4, JK8SEM4, MOD8SEM4,
                        KOD9SEM4, NAM9SEM4, JK9SEM4, MOD9SEM4,
                        KOD10SEM4, NAM10SEM4, JK10SEM4, MOD10SEM4,
                        KOD11SEM4, NAM11SEM4, JK11SEM4, MOD11SEM4,
                        KOD12SEM4, NAM12SEM4, JK12SEM4, MOD12SEM4,
                        KOD13SEM4, NAM13SEM4, JK13SEM4, MOD13SEM4 "

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
                    Dim strJumlahAKA As String = ds.Tables(0).Rows(i).Item(13).ToString
                    Dim strJumlahVOK As String = ds.Tables(0).Rows(i).Item(14).ToString
                    Dim strPNGKK As String = ds.Tables(0).Rows(i).Item(15).ToString
                    ''getting data end

                    myDocument.Add(imgSpacing)
                    myDocument.Add(imgSpacing)
                    myDocument.Add(imgSpacing)
                    myDocument.Add(imgSpacing)

                    'Transkrip Sijil Vokasional Malaysia
                    Dim fontPath As String = String.Concat(Server.MapPath("~/font/"))
                    Dim bf_TNR As BaseFont = BaseFont.CreateFont(fontPath & "timesbd.ttf", BaseFont.CP1252, BaseFont.EMBEDDED)

                    Dim TNR_BOLD_Font As iTextSharp.text.Font = New iTextSharp.text.Font(bf_TNR, 13)

                    Dim table As New PdfPTable(1)
                    Dim myPara001 As New Paragraph()
                    table.WidthPercentage = 103
                    table.SetWidths({100})
                    table.DefaultCell.Border = 0

                    Dim cell = New PdfPCell()
                    Dim cetak = "Transkrip Sijil Vokasional Malaysia"
                    cell.AddElement(New Paragraph(cetak, TNR_BOLD_Font))
                    cell.Border = 0
                    table.AddCell(cell)

                    myDocument.Add(table)

                    myDocument.Add(imgSpacing)
                    myDocument.Add(imgSpacing)

                    ''PROFILE STARTS HERE

                    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

                    ''NAMA
                    table = New PdfPTable(2)
                    table.WidthPercentage = 103
                    table.SetWidths({30, 70})


                    cell = New PdfPCell()
                    cetak = "NAMA"
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 9)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = ":  " & strname

                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 9)))
                    cell.Border = 0
                    table.AddCell(cell)
                    Debug.WriteLine(cetak)

                    myDocument.Add(table)

                    ''NO. KAD PENGENALAN
                    table = New PdfPTable(2)

                    table.WidthPercentage = 103
                    table.SetWidths({30, 70})


                    cell = New PdfPCell()
                    cetak = "NO.KAD PENGENALAN"
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 9)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = ":  " & strmykad
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 9)))
                    cell.Border = 0
                    table.AddCell(cell)
                    Debug.WriteLine(cetak)

                    myDocument.Add(table)

                    ''ANGKA GILIRAN
                    table = New PdfPTable(2)

                    table.WidthPercentage = 103
                    table.SetWidths({30, 70})


                    cell = New PdfPCell()
                    cetak = "ANGKA GILIRAN"
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 9)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = ":  " & strag
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 9)))
                    cell.Border = 0
                    table.AddCell(cell)
                    Debug.WriteLine(cetak)

                    myDocument.Add(table)

                    ''INSTITUSI
                    table = New PdfPTable(2)

                    table.WidthPercentage = 103
                    table.SetWidths({30, 70})


                    cell = New PdfPCell()
                    cetak = "INSTITUSI"
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 9)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = ":  " & strKolej & ", " & strNegeri
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 9)))
                    cell.Border = 0
                    table.AddCell(cell)
                    Debug.WriteLine(cetak)

                    myDocument.Add(table)

                    ''KLUSTER
                    table = New PdfPTable(2)

                    table.WidthPercentage = 103
                    table.SetWidths({30, 70})


                    cell = New PdfPCell()
                    cetak = "KLUSTER"
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 9)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = ":  " & strbidang
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 9)))
                    cell.Border = 0
                    table.AddCell(cell)
                    Debug.WriteLine(cetak)

                    myDocument.Add(table)

                    ''KURSUS
                    table = New PdfPTable(2)

                    table.WidthPercentage = 103
                    table.SetWidths({30, 70})


                    cell = New PdfPCell()
                    cetak = "KURSUS"
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 9)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = ":  " & strProgram

                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 9)))
                    cell.Border = 0
                    table.AddCell(cell)
                    Debug.WriteLine(cetak)

                    myDocument.Add(table)


                    myDocument.Add(imgSpacing)

                    ''profile ends here
                    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    Dim fontSize65 As Single = 6.5
                    Dim fontSize7 As Single = 7

                    table = New PdfPTable(8)

                    table.WidthPercentage = 103
                    table.SetWidths({35, 11, 7, 7, 35, 11, 7, 6})

                    cell = New PdfPCell()
                    cetak = "SEMESTER 1 KOHORT " & strTahun
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", fontSize7, Font.BOLD)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = ""
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", fontSize7, Font.BOLD)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = " "
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", fontSize7, Font.BOLD)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = " "
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", fontSize7, Font.BOLD)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = "SEMESTER 2 KOHORT " & strTahun
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", fontSize7, Font.BOLD)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = ""
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", fontSize7, Font.BOLD)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = ""
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", fontSize7, Font.BOLD)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = ""
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", fontSize7, Font.BOLD)))
                    cell.Border = 0
                    table.AddCell(cell)

                    myDocument.Add(table)

                    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    table = New PdfPTable(8)

                    table.WidthPercentage = 103
                    table.SetWidths({11, 35, 7, 7, 11, 35, 7, 6})

                    cell = New PdfPCell()
                    cetak = "KOD"
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", fontSize7, Font.BOLD)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = "MATA PELAJARAN"
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", fontSize7, Font.BOLD)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = "JAM KREDIT"
                    myPara001 = New Paragraph(cetak, FontFactory.GetFont("Arial", fontSize7, Font.BOLD))
                    myPara001.Alignment = Element.ALIGN_CENTER
                    cell.AddElement(myPara001)
                    cell.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = "GRED"
                    myPara001 = New Paragraph(cetak, FontFactory.GetFont("Arial", fontSize7, Font.BOLD))
                    myPara001.Alignment = Element.ALIGN_CENTER
                    cell.AddElement(myPara001)
                    cell.HorizontalAlignment = 1
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = "KOD"
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", fontSize7, Font.BOLD)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = "MATA PELAJARAN"
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", fontSize7, Font.BOLD)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = "JAM KREDIT"
                    myPara001 = New Paragraph(cetak, FontFactory.GetFont("Arial", fontSize7, Font.BOLD))
                    myPara001.Alignment = Element.ALIGN_CENTER
                    cell.AddElement(myPara001)
                    cell.HorizontalAlignment = 1
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = "GRED"
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", fontSize7, Font.BOLD)))
                    cell.HorizontalAlignment = 1
                    cell.Border = 0
                    table.AddCell(cell)

                    myDocument.Add(table)
                    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    ''matapelajaran sem 1 & 2
                    table = New PdfPTable(8)
                    table.WidthPercentage = 103
                    table.SetWidths({11, 35, 7, 7, 11, 35, 7, 6})
                    table.DefaultCell.Border = 0

                    cell = New PdfPCell()

                    Dim cetakKOD As String = ""
                    Dim cetakNAM As String = ""
                    Dim cetakJK As String = ""
                    Dim cetakMOD As String = ""

                    Dim countKOD As Integer = 16
                    Dim countNAM As Integer = 17
                    Dim countJK As Integer = 18
                    Dim countMOD As Integer = 19

                    For iloop As Integer = 0 To 11

                        If Not ds.Tables(0).Rows(i).Item(countKOD).ToString = "" Then

                            cetakKOD += ds.Tables(0).Rows(i).Item(countKOD).ToString & Environment.NewLine
                            cetakNAM += ds.Tables(0).Rows(i).Item(countNAM).ToString.ToUpper & Environment.NewLine
                            cetakJK += ds.Tables(0).Rows(i).Item(countJK).ToString & Environment.NewLine
                            cetakMOD += "     " & ds.Tables(0).Rows(i).Item(countMOD).ToString & Environment.NewLine

                        End If

                        countKOD = countKOD + 4
                        countNAM = countNAM + 4
                        countJK = countJK + 4
                        countMOD = countMOD + 4

                    Next

                    cell = New PdfPCell()

                    Debug.WriteLine(cetakKOD)
                    myPara001 = New Paragraph(cetakKOD, FontFactory.GetFont("Arial", fontSize65))
                    myPara001.Alignment = Element.ALIGN_LEFT
                    cell.AddElement(myPara001)
                    cell.Border = 0
                    cell.VerticalAlignment = Element.ALIGN_LEFT
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    Debug.WriteLine(cetakNAM)

                    myPara001 = New Paragraph(cetakNAM, FontFactory.GetFont("Arial", fontSize65))
                    myPara001.Alignment = Element.ALIGN_LEFT
                    cell.AddElement(myPara001)
                    cell.Border = 0
                    cell.VerticalAlignment = Element.ALIGN_LEFT
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    Debug.WriteLine(cetakJK)
                    myPara001 = New Paragraph(cetakJK, FontFactory.GetFont("Arial", fontSize65))
                    myPara001.Alignment = Element.ALIGN_CENTER
                    cell.AddElement(myPara001)
                    cell.Border = 0
                    'cell.VerticalAlignment = Element.ALIGN_MIDDLE
                    cell.HorizontalAlignment = Element.ALIGN_CENTER
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    Debug.WriteLine(cetakMOD)

                    myPara001 = New Paragraph(cetakMOD, FontFactory.GetFont("Arial", fontSize65))
                    myPara001.Alignment = Element.ALIGN_LEFT
                    cell.AddElement(myPara001)
                    cell.Border = 0

                    cell.HorizontalAlignment = Element.ALIGN_CENTER
                    table.AddCell(cell)

                    Dim cetakKOD2 As String = ""
                    Dim cetakNAM2 As String = ""
                    Dim cetakJK2 As String = ""
                    Dim cetakMOD2 As String = ""

                    For iloop As Integer = 0 To 12

                        If Not ds.Tables(0).Rows(i).Item(countKOD).ToString = "" Then

                            cetakKOD2 += ds.Tables(0).Rows(i).Item(countKOD).ToString & Environment.NewLine
                            cetakNAM2 += ds.Tables(0).Rows(i).Item(countNAM).ToString.ToUpper & Environment.NewLine
                            cetakJK2 += ds.Tables(0).Rows(i).Item(countJK).ToString & Environment.NewLine
                            cetakMOD2 += "     " & ds.Tables(0).Rows(i).Item(countMOD).ToString & Environment.NewLine

                        End If

                        countKOD = countKOD + 4
                        countNAM = countNAM + 4
                        countJK = countJK + 4
                        countMOD = countMOD + 4

                    Next

                    cell = New PdfPCell()
                    Debug.WriteLine(cetakKOD2)
                    myPara001 = New Paragraph(cetakKOD2, FontFactory.GetFont("Arial", fontSize65))
                    myPara001.Alignment = Element.ALIGN_LEFT
                    cell.AddElement(myPara001)
                    cell.Border = 0
                    cell.VerticalAlignment = Element.ALIGN_LEFT
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    Debug.WriteLine(cetakNAM2)

                    myPara001 = New Paragraph(cetakNAM2, FontFactory.GetFont("Arial", fontSize65))
                    myPara001.Alignment = Element.ALIGN_LEFT
                    cell.AddElement(myPara001)
                    cell.Border = 0
                    cell.VerticalAlignment = Element.ALIGN_LEFT
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    Debug.WriteLine(cetakJK2)
                    myPara001 = New Paragraph(cetakJK2, FontFactory.GetFont("Arial", fontSize65))
                    myPara001.Alignment = Element.ALIGN_CENTER
                    cell.AddElement(myPara001)
                    cell.Border = 0
                    'cell.VerticalAlignment = Element.ALIGN_MIDDLE
                    cell.HorizontalAlignment = Element.ALIGN_CENTER
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    Debug.WriteLine(cetakMOD2)

                    myPara001 = New Paragraph(cetakMOD2, FontFactory.GetFont("Arial", fontSize65))
                    myPara001.Alignment = Element.ALIGN_LEFT
                    cell.AddElement(myPara001)
                    cell.Border = 0

                    cell.HorizontalAlignment = Element.ALIGN_CENTER
                    table.AddCell(cell)


                    myDocument.Add(table)

                    myDocument.Add(imgSpacing)
                    myDocument.Add(imgSpacing)
                    myDocument.Add(imgSpacing)

                    '''''header sem 3 & 4''''''''''''''''''''''''''''''''''''''''''
                    table = New PdfPTable(8)

                    table.WidthPercentage = 103
                    table.SetWidths({35, 11, 7, 7, 35, 11, 7, 6})

                    cell = New PdfPCell()
                    cetak = "SEMESTER 3 KOHORT " & strTahun
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", fontSize7, Font.BOLD)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = ""
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", fontSize7, Font.BOLD)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = " "
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", fontSize7, Font.BOLD)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = " "
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", fontSize7, Font.BOLD)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = "SEMESTER 4 KOHORT " & strTahun
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", fontSize7, Font.BOLD)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = ""
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", fontSize7, Font.BOLD)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = ""
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", fontSize7, Font.BOLD)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = ""
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", fontSize7, Font.BOLD)))
                    cell.Border = 0
                    table.AddCell(cell)

                    myDocument.Add(table)

                    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    table = New PdfPTable(8)

                    table.WidthPercentage = 103
                    table.SetWidths({11, 35, 7, 7, 11, 35, 7, 6})

                    cell = New PdfPCell()
                    cetak = "KOD"
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", fontSize7, Font.BOLD)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = "MATA PELAJARAN"
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", fontSize7, Font.BOLD)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = "JAM KREDIT"
                    myPara001 = New Paragraph(cetak, FontFactory.GetFont("Arial", fontSize7, Font.BOLD))
                    myPara001.Alignment = Element.ALIGN_CENTER
                    cell.AddElement(myPara001)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = "GRED"
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", fontSize7, Font.BOLD)))
                    cell.HorizontalAlignment = 1
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = "KOD"
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", fontSize7, Font.BOLD)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = "MATA PELAJARAN"
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", fontSize7, Font.BOLD)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = "JAM KREDIT"
                    myPara001 = New Paragraph(cetak, FontFactory.GetFont("Arial", fontSize7, Font.BOLD))
                    myPara001.Alignment = Element.ALIGN_CENTER
                    cell.AddElement(myPara001)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = "GRED"
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", fontSize7, Font.BOLD)))
                    cell.HorizontalAlignment = 1
                    cell.Border = 0
                    table.AddCell(cell)

                    myDocument.Add(table)
                    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    ''matapelajaran sem 3 & 4
                    table = New PdfPTable(8)
                    table.WidthPercentage = 103
                    table.SetWidths({11, 35, 7, 7, 11, 35, 7, 6})
                    table.DefaultCell.Border = 0

                    ''matapelajaran semester 3

                    cell = New PdfPCell()

                    Dim cetakKOD3 As String = ""
                    Dim cetakNAM3 As String = ""
                    Dim cetakJK3 As String = ""
                    Dim cetakMOD3 As String = ""

                    For iloop As Integer = 0 To 11

                        If Not ds.Tables(0).Rows(i).Item(countKOD).ToString = "" Then

                            cetakKOD3 += ds.Tables(0).Rows(i).Item(countKOD).ToString & Environment.NewLine
                            cetakNAM3 += ds.Tables(0).Rows(i).Item(countNAM).ToString.ToUpper & Environment.NewLine
                            cetakJK3 += ds.Tables(0).Rows(i).Item(countJK).ToString & Environment.NewLine
                            cetakMOD3 += "     " & ds.Tables(0).Rows(i).Item(countMOD).ToString & Environment.NewLine

                        End If

                        countKOD = countKOD + 4
                        countNAM = countNAM + 4
                        countJK = countJK + 4
                        countMOD = countMOD + 4

                    Next

                    cell = New PdfPCell()
                    Debug.WriteLine(cetakKOD3)
                    myPara001 = New Paragraph(cetakKOD3, FontFactory.GetFont("Arial", fontSize65))
                    myPara001.Alignment = Element.ALIGN_LEFT
                    cell.AddElement(myPara001)
                    cell.Border = 0
                    cell.VerticalAlignment = Element.ALIGN_LEFT
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    Debug.WriteLine(cetakNAM3)

                    myPara001 = New Paragraph(cetakNAM3, FontFactory.GetFont("Arial", fontSize65))
                    myPara001.Alignment = Element.ALIGN_LEFT
                    cell.AddElement(myPara001)
                    cell.Border = 0
                    cell.VerticalAlignment = Element.ALIGN_LEFT
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    Debug.WriteLine(cetakJK3)
                    myPara001 = New Paragraph(cetakJK3, FontFactory.GetFont("Arial", fontSize65))
                    myPara001.Alignment = Element.ALIGN_CENTER
                    cell.AddElement(myPara001)
                    cell.Border = 0
                    ''cell.VerticalAlignment = Element.ALIGN_MIDDLE
                    cell.HorizontalAlignment = Element.ALIGN_CENTER
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    Debug.WriteLine(cetakMOD3)

                    myPara001 = New Paragraph(cetakMOD3, FontFactory.GetFont("Arial", fontSize65))
                    myPara001.Alignment = Element.ALIGN_LEFT
                    cell.AddElement(myPara001)
                    cell.Border = 0

                    cell.HorizontalAlignment = Element.ALIGN_CENTER
                    table.AddCell(cell)

                    Dim cetakKOD4 As String = ""
                    Dim cetakNAM4 As String = ""
                    Dim cetakJK4 As String = ""
                    Dim cetakMOD4 As String = ""

                    For iloop As Integer = 0 To 11

                        If Not ds.Tables(0).Rows(i).Item(countKOD).ToString = "" Then

                            cetakKOD4 += ds.Tables(0).Rows(i).Item(countKOD).ToString & Environment.NewLine
                            cetakNAM4 += ds.Tables(0).Rows(i).Item(countNAM).ToString.ToUpper & Environment.NewLine
                            cetakJK4 += ds.Tables(0).Rows(i).Item(countJK).ToString & Environment.NewLine
                            cetakMOD4 += "     " & ds.Tables(0).Rows(i).Item(countMOD).ToString & Environment.NewLine

                        End If

                        countKOD = countKOD + 4
                        countNAM = countNAM + 4
                        countJK = countJK + 4
                        countMOD = countMOD + 4

                    Next

                    cell = New PdfPCell()
                    Debug.WriteLine(cetakKOD4)
                    myPara001 = New Paragraph(cetakKOD4, FontFactory.GetFont("Arial", fontSize65))
                    myPara001.Alignment = Element.ALIGN_LEFT
                    cell.AddElement(myPara001)
                    cell.Border = 0
                    cell.VerticalAlignment = Element.ALIGN_LEFT
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    Debug.WriteLine(cetakNAM4)

                    myPara001 = New Paragraph(cetakNAM4, FontFactory.GetFont("Arial", fontSize65))
                    myPara001.Alignment = Element.ALIGN_LEFT
                    cell.AddElement(myPara001)
                    cell.Border = 0
                    cell.VerticalAlignment = Element.ALIGN_LEFT
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    Debug.WriteLine(cetakJK4)
                    myPara001 = New Paragraph(cetakJK4, FontFactory.GetFont("Arial", fontSize65))
                    myPara001.Alignment = Element.ALIGN_CENTER
                    cell.AddElement(myPara001)
                    cell.Border = 0
                    'cell.VerticalAlignment = Element.ALIGN_MIDDLE
                    cell.HorizontalAlignment = Element.ALIGN_CENTER
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    Debug.WriteLine(cetakMOD4)

                    myPara001 = New Paragraph(cetakMOD4, FontFactory.GetFont("Arial", fontSize65))
                    myPara001.Alignment = Element.ALIGN_LEFT
                    cell.AddElement(myPara001)
                    cell.Border = 0
                    'cell.VerticalAlignment = Element.ALIGN_MIDDLE
                    cell.HorizontalAlignment = Element.ALIGN_CENTER
                    table.AddCell(cell)


                    myDocument.Add(table)

                    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

                    myDocument.Add(imgSpacing)
                    myDocument.Add(imgSpacing)

                    ''line image
                    table = New PdfPTable(1)
                    table.WidthPercentage = 103
                    table.SetWidths({100})

                    cell = New PdfPCell()
                    cetak = "___________________________________________________________________________________________________________________________________________________"
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", fontSize65)))
                    cell.Border = 0
                    table.AddCell(cell)

                    myDocument.Add(table)


                    ''--draw spacing
                    Dim imgdrawLine As String = Server.MapPath("~/img/spacing2.png")
                    Dim imgDSpacing As Image = Image.GetInstance(imgdrawLine)
                    imgDSpacing.Alignment = Image.ALIGN_LEFT  'ALIGN_LEFT
                    imgDSpacing.Border = 0

                    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

                    '' Get data from kpmkv_svm sem 4

                    ''PNGK akademik & jumlah jam kredit akadmik

                    table = New PdfPTable(8)

                    table.WidthPercentage = 103
                    table.SetWidths({20, 5, 10, 10, 30, 5, 10, 10})

                    cell = New PdfPCell()
                    cetak = "PNGK AKADEMIK"
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 7)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = ":"
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 7)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = FormatNumber(CDbl(strPNGKA), 2)
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 7)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = ""
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 7)))
                    cell.Border = 0
                    table.AddCell(cell)

                    ''get jumlah jam kredit akademik
                    ''sem1

                    'strSQL = "SELECT SUM(JamKredit) as JK1 FROM kpmkv_matapelajaran"
                    'strSQL += " WHERE Tahun ='" & strtahun1 & "'"
                    'strSQL += " AND Semester= '1'"
                    'If strAgama = "ISLAM" Then
                    '    strSQL += " AND NamaMataPelajaran NOT like ('PENDIDIKAN MORAL%')"
                    'Else
                    '    strSQL += " AND NamaMataPelajaran NOT like ('PENDIDIKAN ISLAM%')"
                    'End If
                    'Dim JK1 As Integer = oCommon.getFieldValue(strSQL)

                    '''''sem2
                    'strSQL = "SELECT SUM(JamKredit) as JK2 FROM kpmkv_matapelajaran"
                    'strSQL += " WHERE Tahun ='" & strtahun2 & "'"
                    'strSQL += " AND Semester= '2'"
                    'If strAgama = "ISLAM" Then
                    '    strSQL += " AND NamaMataPelajaran NOT like ('PENDIDIKAN MORAL%')"
                    'Else
                    '    strSQL += " AND NamaMataPelajaran NOT like ('PENDIDIKAN ISLAM%')"
                    'End If
                    'Dim JK2 As Integer = oCommon.getFieldValue(strSQL)

                    '''''sem3
                    'strSQL = "SELECT SUM(JamKredit) as JK3 FROM kpmkv_matapelajaran"
                    'strSQL += " WHERE Tahun ='" & strtahun3 & "'"
                    'strSQL += " AND Semester= '3'"
                    'If strAgama = "ISLAM" Then
                    '    strSQL += " AND NamaMataPelajaran NOT like ('PENDIDIKAN MORAL%')"
                    'Else
                    '    strSQL += " AND NamaMataPelajaran NOT like ('PENDIDIKAN ISLAM%')"
                    'End If
                    'Dim JK3 As Integer = oCommon.getFieldValue(strSQL)

                    '''''sem4
                    'strSQL = "SELECT SUM(JamKredit) as JK4 FROM kpmkv_matapelajaran"
                    'strSQL += " WHERE Tahun ='" & strtahun4 & "'"
                    'strSQL += " AND Semester= '4'"
                    'If strAgama = "ISLAM" Then
                    '    strSQL += " AND NamaMataPelajaran NOT like ('PENDIDIKAN MORAL%')"
                    'Else
                    '    strSQL += " AND NamaMataPelajaran NOT like ('PENDIDIKAN ISLAM%')"
                    'End If
                    'Dim JK4 As Integer = oCommon.getFieldValue(strSQL)

                    'Dim TotalJK As Integer = JK1 + JK2 + JK3 + JK4

                    cell = New PdfPCell()
                    cetak = "JUMLAH JAM KREDIT AKADEMIK"
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 7)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = ":"
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 7)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = strJumlahAKA
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 7)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = ""
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 7)))
                    cell.Border = 0
                    table.AddCell(cell)

                    myDocument.Add(table)

                    ''PNGK vok & jumlah jam kredit vok

                    table = New PdfPTable(8)

                    table.WidthPercentage = 103
                    table.SetWidths({20, 5, 10, 10, 30, 5, 10, 10})

                    cell = New PdfPCell()
                    cetak = "PNGK VOKASIONAL"
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 7)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = ":"
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 7)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = FormatNumber(CDbl(strPNGKV), 2)
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 7)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = ""
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 7)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = "JUMLAH JAM KREDIT VOKASIONAL"
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 7)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = ":"
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 7)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = strJumlahVOK
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 7)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = ""
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 7)))
                    cell.Border = 0
                    table.AddCell(cell)

                    myDocument.Add(table)

                    ''PNGK keseluruhan

                    table = New PdfPTable(8)

                    table.WidthPercentage = 103
                    table.SetWidths({20, 5, 10, 10, 30, 5, 10, 10})

                    cell = New PdfPCell()
                    cetak = "PNGK KESELURUHAN"
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 7)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = ":"
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 7)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = FormatNumber(CDbl(strPNGKK), 2)
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 7)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = ""
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 7)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = ""
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 7)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = ""
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 7)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = ""
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 7)))
                    cell.Border = 0
                    table.AddCell(cell)

                    cell = New PdfPCell()
                    cetak = ""
                    cell.AddElement(New Paragraph(cetak, FontFactory.GetFont("Arial", 7)))
                    cell.Border = 0
                    table.AddCell(cell)

                    myDocument.Add(table)



                    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    myDocument.Add(imgSpacing)

                    strSQL = "SELECT signature_scale, signature_pos_x, signature_pos_y FROM tbl_signature WHERE signature_type = 'transkrip_12_14'"
                    strRet = oCommon.getFieldValueEx(strSQL)

                    Dim sign_measure As Array
                    sign_measure = strRet.Split("|")

                    Dim signScale As Integer = sign_measure(0)
                    Dim signX As Integer = sign_measure(1)
                    Dim signY As Integer = sign_measure(2)

                    strSQL = " Select FileLocation FROM kpmkv_config_pengarahPeperiksaan WHERE ID='" & ddlSign.SelectedValue & "'"
                    Dim FullFileName As String = oCommon.getFieldValue(strSQL)

                    Dim imageHeader As String = String.Concat(Server.MapPath("~/signature/" + FullFileName))

                    'Dim imageHeader As String = Server.MapPath(fileSavePath)
                    Dim imgHeader As Image = Image.GetInstance(imageHeader)
                    imgHeader.ScalePercent(signScale)
                    imgHeader.SetAbsolutePosition(signX, signY)

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