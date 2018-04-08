<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CompEvents.aspx.cs" Inherits="MotFRI.CompEvents" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
            <ContentTemplate><asp:Label ID="Label6" runat="server" Text="CompCode4" Visible="False"></asp:Label><asp:TextBox ID="CompCode4" runat="server" Visible="False"></asp:TextBox><asp:Label ID="Label2" runat="server" Text="CompCode5:" Visible="False"></asp:Label><asp:TextBox ID="CompCode5" runat="server" ReadOnly="True" style="margin-top: 11px" 
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

            </asp:GridView><asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
                      <br /><br />
            <asp:Button ID="ModifyDateTime" runat="server" Text="Visa breytingar" OnClick="ModifyDateTime_Click" />



</asp:Content>
