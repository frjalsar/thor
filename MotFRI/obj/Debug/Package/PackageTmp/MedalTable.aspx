<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MedalTable.aspx.cs" Inherits="MotFRI.MedalTable" %>
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
    <asp:TextBox ID="CompetitionName" runat="server" ReadOnly="true" Height="40px" style="font-size: xx-large" Width="1321px" BorderStyle="None"></asp:TextBox> <br /><br />
    <asp:Label ID="Label1" runat="server" Text="Verðlaunatafla" style="font-size: xx-large"></asp:Label><br /><br />
    <asp:Label ID="DateFilterLabel" runat="server" Text="Veldu dag: " style="font-size: x-large"></asp:Label>&nbsp;&nbsp;<asp:DropDownList ID="SelectDateDropDownList" runat="server" style="font-size: large" AutoPostBack="True">
    </asp:DropDownList>
    <br /><br />
    <asp:TextBox ID="CompCode" runat="server" Visible="false" ReadOnly="true"></asp:TextBox> <br />
    <asp:GridView ID="MedalTableGW" runat="server" AutoGenerateColumns="False" CellPadding="4" DataSourceID="MedalTableDS" ForeColor="#333333" GridLines="None">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:BoundField DataField="ClubCode" HeaderText="Félag" SortExpression="ClubCode" >
            <HeaderStyle Font-Size="X-Large" />
            <ItemStyle Font-Size="X-Large" HorizontalAlign="Left" />
            </asp:BoundField>
            <asp:BoundField DataField="ClubName" HeaderText="Heiti" SortExpression="ClubName" >
                  <HeaderStyle Font-Size="X-Large" />
            <ItemStyle Font-Size="X-Large" HorizontalAlign="Left" />
            </asp:BoundField>
            <asp:BoundField DataField="NoOfGold" HeaderText="Gullverðlaun" SortExpression="NoOfGold" Visible="false" >
                  <HeaderStyle Font-Size="X-Large" />
            <ItemStyle Font-Size="X-Large" HorizontalAlign="Center"  />
            </asp:BoundField>
            <asp:BoundField DataField="NoOfSilver" HeaderText="Silfurverðlaun" SortExpression="NoOfSilver" Visible="false" >
                  <HeaderStyle Font-Size="X-Large"  />
            <ItemStyle Font-Size="X-Large" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="NoOfBronze" HeaderText="Bronsverðlaun" SortExpression="NoOfBronze" Visible="false" >
                  <HeaderStyle Font-Size="X-Large" />
            <ItemStyle Font-Size="X-Large" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="Total" HeaderText="Samtals" ReadOnly="True" SortExpression="Total" Visible="false" >  
                <HeaderStyle Font-Size="X-Large" />
            <ItemStyle Font-Size="X-Large" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:HyperLinkField DataNavigateUrlFields="ClubCode" DataNavigateUrlFormatString="~/MedalWinners.aspx?Club={0}&amp;Type=1" DataTextField="NoOfGold" HeaderText="Gullverðlaun">
            <HeaderStyle Font-Size="Large" />
            <ItemStyle Font-Size="Large" HorizontalAlign="Center" />
            </asp:HyperLinkField>
            <asp:HyperLinkField DataNavigateUrlFields="ClubCode" DataNavigateUrlFormatString="~/MedalWinners.aspx?Club={0}&amp;Type=2" DataTextField="NoOfSilver" HeaderText="Silfurverðlaun" NavigateUrl="~/MedalWinnersaspx">
            <HeaderStyle Font-Size="Large" />
            <ItemStyle Font-Size="Large" HorizontalAlign="Center" />
            </asp:HyperLinkField>
            <asp:HyperLinkField DataNavigateUrlFields="ClubCode" DataNavigateUrlFormatString="~/MedalWinners.aspx?Club={0}&amp;Type=3" DataTextField="NoOfBronze" HeaderText="Bronsverðlaun" NavigateUrl="~/MedalWinners.aspx">
            <HeaderStyle Font-Size="Large" />
            <ItemStyle Font-Size="Large" HorizontalAlign="Center" />
            </asp:HyperLinkField>
            <asp:HyperLinkField DataNavigateUrlFields="ClubCode" DataNavigateUrlFormatString="~/MedalWinners.aspx?Club={0}&amp;Type=0" DataTextField="Total" HeaderText="Samtals" NavigateUrl="~/MedalWinners.aspx">
            <HeaderStyle Font-Size="Large" />
            <ItemStyle Font-Size="Large" HorizontalAlign="Center" />
            </asp:HyperLinkField>
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


    <asp:SqlDataSource ID="MedalTableDS" runat="server" ConnectionString="<%$ ConnectionStrings:AthleticsConnectionString %>" SelectCommand="MedalTable" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:ControlParameter ControlID="CompCode" Name="CompCode" PropertyName="Text" Type="String" />
            <asp:ControlParameter ControlID="SelectDateDropDownList" Name="SelectDate" PropertyName="SelectedValue" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>


</asp:Content>
