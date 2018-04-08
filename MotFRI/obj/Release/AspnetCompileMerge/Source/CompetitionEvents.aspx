<%@ Page Title="Keppnisgreinar Móts" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CompetitionEvents.aspx.cs" Inherits="MotFRI.CompetitionEvents" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h3><br />Veldu keppnisgreinarnar</h3>
    <br />


    <asp:Label ID="Label1" runat="server" Text="Skrá greinar fyrir keppnisdag: "></asp:Label>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">  </asp:ToolkitScriptManager>
    <asp:TextBox ID="DateForEvents" runat="server"></asp:TextBox>
    <asp:CalendarExtender ID="CalendarExtender1" runat="server"
        DaysModeTitleFormat="dd.MM.yyyy" Enabled="True" Format="dd.MM.yyyy" 
        TargetControlID="DateForEvents" TodaysDateFormat="dd.MM.yyyy">
        </asp:CalendarExtender>

   
        
    <br />
 
<br />

    <asp:Button ID="VistaButton" runat="server" Text="Vista" 
        onclick="VistaButton_Click" />
<br />
    <asp:TextBox ID="OutdoorsOrIndoors" runat="server" Visible="false" ReadOnly="true"></asp:TextBox>
    <asp:TextBox ID="Male" runat="server" Visible="false" Text="1" ReadOnly="true"></asp:TextBox>
    <asp:TextBox ID="Female" runat="server" Visible="false" Text="2" ReadOnly="true"></asp:TextBox><br />


    <asp:TextBox ID="Message" runat="server" ReadOnly="true" ForeColor="Red" 
        Width="1236px"></asp:TextBox>
    <br />
    
    <asp:Table ID="Table1" runat="server" Height="26px" Width="1400px">
    <asp:TableRow>
    <asp:TableCell VerticalAlign="Top">
    <asp:GridView ID="NormalEventsMenGrid" runat="server" AutoGenerateColumns="False"  
        ShowFooter="True" DataSourceID="NormalEventsPrAgeGrMale" 
        DataKeyNames="Samsettur lykill"  CellPadding="4" 
        ForeColor="#333333" GridLines="None"  
        onrowdatabound="StandardTextGridView_RowDataBound" >
       
             <AlternatingRowStyle BackColor="White" />
             <Columns>
                 <asp:BoundField DataField="Samsettur lykill" HeaderText="Samsettur lykill" ReadOnly="True" Visible="false"/>
                 <asp:BoundField DataField="Aldursflokkur" HeaderText="Aldursflokkur" ReadOnly="True" />
                 <asp:BoundField DataField="Keppnisgrein" HeaderText="Keppnisgrein" ReadOnly="True" />
               
                   <asp:TemplateField HeaderText="Velja">
                     <ItemTemplate>
                     <asp:CheckBox ID="ValinChk" runat="server" /> 
                 </ItemTemplate>
                 </asp:TemplateField>

                 <asp:TemplateField HeaderText="Velja">
                     <ItemTemplate>
                       <asp:DropDownList ID="UmferdDropDownList" runat="server" Width="100px">
                       <asp:ListItem>Úrslit</asp:ListItem>
                       <asp:ListItem>Undanúrslit og úrslit</asp:ListItem>
                       <asp:ListItem>Riðlakeppni, undanúrslit og úrslit</asp:ListItem>
                       <asp:ListItem>Forkeppni, riðlakeppni, undanúrslit og úrslit</asp:ListItem>
                       </asp:DropDownList> 
                     </ItemTemplate>
                 </asp:TemplateField>
                  <asp:TemplateField HeaderText="Frá">
                     <ItemTemplate>
                         <asp:TextBox ID="MAgeFrom" runat="server" Width="45px" HeaderText="Frá">
                         </asp:TextBox>
                     </ItemTemplate>
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="Til">
                     <ItemTemplate>
                         <asp:TextBox ID="MAgeTo" runat="server" Width="45px" HeaderText="Til">
                         </asp:TextBox>
                     </ItemTemplate>
                 </asp:TemplateField>

                 <asp:BoundField DataField="AldursflokkurFRI2" HeaderText="." ReadOnly="True" Visible="true"/>          
           
             </Columns>
          
             <EditRowStyle BackColor="#2461BF" />
             <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
             <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
             <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
             <RowStyle BackColor="#EFF3FB" />
            
         </asp:GridView> 
    </asp:TableCell>
    <asp:TableCell VerticalAlign="Top">
    <asp:GridView ID="NormalEventsWomenGrid" runat="server" AutoGenerateColumns="False"  
        ShowFooter="True" DataSourceID="NormalEventsPrAgeGrFemale" 
        DataKeyNames="Samsettur lykill"  CellPadding="4" 
        ForeColor="#333333" GridLines="None"  
        onrowdatabound="NormalEventsWomenGridiew_RowDataBound" >
       
             <AlternatingRowStyle BackColor="White" />
             <Columns>
                 <asp:BoundField DataField="Samsettur lykill" HeaderText="Samsettur lykill" ReadOnly="True" visible="false" />
                 <asp:BoundField DataField="Aldursflokkur" HeaderText="Aldursflokkur" ReadOnly="True" />
                 <asp:BoundField DataField="Keppnisgrein" HeaderText="Keppnisgrein" ReadOnly="True" />
               
                   <asp:TemplateField HeaderText="Velja">
                     <ItemTemplate>
                     <asp:CheckBox ID="ValinChk" runat="server" /> 
                 </ItemTemplate>
                 </asp:TemplateField>

                 <asp:TemplateField HeaderText="Velja">
                     <ItemTemplate>
                     <asp:DropDownList ID="UmferdDropDownList" runat="server" Width="100px">
                     <asp:ListItem>Úrslit</asp:ListItem>
                     <asp:ListItem>Undanúrslit og úrslit</asp:ListItem>
                     <asp:ListItem>Riðlakeppni, undanúrslit og úrslit</asp:ListItem>
                     <asp:ListItem>Forkeppni, riðlakeppni, undanúrslit og úrslit</asp:ListItem>
                     </asp:DropDownList> 
                 </ItemTemplate>
                 </asp:TemplateField>
                  
                <asp:TemplateField HeaderText="Frá">
                     <ItemTemplate>
                         <asp:TextBox ID="WAgeFrom" runat="server" Width="45px">
                         </asp:TextBox>
                     </ItemTemplate>
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="Til">
                     <ItemTemplate>
                         <asp:TextBox ID="WAgeTo" runat="server" Width="45px" 
                             >
                         </asp:TextBox>
                     </ItemTemplate>
                 </asp:TemplateField>

                 <asp:BoundField DataField="AldursflokkurFRI2" HeaderText="." ReadOnly="True" Visible="true"/>
            

                 
             </Columns>
          
             <EditRowStyle BackColor="#2461BF" />
             <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
             <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
             <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
             <RowStyle BackColor="#EFF3FB" />
            
         </asp:GridView>
    </asp:TableCell>
    </asp:TableRow>
    </asp:Table>
    <br />
        
    <asp:SqlDataSource ID="NormalEventsPrAgeGrMale" runat="server" 
        ConnectionString="<%$ ConnectionStrings:AthleticsConnectionString %>" 
        SelectCommand="ReturnStandardEventsPrAgeAndGend" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:ControlParameter ControlID="OutdoorsOrIndoors" Name="OutDoorOrIndoor" 
                PropertyName="Text" Type="Int32" />
            <asp:ControlParameter ControlID="Male" Name="Gender" PropertyName="Text" 
                Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="NormalEventsPrAgeGrFemale" runat="server" 
        ConnectionString="<%$ ConnectionStrings:AthleticsConnectionString %>" 
        SelectCommand="ReturnStandardEventsPrAgeAndGend" 
        SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:ControlParameter ControlID="OutdoorsOrIndoors" Name="OutDoorOrIndoor" 
                PropertyName="Text" Type="Int32" />
            <asp:ControlParameter ControlID="Female" Name="Gender" PropertyName="Text" 
                Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    


    </asp:Content>
