<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ParkControl.Login" ClientIDMode="Static" %>

<!DOCTYPE html>

<html class="light" lang="es"><head>
<meta charset="utf-8"/>
<meta content="width=device-width, initial-scale=1.0" name="viewport"/>
<title>Iniciar sesi&#243;n - ParkControl</title>
<script src="https://cdn.tailwindcss.com?plugins=forms,container-queries"></script>
<link href="https://fonts.googleapis.com" rel="preconnect"/>
<link crossorigin="" href="https://fonts.gstatic.com" rel="preconnect"/>
<link href="https://fonts.googleapis.com/css2?family=Inter:wght@400;500;600;700&amp;family=JetBrains+Mono:wght@500;600&amp;display=swap" rel="stylesheet"/>
<link href="https://fonts.googleapis.com/css2?family=Material+Symbols+Outlined:wght,FILL@100..700,0..1&amp;display=swap" rel="stylesheet"/>
<link href="https://fonts.googleapis.com/css2?family=Material+Symbols+Outlined:wght,FILL@100..700,0..1&amp;display=swap" rel="stylesheet"/>
<script id="tailwind-config">
    tailwind.config = {
        darkMode: "class",
        theme: {
            extend: {
                "colors": {
                    "surface-container": "#eceef0",
                    "secondary-container": "#d0e1fb",
                    "on-error-container": "#93000a",
                    "on-primary-fixed": "#111c2d",
                    "on-primary-fixed-variant": "#3c475a",
                    "on-surface-variant": "#45474c",
                    "surface-variant": "#e0e3e5",
                    "on-primary-container": "#8590a6",
                    "primary-fixed-dim": "#bcc7de",
                    "secondary": "#505f76",
                    "surface-container-low": "#f2f4f6",
                    "primary-container": "#1e293b",
                    "tertiary-fixed": "#d8e2ff",
                    "on-background": "#191c1e",
                    "surface-bright": "#f7f9fb",
                    "on-tertiary-fixed-variant": "#004395",
                    "on-secondary-container": "#54647a",
                    "secondary-fixed-dim": "#b7c8e1",
                    "tertiary": "#001334",
                    "inverse-surface": "#2d3133",
                    "on-tertiary": "#ffffff",
                    "surface-tint": "#545f73",
                    "on-secondary-fixed-variant": "#38485d",
                    "primary-fixed": "#d8e3fb",
                    "error-container": "#ffdad6",
                    "on-tertiary-fixed": "#001a42",
                    "tertiary-fixed-dim": "#adc6ff",
                    "on-tertiary-container": "#4c8dff",
                    "outline-variant": "#c5c6cd",
                    "surface-container-lowest": "#ffffff",
                    "surface-container-highest": "#e0e3e5",
                    "on-secondary-fixed": "#0b1c30",
                    "outline": "#75777d",
                    "inverse-primary": "#bcc7de",
                    "on-primary": "#ffffff",
                    "on-secondary": "#ffffff",
                    "primary": "#091426",
                    "surface-dim": "#d8dadc",
                    "on-surface": "#191c1e",
                    "tertiary-container": "#00275b",
                    "secondary-fixed": "#d3e4fe",
                    "background": "#f7f9fb",
                    "surface": "#f7f9fb",
                    "surface-container-high": "#e6e8ea",
                    "on-error": "#ffffff",
                    "error": "#ba1a1a",
                    "inverse-on-surface": "#eff1f3"
                },
                "borderRadius": {
                    "DEFAULT": "0.125rem",
                    "lg": "0.25rem",
                    "xl": "0.5rem",
                    "full": "0.75rem"
                },
                "spacing": {
                    "stack-md": "16px",
                    "gutter": "24px",
                    "stack-sm": "8px",
                    "margin-desktop": "48px",
                    "stack-lg": "32px",
                    "margin-mobile": "16px",
                    "unit": "4px"
                },
                "fontFamily": {
                    "headline-lg": ["Inter"],
                    "headline-lg-mobile": ["Inter"],
                    "label-caps": ["JetBrains Mono"],
                    "body-sm": ["Inter"],
                    "headline-xl": ["Inter"],
                    "data-display": ["JetBrains Mono"],
                    "body-md": ["Inter"]
                },
                "fontSize": {
                    "headline-lg": ["28px", { "lineHeight": "36px", "letterSpacing": "-0.01em", "fontWeight": "600" }],
                    "headline-lg-mobile": ["24px", { "lineHeight": "32px", "letterSpacing": "-0.01em", "fontWeight": "600" }],
                    "label-caps": ["12px", { "lineHeight": "16px", "letterSpacing": "0.05em", "fontWeight": "600" }],
                    "body-sm": ["14px", { "lineHeight": "20px", "fontWeight": "400" }],
                    "headline-xl": ["36px", { "lineHeight": "44px", "letterSpacing": "-0.02em", "fontWeight": "700" }],
                    "data-display": ["18px", { "lineHeight": "24px", "fontWeight": "500" }],
                    "body-md": ["16px", { "lineHeight": "24px", "fontWeight": "400" }]
                }
            }
        }
    }
</script>
</head>
<body class="bg-surface text-on-surface min-h-screen flex items-center justify-center font-body-md text-body-md selection:bg-primary selection:text-on-primary">
<form id="formLogin" runat="server">
<div class="w-full max-w-md px-margin-mobile md:px-0">
<div class="bg-surface-container-lowest border border-surface-container-highest rounded-xl p-stack-lg shadow-sm">

<div class="text-center mb-stack-lg">
<div class="flex items-center justify-center mb-stack-sm text-primary">
<span aria-hidden="true" class="material-symbols-outlined text-4xl" style="font-variation-settings: 'FILL' 1;">local_parking</span>
<span class="font-headline-xl text-headline-xl text-primary ml-2 tracking-tight">ParkControl</span>
</div>
<h1 class="font-headline-lg text-headline-lg text-on-surface mt-stack-sm">Iniciar sesi&#243;n</h1>
<p class="font-body-sm text-body-sm text-on-surface-variant mt-2">Ingres&#225; tus credenciales para acceder al sistema.</p>
</div>

<asp:Panel ID="pnlMensaje" runat="server" Visible="false" role="alert" aria-live="assertive"
    CssClass="mb-stack-md rounded-lg border border-error bg-error-container px-4 py-3">
<p class="font-body-sm text-body-sm text-on-error-container"><asp:Literal ID="litMensaje" runat="server" /></p>
</asp:Panel>

<asp:Panel ID="pnlFormulario" runat="server" DefaultButton="btnLogin" CssClass="space-y-stack-md">

<div>
<label class="block font-label-caps text-label-caps text-on-surface-variant mb-unit" for="txtEmail">Correo electr&#243;nico</label>
<div class="relative">
<div aria-hidden="true" class="absolute inset-y-0 left-0 pl-3 flex items-center pointer-events-none text-on-surface-variant">
<span class="material-symbols-outlined text-xl">mail</span>
</div>
<asp:TextBox ID="txtEmail" runat="server" TextMode="Email" MaxLength="150"
    placeholder="nombre@ejemplo.com" CssClass="block w-full py-2 bg-surface-container-lowest border border-outline-variant rounded-lg text-on-surface placeholder:text-on-surface-variant focus:outline-none focus:ring-2 focus:ring-on-tertiary-container focus:ring-opacity-20 focus:border-on-tertiary-container transition-shadow font-body-sm text-body-sm pl-10 pr-3" />
</div>
<asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail"
    Display="Dynamic" ValidationGroup="Login" CssClass="block font-body-sm text-body-sm text-error mt-1"
    ErrorMessage="El correo electr&#243;nico es obligatorio." />
</div>

<div>
<label class="block font-label-caps text-label-caps text-on-surface-variant mb-unit" for="txtPassword">Contrase&#241;a</label>
<div class="relative">
<div aria-hidden="true" class="absolute inset-y-0 left-0 pl-3 flex items-center pointer-events-none text-on-surface-variant">
<span class="material-symbols-outlined text-xl">lock</span>
</div>
<asp:TextBox ID="txtPassword" runat="server" TextMode="Password" MaxLength="100"
    CssClass="block w-full py-2 bg-surface-container-lowest border border-outline-variant rounded-lg text-on-surface placeholder:text-on-surface-variant focus:outline-none focus:ring-2 focus:ring-on-tertiary-container focus:ring-opacity-20 focus:border-on-tertiary-container transition-shadow font-body-sm text-body-sm pl-10 pr-10" />
<button aria-label="Mostrar u ocultar la contrase&#241;a" id="btnVerPassword" type="button"
    class="absolute inset-y-0 right-0 pr-3 flex items-center text-on-surface-variant hover:text-primary transition-colors focus:outline-none">
<span aria-hidden="true" class="material-symbols-outlined text-xl">visibility</span>
</button>
</div>
<asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword"
    Display="Dynamic" ValidationGroup="Login" CssClass="block font-body-sm text-body-sm text-error mt-1"
    ErrorMessage="La contrase&#241;a es obligatoria." />
<div class="flex justify-end mt-2">
<%-- TODO: la recuperacion de contrasenia esta en pausa hasta resolver la
     contradiccion entre los dos documentos de requerimientos. --%>
<a class="font-body-sm text-body-sm text-on-tertiary-container hover:underline underline-offset-4" href="#">&#191;Olvidaste tu contrase&#241;a?</a>
</div>
</div>

<asp:Button ID="btnLogin" runat="server" Text="Iniciar sesi&#243;n" ValidationGroup="Login"
    OnClick="btnLogin_Click"
    CssClass="w-full flex justify-center py-3 px-4 border border-transparent rounded-lg shadow-sm font-label-caps text-label-caps text-on-primary bg-primary hover:bg-primary-container focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-primary transition-colors" />

</asp:Panel>
</div>

<div class="text-center mt-stack-lg">
<p class="font-body-sm text-body-sm text-on-surface-variant">
&#191;No ten&#233;s una cuenta?
<%-- TODO: el registro de conductores (requisito 2.1.1) todavia no esta implementado. --%>
<a class="text-on-tertiary-container font-medium hover:underline underline-offset-4" href="#">Registrate</a>
</p>
</div>

</div>
</form>
<script>
    document.addEventListener('DOMContentLoaded', function () {
        var boton = document.getElementById('btnVerPassword');
        var campo = document.getElementById('txtPassword');
        if (!boton || !campo) return;
        var icono = boton.querySelector('.material-symbols-outlined');
        boton.addEventListener('click', function () {
            var esPassword = campo.getAttribute('type') === 'password';
            campo.setAttribute('type', esPassword ? 'text' : 'password');
            icono.textContent = esPassword ? 'visibility_off' : 'visibility';
        });
    });
</script>
</body></html>