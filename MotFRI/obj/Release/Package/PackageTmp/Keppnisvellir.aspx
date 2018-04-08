<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Keppnisvellir.aspx.cs" Inherits="MotFRI.Keppnisvellir" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <asp:Button ID="NyrVollur" runat="server" Text="Skrá nýjan völl" 
        PostBackUrl="~/SkraNyjanVoll.aspx" onclick="NyrVollur_Click"  /> <br /><br />
    <asp:GridView ID="KeppnisvellirGridView" runat="server" AllowSorting="True" 
        DataSourceID="Vellir" AutoGenerateColumns="False" 
        DataKeyNames="Heiti_vallar" CellPadding="4" ForeColor="#333333" 
        GridLines="None" onselectedindexchanged="GridView1_SelectedIndexChanged">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:CommandField SelectText="Velja" ShowSelectButton="True" 
                EditImageUrl="~/Img/Custom-Icon-Design-Pretty-Office-10-Pencil.jpg" 
                NewImageUrl="~/Img/plus.gif" NewText="Nýr" SelectImageUrl="~/Img/select.jpg" 
                ShowDeleteButton="True" ShowEditButton="True" UpdateText="Breyta" />
            <asp:BoundField DataField="Heiti_vallar" HeaderText="Heiti" ReadOnly="True" 
                SortExpression="Heiti_vallar" />
            <asp:BoundField DataField="Staður" HeaderText="Staður" 
                SortExpression="Staður" />
            <asp:BoundField DataField="Úti_Inni" HeaderText="Úti/Inni" 
            

                SortExpression="Úti_Inni" />
            <asp:BoundField DataField="Fjöldi_beinna_brauta" 
                HeaderText="Fjöldi beinna brauta" SortExpression="Fjöldi_beinna_brauta" />
            <asp:BoundField DataField="Fj_ hringbrauta spretthlaup" 
                HeaderText="Fj. hringbrauta" SortExpression="Fj_ hringbrauta spretthlaup" />
            <asp:CommandField CancelText="Hætta við" DeleteImageUrl="~/Img/deletered.jpg" 
                DeleteText="Eyða" 
                EditImageUrl="~/Img/Custom-Icon-Design-Pretty-Office-10-Pencil.jpg" 
                EditText="Breyta" ShowDeleteButton="True" />
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
<br />
    <asp:SqlDataSource ID="Vellir" runat="server" 
        ConflictDetection="CompareAllValues" 
        ConnectionString="<%$ ConnectionStrings:AthleticsConnectionString %>" 
        DeleteCommand="DELETE FROM [Mot$Keppnisvöllur] WHERE [Heiti vallar] = @original_Heiti_vallar AND [Staður] = @original_Staður AND [Úti_Inni] = @original_Úti_Inni AND [Fjöldi brauta] = @original_Fjöldi_brauta AND [Fjöldi beinna brauta] = @original_Fjöldi_beinna_brauta" 
        InsertCommand="INSERT INTO [Mot$Keppnisvöllur] ([Heiti vallar], [Staður], [Úti_Inni], [Fjöldi brauta], [Fjöldi beinna brauta]) VALUES (@Heiti_vallar, @Staður, @Úti_Inni, @Fjöldi_brauta, @Fjöldi_beinna_brauta)" 
        OldValuesParameterFormatString="original_{0}" 
        SelectCommand="SELECT Heiti AS Heiti_vallar, Staður, 
          Úti_Inni = Case Úti_Inni  
	    WHEN 0 THEN 'Úti'
        WHEN 1 THEN 'Inni'
        END
          , [Fjöldi beinna brauta] AS Fjöldi_beinna_brauta, [Fj_ hringbrauta spretthlaup] FROM Athl$Keppnisvöllur" 
        
        UpdateCommand="UPDATE [Mot$Keppnisvöllur] SET [Staður] = @Staður, [Úti_Inni] = @Úti_Inni, [Fjöldi brauta] = @Fjöldi_brauta, [Fjöldi beinna brauta] = @Fjöldi_beinna_brauta WHERE [Heiti vallar] = @original_Heiti_vallar AND [Staður] = @original_Staður AND [Úti_Inni] = @original_Úti_Inni AND [Fjöldi brauta] = @original_Fjöldi_brauta AND [Fjöldi beinna brauta] = @original_Fjöldi_beinna_brauta">
        <DeleteParameters>
            <asp:Parameter Name="original_Heiti_vallar" Type="String" />
            <asp:Parameter Name="original_Staður" Type="String" />
            <asp:Parameter Name="original_Úti_Inni" Type="Int32" />
            <asp:Parameter Name="original_Fjöldi_brauta" Type="Int32" />
            <asp:Parameter Name="original_Fjöldi_beinna_brauta" Type="Int32" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="Heiti_vallar" Type="String" />
            <asp:Parameter Name="Staður" Type="String" />
            <asp:Parameter Name="Úti_Inni" Type="Int32" />
            <asp:Parameter Name="Fjöldi_brauta" Type="Int32" />
            <asp:Parameter Name="Fjöldi_beinna_brauta" Type="Int32" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="Staður" Type="String" />
            <asp:Parameter Name="Úti_Inni" Type="Int32" />
            <asp:Parameter Name="Fjöldi_brauta" Type="Int32" />
            <asp:Parameter Name="Fjöldi_beinna_brauta" Type="Int32" />
            <asp:Parameter Name="original_Heiti_vallar" Type="String" />
            <asp:Parameter Name="original_Staður" Type="String" />
            <asp:Parameter Name="original_Úti_Inni" Type="Int32" />
            <asp:Parameter Name="original_Fjöldi_brauta" Type="Int32" />
            <asp:Parameter Name="original_Fjöldi_beinna_brauta" Type="Int32" />
        </UpdateParameters>
    </asp:SqlDataSource>
    <asp:TextBox ID="KeppnisvellirText" runat="server" Width="1284px"></asp:TextBox>
</asp:Content>
