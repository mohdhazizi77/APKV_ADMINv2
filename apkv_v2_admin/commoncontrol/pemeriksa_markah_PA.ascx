﻿<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="pemeriksa_markah_PA.ascx.vb" Inherits="apkv_v2_admin.pemeriksa_markah_PA_" %>
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Vokasional &gt;&gt; Pentaksiran Akhir Vokasional</td>
    </tr>
</table>
<br />
<%--<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Maklumat Kolej</td>
    </tr>
    <tr>
        <td >Negeri:</td>
        <td><asp:DropDownList ID="ddlNegeri" runat="server" Width="350px"></asp:DropDownList></td>
    </tr>
    <tr>
        <td>Jenis Kolej:</td>
        <td><asp:DropDownList ID="ddlJenis" runat="server" AutoPostBack="true"  Width="350px"></asp:DropDownList>
    </tr>
    <tr>
        <td>Nama Kolej:</td>
        <td><asp:DropDownList ID="ddlKolej" runat="server" AutoPostBack="true" Width="350px"></asp:DropDownList></td>
    </tr>
    </table>--%>
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2"></td>
    </tr>
    <tr>
          <td >Kohort:</td>
        <td>
            <asp:DropDownList ID="ddlTahun" runat="server" AutoPostBack="false" Width="350px"></asp:DropDownList>
        </td>
    </tr>
     <tr>
          <td >Semester:</td>
        <td>
            <asp:DropDownList ID="ddlSemester" runat="server" AutoPostBack="false" Width="350px"></asp:DropDownList>
        </td>
    </tr>
     <tr>
          <td >Sesi Pengambilan:</td>
        <td><asp:CheckBoxList ID="chkSesi" runat="server"  AutoPostBack="true" Width="349px" RepeatDirection="Horizontal">
             <asp:ListItem>1</asp:ListItem>
             <asp:ListItem>2</asp:ListItem>
             </asp:CheckBoxList></td>
    </tr>
   <tr>
          <td >Kod Program:</td>
        <td>
            <asp:DropDownList ID="ddlKodKursus" runat="server" AutoPostBack="true" Width="350px"></asp:DropDownList>
        </td>
    </tr>
  <%--   <tr>        
        <td >Kelas:</td>
          <td><asp:DropDownList ID="ddlKelas" runat="server" AutoPostBack="false" Width="350px"></asp:DropDownList></td>
    </tr>--%>
    <tr>
        <td colspan="2"><asp:Button ID="btnCari" runat="server" Text="Cari" CssClass="fbbutton" />&nbsp;</td>
           </tr>
</table> 
<br />
<div class="info" id="divMsgResult" runat="server">
  <asp:Label ID="lblMsgResult" runat="server" Text="Mesej..."></asp:Label>
</div>
<table class="fbform">
    <tr class="fbform_header">
        <td>Senarai Calon
        </td>
    </tr>
    <tr>
        <td>
            <asp:GridView ID="datRespondent" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                CellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames="PelajarID"
                Width="100%" PageSize="40" CssClass="gridview_footer">
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <Columns>
                    <asp:TemplateField HeaderText="#">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Nama">
                        <ItemTemplate>
                            <asp:Label ID="lblNama" runat="server" Text='<%# Bind("Nama")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Mykad">
                        <ItemTemplate>
                            <asp:Label ID="lblMykad" runat="server" Text='<%# Bind("Mykad")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="AngkaGiliran">
                        <ItemTemplate>
                            <asp:Label ID="AngkaGiliran" runat="server" Text='<%# Bind("AngkaGiliran")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Kod Program">
                        <ItemTemplate>
                            <asp:Label ID="KodKursus" runat="server" Text='<%# Bind("KodKursus")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Teori1">
                        <ItemTemplate>
                            <asp:TextBox ID="txtTeori1" runat="server" Width="30px" MaxLength="3" Text='<%# Bind("A_Teori1")%>'></asp:TextBox>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                  <%--  <asp:TemplateField HeaderText="Amali1">
                        <ItemTemplate>
                            <asp:TextBox ID="txtAmali1" runat="server" Width="30px" MaxLength="3" Text='<%# Bind("A_Amali1")%>'></asp:TextBox>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>--%>
                   <%--  <asp:TemplateField HeaderText="Teori2">
                        <ItemTemplate>
                            <asp:TextBox ID="txtTeori2" runat="server" Width="30px" MaxLength="3" Text='<%# Bind("A_Teori2")%>'></asp:TextBox>
                        </ItemTemplate>
                         <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Amali2">
                        <ItemTemplate>
                            <asp:TextBox ID="txtAmali2" runat="server" Width="30px" MaxLength="3" Text='<%# Bind("A_Amali2")%>'></asp:TextBox>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                   <asp:TemplateField HeaderText="Teori3">
                        <ItemTemplate>
                            <asp:TextBox ID="txtTeori3" runat="server" Width="30px" MaxLength="3" Text='<%# Bind("A_Teori3")%>'></asp:TextBox>
                        </ItemTemplate>
                       <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Amali3">
                        <ItemTemplate>
                            <asp:TextBox ID="txtAmali3" runat="server" Width="30px" MaxLength="3" Text='<%# Bind("A_Amali3")%>'></asp:TextBox>
                        </ItemTemplate>
                         <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Teori4">
                        <ItemTemplate>
                            <asp:TextBox ID="txtTeori4" runat="server" Width="30px" MaxLength="3" Text='<%# Bind("A_Teori4")%>'></asp:TextBox>
                        </ItemTemplate>
                         <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Amali4">
                        <ItemTemplate>
                            <asp:TextBox ID="txtAmali4" runat="server" Width="30px" MaxLength="3" Text='<%# Bind("A_Amali4")%>'></asp:TextBox>
                        </ItemTemplate>
                         <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Teori5">
                        <ItemTemplate>
                            <asp:TextBox ID="txtTeori5" runat="server" Width="30px" MaxLength="3" Text='<%# Bind("A_Teori5")%>'></asp:TextBox>
                        </ItemTemplate>
                         <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Amali5">
                        <ItemTemplate>
                            <asp:TextBox ID="txtAmali5" runat="server" Width="30px" MaxLength="3" Text='<%# Bind("A_Amali5")%>'></asp:TextBox>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="Teori6">
                        <ItemTemplate>
                            <asp:TextBox ID="txtTeori6" runat="server" Width="30px" MaxLength="3" Text='<%# Bind("A_Teori6")%>'></asp:TextBox>
                        </ItemTemplate>
                          <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="Amali6">
                        <ItemTemplate>
                            <asp:TextBox ID="txtAmali6" runat="server" Width="30px" MaxLength="3" Text='<%# Bind("A_Amali6")%>'></asp:TextBox>
                        </ItemTemplate>
                          <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Teori7">
                        <ItemTemplate>
                            <asp:TextBox ID="txtTeori7" runat="server" Width="30px" MaxLength="3" Text='<%# Bind("A_Teori7")%>'></asp:TextBox>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Amali7">
                        <ItemTemplate>
                            <asp:TextBox ID="txtAmali7" runat="server" Width="30px" MaxLength="3" Text='<%# Bind("A_Amali7")%>'></asp:TextBox>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Teori8">
                        <ItemTemplate>
                            <asp:TextBox ID="txtTeori8" runat="server" Width="30px" MaxLength="3" Text='<%# Bind("A_Teori8")%>'></asp:TextBox>
                        </ItemTemplate>
                         <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Amali8">
                        <ItemTemplate>
                            <asp:TextBox ID="txtAmali8" runat="server" Width="30px" MaxLength="3" Text='<%# Bind("A_Amali8")%>'></asp:TextBox>
                        </ItemTemplate>
                         <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>--%>
                    
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
        <td>
            <asp:Button ID="btnUpdate" runat="server" Text="Kemaskini" CssClass="fbbutton" Visible="true" />&nbsp;
            <asp:Button ID="btnGred" runat="server" Text="Gred" CssClass="fbbutton" Visible="False" />
        </td>
    </tr>
</table>
<br />
<div class="info" id="divMsg" runat="server">

<asp:Label ID="lblMsg" runat="server" Text="System message..."></asp:Label>
</div>