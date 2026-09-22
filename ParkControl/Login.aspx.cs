using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ParkControl
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            // TODO (L7): implementar el inicio de sesión real.
            //
            // Este método está vacío a propósito. Login.aspx todavía es una maqueta
            // HTML estática: su formulario no tiene controles runat="server", por lo
            // que txtEmail, txtPassword y lblMensaje no existen y el código anterior
            // no compilaba.
            //
            // La reconstrucción de la página como Web Forms y la autenticación contra
            // la base de datos corresponden a las tareas L5, L6 y L7 del Hito 1.
        }
    }
}
