<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RegisterCompetitorInEvent.aspx.cs" Inherits="MotFRI.RegisterCompetitorInEvent" %>
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
    <asp:TextBox ID="CompetitionName" runat="server" Font-Size="X-Large" ReadOnly="True" Width="1376px" BorderStyle="None" ></asp:TextBox>
    <br />
    <asp:Label ID="Label1" runat="server" Text="Rásnúmer: "></asp:Label>&nbsp;
 <asp:TextBox ID="BibNo" runat="server" ReadOnly="true" Width="79px"></asp:TextBox>&nbsp;&nbsp;
    <asp:Label ID="Label2" runat="server" Text="Nafn: "></asp:Label>&nbsp;
    <asp:TextBox ID="CompetitorName" runat="server" ReadOnly="true" Width="319px"></asp:TextBox>&nbsp;&nbsp;
    <asp:Label ID="Label3" runat="server" Text="Fæð.ár: "></asp:Label>&nbsp;
    <asp:TextBox ID="CompetitorYearOfBirth" runat="server" ReadOnly="true" Width="56px"></asp:TextBox>&nbsp;&nbsp;
    <asp:Label ID="Label6" runat="server" Text="Félag: "></asp:Label>&nbsp;
    <asp:TextBox ID="CompetitorClub" runat="server" ReadOnly="true" Width="68px"></asp:TextBox>&nbsp;&nbsp;
    <asp:Label ID="Label4" runat="server" Text="Aldur fyrir greinar: " Font-Bold="True" Font-Italic="True"></asp:Label>&nbsp;
    <asp:TextBox ID="CompetitorAge" runat="server" Width="63px" AutoPostBack="true" Font-Bold="True"></asp:TextBox>&nbsp;&nbsp;
    <asp:Label ID="Label5" runat="server" Text="Kyn: "></asp:Label>&nbsp;
    <asp:TextBox ID="CompetitorGender" runat="server" ReadOnly="true" Width="58px"></asp:TextBox>

    <br />
    <asp:TextBox ID="CompetitionCode" runat="server" ReadOnly="true" Visible="false"></asp:TextBox>
    <asp:TextBox ID="CompetitorCode" runat="server" ReadOnly="true" Visible="false"></asp:TextBox>
    <asp:TextBox ID="Gender" runat="server" ReadOnly="true" Visible="false"></asp:TextBox>
       
    <br />

    <asp:GridView ID="AvailableEvents" runat="server" AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" DataSourceID="AvailableEventsForCompetitor" ForeColor="#333333" GridLines="None" OnRowCommand="AvailableEvents_RowCommand">
        <AlternatingRowStyle BackColor="White" />
        <Columns>

            <asp:TemplateField HeaderText="Skrá/Afskrá">
               <ItemTemplate>
                  <asp:CheckBox ID="ValinChk" runat="server"  /> 
               </ItemTemplate>
               <HeaderStyle HorizontalAlign="Center" />
               <ItemStyle HorizontalAlign="Center" />
           </asp:TemplateField>

            <asp:BoundField DataField="AlreadyRegistered" HeaderText="Þegar skráð(ur)" SortExpression="AlreadyRegistered">
            <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:TemplateField><ItemTemplate><asp:Button ID="GuestYesNo" runat="server" 
               CommandName="SetGuestYesNo" 
               CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"  Text="Gestur Já/Nei" />
             </ItemTemplate></asp:TemplateField>

            <asp:BoundField DataField="Guest" HeaderText="Gestur" SortExpression="Guest" >
            <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="EventName" HeaderText="Heiti greinar" SortExpression="EventName" >
            <HeaderStyle HorizontalAlign="Left" />
            <ItemStyle HorizontalAlign="Left" />
            </asp:BoundField>
            <asp:BoundField DataField="EventDate" HeaderText="Dagsetning" SortExpression="EventDate" />
            <asp:BoundField DataField="EventTime" HeaderText="Tími" SortExpression="EventTime" />
            <asp:BoundField DataField="AgeFrom" HeaderText="Aldur frá" SortExpression="AgeFrom" >
            <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="AgeTo" HeaderText="Aldur til" SortExpression="AgeTo" >
            <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="OrderInStatistics" HeaderText="Röð" SortExpression="OrderInStatistics" />
            <asp:BoundField DataField="EventLine" HeaderText="Gr.númer" SortExpression="EventLine" />
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
    <br />
    <asp:Button ID="RegEventsForComp" runat="server" Text="Skrá/Afskrá í valdar greinar" OnClick="RegEventsForComp_Click" />
    <asp:SqlDataSource ID="AvailableEventsForCompetitor" runat="server" ConnectionString="<%$ ConnectionStrings:AthleticsConnectionString %>" SelectCommand="EventsForAthlete" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:ControlParameter ControlID="CompetitionCode" Name="CompetitionCode" PropertyName="Text" Type="String" />
            <asp:ControlParameter ControlID="Gender" Name="Gender" PropertyName="Text" Type="Int32" />
            <asp:ControlParameter ControlID="CompetitorAge" Name="Age" PropertyName="Text" Type="Int32" />
            <asp:ControlParameter ControlID="BibNo" Name="BibNo" PropertyName="Text" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    </asp:Content>
