<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/svmu.Master" CodeBehind="svmu_maintenance.aspx.vb" Inherits="apkv_v2_admin.svmu_maintenance1" %>

<%@ Register Src="~/commoncontrol/svmu_maintenance.ascx" TagPrefix="uc1" TagName="svmu_maintenance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:svmu_maintenance runat="server" id="svmu_maintenance" />
</asp:Content>
