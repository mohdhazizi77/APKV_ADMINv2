<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="apkv.support.aspx.vb" Inherits="apkv_v2_admin.apkv_support" %>

<%@ Register Src="~/commoncontrol/apkv_support.ascx" TagPrefix="uc1" TagName="apkv_support" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:apkv_support runat="server" id="apkv_support" />
</asp:Content>
