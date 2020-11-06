<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="config.gred.svm.aspx.vb" Inherits="apkv_v2_admin.config_gred_svm" %>
<%@ Register src="commoncontrol/config_penskoran_svm.ascx" tagname="config_penskoran_svm" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:config_penskoran_svm ID="config_penskoran_svm1" runat="server" />
</asp:Content>
