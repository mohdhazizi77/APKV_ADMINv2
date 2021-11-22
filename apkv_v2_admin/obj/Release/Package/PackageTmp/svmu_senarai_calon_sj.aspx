<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="svmu_senarai_calon_sj.aspx.vb" Inherits="apkv_v2_admin.svmu_senarai_calon_sj1" %>

<%@ Register Src="~/commoncontrol/svmu_senarai_calon_sj.ascx" TagPrefix="uc1" TagName="svmu_senarai_calon_sj" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:svmu_senarai_calon_sj runat="server" id="svmu_senarai_calon_sj" />
</asp:Content>
