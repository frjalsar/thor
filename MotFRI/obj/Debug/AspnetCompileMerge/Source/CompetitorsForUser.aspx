<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CompetitorsForUser.aspx.cs" Inherits="MotFRI.CompetitorsForUser" %>
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
        <asp:Label ID="Label1" runat="server" Text="Keppendur félags" Width="196px" style="font-size: x-large; font-style: italic; font-family: 'Segoe UI'"></asp:Label> 
        <asp:TextBox ID="CompCode" Visible="false" runat="server"></asp:TextBox>&nbsp;<asp:TextBox ID="CurrentUserName" runat="server" Visible="false" ReadOnly="true"></asp:TextBox>
        <asp:HyperLink ID="ListOfCompetitions" runat="server" NavigateUrl="~/Default.aspx">Til baka</asp:HyperLink>&nbsp;&nbsp;&nbsp;
        <asp:HyperLink ID="YourCompetitors" runat="server" NavigateUrl="~/SelectedCompetitionCompetitors.aspx">Keppendur</asp:HyperLink>
        <asp:Label ID="YourCompetitorsLabel" runat="server" Text="&nbsp;&nbsp;&nbsp;"></asp:Label>
        <asp:HyperLink ID="Events" runat="server" NavigateUrl="~/SelectedCompetitionEvents.aspx">Keppnisgreinar</asp:HyperLink>&nbsp;&nbsp;&nbsp;
        <asp:HyperLink ID="CompetitionResults" runat="server" NavigateUrl="~/SelectedCompetitionResults.aspx">Úrslit</asp:HyperLink>
        <br />
        <br />
        <asp:TextBox ID="CompetitionName" runat="server" style="font-size: xx-large" Width="1210px" BorderStyle="None" ReadOnly="true"></asp:TextBox>
        <br /><br />
            <asp:HyperLink ID="NewRegistration" runat="server" NavigateUrl="~/CompetitorsRegisterInMeet.aspx">Skrá keppendur í mót</asp:HyperLink>
            <asp:Label ID="NewRegLabel" runat="server" Text="&nbsp;&nbsp;&nbsp;"></asp:Label>
            <asp:HyperLink ID="NewCompetitors" runat="server" NavigateUrl="~/SelectCompetitor.aspx">Nýskráning á keppanda í kerfið</asp:HyperLink>
            <asp:Label ID="CompWithRegLabel" runat="server" Text="&nbsp;&nbsp;&nbsp;"></asp:Label>
            <asp:HyperLink ID="CompetitorsWithRegistratios" runat="server" NavigateUrl="~/ClubCompetitorsWithReg.aspx">Keppendur félags með skráningum</asp:HyperLink>
            &nbsp;<asp:HyperLink ID="RegisterRelayTeams" runat="server" NavigateUrl="~/RegisterRelay.aspx">Boðhlaup</asp:HyperLink>
            <br /><br />
            <asp:Label ID="Label2" runat="server" Text="Veldu félag: " Font-Size="Large"></asp:Label>&nbsp;<asp:DropDownList ID="SelectClubDropDown" runat="server" 
                DataSourceID="ClubsForUserDS" DataTextField="NameOfClub" DataValueField="Club" AutoPostBack="True"></asp:DropDownList>
            <asp:SqlDataSource ID="ClubsForUserDS" runat="server" ConnectionString="<%$ ConnectionStrings:AthleticsConnectionString %>" SelectCommand="ClubsForUser3" SelectCommandType="StoredProcedure">
                <SelectParameters>
                    <asp:ControlParameter ControlID="CurrentUserName" Name="UserLogin" PropertyName="Text" Type="String" />
                    <asp:ControlParameter ControlID="CompCode" Name="CompCode" PropertyName="Text" Type="String" />
                </SelectParameters>
            </asp:SqlDataSource>
            <br />
        <br />
            <asp:TextBox ID="ErrorMessage" runat="server" ReadOnly="true" Text="" BorderStyle="None" ForeColor="#FF0066" Height="25px" Width="957px"></asp:TextBox>
            <br />
                <asp:GridView 
                ID="GridView1" runat="server" AutoGenerateColumns="False" 
            DataKeyNames="mot,rasnumer" DataSourceID="CompetitorsInComp" 
                AllowSorting="True" CellPadding="4" ForeColor="#333333" GridLines="None" 
                onrowcommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
            <AlternatingRowStyle BackColor="White" /><Columns><asp:TemplateField>
            <ItemTemplate><asp:Button ID="RegEvent" runat="server" 
                  CommandName="RegisterEvents" 
                  CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                  Text="Skrá í greinar" /></ItemTemplate>
             </asp:TemplateField>
             <asp:TemplateField><ItemTemplate><asp:Button ID="DeleteCompetitorInComp" runat="server" 
               CommandName="DeleteCompetitorInComp" 
               CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"  Text="Eyða" />
             </ItemTemplate></asp:TemplateField>
             <asp:TemplateField><ItemTemplate><asp:Button ID="GuestYesNo" runat="server" 
               CommandName="SetGuestYesNo" 
               CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"  Text="Gestur Já/Nei" />
             </ItemTemplate></asp:TemplateField>

                <asp:BoundField DataField="rasnumer" HeaderText="Rásnr." ReadOnly="True" 
                    SortExpression="rasnumer" />
                <asp:BoundField DataField="Nafn" HeaderText="Nafn" SortExpression="nafn" />
                <asp:BoundField DataField="felag" HeaderText="Félag" SortExpression="felag" />
                <asp:BoundField DataField="gestur" HeaderText="Gestur" SortExpression="gestur" />
                <asp:BoundField DataField="kyn" HeaderText="Kyn" SortExpression="kyn" /><asp:BoundField DataField="kennitala" HeaderText="Kennitala" 
                    SortExpression="kennitala" Visible="False" /><asp:BoundField DataField="faedingardagur" HeaderText="Fæð.dagur" 
                    SortExpression="faedingardagur" /><asp:BoundField DataField="faedingarar" HeaderText="Fæð.ár" 
                    SortExpression="faedingarar" /><asp:BoundField DataField="aldurkeppanda" HeaderText="Aldur" 
                    SortExpression="aldurkeppanda" /><asp:BoundField DataField="land" HeaderText="Land" SortExpression="land" /><asp:BoundField DataField="fyrirlidi" HeaderText="Fyrirl." 
                    SortExpression="fyrirlidi" /><asp:BoundField DataField="Fj" HeaderText="Fj.skrán" SortExpression="Fj" /></Columns>
    <EditRowStyle BackColor="#2461BF" /><FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" /><HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" /><PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" /><RowStyle BackColor="#EFF3FB" /><SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" /><SortedAscendingCellStyle BackColor="#F5F7FB" /><SortedAscendingHeaderStyle BackColor="#6D95E1" /><SortedDescendingCellStyle BackColor="#E9EBEF" /><SortedDescendingHeaderStyle BackColor="#4870BE" /></asp:GridView><asp:SqlDataSource ID="CompetitorsInComp" runat="server" 
            ConnectionString="<%$ ConnectionStrings:AthleticsConnectionString %>" 
            SelectCommand="CompetitorsInCompFromClub" SelectCommandType="StoredProcedure"><SelectParameters><asp:ControlParameter ControlID="CompCode" Name="CompCode" PropertyName="Text" Type="String" /><asp:ControlParameter ControlID="SelectClubDropDown" Name ="Club"
                    PropertyName="SelectedValue" Type="String" /></SelectParameters></asp:SqlDataSource><br />

</asp:Content>
