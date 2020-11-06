<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="SlipBMSJ.aspx.vb" Inherits="apkv_v2_admin.SlipBMSJ" %>

<%@ Register Src="~/commoncontrol/SlipBMSJ.ascx" TagPrefix="uc1" TagName="SlipBMSJ" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:SlipBMSJ runat="server" id="SlipBMSJ" />
</asp:Content>
