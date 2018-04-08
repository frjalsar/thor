<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UpdateEventResults.aspx.cs" Inherits="MotFRI.UpdateEventResults" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:panel id="Panel01" DefaultButton="SaveValues" runat="server">
    <asp:Label ID="Label1" runat="server" Text="Skráning árangurs" Font-Bold="True" Font-Italic="True" Font-Size="X-Large" ></asp:Label><br />
    <asp:TextBox ID="EventName" runat="server" ReadOnly="true" Width="1357px" Font-Bold="True" Font-Size="Large" BorderStyle="None"></asp:TextBox>
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
    <asp:Button ID="SaveValues" runat="server" Text="Vista" OnClick="SaveValues_Click" Height="37px" Width="81px" />&nbsp;&nbsp;&nbsp;


    <asp:Button ID="AddCompetitor" runat="server" Text="Bæta við keppanda" Width="165px" OnClick="AddCompetitor_Click" />&nbsp;&nbsp;&nbsp;
        
    <asp:Button ID="ShowResultOrder" runat="server" Text="Úrslitaröð" OnClick="ShowResultOrder_Click" />&nbsp;&nbsp;&nbsp;
    <asp:Button ID="PrintResults" runat="server" Text="Prenta úrslit" OnClick="PrintResults_Click" />
    &nbsp;&nbsp;&nbsp;<asp:Button ID="SeedCompetitors" runat="server" Text="Raða keppendum" Visible="true" OnClick="SeedCompetitors_Click" />
    
          <asp:TextBox ID="SavedEventStatus" runat="server" ReadOnly="true" Visible="false"></asp:TextBox>

        
    <asp:Button ID="BackToEvents" runat="server" Text="Til baka" OnClick="BackToEvents_Click" />
             
        &nbsp;
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Ritarablað" />
             
  </asp:panel>
</asp:Content>
