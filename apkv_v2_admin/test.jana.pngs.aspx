<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="test.jana.pngs.aspx.vb" Inherits="apkv_v2_admin.test_jana_pngs" %>

<%@ Register Src="~/commoncontrol/test.jana.pngs.ascx" TagPrefix="uc1" TagName="testjanapngs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:testjanapngs runat="server" id="testjanapngs" />
</asp:Content>
