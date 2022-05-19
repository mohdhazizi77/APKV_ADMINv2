<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pentaksirbm_default.Master" CodeBehind="pentaksirbm_default.aspx.vb" Inherits="apkv_v2_admin.pentaksirbm_default1" %>

<%@ Register Src="~/commoncontrol/pentaksirbm_default.ascx" TagPrefix="uc1" TagName="pentaksirbm_default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:pentaksirbm_default runat="server" id="pentaksirbm_default" />
</asp:Content>
