<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/svmu.Master" CodeBehind="svmu_format_sampul.aspx.vb" Inherits="apkv_v2_admin.svmu_format_sampul1" %>

<%@ Register Src="~/commoncontrol/svmu_format_sampul.ascx" TagPrefix="uc1" TagName="svmu_format_sampul" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:svmu_format_sampul runat="server" id="svmu_format_sampul" />
</asp:Content>
