<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="pentaksir_pendaftaran.ascx.vb" Inherits="apkv_v2_admin.pentaksir_pendaftaran" %>

<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Kes T >> Semak Semula</td>
    </tr>
</table>

<br />

<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Maklumat Pelajar</td>
    </tr>
    <tr>
        <td style="width: 200px">To:</td>
        <td>
            <asp:TextBox ID="txtMykad" runat="server" Width="350px"></asp:TextBox></td>
    </tr>
    <tr>
        <td style="width: 200px"></td>
        <td colspan="2">
            <asp:Button ID="btnSend" runat="server" Text="Hantar" CssClass="fbbutton" />&nbsp;</td>
    </tr>
</table>

<div class="info" id="divMsg" runat="server">

    <asp:Label ID="lblMsg" runat="server" Text="System message..."></asp:Label>

</div>
