using System;
using Dominio;

namespace Negocio
{
    /// <summary>
    /// Resultado tipado de un intento de autenticación.
    /// Usuario solo tiene valor cuando Resultado es Exitoso; en cualquier
    /// otro caso es null. Se construye únicamente a través de los métodos
    /// estáticos de fábrica, de modo que no exista forma de crear una
    /// instancia en un estado inconsistente (por ejemplo, Exitoso sin
    /// Usuario, o un fallo que arrastre un Usuario por error).
    /// </summary>
    public sealed class ResultadoAutenticacion
    {
        public ResultadoLogin Resultado { get; }
        public Usuario Usuario { get; }

        private ResultadoAutenticacion(ResultadoLogin resultado, Usuario usuario)
        {
            Resultado = resultado;
            Usuario = usuario;
        }

        public static ResultadoAutenticacion Exitoso(Usuario usuario)
        {
            if (usuario == null)
                throw new ArgumentNullException(nameof(usuario));

            return new ResultadoAutenticacion(ResultadoLogin.Exitoso, usuario);
        }

        public static ResultadoAutenticacion CredencialesIncorrectas()
        {
            return new ResultadoAutenticacion(ResultadoLogin.CredencialesIncorrectas, null);
        }

        public static ResultadoAutenticacion CuentaDesactivada()
        {
            return new ResultadoAutenticacion(ResultadoLogin.CuentaDesactivada, null);
        }

        public static ResultadoAutenticacion AccesoBloqueado()
        {
            return new ResultadoAutenticacion(ResultadoLogin.AccesoBloqueado, null);
        }
    }
}
