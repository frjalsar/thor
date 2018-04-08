<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TestPagexx.aspx.cs" Inherits="MotFRI.TestPagexx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:TextBox ID="CompCode" runat="server" Text="SILFURL13">   </asp:TextBox>
    <br />
    <asp:TextBox ID="EventStatus" runat="server" Text="0">   </asp:TextBox>
    <br />
    <asp:TextBox ID="GenderFilter" runat="server" Text="%"></asp:TextBox>
    <br />
    <asp:TextBox ID="Nafn" runat="server" AutoPostBack="true"></asp:TextBox><br />
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" DataSourceID="SelCompetitorsWKennitOrNameDS" ForeColor="Black" GridLines="Vertical">
        <AlternatingRowStyle BackColor="#CCCCCC" />
        <Columns>
            <asp:BoundField DataField="LineType" HeaderText="LineType" SortExpression="LineType" />
            <asp:BoundField DataField="CompetitorCode" HeaderText="CompetitorCode" SortExpression="CompetitorCode" />
            <asp:BoundField DataField="Kennitala" HeaderText="Kennitala" SortExpression="Kennitala" />
            <asp:BoundField DataField="Nafn" HeaderText="Nafn" SortExpression="Nafn" />
            <asp:BoundField DataField="KynInt" HeaderText="KynInt" SortExpression="KynInt" />
            <asp:BoundField DataField="Fæð.ár" HeaderText="Fæð.ár" SortExpression="Fæð.ár" />
            <asp:BoundField DataField="Aldur" HeaderText="Aldur" SortExpression="Aldur" />
            <asp:BoundField DataField="Félag" HeaderText="Félag" SortExpression="Félag" />
            <asp:BoundField DataField="Kyn" HeaderText="Kyn" SortExpression="Kyn" />
            <asp:BoundField DataField="Fjöldi afreka" HeaderText="Fjöldi afreka" SortExpression="Fjöldi afreka" />
            <asp:BoundField DataField="Fæðingardagur" HeaderText="Fæðingardagur" ReadOnly="True" SortExpression="Fæðingardagur" />
            <asp:BoundField DataField="Land" HeaderText="Land" SortExpression="Land" />
        </Columns>
        <FooterStyle BackColor="#CCCCCC" />
        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F1F1F1" />
        <SortedAscendingHeaderStyle BackColor="#808080" />
        <SortedDescendingCellStyle BackColor="#CAC9C9" />
        <SortedDescendingHeaderStyle BackColor="#383838" />
    </asp:GridView>
    <asp:SqlDataSource ID="SelCompetitorsWKennitOrNameDS" runat="server" ConnectionString="<%$ ConnectionStrings:AthleticsConnectionString %>" SelectCommand="SelCompetitorsWKennitOrName" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:ControlParameter ControlID="TestCompCode" Name="CompetitionCode" PropertyName="Text" Type="String" />
            <asp:ControlParameter ControlID="TestKennit" Name="KennitIn" PropertyName="Text" Type="String" />
            <asp:ControlParameter ControlID="TestNafn" Name="NameIn" PropertyName="Text" Type="String" />
            <asp:ControlParameter ControlID="TestYearOfBirth" Name="YearOfBirth" PropertyName="Text" Type="String" />
            <asp:ControlParameter ControlID="TextBox1" Name="SearchInNatReg" PropertyName="Text" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
    CompCode <asp:TextBox ID="TestCompCode" runat="server"></asp:TextBox><br />
    Nafn (autopostb)<asp:TextBox ID="TestNafn" runat="server" AutoPostBack="true"></asp:TextBox><br />
    Kennit <asp:TextBox ID="TestKennit" runat="server"></asp:TextBox><br />
    Fæðár<asp:TextBox ID="TestYearOfBirth" runat="server"></asp:TextBox><br />
    Þjóð (0 eða 1)<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>

    </asp:Content>
