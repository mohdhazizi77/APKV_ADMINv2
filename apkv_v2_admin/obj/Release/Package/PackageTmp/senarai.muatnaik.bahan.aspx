<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="senarai.muatnaik.bahan.aspx.vb" Inherits="apkv_v2_admin.senarai_muatnaik_bahan" %>


<%@ Register src="commoncontrol/senarai_muatnaik_bahan.ascx" tagname="senarai_muatnaik_bahan" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:senarai_muatnaik_bahan ID="senarai_muatnaik_bahan1" runat="server" />
</asp:Content>
