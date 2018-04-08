<%@ Page Title="ÞÓR - Mótaforrit Frjálsíþróttasambands Íslands" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="MotFRI._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
<script>
  (function(i,s,o,g,r,a,m){i['GoogleAnalyticsObject']=r;i[r]=i[r]||function(){
  (i[r].q=i[r].q||[]).push(arguments)},i[r].l=1*new Date();a=s.createElement(o),
  m=s.getElementsByTagName(o)[0];a.async=1;a.src=g;m.parentNode.insertBefore(a,m)
  })(window,document,'script','https://www.google-analytics.com/analytics.js','ga');

  ga('create', 'UA-82054248-1', 'auto');
  ga('send', 'pageview');

</script>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    
     
     
    <asp:Label ID="Label1" runat="server" Text="Listi móta" Font-Size="Larger" 
        Font-Italic="True"></asp:Label><br />
    <br />
    <asp:Table ID="Table1" runat="server">
      <asp:TableRow>
          <asp:TableCell>
        Veldu ár: 
        <asp:DropDownList ID="VeljaAr" runat="server" AutoPostBack="True"
            DataSourceID="SkilaAriIMotaskra" DataTextField="Year1" DataValueField="Year2" 
            Width="117px">
        </asp:DropDownList>
              </asp:TableCell>
          <asp:TableCell Width="1200px" HorizontalAlign="Right">
                  <asp:HyperLink ID="InsertNewCompetitionHyperlink" runat="server" NavigateUrl="~/CompetitionSetup.aspx?Code=NYTT">Stofna nýtt mót</asp:HyperLink>

          </asp:TableCell>
      </asp:TableRow>
    </asp:Table>
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
