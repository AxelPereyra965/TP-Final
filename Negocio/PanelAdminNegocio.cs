using System;
using SQL;

namespace Negocio
{
    /// <summary>
    /// Arma el resumen que muestra el panel del administrador.
    ///
    /// Es esta capa la que decide qué métricas se pueden calcular y cuáles
    /// quedan en null por depender de funcionalidades que todavía no existen.
    /// La página solo muestra lo que recibe.
    /// </summary>
    public class PanelAdminNegocio
    {
        private readonly UsuarioSQL _usuarioSQL;

        public PanelAdminNegocio() : this(new UsuarioSQL())
        {
        }

        public PanelAdminNegocio(UsuarioSQL usuarioSQL)
        {
            if (usuarioSQL == null)
                throw new ArgumentNullException(nameof(usuarioSQL));

            _usuarioSQL = usuarioSQL;
        }

        public ResumenPanelAdmin ObtenerResumen()
        {
            ResumenPanelAdmin resumen = new ResumenPanelAdmin();

            // Datos reales: salen de la tabla Usuarios, que ya existe.
            resumen.EmpleadosActivos = _usuarioSQL.ContarPorRol(RolesSistema.Empleado, true);
            resumen.ConductoresRegistrados = _usuarioSQL.ContarPorRol(RolesSistema.Conductor, true);

            // Pendientes. Quedan en null de forma explícita: no se devuelve 0,
            // porque 0 afirmaría que no hay estacionamientos cuando en realidad
            // la funcionalidad no está construida.
            resumen.EstacionamientosActivos = null;   // requisito 2.2
            resumen.CapacidadTotal = null;            // requisito 2.2
            resumen.OcupacionActual = null;           // requisito 2.6

            return resumen;
        }
    }
}
