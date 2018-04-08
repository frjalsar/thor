<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Upplysingatafla.aspx.cs" Inherits="MotFRI.Upplysingatafla" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h1>Stjórnun upplýsingatöflu</h1><br /><asp:TextBox ID="CompCode" runat="server" ReadOnly="true" Visible="false"></asp:TextBox>&nbsp;
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
        <br /><br />

    <asp:GridView ID="ScoreboardGrid" runat="server" AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="mot,lina" DataSourceID="EventsForScoreboard" ForeColor="Black" GridLines="Horizontal" OnRowDataBound="ScoreboardGrid_RowDataBound" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px">
        <Columns>
            <asp:BoundField DataField="lina" HeaderText="lina" ReadOnly="True" SortExpression="lina" >
            <HeaderStyle Font-Size="X-Small" />
            <ItemStyle Font-Size="X-Small" />
            </asp:BoundField>
            <asp:BoundField DataField="timi" DataFormatString="{0:t}" HeaderText="Tími" SortExpression="timi" />
            <asp:BoundField DataField="heitigreinar" HeaderText="Heiti greinar" SortExpression="heitigreinar" >
            <ItemStyle Font-Size="X-Large" />
            </asp:BoundField>
            <asp:BoundField DataField="kyn" HeaderText="Kyn" SortExpression="kyn" />
            <asp:BoundField DataField="stadakeppni" HeaderText="Staða" SortExpression="stadakeppni" />
            <asp:BoundField DataField="FjKepp" HeaderText="Fj. kepp" SortExpression="FjKepp" />
            <asp:BoundField DataField="FjRidla" HeaderText="Fj. ri" SortExpression="FjRidla" />
            <asp:BoundField DataField="Val_fyrir_LedStudio" HeaderText="Á töflu" SortExpression="Val_fyrir_LedStudio" />
            <asp:HyperLinkField DataNavigateUrlFields="lina" DataNavigateUrlFormatString="~\UpdateLEDStudioSelection.aspx?EventLineNo={0}&SelectVal=1" 
                 HeaderText="Val1" Text="Allir K" />
            <asp:HyperLinkField DataNavigateUrlFields="lina" DataNavigateUrlFormatString="~\UpdateLEDStudioSelection.aspx?EventLineNo={0}&SelectVal=2" 
                 HeaderText="Val2" Text="Úrslit" />
            <asp:HyperLinkField DataNavigateUrlFields="lina" DataNavigateUrlFormatString="~\UpdateLEDStudioSelection.aspx?EventLineNo={0}&SelectVal=3" 
                 HeaderText="Val3" Text="Staða nú" />
            <asp:HyperLinkField DataNavigateUrlFields="lina" DataNavigateUrlFormatString="~\UpdateLEDStudioSelection.aspx?EventLineNo={0}&SelectVal=11" 
                 HeaderText="K1" Text="K1" />
            <asp:HyperLinkField DataNavigateUrlFields="lina" DataNavigateUrlFormatString="~\UpdateLEDStudioSelection.aspx?EventLineNo={0}&SelectVal=12" 
                 HeaderText="K2" Text="K2" />
            <asp:HyperLinkField DataNavigateUrlFields="lina" DataNavigateUrlFormatString="~\UpdateLEDStudioSelection.aspx?EventLineNo={0}&SelectVal=13" 
                 HeaderText="K3" Text="K3" />
            <asp:HyperLinkField DataNavigateUrlFields="lina" DataNavigateUrlFormatString="~\UpdateLEDStudioSelection.aspx?EventLineNo={0}&SelectVal=14" 
                 HeaderText="K4" Text="K4" />
            <asp:HyperLinkField DataNavigateUrlFields="lina" DataNavigateUrlFormatString="~\UpdateLEDStudioSelection.aspx?EventLineNo={0}&SelectVal=15" 
                 HeaderText="K5" Text="K5" />
            <asp:HyperLinkField DataNavigateUrlFields="lina" DataNavigateUrlFormatString="~\UpdateLEDStudioSelection.aspx?EventLineNo={0}&SelectVal=16" 
                 HeaderText="K6" Text="K6" />
            <asp:HyperLinkField DataNavigateUrlFields="lina" DataNavigateUrlFormatString="~\UpdateLEDStudioSelection.aspx?EventLineNo={0}&SelectVal=17" 
                 HeaderText="K7" Text="K7" />
            <asp:HyperLinkField DataNavigateUrlFields="lina" DataNavigateUrlFormatString="~\UpdateLEDStudioSelection.aspx?EventLineNo={0}&SelectVal=18" 
                 HeaderText="K8" Text="K8" />
            <asp:HyperLinkField DataNavigateUrlFields="lina" DataNavigateUrlFormatString="~\UpdateLEDStudioSelection.aspx?EventLineNo={0}&SelectVal=19" 
                 HeaderText="K9" Text="K9" />
            <asp:HyperLinkField DataNavigateUrlFields="lina" DataNavigateUrlFormatString="~\UpdateLEDStudioSelection.aspx?EventLineNo={0}&SelectVal=20" 
                 HeaderText="k10" Text="K10" />

            <asp:HyperLinkField DataNavigateUrlFields="lina" DataNavigateUrlFormatString="~\UpdateLEDStudioSelection.aspx?EventLineNo={0}&SelectVal=31" 
                 HeaderText="Ú1" Text="Ú1" />
            <asp:HyperLinkField DataNavigateUrlFields="lina" DataNavigateUrlFormatString="~\UpdateLEDStudioSelection.aspx?EventLineNo={0}&SelectVal=32" 
                 HeaderText="Ú2" Text="Ú2" />
            <asp:HyperLinkField DataNavigateUrlFields="lina" DataNavigateUrlFormatString="~\UpdateLEDStudioSelection.aspx?EventLineNo={0}&SelectVal=33" 
                 HeaderText="Ú3" Text="Ú3" />
            <asp:HyperLinkField DataNavigateUrlFields="lina" DataNavigateUrlFormatString="~\UpdateLEDStudioSelection.aspx?EventLineNo={0}&SelectVal=34" 
                 HeaderText="Ú4" Text="Ú4" />
            <asp:HyperLinkField DataNavigateUrlFields="lina" DataNavigateUrlFormatString="~\UpdateLEDStudioSelection.aspx?EventLineNo={0}&SelectVal=35" 
                 HeaderText="Ú5" Text="Ú5" />
            <asp:HyperLinkField DataNavigateUrlFields="lina" DataNavigateUrlFormatString="~\UpdateLEDStudioSelection.aspx?EventLineNo={0}&SelectVal=36" 
                 HeaderText="Ú6" Text="Ú6" />
            <asp:HyperLinkField DataNavigateUrlFields="lina" DataNavigateUrlFormatString="~\UpdateLEDStudioSelection.aspx?EventLineNo={0}&SelectVal=37" 
                 HeaderText="Ú7" Text="Ú7" />
            <asp:HyperLinkField DataNavigateUrlFields="lina" DataNavigateUrlFormatString="~\UpdateLEDStudioSelection.aspx?EventLineNo={0}&SelectVal=38" 
                 HeaderText="Ú8" Text="Ú8" />
            <asp:HyperLinkField DataNavigateUrlFields="lina" DataNavigateUrlFormatString="~\UpdateLEDStudioSelection.aspx?EventLineNo={0}&SelectVal=39" 
                 HeaderText="Ú9" Text="Ú9" />
            <asp:HyperLinkField DataNavigateUrlFields="lina" DataNavigateUrlFormatString="~\UpdateLEDStudioSelection.aspx?EventLineNo={0}&SelectVal=40" 
                 HeaderText="Ú10" Text="Ú10" />
        </Columns>
        <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
        <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
        <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F7F7F7" />
        <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
        <SortedDescendingCellStyle BackColor="#E5E5E5" />
        <SortedDescendingHeaderStyle BackColor="#242121" />
    </asp:GridView>
    <asp:SqlDataSource ID="EventsForScoreboard" runat="server" ConnectionString="<%$ ConnectionStrings:AthleticsConnectionString %>" SelectCommand="ScoreboardEvents" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:ControlParameter ControlID="CompCode" Name="CompetitionCode" PropertyName="Text" Type="String" />
            <asp:ControlParameter ControlID="EventStat" Name="EventStatus" PropertyName="SelectedValue" Type="String" />
            <asp:ControlParameter ControlID="GenderFilter" Name="GenderFilter" PropertyName="SelectedValue" Type="String" />
            <asp:ControlParameter ControlID="SelectDay" DbType="Date" Name="SelectedDate" PropertyName="SelectedValue" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>
