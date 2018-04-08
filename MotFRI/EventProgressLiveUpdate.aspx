<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EventProgressLiveUpdate.aspx.cs" Inherits="MotFRI.EventProgressLiveUpdate" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
<script>
  (function(i,s,o,g,r,a,m){i['GoogleAnalyticsObject']=r;i[r]=i[r]||function(){
  (i[r].q=i[r].q||[]).push(arguments)},i[r].l=1*new Date();a=s.createElement(o),
  m=s.getElementsByTagName(o)[0];a.async=1;a.src=g;m.parentNode.insertBefore(a,m)
  })(window,document,'script','https://www.google-analytics.com/analytics.js','ga');

  ga('create', 'UA-82054248-1', 'auto');
  ga('send', 'pageview');

</script>

    <meta http-equiv="refresh" content="10">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <asp:Table ID="Table1" runat="server">
        <asp:TableRow>
            <asp:TableCell HorizontalAlign="Center" Height="40px">
              <asp:TextBox ID="CompAndEventName" runat="server" Font-Size="XX-Large" Height="39px" ReadOnly="True" Width="1400px" BorderStyle="None"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>
<asp:TextBox ID="EventStatus" runat="server" Height="30px" style="font-style: italic; font-weight: 700; font-size:xx-large;" ReadOnly="true" Width="1400px" BorderStyle="None"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    <asp:TextBox ID="CompCode" runat="server" Visible="false" ReadOnly="true" ></asp:TextBox><asp:TextBox ID="EventLineNo" runat="server" Visible="false" ReadOnly="true"></asp:TextBox> 
    <asp:Label ID="FilterOnHeatLabel" runat="server" Text="Afmarka við riðil eða hóp: "></asp:Label>
    <asp:DropDownList ID="FilterOnSelectedHeadDropDownList" runat="server">
        <asp:ListItem Value="0">Allir riðlar</asp:ListItem>
    </asp:DropDownList>
    <br />
    <asp:GridView ID="EventStatusGridView" runat="server" AutoGenerateColumns="False" CellPadding="4" DataSourceID="EventInProgressSP" ForeColor="#333333" GridLines="None" OnRowDataBound="EventStatusGridView_RowDataBound" Font-Size="X-Large">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:BoundField DataField="Place" HeaderText="Röð" SortExpression="Place" />
            <asp:BoundField DataField="Performance" HeaderText="Árangur" ControlStyle-Font-Bold="true" ControlStyle-Font-Size="XX-Large" SortExpression="Performance" >
<ControlStyle Font-Bold="True" Font-Size="XX-Large"></ControlStyle>
            <ItemStyle Font-Bold="True" Font-Size="X-Large" />
            </asp:BoundField>
            <asp:BoundField DataField="BibNo" HeaderText="Rásn." SortExpression="BibNo" />
            <asp:BoundField DataField="Name" HeaderText="Nafn" SortExpression="Name" />
            <asp:BoundField DataField="Club" HeaderText="Félag" SortExpression="Club" />
            <asp:BoundField DataField="YearOfBirth" HeaderText="F.ár" SortExpression="YearOfBirth" />
            <asp:BoundField DataField="Series" HeaderText="Sería" SortExpression="Series" />
            <asp:BoundField DataField="NextInLine" HeaderText="Næsti" SortExpression="NextInLine" />
            <asp:BoundField DataField="Waiting" HeaderText="Bíður" SortExpression="Waiting" />
            <asp:BoundField DataField="StatusOfEvent" HeaderText="SE" SortExpression="StatusOfEvent" ControlStyle-Width="1px" >
<ControlStyle Width="1px"></ControlStyle>
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
    <asp:SqlDataSource ID="EventInProgressSP" runat="server" ConnectionString="<%$ ConnectionStrings:AthleticsConnectionString %>" SelectCommand="EventInProgress" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:ControlParameter ControlID="CompCode" Name="CompCode" PropertyName="Text" Type="String" />
            <asp:ControlParameter ControlID="EventLineNo" Name="EventLineNo" PropertyName="Text" Type="Int32" />
            <asp:ControlParameter ControlID="FilterOnSelectedHeadDropDownList" Name="HeatNo" PropertyName="SelectedValue" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <br /><br />
    <asp:Button ID="RefreshButton" runat="server" Text="Uppfæra Stöðu" OnClick="RefreshButton_Click" />
</asp:Content>
