<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CompetitorsFromClub.aspx.cs" Inherits="MotFRI.CompetitorsFromClub" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:panel id="Panel01" DefaultButton="SaveRegistration" runat="server">
    <asp:TextBox ID="CompetitonName" Width="1200px" ReadOnly="true" runat="server" Font-Size="X-Large" BorderStyle="None"></asp:TextBox>
    <br />
    <br />
    <asp:TextBox ID="ClubCode" Width="1406px" ReadOnly="true" runat="server" Font-Size="Small" BorderStyle="None" Height="22px"></asp:TextBox>
    <br />
    <asp:TextBox ID="Message" Width="1200px" ReadOnly="true" runat="server" Font-Size="Large" BorderStyle="None" ForeColor="Red"></asp:TextBox>
    <br />
    <asp:PlaceHolder ID="PH2" runat="server" EnableViewState="true"></asp:PlaceHolder>
    <br />
    <asp:Button ID="SaveRegistration" runat="server" Text="Vista skráningu" OnClick="SaveRegistration_Click" />
        </asp:panel>
   </asp:Content>
