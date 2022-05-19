<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="svmu_tindakan_calon.ascx.vb" Inherits="apkv_v2_admin.svmu_tindakan_calon1" %>

<br />

<table class="fbform">

    <tr class="fbform_header">
        <td colspan="2">KEPUTUSAN PERMOHONAN PENDAFTARAN PEPERIKSAAN SIJIL VOKASIONAL MALAYSIA ULANGAN (SVMU)
            <asp:Label ID="lblTahun" runat="server"></asp:Label></td>
    </tr>

</table>

<br />

<table class="fbform" style="padding-right: 10px; padding-left: 10px">

    <tr>

        <td>

            <p>Maklumat permohonan pendaftaran sebagai calon mengulang SVM anda telah dihantar.</p>

            <ul style="text-align: left">

                <li>
                    <p>
                        <b>Nama Calon :
                        <asp:Label ID="lblNama" runat="server"></asp:Label></b>
                    </p>
                </li>

                <li>
                    <p>
                        <b>No. K/P Peperiksaan :
                        <asp:Label ID="lblMYKAD" runat="server"></asp:Label></b>
                    </p>
                </li>

                <li>
                    <p>
                        <b>No. Permohonan :
                        <asp:Label ID="lblNoPermohonan" runat="server"></asp:Label></b>
                    </p>
                </li>

                <li>
                    <p>
                        <b>Status Permohonan :
                        <asp:Label ID="lblStatusPermohonan" runat="server"></asp:Label></b>
                    </p>
                </li>

                <li>
                    <p>
                        <b>Status Bayaran :
                        <asp:Label ID="lblStatusBayaran" runat="server"></asp:Label>
                            <a visible="true" id="link" target="_blank" runat="server">(Sila cetak resit bayaran di sini)</a></b>
                    </p>
                </li>

            </ul>

        </td>

    </tr>

</table>

<br />

<table style="width: 100%">


    <tr class="fbform_header">
        <td colspan="2">TINDAKAN CALON</td>
    </tr>

    <tr>

        <td colspan="2">Calon hendaklah mencetak dan menghantar/mengepos dokumen berikut dalam tempoh 7 hari ke Jabatan Pendidikan Negeri (JPN) tempat calon memilih untuk menduduki peperiksaan: </td>

    </tr>

    <tr>
        <td>
            <ol>
                <li><b>Salinan Kenyataan Semakan;</b></li>
                <li><b>Salinan Resit bayaran (perbankan elektronik); dan</b></li>
                <li><b>1 keping sampul Pos Laju Prabayar atau Pos Ekspres bersaiz A5</b> dan <b>1 keping sampul Pos Laju Prabayar atau Pos Ekspres bersaiz A4</b>.</li>
            </ol>
        </td>
    </tr>

    <tr>

        <td colspan="2">Kegagalan calon mengemukakan dokumen di atas, akan menyebabkan calon tidak menerima maklumat penting berkaitan peperiksaan.</td>

    </tr>


    <tr>
        <td>Alamat JPN boleh didapati di portal Lembaga Peperiksaan (<a href="http://lp.moe.gov.my">http://lp.moe.gov.my</a>)</td>
    </tr>

    <tr>
        <td>
            <p><b>Nota:</b></p>
        </td>
    </tr>

    <tr>
        <td><b>Pernyataan Pendaftaran Peperiksaan (PPP)</b> hanya boleh dicetak selepas satu (1) bulan dari tarikh tutup pendaftaran. </td>
    </tr>

    <tr>
        <td>Cetak dokumen di bawah:</td>
    </tr>

    <tr>
        <td>
            <ol>
                <li><a id="KenyataanSemakan" runat="server">Kenyataan Semakan</a> (Saiz Kertas: A4 / Orientasi: Portrait)</li>
                <li><a id="PPP" runat="server">Pernyataan Pendaftaran Peperiksaan</a> (PPP)</li>
            </ol>
        </td>
    </tr>
    <tr>
        <td style="text-align: center">
            <p>
                <asp:Button ID="btnBack" runat="server" Text="Ke Halaman Utama" /></p>
        </td>
    </tr>

</table>







