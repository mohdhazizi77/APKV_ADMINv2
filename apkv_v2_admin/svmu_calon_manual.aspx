<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="svmu_calon_manual.aspx.vb" Inherits="apkv_v2_admin.svmu_calon_manual" %>

<%@ Register Src="~/commoncontrol/svmu_calon_manual.ascx" TagPrefix="uc1" TagName="svmu_calon_manual" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:svmu_calon_manual runat="server" id="svmu_calon_manual" />
</asp:Content>
