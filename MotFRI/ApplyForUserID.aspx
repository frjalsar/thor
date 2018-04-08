<%@ Page Title="Umsókn um aðgang" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ApplyForUserID.aspx.cs" Inherits="MotFRI.ApplyForUserID" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <br />
    <asp:Label ID="Label1" runat="server" Text="Umsókn um aðgang að:&nbsp;&nbsp;&nbsp;  Þór - Mótaforriti Frjálsíþróttasambands Íslands" Font-Size="X-Large"></asp:Label>
    <br />
    <br />
    <br />

     <asp:Table ID="Table1" runat="server">
        <asp:TableRow>
            <asp:TableCell>
                <asp:Label ID="Label14" runat="server" Text="Umbeðið notendakenni" Font-Size="Large" Width="270px"></asp:Label>
            </asp:TableCell>
            <asp:TableCell>
                <asp:Label ID="Label15" runat="server" Text="*" ForeColor="#FF0066" Width="15px" Font-Size="X-Large"></asp:Label>
            </asp:TableCell>
            <asp:TableCell>
                <asp:TextBox ID="RequestedUserID" runat="server" Width="400px" Font-Size="Large"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>
                <asp:Label ID="Label16" runat="server" Text="Lykilorð" Font-Size="Large" Width="270px"></asp:Label>
            </asp:TableCell>
            <asp:TableCell>
                <asp:Label ID="Label17" runat="server" Text=" " ForeColor="#FF0066" Width="15px"></asp:Label>
            </asp:TableCell>
            <asp:TableCell>
               <asp:Label ID="Label18" runat="server" Text="- Lykilorð verður sent á tölvupóstfang -" Width="270px"></asp:Label>
            </asp:TableCell>
        </asp:TableRow>

        <asp:TableRow>
            <asp:TableCell>
                <asp:Label ID="Label5" runat="server" Text="Nafn" Font-Size="Large" Width="270px"></asp:Label>
            </asp:TableCell>
            <asp:TableCell>
                <asp:Label ID="Label8" runat="server" Text="*" ForeColor="#FF0066" Width="15px" Font-Size="X-Large"></asp:Label>
            </asp:TableCell>
            <asp:TableCell>
                <asp:TextBox ID="Name" runat="server" Width="400px" Font-Size="Large"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>
                <asp:Label ID="Label2" runat="server" Text="Tölvupóstfangið þitt" Font-Size="Large"></asp:Label>
            </asp:TableCell>
            <asp:TableCell>
                <asp:Label ID="Label9" runat="server" Text="*" ForeColor="#FF0066" Width="15px" Font-Size="X-Large"></asp:Label>
            </asp:TableCell>
            <asp:TableCell>
                <asp:TextBox ID="EmailAddress" runat="server" Width="400px" Font-Size="Large"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>
                <asp:Label ID="Label3" runat="server" Text="Tölvupóstfangið þitt endurtekið" Font-Size="Large"></asp:Label>
            </asp:TableCell>
            <asp:TableCell>
                <asp:Label ID="Label10" runat="server" Text="*" ForeColor="#FF0066" Width="15px" Font-Size="X-Large"></asp:Label>
            </asp:TableCell>
            <asp:TableCell>
                <asp:TextBox ID="EmailAddressAgain" runat="server" Width="400px" Font-Size="Large"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>
                <asp:Label ID="Label19" runat="server" Text="Sími" Font-Size="Large"></asp:Label>
            </asp:TableCell>
            <asp:TableCell>
                <asp:Label ID="Label20" runat="server" Text=" " ForeColor="#FF0066" Width="15px"></asp:Label>
            </asp:TableCell>
            <asp:TableCell>
                <asp:TextBox ID="TelephoneNo" runat="server" Font-Size="Large"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>

        <asp:TableRow>
            <asp:TableCell>
                <asp:Label ID="Label4" runat="server" Text="Félag 1" Font-Size="Large"></asp:Label>
            </asp:TableCell>
            <asp:TableCell>
                <asp:Label ID="Label11" runat="server" Text="*" ForeColor="#FF0066" Width="15px" Font-Size="X-Large"></asp:Label>
            </asp:TableCell>
            <asp:TableCell>
                <asp:TextBox ID="Club_1" runat="server" Font-Size="Large"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>
                <asp:Label ID="Label6" runat="server" Text="Félag 2" Font-Size="Large"></asp:Label>
            </asp:TableCell>
            <asp:TableCell>
                <asp:Label ID="Label12" runat="server" Text="" Width="15px"></asp:Label>
            </asp:TableCell>
            <asp:TableCell>
                <asp:TextBox ID="Club_2" runat="server" Font-Size="Large"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>
                <asp:Label ID="Label7" runat="server" Text="Félag 3" Font-Size="Large"></asp:Label>
            </asp:TableCell>
            <asp:TableCell>
                <asp:Label ID="Label13" runat="server" Text="" Width="15px"></asp:Label>
            </asp:TableCell>
            <asp:TableCell>
                <asp:TextBox ID="Club_3" runat="server" Font-Size="Large"></asp:TextBox>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    <br />
    <br />
    <asp:Label ID="ErrorMsg" runat="server" Font-Size="Large" Width="800px" BorderStyle="None" ForeColor="#FF0066"></asp:Label>
    <br />
    <br />
    <asp:Button ID="SendApplication" runat="server" Text="Sækja um" OnClick="SendApplication_Click" />&nbsp;&nbsp;&nbsp;<asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Umsókn um aðgang að Þór.pdf">Leiðbeiningar</asp:HyperLink>
    <br />
    
    </asp:Content>
