<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Statistics.aspx.cs" Inherits="MotFRI.Statistics" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
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
    <h1><asp:Label ID="Label1" runat="server" Text="Fyrirspurn í afrekaskrá Frjálsíþróttasambands Íslands"></asp:Label></h1>
    <br />
   

                
                <asp:Table ID="Criteria" runat="server">
                    <asp:TableRow>
                        <asp:TableHeaderCell Height="20px">Veldu ár eða tímabil</asp:TableHeaderCell>
                        <asp:TableHeaderCell>|</asp:TableHeaderCell>
                        <asp:TableHeaderCell>Veldu kyn</asp:TableHeaderCell>
                        <asp:TableHeaderCell>|</asp:TableHeaderCell>
                        <asp:TableHeaderCell>Veldu aldursflokk</asp:TableHeaderCell>
                        <asp:TableHeaderCell>|</asp:TableHeaderCell>
                        <asp:TableHeaderCell>Úti eða inni</asp:TableHeaderCell>
                        <asp:TableHeaderCell>|</asp:TableHeaderCell>
                        <asp:TableHeaderCell>Fj. lína</asp:TableHeaderCell>
                        <asp:TableHeaderCell>|</asp:TableHeaderCell>
                        <asp:TableHeaderCell>Aðeins félag</asp:TableHeaderCell>
                    </asp:TableRow>

                    <asp:TableRow>
                        <asp:TableCell VerticalAlign="Middle">
                            <asp:DropDownList ID="SelectYear" runat="server" DataSourceID="Athletics" DataTextField="Year2" DataValueField="Year1" AutoPostBack="True"></asp:DropDownList>
                            <asp:SqlDataSource ID="Athletics" runat="server" ConnectionString="<%$ ConnectionStrings:AthleticsConnectionString %>" SelectCommand="ArIOgFraUpph" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                       </asp:TableCell>
                        <asp:TableCell>|</asp:TableCell>
                       <asp:TableCell>
                           <asp:DropDownList ID="Gender" runat="server" Height="20px" AutoPostBack="True">
                             <asp:ListItem Value="1">Karlar</asp:ListItem>
                             <asp:ListItem Value="2">Konur</asp:ListItem>
                             </asp:DropDownList>
                       </asp:TableCell>
                        <asp:TableCell>|</asp:TableCell>
                        <asp:TableCell>
                            <asp:DropDownList ID="AgeGroup" runat="server" AutoPostBack="True">
                                <asp:ListItem Value="0">Allir aldursflokkar</asp:ListItem>
                                <asp:ListItem Value="1">20-22 ára</asp:ListItem>
                                <asp:ListItem Value="2">18-19 ára</asp:ListItem>
                                <asp:ListItem Value="3">16-17 ára</asp:ListItem>
                                <asp:ListItem Value="4">15 ára</asp:ListItem>
                                <asp:ListItem Value="5">14 ára</asp:ListItem>
                                <asp:ListItem Value="6">13 ára</asp:ListItem>
                                <asp:ListItem Value="7">12 ára</asp:ListItem>
                           </asp:DropDownList>
                        </asp:TableCell>
                        <asp:TableCell>|</asp:TableCell>
                        <asp:TableCell>
                            <asp:DropDownList ID="OutOrIndoors" runat="server" AutoPostBack="True">
                                <asp:ListItem Value="0">Utanhúss</asp:ListItem>
                                <asp:ListItem Value="1">Innahúss</asp:ListItem>
                            </asp:DropDownList>
                        </asp:TableCell>
                        <asp:TableCell>|</asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="ShowNoOfLines" runat="server" Width="74px" Height="20px" Text="100" AutoPostBack="True"></asp:TextBox>
                        </asp:TableCell>
                        <asp:TableCell>|</asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="SelectedClub" Width="90px" runat="server" 
                            AutoPostBack="True"></asp:TextBox>
                        </asp:TableCell>
                    </asp:TableRow>

                    <asp:TableRow>
                        <asp:TableCell>
                            <asp:TextBox ID="DateFr" runat="server" Width="90px" Height="20px" AutoPostBack="True"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" 
                                        DaysModeTitleFormat="dd.MM.yyyy" Enabled="True" Format="dd.MM.yyyy" 
                                        TargetControlID="DateFr" TodaysDateFormat="dd.MM.yyyy"></asp:CalendarExtender>
                             &nbsp;til&nbsp;
                            <asp:TextBox ID="DateTo" runat="server" Width="90px" Height="20px" AutoPostBack="True"></asp:TextBox>
                                                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" 
                                        DaysModeTitleFormat="dd.MM.yyyy" Enabled="True" Format="dd.MM.yyyy" 
                                        TargetControlID="DateTo" TodaysDateFormat="dd.MM.yyyy"></asp:CalendarExtender>

                        </asp:TableCell>
                        <asp:TableCell>|</asp:TableCell>
                        <asp:TableCell>&nbsp;</asp:TableCell>
                        <asp:TableCell>|</asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="AgeFr" runat="server" Width="30px" AutoPostBack="True"></asp:TextBox>&nbsp;til
                            <asp:TextBox ID="AgeTo" runat="server" Width="30px" AutoPostBack="True"></asp:TextBox>&nbsp;ára
                        </asp:TableCell>
                        <asp:TableCell>|</asp:TableCell>
                        <asp:TableCell>Löglegur vindur <asp:CheckBox ID="LeagalWind" runat="server" AutoPostBack="True" /></asp:TableCell>
                        <asp:TableCell>|</asp:TableCell>
                        <asp:TableCell>Með erl. ríkisborgurum <asp:CheckBox ID="ForeignCitizens" runat="server" AutoPostBack="True" /></asp:TableCell>
                        <asp:TableCell>|</asp:TableCell>

                    </asp:TableRow>
                    
                    <asp:TableRow>
                        <asp:TableCell>Veldu grein:</asp:TableCell>
                     </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                             <asp:DropDownList ID="AllEventsToSelect" runat="server" AutoPostBack="True" DataSourceID="AllEvents" 
                                 DataTextField="Heiti" DataValueField="Samsettur lykill">
                                <asp:ListItem></asp:ListItem>
                             </asp:DropDownList>
                            <asp:SqlDataSource ID="AllEvents" runat="server" 
                                ConnectionString="<%$ ConnectionStrings:AthleticsConnectionString %>" SelectCommand="AllEvents" SelectCommandType="StoredProcedure">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="Gend" Name="Gender" PropertyName="Text" Type="Int32" />
                                    <asp:ControlParameter ControlID="OutInd" Name="OutdInd" PropertyName="Text" Type="Int32" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
                <br />


            <br />

                <asp:TextBox ID="LastSelectedYear" runat="server" Visible="False"></asp:TextBox>
                <asp:TextBox ID="LastSelectedAgeGroup" runat="server" Visible="False"></asp:TextBox>
                <asp:TextBox ID="Gre" runat="server" Visible="False"></asp:TextBox>
                <asp:TextBox ID="Gend" runat="server" Visible="False" Text="0"></asp:TextBox>
                <asp:TextBox ID="Flo" runat="server" Visible="False"></asp:TextBox>
                <asp:TextBox ID="OutInd" runat="server" Visible="False" Text="1"></asp:TextBox>
                <asp:TextBox ID="LastSelectedAllEvents" runat="server" Visible="False"></asp:TextBox>
                <asp:TextBox ID="SelectedClubToSubmit" runat="server" Visible="False"></asp:TextBox>
                <asp:TextBox ID="WindFrom" runat="server" Visible="False" Text="-5000"></asp:TextBox>
                <asp:TextBox ID="WindTo" runat="server" Visible="False" Text="2"></asp:TextBox>
                <asp:TextBox ID="Foreigner" runat="server" Visible="False" Text="0"></asp:TextBox>
                <asp:TextBox ID="CompetitionGrCode" runat="server" Text="%" Visible="False"></asp:TextBox>

            
            
      <div class="h6">
        <asp:GridView ID="TopList" runat="server" AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" DataSourceID="TopListSP" ForeColor="#333333" GridLines="None">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:BoundField DataField="Row" HeaderText="Röð" ReadOnly="True" SortExpression="Row" />
                <asp:BoundField DataField="Árangur" HeaderText="Árangur" SortExpression="Árangur" />
                <asp:BoundField DataField="Vindur" HeaderText="Vindur" SortExpression="Vindur" DataFormatString="{0:N1}" />
                <asp:BoundField DataField="Nafn" HeaderText="Nafn" SortExpression="Nafn" />
                <asp:BoundField DataField="Félag" HeaderText="Félag" SortExpression="Félag" />
                <asp:BoundField DataField="Dagsetning" DataFormatString="{0:d}" HeaderText="Dags." SortExpression="Dagsetning" />
                <asp:BoundField DataField="Staður" HeaderText="Staður" SortExpression="Staður" />
                <asp:BoundField DataField="Heiti Móts" HeaderText="Heiti Móts" SortExpression="Heiti Móts" />
                <asp:BoundField DataField="Fæðingarár" HeaderText="Fæð.ár" SortExpression="Fæðingarár" />
                <asp:BoundField DataField="Aldur keppanda" HeaderText="Aldur" SortExpression="Aldur keppanda" />
                <asp:BoundField DataField="Keppandanúmer" HeaderText="Keppandanúmer" SortExpression="Keppandanúmer" Visible="False" />
                <asp:BoundField DataField="Raðsvæði" HeaderText="Raðsvæði" SortExpression="Raðsvæði" Visible="False" />
                <asp:BoundField DataField="Erlendur ríkisborgari" HeaderText="Erlendur ríkisborgari" SortExpression="Erlendur ríkisborgari" Visible="False" />
                <asp:HyperLinkField DataNavigateUrlFields="Keppandanúmer" HeaderText="Afrek keppanda" DataNavigateUrlFormatString="~\CompetitorsAchievements.aspx?CompetitorCode={0}" DataTextField="Nafn" Text="Afrek" />
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
        <asp:SqlDataSource ID="TopListSP" runat="server" ConnectionString="<%$ ConnectionStrings:AthleticsConnectionString %>" SelectCommand="TopList" SelectCommandType="StoredProcedure">
            <SelectParameters>
                <asp:ControlParameter ControlID="Gre" Name="Event" PropertyName="Text" Type="String" />
                <asp:ControlParameter ControlID="Flo" Name="Grp" PropertyName="Text" Type="String" />
                <asp:ControlParameter ControlID="Gend" Name="Gend" PropertyName="Text" Type="Int32" />
                <asp:ControlParameter ControlID="OutInd" Name="OutdoorsIndoors" PropertyName="Text" Type="Int32" />
                <asp:ControlParameter ControlID="DateFr" DbType="Date" Name="DateFrom" PropertyName="Text" />
                <asp:ControlParameter ControlID="DateTo" DbType="Date" Name="DateTo" PropertyName="Text" />
                <asp:ControlParameter ControlID="AgeFr" Name="AgeFrom" PropertyName="Text" Type="Int32" />
                <asp:ControlParameter ControlID="AgeTo" Name="AgeTo" PropertyName="Text" Type="Int32" />
                <asp:ControlParameter ControlID="WindFrom" Name="WindFrom" PropertyName="Text" Type="Int32" />
                <asp:ControlParameter ControlID="WindTo" Name="WindTo" PropertyName="Text" Type="Int32" />
                <asp:ControlParameter ControlID="Foreigner" Name="Foreigner" PropertyName="Text" Type="Int32" />
                <asp:ControlParameter ControlID="ShowNoOfLines" Name="NoOfLines" PropertyName="Text" Type="Int32" />
                <asp:ControlParameter ControlID="SelectedClubToSubmit" Name="Fel" PropertyName="Text" Type="String" />
                <asp:ControlParameter ControlID="CompetitionGrCode" Name="KotiTegMots" PropertyName="Text" Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>

          <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>

</asp:Content>
