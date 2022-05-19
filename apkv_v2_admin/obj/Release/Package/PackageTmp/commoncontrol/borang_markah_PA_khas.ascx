<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="borang_markah_PA_khas.ascx.vb" Inherits="apkv_v2_admin.borang_markah_PA_khas" %>
<script type="text/javascript">
    function deleteConfirm(pubid) {
        var result = confirm('Anda pasti untuk padam ?');
        if (result) {
            return true;
        }
        else {
            return false;
        }
    }
</script>
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Pemeriksa&gt;&gt;Markah &gt;&gt;Borang Markah Khas</td>
    </tr>
</table>
<br />
<table class="fbform">
    <tr class="fbform_header">
        <td>Senarai Calon Borang Markah Khas</td>
    </tr>
    <tr>
        <td>
            <asp:GridView ID="gridView" DataKeyNames="KhasID" runat="server" AutoGenerateColumns="False" ShowFooter="True" HeaderStyle-Font-Bold="true"
                OnRowDeleting="gridView_RowDeleting" OnRowEditing="gridView_RowEditing" OnRowCommand="gridView_RowCommand" OnRowDataBound="gridView_RowDataBound" CellPadding="4"
                EnableModelValidation="True" ForeColor="#333333" GridLines="None"
                Width="100%" PageSize="40" CssClass="gridview_footer">
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <Columns>
                    <asp:TemplateField HeaderText="KhasID" Visible="False">
                        <ItemTemplate>
                            <asp:Label ID="txtKhasID" runat="server" Text='<%#Eval("KhasID")%>' />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Label ID="lblKhasID" runat="server" Width="10px" Text='<%#Eval("KhasID")%>' />
                        </EditItemTemplate>
                        <FooterTemplate>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Tahun Semasa">
                        <ItemTemplate>
                            <asp:Label ID="lblTahun" runat="server" Text='<%#Eval("Tahun")%>' />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtTahun" Width="100px" runat="server" Text='<%#Eval("Tahun")%>' />
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="inTahun" Width="100px" runat="server" Enabled="false" Text="<%# DateTime.Now.Year %>" />
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Kod Pusat">
                        <ItemTemplate>
                            <asp:Label ID="lblKod" runat="server" Text='<%#Eval("Kod")%>' />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtKod" Width="100px" runat="server" Text='<%#Eval("Kod")%>' />
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="inKod" Width="100px" runat="server" />
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Sesi">
                        <ItemTemplate>
                            <asp:Label ID="lblSesi" runat="server" Text='<%#Eval("Sesi")%>' />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtSesi" Width="100px" runat="server" Text='<%#Eval("Sesi")%>' />
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="inSesi" Width="100px" runat="server" />
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Semester">
                        <ItemTemplate>
                            <asp:Label ID="lblSemester" runat="server" Text='<%#Eval("Semester")%>' />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtSemester" Width="100px" runat="server" Text='<%#Eval("Semester")%>' />
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="inSemester" Width="100px" runat="server" />
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Mykad">
                        <ItemTemplate>
                            <asp:Label ID="lblMykad" runat="server" Text='<%#Eval("Mykad")%>' />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtMykad" Width="100px" runat="server" Text='<%#Eval("Mykad") %>' />
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="inMykad" Width="100px" runat="server" />
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="AngkaGiliran">
                        <ItemTemplate>
                            <asp:Label ID="lblAngkaGiliran" runat="server" Text='<%#Eval("AngkaGiliran")%>' />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtAngkaGiliran" Width="100px" runat="server" Text='<%#Eval("AngkaGiliran")%>' />
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="inAngkaGiliran" Width="100px" runat="server" />
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Markah">
                        <ItemTemplate>
                            <asp:Label ID="lblPAT" runat="server" Text='<%#Eval("PAT")%>' />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtPAT" Width="100px" runat="server" Text='<%#Eval("PAT")%>' />
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="inPAT" Width="100px" runat="server" />

                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Catatan">
                        <ItemTemplate>
                            <asp:Label ID="lblCatatan" runat="server" Text='<%#Eval("Catatan")%>' />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtCatatan" Width="300px" runat="server" Text='<%#Eval("Catatan")%>' />
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="inCatatan" Width="300px" runat="server" />
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <EditItemTemplate>
                        </EditItemTemplate>
                        <ItemTemplate>

                            <asp:Button ID="ButtonDelete" runat="server" CommandName="Delete" Text="Delete" />
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Button ID="ButtonAdd" runat="server" CommandName="AddNew" Text="Add New Row" ValidationGroup="validation" />
                        </FooterTemplate>
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
</table>
<br />
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">&nbsp;<asp:Button ID="btnPrint" runat="server" Text="Cetak Borang Markah Khas" CssClass="fbbutton" Width="293px" Enabled="false" />&nbsp;
        </td>

    </tr>
</table>

<div class="info" id="divMsg" runat="server">
    <asp:Label ID="lblKolejID" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblID" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblType" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblTahun" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblPAT" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblSesi" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblSemester" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="lblMsg" runat="server" Text="System message..."></asp:Label>
</div>
