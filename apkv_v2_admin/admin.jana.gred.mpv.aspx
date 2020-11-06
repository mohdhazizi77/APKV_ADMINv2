<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin.jana.gred.mpv.aspx.vb" Inherits="apkv_v2_admin.admin_jana_gred_mpv" %>

<%@ Register src="commoncontrol/admin_gred_skor_MP.ascx" tagname="admin_gred_skor_MP" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:admin_gred_skor_MP ID="admin_gred_skor_MP1" runat="server" />
</asp:Content>
