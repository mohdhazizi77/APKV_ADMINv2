<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="config_tetapan_list.aspx.vb" Inherits="apkv_v2_admin.config_tetapan_list" %>

<%@ Register Src="~/commoncontrol/config_tetapan_list.ascx" TagPrefix="uc1" TagName="config_tetapan_list" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:config_tetapan_list runat="server" id="config_tetapan_list" />
</asp:Content>
