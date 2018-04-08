<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CompetitionClubOrCompetitorInformation.aspx.cs" Inherits="MotFRI.CompetitionClubOrCompetitorInformation" %>
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
    <asp:TextBox ID="CompetitionName" runat="server" BorderStyle="None" Height="40px" style="font-size: xx-large" Width="1264px" ReadOnly="true"></asp:TextBox><br /><br />
    <asp:TextBox ID="PageInfoText" runat="server" Height="42px" style="font-size: x-large" Width="1321px" ReadOnly="true" BorderStyle="None"></asp:TextBox><br /><br />
    <asp:TextBox ID="CompCode" runat="server" Visible="false" ReadOnly="true"></asp:TextBox>
    <asp:TextBox ID="ClubCode" runat="server" Visible="false" ReadOnly="true"></asp:TextBox>
    <asp:TextBox ID="SelectedBibNo" runat="server" Visible="false" ReadOnly="true"></asp:TextBox>
<asp:GridView ID="InfoGrid" runat="server" CellPadding="4" DataSourceID="ClubOrCompetitorInfo" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False">
    <AlternatingRowStyle BackColor="White" />
    <Columns>
        <asp:HyperLinkField DataNavigateUrlFields="BibNo" DataNavigateUrlFormatString="~\CompetitionClubOrCompetitorInformation.aspx?BibNo={0}" DataTextField="BibNo" HeaderText="Rásnr" Text="B" />
        <asp:HyperLinkField DataNavigateUrlFields="CompetitorNo" DataNavigateUrlFormatString="~\CompetitorsAchievements.aspx?CompetitorCode={0}" DataTextField="Name" HeaderText="Nafn" Text="C" />

        <asp:BoundField DataField="BibNo" HeaderText="Rásnr" SortExpression="BibNo" Visible="False" />
        <asp:BoundField DataField="CompetitorNo" HeaderText="CompetitorNo" SortExpression="CompetitorNo" Visible="False" />
        <asp:BoundField DataField="Name" HeaderText="Nafn" SortExpression="Name" Visible="False" />
        <asp:HyperLinkField DataNavigateUrlFields="Club" DataNavigateUrlFormatString="~\CompetitionClubOrCompetitorInformation.aspx?Club={0}" DataTextField="Club" HeaderText="Félag" Text="A" />
        <asp:BoundField DataField="YearOfBirth" HeaderText="F.ár" SortExpression="YearOfBirth" />
        <asp:BoundField DataField="Age" HeaderText="Aldur" SortExpression="Age" />
        <asp:BoundField DataField="Club" HeaderText="Félag" SortExpression="Club" Visible="False" />
        <asp:BoundField DataField="EventName" HeaderText="Grein" SortExpression="EventName" >
        <HeaderStyle Font-Size="Small" />
        <ItemStyle Font-Size="Small" />
        </asp:BoundField>
        <asp:BoundField DataField="PersonalBest" HeaderText="Pb" SortExpression="PersonalBest" />
        <asp:BoundField DataField="SeasonBest" HeaderText="Sb" SortExpression="SeasonBest" />
        <asp:BoundField DataField="ResultOrder" HeaderText="Röð" SortExpression="ResultOrder" />
        <asp:BoundField DataField="ResultPerformance" HeaderText="Árangur" SortExpression="ResultPerformance" />
        <asp:BoundField DataField="WindReading" HeaderText="Vindur" SortExpression="WindReading" />
        <asp:BoundField DataField="Remarks" HeaderText="Ath1" SortExpression="Remarks" />
        <asp:BoundField DataField="PerformanceRemarks" HeaderText="Ath2" SortExpression="PerformanceRemarks" />
        <asp:BoundField DataField="EventDate" HeaderText="Dags" SortExpression="EventDate" DataFormatString="{0:d.MM.yyyy}" >
        <HeaderStyle HorizontalAlign="Right" />
        <ItemStyle HorizontalAlign="Right" />
        </asp:BoundField>
        <asp:BoundField DataField="EventTime" HeaderText="Tími" SortExpression="EventTime" />

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
    <asp:SqlDataSource ID="ClubOrCompetitorInfo" runat="server" ConnectionString="<%$ ConnectionStrings:AthleticsConnectionString %>" SelectCommand="CompetitionClubOrCompetitorInfo" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:ControlParameter ControlID="CompCode" Name="CompCode" PropertyName="Text" Type="String" />
            <asp:ControlParameter ControlID="ClubCode" Name="ClubCode" PropertyName="Text" Type="String" />
            <asp:ControlParameter ControlID="SelectedBibNo" Name="BibNo" PropertyName="Text" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>

</asp:Content>
