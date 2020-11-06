<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="inden.kursus.aspx.vb" Inherits="apkv_v2_admin.inden_kursus1" %>
<%@ Register src="commoncontrol/inden_kursus.ascx" tagname="inden_kursus" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:inden_kursus ID="inden_kursus" runat="server" />
</asp:Content>
