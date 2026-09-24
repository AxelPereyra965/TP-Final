using System;
using System.Web;
using System.Web.UI;

namespace ParkControl.Seguridad
{
    /// <summary>
    /// Clase base de toda página que requiera usuario autenticado.
    ///
    /// Se eligió una clase base en lugar de un método que cada página tenga
    /// que llamar en su Page_Load: una página que hereda no puede olvidarse
    /// la verificación, mientras que una que debe invocar un helper sí. El
    /// control corre en OnInit, antes de que se procese cualquier evento.
    ///
    /// Uso:
    ///     public partial class Panel : PaginaProtegida
    ///     {
    ///         protected override int? RolRequerido
    ///         {
    ///             get { return RolesSistema.Administrador; }
    ///         }
    ///     }
    ///
    /// Si no se sobrescribe RolRequerido, alcanza con estar autenticado.
    /// </summary>
    public class PaginaProtegida : Page
    {
        /// <summary>
        /// Rol necesario para entrar. null = cualquier usuario autenticado.
        /// </summary>
        protected virtual int? RolRequerido
        {
            get { return null; }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            // Las páginas con datos de sesión no deben quedar en la caché del
            // navegador: sin esto, el botón "atrás" después de cerrar sesión
            // vuelve a mostrar la pantalla con los datos del usuario anterior.
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));

            if (!SesionHelper.HaySesionActiva)
            {
                RedirigirA("~/Login.aspx");
                return;
            }

            if (RolRequerido.HasValue && !SesionHelper.TieneRol(RolRequerido.Value))
            {
                // Hay sesión, pero el rol no corresponde. No se cierra la
                // sesión: el usuario está legítimamente autenticado, solo
                // intentó entrar donde no le toca, así que se lo manda a su
                // propio panel.
                string propio = SesionHelper.ObtenerUrlPanel(SesionHelper.UsuarioActual.IdRol);
                RedirigirA(propio ?? "~/Login.aspx");
                return;
            }
        }

        /// <summary>
        /// Redirige sin usar Response.Redirect(url) a secas, que aborta el
        /// hilo lanzando ThreadAbortException. CompleteRequest corta el
        /// procesamiento de forma limpia.
        /// </summary>
        private void RedirigirA(string url)
        {
            Response.Redirect(url, false);
            Context.ApplicationInstance.CompleteRequest();
        }
    }
}
