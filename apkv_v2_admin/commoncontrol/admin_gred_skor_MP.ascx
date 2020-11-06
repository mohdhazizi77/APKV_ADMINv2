<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="admin_gred_skor_MP.ascx.vb" Inherits="apkv_v2_admin.admin_gred_skor_MP" %>
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Jana Gred MataPelajaran Vokasional</td>
    </tr>
</table>
<br />
<table class="fbform" style ="width :100%">
    <tr class="fbform_header">
        <td colspan="3">Carian </td>
    </tr>
      <tr>
        <td style ="width :15%">Negeri</td>
        <td style="width :2%">:</td>
        <td><asp:DropDownList ID="ddlNegeri" runat="server" AutoPostBack="true"  Width="350px"></asp:DropDownList></td>
    </tr>
    <tr>
        <td>Kohort</td>
        <td>:</td>
        <td><asp:DropDownList ID="ddlTahun" runat="server" AutoPostBack="false" Width="100px"></asp:DropDownList></td>  
    </tr>
    <tr>
        <td >Semester</td>
        <td>:</td>
    <td>
            <asp:DropDownList ID="ddlSemester" runat="server" AutoPostBack="false" Width="350px"></asp:DropDownList>
        </td>
    </tr>
       <tr>
         <td style="width: 20%;">Sesi Pengambilan</td>
          <td>:</td>
           <td><asp:CheckBoxList ID="chkSesi" runat="server" AutoPostBack="false" Width="349px" RepeatDirection="Horizontal">
             <asp:ListItem>1</asp:ListItem>
             <asp:ListItem>2</asp:ListItem>
             </asp:CheckBoxList>
    </tr>
    <tr>      
        <td colspan ="3"> <asp:Button ID="btnSearch" runat="server" Text="Cari " CssClass="fbbutton" /></td>
    </tr>
 </table> 
<br />

<table class="fbform" style ="width :100%">
    <tr class="fbform_header">
     <td style ="width :15%">Jumlah Pelajar : <asp:Label ID="lblCountPelajar" runat="server" Text=""></asp:Label></td>
     <td><asp:Button ID="btnGred" runat="server" Text="Jana Keseluruhan Gred " CssClass="fbbutton" /></td>
    </tr>
    <tr>
        <td>Nama Kolej:</td>
        <td><asp:DropDownList ID="ddlKolej" runat="server" AutoPostBack="true" Width="350px"></asp:DropDownList></td>
    </tr>
    <tr>
          <td >Kod Program:</td>
        <td>
            <asp:DropDownList ID="ddlKodKursus" runat="server" AutoPostBack="false" Width="350px"></asp:DropDownList>&nbsp;
            <asp:Button ID="btnJana" runat="server" Text="Jana" CssClass="fbbutton" Width ="10%" />
        </td>
    </tr>
    <tr class="fbform_header">
        <td colspan ="2">Senarai Jana Bermasalah Mengikut Kolej</td>
    </tr>
    <tr>
        <td colspan ="2">
        <asp:GridView ID="datRespondent" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                CellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames="namaKolej"
                Width="100%" PageSize="100" CssClass="gridview_footer" EnableModelValidation="True" 
                Font-Names="Arial" Font-Size="Small">
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <Columns>
                    <asp:TemplateField HeaderText="#">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="NamaKolej">
                        <ItemTemplate>
                            <asp:Label ID="namaKolej" runat="server" Text='<%# Bind("namaKolej")%>'></asp:Label>
                        </ItemTemplate>
                     </asp:TemplateField>
                     <asp:TemplateField HeaderText="KodKursus">
                        <ItemTemplate>
                            <asp:Label ID="kodkursus" runat="server" Text='<%# Bind("kodkursus")%>'></asp:Label>
                        </ItemTemplate>
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="Jumlah Pelajar">
                        <ItemTemplate>
                            <asp:Label ID="jumlahPelajar" runat="server" Text='<%# Bind("jumlahPelajar")%>'></asp:Label>
                        </ItemTemplate>
                           <HeaderStyle/>
                        <ItemStyle/>
                     </asp:TemplateField>
                     <asp:TemplateField HeaderText="SkorKursus">
                        <ItemTemplate>
                            <asp:Label ID="smpTotal" runat="server" Text='<%# Bind("smpTotal")%>'></asp:Label>
                        </ItemTemplate>
                       <HeaderStyle/>
                        <ItemStyle/>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="GredKursus">
                        <ItemTemplate>
                            <asp:Label ID="smpGrade" runat="server" Text='<%# Bind("smpGrade")%>'></asp:Label>
                        </ItemTemplate>
                       <HeaderStyle/>
                        <ItemStyle />
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
    
    <tr> <td colspan="2">
          <div class="info" id="divMsg" runat="server">
           <asp:Label ID="lblMsg" runat="server" Text="Mesej..."></asp:Label>
            <asp:Label ID="lblCOUNTP" runat="server" Text=""></asp:Label>

         </div>
         </td></tr>
    </table>
<br />
