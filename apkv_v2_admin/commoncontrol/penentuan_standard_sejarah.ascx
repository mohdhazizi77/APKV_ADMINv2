<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="penentuan_standard_sejarah.ascx.vb" Inherits="apkv_v2_admin.penentuan_standard_sejarah" %>
<div>
    <table class="fbform" style="width:100%">
        <tr>
            <td>
                <a href="#" runat ="server" id="SaveFunction"><img title="Save" style="vertical-align: middle;" src="icons/save.png" width="20" height="20" alt="::"/></a>
                 |<a href ="#" runat ="server" id ="Refresh" ><img title="Refresh" style="vertical-align: middle;" src="icons/refresh.png" width="20" height="20" alt="::"/></a>
               
            </td>
        </tr>
        <tr>
            <td></td>
        </tr>
        <tr class="fbform_header">
            <td>Penetuan Standard Sejarah</td>
        </tr>
    </table>
</div>
<br />
<div id="addform">
<table class="fbform" style="width :100%">
     <tr>
          <td>Kohort:</td>
          <td><asp:DropDownList ID="ddlTahun" runat="server" AutoPostBack="true" Width="150px"></asp:DropDownList></td>
    </tr>
     <tr>
          <td>Sesi:</td>
          <td><asp:CheckBoxList ID="chkSesi" runat="server" AutoPostBack="true" Width="349px" RepeatDirection="Horizontal">
             <asp:ListItem>1</asp:ListItem>
             <asp:ListItem>2</asp:ListItem>
             </asp:CheckBoxList></td>
    </tr>
      <tr>
         <td >Markah Mula:</td>
         <td style="color: #FF0000"><asp:TextBox ID="txtMarkahFrom" runat="server" Width="150px" MaxLength="50"></asp:TextBox>*[-1-100]</td>
    <tr>
         <td>Markah Akhir:</td>
         <td style="color: #FF0000"><asp:TextBox ID="txtMarkahTo" runat="server" Width="150px" MaxLength="50"></asp:TextBox>*[-1-100]</td>
    </tr>
    <tr>
           <td >Gred:</td>
           <td><asp:TextBox ID="txtGred" runat="server" Width="150px" MaxLength="50"></asp:TextBox></td>
   </tr>
    <tr>
         <td >Status:</td>
         <td><asp:TextBox ID="txtStatus" runat="server" Width="150px" MaxLength="50"></asp:TextBox></td>    
    </tr>
    <tr>
           <td >Kompetensi:</td>
           <td><asp:TextBox ID="txtKompetensi" runat="server" Width="250px" MaxLength="50"></asp:TextBox></td>
    </tr>
    <tr>
           <td >Jenis</td>
           <td><asp:TextBox ID="txtJenis" runat="server" Width="250px" MaxLength="50" Enabled="false" Text ="SEJARAH"></asp:TextBox></td>

    </tr>
    
   
</table>
</div>
<br />

<%-- List --%>
<table class="fbform" style ="width :100%">
    <tr class="fbform_header">
        <td> 
            <asp:Label ID="lblConfig" runat="server" Visible ="true"></asp:Label>
        </td>
    </tr>
    <tr>
        <td>
             <asp:Panel ID="Panel" runat="server" ScrollBars="vertical" Height="300" Width="100%">
           <asp:GridView ID="datRespondent" runat="server" AutoGenerateColumns="False" AllowPaging="false"
                CellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames="GSID"
                Width="100%" PageSize="40" CssClass="gridview_footer">
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
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Sesi">
                        <ItemTemplate>
                            <asp:Label ID="Sesi" runat="server" Text='<%# Bind("Sesi")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="Markah Mula">
                        <ItemTemplate>
                            <asp:Label ID="MarkahFrom" runat="server" Text='<%# Bind("MarkahFrom")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Markah Akhir">
                        <ItemTemplate>
                            <asp:Label ID="MarkahTo" runat="server" Text='<%# Bind("MarkahTo")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Gred">
                        <ItemTemplate>
                            <asp:Label ID="Gred" runat="server" Text='<%# Bind("Gred")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Status">
                        <ItemTemplate>
                            <asp:Label ID="Status" runat="server" Text='<%# Bind("Status")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Kompetensi">
                        <ItemTemplate>
                            <asp:Label ID="Kompetensi" runat="server" Text='<%# Bind("Kompetensi")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Action">
                        <ItemTemplate>

                           <span runat="server" style="float:right">

                           <a href ="penentuan.standard.sejarah.aspx?edit=<%#Eval("GSID")%>"><img title="Edit"  src="icons/edit.png" width="13" height="13" alt="::"/></a>
                          | <asp:ImageButton Width ="12" Height ="12" ID="btnDelete" CommandName="Delete" OnClientClick="javascript:return confirm('Rekod akan dipadamkan.Adakah anda pasti?')" runat="server" ImageUrl="~/icons/delete.png" ToolTip="Delete"/>
                        
                            </span> 
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="right" VerticalAlign="Top"  /><ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                   
                     
                </Columns>
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Font-Underline="true" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" CssClass="cssPager" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" VerticalAlign="Middle"
                    HorizontalAlign="Center" />
                <EditRowStyle BackColor="#999999" />
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            </asp:GridView>
                </asp:Panel>
        </td>
    </tr>
    <tr>
        <td colspan="3"></td>
    </tr>
</table>
<br />
<div class="info" id="divMsg" runat="server">
<asp:Label ID="lblMsg" runat="server" Text="Message.."></asp:Label>
</div>