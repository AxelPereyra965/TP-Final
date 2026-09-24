<%@ Page Title="Panel" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="PanelAdmin.aspx.cs" Inherits="ParkControl.PanelAdmin" %>
<asp:Content ID="Contenido" ContentPlaceHolderID="MainContent" runat="server">

<div class="mb-stack-lg">
<h1 class="font-headline-xl text-headline-xl text-on-surface">Panel de administraci&#243;n</h1>
<p class="font-body-sm text-body-sm text-on-surface-variant mt-1">
Bienvenido/a, <strong><%: NombreCompleto %></strong>. Resumen del estado del sistema.
</p>
</div>

<asp:Panel ID="pnlError" runat="server" Visible="false" role="alert"
    CssClass="mb-stack-md rounded-lg border border-error bg-error-container px-4 py-3">
<p class="font-body-sm text-body-sm text-on-error-container"><asp:Literal ID="litError" runat="server" /></p>
</asp:Panel>

<section aria-label="Indicadores generales" class="grid grid-cols-1 sm:grid-cols-2 xl:grid-cols-4 gap-gutter mb-stack-lg">
<div class="bg-surface-container-lowest border border-outline-variant rounded-lg p-stack-md">
<div class="flex items-start justify-between">
<p class="font-label-caps text-label-caps text-on-surface-variant">Estacionamientos activos</p>
<span aria-hidden="true" class="material-symbols-outlined text-xl text-on-surface-variant">garage</span>
</div>
<p class="font-data-display text-data-display text-on-surface mt-stack-sm text-3xl"><%: Formatear(Resumen.EstacionamientosActivos) %></p>
<p class="font-body-sm text-body-sm text-on-surface-variant mt-2">Disponible al implementar la gesti&#243;n de estacionamientos.</p>
</div>
<div class="bg-surface-container-lowest border border-outline-variant rounded-lg p-stack-md">
<div class="flex items-start justify-between">
<p class="font-label-caps text-label-caps text-on-surface-variant">Ocupaci&#243;n actual</p>
<span aria-hidden="true" class="material-symbols-outlined text-xl text-on-surface-variant">directions_car</span>
</div>
<p class="font-data-display text-data-display text-on-surface mt-stack-sm text-3xl"><%: Formatear(Resumen.OcupacionActual) %></p>
<p class="font-body-sm text-body-sm text-on-surface-variant mt-2">Disponible al implementar el registro de ingresos y egresos.</p>
</div>
<div class="bg-surface-container-lowest border border-outline-variant rounded-lg p-stack-md">
<div class="flex items-start justify-between">
<p class="font-label-caps text-label-caps text-on-surface-variant">Empleados activos</p>
<span aria-hidden="true" class="material-symbols-outlined text-xl text-on-surface-variant">badge</span>
</div>
<p class="font-data-display text-data-display text-on-surface mt-stack-sm text-3xl"><%: Formatear(Resumen.EmpleadosActivos) %></p>

</div>
<div class="bg-surface-container-lowest border border-outline-variant rounded-lg p-stack-md">
<div class="flex items-start justify-between">
<p class="font-label-caps text-label-caps text-on-surface-variant">Conductores registrados</p>
<span aria-hidden="true" class="material-symbols-outlined text-xl text-on-surface-variant">person</span>
</div>
<p class="font-data-display text-data-display text-on-surface mt-stack-sm text-3xl"><%: Formatear(Resumen.ConductoresRegistrados) %></p>

</div>
</section>

<div class="grid grid-cols-1 lg:grid-cols-3 gap-gutter">

<section aria-label="Ocupaci&#243;n por estacionamiento" class="lg:col-span-2 bg-surface-container-lowest border border-outline-variant rounded-lg">
<div class="px-stack-md py-stack-sm border-b border-outline-variant">
<h2 class="font-body-md text-body-md text-on-surface">Ocupaci&#243;n por estacionamiento</h2>
</div>
<div class="p-stack-md">
<div class="flex flex-col items-center justify-center text-center py-stack-lg">
<span aria-hidden="true" class="material-symbols-outlined text-4xl text-outline mb-stack-sm">bar_chart</span>
<p class="font-body-sm text-body-sm text-on-surface-variant">A&#250;n no hay datos de ocupaci&#243;n. Se habilita al implementar la gesti&#243;n de estacionamientos y el registro de ingresos y egresos.</p>
</div>
</div>
</section>

<section aria-label="Actividad reciente" class="bg-surface-container-lowest border border-outline-variant rounded-lg">
<div class="px-stack-md py-stack-sm border-b border-outline-variant">
<h2 class="font-body-md text-body-md text-on-surface">Actividad reciente</h2>
</div>
<div class="p-stack-md">
<div class="flex flex-col items-center justify-center text-center py-stack-lg">
<span aria-hidden="true" class="material-symbols-outlined text-4xl text-outline mb-stack-sm">history</span>
<p class="font-body-sm text-body-sm text-on-surface-variant">A&#250;n no hay operaciones registradas. Se habilita al implementar el historial de operaciones.</p>
</div>
</div>
</section>

</div>
</asp:Content>