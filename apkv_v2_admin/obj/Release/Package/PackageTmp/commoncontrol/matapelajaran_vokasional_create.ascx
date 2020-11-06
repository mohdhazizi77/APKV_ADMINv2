<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="matapelajaran_vokasional_create.ascx.vb" Inherits="apkv_v2_admin.matapelajaran_vokasional_create" %>
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Pendaftaran >> Matapelajaran Vokasional</td>
    </tr>
</table>
<br />
<table class="fbform">
    <tr>
        <td style="width:200px">Kohort:</td>
        <td><asp:DropDownList ID="ddlTahun" runat="server" Width="100px"></asp:DropDownList></td>
    </tr>
     <tr>
        <td>Semester:</td>
        <td><asp:DropDownList ID="ddlSemester" runat="server" Width="100px" AutoPostBack="true"></asp:DropDownList></td>
    </tr>
     <tr>
          <td>Sesi Pengambilan:</td>
        <td><asp:CheckBoxList ID="chkSesi" runat="server"  AutoPostBack="true" RepeatDirection="Horizontal">
             <asp:ListItem>1</asp:ListItem>
             <asp:ListItem>2</asp:ListItem>
             </asp:CheckBoxList></td>
    </tr>
    <tr>
        <td>Kod Program:</td>
        <td> <asp:DropDownList ID="ddlKursus" runat="server"  Width="350px"></asp:DropDownList></td>
    </tr>

    <tr>
        <td>Kod Matapelajaran Vokasional:</td>
        <td><asp:TextBox ID="txtKodMpVok" runat="server" Width="350px" MaxLength="50"></asp:TextBox>*</td>
    </tr>
    <tr>
        <td>Nama Matapelajaran Vokasional:</td>
        <td><asp:TextBox ID="txtNamaMpVok" runat="server" Width="350px" MaxLength="250"></asp:TextBox>*</td>
    </tr>
    <tr>
        <td>&nbsp</td>
    </tr>
    <tr>
        <td></td>
        <td colspan="3"><asp:Button ID="btnCreate" runat="server" Text="Daftar" CssClass="fbbutton" />
        </td>
    </tr>
    </table>
<div class="info" id="divMsg" runat="server">
    <asp:Label ID="lblMsg" runat="server" Text="Mesej..."></asp:Label>
</div>
