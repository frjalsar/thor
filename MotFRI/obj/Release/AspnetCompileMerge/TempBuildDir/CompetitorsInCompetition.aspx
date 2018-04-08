<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CompetitorsInCompetition.aspx.cs" Inherits="MotFRI.CompetitorsInCompetition" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style1
        {
            font-size: large;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <p class="style1">
        Velja Keppendur
        <br />
        <asp:TextBox ID="CompCode" runat="server" Visible="false"></asp:TextBox>
        <asp:TextBox ID="CurrentUserName" runat="server" Visible="false"></asp:TextBox>

        <asp:TextBox ID="SelectedClubOrUser" runat="server" Visible="false"></asp:TextBox>
        <asp:TextBox ID="SelectedGender" runat="server" Visible="false"></asp:TextBox>
        <asp:TextBox ID="SelectedPerformanceYears" runat="server" Visible="false"></asp:TextBox>
        <asp:TextBox ID="SelectedAgeFrom" runat="server" Visible="false"></asp:TextBox>
        <asp:TextBox ID="SelectedAgeTo" runat="server" Visible="false"></asp:TextBox>


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
                <asp:TableCell>Tegund íþróttafélaga</asp:TableCell>
                <asp:TableCell><asp:DropDownList ID="TypeOfClub" runat="server" Width="200px">
        <asp:ListItem Value="1">Íþróttafélög</asp:ListItem>
        <asp:ListItem Value="3">Skokkklúbbar</asp:ListItem>
        <asp:ListItem Value="4">Fyrirtæki</asp:ListItem>
        <asp:ListItem Value="5">Skólar</asp:ListItem>
        <asp:ListItem Value="6">Lönd</asp:ListItem>
        <asp:ListItem Value="7">Annað</asp:ListItem>
        <asp:ListItem Value="8">Ófélagsbundin(n)</asp:ListItem>
        <asp:ListItem Value="0">Öll félög</asp:ListItem>
        </asp:DropDownList></asp:TableCell>
            </asp:TableRow>
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
                <asp:TableCell><asp:DropDownList ID="SelectClub" runat="server" 
                    DataSourceID="SelectClubFromDB" DataTextField="Name" DataValueField="Félag"></asp:DropDownList></asp:TableCell>
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
        <br />
                        <asp:GridView ID="AccessToClubs" runat="server" DataSourceID="UserClubs" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None">
                    <AlternatingRowStyle BackColor="White"></AlternatingRowStyle>
                    <Columns>
                        <asp:BoundField DataField="Club" HeaderText="F&#233;lag" SortExpression="Club"></asp:BoundField>
                        <asp:BoundField DataField="NameOfClub" HeaderText="Heiti f&#233;lags" ReadOnly="True" SortExpression="NameOfClub"></asp:BoundField>
                        <asp:BoundField DataField="H&#233;ra&#240;ssamband" HeaderText="H&#233;ra&#240;ssamband" SortExpression="H&#233;ra&#240;ssamband"></asp:BoundField>
                    </Columns>
                    <EditRowStyle BackColor="#2461BF"></EditRowStyle>

                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White"></FooterStyle>

                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White"></HeaderStyle>

                    <PagerStyle HorizontalAlign="Center" BackColor="#2461BF" ForeColor="White"></PagerStyle>

                    <RowStyle BackColor="#EFF3FB"></RowStyle>

                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333"></SelectedRowStyle>

                    <SortedAscendingCellStyle BackColor="#F5F7FB"></SortedAscendingCellStyle>

                    <SortedAscendingHeaderStyle BackColor="#6D95E1"></SortedAscendingHeaderStyle>

                    <SortedDescendingCellStyle BackColor="#E9EBEF"></SortedDescendingCellStyle>

                    <SortedDescendingHeaderStyle BackColor="#4870BE"></SortedDescendingHeaderStyle>
                </asp:GridView>
                <asp:SqlDataSource runat="server" ID="UserClubs" ConnectionString='<%$ ConnectionStrings:AthleticsConnectionString %>' SelectCommand="ClubsForUser" SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="CurrentUserName" PropertyName="Text" Name="UserLogin" Type="String"></asp:ControlParameter>
                    </SelectParameters>
                </asp:SqlDataSource>

        <br />
        <asp:Button ID="ShowCompetitors" runat="server" Text="Sýna keppendur" OnClick="ShowCompetitors_Click" />
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
                      <asp:ControlParameter ControlID="SelectedClubOrUser" Name="Club" />
                      <asp:ControlParameter ControlID="SelectedGender" Name="GenderFilter" />
                      <asp:ControlParameter ControlID="SelectedPerFormanceYears" Name="NoOfYearsToSelect" />
                      <asp:ControlParameter ControlID="SelectedAgeFrom" Name="AgeFrom" />
                      <asp:ControlParameter ControlID="SelectedAgeTo" Name="AgeTo" />
                      <asp:ControlParameter ControlID="CompCode" Name="CompetitionCode"/>
                  </SelectParameters>
              </asp:SqlDataSource>

        &nbsp;<asp:SqlDataSource ID="SelectClubFromDB" runat="server" ConnectionString="<%$ ConnectionStrings:AthleticsConnectionString %>" SelectCommand="ReturnClubs" SelectCommandType="StoredProcedure">
            <SelectParameters>
                <asp:ControlParameter ControlID="TypeOfClub" DefaultValue="0" Name="ClubType" PropertyName="SelectedValue" Type="Int32" />
                <asp:ControlParameter ControlID="SelectGender" Name="GenderFilter" PropertyName="SelectedValue"/>
                <asp:ControlParameter ControlID="PerFormanceYears" Name="NoOfYearsToSelect" PropertyName="SelectedValue" />
                <asp:ControlParameter ControlID="AgeFrom" Name="AgeFrom" />
                <asp:ControlParameter ControlID="AgeTo" Name="AgeTo" />

            </SelectParameters>
        </asp:SqlDataSource><br />
    </p>
    <asp:Button ID="InsertCompetitors" runat="server" Text="Skrá valda keppendur" OnClick="InsertCompetitors_Click" />
  
</asp:Content>
