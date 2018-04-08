<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RegisterRelay.aspx.cs" Inherits="MotFRI.RegisterRelay" %>
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
<asp:TextBox ID="CompetitionName" runat="server" ReadOnly="true" Height="43px" Width="1421px" BorderStyle="None"></asp:TextBox><br />
    <asp:TextBox ID="CompCode" runat="server" ReadOnly="true" Visible="false"></asp:TextBox>
    <asp:TextBox ID="LoginUserID" runat="server" ReadOnly="true" Visible="false"></asp:TextBox><br />
    <asp:Label ID="Label1" runat="server" Text="Félag: "></asp:Label>
    <asp:DropDownList ID="ClubDropDown" runat="server" DataSourceID="UserClubs" DataTextField="Club" DataValueField="Club" AutoPostBack="True"></asp:DropDownList>
    <br />
    <asp:SqlDataSource ID="UserClubs" runat="server" ConnectionString="<%$ ConnectionStrings:AthleticsConnectionString %>" SelectCommand="ClubsForUser" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:ControlParameter ControlID="LoginUserID" Name="UserLogin" PropertyName="Text" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
    <br />
    <asp:GridView ID="RelaysGrid" runat="server" AutoGenerateColumns="False" CellPadding="4" DataSourceID="RegisteredRelays" ForeColor="#333333" GridLines="None" OnRowDataBound="RelaysGrid_RowDataBound" OnRowCommand="RelaysGrid_RowCommand">
        <AlternatingRowStyle BackColor="White" />
        <Columns runat="server">
            <asp:BoundField DataField="LineType" HeaderText="LineType" SortExpression="LineType" />
            <asp:BoundField DataField="EventName" HeaderText="Heiti greinar" SortExpression="EventName" />
            <asp:BoundField DataField="EventDate" HeaderText="Dagsetning" SortExpression="EventDate" />
            <asp:BoundField DataField="EventTime" HeaderText="Tími" SortExpression="EventTime" />
            <asp:TemplateField ShowHeader="False" HeaderText="Skrá nýja sveit">
            <ItemTemplate>
                <asp:LinkButton ID="LineComm" Text="New" runat="server" CommandName="NewOrDelete" 
                    CommandArgument='<%# Eval("EventLineNo") + ";" + Eval("BibNo") %>' ></asp:LinkButton>
                <asp:LinkButton ID="NameOfRunners" Text="Nöfn" runat="server" CommandName="Runners"
                    CommandArgument='<%# Eval("EventLineNo") + ";" + Eval("BibNo") + ";" + Eval("RelayTeamName") %>' ></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="RelayTeamName" HeaderText="Heiti sveitar" SortExpression="RelayTeamName" />
            <asp:BoundField DataField="Club" HeaderText="Félag" SortExpression="Club" />

            <asp:BoundField DataField="Runners" HeaderText="Skipan boðhlaupssveitar" SortExpression="Runners" >
            <HeaderStyle HorizontalAlign="Left" />
            <ItemStyle Font-Size="Small" HorizontalAlign="Left" />
            </asp:BoundField>
            <asp:CommandField ShowSelectButton="True" Visible="False" />
            <asp:BoundField DataField="EventLineNo" HeaderText="EventLineNo" SortExpression="EventLineNo" />
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
    <asp:SqlDataSource ID="RegisteredRelays" runat="server" ConnectionString="<%$ ConnectionStrings:AthleticsConnectionString %>" SelectCommand="ReturnRegisteredRelayTeamsForClub" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:ControlParameter ControlID="CompCode" Name="CompCode" PropertyName="Text" Type="String" />
            <asp:ControlParameter ControlID="ClubDropDown" Name="SelectedClub" PropertyName="SelectedValue" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>
