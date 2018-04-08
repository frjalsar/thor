<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AthleticsLogon.aspx.cs" Inherits="MotFRI.AthleticsLogon" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <asp:Label ID="Label1" runat="server" Text="Innskráning" Font-Size="X-Large"></asp:Label>
    <br />
    <asp:TextBox ID="SavedPassword" runat="server" Visible="false"></asp:TextBox>
    <br />
    <br />
    <asp:Table ID="LoginTable" runat="server" Width="502px">
        <asp:TableRow>
            <asp:TableCell><asp:Label ID="Label2" runat="server" Text="Notandi" Width="100px"></asp:Label></asp:TableCell>
            <asp:TableCell><asp:TextBox ID="UserName" runat="server" Width="250px" TabIndex="1"></asp:TextBox></asp:TableCell>  
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell><asp:Label ID="Label3" runat="server" Text="Lykilorð"></asp:Label></asp:TableCell>
            <asp:TableCell><asp:TextBox ID="Password" TextMode="Password" runat="server" Width="250px" TabIndex="2"></asp:TextBox></asp:TableCell>
            <asp:TableCell><asp:TextBox ID="AccessLevel" runat="server" Width="300px" BorderStyle="None" ReadOnly="true"></asp:TextBox></asp:TableCell>                  
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>    
                <asp:Button ID="LogInButton" runat="server" Text="Innskráning" OnClick="LoginButton_Click" />
                <asp:Button ID="LogOutButton" runat="server" Text="Útskráning" OnClick="LogOutButton_Click"  />
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>
                    <asp:Button ID="BackButton" runat="server" Text="Til baka" OnClick="BackButton_Click" />
            </asp:TableCell> 
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>&nbsp;</asp:TableCell>
            <asp:TableCell>&nbsp;</asp:TableCell>       
            <asp:TableCell HorizontalAlign="Center">
                <asp:GridView ID="AccessToClubs" runat="server" DataSourceID="UserClubs" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None">
                    <AlternatingRowStyle BackColor="White"></AlternatingRowStyle>
                    <Columns>
                        <asp:BoundField DataField="Club" HeaderText="F&#233;lag" SortExpression="Club"></asp:BoundField>
                        <asp:BoundField DataField="NameOfClub" HeaderText="Heiti f&#233;lags" ReadOnly="True" SortExpression="NameOfClub"></asp:BoundField>
                        <asp:BoundField DataField="H&#233;ra&#240;ssamband" HeaderText="H&#233;ra&#240;ssamband" SortExpression="H&#233;ra&#240;ssamband"></asp:BoundField>
                    </Columns>
                    <EditRowStyle BackColor="#2461BF"></EditRowStyle>

                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White"></FooterStyle>

                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White"></HeaderStyle>

                    <PagerStyle HorizontalAlign="Center" BackColor="#2461BF" ForeColor="White"></PagerStyle>

                    <RowStyle BackColor="#EFF3FB"></RowStyle>

                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333"></SelectedRowStyle>

                    <SortedAscendingCellStyle BackColor="#F5F7FB"></SortedAscendingCellStyle>

                    <SortedAscendingHeaderStyle BackColor="#6D95E1"></SortedAscendingHeaderStyle>

                    <SortedDescendingCellStyle BackColor="#E9EBEF"></SortedDescendingCellStyle>

                    <SortedDescendingHeaderStyle BackColor="#4870BE"></SortedDescendingHeaderStyle>
                </asp:GridView>
                <asp:SqlDataSource runat="server" ID="UserClubs" ConnectionString='<%$ ConnectionStrings:AthleticsConnectionString %>' SelectCommand="ClubsForUser" SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="UserName" PropertyName="Text" Name="UserLogin" Type="String"></asp:ControlParameter>
                    </SelectParameters>
                </asp:SqlDataSource>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
        
        <br />
        <br />
    <asp:TextBox ID="Message" runat="server" ReadOnly="true" Width="843px" BorderStyle="None" ForeColor="Red" Height="17px"></asp:TextBox>
        <br />
        <br />


</asp:Content>
