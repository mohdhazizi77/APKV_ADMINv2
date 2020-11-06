<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="inden.kohort.aspx.vb" Inherits="apkv_v2_admin.inden_kohort1" %>
<%@ Register src="commoncontrol/inden_kohort.ascx" tagname="inden_kohort" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:inden_kohort ID="inden_kohort" runat="server" />
</asp:Content>
