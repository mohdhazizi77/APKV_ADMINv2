<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="pembetulan_markah_PA_akademik.ascx.vb" Inherits="apkv_v2_admin.pembetulan_markah_PA_akademik" %>

<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Akademik >> Pentaksiran Akhir Akademik</td>
    </tr>
</table>
<br />

<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Maklumat Pelajar</td>
    </tr>
    <tr>
        <td style="width: 200px">Mykad:</td>
        <td>
            <asp:TextBox ID="txtMykad" runat="server" Width="350px"></asp:TextBox></td>
    </tr>
    <tr>
        <td style="width: 200px">Angka Giliran:</td>
        <td>
            <asp:TextBox ID="txtAngkaGiliran" runat="server" AutoPostBack="true" Width="350px"></asp:TextBox>
    </tr>
    <tr>
        <td style="width: 200px">Kohort:</td>
        <td>
            <asp:DropDownList ID="ddlTahun" runat="server" AutoPostBack="true" Width="350px"></asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td style="width: 200px">Semester:</td>
        <td>
            <asp:DropDownList ID="ddlSemester" runat="server" AutoPostBack="true" Width="350px"></asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td style="width: 200px">Sesi Pengambilan:</td>
        <td>
            <asp:CheckBoxList ID="chkSesi" runat="server" AutoPostBack="true" Width="349px" RepeatDirection="Horizontal">
                <asp:ListItem>1</asp:ListItem>
                <asp:ListItem>2</asp:ListItem>
            </asp:CheckBoxList></td>
    </tr>
    <tr>
        <td style="width: 200px"></td>
        <td colspan="2">
            <asp:Button ID="btnCari" runat="server" Text="Cari" CssClass="fbbutton" />&nbsp;</td>
    </tr>
</table>

<br />

<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Maklumat Kolej</td>
    </tr>
    <tr>
        <td style="width: 200px">Negeri:</td>
        <td>
            <asp:DropDownList ID="ddlNegeri" runat="server" AutoPostBack="true" Width="350px"></asp:DropDownList></td>
    </tr>
    <tr>
        <td style="width: 200px">Jenis Kolej:</td>
        <td>
            <asp:DropDownList ID="ddlJenis" runat="server" AutoPostBack="true" Width="350px"></asp:DropDownList>
    </tr>
    <tr>
        <td style="width: 200px">Nama Kolej:</td>
        <td>
            <asp:DropDownList ID="ddlKolej" runat="server" AutoPostBack="true" Width="350px"></asp:DropDownList></td>
    </tr>
</table>

<br />

<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2"></td>
    </tr>    
    <tr>
        <td style="width: 200px">Kod Program:</td>
        <td>
            <asp:DropDownList ID="ddlKodKursus" runat="server" AutoPostBack="true" Width="350px"></asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td style="width: 200px">Kelas:</td>
        <td>
            <asp:DropDownList ID="ddlKelas" runat="server" AutoPostBack="false" Width="350px"></asp:DropDownList></td>
    </tr>
</table>

<br />

<div class="info" id="divMsgResult" runat="server">
    <asp:Label ID="lblMsgResult" runat="server" Text="Mesej..."></asp:Label>
</div>
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
                    <asp:TemplateField HeaderText="Mykad">
                        <ItemTemplate>
                            <asp:Label ID="lblMykad" runat="server" Text='<%# Bind("Mykad")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="AngkaGiliran">
                        <ItemTemplate>
                            <asp:Label ID="AngkaGiliran" runat="server" Text='<%# Bind("AngkaGiliran")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Kod Program">
                        <ItemTemplate>
                            <asp:Label ID="KodKursus" runat="server" Text='<%# Bind("KodKursus")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="BM">
                        <ItemTemplate>
                            <asp:TextBox ID="A_BahasaMelayu" runat="server" Width="30px" MaxLength="4" Text='<%# Bind("A_BahasaMelayu")%>' Enabled="false"></asp:TextBox>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="BM1" Visible="false">
                        <ItemTemplate>
                            <asp:TextBox ID="A_BahasaMelayu1" runat="server" Width="30px" MaxLength="4" Text='<%# Bind("A_BahasaMelayu1")%>' Enabled="false"></asp:TextBox>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="BM2" Visible="false">
                        <ItemTemplate>
                            <asp:TextBox ID="A_BahasaMelayu2" runat="server" Width="30px" MaxLength="4" Text='<%# Bind("A_BahasaMelayu2")%>' Enabled="false"></asp:TextBox>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="BM3">
                        <ItemTemplate>
                            <asp:TextBox ID="A_BahasaMelayu3" runat="server" Width="30px" MaxLength="4" Text='<%# Bind("A_BahasaMelayu3")%>' Enabled="false"></asp:TextBox>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="BI">
                        <ItemTemplate>
                            <asp:TextBox ID="A_BahasaInggeris" runat="server" Width="30px" MaxLength="4" Text='<%# Bind("A_BahasaInggeris")%>' Enabled="false"></asp:TextBox>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Math">
                        <ItemTemplate>
                            <asp:TextBox ID="A_Mathematics" runat="server" Width="30px" MaxLength="4" Text='<%# Bind("A_Mathematics")%>' Enabled="false"></asp:TextBox>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Science1">
                        <ItemTemplate>
                            <asp:TextBox ID="A_Science1" runat="server" Width="30px" MaxLength="4" Text='<%# Bind("A_Science1")%>' Enabled="false"></asp:TextBox>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Science2">
                        <ItemTemplate>
                            <asp:TextBox ID="A_Science2" runat="server" Width="30px" MaxLength="4" Text='<%# Bind("A_Science2")%>' Enabled="false"></asp:TextBox>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Sejarah">
                        <ItemTemplate>
                            <asp:TextBox ID="A_Sejarah" runat="server" Width="30px" MaxLength="4" Text='<%# Bind("A_Sejarah")%>' Enabled="false"></asp:TextBox>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="P.Islam1">
                        <ItemTemplate>
                            <asp:TextBox ID="A_PendidikanIslam1" runat="server" Width="30px" MaxLength="4" Text='<%# Bind("A_PendidikanIslam1")%>' Enabled="false"></asp:TextBox>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="P.Islam2">
                        <ItemTemplate>
                            <asp:TextBox ID="A_PendidikanIslam2" runat="server" Width="30px" MaxLength="4" Text='<%# Bind("A_PendidikanIslam2")%>' Enabled="false"></asp:TextBox>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="P.Moral">
                        <ItemTemplate>
                            <asp:TextBox ID="A_PendidikanMoral" runat="server" Width="30px" MaxLength="4" Text='<%# Bind("A_PendidikanMoral")%>' Enabled="false"></asp:TextBox>
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

<br />

<table class="fbform">
    <tr class="fbform_header">
        <td colspan="6">Pilihan Subjek Untuk Pembetulan</td>
    </tr>
    <tr>
        <td>Matapelajaran:
        </td>
        <td>
            <asp:DropDownList ID="ddlMatapelajaran" runat="server" Width="200" AutoPostBack="true"></asp:DropDownList>
        </td>
        <td>
            <asp:DropDownList ID="ddlKertas" runat="server" Width="100" Enabled="false">
                <asp:ListItem Value="1">Kertas 1</asp:ListItem>
                <asp:ListItem Value="2">Kertas 2</asp:ListItem>
            </asp:DropDownList>
        </td>
        <td>
            <asp:TextBox ID="txtMarkah" runat="server" AutoPostBack="false" Width="30px"></asp:TextBox>
        </td>
        <td>
            <asp:Button ID="btnUpdate" runat="server" Text="Kemaskini" CssClass="fbbutton" Visible="true" />&nbsp;
            <asp:Button ID="btnGred" runat="server" Text="Jana Gred" CssClass="fbbutton" Visible="true" />
        </td>
    </tr>
</table>

<br />

<table class="fbform" style="width: 100%">
    <tr class="fbform_header">
        <td colspan="2">Sejarah Pengemaskinian Markah
        </td>
    </tr>
    <tr>
        <td colspan="2">

            <asp:GridView ID="datRespondentAudit" runat="server" AutoGenerateColumns="False"
                CellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames="markahAuditID"
                Width="100%" PageSize="10" AllowPaging="true" CssClass="gridview_footer" EnableModelValidation="True">
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Left" />
                <Columns>
                    <asp:TemplateField HeaderText="#">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Dikemaskini Oleh">
                        <ItemTemplate>
                            <asp:Label ID="Audit" runat="server" Text='<%# Bind("LoginID")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Tarikh">
                        <ItemTemplate>
                            <asp:Label ID="Tarikh" runat="server" Text='<%# Bind("dateTime")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Nama Pelajar">
                        <ItemTemplate>
                            <asp:Label ID="Nama" runat="server" Text='<%# Bind("Nama")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Angka Giliran">
                        <ItemTemplate>
                            <asp:Label ID="Tahun" runat="server" Text='<%# Bind("AngkaGiliran")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Tahun">
                        <ItemTemplate>
                            <asp:Label ID="Tahun" runat="server" Text='<%# Bind("Tahun")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Semester">
                        <ItemTemplate>
                            <asp:Label ID="Semester" runat="server" Text='<%# Bind("Semester")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Sesi">
                        <ItemTemplate>
                            <asp:Label ID="Sesi" runat="server" Text='<%# Bind("Sesi")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                                     
                    <asp:TemplateField HeaderText="Kod Kursus">
                        <ItemTemplate>
                            <asp:Label ID="KodMataPelajaran" runat="server" Text='<%# Bind("KodKursus")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Nama Matapelajaran">
                        <ItemTemplate>
                            <asp:Label ID="NamaMataPelajaran" runat="server" Text='<%# Bind("NamaMataPelajaran")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Markah Sebelum">
                        <ItemTemplate>
                            <asp:Label ID="MarkahSebelum" runat="server" Text='<%# Bind("MarkahSebelum")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Markah Selepas">
                        <ItemTemplate>
                            <asp:Label ID="MarkahSelepas" runat="server" Text='<%# Bind("MarkahSelepas")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Jenis">
                        <ItemTemplate>
                            <asp:Label ID="Jenis" runat="server" Text='<%# Bind("Jenis")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Catatan">
                        <ItemTemplate>
                            <asp:Label ID="Catatan" runat="server" Text='<%# Bind("Catatan")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>

                </Columns>
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Font-Underline="true" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" CssClass="cssPager" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" VerticalAlign="Bottom"
                    HorizontalAlign="Left" />
                <EditRowStyle BackColor="#999999" />
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            </asp:GridView>
        </td>
    </tr>
</table>

<br />

<div class="info" id="divMsgAudit" runat="server">

    <asp:Label ID="lblMsgAudit" runat="server" Text="System message..."></asp:Label>

</div>

