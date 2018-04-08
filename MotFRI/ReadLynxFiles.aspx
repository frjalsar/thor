<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ReadLynxFiles.aspx.cs" Inherits="MotFRI.ReadLynxFiles" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
<script>
  (function(i,s,o,g,r,a,m){i['GoogleAnalyticsObject']=r;i[r]=i[r]||function(){
  (i[r].q=i[r].q||[]).push(arguments)},i[r].l=1*new Date();a=s.createElement(o),
  m=s.getElementsByTagName(o)[0];a.async=1;a.src=g;m.parentNode.insertBefore(a,m)
  })(window,document,'script','https://www.google-analytics.com/analytics.js','ga');

  ga('create', 'UA-82054248-1', 'auto');
  ga('send', 'pageview');

</script>

    <style type="text/css">
        #myfile {
            width: 688px;
        }
        .auto-style2 {
            width: 524px;
            margin-left: 62px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <br />
    <asp:TextBox ID="CompCode" runat="server" ReadOnly="true" Visible="false"></asp:TextBox>
    <asp:TextBox ID="EventLineNo" runat="server" ReadOnly="true" Visible="false"></asp:TextBox>
    <asp:TextBox ID="LifFilePrefix" runat="server" ReadOnly="true" Visible="false"></asp:TextBox>

    <asp:TextBox ID="CompetitionName" runat="server" ReadOnly="true" Height="43px" style="font-size: xx-large" Width="1389px" BorderStyle="None"></asp:TextBox><br /><br />
    <asp:TextBox ID="EventName" runat="server" ReadOnly="true" Height="43px" style="font-size: x-large" Width="1389px" BorderStyle="None"></asp:TextBox>

    <br /><br />
    <asp:Label ID="Label1" runat="server" Text="Lesa tímaskrá úr Lynx kerfi" style="font-size: x-large"></asp:Label><br />
    <br /><br />
    <asp:Label ID="Label2" runat="server" Text="Veldu .lif skrá sem byrjar á "></asp:Label>
    <asp:TextBox ID="SelectFileInfo" runat="server" BorderStyle="None" style="font-size: large" Text="" Width="97px"></asp:TextBox>        
       <input type="file" id="myfile" name="myfile" runat="server" size="100" />

        <br /><br /><br />
    
        <br />
        <asp:Button ID="ReadLifFile" runat="server" Text="Lesa .lif skrá" OnClick="Button1_Click1" style="font-size: large" />
    
    <br />
    <asp:TextBox ID="TextBox1" runat="server" Width="928px"></asp:TextBox><br />
    <asp:TextBox ID="TextBox2" runat="server" Width="924px"></asp:TextBox>
</asp:Content>
