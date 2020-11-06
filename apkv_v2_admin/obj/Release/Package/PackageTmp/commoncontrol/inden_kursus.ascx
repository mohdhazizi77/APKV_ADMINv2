<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="inden_kursus.ascx.vb" Inherits="apkv_v2_admin.inden_kursus" %>
<table class="fbform" style="width: 100%">
    <tr class="fbform_header">
        <td colspan="3">Analisa Bilangan Calon Mengikut Kursus</td>
    </tr>
    <tr>
        <td style="width: 20%">Kohort:</td>
        <td colspan="2">
            <asp:DropDownList ID="ddlTahun" runat="server" Width="150px"></asp:DropDownList></td>
    </tr>

    <tr>
        <td style="width: 20%">Semester:</td>
        <td colspan="2">
            <asp:DropDownList ID="ddlSemester" runat="server" Width="150px"></asp:DropDownList></td>
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
        <td style="width: 20%"></td>
        <td colspan="2">
            <asp:Button ID="btnSearch" runat="server" Text="Jana Analisa" CssClass="fbbutton" /></td>
    </tr>

</table>
<br />
<table class="fbform" style="width: 100%">
    <tr class="fbform_header">
        <td>Senarai Analisa</td>
    </tr>
    <tr>
        <td>
            <asp:GridView ID="datRespondent" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                CellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames="Kod"
                Width="100%" PageSize="150" CssClass="gridview_footer" EnableModelValidation="True">
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <Columns>

                    <asp:TemplateField HeaderText="Kod">
                        <ItemTemplate>
                            <asp:Label ID="lblKod" runat="server" Text='<%# Bind("Kod")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Negeri">
                        <ItemTemplate>
                            <asp:Label ID="lblNegeri" runat="server" Text='<%# Bind("Negeri")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="10%" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Nama">
                        <ItemTemplate>
                            <asp:Label ID="lblNama" runat="server" Text='<%# Bind("Nama")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="20%" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Mata Pelajaran Vokasional">
                        <ItemTemplate>
                            <asp:Label ID="lblKodMPVOK" runat="server" Text='<%# Bind("KodMPVOK")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Kod Kursus">
                        <ItemTemplate>
                            <asp:Label ID="lblKodKursus" runat="server" Text='<%# Bind("KodKursus")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Tahun">
                        <ItemTemplate>
                            <asp:Label ID="lblTahun" runat="server" Text='<%# Bind("Tahun")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Sesi">
                        <ItemTemplate>
                            <asp:Label ID="lblSesi" runat="server" Text='<%# Bind("Sesi")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Bilangan Calon">
                        <ItemTemplate>
                            <asp:Label ID="lblBilangan" runat="server" Text='<%# Bind("BilanganCalon")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Jumlah Calon">
                        <ItemTemplate>
                            <asp:Label ID="lblJumlah" runat="server" Text='<%# Bind("BilanganCalon")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
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
