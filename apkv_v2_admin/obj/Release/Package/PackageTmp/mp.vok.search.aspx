<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="mp.vok.search.aspx.vb" Inherits="apkv_v2_admin.mp_vok_search1" %>
<%@ Register src="commoncontrol/mp_vok_search.ascx" tagname="mp_vok_search" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:mp_vok_search ID="mp_vok_search" runat="server" />
</asp:Content>
