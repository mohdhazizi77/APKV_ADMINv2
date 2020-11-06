<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="jana_FIK_semasa.ascx.vb" Inherits="apkv_v2_admin.jana_FIK_semasa1" %>

<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">
        Jana >> Jana FIK Semasa
</table>

<br />

<table class="fbform">

    <tr class="fbform_header">
        <td colspan="2"></td>
    </tr>
    <tr>
        <td>Kohort:</td>
        <td>
            <asp:DropDownList ID="ddlTahun" runat="server" AutoPostBack="false" Width="350px"></asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>Semester:</td>
        <td>
            <asp:DropDownList ID="ddlSemester" runat="server" AutoPostBack="false" Width="350px"></asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>Sesi:</td>
        <td>
            <asp:CheckBoxList ID="chkSesi" runat="server" AutoPostBack="true" Width="349px" RepeatDirection="Horizontal">
                <asp:ListItem>1</asp:ListItem>
                <asp:ListItem>2</asp:ListItem>
            </asp:CheckBoxList></td>
    </tr>

    <tr>
        <td colspan="2">
            <asp:Button ID="btnCari" runat="server" Text="Eksport" CssClass="fbbutton" />&nbsp;</td>
    </tr>

</table>

<br />

<table>
    <tr>
        <td>
            <asp:GridView ID="datRespondent" runat="server" AutoGenerateColumns="False" AllowPaging="false" Visible="false"
                CellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames="PelajarID"
                Width="100%" PageSize="100" CssClass="gridview_footer">
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <Columns>
                    <asp:TemplateField HeaderText="BIL">
                        <ItemTemplate>
                            <asp:Label ID="BIL" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="PELAJARID">
                        <ItemTemplate>
                            <asp:Label ID="PELAJARID" runat="server" Text='<%# Bind("PelajarID")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="NAMA">
                        <ItemTemplate>
                            <asp:Label ID="NAMA" runat="server" Text='<%# Bind("Nama")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="MYKAD">
                        <ItemTemplate>
                            <asp:Label ID="MYKAD" runat="server" Text='<%# Bind("MYKAD")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="ANGKAGILIRAN">
                        <ItemTemplate>
                            <asp:Label ID="ANGKAGILIRAN" runat="server" Text='<%# Bind("AngkaGiliran")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="KODKURSUS">
                        <ItemTemplate>
                            <asp:Label ID="KODKURSUS" runat="server" Text='<%# Bind("KodKursus")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="SEMESTER">
                        <ItemTemplate>
                            <asp:Label ID="SEMESTER" runat="server" Text='<%# Bind("Semester")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="NEGERI">
                        <ItemTemplate>
                            <asp:Label ID="NEGERI" runat="server" Text='<%# Bind("Negeri")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="STATUS">
                        <ItemTemplate>
                            <asp:Label ID="STATUS" runat="server" Text='<%# Bind("Status")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="JANTINA">
                        <ItemTemplate>
                            <asp:Label ID="JANTINA" runat="server" Text='<%# Bind("Jantina")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="KAUM">
                        <ItemTemplate>
                            <asp:Label ID="KAUM" runat="server" Text='<%# Bind("Kaum")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="AGAMA">
                        <ItemTemplate>
                            <asp:Label ID="AGAMA" runat="server" Text='<%# Bind("Agama")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="KOLEJVOKASIONAL">
                        <ItemTemplate>
                            <asp:Label ID="KOLEJVOKASIONAL" runat="server" Text='<%# Bind("KolejVokasional")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="KOD">
                        <ItemTemplate>
                            <asp:Label ID="KOD" runat="server" Text='<%# Bind("Kod")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="SEM1">
                        <ItemTemplate>
                            <asp:Label ID="SEM1" runat="server"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
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
</table>

<br />

<div class="info" id="divMsg" runat="server">

    <asp:Label ID="lblMsg" runat="server" Text="System message..."></asp:Label>

</div>



