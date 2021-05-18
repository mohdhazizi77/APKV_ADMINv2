<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="pendaftaran_calon_ulang_online.ascx.vb" Inherits="apkv_v2_admin.pendaftaran_calon_ulang_online1" %>

<br />

<form id="form1">
    <table border="1" style="width: 100%; border: solid 2px #000000;">
        <tr>
            <td style="text-align: center; width: 150px">
                <a href="svmu_daftar_calon_ulang.aspx">
                    <asp:Image ID="imgDaftar" runat="server" Height="200px" Width="60%" ImageUrl="~/icons/DaftarCalonMengulang.png"/>
                </a>
            </td>
            <td style="text-align: center; width: 150px">
                <a href="svmu_semak_calon_ulang.aspx">
                    <asp:Image ID="imgSemak" runat="server" Height="200px" Width="60%" ImageUrl="~/icons/SemakStatusPermohonan.png" />

                </a>
            </td>
        </tr>
        <tr>
            <td style="text-align: center; width: 150px">
                <p>Daftar permohonan pendaftaran sebagai calon mengulang SVMU.</p>
                <p>(BM 1104 dan SEJARAH 1251)</p>
            </td>
            <td style="text-align: center; width: 150px">
                <p>Semak status permohonan pendaftaran SVMU</p>
                <p>(BM 1104 dan SEJARAH 1251)</p>
            </td>
        </tr>
    </table>
</form>
 