<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="config.tempoh.masa.aspx.vb" Inherits="apkv_v2_admin.config_tempoh_masa" %>
<%@ Register src="commoncontrol/config_tempoh_tahun.ascx" tagname="config_tempoh_tahun" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:config_tempoh_tahun ID="config_tempoh_tahun1" runat="server" />
</asp:Content>
