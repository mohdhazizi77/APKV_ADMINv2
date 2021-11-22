<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="svmu_senarai_calon_bm.aspx.vb" Inherits="apkv_v2_admin.svmu_senarai_calon_bm1" %>

<%@ Register Src="~/commoncontrol/svmu_senarai_calon_bm.ascx" TagPrefix="uc1" TagName="svmu_senarai_calon_bm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:svmu_senarai_calon_bm runat="server" id="svmu_senarai_calon_bm" />
</asp:Content>
