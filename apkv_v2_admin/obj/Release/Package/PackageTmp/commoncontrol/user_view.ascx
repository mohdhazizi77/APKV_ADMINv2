<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="user_view.ascx.vb" Inherits="apkv_v2_admin.user_view" %>
<table class="fbform" style ="width :100%">
    <tr class="fbform_header">
        <td colspan="2">Paparan Pengguna >> Paparan Maklumat Pengguna</td>
    </tr>
</table>
<br />
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="3">Paparan Maklumat Pengguna</td>
    </tr>
    <tr>
         <td style ="width :20%">Nama Pengguna</td>
        <td style ="width :2%">:</td>
        <td><asp:Label ID="lblNama" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    
    <tr>
        <td >Mykad</td>
        <td>:</td>
        <td><asp:Label ID="lblMYKAD" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <td >Telefon</td>
        <td>:</td>
        <td><asp:Label ID="lblTel" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td >Emel</td>
        <td>:</td>
        <td><asp:Label ID="lblEmail" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <td>Status</td>
        <td>:</td>
        <td><asp:Label ID="lblStatus2" runat="server" Text=""></asp:Label></td>
    </tr>
    <tr>
        <td colspan="3"></td>
    </tr>
  <tr>
        <td colspan="3"><asp:Button ID="btnExecute" runat="server" Text="Kemaskini" CssClass="fbbutton" />&nbsp;
        </td>
    </tr>
    </table>
<table class="fbform" style ="width :100%">
    <tr class="fbform_header">
        <td colspan="3">Konfigurasi Sistem</td>
    </tr>
    <tr>
        <td style ="width :15%">Katalaluan</td>
        <td style ="width :2%">:</td>
        <td><asp:TextBox ID="txtKatalaluan" runat="server" TextMode ="Password"  Width="200px" MaxLength="50"></asp:TextBox></td>
    </tr>
    <tr>
        <td>Kumpulan Pengguna</td>
        <td>:</td>
        <td><asp:DropDownList ID="ddlKumpulan" runat="server" AutoPostBack="false" Width="200px"></asp:DropDownList></td>
    </tr>
    <tr>
        <td colspan="3"><asp:Button ID="btnUpdate" runat="server" Text="Simpan" CssClass="fbbutton" />&nbsp;</td>
    </tr>
</table>

<br />
<div class="info" id="divMsg" runat="server">
  <asp:Label ID="lblKolejID" runat="server" Text="" Visible="false"></asp:Label>
  <asp:Label ID="lblUserID" runat="server" Text="" Visible="false"></asp:Label>
  <asp:Label ID="lblMsg" runat="server" Text="Mesej..."></asp:Label>
</div>
