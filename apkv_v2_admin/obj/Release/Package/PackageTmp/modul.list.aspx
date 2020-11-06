<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="modul.list.aspx.vb" Inherits="apkv_v2_admin.modul_list1" %>
<%@ Register src="commoncontrol/modul_list.ascx" tagname="modul_list" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:modul_list ID="modul_list" runat="server" />
</asp:Content>
