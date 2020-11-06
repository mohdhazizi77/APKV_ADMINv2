<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="markah.sejarah.aspx.vb" Inherits="apkv_v2_admin.markah_sejarah" %>
<%@ Register src="commoncontrol/markah_sejarah.ascx" tagname="markah_sejarah" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:markah_sejarah ID="markah_sejarah" runat="server" />
</asp:Content>
