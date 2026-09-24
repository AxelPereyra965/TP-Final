namespace Negocio
{
    /// <summary>
    /// Identificadores de los roles del sistema. Coinciden con los valores
    /// fijos cargados en la tabla Roles, que usa clave primaria explícita
    /// (sin IDENTITY) justamente para que estas constantes sean estables.
    ///
    /// Se centralizan acá para que ninguna capa compare contra números
    /// sueltos del tipo "if (idRol == 3)".
    /// </summary>
    public static class RolesSistema
    {
        public const int Conductor = 1;
        public const int Empleado = 2;
        public const int Administrador = 3;
    }
}
