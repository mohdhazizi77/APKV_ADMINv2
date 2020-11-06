<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="pemeriksa_markah_import.ascx.vb" Inherits="apkv_v2_admin.pemeriksa_markah_import" %>
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Kemasukan Markah Pelajar
        </td>
    </tr>
    <tr>
        <td>Kohort:</td>
        <td>
            <asp:DropDownList ID="ddlTahun" runat="server"  Width="200px">
            </asp:DropDownList></td>

    </tr>
       <tr>
          <td >Sesi Pengambilan:</td>
        <td><asp:CheckBoxList ID="chkSesi" runat="server"  AutoPostBack="true" Width="349px" RepeatDirection="Horizontal">
             <asp:ListItem>1</asp:ListItem>
             <asp:ListItem>2</asp:ListItem>
             </asp:CheckBoxList></td>
    </tr>
    <tr>
        <td>Semester:</td>
        <td>
            <asp:DropDownList ID="ddlSemester" runat="server"  Width="200px">
            </asp:DropDownList></td>
    </tr>
      <tr>
          <td >Kod Program:</td>
        <td>
            <asp:DropDownList ID="ddlKodKursus" runat="server" Width="350px"></asp:DropDownList>
        </td>
    </tr>
      <tr>
          <td >&nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
     <tr>
         <td colspan ="2">MuatNaik Format Fail Excel:
         <asp:Button ID="btnFile" runat="server" Text="Excel" CssClass="fbbutton" Height="25px" Width="116px" /></td>
    </tr>
    <tr>
        <td>Pilih Fail Excel:
        </td>
        <td>
            <asp:FileUpload ID="FlUploadcsv" runat="server" />&nbsp;
            <asp:RegularExpressionValidator ID="regexValidator" runat="server" ErrorMessage="Only XLSX file are allowed"
                ValidationExpression="(.*\.([Xx][Ll][Ss][Xx])$)" ControlToValidate="FlUploadcsv"></asp:RegularExpressionValidator></td>
    </tr>

    <tr>
        <td class="fbform_sap" colspan="2">&nbsp;
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:Button ID="btnUpload" runat="server" Text="Muatnaik " CssClass="fbbutton" Style="height: 26px" />&nbsp;<asp:Label
                ID="lblMsgTop" runat="server" Text="" ForeColor="Blue"></asp:Label>
        </td>
    </tr>
</table>
<br />
<table class="fbform">
    <tr>
        <td class="fbform_sap">&nbsp;
        </td>
    </tr>
    <tr>
        <td>
            |<asp:LinkButton
                    ID="lnkList" runat="server">Senarai Markah</asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td>Upload Code:<asp:Label ID="lblUploadCode" runat="server" Text=""></asp:Label>
        </td>
    </tr>
</table>
<div class="info" id="divMsg" runat="server">
    <asp:Label ID="lblMsg" runat="server" Text="Mesej..."></asp:Label>
</div>
&nbsp; 