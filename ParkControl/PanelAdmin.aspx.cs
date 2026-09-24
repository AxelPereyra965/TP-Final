using System;
using Negocio;
using ParkControl.Seguridad;

namespace ParkControl
{
    /// <summary>
    /// Panel principal del administrador. Solo muestra: pide el resumen a la
    /// capa de Negocio y lo renderiza. No consulta la base ni decide qué
    /// métricas están disponibles.
    /// </summary>
    public partial class PanelAdmin : PaginaProtegida
    {
        protected override int? RolRequerido
        {
            get { return RolesSistema.Administrador; }
        }

        /// <summary>
        /// Lo consume el markup. Nunca queda en null: si la consulta falla se
        /// carga un resumen vacío para que la página siga renderizando con
        /// guiones en lugar de romperse.
        /// </summary>
        protected ResumenPanelAdmin Resumen { get; private set; }

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
            CargarResumen();
        }

        private void CargarResumen()
        {
            try
            {
                PanelAdminNegocio negocio = new PanelAdminNegocio();
                Resumen = negocio.ObtenerResumen();
            }
            catch (Exception)
            {
                // No se muestra el detalle: expondría la estructura interna.
                // La causa habitual es que la base no esté accesible.
                // TODO: registrar la excepción cuando exista el módulo de logs.
                Resumen = new ResumenPanelAdmin();
                litError.Text = "No se pudieron cargar los indicadores. Intente nuevamente en unos minutos.";
                pnlError.Visible = true;
            }
        }

        /// <summary>
        /// null se muestra como guión, no como cero: un cero afirmaría que no
        /// hay registros, cuando en realidad la funcionalidad no existe.
        /// </summary>
        protected string Formatear(int? valor)
        {
            return valor.HasValue ? valor.Value.ToString("N0") : "—";
        }
    }
}
