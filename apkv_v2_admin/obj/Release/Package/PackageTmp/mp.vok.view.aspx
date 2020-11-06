<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="mp.vok.view.aspx.vb" Inherits="apkv_v2_admin.mp_vok_view" %>
<%@ Register src="commoncontrol/mp_vok_view.ascx" tagname="mp_vok_view" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:mp_vok_view ID="mp_vok_view1" runat="server" />
</asp:Content>
