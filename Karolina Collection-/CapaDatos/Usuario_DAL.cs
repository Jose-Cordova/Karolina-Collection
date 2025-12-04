using Karolina_Collection_.CapaEntidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karolina_Collection_.CapaDatos
{
    public class Usuario_DAL
    {
        //Metodo que valida nombre de usuario, clave y estado para logearnos
        public static Usuario Login(string nombreUsuario, string claveHash)
        {
            using (SqlConnection cn = new SqlConnection(Conexion_DB.cadena_conexion))
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand(
                    "SELECT id, nombre_usuario, rol, estado FROM Usuario WHERE nombre_usuario=@u AND clave_hash=@h AND estado=1", cn))
                {
                    cmd.Parameters.AddWithValue("@u", nombreUsuario);
                    cmd.Parameters.AddWithValue("@h", claveHash);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            return new Usuario
                            {
                                id = Convert.ToInt32(dr["id"]),
                                nombre_usuario = dr["nombre_usuario"].ToString(),
                                rol = dr["rol"].ToString(),
                                estado = Convert.ToBoolean(dr["estado"])
                            };
                        }
                    }
                }
            }
            return null;
        }
        // Listar todos los usuarios
        public static List<Usuario> Listar()
        {
            var lista = new List<Usuario>();
            using (SqlConnection cn = new SqlConnection(Conexion_DB.cadena_conexion))
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT id, nombre_usuario, rol, estado FROM Usuario", cn))
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new Usuario
                        {
                            id = Convert.ToInt32(dr["id"]),
                            nombre_usuario = dr["nombre_usuario"].ToString(),
                            rol = dr["rol"].ToString(),
                            estado = Convert.ToBoolean(dr["estado"])
                        });
                    }
                }
            }
            return lista;
        }
        // Insertar usuario (recibe hash ya calculado)
        public static int Insertar(string nombreUsuario, string claveHash, string rol)
        {
            using (SqlConnection cn = new SqlConnection(Conexion_DB.cadena_conexion))
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand(
                    "INSERT INTO Usuario (nombre_usuario, clave_hash, rol, estado) VALUES (@u, @h, @r, 1); SELECT SCOPE_IDENTITY();", cn))
                {
                    cmd.Parameters.AddWithValue("@u", nombreUsuario);
                    cmd.Parameters.AddWithValue("@h", claveHash);
                    cmd.Parameters.AddWithValue("@r", rol);
                    object result = cmd.ExecuteScalar();
                    return Convert.ToInt32(result);
                }
            }
        }
        // Actualizar (no actualiza clave)
        public static bool Actualizar(int id, string nombreUsuario, string rol, bool estado)
        {
            using (SqlConnection cn = new SqlConnection(Conexion_DB.cadena_conexion))
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand(
                    "UPDATE Usuario SET nombre_usuario=@u, Rol=@r, estado=@e WHERE id=@id", cn))
                {
                    cmd.Parameters.AddWithValue("@u", nombreUsuario);
                    cmd.Parameters.AddWithValue("@r", rol);
                    cmd.Parameters.AddWithValue("@e", estado);
                    cmd.Parameters.AddWithValue("@id", id);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
        //Eliminar Usuario
        public static bool Eliminar(int id)
        {
            using (SqlConnection cn = new SqlConnection(Conexion_DB.cadena_conexion))
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("DELETE FROM Usuario WHERE id=@id", cn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
        // Cambiar clave (recibe hash nuevo)
        public static bool CambiarClave(int id, string claveHashNueva)
        {
            using (SqlConnection cn = new SqlConnection(Conexion_DB.cadena_conexion))
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand("UPDATE Usuario SET clave_hash=@h WHERE id=@id", cn))
                {
                    cmd.Parameters.AddWithValue("@h", claveHashNueva);
                    cmd.Parameters.AddWithValue("@id", id);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

    }
}
