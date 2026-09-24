<%@ Page Title="Panel de administración" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PanelAdmin.aspx.cs" Inherits="ParkControl.PanelAdmin" %>
<asp:Content ID="Contenido" ContentPlaceHolderID="MainContent" runat="server">
    <h1>ParkControl</h1>
    <h2><%: Title %></h2>
    <p class="lead">Bienvenido/a, <strong><%: NombreCompleto %></strong>.</p>
    <p>Desde acá vas a gestionar estacionamientos, tarifas, empleados y el historial de operaciones.</p>
</asp:Content>
