<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="muatnaik_bahan.ascx.vb" Inherits="apkv_v2_admin.muatnaik_bahan" %>
<%-- <%--action --%>
<script src="http://ajax.googleapis.com/ajax/libs/jquery/1.6/jquery.min.js" type="text/javascript"></script>
<script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/jquery-ui.min.js" type="text/javascript"></script>
<link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/base/jquery-ui.css" rel="Stylesheet" type="text/css" />

<script type="text/JavaScript">
    $(document).ready(function () {
        $('#<%= txtDateStart.ClientID%>').datepicker({ dateFormat: 'dd-mm-yy' }).val();
    });

    $(document).ready(function () {
        $('#<%= txtDateEnd.ClientID%>').datepicker({ dateFormat: 'dd-mm-yy' }).val();
    });

</script>
<div>
    <table class="fbform" style="width:100%">
        <tr>
            <td>
                <a href ="#" runat ="server" id ="Refresh" ><img title="Refresh" style="vertical-align: middle;" src="icons/refresh.png" width="20" height="20" alt="::"/></a>
            </td>
        </tr>
        <tr>
            <td></td>
        </tr>
        <tr class="fbform_header">
            <td>Konfigurasi</td>
        </tr>
    </table>
</div>
<br />
<table class="fbform" style="width :100%">
    <tr>
        <td style="width: 20%;">Kategori:</td>
        <td style="width: 30%;"><asp:DropDownList ID="ddlKategory" runat="server" Width="400px"></asp:DropDownList></td>
        <td colspan ="2"><a href="prm.muatnaik.bahan.aspx?p=KATEGORIMUATNAIKBAHAN" style ="color :green; text-decoration :underline">[+]Konfigurasi</a></td>
    </tr>
     <tr>
        <td>Kohort:</td>
        <td><asp:DropDownList ID="ddlKohort" runat="server" Width="100px"></asp:DropDownList>
            &nbsp;<asp:Label ID ="lblSta" runat ="server" Font-Size ="Smaller" >(Label Tahun digunakan untuk komponen "UMUM")</asp:Label>
        </td>
        <td colspan ="2"></td>
    </tr>
    <tr>
         <td>Semester:</td>
         <td> <asp:DropDownList ID="ddlSemester" runat="server"  Width="100px" AutoPostBack ="true" ></asp:DropDownList></td>
         <td colspan ="2"></td>
    </tr>
     <tr>
         <td>Sesi Pengambilan:</td>
             <td><asp:DropDownList ID="ddlSesi" runat="server" Width="100px" AutoPostBack ="true">
                <asp:ListItem Value ="">-Pilih-</asp:ListItem>
                <asp:ListItem Value ="1">1</asp:ListItem>
                <asp:ListItem Value ="2">2</asp:ListItem>
             </asp:DropDownList>
         </td>
         <td colspan ="2"></td>
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
            &nbsp; <asp:DropDownList ID ="ddlJenisKursus" runat ="server" Width ="100">
                    <asp:ListItem Value ="">-JenisKursus-</asp:ListItem>
                    <asp:ListItem Value ="SOCIAL" >SOCIAL</asp:ListItem>
                    <asp:ListItem Value ="TECHNOLOGY">TECHNOLOGY</asp:ListItem>
                   </asp:DropDownList>
        </td>
    </tr>
    <tr>
       <td>Tajuk:</td>
       <td><asp:TextBox ID="txttitle" runat="server" TextMode="MultiLine" Width="400px"></asp:TextBox></td>
       <td colspan ="2"></td>
    </tr>
    <tr>
        <td>Catatan:</td>
        <td><asp:TextBox ID="txtDesc" runat="server"  Height="70px" Rows="1" TextMode="MultiLine" Width="400px"></asp:TextBox></td>
        <td colspan ="2"></td>
    <tr>
         <td>Tarikh Mula:</td>
         <td><asp:TextBox ID ="txtDateStart" runat ="server" Width ="100"></asp:TextBox>
             <img title="Kalendar : Tekan pada ruangan sebelah" style="vertical-align: middle;" src="icons/calendar.png" width="11" height="11" alt="::"/> 

             &nbsp;&nbsp;&nbsp;&nbsp;
             Tarikh Akhir: <asp:TextBox ID="txtDateEnd" runat="server" Width ="100"></asp:TextBox>
             <img title="Kalendar : Tekan pada ruangan sebelah" style="vertical-align: middle;" src="icons/calendar.png" width="11" height="11" alt="::"/>
        
         </td>
          
        <td colspan ="2"></td>
    </tr>
    <tr>
        <td colspan ="4"></td>
    </tr>
     <tr>
        <td colspan="4">
            <asp:Button ID="btnSearch" runat="server" Text="Cari" CssClass="fbbutton" Style="height: 26px;width :130px" />
        </td>
    </tr>
    
    </table>
<br />
<div id="divImport" runat="server">
<table class="fbform" style ="width :100%">
    <tr class="fbform_header">
        <td colspan="2">Muat Naik Format Fail</td>
    </tr>
    <tr>
        <td style="width: 20%;">Pilih Fail PDF: </td>
         <td>
            <asp:FileUpload ID="FlUploadpdf" runat="server" />
        </td>
    </tr>
    <tr>
        <td>Perlukan Pengesahan Muat Naik?</td>
        <td><asp:CheckBox ID ="chkVerified" runat ="server" Checked ="true"  />Ya</td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:Button ID="btnUpload" runat="server" Text="Muat Naik " CssClass="fbbutton" Style="height: 26px" />
        </td>
    </tr>
    </table>
    </div> 
<div class="info" id="divMsg" runat="server">
    <asp:Label ID="lblMsg" runat="server" Text="Mesej..."></asp:Label>
</div>
<br />
<table class="fbform" style="width :100%">
    <tr class="fbform_header">
        <td colspan="2">Senarai Muat Naik Bahan</td>
    </tr>
    <tr>
        <td>
<asp:Panel Width ="100%" Height ="450%" ScrollBars="Vertical" runat ="server"  >
<asp:GridView ID="datRespondent" runat="server" AutoGenerateColumns="False"
cellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames="ID"
Width="100%" CssClass="gridview_footer" EnableModelValidation="True" OnRowCommand ="datRespondent_RowCommand" OnRowDataBound ="datRespondent_RowDataBound">
<RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
<Columns>
        <asp:TemplateField HeaderText="#">
            <ItemTemplate>
                <%# Container.DataItemIndex + 1 %>
            </ItemTemplate>
            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
            <ItemStyle VerticalAlign="Middle" />
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
            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" /><ItemStyle VerticalAlign="Middle"  HorizontalAlign ="Center" />
        </asp:TemplateField>
            <asp:TemplateField HeaderText="Semester">
            <ItemTemplate>
                <asp:Label ID="Semester" runat="server" Text='<%# Bind("Semester")%>'></asp:Label>
            </ItemTemplate>
            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" /><ItemStyle VerticalAlign="Middle"  HorizontalAlign ="Center" />
        </asp:TemplateField>
            <asp:TemplateField HeaderText="Sesi">
            <ItemTemplate>
                <asp:Label ID="Sesi" runat="server" Text='<%# Bind("Sesi")%>'></asp:Label>
            </ItemTemplate>
            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" /><ItemStyle VerticalAlign="Middle"  HorizontalAlign ="Center" />
        </asp:TemplateField>
    <asp:TemplateField HeaderText="KodKursus">
            <ItemTemplate>
                <asp:Label ID="KodKursus" runat="server" Text='<%# Bind("KodKursus")%>'></asp:Label>
            </ItemTemplate>
            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" /><ItemStyle VerticalAlign="Middle" HorizontalAlign ="Center"   />
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
                <asp:ImageButton Width ="16" Height ="16" ID="btnDownload"  CommandName="DownloadBahan" CommandArgument="<%# Container.DataItemIndex %>"  runat="server" ImageUrl="~/icons/download.gif" ToolTip="Muatturun"/>
            </ItemTemplate>
            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" /><ItemStyle VerticalAlign="Middle" HorizontalAlign ="Center"  />
        </asp:TemplateField>
     <asp:TemplateField HeaderText="Kesalahan">
            <ItemTemplate>
                 <asp:TextBox ID="Komen" runat="server" Text='<%# Bind("Komen")%>' TextMode="MultiLine" Width="150px" Enabled ="false" ></asp:TextBox>
            </ItemTemplate>
            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" /><ItemStyle VerticalAlign="Middle" />
        </asp:TemplateField>
                <asp:TemplateField HeaderText="Padam">
            <ItemTemplate>
                <asp:ImageButton Width ="12" Height ="12" ID="btnDelete" CommandName="Delete" OnClientClick="javascript:return confirm('Anda pasti untuk padam rekod ini? Pemadaman yang dilakukan tidak boleh diubah')" runat="server" ImageUrl="~/icons/download.jpg" ToolTip="Padam Rekod"/>
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
   
</table>

<div>
    <asp:Label ID ="lblAction" runat ="server" Visible ="false" ></asp:Label>
</div>