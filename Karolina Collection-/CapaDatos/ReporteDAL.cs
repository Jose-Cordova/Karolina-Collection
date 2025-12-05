using System;
using System.Data;
using System.Data.SqlClient;
// Importamos las librerías necesarias para trabajar con fechas, datos y SQL

// Definimos en qué carpeta/espacio de nombres está esta clase
namespace Karolina_Collection_.CapaDatos
{
    // Esta clase maneja los reportes de ventas desde la base de datos
    public class ReporteDAL
    {
        // Comentario de documentación que explica qué hace este método
        /// <summary>
        /// Obtiene las ventas entre dos fechas usando el SP corregido
        /// </summary>

        // ========== MÉTODO: GENERAR REPORTE DE VENTAS POR PERÍODO ==========
        // Este método trae todas las ventas realizadas entre dos fechas específicas
        // Recibe: fecha de inicio y fecha de fin
        // Devuelve: una tabla con todas las ventas en ese rango
        public DataTable ReporteVentas(DateTime fechaInicio, DateTime fechaFin)
        {
            // Creamos una tabla vacía donde guardaremos los resultados del reporte
            DataTable tabla = new DataTable();

            // Abrimos conexión a la base de datos (como abrir la puerta del almacén)
            using (SqlConnection conn = new SqlConnection(Conexion_DB.cadena_conexion))
            {
                // Creamos un comando ESPECIAL que llama a un Stored Procedure (SP)
                // Un SP es como una "función guardada" en la base de datos
                // "sp_reporte_ventas_periodo" es el nombre del procedimiento almacenado
                using (SqlCommand cmd = new SqlCommand("sp_reporte_ventas_periodo", conn))
                {
                    // Le decimos al comando que NO es una consulta SQL normal
                    // sino que es un Stored Procedure (procedimiento almacenado)
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Pasamos los parámetros al Stored Procedure:
                    // @FechaInicio: la fecha desde donde queremos el reporte
                    // .Date asegura que solo enviamos la fecha (sin hora)
                    cmd.Parameters.AddWithValue("@FechaInicio", fechaInicio.Date);

                    // @FechaFin: la fecha hasta donde queremos el reporte
                    cmd.Parameters.AddWithValue("@FechaFin", fechaFin.Date);

                    // Abrimos la conexión a la base de datos
                    conn.Open();

                    // Ejecutamos el Stored Procedure y leemos los resultados
                    // El SP hace toda la lógica compleja dentro de la base de datos
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        // Cargamos todos los datos que devolvió el SP en nuestra tabla
                        tabla.Load(dr);
                    }
                }
            }

            // Devolvemos la tabla completa con todas las ventas del período
            return tabla;
        }
    }
}