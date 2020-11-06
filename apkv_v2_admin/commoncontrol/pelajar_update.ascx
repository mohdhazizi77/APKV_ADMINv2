<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="pelajar_update.ascx.vb"
    Inherits="apkv_v2_admin.pelajar_update" %>
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Carian Dan Penyelengaraan >> Calon >> Kemaskini Maklumat Calon</td>
    </tr>
</table>
<br />
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Paparan Maklumat Calon</td>
    </tr>
    <tr><td  colspan="2"></td></tr>
     <tr>
         <td >Peringkat Pengajian:</td>
        <td><asp:Label ID="lblPengajian" runat="server"></asp:Label></td>
    </tr>
     <tr>
         <td>&nbsp;</td>
        <td>&nbsp;</td>
    </tr>
    <tr>
         <td >Kohort:</td>
         <td><asp:Label ID="lblTahun" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
          <td >Sesi Pengambilan:</td>
         <td>
            <asp:Label ID="lblSesi" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
         <td >Nama Bidang:</td>
         <td>
            <asp:Label ID="lblKluster" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
         <td >Kod Program:</td>
         <td>
            <asp:Label ID="lblKodKursus" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
          <td >Nama Program:</td>
         <td>
            <asp:Label ID="lblNamaKursus" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
          <td >Nama Kelas:</td>
         <td>
            <asp:Label ID="lblNamaKelas" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
         <td></td>
    </tr>
     <tr>
          <td >Nama Calon: </td>
         <td>
            <asp:TextBox ID="txtNama" runat="server" Width="350px" MaxLength="250"></asp:TextBox>
        </td>
    </tr>
    <tr>
          <td >Mykad:</td>
         <td>
            <asp:TextBox ID="txtMYKAD" runat="server" Width="350px" MaxLength="50"></asp:TextBox>
              <asp:Label ID="lblMykad" runat="server" Visible ="false" ></asp:Label>
        </td>
    </tr>
     <tr>
          <td >AngkaGiliran:</td>
         <td>
            <asp:Label ID="lblAngkaGiliran" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
          <td >Jantina:</td>
         <td><asp:CheckBoxList ID="chkJantina" runat="server" Width="349px" RepeatDirection="Horizontal">
             <asp:ListItem>LELAKI</asp:ListItem>
             <asp:ListItem>PEREMPUAN</asp:ListItem>
             </asp:CheckBoxList>   
    </tr>
    <tr>
          <td >Kaum:</td>
         <td>
            <asp:DropDownList ID="ddlKaum" runat="server" AutoPostBack="false" Width="350px">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
         <td >Agama:</td>
         <td><asp:CheckBoxList ID="chkAgama" runat="server" Width="349px" RepeatDirection="Horizontal">
             <asp:ListItem>ISLAM</asp:ListItem>
             <asp:ListItem>LAIN-LAIN</asp:ListItem>
             </asp:CheckBoxList>
    </tr>
    <tr>
         <td >Emel:</td>
         <td>
            <asp:TextBox ID="txtEmail" runat="server" Width="350px" MaxLength="250"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td colspan="2"></td>
    </tr>
    <tr>
          <td >Status Calon:</td>
         <td><asp:DropDownList ID="ddlStatus" runat="server" AutoPostBack="false" Width="350px"></asp:DropDownList>
</td>
    </tr>
    <tr>
          <td >Jenis Calon:</td>
         <td><asp:DropDownList ID="ddlJenisCalon" runat="server" AutoPostBack="false" Width="350px"></asp:DropDownList>
</td>
    </tr>

    <tr>
          <td >Catatan:</td>
         <td>
            <asp:Textbox ID="txtCatatan" runat="server" Width="460px" Height="117px" TextMode="MultiLine"></asp:Textbox>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:Button ID="btnUpdate" runat="server" Text="Kemaskini" CssClass="fbbutton" />&nbsp; <asp:Button ID="btnDelete" runat="server" Text="Hapuskan" CssClass="fbbutton" />&nbsp;

        </td>
    </tr>
</table>
<br />

 <table class="fbform" style ="width :100%">
    <tr class="fbform_header">
        <td colspan="2">Sejarah Penukaran Data
        </td>
    </tr>
    <tr>
         <td colspan="2">

            <asp:GridView ID="datRespondent" runat="server" AutoGenerateColumns="False"
                CellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames="ID"
                Width="100%" PageSize="15" CssClass="gridview_footer" EnableModelValidation="True">
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Left" />
                <Columns>
                    <asp:TemplateField HeaderText="#">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Dikemaskini Oleh">
                        <ItemTemplate>
                            <asp:Label ID="Audit" runat="server" Text='<%# Bind("Audit")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" /><ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>

                     <asp:TemplateField HeaderText="Tarikh">
                        <ItemTemplate>
                            <asp:Label ID="ChangeDate" runat="server" Text='<%# Bind("ChangeDate")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" /><ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Nama">
                        <ItemTemplate>
                            <asp:Label ID="Nama" runat="server" Text='<%# Bind("Nama")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" /><ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Mykad">
                        <ItemTemplate>
                            <asp:Label ID="Mykad" runat="server" Text='<%# Bind("Mykad")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" /><ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Jantina">
                        <ItemTemplate>
                            <asp:Label ID="Jantina" runat="server" Text='<%# Bind("Jantina")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" /><ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Kaum">
                        <ItemTemplate>
                            <asp:Label ID="Kaum" runat="server" Text='<%# Bind("Kaum")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" /><ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Agama">
                        <ItemTemplate>
                            <asp:Label ID="Agama" runat="server" Text='<%# Bind("Agama")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" /><ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Email">
                        <ItemTemplate>
                            <asp:Label ID="Email" runat="server" Text='<%# Bind("Email")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" /><ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Catatan">
                        <ItemTemplate>
                            <asp:Label ID="Catatan" runat="server" Text='<%# Bind("Catatan")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" /><ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Catatan Kemaskini">
                        <ItemTemplate>
                            <asp:Label ID="CatatanKemaskini" runat="server" Text='<%# Bind("CatatanKemaskini")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" /><ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                   
                </Columns>
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Font-Underline="true" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" CssClass="cssPager" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" VerticalAlign="Bottom"
                    HorizontalAlign="Left" />
                <EditRowStyle BackColor="#999999" />
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            </asp:GridView>
        </td>
    </tr>
</table>
<br />

<div class="info" id="divMsg" runat="server">
        <asp:Label ID="lblKod" runat="server" Text="" Visible="false"></asp:Label>
        <asp:Label ID="lblHidMykad" runat ="server" Text ="" Visible ="false" ></asp:Label>
        <asp:Label ID="lblHidKolej" runat ="server" Text ="" Visible ="false" ></asp:Label>
        <asp:Label ID="lblMsg" runat="server" Text="Mesej..."></asp:Label>

</div>


