<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="inden_matapelajaran_vok.ascx.vb" Inherits="apkv_v2_admin.inden_matapelajaran_vok" %>
<table class="fbform" style="width: 100%">
    <tr class="fbform_header">
        <td colspan="3">Analisa Matapelajaran Vokasional Mengikut Negeri</td>
    </tr>
    <tr>
        <td style="width: 20%">Kohort:</td>
        <td colspan="2">
            <asp:DropDownList ID="ddlTahun" runat="server" Width="150px" AutoPostBack="true"></asp:DropDownList></td>
    </tr>

    <tr>
        <td style="width: 20%">Semester:</td>
        <td colspan="2">
            <asp:DropDownList ID="ddlSemester" runat="server" Width="150px"></asp:DropDownList></td>
    </tr>

    <tr>
        <td style="width: 20%">Sesi:</td>
        <td colspan="2">
            <asp:CheckBoxList ID="chkSesi" runat="server" Width="200px" RepeatDirection="Horizontal">
                <asp:ListItem>1</asp:ListItem>
                <asp:ListItem>2</asp:ListItem>
            </asp:CheckBoxList>
        </td>
    </tr>

    <tr>
        <td style="width: 20%"></td>
        <td colspan="2">
            <asp:Button ID="btnSearch" runat="server" Text="Jana Analisa" CssClass="fbbutton" /></td>
    </tr>

</table>
<br />
<table class="fbform">
    <tr class="fbform_header">
        <td>Senarai Analisa</td>
    </tr>
    <tr>
        <td>
            <asp:GridView ID="datRespondent" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                CellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames="Negeri"
                Width="100%" PageSize="50" CssClass="gridview_footer" EnableModelValidation="True">
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <Columns>

                    <asp:TemplateField HeaderText="Negeri">
                        <ItemTemplate>
                            <asp:Label ID="Negeri" runat="server" Text='<%# Bind("Negeri")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="1%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Tahun">
                        <ItemTemplate>
                            <asp:Label ID="Tahun" runat="server" Text='<%# Bind("Tahun")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="1%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Sesi">
                        <ItemTemplate>
                            <asp:Label ID="Sesi" runat="server" Text='<%# Bind("Sesi")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="1%" />
                    </asp:TemplateField>
                    <%--<asp:TemplateField HeaderText="JumlahCalon">
                        <ItemTemplate>
                            <asp:Label ID="JumlahCalon" runat="server" Text='<%# Bind("JumlahCalon") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="1%" />
                    </asp:TemplateField>--%>
                    <asp:TemplateField HeaderText="V0101">
                        <ItemTemplate>
                            <asp:Label ID="V0101" runat="server" Text='<%# Bind("V0101")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V0201">
                        <ItemTemplate>
                            <asp:Label ID="V0201" runat="server" Text='<%# Bind("V0201")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V0301">
                        <ItemTemplate>
                            <asp:Label ID="V0301" runat="server" Text='<%# Bind("V0301")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V0401">
                        <ItemTemplate>
                            <asp:Label ID="V0401" runat="server" Text='<%# Bind("V0401")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V0501">
                        <ItemTemplate>
                            <asp:Label ID="V0501" runat="server" Text='<%# Bind("V0501")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V0601">
                        <ItemTemplate>
                            <asp:Label ID="V0601" runat="server" Text='<%# Bind("V0601")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V0701">
                        <ItemTemplate>
                            <asp:Label ID="V0701" runat="server" Text='<%# Bind("V0701")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V0801">
                        <ItemTemplate>
                            <asp:Label ID="V0801" runat="server" Text='<%# Bind("V0801")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V0901">
                        <ItemTemplate>
                            <asp:Label ID="V0901" runat="server" Text='<%# Bind("V0901")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V1001">
                        <ItemTemplate>
                            <asp:Label ID="V1001" runat="server" Text='<%# Bind("V1001")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V1101">
                        <ItemTemplate>
                            <asp:Label ID="V1101" runat="server" Text='<%# Bind("V1101")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V1201">
                        <ItemTemplate>
                            <asp:Label ID="V1201" runat="server" Text='<%# Bind("V1201")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V1301">
                        <ItemTemplate>
                            <asp:Label ID="V1301" runat="server" Text='<%# Bind("V1301")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V1401">
                        <ItemTemplate>
                            <asp:Label ID="V1401" runat="server" Text='<%# Bind("V1401")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V1501">
                        <ItemTemplate>
                            <asp:Label ID="V1501" runat="server" Text='<%# Bind("V1501")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V1601">
                        <ItemTemplate>
                            <asp:Label ID="V1601" runat="server" Text='<%# Bind("V1601")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V1701">
                        <ItemTemplate>
                            <asp:Label ID="V1701" runat="server" Text='<%# Bind("V1701")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V1801">
                        <ItemTemplate>
                            <asp:Label ID="V1801" runat="server" Text='<%# Bind("V1801")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V1901">
                        <ItemTemplate>
                            <asp:Label ID="V1901" runat="server" Text='<%# Bind("V1901")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V2001">
                        <ItemTemplate>
                            <asp:Label ID="V2001" runat="server" Text='<%# Bind("V2001")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V2101">
                        <ItemTemplate>
                            <asp:Label ID="V2101" runat="server" Text='<%# Bind("V2101")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V2201">
                        <ItemTemplate>
                            <asp:Label ID="V2201" runat="server" Text='<%# Bind("V2201")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V2301">
                        <ItemTemplate>
                            <asp:Label ID="V2301" runat="server" Text='<%# Bind("V2301")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V2401">
                        <ItemTemplate>
                            <asp:Label ID="V2401" runat="server" Text='<%# Bind("V2401")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V2501">
                        <ItemTemplate>
                            <asp:Label ID="V2501" runat="server" Text='<%# Bind("V2501")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V2601">
                        <ItemTemplate>
                            <asp:Label ID="V2601" runat="server" Text='<%# Bind("V2601")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V2701">
                        <ItemTemplate>
                            <asp:Label ID="V2701" runat="server" Text='<%# Bind("V2701")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V2801">
                        <ItemTemplate>
                            <asp:Label ID="V2801" runat="server" Text='<%# Bind("V2801")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V2901">
                        <ItemTemplate>
                            <asp:Label ID="V2901" runat="server" Text='<%# Bind("V2901")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V3001">
                        <ItemTemplate>
                            <asp:Label ID="V3001" runat="server" Text='<%# Bind("V3001")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V3101">
                        <ItemTemplate>
                            <asp:Label ID="V3101" runat="server" Text='<%# Bind("V3101")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V3201">
                        <ItemTemplate>
                            <asp:Label ID="V3201" runat="server" Text='<%# Bind("V3201")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V3301">
                        <ItemTemplate>
                            <asp:Label ID="V3301" runat="server" Text='<%# Bind("V3301")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V3401">
                        <ItemTemplate>
                            <asp:Label ID="V3401" runat="server" Text='<%# Bind("V3401")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V3501">
                        <ItemTemplate>
                            <asp:Label ID="V3501" runat="server" Text='<%# Bind("V3501")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V3601">
                        <ItemTemplate>
                            <asp:Label ID="V3601" runat="server" Text='<%# Bind("V3601")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V3701">
                        <ItemTemplate>
                            <asp:Label ID="V3701" runat="server" Text='<%# Bind("V3701")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V3801">
                        <ItemTemplate>
                            <asp:Label ID="V3801" runat="server" Text='<%# Bind("V3801")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V3901">
                        <ItemTemplate>
                            <asp:Label ID="V3901" runat="server" Text='<%# Bind("V3901")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V4001">
                        <ItemTemplate>
                            <asp:Label ID="V4001" runat="server" Text='<%# Bind("V4001")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Jumlah_Calon">
                        <ItemTemplate>
                            <asp:Label ID="Jumlah_Calon" runat="server" Text='<%# Bind("Jumlah_Calon")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="1%" />
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

            <asp:GridView ID="datRespondent2" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                CellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames="Negeri"
                Width="100%" PageSize="50" CssClass="gridview_footer" EnableModelValidation="True">
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <Columns>

                    <asp:TemplateField HeaderText="Negeri">
                        <ItemTemplate>
                            <asp:Label ID="Negeri" runat="server" Text='<%# Bind("Negeri")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="1%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Tahun">
                        <ItemTemplate>
                            <asp:Label ID="Tahun" runat="server" Text='<%# Bind("Tahun")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="1%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Sesi">
                        <ItemTemplate>
                            <asp:Label ID="Sesi" runat="server" Text='<%# Bind("Sesi")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="1%" />
                    </asp:TemplateField>
                    <%--<asp:TemplateField HeaderText="JumlahCalon">
                        <ItemTemplate>
                            <asp:Label ID="JumlahCalon" runat="server" Text='<%# Bind("JumlahCalon") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="1%" />
                    </asp:TemplateField>--%>
                    <asp:TemplateField HeaderText="V0102">
                        <ItemTemplate>
                            <asp:Label ID="V0102" runat="server" Text='<%# Bind("V0102")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V0202">
                        <ItemTemplate>
                            <asp:Label ID="V0202" runat="server" Text='<%# Bind("V0202")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V0302">
                        <ItemTemplate>
                            <asp:Label ID="V0302" runat="server" Text='<%# Bind("V0302")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V0402">
                        <ItemTemplate>
                            <asp:Label ID="V0402" runat="server" Text='<%# Bind("V0402")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V0502">
                        <ItemTemplate>
                            <asp:Label ID="V0502" runat="server" Text='<%# Bind("V0502")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V0602">
                        <ItemTemplate>
                            <asp:Label ID="V0602" runat="server" Text='<%# Bind("V0602")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V0702">
                        <ItemTemplate>
                            <asp:Label ID="V0702" runat="server" Text='<%# Bind("V0702")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V0802">
                        <ItemTemplate>
                            <asp:Label ID="V0802" runat="server" Text='<%# Bind("V0802")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V0902">
                        <ItemTemplate>
                            <asp:Label ID="V0902" runat="server" Text='<%# Bind("V0902")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V1002">
                        <ItemTemplate>
                            <asp:Label ID="V1002" runat="server" Text='<%# Bind("V1002")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V1102">
                        <ItemTemplate>
                            <asp:Label ID="V1102" runat="server" Text='<%# Bind("V1102")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V1202">
                        <ItemTemplate>
                            <asp:Label ID="V1202" runat="server" Text='<%# Bind("V1202")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V1302">
                        <ItemTemplate>
                            <asp:Label ID="V1302" runat="server" Text='<%# Bind("V1302")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V1402">
                        <ItemTemplate>
                            <asp:Label ID="V1402" runat="server" Text='<%# Bind("V1402")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                   <%-- <asp:TemplateField HeaderText="V1502">
                        <ItemTemplate>
                            <asp:Label ID="V1502" runat="server" Text='<%# Bind("V1502")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>--%>
                    <asp:TemplateField HeaderText="V1602">
                        <ItemTemplate>
                            <asp:Label ID="V1602" runat="server" Text='<%# Bind("V1602")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                   <%-- <asp:TemplateField HeaderText="V1702">
                        <ItemTemplate>
                            <asp:Label ID="V1702" runat="server" Text='<%# Bind("V1702")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>--%>
                    <asp:TemplateField HeaderText="V1802">
                        <ItemTemplate>
                            <asp:Label ID="V1802" runat="server" Text='<%# Bind("V1802")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V1902">
                        <ItemTemplate>
                            <asp:Label ID="V1902" runat="server" Text='<%# Bind("V1902")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V2002">
                        <ItemTemplate>
                            <asp:Label ID="V2002" runat="server" Text='<%# Bind("V2002")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V2102">
                        <ItemTemplate>
                            <asp:Label ID="V2102" runat="server" Text='<%# Bind("V2102")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V2202">
                        <ItemTemplate>
                            <asp:Label ID="V2202" runat="server" Text='<%# Bind("V2202")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V2302">
                        <ItemTemplate>
                            <asp:Label ID="V2302" runat="server" Text='<%# Bind("V2302")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V2402">
                        <ItemTemplate>
                            <asp:Label ID="V2402" runat="server" Text='<%# Bind("V2402")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V2502">
                        <ItemTemplate>
                            <asp:Label ID="V2502" runat="server" Text='<%# Bind("V2502")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V2602">
                        <ItemTemplate>
                            <asp:Label ID="V2602" runat="server" Text='<%# Bind("V2602")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V2702">
                        <ItemTemplate>
                            <asp:Label ID="V2702" runat="server" Text='<%# Bind("V2702")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V2802">
                        <ItemTemplate>
                            <asp:Label ID="V2802" runat="server" Text='<%# Bind("V2802")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V2902">
                        <ItemTemplate>
                            <asp:Label ID="V2902" runat="server" Text='<%# Bind("V2902")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V3002">
                        <ItemTemplate>
                            <asp:Label ID="V3002" runat="server" Text='<%# Bind("V3002")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V3102">
                        <ItemTemplate>
                            <asp:Label ID="V3102" runat="server" Text='<%# Bind("V3102")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V3202">
                        <ItemTemplate>
                            <asp:Label ID="V3202" runat="server" Text='<%# Bind("V3202")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V3302">
                        <ItemTemplate>
                            <asp:Label ID="V3302" runat="server" Text='<%# Bind("V3302")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V4202">
                        <ItemTemplate>
                            <asp:Label ID="V4202" runat="server" Text='<%# Bind("V4202")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V4302">
                        <ItemTemplate>
                            <asp:Label ID="V4302" runat="server" Text='<%# Bind("V4302")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                  <%--  <asp:TemplateField HeaderText="V3602">
                        <ItemTemplate>
                            <asp:Label ID="V3602" runat="server" Text='<%# Bind("V3602")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V3702">
                        <ItemTemplate>
                            <asp:Label ID="V3702" runat="server" Text='<%# Bind("V3702")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V3802">
                        <ItemTemplate>
                            <asp:Label ID="V3802" runat="server" Text='<%# Bind("V3802")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V3902">
                        <ItemTemplate>
                            <asp:Label ID="V3902" runat="server" Text='<%# Bind("V3902")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V4002">
                        <ItemTemplate>
                            <asp:Label ID="V4002" runat="server" Text='<%# Bind("V4002")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>--%>
                    <asp:TemplateField HeaderText="Jumlah_Calon">
                        <ItemTemplate>
                            <asp:Label ID="Jumlah_Calon" runat="server" Text='<%# Bind("Jumlah_Calon")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="1%" />
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

            <asp:GridView ID="datRespondent3" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                CellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames="Negeri"
                Width="100%" PageSize="50" CssClass="gridview_footer" EnableModelValidation="True">
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <Columns>

                    <asp:TemplateField HeaderText="Negeri">
                        <ItemTemplate>
                            <asp:Label ID="Negeri" runat="server" Text='<%# Bind("Negeri")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="1%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Tahun">
                        <ItemTemplate>
                            <asp:Label ID="Tahun" runat="server" Text='<%# Bind("Tahun")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="1%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Sesi">
                        <ItemTemplate>
                            <asp:Label ID="Sesi" runat="server" Text='<%# Bind("Sesi")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="1%" />
                    </asp:TemplateField>
                    <%--<asp:TemplateField HeaderText="JumlahCalon">
                        <ItemTemplate>
                            <asp:Label ID="JumlahCalon" runat="server" Text='<%# Bind("JumlahCalon") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="1%" />
                    </asp:TemplateField>--%>
                    <asp:TemplateField HeaderText="V0103">
                        <ItemTemplate>
                            <asp:Label ID="V0103" runat="server" Text='<%# Bind("V0103")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V0203">
                        <ItemTemplate>
                            <asp:Label ID="V0203" runat="server" Text='<%# Bind("V0203")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V0303">
                        <ItemTemplate>
                            <asp:Label ID="V0303" runat="server" Text='<%# Bind("V0303")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V0403">
                        <ItemTemplate>
                            <asp:Label ID="V0403" runat="server" Text='<%# Bind("V0403")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V0503">
                        <ItemTemplate>
                            <asp:Label ID="V0503" runat="server" Text='<%# Bind("V0503")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V0603">
                        <ItemTemplate>
                            <asp:Label ID="V0603" runat="server" Text='<%# Bind("V0603")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V0703">
                        <ItemTemplate>
                            <asp:Label ID="V0703" runat="server" Text='<%# Bind("V0703")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V0803">
                        <ItemTemplate>
                            <asp:Label ID="V0803" runat="server" Text='<%# Bind("V0803")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V0903">
                        <ItemTemplate>
                            <asp:Label ID="V0903" runat="server" Text='<%# Bind("V0903")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V1003">
                        <ItemTemplate>
                            <asp:Label ID="V1003" runat="server" Text='<%# Bind("V1003")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V1103">
                        <ItemTemplate>
                            <asp:Label ID="V1103" runat="server" Text='<%# Bind("V1103")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V1203">
                        <ItemTemplate>
                            <asp:Label ID="V1203" runat="server" Text='<%# Bind("V1203")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V1303">
                        <ItemTemplate>
                            <asp:Label ID="V1303" runat="server" Text='<%# Bind("V1303")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V1403">
                        <ItemTemplate>
                            <asp:Label ID="V1403" runat="server" Text='<%# Bind("V1403")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V1503">
                        <ItemTemplate>
                            <asp:Label ID="V1503" runat="server" Text='<%# Bind("V1503")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V1603">
                        <ItemTemplate>
                            <asp:Label ID="V1603" runat="server" Text='<%# Bind("V1603")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V1703">
                        <ItemTemplate>
                            <asp:Label ID="V1703" runat="server" Text='<%# Bind("V1703")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V1803">
                        <ItemTemplate>
                            <asp:Label ID="V1803" runat="server" Text='<%# Bind("V1803")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V1903">
                        <ItemTemplate>
                            <asp:Label ID="V1903" runat="server" Text='<%# Bind("V1903")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V2003">
                        <ItemTemplate>
                            <asp:Label ID="V2003" runat="server" Text='<%# Bind("V2003")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V2103">
                        <ItemTemplate>
                            <asp:Label ID="V2103" runat="server" Text='<%# Bind("V2103")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V2203">
                        <ItemTemplate>
                            <asp:Label ID="V2203" runat="server" Text='<%# Bind("V2203")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V2303">
                        <ItemTemplate>
                            <asp:Label ID="V2303" runat="server" Text='<%# Bind("V2303")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V2403">
                        <ItemTemplate>
                            <asp:Label ID="V2403" runat="server" Text='<%# Bind("V2403")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V2503">
                        <ItemTemplate>
                            <asp:Label ID="V2503" runat="server" Text='<%# Bind("V2503")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V2603">
                        <ItemTemplate>
                            <asp:Label ID="V2603" runat="server" Text='<%# Bind("V2603")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V2703">
                        <ItemTemplate>
                            <asp:Label ID="V2703" runat="server" Text='<%# Bind("V2703")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V2803">
                        <ItemTemplate>
                            <asp:Label ID="V2803" runat="server" Text='<%# Bind("V2803")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V2903">
                        <ItemTemplate>
                            <asp:Label ID="V2903" runat="server" Text='<%# Bind("V2903")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V3003">
                        <ItemTemplate>
                            <asp:Label ID="V3003" runat="server" Text='<%# Bind("V3003")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V3103">
                        <ItemTemplate>
                            <asp:Label ID="V3103" runat="server" Text='<%# Bind("V3103")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V3203">
                        <ItemTemplate>
                            <asp:Label ID="V3203" runat="server" Text='<%# Bind("V3203")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V3303">
                        <ItemTemplate>
                            <asp:Label ID="V3303" runat="server" Text='<%# Bind("V3303")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V3403">
                        <ItemTemplate>
                            <asp:Label ID="V3403" runat="server" Text='<%# Bind("V3403")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V3503">
                        <ItemTemplate>
                            <asp:Label ID="V3503" runat="server" Text='<%# Bind("V3503")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V3603">
                        <ItemTemplate>
                            <asp:Label ID="V3603" runat="server" Text='<%# Bind("V3603")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V3703">
                        <ItemTemplate>
                            <asp:Label ID="V3703" runat="server" Text='<%# Bind("V3703")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V3803">
                        <ItemTemplate>
                            <asp:Label ID="V3803" runat="server" Text='<%# Bind("V3803")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V3903">
                        <ItemTemplate>
                            <asp:Label ID="V3903" runat="server" Text='<%# Bind("V3903")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V4003">
                        <ItemTemplate>
                            <asp:Label ID="V4003" runat="server" Text='<%# Bind("V4003")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Jumlah_Calon">
                        <ItemTemplate>
                            <asp:Label ID="Jumlah_Calon" runat="server" Text='<%# Bind("Jumlah_Calon")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="1%" />
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

            <asp:GridView ID="datRespondent4" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                CellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames="Negeri"
                Width="100%" PageSize="50" CssClass="gridview_footer" EnableModelValidation="True">
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <Columns>

                    <asp:TemplateField HeaderText="Negeri">
                        <ItemTemplate>
                            <asp:Label ID="Negeri" runat="server" Text='<%# Bind("Negeri")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="1%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Tahun">
                        <ItemTemplate>
                            <asp:Label ID="Tahun" runat="server" Text='<%# Bind("Tahun")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="1%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Sesi">
                        <ItemTemplate>
                            <asp:Label ID="Sesi" runat="server" Text='<%# Bind("Sesi")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="1%" />
                    </asp:TemplateField>
                    <%--<asp:TemplateField HeaderText="JumlahCalon">
                        <ItemTemplate>
                            <asp:Label ID="JumlahCalon" runat="server" Text='<%# Bind("JumlahCalon") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="1%" />
                    </asp:TemplateField>--%>
                    <asp:TemplateField HeaderText="V0104">
                        <ItemTemplate>
                            <asp:Label ID="V0104" runat="server" Text='<%# Bind("V0104")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V0204">
                        <ItemTemplate>
                            <asp:Label ID="V0204" runat="server" Text='<%# Bind("V0204")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V0304">
                        <ItemTemplate>
                            <asp:Label ID="V0304" runat="server" Text='<%# Bind("V0304")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V0404">
                        <ItemTemplate>
                            <asp:Label ID="V0404" runat="server" Text='<%# Bind("V0404")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V0504">
                        <ItemTemplate>
                            <asp:Label ID="V0504" runat="server" Text='<%# Bind("V0504")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V0604">
                        <ItemTemplate>
                            <asp:Label ID="V0604" runat="server" Text='<%# Bind("V0604")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V0704">
                        <ItemTemplate>
                            <asp:Label ID="V0704" runat="server" Text='<%# Bind("V0704")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V0804">
                        <ItemTemplate>
                            <asp:Label ID="V0804" runat="server" Text='<%# Bind("V0804")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V0904">
                        <ItemTemplate>
                            <asp:Label ID="V0904" runat="server" Text='<%# Bind("V0904")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V1004">
                        <ItemTemplate>
                            <asp:Label ID="V1004" runat="server" Text='<%# Bind("V1004")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V1104">
                        <ItemTemplate>
                            <asp:Label ID="V1104" runat="server" Text='<%# Bind("V1104")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V1204">
                        <ItemTemplate>
                            <asp:Label ID="V1204" runat="server" Text='<%# Bind("V1204")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V1304">
                        <ItemTemplate>
                            <asp:Label ID="V1304" runat="server" Text='<%# Bind("V1304")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V1404">
                        <ItemTemplate>
                            <asp:Label ID="V1404" runat="server" Text='<%# Bind("V1404")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                   <%-- <asp:TemplateField HeaderText="V1504">
                        <ItemTemplate>
                            <asp:Label ID="V1504" runat="server" Text='<%# Bind("V1504")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>--%>
                    <asp:TemplateField HeaderText="V1604">
                        <ItemTemplate>
                            <asp:Label ID="V1604" runat="server" Text='<%# Bind("V1604")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <%--<asp:TemplateField HeaderText="V1704">
                        <ItemTemplate>
                            <asp:Label ID="V1704" runat="server" Text='<%# Bind("V1704")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>--%>
                    <asp:TemplateField HeaderText="V1804">
                        <ItemTemplate>
                            <asp:Label ID="V1804" runat="server" Text='<%# Bind("V1804")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V1904">
                        <ItemTemplate>
                            <asp:Label ID="V1904" runat="server" Text='<%# Bind("V1904")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V2004">
                        <ItemTemplate>
                            <asp:Label ID="V2004" runat="server" Text='<%# Bind("V2004")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V2104">
                        <ItemTemplate>
                            <asp:Label ID="V2104" runat="server" Text='<%# Bind("V2104")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V2204">
                        <ItemTemplate>
                            <asp:Label ID="V2204" runat="server" Text='<%# Bind("V2204")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V2304">
                        <ItemTemplate>
                            <asp:Label ID="V2304" runat="server" Text='<%# Bind("V2304")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V2404">
                        <ItemTemplate>
                            <asp:Label ID="V2404" runat="server" Text='<%# Bind("V2404")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V2504">
                        <ItemTemplate>
                            <asp:Label ID="V2504" runat="server" Text='<%# Bind("V2504")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V2604">
                        <ItemTemplate>
                            <asp:Label ID="V2604" runat="server" Text='<%# Bind("V2604")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V2704">
                        <ItemTemplate>
                            <asp:Label ID="V2704" runat="server" Text='<%# Bind("V2704")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V2804">
                        <ItemTemplate>
                            <asp:Label ID="V2804" runat="server" Text='<%# Bind("V2804")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V2904">
                        <ItemTemplate>
                            <asp:Label ID="V2904" runat="server" Text='<%# Bind("V2904")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V3004">
                        <ItemTemplate>
                            <asp:Label ID="V3004" runat="server" Text='<%# Bind("V3004")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V3104">
                        <ItemTemplate>
                            <asp:Label ID="V3104" runat="server" Text='<%# Bind("V3104")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V3204">
                        <ItemTemplate>
                            <asp:Label ID="V3204" runat="server" Text='<%# Bind("V3204")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V3304">
                        <ItemTemplate>
                            <asp:Label ID="V3304" runat="server" Text='<%# Bind("V3304")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>



                    <asp:TemplateField HeaderText="V4204">
                        <ItemTemplate>
                            <asp:Label ID="V4204" runat="server" Text='<%# Bind("V4204")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="V4304">
                        <ItemTemplate>
                            <asp:Label ID="V4304" runat="server" Text='<%# Bind("V4304")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>
                    <%--<asp:TemplateField HeaderText="V3604">
                        <ItemTemplate>
                            <asp:Label ID="V3604" runat="server" Text='<%# Bind("V3604")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>--%>
                   <%-- <asp:TemplateField HeaderText="V3704">
                        <ItemTemplate>
                            <asp:Label ID="V3704" runat="server" Text='<%# Bind("V3704")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>--%>
                    <%--<asp:TemplateField HeaderText="V3804">
                        <ItemTemplate>
                            <asp:Label ID="V3804" runat="server" Text='<%# Bind("V3804")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>--%>
                 <%--   <asp:TemplateField HeaderText="V3904">
                        <ItemTemplate>
                            <asp:Label ID="V3904" runat="server" Text='<%# Bind("V3904")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>--%>
                   <%-- <asp:TemplateField HeaderText="V4004">
                        <ItemTemplate>
                            <asp:Label ID="V4004" runat="server" Text='<%# Bind("V4004")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="3%" />
                    </asp:TemplateField>--%>
                    <asp:TemplateField HeaderText="Jumlah_Calon">
                        <ItemTemplate>
                            <asp:Label ID="Jumlah_Calon" runat="server" Text='<%# Bind("Jumlah_Calon")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="1%" />
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
            <asp:Button ID="btnExport" runat="server" Text="Eksport" CssClass="fbbutton" Visible="true" />
        </td>
    </tr>

</table>
<br />
<div class="info" id="divMsg" runat="server">
    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
</div>
