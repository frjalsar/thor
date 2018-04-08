<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="MotFRI._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    
     
        <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="~/Motalisti.aspx">Mótalisti (Stored Procedure)</asp:HyperLink>
        <br />
        
        <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="~/Keppnisvellir.aspx">Keppnisvellir</asp:HyperLink>
        <br />
        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/NyttMot.aspx">Skrá nýtt mót</asp:HyperLink><br />
        <br /><br />
        </asp:Content>
