<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="pentaksirbm_senarai_kjpp.aspx.vb" Inherits="apkv_v2_admin.pentaksirbm_senarai_kjpp1" %>

<%@ Register Src="~/commoncontrol/pentaksirbm_senarai_kjpp.ascx" TagPrefix="uc1" TagName="pentaksirbm_senarai_kjpp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:pentaksirbm_senarai_kjpp runat="server" id="pentaksirbm_senarai_kjpp" />
</asp:Content>
