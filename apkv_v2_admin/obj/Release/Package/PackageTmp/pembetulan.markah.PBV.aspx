<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="pembetulan.markah.PBV.aspx.vb" Inherits="apkv_v2_admin.pembetulan_markah_PBV" %>

<%@ Register Src="~/commoncontrol/pembetulan_markah_PB_vokasional.ascx" TagPrefix="uc1" TagName="pembetulan_markah_PB_vokasional" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:pembetulan_markah_PB_vokasional runat="server" id="pembetulan_markah_PB_vokasional" />
</asp:Content>
