<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="user.update.aspx.vb" Inherits="apkv_v2_admin.user_update1" %>
<%@ Register src="commoncontrol/user_update.ascx" tagname="user_update" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:user_update ID="user_update" runat="server" />
</asp:Content>
