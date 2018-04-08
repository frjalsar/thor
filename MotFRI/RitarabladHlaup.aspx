<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RitarabladHlaup.aspx.cs" Inherits="MotFRI.RitarabladHlaup" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:TextBox ID="CompetitionName" runat="server" ReadOnly="true" Height="42px" Width="1082px" BorderStyle="None" style="font-size: x-large"></asp:TextBox><br />
    <asp:TextBox ID="EventName" runat="server" ReadOnly="true" Height="42px" Width="1190px" BorderStyle="None" style="font-size: large"></asp:TextBox><br />
    <asp:TextBox ID="CompCode" runat="server" Visible="false"></asp:TextBox><asp:TextBox ID="EventLineNo" runat="server" Visible="false"> </asp:TextBox><br />

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    </asp:Content>
