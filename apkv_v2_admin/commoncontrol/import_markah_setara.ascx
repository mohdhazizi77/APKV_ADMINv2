<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="import_markah_setara.ascx.vb" Inherits="apkv_v2_admin.import_markah_setara" %>

<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Akademik >> Import Markah Setara >> </td>
    </tr>
</table>
<br />

<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Tetapan Import Markah Setara</td>
    </tr>
    <tr>
        <td>Tahun Peperiksaan:</td>
        <td>
            <asp:DropDownList ID="ddlTahunPeperiksaan" runat="server" AutoPostBack="false" Width="350px"></asp:DropDownList>
        </td>
    </tr>

    <tr>
        <td>Matapelajaran : </td>
        <td>
            <asp:RadioButtonList ID="radioMP" runat="server" RepeatDirection="Horizontal" AutoPostBack="true">
                <asp:ListItem Value="BM1">Bahasa Melayu K1</asp:ListItem>
                <asp:ListItem Value="BM2">Bahasa Melayu K2</asp:ListItem>
                <asp:ListItem Value="SJ">Sejarah</asp:ListItem>
            </asp:RadioButtonList></td>
                
    </tr>
</table>
<br />
<div id="divImport" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td colspan="2">Import Markah Setara</td>
        </tr>
       
        <tr>
            <td>Pilih Fail Excel:
            </td>
            <td>
                <asp:FileUpload ID="FlUploadcsv" runat="server" />&nbsp;
            <asp:RegularExpressionValidator ID="regexValidator" runat="server" ErrorMessage="Only CSV file are allowed"
                ValidationExpression="(.*\.([Cc][Ss][Vv])$)" ControlToValidate="FlUploadcsv"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Button ID="btnUpload" runat="server" Text="Muatnaik " CssClass="fbbutton" Style="height: 26px" />
            </td>
        </tr>
    </table>
</div>

<div class="info" id="divMsg" runat="server">
    <asp:Label ID="lblMsg" runat="server" Text="Mesej..."></asp:Label>

</div>
