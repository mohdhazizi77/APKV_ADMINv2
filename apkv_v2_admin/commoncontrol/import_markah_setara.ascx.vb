Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization
Imports System.Data.Common

Public Class import_markah_setara
    Inherits System.Web.UI.UserControl
    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim KodMatapelajaran As String = ""
    Dim NamaMatapelajaran As String = ""
    Dim AngkaGiliran As String = ""
    Dim MYKAD As String = ""
    Dim Markah As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        lblMsg.Text = ""
        Try
            If Not IsPostBack Then

                kpmkv_tahun_list()
                ddlTahunPeperiksaan.Text = Now.Year

                divImport.Visible = False

            End If

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message
        End Try
    End Sub

    Private Sub kpmkv_tahun_list()
        strSQL = "SELECT Tahun FROM kpmkv_tahun  ORDER BY TahunID"
        Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
        Dim objConn As SqlConnection = New SqlConnection(strConn)
        Dim sqlDA As New SqlDataAdapter(strSQL, objConn)

        Try
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            ddlTahunPeperiksaan.DataSource = ds
            ddlTahunPeperiksaan.DataTextField = "Tahun"
            ddlTahunPeperiksaan.DataValueField = "Tahun"
            ddlTahunPeperiksaan.DataBind()

            '--ALL
            ddlTahunPeperiksaan.Items.Add(New ListItem("PILIH", "PILIH"))

        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        Finally
            objConn.Dispose()
        End Try

    End Sub

    Private Sub btnUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpload.Click
        lblMsg.Text = ""

        Try
            If ImportExcel() = True Then
                divMsg.Attributes("class") = "info"
            Else
                divMsg.Attributes("class") = "error"
                lblMsg.Text = "Gagal untuk memuatnaik fail"
            End If
        Catch ex As Exception
            lblMsg.Text = "System Error:" & ex.Message

        End Try

    End Sub
    Private Function ImportExcel() As Boolean
        lblMsg.Text = ""
        Dim path As String = String.Concat(Server.MapPath("~/inbox/"))

        If FlUploadcsv.HasFile Then
            Dim rand As Random = New Random()
            Dim randNum = rand.Next(1000)
            Dim fullFileName As String = path + oCommon.getRandom + "-" + FlUploadcsv.FileName
            FlUploadcsv.PostedFile.SaveAs(fullFileName)

            '--required ms access engine
            Dim excelConnectionString As String = ("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & fullFileName & ";Extended Properties=Excel 12.0;")
            'Dim excelConnectionString As String = ("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & fullFileName & ";Extended Properties=Excel 8.0;HDR=YES;")
            'Response.Write("excelConnectionString:" & excelConnectionString)

            Dim connection As OleDbConnection = New OleDbConnection(excelConnectionString)
            Dim command As OleDbCommand = New OleDbCommand("SELECT * FROM [markah$]", connection)
            Dim da As OleDbDataAdapter = New OleDbDataAdapter(command)
            Dim ds As DataSet = New DataSet

            Try
                connection.Open()
                da.Fill(ds)
                Dim validationMessage As String = ValidateSiteData(ds)
                If validationMessage = "" Then
                    SaveSiteData(ds)

                Else
                    'lblMsgTop.Text = "Muatnaik GAGAL!. Lihat mesej dibawah."
                    divMsg.Attributes("class") = "error"
                    lblMsg.Text = "Kesalahan pada data markah calon:<br />" & validationMessage
                    Return False
                End If

                da.Dispose()
                connection.Close()
                command.Dispose()

            Catch ex As Exception
                lblMsg.Text = "System Error:" & ex.Message
                Return False
            Finally
                If connection.State = ConnectionState.Open Then
                    connection.Close()
                End If
            End Try

        Else
            divMsg.Attributes("class") = "error"
            lblMsg.Text = "Sila pilih fail untuk dimuatnaik!"
            Return False
        End If

        Return True

    End Function
    Public Function FileIsLocked(ByVal strFullFileName As String) As Boolean
        Dim blnReturn As Boolean = False
        Dim fs As System.IO.FileStream

        Try
            fs = System.IO.File.Open(strFullFileName, IO.FileMode.OpenOrCreate, IO.FileAccess.Read, IO.FileShare.None)
            fs.Close()
        Catch ex As System.IO.IOException
            divMsg.Attributes("class") = "error"
            lblMsg.Text = "Error Message FileIsLocked:" & ex.Message
            blnReturn = True
        End Try

        Return blnReturn
    End Function
    Protected Function ValidateSiteData(ByVal SiteData As DataSet) As String
        Try
            'Loop through DataSet and validate data
            'If data is bad, bail out, otherwise continue on with the bulk copy
            Dim strMsg As String = ""
            Dim sb As StringBuilder = New StringBuilder()
            For i As Integer = 0 To SiteData.Tables(0).Rows.Count - 1

                refreshVar()
                strMsg = ""

                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("Ipx_code")) Then
                    KodMatapelajaran = SiteData.Tables(0).Rows(i).Item("Ipx_code")
                Else
                    strMsg += "Sila isi Kod Matapelajaran|"
                End If

                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("Ipx_name")) Then
                    NamaMatapelajaran = SiteData.Tables(0).Rows(i).Item("Ipx_name")
                Else
                    strMsg += "Sila isi Nama Matapelajaran|"
                End If

                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("candidate_qno")) Then
                    AngkaGiliran = SiteData.Tables(0).Rows(i).Item("candidate_qno")
                Else
                    strMsg += "Sila isi AngkaGiliran Calon|"
                End If

                If Not IsDBNull(SiteData.Tables(0).Rows(i).Item("candidate_ic")) Then
                    MYKAD = SiteData.Tables(0).Rows(i).Item("candidate_ic")
                Else
                    strMsg += "Sila isi MYKAD Calon|"
                End If

                If IsDBNull(SiteData.Tables(0).Rows(i).Item("final_mark")) Then
                    Markah = SiteData.Tables(0).Rows(i).Item("final_mark")
                Else
                    Markah = 0
                End If

                If IsNumeric(SiteData.Tables(0).Rows(i).Item("final_mark")) Then
                    Markah = SiteData.Tables(0).Rows(i).Item("final_mark")
                Else
                    Markah = 0
                End If

                sb.Append(strMsg)
                'disp bil

            Next
            Return sb.ToString()
        Catch ex As Exception
            Return ex.Message
        End Try

    End Function
    Private Function SaveSiteData(ByVal SiteData As DataSet) As String
        lblMsg.Text = ""

        Try
            Dim i As Integer = 0
            Dim sb As StringBuilder = New StringBuilder()
            For i = 0 To SiteData.Tables(0).Rows.Count - 1

                KodMatapelajaran = SiteData.Tables(0).Rows(i).Item("Ipx_code")
                NamaMatapelajaran = SiteData.Tables(0).Rows(i).Item("Ipx_name")
                AngkaGiliran = SiteData.Tables(0).Rows(i).Item("candidate_qno")
                MYKAD = SiteData.Tables(0).Rows(i).Item("candidate_ic")
                Markah = SiteData.Tables(0).Rows(i).Item("final_mark")

                strSQL = "SELECT PelajarID FROM kpmkv_pelajar WHERE AngkaGiliran = '" & AngkaGiliran & "' AND MYKAD = '" & MYKAD & "' AND Semester = '4'"
                Dim PelajarID As String = oCommon.getFieldValue(strSQL)

                If Not String.IsNullOrEmpty(PelajarID) Then

                    If radioMP.Text = "BM1" Then

                        strSQL = "SELECT id FROM kpmkv_pelajar_markah_import_bm WHERE PelajarID = '" & PelajarID & "'"
                        Dim checkPelajarID As String = oCommon.getFieldValue(strSQL)

                        If checkPelajarID = "" Then

                            strSQL = ""

                        End If

                        strRet = oCommon.ExecuteSQL(strSQL)

                    ElseIf radioMP.Text = "BM2" Then


                    Else


                    End If

                    If strRet = "0" Then

                        divMsg.Attributes("class") = "info"
                        lblMsg.Text = "Markah berjaya di Import"
                    Else
                        divMsg.Attributes("class") = "error"
                        lblMsg.Text = "Markah tidak berjaya di Import"
                        Return False
                        Exit For
                    End If
                Else
                End If
            Next

            'Response.Write(strSQL)
        Catch ex As Exception
            divMsg.Attributes("class") = "error"
            'lblMsg.Text = "Calon tidak berjaya didaftarkan"
            'Return False
        End Try
        Return True
    End Function
    Private Sub refreshVar()

        KodMatapelajaran = ""
        NamaMatapelajaran = ""
        AngkaGiliran = ""
        MYKAD = ""
        Markah = ""

    End Sub

    Private Sub radioMP_SelectedIndexChanged(sender As Object, e As EventArgs) Handles radioMP.SelectedIndexChanged
        If Not radioMP.Text = "" Then

            divImport.Visible = True

        Else

            divImport.Visible = False

        End If
    End Sub
End Class