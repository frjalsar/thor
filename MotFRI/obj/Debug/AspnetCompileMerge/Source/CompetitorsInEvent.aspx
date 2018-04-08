<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CompetitorsInEvent.aspx.cs" Inherits="MotFRI.CompetitorsInEvent" %>
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
    <asp:TextBox ID="CompetitionName" runat="server" ReadOnly="true" Font-Size="Large" Width="983px"></asp:TextBox>
    <br /><br />
    <asp:TextBox ID="CompetitionEventName" runat="server" ReadOnly="true" Font-Size="Large" Width="784px"></asp:TextBox>
    <asp:TextBox ID="CompCode" runat="server" Visible="false"></asp:TextBox><asp:TextBox ID="EventLineNo" runat="server" Visible="false"></asp:TextBox>
    <br />
    <asp:GridView ID="CompetitorsInEventGrid" runat="server" AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="mot,greinarnumer,lina" DataSourceID="CompInEvent" ForeColor="#333333" GridLines="None">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:BoundField DataField="rasnumer" HeaderText="Rásnr." SortExpression="rasnumer" />
            <asp:BoundField DataField="ridillnumer" HeaderText="Riðill" SortExpression="ridillnumer" />
            <asp:BoundField DataField="stokkkastrod" HeaderText="Braut/röð" SortExpression="stokkkastrod" />
            <asp:BoundField DataField="nafn" HeaderText="Nafn" SortExpression="nafn" />
            <asp:BoundField DataField="faedingarar" HeaderText="F.ár" SortExpression="faedingarar" />
            <asp:BoundField DataField="felag" HeaderText="Félag" SortExpression="felag" />
            <asp:BoundField DataField="vindur" HeaderText="Vind" SortExpression="vindur" />
            <asp:BoundField DataField="arangur" HeaderText="Árangur" SortExpression="arangur" />
            <asp:BoundField DataField="urslitarod" HeaderText="Úrsl.röð" SortExpression="urslitarod" />
            <asp:BoundField DataField="stig" HeaderText="Stig" SortExpression="stig" />
            <asp:BoundField DataField="IAAF Stig" HeaderText="IAAF Stig" SortExpression="IAAF Stig" />
            <asp:BoundField DataField="tilraun1" HeaderText="Tilraun 1" SortExpression="tilraun1" />
            <asp:BoundField DataField="tilraun2" HeaderText="Tilraun 2" SortExpression="tilraun2" />
            <asp:BoundField DataField="tilraun3" HeaderText="Tilraun 3" SortExpression="tilraun3" />
            <asp:BoundField DataField="tilraun4" HeaderText="Tilraun 4" SortExpression="tilraun4" />
            <asp:BoundField DataField="tilraun5" HeaderText="Tilraun 5" SortExpression="tilraun5" />
            <asp:BoundField DataField="tilraun6" HeaderText="Tilraun 6" SortExpression="tilraun6" />
            <asp:BoundField DataField="athugasemd" HeaderText="Athugasemd" SortExpression="athugasemd" />
            <asp:BoundField DataField="bestiaranguriartexti" HeaderText="Ársbesta" SortExpression="bestiaranguriartexti" />
            <asp:BoundField DataField="personulegtmettexti" HeaderText="Pers.met" SortExpression="personulegtmettexti" />
            <asp:BoundField DataField="Unglingastig" HeaderText="Unglingastig" SortExpression="Unglingastig" />
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
    <asp:SqlDataSource ID="CompInEvent" runat="server" ConnectionString="<%$ ConnectionStrings:AthleticsConnectionString %>" SelectCommand="CompetitorsInEvent" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:ControlParameter ControlID="CompCode" Name="CompetitionCode" PropertyName="Text" Type="String" />
            <asp:ControlParameter ControlID="EventLineNo" Name="EventNumber" PropertyName="Text" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>
