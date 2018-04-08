<%@ Page Title="Keppnisgreinar móts" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SelectedCompetitionEvents.aspx.cs" Inherits="MotFRI.SelectedCompetitionEvents" %>
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
      
        <br />
        <asp:Label ID="CompEventsLbl" runat="server" Text="Keppnisgreinar móts" Width="1341px" style="font-size: x-large; font-style: italic; font-family: 'Segoe UI'"></asp:Label>&nbsp;&nbsp;
        <asp:TextBox ID="CompCode" Visible="false" runat="server"></asp:TextBox>
        <asp:HyperLink ID="ListOfCompetitions" runat="server" NavigateUrl="~/Default.aspx" Width="128px">Til baka</asp:HyperLink>&nbsp;&nbsp;&nbsp;
        <asp:HyperLink ID="YourCompetitors" runat="server" NavigateUrl="~/CompetitorsForUser.aspx">Þínir keppendur</asp:HyperLink>
        <asp:Label ID="YourCompetitorsLabel" runat="server" Text="&nbsp;&nbsp;&nbsp;"></asp:Label>
        <asp:HyperLink ID="Competitors" runat="server" NavigateUrl="~/SelectedCompetitionCompetitors.aspx">Keppendur</asp:HyperLink>&nbsp;&nbsp;&nbsp;
        <asp:HyperLink ID="CompetitionResults" runat="server" NavigateUrl="~/SelectedCompetitionResults.aspx">Úrslit</asp:HyperLink>&nbsp;&nbsp&nbsp;
<asp:HyperLink ID="CompSetup" runat="server" NavigateUrl="~/CompetitionSetup.aspx">Breyta uppsetningu móts</asp:HyperLink>&nbsp;&nbsp;
    <asp:HyperLink ID="UpdCompEvents" runat="server" NavigateUrl="~/UpdCompEvents.aspx">Breyta tímaseðli</asp:HyperLink>&nbsp;&nbsp;
        <asp:HyperLink ID="PointsStanding" runat="server" NavigateUrl="~/Points.aspx">Stigastaðan</asp:HyperLink>
    <asp:HyperLink ID="PointsStandingMIYngri" runat="server" NavigateUrl="~/PointsStandingMIYngri.aspx">Stigastaðan</asp:HyperLink>
    &nbsp;<asp:HyperLink ID="ExportLynxFiles" runat="server" NavigateUrl="~/ExportLynxFiles.aspx">Lynx skrár</asp:HyperLink>
        <br />
    <br />
       
        <asp:TextBox ID="CompetitionName" runat="server" style="font-size: xx-large" Width="1402px" BorderStyle="None" ReadOnly="true"></asp:TextBox>
    <br />
        
          <asp:TextBox ID="CompCode5" runat="server" ReadOnly="True" style="margin-top: 11px" 
                Visible="False"></asp:TextBox><asp:Label ID="Label3" runat="server" Text="EventNo:" Visible="False"></asp:Label>
          <asp:TextBox ID="EvenNo" runat="server" ReadOnly="True" Visible="False"></asp:TextBox>
          <asp:Label ID="Label4" runat="server" Text="SelGender:" Visible="False"></asp:Label>
          <asp:TextBox ID="SelGender" runat="server" ReadOnly="True" Visible="False"></asp:TextBox>
          <asp:Label ID="Label5" runat="server" Text="EventCode" Visible="False"></asp:Label>
          <asp:TextBox ID="EventCode" runat="server" ReadOnly="True" Visible="False"></asp:TextBox> 
    
        <br />
            
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
           
            <br /><br />
         

            
            <asp:GridView ID="GridView3" runat="server" 
                    DataSourceID="EventsInCompetition" AllowSorting="True" 
                    AutoGenerateColumns="False" CellPadding="4" DataKeyNames="mot,greinnumer" 
                    ForeColor="#333333" GridLines="None" 
                    onselectedindexchanged="GridView3_SelectedIndexChanged" 
                onrowdatabound="GridView3_RowDataBound" 
                onrowcommand="GridView3_RowCommand"><AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:HyperLinkField AccessibleHeaderText="ShowCompetitors" HeaderText="Keppendur" />
                    <asp:BoundField DataField="greinnumer" HeaderText="greinnumer" ReadOnly="True" 
                            SortExpression="greinnumer" Visible="False"/>
                                     
                    <asp:BoundField DataField="dags" HeaderText="Dags." 
                            SortExpression="dags" >
                    <ItemStyle Font-Size="Smaller" />
                    </asp:BoundField>
                    <asp:BoundField DataField="timi" HeaderText="Tími" 
                            SortExpression="timi" >
                    <ItemStyle Font-Size="Smaller" />
                    </asp:BoundField>
                           
                    <asp:BoundField DataField="kyn" HeaderText="Kyn" 
                            SortExpression="kyn" >
                     
                    <ItemStyle Font-Size="Smaller" />
                    </asp:BoundField>
                     
                    <asp:BoundField DataField="grein" HeaderText="Tákn greinar" 
                            SortExpression="grein" Visible="False" />
                    <asp:BoundField DataField="heitigreinar" HeaderText="Grein" 
                            SortExpression="heitigreinar" />
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
                    
          <asp:BoundField DataField="heitihtmlskrar" HeaderText="PDF skrá"  >
                    <ItemStyle Font-Size="Smaller" HorizontalAlign="Right" />
                    </asp:BoundField>

                            
                    <asp:BoundField DataField="tilkynnaverdlaunaafhendingu" HeaderText="Verðl." 
        SortExpression="tilkynnaverdlaunaafhendingu" >
                    <ItemStyle Font-Size="Smaller" />
                    </asp:BoundField>
     <asp:TemplateField HeaderText="Verðlaun"><ItemTemplate><asp:Button ID="PrizeFinished" runat="server" 
      CommandName="PrizeCeremonyFinished" 
      CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
      Text="Afhendingu lokið" />
     </ItemTemplate>
         </asp:TemplateField>
                    
     <asp:TemplateField HeaderText="Úrslit"><ItemTemplate><asp:Button ID="Results" runat="server" 
      CommandName="ShowResults" 
      CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
      Text="Úrslit" />
     </ItemTemplate>
         </asp:TemplateField>
                    
                   
                    <asp:HyperLinkField AccessibleHeaderText="Results" HeaderImageUrl="~/Img/pdf10.png" HeaderText="Results" Text="Results" />
                    
                   
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
              <asp:SqlDataSource ID="EventsInCompetition" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:AthleticsConnectionString %>" 
                    SelectCommand="EventsInCompetition2" SelectCommandType="StoredProcedure">
                        <SelectParameters><asp:ControlParameter ControlID="CompCode" Name="CompCode" PropertyName="Text" 
                            Type="String" /><asp:ControlParameter ControlID="EventStatusFilter" Name="EventStatusFilter" 
                            PropertyName="SelectedValue" Type="String" /><asp:ControlParameter ControlID="EventGenderFilter" Name="GenderFilter" 
                            PropertyName="SelectedValue" Type="String" />
                        <asp:ControlParameter ControlID="EventDateFilterDropDown" Name="EventDateFilter" PropertyName="SelectedValue" Type="String" />
                    </SelectParameters></asp:SqlDataSource>
    <br />
    <asp:Label ID="Label2" runat="server" Text="Hlekkur á þessa síðu: "></asp:Label>    
     <asp:TextBox ID="LinkToPage" runat="server" ReadOnly="true" Font-Size="Small" Width="500px" BorderStyle="None"></asp:TextBox>
    <br />
</asp:Content>
