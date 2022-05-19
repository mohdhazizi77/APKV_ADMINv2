<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="create_user.ascx.vb" Inherits="apkv_v2_admin.create_user" %>
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Pendaftaran >> Pengguna</td>
    </tr>
</table>
<br />
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Daftar Pengguna Baru</td>
    </tr>
    <tr>
        <td style="width: 200px">Kumpulan Pengguna:</td>
        <td>
            <asp:DropDownList ID="ddlKumpulan" runat="server" AutoPostBack="true" Width="350px"></asp:DropDownList></td>
    </tr>
    <tr>
        <td>Negeri:</td>
        <td>
            <asp:DropDownList ID="ddlNegeri" runat="server" AutoPostBack="false" Width="350px"></asp:DropDownList></td>
    </tr>
    <tr>
        <td>Nama :</td>
        <td>
            <asp:TextBox ID="txtNama" runat="server" Width="350px"></asp:TextBox></td>

    </tr>
    <tr>
        <td>Mykad:</td>
        <td>
            <asp:TextBox ID="txtMYKAD" runat="server" Width="350px" MaxLength="12"></asp:TextBox></td>
    </tr>
    <tr>
        <td>Telefon:</td>
        <td>
            <asp:TextBox ID="txtTel" runat="server" Width="350px" MaxLength="15"></asp:TextBox></td>
    </tr>
    <tr>
        <td>Emel:</td>
        <td>
            <asp:TextBox ID="txtEmail" runat="server" Width="350px" MaxLength="150"></asp:TextBox></td>
    </tr>
    <tr>
        <td>Login ID:</td>
        <td>
            <asp:TextBox ID="txtLogin" runat="server" Width="350px"></asp:TextBox></td>
    </tr>
    <tr>
        <td>KataLaluan:</td>
        <td>
            <asp:TextBox ID="txtPwd" runat="server" Width="350px"></asp:TextBox></td>
    </tr>
    <tr>
        <td></td>
        <td colspan="2">
            <asp:Button ID="btnCreate" runat="server" Text="Daftar Baru" CssClass="fbbutton" />&nbsp;</td>
    </tr>
</table>

<br />

<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Import Pengguna
        </td>
    </tr>
    <tr>
        <td style="width: 200px">MuatNaik Format Fail Excel : </td>
        <td>
            <asp:Button ID="btnFile" runat="server" Text="Excel" CssClass="fbbutton" Height="25px" Width="100px" /></td>
    </tr>
    <tr>
        <td>Pilih Fail Excel:</td>

        <td>
            <asp:FileUpload ID="FlUploadcsv" runat="server" />&nbsp;
           
            <asp:RegularExpressionValidator ID="regexValidator" runat="server" ErrorMessage="Only XLSX file are allowed"
                ValidationExpression="(.*\.([Xx][Ll][Ss][Xx])$)" ControlToValidate="FlUploadcsv"></asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
        <td>&nbsp</td>
    </tr>
    <tr>
        <td></td>
        <td colspan="2">
            <asp:Button ID="btnUpload" runat="server" Text="Muatnaik" CssClass="fbbutton" Style="height: 26px; width: 100px" />
        </td>
    </tr>
</table>
<div class="info" id="divMsg" runat="server">
    <asp:Label ID="lblMsg" runat="server" Text="Mesej..."></asp:Label>
</div>



