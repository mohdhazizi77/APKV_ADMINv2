<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="senarai_muatnaik_bahan.ascx.vb" Inherits="apkv_v2_admin.senarai_muatnaik_bahan1" %>

<table class="fbform" style ="width :100%">
    <tr class="fbform_header">
        <td colspan="2">Pengesahan Muatnaik Bahan</td>
    </tr>
</table>
<br />
<table class="fbform" style="width :100%">
    <tr>
        <td style="width: 20%;">Kategori:</td>
        <td><asp:DropDownList ID="ddlKategory" runat="server" Width="400px" ></asp:DropDownList></td>
    </tr>
     <tr>
        <td>Kohort:</td>
        <td><asp:DropDownList ID="ddlKohort" runat="server" Width="100px" ></asp:DropDownList></td>
    </tr>
    <tr>
         <td>Semester:</td>
         <td> <asp:DropDownList ID="ddlSemester" runat="server"  Width="100px" ></asp:DropDownList></td>
    </tr>
     <tr>
         <td>Sesi Pengambilan:</td>
             <td><asp:DropDownList ID="ddlSesi" runat="server" Width="100px">
                <asp:ListItem Value ="">-Pilih-</asp:ListItem>
                <asp:ListItem Value ="1">1</asp:ListItem>
                <asp:ListItem Value ="2">2</asp:ListItem>
             </asp:DropDownList>
         </td>
    </tr> 
    <tr>
        <td>Komponen:</td>
        <td><asp:DropDownList ID ="ddlKomponen" runat ="server" AutoPostBack ="true" >
            <asp:ListItem Value ="">-Pilih-</asp:ListItem>
            <asp:ListItem Value ="UMUM">UMUM</asp:ListItem>
            <asp:ListItem Value ="VOKASIONAL" >VOKASIONAL</asp:ListItem>
            <asp:ListItem Value ="AKADEMIK">AKADEMIK</asp:ListItem>
            </asp:DropDownList>

             &nbsp; <asp:DropDownList ID ="ddlKodkursus" runat ="server" Width ="100" AutoPostBack ="true" ></asp:DropDownList>
            
        </td>
    </tr>
    <tr>
        <td></td>
        <td ><asp:CheckBox ID ="chkstatus" runat ="server" /> Senarai Semua Status Pengesahan</td>
    </tr>
    <tr>
        <td colspan ="2"></td>
    </tr>
    <tr>
         <td colspan="2">
            <asp:Button ID="btnSearch" runat="server" Text="Cari " CssClass="fbbutton" Width ="100" /></td>
    </tr>
    </table>
<br />
<table class="fbform" style="width :100%">
    <tr class="fbform_header">
        <td colspan="2">Senarai Muat Naik Bahan</td>
    </tr>
    <tr>
        <td>
<asp:Panel Width ="100%" Height ="450%" ScrollBars="Vertical" runat ="server"  >
<asp:GridView ID="datRespondent" runat="server" AutoGenerateColumns="False" AllowPaging="false"
cellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames="ID"
Width="100%" PageSize="10" CssClass="gridview_footer" EnableModelValidation="True" OnRowCommand ="datRespondent_RowCommand">
<RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
<Columns>
        <asp:TemplateField HeaderText="#">
            <ItemTemplate>
                <%# Container.DataItemIndex + 1 %>
            </ItemTemplate>
            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
            <ItemStyle VerticalAlign="Middle" />
        </asp:TemplateField>
    <asp:TemplateField >
            <HeaderTemplate >
                <asp:CheckBox ID="chkAll" runat="server" AutoPostBack="true" OnCheckedChanged="CheckUncheckAll" />
            </HeaderTemplate>
        <ItemTemplate>
            <asp:CheckBox ID="chkSelect"  runat="server" Checked ='<%# If(Eval("isVerified").ToString() = "Y", "True", "False") %>' />
        </ItemTemplate>
            <HeaderStyle HorizontalAlign="Left"/>
        <ItemStyle VerticalAlign="Middle" />
    </asp:TemplateField>  
      <asp:TemplateField HeaderText="Komen Kesalahan">
            <ItemTemplate>
                 <asp:TextBox ID="Komen" runat="server" Text='<%# Bind("Komen")%>' TextMode="MultiLine" Width="150px"></asp:TextBox>
            </ItemTemplate>
            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" /><ItemStyle VerticalAlign="Middle" />
        </asp:TemplateField>
    <asp:TemplateField HeaderText="Mula">
            <ItemTemplate>
                <asp:Label ID="STarikh" runat="server" Text='<%# Bind("STarikh")%>'></asp:Label>
            </ItemTemplate>
            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" /><ItemStyle VerticalAlign="Middle" />
        </asp:TemplateField>
    <asp:TemplateField HeaderText="Tamat">
            <ItemTemplate>
                <asp:Label ID="ETarikh" runat="server" Text='<%# Bind("ETarikh")%>'></asp:Label>
            </ItemTemplate>
            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" /><ItemStyle VerticalAlign="Middle" />
        </asp:TemplateField>
            <asp:TemplateField HeaderText="Kohort">
            <ItemTemplate>
                <asp:Label ID="Kohort" runat="server" Text='<%# Bind("Kohort")%>'></asp:Label>
            </ItemTemplate>
            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" /><ItemStyle VerticalAlign="Middle" HorizontalAlign ="Center"  />
        </asp:TemplateField>
            <asp:TemplateField HeaderText="Sem">
            <ItemTemplate>
                <asp:Label ID="Semester" runat="server" Text='<%# Bind("Semester")%>'></asp:Label>
            </ItemTemplate>
            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" /><ItemStyle VerticalAlign="Middle" HorizontalAlign ="Center"  />
        </asp:TemplateField>
            <asp:TemplateField HeaderText="Sesi">
            <ItemTemplate>
                <asp:Label ID="Sesi" runat="server" Text='<%# Bind("Sesi")%>'></asp:Label>
            </ItemTemplate>
            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" /><ItemStyle VerticalAlign="Middle" HorizontalAlign ="Center"  />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="KodKursus">
            <ItemTemplate>
                <asp:Label ID="KodKursus" runat="server" Text='<%# Bind("KodKursus")%>'></asp:Label>
            </ItemTemplate>
            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" /><ItemStyle VerticalAlign="Middle" HorizontalAlign ="Center"  />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Tajuk">
            <ItemTemplate>
                <asp:TextBox ID="Tajuk" runat="server" Text='<%# Bind("Tajuk")%>' TextMode="MultiLine" Width="150px" Enabled ="false" ></asp:TextBox>
            </ItemTemplate>
            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" /><ItemStyle VerticalAlign="Middle" />
        </asp:TemplateField>
            <asp:TemplateField HeaderText="Catatan">
            <ItemTemplate>
                 <asp:TextBox ID="Catatan" runat="server" Text='<%# Bind("Catatan")%>' TextMode="MultiLine" Width="150px" Enabled ="false" ></asp:TextBox>
            </ItemTemplate>
            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" /><ItemStyle VerticalAlign="Middle" />
        </asp:TemplateField>
     <asp:TemplateField HeaderText="Muat Turun">
            <ItemTemplate>
                <asp:Label ID="lblID" runat="server" Text='<%# Bind("ID")%>' Visible ="false" ></asp:Label>
                <asp:ImageButton Width ="20" Height ="20" ID="btnDownload" CommandName="DownloadBahan" CommandArgument="<%# Container.DataItemIndex %>"  runat="server" ImageUrl="~/icons/download.gif" ToolTip="Muatturun"/>
            </ItemTemplate>
            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" /><ItemStyle VerticalAlign="Middle" HorizontalAlign ="Center"  />
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
    <tr>
        <td>&nbsp;</td>
    </tr>
    <tr>
         <td colspan ="2"><asp:Button ID ="btnSave" runat ="server" Text ="Simpan" CssClass="fbbutton" />
             &nbsp;<asp:Label ID ="lblSta" runat ="server" Font-Size ="Smaller" ForeColor ="Red"  >(Tandakan Checkbox untuk sahkan bahan muatnaik)</asp:Label>
         </td>
    </tr>
</table>
<div class="info" id="divMsg" runat="server">
<asp:Label ID="lblKolejID" runat="server" Text="" Visible="false"></asp:Label>
<asp:Label ID="lblJenisKursus" runat="server" Text="" Visible="false"></asp:Label>
<asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
</div>