<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="svmu_daftar_calon_ulang.ascx.vb" Inherits="apkv_v2_admin.svmu_daftar_calon_ulang1" %>

<br />

<div style="width: 100%; border: solid 2px #000000;">

    <br />

    <ol type="1" style="text-align: left">
        <li>
            <p>Peperiksaan ini ditawarkan hanya kepada <b>calon yang pernah menduduki peperiksaan SVM (calon mengulang) sahaja</b> tertakluk kepada syarat-syarat yang ditetapkan oleh Lembaga Peperiksaan.</p>
        </li>
        <li>
            <p>Mata pelajaran yang ditawarkan adalah <b>Bahasa Melayu (1104) dan Sejarah (1251) sahaja</b> tertakluk kepada syarat-syarat yang ditetapkan oleh Lembaga Peperiksaan.</p>
        </li>
        <li>
            <p>Pastikan semua maklumat yang diberikan adalah benar dan tepat sebelum menghantar borang permohonan. Lembaga Peperiksaan berhak membatalkan pendaftaran calon sekiranya maklumat yang diberikan tidak benar atau palsu.</p>
        </li>
        <li>
            <p>Membuat permohonan pendaftaran SVMU di portal Permohonan Pendaftaran SVMU antara
                <asp:Label ID="lblTarikhMula" runat="server"></asp:Label>
                sehingga
                <asp:Label ID="lblTarikhAkhir" runat="server"></asp:Label>.</p>
        </li>
        <li>
            <p>Bayaran bagi permohonan pendaftaran SVMU hendaklah dilaksanakan melalui kaedah perbankan elektronik.</p>
        </li>
        <li>
            <p>Setelah melaksanakan permohonan dan bayaran secara dalam talian, pemohon hendaklah mencetak dan menghantar/mengepos dokumen berikut ke Jabatan Pendidikan Negeri (JPN) tempat pemohon memilih untuk menduduki peperiksaan :</p>
            <ul>
                <li>Salinan Kenyataan Semakan;</li>
                <li>Salinan Resit bayaran (perbankan elektronik); dan</li>
                <li><b>1 keping sampul Pos Laju Prabayar atau Pos Ekspres bersaiz A5</b> dan <b>1 keping sampul Pos Laju Prabayar atau Pos Ekspres bersaiz A4</b>.<a id="formatSampul" runat="server">Rujuk format sampul di sini</a>.</li>
            </ul>
            <p>Kegagalan calon mengemukakan dokumen di perkara 6, akan menyebabkan calon tidak menerima maklumat penting berkaitan peperiksaan.</p>
            <p>Alamat JPN boleh didapati di portal Lembaga Peperiksaan (<a href="http://lp.moe.gov.my">http://lp.moe.gov.my</a>) .</p>
        </li>
        <li>
            <p>Pastikan borang Pernyataan Pendaftaran Peperiksaan (PPP) dicetak dan dibawa bersama-sama maklumat peribadi ke pusat peperiksaan semasa hari peperiksaan dijalankan.</p>
        </li>
        
    </ol>

    <table style="width: 100%">

        <tr>

            <td style="width: 35px"></td>
            <td style="width: 130px">No. Kad Pengenalan</td>
            <td>:</td>
            <td>
                <asp:TextBox ID="txtMYKAD" runat="server" Width="150px"></asp:TextBox>
                (Nota: Tanpa "-")
            </td>
            <td>
                <asp:Label ID="lblMsgMYKAD" runat="server" ForeColor="Red"></asp:Label>
            </td>

        </tr>

        <tr>

            <td style="width: 35px"></td>
            <td style="width: 130px">Angka Giliran</td>
            <td>:</td>
            <td>
                <asp:TextBox ID="txtAngkaGiliran" runat="server" Width="150px"></asp:TextBox>
                (Contoh: K011FETE001)</td>
            <td>
                <asp:Label ID="lblMsgAngkaGiliran" runat="server" ForeColor="Red"></asp:Label>
            </td>

        </tr>

    </table>

    <br />

    <table style="width: 100%">

         <tr>
            <td style="text-align:center"><asp:Label ID="lblMsg1" runat="server" Text="ANDA TELAH MEMBUAT PERMOHONAN UNTUK KEDUA-DUA SUBJEK SVMU" ForeColor="Red" Font-Bold="true" Visible="false"></asp:Label></td>
        </tr>

        <tr>

            <td style="text-align: center">
                <asp:Button ID="btnBack" runat="server" Text="Ke Laman Utama" />
                <asp:Button ID="btnApply" runat="server" Text="Mohon" />
            </td>

        </tr>

       

    </table>

    <br />

</div>

<br />
<br />
<br />
<br />
<br />

