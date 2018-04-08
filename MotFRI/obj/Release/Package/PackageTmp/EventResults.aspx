<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EventResults.aspx.cs" Inherits="MotFRI.EventResults" %>
<asp:Content ID="EventResultsHdr" ContentPlaceHolderID="HeadContent" runat="server">
   Úrslit greinar
</asp:Content>
<asp:Content ID="EventResultsMain" ContentPlaceHolderID="MainContent" runat="server">
    <asp:TextBox ID="CompCode5" runat="server" Visible="False" ReadOnly="True"></asp:TextBox>
    <asp:TextBox ID="EvenNo" runat="server" Visible="False" ReadOnly="True"></asp:TextBox>
    <asp:TextBox ID="SelGender" runat="server" Visible="False" ReadOnly="True"></asp:TextBox>
    <asp:TextBox ID="EventCode" runat="server" Visible="False" ReadOnly="True"></asp:TextBox>
    <asp:TextBox ID="CompetitionName" runat="server" Font-Bold="True" Font-Size="Larger" 
        ReadOnly="True" Width="1367px"></asp:TextBox> <br /><br />
    <asp:TextBox ID="EventName" runat="server" Font-Bold="True" Font-Size="Larger" 
        ReadOnly="True" Width="1367px"></asp:TextBox>
        <br />
        <br />
    <asp:GridView ID="GridView1" runat="server" AllowSorting="True" 
        AutoGenerateColumns="False" CellPadding="4" 
        DataSourceID="CompetitionEventResults" ForeColor="#333333" GridLines="None">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:BoundField DataField="Röð innan greinar" HeaderText="Röð innan greinar" 
                SortExpression="Röð innan greinar" Visible="False" />
            <asp:BoundField DataField="Röð" HeaderText="Röð" SortExpression="Röð" />
            <asp:BoundField DataField="Raðsvæði" HeaderText="Raðsvæði" 
                SortExpression="Raðsvæði" Visible="False" />
            <asp:BoundField DataField="Árangur" HeaderText="Árangur" 
                SortExpression="Árangur" />
            <asp:BoundField DataField="Vindur" HeaderText="Vindur" ReadOnly="True" 
                SortExpression="Vindur" />
            <asp:BoundField DataField="Keppandanúmer" HeaderText="Keppandanúmer" 
                SortExpression="Keppandanúmer" Visible="False" />
            <asp:BoundField DataField="Nafn" HeaderText="Nafn" SortExpression="Nafn" />
            <asp:BoundField DataField="Félag" HeaderText="Félag" SortExpression="Félag" />
            <asp:BoundField DataField="Fæðingarár" HeaderText="Fæðingarár" 
                SortExpression="Fæðingarár" />
            <asp:BoundField DataField="Aldur keppanda" HeaderText="Aldur keppanda" 
                SortExpression="Aldur keppanda" />
            <asp:BoundField DataField="Sería" HeaderText="Sería" SortExpression="Sería" />
            <asp:BoundField DataField="Athugasemd" HeaderText="Athugasemd" 
                SortExpression="Athugasemd" />
            <asp:BoundField DataField="Erlendur ríkisborgari" 
                HeaderText="Erlendur ríkisborgari" ReadOnly="True" 
                SortExpression="Erlendur ríkisborgari" />
            <asp:BoundField DataField="Metaupplýsingar" HeaderText="Metaupplýsingar" 
                SortExpression="Metaupplýsingar" />
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
        <asp:SqlDataSource ID="CompetitionEventResults" runat="server" 
        ConnectionString="<%$ ConnectionStrings:AthleticsConnectionString %>" 
        SelectCommand="EventResults" SelectCommandType="StoredProcedure">
            <SelectParameters>
                <asp:ControlParameter ControlID="CompCode5" Name="CompCode" PropertyName="Text" 
                    Type="String" />
                <asp:ControlParameter ControlID="EvenNo" Name="EventNo" PropertyName="Text" 
                    Type="String" />
                <asp:ControlParameter ControlID="EventCode" Name="EventCode" 
                    PropertyName="Text" Type="String" />
                <asp:ControlParameter ControlID="SelGender" Name="Gender" PropertyName="Text" 
                    Type="String" />
            </SelectParameters>
    </asp:SqlDataSource>
        </asp:Content>
