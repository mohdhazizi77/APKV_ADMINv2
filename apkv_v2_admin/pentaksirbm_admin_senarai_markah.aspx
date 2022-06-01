<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="pentaksirbm_admin_senarai_markah.aspx.vb" Inherits="apkv_v2_admin.pentaksirbm_admin_senarai_markah1" %>

<%@ Register Src="~/commoncontrol/pentaksirbm_admin_senarai_markah.ascx" TagPrefix="uc1" TagName="pentaksirbm_admin_senarai_markah" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:pentaksirbm_admin_senarai_markah runat="server" id="pentaksirbm_admin_senarai_markah" />
</asp:Content>
