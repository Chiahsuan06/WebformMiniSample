<%@ Page Title="" Language="C#" MasterPageFile="~/TryMain.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TryWebApplication.Default" %>

<%@ Register Src="~/WebUserControl1.ascx" TagPrefix="uc1" TagName="WebUserControl1" %>
<%@ Register Src="~/ucCoverImage.ascx" TagPrefix="uc1" TagName="ucCoverImage" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <uc1:WebUserControl1 runat="server" id="WebUserControl1" />
    <uc1:ucCoverImage runat="server" id="ucCoverImage"  MyTitle="測試 uc 1" BackColor="Red"/>
    <uc1:WebUserControl1 runat="server" id="WebUserControl2" />
    <uc1:ucCoverImage runat="server" id="ucCoverImage1" />
    <uc1:WebUserControl1 runat="server" id="WebUserControl3" />
    <uc1:ucCoverImage runat="server" id="ucCoverImage2" />
    <uc1:WebUserControl1 runat="server" id="WebUserControl4" />
    <uc1:ucCoverImage runat="server" id="ucCoverImage3" />

</asp:Content>