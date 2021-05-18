<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="svmu_senarai_calon.aspx.vb" Inherits="apkv_v2_admin.svmu_senarai_calon1" %>

<%@ Register Src="~/commoncontrol/svmu_senarai_calon.ascx" TagPrefix="uc1" TagName="svmu_senarai_calon" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:svmu_senarai_calon runat="server" ID="svmu_senarai_calon" />
</asp:Content>
