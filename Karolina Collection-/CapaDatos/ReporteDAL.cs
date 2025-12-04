using System;
using System.Data;
using System.Data.SqlClient;

namespace Karolina_Collection_.CapaDatos
{
    public class ReporteDAL
    {
        /// <summary>
        /// Obtiene las ventas entre dos fechas usando el SP corregido
        /// </summary>
        public DataTable ReporteVentas(DateTime fechaInicio, DateTime fechaFin)
        {
            DataTable tabla = new DataTable();

            using (SqlConnection conn = new SqlConnection(Conexion_DB.cadena_conexion))
            {
                using (SqlCommand cmd = new SqlCommand("sp_reporte_ventas_periodo", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@FechaInicio", fechaInicio.Date);
                    cmd.Parameters.AddWithValue("@FechaFin", fechaFin.Date);

                    conn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        tabla.Load(dr);
                    }
                }
            }

            return tabla;
        }
    }
}
