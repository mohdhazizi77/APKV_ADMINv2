<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="pembetulan.markah.PAA.aspx.vb" Inherits="apkv_v2_admin.pembetulan_markah_PAA" %>

<%@ Register Src="~/commoncontrol/pembetulan_markah_PA_akademik.ascx" TagPrefix="uc1" TagName="pembetulan_markah_PA_akademik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:pembetulan_markah_PA_akademik runat="server" id="pembetulan_markah_PA_akademik" />
</asp:Content>
