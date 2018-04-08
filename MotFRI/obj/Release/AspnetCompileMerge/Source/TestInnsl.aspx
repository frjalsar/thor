<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TestInnsl.aspx.cs" Inherits="MotFRI.TestInnsl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <p>
        <br />
    </p>
    <p>
        <asp:TextBox ID="StdTextBox" runat="server" Width="468px"></asp:TextBox>
    </p>
    <br />
    <asp:GridView ID="StandardTextGridView" runat="server" AutoGenerateColumns="False"  
        ShowFooter="True" DataSourceID="StdTxt" 
        onselectedindexchanged="GridView1_SelectedIndexChanged" 
        DataKeyNames="Code" AllowSorting="True" CellPadding="4" 
        ForeColor="#333333" GridLines="None" Width="548px" 
        onrowdatabound="StandardTextGridView_RowDataBound" >
       
             <AlternatingRowStyle BackColor="White" />
             <Columns>
                 <asp:BoundField DataField="Code" HeaderText="Code" ReadOnly="True" 
                     SortExpression="Code" />
                 <asp:TemplateField HeaderText="Description" SortExpression="Description">
                     <EditItemTemplate>
                         <asp:TextBox ID="DescriptionTextBox" runat="server" Text='<%# Bind("Description") %>'></asp:TextBox>
                     </EditItemTemplate>
                     <ItemTemplate>
                         <asp:Label ID="Label1" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="TxtEditableDescr">
                     <ItemTemplate>
    <asp:TextBox ID="TxtFirstName" runat="server" width="100%" Text='<%# Bind("Description") %>'> HeaderText="TxtFirstName" </asp:TextBox>
     
</ItemTemplate>
                 </asp:TemplateField>               
                 
                   <asp:TemplateField HeaderText="YourColumn" InsertVisible="False" SortExpression="YourColumn">
                    <EditItemTemplate>
                        <asp:Label ID="Label105" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:Button ID="UpdateDescr" runat="server" Text="Update Descr Fld" OnClick="UpdBtn" />
                    </FooterTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label106" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                 
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
    <asp:SqlDataSource ID="StdTxt" runat="server" 
        ConnectionString="<%$ ConnectionStrings:AthleticsConnectionString %>" 
        SelectCommand="SELECT * FROM [Athl$Standard Text]"></asp:SqlDataSource>
    <br />
</asp:Content>
