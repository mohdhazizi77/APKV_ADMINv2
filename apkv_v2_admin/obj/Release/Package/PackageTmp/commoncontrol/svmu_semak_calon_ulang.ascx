<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="svmu_semak_calon_ulang.ascx.vb" Inherits="apkv_v2_admin.svmu_semak_calon_ulang1" %>

<br />

<div style="width: 100%; border: solid 2px #000000;">

    <br />

    <table style="width: 100%">

        <tr>

            <td style="width: 35px"></td>
            <td style="width: 130px">No. Kad Pengenalan</td>
            <td>:</td>
            <td>
                <asp:TextBox ID="txtMYKAD" runat="server" Width="150px"></asp:TextBox>
                (Nota: Tanpa "-")
            </td>
            <td>
                <asp:Label ID="lblMsgMYKAD" runat="server" ForeColor="Red"></asp:Label>
            </td>

        </tr>

        <tr>

            <td style="width: 35px"></td>
            <td style="width: 130px">Angka Giliran</td>
            <td>:</td>
            <td>
                <asp:TextBox ID="txtAngkaGiliran" runat="server" Width="150px"></asp:TextBox>
                (Contoh: K011FETE001)</td>
            <td>
                <asp:Label ID="lblMsgAngkaGiliran" runat="server" ForeColor="Red"></asp:Label>
            </td>

        </tr>

    </table>

    <br />

    <table style="width: 100%">

        <tr>

            <td style="text-align: center">
                <asp:Button ID="btnBack" runat="server" Text="Ke Laman Utama" />
                <asp:Button ID="btnApply" runat="server" Text="Semak" />
            </td>

        </tr>

    </table>

    <br />

</div>

<br />
<br />
<br />
<br />
<br />


