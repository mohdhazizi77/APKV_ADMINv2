<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="inden_matapelajaran_akademik_negeri.ascx.vb" Inherits="apkv_v2_admin.inden_matapelajaran_akademik_negeri" %>
<table class="fbform" style ="width :100%">
    <tr class="fbform_header">
        <td colspan="3">Analisa Matapelajaran Akademik Mengikut Negeri</td>
    </tr>
    <tr>
           <td style ="width :20%">Kohort:</td>
           <td colspan ="2"><asp:DropDownList ID="ddlTahun" runat="server" Width="150px"></asp:DropDownList></td>
    </tr>
   
    <tr>
           <td style ="width :20%">Semester:</td>
           <td colspan ="2"><asp:DropDownList ID="ddlSemester" runat="server" Width="150px"></asp:DropDownList></td>
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
           <td style ="width :20%"></td>
           <td colspan ="2"><asp:Button ID="btnSearch" runat="server" Text="Jana Analisa" CssClass="fbbutton" /></td>
    </tr>
    
</table>
<br />
<table class="fbform" style ="width :100%">
    <tr class="fbform_header">
        <td>Senarai Analisa</td>
    </tr>
    <tr>
        <td>
            <asp:GridView ID="datRespondent" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                CellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames="Negeri"
                Width="100%" PageSize="100" CssClass="gridview_footer" EnableModelValidation="True">
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <Columns>
                            
                     <asp:TemplateField HeaderText="Negeri">
                        <ItemTemplate>
                            <asp:Label ID="lblNegeri" runat="server" Text='<%# Bind("Negeri")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left"  VerticalAlign="Top"  />
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width ="10%" />
                    </asp:TemplateField>    
                    <asp:TemplateField HeaderText="Bahasa Melayu">
                        <ItemTemplate>
                            <asp:Label ID="BahasaMelayu" runat="server" Text='<%# Bind("BahasaMelayu")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center"  VerticalAlign="Top"  />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width ="10%" />
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Bahasa Inggeris">
                        <ItemTemplate>
                            <asp:Label ID="BahasaInggeris" runat="server" Text='<%# Bind("BahasaInggeris")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center"  VerticalAlign="Top"  />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width ="10%" />
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Matematik">
                        <ItemTemplate>
                            <asp:Label ID="Matematik" runat="server" Text='<%# Bind("Matematik") %>'></asp:Label>
                        </ItemTemplate>
                         <HeaderStyle HorizontalAlign="Center"  VerticalAlign="Top"  />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width ="10%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Sains">
                        <ItemTemplate>
                            <asp:Label ID="Sains" runat="server" Text='<%# Bind("Sains") %>'></asp:Label>
                        </ItemTemplate>
                         <HeaderStyle HorizontalAlign="Center"  VerticalAlign="Top"  />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width ="10%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Sejarah">
                        <ItemTemplate>
                            <asp:Label ID="Sejarah" runat="server" Text='<%# Bind("Sejarah") %>'></asp:Label>
                        </ItemTemplate>
                         <HeaderStyle HorizontalAlign="Center"  VerticalAlign="Top"  />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width ="10%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Pendidikan Islam">
                        <ItemTemplate>
                            <asp:Label ID="PendidikanIslam" runat="server" Text='<%# Bind("PendidikanIslam")%>'></asp:Label>
                        </ItemTemplate>
                         <HeaderStyle HorizontalAlign="Center"  VerticalAlign="Top"  />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width ="10%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Pendidikan Moral">
                        <ItemTemplate>
                            <asp:Label ID="PendidikanMoral" runat="server" Text='<%# Bind("PendidikanMoral")%>'></asp:Label>
                        </ItemTemplate>
                         <HeaderStyle HorizontalAlign="Center"  VerticalAlign="Top"  />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width ="10%" />
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
                Width="100%" PageSize="100" CssClass="gridview_footer" EnableModelValidation="True">
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <Columns>

                              
                     <asp:TemplateField HeaderText="Negeri">
                        <ItemTemplate>
                            <asp:Label ID="lblNegeri" runat="server" Text='<%# Bind("Negeri")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left"  VerticalAlign="Top"  />
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width ="15%" />
                    </asp:TemplateField>   
                    <asp:TemplateField HeaderText="Bahasa Melayu">
                        <ItemTemplate>
                            <asp:Label ID="BahasaMelayu" runat="server" Text='<%# Bind("BahasaMelayu")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center"  VerticalAlign="Top"  />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width ="10%" />
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Bahasa Inggeris">
                        <ItemTemplate>
                            <asp:Label ID="BahasaInggeris" runat="server" Text='<%# Bind("BahasaInggeris")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center"  VerticalAlign="Top"  />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width ="10%" />
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Matematik Untuk Pengajian Sosial">
                        <ItemTemplate>
                            <asp:Label ID="MatematikS" runat="server" Text='<%# Bind("MatematikUntukPengajianSosial") %>'></asp:Label>
                        </ItemTemplate>
                         <HeaderStyle HorizontalAlign="Center"  VerticalAlign="Top"  />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width ="10%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Matematik Untuk Teknologi">
                        <ItemTemplate>
                            <asp:Label ID="MathematicsT" runat="server" Text='<%# Bind("MatematikUntukTeknologi") %>'></asp:Label>
                        </ItemTemplate>
                         <HeaderStyle HorizontalAlign="Center"  VerticalAlign="Top"  />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width ="10%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Sains Untuk Pengajian Sosial">
                        <ItemTemplate>
                            <asp:Label ID="SainsS" runat="server" Text='<%# Bind("SainsUntukPengajianSosial") %>'></asp:Label>
                        </ItemTemplate>
                         <HeaderStyle HorizontalAlign="Center"  VerticalAlign="Top"  />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width ="10%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Sains Untuk Teknologi">
                        <ItemTemplate>
                            <asp:Label ID="SainsT" runat="server" Text='<%# Bind("SainsUntukTeknologi")%>'></asp:Label>
                        </ItemTemplate>
                         <HeaderStyle HorizontalAlign="Center"  VerticalAlign="Top"  />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width ="10%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Sejarah">
                        <ItemTemplate>
                            <asp:Label ID="Sejarah" runat="server" Text='<%# Bind("Sejarah")%>'></asp:Label>
                        </ItemTemplate>
                         <HeaderStyle HorizontalAlign="Center"  VerticalAlign="Top"  />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width ="10%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="PendidikanIslam">
                        <ItemTemplate>
                            <asp:Label ID="PendidikanIslam" runat="server" Text='<%# Bind("PendidikanIslam")%>'></asp:Label>
                        </ItemTemplate>
                         <HeaderStyle HorizontalAlign="Center"  VerticalAlign="Top"  />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width ="10%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="PendidikanMoral">
                        <ItemTemplate>
                            <asp:Label ID="PendidikanMoral" runat="server" Text='<%# Bind("PendidikanMoral")%>'></asp:Label>
                        </ItemTemplate>
                         <HeaderStyle HorizontalAlign="Center"  VerticalAlign="Top"  />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width ="10%" />
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

