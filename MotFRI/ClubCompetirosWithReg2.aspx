<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ClubCompetirosWithReg2.aspx.cs" Inherits="MotFRI.ClubCompetirosWithReg2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:TextBox ID="CompCode" runat="server" Visible="false" ReadOnly="true"></asp:TextBox>
    <br />
    <asp:TextBox ID="CompetitionName" runat="server" style="font-weight: 700; font-size: large" Width="1231px" BorderStyle="None" ReadOnly="true"></asp:TextBox>
    <br />
    <asp:Label ID="SelectClubLbl" runat="server" Text="Veldu félag: "></asp:Label>&nbsp;
    <asp:DropDownList ID="SelectClub" runat="server" AutoPostBack="true" >
        <asp:ListItem Value="%">Öll félög</asp:ListItem>
    </asp:DropDownList><br /><br />
    <asp:GridView ID="ClubCompetitorsWithReg2" runat="server" AutoGenerateColumns="False" CellPadding="4" DataSourceID="ClubCompetitorsWithReg2DS" ForeColor="#333333" GridLines="None" OnRowDataBound="ClubCompetitorsWithReg2_RowDataBound">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:BoundField DataField="LineType" HeaderText="LineType" SortExpression="LineType" />
            <asp:BoundField DataField="AgeGroupDescription" HeaderText="Aldursflokkur" SortExpression="AgeGroupDescription" />
            <asp:BoundField DataField="BibNo" HeaderText="Rásnr." SortExpression="BibNo" />
            <asp:BoundField DataField="CompetitorName" HeaderText="Nafn" SortExpression="CompetitorName" />
            <asp:BoundField DataField="CompetitorYearOfBirth" HeaderText="F.ár" SortExpression="CompetitorYearOfBirth" />
            <asp:BoundField DataField="CompetitorAge" HeaderText="Aldur" SortExpression="CompetitorAge" />
            <asp:BoundField DataField="CompetitorClub" HeaderText="Félag" SortExpression="CompetitorClub" />
            <asp:BoundField DataField="Event01" HeaderText="Gr.1" SortExpression="Event01" />
            <asp:BoundField DataField="Event02" HeaderText="Gr.2" SortExpression="Event02" />
            <asp:BoundField DataField="Event03" HeaderText="Gr.3" SortExpression="Event03" />
            <asp:BoundField DataField="Event04" HeaderText="Gr.4" SortExpression="Event04" />
            <asp:BoundField DataField="Event05" HeaderText="Gr.5" SortExpression="Event05" />
            <asp:BoundField DataField="Event06" HeaderText="Gr.6" SortExpression="Event06" />
            <asp:BoundField DataField="Event07" HeaderText="Gr.7" SortExpression="Event07" />
            <asp:BoundField DataField="Event08" HeaderText="Gr.8" SortExpression="Event08" />
            <asp:BoundField DataField="Event09" HeaderText="Gr.9" SortExpression="Event09" />
            <asp:BoundField DataField="Event10" HeaderText="Gr.10" SortExpression="Event10" />
            <asp:BoundField DataField="Event11" HeaderText="Gr.11" SortExpression="Event11" />
            <asp:BoundField DataField="Event12" HeaderText="Gr.12" SortExpression="Event12" />
            <asp:BoundField DataField="Event13" HeaderText="Gr.13" SortExpression="Event13" />
            <asp:BoundField DataField="Event14" HeaderText="Gr.14" SortExpression="Event14" />
            <asp:BoundField DataField="Event15" HeaderText="Gr.15" SortExpression="Event15" />
            <asp:BoundField DataField="Event16" HeaderText="Gr.16" SortExpression="Event16" />
            <asp:BoundField DataField="Event17" HeaderText="Gr.17" SortExpression="Event17" />
            <asp:BoundField DataField="Event18" HeaderText="Gr.18" SortExpression="Event18" />
            <asp:BoundField DataField="Event19" HeaderText="Gr.19" SortExpression="Event19" />
            <asp:BoundField DataField="Event20" HeaderText="Gr.20" SortExpression="Event20" />
            <asp:BoundField DataField="Event21" HeaderText="Gr.21" SortExpression="Event21" />
            <asp:BoundField DataField="Event22" HeaderText="Gr.22" SortExpression="Event22" />
            <asp:BoundField DataField="Event23" HeaderText="Gr.23" SortExpression="Event23" />
            <asp:BoundField DataField="Event24" HeaderText="Gr.24" SortExpression="Event24" />
            <asp:BoundField DataField="Event25" HeaderText="Gr.25" SortExpression="Event25" />
            <asp:BoundField DataField="Event26" HeaderText="Gr.26" SortExpression="Event26" />
            <asp:BoundField DataField="Event27" HeaderText="Gr.27" SortExpression="Event27" />
            <asp:BoundField DataField="Event28" HeaderText="Gr.28" SortExpression="Event28" />
            <asp:BoundField DataField="Event29" HeaderText="Gr.29" SortExpression="Event29" />
            <asp:BoundField DataField="Event30" HeaderText="Gr.30" SortExpression="Event30" />
            <asp:BoundField DataField="EventLine01" HeaderText="EventLine01" SortExpression="EventLine01" />
            <asp:BoundField DataField="EventLine02" HeaderText="EventLine02" SortExpression="EventLine02" />
            <asp:BoundField DataField="EventLine03" HeaderText="EventLine03" SortExpression="EventLine03" />
            <asp:BoundField DataField="EventLine04" HeaderText="EventLine04" SortExpression="EventLine04" />
            <asp:BoundField DataField="EventLine05" HeaderText="EventLine05" SortExpression="EventLine05" />
            <asp:BoundField DataField="EventLine06" HeaderText="EventLine06" SortExpression="EventLine06" />
            <asp:BoundField DataField="EventLine07" HeaderText="EventLine07" SortExpression="EventLine07" />
            <asp:BoundField DataField="EventLine08" HeaderText="EventLine08" SortExpression="EventLine08" />
            <asp:BoundField DataField="EventLine09" HeaderText="EventLine09" SortExpression="EventLine09" />
            <asp:BoundField DataField="EventLine10" HeaderText="EventLine10" SortExpression="EventLine10" />
            <asp:BoundField DataField="EventLine11" HeaderText="EventLine11" SortExpression="EventLine11" />
            <asp:BoundField DataField="EventLine12" HeaderText="EventLine12" SortExpression="EventLine12" />
            <asp:BoundField DataField="EventLine13" HeaderText="EventLine13" SortExpression="EventLine13" />
            <asp:BoundField DataField="EventLine14" HeaderText="EventLine14" SortExpression="EventLine14" />
            <asp:BoundField DataField="EventLine15" HeaderText="EventLine15" SortExpression="EventLine15" />
            <asp:BoundField DataField="EventLine16" HeaderText="EventLine16" SortExpression="EventLine16" />
            <asp:BoundField DataField="EventLine17" HeaderText="EventLine17" SortExpression="EventLine17" />
            <asp:BoundField DataField="EventLine18" HeaderText="EventLine18" SortExpression="EventLine18" />
            <asp:BoundField DataField="EventLine19" HeaderText="EventLine19" SortExpression="EventLine19" />
            <asp:BoundField DataField="EventLine20" HeaderText="EventLine20" SortExpression="EventLine20" />
            <asp:BoundField DataField="EventLine21" HeaderText="EventLine21" SortExpression="EventLine21" />
            <asp:BoundField DataField="EventLine22" HeaderText="EventLine22" SortExpression="EventLine22" />
            <asp:BoundField DataField="EventLine23" HeaderText="EventLine23" SortExpression="EventLine23" />
            <asp:BoundField DataField="EventLine24" HeaderText="EventLine24" SortExpression="EventLine24" />
            <asp:BoundField DataField="EventLine25" HeaderText="EventLine25" SortExpression="EventLine25" />
            <asp:BoundField DataField="EventLine26" HeaderText="EventLine26" SortExpression="EventLine26" />
            <asp:BoundField DataField="EventLine27" HeaderText="EventLine27" SortExpression="EventLine27" />
            <asp:BoundField DataField="EventLine28" HeaderText="EventLine28" SortExpression="EventLine28" />
            <asp:BoundField DataField="EventLine29" HeaderText="EventLine29" SortExpression="EventLine29" />
            <asp:BoundField DataField="EventLine30" HeaderText="EventLine30" SortExpression="EventLine30" />
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

    <asp:SqlDataSource ID="ClubCompetitorsWithReg2DS" runat="server" ConnectionString="<%$ ConnectionStrings:AthleticsConnectionString %>" SelectCommand="AgeGroupEventsForRegistration" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:ControlParameter ControlID="CompCode" Name="CompCode" PropertyName="Text" Type="String" />
            <asp:ControlParameter ControlID="SelectClub" Name="SelectedClub" PropertyName="SelectedValue" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>

</asp:Content>
