<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="admin_skor_vokasional.ascx.vb" Inherits="apkv_v2_admin.admin_skor_vokasional" %>
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Konfigurasi >> Skor Pemarkahan Vokasional >> Pendaftaran Baru</td>
    </tr>
</table>
<br />
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Paparan Maklumat Skor Pemarkahan Vokasional</td>
    </tr>
    <tr>
          <td>Kohort:</td>
          <td><asp:DropDownList ID="ddlTahun" runat="server" Width="350px"></asp:DropDownList></td>
    </tr>
     <tr>
          <td>Sesi Pengambilan:</td>
          <td><asp:CheckBoxList ID="chkSesi" runat="server" Width="349px" RepeatDirection="Horizontal">
             <asp:ListItem>1</asp:ListItem>
             <asp:ListItem>2</asp:ListItem>
             </asp:CheckBoxList></td>
    </tr>
      <tr>
          <td >Semester:</td>
        <td>
            <asp:DropDownList ID="ddlSemester" runat="server" Width="350px"></asp:DropDownList>
        </td>
    </tr>
      <tr>
         <td >Penskoran PB:</td>
         <td style="color: #FF0000; font-size: small; font-family: Arial, Helvetica, sans-serif;"><asp:TextBox ID="txtPB" runat="server" Width="350px" MaxLength="50"></asp:TextBox>*[1-100]</td>
    <tr>
         <td>Penskoran PA Amali:</td>
         <td style="color: #FF0000; font-family: Arial, Helvetica, sans-serif; font-size: small;"><asp:TextBox ID="txtPAA" runat="server" Width="350px" MaxLength="50"></asp:TextBox>*[1-100]</td>
    </tr>
    <tr>
           <td >Penskoran PA Teori:</td>
           <td style="color: #FF0000; font-family: Arial, Helvetica, sans-serif; font-size: small;"><asp:TextBox ID="txtPAT" runat="server" Width="350px" MaxLength="50"></asp:TextBox>*[1-100]</td>
   </tr>
    <tr>
           <td >&nbsp;</td>
           <td>&nbsp;</td>
   </tr>
    <tr>
        <td></td>
        <td><asp:Button ID="btnDaftar" runat="server" Text="Daftar Baru" CssClass="fbbutton" /> &nbsp;</td>
    </tr>
   
</table>
<br />
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Senarai Skor Pemarkahan Vokasional</td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:GridView ID="datRespondent" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                CellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames="SkorID"
                Width="100%" PageSize="50" CssClass="gridview_footer">
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333"/>
                <Columns>
                    <asp:TemplateField HeaderText="#">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top"/>
                        <ItemStyle VerticalAlign="Middle"/>
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="Tahun">
                        <ItemTemplate>
                            <asp:Label ID="Tahun" runat="server" Text='<%# Bind("Tahun")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" /><ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="Sesi">
                        <ItemTemplate>
                            <asp:Label ID="Sesi" runat="server" Text='<%# Bind("Sesi")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" /><ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="Semester">
                        <ItemTemplate>
                            <asp:Label ID="Semester" runat="server" Text='<%# Bind("Semester")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" /><ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="SMP_PB">
                        <ItemTemplate>
                            <asp:Label ID="SMP_PB" runat="server" Text='<%# Bind("SMP_PB")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" /><ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="SMP_PAA">
                        <ItemTemplate>
                            <asp:Label ID="SMP_PAA" runat="server" Text='<%# Bind("SMP_PAA")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" /><ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="SMP_PAT">
                        <ItemTemplate>
                            <asp:Label ID="SMP_PAT" runat="server" Text='<%# Bind("SMP_PAT")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" /><ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="Padam">
                        <ItemTemplate>
                            <asp:ImageButton Width ="12" Height ="12" ID="btnDelete" CommandName="Delete" OnClientClick="javascript:return confirm('Anda pasti untuk padam rekod ini? Pemadaman yang dilakukan tidak boleh diubah')" runat="server" ImageUrl="~/icons/download.jpg" ToolTip="Padam Rekod"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                    </Columns>
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Font-Underline="true" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" CssClass="cssPager" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" VerticalAlign="Middle"
                    HorizontalAlign="Left" />
                <EditRowStyle BackColor="#999999" />
                <AlternatingRowStyle BackColor="White" ForeColor="#284775"/>
            </asp:GridView>
        </td>
    </tr>
    
</table>
<div class="info" id="divMsg" runat="server">
 <asp:Label ID="lblMsg" runat="server" Text="Mesej..."></asp:Label>
    
</div>