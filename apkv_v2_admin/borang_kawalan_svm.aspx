<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="borang_kawalan_svm.aspx.vb" Inherits="apkv_v2_admin.borang_kawalan_svm" %>

<%@ Register Src="~/commoncontrol/borang_kawalan_svm.ascx" TagPrefix="uc1" TagName="borang_kawalan_svm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:borang_kawalan_svm runat="server" id="borang_kawalan_svm" />
</asp:Content>
