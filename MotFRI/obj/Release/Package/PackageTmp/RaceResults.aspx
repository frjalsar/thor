<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RaceResults.aspx.cs" Inherits="MotFRI.EventRes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

        <asp:Label ID="Label1" runat="server" Text="Úrslitaröð" Font-Bold="True" Font-Italic="True" Font-Size="X-Large" ></asp:Label><br />
    <asp:TextBox ID="EventName" runat="server" ReadOnly="true" Width="1357px" Font-Bold="True" Font-Size="Large"></asp:TextBox>
    <br />
    <br />
    <asp:PlaceHolder ID="PH1" runat="server"></asp:PlaceHolder>
        <br />
        <br />
        <asp:Button ID="SaveRaceResultOrder" runat="server" OnClick="SaveRaceResultOrder_Click" Text="Vista" />
    &nbsp;
        <asp:Button ID="BackButton" runat="server" Text="Til baka" OnClick="BackButton_Click" />
    <br />


</asp:Content>
