<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="config_tetapan_list.ascx.vb" Inherits="apkv_v2_admin.config_tetapan_list1" %>

<table class="fbform">

    <tr class="fbform_header">

        <td colspan="3">Konfigurasi Umum >> Tetapan</td>

    </tr>

</table>

<br />

<table class="fbform">

    <tr class="fbform_header">

        <td colspan="3">Tetapan</td>

    </tr>

    <tr>

        <td style="width: 100px;">Nama Config</td>
        <td style="width: 100px;">
            <asp:TextBox ID="txtCari" runat="server" Width="200px"></asp:TextBox>
        </td>
        <td>
            <asp:Button ID="btnSearch" runat="server" Text="Cari" CssClass="fbbutton" />
        </td>
    </tr>

    <tr>

        <td style="width: 100px;"></td>
        <td style="width: 100px;"></td>
        <td>
            <asp:Button ID="btnDaftar" runat="server" Text="Daftar Baru" CssClass="fbbutton" />
        </td>

    </tr>

</table>

<br />

<table class="fbform">

    <tr class="fbform_header">

        <td colspan="2">Senarai Tetapan

        </td>

    </tr>
    <tr>

        <td colspan="2">

            <asp:GridView ID="datRespondent" runat="server" DataKeyNames="configID"
                AutoGenerateColumns="false" AllowPaging="true" CellPadding="4" ForeColor="#333333"
                GridLines="None" Width="100%" PageSize="30" CssClass="gridview_footer">

                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />

                <Columns>

                    <asp:TemplateField HeaderText="#">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Config Name">
                        <ItemTemplate>
                            <asp:Label ID="lblConfigName" runat="server" Text='<%# Bind("configName")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Config Value">
                        <ItemTemplate>
                            <asp:Label ID="lblConfigValue" runat="server" Text='<%# Bind("configValue")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Menu Utama">
                        <ItemTemplate>
                            <asp:Label ID="lblConfigMenuUtama" runat="server" Text='<%# Bind("configMenuUtama")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Menu">
                        <ItemTemplate>
                            <asp:Label ID="lblConfigMenu" runat="server" Text='<%# Bind("configMenu")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Date / Time">
                        <ItemTemplate>
                            <asp:Label ID="lblConfigDateTime" runat="server" Text='<%# Bind("configDateTime")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:CommandField SelectText="[KEMASKINI]" ShowSelectButton="True" HeaderText=" KEMASKINI " />

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
    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label><br />
</div>
