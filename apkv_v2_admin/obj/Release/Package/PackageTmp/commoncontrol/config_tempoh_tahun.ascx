<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="config_tempoh_tahun.ascx.vb" Inherits="apkv_v2_admin.config_tempoh_tahun" %>
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Konfigurasi Umum >> Tempoh Tahun</td>
    </tr>
</table>
<br />
<table class="fbform" style ="width :100%">
    <tr class="fbform_header">
        <td colspan="3">Tempoh Tahun
        </td>
    </tr>
    <tr>
        <td style ="width :15%">Dari Tahun</td>
        <td style ="width :2px">:</td>
        <td><asp:TextBox ID="txtYearFrom" runat="server" Width="150px" MaxLength="4"></asp:TextBox></td>
    </tr>
    <tr>
        <td>Sehingga Tahun</td>
        <td>:</td>
        <td><asp:TextBox ID="txtYearEnd" runat="server" Width="150px" MaxLength="4"></asp:TextBox></td>
    </tr>
    <tr>
        <td colspan="3"><asp:Button ID="btnUpdate" runat="server" Text="Simpan" CssClass="fbbutton" />
        </td>
    </tr>
</table>
<br />
<table class="fbform" style ="width :100%">
    <tr class="fbform_header">
        <td colspan="2">Senarai Parameter</td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:GridView ID="datRespondent" runat="server" AutoGenerateColumns="False" AllowPaging="False"
                CellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames="ID"
                Width="100%" PageSize="40" CssClass="gridview_footer">
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <Columns>
                    <asp:TemplateField HeaderText="#">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Dari Tahun">
                        <ItemTemplate>
                            <asp:Label ID="YearFrom" runat="server" Text='<%# Bind("YearFrom")%>'></asp:Label>
                        </ItemTemplate>
                     <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" /><ItemStyle VerticalAlign="Middle" />
              </asp:TemplateField>
                      <asp:TemplateField HeaderText="Sehingga Tahun">
                        <ItemTemplate>
                            <asp:Label ID="YearEnd" runat="server" Text='<%# Bind("YearEnd")%>'></asp:Label>
                        </ItemTemplate>
                     <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" /><ItemStyle VerticalAlign="Middle" />
              </asp:TemplateField>
                     <asp:TemplateField>
                        <ItemTemplate>
                      <%--      ADD THE DELETE LINK BUTTON---%>
                            <asp:LinkButton runat="server" OnClientClick="return confirm('Anda pasti untuk Batal?');"
                                CommandName="DELETE">Batal</asp:LinkButton>
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
        <td class="fbform_sap" colspan="5">&nbsp;</td>
    </tr>
    
   </table>
<div class="info" id="divMsg" runat="server">
 <asp:Label ID="lblMsg" runat="server" Text="Mesej..."></asp:Label>
    <asp:Label ID="lblTahun" runat="server" Text="" Visible="false"></asp:Label>
    
</div>