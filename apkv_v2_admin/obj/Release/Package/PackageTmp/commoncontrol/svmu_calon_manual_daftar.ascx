<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="svmu_calon_manual_daftar.ascx.vb" Inherits="apkv_v2_admin.svmu_calon_manual_daftar" %>

<%--<script src="http://ajax.googleapis.com/ajax/libs/jquery/1.6/jquery.min.js" type="text/javascript"></script>
<script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/jquery-ui.min.js" type="text/javascript"></script>
<link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/base/jquery-ui.css" rel="Stylesheet" type="text/css" />

<script type="text/javascript">
    $(function () {
        $("[id$=txtDate]").datepicker({
            dateFormat: 'dd-mm-yy',
            showOn: 'button',
            buttonImageOnly: true,
            changeMonth: true,
            changeYear: true,
            buttonImage: '/icons/calendar.gif'
        });
    });
</script>--%>


<table class="fbform">

    <tr class="fbform_header">
        <td colspan="2">Maklumat Peribadi</td>
    </tr>

</table>

<br />

<table class="fbform" style="padding-right: 10px">

    <tr>

        <td style="width: 150px">Nama</td>
        <td style="width: 3px">:</td>
        <td colspan="6">
            <asp:TextBox ID="txtNama" runat="server" Enabled="false" Width="100%"></asp:TextBox>
        </td>

    </tr>

    <tr>

        <td style="width: 150px">No. Kad Pengenalan</td>
        <td style="width: 3px">:</td>
        <td style="width: 150px">
            <asp:TextBox ID="txtMYKAD" runat="server" Enabled="false" Width="90%"></asp:TextBox>
        </td>

        <td style="width: 150px">Angka Giliran</td>
        <td style="width: 3px">:</td>
        <td style="width: 150px">
            <asp:TextBox ID="txtAngkaGiliran" runat="server" Enabled="false" Width="100%"></asp:TextBox>
        </td>

    </tr>

    <tr>

        <td style="width: 150px">Tarikh Lahir</td>
        <td style="width: 3px">:</td>
        <td style="width: 150px">
            <asp:TextBox ID="txtDate" runat="server" Enabled="false" Width="90%"></asp:TextBox>
        </td>

        <td style="width: 150px">Jantina</td>
        <td style="width: 3px">:</td>
        <td style="width: 150px">
            <asp:TextBox ID="txtJantina" runat="server" Enabled="false" Width="100%"></asp:TextBox>
        </td>

    </tr>

    <tr>

        <td style="width: 150px">Bangsa</td>
        <td style="width: 3px">:</td>
        <td style="width: 150px">
            <asp:DropDownList ID="ddlBangsa" runat="server" Width="95%"></asp:DropDownList><asp:Label ID="lblErrBangsa" runat="server" ForeColor="Red"></asp:Label>
        </td>

        <td style="width: 150px">Agama</td>
        <td style="width: 3px">:</td>
        <td style="width: 150px">
            <asp:TextBox ID="txtAgama" runat="server" Enabled="false" Width="100%"></asp:TextBox>
        </td>

    </tr>

    <tr>

        <td style="width: 150px">Kolej</td>
        <td style="width: 3px">:</td>
        <td colspan="6">
            <asp:TextBox ID="txtKolej" runat="server" Enabled="false" Width="100%"></asp:TextBox>
        </td>

    </tr>

    <tr>

        <td style="width: 150px">Kohort</td>
        <td style="width: 3px">:</td>
        <td style="width: 150px">
            <asp:TextBox ID="txtKohort" runat="server" Enabled="false" Width="90%"></asp:TextBox>
        </td>

    </tr>

    <tr>

        <td style="width: 150px">Alamat*</td>
        <td style="width: 3px">:</td>
        <td colspan="6">
            <asp:TextBox ID="txtAlamat" runat="server" Width="100%"></asp:TextBox><asp:Label ID="lblErrAlamat" runat="server" ForeColor="Red"></asp:Label>
        </td>

    </tr>

    <tr>

        <td style="width: 150px">Poskod*</td>
        <td style="width: 3px">:</td>
        <td style="width: 150px">
            <asp:TextBox ID="txtPoskod" runat="server" Width="90%"></asp:TextBox><asp:Label ID="lblErrPoskod" runat="server" ForeColor="Red"></asp:Label>
        </td>

        <td style="width: 150px">Bandar*</td>
        <td style="width: 3px">:</td>
        <td style="width: 150px">
            <asp:TextBox ID="txtBandar" runat="server" Width="100%"></asp:TextBox><asp:Label ID="lblErrBandar" runat="server" ForeColor="Red"></asp:Label>
        </td>

    </tr>

    <tr>

        <td style="width: 150px">Negeri*</td>
        <td style="width: 3px">:</td>
        <td style="width: 150px">
            <asp:DropDownList ID="ddlNegeri" runat="server" Width="95%"></asp:DropDownList><asp:Label ID="lblErrNegeri" runat="server" ForeColor="Red"></asp:Label>
        </td>

        <td style="width: 150px">No. Telefon (HP)*</td>
        <td style="width: 3px">:</td>
        <td style="width: 150px">
            <asp:TextBox ID="txtTelefon" runat="server" Width="100%"></asp:TextBox><asp:Label ID="lblErrTelefon" runat="server" ForeColor="Red"></asp:Label>
        </td>

    </tr>

    <tr>

        <td style="width: 150px">Alamat E-mel*</td>
        <td style="width: 3px">:</td>
        <td colspan="6">
            <asp:TextBox ID="txtEmail" runat="server" Width="100%"></asp:TextBox><asp:Label ID="lblErrEmail" runat="server" ForeColor="Red"></asp:Label>
        </td>

    </tr>

</table>

<br />

<table class="fbform">

    <tr class="fbform_header">
        <td colspan="2">Mata Pelajaran
            <asp:Label ID="lblErrMataPelajaran" runat="server" ForeColor="Red"></asp:Label></td>
    </tr>

</table>

<br />

<table class="fbform" style="padding-right: 10px">



    <tr>
        <th style="text-align: center">Bil
        </th>

        <th style="text-align: center">Tanda
        </th>

        <th style="text-align: center">Kod Mata Pelajaran
        </th>

        <th>Nama Mata Pelajaran Peperiksaan
        </th>

        <th style="text-align: center">Gred Dahulu
        </th>

    </tr>

    <tr>

        <td style="text-align: center">1</td>
        <td style="text-align: center">
            <asp:CheckBox ID="chkBM" runat="server" /></td>
        <td style="text-align: center">1104</td>
        <td>Bahasa Melayu (Kod Kertas A01401, A01402 dan A01403)</td>
        <td style="text-align: center">
            <asp:Label ID="lblGredBM" runat="server"></asp:Label></td>

    </tr>

    <tr>

        <td style="text-align: center">2</td>
        <td style="text-align: center">
            <asp:CheckBox ID="chkSJ" runat="server" /></td>
        <td style="text-align: center">1251</td>
        <td>Sejarah (Kod Kertas A05401)</td>
        <td style="text-align: center">
            <asp:Label ID="lblGredSJ" runat="server"></asp:Label></td>

    </tr>

</table>

<br />

<br />

<table class="fbform">

    <tr class="fbform_header">
        <td colspan="2">Keterangan Bayaran Calon Mengulang</td>
    </tr>

</table>

<br />

<table border="1" class="fbform" style="padding-right: 10px">

    <tr>

        <td style="text-align: center; width: 10px">1
        </td>

        <td style="width: 200px">Bayaran Asas SVM
        </td>

        <td style="text-align: center; width: 150px">RM
            <asp:Label ID="lblRMAsas" runat="server"></asp:Label>
        </td>

    </tr>

    <tr>

        <td style="text-align: center; width: 10px">2
        </td>

        <td style="width: 200px">1104 - Bahasa Melayu
        </td>

        <td style="text-align: center; width: 150px">RM
            <asp:Label ID="lblRMBM" runat="server"></asp:Label>
        </td>

    </tr>

    <tr>

        <td style="text-align: center; width: 10px">3
        </td>

        <td style="width: 200px">1251 - Sejarah
        </td>

        <td style="text-align: center; width: 150px">RM
            <asp:Label ID="lblRMSJ" runat="server"></asp:Label>
        </td>

    </tr>

</table>

<br />

<table class="fbform">

    <tr class="fbform_header">
        <td colspan="2">Pemilihan Pusat Peperiksaan</td>
    </tr>

</table>

<br />

<table class="fbform" style="padding-right: 10px">

    <tr>

        <td style="width: 150px">Negeri</td>
        <td style="width: 3px">:</td>
        <td>
            <asp:DropDownList ID="ddlNegeriKV" runat="server" Width="250px" AutoPostBack="true"></asp:DropDownList>
            <asp:Label ID="lblErrNegeriKV" runat="server" ForeColor="Red"></asp:Label></td>

    </tr>

    <tr>

        <td style="width: 150px">Kolej Vokasional</td>
        <td style="width: 3px">:</td>
        <td>
            <asp:DropDownList ID="ddlKolej" runat="server" Width="250px"></asp:DropDownList>
            <asp:Label ID="lblErrKolej" runat="server" ForeColor="Red"></asp:Label></td>

    </tr>

</table>

<br />

<table style="width: 100%">

    <tr>

        <td style="text-align: left">
            <asp:Button ID="btnBack" runat="server" Text="Kembali" />
        </td>

        <td style="text-align: right">
            <asp:Button ID="btnProceed" runat="server" Text="Daftar" />
        </td>

    </tr>

</table>

<div class="info" id="divMsg" runat="server">

    <asp:Label ID="lblMsg" runat="server" Text="System message..."></asp:Label>
</div>
