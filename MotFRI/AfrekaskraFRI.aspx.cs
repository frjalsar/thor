using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MotFRI
{
    public partial class AfrekaskraFRI : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

  //                  <br />
  //      <br />
  //                         <asp:Label ID="Label1" runat="server" Text="Fyrirspurn í Afrekaskrá FRÍ" Font-Size="X-Large"></asp:Label>
  //  </p> 
  //  <br />
  //  <asp:TextBox ID="SelectedEvent" runat="server" Visible="true" ReadOnly="true"></asp:TextBox>
  //  <asp:TextBox ID="SelectedGender" runat="server" Visible="true" ReadOnly="true"></asp:TextBox>
  //  <asp:TextBox ID="SelectedGroup" runat="server" Visible="true" ReadOnly="true"></asp:TextBox>
  //  <asp:TextBox ID="SelectedOutdoorsIndoors" runat="server" Visible="true" ReadOnly="true"></asp:TextBox>
  //  <asp:TextBox ID="SelectedDateFrom" runat="server" Visible="true" ReadOnly="true"></asp:TextBox>
  //  <asp:TextBox ID="SelectedDateTo" runat="server" Visible="true" ReadOnly="true"></asp:TextBox>


  //  <asp:Table ID="Selection" runat="server">
  //      <asp:TableHeaderRow>
  //          <asp:TableHeaderCell>Afmarkanir</asp:TableHeaderCell>
  //          <asp:TableHeaderCell>&nbsp</asp:TableHeaderCell>
  //      </asp:TableHeaderRow>
  //      <asp:TableRow>
  //          <asp:TableCell>
  //              Utanhúss eða innanhúss&nbsp;&nbsp;&nbsp;
  //          </asp:TableCell>
  //          <asp:TableCell>
  //                  <asp:DropDownList ID="OutdoorsIndoors" runat="server" AutoPostBack="true">
  //                    <asp:ListItem Value="0">Utanhúss</asp:ListItem>
  //                    <asp:ListItem Value="1">Innahúss</asp:ListItem>
  //                  </asp:DropDownList>
  //          </asp:TableCell>
  //      </asp:TableRow>
  //      <asp:TableRow>    
  //         <asp:TableCell>
  //              Kyn
  //          </asp:TableCell>
  //          <asp:TableCell>
  //                  <asp:DropDownList ID="Gender" runat="server" AutoPostBack="true">
  //                    <asp:ListItem Value="1">Karlar</asp:ListItem>
  //                    <asp:ListItem Value="2">Konur</asp:ListItem>                    
  //                  </asp:DropDownList>
  //          </asp:TableCell>
  //       </asp:TableRow>
  //      <asp:TableRow>    
  //         <asp:TableCell>
  //              Aldur frá og til
  //          </asp:TableCell>
  //          <asp:TableCell>
  //              <asp:TextBox ID="AgeFrom" runat="server" Width="40px" AutoPostBack="true"></asp:TextBox>
  //              &nbsp;&nbsp;
  //              <asp:TextBox ID="AgeTo" runat="server" Width="40px" AutoPostBack="true"></asp:TextBox>
  //          </asp:TableCell>
  //       </asp:TableRow>
  //      <asp:TableRow>
  //          <asp:TableCell>
  //              Grein
  //          </asp:TableCell>
  //          <asp:TableCell>
  //            <asp:DropDownList ID="EventCode" runat="server" DataSourceID="EventsForAgeGr" DataTextField="Heiti" DataValueField="Samsettur lykill greinar" AutoPostBack="true">
  //                <asp:ListItem Value="%"> - Veldu grein - </asp:ListItem>
  //            </asp:DropDownList>
  //          </asp:TableCell>
  //      </asp:TableRow>
  //      <asp:TableRow>
  //          <asp:TableCell>
  //             Dagsetning frá og til
  //          </asp:TableCell>
  //          <asp:TableCell>
  //              <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" 
  //                                      CombineScripts="True"></asp:ToolkitScriptManager>
  //  <asp:TextBox ID="DateFrom" runat="server" AutoPostBack="True" Width="150px"></asp:TextBox><asp:CalendarExtender ID="CalendarExtender1" runat="server" 
  //                                      DaysModeTitleFormat="dd.MM.yyyy" Enabled="True" Format="dd.MM.yyyy" 
  //                                      TargetControlID="DateFrom" TodaysDateFormat="dd.MM.yyyy"></asp:CalendarExtender>
  //  <asp:Label ID="Label2" runat="server" Text="  -  "></asp:Label>
  //<asp:TextBox ID="DateTo" runat="server" AutoPostBack="True" Width="150px"></asp:TextBox><asp:CalendarExtender ID="CalendarExtender2" runat="server" 
  //                                      DaysModeTitleFormat="dd.MM.yyyy" Enabled="True" Format="dd.MM.yyyy" 
  //                                      TargetControlID="DateTo" TodaysDateFormat="dd.MM.yyyy"></asp:CalendarExtender>
  //          </asp:TableCell>
  //      </asp:TableRow>
  //      <asp:TableRow>
  //          <asp:TableCell>
  //              Vindur
  //          </asp:TableCell>
  //          <asp:TableCell>
  //                 <asp:DropDownList ID="Wind" runat="server" AutoPostBack="true">
  //                    <asp:ListItem Value="0">Löglegur vindur</asp:ListItem>
  //                    <asp:ListItem Value="1">Bæði löglegur og of mikill</asp:ListItem>                        
  //                  </asp:DropDownList>
  //          </asp:TableCell>
  //      </asp:TableRow>
  //      <asp:TableRow>
  //          <asp:TableCell>
  //              Íslendingar
  //          </asp:TableCell>
  //          <asp:TableCell>
  //                  <asp:DropDownList ID="NativeOrForeigners" runat="server" AutoPostBack="true">
  //                    <asp:ListItem Value="0">Aðeins íslendingar</asp:ListItem>
  //                      <asp:ListItem Value="%">Bæði íslendinar og erlendir ríkisborgarar</asp:ListItem>
  //                    <asp:ListItem Value="1">Aðeins erlendir ríkisborgarar</asp:ListItem>                        
  //                  </asp:DropDownList>
  //          </asp:TableCell>
  //      </asp:TableRow>
  //      <asp:TableRow>
  //          <asp:TableCell>
  //              Afmörkun við félag
  //          </asp:TableCell>
  //          <asp:TableCell>
  //                <asp:DropDownList ID="SelectClub" runat="server" DataSourceID="SelectedClubsDataSource" DataTextField="ClubName" DataValueField="ClubCode" AutoPostBack="true"> 
  //  </asp:DropDownList>
  //          </asp:TableCell>
  //      </asp:TableRow>
  //      <asp:TableRow>
  //          <asp:TableCell>
  //              Fjöldi lína
  //          </asp:TableCell>
  //          <asp:TableCell>
  //              <asp:TextBox ID="NoOfLines" runat="server" AutoPostBack="true"></asp:TextBox>
  //          </asp:TableCell>
  //      </asp:TableRow>

  //  </asp:Table>

  //  <asp:Button ID="Button1" runat="server" Text="Birta" OnClick="Button1_Click" />
  //  <br />
  //  <asp:GridView ID="TopListGridView" runat="server" AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" DataSourceID="TopListThor" ForeColor="#333333" GridLines="None" DataKeyNames="Row">
  //      <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
  //      <Columns>
  //          <asp:BoundField DataField="Row" HeaderText="Row" ReadOnly="True" SortExpression="Row" />
  //          <asp:BoundField DataField="Árangur" HeaderText="Árangur" SortExpression="Árangur" />
  //          <asp:BoundField DataField="Vindur" HeaderText="Vindur" SortExpression="Vindur" />
  //          <asp:BoundField DataField="Nafn" HeaderText="Nafn" SortExpression="Nafn" />
  //          <asp:BoundField DataField="Dagsetning" HeaderText="Dagsetning" SortExpression="Dagsetning" />
  //          <asp:BoundField DataField="Staður" HeaderText="Staður" SortExpression="Staður" />
  //          <asp:BoundField DataField="Heiti móts" HeaderText="Heiti móts" SortExpression="Heiti móts" />
  //          <asp:BoundField DataField="Aldur keppanda" HeaderText="Aldur keppanda" SortExpression="Aldur keppanda" />
  //          <asp:BoundField DataField="Keppandanúmer" HeaderText="Keppandanúmer" SortExpression="Keppandanúmer" />
  //          <asp:BoundField DataField="Raðsvæði" HeaderText="Raðsvæði" SortExpression="Raðsvæði" />
  //          <asp:BoundField DataField="Erlendur ríkisborgari" HeaderText="Erlendur ríkisborgari" SortExpression="Erlendur ríkisborgari" />
  //          <asp:BoundField DataField="Fæðingarár" HeaderText="Fæðingarár" SortExpression="Fæðingarár" />
  //      </Columns>
  //      <EditRowStyle BackColor="#999999" />
  //      <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
  //      <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
  //      <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
  //      <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
  //      <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
  //      <SortedAscendingCellStyle BackColor="#E9E7E2" />
  //      <SortedAscendingHeaderStyle BackColor="#506C8C" />
  //      <SortedDescendingCellStyle BackColor="#FFFDF8" />
  //      <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
  //  </asp:GridView>
  
  //  <asp:SqlDataSource ID="TopListThor" runat="server" ConnectionString="<%$ ConnectionStrings:AthleticsConnectionString %>" SelectCommand="TopList_Thor" SelectCommandType="StoredProcedure">
  //      <SelectParameters>
  //          <asp:ControlParameter ControlID="SelectedEvent" Name="EventCode" PropertyName="Text" Type="String" />
  //          <asp:ControlParameter ControlID="Gender" Name="Gender" PropertyName="Text" Type="String" />
  //          <asp:ControlParameter ControlID="SelectedGroup" Name="GroupCode" PropertyName="Text" Type="String" />
  //          <asp:ControlParameter ControlID="SelectedOutdoorsIndoors" Name="OutdoorsOrIndoors" PropertyName="Text" Type="String" />
  //          <asp:ControlParameter ControlID="SelectedDateFrom" DbType="Date" Name="DateFrom" PropertyName="Text" />
  //          <asp:ControlParameter ControlID="SelectedDateTo" DbType="Date" Name="DateTo" PropertyName="Text" />
  //          <asp:ControlParameter ControlID="AgeFrom" Name="AgeFrom" PropertyName="Text" Type="Int32" />
  //          <asp:ControlParameter ControlID="AgeTo" Name="AgeTo" PropertyName="Text" Type="Int32" />
  //          <asp:ControlParameter ControlID="Wind" Name="WindFilter" PropertyName="Text" Type="String" />
  //          <asp:ControlParameter ControlID="NativeOrForeigners" Name="Foreigner" PropertyName="Text" Type="String" />
  //          <asp:ControlParameter ControlID="NoOfLines" Name="NoOfLines" PropertyName="Text" Type="Int32" />
  //          <asp:ControlParameter ControlID="SelectClub" Name="ClubFilter" PropertyName="Text" Type="String" />
  //      </SelectParameters>
  //  </asp:SqlDataSource>
   
    
  

  //  <asp:SqlDataSource ID="EventsForAgeGr" runat="server" ConnectionString="<%$ ConnectionStrings:AthleticsConnectionString %>" SelectCommand="ReturnEventsForAge" SelectCommandType="StoredProcedure">
  //      <SelectParameters>
  //          <asp:ControlParameter ControlID="Gender" Name="Gender" PropertyName="Text" Type="Int32" />
  //          <asp:ControlParameter ControlID="AgeFrom" Name="AgeFrom" PropertyName="Text" Type="Int32" />
  //          <asp:ControlParameter ControlID="AgeTo" Name="AgeTo" PropertyName="Text" Type="Int32" />
  //          <asp:ControlParameter ControlID="OutdoorsIndoors" Name="OutdInd" PropertyName="Text" Type="Int32" />
  //      </SelectParameters>
  //  </asp:SqlDataSource>

               

  //  <asp:SqlDataSource ID="SelectedClubsDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:AthleticsConnectionString %>" SelectCommand="FelogIAfrekaskra" SelectCommandType="StoredProcedure"></asp:SqlDataSource>





            
  //          if (!IsPostBack)
  //          {
  //              AgeFrom.Text = "0";
  //              AgeTo.Text = "999";
  //              DateFrom.Text = "1.1." + DateTime.Today.Year.ToString();
  //              DateTo.Text = "31.12." + DateTime.Today.Year.ToString();
  //              NoOfLines.Text = "100";
  //          }
  //          else
  //          {
  //              DateTime WrkDate;
  //              string[] EventArr = EventCode.SelectedValue.ToString().Split(';');
  //              SelectedEvent.Text = EventArr[0];
  //              SelectedGender.Text = EventArr[1];
  //              SelectedGroup.Text = EventArr[2];
  //              SelectedOutdoorsIndoors.Text = EventArr[3];
  //              WrkDate = Convert.ToDateTime(DateFrom.Text);
  //              SelectedDateFrom.Text = WrkDate.Year.ToString() + "." + WrkDate.Month.ToString() + "." + WrkDate.Day.ToString();
  //              WrkDate = Convert.ToDateTime(DateTo.Text);
  //              SelectedDateTo.Text = WrkDate.Year.ToString() + "." + WrkDate.Month.ToString() + "." + WrkDate.Day.ToString();
                
  //          }


  //          // <asp:ControlParameter ControlID="SelectedEvent" Name="EventCode" PropertyName="Text" Type="String" />
  //          //<asp:ControlParameter ControlID="SelectedGender" Name="Gender" PropertyName="Text" Type="String" />
  //          //<asp:ControlParameter ControlID="SelectedGroup" Name="GroupCode" PropertyName="Text" Type="String" />
  //          //<asp:ControlParameter ControlID="SelectedOutdoorsIndoors" Name="OutdoorsOrIndoors" PropertyName="Text" Type="String" />
  //          //<asp:ControlParameter ControlID="DateFrom" DbType="Date" Name="DateFrom" PropertyName="Text" />
  //          //<asp:ControlParameter ControlID="DateTo" DbType="Date" Name="DateTo" PropertyName="Text" />
  //          //<asp:ControlParameter ControlID="AgeFrom" Name="AgeFrom" PropertyName="Text" Type="Int32" />
  //          //<asp:ControlParameter ControlID="AgeTo" Name="AgeTo" PropertyName="Text" Type="Int32" />
  //          //<asp:ControlParameter ControlID="Wind" Name="WindFilter" PropertyName="Text" Type="String" />
  //          //<asp:ControlParameter ControlID="NativeOrForeigners" Name="Foreigner" PropertyName="Text" Type="String" />
  //          //<asp:ControlParameter ControlID="NoOfLines" Name="NoOfLines" PropertyName="Text" Type="Int32" />
  //          //<asp:ControlParameter ControlID="SelectClub" Name="ClubFilter" PropertyName="Text" Type="String" />

  //          //string DebugStr = EventCode.Text + ";" + Gender.SelectedValue.ToString() + ";" + Gender.SelectedValue.ToString() + ";" +
  //          //      SelectedOutdoorsIndoors.Text + ";" + DateFrom.Text + ";" + DateTo.Text + ";" + AgeFrom.Text + ";" + AgeTo.Text + ";" +
  //          //      Wind.SelectedValue.ToString() + ";" + NativeOrForeigners.SelectedValue.ToString() + ";" + NoOfLines.Text + ";" + SelectClub.SelectedValue.ToString();

  //          ListItem SelEvent = new ListItem();
  //          SelEvent.Text = "Veldu keppnisgrein";
  //          SelEvent.Value = "xx";
  //          EventCode.Items.Add(SelEvent);
           
  //      }

  //      protected void Button1_Click(object sender, EventArgs e)
  //      {
  //          TopListGridView.DataBind();
          }
    }
}