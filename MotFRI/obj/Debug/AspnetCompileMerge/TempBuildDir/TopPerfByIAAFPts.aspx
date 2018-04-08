<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TopPerfByIAAFPts.aspx.cs" Inherits="MotFRI.TopPerfByIAAFPts" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <asp:TextBox ID="CompCode" runat="server" Visible="false" ReadOnly="true"></asp:TextBox><br />
    <asp:TextBox ID="CompName" runat="server" Height="70px" style="font-size: xx-large" Width="1369px" ReadOnly="true" BorderStyle="None"></asp:TextBox><br /><br />
    <asp:Label ID="Label2" runat="server" Text="Besti árangur skv. stigatöflu IAAF" style="font-size: x-large"></asp:Label><br /><br />
    <asp:Label ID="Label1" runat="server" Text="Veldu kyn: " style="font-size: large"></asp:Label>&nbsp;&nbsp; <asp:DropDownList ID="SelectGender" runat="server" style="font-size: large" AutoPostBack="True">
        <asp:ListItem Value="%">Bæði karlar og konur</asp:ListItem>
        <asp:ListItem Value="1">Karlar</asp:ListItem>
        <asp:ListItem Value="2">Konur</asp:ListItem>
    </asp:DropDownList><br /><br />


    <asp:GridView ID="TopIAAFPoints" runat="server" AutoGenerateColumns="False" CellPadding="4" DataSourceID="CompTopIAAFPts" ForeColor="#333333" GridLines="None">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:BoundField DataField="IAAF Stig" HeaderText="IAAF Stig" SortExpression="IAAF Stig" />
            <asp:BoundField DataField="arangur" HeaderText="Árangur" SortExpression="arangur" />
            <asp:BoundField DataField="nafn" HeaderText="Nafn" SortExpression="nafn" />
            <asp:BoundField DataField="felag" HeaderText="Félag" SortExpression="felag" />
            <asp:BoundField DataField="Column1" HeaderText="Grein" ReadOnly="True" SortExpression="Column1" />
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
    <asp:SqlDataSource ID="CompTopIAAFPts" runat="server" ConnectionString="<%$ ConnectionStrings:AthleticsConnectionString %>" SelectCommand="TopPerformanceByIAAFPoints" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:ControlParameter ControlID="CompCode" Name="CompCode" PropertyName="Text" Type="String" />
            <asp:ControlParameter ControlID="SelectGender" Name="GenderFilter" PropertyName="SelectedValue" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>
