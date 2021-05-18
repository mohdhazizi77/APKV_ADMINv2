<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/svmu.Master" CodeBehind="svmu.tindakan.calon.aspx.vb" Inherits="apkv_v2_admin.svmu_tindakan_calon" %>

<%@ Register Src="~/commoncontrol/svmu_tindakan_calon.ascx" TagPrefix="uc1" TagName="svmu_tindakan_calon" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:svmu_tindakan_calon runat="server" id="svmu_tindakan_calon" />
</asp:Content>
