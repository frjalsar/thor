<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SelectCompetitor.aspx.cs" Inherits="MotFRI.SelectCompetitor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style1
        {
            font-family: "Segoe UI";
            font-size: large;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <br />
        <span class="style1">Veldu keppanda:</span></p> 
   
    <asp:TextBox ID="CompetitionCode" runat="server" Visible="true" ReadOnly="true"></asp:TextBox>
    <asp:TextBox ID="KennitalaText" runat="server" Visible="true" ReadOnly="true"></asp:TextBox>
    <asp:TextBox ID="NafnText" runat="server" Visible="true" ReadOnly="true"></asp:TextBox>
    <asp:TextBox ID="FaedArText" runat="server" Visible="true" ReadOnly="true"></asp:TextBox>
    <asp:GridView ID="SelectCompetitorGridView" runat="server" 
        DataSourceID="SelCompetitorsWKennit" AllowSorting="True" 
        AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" 
        GridLines="None" 
        onselectedindexchanged="SelectCompetitorGridView_SelectedIndexChanged">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
                      <asp:TemplateField HeaderText="Velja">
                       <ItemTemplate>
                         <asp:CheckBox ID="ValinChk" runat="server"  /> 
                       </ItemTemplate>
                          <HeaderStyle HorizontalAlign="Center" />
                          <ItemStyle HorizontalAlign="Center" />
                      </asp:TemplateField>

            <asp:BoundField DataField="CompetitorCode" HeaderText="Kepp.nr." SortExpression="CompetitorCode" />
            <asp:BoundField DataField="Kennitala" HeaderText="Kennitala" 
                SortExpression="Kennitala" />
            <asp:BoundField DataField="Nafn" HeaderText="Nafn" SortExpression="Nafn" />
            <asp:BoundField DataField="Fæðingardagur" HeaderText="Fæðingardagur" ReadOnly="True" SortExpression="Fæðingardagur" />
            <asp:BoundField DataField="Fæð.ár" HeaderText="Fæð.ár" 
                SortExpression="Fæð.ár" />
            <asp:BoundField DataField="Aldur" HeaderText="Aldur" 
                SortExpression="Aldur" />
            <asp:BoundField DataField="Félag" HeaderText="Félag" SortExpression="Félag" />
            <asp:BoundField DataField="Kyn" HeaderText="Kyn" SortExpression="Kyn" />
            <asp:BoundField DataField="Fjöldi afreka" HeaderText="Fjöldi afreka" 
                SortExpression="Fjöldi afreka" />
            <asp:BoundField DataField="Land" HeaderText="Land" SortExpression="Land" />
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
    <asp:SqlDataSource ID="SelCompetitorsWKennit" runat="server" 
        ConnectionString="<%$ ConnectionStrings:AthleticsConnectionString %>" 
        SelectCommand="SelCompetitorsWKennitOrName" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:ControlParameter ControlID="CompetitionCode" Name="CompetitionCode" PropertyName="Text" Type="String" />
            <asp:ControlParameter ControlID="KennitalaText" Name="KennitIn" 
                PropertyName="Text" Type="String" />
            <asp:ControlParameter ControlID="NafnText" Name="NameIn" PropertyName="Text" Type="String" />
            <asp:ControlParameter ControlID="FaedArText" Name="YearOfBirth" PropertyName="Text" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
    <br />
    <asp:Button ID="InsertSelected" runat="server" Text="Skrá valda keppendur" OnClick="InsertSelected_Click" />
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
</asp:Content>
