<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RecordsPersonalBestSeasonBests.aspx.cs" Inherits="MotFRI.RecordsPersonalBestSeasonBests" %>
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
    <asp:TextBox ID="CompetitionName" runat="server" style="margin-bottom: 54px; font-size: xx-large; font-weight: 700;" Width="1404px" Height="49px" BorderStyle="None" ReadOnly="true"></asp:TextBox>
    
    <asp:TextBox ID="PlaceAndDate" runat="server" Height="35px" style="font-size: x-large; font-weight: 700" Width="1378px" ReadOnly="true" BorderStyle="None"></asp:TextBox><br /><br />
    <asp:TextBox ID="CompCode" runat="server" Visible="false"></asp:TextBox>
    <br />
    <asp:Label ID="Label1" runat="server" Text="Persónulegar bætingar keppenda á mótinu" style="font-size: x-large"></asp:Label>
    <br /><br />
    <asp:TextBox ID="InfoLine" runat="server" BorderStyle="None" style="font-size: large" Width="1373px"></asp:TextBox>
    <br /><br />
    <asp:GridView ID="RecordsPBsAndSBs" runat="server" AutoGenerateColumns="False" CellPadding="4" DataSourceID="RecordsPBandSBDataSource" ForeColor="#333333" GridLines="None" OnRowDataBound="RecordsPBsAndSBs_RowDataBound">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:BoundField DataField="LineType" ReadOnly="True" SortExpression="LineType" />
            <asp:BoundField DataField="Name" HeaderText="Nafn" SortExpression="Name" />
            <asp:BoundField DataField="Club" HeaderText="Félag" SortExpression="Club" />
            <asp:BoundField DataField="YearOfBirth" HeaderText="F.ár" SortExpression="YearOfBirth" />
            <asp:BoundField DataField="Remarks" HeaderText="Athugasemd" SortExpression="Remarks">
            <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="Performance" HeaderText="Árangur" SortExpression="Performance" />
            <asp:BoundField DataField="EventName" HeaderText="Heiti greinar" ReadOnly="True" SortExpression="EventName" />
            <asp:BoundField DataField="NoOfRecords" HeaderText="NoOfRecords" ReadOnly="True" SortExpression="NoOfRecords" />
            <asp:BoundField DataField="NoOfPbsAndSbs" HeaderText="NoOfPbsAndSbs" ReadOnly="True" SortExpression="NoOfPbsAndSbs" />  

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
    <asp:SqlDataSource ID="RecordsPBandSBDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:AthleticsConnectionString %>" SelectCommand="RecordsPersBestsAndSeasonBests" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:ControlParameter ControlID="CompCode" Name="CompCode" PropertyName="Text" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>
