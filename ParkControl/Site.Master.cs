using System;
using System.Web.UI;
using ParkControl.Seguridad;

namespace ParkControl
{
    public partial class SiteMaster : MasterPage
    {
        /// <summary>El markup usa estas propiedades para mostrar al usuario
        /// autenticado. Los datos salen de la sesion; no hay nada fijo.</summary>
        protected bool HaySesionActiva
        {
            get { return SesionHelper.HaySesionActiva; }
        }

        protected string NombreUsuario
        {
            get
            {
                UsuarioSesion usuario = SesionHelper.UsuarioActual;
                return usuario != null ? usuario.NombreCompleto : string.Empty;
            }
        }

        protected string NombreRol
        {
            get
            {
                UsuarioSesion usuario = SesionHelper.UsuarioActual;
                return usuario != null ? usuario.NombreRol : string.Empty;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}
