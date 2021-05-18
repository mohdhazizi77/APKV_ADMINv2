<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="svmu_pembayaran_pendaftaran.ascx.vb" Inherits="apkv_v2_admin.svmu_pembayaran_pendaftaran" %>

<br />

<table class="fbform">

    <tr class="fbform_header">
        <td colspan="2">Permohonan Pendaftaran SVMU</td>
    </tr>

</table>

<br />

<table class="fbform" style="padding-right: 10px; padding-left:10px">

    <tr>

        <td>

            <p>Permohonan pendaftaran anda belum selesai. Sila buat bayaran.</p>

            <ul style="text-align: left">
             
                <li>
                    <p><b>No. K/P Peperiksaan : <asp:Label ID="lblMYKAD" runat="server"></asp:Label></b></p>
                </li>

                <li>
                    <p><b>No. Permohonan : <asp:Label ID="lblNoPermohonan" runat="server"></asp:Label></b></p>
                </li>

                <li>
                    <p><b>Status Bayaran : <asp:Label ID="lblStatusBayaran" runat="server" Text="BELUM BAYAR"></asp:Label></b></p>
                </li>
               
            </ul>

        </td>

    </tr>

    <tr>

        <td colspan="2">Sila tekan butang <b>Perbankan Elektronik</b> untuk membuat pembayaran.</td>

    </tr>

</table>

<br />

<table style="width: 100%">

    <tr>

                <td style="text-align: center">
            <asp:Button ID="btnPay" runat="server" Text="Perbankan Elektronik" />
        </td>

    </tr>

</table>






