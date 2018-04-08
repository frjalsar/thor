<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CompetitorsInEv.aspx.cs" Inherits="MotFRI.CompetorsInEv" %>
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
    <asp:TextBox ID="CompCode" runat="server" Visible="false" ReadOnly="true"></asp:TextBox>
    <asp:TextBox ID="EventLineNo" runat="server" Visible="false" ReadOnly="true"></asp:TextBox><br />
<asp:TextBox ID="CompetitionName" runat="server" ReadOnly="true" Height="38px" Width="1310px" BorderStyle="None" style="font-size: xx-large"></asp:TextBox>
    <br /> <br /><br />
<asp:TextBox ID="EventName" runat="server" ReadOnly="true" Height="45px" Width="1306px" BorderStyle="None" style="font-size: xx-large"></asp:TextBox><br /><br /><br />
    <asp:TextBox ID="AdditionalInfo1" runat="server" ReadOnly="true" BorderStyle="None" Width="800px" style="font-size: large"></asp:TextBox>&nbsp;
    <asp:TextBox ID="MeetRec1" runat="server" ReadOnly="true" BorderStyle="None" Width="580px" style="font-size: large"></asp:TextBox><br />
    <asp:TextBox ID="AdditionalInfo2" runat="server" ReadOnly="true" BorderStyle="None" Width="800px" style="font-size: large"></asp:TextBox>&nbsp;
    <asp:TextBox ID="MeetRec2" runat="server" ReadOnly="true" BorderStyle="None" Width="580px" style="font-size: large"  ></asp:TextBox><br />
    <asp:TextBox ID="AdditionalInfo3" runat="server" ReadOnly="true" BorderStyle="None" Width="800px" style="font-size: large"></asp:TextBox>&nbsp;
    <asp:TextBox ID="MeetRec3" runat="server" ReadOnly="true" BorderStyle="None" Width="580px" style="font-size: large" ></asp:TextBox><br />
    <asp:GridView ID="FieldEventGridView" runat="server" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="mot,greinarnumer,lina" DataSourceID="FieldEventDataSource" ForeColor="#333333" GridLines="None" OnRowDataBound="FieldEventGridView_RowDataBound">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:BoundField DataField="stokkkastrod" HeaderText="Röð" SortExpression="stokkkastrod" >
            <HeaderStyle Font-Size="X-Large" />
            <ItemStyle Font-Size="X-Large" />
            </asp:BoundField>
            <asp:BoundField DataField="rasnumer" HeaderText="Rásn." SortExpression="rasnumer" >
                            <HeaderStyle Font-Size="X-Large" />
                            <ItemStyle Font-Size="X-Large" />
            </asp:BoundField>
            <asp:BoundField DataField="nafn" HeaderText="Nafn" SortExpression="nafn" >
                            <HeaderStyle Font-Size="X-Large" />
                            <ItemStyle Font-Size="X-Large" />
            </asp:BoundField>

            <asp:BoundField DataField="felag" HeaderText="Félag" SortExpression="felag" >
                            <HeaderStyle Font-Size="X-Large" />
                            <ItemStyle Font-Size="X-Large" />
            </asp:BoundField>

            <asp:BoundField DataField="faedingarar" HeaderText="F.ár" SortExpression="faedingarar" >
                            <HeaderStyle Font-Size="X-Large" />
                            <ItemStyle Font-Size="X-Large" />
            </asp:BoundField>

            <asp:BoundField DataField="bestiaranguriartexti" HeaderText=" SB " SortExpression="bestiaranguriartexti" >
                            <HeaderStyle Font-Size="X-Large" />
                            <ItemStyle Font-Size="X-Large" />
            </asp:BoundField>

            <asp:BoundField DataField="personulegtmettexti" HeaderText=" PB " SortExpression="personulegtmettexti" >
                            <HeaderStyle Font-Size="X-Large" />
                            <ItemStyle Font-Size="X-Large" />
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
    <asp:SqlDataSource ID="FieldEventDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:AthleticsConnectionString %>" SelectCommand="CompetitorsInEvent" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:ControlParameter ControlID="CompCode" Name="CompetitionCode" PropertyName="Text" Type="String" />
            <asp:ControlParameter ControlID="EventLineNo" Name="EventNumber" PropertyName="Text" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:GridView ID="TrackEventGridView" runat="server" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="mot,greinarnumer,lina" DataSourceID="TrackEventDataSource" ForeColor="#333333" GridLines="None" OnRowDataBound="TrackEventGridView_RowDataBound">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:BoundField DataField="ridillnumer" HeaderText="Riðill" SortExpression="ridillnumer" >
            <HeaderStyle Font-Size="X-Large" />
            <ItemStyle Font-Size="X-Large" />
            </asp:BoundField>
            <asp:BoundField DataField="stokkkastrod" HeaderText="Braut" SortExpression="stokkkastrod" >
           <HeaderStyle Font-Size="X-Large" />
            <ItemStyle Font-Size="X-Large" />
            </asp:BoundField>

            <asp:BoundField DataField="rasnumer" HeaderText="Rásnr." SortExpression="rasnumer" >
                           <HeaderStyle Font-Size="X-Large" />
            <ItemStyle Font-Size="X-Large" />
            </asp:BoundField>

            <asp:BoundField DataField="nafn" HeaderText="Nafn" SortExpression="nafn" >
                           <HeaderStyle Font-Size="X-Large" />
            <ItemStyle Font-Size="X-Large" />
            </asp:BoundField>

            <asp:BoundField DataField="felag" HeaderText="Félag" SortExpression="felag" >
           <HeaderStyle Font-Size="X-Large" />
            <ItemStyle Font-Size="X-Large" />
            </asp:BoundField>

            <asp:BoundField DataField="faedingarar" HeaderText="F.ár" SortExpression="faedingarar" >
           <HeaderStyle Font-Size="X-Large" />
            <ItemStyle Font-Size="X-Large" />
            </asp:BoundField>

            <asp:BoundField DataField="bestiaranguriartexti" HeaderText=" SB " SortExpression="bestiaranguriartexti" >
           <HeaderStyle Font-Size="X-Large" />
            <ItemStyle Font-Size="X-Large" />
            </asp:BoundField>

            <asp:BoundField DataField="personulegtmettexti" HeaderText=" PB " SortExpression="personulegtmettexti" >
                           <HeaderStyle Font-Size="X-Large" />
            <ItemStyle Font-Size="X-Large" />
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
    <asp:SqlDataSource ID="TrackEventDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:AthleticsConnectionString %>" SelectCommand="CompetitorsInEvent" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:ControlParameter ControlID="CompCode" Name="CompetitionCode" PropertyName="Text" Type="String" />
            <asp:ControlParameter ControlID="EventLineNo" Name="EventNumber" PropertyName="Text" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <br /><br />
    <asp:Button ID="ListOfEvents" runat="server" Text="Til baka" OnClick="ListOfEvents_Click" />
</asp:Content>
