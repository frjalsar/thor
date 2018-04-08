<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FyrirSjonvarp.aspx.cs" Inherits="MotFRI.FyrirSjonvarp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <h1>Valmynd fyrir Sjónvarpið</h1><br /><asp:TextBox ID="CompCode" runat="server" ReadOnly="true" Visible="false"></asp:TextBox>&nbsp;
        <asp:TextBox ID="CompName" runat="server" Width="1327px" ReadOnly="true" Font-Size="X-Large"  BorderStyle="None"></asp:TextBox>
<br /><br />
    <asp:Label ID="Label1" runat="server" Text="Afmarka við stöðu keppni: "></asp:Label><asp:DropDownList ID="EventStat" runat="server" AutoPostBack="true">
        <asp:ListItem Value="%">Sýna allar greinar</asp:ListItem>
        <asp:ListItem Value="1">Aðeins keppni stendur yfir</asp:ListItem>
        <asp:ListItem Value="2">Aðeins keppni lokið</asp:ListItem>
        <asp:ListItem Value="0">Aðeins keppni ekki hafin</asp:ListItem>
    </asp:DropDownList>&nbsp;&nbsp;
    <asp:Label ID="Label2" runat="server" Text="Afmarka við kyn: "></asp:Label>
    <asp:DropDownList ID="GenderFilter" runat="server" AutoPostBack="true">
        <asp:ListItem Value="%">Bæði kyn</asp:ListItem>
        <asp:ListItem Value="1">Aðeins karlagreinar</asp:ListItem>
        <asp:ListItem Value="2">Aðeins kvennagreinar</asp:ListItem>
    </asp:DropDownList>&nbsp;&nbsp;
    <asp:Label ID="SelDayLabel" runat="server" Text="Afmarka við dag: "></asp:Label>
    <asp:DropDownList ID="SelectDay" runat="server" AutoPostBack="True"></asp:DropDownList>
        <br />
    <br />
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="mot,lina" DataSourceID="EventsForTV" ForeColor="#333333" GridLines="None" OnRowDataBound="GridView1_RowDataBound">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
            <asp:BoundField DataField="lina" HeaderText="lina" ReadOnly="True" SortExpression="lina" />
            <asp:BoundField DataField="dagsetning" DataFormatString="{0:d}" HeaderText="Dagsetning" SortExpression="dagsetning" />
            <asp:BoundField DataField="timi" DataFormatString="{0:t}" HeaderText="Tími" SortExpression="timi" />
            <asp:BoundField DataField="heitigreinar" HeaderText="Heiti greinar" SortExpression="heitigreinar" />
            <asp:BoundField DataField="kyn" HeaderText="Kyn" SortExpression="kyn" />
            <asp:BoundField DataField="stadakeppni" HeaderText="Staða keppni" SortExpression="stadakeppni" />
            <asp:BoundField DataField="FjKepp" HeaderText="Fj. keppenda" SortExpression="FjKepp" />
            <asp:BoundField DataField="FjRidla" HeaderText="Fj. riðla" SortExpression="FjRidla" />
            <asp:BoundField DataField="ValFyrirSjonvarp" HeaderText="Val f. Sjónvarp" SortExpression="ValFyrirSjonvarp" />
            <asp:HyperLinkField DataNavigateUrlFields="lina" DataNavigateUrlFormatString="~\UpdateTVSelection.aspx?EventLineNo={0}&SelectVal=1" 
                 HeaderText="Val1" Text="Keppendur" />
            <asp:HyperLinkField DataNavigateUrlFields="lina" DataNavigateUrlFormatString="~\UpdateTVSelection.aspx?EventLineNo={0}&SelectVal=2" 
                 HeaderText="Val2" Text="Úrslit" />
            <asp:HyperLinkField DataNavigateUrlFields="lina" DataNavigateUrlFormatString="~\UpdateTVSelection.aspx?EventLineNo={0}&SelectVal=3" 
                 HeaderText="Val3" Text="Staðan nú" />
            <asp:HyperLinkField DataNavigateUrlFields="lina" DataNavigateUrlFormatString="~\UpdateTVSelection.aspx?EventLineNo={0}&SelectVal=11" 
                 HeaderText="Val4" Text="Keppendur R1" />
            <asp:HyperLinkField DataNavigateUrlFields="lina" DataNavigateUrlFormatString="~\UpdateTVSelection.aspx?EventLineNo={0}&SelectVal=12" 
                 HeaderText="Val5" Text="Keppendur R2" />            
            <asp:HyperLinkField DataNavigateUrlFields="lina" DataNavigateUrlFormatString="~\UpdateTVSelection.aspx?EventLineNo={0}&SelectVal=21" 
                 HeaderText="Val6" Text="Úrslit R1" />
            <asp:HyperLinkField DataNavigateUrlFields="lina" DataNavigateUrlFormatString="~\UpdateTVSelection.aspx?EventLineNo={0}&SelectVal=22" 
                 HeaderText="Val7" Text="Úrslit R2" />
 
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
    <asp:SqlDataSource ID="EventsForTV" runat="server" ConnectionString="<%$ ConnectionStrings:AthleticsConnectionString %>" SelectCommand="TVEvents" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:ControlParameter ControlID="CompCode" Name="CompetitionCode" PropertyName="Text" Type="String" />
            <asp:ControlParameter ControlID="EventStat" Name="EventStatus" PropertyName="SelectedValue" Type="String" />
            <asp:ControlParameter ControlID="GenderFilter" Name="GenderFilter" PropertyName="SelectedValue" Type="String" />
            <asp:ControlParameter ControlID="SelectDay" DbType="Date" Name="SelectedDate" PropertyName="SelectedValue" />
        </SelectParameters>
    </asp:SqlDataSource>
        <br /><br />


</asp:Content>
