<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/svmu.Master" CodeBehind="svmu_pusat_peperiksaan.aspx.vb" Inherits="apkv_v2_admin.svmu_pusat_peperiksaan" %>

<%@ Register Src="~/commoncontrol/svmu_pusat_peperiksaan.ascx" TagPrefix="uc1" TagName="svmu_pusat_peperiksaan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:svmu_pusat_peperiksaan runat="server" id="svmu_pusat_peperiksaan" />
</asp:Content>
