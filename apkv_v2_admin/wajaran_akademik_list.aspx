<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="wajaran_akademik_list.aspx.vb" Inherits="apkv_v2_admin.wajaran_akademik_list1" %>

<%@ Register Src="~/commoncontrol/wajaran_akademik_list.ascx" TagPrefix="uc1" TagName="wajaran_akademik_list" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:wajaran_akademik_list runat="server" id="wajaran_akademik_list" />
</asp:Content>
