<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="config_tetapan_add.aspx.vb" Inherits="apkv_v2_admin.config_tetapan_add" %>

<%@ Register Src="~/commoncontrol/config_tetapan_add.ascx" TagPrefix="uc1" TagName="config_tetapan_add" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:config_tetapan_add runat="server" id="config_tetapan_add" />
</asp:Content>
