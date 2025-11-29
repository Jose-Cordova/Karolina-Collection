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
    public class ColorDAL
    {
        //Metodo que retorna una lista de colores desde la base de datos
        public List<Color> Listar_color()
        {
            //Crea una lista vacia de colores que se obtendra desde DB
            List<Color> lista = new List<Color>();
            using (SqlConnection conexion = new SqlConnection(Conexion_DB.cadena_conexion))
            {
                try
                {
                    string consulta = "SELECT id, nombre_color FROM Color";
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
                            //Crea un objeto Color y lo agrega a la lista
                            lista.Add(new Color
                            {
                                id = Convert.ToInt32(dr["id"]),
                                nombre_color = dr["nombre_color"].ToString()
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    //En caso de error, retorna una lista vacia
                    lista = new List<Color>();
                }
            }
            //Retorna la lista de colores obtenidas desde la DB
            return lista;
        }
    }
}

