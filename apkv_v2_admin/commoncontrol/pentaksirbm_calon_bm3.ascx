<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="pentaksirbm_calon_bm3.ascx.vb" Inherits="apkv_v2_admin.pentaksirbm_calon_bm3" %>

<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Senarai Calon Markah Ujian Bertutur Bahasa Melayu</td>
    </tr>
</table>

<br />

<table class="fbform">
    <tr>
        <td></td>
        <td></td>
    </tr>
    <tr>
        <td></td>
        <td></td>
    </tr>
    <tr>
        <td></td>
        <td></td>
    </tr>
    <tr>
        <td style="width: 200px">Mata Pelajaran / Kertas:</td>
        <td>
            <asp:Label ID="lblMP" runat="server"></asp:Label>
        </td>
        <td style="text-align: right">

            <asp:Button ID="btnSubmit" runat="server" Text="Hantar" CssClass="fbbutton" Width="70px" Visible="true" />&nbsp;

         </td>
    </tr>

    <tr>
        <td style="width: 200px">Pusat Peperiksaan:</td>
        <td>
            <asp:Label ID="lblPP" runat="server"></asp:Label>
        </td>
        <td style="text-align: right">

            <asp:Button ID="btnCetak" runat="server" Text="Cetak" CssClass="fbbutton" Width="70px" Visible="true" />&nbsp;
        </td>
    </tr>

    <tr>
        <td style="width: 200px">Peranan:</td>
        <td>
            <asp:Label ID="lblPeranan" runat="server" Text="PENTAKSIR"></asp:Label>
        </td>
        <td></td>
    </tr>
    <tr>
        <td></td>
        <td></td>
    </tr>
    <tr>
        <td></td>
        <td></td>
    </tr>
    <tr>
        <td></td>
        <td></td>
    </tr>

</table>

<br />

<div class="info" id="divMsg" runat="server">
    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label><br />
</div>

<br />

<table class="fbform">
    <tr class="fbform_header">
        <td>Senarai Calon
        </td>
    </tr>
    <tr>
        <td>
            <asp:GridView ID="datRespondent" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                CellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames="id"
                Width="100%" PageSize="500" CssClass="gridview_footer">
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
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemTemplate>
                            <asp:Label ID="AngkaGiliran" runat="server" Text='<%# Bind("AngkaGiliran")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Status Hantar">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="Status" runat="server" Text='<%# Bind("StatusHantarBM3_Pentaksir")%>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Individu">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:TextBox ID="Individu" runat="server" Width="30px" MaxLength="2" Text='<%# Bind("BM3_1")%>'></asp:TextBox>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Kumpulan">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:TextBox ID="Kumpulan" runat="server" Width="30px" MaxLength="2" Text='<%# Bind("BM3_2")%>'></asp:TextBox>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Jumlah Markah">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="Jumlah" Enabled="false" runat="server" Width="30px" MaxLength="2" Text='<%# Bind("BM3_Total")%>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
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
        <td></td>
    </tr>
    <tr>
        <td></td>
    </tr>
    <tr>
        <td></td>
    </tr>
    <tr>
        <td></td>
    </tr>


    <tr>



        <td style="text-align: center">
            <asp:Button ID="btnBack" runat="server" Text="Kembali" CssClass="fbbutton" Width="70px" Visible="true" />&nbsp;
                       
            <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="fbbutton" Width="70px" Visible="true" />&nbsp;
                       
            <asp:Button ID="btnSave" runat="server" Text="Simpan" CssClass="fbbutton" Width="70px" Visible="true" />&nbsp;
        </td>
    </tr>

    <tr>
        <td></td>
    </tr>
    <tr>
        <td></td>
    </tr>
    <tr>
        <td></td>
    </tr>
    <tr>
        <td></td>
    </tr>
</table>

<br />



