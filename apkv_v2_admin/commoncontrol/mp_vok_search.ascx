<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="mp_vok_search.ascx.vb" Inherits="apkv_v2_admin.mp_vok_search" %>
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Carian MataPelajaran</td>
       
    </tr>
    <tr>
        <td>Kohort:</td>
        <td><asp:DropDownList ID="ddlTahun" runat="server" AutoPostBack="false" Width="350px"></asp:DropDownList></td>
       
    </tr>
    <tr>
        <td>Semester:</td>
        <td>
            <asp:DropDownList ID="ddlSemester" runat="server" AutoPostBack="false" Width="350px">
            </asp:DropDownList>
        </td>

    </tr>
     <tr>
         <td style="width: 20%;">Sesi Pengambilan:</td>
        <td><asp:CheckBoxList ID="chkSesi" runat="server"  Width="349px" RepeatDirection="Horizontal" AutoPostBack ="true" >
             <asp:ListItem>1</asp:ListItem>
             <asp:ListItem>2</asp:ListItem>
             </asp:CheckBoxList></td>
    </tr>
    <tr>
        <td>Kod Kursus:
        </td>
        <td>
            <asp:DropDownList ID="ddlKodKursus" runat="server" AutoPostBack="false" Width="350px">
            </asp:DropDownList>
        </td>

    </tr>
    <tr>
        <td>Kod MataPelajaran:</td>
        <td><asp:DropDownList ID="ddlKodMataPelajaran" runat="server" AutoPostBack="false" Width="350px"></asp:DropDownList></td>

    </tr>
    <tr>
        <td>Nama MataPelajaran:</td>
        <td><asp:TextBox ID="txtNamaMataPelajaran" runat="server" Width="350px" MaxLength="250"></asp:TextBox></td>


    </tr>
    <tr>
        <td class="fbform_sap" colspan="2">&nbsp;<asp:Button ID="btnSearch" runat="server" Text="Cari " CssClass="fbbutton" /></td>
       
    </tr>
        </table>
<br />
<div class="info" id="divMsgTop" runat="server">
<asp:Label ID="lblMsgTop" runat="server" Text=""></asp:Label>
</div>
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Senarai MataPelajaran</td>
    
    </tr>
    <tr>
        <td colspan="2">
            <asp:GridView ID="datRespondent" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                CellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames="MPVID"
                Width="100%" PageSize="25" CssClass="gridview_footer">
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
                            <asp:Label ID="Tahun" runat="server" Text='<%# Bind("Tahun")%>'></asp:Label>
                        </ItemTemplate>
                         <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" /><ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                  
                    <asp:TemplateField HeaderText="Semester">
                        <ItemTemplate>
                            <asp:Label ID="Semester" runat="server" Text='<%# Bind("Semester")%>'></asp:Label>
                        </ItemTemplate>
                         <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" /><ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Sesi">
                        <ItemTemplate>
                            <asp:Label ID="Sesi" runat="server" Text='<%# Bind("Sesi")%>'></asp:Label>
                        </ItemTemplate>
                         <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" /><ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="KodKursus">
                        <ItemTemplate>
                            <asp:Label ID="KodKursus" runat="server" Text='<%# Bind("KodKursus")%>'></asp:Label>
                        </ItemTemplate>
                         <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" /><ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="KodMataPelajaran">
                        <ItemTemplate>
                            <asp:Label ID="KodMPVok" runat="server" Text='<%# Bind("KodMPVok")%>'></asp:Label>
                        </ItemTemplate>
                         <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" /><ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Nama Matapelajaran">
                        <ItemTemplate>
                            <asp:Label ID="NamaMPVok" runat="server" Text='<%# Bind("NamaMPVok")%>'></asp:Label>
                        </ItemTemplate>
                         <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" /><ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                      <asp:CommandField SelectText="[PILIH]" ShowSelectButton="True" HeaderText="PILIH" />
                    <asp:TemplateField HeaderText="Padam">
                        <ItemTemplate>
                            <asp:ImageButton Width ="12" Height ="12" ID="btnDelete" CommandName="Delete" OnClientClick="javascript:return confirm('Anda pasti untuk padam rekod ini? Pemadaman yang dilakukan tidak boleh diubah')" runat="server" ImageUrl="~/icons/download.jpg" ToolTip="Padam Rekod"/>
                        </ItemTemplate>
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
        <td colspan="2"></td>
       
    </tr>
  
</table>
<br />
<div class="info" id="divMsg" runat="server">
    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label><br />
</div>