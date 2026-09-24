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

        // Admiten NULL en la base: el enunciado los exige solo en el registro de
        // conductor. La obligatoriedad para ese caso se valida en la capa Negocio.
        public string Telefono { get; set; }
        public string DNI { get; set; }

        // Credenciales: PBKDF2-HMAC-SHA256, 10000 iteraciones.
        // Hash de 32 bytes y salt de 16 bytes, generados en la capa Negocio.
        // La contraseña en texto plano NUNCA se guarda en esta entidad: viaja
        // como parámetro suelto hacia Negocio y se descarta apenas se verifica.
        public byte[] ContraseniaHash { get; set; }
        public byte[] ContraseniaSalt { get; set; }

        public int IdRol { get; set; }

        // Se completa solo cuando la consulta trae el JOIN con Roles.
        // Si la consulta no lo incluye, queda en null.
        public Rol Rol { get; set; }

        public bool Activo { get; set; }
        public DateTime FechaCreacion { get; set; }

        // Control de bloqueo temporal por intentos fallidos.
        // El umbral y la duración del bloqueo son constantes de la capa Negocio.
        public int IntentosFallidos { get; set; }
        public DateTime? FechaBloqueoHasta { get; set; }

        public string NombreCompleto => $"{Nombre} {Apellido}";

        public Usuario() { }
    }
}
