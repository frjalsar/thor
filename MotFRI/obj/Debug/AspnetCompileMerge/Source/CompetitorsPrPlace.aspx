<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CompetitorsPrPlace.aspx.cs" Inherits="MotFRI.CompetitorsPrPlace" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <asp:TextBox ID="CompetitionName" runat="server" ReadOnly="true" BorderStyle="None" style="font-size: x-large" Width="1396px"></asp:TextBox>
    <br /><br />
    <asp:TextBox ID="SelectionDescription" runat="server" ReadOnly="true" BorderStyle="None" style="font-size: large; font-weight: 700" Width="1374px"></asp:TextBox>
    <br /><br />
    <asp:TextBox ID="CompCode" runat="server" Visible="false" ReadOnly="true"></asp:TextBox>
    <asp:TextBox ID="SelectedClub" runat="server" Visible="false" ReadOnly="true"></asp:TextBox>
    <asp:TextBox ID="SelectedPlace" runat="server" Visible="false" ReadOnly="true"></asp:TextBox>
   <asp:TextBox ID="SelectedGender" runat="server" Visible="false" ReadOnly="true"></asp:TextBox>
    <asp:GridView ID="CompetitorsPrPlaceGridView" runat="server" CellPadding="4" DataSourceID="CompetitorsInSelPlaceDS" ForeColor="#333333" GridLines="None" AllowSorting="True" AutoGenerateColumns="False">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
            <asp:BoundField DataField="Place" HeaderText="Röð" SortExpression="Place" />
            <asp:BoundField DataField="Result" HeaderText="Árangur" SortExpression="Result" />
            <asp:BoundField DataField="CompetitorBibNo" HeaderText="Rásn." SortExpression="CompetitorBibNo" />
            <asp:BoundField DataField="CompetitorName" HeaderText="Nafn" SortExpression="CompetitorName" />
            <asp:BoundField DataField="CompetitorClub" HeaderText="Félag" SortExpression="CompetitorClub" />
            <asp:BoundField DataField="EventName" HeaderText="Heiti greinar" SortExpression="EventName" >
            <ItemStyle Font-Bold="True" Font-Size="Large" />
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
    <asp:SqlDataSource ID="CompetitorsInSelPlaceDS" runat="server" ConnectionString="<%$ ConnectionStrings:AthleticsConnectionString %>" SelectCommand="CompetitorsInSelPlace" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:ControlParameter ControlID="CompCode" Name="CompCode" PropertyName="Text" Type="String" />
            <asp:ControlParameter ControlID="SelectedClub" Name="SelectedClub" PropertyName="Text" Type="String" />
            <asp:ControlParameter ControlID="SelectedPlace" Name="SelectedPlace" PropertyName="Text" Type="String" />
            <asp:ControlParameter ControlID="SelectedGender" Name="SelectGender" PropertyName="Text" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>
