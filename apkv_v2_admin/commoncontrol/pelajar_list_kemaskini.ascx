<%@ control language="vb" autoeventwireup="false" codebehind="pelajar_list_kemaskini.ascx.vb" inherits="apkv_v2_admin.pelajar_list_kemaskini" %>

<table class="fbform">
    <tr class="fbform_header">
        <td colspan="3">Pendaftaran >> Kemaskini</td>
    </tr>
</table>
<br />
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="3">Maklumat Kolej</td>
    </tr>
    <tr>
        <td style="width: 150px">Negeri:</td>
        <td>
            <asp:dropdownlist id="ddlNegeri" runat="server" width="350px"></asp:dropdownlist>
        </td>
    </tr>
    <tr>
        <td>Jenis Kolej:</td>
        <td>
            <asp:dropdownlist id="ddlJenis" runat="server" autopostback="true" width="350px"></asp:dropdownlist>
    </tr>
    <tr>
        <td>Nama Kolej:</td>
        <td>
            <asp:dropdownlist id="ddlKolej" runat="server" autopostback="true" width="350px"></asp:dropdownlist>
        </td>
    </tr>
</table>
<div class="info" id="divMsg1" runat="server">
    <asp:label id="lblMsg2" runat="server" text=""></asp:label>
</div>
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="3">Maklumat Calon</td>
    </tr>
    <tr>
        <td style="width: 150px">Peringkat Pengajian:</td>
        <td>
            <asp:label id="Label1" runat="server" text="PRA DIPLOMA"></asp:label>
        </td>
    </tr>
    <tr>
        <td>Kohort:</td>
        <td>
            <asp:dropdownlist id="ddlTahun" runat="server" autopostback="false" width="100px">
            </asp:dropdownlist>
        </td>
    </tr>
    <tr>
        <td>Semester:</td>
        <td>
            <asp:dropdownlist id="ddlSemester" runat="server" autopostback="false" width="100px">
            </asp:dropdownlist>
        </td>
    </tr>
    <tr>
        <td>Sesi Pengambilan:</td>
        <td>
            <asp:checkboxlist id="chkSesi" runat="server" repeatdirection="Horizontal" autopostback="true">
                <asp:listitem>1</asp:listitem>
                <asp:listitem>2</asp:listitem>
            </asp:checkboxlist>
    </tr>
    <tr>
        <td>Nama Bidang:</td>
        <td>
            <asp:dropdownlist id="ddlKluster" runat="server" autopostback="true" width="350px"></asp:dropdownlist>
        </td>
    </tr>
    <tr>
        <td>Kod Program:</td>
        <td>
            <asp:dropdownlist id="ddlKodKursus" runat="server" autopostback="true" width="350px">
            </asp:dropdownlist>
        </td>
    </tr>
    <tr>
        <td>Nama Kelas:</td>
        <td>
            <asp:dropdownlist id="ddlNamaKelas" runat="server" autopostback="false" width="350px">
            </asp:dropdownlist>
        </td>
    </tr>
    <tr>
        <td colspan="2"></td>
    </tr>
    <tr>
        <td>Nama Calon: </td>
        <td>
            <asp:textbox id="txtNama" runat="server" width="350px" maxlength="250"></asp:textbox>
        </td>
    </tr>
    <tr>
        <td>Mykad:</td>
        <td>
            <asp:textbox id="txtMYKAD" runat="server" width="350px" maxlength="50"></asp:textbox>
        </td>
    </tr>
    <tr>
        <td>Jantina:</td>
        <td>
            <asp:checkboxlist id="chkJantina" runat="server" width="349px" repeatdirection="Horizontal">
                <asp:listitem>LELAKI</asp:listitem>
                <asp:listitem>PEREMPUAN</asp:listitem>
            </asp:checkboxlist>
    </tr>
    <tr>
        <td>Kaum:</td>
        <td>
            <asp:dropdownlist id="ddlKaum" runat="server" autopostback="false" width="350px">
            </asp:dropdownlist>
        </td>
    </tr>
    <tr>
        <td>Agama:</td>
        <td>
            <asp:checkboxlist id="chkAgama" runat="server" width="349px" repeatdirection="Horizontal">
                <asp:listitem>ISLAM</asp:listitem>
                <asp:listitem>LAIN-LAIN</asp:listitem>
            </asp:checkboxlist>
    </tr>
    <tr>
        <td>Emel:</td>
        <td>
            <asp:textbox id="txtEmail" runat="server" width="350px" maxlength="250"></asp:textbox>
        </td>
    </tr>

    <tr>
        <td>Catatan
            :</td>
        <td>
            <asp:textbox id="txtCatatan" runat="server" width="350px" maxlength="250" height="117px"></asp:textbox>
        </td>
    </tr>
    <tr>
        <td colspan="2"></td>
    </tr>
    <tr>
        <td>&nbsp</td>
    </tr>
    <tr>
        <td></td>
        <td colspan="2">
            <asp:button id="btnCreate" runat="server" text="Kemaskini" cssclass="fbbutton" />
            &nbsp;</td>
    </tr>
</table>

<div class="info" id="divMsg2" runat="server">
    <asp:label id="lblMsg" runat="server" text="Mesej..."></asp:label>
</div>
