using System;
using System.Security.Cryptography;
using System.Text;

namespace Negocio
{
    /// <summary>
    /// Deriva y verifica hashes de contraseña con PBKDF2-HMACSHA256.
    /// No depende de acceso a datos ni de infraestructura web: solo
    /// manipula bytes en memoria. Lo usa esta capa para el login, y lo
    /// va a poder usar directamente el futuro módulo de registro para
    /// generar el hash y la sal de una contraseña nueva.
    ///
    /// Parámetros fijados por la especificación del algoritmo:
    ///   - PBKDF2 con HMAC-SHA256
    ///   - 10000 iteraciones
    ///   - hash de 32 bytes, sal de 16 bytes (aleatoria y distinta por usuario)
    ///   - la contraseña se codifica en UTF-8 antes de derivar
    ///
    /// IMPORTANTE: se usa la sobrecarga de Rfc2898DeriveBytes que recibe
    /// HashAlgorithmName.SHA256. La sobrecarga de tres argumentos
    /// -Rfc2898DeriveBytes(pass, salt, iteraciones)- usa HMAC-SHA1 por
    /// defecto y produce un hash completamente distinto: si alguien la
    /// cambia por error, el login falla siempre con "credenciales
    /// incorrectas" sin ningún indicio de que el problema es criptográfico.
    /// </summary>
    public static class ServicioContrasenia
    {
        private const int Iteraciones = 10000;
        private const int LongitudHashEnBytes = 32;
        private const int LongitudSaltEnBytes = 16;

        /// <summary>
        /// Genera una sal aleatoria (generador criptográficamente seguro)
        /// y el hash PBKDF2-HMACSHA256 de la contraseña con esa sal.
        /// Pensado para el alta de un usuario o el cambio de contraseña.
        /// </summary>
        public static void GenerarHash(string contrasenia, out byte[] hash, out byte[] salt)
        {
            if (contrasenia == null)
                throw new ArgumentNullException(nameof(contrasenia));

            salt = GenerarSalt();
            hash = DerivarHash(contrasenia, salt);
        }

        /// <summary>
        /// Verifica una contraseña en texto plano contra un hash y una sal
        /// ya almacenados: deriva el hash de la contraseña ingresada con
        /// la misma sal y compara en tiempo constante.
        /// </summary>
        public static bool Verificar(string contrasenia, byte[] hashAlmacenado, byte[] saltAlmacenada)
        {
            if (contrasenia == null || hashAlmacenado == null || saltAlmacenada == null)
                return false;

            byte[] hashCalculado = DerivarHash(contrasenia, saltAlmacenada);
            return SonIguales(hashCalculado, hashAlmacenado);
        }

        private static byte[] GenerarSalt()
        {
            byte[] salt = new byte[LongitudSaltEnBytes];
            using (RandomNumberGenerator generador = RandomNumberGenerator.Create())
            {
                generador.GetBytes(salt);
            }
            return salt;
        }

        private static byte[] DerivarHash(string contrasenia, byte[] salt)
        {
            // La codificación se hace explícita en lugar de delegarla a la
            // sobrecarga que recibe un string: así el parámetro queda a la
            // vista y no depende de un detalle interno del framework.
            byte[] contraseniaUtf8 = Encoding.UTF8.GetBytes(contrasenia);

            using (var pbkdf2 = new Rfc2898DeriveBytes(contraseniaUtf8, salt, Iteraciones, HashAlgorithmName.SHA256))
            {
                return pbkdf2.GetBytes(LongitudHashEnBytes);
            }
        }

        /// <summary>
        /// Compara dos arreglos de bytes recorriéndolos siempre por
        /// completo (sin salir apenas encuentra una diferencia), para no
        /// filtrar por tiempo de respuesta cuánto coincide un hash con
        /// otro.
        /// </summary>
        private static bool SonIguales(byte[] a, byte[] b)
        {
            if (a.Length != b.Length)
                return false;

            int diferencia = 0;
            for (int i = 0; i < a.Length; i++)
            {
                diferencia |= a[i] ^ b[i];
            }
            return diferencia == 0;
        }
    }
}
