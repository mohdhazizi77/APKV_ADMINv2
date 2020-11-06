<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="pelajar.ulang.akademik.v2.aspx.vb" Inherits="apkv_v2_admin.pelajar_ulang_akademik_v2" %>

<%@ Register Src="~/commoncontrol/pelajar_ulang_akademik_v2.ascx" TagPrefix="uc1" TagName="pelajar_ulang_akademik_v2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:pelajar_ulang_akademik_v2 runat="server" ID="pelajar_ulang_akademik_v2" />
</asp:Content>
