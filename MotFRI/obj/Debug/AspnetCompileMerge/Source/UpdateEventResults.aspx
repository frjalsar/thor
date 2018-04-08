<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UpdateEventResults.aspx.cs" Inherits="MotFRI.UpdateEventResults" %>
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
    <asp:Label ID="Label1" runat="server" Text="Skráning árangurs" Font-Bold="True" Font-Italic="True" Font-Size="X-Large" ></asp:Label><br />
    <asp:TextBox ID="EventName" runat="server" ReadOnly="true" Width="1357px" Font-Bold="True" Font-Size="Large" BorderStyle="None"></asp:TextBox>
        <asp:TextBox ID="CurrPageAndHeat" runat="server" Width="340px" ReadOnly="true" Visible="false"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
     <asp:Label ID="Label2" runat="server" Text="Veldu innsláttarröð: "></asp:Label>
        <asp:DropDownList ID="SelectTabOrder" runat="server" AutoPostBack="True">
            <asp:ListItem Value="0">Línur</asp:ListItem>
            <asp:ListItem Value="1">Dálkar</asp:ListItem>
        </asp:DropDownList>   
    <br />
    <asp:Button ID="FillOutHeights" runat="server" OnClick="FillOutHeights_Click" style="margin-left: 560px" Text="Fylla út hæðir" Visible="False" Width="120px" />
        <br />
        <br />
        <asp:Label ID="FilterOnHeatLabel" runat="server" Text="Afmarka við riðil eða hóp: "></asp:Label>
        <asp:DropDownList ID="FilterOnSelectedHeadDropDownList" runat="server" AutoPostBack="True">
            <asp:ListItem Value="0">Allir riðlar</asp:ListItem>
        </asp:DropDownList>
        &nbsp;<asp:TextBox ID="SelectedHeatFilter" runat="server" Visible="false" BorderStyle="None"></asp:TextBox>
        <br />
        <br />
        <asp:PlaceHolder ID="PH1" runat="server"></asp:PlaceHolder>
        <br />
        <br />
        <asp:Label ID="EvStatus" runat="server" Text="Staða keppni:"></asp:Label>
        &nbsp;&nbsp;
        <asp:DropDownList ID="EventStatusDropDown" runat="server" Width="150px" AutoPostBack="True">
            <asp:ListItem Value="0">Ekki hafin</asp:ListItem>
            <asp:ListItem Value="1">Stendur yfir</asp:ListItem>
            <asp:ListItem Value="2">Lokið</asp:ListItem>
        </asp:DropDownList>
        <br /><br />
        <asp:TextBox ID="MsgBox" runat="server" ReadOnly="true" BorderStyle="None" Font-Size="Large" ForeColor="Red" style="font-weight: 700; font-style: italic" Width="1372px"></asp:TextBox><br />
    <asp:Button ID="SaveValues" runat="server" Text="Vista" OnClick="SaveValues_Click" Height="37px" Width="81px" />&nbsp;&nbsp;


    <asp:Button ID="AddCompetitor" runat="server" Text="Bæta við keppanda" Width="192px" OnClick="AddCompetitor_Click" />&nbsp;&nbsp
        
    <asp:Button ID="ShowResultOrder" runat="server" Text="Úrslitaröð" OnClick="ShowResultOrder_Click" Width="122px" />&nbsp;&nbsp;
    <asp:Button ID="PrintResults" runat="server" Text="Prenta úrslit" OnClick="PrintResults_Click" Width="140px" />
    &nbsp;&nbsp;<asp:Button ID="SeedCompetitors" runat="server" Text="Raða keppendum" Visible="true" OnClick="SeedCompetitors_Click" Width="185px" />&nbsp;&nbsp;        
    <asp:Button ID="BackToEvents" runat="server" Text="Til baka" OnClick="BackToEvents_Click" />
             
        &nbsp;
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Ritarablað" />
             
        &nbsp;&nbsp;<asp:Button ID="RetriveRunnersFromPrevRnd" runat="server" Text="Sækja úr ..." OnClick="RetriveRunnersFromPrevRnd_Click" />
        &nbsp;&nbsp;<asp:Button ID="LiveUpdate" runat="server" Text="Lifandi úrslit" OnClick="LiveUpdate_Click" Visible="False" Width="141px" />&nbsp;&nbsp;
        <asp:Button ID="ReadLynxFiles" runat="server" Text="Lesa Lynx tímatökuskrár" OnClick="ReadLynxFiles_Click" Width="238px" />
          <asp:TextBox ID="SavedEventStatus" runat="server" ReadOnly="true" Visible="false"></asp:TextBox>
             
  </asp:panel>
</asp:Content>
