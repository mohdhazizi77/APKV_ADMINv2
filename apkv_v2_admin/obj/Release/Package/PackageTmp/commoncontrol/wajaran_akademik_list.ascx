<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="wajaran_akademik_list.ascx.vb" Inherits="apkv_v2_admin.wajaran_akademik_list" %>

<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Konfigurasi >> Wajaran Akademik >> Senarai Wajaran Akademik</td>
    </tr>
</table>
<br />
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Wajaran Akademik</td>
    </tr>
    <tr>
        <td style="width: 20%;">Kohort : </td>
        <td>
            <asp:DropDownList ID="ddlKohort" runat="server" AutoPostBack="false" Width="200px">
            </asp:DropDownList>
    </tr>
    <tr>
        <td style="width: 20%;">TahunPeperiksaan : </td>
        <td>
            <asp:DropDownList ID="ddlTahunPeperiksaan" runat="server" AutoPostBack="false" Width="200px">
            </asp:DropDownList>
    </tr>
    <tr>
        <td style="width: 20%;">Semester : </td>
        <td>
            <asp:DropDownList ID="ddlSemester" runat="server" AutoPostBack="false" Width="200px">
            </asp:DropDownList>
    </tr>
    <tr>
        <td></td>
        <td colspan="2">
            <asp:Button ID="btnSearch" runat="server" Text="Cari " CssClass="fbbutton" Width="100px" />&nbsp;
            <asp:Button ID="btnDaftar" runat="server" Text="Daftar Baru " CssClass="fbbutton"  Width="100px" />
        </td>
    </tr>
</table>
<br />
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Senarai Wajaran Akademik</td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:GridView ID="datRespondent" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                CellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames="WajaranID"
                Width="100%" PageSize="30" CssClass="gridview_footer">
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <Columns>
                    <asp:TemplateField HeaderText="#">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Kohort">
                        <ItemTemplate>
                            <asp:Label ID="Kohort" runat="server" Text='<%# Bind("Kohort")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Tahun Peperiksaan">
                        <ItemTemplate>
                            <asp:Label ID="TahunPeperiksaan" runat="server" Text='<%# Bind("TahunPeperiksaan")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Semester">
                        <ItemTemplate>
                            <asp:Label ID="Semester" runat="server" Text='<%# Bind("Semester")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Mata Pelajaran">
                        <ItemTemplate>
                            <asp:Label ID="Subjek" runat="server" Text='<%# Bind("Subjek")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Wajaran Berterusan">
                        <ItemTemplate>
                            <asp:Label ID="Berterusan" runat="server" Text='<%# Bind("Berterusan")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Wajaran Akhir 1">
                        <ItemTemplate>
                            <asp:Label ID="Akhir1" runat="server" Text='<%# Bind("Akhir1")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Wajaran Akhir 2">
                        <ItemTemplate>
                            <asp:Label ID="Akhir2" runat="server" Text='<%# Bind("Akhir2")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:CommandField SelectText="[-PILIH-]" ShowSelectButton="True" HeaderText="PILIH" />
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
        <td class="fbform_sap">&nbsp;
        </td>
    </tr>
</table>
<div class="info" id="divMsg" runat="server">
    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label><br />
</div>
