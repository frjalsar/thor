<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ClubCompetitorsWithReg.aspx.cs" Inherits="MotFRI.ClubCompetitorsWithReg" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
<script>
  (function(i,s,o,g,r,a,m){i['GoogleAnalyticsObject']=r;i[r]=i[r]||function(){
  (i[r].q=i[r].q||[]).push(arguments)},i[r].l=1*new Date();a=s.createElement(o),
  m=s.getElementsByTagName(o)[0];a.async=1;a.src=g;m.parentNode.insertBefore(a,m)
  })(window,document,'script','https://www.google-analytics.com/analytics.js','ga');

  ga('create', 'UA-82054248-1', 'auto');
  ga('send', 'pageview');

</script>

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
        <br />
    <asp:Button ID="Back2" runat="server" Text="Til baka" OnClick="Back2_Click" /><br /><br />


        <asp:PlaceHolder ID="PH1" runat="server"></asp:PlaceHolder>
        <br />
<br />
    <asp:Button ID="SaveValues" runat="server" Text="Vista" OnClick="SaveValues_Click" Height="37px" Width="127px" Font-Size="X-Large" />
        <br /><br />
    <asp:Button ID="SaveAndReturn" runat="server" Text="Vista og loka"  Height="37px" Width="131px" OnClick="SaveAndReturn_Click" />
        <br />        
        <br />
    <asp:Button ID="BackToEvents" runat="server" Text="Til baka" OnClick="BackToEvents_Click" />
             

  </asp:panel>

</asp:Content>
