<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="jana.rawmarkah.BMSetara.aspx.vb" Inherits="apkv_v2_admin.jana_rawmarkah_BMSetara" %>

<%@ Register Src="~/commoncontrol/rawmarkah_BMSetara.ascx" TagPrefix="uc1" TagName="rawmarkah_BMSetara" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:rawmarkah_BMSetara runat="server" id="rawmarkah_BMSetara" />
</asp:Content>
