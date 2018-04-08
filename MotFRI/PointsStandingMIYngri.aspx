<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PointsStandingMIYngri.aspx.cs" Inherits="MotFRI.PointsStandingMIYngri" %>
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
    <asp:TextBox ID="CompCode" runat="server" Visible="false"></asp:TextBox>

    <asp:TextBox ID="CompetitionName" runat="server" BorderStyle="None" Font-Size="XX-Large" Width="970px" ReadOnly="true"></asp:TextBox>
    <br />
    <br />
    <asp:Label ID="SelectDayLabel" runat="server" Text="Veldu dag: "></asp:Label>&nbsp;<asp:DropDownList ID="SelectDay" runat="server" Height="23px" Width="141px" AutoPostBack="True"></asp:DropDownList>
    &nbsp;
    <asp:Label ID="AgeFromLabel" runat="server" Text="Aldur frá: "></asp:Label>&nbsp;
    <asp:DropDownList ID="SelAgeFrom" runat="server" AutoPostBack="True"></asp:DropDownList>
    &nbsp;<asp:Label ID="AgeToLabel" runat="server" Text="Aldur til: "></asp:Label>&nbsp;
    <asp:DropDownList ID="SelAgeTo" runat="server" AutoPostBack="True"></asp:DropDownList>
    <br /><br />
    <asp:GridView ID="PointsStanding" runat="server" AutoGenerateColumns="False" CellPadding="4" DataSourceID="PointsMIYngriWithFilters" ForeColor="#333333" GridLines="None" OnRowDataBound="PointsStanding_RowDataBound">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
            <asp:BoundField DataField="RecordType" HeaderText="RecordType" SortExpression="RecordType" ItemStyle-HorizontalAlign="Left" >
<ItemStyle HorizontalAlign="Left"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="AgeGroupDescription" HeaderText="AgeGroupDescription" SortExpression="AgeGroupDescription" />
            <asp:BoundField DataField="HeitiFelags" HeaderText="HeitiFelags" SortExpression="HeitiFelags" />
            <asp:BoundField DataField="Utskyring" HeaderText="Utskyring" SortExpression="Utskyring" />
            <asp:BoundField DataField="TotalPoints" HeaderText="Samtals stig" DataFormatString="{0:F1}" SortExpression="TotalPoints" >
            <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="PointsDay1" HeaderText="PointsDay1" DataFormatString="{0:F1}" SortExpression="PointsDay1" >
            <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="PointsDay2" HeaderText="PointsDay2" DataFormatString="{0:F1}" SortExpression="PointsDay2" >
            <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="PointsDay3" HeaderText="PointsDay3" DataFormatString="{0:F1}" SortExpression="PointsDay3" >
  
            <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
  
        </Columns>
        <EditRowStyle BackColor="#999999" />
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#E9E7E2" />
        <SortedAscendingHeaderStyle BackColor="#506C8C" />
        <SortedDescendingCellStyle BackColor="#FFFDF8" />
        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
    </asp:GridView>
    <asp:SqlDataSource ID="PointsMIYngriWithFilters" runat="server" ConnectionString="<%$ ConnectionStrings:AthleticsConnectionString %>" SelectCommand="PointsStandingMIYngriWithParam" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:ControlParameter ControlID="CompCode" Name="CompetitionCode" PropertyName="Text" Type="String" />
            <asp:ControlParameter ControlID="SelectDay" Name="DayFilter" PropertyName="SelectedValue" Type="String" />
            <asp:ControlParameter ControlID="SelAgeFrom" Name="AgeFrom" PropertyName="SelectedValue" Type="String" />
            <asp:ControlParameter ControlID="SelAgeTo" Name="AgeTo" PropertyName="SelectedValue" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>
