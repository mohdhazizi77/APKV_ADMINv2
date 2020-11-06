<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="pembetulan.markah.PBA.aspx.vb" Inherits="apkv_v2_admin.pembetulan_markah_PBA" %>

<%@ Register Src="~/commoncontrol/pembetulan_markah_PB_akademik.ascx" TagPrefix="uc1" TagName="pembetulan_markah_PB_akademik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:pembetulan_markah_PB_akademik runat="server" id="pembetulan_markah_PB_akademik" />
</asp:Content>
