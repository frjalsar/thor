<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ExportLynxFiles.aspx.cs" Inherits="MotFRI.ExportLynxFiles" %>
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
    <br />
    
 <asp:TextBox ID="CompetitionName" runat="server" BorderStyle="None" Height="56px" Width="1417px" ReadOnly="true"  Font-Size="XX-Large" Font-Bold="True"></asp:TextBox>
    <br />
    <br />
    <asp:Button ID="ExportLynxPpl" runat="server" Text="Export PPL file" OnClick="ExportLynxPpl_Click" ToolTip="Keppendur í móti" />&nbsp;&nbsp;&nbsp;
    <asp:Button ID="ExportLynxSch" runat="server" Text="Export SCH file"  ToolTip="Keppnisgreinar móts" OnClick="ExportLynxSch_Click" />&nbsp;&nbsp;&nbsp;
    <asp:Button ID="ExportLynxEvt" runat="server" Text="Export EVT file"  ToolTip="Keppendur í grein" OnClick="ExportLynxEvt_Click"  />
</asp:Content>
