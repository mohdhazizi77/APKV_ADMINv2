Imports System.Data.SqlClient
Public Class pentaksirbm_calon
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
                strRet = BindData(datRespondent)

            End If

        Catch ex As Exception
            lblMsg.Text = ex.Message
        End Try
    End Sub

    Private Sub datRespondent_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles datRespondent.PageIndexChanging
        datRespondent.PageIndex = e.NewPageIndex
        strRet = BindData(datRespondent)
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
        Dim strOrder As String = " ORDER BY kpmkv_pelajar.Nama ASC"

        '--not deleted
        tmpSQL = "SELECT kpmkv_pelajar.PelajarID, kpmkv_pelajar.Nama, kpmkv_pelajar.AngkaGiliran, "
        tmpSQL += " kpmkv_pelajar.MYKAD"
        tmpSQL += " FROM kpmkv_pelajar"
        strWhere = " WHERE kpmkv_pelajar.KolejRecordID='" & KolejRecordID & "' AND kpmkv_pelajar.Tahun ='" & Kohort & "' AND kpmkv_pelajar.StatusID='2' AND kpmkv_pelajar.JenisCalonID='2' AND kpmkv_pelajar.Semester ='" & Semester & "'"

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

    'Protected Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
    '    lblMsg.Text = ""

    '    Try
    '        If ValidateForm() = False Then
    '            lblMsgResult.Text = "Sila masukkan NOMBOR SAHAJA"
    '            lblMsg.Text = "Sila masukkan NOMBOR 0-100 SAHAJA"
    '            Exit Sub
    '        End If

    '        For i As Integer = 0 To datRespondent.Rows.Count - 1
    '            Dim row As GridViewRow = datRespondent.Rows(i)
    '            Dim strBahasaMelayu As TextBox = datRespondent.Rows(i).FindControl("B_BahasaMelayu")
    '            Dim strBahasaMelayu1 As TextBox = datRespondent.Rows(i).FindControl("B_BahasaMelayu1")
    '            Dim strBahasaMelayu2 As TextBox = datRespondent.Rows(i).FindControl("B_BahasaMelayu2")
    '            Dim strBahasaMelayu3 As TextBox = datRespondent.Rows(i).FindControl("B_BahasaMelayu3")
    '            Dim strBahasaInggeris As TextBox = datRespondent.Rows(i).FindControl("B_BahasaInggeris")
    '            Dim strScience As TextBox = datRespondent.Rows(i).FindControl("B_Science1")
    '            Dim strSejarah As TextBox = datRespondent.Rows(i).FindControl("B_Sejarah")
    '            Dim strPendidikanIslam As TextBox = datRespondent.Rows(i).FindControl("B_PendidikanIslam1")
    '            Dim strPendidikanMoral As TextBox = datRespondent.Rows(i).FindControl("B_PendidikanMoral")
    '            Dim strMatematik As TextBox = datRespondent.Rows(i).FindControl("B_Mathematics")


    '            'assign value to integer
    '            Dim BM As Integer = strBahasaMelayu.Text
    '            Dim BM1 As Integer = strBahasaMelayu1.Text
    '            Dim BM2 As Integer = strBahasaMelayu2.Text
    '            Dim BM3 As Integer = strBahasaMelayu3.Text
    '            Dim BI As Integer = strBahasaInggeris.Text
    '            Dim SC As Integer = strScience.Text
    '            Dim SEJ As Integer = strSejarah.Text
    '            Dim PI As Integer = strPendidikanIslam.Text
    '            Dim PM As Integer = strPendidikanMoral.Text
    '            Dim Matematik As Integer = strMatematik.Text


    '            strSQL = "UPDATE kpmkv_pelajar_markah SET B_BahasaMelayu='" & BM & "', B_BahasaMelayu1='" & BM1 & "', B_BahasaMelayu2='" & BM2 & "', "
    '            strSQL += " B_BahasaMelayu3='" & BM3 & "', B_BahasaInggeris='" & BI & "', B_Science1='" & SC & "', B_Sejarah='" & SEJ & "',"
    '            strSQL += " B_PendidikanIslam1='" & PI & "', B_PendidikanMoral='" & PM & "',"
    '            strSQL += " B_Mathematics='" & Matematik & "' WHERE PelajarID='" & datRespondent.DataKeys(i).Value.ToString & "'"

    '            strRet = oCommon.ExecuteSQL(strSQL)
    '            If strRet = "0" Then
    '                divMsgResult.Attributes("class") = "info"
    '                lblMsgResult.Text = "Markah Pentaksiran Berterusan Akademik berjaya dikemaskini"
    '            Else
    '                divMsgResult.Attributes("class") = "error"
    '                lblMsgResult.Text = "Markah Pentaksiran Berterusan Akademik tidak berjaya dikemaskini"
    '            End If
    '        Next

    '    Catch ex As Exception
    '        lblMsg.Text = "Error:" & ex.Message
    '    End Try

    '    strRet = BindData(datRespondent)
    'End Sub

    'Private Function ValidateForm() As Boolean
    '    For i As Integer = 0 To datRespondent.Rows.Count - 1
    '        Dim row As GridViewRow = datRespondent.Rows(i)
    '        Dim strBahasaMelayu As TextBox = CType(row.FindControl("B_BahasaMelayu"), TextBox)
    '        Dim strBahasaMelayu1 As TextBox = CType(row.FindControl("B_BahasaMelayu1"), TextBox)
    '        Dim strBahasaMelayu2 As TextBox = CType(row.FindControl("B_BahasaMelayu2"), TextBox)
    '        Dim strBahasaMelayu3 As TextBox = CType(row.FindControl("B_BahasaMelayu3"), TextBox)
    '        Dim strBahasaInggeris As TextBox = CType(row.FindControl("B_BahasaInggeris"), TextBox)
    '        Dim strScience As TextBox = CType(row.FindControl("B_Science1"), TextBox)
    '        Dim strSejarah As TextBox = CType(row.FindControl("B_Sejarah"), TextBox)
    '        Dim strPendidikanIslam As TextBox = CType(row.FindControl("B_PendidikanIslam1"), TextBox)
    '        Dim strPendidikanMoral As TextBox = CType(row.FindControl("B_PendidikanMoral"), TextBox)
    '        Dim strMatematik As TextBox = CType(row.FindControl("B_Mathematics"), TextBox)

    '        '--validate NUMBER and less than 100
    '        '--strBahasaMelayu
    '        If Not strBahasaMelayu.Text.Length = 0 Then
    '            If oCommon.IsCurrency(strBahasaMelayu.Text) = False Then
    '                Return False
    '            End If
    '            If CInt(strBahasaMelayu.Text) > 100 Then
    '                Return False
    '            End If
    '            If CInt(strBahasaMelayu.Text) = -1 Then
    '                Return False
    '            End If
    '        Else
    '            strBahasaMelayu.Text = "0"
    '        End If

    '        '--strBahasaMelayu1
    '        If Not strBahasaMelayu1.Text.Length = 0 Then
    '            If oCommon.IsCurrency(strBahasaMelayu1.Text) = False Then
    '                Return False
    '            End If
    '            If CInt(strBahasaMelayu1.Text) > 100 Then
    '                Return False
    '            End If
    '            If CInt(strBahasaMelayu1.Text) = -1 Then
    '                Return False
    '            End If
    '        Else
    '            strBahasaMelayu1.Text = "0"
    '        End If

    '        '--strBahasaMelayu2
    '        If Not strBahasaMelayu2.Text.Length = 0 Then
    '            If oCommon.IsCurrency(strBahasaMelayu2.Text) = False Then
    '                Return False
    '            End If
    '            If CInt(strBahasaMelayu2.Text) > 100 Then
    '                Return False
    '            End If
    '            If CInt(strBahasaMelayu2.Text) = -1 Then
    '                Return False
    '            End If
    '        Else
    '            strBahasaMelayu2.Text = "0"
    '        End If

    '        '--strBahasaMelayu3
    '        If Not strBahasaMelayu3.Text.Length = 0 Then
    '            If oCommon.IsCurrency(strBahasaMelayu3.Text) = False Then
    '                Return False
    '            End If
    '            If CInt(strBahasaMelayu3.Text) > 100 Then
    '                Return False
    '            End If
    '            If CInt(strBahasaMelayu3.Text) = -1 Then
    '                Return False
    '            End If
    '        Else
    '            strBahasaMelayu3.Text = "0"
    '        End If
    '        '--strBahasaInggeris
    '        If Not strBahasaInggeris.Text.Length = 0 Then
    '            If oCommon.IsCurrency(strBahasaInggeris.Text) = False Then
    '                Return False
    '            End If
    '            If CInt(strBahasaInggeris.Text) > 100 Then
    '                Return False
    '            End If
    '            If CInt(strBahasaInggeris.Text) = -1 Then
    '                Return False
    '            End If
    '        Else
    '            strBahasaInggeris.Text = "0"
    '        End If

    '        '--strScience
    '        If Not strScience.Text.Length = 0 Then
    '            If oCommon.IsCurrency(strScience.Text) = False Then
    '                Return False
    '            End If
    '            If CInt(strScience.Text) > 100 Then
    '                Return False
    '            End If
    '            If CInt(strScience.Text) = -1 Then
    '                Return False
    '            End If
    '        Else
    '            strScience.Text = "0"
    '        End If

    '        '--strSejarah
    '        If Not strSejarah.Text.Length = 0 Then
    '            If oCommon.IsCurrency(strSejarah.Text) = False Then
    '                Return False
    '            End If
    '            If CInt(strSejarah.Text) > 100 Then
    '                Return False
    '            End If
    '            If CInt(strSejarah.Text) = -1 Then
    '                Return False
    '            End If
    '        Else
    '            strSejarah.Text = "0"
    '        End If

    '        '--strPendidikanIslam
    '        If Not strPendidikanIslam.Text.Length = 0 Then
    '            If oCommon.IsCurrency(strPendidikanIslam.Text) = False Then
    '                Return False
    '            End If
    '            If CInt(strPendidikanIslam.Text) > 100 Then
    '                Return False
    '            End If
    '            If CInt(strPendidikanIslam.Text) = -1 Then
    '                Return False
    '            End If
    '        Else
    '            strPendidikanIslam.Text = "0"
    '        End If

    '        '--strPendidikanMoral
    '        If Not strPendidikanMoral.Text.Length = 0 Then
    '            If oCommon.IsCurrency(strPendidikanMoral.Text) = False Then
    '                Return False
    '            End If
    '            If CInt(strPendidikanMoral.Text) > 100 Then
    '                Return False
    '            End If
    '            If CInt(strPendidikanMoral.Text) = -1 Then
    '                Return False
    '            End If
    '        Else
    '            strPendidikanMoral.Text = "0"
    '        End If

    '        'strMatematik
    '        If Not strMatematik.Text.Length = 0 Then
    '            If oCommon.IsCurrency(strMatematik.Text) = False Then
    '                Return False
    '            End If
    '            If CInt(strMatematik.Text) > 100 Then
    '                Return False
    '            End If
    '            If CInt(strMatematik.Text) = -1 Then
    '                Return False
    '            End If
    '        Else
    '            strMatematik.Text = "0"
    '        End If
    '    Next

    '    Return True
    'End Function

End Class

