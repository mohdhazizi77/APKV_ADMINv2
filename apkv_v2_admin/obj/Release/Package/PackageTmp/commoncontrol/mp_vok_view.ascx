<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="mp_vok_view.ascx.vb" Inherits="apkv_v2_admin.mp_vok_view1" %>
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Kemaskini Maklumat MataPelajaran Vokasional</td>
    </tr>
    <tr>
        <td>Kohort:</td>
        <td><asp:DropDownList ID="ddlTahun" runat="server" Width="200px"></asp:DropDownList></td>
    </tr>
     <tr>
        <td>Semester:</td>
        <td><asp:DropDownList ID="ddlSemester" runat="server" Width="200px" AutoPostBack ="true" ></asp:DropDownList></td>
    </tr>
     <tr>
          <td>Sesi Pengambilan:</td>
        <td>
            <asp:DropDownList ID="ddlSesi" runat="server" AutoPostBack ="true" >
             <asp:ListItem Value="">-Pilih-</asp:ListItem>
             <asp:ListItem Value="1">1</asp:ListItem>
             <asp:ListItem Value="2">2</asp:ListItem>
             
            </asp:DropDownList></td>
    </tr>
    <tr>
        <td>Kod Program:</td>
        <td> <asp:DropDownList ID="ddlKursus" runat="server"  Width="350px"></asp:DropDownList></td>
    </tr>

    <tr>
        <td>Kod Matapelajaran Vokasional:</td>
        <td><asp:TextBox ID="txtKodMpVok" runat="server" Width="350px" MaxLength="50" Enabled ="false" ></asp:TextBox>*</td>
    </tr>
    <tr>
        <td>Nama Matapelajaran Vokasional:</td>
        <td><asp:TextBox ID="txtNamaMpVok" runat="server" Width="350px" MaxLength="250"></asp:TextBox>*</td>
    </tr>
    <tr>
        <td colspan="3"><asp:Button ID="btnUpdate" runat="server" Text="Kemaskini" CssClass="fbbutton" />
        </td>
    </tr>
    </table>
<div class="info" id="divMsg" runat="server">
    <asp:Label ID="lblMsg" runat="server" Text="Mesej..."></asp:Label>
</div>
