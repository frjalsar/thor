<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="MotFRI.ChangePassword" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .auto-style1 {
            font-family: "Segoe UI";
            font-size: x-large;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <p>
        <br />
        <span class="auto-style1">&nbsp;&nbsp;Breyting á lykilorði</span></p>
    <p>
        &nbsp;</p>

  
    <asp:Table ID="Table1" runat="server" Font-Size="Large" Width="466px">
        <asp:TableRow>
            <asp:TableCell>
                <asp:Label ID="Label4" runat="server" Text="Notandi"></asp:Label>
            </asp:TableCell>
            <asp:TableCell>
                <asp:TextBox ID="UserID" runat="server" Width="200px"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>
                <asp:Label ID="Label1" runat="server" Text="Gamla lykilorðið"></asp:Label>
            </asp:TableCell>
            <asp:TableCell>
                <asp:TextBox ID="OldPassword" runat="server" TextMode="Password" Width="200px"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow Font-Size="Small">
            <asp:TableCell>&nbsp;</asp:TableCell></asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>
                <asp:Label ID="Label2" runat="server" Text="Nýja lykilorðið"></asp:Label>
            </asp:TableCell>
            <asp:TableCell>
                <asp:TextBox ID="NewPassword1" runat="server" TextMode="Password" Width="200px"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>
                <asp:Label ID="Label3" runat="server" Text="Nýja lykilorðið aftur"></asp:Label>
            </asp:TableCell>
            <asp:TableCell>
                <asp:TextBox ID="NewPassword2" runat="server" TextMode="Password" Width="200px"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    <br />
    <br />
    <asp:TextBox ID="MessageBox" runat="server" Width="864px" BorderStyle="None" Font-Size="X-Large" ForeColor="#FF0066" Height="22px" ReadOnly="True"></asp:TextBox>
    <br />
    <br />
    <asp:Button ID="Button1" runat="server" Text="Vista breytingu á lykilorði" Font-Size="Large" OnClick="Button1_Click" />
</asp:Content>
