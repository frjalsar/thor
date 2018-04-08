<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CompetitionSetup.aspx.cs" Inherits="MotFRI.CompetitionSetup" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Uppsetning móts</title>
    </head>
<body>
    <form id="form1" runat="server">
    <div>
<ContentTemplate>
<p class="style1">
<p style="font-size: x-large; font-family:Arial; font-weight: 700;">Uppsetning móts</p>
<asp:TextBox ID="GamliVollur" runat="server" ReadOnly="True" Visible="False"></asp:TextBox>
<asp:TextBox ID="GamlaDags" runat="server" ReadOnly="True" Visible="False"></asp:TextBox>
 <Table style="font-size: x-large; font-family:Arial">
                <tr><td>&nbsp;</td><td style="font-size: large">Númer</td>
                    <td>&nbsp;</td><td><asp:TextBox ID="CompetitionCode" runat="server" Enabled="False" 
                                ReadOnly="True" Width="258px"></asp:TextBox></td>
                    <tr><td>&nbsp;&nbsp;</td><td style="font-size: large">Heiti</td><td>*</td><td><asp:TextBox ID="HeitiMots" runat="server" 
                                    onkeypress="javascript: return limitCharsLength(this,50);" Width="258px"></asp:TextBox></td>
                        <tr><td>&nbsp;&nbsp;</td><td style="font-size: large">Enskt heiti</td><td>&nbsp;</td><td><asp:TextBox ID="EnsktHeiti" runat="server" 
                                        onkeypress="javascript: return limitCharsLength(this,50);" Width="258px"></asp:TextBox></td></tr>
                        <tr><td>&nbsp;&nbsp;</td><td style="font-size: large">
                            Dagsetning (DD.MM.ÁÁÁÁ)&nbsp;&nbsp;&nbsp;&nbsp;</td><td>*</td><td><asp:TextBox ID="Dagsetning" runat="server" AutoPostBack="True" Width="150px"></asp:TextBox>
                            <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
                            </asp:ToolkitScriptManager>
                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" 
                                        DaysModeTitleFormat="dd.MM.yyyy" Enabled="True" Format="dd.MM.yyyy" 
                                        TargetControlID="Dagsetning" TodaysDateFormat="dd.MM.yyyy"></asp:CalendarExtender></td>
           <td style="font-size: large">Dagur 2:&nbsp;<asp:TextBox ID="CompetitionDate2" runat="server"></asp:TextBox>&nbsp;
                            <asp:CalendarExtender ID="CalendarExtender2" runat="server" 
                                        DaysModeTitleFormat="dd.MM.yyyy" Enabled="True" Format="dd.MM.yyyy" 
                                        TargetControlID="CompetitionDate2" TodaysDateFormat="dd.MM.yyyy"></asp:CalendarExtender></td>
       <td style="font-size: large">Dagur 3:&nbsp;
           <asp:TextBox ID="CompetitionDate3" runat="server"></asp:TextBox>&nbsp&nbsp
                                           <asp:CalendarExtender ID="CalendarExtender3" runat="server" 
                                        DaysModeTitleFormat="dd.MM.yyyy" Enabled="True" Format="dd.MM.yyyy" 
                                        TargetControlID="CompetitionDate3" TodaysDateFormat="dd.MM.yyyy"></asp:CalendarExtender></td>
           <td style="font-size: large">Dagur 4:&nbsp;<asp:TextBox ID="CompetitionDate4" runat="server"></asp:TextBox>&nbsp&nbsp
                                           <asp:CalendarExtender ID="CalendarExtender4" runat="server" 
                                        DaysModeTitleFormat="dd.MM.yyyy" Enabled="True" Format="dd.MM.yyyy" 
                                        TargetControlID="CompetitionDate4" TodaysDateFormat="dd.MM.yyyy"></asp:CalendarExtender></td>
       </tr>                                 
                            <tr><td>&nbsp;&nbsp;</td><td style="font-size: large">
                            Keppnisvöllur</td><td>*</td><td><asp:DropDownList ID="Vellir" runat="server" AppendDataBoundItems="True" 
                                            AutoPostBack="True" DataSourceID="SqlDataSource1" DataTextField="Heiti" 
                                            DataValueField="Heiti" Width="250px"><asp:ListItem Selected="True" Value="-1">---Veldu keppnisvöll---</asp:ListItem></asp:DropDownList><asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                                            ConnectionString="<%$ ConnectionStrings:AthleticsConnectionString %>" 
                                            SelectCommand="SELECT [Heiti] FROM [Athl$Venues]"></asp:SqlDataSource></td><td style="font-size: large">Staður</td><td><asp:TextBox ID="Stadur" runat="server" Enabled="False" ReadOnly="True" 
                                            Width="200px"></asp:TextBox></td></tr>
                            <tr><td>&nbsp;</td><td>&nbsp;&nbsp;</td><td></td><td></td>
                                                <td style="font-size: large">Úti/Inni</td><td><asp:TextBox ID="UtiEdaInni" runat="server" Enabled="False" ReadOnly="True" 
                                            Width="200px"></asp:TextBox></td></tr>
                            <tr><td>&nbsp;&nbsp;</td><td style="font-size: large">Mótshaldari </td><td>*</td><td><asp:TextBox ID="Motshaldari" runat="server" 
                                            onkeypress="javascript: return limitCharsLength(this,10);" Width="200px"></asp:TextBox></td></tr>
                            <tr><td>&nbsp;&nbsp;</td><td style="font-size: large">Mótsstjóri</td><td>*</td>
                                                <td><asp:TextBox ID="Motsstjori" runat="server" 
                                            onkeypress="javascript: return limitCharsLength(this,30);" Width="200px"></asp:TextBox></td></tr>
                            <tr><td>&nbsp;&nbsp;</td><td style="font-size: large">Yfirdómari</td><td>*</td>
                                                <td><asp:TextBox ID="Yfirdomari" runat="server" 
                                            onkeypress="javascript: return limitCharsLength(this,30);" Width="200px"></asp:TextBox></td></tr>
                            <tr><td>&nbsp;&nbsp;</td><td style="font-size: large">Skrán.gj. pr. grein</td><td>&nbsp;</td>
                                <td><asp:TextBox ID="SkranGjaldPrGrein" runat="server" Text="0"
                                            onkeypress="javascript : return isNumberKey(this)" Width="200px" HorizontalAlign="right" ></asp:TextBox></td></tr>
                            <tr><td>&nbsp;&nbsp;</td>
                                                <td style="font-size: large">Skrán.gj. pr. boðhl. </td>
                                                <td>&nbsp;</td>
                                                <td><asp:TextBox ID="SkranGjPrBodhl" runat="server" Text="0"
                                            onkeypress="javascript : return isNumberKey(this)" Width="200px" HorizontalAlign="right"></asp:TextBox></td></tr>
                            <tr><<td>&nbsp;&nbsp;</td><td style="font-size: large">Skrán.gj. pr. grein 17 ára og yngri</td><td>
                                &nbsp;</td>
                                <td><asp:TextBox ID="SkranGjPrGreinU18" runat="server" Text="0"
                                            onkeypress="javascript : return isNumberKey(this)" Width="200px" HorizontalAlign="right"></asp:TextBox></td></tr>
                            <tr><td>&nbsp;&nbsp;</td>
                                                <td style="font-size: large">Skrán.gj. pr. boðhl. 17 ára og yngri</td>
                                                <td>&nbsp;</td>
                                                <td><asp:TextBox ID="SkranGjPrBodhlU18" runat="server" Text="0"
                                            onkeypress="javascript : return isNumberKey(this)" Width="200px" HorizontalAlign="right"></asp:TextBox></td></tr>

                            <tr><td>&nbsp;&nbsp;</td><td style="font-size: large">Reikna ungl.stig </td><td>&nbsp;</td><td><asp:DropDownList ID="ReiknaUnglStig" runat="server" Width="100px">
                                  <asp:ListItem Value="0">Nei</asp:ListItem><asp:ListItem Value="1">Já</asp:ListItem>
                                </asp:DropDownList></td></tr>
                            <tr><td>&nbsp;&nbsp;</td><td style="font-size: large">Reikna IAAF stig </td><td>&nbsp;</td><td><asp:DropDownList ID="ReiknaIAAFStig" runat="server" Width="100px">
                                  <asp:ListItem Value="0">Nei</asp:ListItem><asp:ListItem Value="1">Já</asp:ListItem>
                                </asp:DropDownList></td></tr>
                            <tr><td>&nbsp;&nbsp;</td>
                                <td style="font-size: large">Teg. stigakeppni </td><td>&nbsp;</td><td>
                                    <asp:DropDownList ID="TegundStigakeppni" runat="server" Width="200px">
                                        <asp:ListItem Value="0">Engin</asp:ListItem>
                                        <asp:ListItem Value="1">Aldursflokkar FRÍ</asp:ListItem>
                                        <asp:ListItem Value="2">Bikarkeppni</asp:ListItem>
                                        <asp:ListItem Value="3">Hver flokkur fyrir sig (MÍ yngri flokkar)</asp:ListItem>
                                        <asp:ListItem Value="4">Fjölþrautakeppni</asp:ListItem>
                                        <asp:ListItem Value="5">MÍ fullorðinna (IAAF stig)</asp:ListItem>
                                        <asp:ListItem Value="6">Fjölþrautakeppni með ungl.stigum</asp:ListItem>
                                        <asp:ListItem Value="7">Fjölþraut með sætisstigum</asp:ListItem>
                                </asp:DropDownList></td></tr></tr></tr></tr>
                    <tr><td>&nbsp;<td style="font-size: large">Staða móts:</td><td></td><td><asp:DropDownList ID="CompStatus" runat="server">
                        <asp:ListItem Value="0">Ósamþykkt af FRÍ</asp:ListItem>
                        <asp:ListItem Value="1">Samþykkt af FRÍ</asp:ListItem>
                        <asp:ListItem Value="2">Opið fyrir skráningu</asp:ListItem>
                        <asp:ListItem Value="3">Lokað fyrir skráningu</asp:ListItem>
                        <asp:ListItem Value="4">Keppni stendur yfir</asp:ListItem>
                        <asp:ListItem Value="5">Keppni lokið</asp:ListItem>
                        </asp:DropDownList></td></td>
                </Table>
                
                    <br /><asp:TextBox ID="Message" runat="server" Font-Size="Larger" ForeColor="Red" 
        Enabled="False" BorderStyle="None" Width="100%"></asp:TextBox>
            <asp:Button ID="SetupEvents" runat="server" Text="Bæta við nýjum keppnisgreinum" 
            onclick="SetupEvents_Click" style="font-size: large" Width="427px" />
            <br />
    <asp:Button ID="CopyEventsFromPrevComp" runat="server" Text="Afrita keppnisgreinar frá gömlu móti" OnClick="CopyEventsFromPrevComp_Click" style="font-size: large" Width="463px"></asp:Button>
    <br />
    <asp:Button ID="UpdateEventSetup" runat="server" Text="Breyta tímaseðli" OnClick="UpdateEventSetup_Click" style="font-size: large" Width="234px"></asp:Button>
    <br />
            <asp:Button ID="Vista" runat="server" onclick="VistaNyttMot_Click" 
            Text="Vista" style="font-size: large; font-weight: 700" />
                        </p>
       <script language="javascript">
           function limitCharsLength(Object, MaxLen) {
               if (Object.value.length > MaxLen - 1) {
                   Object.value = Object.value.substring(0, MaxLen);
               }
           }
           function isNumberKey(evt) {
               var charCode = (evt.which) ? evt.which : event.keyCode
               //    if ((charCode < 48 || charCode > 57) && (charCode != 44) && (charCode != 46)) // 0..9, , .
               if ((charCode < 48 || charCode > 57) && (charCode != 46))    // 0..9 .
                   return false;

               return true;
           }
</script>
        </ContentTemplate> 

    
        </div>
    </form>
</body>
 
</html>
