﻿<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="kelas.calon.aspx.vb" Inherits="apkv_v2_admin.kelas_calon" %>
<%@ Register src="commoncontrol/kelas.calon.year.ascx" tagname="kelas" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:kelas ID="kelas1" runat="server" />
</asp:Content>
