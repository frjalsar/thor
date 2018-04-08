<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AfrekaskraFRI.aspx.cs" Inherits="MotFRI.AfrekaskraFRI" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <p>
        <br />
        Afrekaskrá FRÍ - væntanleg á næstunni
        <br /><br />

        <asp:HyperLink ID="GamlaAfrekaskrain" runat="server" NavigateUrl="http://fri.is/sida/afrekaskra-fri">&#39;Gamla afrekaskráin&#39;</asp:HyperLink>
        <br /><br />
        <asp:HyperLink ID="StatisticsPage" runat="server" NavigateUrl="~/Statistics.aspx">Fyrirspurn í Afrekaskrá FRÍ</asp:HyperLink>
        <br />

</asp:Content>
