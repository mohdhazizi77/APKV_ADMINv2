﻿<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="kolej_update.ascx.vb"
    Inherits="apkv_v2_admin.kolej_update" %>
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="3">Carian Dan Penyelenggaraan >> Kolej >> Kemaskini Maklumat Kolej</td>
    </tr>
</table>
<br />
<table class="fbform">
    <tr>
        <td style="width:20%;">JenisKolej:</td>
        <td style="width:2%;">:</td>
        <td><asp:DropDownList ID="ddlJenis" runat="server" AutoPostBack="false" Width="350px"></asp:DropDownList> *</td>
    </tr>
    <tr>
        <td>Kod Kolej</td>
        <td>:</td>
        <td><asp:TextBox ID="txtKod" runat="server" Width="350px" MaxLength="50"></asp:TextBox>*</td>
    </tr>
    <tr>
        <td>Nama Kolej</td>
        <td>:</td>
        <td><asp:TextBox ID="txtNama" runat="server" Width="350px" MaxLength="250"></asp:TextBox>*</td>
    </tr>
    <tr>
        <td>Telefon</td>
        <td>:</td>
        <td><asp:TextBox ID="txtTel" runat="server" Width="350px" MaxLength="150"></asp:TextBox>*</td>
    </tr>
    <tr>
        <td>Fax</td>
        <td>:</td>
        <td><asp:TextBox ID="txtFax" runat="server" Width="350px" MaxLength="150"></asp:TextBox>*</td>
    </tr>
    <tr>
        <td>Emel</td>
        <td>:</td>
        <td><asp:TextBox ID="txtEmail" runat="server" Width="350px" MaxLength="150"></asp:TextBox>*</td>
    </tr>
    <tr>
        <td>Alamat</td>
        <td>:</td>
        <td><asp:TextBox ID="txtAlamat1" runat="server" Width="350px" MaxLength="250"></asp:TextBox>*</td>
    </tr>
    <tr>
        <td colspan="2"></td>
        <td>
            <asp:TextBox ID="txtAlamat2" runat="server" Width="350px" MaxLength="250"></asp:TextBox>*
        </td>
    </tr>
    <tr>
        <td>Poskod</td>
        <td>:</td>
        <td><asp:TextBox ID="txtPoskod" runat="server" Width="350px" MaxLength="250"></asp:TextBox>*</td>
    </tr>
    <tr>
        <td>Bandar</td>
        <td>:</td>
        <td><asp:TextBox ID="txtBandar" runat="server" Width="350px" MaxLength="250"></asp:TextBox>*</td>
    </tr>
    <tr>
        <td>Negeri</td>
        <td>:</td>
        <td><asp:DropDownList ID="ddlNegeri" runat="server" AutoPostBack="false" Width="350px"></asp:DropDownList>* </td>
    </tr>
    <tr class="fbform_header">
        <td colspan="3"> Maklumat Lain</td>
    </tr>
    <tr>
        <td>Nama Pengarah</td>
        <td>:</td>
        <td><asp:TextBox ID="txtNamaPengarah" runat="server" Width="350px" MaxLength="150"></asp:TextBox></td>
    </tr>
     <tr>
          <td>Jawatan</td>
         <td>:</td>
         <td><asp:TextBox ID="txtJawatanPengarah" runat="server"></asp:TextBox></td>
    </tr>
    <tr>
        <td>Gred</td>
        <td>:</td>
        <td><asp:TextBox ID="txtGredPengarah" runat="server"></asp:TextBox></td>
    </tr>
    <tr>
        <td>Emel</td>
        <td>:</td>
        <td><asp:TextBox ID="txtEmailPengarah" runat="server" Width="350px" MaxLength="150"></asp:TextBox> </td>
    </tr>
    <tr>
        <td>Telefon Bimbit</td>
        <td>:</td>
        <td><asp:TextBox ID="txtMobilePengarah" runat="server" Width="350px" MaxLength="150"></asp:TextBox></td>
    </tr>
    <tr>
        <td>Telefon</td>
        <td>:</td>
        <td><asp:TextBox ID="txtTelPengarah" runat="server" Width="350px" MaxLength="150"></asp:TextBox></td>
    </tr>
    <tr>
        <td colspan="3">&nbsp;</td>
        
    </tr>
    <tr>
        <td>Nama KJPP</td>
        <td>:</td>
        <td><asp:TextBox ID="txtNamaKJPP" runat="server" Width="350px" MaxLength="150"></asp:TextBox></td>
    </tr>
    <tr>
        <td>Jawatan</td>
        <td>:</td>
        <td><asp:TextBox ID="txtJawatanKJPP" runat="server"></asp:TextBox></td>
    </tr>
    <tr>
        <td>Gred</td>
        <td>:</td>
        <td><asp:TextBox ID="txtGredKJPP" runat="server"></asp:TextBox></td>
    </tr>
    <tr>
        <td>Emel</td>
        <td>:</td>
        <td><asp:TextBox ID="txtEmailKJPP" runat="server" Width="350px" MaxLength="150"></asp:TextBox></td>
    </tr>
    <tr>
        <td>Telefon Bimbit</td>
        <td>:</td>
        <td><asp:TextBox ID="txtBimbitKJPP" runat="server" Width="350px" MaxLength="150"></asp:TextBox></td>
    </tr>
    <tr>
        <td>Telefon</td>
        <td>:</td>
        <td>&nbsp;<asp:TextBox ID="txtTelKJPP" runat="server" Width="350px" MaxLength="150"></asp:TextBox></td>
    </tr>
   <tr>
        <td colspan="3">&nbsp;</td>
        
    </tr>
     <tr>
           <td>Nama SUP</td>
         <td>:</td>
         <td><asp:TextBox ID="txtNamaSUP" runat="server" Width="350px" MaxLength="150"></asp:TextBox></td>
    </tr>
     <tr>
          <td>Jawatan</td>
         <td>:</td>
         <td><asp:TextBox ID="txtJawatanSUP" runat="server" Width="350px" MaxLength="150"></asp:TextBox></td>
    </tr>
    <tr>
         <td>Gred</td>
        <td>:</td>
         <td><asp:TextBox ID="txtGredSUP" runat="server" Width="350px" MaxLength="150"></asp:TextBox></td>
    </tr>
    <tr>
          <td>Emel</td>
        <td>:</td>
         <td><asp:TextBox ID="txtEmailSUP" runat="server" Width="350px" MaxLength="150"></asp:TextBox></td>
    </tr>
    <tr>
          <td>Telefon Bimbit</td>
        <td>:</td>
         <td><asp:TextBox ID="txtMobileSUP" runat="server" Width="350px" MaxLength="150"></asp:TextBox></td>
    </tr>
    <tr>
          <td>Telefon</td>
        <td>:</td>
         <td><asp:TextBox ID="txtTelSUP" runat="server" Width="350px" MaxLength="150"></asp:TextBox></td>
    </tr>
    <tr>
        <td colspan="3"></td>

    </tr>
    <tr>
        <td colspan="2">&nbsp;</td>
        <td style="text-align: left;">
            <asp:Button ID="btnUpdate" runat="server" Text="Kemaskini" CssClass="fbbutton" />&nbsp;<asp:Button
                ID="btnDelete" runat="server" Text="Hapuskan" CssClass="fbbutton" />
        </td>
    </tr>
</table>
<div class="info" id="divMsg" runat="server">
    <asp:Label ID="lblMsg" runat="server" Text="Mesej..."></asp:Label></div>
<asp:Label ID="lblKod" runat="server" Text="" Visible="false"></asp:Label>