using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karolina_Collection_.CapaDatos
{
    public class Cierre_cajaDAL
    {
        // 1. OBTENER TOTAL VENTAS DE HOY
        public static decimal ObtenerVentasDelDia(DateTime fecha)
        {
            //Variable para guardar el total de dinero
            decimal total = 0;
            using (SqlConnection con = new SqlConnection(Conexion_DB.cadena_conexion))
            {
                try
                {
                    // ISNULL devuelve 0 si no hay ventas (para evitar valores nulos)
                    // CAST convierte las fechas a solo DÍA (sin hora) para comparar correctamente
                    string sql = @"
                    SELECT ISNULL(SUM(total_venta), 0) 
                    FROM Venta 
                    WHERE CAST(fecha AS DATE) = CAST(@fecha AS DATE)";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@fecha", fecha);
                        con.Open();
                        //Se ejecuta la consulta y convierte el resultado a numero decimal
                        total = Convert.ToDecimal(cmd.ExecuteScalar());
                    }
                }
                catch (Exception)
                {
                    //En caso de error se pone total en sero
                    total = 0;
                }
            }
            //Devuelve el total de ventas
            return total;
        }
    }
}

