<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/svmu.Master" CodeBehind="svmu_rumusan_pendaftaran.aspx.vb" Inherits="apkv_v2_admin.svmu_rumusan_pendaftaran1" %>

<%@ Register Src="~/commoncontrol/svmu_rumusan_pendaftaran.ascx" TagPrefix="uc1" TagName="svmu_rumusan_pendaftaran" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:svmu_rumusan_pendaftaran runat="server" id="svmu_rumusan_pendaftaran" />
</asp:Content>
