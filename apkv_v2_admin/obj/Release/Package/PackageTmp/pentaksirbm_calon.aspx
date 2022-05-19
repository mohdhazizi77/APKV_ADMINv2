<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pentaksirbm.Master" CodeBehind="pentaksirbm_calon.aspx.vb" Inherits="apkv_v2_admin.pentaksirbm_calon1" %>

<%@ Register Src="~/commoncontrol/pentaksirbm_calon.ascx" TagPrefix="uc1" TagName="pentaksirbm_calon" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:pentaksirbm_calon runat="server" id="pentaksirbm_calon" />
</asp:Content>
