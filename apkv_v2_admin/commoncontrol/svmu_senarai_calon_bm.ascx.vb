
Imports System.Data.SqlClient
Imports iTextSharp.text
Imports iTextSharp.text.pdf

Public Class svmu_senarai_calon_bm
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Dim strNegeri As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then


                strRet = BindData(datRespondent)

            End If

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message
        End Try
    End Sub
    'Private Sub kpmkv_tahun_list()
    '    strSQL = "SELECT DISTINCT TahunPeperiksaan FROM kpmkv_svmu_calon ORDER BY TahunPeperiksaan ASC"
    '    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    '    Dim objConn As SqlConnection = New SqlConnection(strConn)
    '    Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

    '    Try
    '        Dim ds As DataSet = New DataSet
    '        sqlDA.Fill(ds, "AnyTable")

    '        ddlTahunPeperiksaanSVMU.DataSource = ds
    '        ddlTahunPeperiksaanSVMU.DataTextField = "TahunPeperiksaan"
    '        ddlTahunPeperiksaanSVMU.DataValueField = "TahunPeperiksaan"
    '        ddlTahunPeperiksaanSVMU.DataBind()

    '        ddlTahunPeperiksaanSVMU.Items.Add(New ListItem("-Pilih-", "0"))
    '        ddlTahunPeperiksaanSVMU.Text = "0"

    '    Catch ex As Exception

    '        lblMsg.Text = "System Error:" & ex.Message

    '    Finally
    '        objConn.Dispose()
    '    End Try

    'End Sub

    'Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
    '    lblMsg.Text = ""
    '    strRet = BindData(datRespondent)

    'End Sub
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
                lblMsg.Text = "Jumlah Rekod#:" & myDataSet.Tables(0).Rows.Count
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

        strSQL = "SELECT setting_value_int FROM kpmkv_svmu_setting WHERE setting_parameter = 'TAHUN_PEPERIKSAAN'"
        Dim TahunPeperiksaan As String = oCommon.getFieldValue(strSQL)

        strSQL = "SELECT UserID FROM kpmkv_users WHERE LoginID='" & Session("LoginID") & "'"
        Dim UserID As String = oCommon.getFieldValue(strSQL)

        strSQL = "SELECT Negeri FROM kpmkv_users WHERE UserID = '" & UserID & "'"
        Dim Negeri As String = oCommon.getFieldValue(strSQL)

        If Not Negeri = "" Then

            strSQL = " SELECT
                    kpmkv_svmu.svmu_id, kpmkv_svmu.MYKAD, kpmkv_svmu.AngkaGiliran,
                    kpmkv_svmu_calon.svmu_calon_id, kpmkv_svmu_calon.Nama, kpmkv_svmu_calon.Status, kpmkv_svmu_calon.MataPelajaran,
                    A.Kod AS 'KodPusatPilihan',
					B.Kod AS 'KodPusatJPN',
                    kpmkv_svmu_payment_status.PaymentStatus
                    FROM kpmkv_svmu
                    LEFT JOIN kpmkv_svmu_calon ON kpmkv_svmu_calon.svmu_id = kpmkv_svmu.svmu_id
                    LEFT JOIN kpmkv_kolej A ON A.RecordID = kpmkv_svmu_calon.PusatPeperiksaanID
					LEFT JOIN kpmkv_kolej B ON B.RecordID = kpmkv_svmu_calon.PusatPeperiksaanJPN
					LEFT JOIN kpmkv_svmu_payment_status ON kpmkv_svmu_payment_status.RefNo = kpmkv_svmu_calon.RefNo
                    WHERE
                    A.Negeri = '" & Negeri & "'
					AND B.Negeri = '" & Negeri & "'
                    AND kpmkv_svmu_calon.MataPelajaran = 'BM'
                    AND kpmkv_svmu_payment_status.PaymentStatus = 'success'
                    AND kpmkv_svmu_calon.TahunPeperiksaan = '" & TahunPeperiksaan & "'
                    AND kpmkv_svmu_calon.Status = 'DISAHKAN'"

            strSQL += " ORDER BY A.Kod, kpmkv_svmu_calon.Nama"

        Else

            strSQL = " SELECT
                    kpmkv_svmu.svmu_id, kpmkv_svmu.MYKAD, kpmkv_svmu.AngkaGiliran,
                    kpmkv_svmu_calon.svmu_calon_id, kpmkv_svmu_calon.Nama, kpmkv_svmu_calon.Status, kpmkv_svmu_calon.MataPelajaran,
                    A.Kod AS 'KodPusatPilihan',
					B.Kod AS 'KodPusatJPN',
                    kpmkv_svmu_payment_status.PaymentStatus
                    FROM kpmkv_svmu
                    LEFT JOIN kpmkv_svmu_calon ON kpmkv_svmu_calon.svmu_id = kpmkv_svmu.svmu_id
                    LEFT JOIN kpmkv_kolej A ON A.RecordID = kpmkv_svmu_calon.PusatPeperiksaanID
					LEFT JOIN kpmkv_kolej B ON B.RecordID = kpmkv_svmu_calon.PusatPeperiksaanJPN
					LEFT JOIN kpmkv_svmu_payment_status ON kpmkv_svmu_payment_status.RefNo = kpmkv_svmu_calon.RefNo
                    WHERE
                    kpmkv_svmu_calon.MataPelajaran = 'BM'
                    AND kpmkv_svmu_payment_status.PaymentStatus = 'success'
                    AND kpmkv_svmu_calon.TahunPeperiksaan = '" & TahunPeperiksaan & "'
                    AND kpmkv_svmu_calon.Status = 'DISAHKAN'"

            strSQL += " ORDER BY A.Kod, kpmkv_svmu_calon.Nama"

        End If

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

    Private Sub btnCetak_Click(sender As Object, e As EventArgs) Handles btnCetak.Click

        Dim myDocument As New Document(PageSize.A4.Rotate)

        Try
            HttpContext.Current.Response.ContentType = "application/pdf"
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=SenaraiCalonSVMU-BM.pdf")
            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache)

            PdfWriter.GetInstance(myDocument, HttpContext.Current.Response.OutputStream)

            myDocument.Open()

            ''--draw spacing
            Dim imgdrawSpacing As String = Server.MapPath("~/img/spacing.png")
            Dim imgSpacing As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(imgdrawSpacing)
            imgSpacing.Alignment = iTextSharp.text.Image.LEFT_ALIGN  'left
            imgSpacing.Border = 0

            strSQL = "SELECT setting_value_int FROM kpmkv_svmu_setting WHERE setting_parameter = 'TAHUN_PEPERIKSAAN'"
            Dim TahunPeperiksaan As String = oCommon.getFieldValue(strSQL)

            strSQL = "SELECT UserID FROM kpmkv_users WHERE LoginID='" & Session("LoginID") & "'"
            Dim UserID As String = oCommon.getFieldValue(strSQL)

            strSQL = "SELECT Negeri FROM kpmkv_users WHERE UserID = '" & UserID & "'"
            Dim Negeri As String = oCommon.getFieldValue(strSQL)

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
            cetak = "SIJIL VOKASIONAL MALAYSIA (ULANGAN)"
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
            cetak = "BAHASA MELAYU, TAHUN " & Now.Year
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
            cetak = "JPN NEGERI " & Negeri
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
            table.SetWidths({5, 25, 10, 10, 20, 15, 15})
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
            cetak = "NO. KP"
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
            cetak = "KOD MATAPELAJARAN"
            myPara001 = New Paragraph(cetak, FontFactory.GetFont("Arial", 9))
            myPara001.Alignment = Element.ALIGN_CENTER
            cell.AddElement(myPara001)
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
            table.AddCell(cell)
            myDocument.Add(table)

            cell = New PdfPCell()
            cetak = "KOD PUSAT PILIHAN"
            myPara001 = New Paragraph(cetak, FontFactory.GetFont("Arial", 9))
            myPara001.Alignment = Element.ALIGN_CENTER
            cell.AddElement(myPara001)
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
            table.AddCell(cell)
            myDocument.Add(table)

            cell = New PdfPCell()
            cetak = "KOD PUSAT JPN"
            myPara001 = New Paragraph(cetak, FontFactory.GetFont("Arial", 9))
            myPara001.Alignment = Element.ALIGN_CENTER
            cell.AddElement(myPara001)
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
            table.AddCell(cell)
            myDocument.Add(table)

            '***************get student data*******************'

            If Not Negeri = "" Then

                strSQL = " SELECT
                    kpmkv_svmu.svmu_id, kpmkv_svmu.MYKAD, kpmkv_svmu.AngkaGiliran,
                    kpmkv_svmu_calon.svmu_calon_id, kpmkv_svmu_calon.Nama, kpmkv_svmu_calon.Status, kpmkv_svmu_calon.MataPelajaran,
                    A.Kod AS 'KodPusatPilihan',
					B.Kod AS 'KodPusatJPN',
                    kpmkv_svmu_payment_status.PaymentStatus
                    FROM kpmkv_svmu
                    LEFT JOIN kpmkv_svmu_calon ON kpmkv_svmu_calon.svmu_id = kpmkv_svmu.svmu_id
                    LEFT JOIN kpmkv_kolej A ON A.RecordID = kpmkv_svmu_calon.PusatPeperiksaanID
					LEFT JOIN kpmkv_kolej B ON B.RecordID = kpmkv_svmu_calon.PusatPeperiksaanJPN
					LEFT JOIN kpmkv_svmu_payment_status ON kpmkv_svmu_payment_status.RefNo = kpmkv_svmu_calon.RefNo
                    WHERE
                    A.Negeri = '" & Negeri & "'
					AND B.Negeri = '" & Negeri & "'
                    AND kpmkv_svmu_calon.MataPelajaran = 'BM'
                    AND kpmkv_svmu_payment_status.PaymentStatus = 'success'
                    AND kpmkv_svmu_calon.TahunPeperiksaan = '" & TahunPeperiksaan & "'
                    AND kpmkv_svmu_calon.Status = 'DISAHKAN'"

                strSQL += " ORDER BY A.Kod, kpmkv_svmu_calon.Nama"

            Else

                strSQL = " SELECT
                    kpmkv_svmu.svmu_id, kpmkv_svmu.MYKAD, kpmkv_svmu.AngkaGiliran,
                    kpmkv_svmu_calon.svmu_calon_id, kpmkv_svmu_calon.Nama, kpmkv_svmu_calon.Status, kpmkv_svmu_calon.MataPelajaran,
                    A.Kod AS 'KodPusatPilihan',
					B.Kod AS 'KodPusatJPN',
                    kpmkv_svmu_payment_status.PaymentStatus
                    FROM kpmkv_svmu
                    LEFT JOIN kpmkv_svmu_calon ON kpmkv_svmu_calon.svmu_id = kpmkv_svmu.svmu_id
                    LEFT JOIN kpmkv_kolej A ON A.RecordID = kpmkv_svmu_calon.PusatPeperiksaanID
					LEFT JOIN kpmkv_kolej B ON B.RecordID = kpmkv_svmu_calon.PusatPeperiksaanJPN
					LEFT JOIN kpmkv_svmu_payment_status ON kpmkv_svmu_payment_status.RefNo = kpmkv_svmu_calon.RefNo
                    WHERE
                    kpmkv_svmu_calon.MataPelajaran = 'BM'
                    AND kpmkv_svmu_payment_status.PaymentStatus = 'success'
                    AND kpmkv_svmu_calon.TahunPeperiksaan = '" & TahunPeperiksaan & "'
                    AND kpmkv_svmu_calon.Status = 'DISAHKAN'"

                strSQL += " ORDER BY A.Kod, kpmkv_svmu_calon.Nama"

            End If

            strRet = oCommon.ExecuteSQL(strSQL)

            Dim sqlDA As New SqlDataAdapter(strSQL, objConn)
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            Dim checkPusatPeperiksaan As String = ""

            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1

                Dim svmu_id As String = ds.Tables(0).Rows(i).Item(0).ToString

                Dim MYKAD As String = ds.Tables(0).Rows(i).Item(1).ToString
                Dim AngkaGiliran As String = ds.Tables(0).Rows(i).Item(2).ToString
                Dim svmu_calon_id As String = ds.Tables(0).Rows(i).Item(3).ToString
                Dim Nama As String = ds.Tables(0).Rows(i).Item(4).ToString
                Dim Status As String = ds.Tables(0).Rows(i).Item(5).ToString
                Dim PusatPeperiksaanID As String = ds.Tables(0).Rows(i).Item(6).ToString
                Dim PusatPeperiksaanJPN As String = ds.Tables(0).Rows(i).Item(7).ToString
                Dim Kod As String = ds.Tables(0).Rows(i).Item(8).ToString
                Dim PaymentStatus As String = ds.Tables(0).Rows(i).Item(9).ToString

                If i = 0 Then

                    checkPusatPeperiksaan = PusatPeperiksaanID

                End If

                If Not PusatPeperiksaanID = checkPusatPeperiksaan Then

                    checkPusatPeperiksaan = PusatPeperiksaanID
                    myDocument.NewPage()

                    table = New PdfPTable(1)
                    myPara001 = New Paragraph()
                    table.WidthPercentage = 103
                    table.SetWidths({100})
                    table.DefaultCell.Border = 0

                    cell = New PdfPCell()
                    cetak = "JPN NEGERI " & Negeri
                    myPara001 = New Paragraph(cetak, FontFactory.GetFont("Arial", 9))
                    myPara001.Alignment = Element.ALIGN_LEFT
                    cell.AddElement(myPara001)
                    cell.VerticalAlignment = PdfPCell.ALIGN_LEFT
                    cell.Border = 0
                    table.AddCell(cell)
                    myDocument.Add(table)

                    myDocument.Add(imgSpacing)
                    myDocument.Add(imgSpacing)

                    table = New PdfPTable(7)
                    myPara001 = New Paragraph()
                    table.WidthPercentage = 103
                    table.SetWidths({5, 25, 10, 10, 20, 15, 15})
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
                    cetak = "NO. KP"
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
                    cetak = "KOD MATAPELAJARAN"
                    myPara001 = New Paragraph(cetak, FontFactory.GetFont("Arial", 9))
                    myPara001.Alignment = Element.ALIGN_CENTER
                    cell.AddElement(myPara001)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    table.AddCell(cell)
                    myDocument.Add(table)

                    cell = New PdfPCell()
                    cetak = "KOD PUSAT PILIHAN"
                    myPara001 = New Paragraph(cetak, FontFactory.GetFont("Arial", 9))
                    myPara001.Alignment = Element.ALIGN_CENTER
                    cell.AddElement(myPara001)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    table.AddCell(cell)
                    myDocument.Add(table)

                    cell = New PdfPCell()
                    cetak = "KOD PUSAT JPN"
                    myPara001 = New Paragraph(cetak, FontFactory.GetFont("Arial", 9))
                    myPara001.Alignment = Element.ALIGN_CENTER
                    cell.AddElement(myPara001)
                    cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                    table.AddCell(cell)
                    myDocument.Add(table)

                End If

                table = New PdfPTable(7)
                myPara001 = New Paragraph()
                table.WidthPercentage = 103
                table.SetWidths({5, 25, 10, 10, 20, 15, 15})
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
                cetak = Nama
                cetak += Environment.NewLine & " "
                myPara001 = New Paragraph(cetak, FontFactory.GetFont("Arial", 9))
                myPara001.Alignment = Element.ALIGN_LEFT
                cell.AddElement(myPara001)
                cell.VerticalAlignment = PdfPCell.ALIGN_LEFT
                table.AddCell(cell)
                myDocument.Add(table)

                cell = New PdfPCell()
                cetak = MYKAD
                cetak += Environment.NewLine & " "
                myPara001 = New Paragraph(cetak, FontFactory.GetFont("Arial", 9))
                myPara001.Alignment = Element.ALIGN_CENTER
                cell.AddElement(myPara001)
                cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                table.AddCell(cell)
                myDocument.Add(table)

                cell = New PdfPCell()
                cetak = AngkaGiliran
                cetak += Environment.NewLine & " "
                myPara001 = New Paragraph(cetak, FontFactory.GetFont("Arial", 9))
                myPara001.Alignment = Element.ALIGN_CENTER
                cell.AddElement(myPara001)
                cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                table.AddCell(cell)
                myDocument.Add(table)

                cell = New PdfPCell()
                cetak = "BM 1104"
                cetak += Environment.NewLine & " "
                myPara001 = New Paragraph(cetak, FontFactory.GetFont("Arial", 9))
                myPara001.Alignment = Element.ALIGN_CENTER
                cell.AddElement(myPara001)
                cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                table.AddCell(cell)
                myDocument.Add(table)

                strSQL = "SELECT Kod FROM kpmkv_kolej WHERE RecordID = '" & PusatPeperiksaanID & "'"
                Dim KodPusatPilihan As String = oCommon.getFieldValue(strSQL)

                cell = New PdfPCell()
                cetak = KodPusatPilihan
                cetak += Environment.NewLine & " "
                myPara001 = New Paragraph(cetak, FontFactory.GetFont("Arial", 9))
                myPara001.Alignment = Element.ALIGN_CENTER
                cell.AddElement(myPara001)
                cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
                table.AddCell(cell)
                myDocument.Add(table)

                 strSQL = "SELECT Kod FROM kpmkv_kolej WHERE RecordID = '" & PusatPeperiksaanJPN & "'"
                Dim KodPusatJPN As String = oCommon.getFieldValue(strSQL)

                cell = New PdfPCell()
                cetak = KodPusatJPN
                cetak += Environment.NewLine & " "
                myPara001 = New Paragraph(cetak, FontFactory.GetFont("Arial", 9))
                myPara001.Alignment = Element.ALIGN_CENTER
                cell.AddElement(myPara001)
                cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE
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