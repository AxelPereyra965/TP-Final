using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    [Serializable]
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public string Contrasenia { get; set; }
        public int IdRol { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaCreacion { get; set; }

        // Propiedad de solo lectura al estilo de tu proyecto anterior
        public string NombreCompleto => $"{Nombre} {Apellido}";

        public Usuario() { }
    }
}
