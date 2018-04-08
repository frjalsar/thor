<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HeightsInHJandPV.aspx.cs" Inherits="MotFRI.HeightsInHJandPV" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="font-family: Arial">
            <asp:TextBox runat="server" ID="EventName" BorderStyle="None" Font-Bold="True" Font-Size="X-Large" Width="987px"></asp:TextBox>
            <br /> <br />

            <asp:Table ID="Table1" runat="server">
                <asp:TableRow> 
                    <asp:TableCell>Byrjunarhæð í sentimetrum</asp:TableCell> 
                    <asp:TableCell><asp:TextBox ID="OpeningHeight" runat="server" Width="100px"></asp:TextBox> </asp:TableCell>
                 </asp:TableRow>
                 <asp:TableRow> 
                     <asp:TableCell>Hækkun um</asp:TableCell>
                     <asp:TableCell><asp:TextBox ID="FirstIncreaseBy" runat="server" Width="100px"></asp:TextBox></asp:TableCell>
                     <asp:TableCell> sentimetra</asp:TableCell>
                 </asp:TableRow>
                 <asp:TableRow> 
                     <asp:TableCell>Upp í</asp:TableCell>
                     <asp:TableCell><asp:TextBox ID="FirstLimit" runat="server" Width="100px"></asp:TextBox></asp:TableCell>
                 </asp:TableRow>
                 <asp:TableRow> 
                     <asp:TableCell>Síðan um</asp:TableCell>
                     <asp:TableCell><asp:TextBox ID="SecondIncreaseBy" runat="server" Width="100px"></asp:TextBox></asp:TableCell>
                     <asp:TableCell> sentimetra</asp:TableCell>
                 </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>Upp í</asp:TableCell>
                    <asp:TableCell>
                        <asp:TextBox ID="SecondLimit" runat="server" Width="100px"></asp:TextBox></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>Að lokum um</asp:TableCell>
                    <asp:TableCell>
                        <asp:TextBox ID="ThirdIncreaseBy" runat="server" Width="100px"></asp:TextBox></asp:TableCell>
                        <asp:TableCell> sentimetra</asp:TableCell>
                 </asp:TableRow>
            </asp:Table>
            <br />
            <asp:Button ID="CalculateButton" runat="server" Text="Reikna" OnClick="CalculateButton_Click" />
            <br />
            <br />
            <br />
            HÆÐIR:
            <br />            
            <asp:Label ID="Label1" runat="server" Text="1" Width="45px" BorderStyle="None" style="text-align: center;"></asp:Label>
            <asp:Label ID="Label2" runat="server" Text="2" Width="45px" BorderStyle="None" style="text-align: center;"></asp:Label>
            <asp:Label ID="Label3" runat="server" Text="3" Width="45px" BorderStyle="None" style="text-align: center;"></asp:Label>
            <asp:Label ID="Label4" runat="server" Text="4" Width="45px" BorderStyle="None" style="text-align: center;"></asp:Label>
            <asp:Label ID="Label5" runat="server" Text="5" Width="45px" BorderStyle="None" style="text-align: center;"></asp:Label>
            <asp:Label ID="Label6" runat="server" Text="6" Width="45px" BorderStyle="None" style="text-align: center;"></asp:Label>
            <asp:Label ID="Label7" runat="server" Text="7" Width="45px" BorderStyle="None" style="text-align: center;"></asp:Label>
            <asp:Label ID="Label8" runat="server" Text="8" Width="45px" BorderStyle="None" style="text-align: center;"></asp:Label>
            <asp:Label ID="Label9" runat="server" Text="9" Width="45px" BorderStyle="None" style="text-align: center;"></asp:Label>
            <asp:Label ID="Label10" runat="server" Text="10" Width="45px" BorderStyle="None" style="text-align: center;"></asp:Label>
            <asp:Label ID="Label11" runat="server" Text="11" Width="45px" BorderStyle="None" style="text-align: center;"></asp:Label>
            <asp:Label ID="Label12" runat="server" Text="12" Width="45px" BorderStyle="None" style="text-align: center;"></asp:Label>
            <asp:Label ID="Label13" runat="server" Text="13" Width="45px" BorderStyle="None" style="text-align: center;"></asp:Label>
            <asp:Label ID="Label14" runat="server" Text="14" Width="45px" BorderStyle="None" style="text-align: center;"></asp:Label>
            <asp:Label ID="Label15" runat="server" Text="15" Width="45px" BorderStyle="None" style="text-align: center;"></asp:Label>
            <asp:Label ID="Label16" runat="server" Text="16" Width="45px" BorderStyle="None" style="text-align: center;"></asp:Label>
            <asp:Label ID="Label17" runat="server" Text="17" Width="45px" BorderStyle="None" style="text-align: center;"></asp:Label>
            <asp:Label ID="Label18" runat="server" Text="18" Width="45px" BorderStyle="None" style="text-align: center;"></asp:Label>
            <asp:Label ID="Label19" runat="server" Text="19" Width="45px" BorderStyle="None" style="text-align: center;"></asp:Label>
            <asp:Label ID="Label20" runat="server" Text="20" Width="45px" BorderStyle="None" style="text-align: center;"></asp:Label>
            <asp:Label ID="Label21" runat="server" Text="21" Width="45px" BorderStyle="None" style="text-align: center;"></asp:Label>
            <asp:Label ID="Label22" runat="server" Text="22" Width="45px" BorderStyle="None" style="text-align: center;"></asp:Label>
            <asp:Label ID="Label23" runat="server" Text="23" Width="45px" BorderStyle="None" style="text-align: center;"></asp:Label>
            <asp:Label ID="Label24" runat="server" Text="24" Width="45px" BorderStyle="None" style="text-align: center;"></asp:Label>
            <asp:Label ID="Label25" runat="server" Text="25" Width="45px" BorderStyle="None" style="text-align: center;"></asp:Label>
            <br />
            <asp:TextBox runat="server" ID="Height01" Width="45px" BorderStyle="None" style="text-align: center;"></asp:TextBox>
            <asp:TextBox runat="server" ID="Height02" Width="45px" BorderStyle="None" style="text-align: center;"></asp:TextBox>
            <asp:TextBox runat="server" ID="Height03" Width="45px" BorderStyle="None" style="text-align: center;"></asp:TextBox>
            <asp:TextBox runat="server" ID="Height04" Width="45px" BorderStyle="None" style="text-align: center;"></asp:TextBox>
            <asp:TextBox runat="server" ID="Height05" Width="45px" BorderStyle="None" style="text-align: center;"></asp:TextBox>
            <asp:TextBox runat="server" ID="Height06" Width="45px" BorderStyle="None" style="text-align: center;"></asp:TextBox>
            <asp:TextBox runat="server" ID="Height07" Width="45px" BorderStyle="None" style="text-align: center;"></asp:TextBox>
            <asp:TextBox runat="server" ID="Height08" Width="45px" BorderStyle="None" style="text-align: center;"></asp:TextBox>
            <asp:TextBox runat="server" ID="Height09" Width="45px" BorderStyle="None" style="text-align: center;"></asp:TextBox>
            <asp:TextBox runat="server" ID="Height10" Width="45px" BorderStyle="None" style="text-align: center;"></asp:TextBox>
            <asp:TextBox runat="server" ID="Height11" Width="45px" BorderStyle="None" style="text-align: center;"></asp:TextBox>
            <asp:TextBox runat="server" ID="Height12" Width="45px" BorderStyle="None" style="text-align: center;"></asp:TextBox>
            <asp:TextBox runat="server" ID="Height13" Width="45px" BorderStyle="None" style="text-align: center;"></asp:TextBox>
            <asp:TextBox runat="server" ID="Height14" Width="45px" BorderStyle="None" style="text-align: center;"></asp:TextBox>
            <asp:TextBox runat="server" ID="Height15" Width="45px" BorderStyle="None" style="text-align: center;"></asp:TextBox>
            <asp:TextBox runat="server" ID="Height16" Width="45px" BorderStyle="None" style="text-align: center;"></asp:TextBox>
            <asp:TextBox runat="server" ID="Height17" Width="45px" BorderStyle="None" style="text-align: center;"></asp:TextBox>
            <asp:TextBox runat="server" ID="Height18" Width="45px" BorderStyle="None" style="text-align: center;"></asp:TextBox>
            <asp:TextBox runat="server" ID="Height19" Width="45px" BorderStyle="None" style="text-align: center;"></asp:TextBox>
            <asp:TextBox runat="server" ID="Height20" Width="45px" BorderStyle="None" style="text-align: center;"></asp:TextBox>
            <asp:TextBox runat="server" ID="Height21" Width="45px" BorderStyle="None" style="text-align: center;"></asp:TextBox>
            <asp:TextBox runat="server" ID="Height22" Width="45px" BorderStyle="None" style="text-align: center;"></asp:TextBox>
            <asp:TextBox runat="server" ID="Height23" Width="45px" BorderStyle="None" style="text-align: center;"></asp:TextBox>
            <asp:TextBox runat="server" ID="Height24" Width="45px" BorderStyle="None" style="text-align: center;"></asp:TextBox>
            <asp:TextBox runat="server" ID="Height25" Width="45px" BorderStyle="None" style="text-align: center;"></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="SaveHeights" runat="server" Text="Vista" OnClick="SaveHeights_Click" />
        </div>
    </form>
</body>
</html>
