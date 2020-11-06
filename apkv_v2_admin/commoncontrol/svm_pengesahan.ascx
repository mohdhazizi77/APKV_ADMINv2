<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="svm_pengesahan.ascx.vb" Inherits="apkv_v2_admin.svm_pengesahan1" %>

<style type="text/css">
    .auto-style1 {
        margin-right: 0px;
    }
    .auto-style6 {
        width: 250px;
        margin-right: 0px;
    }
    .auto-style7 {
        width: 250px;
    }
    .auto-style8 {
        width: 545px;
    }
</style>

<table class="fbform" style="width:1000px">
    <tr class="fbform_header">
        <td colspan="3" style="text-align: center">Sijil Vokasional Malaysia
            <br />
        </td>
    </tr>
    <tr>
        <td class="auto-style8">
            <asp:Label ID="Label1" runat="server" Text="NAMA"></asp:Label>
        </td>
        <td>
            :<asp:Label ID="lblNama" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="auto-style8">
            <asp:Label ID="Label2" runat="server" Text="NO. KAD PENGENALAN"></asp:Label>
        </td>
        <td>
            :<asp:Label ID="lblMykad" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="auto-style8">
            <asp:Label ID="Label3" runat="server" Text="ANGKA GILIRAN"></asp:Label>
        </td>
        <td>
            :<asp:Label ID="lblAG" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="auto-style8">
            <asp:Label ID="Label4" runat="server" Text="INSTITUSI"></asp:Label>
        </td>
        <td>
            :<asp:Label ID="lblInstitusi" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="auto-style8">
            <asp:Label ID="Label5" runat="server" Text="KLUSTER"></asp:Label>
        </td>
        <td>
            :<asp:Label ID="lblKluster" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="auto-style8">
            <asp:Label ID="Label6" runat="server" Text="KURSUS"></asp:Label>
        </td>
        <td>
            :<asp:Label ID="lblKursus" runat="server"></asp:Label>
        </td>
    </tr>
</table>

<br />

<table class="fbform" style="width:1000px">
    <tr>
        <td class="auto-style8">
            <asp:Label ID="kodVok1" runat="server" Width="300px" CssClass="auto-style1"></asp:Label>
        </td>  
        <td>
            <asp:Label ID="gredVok1" runat="server"></asp:Label>
        </td>  
    </tr>
    <tr>
        <td class="auto-style8">
            <asp:Label ID="kodVok2" runat="server"></asp:Label>
        </td>
        <td>
            <asp:Label ID="gredVok2" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="auto-style8">
            <asp:Label ID="kodVok3" runat="server"></asp:Label>
        </td>
        <td>
            <asp:Label ID="gredVok3" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="auto-style8">
            <asp:Label ID="kodVok4" runat="server"></asp:Label>
        </td>
        <td>
            <asp:Label ID="gredVok4" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="auto-style8">
            <asp:Label ID="kodVok5" runat="server"></asp:Label>
        </td>
        <td>
            <asp:Label ID="gredVok5" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="auto-style8">
            <asp:Label ID="kodVok6" runat="server"></asp:Label>
        </td>
        <td>
            <asp:Label ID="gredVok6" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="auto-style8">
            <asp:Label ID="kodVok7" runat="server"></asp:Label>
        </td>
        <td>
            <asp:Label ID="gredVok7" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="auto-style8">
            <asp:Label ID="kodVok8" runat="server"></asp:Label>
        </td>
        <td>
            <asp:Label ID="gredVok8" runat="server"></asp:Label>
        </td>
    </tr>  
</table>

<br />

<table class="fbform" style="width:1000px">

    <tr>
        <td class="auto-style8">
            <asp:Label ID="lblBM" runat="server" Text="1104"></asp:Label>
        </td>
        <td>
            <asp:Label ID="lblGredBM" runat="server" Text="1104"></asp:Label>
        </td>
        
    </tr>
    <tr>
        <td class="auto-style8">
            <asp:Label ID="lblSJ" runat="server" Text="1251"></asp:Label>
        </td>    
        <td>
            <asp:Label ID="lblGredSJ" runat="server" Text="1104"></asp:Label>
        </td>
    </tr> 

</table>

<br />

<table class="fbform" style="width:1000px">
    <tr>
        <td class="auto-style7">
            <asp:Label ID="Label7" runat="server" Text="KOMPETEN SEMUA KURSUS"></asp:Label>
        </td>        
    </tr>
    <tr>
        <td class="auto-style7">
            <asp:Label ID="lblKompeten" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="auto-style7">
            <asp:Label ID="Label8" runat="server" Text="PURATA NILAI GRED KUMULATIF AKADEMIK (PNGKA)"></asp:Label>
        </td>
        <td style="width: 200px;">
            <asp:Label ID="lblPNGKA" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="auto-style6">
            <asp:Label ID="Label9" runat="server" Text="PURATA NILAI GRED KUMULATIF VOKASIONAL (PNGKV)"></asp:Label>
        </td>
        <td class="auto-style1">
            <asp:Label ID="lblPNGKV" runat="server"></asp:Label>
        </td>
    </tr>
</table>

<br />

<div id="tblSetara" runat="server">
    <table class="fbform" style="width:1000px">
        <tr>
            <td style="width: 550px;">
                Kelulusan Bahasa Melayu Kod 1104 adalah setara dengan Bahasa Melayu Kod 1103 SPM.</td>
        </tr>
        <tr>
            <td style="width: 550px;">
                Kelulusan Sejarah Kod 1251 adalah setara dengan lulus Sejarah Kod 1249 SPM.</td>
        </tr>
        <tr>
            <td style="width: 550px;">
                Kelulusan bagi semua mata pelajaran vokasional adalah setara dengan mata pelajaran elektif SPM.</td>
        </tr>
    </table>
</div>

<br />
<br />

<div class="info" id="divMsg" runat="server">
    <asp:Label ID="lblMsg" runat="server" Text="Untuk tujuan pengesahan Sijil Vokasional Malaysia."></asp:Label>
</div>

