using System;

namespace ParkControl.Seguridad
{
    /// <summary>
    /// Datos del usuario autenticado que se guardan en la sesión.
    ///
    /// Deliberadamente NO es la entidad Dominio.Usuario: esa lleva
    /// ContraseniaHash y ContraseniaSalt, y no hay ninguna razón para que el
    /// material criptográfico quede almacenado en el estado de sesión, donde
    /// cualquier página podría leerlo. Acá viaja únicamente lo que la
    /// interfaz necesita: identificar al usuario, saludarlo y decidir a qué
    /// pantallas puede entrar.
    ///
    /// [Serializable] porque si en el futuro la sesión deja de ser InProc
    /// (StateServer o SQL Server), el objeto tiene que poder serializarse.
    /// </summary>
    [Serializable]
    public class UsuarioSesion
    {
        public int IdUsuario { get; set; }
        public string NombreCompleto { get; set; }
        public string Email { get; set; }
        public int IdRol { get; set; }
        public string NombreRol { get; set; }

        public UsuarioSesion() { }
    }
}
