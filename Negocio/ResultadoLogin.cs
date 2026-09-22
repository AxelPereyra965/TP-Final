namespace Negocio
{
    /// <summary>
    /// Las cuatro situaciones posibles al intentar iniciar sesión, sin
    /// ambigüedad entre ellas. La capa de presentación traduce cada valor
    /// al mensaje correspondiente del enunciado; esta capa no conoce texto
    /// alguno destinado a la interfaz.
    /// </summary>
    public enum ResultadoLogin
    {
        Exitoso,
        CredencialesIncorrectas,
        CuentaDesactivada,
        AccesoBloqueado
    }
}
