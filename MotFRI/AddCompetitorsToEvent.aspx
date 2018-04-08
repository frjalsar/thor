<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddCompetitorsToEvent.aspx.cs" Inherits="MotFRI.AddCompetitorsToEvent" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:TextBox ID="CompCode" runat="server" Visible="false" ReadOnly="false"></asp:TextBox>
        <asp:TextBox ID="EventLineNo" runat="server" Visible="false" ReadOnly="false"></asp:TextBox>
        <asp:TextBox ID="Gender" runat="server" Visible="false" ReadOnly="false"></asp:TextBox>
        <asp:TextBox ID="AgeTo" runat="server" Visible="false" ReadOnly="false"></asp:TextBox>
        <br />
        <asp:Label ID="Label1" runat="server" Text="Bæta við keppendum í grein" Font-Names="Arial"></asp:Label>
        <br /><br />
        <asp:TextBox ID="EventName" runat="server" BorderStyle="None" Font-Bold="True" Font-Italic="True" Font-Names="Arial" Width="1179px"></asp:TextBox>
        <br />
        <asp:GridView ID="Candidates" runat="server" AutoGenerateColumns="False" CellPadding="4" DataSourceID="CandidatesForEvent" ForeColor="#333333" GridLines="None" Font-Names="Arial" AllowSorting="True">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                               
                 <asp:TemplateField HeaderText="Velja">
                     <ItemTemplate>
                        <asp:CheckBox ID="ValinChk" runat="server" /> 
                     </ItemTemplate>
                 </asp:TemplateField>

                <asp:BoundField DataField="rasnumer" HeaderText="Rásnúmer" SortExpression="rasnumer">
                <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField DataField="nafn" HeaderText="Nafn" SortExpression="nafn">
                <HeaderStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="felag" HeaderText="Félag" SortExpression="felag">
                <HeaderStyle HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="faedingardagur" HeaderText="Fæð.dagur" ReadOnly="True" SortExpression="faedingardagur" />
                 <asp:BoundField DataField="aldurkeppanda" HeaderText="Aldur" SortExpression="aldurkeppanda" />
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
        <asp:SqlDataSource ID="CandidatesForEvent" runat="server" ConnectionString="<%$ ConnectionStrings:AthleticsConnectionString %>" SelectCommand="ReturnNewCandidatesForEvent" SelectCommandType="StoredProcedure">
            <SelectParameters>
                <asp:ControlParameter ControlID="CompCode" Name="CompCode" PropertyName="Text" Type="String" />
                <asp:ControlParameter ControlID="EventLineNo" Name="EventLineNo" PropertyName="Text" Type="String" />
                <asp:ControlParameter ControlID="Gender" Name="Gender" PropertyName="Text" Type="Int32" />
                <asp:ControlParameter ControlID="AgeTo" Name="AgeTo" PropertyName="Text" Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>
        <asp:Button ID="SelectButton" runat="server" Text="Velja" OnClick="SelectButton_Click" />&nbsp;&nbsp;&nbsp;<asp:Button ID="CancelButton" runat="server" Text="Hætta við" OnClick="CancelButton_Click" />
    </div>
    </form>
</body>
</html>
