
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrintEventResults.aspx.cs" Inherits="MotFRI.PrintEventReults" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:TextBox ID="HeatNoFilter" Visible="false" runat="server" BorderStyle="None"></asp:TextBox><br />
   
    <asp:textbox ID="CompetitionName" runat="server" Height="55px" Width="1000px" Font-Bold="True" Font-Size="X-Large" BorderStyle="None" style="text-align: center" ReadOnly="true"></asp:textbox>
    </div>
        <asp:TextBox ID="CompetitionVenue" runat="server" Width="1000px" BorderStyle="None" style="text-align: center"></asp:TextBox>
        <br />
        <asp:TextBox ID="CompetitionDates" runat="server" Width="1000px" BorderStyle="None" style="text-align: center"></asp:TextBox>
        <br />
        <br />
        <asp:TextBox ID="CompetitionEventName" runat="server" Font-Size="Larger" Font-Bold="True" Font-Italic="True" Width="1000px" BorderStyle="None" ReadOnly="True"></asp:TextBox>
        <br />
        <br />
        <asp:PlaceHolder ID="PH" runat="server"></asp:PlaceHolder>
        <br />
        <asp:TextBox ID="DateAndTime" runat="server"  Width="250px" BorderStyle="None" ReadOnly="True" style="text-align: left"></asp:TextBox>

    </form>
</body>
</html>
