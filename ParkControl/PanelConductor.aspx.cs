using System;
using Negocio;
using ParkControl.Seguridad;

namespace ParkControl
{
    /// <summary>
    /// Pantalla provisoria. Existe para que el login tenga a donde redirigir
    /// y para verificar el control de acceso por rol. Su contenido real se
    /// construye en los hitos siguientes.
    /// </summary>
    public partial class PanelConductor : PaginaProtegida
    {
        protected override int? RolRequerido
        {
            get { return RolesSistema.Conductor; }
        }

        protected string NombreCompleto
        {
            get
            {
                UsuarioSesion usuario = SesionHelper.UsuarioActual;
                return usuario != null ? usuario.NombreCompleto : string.Empty;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}
