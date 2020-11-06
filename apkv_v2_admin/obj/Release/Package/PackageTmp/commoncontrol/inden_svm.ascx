<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="inden_svm.ascx.vb" Inherits="apkv_v2_admin.inden_svm" %>
<table class="fbform" style="width: 100%">
    <tr class="fbform_header">
        <td colspan="3">Analisa SVM</td>
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
                            <asp:Label ID="Negeri" runat="server" Text='<%# Bind("Kod")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="1%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Negeri">
                        <ItemTemplate>
                            <asp:Label ID="BahasaMelayu" runat="server" Text='<%# Bind("Negeri")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Nama Pusat">
                        <ItemTemplate>
                            <asp:Label ID="BahasaInggeris" runat="server" Text='<%# Bind("Nama")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="20%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Kod Program">
                        <ItemTemplate>
                            <asp:Label ID="Mathematics" runat="server" Text='<%# Bind("KodKursus") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="8%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Kod Matapelajaran">
                        <ItemTemplate>
                            <asp:Label ID="Science" runat="server" Text='<%# Bind("KodMPVOK")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="12%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Nama Matapelajaran">
                        <ItemTemplate>
                            <asp:Label ID="lblNamaMP" runat="server" Text='<%# Bind("NamaMPVOK")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="30%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Bil. Calon">
                        <ItemTemplate>
                            <asp:Label ID="PendidikanIslam" runat="server" Text='<%# Bind("BILCALON")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Nearest 5">
                        <ItemTemplate>
                            <asp:Label ID="lblNearest5" runat="server" Text='<%# Bind("NEAR5")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Plus 5">
                        <ItemTemplate>
                            <asp:Label ID="lblPlus5" runat="server" Text='<%# Bind("PLUS5")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
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
