Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.IO
Imports System.Globalization
Imports System.Drawing
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports System.Security.Cryptography

Public Class svm_pengesahan1

    Inherits System.Web.UI.UserControl
    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim PelajarID As String = Request.QueryString("id")

        If Not PelajarID = Nothing Then

            'Dim PelajarID As String = Decrypt(HttpUtility.UrlDecode(PelajarID))

            strSQL = "  SELECT 
                    kpmkv_pelajar.Nama, kpmkv_pelajar.MYKAD, kpmkv_pelajar.AngkaGiliran, 
                    kpmkv_kolej.Nama, kpmkv_kolej.Negeri,
                    kpmkv_kursus.NamaKursus, kpmkv_kursus.KodKursus,
                    kpmkv_kluster.NamaKluster,
                    kpmkv_pelajar_markah.GredBMSetara,
                    kpmkv_SVM.PNGKA, kpmkv_SVM.PNGKV
                    FROM kpmkv_pelajar
                    LEFT JOIN kpmkv_kolej ON kpmkv_kolej.RecordID = kpmkv_pelajar.KolejRecordID
                    LEFT JOIN kpmkv_kursus On kpmkv_kursus.KursusID = kpmkv_pelajar.KursusID
                    LEFT JOIN kpmkv_kluster ON kpmkv_kluster.KlusterID = kpmkv_kursus.KlusterID 
                    LEFT JOIN kpmkv_pelajar_markah ON kpmkv_pelajar_markah.PelajarID = kpmkv_pelajar.PelajarID
                    LEFT JOIN kpmkv_SVM ON kpmkv_svm.PelajarID = kpmkv_pelajar.PelajarID
                    WHERE
                    kpmkv_pelajar.PelajarID = '" & PelajarID & "'"

            strRet = oCommon.ExecuteSQL(strSQL)

            Dim sqlDA As New SqlDataAdapter(strSQL, objConn)
            Dim ds As DataSet = New DataSet
            sqlDA.Fill(ds, "AnyTable")

            For i As Integer = 0 To ds.Tables(0).Rows.Count - 1

                Dim strNama As String = ds.Tables(0).Rows(i).Item(0).ToString
                Dim strMykad As String = ds.Tables(0).Rows(i).Item(1).ToString
                Dim strAG As String = ds.Tables(0).Rows(i).Item(2).ToString
                Dim strInstitusi As String = ds.Tables(0).Rows(i).Item(3).ToString
                Dim strNegeri As String = ds.Tables(0).Rows(i).Item(4).ToString
                Dim strNamaKursus As String = ds.Tables(0).Rows(i).Item(5).ToString
                Dim strKodKursus As String = ds.Tables(0).Rows(i).Item(6).ToString
                Dim strKluster As String = ds.Tables(0).Rows(i).Item(7).ToString
                Dim strPNGKA As String = ds.Tables(0).Rows(i).Item(9).ToString
                Dim strPNGKV As String = ds.Tables(0).Rows(i).Item(10).ToString

                strSQL = " SELECT KursusID FROM kpmkv_pelajar WHERE PelajarID = '" & PelajarID & "'"
                Dim strKursusID As String = oCommon.getFieldValue(strSQL)

                'Dim strStatus As String = ""
                'If (strPNGKA >= 3.33 And strPNGKV >= 3.67) Then
                '    strStatus = "SETARA"
                'ElseIf (strPNGKA < 3.33 Or strPNGKV < 3.67) Then
                '    strStatus = "TAKSETARA"
                'End If

                strSQL = "SELECT Tahun FROM kpmkv_pelajar WHERE PelajarID = '" & PelajarID & "'"
                Dim strTahun As String = oCommon.getFieldValue(strSQL)

                strSQL = "SELECT GredBMSetara FROM kpmkv_SVM WHERE PelajarID = '" & PelajarID & "'"
                Dim strGredBMSetara As String = oCommon.getFieldValue(strSQL)

                strSQL = "SELECT GredSJSetara FROM kpmkv_SVM WHERE PelajarID = '" & PelajarID & "'"
                Dim strGredSJSetara As String = oCommon.getFieldValue(strSQL)

                strSQL = "SELECT Status FROM kpmkv_gred_bmsetara WHERE Gred = '" & strGredBMSetara & "' AND Tahun = '" & strTahun & "'"
                Dim strStatusBM As String = oCommon.getFieldValue(strSQL)

                strSQL = "SELECT Status FROM kpmkv_gred_sjsetara WHERE Gred = '" & strGredBMSetara & "' AND Tahun = '" & strTahun & "'"
                Dim strStatusSJ As String = oCommon.getFieldValue(strSQL)

                strSQL = "SELECT Kompetensi FROM kpmkv_gred_sejarah WHERE Gred = '" & strGredSJSetara & "' AND Tahun = '" & strTahun & "'"
                Dim strKompetensiSJ As String = oCommon.getFieldValue(strSQL)

                strSQL = " SELECT kpmkv_matapelajaran_v.KodMPVOK, kpmkv_matapelajaran_v.NamaMPVOK FROM kpmkv_matapelajaran_v WHERE KursusID = '" & strKursusID & "' AND Tahun = '" & strTahun & "'  ORDER BY kpmkv_matapelajaran_v.Semester"
                Dim sqlDA2 As New SqlDataAdapter(strSQL, objConn)
                Dim ds2 As DataSet = New DataSet
                sqlDA2.Fill(ds2, "AnyTable")

                For j As Integer = 0 To ds2.Tables(0).Rows.Count - 1

                    strSQL = "  SELECT 
                                MAX(CASE WHEN kpmkv_pelajar.Semester = '1' THEN kpmkv_pelajar_markah.SMP_Grade ELSE '' END) AS 'SMP1',
                                MAX(CASE WHEN kpmkv_pelajar.Semester = '2' THEN kpmkv_pelajar_markah.SMP_Grade ELSE '' END) AS 'SMP2',
                                MAX(CASE WHEN kpmkv_pelajar.Semester = '3' THEN kpmkv_pelajar_markah.SMP_Grade ELSE '' END) AS 'SMP3',
                                MAX(CASE WHEN kpmkv_pelajar.Semester = '4' THEN kpmkv_pelajar_markah.SMP_Grade ELSE '' END) AS 'SMP4'
                                FROM kpmkv_pelajar_markah
                                LEFT JOIN kpmkv_pelajar ON kpmkv_pelajar.PelajarID = kpmkv_pelajar_markah.PelajarID
                                WHERE MYKAD = '" & strMykad & "'
                                GROUP BY MYKAD"
                    strRet = oCommon.getFieldValueEx(strSQL)

                    Dim fieldValueEx As Array
                    fieldValueEx = strRet.Split("|")
                    Dim StrGredV1 As String = fieldValueEx(0)
                    Dim StrGredV2 As String = fieldValueEx(1)
                    Dim StrGredV3 As String = fieldValueEx(2)
                    Dim StrGredV4 As String = fieldValueEx(3)

                    If Not fieldValueEx(j) = "G" Then

                        strSQL = " SELECT Status FROM kpmkv_gred_vokasional WHERE Gred = '" & fieldValueEx(j) & "' AND Tahun = '" & strTahun & "'"
                        Dim strStatusV As String = oCommon.getFieldValue(strSQL)

                        Dim strKodMPVOK As String = ds2.Tables(0).Rows(j).Item(0).ToString
                        Dim strNamaMPVOK As String = ds2.Tables(0).Rows(j).Item(1).ToString

                        If j = 0 Then
                            kodVok1.Text = strKodMPVOK & "          " & strNamaMPVOK
                            gredVok1.Text = fieldValueEx(j) & "          " & strStatusV
                        ElseIf j = 1 Then
                            kodVok2.Text = strKodMPVOK & "          " & strNamaMPVOK
                            gredVok2.Text = fieldValueEx(j) & "          " & strStatusV
                        ElseIf j = 2 Then
                            kodVok3.Text = strKodMPVOK & "          " & strNamaMPVOK
                            gredVok3.Text = fieldValueEx(j) & "          " & strStatusV
                        ElseIf j = 3 Then
                            kodVok4.Text = strKodMPVOK & "          " & strNamaMPVOK
                            gredVok4.Text = fieldValueEx(j) & "          " & strStatusV
                        ElseIf j = 4 Then
                            kodVok5.Text = strKodMPVOK & "          " & strNamaMPVOK
                            gredVok5.Text = fieldValueEx(j) & "          " & strStatusV
                        ElseIf j = 5 Then
                            kodVok6.Text = strKodMPVOK & "          " & strNamaMPVOK
                            gredVok6.Text = fieldValueEx(j) & "          " & strStatusV
                        ElseIf j = 6 Then
                            kodVok7.Text = strKodMPVOK & "          " & strNamaMPVOK
                            gredVok7.Text = fieldValueEx(j) & "          " & strStatusV
                        ElseIf j = 7 Then
                            kodVok8.Text = strKodMPVOK & "          " & strNamaMPVOK
                            gredVok8.Text = fieldValueEx(j) & "          " & strStatusV
                        End If

                    End If

                Next

                lblNama.Text = strNama
                lblMykad.Text = strMykad
                lblAG.Text = strAG
                lblInstitusi.Text = strInstitusi & ", " & strNegeri
                lblKluster.Text = strKluster
                lblKursus.Text = strNamaKursus & " (" & strKodKursus & ")"


                lblBM.Text = "1104 BAHASA MELAYU"
                lblGredBM.Text = strGredBMSetara & " " & strStatusBM
                lblSJ.Text = "1251 SEJARAH"
                lblGredSJ.Text = strKompetensiSJ
                lblKompeten.Text = "KOMPETEN SEMUA MODUL " & strKluster
                lblPNGKA.Text = strPNGKA
                lblPNGKV.Text = strPNGKV

                'If strStatus = "SETARA" Then
                '    tblSetara.Visible = True
                'Else
                '    tblSetara.Visible = False
                'End If

            Next

        End If

    End Sub

    Private Function Decrypt(qrCipher As String) As String

        Dim encryptionKey As String = "MAKV2SPBNI99212"

        Dim qrCipherBytes As Byte() = Convert.FromBase64String(qrCipher)
        Using encryptor As Aes = Aes.Create()
            Dim pdb As New Rfc2898DeriveBytes(encryptionKey, New Byte() {&H49, &H76, &H61, &H6E, &H20, &H4D,
         &H65, &H64, &H76, &H65, &H64, &H65,
         &H76})
            encryptor.Key = pdb.GetBytes(32)
            encryptor.IV = pdb.GetBytes(16)
            Using ms As New MemoryStream()
                Using cs As New CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write)
                    cs.Write(qrCipherBytes, 0, qrCipherBytes.Length)
                    cs.Close()
                End Using
                qrCipher = Encoding.Unicode.GetString(ms.ToArray())
            End Using
        End Using

        Return qrCipher

    End Function

End Class