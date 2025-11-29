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
    public class ProveedorDAL
    {
        //Metodo que retorna una lista de proveedores desde la base de datos
        public List<Proveedor> Listar_proveedor()
        {
            //Crea una lista vacia de proveedores que se obtendra desde DB
            List<Proveedor> lista = new List<Proveedor>();
            using (SqlConnection conexion = new SqlConnection(Conexion_DB.cadena_conexion))
            {
                try
                {
                    string consulta = "SELECT id, nombre_proveedor FROM Proveedor";
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
                            //Crea un objeto proveedor y lo agrega a la lista
                            lista.Add(new Proveedor
                            {
                                id = Convert.ToInt32(dr["id"]),
                                nombre_proveedor = dr["nombre_proveedor"].ToString()
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    //En caso de error, retorna una lista vacia
                    lista = new List<Proveedor>();
                }
            }
            //Retorna la lista de proveedores obtenidas desde la DB
            return lista;
        }
    }
}
