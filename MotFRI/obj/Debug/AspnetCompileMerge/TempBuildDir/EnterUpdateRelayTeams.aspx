<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EnterUpdateRelayTeams.aspx.cs" Inherits="MotFRI.EnterUpdateRelayTeams" %>
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
<asp:TextBox ID="CompName" runat="server" ReadOnly="true" BorderStyle="None" Height="50px" style="font-size: xx-large" Width="1388px"></asp:TextBox><br /><br />
<asp:TextBox ID="EventName" runat="server" ReadOnly="true" BorderStyle="None" style="font-size: x-large"></asp:TextBox><br />
    <asp:Label ID="Label2" runat="server" Text="Skráning boðhlaupssveita" style="font-size: medium"></asp:Label><br /><br />
    <asp:TextBox ID="CompCode" runat="server" ReadOnly="true" Visible="false"></asp:TextBox>
    <asp:TextBox ID="EventLineNo" runat="server" ReadOnly="true" Visible="false"></asp:TextBox><br />
<asp:Label ID="Label1" runat="server" Text="Veldu félag:" style="font-size: medium"></asp:Label>&nbsp;&nbsp; 
<asp:DropDownList ID="SelectClubDropDownL" runat="server" style="font-size: medium" AutoPostBack="True"></asp:DropDownList><br /><br />
    <asp:Label ID="Label3" runat="server" Text="Heiti sveitar:" style="font-size: medium"></asp:Label>&nbsp;&nbsp;

<asp:TextBox ID="TeamName" runat="server" ReadOnly="true" BorderStyle="None" style="font-size: large" Width="979px"></asp:TextBox><br /><br />

<asp:TextBox ID="Label4" runat="server" Text="Sprettur" ReadOnly="true" BorderStyle="None" Width="67px"></asp:TextBox>&nbsp;&nbsp;
<asp:TextBox ID="Label5" runat="server" Text="Rásnr." ReadOnly="true" BorderStyle="Outset" Width="67px" style="text-align: center"></asp:TextBox>&nbsp;&nbsp;
<asp:TextBox ID="Label6" runat="server" Text="Nafn" ReadOnly="true" BorderStyle="None" Width="400px" style="font-size: medium"></asp:TextBox>&nbsp;&nbsp;
<asp:TextBox ID="Label7" runat="server" Text="F.ár"  ReadOnly="true" BorderStyle="None" Width="67px"></asp:TextBox>&nbsp;&nbsp;
<asp:TextBox ID="Label8" runat="server" Text="Aldur" ReadOnly="true" BorderStyle="None" Width="67px"></asp:TextBox><br />

<asp:TextBox ID="Leg1" runat="server" ReadOnly="true" BorderStyle="None" Width="67px" style="text-align: center"></asp:TextBox>&nbsp;&nbsp;
<asp:TextBox ID="Bib1" runat="server" BorderStyle="Outset" Width="67px" style="text-align: center; font-weight: 700;" TabIndex="1"></asp:TextBox>&nbsp;&nbsp;
<asp:TextBox ID="Name1" runat="server" ReadOnly="true" BorderStyle="None" Width="400px" style="font-size: medium"></asp:TextBox>&nbsp;&nbsp;
<asp:TextBox ID="Yob1" runat="server" ReadOnly="true" BorderStyle="None" Width="67px"></asp:TextBox>&nbsp;&nbsp;
<asp:TextBox ID="Age1" runat="server" ReadOnly="true" BorderStyle="None" Width="67px"></asp:TextBox><br />

<asp:TextBox ID="Leg2" runat="server" ReadOnly="true" BorderStyle="None" Width="67px" style="text-align: center"></asp:TextBox>&nbsp;&nbsp;
<asp:TextBox ID="Bib2" runat="server" BorderStyle="Outset" Width="67px" style="text-align: center; font-weight: 700;" TabIndex="2"></asp:TextBox>&nbsp;&nbsp;
<asp:TextBox ID="Name2" runat="server" ReadOnly="true" BorderStyle="None" Width="400px" style="font-size: medium"></asp:TextBox>&nbsp;&nbsp;
<asp:TextBox ID="Yob2" runat="server" ReadOnly="true" BorderStyle="None" Width="67px"></asp:TextBox>&nbsp;&nbsp;
<asp:TextBox ID="Age2" runat="server" ReadOnly="true" BorderStyle="None" Width="67px"></asp:TextBox><br />

<asp:TextBox ID="Leg3" runat="server" ReadOnly="true" BorderStyle="None" Width="67px" style="text-align: center"></asp:TextBox>&nbsp;&nbsp;
<asp:TextBox ID="Bib3" runat="server" BorderStyle="Outset" Width="67px" style="text-align: center; font-weight: 700;" TabIndex="3"></asp:TextBox>&nbsp;&nbsp;
<asp:TextBox ID="Name3" runat="server" ReadOnly="true" BorderStyle="None" Width="400px" style="font-size: medium"></asp:TextBox>&nbsp;&nbsp;
<asp:TextBox ID="Yob3" runat="server" ReadOnly="true" BorderStyle="None" Width="67px"></asp:TextBox>&nbsp;&nbsp;
<asp:TextBox ID="Age3" runat="server" ReadOnly="true" BorderStyle="None" Width="67px"></asp:TextBox><br />

<asp:TextBox ID="Leg4" runat="server" ReadOnly="true" BorderStyle="None" Width="67px" style="text-align: center"></asp:TextBox>&nbsp;&nbsp;
<asp:TextBox ID="Bib4" runat="server" BorderStyle="Outset" Width="67px" style="text-align: center; font-weight: 700;" TabIndex="4"></asp:TextBox>&nbsp;&nbsp;
<asp:TextBox ID="Name4" runat="server" ReadOnly="true" BorderStyle="None" Width="400px" style="font-size: medium"></asp:TextBox>&nbsp;&nbsp;
<asp:TextBox ID="Yob4" runat="server" ReadOnly="true" BorderStyle="None" Width="67px"></asp:TextBox>&nbsp;&nbsp;
<asp:TextBox ID="Age4" runat="server" ReadOnly="true" BorderStyle="None" Width="67px"></asp:TextBox><br /><br />

<asp:Button ID="UpdateButton" runat="server" Text="Breyta sveit" />
<asp:Button ID="InsNewButton" runat="server" Text="Skrá nýja sveit" />

    <br />
    <br />
    <asp:Literal ID="Literal1" runat="server" Text="Skráðar sveitir:"></asp:Literal>

    <br />
    <br />
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="mot,greinarnumer,lina" DataSourceID="CompetitorsRegisteredInEventDS" ForeColor="#333333" GridLines="None">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
            <asp:CommandField ShowSelectButton="True" />
            <asp:BoundField DataField="rasnumer" HeaderText="Rásnr" SortExpression="rasnumer" />
            <asp:BoundField DataField="ridillnumer" HeaderText="Riðill" SortExpression="ridillnumer" />
            <asp:BoundField DataField="stokkkastrod" HeaderText="Braut" SortExpression="stokkkastrod" />
            <asp:BoundField DataField="nafn" HeaderText="Heiti sveitar" SortExpression="nafn" />
            <asp:BoundField DataField="felag" HeaderText="Félag" SortExpression="felag" />
            <asp:BoundField DataField="seria" HeaderText="Skipan boðhlaupssveitar" SortExpression="seria" />
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
    <asp:SqlDataSource ID="CompetitorsRegisteredInEventDS" runat="server" ConnectionString="<%$ ConnectionStrings:AthleticsConnectionString %>" SelectCommand="CompetitorsInEvent" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:ControlParameter ControlID="CompCode" Name="CompetitionCode" PropertyName="Text" Type="String" />
            <asp:ControlParameter ControlID="EventLineNo" Name="EventNumber" PropertyName="Text" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>

<br /><br />
<asp:PlaceHolder ID="PH1" runat="server"></asp:PlaceHolder>
    <asp:GridView ID="AvailableRunners" runat="server" AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" DataSourceID="AvailableRelayRunners" ForeColor="#333333" GridLines="None">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
            <asp:BoundField DataField="rasnumer" HeaderText="Rásnúmer" SortExpression="rasnumer" />
            <asp:BoundField DataField="nafn" HeaderText="Nafn" SortExpression="nafn" />
            <asp:BoundField DataField="felag" HeaderText="Félag" SortExpression="felag" />
            <asp:BoundField DataField="faedingarar" HeaderText="F.ár" SortExpression="faedingarar" />
            <asp:BoundField DataField="aldurkeppanda" HeaderText="Aldur" SortExpression="aldurkeppanda" />
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





    <asp:SqlDataSource ID="AvailableRelayRunners" runat="server" ConnectionString="<%$ ConnectionStrings:AthleticsConnectionString %>" SelectCommand="GetAvailableRelayRunners" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:ControlParameter ControlID="CompCode" Name="CompCode" PropertyName="Text" Type="String" />
            <asp:ControlParameter ControlID="SelectClubDropDownL" Name="Club" PropertyName="SelectedValue" Type="String" />
            <asp:ControlParameter ControlID="EventLineNo" Name="EventLineNo" PropertyName="Text" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>





</asp:Content>
