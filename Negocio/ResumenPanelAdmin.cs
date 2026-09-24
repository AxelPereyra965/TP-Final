namespace Negocio
{
    /// <summary>
    /// Métricas que muestra el panel del administrador.
    ///
    /// Todas las propiedades son anulables a propósito, y null NO significa
    /// cero: significa que la funcionalidad de la que sale ese número todavía
    /// no está implementada. La diferencia importa.
    ///
    ///   EmpleadosActivos = 0     -> la tabla existe y no hay empleados
    ///   EstacionamientosActivos = null -> la tabla de estacionamientos no existe
    ///
    /// La capa de presentación traduce null a "—" y nunca a un número.
    /// </summary>
    public class ResumenPanelAdmin
    {
        /// <summary>Pendiente: requiere la tabla Estacionamientos (requisito 2.2).</summary>
        public int? EstacionamientosActivos { get; set; }

        /// <summary>Pendiente: requiere el registro de ingresos y egresos (requisito 2.6).</summary>
        public int? OcupacionActual { get; set; }

        /// <summary>Pendiente: requiere la tabla Estacionamientos (requisito 2.2).</summary>
        public int? CapacidadTotal { get; set; }

        /// <summary>Disponible: usuarios con rol Empleado y Activo = 1 (requisito 2.8).</summary>
        public int? EmpleadosActivos { get; set; }

        /// <summary>Disponible: usuarios con rol Conductor y Activo = 1.</summary>
        public int? ConductoresRegistrados { get; set; }

        public ResumenPanelAdmin() { }
    }
}
