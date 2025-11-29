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
    public class CategoriaDAL
    {
        //Metodo que retorna una lista de categorias desde la base de datos
        public List<Categoria> Listar_categoria()
        {
            //Crea una lista vacia de categorias que se obtendra desde DB
            List<Categoria> lista = new List<Categoria>();
            using (SqlConnection conexion = new SqlConnection(Conexion_DB.cadena_conexion))
            {
                try
                {
                    string consulta = "SELECT id, nombre_categoria FROM Categoria";
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
                            //Crea un objeto Categoria y lo agrega a la lista
                            lista.Add(new Categoria
                            {
                                id = Convert.ToInt32(dr["id"]),
                                nombre_categoria = dr["nombre_categoria"].ToString()
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    //En caso de error, retorna una lista vacia
                    lista = new List<Categoria>();
                }
            }
            //Retorna la lista de categorias obtenidas desde la DB
            return lista;
        }
    }
}
