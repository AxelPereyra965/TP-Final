<%@ Page Title="Panel del conductor" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PanelConductor.aspx.cs" Inherits="ParkControl.PanelConductor" %>
<asp:Content ID="Contenido" ContentPlaceHolderID="MainContent" runat="server">
    <h1>ParkControl</h1>
    <h2><%: Title %></h2>
    <p class="lead">Bienvenido/a, <strong><%: NombreCompleto %></strong>.</p>
    <p>Desde acá vas a consultar disponibilidad, reservar y gestionar tus vehículos.</p>
</asp:Content>
