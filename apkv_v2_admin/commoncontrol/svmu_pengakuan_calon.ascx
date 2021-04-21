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

        <td><asp:CheckBox ID="chkPengakuan" runat="server" /></td>
        <td>

            <ul style="text-align: left">
                <li>
                    <p>Saya mengaku telah membaca dan memahami semua peraturan dan arahan peperiksaan ini.</p>
                </li>
                <li>
                    <p>Saya telah memastikan nama program yang didaftarkan adalah program yang saya ingin duduki untuk peperiksaan / pentaksiran ini. Saya juga ingin memastikan nama program yang dicetak pada Kenyataan Kemasukan Peperiksaan adalah sama dengan program / mata pelajaran yang telah saya nyatakan pada borang pendaftaran.</p>
                </li>
                <li>
                    <p>Sekiranya maklumat yang diberikan tidak benar, pendaftaran ini akan terbatal dengan sendirinya</p>
                </li>
            </ul>

        </td>

    </tr>

    <tr>
        <td colspan="2">Sila tandakan di kotak <asp:CheckBox runat="server" Enabled="false" Checked="false"/> sekiranya anda telah membaca, memahami dan bersetuju dengan semua peraturan dan arahan peperiksaan di atas sebelum menghantar borang permohonan.</td>

    </tr>

</table>

<br />

<table style="width: 100%">

    <tr>

        <td style="text-align: left">
            <asp:Button ID="btnBack" runat="server" Text="Kembali" />
        </td>

        <td style="text-align: right">
            <asp:Button ID="btnSubmit" runat="server" Text="Hantar" />
        </td>

    </tr>

</table>



