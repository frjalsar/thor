<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UpdCompEvents.aspx.cs" Inherits="MotFRI.UpdCompEvents" %>
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
    <asp:TextBox ID="CompCode" runat="server" Visible="false" ReadOnly="true"></asp:TextBox><br />
    <asp:TextBox ID="CompName" runat="server" BorderStyle="None" Height="36px" Width="1403px" ReadOnly="true" style="font-size: xx-large"></asp:TextBox><br />
    <br /><br />

    <asp:GridView ID="EventsGW" runat="server" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="mot,lina" DataSourceID="CompetitionEventsDS" ForeColor="#333333" GridLines="None" OnRowDataBound="EventsGW_RowDataBound">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
                        <asp:TemplateField HeaderText="Eyða">
                       <ItemTemplate>
                         <asp:CheckBox ID="DeleteChk" runat="server"  /> 
                       </ItemTemplate>
                          <HeaderStyle HorizontalAlign="Center" Font-Size="Large" />
                          <ItemStyle HorizontalAlign="Center" Font-Size="Large" />
                      </asp:TemplateField>
            <asp:BoundField DataField="lina" HeaderText="lina" ReadOnly="True" SortExpression="lina" />
            <asp:BoundField DataField="dagsetning" HeaderText="Dags" ReadOnly="True" SortExpression="dagsetning" />
            <asp:BoundField DataField="timi" HeaderText="Tími" ReadOnly="True" SortExpression="timi" />
    
             <asp:TemplateField HeaderText="Dags.">
              <ItemTemplate>
                 <asp:DropDownList ID="DropDDate" Width="100px" runat="server">
                 </asp:DropDownList>
              </ItemTemplate>
          </asp:TemplateField>

                       
                 <asp:TemplateField HeaderText="Tími">
                     <ItemTemplate>
                        <asp:TextBox ID="TimiEd" runat="server" Width="70px" Text='<%# Bind("timi") %>'>  </asp:TextBox>     
                     </ItemTemplate>
                 </asp:TemplateField>               
               
                 <asp:TemplateField HeaderText="Heiti greinar">
                     <ItemTemplate>
                        <asp:TextBox ID="EventDescription" runat="server" Width="320px" Text='<%# Bind("heitigreinar") %>'>  </asp:TextBox>     
                     </ItemTemplate>
                 </asp:TemplateField>               


                 <asp:TemplateField HeaderText="Fj. umf">
                     <ItemTemplate>
                        <asp:TextBox ID="NoOfRounds" runat="server" Width="70px" Text='<%# Bind("fjoldiumferda") %>'>  </asp:TextBox>     
                     </ItemTemplate>
                 </asp:TemplateField>               

                 <asp:TemplateField HeaderText="Fj. brauta">
                     <ItemTemplate>
                        <asp:TextBox ID="NoOfLanes" runat="server" Width="70px" Text='<%# Bind("fjoldiibrauta") %>'>  </asp:TextBox>     
                     </ItemTemplate>
                 </asp:TemplateField>               

                 <asp:TemplateField HeaderText="Aldur frá">
                     <ItemTemplate>
                        <asp:TextBox ID="AgeFrom" runat="server" Width="70px" Text='<%# Bind("aldurfra") %>'>  </asp:TextBox>     
                     </ItemTemplate>
                 </asp:TemplateField>               

                 <asp:TemplateField HeaderText="Aldur til">
                     <ItemTemplate>
                        <asp:TextBox ID="AgeTo" runat="server" Width="70px" Text='<%# Bind("aldurtil") %>'>  </asp:TextBox>     
                     </ItemTemplate>
                 </asp:TemplateField>               

            <asp:BoundField DataField="fjoldiumferda" HeaderText="Fj. umf" ReadOnly="True" SortExpression="fjoldiumferda" />
            <asp:BoundField DataField="fjoldiibrauta" HeaderText="Fj. brauta" ReadOnly="True" SortExpression="fjoldiibrauta" />
            <asp:BoundField DataField="aldurfra" HeaderText="Aldur frá" ReadOnly="True" SortExpression="aldurfra" />
            <asp:BoundField DataField="aldurtil" HeaderText="Aldur til" ReadOnly="True" SortExpression="aldurtil" />
            <asp:BoundField DataField="tegundgreinar" HeaderText="Tegund greinar" ReadOnly="True" SortExpression="tegundgreinar" />
            <asp:BoundField DataField="heitigreinar" HeaderText="Heiti greinar" ReadOnly="True" SortExpression="heitigreinar" />
            <asp:BoundField DataField="nanaritegundargreining" HeaderText="Nánari tegundargreining" ReadOnly="true" SortExpression="nanaritegundargreining" />
            <asp:BoundField DataField="NoOfCompetitorsInEv" HeaderText="Fj. kepp." ReadOnly="true" SortExpression="NoOfCompetitorsInEv" />
            <asp:BoundField DataField="stadakeppni" HeaderText="Staða" ReadOnly="True" SortExpression="stadakeppni" />

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
    <asp:SqlDataSource ID="CompetitionEventsDS" runat="server" ConnectionString="<%$ ConnectionStrings:AthleticsConnectionString %>" SelectCommand="CompetitionEvents" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:ControlParameter ControlID="CompCode" Name="CompCode" PropertyName="Text" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
    <br /><br />
    <asp:Button ID="SaveValues" runat="server" OnClick="Button1_Click" Text="Vista" style="font-size: large" />
&nbsp;&nbsp; <asp:Button ID="BackToEvents" runat="server" Text="Til baka" style="font-size: large" OnClick="BackToEvents_Click" />
</asp:Content>
