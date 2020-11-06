<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="gred.sjsetara.view.aspx.vb" Inherits="apkv_v2_admin.gred_sjsetara_view" %>

<%@ Register Src="~/commoncontrol/gred_sjsetara_view.ascx" TagPrefix="uc1" TagName="gred_sjsetara_view" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:gred_sjsetara_view runat="server" id="gred_sjsetara_view" />
</asp:Content>
