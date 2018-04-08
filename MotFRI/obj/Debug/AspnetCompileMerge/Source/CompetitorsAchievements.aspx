<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CompetitorsAchievements.aspx.cs" Inherits="MotFRI.CompetitorsAchievements" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
<script>
  (function(i,s,o,g,r,a,m){i['GoogleAnalyticsObject']=r;i[r]=i[r]||function(){
  (i[r].q=i[r].q||[]).push(arguments)},i[r].l=1*new Date();a=s.createElement(o),
  m=s.getElementsByTagName(o)[0];a.async=1;a.src=g;m.parentNode.insertBefore(a,m)
  })(window,document,'script','https://www.google-analytics.com/analytics.js','ga');

  ga('create', 'UA-82054248-1', 'auto');
  ga('send', 'pageview');

</script>

    <asp:Label runat="server" Text="Afrek keppanda"></asp:Label>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <br /><p style="font-family: Arial">
    <asp:TextBox ID="CompetitorsCode" runat="server" Visible="false" ></asp:TextBox>
        <asp:TextBox ID="YearFromControl" runat="server" Visible="false"></asp:TextBox>
        <asp:TextBox ID="YearToControl" runat="server" Visible="false"></asp:TextBox>
        <asp:TextBox ID="OutdoorsIndoorsControl" runat="server" Visible="false"></asp:TextBox>
        <br />

        <asp:Table ID="Table1" runat="server" >
    <asp:TableRow> 
        <asp:TableCell> <asp:Label ID="Label1" runat="server" Text="Nafn: "></asp:Label></asp:TableCell>
        <asp:TableCell><asp:TextBox ID="CompetitorsName" runat="server" Width="350px" style="font-size: large" ReadOnly="true" BorderStyle="None"></asp:TextBox> </asp:TableCell>
        <asp:TableCell>Ár frá: </asp:TableCell><asp:TableCell>
            <asp:DropDownList ID="YearFrom" runat="server" AutoPostBack="true"></asp:DropDownList></asp:TableCell>
        <asp:TableCell>Ár til: </asp:TableCell><asp:TableCell>
            <asp:DropDownList ID="YearTo" runat="server" AutoPostBack="true"></asp:DropDownList></asp:TableCell>
        <asp:TableCell>Outdoors/Indoors: 
            </asp:TableCell>
        <asp:TableCell>
                    <asp:DropDownList ID="OutdoorsIndoors" runat="server" AutoPostBack="true">
            <asp:ListItem Value="0">Utanhúss</asp:ListItem>
            <asp:ListItem Value="1">Innanhúss</asp:ListItem>
            <asp:ListItem Value="%">Bæði utan- og innanhúss</asp:ListItem>
                       
        </asp:DropDownList>
        </asp:TableCell>
    </asp:TableRow>
     <asp:TableRow>
        <asp:TableCell> <asp:Label ID="Label2" runat="server" Text="Félag: "></asp:Label></asp:TableCell>
        <asp:TableCell><asp:TextBox ID="CompetitorsClub" runat="server" ReadOnly="true" BorderStyle="None"></asp:TextBox></asp:TableCell> 
     </asp:TableRow>
     <asp:TableRow>
         <asp:TableCell><asp:Label ID="Label3" runat="server" Text="Fæðingarár"></asp:Label></asp:TableCell>
        <asp:TableCell><asp:TextBox ID="YearOfBirth" runat="server" ReadOnly="true" BorderStyle="None"></asp:TextBox></asp:TableCell>
     </asp:TableRow>
</asp:Table>
        <br />
        <asp:Label ID="RecordsHeader" runat="server" Text="Listi yfir met:" Width="300px" style="font-size: large; font-style: italic"></asp:Label>
        <asp:GridView ID="RecordsGridView" runat="server" AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" DataSourceID="CompetitorRecs" ForeColor="#333333" GridLines="None">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField DataField="ActiveText" HeaderText="Virkt/Óvirkt" SortExpression="ActiveText" />
                <asp:BoundField DataField="AgeGroup" HeaderText="Aldursfl" SortExpression="AgeGroup" />
                <asp:BoundField DataField="EventName" HeaderText="Keppnisgrein" SortExpression="EventName" />
                <asp:BoundField DataField="OutdoorsOrIndoors" HeaderText="Úti/Inni" SortExpression="OutdoorsOrIndoors" />
                <asp:BoundField DataField="Results" HeaderText="Árangur" SortExpression="Results" />
                <asp:BoundField DataField="AchievementDate" DataFormatString="{0:d}" HeaderText="Dagsetning" SortExpression="AchievementDate" />
                <asp:BoundField DataField="Venue" HeaderText="Staður" SortExpression="Venue" />
                <asp:BoundField DataField="Club" HeaderText="Félag" SortExpression="Club" />
                <asp:BoundField DataField="Age" HeaderText="Aldur" SortExpression="Age" />
                <asp:BoundField DataField="WindReading" DataFormatString=" {0:F1}" HeaderText="Vindur" SortExpression="WindReading" />
                <asp:BoundField DataField="CompetitionName" HeaderText="Heiti móts" SortExpression="CompetitionName" />
                <asp:BoundField DataField="CompetitionCode" HeaderText="Tákn móts" SortExpression="CompetitionCode" />
                <asp:BoundField DataField="Placing" HeaderText="Röð" SortExpression="Placing" />
                <asp:BoundField DataField="Remarks" HeaderText="Aths" SortExpression="Remarks" />
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
        <asp:SqlDataSource ID="CompetitorRecs" runat="server" ConnectionString="<%$ ConnectionStrings:AthleticsConnectionString %>" SelectCommand="CompetitorsRecords" SelectCommandType="StoredProcedure">
            <SelectParameters>
                <asp:ControlParameter ControlID="CompetitorsCode" Name="CompetitorNo" PropertyName="Text" Type="String" />
                <asp:ControlParameter ControlID="YearFromControl" Name="YearFrom" PropertyName="Text" Type="Int32" />
                <asp:ControlParameter ControlID="YearToControl" Name="YearTo" PropertyName="Text" Type="Int32" />
                <asp:ControlParameter ControlID="OutdoorsIndoorsControl" Name="OutdoorsIndoorsFilter" PropertyName="Text" Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>
        <br />
        <br />
        <asp:Label ID="AchievementHeader" runat="server" Text="Afrekalisti:" Width="300px" style="font-size: large; font-style: italic"></asp:Label>

        <br />
        <asp:GridView ID="CompetitorAchievmentsGrid" runat="server" AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" DataSourceID="CompAchievem" ForeColor="#333333" GridLines="None">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField DataField="OutdoorsOrIndoors" HeaderText="Úti/Inni" SortExpression="OutdoorsOrIndoors" />
                <asp:BoundField DataField="EventName" HeaderText="Keppnisgrein" SortExpression="EventName" />
                <asp:BoundField DataField="Results" HeaderText="Árangur" SortExpression="Results" />
                <asp:BoundField DataField="WindReadingText" HeaderText="Vindur" SortExpression="WindReading"  />
                <asp:BoundField DataField="Placing" HeaderText="Röð" SortExpression="Placing" />
                <asp:BoundField DataField="AchievementDate" HeaderText="Dagsetn." SortExpression="AchievementDate" DataFormatString="{0:d}" />
                <asp:BoundField DataField="CompetitionName" HeaderText="Heiti móts" SortExpression="CompetitionName" />
                <asp:BoundField DataField="Venue" HeaderText="Staður" SortExpression="Venue" />
                <asp:BoundField DataField="Club" HeaderText="Félag" SortExpression="Club" />
                <asp:BoundField DataField="Age" HeaderText="Aldur" SortExpression="Age" />
                <asp:BoundField DataField="Series" HeaderText="Sería" SortExpression="Series">
                <ItemStyle Font-Size="X-Small" />
                </asp:BoundField>
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
        <asp:SqlDataSource ID="CompAchievem" runat="server" ConnectionString="<%$ ConnectionStrings:AthleticsConnectionString %>" SelectCommand="CompetitorsAchievements" SelectCommandType="StoredProcedure">
            <SelectParameters>
                <asp:ControlParameter ControlID="CompetitorsCode" Name="CompetitorNo" PropertyName="Text" Type="String" />
                <asp:ControlParameter ControlID="YearFromControl" DefaultValue="" Name="YearFrom" PropertyName="Text" Type="Int32" />
                <asp:ControlParameter ControlID="YearToControl" DefaultValue="" Name="YearTo" PropertyName="Text" Type="Int32" />
                <asp:ControlParameter ControlID="OutdoorsIndoorsControl" DefaultValue="" Name="OutdoorsIndoorsFilter" PropertyName="Text" Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>
        </p>
</asp:Content>
