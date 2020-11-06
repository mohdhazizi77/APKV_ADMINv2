<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="markah.ulang.BMSetara.aspx.vb" Inherits="apkv_v2_admin.markah_ulang_BMSetara" %>

<%@ Register Src="~/commoncontrol/markah_ulang_BMSetara.ascx" TagPrefix="uc1" TagName="markah_ulang_BMSetara" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:markah_ulang_BMSetara runat="server" id="markah_ulang_BMSetara" />
</asp:Content>
