<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/svmu.Master" CodeBehind="svmu_pembayaran_pendaftaran.aspx.vb" Inherits="apkv_v2_admin.svmu_pembayaran_pendaftaran2" %>

<%@ Register Src="~/commoncontrol/svmu_pembayaran_pendaftaran.ascx" TagPrefix="uc1" TagName="svmu_pembayaran_pendaftaran" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:svmu_pembayaran_pendaftaran runat="server" id="svmu_pembayaran_pendaftaran" />
</asp:Content>
