<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pentaksirbm.Master" CodeBehind="pentaksirbm_main.aspx.vb" Inherits="apkv_v2_admin.pentaksirbm_main" %>

<%@ Register Src="~/commoncontrol/pentaksirbm_main.ascx" TagPrefix="uc1" TagName="pentaksirbm_main" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:pentaksirbm_main runat="server" id="pentaksirbm_main" />
</asp:Content>
