using System;
using System.Web;
using Dominio;
using Negocio;

namespace ParkControl.Seguridad
{
    /// <summary>
    /// Único punto por el que se lee y escribe la sesión autenticada.
    /// Ninguna página debería acceder a Session["..."] directamente: si la
    /// clave o el contenido cambian, hay que tocar un solo archivo.
    /// </summary>
    public static class SesionHelper
    {
        private const string ClaveUsuario = "ParkControl.UsuarioAutenticado";

        /// <summary>
        /// Registra al usuario recién autenticado. Recibe la entidad de
        /// Dominio y guarda solo los campos necesarios: el hash y la salt
        /// quedan afuera a propósito.
        /// </summary>
        public static void IniciarSesion(Usuario usuario)
        {
            if (usuario == null)
                throw new ArgumentNullException(nameof(usuario));

            // Se limpia cualquier resto de una sesión anterior antes de
            // escribir la nueva, para que no sobreviva estado viejo.
            HttpContext.Current.Session.Clear();

            HttpContext.Current.Session[ClaveUsuario] = new UsuarioSesion
            {
                IdUsuario = usuario.IdUsuario,
                NombreCompleto = usuario.NombreCompleto,
                Email = usuario.Email,
                IdRol = usuario.IdRol,
                NombreRol = usuario.Rol != null ? usuario.Rol.Nombre : string.Empty
            };
        }

        /// <summary>
        /// El usuario autenticado, o null si no hay sesión activa.
        /// </summary>
        public static UsuarioSesion UsuarioActual
        {
            get
            {
                HttpContext contexto = HttpContext.Current;
                if (contexto == null || contexto.Session == null)
                    return null;

                return contexto.Session[ClaveUsuario] as UsuarioSesion;
            }
        }

        public static bool HaySesionActiva
        {
            get { return UsuarioActual != null; }
        }

        /// <summary>
        /// Verdadero si hay sesión activa y el usuario tiene ese rol.
        /// </summary>
        public static bool TieneRol(int idRol)
        {
            UsuarioSesion usuario = UsuarioActual;
            return usuario != null && usuario.IdRol == idRol;
        }

        /// <summary>
        /// Página inicial que corresponde a cada rol. Devuelve null si el rol
        /// no es ninguno de los tres conocidos: quien llama decide qué hacer.
        /// Centralizarlo acá evita que la ruta de cada panel quede repetida
        /// en el login, en las páginas protegidas y en el menú.
        /// </summary>
        public static string ObtenerUrlPanel(int idRol)
        {
            switch (idRol)
            {
                case RolesSistema.Administrador: return "~/PanelAdmin.aspx";
                case RolesSistema.Empleado:      return "~/PanelEmpleado.aspx";
                case RolesSistema.Conductor:     return "~/PanelConductor.aspx";
                default:                         return null;
            }
        }

        /// <summary>
        /// Cierra la sesión: vacía el contenido, la abandona y vence la
        /// cookie de sesión en el navegador. Los tres pasos importan: sin
        /// abandonar, el identificador de sesión sigue vivo del lado del
        /// servidor; sin vencer la cookie, el navegador la sigue enviando.
        /// </summary>
        public static void CerrarSesion()
        {
            HttpContext contexto = HttpContext.Current;
            if (contexto == null)
                return;

            if (contexto.Session != null)
            {
                contexto.Session.Clear();
                contexto.Session.Abandon();
            }

            if (contexto.Request.Cookies["ASP.NET_SessionId"] != null)
            {
                HttpCookie cookie = new HttpCookie("ASP.NET_SessionId", string.Empty);
                cookie.Expires = DateTime.Now.AddDays(-1);
                contexto.Response.Cookies.Add(cookie);
            }
        }
    }
}
