<%@ Page Title="Panel del empleado" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PanelEmpleado.aspx.cs" Inherits="ParkControl.PanelEmpleado" %>
<asp:Content ID="Contenido" ContentPlaceHolderID="MainContent" runat="server">
    <h1>ParkControl</h1>
    <h2><%: Title %></h2>
    <p class="lead">Bienvenido/a, <strong><%: NombreCompleto %></strong>.</p>
    <p>Desde acá vas a registrar ingresos y egresos, cobrar y consultar la ocupación.</p>
</asp:Content>
