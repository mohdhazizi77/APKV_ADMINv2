<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="penentuan_standard_vok.ascx.vb" Inherits="apkv_v2_admin.penentuan_standard_vok" %>
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
            <td>Penetuan Standard Matapelajaran Vokasional</td>
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
           <td >Semester:</td>
             <td> <asp:DropDownList ID="ddlSemester" runat="server" Width="100px">
            </asp:DropDownList>
        </td>
    </tr>
     <tr>
          <td>Sesi:</td>
          <td><asp:CheckBoxList ID="chkSesi" runat="server" AutoPostBack="true" Width="349px" RepeatDirection="Horizontal">
             <asp:ListItem>1</asp:ListItem>
             <asp:ListItem>2</asp:ListItem>
             </asp:CheckBoxList></td>
    </tr>
    <tr>
         <td>SMP PB:</td>
         <td><asp:TextBox ID="txtSmpPB" runat="server" Width="100px"></asp:TextBox></td>
    </tr>
    <tr>
         <td>SMP PAA:</td>
         <td><asp:TextBox ID="txtSmpPAA" runat="server" Width="100px"></asp:TextBox></td>
    </tr>
    <tr>
         <td>SMP PAT:</td>
         <td><asp:TextBox ID="txtSmpPAT" runat="server" Width="100px"></asp:TextBox></td>
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
             CellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames="SkorID"
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
                    <asp:TemplateField HeaderText="Kohort" >
                        <ItemTemplate>
                            <asp:Label ID="Tahun" runat="server" Text='<%# Bind("Tahun")%>'></asp:Label>
                        </ItemTemplate>
                         <ItemStyle HorizontalAlign ="Center" />
                        <HeaderStyle HorizontalAlign="center" VerticalAlign="Top" Width ="10%" /><ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Semester">
                        <ItemTemplate>
                            <asp:Label ID="semester" runat="server" Text='<%# Bind("semester")%>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign ="Center" />
                        <HeaderStyle HorizontalAlign="center" VerticalAlign="Top" Width ="10%" /><ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Sesi">
                        <ItemTemplate>
                            <asp:Label ID="Sesi" runat="server" Text='<%# Bind("Sesi")%>'></asp:Label>
                        </ItemTemplate>
                       <ItemStyle HorizontalAlign ="Center" />
                        <HeaderStyle HorizontalAlign="center" VerticalAlign="Top" Width ="10%" /><ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="SMP PB">
                        <ItemTemplate>
                            <asp:Label ID="SMP_PB" runat="server" Text='<%# Bind("SMP_PB")%>'></asp:Label>
                        </ItemTemplate>
                         <ItemStyle HorizontalAlign ="Center" />
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Width ="10%" /><ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="SMP PAA">
                        <ItemTemplate>
                            <asp:Label ID="SMP_PAA" runat="server" Text='<%# Bind("SMP_PAA")%>'></asp:Label>
                        </ItemTemplate>
                         <ItemStyle HorizontalAlign ="Center" />
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Width ="10%" /><ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="SMP PAT">
                        <ItemTemplate>
                            <asp:Label ID="SMP_PAT" runat="server" Text='<%# Bind("SMP_PAT")%>'></asp:Label>
                        </ItemTemplate>
                         <ItemStyle HorizontalAlign ="Center" />
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" Width ="10%" /><ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Action">
                        <ItemTemplate>

                           <span runat="server" style="float:right">

                           <a href ="penentuan.standard.vok.aspx?edit=<%#Eval("SkorID")%>"><img title="Edit"  src="icons/edit.png" width="13" height="13" alt="::"/></a>
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