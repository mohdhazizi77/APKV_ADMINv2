<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="muatturun.bahan.aspx.vb" Inherits="apkv_v2_admin.muatturun_bahan" %>

<%@ register src="~/commoncontrol/muatturun.bahan.ascx" tagprefix="uc1" tagname="muatturunbahan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:muatturunbahan runat="server" id="muatturunbahan" />
</asp:Content>
