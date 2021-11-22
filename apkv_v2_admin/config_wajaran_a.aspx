<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="config_wajaran_a.aspx.vb" Inherits="apkv_v2_admin.config_wajaran_a" %>

<%@ Register Src="~/commoncontrol/config_wajaran_a.ascx" TagPrefix="uc1" TagName="config_wajaran_a" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:config_wajaran_a runat="server" id="config_wajaran_a" />
</asp:Content>
