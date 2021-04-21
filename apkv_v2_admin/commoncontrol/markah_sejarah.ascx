<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="markah_sejarah.ascx.vb" Inherits="apkv_v2_admin.markah_sejarah1" %>
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Akademik >>Pemeriksa >> Kemasukkan Markah PA Sejarah</td>
    </tr>
</table>
<br />
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="3">Maklumat Kolej</td>
    </tr>
    <tr>
        <td style ="width :10%">Tahun Peperiksaan</td>
        <td style ="width :2%">:</td>
        <td><asp:DropDownList ID="ddlTahun" runat="server" AutoPostBack="true" Width="150px"></asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td >Kod Pusat</td>
        <td>:</td>
        <td><asp:DropDownList ID="ddlKodPusat" runat="server" AutoPostBack="true" Width="150px"></asp:DropDownList>
        </td>
    </tr>
    <%--<tr>
        <td >Kod Kursus</td>
        <td>:</td>
        <td><asp:DropDownList ID="ddlKodKursus" runat="server" AutoPostBack="False" Width="150px"></asp:DropDownList>
        </td>
    </tr>--%>
    <tr>
        <td>Semester</td>
        <td>:</td>
        <td><asp:DropDownList ID="ddlSemester" runat="server" AutoPostBack="false" Width="150px"></asp:DropDownList>
        </td>
    </tr>
     <tr>
         <td>Sesi</td>
         <td>:</td>
          <td><asp:CheckBoxList ID="chkSesi" runat="server" AutoPostBack="true" Width="250px" RepeatDirection="Horizontal">
             <asp:ListItem>1</asp:ListItem>
             <asp:ListItem>2</asp:ListItem>
             </asp:CheckBoxList>
    </tr>
    <tr>
        <td colspan="3"><asp:Button ID="btnCari" runat="server" Text="Cari" CssClass="fbbutton" />&nbsp;</td>
           </tr>
   </table> 
<div class="info" id="divMsgResult" runat="server">
  <asp:Label ID="lblMsgResult" runat="server" Text="Mesej..."></asp:Label>
</div>
<table class="fbform">
    <tr class="fbform_header">
        <td>Senarai Calon
        </td>
    </tr>
    <tr>
       <td>
            <asp:GridView ID="datRespondent" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                CellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames="PemeriksaID"
                Width="100%" PageSize="12" CssClass="gridview_footer">
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <Columns>
                    <asp:TemplateField HeaderText="#">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Kohort">
                        <ItemTemplate>
                            <asp:Label ID="lblTahun" runat="server" Text='<%# Bind("Tahun")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                   
                    <asp:TemplateField HeaderText="Sesi">
                        <ItemTemplate>
                            <asp:Label ID="lblSesi" runat="server" Text='<%# Bind("Sesi")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Semester">
                        <ItemTemplate>
                            <asp:Label ID="lblSemester" runat="server" Text='<%# Bind("Semester")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                     <asp:TemplateField HeaderText="Pilih"> 
                        <ItemTemplate> 
                            <asp:Button ID="btnPilih" runat="server" Text="Pilih" CommandName="Pilih" CommandArgument ='<%#Eval("PemeriksaID")%>'/></td>
                        </ItemTemplate> 
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
            &nbsp;</td>
    </tr>
</table>
<br />
<div class="info" id="divMsg" runat="server">
<asp:Label ID="lblUserId" runat="server" Text="" Visible ="false"></asp:Label>
<asp:Label ID="lblUserType" runat="server" Text="" Visible ="false"></asp:Label>

<asp:Label ID="lblMsg" runat="server" Text="System message..."></asp:Label>
</div>