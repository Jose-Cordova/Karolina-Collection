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
    public class Sub_categoriaDAL
    {
        //Metodo que retorna una lista de subcategorias desde la DB segun la categoria seleccionada
        public List<Sub_categoria> Listar_sub_categoria(int id_categoria)
        {
            //Crea una lista vacia de sub_categorias que se obtendra desde DB
            List<Sub_categoria> lista = new List<Sub_categoria>();
            using (SqlConnection conexion = new SqlConnection(Conexion_DB.cadena_conexion))
            {
                try
                {
                    string consulta = "SELECT id, nombre_sub_categoria FROM Sub_categoria WHERE id_categoria = @id_categoria";
                    SqlCommand cmd = new SqlCommand(consulta, conexion);
                    cmd.Parameters.AddWithValue("@id_categoria", id_categoria);
                    //Esprecifica que el comando es una consulta
                    cmd.CommandType = CommandType.Text;

                    conexion.Open();
                    //Ejecuta la consulta y obtiene un lector de datos
                    using (SqlDataReader dr = cmd.ExecuteReader()) //dr lee los datos de la DB de forma secuencial
                    {
                        //Recorre los registros obtenidos
                        while (dr.Read())
                        {
                            //Crea un objeto sub_categoria y lo agrega a la lista
                            lista.Add(new Sub_categoria
                            {
                                id = Convert.ToInt32(dr["id"]),
                                nombre_sub_categoria = dr["nombre_sub_categoria"].ToString()
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    //En caso de error, retorna una lista vacia
                    lista = new List<Sub_categoria>();
                }
            }
            //Retorna la lista de sub_categorias obtenidas desde la DB
            return lista;
        }
    }
}

