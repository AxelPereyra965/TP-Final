using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace SQL
{
    public class UsuarioSQL
    {
        // ------------------------------------------------------------------
        // OBTENER POR EMAIL
        //
        // Reemplaza al antiguo método Loguear(). La contraseña ya NO se puede
        // comparar en el WHERE: cada usuario tiene su propia salt, así que el
        // hash almacenado no es comparable por igualdad directa.
        //
        // Tampoco se filtra por Activo ni por bloqueo: esta capa solo trae el
        // registro. La capa Negocio decide si las credenciales son válidas, si
        // la cuenta está activa y si está bloqueada, porque el enunciado exige
        // mensajes distintos para cada situación.
        // ------------------------------------------------------------------
        public Usuario ObtenerPorEmail(string email)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta(
                    "SELECT u.IdUsuario, u.Nombre, u.Apellido, u.Email, u.Telefono, u.DNI, " +
                    "       u.ContraseniaHash, u.ContraseniaSalt, u.IdRol, u.Activo, u.FechaCreacion, " +
                    "       u.IntentosFallidos, u.FechaBloqueoHasta, " +
                    "       r.Nombre AS NombreRol, r.Activo AS RolActivo " +
                    "FROM Usuarios u " +
                    "INNER JOIN Roles r ON r.IdRol = u.IdRol " +
                    "WHERE u.Email = @email");
                datos.setearParametro("@email", email);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                    return MapearUsuarioCompleto(datos);

                return null;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        // ------------------------------------------------------------------
        // OBTENER POR ID
        // ------------------------------------------------------------------
        public Usuario ObtenerPorId(int idUsuario)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta(
                    "SELECT u.IdUsuario, u.Nombre, u.Apellido, u.Email, u.Telefono, u.DNI, " +
                    "       u.ContraseniaHash, u.ContraseniaSalt, u.IdRol, u.Activo, u.FechaCreacion, " +
                    "       u.IntentosFallidos, u.FechaBloqueoHasta, " +
                    "       r.Nombre AS NombreRol, r.Activo AS RolActivo " +
                    "FROM Usuarios u " +
                    "INNER JOIN Roles r ON r.IdRol = u.IdRol " +
                    "WHERE u.IdUsuario = @id");
                datos.setearParametro("@id", idUsuario);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                    return MapearUsuarioCompleto(datos);

                return null;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        // ------------------------------------------------------------------
        // LISTAR
        //
        // No trae ContraseniaHash ni ContraseniaSalt: ninguna pantalla de
        // listado los necesita y no conviene pasearlos por la aplicación.
        // ------------------------------------------------------------------
        public List<Usuario> Listar()
        {
            List<Usuario> lista = new List<Usuario>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta(
                    "SELECT u.IdUsuario, u.Nombre, u.Apellido, u.Email, u.Telefono, u.DNI, " +
                    "       u.IdRol, u.Activo, u.FechaCreacion, " +
                    "       u.IntentosFallidos, u.FechaBloqueoHasta, " +
                    "       r.Nombre AS NombreRol, r.Activo AS RolActivo " +
                    "FROM Usuarios u " +
                    "INNER JOIN Roles r ON r.IdRol = u.IdRol " +
                    "ORDER BY u.Apellido, u.Nombre");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                    lista.Add(MapearUsuarioSinCredenciales(datos));

                return lista;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        // ------------------------------------------------------------------
        // AGREGAR
        //
        // Devuelve el IdUsuario generado. El hash y la salt ya deben venir
        // calculados desde la capa Negocio.
        // ------------------------------------------------------------------
        public int Agregar(Usuario usuario)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta(
                    "INSERT INTO Usuarios (Nombre, Apellido, Email, Telefono, DNI, " +
                    "                      ContraseniaHash, ContraseniaSalt, IdRol, Activo) " +
                    "OUTPUT INSERTED.IdUsuario " +
                    "VALUES (@nombre, @apellido, @email, @telefono, @dni, " +
                    "        @hash, @salt, @idRol, @activo)");

                datos.setearParametro("@nombre", usuario.Nombre);
                datos.setearParametro("@apellido", usuario.Apellido);
                datos.setearParametro("@email", usuario.Email);
                datos.setearParametro("@telefono", usuario.Telefono);
                datos.setearParametro("@dni", usuario.DNI);
                datos.setearParametro("@hash", usuario.ContraseniaHash);
                datos.setearParametro("@salt", usuario.ContraseniaSalt);
                datos.setearParametro("@idRol", usuario.IdRol);
                datos.setearParametro("@activo", usuario.Activo);

                return Convert.ToInt32(datos.ejecutarEscalar());
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        // ------------------------------------------------------------------
        // MODIFICAR
        //
        // No toca las credenciales: el cambio de contraseña tiene su propio
        // método, para que nadie las pise sin querer al editar un perfil.
        // ------------------------------------------------------------------
        public void Modificar(Usuario usuario)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta(
                    "UPDATE Usuarios " +
                    "SET Nombre = @nombre, Apellido = @apellido, Email = @email, " +
                    "    Telefono = @telefono, DNI = @dni, IdRol = @idRol, Activo = @activo " +
                    "WHERE IdUsuario = @id");

                datos.setearParametro("@nombre", usuario.Nombre);
                datos.setearParametro("@apellido", usuario.Apellido);
                datos.setearParametro("@email", usuario.Email);
                datos.setearParametro("@telefono", usuario.Telefono);
                datos.setearParametro("@dni", usuario.DNI);
                datos.setearParametro("@idRol", usuario.IdRol);
                datos.setearParametro("@activo", usuario.Activo);
                datos.setearParametro("@id", usuario.IdUsuario);

                datos.ejecutarAccion();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        // ------------------------------------------------------------------
        // ACTUALIZAR CONTRASEÑA
        // ------------------------------------------------------------------
        public void ActualizarContrasenia(int idUsuario, byte[] hash, byte[] salt)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta(
                    "UPDATE Usuarios SET ContraseniaHash = @hash, ContraseniaSalt = @salt " +
                    "WHERE IdUsuario = @id");
                datos.setearParametro("@hash", hash);
                datos.setearParametro("@salt", salt);
                datos.setearParametro("@id", idUsuario);
                datos.ejecutarAccion();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        // ------------------------------------------------------------------
        // ACTUALIZAR INTENTOS FALLIDOS
        //
        // Un solo método sirve para incrementar y para reiniciar: la capa
        // Negocio decide los valores (cuántos intentos van y hasta cuándo dura
        // el bloqueo, o 0 y null al iniciar sesión correctamente).
        // ------------------------------------------------------------------
        public void ActualizarIntentosFallidos(int idUsuario, int intentosFallidos, DateTime? fechaBloqueoHasta)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta(
                    "UPDATE Usuarios " +
                    "SET IntentosFallidos = @intentos, FechaBloqueoHasta = @bloqueo " +
                    "WHERE IdUsuario = @id");
                datos.setearParametro("@intentos", intentosFallidos);
                datos.setearParametro("@bloqueo", fechaBloqueoHasta);
                datos.setearParametro("@id", idUsuario);
                datos.ejecutarAccion();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        // ------------------------------------------------------------------
        // ELIMINAR (baja lógica)
        //
        // No se hace DELETE: apagar el bit Activo conserva el historial de
        // operaciones asociado al usuario.
        // ------------------------------------------------------------------
        public void Eliminar(int idUsuario)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE Usuarios SET Activo = 0 WHERE IdUsuario = @id");
                datos.setearParametro("@id", idUsuario);
                datos.ejecutarAccion();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        // ==================================================================
        // MAPEO
        // ==================================================================

        private Usuario MapearUsuarioCompleto(AccesoDatos datos)
        {
            Usuario usuario = MapearUsuarioSinCredenciales(datos);
            usuario.ContraseniaHash = (byte[])datos.Lector["ContraseniaHash"];
            usuario.ContraseniaSalt = (byte[])datos.Lector["ContraseniaSalt"];
            return usuario;
        }

        private Usuario MapearUsuarioSinCredenciales(AccesoDatos datos)
        {
            Usuario usuario = new Usuario();

            usuario.IdUsuario = (int)datos.Lector["IdUsuario"];
            usuario.Nombre = (string)datos.Lector["Nombre"];
            usuario.Apellido = (string)datos.Lector["Apellido"];
            usuario.Email = (string)datos.Lector["Email"];

            usuario.Telefono = datos.Lector["Telefono"] != DBNull.Value
                ? (string)datos.Lector["Telefono"]
                : null;

            usuario.DNI = datos.Lector["DNI"] != DBNull.Value
                ? (string)datos.Lector["DNI"]
                : null;

            usuario.IdRol = (int)datos.Lector["IdRol"];
            usuario.Activo = (bool)datos.Lector["Activo"];
            usuario.FechaCreacion = (DateTime)datos.Lector["FechaCreacion"];

            // IntentosFallidos es TINYINT en la base, por lo que el lector
            // devuelve un byte. Un cast directo a int falla, así que se convierte.
            usuario.IntentosFallidos = Convert.ToInt32(datos.Lector["IntentosFallidos"]);

            usuario.FechaBloqueoHasta = datos.Lector["FechaBloqueoHasta"] != DBNull.Value
                ? (DateTime?)datos.Lector["FechaBloqueoHasta"]
                : null;

            usuario.Rol = new Rol
            {
                IdRol = usuario.IdRol,
                Nombre = (string)datos.Lector["NombreRol"],
                Activo = (bool)datos.Lector["RolActivo"]
            };

            return usuario;
        }
    }
}
