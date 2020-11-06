<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="muatnaik.bahan.aspx.vb" Inherits="apkv_v2_admin.muatnaik_bahan1" %>
<%@ Register src="commoncontrol/muatnaik_bahan.ascx" tagname="muatnaik_bahan" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:muatnaik_bahan ID="muatnaik_bahan" runat="server" />
</asp:Content>
