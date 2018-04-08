<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Motalisti.aspx.cs" Inherits="MotFRI.Motalisti" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="Label1" runat="server" Text="Listi móta" Font-Size="Larger" 
        Font-Italic="True"></asp:Label><br />
    <br />
    Veldu ár: 
    <asp:DropDownList ID="VeljaAr" runat="server" AutoPostBack="True"
        DataSourceID="SkilaAriIMotaskra" DataTextField="Year1" DataValueField="Year2" 
        Width="117px">
    </asp:DropDownList>
    <br />
    <br />
    <asp:GridView ID="MotArsins" runat="server" AllowSorting="True" 
    AutoGenerateColumns="False" CellPadding="4" DataKeyNames="Mót" 
    DataSourceID="SkilaMotumArsins" ForeColor="#333333" GridLines="None" 
        onselectedindexchanged="MotArsins_SelectedIndexChanged">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
            <asp:CommandField SelectText="Velja" ShowSelectButton="True" 
                ButtonType="Button" EditText="Breyta" NewText="" />
            <asp:BoundField DataField="Mót" HeaderText="Mót" 
                SortExpression="Mót" ReadOnly="True" />
            <asp:BoundField DataField="Heiti" HeaderText="Heiti" 
                SortExpression="Heiti"   />
            <asp:BoundField DataField="Dagsetn." HeaderText="Dagsetn." 
                SortExpression="Dagsetn." ReadOnly="True" />
            <asp:BoundField DataField="Date" HeaderText="Date" 
                SortExpression="Date" Visible="False" />
            <asp:BoundField DataField="Staður" HeaderText="Staður" 
                SortExpression="Staður" />
            <asp:BoundField DataField="Völlur" HeaderText="Völlur" 
                SortExpression="Völlur" />
            <asp:BoundField DataField="utiinni" HeaderText="Úti/Inni" ReadOnly="True" 
                SortExpression="utiinni" />
            <asp:BoundField DataField="Mótshaldari" HeaderText="Mótshaldari" 
                SortExpression="Mótshaldari" />
            <asp:BoundField DataField="hlaupedamot" HeaderText="Tegund" ReadOnly="True" 
                SortExpression="hlaupedamot" />
            <asp:BoundField DataField="EventType" HeaderText="Tegund móts" 
                SortExpression="EventType" />
            <asp:BoundField DataField="NoOfLines" HeaderText="Fj. lína" SortExpression="NoOfLines" DataFormatString="{0:#}" >
            <ItemStyle HorizontalAlign="Right" />
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
    <br />
    <asp:SqlDataSource ID="SkilaAriIMotaskra" runat="server" 
        ConnectionString="<%$ ConnectionStrings:AthleticsConnectionString %>" 
        SelectCommand="ArIMotaskra" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
    <asp:SqlDataSource ID="SkilaMotumArsins" runat="server" 
        ConnectionString="<%$ ConnectionStrings:AthleticsConnectionString %>" 
        SelectCommand="MotArsins" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:ControlParameter ControlID="VeljaAr" Name="Year" 
                PropertyName="SelectedValue" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>
