﻿<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="kelas_pensyarah_akademik.ascx.vb" Inherits="apkv_v2_admin.kelas_pensyarah_akademik1" %>
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Pendaftaran >> Kelas >> Penetapan Pensyarah</td>
    </tr>
</table>
<br />
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Maklumat Kolej</td>
    </tr>
    <tr>
        <td >Negeri:</td>
        <td><asp:DropDownList ID="ddlNegeri" runat="server" AutoPostBack="true"  Width="350px"></asp:DropDownList></td>
    </tr>
    <tr>
        <td>Jenis Kolej:</td>
        <td><asp:DropDownList ID="ddlJenis" runat="server" AutoPostBack="true"  Width="350px"></asp:DropDownList>
    </tr>
    <tr>
        <td>Nama Kolej:</td>
        <td><asp:DropDownList ID="ddlKolej" runat="server" AutoPostBack="true" Width="350px"></asp:DropDownList></td>
    </tr>
    </table>
<br />
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Pemilihan Kursus Mengikut Pensyarah</td>
    </tr>
    <tr>
           <td>Kohort:</td>
        <td><asp:DropDownList ID="ddlTahun" runat="server" AutoPostBack="true" Width="350px"></asp:DropDownList></td>
    </tr>
    <tr>
           <td>Semester:</td>
        <td><asp:DropDownList ID="ddlSemester" runat="server" AutoPostBack="true" Width="350px"></asp:DropDownList></td>
    </tr>
    <tr>
         <td >Sesi Pengambilan:</td>
             <td><asp:CheckBoxList ID="chkSesi" runat="server" AutoPostBack="true" Width="349px" RepeatDirection="Horizontal">
             <asp:ListItem>1</asp:ListItem>
             <asp:ListItem>2</asp:ListItem>
             </asp:CheckBoxList>
    </tr>
     <tr>
         <td >Nama Bidang:</td>
         <td> <asp:DropDownList ID="ddlNamaKluster" runat="server" AutoPostBack="true" Width="350px"></asp:DropDownList>
        </td>
    </tr>
    <tr>
         <td >Kod Program:</td>
        <td><asp:DropDownList ID="ddlKodKursus" runat="server" AutoPostBack="true" Width="350px"></asp:DropDownList></td> 
    </tr>
     <tr>
         <td >Nama Kelas:</td>
        <td><asp:DropDownList ID="ddlNamaKelas" runat="server" AutoPostBack="false" Width="350px"></asp:DropDownList></td> 
    </tr>
    <tr>
        <td colspan ="2"><asp:Button ID="btnCari" runat="server" Text="Cari" CssClass="fbbutton" /></td>      
    </tr>
</table>
<div class="info" id="divMsg" runat="server">
 <asp:Label ID="lblmsg" runat="server" Text=""></asp:Label></div>
<br />

<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Senarai Matapelajaran</td>
    </tr>
    <tr>
    <td colspan="2">
            <asp:GridView ID="datRespondent" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                CellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames="MataPelajaranID"
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
                     <asp:TemplateField HeaderText="Kohort">
                        <ItemTemplate>
                            <asp:Label ID="Tahun" runat="server" Text='<%# Bind("Tahun")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" /><ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Kod MataPelajaran">
                        <ItemTemplate>
                            <asp:Label ID="lblKodModul" runat="server" Text='<%# Bind("KodMataPelajaran")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" /><ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Nama MataPelajaran">
                        <ItemTemplate>
                            <asp:Label ID="lblNamaModul" runat="server" Text='<%# Bind("NamaMataPelajaran")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" /><ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Jam Kredit">
                        <ItemTemplate>
                            <asp:Label ID="lblJamKredit" runat="server" Text='<%# Bind("JamKredit")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" /><ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Pilih">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkSelect" runat="server" />
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" /><ItemStyle VerticalAlign="Middle" />
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
</table>
<br />
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Pemilihan Pensyarah</td>
    </tr>
     <tr>
        <td colspan="2">
            <asp:GridView ID="datRespondent2" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                CellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames="Nama"
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
                    <asp:TemplateField HeaderText="Nama">
                        <ItemTemplate>
                           <asp:linkbutton ID="linkbutton" runat="server" Text='<%#Eval("Nama")%>'></asp:linkbutton>                         
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" /><ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Mykad">
                        <ItemTemplate>
                            <asp:Label ID="MYKAD" runat="server" Text='<%# Bind("MYKAD") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" /><ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Pilih"> 
                        <ItemTemplate> 
                            <asp:Button ID="btnCreate" runat="server" Text="+" CommandName="Pilih" CommandArgument ='<%#Eval("PensyarahID")%>' /></td>
                        </ItemTemplate> 
                         <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" /><ItemStyle VerticalAlign="Middle" />
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
</table>
<br />
