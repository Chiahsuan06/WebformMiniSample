<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucCoverImage.ascx.cs" Inherits="TryWebApplication.ucCoverImage" %>

<div runat="server" id="divMain" style="background-color:aquamarine">
    <img runat="server" id="imgCover" src="貓.png" />
    <span>
        <asp:Literal ID="ltlTitle" runat="server"></asp:Literal>
    </span>
    <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" />
</div>