<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CompetitorsRegisterInMeet.aspx.cs" Inherits="MotFRI.CompetitorsInCompetition" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
<script>
  (function(i,s,o,g,r,a,m){i['GoogleAnalyticsObject']=r;i[r]=i[r]||function(){
  (i[r].q=i[r].q||[]).push(arguments)},i[r].l=1*new Date();a=s.createElement(o),
  m=s.getElementsByTagName(o)[0];a.async=1;a.src=g;m.parentNode.insertBefore(a,m)
  })(window,document,'script','https://www.google-analytics.com/analytics.js','ga');

  ga('create', 'UA-82054248-1', 'auto');
  ga('send', 'pageview');

</script>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <p class="style1">
        Velja Keppendur
        <br />
        <asp:TextBox ID="CompCode" runat="server" Visible="false"></asp:TextBox>
        <asp:TextBox ID="CurrentUserName" runat="server" Visible="false"></asp:TextBox>
        <asp:TextBox ID="SelectedClub" runat="server" Visible="false"></asp:TextBox>
        <asp:TextBox ID="SelectedGender" runat="server" Visible="false"></asp:TextBox>
        <asp:TextBox ID="SelectedPerformanceYears" runat="server" Visible="false"></asp:TextBox>
        <asp:TextBox ID="SelectedAgeFrom" runat="server" Visible="false"></asp:TextBox>
        <asp:TextBox ID="SelectedAgeTo" runat="server" Visible="false"></asp:TextBox>
        <asp:TextBox ID="TypeOfClub"  runat="server" Visible="false" Text="0"></asp:TextBox>

    </p>
    <p class="style1">

        <asp:Table ID="SelectionTable01" runat="server" BorderStyle="Solid" GridLines="Both">
            <asp:TableHeaderRow>
                <asp:TableHeaderCell>Veldu félag:</asp:TableHeaderCell>
                <asp:TableHeaderCell>Tegund afmörkunar</asp:TableHeaderCell>
                <asp:TableHeaderCell>Afmörkun</asp:TableHeaderCell>
            </asp:TableHeaderRow>
            
            <asp:TableRow>
                <asp:TableCell>&nbsp;</asp:TableCell>
                <asp:TableCell>Tímabil afreka:</asp:TableCell>
                <asp:TableCell><asp:DropDownList ID="PerformanceYears" runat="server" Width="200px">
            <asp:ListItem Value="1">Síðasta ár</asp:ListItem>
            <asp:ListItem Value="2">Undanfarin tvö ár</asp:ListItem>
            <asp:ListItem Value="3">Undanfarin 3 ár</asp:ListItem>
            <asp:ListItem Value="10">Undanfarin 10 ár</asp:ListItem>
            <asp:ListItem Value="0">Árið í ár</asp:ListItem>
            <asp:ListItem Value="200">Öll ár</asp:ListItem>
        </asp:DropDownList></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>&nbsp;</asp:TableCell>
                <asp:TableCell>Kyn</asp:TableCell>
                <asp:TableCell>
                     <asp:DropDownList ID="SelectGender" runat="server" Width="200px">
          <asp:ListItem Value="%">Bæði karlar og konur </asp:ListItem>
          <asp:ListItem Value="1">Karlar</asp:ListItem>
          <asp:ListItem Value="2">Konur</asp:ListItem>
        </asp:DropDownList>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>&nbsp;</asp:TableCell>
                <asp:TableCell>Aldur frá og til</asp:TableCell>
                <asp:TableCell HorizontalAlign="Center">
                  <asp:TextBox ID="AgeFrom" Text="0" runat="server" Width="80px"></asp:TextBox>
                  <asp:Label ID="Label2" runat="server" Text="-" Width="40px"></asp:Label>
                  <asp:TextBox ID="AgeTo" Text="999" runat="server" Width="80px"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <asp:Table ID="SelectionTable02" runat="server">                        
            <asp:TableRow>
                <asp:TableCell>&nbsp;</asp:TableCell>
                <asp:TableCell>Tímabil afreka:</asp:TableCell>
                <asp:TableCell><asp:DropDownList ID="PerformanceYears02" runat="server" Width="200px">
            <asp:ListItem Value="1">Síðasta ár</asp:ListItem>
            <asp:ListItem Value="2">Undanfarin tvö ár</asp:ListItem>
            <asp:ListItem Value="3">Undanfarin 3 ár</asp:ListItem>
            <asp:ListItem Value="10">Undanfarin 10 ár</asp:ListItem>
            <asp:ListItem Value="0">Árið í ár</asp:ListItem>
            <asp:ListItem Value="200">Öll ár</asp:ListItem>
        </asp:DropDownList>
         </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
          <asp:TableCell>&nbsp;</asp:TableCell>
          <asp:TableCell>Kyn</asp:TableCell>
          <asp:TableCell>
             <asp:DropDownList ID="SelectGender02" runat="server" Width="200px">
               <asp:ListItem Value="%">Bæði karlar og konur </asp:ListItem>
               <asp:ListItem Value="1">Karlar</asp:ListItem>
               <asp:ListItem Value="2">Konur</asp:ListItem>
             </asp:DropDownList>
          </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
          <asp:TableCell>&nbsp;</asp:TableCell>
          <asp:TableCell>Aldur frá og til</asp:TableCell>
          <asp:TableCell HorizontalAlign="Center">
               <asp:TextBox ID="AgeFrom02" Text="0" runat="server" Width="80px"></asp:TextBox>
                  <asp:Label ID="Dash02" runat="server" Text="-" Width="40px"></asp:Label>
                  <asp:TextBox ID="AgeTo02" Text="999" runat="server" Width="80px"></asp:TextBox>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <br />
        <asp:TextBox ID="Message" runat="server" Width="1200px" ReadOnly="true" ForeColor="Red" BorderStyle="None" Font-Size="Large"></asp:TextBox>
        <br />
        <asp:Label ID="Label1" runat="server" Text="Veldu félag: " Font-Size="Large"></asp:Label>&nbsp;&nbsp;
        <asp:DropDownList ID="SelUserClub" runat="server" DataSourceID="ClubsForUser" DataTextField="NameOfClub" DataValueField="Club"> </asp:DropDownList>
        <asp:SqlDataSource ID="ClubsForUser" runat="server" ConnectionString="<%$ ConnectionStrings:AthleticsConnectionString %>" SelectCommand="ClubsForUser3" SelectCommandType="StoredProcedure">
            <SelectParameters>
                <asp:ControlParameter ControlID="CurrentUserName" Name="UserLogin" PropertyName="Text" Type="String" />
                <asp:ControlParameter ControlID="CompCode" Name="CompCode" PropertyName="Text" Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>
        </p>
    <p class="style1">

        <br />

        <br />
        <asp:Button ID="ShowCompetitors" runat="server" Text="Sýna keppendur til að velja" OnClick="ShowCompetitors_Click" />
        &nbsp;&nbsp;&nbsp;
        <asp:Button ID="ClubsCompetitors" runat="server" Text="Keppendur með skráningum" OnClick="ClubsCompetitors_Click" />
        <br />

        Keppendur:</p>
          <p class="style1">
              <asp:GridView ID="SelectedCompetitors" runat="server" AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" 
                  DataSourceID="SelectCompetitorsfromClub" ForeColor="#333333" GridLines="None" OnRowDataBound="SelectedCompetitors_RowDataBound">
                  <AlternatingRowStyle BackColor="White" />
                  <Columns>
                      <asp:TemplateField HeaderText="Velja">
                       <ItemTemplate>
                         <asp:CheckBox ID="ValinChk" runat="server"  /> 
                       </ItemTemplate>
                          <HeaderStyle HorizontalAlign="Center" />
                          <ItemStyle HorizontalAlign="Center" />
                      </asp:TemplateField>
                      <asp:BoundField DataField="CompetitorCode" HeaderText="Kóti keppanda" SortExpression="CompetitorCode" >
                      <ItemStyle HorizontalAlign="Center" />
                      </asp:BoundField>
                      <asp:BoundField DataField="Kennitala" HeaderText="Kennitala" SortExpression="Kennitala" />
                      <asp:BoundField DataField="Nafn" HeaderText="Nafn" SortExpression="Nafn" />
                      <asp:BoundField DataField="Fæð.ár" HeaderText="Fæð.ár" SortExpression="Fæð.ár" />
                      <asp:BoundField DataField="Aldur" HeaderText="Aldur" SortExpression="Aldur" />
                      <asp:BoundField DataField="Félag" HeaderText="Félag" SortExpression="Félag" />
                      <asp:BoundField DataField="Kyn" HeaderText="Kyn" SortExpression="Kyn" />
                      <asp:BoundField DataField="Fjöldi afreka" HeaderText="Fjöldi afreka" SortExpression="Fjöldi afreka" >
                      <ItemStyle HorizontalAlign="Right" />
                      </asp:BoundField>
                      <asp:BoundField DataField="Fæðingardagur" HeaderText="Fæðingardagur" SortExpression="Fæðingardagur" >
                      <ItemStyle HorizontalAlign="Center" />
                      </asp:BoundField>
                      <asp:BoundField DataField="Land" HeaderText="Land" SortExpression="Land" />
                      <asp:BoundField DataField="Already Registered" HeaderText="Þegar skráður" SortExpression="Already Registered" >
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


              <asp:SqlDataSource ID="SelectCompetitorsfromClub" runat="server" 
                   ConnectionString="<%$ ConnectionStrings:AthleticsConnectionString %>" SelectCommand="SelCompetitorsFromClub"
                     SelectCommandType="StoredProcedure">
                  <SelectParameters>
                      <asp:ControlParameter ControlID="SelectedGender" Name="GenderFilter" />
                      <asp:ControlParameter ControlID="SelectedPerFormanceYears" Name="NoOfYearsToSelect" />
                      <asp:ControlParameter ControlID="SelectedAgeFrom" Name="AgeFrom" />
                      <asp:ControlParameter ControlID="SelectedAgeTo" Name="AgeTo" />
                      <asp:ControlParameter ControlID="CompCode" Name="CompetitionCode"/>
                      <asp:ControlParameter ControlID="SelectedClub" Name="SelectedClub" PropertyName="Text" Type="String" />
                  </SelectParameters>
              </asp:SqlDataSource>

        &nbsp;<asp:SqlDataSource ID="SelectClubFromDB" runat="server" ConnectionString="<%$ ConnectionStrings:AthleticsConnectionString %>" SelectCommand="ReturnClubs" SelectCommandType="StoredProcedure">
            <SelectParameters>
                <asp:ControlParameter ControlID="TypeOfClub" Name="ClubType"  />
                <asp:ControlParameter ControlID="SelectGender" Name="GenderFilter" PropertyName="SelectedValue"/>
                <asp:ControlParameter ControlID="PerFormanceYears" Name="NoOfYearsToSelect" PropertyName="SelectedValue" />
                <asp:ControlParameter ControlID="AgeFrom" Name="AgeFrom" />
                <asp:ControlParameter ControlID="AgeTo" Name="AgeTo" />

            </SelectParameters>
        </asp:SqlDataSource><br />
    </p>
    <asp:Button ID="InsertCompetitors" runat="server" Text="Skrá valda keppendur" OnClick="InsertCompetitors_Click" />
  
</asp:Content>
