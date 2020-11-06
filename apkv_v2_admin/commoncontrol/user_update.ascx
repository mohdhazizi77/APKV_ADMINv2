<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="user_update.ascx.vb" Inherits="apkv_v2_admin.user_update" %>
<table class="fbform" style ="width :100%">
    <tr class="fbform_header">
        <td colspan="3">Paparan Pengguna >> Paparan Maklumat Pengguna</td>
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
        <td><asp:textbox ID="txtNama" runat="server" Text=""></asp:textbox></td>
    </tr>
    
    <tr>
        <td >Mykad</td>
        <td>:</td>
        <td><asp:textbox ID="txtMYKAD" runat="server" Text=""></asp:textbox>
        </td>
    </tr>
    <tr>
        <td >Telefon</td>
        <td>:</td>
        <td><asp:textbox  ID="txtTel" runat="server"></asp:textbox>
        </td>
    </tr>
    <tr>
        <td >Emel</td>
        <td>:</td>
        <td><asp:textbox  ID="txtEmail" runat="server" Text=""></asp:textbox>
        </td>
    </tr>
    <tr>
         <td >Status</td>
         <td>:</td>
        <td><asp:DropDownList ID="ddlStatus" runat="server" AutoPostBack="false"></asp:DropDownList>*</td>
    </tr>
    <tr>
        
        <td class="fbform_sap" colspan ="3"></td>
    </tr>
    <tr>
       <td colspan="3">
            <asp:Button ID="btnUpdate" runat="server" Text="Simpan" CssClass="fbbutton" />&nbsp;
            <asp:Button ID="btnDelete" runat="server" Text="Hapuskan" CssClass="fbbutton" />
        </td>
    </tr>
</table>
<br />
<br />
<div class="info" id="divMsg" runat="server">
  <asp:Label ID="lblKolejID" runat="server" Text="" Visible="false"></asp:Label>
  <asp:Label ID="lblUserID" runat="server" Text="" Visible="false"></asp:Label>
  <asp:Label ID="lblMsg" runat="server" Text="Mesej..."></asp:Label>
</div>
