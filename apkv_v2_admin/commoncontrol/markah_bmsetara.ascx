<%@ control language="vb" autoeventwireup="false" codebehind="markah_bmsetara.ascx.vb" inherits="apkv_v2_admin.markah_bmsetara" %>
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Pemeriksa>>Markah &gt;&gt;Kemasukkan Markah BM Setara</td>
    </tr>
</table>
<br />
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Maklumat Kolej</td>
    </tr>
    <tr>
        <td style="width: 250px">Tahun Peperiksaan:</td>
        <td>
            <asp:dropdownlist id="ddlTahun" runat="server" autopostback="true" width="250px"></asp:dropdownlist>
        </td>
    </tr>
    <tr>
        <td style="width: 250px">Kod Pusat:</td>
        <td><asp:dropdownlist id="ddlKodPusat" runat="server" autopostback="False" width="250px"></asp:dropdownlist>
        </td>
    </tr>
    <tr>
        <td>Sesi:</td>
        <td>
            <asp:checkboxlist id="chkSesi" runat="server" autopostback="true" width="349px" repeatdirection="Horizontal">
                <asp:ListItem>1</asp:ListItem>
                <asp:ListItem>2</asp:ListItem>
            </asp:checkboxlist>
    </tr>
    <tr>
        <td colspan="2">
            <asp:button id="btnCari" runat="server" text="Cari" cssclass="fbbutton" />
            &nbsp;</td>
    </tr>
</table>
<div class="info" id="divMsgResult" runat="server">
    <asp:label id="lblMsgResult" runat="server" text="Mesej..."></asp:label>
</div>
<table class="fbform">
    <tr class="fbform_header">
        <td>Senarai Calon
        </td>
    </tr>
    <tr>
        <td>
            <asp:gridview id="datRespondent" runat="server" autogeneratecolumns="False" allowpaging="True"
                cellpadding="4" forecolor="#333333" gridlines="None" datakeynames="PemeriksaID"
                width="100%" pagesize="12" cssclass="gridview_footer">
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <Columns>
                    <asp:TemplateField HeaderText="#">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Kohort">
                        <ItemTemplate>
                            <asp:Label ID="lblTahun" runat="server" Text='<%# Bind("Tahun")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Semester">
                        <ItemTemplate>
                            <asp:Label ID="lblSemester" runat="server" Text='<%# Bind("Semester")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Sesi">
                        <ItemTemplate>
                            <asp:Label ID="lblSesi" runat="server" Text='<%# Bind("Sesi")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Kertas No">
                        <ItemTemplate>
                            <asp:Label ID="lblKertasNo" runat="server" Text='<%# Bind("KertasNo")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Pilih">
                        <ItemTemplate>
                            <asp:Button ID="btnPilih" runat="server" Text="Pilih" CommandName="Pilih" CommandArgument='<%#Eval("PemeriksaID")%>' />
        </td>
        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
               
        <footerstyle backcolor="#5D7B9D" font-bold="True" forecolor="White" font-underline="true" />
        <pagerstyle backcolor="#284775" forecolor="White" horizontalalign="Center" cssclass="cssPager" />
        <selectedrowstyle backcolor="#E2DED6" font-bold="True" forecolor="#333333" />
        <headerstyle backcolor="#5D7B9D" font-bold="True" forecolor="White" verticalalign="Middle"
            horizontalalign="Left" />
        <editrowstyle backcolor="#999999" />
        <alternatingrowstyle backcolor="White" forecolor="#284775" />
        </asp:GridView>
        </td>
   
    </tr>
    <tr>
        <td>&nbsp;</td>
    </tr>
</table>
<br />
<div class="info" id="divMsg" runat="server">
    <asp:label id="lblUserId" runat="server" text="" visible="false"></asp:label>
    <asp:label id="lblUserType" runat="server" text="" visible="false"></asp:label>
    <asp:label id="lblMsg" runat="server" text="System message..."></asp:label>
</div>
