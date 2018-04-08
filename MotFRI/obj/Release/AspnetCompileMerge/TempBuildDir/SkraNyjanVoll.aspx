<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SkraNyjanVoll.aspx.cs" Inherits="MotFRI.SkraNyjanVoll" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <br />
     <p style="font-size: x-large">
        Skráning á nýjum velli:</p>
    <br />
    <br />
    <asp:Table ID="NewVenue" runat="server">
        <asp:TableRow>
          <asp:TableCell>Heiti vallar
          </asp:TableCell>
          <asp:TableCell><asp:TextBox ID="HeitiVallar" runat="server" Width="200px"></asp:TextBox>
          </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
          <asp:TableCell>Staður
          </asp:TableCell>
          <asp:TableCell><asp:TextBox ID="Stadur" runat="server" Width="200px"></asp:TextBox>
          </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
          <asp:TableCell>Úti eða inni
          </asp:TableCell>
          <asp:TableCell><asp:DropDownList ID="UtiInni" Width="100px" runat="server">
          <asp:ListItem Value="0">Úti</asp:ListItem>
        <asp:ListItem Value="1">Inni</asp:ListItem> 
      </asp:DropDownList><br />
          </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
          <asp:TableCell>Fjöldi beinna brauta
          </asp:TableCell>
          <asp:TableCell><asp:TextBox ID="FjBrauta" runat="server" Width="200px"></asp:TextBox>
          </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
          <asp:TableCell>Fjöldi hringbrauta
          </asp:TableCell>
          <asp:TableCell><asp:TextBox ID="FjBeinnaBrauta" runat="server" Width="200px"></asp:TextBox>
          </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    <br />
    <br />
    <br />
    
    <br />
    <br />
    <asp:Button ID="Button" runat="server" Text="Skrá völlinn" 
        onclick="Button_Click" />&nbsp;&nbsp;<asp:Button ID="Button2"
        runat="server" Text="Hætta við" PostBackUrl="~/Keppnisvellir.aspx" 
        onclick="Button2_Click" />
</asp:Content>
