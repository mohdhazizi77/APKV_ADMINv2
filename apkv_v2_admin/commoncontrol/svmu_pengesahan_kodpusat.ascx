<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="svmu_pengesahan_kodpusat.ascx.vb" Inherits="apkv_v2_admin.svmu_pengesahan_kodpusat" %>

<table class="fbform">

    <tr class="fbform_header">
        <td colspan="2">Maklumat Calon</td>
    </tr>

</table>

<br />

<table class="fbform" style="padding-right: 10px">

    <tr>

        <td style="width: 130px">Nama</td>
        <td style="width: 3px">:</td>
        <td colspan="6">
            <asp:TextBox ID="txtNama" runat="server" Enabled="false" Width="100%"></asp:TextBox>
        </td>

    </tr>

    <tr>

        <td style="width: 130px">No. Kad Pengenalan</td>
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

        <td style="width: 130px">Tarikh Lahir</td>
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

        <td style="width: 130px">Bangsa</td>
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

        <td style="width: 130px">Kolej</td>
        <td style="width: 3px">:</td>
        <td colspan="6">
            <asp:TextBox ID="txtKolej" runat="server" Enabled="false" Width="100%"></asp:TextBox>
        </td>

    </tr>

    <tr>

        <td style="width: 130px">Kohort</td>
        <td style="width: 3px">:</td>
        <td style="width: 150px">
            <asp:TextBox ID="txtKohort" runat="server" Enabled="false" Width="90%"></asp:TextBox>
        </td>

    </tr>

    <tr>

        <td style="width: 130px">Alamat</td>
        <td style="width: 3px">:</td>
        <td colspan="6">
            <asp:TextBox ID="txtAlamat" runat="server" Enabled="false" Width="100%"></asp:TextBox>
        </td>

    </tr>

    <tr>

        <td style="width: 130px">Poskod</td>
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

        <td style="width: 130px">Negeri</td>
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

        <td style="width: 130px">Alamat E-mel</td>
        <td style="width: 3px">:</td>
        <td colspan="6">
            <asp:TextBox ID="txtEmail" runat="server" Enabled="false" Width="100%"></asp:TextBox>
        </td>

    </tr>

</table>

<br />

<table class="fbform">

    <tr class="fbform_header">
        <td colspan="2">Pusat Peperiksaan</td>
    </tr>

</table>

<br />

<table border="0" class="fbform" style="padding-right: 10px">

    <tr>
        <td style="width: 100px">Negeri</td>
        <td>:</td>
        <td>
            <asp:Label ID="lblNegeri" runat="server"></asp:Label></td>
    </tr>

    <tr>
        <td style="width: 100px">Kod Pusat</td>
        <td>:</td>
        <td>
            <asp:Label ID="lblKodPusat" runat="server"></asp:Label></td>
    </tr>

    <tr>
        <td style="width: 100px">Kolej Vokasional</td>
        <td>:</td>
        <td>
            <asp:DropDownList ID="ddlKolej" runat="server" AutoPostBack="false"></asp:DropDownList>
            <asp:Button ID="btnKemaskini" runat="server" Text="Kemaskini" />
            <asp:Label ID="lblKemaskini" runat="server"></asp:Label></td>

    </tr>

</table>

<br />

<table style="width: 100%">

    <tr>

        <td style="text-align: right">
            <asp:Button ID="btnBack" runat="server" Text="Kembali" />

            <asp:Button ID="btnProceed" runat="server" Text="Pengesahan" />
        </td>

    </tr>

</table>


