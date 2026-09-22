using System;
using Dominio;
using SQL;

namespace Negocio
{
    /// <summary>
    /// Autenticación de usuarios para el login de ParkControl.
    /// No conoce HttpContext, Session, Request ni Response: recibe email y
    /// contraseña en texto plano y devuelve un ResultadoAutenticacion
    /// tipado. El manejo de sesión queda para la capa de presentación.
    /// </summary>
    public class AutenticacionNegocio
    {
        // Reglas de negocio fijas (no configurables, no son columnas de la base).
        private const int UmbralIntentosFallidos = 3;
        private const int DuracionBloqueoEnMinutos = 15;

        private readonly UsuarioSQL _usuarioSQL;

        public AutenticacionNegocio() : this(new UsuarioSQL())
        {
        }

        // Permite inyectar la dependencia (por ejemplo, para pruebas).
        public AutenticacionNegocio(UsuarioSQL usuarioSQL)
        {
            if (usuarioSQL == null)
                throw new ArgumentNullException(nameof(usuarioSQL));

            _usuarioSQL = usuarioSQL;
        }

        /// <summary>
        /// Intenta autenticar a un usuario por email y contraseña.
        /// Orden de verificación (ver justificación de diseño):
        ///   1. Entrada vacía/nula -> credenciales incorrectas, sin tocar la base.
        ///   2. El correo no existe -> credenciales incorrectas, sin tocar la base.
        ///   3. Bloqueo vigente -> acceso bloqueado, sin verificar la contraseña.
        ///   4. Contraseña incorrecta -> se cuenta el intento, credenciales
        ///      incorrectas (o acceso bloqueado si este intento cumple el umbral).
        ///   5. Contraseña correcta pero cuenta desactivada -> cuenta desactivada.
        ///   6. Contraseña correcta y cuenta activa -> éxito, contador en cero.
        /// </summary>
        public ResultadoAutenticacion Autenticar(string email, string contrasenia)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrEmpty(contrasenia))
                return ResultadoAutenticacion.CredencialesIncorrectas();

            string emailNormalizado = NormalizarEmail(email);
            Usuario usuario = _usuarioSQL.ObtenerPorEmail(emailNormalizado);

            // El correo no existe: no hay fila que actualizar ni intento que
            // contar. Mismo resultado que una contraseña incorrecta, para no
            // revelar si un correo está o no registrado.
            if (usuario == null)
                return ResultadoAutenticacion.CredencialesIncorrectas();

            // El bloqueo se respeta sin mirar la contraseña: mientras esté
            // vigente, el resultado es el mismo la haya acertado o no.
            if (EstaBloqueadoVigente(usuario))
                return ResultadoAutenticacion.AccesoBloqueado();

            bool contraseniaCorrecta = ServicioContrasenia.Verificar(
                contrasenia, usuario.ContraseniaHash, usuario.ContraseniaSalt);

            if (!contraseniaCorrecta)
            {
                bool bloqueoRecienActivado = RegistrarIntentoFallido(usuario);

                return bloqueoRecienActivado
                    ? ResultadoAutenticacion.AccesoBloqueado()
                    : ResultadoAutenticacion.CredencialesIncorrectas();
            }

            // La contraseña es correcta: recién acá se revela el estado de
            // la cuenta (ver justificación de diseño, caso límite 4).
            if (!usuario.Activo)
                return ResultadoAutenticacion.CuentaDesactivada();

            RegistrarInicioExitoso(usuario);
            return ResultadoAutenticacion.Exitoso(usuario);
        }

        private static string NormalizarEmail(string email)
        {
            return email.Trim().ToLowerInvariant();
        }

        private static bool EstaBloqueadoVigente(Usuario usuario)
        {
            return usuario.FechaBloqueoHasta.HasValue
                && usuario.FechaBloqueoHasta.Value > DateTime.Now;
        }

        /// <summary>
        /// Suma un intento fallido y, si corresponde, activa el bloqueo.
        /// Devuelve true si este intento fue el que activó el bloqueo.
        /// </summary>
        private bool RegistrarIntentoFallido(Usuario usuario)
        {
            // Si hubo un bloqueo anterior y ya venció, ese bloqueo ya
            // "se cumplió": este intento arranca un contador nuevo en
            // lugar de sumarse a una racha ya vieja.
            bool bloqueoAnteriorVencido = usuario.FechaBloqueoHasta.HasValue
                && usuario.FechaBloqueoHasta.Value <= DateTime.Now;

            int intentosPrevios = bloqueoAnteriorVencido ? 0 : usuario.IntentosFallidos;
            int nuevosIntentos = intentosPrevios + 1;

            bool alcanzaUmbral = nuevosIntentos >= UmbralIntentosFallidos;
            DateTime? nuevaFechaBloqueo = alcanzaUmbral
                ? DateTime.Now.AddMinutes(DuracionBloqueoEnMinutos)
                : (DateTime?)null;

            _usuarioSQL.ActualizarIntentosFallidos(usuario.IdUsuario, nuevosIntentos, nuevaFechaBloqueo);

            return alcanzaUmbral;
        }

        private void RegistrarInicioExitoso(Usuario usuario)
        {
            // Solo se escribe si hay algo que limpiar: en el caso habitual
            // (login correcto sin fallos previos) se evita un UPDATE innecesario.
            if (usuario.IntentosFallidos == 0 && !usuario.FechaBloqueoHasta.HasValue)
                return;

            _usuarioSQL.ActualizarIntentosFallidos(usuario.IdUsuario, 0, null);
        }
    }
}
