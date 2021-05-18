<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="svmu_rumusan_pendaftaran.ascx.vb" Inherits="apkv_v2_admin.svmu_rumusan_pendaftaran" %>

<br />

<table class="fbform">

    <tr class="fbform_header">
        <td colspan="2">Maklumat Peribadi</td>
    </tr>

</table>

<br />

<table class="fbform" style="padding-right: 10px">

    <tr>

        <td style="width: 150px">Nama</td>
        <td style="width: 3px">:</td>
        <td colspan="6">
            <asp:TextBox ID="txtNama" runat="server" Enabled="false" Width="100%"></asp:TextBox>
        </td>

    </tr>

    <tr>

        <td style="width: 150px">No. Kad Pengenalan</td>
        <td style="width: 3px">:</td>
        <td style="width: 150px">
            <asp:TextBox ID="txtMYKAD" runat="server" Enabled="false" Width="90%"></asp:TextBox>
        </td>

        <td style="width: 150px">Angka Giliran</td>
        <td style="width: 3px">:</td>
        <td style="width: 150px">
            <asp:TextBox ID="txtAngkaGiliran" runat="server" Enabled="false" Width="100%"></asp:TextBox>
        </td>

    </tr>

    <tr>

        <td style="width: 150px">Tarikh Lahir</td>
        <td style="width: 3px">:</td>
        <td style="width: 150px">
            <asp:TextBox ID="txtDate" runat="server" Enabled="false" ReadOnly="true" Width="90%"></asp:TextBox>
        </td>

        <td style="width: 150px">Jantina</td>
        <td style="width: 3px">:</td>
        <td style="width: 150px">
            <asp:TextBox ID="txtJantina" runat="server" Enabled="false" Width="100%"></asp:TextBox>
        </td>

    </tr>

    <tr>

        <td style="width: 150px">Bangsa</td>
        <td style="width: 3px">:</td>
        <td style="width: 150px">
            <asp:TextBox ID="txtBangsa" runat="server" Enabled="false" Width="90%"></asp:TextBox>
        </td>

        <td style="width: 150px">Agama</td>
        <td style="width: 3px">:</td>
        <td style="width: 150px">
            <asp:TextBox ID="txtAgama" runat="server" Enabled="false" Width="100%"></asp:TextBox>
        </td>

    </tr>

    <tr>

        <td style="width: 150px">Kolej</td>
        <td style="width: 3px">:</td>
        <td colspan="6">
            <asp:TextBox ID="txtKolej" runat="server" Enabled="false" Width="100%"></asp:TextBox>
        </td>

    </tr>

    <tr>

        <td style="width: 150px">Kohort</td>
        <td style="width: 3px">:</td>
        <td style="width: 150px">
            <asp:TextBox ID="txtKohort" runat="server" Enabled="false" Width="90%"></asp:TextBox>
        </td>

    </tr>

    <tr>

        <td style="width: 150px">Alamat</td>
        <td style="width: 3px">:</td>
        <td colspan="6">
            <asp:TextBox ID="txtAlamat" runat="server" Enabled="false" Width="100%"></asp:TextBox>
        </td>

    </tr>

    <tr>

        <td style="width: 150px">Poskod</td>
        <td style="width: 3px">:</td>
        <td style="width: 150px">
            <asp:TextBox ID="txtPoskod" runat="server" Enabled="false" Width="90%"></asp:TextBox>
        </td>

        <td style="width: 150px">Bandar</td>
        <td style="width: 3px">:</td>
        <td style="width: 150px">
            <asp:TextBox ID="txtBandar" runat="server" Enabled="false" Width="100%"></asp:TextBox>
        </td>

    </tr>

    <tr>

        <td style="width: 150px">Negeri</td>
        <td style="width: 3px">:</td>
        <td style="width: 150px">
            <asp:TextBox ID="txtNegeri" runat="server" Enabled="false" Width="90%"></asp:TextBox>
        </td>

        <td style="width: 150px">No. Telefon (HP)</td>
        <td style="width: 3px">:</td>
        <td style="width: 150px">
            <asp:TextBox ID="txtTelefon" runat="server" Enabled="false" Width="100%"></asp:TextBox>
        </td>

    </tr>

    <tr>

        <td style="width: 150px">Alamat E-mel</td>
        <td style="width: 3px">:</td>
        <td colspan="6">
            <asp:TextBox ID="txtEmail" runat="server" Enabled="false" Width="100%"></asp:TextBox>
        </td>

    </tr>

</table>

<br />

<table class="fbform">

    <tr class="fbform_header">
        <td colspan="2">Mata Pelajaran yang Didaftarkan</td>
    </tr>

</table>

<br />

<table border="1" class="fbform" style="padding-right: 10px">

    <tr>

        <th colspan="2" style="text-align: center">Keterangan</th>
        <th style="text-align: center">Bayaran</th>

    </tr>

    <tr>

        <td style="text-align: center; width: 10px">1
        </td>

        <td style="width: 200px">Bayaran Asas SVM
        </td>

        <td style="text-align: center; width: 150px">
            RM <asp:Label ID="lblRMAsas" runat="server"></asp:Label>
        </td>

    </tr>

    <tr id="trBM" runat="server">

        <td style="text-align: center; width: 10px">2
        </td>

        <td style="width: 200px">1104 - Bahasa Melayu
        </td>

        <td style="text-align: center; width: 150px">
            RM <asp:Label ID="lblRMBM" runat="server"></asp:Label>
        </td>

    </tr>

    <tr id="trSJ" runat="server">

        <td style="text-align: center; width: 10px">3
        </td>

        <td style="width: 200px">1251 - Sejarah
        </td>

        <td style="text-align: center; width: 150px">
            RM <asp:Label ID="lblRMSJ" runat="server"></asp:Label>
        </td>

    </tr>

    <tr>

        <th colspan="2" style="text-align: right">Jumlah Bayaran</th>
        <th style="text-align: center">
            RM <asp:Label ID="lblTotalRM" runat="server"></asp:Label></th>

    </tr>

</table>

<br />

<table class="fbform">

    <tr class="fbform_header">
        <td colspan="2">Pusat Peperiksaan Pilihan</td>
    </tr>

</table>

<br />

<table border="1" class="fbform" style="padding-right: 10px">

    <tr>
        <th style="text-align: center">Kod Pusat</th>
        <th style="text-align: center">Negeri</th>
        <th style="text-align: center">Kolej Vokasional</th>
    </tr>

    <tr>
        <th style="text-align: center"><u><asp:Label ID="lblKodPusat" runat="server"></asp:Label></u></th>
        <td style="text-align: center"><asp:Label ID="lblNegeri" runat="server"></asp:Label></td>
        <td style="text-align: center"><asp:Label ID="lblKolej" runat="server"></asp:Label></td>
    </tr>

</table>

<br />

<table style="width: 100%">

    <tr>

        <td style="text-align: left">
            <asp:Button ID="btnBack" runat="server" Text="Kembali" />
        </td>

        <td style="text-align: right">
            <asp:Button ID="btnProceed" runat="server" Text="Teruskan" />
        </td>

    </tr>

</table>

