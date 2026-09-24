using System;
using System.Web.UI;
using ParkControl.Seguridad;

namespace ParkControl
{
    /// <summary>
    /// Layout del área de administración: menú lateral, encabezado y zona de
    /// contenido. Es propio del administrador; cuando existan los paneles de
    /// empleado y conductor se evaluará extraer lo común.
    ///
    /// Solo expone datos de la sesión al markup. No consulta la base ni
    /// contiene reglas de negocio.
    /// </summary>
    public partial class AdminMaster : MasterPage
    {
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
