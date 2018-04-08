<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Points.aspx.cs" Inherits="MotFRI.Points" %>
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
    <asp:TextBox ID="CompCode" runat="server" Visible="false" ReadOnly="true"></asp:TextBox><br />
    <asp:TextBox ID="CompetitionName" runat="server" Height="34px" Width="1282px" ReadOnly="true" Font-Size="X-Large" BorderStyle="None"></asp:TextBox>
    <br />
    <br />
        <asp:Label ID="Label3" runat="server" Text="Samtals stigastaða" Font-Size="Large"></asp:Label><br />
    <asp:GridView ID="TotalPoints" runat="server" AutoGenerateColumns="False" CellPadding="4" DataSourceID="PointsStandingsTotalDS" ForeColor="#333333" GridLines="None" OnRowDataBound="TotalPoints_RowDataBound">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
            <asp:BoundField DataField="RecordType" HeaderText="RecordType" SortExpression="RecordType" />
            <asp:BoundField DataField="Points" HeaderText="Stig" SortExpression="Points" DataFormatString="{0:F1}" >
            <ItemStyle Font-Size="Large" HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:BoundField DataField="Club" HeaderText="Félag" SortExpression="Club" >
            <ItemStyle Font-Size="Large" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="Heitifelags" HeaderText="Heiti félags" SortExpression="Heitifelags" >
            <ItemStyle Font-Size="Large" />
            </asp:BoundField>
            <asp:BoundField DataField="Lina" HeaderText="Lina" SortExpression="Lina" />
            <asp:BoundField DataField="NoOfTeamsInComp" HeaderText="NoOfTeamsInComp" />
            <asp:HyperLinkField DataNavigateUrlFields="Club" DataNavigateUrlFormatString="~/CompetitorsPrPlace.aspx?Club={0}&amp;Place=1&amp;Gender=0" 
                DataTextField="NoOf1Place" HeaderText="1." NavigateUrl="~/CompetitorsPrPlace.aspx">
            <HeaderStyle Font-Size="Large" />
            <ItemStyle Font-Size="Large" HorizontalAlign="Center" />
            </asp:HyperLinkField>
            <asp:HyperLinkField DataNavigateUrlFields="Club" DataNavigateUrlFormatString="~/CompetitorsPrPlace.aspx?Club={0}&amp;Place=2&amp;Gender=0" 
                DataTextField="NoOf2Place" HeaderText="2." NavigateUrl="~/CompetitorsPrPlace.aspx">
            <HeaderStyle Font-Size="Large" />
            <ItemStyle Font-Size="Large" HorizontalAlign="Center" />
            </asp:HyperLinkField>
            <asp:HyperLinkField DataNavigateUrlFields="Club" DataNavigateUrlFormatString="~/CompetitorsPrPlace.aspx?Club={0}&amp;Place=3&amp;Gender=0" 
                DataTextField="NoOf3Place" HeaderText="3." NavigateUrl="~/CompetitorsPrPlace.aspx">
            <HeaderStyle Font-Size="Large" />
            <ItemStyle Font-Size="Large" HorizontalAlign="Center" />
            </asp:HyperLinkField>
            <asp:HyperLinkField DataNavigateUrlFields="Club" DataNavigateUrlFormatString="~/CompetitorsPrPlace.aspx?Club={0}&amp;Place=4&amp;Gender=0" 
                DataTextField="NoOf4Place" HeaderText="4." NavigateUrl="~/CompetitorsPrPlace.aspx">
            <HeaderStyle Font-Size="Large" />
            <ItemStyle Font-Size="Large" HorizontalAlign="Center" />
            </asp:HyperLinkField>
            <asp:HyperLinkField DataNavigateUrlFields="Club" DataNavigateUrlFormatString="~/CompetitorsPrPlace.aspx?Club={0}&amp;Place=5&amp;Gender=0" 
                DataTextField="NoOf5Place" HeaderText="5." NavigateUrl="~/CompetitorsPrPlace.aspx">
            <HeaderStyle Font-Size="Large" />
            <ItemStyle Font-Size="Large" HorizontalAlign="Center" />
            </asp:HyperLinkField>
            <asp:HyperLinkField DataNavigateUrlFields="Club" DataNavigateUrlFormatString="~/CompetitorsPrPlace.aspx?Club={0}&amp;Place=6&amp;Gender=0" 
                DataTextField="NoOf6Place" HeaderText="6." NavigateUrl="~/CompetitorsPrPlace.aspx">
            <HeaderStyle Font-Size="Large" />
            <ItemStyle Font-Size="Large" HorizontalAlign="Center" />
            </asp:HyperLinkField>
            <asp:HyperLinkField DataNavigateUrlFields="Club" DataNavigateUrlFormatString="~/CompetitorsPrPlace.aspx?Club={0}&amp;Place=7&amp;Gender=0" 
                DataTextField="NoOf7Place" HeaderText="7." NavigateUrl="~/CompetitorsPrPlace.aspx">
            <HeaderStyle Font-Size="Large" />
            <ItemStyle Font-Size="Large" HorizontalAlign="Center" />
            </asp:HyperLinkField>
            <asp:HyperLinkField DataNavigateUrlFields="Club" DataNavigateUrlFormatString="~/CompetitorsPrPlace.aspx?Club={0}&amp;Place=8&amp;Gender=0" 
                DataTextField="NoOf8Place" HeaderText="8." NavigateUrl="~/CompetitorsPrPlace.aspx">
            <HeaderStyle Font-Size="Large" />
            <ItemStyle Font-Size="Large" HorizontalAlign="Center" />
            </asp:HyperLinkField>
            <asp:HyperLinkField DataNavigateUrlFields="Club" DataNavigateUrlFormatString="~/CompetitorsPrPlace.aspx?Club={0}&amp;Place=9&amp;Gender=0" 
                DataTextField="NoOf9Place" HeaderText="9." NavigateUrl="~/CompetitorsPrPlace.aspx">
            <HeaderStyle Font-Size="Large" />
            <ItemStyle Font-Size="Large" HorizontalAlign="Center" />
            </asp:HyperLinkField>
            <asp:HyperLinkField DataNavigateUrlFields="Club" DataNavigateUrlFormatString="~/CompetitorsPrPlace.aspx?Club={0}&amp;Place=10&amp;Gender=0" 
                DataTextField="NoOf10Place" HeaderText="10." NavigateUrl="~/CompetitorsPrPlace.aspx">
            <HeaderStyle Font-Size="Large" />
            <ItemStyle Font-Size="Large" HorizontalAlign="Center" />
            </asp:HyperLinkField>

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
    <br />
    <br />
        <asp:Label ID="Label1" runat="server" Text="Staðan í kvennakeppninni" Font-Size="Large"></asp:Label><br />
    <asp:GridView ID="GridViewWomen" runat="server" AutoGenerateColumns="False" CellPadding="4" DataSourceID="PointsStandingWomenDS" ForeColor="#333333" GridLines="None" OnRowDataBound="TotalPoints_RowDataBound">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
            <asp:BoundField DataField="RecordType" HeaderText="RecordType" SortExpression="RecordType" />
            <asp:BoundField DataField="Points" HeaderText="Stig" SortExpression="Points" DataFormatString="{0:F1}" >
            <ItemStyle Font-Size="Large" HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:BoundField DataField="Club" HeaderText="Félag" SortExpression="Club" >
            <ItemStyle Font-Size="Large" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="Heitifelags" HeaderText="Heiti félags" SortExpression="Heitifelags" >
            <ItemStyle Font-Size="Large" />
            </asp:BoundField>
            <asp:BoundField DataField="Lina" HeaderText="Lina" SortExpression="Lina" />
            <asp:BoundField DataField="NoOfTeamsInComp" HeaderText="NoOfTeamsInComp" />
                        <asp:HyperLinkField DataNavigateUrlFields="Club" DataNavigateUrlFormatString="~/CompetitorsPrPlace.aspx?Club={0}&amp;Place=1&amp;Gender=2" 
                DataTextField="NoOf1Place" HeaderText="1." NavigateUrl="~/CompetitorsPrPlace.aspx">
            <HeaderStyle Font-Size="Large" />
            <ItemStyle Font-Size="Large" HorizontalAlign="Center" />
            </asp:HyperLinkField>
            <asp:HyperLinkField DataNavigateUrlFields="Club" DataNavigateUrlFormatString="~/CompetitorsPrPlace.aspx?Club={0}&amp;Place=2&amp;Gender=2" 
                DataTextField="NoOf2Place" HeaderText="2." NavigateUrl="~/CompetitorsPrPlace.aspx">
            <HeaderStyle Font-Size="Large" />
            <ItemStyle Font-Size="Large" HorizontalAlign="Center" />
            </asp:HyperLinkField>
            <asp:HyperLinkField DataNavigateUrlFields="Club" DataNavigateUrlFormatString="~/CompetitorsPrPlace.aspx?Club={0}&amp;Place=3&amp;Gender=2" 
                DataTextField="NoOf3Place" HeaderText="3." NavigateUrl="~/CompetitorsPrPlace.aspx">
            <HeaderStyle Font-Size="Large" />
            <ItemStyle Font-Size="Large" HorizontalAlign="Center" />
            </asp:HyperLinkField>
            <asp:HyperLinkField DataNavigateUrlFields="Club" DataNavigateUrlFormatString="~/CompetitorsPrPlace.aspx?Club={0}&amp;Place=4&amp;Gender=2" 
                DataTextField="NoOf4Place" HeaderText="4." NavigateUrl="~/CompetitorsPrPlace.aspx">
            <HeaderStyle Font-Size="Large" />
            <ItemStyle Font-Size="Large" HorizontalAlign="Center" />
            </asp:HyperLinkField>
            <asp:HyperLinkField DataNavigateUrlFields="Club" DataNavigateUrlFormatString="~/CompetitorsPrPlace.aspx?Club={0}&amp;Place=5&amp;Gender=2" 
                DataTextField="NoOf5Place" HeaderText="5." NavigateUrl="~/CompetitorsPrPlace.aspx">
            <HeaderStyle Font-Size="Large" />
            <ItemStyle Font-Size="Large" HorizontalAlign="Center" />
            </asp:HyperLinkField>
            <asp:HyperLinkField DataNavigateUrlFields="Club" DataNavigateUrlFormatString="~/CompetitorsPrPlace.aspx?Club={0}&amp;Place=6&amp;Gender=2" 
                DataTextField="NoOf6Place" HeaderText="6." NavigateUrl="~/CompetitorsPrPlace.aspx">
            <HeaderStyle Font-Size="Large" />
            <ItemStyle Font-Size="Large" HorizontalAlign="Center" />
            </asp:HyperLinkField>
            <asp:HyperLinkField DataNavigateUrlFields="Club" DataNavigateUrlFormatString="~/CompetitorsPrPlace.aspx?Club={0}&amp;Place=7&amp;Gender=2" 
                DataTextField="NoOf7Place" HeaderText="7." NavigateUrl="~/CompetitorsPrPlace.aspx">
            <HeaderStyle Font-Size="Large" />
            <ItemStyle Font-Size="Large" HorizontalAlign="Center" />
            </asp:HyperLinkField>
            <asp:HyperLinkField DataNavigateUrlFields="Club" DataNavigateUrlFormatString="~/CompetitorsPrPlace.aspx?Club={0}&amp;Place=8&amp;Gender=2" 
                DataTextField="NoOf8Place" HeaderText="8." NavigateUrl="~/CompetitorsPrPlace.aspx">
            <HeaderStyle Font-Size="Large" />
            <ItemStyle Font-Size="Large" HorizontalAlign="Center" />
            </asp:HyperLinkField>
            <asp:HyperLinkField DataNavigateUrlFields="Club" DataNavigateUrlFormatString="~/CompetitorsPrPlace.aspx?Club={0}&amp;Place=9&amp;Gender=2" 
                DataTextField="NoOf9Place" HeaderText="9." NavigateUrl="~/CompetitorsPrPlace.aspx">
            <HeaderStyle Font-Size="Large" />
            <ItemStyle Font-Size="Large" HorizontalAlign="Center" />
            </asp:HyperLinkField>
            <asp:HyperLinkField DataNavigateUrlFields="Club" DataNavigateUrlFormatString="~/CompetitorsPrPlace.aspx?Club={0}&amp;Place=10&amp;Gender=2" 
                DataTextField="NoOf10Place" HeaderText="10." NavigateUrl="~/CompetitorsPrPlace.aspx">
            <HeaderStyle Font-Size="Large" />
            <ItemStyle Font-Size="Large" HorizontalAlign="Center" />
            </asp:HyperLinkField>

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
   
<br /><br />
            <asp:Label ID="Label2" runat="server" Text="Staðan í karlakeppninni" Font-Size="Large"></asp:Label><br />
    <asp:GridView ID="GridViewMen" runat="server" AutoGenerateColumns="False" CellPadding="4" DataSourceID="PointsStandingMenDS" ForeColor="#333333" GridLines="None" OnRowDataBound="TotalPoints_RowDataBound">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
            <asp:BoundField DataField="RecordType" HeaderText="RecordType" SortExpression="RecordType" />
            <asp:BoundField DataField="Points" HeaderText="Stig" SortExpression="Points" DataFormatString="{0:F1}" >
            <ItemStyle Font-Size="Large" HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:BoundField DataField="Club" HeaderText="Félag" SortExpression="Club" >
            <ItemStyle Font-Size="Large" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="Heitifelags" HeaderText="Heiti félags" SortExpression="Heitifelags" >
            <ItemStyle Font-Size="Large" />
            </asp:BoundField>
            <asp:BoundField DataField="Lina" HeaderText="Lina" SortExpression="Lina" />
            <asp:BoundField DataField="NoOfTeamsInComp" HeaderText="NoOfTeamsInComp" />
            <asp:HyperLinkField DataNavigateUrlFields="Club" DataNavigateUrlFormatString="~/CompetitorsPrPlace.aspx?Club={0}&amp;Place=1&amp;Gender=1" 
                DataTextField="NoOf1Place" HeaderText="1." NavigateUrl="~/CompetitorsPrPlace.aspx">
            <HeaderStyle Font-Size="Large" />
            <ItemStyle Font-Size="Large" HorizontalAlign="Center" />
            </asp:HyperLinkField>
            <asp:HyperLinkField DataNavigateUrlFields="Club" DataNavigateUrlFormatString="~/CompetitorsPrPlace.aspx?Club={0}&amp;Place=2&amp;Gender=1" 
                DataTextField="NoOf2Place" HeaderText="2." NavigateUrl="~/CompetitorsPrPlace.aspx">
            <HeaderStyle Font-Size="Large" />
            <ItemStyle Font-Size="Large" HorizontalAlign="Center" />
            </asp:HyperLinkField>
            <asp:HyperLinkField DataNavigateUrlFields="Club" DataNavigateUrlFormatString="~/CompetitorsPrPlace.aspx?Club={0}&amp;Place=3&amp;Gender=1" 
                DataTextField="NoOf3Place" HeaderText="3." NavigateUrl="~/CompetitorsPrPlace.aspx">
            <HeaderStyle Font-Size="Large" />
            <ItemStyle Font-Size="Large" HorizontalAlign="Center" />
            </asp:HyperLinkField>
            <asp:HyperLinkField DataNavigateUrlFields="Club" DataNavigateUrlFormatString="~/CompetitorsPrPlace.aspx?Club={0}&amp;Place=4&amp;Gender=1" 
                DataTextField="NoOf4Place" HeaderText="4." NavigateUrl="~/CompetitorsPrPlace.aspx">
            <HeaderStyle Font-Size="Large" />
            <ItemStyle Font-Size="Large" HorizontalAlign="Center" />
            </asp:HyperLinkField>
            <asp:HyperLinkField DataNavigateUrlFields="Club" DataNavigateUrlFormatString="~/CompetitorsPrPlace.aspx?Club={0}&amp;Place=5&amp;Gender=1" 
                DataTextField="NoOf5Place" HeaderText="5." NavigateUrl="~/CompetitorsPrPlace.aspx">
            <HeaderStyle Font-Size="Large" />
            <ItemStyle Font-Size="Large" HorizontalAlign="Center" />
            </asp:HyperLinkField>
            <asp:HyperLinkField DataNavigateUrlFields="Club" DataNavigateUrlFormatString="~/CompetitorsPrPlace.aspx?Club={0}&amp;Place=6&amp;Gender=1" 
                DataTextField="NoOf6Place" HeaderText="6." NavigateUrl="~/CompetitorsPrPlace.aspx">
            <HeaderStyle Font-Size="Large" />
            <ItemStyle Font-Size="Large" HorizontalAlign="Center" />
            </asp:HyperLinkField>
            <asp:HyperLinkField DataNavigateUrlFields="Club" DataNavigateUrlFormatString="~/CompetitorsPrPlace.aspx?Club={0}&amp;Place=7&amp;Gender=1" 
                DataTextField="NoOf7Place" HeaderText="7." NavigateUrl="~/CompetitorsPrPlace.aspx">
            <HeaderStyle Font-Size="Large" />
            <ItemStyle Font-Size="Large" HorizontalAlign="Center" />
            </asp:HyperLinkField>
            <asp:HyperLinkField DataNavigateUrlFields="Club" DataNavigateUrlFormatString="~/CompetitorsPrPlace.aspx?Club={0}&amp;Place=8&amp;Gender=1" 
                DataTextField="NoOf8Place" HeaderText="8." NavigateUrl="~/CompetitorsPrPlace.aspx">
            <HeaderStyle Font-Size="Large" />
            <ItemStyle Font-Size="Large" HorizontalAlign="Center" />
            </asp:HyperLinkField>
            <asp:HyperLinkField DataNavigateUrlFields="Club" DataNavigateUrlFormatString="~/CompetitorsPrPlace.aspx?Club={0}&amp;Place=9&amp;Gender=1" 
                DataTextField="NoOf9Place" HeaderText="9." NavigateUrl="~/CompetitorsPrPlace.aspx">
            <HeaderStyle Font-Size="Large" />
            <ItemStyle Font-Size="Large" HorizontalAlign="Center" />
            </asp:HyperLinkField>
            <asp:HyperLinkField DataNavigateUrlFields="Club" DataNavigateUrlFormatString="~/CompetitorsPrPlace.aspx?Club={0}&amp;Place=10&amp;Gender=1" 
                DataTextField="NoOf10Place" HeaderText="10." NavigateUrl="~/CompetitorsPrPlace.aspx">
            <HeaderStyle Font-Size="Large" />
            <ItemStyle Font-Size="Large" HorizontalAlign="Center" />
            </asp:HyperLinkField>
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
   

    <asp:SqlDataSource ID="PointsStandingsTotalDS" runat="server" ConnectionString="<%$ ConnectionStrings:AthleticsConnectionString %>" SelectCommand="PointsStandingMIAdalhl" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:ControlParameter ControlID="CompCode" Name="CompetitionCode" PropertyName="Text" Type="String" />
            <asp:Parameter DefaultValue="0" Name="Type" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <br />    
        <asp:SqlDataSource ID="PointsStandingMenDS" runat="server" ConnectionString="<%$ ConnectionStrings:AthleticsConnectionString %>" SelectCommand="PointsStandingMIAdalhl" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:ControlParameter ControlID="CompCode" Name="CompetitionCode" PropertyName="Text" Type="String" />
            <asp:Parameter DefaultValue="1" Name="Type" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <br />
    <asp:SqlDataSource ID="PointsStandingWomenDS" runat="server" ConnectionString="<%$ ConnectionStrings:AthleticsConnectionString %>" SelectCommand="PointsStandingMIAdalhl" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:ControlParameter ControlID="CompCode" Name="CompetitionCode" PropertyName="Text" Type="String" />
            <asp:Parameter DefaultValue="2" Name="Type" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>
