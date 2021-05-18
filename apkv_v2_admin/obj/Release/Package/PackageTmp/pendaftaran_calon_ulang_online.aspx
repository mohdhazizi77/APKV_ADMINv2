<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/svmu.Master" CodeBehind="pendaftaran_calon_ulang_online.aspx.vb" Inherits="apkv_v2_admin.pendaftaran_calon_ulang_online" %>

<%@ Register Src="~/commoncontrol/pendaftaran_calon_ulang_online.ascx" TagPrefix="uc1" TagName="pendaftaran_calon_ulang_online" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:pendaftaran_calon_ulang_online runat="server" id="pendaftaran_calon_ulang_online" />
</asp:Content>
