<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="prm_muatnaik_bahan.ascx.vb" Inherits="apkv_v2_admin.prm_muatnaik_bahan1" %>
<div>
    <table class="fbform" style="width:100%">
        <tr>
            <td>
                <a href ="muatnaik.bahan.aspx" ><img title="Back" style="vertical-align: middle;" src="icons/back.png" width="20" height="20" alt="::"/></a>
                |<a href="#" runat ="server" id="SaveFunction"><img title="Save" style="vertical-align: middle;" src="icons/save.png" width="20" height="20" alt="::"/></a>
                 |<a href ="#" runat ="server" id ="Refresh" ><img title="Refresh" style="vertical-align: middle;" src="icons/refresh.png" width="20" height="20" alt="::"/></a>
               
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
<div id="addform">
<table class="fbform" style="width :100%">
    <tr>
         <td>Nama Kategori</td>
         <td>:</td>
         <td colspan="4">
             <asp:TextBox ID="txtCategory" runat="server" Width="350px" ></asp:TextBox>
         </td>
    </tr>
    <tr>
         <td style="width: 15%;">Idx</td>
         <td width:1%;">:</td>
         <td colspan="4"><asp:TextBox ID="txtidx" runat="server" Width="100px" ></asp:TextBox></td>
    </tr>
    <tr>
         <td>Keterangan</td>
         <td>:</td>
         <td colspan="4"><asp:TextBox ID="txtDescription" runat="server" Width="350px"></asp:TextBox></td>
    </tr>
    <tr>
         <td>Status</td>
         <td>:</td>
         <td colspan="4">
             <asp:TextBox ID="txtStatus" runat="server" Width="200px" ></asp:TextBox>&nbsp;
             <asp:Label ID ="lblSta" runat ="server" Font-Size ="Smaller" >(1:Aktif , 0:Tidak Aktif)</asp:Label>
         </td>
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
             CellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames="ID"
                Width="100%" PageSize="100" CssClass="gridview_footer">
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <Columns>
                    <asp:TemplateField HeaderText="#">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Kategori" >
                        <ItemTemplate>
                            <asp:Label ID="Parameter" runat="server" Text='<%# Bind("Parameter")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top"  Width ="20%" /><ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Index">
                        <ItemTemplate>
                            <asp:Label ID="idx" runat="server" Text='<%# Bind("idx")%>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign ="Center" />
                        <HeaderStyle HorizontalAlign="center" VerticalAlign="Top" Width ="5%" /><ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Keterangan">
                        <ItemTemplate>
                            <asp:Label ID="Description" runat="server" Text='<%# Bind("Description")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" Width ="35%" /><ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Status">
                        <ItemTemplate>
                            <asp:Label ID="Value" runat="server" Text='<%# Bind("Value")%>'></asp:Label>
                        </ItemTemplate>
                         <ItemStyle HorizontalAlign ="Center" />
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Width ="10%" /><ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Action">
                        <ItemTemplate>

                           <span runat="server" style="float:right">

                           <a href ="prm.muatnaik.bahan.aspx?edit=<%#Eval("ID")%>"><img title="Edit"  src="icons/edit.png" width="13" height="13" alt="::"/></a>
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