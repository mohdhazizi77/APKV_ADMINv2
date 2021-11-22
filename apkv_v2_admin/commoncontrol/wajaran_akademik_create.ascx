<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="wajaran_akademik_create.ascx.vb" Inherits="apkv_v2_admin.wajaran_akademik_create" %>

<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Konfigurasi >> Wajaran Akademik >> Pendaftaran Wajaran Akademik</td>
    </tr>
</table>
<br />
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="6">Paparan Maklumat Wajaran Akademik 
        </td>
    </tr>
    <tr>
        <td style="width: 150px">Kohort :</td>
        <td>
            <asp:DropDownList ID="ddlKohort" runat="server" Width="100px"></asp:DropDownList></td>
    </tr>
    <tr>
        <td style="width: 150px">Tahun Peperiksaan :</td>
        <td>
            <asp:DropDownList ID="ddlTahunPeperiksaan" runat="server" Width="100px"></asp:DropDownList></td>
    </tr>
    <tr>
        <td style="width: 150px">Semester :</td>
        <td>
            <asp:DropDownList ID="ddlSemester" runat="server" Width="100px"></asp:DropDownList></td>

    </tr>



    <tr>
        <td colspan="3">
            <asp:Button ID="btnDaftar" runat="server" Text="Daftar Baru" CssClass="fbbutton" />&nbsp;<asp:Button ID="btnList" runat="server" Text="Senarai Gred" CssClass="fbbutton" />
        </td>
    </tr>

</table>
<br />

<table class="fbform">
    <tr class="fbform_header">
        <td>Senarai Mata Pelajaran
        </td>
    </tr>
    <tr>
        <td>
            <asp:GridView ID="datRespondent" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                CellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames="WajaranID"
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
                    <asp:TemplateField HeaderText="Mata Pelajaran">
                        <ItemTemplate>
                            <asp:Label ID="Subjek" runat="server" Width="50px" Text='<%# Bind("Subjek")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Wajaran Berterusan">
                        <ItemTemplate>
                            <asp:TextBox ID="Berterusan" runat="server" Width="30px" Text='<%# Bind("Berterusan")%>'></asp:TextBox>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Wajaran Akhir 1">
                        <ItemTemplate>
                            <asp:TextBox ID="Akhir1" runat="server" Width="30px" Text='<%# Bind("Akhir1")%>'></asp:TextBox>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Wajaran Akhir 2">
                        <ItemTemplate>
                            <asp:TextBox ID="Akhir2" runat="server" Width="30px" Text='<%# Bind("Akhir2")%>'></asp:TextBox>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
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
            <asp:Button ID="btnUpdate" runat="server" Text="Kemaskini" CssClass="fbbutton" Visible="true" />&nbsp;
           
            <asp:Button ID="btnGred" runat="server" Text="Gred" CssClass="fbbutton" Visible="true" />
        </td>
    </tr>
</table>



<br />
<div class="info" id="divMsg" runat="server">
    <asp:Label ID="lblMsg" runat="server" Text="Mesej..."></asp:Label>

</div>
