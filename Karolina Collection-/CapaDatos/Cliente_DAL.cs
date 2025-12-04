using Karolina_Collection_.CapaEntidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karolina_Collection_.CapaDatos
{
    public class Cliente_DAL
    {
        // Este método sirve para llenar el ComboBox de clientes.
        public static List<Cliente> ListarActivos()
        {
            List<Cliente> lista = new List<Cliente>();

            using (SqlConnection con = new SqlConnection(Conexion_DB.cadena_conexion))
            {

                string sql = "SELECT * FROM Cliente";

                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    con.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Cliente
                            {
                                id = Convert.ToInt32(dr["id"]),
                                nombre_completo = dr["nombre_completo"].ToString(),
                                dui = dr["dui"].ToString(),
                                telefono = dr["telefono"].ToString(),
                                correo_electronico = dr["correo_electronico"].ToString(),

                            });
                        }
                    }
                }
            }

            return lista;
        }
        public DataTable Listar()
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(Conexion_DB.cadena_conexion))
            {
                string sql = "SELECT * FROM Cliente";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    con.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        dt.Load(dr);
                    }
                }
            }
            return dt;


        }
        public int Insertar(Cliente c)
        {
            using (SqlConnection cn = new SqlConnection(Conexion_DB.cadena_conexion))
            {
                string sql = "INSERT INTO Cliente (nombre_completo, dui, telefono, correo_electronico) " +
                             "VALUES (@nombre_completo, @dui, @telefono, @correo_electronico); " +
                             "SELECT SCOPE_IDENTITY();";
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cmd.Parameters.AddWithValue("@nombre_completo", c.nombre_completo);
                    cmd.Parameters.AddWithValue("@dui", c.dui);
                    cmd.Parameters.AddWithValue("@telefono", c.telefono);
                    cmd.Parameters.AddWithValue("@correo_electronico", c.correo_electronico);
                    cn.Open();
                    // Ejecuta el comando y obtiene el ID generado
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }

            }

        }
        public bool Actualizar(Cliente c)
        {
            using (SqlConnection cn = new SqlConnection(Conexion_DB.cadena_conexion))
            {
                string sql = "UPDATE Cliente SET nombre_completo = @nombre_completo, dui = @dui, " +
                             "telefono = @telefono, correo_electronico = @correo_electronico " +
                             "WHERE id = @id";
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cmd.Parameters.AddWithValue("@nombre_completo", c.nombre_completo);
                    cmd.Parameters.AddWithValue("@dui", c.dui);
                    cmd.Parameters.AddWithValue("@telefono", c.telefono);
                    cmd.Parameters.AddWithValue("@correo_electronico", c.correo_electronico);
                    cmd.Parameters.AddWithValue("@id", c.id);
                    cn.Open();

                    return cmd.ExecuteNonQuery() > 0;


                }
            }



        }
        public bool Eliminar(int id)
        {
            using (SqlConnection cn = new SqlConnection(Conexion_DB.cadena_conexion))
            {
                string sql = "DELETE FROM Cliente WHERE id = @id";
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
        public DataTable Buscar(string filtro)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(Conexion_DB.cadena_conexion))
            {
                string sql = "SELECT * FROM Cliente WHERE nombre_completo LIKE @texto OR dui LIKE @texto";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@texto", "%" + filtro + "%");
                    con.Open();
                    new SqlDataAdapter(cmd).Fill(dt);
                }
            }
            return dt;
        }
        public bool ExisteClientePorDui(string dui)
        {
            using (SqlConnection con = new SqlConnection(Conexion_DB.cadena_conexion))
            {
                string sql = "SELECT COUNT(*) FROM Cliente WHERE dui = @dui";

                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@dui", dui);
                    con.Open();
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0;
                }
            }
        }

    }
}
