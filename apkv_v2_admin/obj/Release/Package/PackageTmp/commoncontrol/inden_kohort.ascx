<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="inden_kohort.ascx.vb" Inherits="apkv_v2_admin.inden_kohort" %>
<table class="fbform" style ="width :100%">
    <tr class="fbform_header">
        <td colspan="3">Analisa Bilangan Calon Mengikut Negeri dan Kohort Semester</td>
    </tr>
    <tr>
           <td style ="width :20%">Tahun:</td>
           <td colspan ="2"><asp:DropDownList ID="ddlTahun" runat="server" Width="150px"></asp:DropDownList></td>
    </tr>
   
    <tr>
        <td style="width: 20%">Sesi:</td>
        <td colspan="2">
            <asp:CheckBoxList ID="chkSesi" runat="server" Width="200px" RepeatDirection="Horizontal">
                <asp:ListItem>1</asp:ListItem>
                <asp:ListItem>2</asp:ListItem>
            </asp:CheckBoxList>
        </td>
    </tr>

    <tr>
           <td style ="width :20%"></td>
           <td colspan ="2"><asp:Button ID="btnSearch" runat="server" Text="Jana Analisa" CssClass="fbbutton" /></td>
    </tr>
    
</table>
<br />
<table class="fbform" style ="width :100%">
    <tr class="fbform_header">
        <td>Senarai Analisa</td>
    </tr>
    <tr>
        <td>
            <asp:GridView ID="datRespondent" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                CellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames="Negeri"
                Width="100%" PageSize="1000" CssClass="gridview_footer" EnableModelValidation="True">
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <Columns>
                    
                     <asp:TemplateField HeaderText="Negeri">
                        <ItemTemplate>
                            <asp:Label ID="lblNegeri" runat="server" Text='<%# Bind("Negeri")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left"  VerticalAlign="Top"  />
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width ="25%" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Kohort">
                        <ItemTemplate>
                            <asp:Label ID="lblsem1" runat="server" Text='<%# Bind("Semester1Sesi1")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center"  VerticalAlign="Top"  />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width ="25%" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Kohort">
                        <ItemTemplate>
                            <asp:Label ID="lblsem3" runat="server" Text='<%# Bind("Semester3Sesi1")%>'></asp:Label>
                        </ItemTemplate>
                         <HeaderStyle HorizontalAlign="Center"  VerticalAlign="Top"  />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width ="25%" />
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
            <asp:Button ID="btnExport" runat="server" Text="Eksport" CssClass="fbbutton" Visible="true" />
        </td>
    </tr>

    </table>
<br />
<div class="info" id="divMsg" runat="server">
<asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
</div>
