<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="gred_bmlisan_list.aspx.vb" Inherits="apkv_v2_admin.gred_bmlisan_list1" %>

<%@ Register Src="~/commoncontrol/gred_bmlisan_list.ascx" TagPrefix="uc1" TagName="gred_bmlisan_list" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:gred_bmlisan_list runat="server" id="gred_bmlisan_list" />
</asp:Content>
