<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="sskt.aspx.vb" Inherits="apkv_v2_admin.sskt" %>

<%@ Register Src="~/commoncontrol/semak_semula_kes_t.ascx" TagPrefix="uc1" TagName="semak_semula_kes_t" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:semak_semula_kes_t runat="server" id="semak_semula_kes_t" />
</asp:Content>
