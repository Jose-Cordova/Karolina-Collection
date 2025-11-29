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
    public class TallaDAL
    {
        //Metodo que retorna una lista de tallas desde la base de datos
        public List<Talla> Listar_talla()
        {
            //Crea una lista vacia de tallas que se obtendra desde DB
            List<Talla> lista = new List<Talla>();
            using (SqlConnection conexion = new SqlConnection(Conexion_DB.cadena_conexion))
            {
                try
                {
                    string consulta = "SELECT id, nombre_talla FROM Talla";
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
                            //Crea un objeto Talla y lo agrega a la lista
                            lista.Add(new Talla
                            {
                                id = Convert.ToInt32(dr["id"]),
                                nombre_talla = dr["nombre_talla"].ToString()
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    //En caso de error, retorna una lista vacia
                    lista = new List<Talla>();
                }
            }
            //Retorna la lista de tallas obtenidas desde la DB
            return lista;
        }
    }
}

