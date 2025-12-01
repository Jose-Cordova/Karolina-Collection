using Karolina_Collection_.CapaEntidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karolina_Collection_.CapaDatos
{
    public class ClienteDAL
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

    }
}
