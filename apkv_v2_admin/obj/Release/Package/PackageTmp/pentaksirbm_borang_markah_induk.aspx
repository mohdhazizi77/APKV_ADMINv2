<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="pentaksirbm_borang_markah_induk.aspx.vb" Inherits="apkv_v2_admin.pentaksirbm_borang_markah_induk" %>

<%@ Register Src="~/commoncontrol/pentaksirbm_borang_markah_induk.ascx" TagPrefix="uc1" TagName="pentaksirbm_borang_markah_induk" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:pentaksirbm_borang_markah_induk runat="server" id="pentaksirbm_borang_markah_induk" />
</asp:Content>
