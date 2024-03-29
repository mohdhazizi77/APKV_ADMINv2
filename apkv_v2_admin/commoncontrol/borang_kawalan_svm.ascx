﻿<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="borang_kawalan_svm.ascx.vb" Inherits="apkv_v2_admin.borang_kawalan_svm1" %>


<table class="fbform">

    <tr class="fbform_header">
        <td colspan="2">Borang Kawalan Sijil Vokasional Malaysia</td>
    </tr>

</table>

<table class="fbform" style="width: 100%">

    <tr class="fbform_header">
        <td colspan="2">Maklumat Kolej</td>
    </tr>

    <tr>
        <td style="width: 20%;">Negeri:</td>
        <td>
            <asp:DropDownList ID="ddlNegeri" runat="server" Width="350px" AutoPostBack="true"></asp:DropDownList>
        </td>
    </tr>

    <tr>
        <td>Jenis Kolej:</td>
        <td>
            <asp:DropDownList ID="ddlJenis" runat="server" AutoPostBack="true" Width="350px"></asp:DropDownList>
    </tr>

    <tr>
        <td>Nama Kolej:</td>
        <td>
            <asp:DropDownList ID="ddlKolej" runat="server" AutoPostBack="false" Width="350px"></asp:DropDownList>
        </td>
    </tr>

</table>

<br />

<table class="fbform" style="width: 100%">

    <tr>
        <td style="width: 20%;">Kohort:</td>
        <td>
            <asp:DropDownList ID="ddlTahun" runat="server" AutoPostBack="false" Width="200px">
            </asp:DropDownList>
        </td>
    </tr>

    <tr>
        <td style="width: 20%;">Sesi Pengambilan</td>
        <td>
            <asp:CheckBoxList ID="chkSesi" runat="server" AutoPostBack="true" Width="349px" RepeatDirection="Horizontal">
                <asp:ListItem Selected="True">1</asp:ListItem>
                <asp:ListItem>2</asp:ListItem>
            </asp:CheckBoxList>
        </td>
    </tr>

    <tr>
        <td style="width: 20%;">Kod Program:</td>
        <td>
            <asp:DropDownList ID="ddlKodKursus" runat="server" AutoPostBack="true" Width="350px"></asp:DropDownList>
        </td>
    </tr>

    <tr>
        <td style="width: 20%;">Nama Kelas:</td>
        <td>
            <asp:DropDownList ID="ddlNamaKelas" runat="server" AutoPostBack="false" Width="350px"></asp:DropDownList>
        </td>
    </tr>

    <tr>
        <td style="width: 20%;">Nama Calon:</td>
        <td>
            <asp:TextBox ID="txtNama" runat="server" Width="350px" MaxLength="200"></asp:TextBox>
        </td>
    </tr>

    <tr>
        <td style="width: 20%;">Mykad:</td>
        <td>
            <asp:TextBox ID="txtMYKAD" runat="server" Width="350px" MaxLength="200"></asp:TextBox>
        </td>
    </tr>

    <tr>
        <td style="width: 20%;">Angka Giliran:</td>
        <td>
            <asp:TextBox ID="txtAngkaGiliran" runat="server" Width="350px" MaxLength="200"></asp:TextBox>
        </td>
    </tr>

    <tr>
        <td>Status:</td>
        <td>
            <asp:DropDownList ID="ddlStatus" runat="server">
                <asp:ListItem Value="SEMUA">SEMUA</asp:ListItem>
                <asp:ListItem Value="SETARA">SETARA</asp:ListItem>
                <asp:ListItem Value="TIDAK SETARA">TIDAK SETARA</asp:ListItem>
                <asp:ListItem Value="LAYAK">LAYAK</asp:ListItem>
                <asp:ListItem Value="TIDAK LAYAK">TIDAK LAYAK</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>

    <tr>
        <td></td>
        <td colspan="2">
            <asp:Button ID="btnSearch" runat="server" Text="Cari " CssClass="fbbutton" />&nbsp;

            <asp:Button ID="btnKemaskini" runat="server" Text="Kemaskini" CssClass="fbbutton" />
        </td>
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
            <asp:GridView ID="datRespondent" runat="server" AutoGenerateColumns="False" AllowPaging="False"
                CellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames="PelajarID"
                Width="100%" CssClass="gridview_footer">
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <Columns>
                    <asp:TemplateField HeaderText="#">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="RunningNo">
                        <ItemTemplate>
                            <asp:Label ID="lblRunningNo" runat="server" Text='<%# Bind("RunningNo")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Kohort">
                        <ItemTemplate>
                            <asp:Label ID="Tahun" runat="server" Text='<%# Bind("Tahun")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Sem">
                        <ItemTemplate>
                            <asp:Label ID="Semester" runat="server" Text='<%# Bind("Semester")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Sesi">
                        <ItemTemplate>
                            <asp:Label ID="Sesi" runat="server" Text='<%# Bind("Sesi") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Nama">
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
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Angka Giliran">
                        <ItemTemplate>
                            <asp:Label ID="AngkaGiliran" runat="server" Text='<%# Bind("AngkaGiliran")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Nama Bidang">
                        <ItemTemplate>
                            <asp:Label ID="NamaKluster" runat="server" Text='<%# Bind("NamaKluster")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Nama Program">
                        <ItemTemplate>
                            <asp:Label ID="NamaKursus" runat="server" Text='<%# Bind("NamaKursus")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Nama Kelas">
                        <ItemTemplate>
                            <asp:Label ID="NamaKelas" runat="server" Text='<%# Bind("NamaKelas")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="PNGKA">
                        <ItemTemplate>
                            <asp:Label ID="PNGKA" runat="server" Text='<%# Bind("PNGKA")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="PNGKV">
                        <ItemTemplate>
                            <asp:Label ID="PNGKV" runat="server" Text='<%# Bind("PNGKV")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <%--<asp:TemplateField HeaderText="status">
                        <ItemTemplate>
                            <asp:Label ID="lblStatusCetak" runat="server"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>--%>

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

<%--<table class="fbform">

    <tr class="fbform_header">
        <td colspan="3">Pemilihan Cetakan Tandatangan </td>
    </tr>

    <tr>
        <td style="width: 20%">Nama Pengarah Peperiksaan</td>
        <td style="width: 2%">:</td>
        <td>
            <asp:DropDownList ID="ddlSign" runat="server" Width="350"></asp:DropDownList>
            <a href="~/prm.pengarahPeperiksaan.aspx" runat="server" style="color: green; font: 9px; vertical-align: middle">[+]Konfigurasi</a>
        </td>
    </tr>

</table>

<br />--%>

<%--<table class="fbform">

    <tr class="fbform_header">
        <td colspan="3">Sijil Vokasional Malaysia </td>
    </tr>

    <tr>
        <td style="width: 20%">Cetak QR Code : </td>
        <td>
            <asp:DropDownList ID="printQR" runat="server" Width="350">
                <asp:ListItem Value="1">YA</asp:ListItem>
                <asp:ListItem Value="0">TIDAK</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>--%>

    <%--<tr>
        <td></td>
        <td>
            <asp:Button ID="btnPrintSlip" runat="server" Text="Cetak Slip Keputusan" CssClass="fbbutton" />&nbsp;<asp:HyperLink ID="HyPDF2" runat="server" Target="_blank"
                Visible="false">Klik disini untuk muat turun.</asp:HyperLink>

            <asp:Button ID="btnPrintbyNegeri" runat="server" Text="Cetak Slip Keputusan Negeri" CssClass="fbbutton" Width="200px" />&nbsp;<asp:HyperLink ID="HyperLink1" runat="server" Target="_blank"
                Visible="false">Klik disini untuk muat turun.</asp:HyperLink>
        </td>
    </tr>--%>

<%--</table>--%>

<br />

<table class="fbform" style="width: 100%">

    <tr class="fbform_header">
        <td>Borang Kawalan Sijil Vokasional Malaysia</td>
    </tr>

    <tr>
        <td>
            <asp:Button ID="btnBorangKawalan" runat="server" Text="Cetak Borang Kawalan" CssClass="fbbutton" />
        </td>
    </tr>

</table>

<br />

<div class="info" id="divMsg" runat="server">
    <asp:Label ID="lblKolejID" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
</div>

