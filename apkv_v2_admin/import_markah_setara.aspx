<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="import_markah_setara.aspx.vb" Inherits="apkv_v2_admin.import_markah_setara1" %>

<%@ Register Src="~/commoncontrol/import_markah_setara.ascx" TagPrefix="uc1" TagName="import_markah_setara" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:import_markah_setara runat="server" id="import_markah_setara" />
</asp:Content>
