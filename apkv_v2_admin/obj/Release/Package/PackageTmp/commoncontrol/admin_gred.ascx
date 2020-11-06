<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="admin_gred.ascx.vb" Inherits="apkv_v2_admin.admin_gred" %>
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Carian </td>
    </tr>
    <tr>
        <td>Kohort:</td>
        <td>
            <asp:DropDownList ID="ddlTahun" runat="server" AutoPostBack="true" Width="350px"></asp:DropDownList>
        </td>
    </tr>

    <tr>
        <td>
            Negeri: 
        </td>
        <td>
            <asp:DropDownList ID="ddlNegeri" runat="server" AutoPostBack="true" Width="350px"></asp:DropDownList>
        </td>
    </tr>

    <tr>
        <td>
            Kolej: 
        </td>
        <td>
            <asp:DropDownList ID="ddlKolej" runat="server" AutoPostBack="true" Width="350px"></asp:DropDownList>
        </td>
    </tr>

    <tr>
        <td style="width: 20%;">Sesi:</td>
        <td>
            <asp:CheckBoxList ID="chkSesi" runat="server" AutoPostBack="true" Width="349px" RepeatDirection="Horizontal">
                <asp:ListItem Selected="True">1</asp:ListItem>
                <asp:ListItem>2</asp:ListItem>
            </asp:CheckBoxList>
    </tr>
    <tr>
        <td>Semester:</td>
        <td>
            <asp:DropDownList ID="ddlSemester" runat="server" AutoPostBack="false" Width="350px"></asp:DropDownList></td>
    </tr>
    <tr>
        <td>
            <asp:Button ID="btnSearch" runat="server" Text="Cari " CssClass="fbbutton" /></td>
    </tr>
</table>

<br />

<table class="fbform">
    <tr class="fbform_header">
        <td>Senarai Kolej Vokasional yang BELUM MEMBUAT PENGESAHAN Markah Pentaksiran Berterusan Akademik (PBA).</td>
    </tr>
    <tr>
        <td>
            <asp:GridView ID="datRespondentPBA" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                CellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames="KolejRecordID"
                Width="100%" PageSize="5" CssClass="gridview_footer">
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <Columns>
                    <asp:TemplateField HeaderText="#">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>         
                   
                    <asp:TemplateField HeaderText="Kolej">
                        <ItemTemplate>
                            <asp:Label ID="lblKolej" runat="server" Text='<%# Bind("Nama")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                        <HeaderStyle Width="30%" />
                        <ItemStyle Width="30%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Kod Program">
                        <ItemTemplate>
                            <asp:Label ID="lblKodKursus" runat="server" Text='<%# Bind("KodKursus")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Nama Program">
                        <ItemTemplate>
                            <asp:Label ID="lblNamaprogram" runat="server" Text='<%# Bind("NamaKursus")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Kohort">
                        <ItemTemplate>
                            <asp:Label ID="lblKohort" runat="server" Text='<%# Bind("Tahun")%>'></asp:Label>
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
                    <asp:TemplateField HeaderText="Bil. Calon Keseluruhan">
                        <ItemTemplate>
                            <asp:Label ID="lbltotalStudent" runat="server" Width="30px" Text='<%# Bind("totalStudent")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Belum Pengesahan (PBA)" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblPBA" runat="server" Width="30px" Text='<%# Bind("sahPBA")%>'></asp:Label>
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
    <tr style="text-align: center">
        <td colspan="2">
            <asp:Button ID="btnExportPBA" runat="server" Text="Eksport " CssClass="fbbutton" Style="height: 26px" />
        </td>
    </tr>
</table>

<br />

<div class="info" id="divMsgPBA" runat="server">
    <asp:Label ID="lblMsgPBA" runat="server" Text="Mesej..."></asp:Label>
</div>

<br />



<table class="fbform">
    <tr class="fbform_header">
        <td>Senarai Kolej Vokasional yang BELUM MEMBUAT PENGESAHAN Markah Pentaksiran Berterusan Vokasional (PBV).</td>
    </tr>
    <tr>
        <td>
            <asp:GridView ID="datRespondentPBV" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                CellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames="KolejRecordID"
                Width="100%" PageSize="5" CssClass="gridview_footer">
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <Columns>
                    <asp:TemplateField HeaderText="#">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>         
                   
                    <asp:TemplateField HeaderText="Kolej">
                        <ItemTemplate>
                            <asp:Label ID="lblKolej" runat="server" Text='<%# Bind("Nama")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                        <HeaderStyle Width="30%" />
                        <ItemStyle Width="30%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Kod Program">
                        <ItemTemplate>
                            <asp:Label ID="lblKodKursus" runat="server" Text='<%# Bind("KodKursus")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Nama Program">
                        <ItemTemplate>
                            <asp:Label ID="lblNamaprogram" runat="server" Text='<%# Bind("NamaKursus")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Kohort">
                        <ItemTemplate>
                            <asp:Label ID="lblKohort" runat="server" Text='<%# Bind("Tahun")%>'></asp:Label>
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
                    <asp:TemplateField HeaderText="Bil. Calon Keseluruhan">
                        <ItemTemplate>
                            <asp:Label ID="lbltotalStudent" runat="server" Width="30px" Text='<%# Bind("totalStudent")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Belum Pengesahan (PBV)" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblPBV" runat="server" Width="30px" Text='<%# Bind("sahPBV")%>'></asp:Label>
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
    <tr style="text-align: center">
        <td colspan="2">
            <asp:Button ID="btnExportPBV" runat="server" Text="Eksport " CssClass="fbbutton" Style="height: 26px" />
        </td>
    </tr>
</table>

<br />

<div class="info" id="divMsgPBV" runat="server">
    <asp:Label ID="lblMsgPBV" runat="server" Text="Mesej..."></asp:Label>
</div>

<br />

<table class="fbform">
    <tr class="fbform_header">
        <td>Senarai Kolej Vokasional yang BELUM MEMBUAT PENGESAHAN Markah Pentaksiran Akhir Akademik (PAA).</td>
    </tr>
    <tr>
        <td>
            <asp:GridView ID="datRespondentPAA" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                CellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames="KolejRecordID"
                Width="100%" PageSize="5" CssClass="gridview_footer">
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <Columns>
                    <asp:TemplateField HeaderText="#">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>         
                   
                    <asp:TemplateField HeaderText="Kolej">
                        <ItemTemplate>
                            <asp:Label ID="lblKolej" runat="server" Text='<%# Bind("Nama")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                        <HeaderStyle Width="30%" />
                        <ItemStyle Width="30%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Kod Program">
                        <ItemTemplate>
                            <asp:Label ID="lblKodKursus" runat="server" Text='<%# Bind("KodKursus")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Nama Program">
                        <ItemTemplate>
                            <asp:Label ID="lblNamaprogram" runat="server" Text='<%# Bind("NamaKursus")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Kohort">
                        <ItemTemplate>
                            <asp:Label ID="lblKohort" runat="server" Text='<%# Bind("Tahun")%>'></asp:Label>
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
                    <asp:TemplateField HeaderText="Bil. Calon Keseluruhan">
                        <ItemTemplate>
                            <asp:Label ID="lbltotalStudent" runat="server" Width="30px" Text='<%# Bind("totalStudent")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Belum Pengesahan (PAA)" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblPAA" runat="server" Width="30px" Text='<%# Bind("sahPAA")%>'></asp:Label>
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
    <tr style="text-align: center">
        <td colspan="2">
            <asp:Button ID="btnExportPAA" runat="server" Text="Eksport " CssClass="fbbutton" Style="height: 26px" />
        </td>
    </tr>
</table>

<br />

<div class="info" id="divMsgPAA" runat="server">
    <asp:Label ID="lblMsgPAA" runat="server" Text="Mesej..."></asp:Label>
</div>

<br />

<table class="fbform">
    <tr class="fbform_header">
        <td>Senarai Kolej Vokasional yang BELUM MEMBUAT PENGESAHAN Markah Pentaksiran Akhir Amali Vokasional (PAV).</td>
    </tr>
    <tr>
        <td>
            <asp:GridView ID="datRespondentPAV" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                CellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames="KolejRecordID"
                Width="100%" PageSize="5" CssClass="gridview_footer">
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <Columns>
                    <asp:TemplateField HeaderText="#">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>         
                   
                    <asp:TemplateField HeaderText="Kolej">
                        <ItemTemplate>
                            <asp:Label ID="lblKolej" runat="server" Text='<%# Bind("Nama")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                        <HeaderStyle Width="30%" />
                        <ItemStyle Width="30%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Kod Program">
                        <ItemTemplate>
                            <asp:Label ID="lblKodKursus" runat="server" Text='<%# Bind("KodKursus")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Nama Program">
                        <ItemTemplate>
                            <asp:Label ID="lblNamaprogram" runat="server" Text='<%# Bind("NamaKursus")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Kohort">
                        <ItemTemplate>
                            <asp:Label ID="lblKohort" runat="server" Text='<%# Bind("Tahun")%>'></asp:Label>
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
                    <asp:TemplateField HeaderText="Bil. Calon Keseluruhan">
                        <ItemTemplate>
                            <asp:Label ID="lbltotalStudent" runat="server" Width="30px" Text='<%# Bind("totalStudent")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Belum Pengesahan (PAV)" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblPAV" runat="server" Width="30px" Text='<%# Bind("sahPAV")%>'></asp:Label>
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
    <tr style="text-align: center">
        <td colspan="2">
            <asp:Button ID="btnExportPAV" runat="server" Text="Eksport " CssClass="fbbutton" Style="height: 26px" />
        </td>
    </tr>
</table>

<br />

<div class="info" id="divMsgPAV" runat="server">
    <asp:Label ID="lblMsgPAV" runat="server" Text="Mesej..."></asp:Label>
</div>
