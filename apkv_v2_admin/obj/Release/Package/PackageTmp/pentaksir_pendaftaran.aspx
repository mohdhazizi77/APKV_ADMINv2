<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="pentaksir_pendaftaran.aspx.vb" Inherits="apkv_v2_admin.pentaksir_pendaftaran1" %>

<%@ Register Src="~/commoncontrol/pentaksir_pendaftaran.ascx" TagPrefix="uc1" TagName="pentaksir_pendaftaran" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:pentaksir_pendaftaran runat="server" id="pentaksir_pendaftaran" />
</asp:Content>
