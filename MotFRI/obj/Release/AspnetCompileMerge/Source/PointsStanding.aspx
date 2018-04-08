<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PointsStanding.aspx.cs" Inherits="MotFRI.PointsStanding" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<br />
<asp:TextBox ID="CompCode" runat="server" Visible="false" BorderStyle="None"></asp:TextBox>
<br />

    <asp:GridView ID="PointsGrid" runat="server" AutoGenerateColumns="False" DataSourceID="PointsStandingTwoDayComp" CellPadding="4" ForeColor="#333333" GridLines="None">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:BoundField DataField="Grp" HeaderText="Fl." SortExpression="Grp" />
            <asp:BoundField DataField="AgeGroup" HeaderText="Aldursflokkur" SortExpression="AgeGroup" />
            <asp:BoundField DataField="Club" HeaderText="Félag" SortExpression="Club" />
            <asp:BoundField DataField="ClubName" HeaderText="ClubName" SortExpression="ClubName" />
            <asp:BoundField DataField="PointsDay1" HeaderText="Stig fyrri dags" SortExpression="PointsDay1" >
            <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="PointsDay2" HeaderText="Stig síðari dags" SortExpression="PointsDay2" >
            <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="TotalPoints" HeaderText="Samtals stig" SortExpression="TotalPoints" >
            <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Center" />
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
    <asp:SqlDataSource ID="PointsStandingTwoDayComp" runat="server" ConnectionString="<%$ ConnectionStrings:AthleticsConnectionString %>" SelectCommand="PointsStandingTwoDaysComp" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:ControlParameter ControlID="CompCode" Name="CompetitionCode" PropertyName="Text" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>
