<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="svmu_pengakuan_calon.ascx.vb" Inherits="apkv_v2_admin.svmu_pengakuan_calon" %>

<br />

<table class="fbform">

    <tr class="fbform_header">
        <td colspan="2">Pengakuan Calon</td>
    </tr>

</table>

<br />

<table class="fbform" style="padding-right: 10px; padding-left:10px">

    <tr>

        <td></td>
        <td>

            <ol type="1" style="text-align: left">
                <li>
                    <p>Saya mengaku telah membaca dan memahami semua peraturan dan arahan peperiksaan ini. Lembaga Peperiksaan berhak membatalkan pendaftaran saya sekiranya maklumat yang diberikan tidak benar atau palsu dan bayaran pendaftaran tidak akan dikembalikan.</p>
                </li>
                <li>
                    <p>Saya telah memastikan mata pelajaran yang didaftarkan adalah mata pelajaran yang ingin saya duduki untuk peperiksaan ini.</p>
                </li>
                <li>
                    <p>Sekiranya saya membuat permohonan pembetulan mata pelajaran, saya akan dikenakan bayaran RM30.00 bagi setiap mata pelajaran yang ditambah dan RM30.00 bagi setiap mata pelajaran yang digugurkan.</p>
                </li>
                <li>
                    <p>Saya akan memastikan mata pelajaran yang dicetak pada Pernyataan Pendaftaran Peperiksaan (PPP) adalah sama dengan mata pelajaran yang telah saya daftarkan.</p>
                </li>
                <li>
                    <p>Saya juga faham bahawa saya perlu membuat bayaran permohonan pendaftaran peperiksaan melalui perbankan elektronik. Proses pendaftaran selesai setelah pembayaran dibuat.</p>
                </li>
                <li>
                    <p>Saya akan menghantar/mengepos dokumen berikut dalam tempoh 7 hari selepas bayaran pendaftaran diselesaikan ke Jabatan Pendidikan Negeri (JPN) tempat saya memilih untuk menduduki peperiksaan :</p>
                    <ul>
                        <li>Salinan Kenyataan Semakan;</li>
                        <li>Salinan Resit bayaran (perbankan elektronik); dan</li>
                        <li>2 keping Pos Laju Prabayar atau Pos Ekspres bersaiz A3  beralamat sendiri.</li>
                    </ul>
                </li>
                <li>
                    <p>Saya mengaku sekiranya saya gagal mengemukakan dokumen di perkara 6, saya tidak akan menerima maklumat penting berkaitan peperiksaan.</p>
                </li>
            </ol>
            <p style="text-align:center"><b><asp:CheckBox ID="chkPengakuan" runat="server" AutoPostBack="true" /> Saya bersetuju dengan semua syarat-syarat iaitu perkara 1 hingga 7 yang dinyatakan di atas</b>.</p>

        </td>

    </tr>

    <tr>
        <td colspan="2">Nota:	Sila klik pada ruangan <asp:CheckBox runat="server" Enabled="false" Checked="false"/> sekiranya anda telah membaca, memahami dan bersetuju dengan semua peraturan dan arahan peperiksaan di atas sebelum menghantar borang permohonan.</td>
    </tr>

</table>

<br />

<table style="width: 100%">

    <tr>

        <td style="text-align: left">
            <asp:Button ID="btnBack" runat="server" Text="Kembali" />
        </td>

        <td style="text-align: right">
            <asp:Button ID="btnSubmit" runat="server" Enabled="false" Text="Hantar" />
        </td>

    </tr>

</table>



