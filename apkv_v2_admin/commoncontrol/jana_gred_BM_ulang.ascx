<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="jana_gred_BM_ulang.ascx.vb" Inherits="apkv_v2_admin.jana_gred_BM_ulang1" %>

<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Pendaftaran Calon Ulang >> Akademik</td>
    </tr>
</table>

<br />

<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Maklumat Kolej</td>
    </tr>
    <tr>
        <td>Negeri:</td>
        <td>
            <asp:DropDownList ID="ddlNegeri" runat="server" AutoPostBack="true" Width="350px"></asp:DropDownList></td>
    </tr>
    <tr>
        <td>Jenis Kolej:</td>
        <td>
            <asp:DropDownList ID="ddlJenis" runat="server" AutoPostBack="true" Width="350px"></asp:DropDownList>
    </tr>
    <tr>
        <td>Nama Kolej:</td>
        <td>
            <asp:DropDownList ID="ddlKolej" runat="server" AutoPostBack="true" Width="350px"></asp:DropDownList></td>
    </tr>
</table>

<br />

<table class="fbform">

    <tr class="fbform_header">
        <td colspan="2">Akademik.</td>
    </tr>
    <tr>
        <td style="width: 20%;">Kohort:</td>
        <td>
            <asp:DropDownList ID="ddlTahun" runat="server" AutoPostBack="false" Width="350px"></asp:DropDownList>
        </td>
    </tr> 
    <tr>
        <td colspan="2">
            <asp:Button ID="btnCari" runat="server" Text="Cari" CssClass="fbbutton" />&nbsp;</td>
    </tr>

</table>

<br />

<table class="fbform">
    <tr class="fbform_header">
        <td>Senarai Calon
        </td>
    </tr>
    <tr>
        <td>
            <asp:GridView ID="datRespondent" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                CellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames="PelajarID"
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
                    <asp:TemplateField HeaderText="Nama">
                        <ItemTemplate>
                            <asp:Label ID="lblNama" runat="server" Text='<%# Bind("Nama")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="PelajarID">
                        <ItemTemplate>
                            <asp:Label ID="PelajarID" runat="server" Text='<%# Bind("PelajarID")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Mykad">
                        <ItemTemplate>
                            <asp:Label ID="Mykad" runat="server" Text='<%# Bind("MYKAD")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Angka Giliran">
                        <ItemTemplate>
                            <asp:Label ID="AngkaGiliran" runat="server" Text='<%# Bind("AngkaGiliran")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Bahasa Melayu">
                        <ItemTemplate>
                            <asp:Label ID="AngkaGiliran" runat="server" Text='<%# Bind("BahasaMelayu")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Gred BM">
                        <ItemTemplate>
                            <asp:Label ID="GredBM" runat="server" Width="30px" Text='<%# Bind("GredBM")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Markah Ulang">
                        <ItemTemplate>
                            <asp:Label ID="MarkahUlang" runat="server" Width="30px" Text='<%# Bind("MarkahPA")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Gred Ulang">
                        <ItemTemplate>
                            <asp:Label ID="GredUlang" runat="server" Width="30px" Text='<%# Bind("Gred")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:CheckBox ID="chkAll" runat="server" AutoPostBack="true" OnCheckedChanged="CheckUncheckAll" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="chkSelect" runat="server" />
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
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
</table>
<br />

<table class="fbform">
    <tr class="fbform_header">
        <td>Pendaftaran Calon Ulang
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:Button ID="btnDaftar" runat="server" Text="Daftar" CssClass="fbbutton" />&nbsp;</td>
    </tr>
</table>

<br />
<div class="info" id="divMsg" runat="server">
    <asp:Label ID="lblKolejID" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblMsg" runat="server" Text="System message..."></asp:Label>
</div>
