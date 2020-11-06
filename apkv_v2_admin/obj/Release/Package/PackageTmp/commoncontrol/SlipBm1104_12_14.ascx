<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="SlipBm1104_12_14.ascx.vb" Inherits="apkv_v2_admin.SlipBm1104_12_14" %>
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Slip Pernyataan Keputusan BM1104 Tahun 2012 sehingga 2014</td>
    </tr>
</table>
<br />
<table class="fbform" style="width: 100%">
    <tr>
        <td style="width: 15%;">Mykad</td>
        <td style ="width :2%">:</td>
        <td><asp:TextBox ID="txtMYKAD" runat="server" Width="200px" MaxLength="100"></asp:TextBox></td>
    </tr>
    <tr>
        <td>Angka Giliran</td>
        <td>:</td>
        <td><asp:TextBox ID="txtAngkaGiliran" runat="server" Width="200px" MaxLength="100"></asp:TextBox></td>
    </tr>
     <tr>
        <td>Nama Calon</td>
        <td>:</td>
        <td><asp:TextBox ID="txtNama" runat="server" Width="200px" MaxLength="100"></asp:TextBox></td>
    </tr>
</table>
<br />

<table class="fbform" style="width: 100%">
    <tr class="fbform_header">
        <td colspan="3">Perincian Carian</td>
    </tr>
    <tr>
        <td>Nama Kolej</td>
        <td>:</td>
        <td><asp:DropDownList ID="ddlKolej" runat="server" Width="350px"></asp:DropDownList></td>
    </tr>
    <tr>
        <td style="width: 15%;">Kohort</td>
        <td style ="width :2%">:</td>
        <td><asp:DropDownList ID="ddlTahun" runat="server" Width="200px" AutoPostBack ="true" ></asp:DropDownList></td>
    </tr>

    <tr>
        <td>Sesi Pengambilan</td>
        <td>:</td>
        <td>
            <asp:CheckBoxList ID="chkSesi" runat="server" AutoPostBack="true" Width="250px" RepeatDirection="Horizontal">
                <asp:ListItem Value = "1">1</asp:ListItem>
                <asp:ListItem Value = "2">2</asp:ListItem>
            </asp:CheckBoxList></td>
    </tr>
    <tr>
        <td>Program</td>
        <td>:</td>
        <td><asp:DropDownList ID="ddlKodKursus" runat="server" Width="350px"></asp:DropDownList></td>
    </tr>
    <tr>
        <td>Status</td>
        <td>:</td>
        <td>
            <asp:DropDownList ID="ddlStatus" runat="server">
                <asp:ListItem Value="">SEMUA</asp:ListItem>
                <asp:ListItem Value="SETARA">SETARA</asp:ListItem>
                <asp:ListItem Value="TIDAK SETARA">TIDAK SETARA</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td colspan ="3">&nbsp</td>
    </tr>
    <tr>
        <td colspan="3"><asp:Button ID="btnSearch" runat="server" Text="Cari " CssClass="fbbutton" Width ="150" /></td>
    </tr>
</table>
<br />
<table class="fbform" style="width: 100%">
    <tr class="fbform_header">
        <td>Senarai Calon
        </td>
    </tr>
    <tr>
        <td>
            <asp:Panel ID ="lblPnl" runat ="server"  Width ="100%" Height ="450" ScrollBars ="Vertical">
            <asp:GridView ID="datRespondent" runat="server" AutoGenerateColumns="False" AllowPaging="False"
                CellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames="ID"
                Width="100%"  CssClass="gridview_footer">
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <Columns>
                    <asp:TemplateField HeaderText="#">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top"/><ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="NoSiri">
                        <ItemTemplate>
                            <asp:Label ID="lblRunningNo" runat="server" Text='<%# Bind("NoSiri")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" /><ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Kohort">
                        <ItemTemplate>
                            <asp:Label ID="Kohort" runat="server" Text='<%# Bind("Kohort")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top"/><ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Sesi">
                        <ItemTemplate>
                            <asp:Label ID="Sesi" runat="server" Text='<%# Bind("Sesi") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="center" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Nama" ItemStyle-Width="20%">
                        <ItemTemplate>
                            <asp:Label ID="Nama" runat="server" Text='<%# Bind("Nama") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Mykad">
                        <ItemTemplate>
                            <asp:Label ID="Mykad" runat="server" Text='<%# Bind("Mykad")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="AngkaGiliran">
                        <ItemTemplate>
                            <asp:Label ID="AngkaGiliran" runat="server" Text='<%# Bind("AngkaGiliran")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Program">
                        <ItemTemplate>
                            <asp:Label ID="lblKursus" runat="server" Text='<%# Bind("Kursus")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                   <%-- <asp:TemplateField HeaderText="Nama Program">
                        <ItemTemplate>
                            <asp:Label ID="NamaKursus" runat="server" Text='<%# Bind("Kursus")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>--%>
                  
                    <asp:TemplateField HeaderText="GredBMSetara">
                        <ItemTemplate>
                            <asp:Label ID="GredBMSetara" runat="server" Text='<%# Bind("BMSetara")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="center" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="PNGKA">
                        <ItemTemplate>
                            <asp:Label ID="PNGKAKA" runat="server" Text='<%# Bind("PNGKAKA")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="PNGKV">
                        <ItemTemplate>
                            <asp:Label ID="PNGKVOK" runat="server" Text='<%# Bind("PNGKVOK")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
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
                </asp:Panel>
        </td>
    </tr>
</table>
<br />
<table class="fbform" style ="width :100%">
    <tr class="fbform_header">
        <td colspan="3">Pemilihan Cetakan Tandatangan </td>
    </tr>
    <tr>
        <td style="width: 20%">Nama Pengarah Peperiksaan</td>
        <td style="width: 1%">:</td>
        <td>
            <asp:DropDownList ID="ddlSign" runat="server" Width="350"></asp:DropDownList>
            <a href="~/prm.pengarahPeperiksaan.aspx" runat="server" style="color: green; font: 9px; vertical-align: middle">[+]Konfigurasi</a>
        </td>
    </tr>
</table>
<br />
<table class="fbform" style ="width :100%">
    <tr class="fbform_header">
        <td>Cetak Slip Pernyataan Keputusan BM1104 </td>
    </tr>

    <tr>
        <td>
            <asp:Button ID="btnPrintSlip" runat="server" Text="Cetak Slip Keputusan" CssClass="fbbutton"/>
            &nbsp;<asp:HyperLink ID="HyPDF2" runat="server" Target="_blank" Visible="false">Klik disini untuk muat turun</asp:HyperLink>
        </td>
    </tr>
</table>
<br />
<div class="info" id="divMsg" runat="server">
    <asp:Label ID="lblKolejID" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
</div>

