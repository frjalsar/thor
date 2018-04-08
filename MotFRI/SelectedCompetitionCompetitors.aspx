<%@ Page Title="Keppendur móts" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SelectedCompetitionCompetitors.aspx.cs" Inherits="MotFRI.SelectedCompetitionCopetitors" %>
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
    <p>
        <br />
        <asp:Label ID="Label1" runat="server" Text="Keppendur móts" Width="196px" style="font-size: x-large; font-style: italic; font-family: 'Segoe UI'"></asp:Label> 
        <asp:TextBox ID="CompCode" Visible="false" runat="server"></asp:TextBox>&nbsp;
        <asp:HyperLink ID="ListOfCompetitions" runat="server" NavigateUrl="~/Default.aspx">Til baka</asp:HyperLink>&nbsp;&nbsp;&nbsp;
        <asp:HyperLink ID="YourCompetitors" runat="server" NavigateUrl="~/CompetitorsForUser.aspx">Þínir keppendur</asp:HyperLink>
        <asp:Label ID="CoachPageLabel" runat="server" Text="&nbsp;&nbsp;&nbsp;"></asp:Label>
        <asp:HyperLink ID="CoachPage" runat="server" NavigateUrl="~/CoachPage.aspx">Þjálfarasíða</asp:HyperLink>
        <asp:Label ID="YourCompetitorsLabel" runat="server" Text="&nbsp;&nbsp;&nbsp;"></asp:Label>

        <asp:HyperLink ID="Events" runat="server" NavigateUrl="~/SelectedCompetitionEvents.aspx">Keppnisgreinar</asp:HyperLink>&nbsp;&nbsp;&nbsp;
        <asp:HyperLink ID="CompetitionResults" runat="server" NavigateUrl="~/SelectedCompetitionResults.aspx">Úrslit</asp:HyperLink>&nbsp;&nbsp&nbsp;
        <asp:HyperLink ID="PointsStanding" runat="server" NavigateUrl="~/Points.aspx">Stigastaðan</asp:HyperLink>
        <asp:HyperLink ID="PointsStandingMIYngri" runat="server" NavigateUrl="~/PointsStandingMIYngri.aspx">Stigastaðan</asp:HyperLink>
        <br />
        <br />
        <asp:TextBox ID="CompetitionName" runat="server" style="font-size: xx-large" Width="1210px" BorderStyle="None" ReadOnly="true"></asp:TextBox>
        <br />
        <br />
                <asp:GridView 
                ID="GridView1" runat="server" AutoGenerateColumns="False" 
            DataKeyNames="mot,rasnumer" DataSourceID="CompetitorsInComp" 
                AllowSorting="True" CellPadding="4" ForeColor="#333333" GridLines="None" 
                onrowcommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound"><AlternatingRowStyle BackColor="White" /><Columns><asp:TemplateField><ItemTemplate><asp:Button ID="RegEvent" runat="server" 
                  CommandName="RegisterEvents" 
                  CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                  Text="Skrá í greinar" /></ItemTemplate></asp:TemplateField><asp:TemplateField><ItemTemplate><asp:Button ID="RegisterInEventButton" runat="server" 
             CommandName="RegisterInEvents" 
             CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
             Text="Eyða" /></ItemTemplate></asp:TemplateField>

                        <asp:BoundField DataField="rasnumer" HeaderText="Rásnr." ReadOnly="True" 
                    SortExpression="rasnumer" />
                    <asp:BoundField DataField="Nafn" HeaderText="Nafn" SortExpression="nafn" /><asp:BoundField DataField="felag" HeaderText="Félag" SortExpression="felag" />
                    <asp:BoundField DataField="kyn" HeaderText="Kyn" SortExpression="kyn" /><asp:BoundField DataField="kennitala" HeaderText="Kennitala" 
                    SortExpression="kennitala" Visible="False" /><asp:BoundField DataField="faedingardagur" HeaderText="Fæð.dagur" 
                    SortExpression="faedingardagur" /><asp:BoundField DataField="faedingarar" HeaderText="Fæð.ár" 
                    SortExpression="faedingarar" />

                    <asp:BoundField DataField="aldurkeppanda" HeaderText="Aldur" 
                    SortExpression="aldurkeppanda" /><asp:BoundField DataField="land" HeaderText="Land" SortExpression="land" /><asp:BoundField DataField="fyrirlidi" HeaderText="Fyrirl." 
                    SortExpression="fyrirlidi" /><asp:BoundField DataField="Fj" HeaderText="Fj.skrán" SortExpression="Fj" />
        <asp:HyperLinkField DataNavigateUrlFields="rasnumer" DataNavigateUrlFormatString="~\CompetitionClubOrCompetitorInformation.aspx?BibNo={0}" DataTextField="fj" HeaderText="Fj. skráninga" Text="B" >

                        <ItemStyle HorizontalAlign="Center" />
                        </asp:HyperLinkField>
                    </Columns>
    <EditRowStyle BackColor="#2461BF" /><FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" /><HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" /><RowStyle BackColor="#EFF3FB" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#F5F7FB" /><SortedAscendingHeaderStyle BackColor="#6D95E1" />
                    <SortedDescendingCellStyle BackColor="#E9EBEF" /><SortedDescendingHeaderStyle BackColor="#4870BE" />

                </asp:GridView><asp:SqlDataSource ID="CompetitorsInComp" runat="server" 
            ConnectionString="<%$ ConnectionStrings:AthleticsConnectionString %>" 
            SelectCommand="CompetitorsInComp" SelectCommandType="StoredProcedure"><SelectParameters><asp:ControlParameter ControlID="CompCode" Name="CompCode" 
                    PropertyName="Text" Type="String" /></SelectParameters></asp:SqlDataSource><br />
        <br />        
    <asp:Label ID="Label2" runat="server" Text="Hlekkur á þessa síðu: "></asp:Label>    
     <asp:TextBox ID="LinkToPage" runat="server" ReadOnly="true" Font-Size="Small" Width="500px" BorderStyle="None"></asp:TextBox>
<br />

</asp:Content>
