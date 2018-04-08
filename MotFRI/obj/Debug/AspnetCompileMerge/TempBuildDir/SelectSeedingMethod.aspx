<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelectSeedingMethod.aspx.cs" Inherits="MotFRI.SelectSeedingMethod" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div >
        <br />
        <asp:TextBox ID="EventName" runat="server" BorderStyle="None" Font-Names="Arial" Font-Bold="True" Font-Italic="True" Font-Size="X-Large" Height="41px" Width="1142px"></asp:TextBox>
        <br /><br />

        <asp:RadioButtonList ID="SelectSeedingTypeTrack" runat="server" Font-Names="Arial">            
            <asp:ListItem Value="PERFORMANCEBASED">Skv. &#225;rangri</asp:ListItem>
            <asp:ListItem Value="RANDOM">Happa og glappa</asp:ListItem>
            <asp:ListItem Value="MANUAL">Handvirk r&#246;&#240;un</asp:ListItem>
            <asp:ListItem Value="BESTHEATLAST">Besti ri&#240;ill s&#237;&#240;astur</asp:ListItem>
            <asp:ListItem Value="200MAND400MINDOORS">200m og 400m - besti ri&#240;ill s&#237;&#240;astur</asp:ListItem>
            <asp:ListItem Value="ALPHABETICAL">Stafr&#243;fsr&#246;&#240;</asp:ListItem>
            <asp:ListItem Value="CANCEL">Hætta við</asp:ListItem>
        </asp:RadioButtonList>
        <asp:RadioButtonList ID="SelectSeedingTypeField" runat="server" Font-Names="Arial">
            <asp:ListItem Value="RANDOM">Happa og glappa</asp:ListItem>
            <asp:ListItem Value="MANUAL">Handvirk r&#246;&#240;un</asp:ListItem>
            <asp:ListItem Value="ALPHABETICAL">Stafr&#243;fsr&#246;&#240;</asp:ListItem>
            <asp:ListItem Value="CANCEL">Hætta við</asp:ListItem>
        </asp:RadioButtonList>
        <br />        <br />
        <asp:Button ID="SelSeeding" runat="server" Text="Velja" OnClick="SelSeeding_Click" />
    <br /><br /><br />
        <asp:Button ID="SelBest8" runat="server" Text="Finna 8 bestu fyrir úrslit" OnClick="SelBest8_Click" />
</div>
    </form>
</body>
</html>
