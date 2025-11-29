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
    public class MarcaDAL
    {
        //Metodo que retorna una lista de marcas desde la base de datos
        public List<Marca> Listar_marca()
        {
            //Crea una lista vacia de marcas que se obtendra desde DB
            List<Marca> lista = new List<Marca>();
            using (SqlConnection conexion = new SqlConnection(Conexion_DB.cadena_conexion))
            {
                try
                {
                    string consulta = "SELECT id, nombre_marca FROM Marca";
                    SqlCommand cmd = new SqlCommand(consulta, conexion);
                    //Esprecifica que el comando es una consulta
                    cmd.CommandType = CommandType.Text;

                    conexion.Open();
                    //Ejecuta la consulta y obtiene un lector de datos
                    using (SqlDataReader dr = cmd.ExecuteReader()) //dr lee los datos de la DB de forma secuencial
                    {
                        //Recorre los registros obtenidos
                        while (dr.Read())
                        {
                            //Crea un objeto Marca y lo agrega a la lista
                            lista.Add(new Marca()
                            {
                                id = Convert.ToInt32(dr["id"]),
                                nombre_marca = dr["nombre_marca"].ToString()
                            });
                        }

                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    //En caso de error, retorna una lista vacia
                    lista = new List<Marca>();
                }
            }
            //Retorna la lista de marcas obtenidas desde la DB
            return lista;
        }
    }
}
