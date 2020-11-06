<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="kemaskini.kes.t.aspx.vb" Inherits="apkv_v2_admin.kemaskini_kes_t" %>

<%@ Register Src="~/commoncontrol/kemaskini_kes_t.ascx" TagPrefix="uc1" TagName="kemaskini_kes_t" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:kemaskini_kes_t runat="server" ID="kemaskini_kes_t" />
</asp:Content>
