<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="borang_markah_sejarah_khas.ascx.vb" Inherits="apkv_v2_admin.borang_markah_sejarah_khas1" %>
<script type="text/javascript">
    function deleteConfirm(pubid) {
        var result = confirm('Anda pasti untuk padam ?');
        if (result) {
            return true;
        }
        else {
            return false;
        }
    }
</script>
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Pemeriksa >> Markah Borang >> Markah Khas Sejarah</td>
    </tr>
    <tr>
                <td style="width: 200px">Tahun Peperiksaan:</td>
        <td>
            <asp:DropDownList ID="ddlTahun" runat="server" AutoPostBack="true" Width="350px"></asp:DropDownList>
        </td>
    </tr>
</table>
<br />
<table class="fbform">
<tr class="fbform_header">
        <td>Senarai Calon Borang Markah Khas Sejarah.</td>
    </tr>
    <tr>
       <td>
       <asp:GridView ID="gridView" DataKeyNames="KhasSejID" runat="server" AutoGenerateColumns="False" ShowFooter="True" HeaderStyle-Font-Bold="true"
        onrowdeleting="gridView_RowDeleting"  onrowediting="gridView_RowEditing" onrowcommand="gridView_RowCommand" OnRowDataBound="gridView_RowDataBound" CellPadding="4" 
        EnableModelValidation="True" ForeColor="#333333" GridLines="None"
        Width="100%" PageSize="40"  CssClass="gridview_footer">
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
        <Columns>
        <asp:TemplateField HeaderText="KhasSejID" Visible="False">
        <ItemTemplate>
        <asp:Label ID="txtKhasSejID" runat="server" Text='<%#Eval("KhasSejID")%>'/>
       </ItemTemplate>
       <EditItemTemplate>
        <asp:Label ID="lblKhasSejID" runat="server" width="10px" Text='<%#Eval("KhasSejID")%>'/>
      </EditItemTemplate>
    <FooterTemplate>
</FooterTemplate>
    </asp:TemplateField>
 <asp:TemplateField HeaderText="Tahun">
      <ItemTemplate>
         <asp:Label ID="lblTahun" runat="server" Text='<%#Eval("Tahun")%>'/>
     </ItemTemplate>
     <EditItemTemplate>
         <asp:TextBox ID="txtTahun" width="100px"  runat="server" Text='<%#Eval("Tahun")%>'/>
     </EditItemTemplate>
     <FooterTemplate>
         <asp:TextBox ID="inTahun"  width="100px" runat="server"/>
</FooterTemplate>
 </asp:TemplateField>
 <asp:TemplateField HeaderText="Kod Pusat">
      <ItemTemplate>
         <asp:Label ID="lblKod" runat="server" Text='<%#Eval("Kod")%>'/>
     </ItemTemplate>
     <EditItemTemplate>
         <asp:TextBox ID="txtKod" width="100px"  runat="server" Text='<%#Eval("Kod")%>'/>
     </EditItemTemplate>
     <FooterTemplate>
         <asp:TextBox ID="inKod"  width="100px" runat="server"/>
     </FooterTemplate>
 </asp:TemplateField>
 <asp:TemplateField HeaderText="Sesi">
      <ItemTemplate>
         <asp:Label ID="lblSesi" runat="server" Text='<%#Eval("Sesi")%>'/>
     </ItemTemplate>
     <EditItemTemplate>
         <asp:TextBox ID="txtSesi" width="100px"  runat="server" Text='<%#Eval("Sesi")%>'/>
     </EditItemTemplate>
     <FooterTemplate>
         <asp:TextBox ID="inSesi"  width="100px" runat="server"/>
</FooterTemplate>
 </asp:TemplateField>
 <asp:TemplateField HeaderText="Semester">
      <ItemTemplate>
         <asp:Label ID="lblSemester" runat="server" Text='<%#Eval("Semester")%>'/>
     </ItemTemplate>
     <EditItemTemplate>
         <asp:TextBox ID="txtSemester" width="100px"  runat="server" Text='<%#Eval("Semester")%>'/>
     </EditItemTemplate>
     <FooterTemplate>
         <asp:TextBox ID="inSemester"  width="100px" runat="server"/>
</FooterTemplate>
 </asp:TemplateField>
 <asp:TemplateField HeaderText="Mykad">
      <ItemTemplate>
         <asp:Label ID="lblMykad" runat="server" Text='<%#Eval("Mykad")%>'/>
     </ItemTemplate>
     <EditItemTemplate>
         <asp:TextBox ID="txtMykad" width="100px"  runat="server" Text='<%#Eval("Mykad") %>'/>
     </EditItemTemplate>
     <FooterTemplate>
         <asp:TextBox ID="inMykad"  width="100px" runat="server"/>
</FooterTemplate>
 </asp:TemplateField>
 <asp:TemplateField HeaderText="AngkaGiliran">
     <ItemTemplate>
         <asp:Label ID="lblAngkaGiliran" runat="server" Text='<%#Eval("AngkaGiliran")%>'/>
     </ItemTemplate>
     <EditItemTemplate>
         <asp:TextBox ID="txtAngkaGiliran" width="100px" runat="server" Text='<%#Eval("AngkaGiliran")%>'/>
     </EditItemTemplate>
    <FooterTemplate>
        <asp:TextBox ID="inAngkaGiliran" width="100px"  runat="server"/>
</FooterTemplate>
 </asp:TemplateField>
  <asp:TemplateField HeaderText="Markah">
       <ItemTemplate>
         <asp:Label ID="lblPAT" runat="server" Text='<%#Eval("PAT")%>'/>
     </ItemTemplate>
     <EditItemTemplate>
         <asp:TextBox ID="txtPAT" width="100px"  runat="server" Text='<%#Eval("PAT")%>'/>
     </EditItemTemplate>
    <FooterTemplate>
        <asp:TextBox ID="inPAT" width="100px"  runat="server"/>
</FooterTemplate>
    </asp:TemplateField>
<asp:TemplateField HeaderText="Catatan">
     <ItemTemplate>
         <asp:Label ID="lblCatatan" runat="server" Text='<%#Eval("Catatan")%>'/>
     </ItemTemplate>
    <EditItemTemplate>
         <asp:TextBox ID="txtCatatan" width="300px"  runat="server" Text='<%#Eval("Catatan")%>'/>
     </EditItemTemplate>
    <FooterTemplate>
        <asp:TextBox ID="inCatatan" width="300px" runat="server"/>
</FooterTemplate>
 </asp:TemplateField>
 <asp:TemplateField>
    <EditItemTemplate>
</EditItemTemplate>
    <ItemTemplate>
<asp:Button ID="ButtonDelete" runat="server" CommandName="Delete"  Text="Delete"  />
    </ItemTemplate>
    <FooterTemplate>
        <asp:Button ID="ButtonAdd" runat="server" CommandName="AddNew"  Text="Add New Row" ValidationGroup="validation" />
    </FooterTemplate>
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
    <tr>
        <td>
        </td>
    </tr>
</table>
<br />
<table class="fbform">
    <tr class="fbform_header">
     <td colspan="2">&nbsp;<asp:Button ID="btnPrint" runat="server" Text="Cetak Borang Markah Khas" CssClass="fbbutton" Width="293px"/>&nbsp;
          </td>

    </tr>
</table>

<div class="info" id="divMsg" runat="server">
<asp:Label ID="lblID" runat="server" Text="" Visible="false"></asp:Label>
<asp:Label ID="lblType" runat="server" Text="" Visible="false"></asp:Label>
<asp:Label ID="lblTahun" runat="server" Text="" Visible="false"></asp:Label>
<asp:Label ID="lblPAT" runat="server" Text="" Visible="false"></asp:Label>
<asp:Label ID="lblSesi" runat="server" Text="" Visible="false"></asp:Label>
<asp:Label ID="lblSemester" runat="server" Text="" Visible="false"></asp:Label>
<asp:Label ID="lblMsg" runat="server" Text="System message..."></asp:Label>
</div>