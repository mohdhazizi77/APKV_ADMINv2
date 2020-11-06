<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="test.jana.pngs.ascx.vb" Inherits="apkv_v2_admin.test_jana_pngs1" %>

<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Jana Keseluruhan PNGS</td>
    </tr>
</table>
<br />
<table class="fbform" style ="width :100%">
    <tr>
        <td style="width: 20%;">Negeri:</td>
        <td><asp:DropDownList ID="ddlNegeri" runat="server" Width="350px" AutoPostBack="true"></asp:DropDownList></td>
    </tr>
    <tr>
        <td>Jenis Kolej:</td>
        <td><asp:DropDownList ID="ddlJenis" runat="server" AutoPostBack="true"  Width="350px"></asp:DropDownList>
    </tr>
    <tr>
        <td>Nama Kolej:</td>
        <td><asp:DropDownList ID="ddlKolej" runat="server" AutoPostBack="false" Width="350px"></asp:DropDownList></td>
    </tr>
    <tr>
        <td colspan ="2"> &nbsp;</td>
    </tr>
    </table>
<br />
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2"></td>
    </tr>
    <tr>
          <td >Kohort:</td>
        <td>
            <asp:DropDownList ID="ddlTahun" runat="server" AutoPostBack="false" Width="350px"></asp:DropDownList>
        </td>
    </tr>
     <tr>
          <td >Semester:</td>
        <td>
            <asp:DropDownList ID="ddlSemester" runat="server" AutoPostBack="false" Width="350px"></asp:DropDownList>
        </td>
    </tr>
     <tr>
        <td >Sesi Pengambilan:</td>
        <td><asp:CheckBoxList ID="chkSesi" runat="server"  AutoPostBack="true" Width="349px" RepeatDirection="Horizontal">
             <asp:ListItem>1</asp:ListItem>
             <asp:ListItem>2</asp:ListItem>
             </asp:CheckBoxList>
        </td>
    </tr>
   <tr>
        <td >Kod Program:</td>
        <td>
            <asp:DropDownList ID="ddlKodKursus" runat="server" Width="350px"></asp:DropDownList>
        </td>
    </tr>
      <tr>
        <td colspan="2"><asp:Button ID="btnCari" runat="server" Text="Cari" CssClass="fbbutton" />&nbsp;</td>
      </tr>
</table> 
<br />

<table class="fbform">
    <tr class="fbform_header">
        <td>Senarai Program Kolej
        </td>
    </tr>
    <tr>
       <td>
            <asp:GridView ID="datRespondent" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                CellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames="KursusID"
                Width="100%" PageSize="100" CssClass="gridview_footer">
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <Columns>
                    <asp:TemplateField HeaderText="#">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="KodKursus">
                        <ItemTemplate>
                            <asp:Label ID="lblKod" runat="server" Text='<%# Bind("KodKursus")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                  
                  <asp:TemplateField >
                         <HeaderTemplate >
                                <asp:CheckBox ID="chkAll" runat="server" AutoPostBack="true" OnCheckedChanged="CheckUncheckAll" />
                            </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="chkSelect"  runat="server" />
                        </ItemTemplate>
                         <HeaderStyle HorizontalAlign="Left"/>
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField> 
                     
                    </Columns>
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Font-Underline="true" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" CssClass="cssPager" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" VerticalAlign="Middle"
                    HorizontalAlign="Left" />
                <EditRowStyle BackColor="#999999" />
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            </asp:GridView>
        </td>
    </tr>
    <tr>
        <td>
          <asp:Button ID="btnGred" runat="server" Text="Jana PNGS" CssClass="fbbutton" Visible="true" />
        </td>
    </tr>
</table>

<div class="info" id="divMsg" runat="server">
  <asp:Label ID="lblMsg" runat="server" Text="Mesej..."></asp:Label>
</div>
 
