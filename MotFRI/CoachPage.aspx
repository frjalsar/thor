<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CoachPage.aspx.cs" Inherits="MotFRI.CoachPage" %>
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

    <asp:TextBox ID="CompCode" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="CurrentUser" runat="server" Visible="false"></asp:TextBox>
    <br />
    <br />
    <asp:Table ID="Table1" runat="server" Font-Size="Large">
        <asp:TableRow>
            <asp:TableCell>
                <asp:Label runat="server" Text="Nafnakall í hlaupum - fjöldi mínúta fyrir start"></asp:Label>
            </asp:TableCell>
            <asp:TableCell>
                <asp:TextBox ID="CallRoomTrack" runat="server" AutoPostBack="true" Text="20" Width="40px" ></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>
                <asp:Label runat="server" Text="Nafnakall í köstum, langstökki og þrístökki - fjöldi mínúta fyrir start"></asp:Label>&nbsp;&nbsp;
            </asp:TableCell>
            <asp:TableCell>
                <asp:TextBox ID="CallRoomField" runat="server" AutoPostBack="true" Text="30" Width="40px"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>
                <asp:TableRow>
            <asp:TableCell>
                <asp:Label runat="server" Text="Nafnakall í hástökki og stangarstökki - fjöldi mínúta fyrir start"></asp:Label>&nbsp;&nbsp;
            </asp:TableCell>
            <asp:TableCell>
                <asp:TextBox ID="CallRoomHJPV" runat="server" AutoPostBack="true" Text="30" Width="40px"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
            <br /><br />
    <asp:GridView ID="CoachesPageGV" runat="server" AutoGenerateColumns="False" DataSourceID="CoachesPageDataSource" CellPadding="4" ForeColor="#333333" GridLines="None" Font-Size="Small" OnRowDataBound="CoachesPageGV_RowDataBound">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
            <asp:BoundField DataField="EventDate" HeaderText="Dagsetn." SortExpression="EventDate" DataFormatString="{0:d}" />
            <asp:BoundField DataField="CallRoomTime" HeaderText="Nafnakall" SortExpression="CallRoomTime" >
            <ItemStyle Font-Bold="True" Font-Size="Large" />
            </asp:BoundField>
            <asp:BoundField DataField="EventTime" HeaderText="Tími" SortExpression="EventTime" />
            <asp:BoundField DataField="BibNo" HeaderText="Rásnr." SortExpression="BibNo" />
            <asp:BoundField DataField="CompetitorName" HeaderText="Keppandi" SortExpression="CompetitorName" >
            <HeaderStyle HorizontalAlign="Left" />
            <ItemStyle HorizontalAlign="Left" />
            </asp:BoundField>
            <asp:BoundField DataField="CompetitorYearOfBirth" HeaderText="F.ár" SortExpression="CompetitorYearOfBirth" />
            <asp:BoundField DataField="CompetitorClub" HeaderText="Félag" SortExpression="CompetitorClub" />
            <asp:BoundField DataField="HeatNumber" HeaderText="Riðill" SortExpression="HeatNumber" DataFormatString="{0:#}" />
            <asp:BoundField DataField="LaneOrOrder" HeaderText="Br/Röð" SortExpression="LaneOrOrder" />
            <asp:BoundField DataField="LineType" HeaderText="LineType" SortExpression="LineType" />
            <asp:BoundField DataField="EventLineNo" HeaderText="EventLineNo" SortExpression="EventLineNo" Visible="False" />
            <asp:BoundField DataField="EventType" HeaderText="EventType" SortExpression="EventType" Visible="False" />
            <asp:BoundField DataField="CloserEventType" HeaderText="CloserEventType" SortExpression="CloserEventType" Visible="False" />
            <asp:BoundField DataField="Pb" HeaderText="Pb" SortExpression="Pb" />
            <asp:BoundField DataField="Sb" HeaderText="Sb" SortExpression="Sb" />
            <asp:BoundField DataField="ResultOrder" HeaderText="Úrsl" SortExpression="ResultOrder" />
            <asp:BoundField DataField="Result" HeaderText="Árangur" SortExpression="Reult" />
            <asp:BoundField DataField="PerformanceRemarks" HeaderText="Ath" SortExpression="PerformanceRemarks" />
            <asp:BoundField DataField="EventName" HeaderText="-" SortExpression="EventName" />
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

    <asp:SqlDataSource ID="CoachesPageDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:AthleticsConnectionString %>" SelectCommand="RegistrationInEvFromClubWithDateTime" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:ControlParameter ControlID="CompCode" Name="CompCode" PropertyName="Text" Type="String" />
            <asp:ControlParameter ControlID="CurrentUser" Name="CurrentUser" PropertyName="Text" Type="String" />
            <asp:ControlParameter ControlID="CallRoomTrack" Name="CallRoomTrack" PropertyName="Text" Type="Int32" />
            <asp:ControlParameter ControlID="CallRoomField" Name="CallRoomField" PropertyName="Text" Type="Int32" />
            <asp:ControlParameter ControlID="CallRoomHJPV" Name="CallRoomHJPV" PropertyName="Text" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <br /><br />
    <asp:HyperLink ID="BackToCompetitors" runat="server" NavigateUrl="~/SelectedCompetitionCompetitors.aspx">Til baka í keppendur</asp:HyperLink>
    &nbsp;&nbsp;&nbsp;
    <asp:HyperLink ID="BackToEvents" runat="server" NavigateUrl="~/SelectedCompetitionEvents.aspx">Til baka í keppnisgreinar móts</asp:HyperLink>
</asp:Content>
