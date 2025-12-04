using Karolina_Collection_.CapaEntidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karolina_Collection_.CapaDatos
{
    public class Metodo_pago_DAL
    {
        public static List<Metodo_pago> Listar()

        {
            // Este método también servirá para llenar el ComboBox de Metodo_pago.
            List<Metodo_pago> lista = new List<Metodo_pago>();

            using (SqlConnection con = new SqlConnection(Conexion_DB.cadena_conexion))
            {
                string sql = "SELECT * FROM Metodo_pago";

                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    con.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        // Mientras haya filas por leer (mientras existan métodos de pago)
                        // Read() avanza a la siguiente fila y devuelve true si hay datos
                        while (dr.Read())
                        {
                            // Por cada fila encontrada, creamos un nuevo objeto Metodo_pago
                            // y lo agregamos a nuestra lista
                            lista.Add(new Metodo_pago
                            {
                                id = Convert.ToInt32(dr["id"]),
                                tipo = dr["tipo"].ToString()
                            });
                        }
                    }
                }
            }

            return lista;
        }

    }
}
