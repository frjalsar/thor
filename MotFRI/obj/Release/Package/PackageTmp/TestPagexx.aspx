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
    <asp:GridView ID="Gre" runat="server" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="mot,greinnumer" DataSourceID="Greinar2" ForeColor="#333333" GridLines="None">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:BoundField DataField="mot" HeaderText="mot" ReadOnly="True" SortExpression="mot" />
            <asp:BoundField DataField="greinnumer" HeaderText="greinnumer" ReadOnly="True" SortExpression="greinnumer" />
            <asp:BoundField DataField="grein" HeaderText="grein" SortExpression="grein" />
            <asp:BoundField DataField="kyn" HeaderText="kyn" ReadOnly="True" SortExpression="kyn" />
            <asp:BoundField DataField="flokkur" HeaderText="flokkur" SortExpression="flokkur" />
            <asp:BoundField DataField="heitigreinar" HeaderText="heitigreinar" SortExpression="heitigreinar" />
            <asp:BoundField DataField="aldurfra" HeaderText="aldurfra" SortExpression="aldurfra" />
            <asp:BoundField DataField="aldurtil" HeaderText="aldurtil" SortExpression="aldurtil" />
            <asp:BoundField DataField="rodiafrekaskra" HeaderText="rodiafrekaskra" ReadOnly="True" SortExpression="rodiafrekaskra" />
            <asp:BoundField DataField="ridilledaurslit" HeaderText="ridilledaurslit" SortExpression="ridilledaurslit" />
            <asp:BoundField DataField="dagsetninggreinar" HeaderText="dagsetninggreinar" SortExpression="dagsetninggreinar" />
            <asp:BoundField DataField="timi" HeaderText="timi" ReadOnly="True" SortExpression="timi" />
            <asp:BoundField DataField="ensktheitigreinar" HeaderText="ensktheitigreinar" SortExpression="ensktheitigreinar" />
            <asp:BoundField DataField="stadur" HeaderText="stadur" SortExpression="stadur" />
            <asp:BoundField DataField="Dagsetning og tími" HeaderText="Dagsetning og tími" ReadOnly="True" SortExpression="Dagsetning og tími" />
            <asp:BoundField DataField="Aldursflokkur" HeaderText="Aldursflokkur" ReadOnly="True" SortExpression="Aldursflokkur" />
            <asp:BoundField DataField="Tegundgreinar" HeaderText="Tegundgreinar" ReadOnly="True" SortExpression="Tegundgreinar" />
            <asp:BoundField DataField="NánariTegundagreining" HeaderText="NánariTegundagreining" ReadOnly="True" SortExpression="NánariTegundagreining" />
            <asp:BoundField DataField="StaðaKeppni" HeaderText="StaðaKeppni" ReadOnly="True" SortExpression="StaðaKeppni" />
            <asp:BoundField DataField="Fjoldi" HeaderText="Fjoldi" ReadOnly="True" SortExpression="Fjoldi" />
            <asp:BoundField DataField="stigagrein" HeaderText="stigagrein" ReadOnly="True" SortExpression="stigagrein" />
        </Columns>
        <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
        <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
        <SortedAscendingCellStyle BackColor="#FDF5AC" />
        <SortedAscendingHeaderStyle BackColor="#4D0000" />
        <SortedDescendingCellStyle BackColor="#FCF6C0" />
        <SortedDescendingHeaderStyle BackColor="#820000" />
    </asp:GridView>
    <asp:SqlDataSource ID="Greinar2" runat="server" ConnectionString="<%$ ConnectionStrings:AthleticsConnectionString %>" SelectCommand="EventsInCompetition2" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:ControlParameter ControlID="CompCode" Name="CompCode" PropertyName="Text" Type="String" />
            <asp:ControlParameter ControlID="EventStatus" Name="EventStatusFilter" PropertyName="Text" Type="String" />
            <asp:ControlParameter ControlID="GenderFilter" Name="GenderFilter" PropertyName="Text" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>
