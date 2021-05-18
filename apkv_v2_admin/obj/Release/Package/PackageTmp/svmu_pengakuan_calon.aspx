<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/svmu.Master" CodeBehind="svmu_pengakuan_calon.aspx.vb" Inherits="apkv_v2_admin.svmu_pengakuan_calon1" %>

<%@ Register Src="~/commoncontrol/svmu_pengakuan_calon.ascx" TagPrefix="uc1" TagName="svmu_pengakuan_calon" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:svmu_pengakuan_calon runat="server" id="svmu_pengakuan_calon" />
</asp:Content>
