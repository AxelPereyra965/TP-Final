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
        // LOGIN
        public Usuario Loguear(string email, string pass)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                // Agregamos Activo = 1 para que no puedan loguearse usuarios dados de baja
                datos.setearConsulta("SELECT IdUsuario, Nombre, Apellido, Email, Telefono, Contrasenia, IdRol, Activo FROM Usuarios WHERE Email = @email AND Contrasenia = @pass AND Activo = 1");
                datos.setearParametro("@email", email);
                datos.setearParametro("@pass", pass);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    return new Usuario
                    {
                        IdUsuario = (int)datos.Lector["IdUsuario"],
                        Nombre = (string)datos.Lector["Nombre"],
                        Apellido = (string)datos.Lector["Apellido"],
                        Email = (string)datos.Lector["Email"],
                        Telefono = datos.Lector["Telefono"] != DBNull.Value ? (string)datos.Lector["Telefono"] : "",
                        Contrasenia = (string)datos.Lector["Contrasenia"],
                        IdRol = (int)datos.Lector["IdRol"],
                        Activo = (bool)datos.Lector["Activo"]
                    };
                }
                return null;
            }
            catch (Exception ex) { throw ex; }
            finally { datos.cerrarConexion(); }
        }

        // LISTAR
        public List<Usuario> Listar()
        {
            List<Usuario> lista = new List<Usuario>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT IdUsuario, Nombre, Apellido, Email, Telefono, Contrasenia, IdRol, Activo, FechaCreacion FROM Usuarios");
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    lista.Add(new Usuario
                    {
                        IdUsuario = (int)datos.Lector["IdUsuario"],
                        Nombre = (string)datos.Lector["Nombre"],
                        Apellido = (string)datos.Lector["Apellido"],
                        Email = (string)datos.Lector["Email"],
                        Telefono = datos.Lector["Telefono"] != DBNull.Value ? (string)datos.Lector["Telefono"] : "",
                        Contrasenia = (string)datos.Lector["Contrasenia"],
                        IdRol = (int)datos.Lector["IdRol"],
                        Activo = (bool)datos.Lector["Activo"],
                        FechaCreacion = (DateTime)datos.Lector["FechaCreacion"]
                    });
                }
                return lista;
            }
            catch (Exception ex) { throw ex; }
            finally { datos.cerrarConexion(); }
        }

        // AGREGAR
        public void Agregar(Usuario user)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("INSERT INTO Usuarios (Nombre, Apellido, Email, Telefono, Contrasenia, IdRol, Activo) VALUES (@nom, @ape, @email, @tel, @pass, @idRol, 1)");
                datos.setearParametro("@nom", user.Nombre);
                datos.setearParametro("@ape", user.Apellido);
                datos.setearParametro("@email", user.Email);
                datos.setearParametro("@tel", user.Telefono);
                datos.setearParametro("@pass", user.Contrasenia);
                datos.setearParametro("@idRol", user.IdRol);
                datos.ejecutarAccion();
            }
            catch (Exception ex) { throw ex; }
            finally { datos.cerrarConexion(); }
        }

        // MODIFICAR
        public void Modificar(Usuario user)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = "UPDATE Usuarios SET Nombre=@nom, Apellido=@ape, Email=@email, Telefono=@tel, IdRol=@idRol, Activo=@activo";
                if (!string.IsNullOrEmpty(user.Contrasenia))
                    consulta += ", Contrasenia=@pass";
                consulta += " WHERE IdUsuario=@id";

                datos.setearConsulta(consulta);
                datos.setearParametro("@nom", user.Nombre);
                datos.setearParametro("@ape", user.Apellido);
                datos.setearParametro("@email", user.Email);
                datos.setearParametro("@tel", user.Telefono);
                datos.setearParametro("@idRol", user.IdRol);
                datos.setearParametro("@activo", user.Activo);
                datos.setearParametro("@id", user.IdUsuario);

                if (!string.IsNullOrEmpty(user.Contrasenia))
                    datos.setearParametro("@pass", user.Contrasenia);

                datos.ejecutarAccion();
            }
            catch (Exception ex) { throw ex; }
            finally { datos.cerrarConexion(); }
        }

        // ELIMINAR (Baja LÃ³gica)
        public void Eliminar(int idUsuario)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                // En lugar de hacer DELETE, en ParkControl conviene apagar el bit "Activo" 
                // para no romper el historial de operaciones de los empleados.
                datos.setearConsulta("UPDATE Usuarios SET Activo = 0 WHERE IdUsuario = @id");
                datos.setearParametro("@id", idUsuario);
                datos.ejecutarAccion();
            }
            catch (Exception ex) { throw ex; }
            finally { datos.cerrarConexion(); }
        }

        // GET BY ID
        public Usuario GetById(int idUsuario)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT * FROM Usuarios WHERE IdUsuario = @id");
                datos.setearParametro("@id", idUsuario);
                datos.ejecutarLectura();
                if (datos.Lector.Read())
                {
                    return new Usuario
                    {
                        IdUsuario = (int)datos.Lector["IdUsuario"],
                        Nombre = (string)datos.Lector["Nombre"],
                        Apellido = (string)datos.Lector["Apellido"],
                        Email = (string)datos.Lector["Email"],
                        Telefono = datos.Lector["Telefono"] != DBNull.Value ? (string)datos.Lector["Telefono"] : "",
                        Contrasenia = (string)datos.Lector["Contrasenia"],
                        IdRol = (int)datos.Lector["IdRol"],
                        Activo = (bool)datos.Lector["Activo"],
                        FechaCreacion = (DateTime)datos.Lector["FechaCreacion"]
                    };
                }
                return null;
            }
            catch (Exception ex) { throw ex; }
            finally { datos.cerrarConexion(); }
        }
    }
}