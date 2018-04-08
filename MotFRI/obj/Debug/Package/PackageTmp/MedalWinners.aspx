<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MedalWinners.aspx.cs" Inherits="MotFRI.MedalWinners" %>
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
    <asp:TextBox ID="CompCode" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="ClubCode" runat="server" Visible="false"></asp:TextBox><br />
    <asp:TextBox ID="CompName" runat="server" ReadOnly="true" BorderStyle="None" Height="41px" style="font-size: xx-large" Width="1336px"></asp:TextBox><br /><br /><br />
    <asp:FormView ID="ClubNam" runat="server" DataSourceID="GetClubName" Font-Bold="True" Font-Size="X-Large">
        <EditItemTemplate>
            Heiti félags:
            <asp:TextBox ID="Heiti_félagsTextBox" runat="server" Text='<%# Bind("[Heiti félags]") %>' />
            <br />
            <asp:LinkButton ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update" Text="Update" />
            &nbsp;<asp:LinkButton ID="UpdateCancelButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" />
        </EditItemTemplate>
        <InsertItemTemplate>
            Heiti félags:
            <asp:TextBox ID="Heiti_félagsTextBox" runat="server" Text='<%# Bind("[Heiti félags]") %>' />
            <br />
            <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert" Text="Insert" />
            &nbsp;<asp:LinkButton ID="InsertCancelButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" />
        </InsertItemTemplate>
        <ItemTemplate>
            Heiti félags:
            <asp:Label ID="Heiti_félagsLabel" runat="server" Text='<%# Bind("[Heiti félags]") %>' />
            <br />

        </ItemTemplate>
    </asp:FormView>
    <asp:SqlDataSource ID="GetClubName" runat="server" ConnectionString="<%$ ConnectionStrings:AthleticsConnectionString %>" SelectCommand="ReturnClubName" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:ControlParameter ControlID="ClubCode" Name="ClubCode" PropertyName="Text" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
    <br />
    <asp:TextBox ID="PageDescription" runat="server" ReadOnly="true" BorderStyle="None" Height="41px" style="font-size: large" Width="1336px" ></asp:TextBox>
    <asp:Label ID="TypeOfMedalLbl" runat="server" Text="Tegund verðlauna: " style="font-size: x-large"></asp:Label>&nbsp;&nbsp;<asp:DropDownList ID="TypeOfMedal" runat="server" style="font-size: x-large" AutoPostBack="True">
        <asp:ListItem Value="0">Öll verðlaun</asp:ListItem>
        <asp:ListItem Value="1">Gullverðlaun</asp:ListItem>
        <asp:ListItem Value="2">Silfurverðlaun</asp:ListItem>
        <asp:ListItem Value="3">Bronsverðlaun</asp:ListItem>
    </asp:DropDownList>&nbsp;&nbsp;&nbsp;
    <asp:Label ID="SelectDayLbl" runat="server" Text="Veldu dag: " style="font-size: x-large"></asp:Label>&nbsp;&nbsp;
    <asp:DropDownList ID="SelectDayDropDownList" runat="server" style="font-size: x-large" AutoPostBack="True"></asp:DropDownList>
    <br /><br /><br />
    <asp:GridView ID="MedalWinnersGW" runat="server" AutoGenerateColumns="False" CellPadding="4" DataSourceID="MedalWinnersDS" ForeColor="#333333" GridLines="None">
        <AlternatingRowStyle BackColor="White" />
        <Columns>

            <asp:BoundField DataField="Place" HeaderText="Verðlaun" SortExpression="Place" >
                            <HeaderStyle Font-Size="X-Large" HorizontalAlign="Left" />
            <ItemStyle Font-Size="Large" />
            </asp:BoundField>

            <asp:BoundField DataField="Name" HeaderText="Nafn" SortExpression="Name">
            <HeaderStyle Font-Size="X-Large" HorizontalAlign="Left" />
            <ItemStyle Font-Size="Large" />
            </asp:BoundField>
            <asp:BoundField DataField="Club" HeaderText="Félag" SortExpression="Club" >
                            <HeaderStyle Font-Size="X-Large" HorizontalAlign="Left" />
            <ItemStyle Font-Size="Large" />
            </asp:BoundField>

            <asp:BoundField DataField="EventName" HeaderText="Keppnisgrein" SortExpression="EventName" >
                            <HeaderStyle Font-Size="X-Large" HorizontalAlign="Left" />
            <ItemStyle Font-Size="Large" />
            </asp:BoundField>

            <asp:BoundField DataField="Performance" HeaderText="Árangur" SortExpression="Performance" >
                            <HeaderStyle Font-Size="X-Large" HorizontalAlign="Left" />
            <ItemStyle Font-Size="Large" />
            </asp:BoundField>

            <asp:BoundField DataField="EventDate" HeaderText="Dagsetn." SortExpression="EventDate" DataFormatString="{0:d.MM.yyyy}" >
                            <HeaderStyle Font-Size="X-Large" HorizontalAlign="Left" />
            <ItemStyle Font-Size="Large" />
            </asp:BoundField>

            <asp:BoundField DataField="EventLineNo" HeaderText="EventLineNo" SortExpression="EventLineNo" Visible="False" >
                            <HeaderStyle Font-Size="X-Large" HorizontalAlign="Left" />
            <ItemStyle Font-Size="Large" />
            </asp:BoundField>

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
    <asp:SqlDataSource ID="MedalWinnersDS" runat="server" ConnectionString="<%$ ConnectionStrings:AthleticsConnectionString %>" SelectCommand="MedalWinnersPrClub" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:ControlParameter ControlID="CompCode" Name="CompCode" PropertyName="Text" Type="String" />
            <asp:ControlParameter ControlID="ClubCode" Name="ClubCode" PropertyName="Text" Type="String" />
            <asp:ControlParameter ControlID="TypeOfMedal" Name="MedalType" PropertyName="SelectedValue" Type="Int32" />
            <asp:ControlParameter ControlID="SelectDayDropDownList" Name="SelectDate" PropertyName="SelectedValue" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>
