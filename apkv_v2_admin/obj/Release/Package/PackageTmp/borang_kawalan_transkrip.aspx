<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="borang_kawalan_transkrip.aspx.vb" Inherits="apkv_v2_admin.borang_kawalan_transkrip" %>

<%@ Register Src="~/commoncontrol/borang_kawalan_transkrip.ascx" TagPrefix="uc1" TagName="borang_kawalan_transkrip" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:borang_kawalan_transkrip runat="server" id="borang_kawalan_transkrip" />
</asp:Content>
