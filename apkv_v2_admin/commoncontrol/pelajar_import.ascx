<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="pelajar_import.ascx.vb" Inherits="apkv_v2_admin.pelajar_import" %>
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Maklumat Kolej</td>
    </tr>
    <tr>
        <td style="width:200px" >Negeri :</td>
        <td><asp:DropDownList ID="ddlNegeri" runat="server" Width="350px"></asp:DropDownList></td>
    </tr>
    <tr>
        <td>Jenis Kolej :</td>
        <td><asp:DropDownList ID="ddlJenis" runat="server" AutoPostBack="true"  Width="350px"></asp:DropDownList>
    </tr>
    <tr>
        <td>Nama Kolej :</td>
        <td><asp:DropDownList ID="ddlKolej" runat="server" AutoPostBack="true" Width="350px"></asp:DropDownList></td>
    </tr>
    </table>
<br />
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Import Senarai Calon
        </td>
    </tr>
    <tr>
         <td style="width:200px">MuatNaik Format Fail Excel : </td>
             <td>
         <asp:Button ID="btnFile" runat="server" Text="Excel" CssClass="fbbutton" Height="25px" Width="100px" /></td>
    </tr>
    <tr>
         <td>Pilih Fail Excel:
         <td>
            <asp:FileUpload ID="FlUploadcsv" runat="server" />&nbsp;
            <asp:RegularExpressionValidator ID="regexValidator" runat="server" ErrorMessage="Only XLSX file are allowed"
                ValidationExpression="(.*\.([Xx][Ll][Ss][Xx])$)" ControlToValidate="FlUploadcsv"></asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr><td>&nbsp</td></tr>
    <tr>
        <td></td>
        <td colspan="2">
            <asp:Button ID="btnUpload" runat="server" Text="Muatnaik"  CssClass="fbbutton" Style="height: 26px; Width:100px" />
        </td>
    </tr>
    </table>


<div class="info" id="divMsg" runat="server">
    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
</div>
