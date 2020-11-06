<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="jana.FIK.semasa.aspx.vb" Inherits="apkv_v2_admin.jana_FIK_semasa" %>

<%@ Register Src="~/commoncontrol/jana_FIK_semasa.ascx" TagPrefix="uc1" TagName="jana_FIK_semasa" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:jana_FIK_semasa runat="server" id="jana_FIK_semasa" />
</asp:Content>
