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
            string email = txtEmail.Text;
            string password = txtPassword.Text;

            if (email == "admin@parkcontrol.com" && password == "1234")
            {
                Response.Redirect("Default.aspx");
            }
            else
            {
                lblMensaje.Text = "Email o contraseña incorrectos.";
            }
        }
    }
}