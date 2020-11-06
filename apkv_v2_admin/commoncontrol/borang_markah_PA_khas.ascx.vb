
Imports System.Data.SqlClient
Imports System.IO
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports System.Globalization

Public Class borang_markah_PA_khas
    Inherits System.Web.UI.UserControl
    Dim oCommon As New Commonfunction
    Dim strSQL As String
    Dim strRet As String
    Dim IntTakwim As Integer = 0

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then

                '------exist takwim
                strSQL = "SELECT * FROM kpmkv_takwim WHERE Tahun='" & Now.Year & "' AND SubMenuText='Borang Markah Khas' AND Aktif='1'"
                If oCommon.isExist(strSQL) = True Then

                    'count data takwim
                    'Get the data from database into datatable
                    Dim cmd As New SqlCommand("SELECT TakwimID FROM kpmkv_takwim WHERE Tahun='" & Now.Year & "' AND SubMenuText='Borang Markah Khas' AND Aktif='1'")
                    Dim dt As DataTable = GetData(cmd)

                    For i As Integer = 0 To dt.Rows.Count - 1
                        IntTakwim = dt.Rows(i)("TakwimID")

                        strSQL = "SELECT TarikhMula,TarikhAkhir FROM kpmkv_takwim WHERE TakwimID='" & IntTakwim & "'"
                        strRet = oCommon.getFieldValueEx(strSQL)

                        Dim ar_user_login As Array
                        ar_user_login = strRet.Split("|")
                        Dim strMula As String = ar_user_login(0)
                        Dim strAkhir As String = ar_user_login(1)

                        Dim strdateNow As Date = Date.Now.Date
                        Dim startDate = DateTime.ParseExact(strMula, "dd-MM-yyyy", CultureInfo.InvariantCulture)
                        Dim endDate = DateTime.ParseExact(strAkhir, "dd-MM-yyyy", CultureInfo.InvariantCulture)

                        Dim ts As New TimeSpan
                        ts = startDate.Subtract(strdateNow)
                        Dim dayDiffMula = ts.Days
                        ts = endDate.Subtract(strdateNow)
                        Dim dayDiffAkhir = ts.Days

                        If strMula IsNot Nothing And dayDiffMula <= 0 Then
                            If strAkhir IsNot Nothing And dayDiffAkhir >= 0 Then

                                lblMsg.Text = ""

                                strSQL = "SELECT UserID FROM kpmkv_users WHERE LoginID='" & Session("LoginID") & "' AND Pwd = '" & Session("Password") & "'"
                                lblID.Text = oCommon.getFieldValue(strSQL)

                                strSQL = "SELECT UserType FROM kpmkv_users WHERE LoginID='" & Session("LoginID") & "' AND Pwd = '" & Session("Password") & "'"
                                lblType.Text = oCommon.getFieldValue(strSQL)


                                If lblType.Text = "ADMIN" Then
                                Else
                                    strSQL = " SELECT Semester,Sesi FROM  kpmkv_pemeriksa "
                                    strSQL += " WHERE UserID='" & lblID.Text & "' AND Tahun='" & Now.Year & "'"
                                    strRet = oCommon.getFieldValueEx(strSQL)

                                    Dim ar_pemeriksa As Array
                                    ar_pemeriksa = strRet.Split("|")
                                    lblPAT.Text = ar_pemeriksa(0)
                                    lblSemester.Text = ar_pemeriksa(0)
                                    lblSesi.Text = ar_pemeriksa(0)
                                End If

                                loadStores()

                                'checkinbox
                                strSQL = "SELECT Sesi FROM kpmkv_takwim WHERE TakwimId='" & IntTakwim & "'ORDER BY Kohort ASC"
                                strRet = oCommon.getFieldValue(strSQL)

                                If strRet = 1 Then
                                    'chkSesi.Items(0).Enabled = True
                                    'chkSesi.Items(0).Selected = True
                                    ' chkSesi.Items(1).Enabled = False
                                Else
                                    ' chkSesi.Items(0).Enabled = False
                                    'chkSesi.Items(1).Enabled = True
                                End If

                                btnPrint.Enabled = True
                                lblMsg.Text = ""

                            End If


                        Else

                            lblMsg.Text = "Borang Markah Khas telah ditutup!"

                        End If
                    Next
                Else
                    lblMsg.Text = "Borang Markah Khas telah ditutup!"
                End If

            End If
        Catch ex As Exception
            lblMsg.Text = ex.Message
        End Try
    End Sub

    Private Shared Function RepoveDuplicate(ByVal ddl As DropDownList) As DropDownList
        For Row As Int16 = 0 To ddl.Items.Count - 2
            For RowAgain As Int16 = ddl.Items.Count - 1 To Row + 1 Step -1
                If ddl.Items(Row).ToString = ddl.Items(RowAgain).ToString Then
                    ddl.Items.RemoveAt(RowAgain)
                End If
            Next
        Next
        Return ddl
    End Function

    Protected Sub loadStores()
        objConn.Open()
        If lblType.Text = "ADMIN" Then
            strSQL = "SELECT * from kpmkv_markah_khas WHERE Tahun='" & Now.Year & "'"
        Else
            strSQL = "SELECT * from kpmkv_markah_khas WHERE PemeriksaID='" & lblID.Text & "' AND Tahun='" & Now.Year & "'"
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
            Dim inTahun As TextBox = gridView.FooterRow.FindControl("inTahun")
            Dim inKod As TextBox = gridView.FooterRow.FindControl("inKod")
            Dim inSesi As TextBox = gridView.FooterRow.FindControl("inSesi")
            Dim inSemester As TextBox = gridView.FooterRow.FindControl("inSemester")
            Dim inMykad As TextBox = gridView.FooterRow.FindControl("inMykad")
            Dim inAngkaGiliran As TextBox = gridView.FooterRow.FindControl("inAngkaGiliran")
            Dim inPAT As TextBox = gridView.FooterRow.FindControl("inPAT")
            Dim inCatatan As TextBox = gridView.FooterRow.FindControl("inCatatan")

            'assign value to integer
            Dim strTahun As String = Now.Year
            Dim strKod As String = inKod.Text
            Dim strSesi As String = inSesi.Text
            Dim strSemester As String = inSemester.Text
            Dim strMykad As String = inMykad.Text
            Dim strAngkaGiliran As String = inAngkaGiliran.Text
            Dim strPAT As String = inPAT.Text
            Dim strCatatan As String = inCatatan.Text

            objConn.Open()
            If lblType.Text = "ADMIN" Then
                strSQL = "INSERT into kpmkv_markah_khas(Tahun,Kod,Sesi,Semester,Mykad,AngkaGiliran,PAT,Catatan,PemeriksaID) values('" & strTahun & "','" & strKod & "','" & strSesi & "','" & strSemester & "','" & strMykad & "','" & strAngkaGiliran & "','" & strPAT & "','" & strCatatan & "','" & lblID.Text & "')"
            Else
                strSQL = "INSERT into kpmkv_markah_khas(Tahun,Kod,Sesi,Semester,Mykad,AngkaGiliran,PAT,Catatan,PemeriksaID) values('" & strTahun & "','" & strKod & "','" & strSesi & "','" & strSemester & "','" & strMykad & "','" & strAngkaGiliran & "','" & strPAT & "','" & strCatatan & "','" & lblID.Text & "')"
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

    Protected Sub btnPrint_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnPrint.Click

        If lblType.Text = "ADMIN" Then
            strSQL = "SELECT * from kpmkv_markah_khas WHERE Tahun='" & Now.Year & "'"
        Else
            strSQL = "SELECT * from kpmkv_markah_khas WHERE PemeriksaID='" & lblID.Text & "' AND Tahun='" & Now.Year & "'"
        End If

        Try
            Dim tableColumn As DataColumnCollection
            Dim tableRows As DataRowCollection

            Dim myDataSet As New DataSet
            Dim myDataAdapter As New SqlDataAdapter(strSQL, strConn)
            myDataAdapter.Fill(myDataSet, "Borang Markah Khas")
            myDataAdapter.SelectCommand.CommandTimeout = 80000
            objConn.Close()

            '--transfer to an object
            tableColumn = myDataSet.Tables(0).Columns
            tableRows = myDataSet.Tables(0).Rows

            CreatePDF(tableColumn, tableRows)

        Catch ex As Exception
            '--display on screen
            lblMsg.Text = "System Error." & ex.Message


        End Try
    End Sub

    Private Sub CreatePDF(ByVal tableColumns As DataColumnCollection, ByVal tableRows As DataRowCollection)
        Dim msFileName As String
        Dim msFilePath As String
        'Step 1: First create an instance of document object
        Dim myDocument As New Document(PageSize.A4)
        ' Document.SetPageSize(iTextSharp.text.PageSize.A4.Rotate())
        Try
            ''--msFileName = Guid.NewGuid.ToString & ".pdf"

            msFileName = "BorangMarkahPAKhas.pdf"
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

            Dim myTable0 As New PdfPTable(2)
            myTable0.WidthPercentage = 100 ' Table size is set to 100% of the page
            myTable0.HorizontalAlignment = 1 '//0=Left, 1=Centre, 2=Right
            'myTable0.HorizontalAlignment = Rectangle.NO_BORDER
            Dim intTbl0Width() As Integer = {50, 50}
            myTable0.SetWidths(intTbl0Width)

            Dim Tbl0Cell1Hdr As New PdfPCell(New Phrase("BORANG MARKAH KHAS ", FontFactory.GetFont("Arial", 9, Font.UNDERLINE)))
            Tbl0Cell1Hdr.Border = Rectangle.NO_BORDER
            myTable0.AddCell(Tbl0Cell1Hdr)

            Dim Tbl0Cell2Hdr As New PdfPCell(New Phrase("", FontFactory.GetFont("Arial", 9, Font.NORMAL)))
            Tbl0Cell2Hdr.Border = Rectangle.NO_BORDER
            myTable0.AddCell(Tbl0Cell2Hdr)

            myDocument.Add(myTable0)

            myDocument.Add(imgSpacing)
            myDocument.Add(imgSpacing)

            Dim myTable As New PdfPTable(8)
            myTable.WidthPercentage = 100 ' Table size is set to 100% of the page
            myTable.HorizontalAlignment = 1 '//0=Left, 1=Centre, 2=Right
            'myTable.HorizontalAlignment = Rectangle.NO_BORDER
            Dim intTblWidth() As Integer = {15, 10, 10, 10, 15, 15, 15, 15}
            myTable.SetWidths(intTblWidth)

            Dim Cell1Hdr As New PdfPCell(New Phrase("TAHUN ", FontFactory.GetFont("Arial", 9, Font.BOLD)))
            Cell1Hdr.Border = Rectangle.NO_BORDER
            myTable.AddCell(Cell1Hdr)

            Dim Cell8Hdr As New PdfPCell(New Phrase("KOD PUSAT ", FontFactory.GetFont("Arial", 9, Font.BOLD)))
            Cell8Hdr.Border = Rectangle.NO_BORDER
            myTable.AddCell(Cell8Hdr)

            Dim Cell2Hdr As New PdfPCell(New Phrase("SESI ", FontFactory.GetFont("Arial", 9, Font.BOLD)))
            Cell2Hdr.Border = Rectangle.NO_BORDER
            myTable.AddCell(Cell2Hdr)

            Dim Cell3Hdr As New PdfPCell(New Phrase("SEMESTER ", FontFactory.GetFont("Arial", 9, Font.BOLD)))
            Cell3Hdr.Border = Rectangle.NO_BORDER
            myTable.AddCell(Cell3Hdr)

            Dim Cell4Hdr As New PdfPCell(New Phrase("MYKAD ", FontFactory.GetFont("Arial", 9, Font.BOLD)))
            Cell4Hdr.Border = Rectangle.NO_BORDER
            myTable.AddCell(Cell4Hdr)

            Dim Cell5Hdr As New PdfPCell(New Phrase("ANGKA GILIRAN ", FontFactory.GetFont("Arial", 9, Font.BOLD)))
            Cell5Hdr.Border = Rectangle.NO_BORDER
            myTable.AddCell(Cell5Hdr)

            Dim Cell6Hdr As New PdfPCell(New Phrase("MARKAH ", FontFactory.GetFont("Arial", 9, Font.BOLD)))
            Cell6Hdr.Border = Rectangle.NO_BORDER
            myTable.AddCell(Cell6Hdr)

            Dim Cell7Hdr As New PdfPCell(New Phrase("CATATAN ", FontFactory.GetFont("Arial", 9, Font.BOLD)))
            Cell7Hdr.Border = Rectangle.NO_BORDER
            myTable.AddCell(Cell7Hdr)

            myDocument.Add(myTable)

            myDocument.Add(imgSpacing)

            If lblType.Text = "ADMIN" Then
                strSQL = "SELECT * from kpmkv_markah_khas WHERE Tahun='" & Now.Year & "'"
            Else
                strSQL = "SELECT * from kpmkv_markah_khas WHERE PemeriksaID='" & lblID.Text & "' AND Tahun='" & Now.Year & "'"

            End If

            'Get the data from database into datatable 
            Dim cmd As New SqlCommand(strSQL)
            Dim dt As DataTable = GetData(cmd)

            'append new line 
            For i As Integer = 0 To dt.Rows.Count - 1
                Dim intCountPelajar As Integer = dt.Rows.Count
                Dim strStudentID As String = ""

                Dim strKey As String = ""
                Dim strTahun As String = ""
                Dim strKod As String = ""
                Dim strSesi As String = ""
                Dim strSemester As String = ""
                Dim strMykad As String = ""
                Dim strAngkaGiliran As String = ""
                Dim strPAT As String = ""
                Dim strCatatan As String = ""

                'set RPN

                strKey = gridView.DataKeys(i).Value.ToString()

                strTahun = Now.Year

                strSQL = "SELECT Kod FROM kpmkv_markah_khas WHERE KhasID = '" & strKey & "'"
                strKod = oCommon.getFieldValue(strSQL)

                strSQL = "SELECT Sesi FROM kpmkv_markah_khas WHERE KhasID = '" & strKey & "'"
                strSesi = oCommon.getFieldValue(strSQL)

                strSQL = "SELECT Semester FROM kpmkv_markah_khas WHERE KhasID = '" & strKey & "'"
                strSemester = oCommon.getFieldValue(strSQL)

                strSQL = "SELECT Mykad FROM kpmkv_markah_khas WHERE KhasID = '" & strKey & "'"
                strMykad = oCommon.getFieldValue(strSQL)

                strSQL = "SELECT AngkaGiliran FROM kpmkv_markah_khas WHERE KhasID = '" & strKey & "'"
                strAngkaGiliran = oCommon.getFieldValue(strSQL)

                strSQL = "SELECT PAT FROM kpmkv_markah_khas WHERE KhasID = '" & strKey & "'"
                strPAT = oCommon.getFieldValue(strSQL)

                strSQL = "SELECT Catatan FROM kpmkv_markah_khas WHERE KhasID = '" & strKey & "'"
                strCatatan = oCommon.getFieldValue(strSQL)

                Dim myTableMP2 As New PdfPTable(8)
                myTableMP2.WidthPercentage = 100 ' Table size is set to 100% of the page
                myTableMP2.HorizontalAlignment = 1 '//0=Left, 1=Centre, 2=Right
                myTableMP2.DefaultCell.BorderWidth = Rectangle.NO_BORDER
                Dim intTblCell8() As Integer = {15, 10, 10, 10, 15, 15, 15, 15}
                myTableMP2.SetWidths(intTblCell8)

                myTableMP2.AddCell(New Phrase(strTahun, FontFactory.GetFont("Arial", 9, Font.NORMAL)))
                myTableMP2.AddCell(New Phrase(strKod, FontFactory.GetFont("Arial", 9, Font.NORMAL)))
                myTableMP2.AddCell(New Phrase(strSesi, FontFactory.GetFont("Arial", 9, Font.NORMAL)))
                myTableMP2.AddCell(New Phrase(strSemester, FontFactory.GetFont("Arial", 9, Font.NORMAL)))
                myTableMP2.AddCell(New Phrase(strMykad, FontFactory.GetFont("Arial", 9, Font.NORMAL)))
                myTableMP2.AddCell(New Phrase(strAngkaGiliran, FontFactory.GetFont("Arial", 9, Font.NORMAL)))
                myTableMP2.AddCell(New Phrase(strPAT, FontFactory.GetFont("Arial", 9, Font.NORMAL)))
                myTableMP2.AddCell(New Phrase(strCatatan, FontFactory.GetFont("Arial", 9, Font.NORMAL)))

                myDocument.Add(myTableMP2)
                myDocument.Add(imgSpacing)

            Next

            lblMsg.Text = "PDF siap dijana."

        Catch ex As DocumentException
            '--display on screen
            lblMsg.Text = "System Error. Contact system admin. " & ex.Message

        Catch ioe As IOException
            '--display on screen
            lblMsg.Text = "System Error. Contact system admin. " & ioe.Message.ToString

        Finally
            'Step 5: Remember to close the documnet
            myDocument.Close()

        End Try
    End Sub



End Class