<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="pemeriksa_borang_markah_PA_2.ascx.vb" Inherits="apkv_v2_admin.pemeriksa_borang_markah_PA_2" %>

<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Vokasional >> Daftar Pemeriksa Borang Markah</td>
    </tr>
</table>

<div id="divMsgTop" runat="server"><asp:Label ID="lblMsgTop" runat="server" Text=""></asp:Label></div>
<table class="fbform" style ="width :100%">
    <tr class="fbform_header">
        <td colspan="3">Maklumat Kolej <span style ="font-size :10px;color :darkblue "> (Optional)</span></td>
    </tr>
    <tr>
        <td style ="width :15%">Kod Pusat</td>
        <td style ="width :2px">:</td>
        <td>
            <asp:DropDownList ID="ddlKodPusat" runat="server" AutoPostBack="true" Width="250px"></asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>Nama Kolej</td>
        <td>:</td>
        <td>
            <asp:Label ID="lblNamaKolej" runat="server" Text=""></asp:Label>
        </td>
    </tr>
</table>
<br />





<table class="fbform" style ="width :100%">
    <tr class="fbform_header">
        <td colspan="3">Maklumat Perincian Carian</td>
    </tr>
    <tr>
        <td style ="width :15%">Kohort</td>
        <td style ="width :2px">:</td>
        <td>
            <asp:DropDownList ID="ddlTahun" runat="server" AutoPostBack="false" Width="250px"></asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>Semester</td>
        <td>:</td>
        <td>
            <asp:DropDownList ID="ddlSemester" runat="server" AutoPostBack="false" Width="250px"></asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>Sesi</td>
        <td>:</td>
        <td>
            <asp:CheckBoxList ID="chkSesi" runat="server" AutoPostBack="true" Width="349px" RepeatDirection="Horizontal">
             <asp:ListItem Value ="1">1</asp:ListItem>
             <asp:ListItem Value ="2">2</asp:ListItem>
             <asp:ListItem Enabled ="false" Value =""></asp:ListItem>

             </asp:CheckBoxList>
         </td>
    </tr>
    <tr>
        <td>Kod Program</td>
        <td>:</td>
        <td>
            <asp:DropDownList ID="ddlKodKursus" runat="server" Width="250px"></asp:DropDownList>
        </td>
    </tr>
    <tr><td colspan ="3"></td></tr>
    <tr>
        <td></td>
        <td colspan ="3"><asp:Button ID="btnCari" runat="server" Text="Cari" CssClass="fbbutton" Width ="150px" /></td>      
    </tr>
      </table>
<br/>

<table class="fbform" style ="width :100%">
    <tr class="fbform_header">
        <td>Senarai Pemeriksa</td>
    </tr>
    <tr>
    <td>
            <asp:GridView ID="datRespondent" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                CellPadding="10" ForeColor="#333333" GridLines="None" DataKeyNames="PemeriksaID"
                Width="100%" PageSize="5" CssClass="gridview_footer">
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
                            <asp:Label ID="lblKohort" runat="server" Text='<%# Bind("Tahun")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" /><ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Semester">
                        <ItemTemplate>
                            <asp:Label ID="lblSemester" runat="server" Text='<%# Bind("Semester")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" /><ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Sesi Pengambilan">
                        <ItemTemplate>
                            <asp:Label ID="lblSesi" runat="server" Text='<%# Bind("Sesi")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" /><ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Nama Pemeriksa">
                        <ItemTemplate>
                            <asp:Label ID="lblNamaPemeriksa" runat="server" Text='<%# Bind("NamaPemeriksa")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" /><ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Kod Pusat">
                        <ItemTemplate>
                            <asp:Label ID="lblKodPusat" runat="server" Text='<%# Bind("KodKolej")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" /><ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Kod Kursus">
                        <ItemTemplate>
                            <asp:Label ID="lblKodKursus" runat="server" Text='<%# Bind("KodKursus")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" /><ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Batal"> 
                        <ItemTemplate> 
                            <asp:Button ID="btnBatal" runat="server" Text="-" CommandName="Batal" CommandArgument ='<%#Eval("PemeriksaID")%>'/></td>
                        </ItemTemplate> 
                    </asp:TemplateField> 
                    </Columns>
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Font-Underline="true" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" CssClass="cssPager" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Left" />
                <EditRowStyle BackColor="#999999" />
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            </asp:GridView>
        </td>
        </tr>
</table>
<br />

<table class="fbform" style ="width :100%">
     <tr class="fbform_header">
        <td>Pemilihan Pemeriksa : <asp:DropDownList ID="ddlPemeriksa" runat="server" AutoPostBack="false" Width="350px"></asp:DropDownList>
            &nbsp;<asp:Button ID="btnSimpan" runat="server" Text="Simpan" CssClass="fbbutton"  Width ="150px"  />

        </td>
    </tr>
</table>
<br />

<table class="fbform" style ="width :100%">
    <tr class="fbform_header">
        <td>Senarai Pusat</td>
    </tr>
    <tr>
         <td>
             <asp:Panel runat ="server" ScrollBars="Vertical" Height ="300px">
            <asp:GridView ID="datRespondent2" runat="server" AutoGenerateColumns="False" AllowPaging="False"
                CellPadding="10" ForeColor="#333333" GridLines="None" DataKeyNames="TxnKursusKolejID"
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
                    <asp:TemplateField HeaderText="Klik">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkSelect" runat="server" />
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="KodPusat">
                        <ItemTemplate>
                            <asp:Label ID="lblKodPusat" runat="server" Text='<%# Bind("Kod")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" /><ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="KodKursus">
                        <ItemTemplate>
                            <asp:Label ID="lblKodKursus" runat="server" Text='<%# Bind("KodKursus")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" /><ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Kolej">
                        <ItemTemplate>
                            <asp:Label ID="lblNamaKolej" runat="server" Text='<%# Bind("Nama")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" /><ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    
              </Columns>
              <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Font-Underline="true" />
              <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" CssClass="cssPager" />
              <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
              <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Left" />
              <EditRowStyle BackColor="#999999" />
              <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            </asp:GridView>
             
             </asp:Panel>
        </td>
    </tr>
</table>
<br />

<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Import Pemeriksa
        </td>
    </tr>
    <tr>
        <td style="width: 200px">MuatNaik Format Fail Excel : </td>
        <td>
            <asp:Button ID="btnFile" runat="server" Text="Excel" CssClass="fbbutton" Height="25px" Width="100px" /></td>
    </tr>
    <tr>
        <td>Pilih Fail Excel:</td>

        <td>
            <asp:FileUpload ID="FlUploadcsv" runat="server" />&nbsp;
           
            <asp:RegularExpressionValidator ID="regexValidator" runat="server" ErrorMessage="Only XLSX file are allowed"
                ValidationExpression="(.*\.([Xx][Ll][Ss][Xx])$)" ControlToValidate="FlUploadcsv"></asp:RegularExpressionValidator>
        </td>
    </tr>
    <tr>
        <td>&nbsp</td>
    </tr>
    <tr>
        <td></td>
        <td colspan="2">
            <asp:Button ID="btnUpload" runat="server" Text="Muatnaik" CssClass="fbbutton" Style="height: 26px; width: 100px" />
        </td>
    </tr>
</table>

<div class="info" id="divMsg" runat="server">
 <asp:Label ID="lblmsg" runat="server" Text=""></asp:Label>
</div>