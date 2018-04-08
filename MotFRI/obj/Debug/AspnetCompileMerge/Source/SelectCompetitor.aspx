<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SelectCompetitor.aspx.cs" Inherits="MotFRI.SelectCompetitor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
 <script>
  (function(i,s,o,g,r,a,m){i['GoogleAnalyticsObject']=r;i[r]=i[r]||function(){
  (i[r].q=i[r].q||[]).push(arguments)},i[r].l=1*new Date();a=s.createElement(o),
  m=s.getElementsByTagName(o)[0];a.async=1;a.src=g;m.parentNode.insertBefore(a,m)
  })(window,document,'script','https://www.google-analytics.com/analytics.js','ga');

  ga('create', 'UA-82054248-1', 'auto');
  ga('send', 'pageview');

</script>

    <style type="text/css">
        .style1
        {
            font-family: "Segoe UI";
            font-size: large;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <br /><br />
    <asp:TextBox ID="CurrentUser" runat="server" Visible="false" ReadOnly="true"></asp:TextBox>
    <asp:TextBox ID="IncludeNatRegSearch" runat="server" Visible="false" ReadOnly="true"></asp:TextBox>
    <asp:TextBox ID="SearchOrInsertMode" runat="server" Visible="false" ReadOnly="true"></asp:TextBox><br />
    <asp:TextBox ID="NameForDS" runat="server" Visible="false" ReadOnly="true"></asp:TextBox>
    <asp:TextBox ID="KennitForDS" runat="server" Visible="false" ReadOnly="true"></asp:TextBox>
    <asp:TextBox ID="YearOfBirthForDS" runat="server" Visible="false" ReadOnly="true"></asp:TextBox>
    <asp:TextBox ID="SearchInNattRegForDS" runat="server" Visible="false" ReadOnly="true"></asp:TextBox>

    <asp:Label ID="Label1" runat="server" Text="Leit að keppanda í keppandaskrá FRÍ" Font-Size="X-Large"></asp:Label>
    <br /><br /><br />
    <asp:Label ID="Label4" runat="server" Text="Fylltu út nafnið og ef þú setur fæðingarár keppandans í fæðingarársreitinn, þá leitar kerfið eftir þeim keppendum" Font-Size="Large"></asp:Label><br />
    <asp:Label ID="Label5" runat="server" Text=" sem eru fæddir á því ári eða eru 5 árum eldri eða yngri" Font-Size="Large"></asp:Label><br />
    <asp:Label ID="Label10" runat="server" Text="Þú getur einnig fyllt út fyrstu stafina í kennitölunni og þá afmarkast leitin við keppendur sem hafa kennitölu sme byrjar á þeim stöfum." Font-Size="Large"></asp:Label>
    <br /><br />
    <asp:Table ID="Table1" runat="server" Font-Size="Large">
        <asp:TableRow>
            <asp:TableCell>
                <asp:Label ID="Label2" runat="server" Text="Nafn eða hluti úr nafni" Width="250px"></asp:Label>
            </asp:TableCell>
            <asp:TableCell>
                <asp:TextBox ID="NafnText" runat="server" Visible="true" ReadOnly="false" Width="250px"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>
          <asp:TableRow>
            <asp:TableCell>
                <asp:Label ID="Label7" runat="server" Text="Kennitala eða fyrstu 6"></asp:Label>
            </asp:TableCell>
            <asp:TableCell>
                <asp:TextBox ID="KennitalaText" runat="server" Visible="true" ReadOnly="false" Width="250px"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>
                <asp:Label ID="Label3" runat="server" Text="Fæðingarár (+/- 5ár)"></asp:Label>
            </asp:TableCell>
            <asp:TableCell>
                    <asp:TextBox ID="FaedArText" runat="server" Visible="true" ReadOnly="false" Width="250px"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>
                <asp:Label ID="Label6" runat="server" Text="Leita einnig í þjóðskrá"></asp:Label>
            </asp:TableCell>
            <asp:TableCell>
                <asp:CheckBox ID="SearchNatReg" runat="server" />
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    <br /><br />
    <asp:Button ID="SearchButton" runat="server" Text="Hefja leit" OnClick="SearchButton_Click" />
    <br />
    <asp:TextBox ID="CompetitionCode" runat="server" Visible="false" ReadOnly="false"></asp:TextBox>
    <br />
    <asp:TextBox ID="ErrorMsgBox" runat="server" ForeColor="#FF0066" Font-Size="X-Large" Width="1044px" ReadOnly="true"></asp:TextBox>
    <br />
    <br />
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
                          <HeaderStyle HorizontalAlign="Center" Font-Size="Large" />
                          <ItemStyle HorizontalAlign="Center" Font-Size="Large" />
                      </asp:TemplateField>


            <asp:BoundField DataField="LineType" HeaderText="LineType" SortExpression="LineType" Visible="False" />
            <asp:BoundField DataField="CompetitorCode" HeaderText="Keppandanr." 
                SortExpression="CompetitorCode" />
            <asp:BoundField DataField="Kennitala" HeaderText="Kennitala" SortExpression="Kennitala" />
            <asp:BoundField DataField="Nafn" HeaderText="Nafn" SortExpression="Nafn" />
            <asp:BoundField DataField="KynInt" HeaderText="KynInt" 
                SortExpression="KynInt" />
            <asp:BoundField DataField="Fæð.ár" HeaderText="Fæð.ár" 
                SortExpression="Fæð.ár" />
            <asp:BoundField DataField="Aldur" HeaderText="Aldur" SortExpression="Aldur" />
            <asp:BoundField DataField="Félag" HeaderText="Félag" SortExpression="Félag" />
            <asp:BoundField DataField="Kyn" HeaderText="Kyn" 
                SortExpression="Kyn" />
            <asp:BoundField DataField="Fjöldi afreka" HeaderText="Fj. afreka" SortExpression="Fjöldi afreka" />
                      <asp:BoundField DataField="Fæðingardagur" HeaderText="Fæðingardagur" ReadOnly="True" SortExpression="Fæðingardagur" />
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
            <asp:ControlParameter ControlID="KennitForDS" Name="KennitIn" 
                PropertyName="Text" Type="String" />
            <asp:ControlParameter ControlID="NameForDS" Name="NameIn" PropertyName="Text" Type="String" />
            <asp:ControlParameter ControlID="YearOfBirthForDS" Name="YearOfBirth" PropertyName="Text" Type="String" />
            <asp:ControlParameter ControlID="SearchInNattRegForDS" Name="SearchInNatReg" PropertyName="Text" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
    <br />
    <asp:Button ID="InsertSelected" runat="server" Text="Skrá valda keppendur" OnClick="InsertSelected_Click" Width="163px" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Label ID="Label8" runat="server" Text="Flytja valda keppendur yfir í félagið mitt: &nbsp;&nbsp;" Height="25px" Font-Size="Large" style="margin-top: 0px"></asp:Label>
    &nbsp;&nbsp;
    <asp:DropDownList ID="RegistrationAction" runat="server">
        <asp:ListItem Value="0">- Veldu -</asp:ListItem>
        <asp:ListItem Value="1">Nei</asp:ListItem>
        <asp:ListItem Value="2">Já</asp:ListItem>
    </asp:DropDownList> &nbsp;&nbsp;&nbsp;
    <asp:Label ID="Label9" runat="server" Text="Félagið mitt: " Font-Size="Large"></asp:Label>&nbsp;&nbsp;
    <asp:DropDownList ID="SelectedClub" runat="server" DataSourceID="ClubsForUser" DataTextField="NameOfClub" DataValueField="Club"></asp:DropDownList>
    <asp:SqlDataSource ID="ClubsForUser" runat="server" ConnectionString="<%$ ConnectionStrings:AthleticsConnectionString %>" SelectCommand="ClubsForUser" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:ControlParameter ControlID="CurrentUser" Name="UserLogin" PropertyName="Text" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
    <p>
        &nbsp;</p>
    <p>
    </p>
    </asp:Content>
