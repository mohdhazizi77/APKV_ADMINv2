<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/svmu.Master" CodeBehind="pendaftaran_calon_ulang_online_contact.aspx.vb" Inherits="apkv_v2_admin.pendaftaran_calon_ulang_online_contact" %>

<%@ Register Src="~/commoncontrol/contact_us.ascx" TagPrefix="uc1" TagName="contact_us" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:contact_us runat="server" ID="contact_us" />
</asp:Content>
