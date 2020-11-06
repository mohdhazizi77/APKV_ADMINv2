<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="apkv.borangmarkah.pa.aspx.vb" Inherits="apkv_v2_admin.apkv_borangmarkah_pa" %>
<%@ Register src="commoncontrol/borang_markah_PA.ascx" tagname="borang_markah_PA" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:borang_markah_PA ID="borang_markah_PA1" runat="server" />
</asp:Content>
