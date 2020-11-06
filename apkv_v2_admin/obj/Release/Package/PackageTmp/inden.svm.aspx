<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="inden.svm.aspx.vb" Inherits="apkv_v2_admin.inden_svm1" %>
<%@ Register src="commoncontrol/inden_svm.ascx" tagname="inden_svm" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:inden_svm ID="inden_svm" runat="server" />
</asp:Content>
