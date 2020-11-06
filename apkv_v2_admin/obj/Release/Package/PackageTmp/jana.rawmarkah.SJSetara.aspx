<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="jana.rawmarkah.SJSetara.aspx.vb" Inherits="apkv_v2_admin.jana_rawmarkah_SJSetara" %>

<%@ Register Src="~/commoncontrol/rawmarkah_SJSetara.ascx" TagPrefix="uc1" TagName="rawmarkah_SJSetara" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:rawmarkah_SJSetara runat="server" id="rawmarkah_SJSetara" />
</asp:Content>
