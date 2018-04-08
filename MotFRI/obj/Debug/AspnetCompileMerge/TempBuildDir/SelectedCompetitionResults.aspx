<%@ Page Title="Úrslit móts" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SelectedCompetitionResults.aspx.cs" Inherits="MotFRI.SelectedCompetition" %>
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
    <p>
        <br />
        <asp:Label ID="Label1" runat="server" Text="Úrslit móts" Width="196px" style="font-size: x-large; font-style: italic; font-family: 'Segoe UI'"></asp:Label> 
        <asp:TextBox ID="CompCode" Visible="false" runat="server" Width="98px"></asp:TextBox>
        <asp:HyperLink ID="ListOfCompetitions" runat="server" NavigateUrl="~/Default.aspx">Til baka</asp:HyperLink>&nbsp;&nbsp;&nbsp;
        <asp:HyperLink ID="YourCompetitors" runat="server" NavigateUrl="~/CompetitorsForUser.aspx">Þínir keppendur</asp:HyperLink>
        <asp:Label ID="YourCompetitorsLabel" runat="server" Text="&nbsp;&nbsp;&nbsp;"></asp:Label>
        <asp:HyperLink ID="Events" runat="server" NavigateUrl="~/SelectedCompetitionEvents.aspx">Keppnisgreinar</asp:HyperLink>&nbsp;&nbsp;
        <asp:HyperLink ID="Competitors" runat="server" NavigateUrl="~/SelectedCompetitionCompetitors.aspx">Keppendur</asp:HyperLink>&nbsp;&nbsp;      
        <asp:HyperLink ID="MedalTable" runat="server" NavigateUrl="~/MedalTable.aspx">Skipting verðlauna</asp:HyperLink>&nbsp;&nbsp;
        <asp:HyperLink ID="PointsStanding" runat="server" NavigateUrl="~/Points.aspx">Stigastaðan</asp:HyperLink>&nbsp;&nbsp;
        <asp:HyperLink ID="PointsStandingMIYngri" runat="server" NavigateUrl="~/PointsStandingMIYngri.aspx">Stigastaðan</asp:HyperLink>&nbsp;&nbsp;
        <asp:HyperLink ID="TopPerfByIAAFPts" runat="server" NavigateUrl="~/TopPerfByIAAFPts.aspx">Bestu afrek skv. IAAF stigum</asp:HyperLink>
        <asp:HyperLink ID="RecordsPbsAndSbs" runat="server" NavigateUrl="~/RecordsPersonalBestSeasonBests.aspx">Bætingar keppenda</asp:HyperLink>
        <br />
        <br />
        <asp:TextBox ID="CompetitionName" runat="server" style="font-size: xx-large" Width="1210px" BorderStyle="None" ReadOnly="true"></asp:TextBox>
        <br />
        <br />
        <asp:Label ID="SelectGenderLabel" runat="server" Text="Veldu kyn:"></asp:Label>&nbsp;&nbsp;<asp:DropDownList ID="SelGender" runat="server" AutoPostBack="True">
            <asp:ListItem Value="%">Bæði</asp:ListItem>
            <asp:ListItem Value="1">Karlar</asp:ListItem>
            <asp:ListItem Value="2">Konur</asp:ListItem>
        </asp:DropDownList>&nbsp;&nbsp;
        <asp:Label ID="AgeFromLabel" runat="server" Text="Aldur frá:"></asp:Label>&nbsp;&nbsp;<asp:DropDownList ID="SelAgeFrom" runat="server" AutoPostBack="True"></asp:DropDownList>&nbsp;&nbsp;
        <asp:Label ID="AgeToLabel" runat="server" Text="Aldur til:"></asp:Label>&nbsp;&nbsp;<asp:DropDownList ID="SelAgeTo" runat="server" AutoPostBack="True"></asp:DropDownList>&nbsp;&nbsp;
        <asp:Label ID="SelectDayLabel" runat="server" Text="Veldu dag:"></asp:Label>&nbsp;&nbsp;<asp:DropDownList ID="SelectDay" runat="server" AutoPostBack="True"></asp:DropDownList>&nbsp;&nbsp;
        <asp:Label ID="SelMaxLinesLabel" runat="server" Text="Hámark fjöldi lína:"></asp:Label>&nbsp;&nbsp;<asp:TextBox ID="MaxNoOfLines" runat="server" Text="1000" Width="70px" AutoPostBack="true"></asp:TextBox>&nbsp;&nbsp;

        <br />

    </p>
              <asp:GridView ID="CompetitionResultsDataGrid" runat="server" 
                AutoGenerateColumns="False" DataSourceID="CompResults" 
                CellPadding="4" ForeColor="#333333" GridLines="None" 
                onrowdatabound="CompetitionResultsDataGrid_RowDataBound"><AlternatingRowStyle BackColor="White" />
                <Columns><asp:BoundField DataField="Place" HeaderText="Röð" 
                        SortExpression="Place" /><asp:BoundField DataField="Result" HeaderText="Árangur" SortExpression="Result" ></asp:BoundField>
                    <asp:BoundField DataField="WindText" HeaderText="Vindur" 
                        SortExpression="WindText" />
                    <asp:BoundField DataField="Points" HeaderText="Stig" SortExpression="Points" >
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Name" HeaderText="Nafn" SortExpression="Name" />
                    <asp:HyperLinkField DataNavigateUrlFields="CompetitorCode" DataNavigateUrlFormatString="~\CompetitorsAchievements.aspx?CompetitorCode={0}" DataTextField="Name" HeaderText="Afr" Text="A" />
                    <asp:BoundField DataField="YearOfBirth" HeaderText="F.ár" 
                        SortExpression="YearOfBirth" /><asp:BoundField DataField="Club" HeaderText="Félag"                             
                        SortExpression="Club"></asp:BoundField><asp:BoundField DataField="Series" HeaderText="Sería" 
                        SortExpression="Series" >
                    <ItemStyle Font-Size="Small" />
                    </asp:BoundField>

                    <asp:BoundField DataField="LineType" HeaderText="LineType" 
                        SortExpression="LineType" /><asp:BoundField DataField="EventDate" HeaderText="EventDate" 
                        SortExpression="EventDate" ReadOnly="True" /><asp:BoundField DataField="EventName" HeaderText="EventName" 
                        SortExpression="EventName" />
                    <asp:BoundField DataField="CompetitorCode" HeaderText="CompetitorCode" SortExpression="CompetitorCode" Visible="False" />
                </Columns><EditRowStyle BackColor="#2461BF" /><FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" /><HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" /><PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" /><RowStyle BackColor="#EFF3FB" /><SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" /><SortedAscendingCellStyle BackColor="#F5F7FB" /><SortedAscendingHeaderStyle BackColor="#6D95E1" /><SortedDescendingCellStyle BackColor="#E9EBEF" /><SortedDescendingHeaderStyle BackColor="#4870BE" /></asp:GridView><asp:SqlDataSource ID="CompResults" runat="server" 
                ConnectionString="<%$ ConnectionStrings:AthleticsConnectionString %>" 
                SelectCommand="CompResultsWithParameters" SelectCommandType="StoredProcedure"><SelectParameters><asp:ControlParameter ControlID="CompCode" Name="CompCode" PropertyName="Text" 
                        Type="String" />
            <asp:ControlParameter ControlID="SelGender" Name="SelectedGender" PropertyName="SelectedValue" Type="String" />
            <asp:ControlParameter ControlID="SelAgeFrom" Name="SelectedAgeFrom" PropertyName="SelectedValue" Type="String" />
            <asp:ControlParameter ControlID="SelAgeTo" Name="SelectedAgeTo" PropertyName="SelectedValue" Type="String" />
            <asp:ControlParameter ControlID="SelectDay" Name="SelectedDay" PropertyName="SelectedValue" Type="String" />
            <asp:ControlParameter ControlID="MaxNoOfLines" Name="MaxNoOfLinesText" PropertyName="Text" Type="String" />
        </SelectParameters></asp:SqlDataSource></ContentTemplate>

        <br />
     
    <asp:Label ID="Label2" runat="server" Text="Hlekkur á þessa síðu: "></asp:Label>    
     <asp:TextBox ID="LinkToPage" runat="server" ReadOnly="true" Font-Size="Small" Width="500px" BorderStyle="None"></asp:TextBox>
    <br />
    </p>
</asp:Content>
