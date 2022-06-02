<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="pelajar_list_kemaskini.aspx.vb" Inherits="apkv_v2_admin.pelajar_list_kemaskini1" %>

<%@ Register Src="~/commoncontrol/pelajar_list_kemaskini.ascx" TagPrefix="uc1" TagName="pelajar_list_kemaskini" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:pelajar_list_kemaskini runat="server" ID="pelajar_list_kemaskini" />
</asp:Content>
