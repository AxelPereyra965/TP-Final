using System;
using System.Web.UI;
using Negocio;
using ParkControl.Seguridad;

namespace ParkControl
{
    public partial class Login : Page
    {
        // Textos exactos del enunciado (requisito 2.1.2).
        private const string MensajeCredenciales = "Credenciales incorrectas. Por favor, intente nuevamente.";
        private const string MensajeDesactivada = "Cuenta desactivada. Contacte al administrador.";
        private const string MensajeBloqueado = "Acceso temporalmente bloqueado. Intente nuevamente más tarde.";
        private const string MensajeErrorTecnico = "No se pudo procesar el inicio de sesión. Intente nuevamente en unos minutos.";

        protected void Page_Load(object sender, EventArgs e)
        {
            // Si ya hay una sesión abierta, no tiene sentido mostrar el login:
            // se va directo al panel que corresponde al rol.
            if (!IsPostBack && SesionHelper.HaySesionActiva)
            {
                string url = SesionHelper.ObtenerUrlPanel(SesionHelper.UsuarioActual.IdRol);
                if (url != null)
                    RedirigirA(url);
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            // Los validadores ya corrieron del lado del cliente, pero esta
            // verificación del lado del servidor es la que realmente protege:
            // la del navegador se puede desactivar.
            if (!Page.IsValid)
                return;

            try
            {
                AutenticacionNegocio autenticacion = new AutenticacionNegocio();
                ResultadoAutenticacion resultado = autenticacion.Autenticar(txtEmail.Text, txtPassword.Text);

                switch (resultado.Resultado)
                {
                    case ResultadoLogin.Exitoso:
                        SesionHelper.IniciarSesion(resultado.Usuario);
                        string url = SesionHelper.ObtenerUrlPanel(resultado.Usuario.IdRol);
                        RedirigirA(url ?? "~/Login.aspx");
                        return;

                    case ResultadoLogin.CuentaDesactivada:
                        MostrarMensaje(MensajeDesactivada);
                        break;

                    case ResultadoLogin.AccesoBloqueado:
                        MostrarMensaje(MensajeBloqueado);
                        break;

                    default:
                        MostrarMensaje(MensajeCredenciales);
                        break;
                }
            }
            catch (Exception)
            {
                // No se muestra el detalle del error: expondría la estructura
                // interna del sistema. La causa más habitual acá es que la base
                // de datos no esté accesible.
                // TODO: registrar la excepción cuando exista el módulo de logs.
                MostrarMensaje(MensajeErrorTecnico);
            }
            finally
            {
                // La contraseña nunca se devuelve al navegador.
                txtPassword.Text = string.Empty;
            }
        }

        private void MostrarMensaje(string mensaje)
        {
            litMensaje.Text = mensaje;
            pnlMensaje.Visible = true;
        }

        private void RedirigirA(string url)
        {
            Response.Redirect(url, false);
            Context.ApplicationInstance.CompleteRequest();
        }
    }
}
