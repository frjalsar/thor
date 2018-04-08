<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NyttMot.aspx.cs" Inherits="MotFRI.NyttMot" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" 
        Font-Size="Larger">
        <asp:TabPanel runat="server" HeaderText="Uppsetning móts" ID="TabPanel1" Font-Size="Larger">
        <ContentTemplate><p style="font-size: x-large; width: 352px;">Uppsetning móts</p><br /><asp:TextBox ID="GamliVollur" runat="server" ReadOnly="True" Visible="False"></asp:TextBox><asp:TextBox ID="GamlaDags" runat="server" ReadOnly="True" Visible="False"></asp:TextBox><br />
            <Table>
                <caption></td><tr><td>Númer</td>
                    <td>&nbsp;</td><td><asp:TextBox ID="CompetitionCode" runat="server" Enabled="False" 
                                ReadOnly="True" Width="258px"></asp:TextBox></td>
                    <tr><td>Heiti</td><td>*</td><td><asp:TextBox ID="HeitiMots" runat="server" 
                                    onkeypress="javascript: return limitCharsLength(this,50);" Width="258px"></asp:TextBox></td>
                        <tr><td>Enskt heiti</td><td>&nbsp;</td><td><asp:TextBox ID="EnsktHeiti" runat="server" 
                                        onkeypress="javascript: return limitCharsLength(this,50);" Width="258px"></asp:TextBox></td></tr>
                        <tr><td>Dagsetning (DD.MM.ÁÁÁÁ)&nbsp;&nbsp;&nbsp;&nbsp;</td><td>*</td><td><asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" 
                                        CombineScripts="True"></asp:ToolkitScriptManager><asp:TextBox ID="Dagsetning" runat="server" AutoPostBack="True" Width="150px"></asp:TextBox><asp:CalendarExtender ID="CalendarExtender1" runat="server" 
                                        DaysModeTitleFormat="dd.MM.yyyy" Enabled="True" Format="dd.MM.yyyy" 
                                        TargetControlID="Dagsetning" TodaysDateFormat="dd.MM.yyyy"></asp:CalendarExtender></td>
                            <tr><td>Keppnisvöllur</td><td>*</td><td><asp:DropDownList ID="Vellir" runat="server" AppendDataBoundItems="True" 
                                            AutoPostBack="True" DataSourceID="SqlDataSource1" DataTextField="Heiti" 
                                            DataValueField="Heiti" Width="250px"><asp:ListItem Selected="True" Value="-1">---Veldu keppnisvöll---</asp:ListItem></asp:DropDownList><asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                                            ConnectionString="<%$ ConnectionStrings:AthleticsConnectionString %>" 
                                            SelectCommand="SELECT [Heiti] FROM [Athl$Venues]"></asp:SqlDataSource></td><td>&nbsp;</td><td>Staður</td><td><asp:TextBox ID="Stadur" runat="server" Enabled="False" ReadOnly="True" 
                                            Width="200px"></asp:TextBox></td></tr><tr><td></td><td>&nbsp;</td><td></td><td></td>
                                                <td>Úti/Inni</td><td><asp:TextBox ID="UtiEdaInni" runat="server" Enabled="False" ReadOnly="True" 
                                            Width="200px"></asp:TextBox></td></tr>
                            <tr><td>Mótshaldari </td><td>*</td><td><asp:TextBox ID="Motshaldari" runat="server" 
                                            onkeypress="javascript: return limitCharsLength(this,10);" Width="200px"></asp:TextBox></td></tr><tr><td>Yfirdómari</td><td>*</td>
                                                <td><asp:TextBox ID="Yfirdomari" runat="server" 
                                            onkeypress="javascript: return limitCharsLength(this,30);" Width="200px"></asp:TextBox></td></tr>
                            <tr><td>Skrán.gj. pr. grein</td><td>&nbsp;</td>
                                <td><asp:TextBox ID="SkranGjaldPrGrein" runat="server" 
                                            onkeypress="javascript : return isNumberKey(this)" Width="200px" HorizontalAlign="right" ></asp:TextBox></td></tr><tr>
                                                <td>Skrán.gj. pr. boðhl. </td>
                                                <td>&nbsp;</td>
                                                <td><asp:TextBox ID="SkranGjPrBodhl" runat="server" 
                                            onkeypress="javascript : return isNumberKey(this)" Width="200px" HorizontalAlign="right"></asp:TextBox></td></tr>
                            <tr><td>Skrán.gj. pr. grein 17 ára og yngri</td><td>&nbsp;</td>
                                <td><asp:TextBox ID="SkranGjPrGreinU18" runat="server" 
                                            onkeypress="javascript : return isNumberKey(this)" Width="200px" HorizontalAlign="right"></asp:TextBox></td></tr><tr>
                                                <td>Skrán.gj. pr. boðhl. 17 ára og yngri</td>
                                                <td>&nbsp;</td>
                                                <td><asp:TextBox ID="SkranGjPrBodhlU18" runat="server" 
                                            onkeypress="javascript : return isNumberKey(this)" Width="200px" HorizontalAlign="right"></asp:TextBox></td></tr>

                            <tr><td>Reikna ungl.stig </td><td>&nbsp;</td><td><asp:DropDownList ID="ReiknaUnglStig" runat="server" Width="100px">
                                  <asp:ListItem Value="0">Nei</asp:ListItem><asp:ListItem Value="1">Já</asp:ListItem>
                                </asp:DropDownList></td></tr>
                            <tr><td>Reikna IAAF stig </td><td>&nbsp;</td><td><asp:DropDownList ID="ReiknaIAAFStig" runat="server" Width="100px">
                                  <asp:ListItem Value="0">Nei</asp:ListItem><asp:ListItem Value="1">Já</asp:ListItem>
                                </asp:DropDownList></td></tr>
                            <tr>
                                <td>Teg. stigakeppni </td><td>&nbsp;</td><td><asp:DropDownList ID="TegundStigakeppni" runat="server" Width="200px"><asp:ListItem Value="0">Engin</asp:ListItem><asp:ListItem Value="1">Bikarkeppni</asp:ListItem><asp:ListItem Value="2">Aldursflokkar FRÍ</asp:ListItem></asp:DropDownList></td></tr></tr></tr></tr>

                </caption></Table><br /><asp:TextBox ID="Message" runat="server" Font-Size="Larger" ForeColor="Red" 
        Enabled="False" BorderStyle="None" Width="100%"></asp:TextBox>
            <asp:Button ID="SetupEvents" runat="server" Text="Uppsetning keppnisgreina" 
            onclick="SetupEvents_Click" />
            <br />
            <asp:Button ID="Vista" runat="server" onclick="VistaNyttMot_Click" 
            Text="Vista" />

        </ContentTemplate> 
        </asp:TabPanel>
        <asp:TabPanel ID="KeppendurPanel" runat="server" HeaderText="Keppendur" Font-Size="Larger">

        <ContentTemplate><p style="font-size: x-large; width: 352px;">Keppendur í móti <asp:TextBox ID="CompCode2" runat="server" Enabled="False" Visible="False"></asp:TextBox>

                         </p><asp:Button ID="NewCompetitorButton" runat="server" onclick="Button1_Click" 
    Text="Nýr keppandi" Width="180px" /> <br />
            <asp:Button ID="YourCompetitors" runat="server" Text="Þínir keppendur" OnClick="YourCompetitors_Click" /> <br />
            <asp:TextBox ID="MessageKeppendur" runat="server" Font-Size="Larger" ForeColor="Red" 
         BorderStyle="None" Width="100%" ReadOnly="True"></asp:TextBox>
            <asp:Button ID="AddNewCompetitorsBtn" runat="server" OnClick="Button2_Click" Text="Bæta við nýjum keppendum félags" Width="305px" />
            <br />
            <asp:Table ID="NyrKeppandiTbl" runat="server">
                <asp:TableRow runat="server"><asp:TableCell runat="server"><asp:Label ID="KennitLbl" runat="server" Text="Kennitala" Width='140px'></asp:Label>
</asp:TableCell><asp:TableCell runat="server"><asp:Label ID="NafnLbl" runat="server" Text="Nafn" Width='250px'></asp:Label>
</asp:TableCell><asp:TableCell runat="server"><asp:Label ID="FaedArLbl" runat="server" Text="Fæð.ár" Width='100px'></asp:Label>
</asp:TableCell><asp:TableCell runat="server"><asp:Label ID="FelagLbl" runat="server" Text="Félag" Width='140px'></asp:Label>
</asp:TableCell></asp:TableRow><asp:TableRow runat="server"><asp:TableCell runat="server"><asp:TextBox ID="KennitText" runat="server" Width='140px'></asp:TextBox>
</asp:TableCell><asp:TableCell runat="server"><asp:TextBox ID="NafnText" runat="server" Width='250px'></asp:TextBox>
</asp:TableCell><asp:TableCell runat="server"><asp:TextBox ID="FaedArText" runat="server" Width='100px'></asp:TextBox>
</asp:TableCell><asp:TableCell runat="server"><asp:TextBox ID="FelagText" runat="server" Width='140px'></asp:TextBox>
</asp:TableCell><asp:TableCell runat="server"><asp:Button ID="SearchForCompetitorsBtn" runat="server" Text="Leita" OnClick="SearchForCompetitorsBtn_Click" />
</asp:TableCell></asp:TableRow></asp:Table>
            <asp:TextBox ID="SidastaKt" runat="server" ReadOnly="True" Visible="False"></asp:TextBox>
            <asp:TextBox ID="SidastaNafn" runat="server" ReadOnly="True" Visible="False"></asp:TextBox>
            <asp:TextBox ID="SidastaFaedAr" runat="server" ReadOnly="True" Visible="False"></asp:TextBox>
        <asp:GridView 
                ID="GridView1" runat="server" AutoGenerateColumns="False" 
            DataKeyNames="mot,rasnumer" DataSourceID="CompetitorsInComp" 
                AllowSorting="True" CellPadding="4" ForeColor="#333333" GridLines="None" 
                onselectedindexchanged="GridView1_SelectedIndexChanged" 
                onrowcommand="GridView1_RowCommand"><AlternatingRowStyle BackColor="White" /><Columns><asp:TemplateField><ItemTemplate><asp:Button ID="RegEvent" runat="server" 
                  CommandName="RegisterEvents" 
                  CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                  Text="Skrá í greinar" /></ItemTemplate></asp:TemplateField><asp:TemplateField><ItemTemplate><asp:Button ID="RegisterInEventButton" runat="server" 
             CommandName="RegisterInEvents" 
             CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
             Text="Eyða" /></ItemTemplate></asp:TemplateField><asp:BoundField DataField="rasnumer" HeaderText="Rásnr." ReadOnly="True" 
                    SortExpression="rasnumer" /><asp:BoundField DataField="Nafn" HeaderText="Nafn" SortExpression="nafn" /><asp:BoundField DataField="felag" HeaderText="Félag" SortExpression="felag" /><asp:BoundField DataField="kyn" HeaderText="Kyn" SortExpression="kyn" /><asp:BoundField DataField="kennitala" HeaderText="Kennitala" 
                    SortExpression="kennitala" /><asp:BoundField DataField="faedingardagur" HeaderText="Fæð.dagur" 
                    SortExpression="faedingardagur" /><asp:BoundField DataField="faedingarar" HeaderText="Fæð.ár" 
                    SortExpression="faedingarar" /><asp:BoundField DataField="aldurkeppanda" HeaderText="Aldur" 
                    SortExpression="aldurkeppanda" /><asp:BoundField DataField="land" HeaderText="Land" SortExpression="land" /><asp:BoundField DataField="fyrirlidi" HeaderText="Fyrirl." 
                    SortExpression="fyrirlidi" /><asp:BoundField DataField="Fj" HeaderText="Fj.skrán" SortExpression="Fj" /></Columns>
    <EditRowStyle BackColor="#2461BF" /><FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" /><HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" /><PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" /><RowStyle BackColor="#EFF3FB" /><SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" /><SortedAscendingCellStyle BackColor="#F5F7FB" /><SortedAscendingHeaderStyle BackColor="#6D95E1" /><SortedDescendingCellStyle BackColor="#E9EBEF" /><SortedDescendingHeaderStyle BackColor="#4870BE" /></asp:GridView><asp:SqlDataSource ID="CompetitorsInComp" runat="server" 
            ConnectionString="<%$ ConnectionStrings:AthleticsConnectionString %>" 
            SelectCommand="CompetitorsInComp" SelectCommandType="StoredProcedure"><SelectParameters><asp:ControlParameter ControlID="CompCode2" Name="CompCode" 
                    PropertyName="Text" Type="String" /></SelectParameters></asp:SqlDataSource><br /></ContentTemplate>
        </asp:TabPanel>
       
        <asp:TabPanel ID="Keppnisgr2" runat="server" HeaderText="Keppnisgreinar" Font-Size="Larger">

        <ContentTemplate><asp:Label ID="Label6" runat="server" Text="CompCode4" Visible="False"></asp:Label><asp:TextBox ID="CompCode4" runat="server" Visible="False" OnTextChanged="CompCode4_TextChanged"></asp:TextBox><asp:Label ID="Label2" runat="server" Text="CompCode5:" Visible="False"></asp:Label><asp:TextBox ID="CompCode5" runat="server" ReadOnly="True" style="margin-top: 11px" 
                Visible="False"></asp:TextBox><asp:Label ID="Label3" runat="server" Text="EventNo:" Visible="False"></asp:Label><asp:TextBox ID="EvenNo" runat="server" ReadOnly="True" Visible="False"></asp:TextBox><asp:Label ID="Label4" runat="server" Text="SelGender:" Visible="False"></asp:Label><asp:TextBox ID="SelGender" runat="server" ReadOnly="True" Visible="False"></asp:TextBox><asp:Label ID="Label5" runat="server" Text="EventCode" Visible="False"></asp:Label><asp:TextBox ID="EventCode" runat="server" ReadOnly="True" Visible="False"></asp:TextBox><p style="font-size: x-large; width: 352px;">
                    <asp:TextBox ID="CompetitionEventsHdr" runat="server" 
                ReadOnly="True" Font-Size="Larger" Font-Italic="True" Width="975px" BorderStyle="None"></asp:TextBox><asp:TextBox ID="MessageCompEvents2" runat="server" ForeColor="Red" 
                    ReadOnly="True" Width="280%" BorderStyle="None"></asp:TextBox><asp:SqlDataSource ID="EventsInCompetition" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:AthleticsConnectionString %>" 
                    SelectCommand="EventsInCompetition2" SelectCommandType="StoredProcedure"><SelectParameters><asp:ControlParameter ControlID="CompCode4" Name="CompCode" PropertyName="Text" 
                            Type="String" /><asp:ControlParameter ControlID="EventStatusFilter" Name="EventStatusFilter" 
                            PropertyName="SelectedValue" Type="String" /><asp:ControlParameter ControlID="EventGenderFilter" Name="GenderFilter" 
                            PropertyName="SelectedValue" Type="String" />
                        <asp:ControlParameter ControlID="EventDateFilterDropDown" Name="EventDateFilter" PropertyName="SelectedValue" Type="String" />
                    </SelectParameters></asp:SqlDataSource></p>            
            
            <asp:Label ID="Label7" runat="server" Text="Veldu stöðu keppni"></asp:Label>
            <asp:DropDownList ID="EventStatusFilter" runat="server" AutoPostBack="True">
                <asp:ListItem Value="[0-2]">Allt</asp:ListItem>
                <asp:ListItem Value="%0%">Ekki hafin</asp:ListItem>
                <asp:ListItem Value="%1%">Stendur yfir</asp:ListItem>
                <asp:ListItem Value="%2%">Lokið</asp:ListItem>
                <asp:ListItem Value="[0-1]">Ekki hafin og stendur yfir</asp:ListItem>
            </asp:DropDownList>&nbsp;&nbsp; 
            <asp:Label ID="Label8" runat="server" Text="Veldu kyn: "></asp:Label>
            <asp:DropDownList ID="EventGenderFilter" runat="server" AutoPostBack="True">
                <asp:ListItem Value="%">Bæði kyn</asp:ListItem>
                <asp:ListItem Value="1">Karlar</asp:ListItem>
                <asp:ListItem Value="2">Konur</asp:ListItem>
            </asp:DropDownList>&nbsp;&nbsp;&nbsp;
            <asp:Label ID="EventDateFilterLabel" runat="server" Text="Veldu dagsetningu: "></asp:Label>
            <asp:DropDownList ID="EventDateFilterDropDown" runat="server" AutoPostBack="True"></asp:DropDownList>
            <br />
           
            <asp:Button ID="Points" runat="server" Height="43px" Text="Stigastaðan" Visible="False" Width="81px" OnClick="Points_Click" />
            <br /><br />
         

            
            <asp:GridView ID="GridView3" runat="server" 
                    DataSourceID="EventsInCompetition" AllowSorting="True" 
                    AutoGenerateColumns="False" CellPadding="4" DataKeyNames="mot,greinnumer" 
                    ForeColor="#333333" GridLines="None" 
                    onselectedindexchanged="GridView3_SelectedIndexChanged" 
                onrowdatabound="GridView3_RowDataBound" 
                onrowcommand="GridView3_RowCommand"><AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:TemplateField HeaderText="Árangur"><ItemTemplate><asp:Button ID="EntEventResults" runat="server" 
                              CommandName="EnterEventResults" 
                              CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                              Text="Árangur" />
                  </ItemTemplate>

                    </asp:TemplateField>
                    <asp:BoundField DataField="greinnumer" HeaderText="greinnumer" ReadOnly="True" 
                            SortExpression="greinnumer" Visible="False"/>

                 <asp:TemplateField HeaderText="Dags." >
                     <ItemTemplate>
                         
                       <asp:TextBox ID="DagsGreinar" runat="server" Text='<%# Bind("dags") %>'> HeaderText="Dags." AutoPostBack="True" </asp:TextBox>
                         <asp:CalendarExtender ID="CalendarExtender1" runat="server" 
                                        DaysModeTitleFormat="dd.MM.yyyy" Enabled="True" Format="dd.MM.yyyy" 
                                        TargetControlID="DagsGreinar" TodaysDateFormat="dd.MM.yyyy"></asp:CalendarExtender>

                     </ItemTemplate>
                     <ControlStyle Width="85px" />
                     <FooterStyle Width="85px" />
                     <HeaderStyle Width="85px" />
                     <ItemStyle Width="85px" />

                 </asp:TemplateField> 
                    
                 <asp:TemplateField HeaderText="Tími">
                     <ItemTemplate>
                       <asp:TextBox ID="TimiGreinar" runat="server" Text='<%# Bind("timi") %>'> HeaderText="Tími." </asp:TextBox>
                     </ItemTemplate>
                     <ControlStyle Width="55px" />
                     <FooterStyle Width="55px" />
                     <HeaderStyle Width="55px" />
                     <ItemStyle Width="55px" />
                 </asp:TemplateField> 
                                            
                    <asp:BoundField DataField="kyn" HeaderText="Kyn" 
                            SortExpression="kyn" >
                     
                    <ItemStyle Font-Size="Smaller" />
                    </asp:BoundField>
                     
                    <asp:BoundField DataField="grein" HeaderText="Tákn greinar" 
                            SortExpression="grein" Visible="False" />
                    <asp:BoundField DataField="heitigreinar" HeaderText="Grein" 
                            SortExpression="heitigreinar" />
                    <asp:BoundField DataField="aldurfra" HeaderText="Ald frá" SortExpression="aldurfra" DataFormatString="{0:#}" >
                    <ItemStyle Font-Size="Smaller" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="aldurtil" HeaderText="Ald til" SortExpression="aldurtil" DataFormatString="{0:#}" >
                    <ItemStyle Font-Size="Smaller" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Aldursflokkur" HeaderText="Aldursfl." 
        SortExpression="Aldursflokkur" >
                    <ItemStyle Font-Size="Smaller" />
                    </asp:BoundField>
                    <asp:BoundField DataField="NoOfLanes" HeaderText="Fj. br." SortExpression="NoOfLanes" DataFormatString="{0:#}" >
                    <ItemStyle Font-Size="Smaller" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="NoOfAttempts" HeaderText="Fj. umf" SortExpression="NoOfAttempts" DataFormatString="{0:#}" >
                    <ItemStyle Font-Size="Smaller" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="rodiafrekaskra" HeaderText="Röð" 
                            SortExpression="rodiafrekaskra" Visible="False" />
                    <asp:BoundField DataField="Tegundgreinar" HeaderText="Tegund" 
        SortExpression="Tegundgreinar" >
                    <ItemStyle Font-Size="Smaller" />
                    </asp:BoundField>
                    <asp:BoundField DataField="StaðaKeppni" HeaderText="Staða" 
        SortExpression="StaðaKeppni" >
                    <ItemStyle Font-Size="Smaller" />
                    </asp:BoundField>
                    <asp:BoundField DataField="stigagrein" HeaderText="Stigagr." 
        SortExpression="stigagrein" >
                    <ItemStyle Font-Size="Smaller" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Fjoldi" HeaderText="Fjoldi" SortExpression="Fjoldi" DataFormatString="{0:#}" >
                    
                    <ItemStyle Font-Size="Smaller" HorizontalAlign="Right" />
                    </asp:BoundField>
                    
     <asp:TemplateField HeaderText="Úrslit"><ItemTemplate><asp:Button ID="Results" runat="server" 
      CommandName="ShowResults" 
      CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
      Text="Úrslit" />
     </ItemTemplate>
         </asp:TemplateField>
                    
                   
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
            <br /><br />
            <asp:Button ID="ModifyDateTime" runat="server" Text="Visa breytingar" OnClick="ModifyDateTime_Click" />



        </ContentTemplate>
        </asp:TabPanel>
        <asp:TabPanel runat="server" HeaderText="Heildarúrslit móts" ID="UrslGreinar" Font-Size="Larger">
        <ContentTemplate>
            <asp:TextBox ID="CompCode7" runat="server"></asp:TextBox>
            <asp:Label ID="Label1" runat="server" Text="Úrslit"></asp:Label>
            

            <asp:GridView ID="CompetitionResultsDataGrid" runat="server" 
                AutoGenerateColumns="False" DataSourceID="CompResults" 
                CellPadding="4" ForeColor="#333333" GridLines="None" 
                onrowdatabound="CompetitionResultsDataGrid_RowDataBound"><AlternatingRowStyle BackColor="White" /><Columns><asp:BoundField DataField="Place" HeaderText="Röð" 
                        SortExpression="Place" /><asp:BoundField DataField="Result" HeaderText="Úrslit" 
                        SortExpression="Result" /><asp:BoundField DataField="WindText" HeaderText="Vindur" 
                        SortExpression="WindText" /><asp:BoundField DataField="Name" HeaderText="Nafn" 
                        SortExpression="Name" /><asp:BoundField DataField="Club" HeaderText="Félag" SortExpression="Club" ></asp:BoundField><asp:BoundField DataField="YearOfBirth" HeaderText="F.ár" 
                        SortExpression="YearOfBirth" /><asp:BoundField DataField="Series" HeaderText="Sería/Skipan boðhlaupssveitar" 
                        SortExpression="Series"><ItemStyle Font-Size="Smaller" /></asp:BoundField><asp:BoundField DataField="EventName" HeaderText="." 
                        SortExpression="EventName" /><asp:BoundField DataField="EventDate" HeaderText="." 
                        SortExpression="EventDate" /><asp:BoundField DataField="LineType" HeaderText="." 
                        SortExpression="LineType" ></asp:BoundField></Columns><EditRowStyle BackColor="#2461BF" /><FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" /><HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" /><PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" /><RowStyle BackColor="#EFF3FB" /><SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" /><SortedAscendingCellStyle BackColor="#F5F7FB" /><SortedAscendingHeaderStyle BackColor="#6D95E1" /><SortedDescendingCellStyle BackColor="#E9EBEF" /><SortedDescendingHeaderStyle BackColor="#4870BE" /></asp:GridView><asp:SqlDataSource ID="CompResults" runat="server" 
                ConnectionString="<%$ ConnectionStrings:AthleticsConnectionString %>" 
                SelectCommand="CompResults" SelectCommandType="StoredProcedure"><SelectParameters><asp:ControlParameter ControlID="CompCode7" Name="CompCode" PropertyName="Text" 
                        Type="String" /></SelectParameters></asp:SqlDataSource></ContentTemplate>
            </asp:TabPanel>
    </asp:TabContainer>

    
                        <p style="font-size: x-large; width: 1000px;" 
        __designer:mapid="30a">
        </p>

    
    <script language="javascript">
    function limitCharsLength(Object, MaxLen)
  {
    if(Object.value.length > MaxLen-1)
      {
        Object.value= Object.value.substring(0,MaxLen);
      }
}
function isNumberKey(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode
    //    if ((charCode < 48 || charCode > 57) && (charCode != 44) && (charCode != 46)) // 0..9, , .
    if ((charCode < 48 || charCode > 57) && (charCode != 46))    // 0..9 .
        return false;

    return true;
}



//function isDate(value) {
//    try {
        //Change the below values to determine which format of date you wish to check. It is set to dd/mm/yyyy by default.
//        var DayIndex = 0;
//        var MonthIndex = 1;
//        var YearIndex = 2;

//        value = value.replace("-", ".").replace("/", ".");
//        var SplitValue = value.split(".");
//        var OK = true;
//        if (!(SplitValue[DayIndex].length == 1 || SplitValue[DayIndex].length == 2)) {
//            OK = false;
//        }
//        if (OK && !(SplitValue[MonthIndex].length == 1 || SplitValue[MonthIndex].length == 2)) {
//            OK = false;
//        }
//        if (OK && SplitValue[YearIndex].length != 4) {
//            OK = false;
//        }
//        if (OK) {
//            var Day = parseInt(SplitValue[DayIndex], 10);
//            var Month = parseInt(SplitValue[MonthIndex], 10);
//            var Year = parseInt(SplitValue[YearIndex], 10);

//            if (OK = ((Year > 1900) && (Year < new Date().getFullYear()))) {
//                if (OK = (Month <= 12 && Month > 0)) {
//                    var LeapYear = (((Year % 4) == 0) && ((Year % 100) != 0) || ((Year % 400) == 0));

//                    if (Month == 2) {
//                        OK = LeapYear ? Day <= 29 : Day <= 28;
//                    }
//                    else {
//                        if ((Month == 4) || (Month == 6) || (Month == 9) || (Month == 11)) {
//                            OK = (Day > 0 && Day <= 30);
//                        }
//                        else {
//                            OK = (Day > 0 && Day <= 31);
//                        }
//                    }
//                }
//            }
//        }
//        return OK;
//    }
//    catch (e) {
//        return false;
//    }
//}

  </script>
    </asp:Content>
   