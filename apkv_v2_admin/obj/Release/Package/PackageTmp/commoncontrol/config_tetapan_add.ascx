<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="config_tetapan_add.ascx.vb" Inherits="apkv_v2_admin.config_tetapan_add1" %>

<table class="fbform">

    <tr class="fbform_header">

        <td>Konfigurasi Umum >> Tetapan >> Daftar Baru

        </td>

    </tr>

</table>

<br />

<table class="fbform">

    <tr class="fbform_header">

        <td colspan="3">Daftar Tetapan Baru</td>

    </tr>

    <tr>

        <td style="width: 150px;">Config Name</td>
        <td>
            <asp:TextBox ID="txtConfigName" runat="server" Width="350px"></asp:TextBox>
        </td>

    </tr>

    <tr>

        <td style="width: 150px;">Config Value</td>
        <td>
            <asp:TextBox ID="txtConfigValue" runat="server" Width="350px"></asp:TextBox>
        </td>

    </tr>

    <tr>

        <td style="width: 150px;">Config Menu Utama</td>
        <td>
            <asp:DropDownList ID="ddlConfigMenuUtama" runat="server" AutoPostBack="true" Width="350px"></asp:DropDownList>
        </td>

    </tr>

    <tr>

        <td style="width: 150px;">Config Menu</td>
        <td>
            <asp:DropDownList ID="ddlConfigMenu" runat="server" Width="350px"></asp:DropDownList>
        </td>

    </tr>

    <tr>

        <td style="width: 150px;"></td>
        <td>
            <asp:Button ID="btnDaftar" runat="server" Text="Daftar" CssClass="fbbutton" />
            <a href="~/config_tetapan_list.aspx" runat="server">Senarai Tetapan</a>
        </td>

    </tr>

</table>

<br />

<div class="info" id="divMsg" runat="server">

    <asp:Label ID="lblMsg" runat="server" Text="Mesej..."></asp:Label>

</div>
