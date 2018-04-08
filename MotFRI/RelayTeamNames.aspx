<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RelayTeamNames.aspx.cs" Inherits="MotFRI.RelayTeamNames" %>
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
    <br />
    <br />
    <asp:TextBox ID="CompName" runat="server" Height="34px" Width="1079px" BorderStyle="None" style="font-size: x-large; font-weight: 700" ReadOnly="true"></asp:TextBox>
    <br /><br />
    <asp:TextBox ID="EventName" runat="server" ReadOnly="true" BorderStyle="None" Height="29px" style="font-weight: 700; font-size: medium" Width="995px" ></asp:TextBox>
    <br /><br />
    <asp:Label ID="Label1" runat="server" Text="Félag: "></asp:Label>&nbsp;&nbsp;<asp:TextBox ID="Club" runat="server" Width="173px" ReadOnly="true" style="font-size: medium; font-weight: 700;"  BorderStyle="None"></asp:TextBox>
    &nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="RelayTeamName" runat="server" ReadOnly="true" style="font-size: medium; font-weight: 700;" Width="775px" BorderStyle="None"></asp:TextBox>
    <br /><br />
    <asp:TextBox ID="CompCode" runat="server" ReadOnly="true" Visible="false"></asp:TextBox>
    <asp:TextBox ID="EventLineNo" runat="server" ReadOnly="true" Visible="false"></asp:TextBox>
    <asp:TextBox ID="CompetitorBibNo" runat="server" ReadOnly="true" Visible="false"></asp:TextBox>
    <br />
    <br />
    <br />
    
    <asp:Label ID="Label6" runat="server" Text=" " Width="100px"></asp:Label>&nbsp;&nbsp;<asp:TextBox ID="BibNoHdr" runat="server" Text="Rásnr." ReadOnly="true" BorderStyle="None" style="font-size: large; font-weight: 700" Width="60px"></asp:TextBox>&nbsp;&nbsp;<asp:TextBox ID="TextBox2" runat="server" Width="394px" ReadOnly="true" BorderStyle="None" Text="Nafn" style="font-size: large; font-weight: 700"></asp:TextBox><br />
    <asp:Label ID="Label2" runat="server" Text="1. sprettur" Width="100px"></asp:Label>&nbsp;&nbsp;<asp:TextBox ID="BibNo1" runat="server" ReadOnly="true" BorderStyle="None" style="font-size: large; font-weight: 700" Width="60px"></asp:TextBox>&nbsp;&nbsp;<asp:TextBox ID="Runner1" runat="server" Width="394px" ReadOnly="true" BorderStyle="None" style="font-size: large; font-weight: 700"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;<br />
    <asp:Label ID="Label3" runat="server" Text="2. sprettur" Width="100px"></asp:Label>&nbsp;&nbsp;<asp:TextBox ID="BibNo2" runat="server" ReadOnly="true" BorderStyle="None" style="font-size: large; font-weight: 700" Width="60px"></asp:TextBox>&nbsp;&nbsp;<asp:TextBox ID="Runner2" runat="server" Width="394px" ReadOnly="true" BorderStyle="None" style="font-weight: 700; font-size: large"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;<br />
    <asp:Label ID="Label4" runat="server" Text="3. sprettur" Width="100px"></asp:Label>&nbsp;&nbsp;<asp:TextBox ID="BibNo3" runat="server" ReadOnly="true" BorderStyle="None" style="font-size: large; font-weight: 700" Width="60px"></asp:TextBox>&nbsp;&nbsp;<asp:TextBox ID="Runner3" runat="server" Width="394px" ReadOnly="true" BorderStyle="None" style="font-weight: 700; font-size: large"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;<br />
    <asp:Label ID="Label5" runat="server" Text="4. sprettur" Width="100px"></asp:Label>&nbsp;&nbsp;<asp:TextBox ID="BibNo4" runat="server" ReadOnly="true" BorderStyle="None" style="font-size: large; font-weight: 700" Width="60px"></asp:TextBox>&nbsp;&nbsp;<asp:TextBox ID="Runner4" runat="server" Width="394px" ReadOnly="true" BorderStyle="None" style="font-weight: 700; font-size: large"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br />
<br />
    <asp:TextBox ID="ErrorMsg" runat="server" ReadOnly="true" Height="40px" style="font-weight: 700; font-size: x-large" Width="1190px" BorderStyle="None" ForeColor="Red"></asp:TextBox>
    <br />
    <br />


<asp:GridView ID="ClubRunnersGridView" runat="server" AutoGenerateColumns="False" CellPadding="4" DataSourceID="AvailableRunnersDS" ForeColor="#333333" GridLines="None" OnRowDataBound="ClubRunnersGridView_RowDataBound">
    <AlternatingRowStyle BackColor="White" />
    <Columns>
        <asp:TemplateField HeaderText="Sprettur Nr.">
               <ItemTemplate>
                    <asp:DropDownList ID="SelectLeg" runat="server">
                        <asp:ListItem Value="0">-</asp:ListItem>
                        <asp:ListItem Value="1">1.</asp:ListItem>
                        <asp:ListItem Value="2">2.</asp:ListItem>
                        <asp:ListItem Value="3">3.</asp:ListItem>
                        <asp:ListItem Value="4">4.</asp:ListItem>
                    </asp:DropDownList>            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="rasnumer" HeaderText="Rásnúmer" SortExpression="rasnumer" />
        <asp:BoundField DataField="nafn" HeaderText="Nafn" SortExpression="nafn" />
        <asp:BoundField DataField="felag" HeaderText="Félag" SortExpression="felag" />
        <asp:BoundField DataField="faedingarar" HeaderText="F.ár" SortExpression="faedingarar" />
        <asp:BoundField DataField="aldurkeppanda" HeaderText="Aldur" SortExpression="aldurkeppanda" />
        <asp:BoundField DataField="spretturnr" HeaderText="SpretturNr" SortExpression="spretturnr" />
    </Columns>
    <EditRowStyle BackColor="#2461BF" />
    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
    <RowStyle BackColor="#EFF3FB" />
    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
    <SortedAscendingCellStyle BackColor="#F5F7FB" />
    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
    <SortedDescendingCellStyle BackColor="#E9EBEF" />
    <SortedDescendingHeaderStyle BackColor="#4870BE" />
    </asp:GridView>

    <asp:SqlDataSource ID="AvailableRunnersDS" runat="server" ConnectionString="<%$ ConnectionStrings:AthleticsConnectionString %>" SelectCommand="GetAvailableRelayRunners" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:ControlParameter ControlID="CompCode" Name="CompCode" PropertyName="Text" Type="String" />
            <asp:ControlParameter ControlID="Club" Name="Club" PropertyName="Text" Type="String" />
            <asp:ControlParameter ControlID="EventLineNo" Name="EventLineNo" PropertyName="Text" Type="Int32" />
            <asp:ControlParameter ControlID="CompetitorBibNo" Name="CurrentBibNo" PropertyName="Text" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <br />
    <br />
    <asp:Button ID="Vista"  runat="server" Text="Vista" OnClick="Vista_Click" style="font-size: x-large; font-weight: 700;" />
&nbsp;&nbsp;&nbsp;
    <asp:Button ID="Button1" runat="server" Text="Til baka í Boðhlaup" style="font-size: large" OnClick="Button1_Click" />
</asp:Content>
