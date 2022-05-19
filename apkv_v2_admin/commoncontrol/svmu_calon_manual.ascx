<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="svmu_calon_manual.ascx.vb" Inherits="apkv_v2_admin.svmu_calon_manual1" %>

<table class="fbform">
    <tr class="fbform_header">
        <td colspan="4">Daftar Maklumat Calon SVMU (MANUAL)</td>
    </tr>

  <%--  <tr>

        <td style="width: 35px"></td>
        <td style="width: 130px">No. Rujukan</td>
        <td>:</td>
        <td>
            <asp:TextBox ID="txtRujukan" runat="server" Width="150px"></asp:TextBox>
            (Contoh: LPPay60aba4ac696db)
        </td>
        <td>
            <asp:Label ID="lblMsgRujukan" runat="server" ForeColor="Red"></asp:Label>
        </td>

    </tr>--%>

    <tr>

        <td style="width: 35px"></td>
        <td style="width: 130px">No. Kad Pengenalan</td>
        <td>:</td>
        <td>
            <asp:TextBox ID="txtMYKAD" runat="server" Width="150px"></asp:TextBox>
            (Nota: Tanpa "-")
            <asp:Label ID="lblMsgMYKAD" runat="server" ForeColor="Red"></asp:Label>
        </td>


    </tr>

    <tr>

        <td style="width: 35px"></td>
        <td style="width: 130px">Angka Giliran</td>
        <td>:</td>
        <td>
            <asp:TextBox ID="txtAngkaGiliran" runat="server" Width="150px"></asp:TextBox>
            (Contoh: K011FETE001)
            <asp:Label ID="lblMsgAngkaGiliran" runat="server" ForeColor="Red"></asp:Label>
        </td>

    </tr>

    <tr>
        <td style="width: 35px"></td>
        <td style="width: 130px"></td>
        <td></td>
        <td>
            <asp:Button ID="btnSemak" runat="server" Text="Semak" />
        </td>

    </tr>
</table>
