<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ClubCompetitorsWithReg.aspx.cs" Inherits="MotFRI.ClubCompetitorsWithReg" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:panel id="Panel01" DefaultButton="SaveValues" runat="server">

            <asp:TextBox ID="CompetitonName" Width="1200px" ReadOnly="true" runat="server" Font-Size="X-Large" BorderStyle="None"></asp:TextBox>
        <br /><br />
    <asp:Label ID="Label1" runat="server" Text="Keppendur félags" Font-Bold="True" Font-Italic="True" Font-Size="X-Large" ></asp:Label><br />
    <br />
    <asp:TextBox ID="ClubCode" Width="1406px" ReadOnly="true" runat="server" Font-Size="Small" BorderStyle="None" Height="22px"></asp:TextBox>
    <br />
    <asp:TextBox ID="Message" Width="1200px" ReadOnly="true" runat="server" Font-Size="Large" BorderStyle="None" ForeColor="Red"></asp:TextBox>
        <br />
        <asp:PlaceHolder ID="PH1" runat="server"></asp:PlaceHolder>
        <br />
<br />
    <asp:Button ID="SaveValues" runat="server" Text="Vista" OnClick="SaveValues_Click" Height="37px" Width="81px" />&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
        <br /><br />
    <asp:Button ID="BackToEvents" runat="server" Text="Til baka" OnClick="BackToEvents_Click" />
             

  </asp:panel>

</asp:Content>
