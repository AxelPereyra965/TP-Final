using System;
using System.Web.UI;
using ParkControl.Seguridad;

namespace ParkControl
{
    /// <summary>
    /// Cierra la sesión y vuelve al login. No tiene interfaz propia: su único
    /// trabajo es limpiar la sesión y redirigir.
    /// </summary>
    public partial class Logout : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SesionHelper.CerrarSesion();
            Response.Redirect("~/Login.aspx", false);
            Context.ApplicationInstance.CompleteRequest();
        }
    }
}
