<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="jana.keseluruhan.akademik.aspx.vb" Inherits="apkv_v2_admin.jana_keseluruhan_akademik" %>
<%@ Register src="commoncontrol/jana_keseluruhan_akademik.ascx" tagname="jana_keseluruhan_akademik" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:jana_keseluruhan_akademik ID="jana_keseluruhan_akademik" runat="server" />
</asp:Content>
