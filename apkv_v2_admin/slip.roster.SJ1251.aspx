<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="slip.roster.SJ1251.aspx.vb" Inherits="apkv_v2_admin.slip_roster_SJ1251" %>

<%@ Register Src="~/commoncontrol/slip_roster_SJ1251.ascx" TagPrefix="uc1" TagName="slip_roster_SJ1251" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:slip_roster_SJ1251 runat="server" id="slip_roster_SJ1251" />
</asp:Content>
