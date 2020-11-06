<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="gred.sjsetara.list.aspx.vb" Inherits="apkv_v2_admin.gred_sjsetara_list" %>

<%@ Register Src="~/commoncontrol/gred_sjsetara_list.ascx" TagPrefix="uc1" TagName="gred_sjsetara_list" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:gred_sjsetara_list runat="server" id="gred_sjsetara_list" />
</asp:Content>
