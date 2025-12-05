using System;
using System.Data;
using Karolina_Collection_.CapaDatos;

namespace Karolina_Collection_.CapaNegocio
{
    // Esta clase actúa como intermediario entre la interfaz y la base de datos para los reportes
    public class ReporteBLL
    {
        // Creamos una instancia de la capa DAL para poder llamar sus métodos
        private ReporteDAL dal = new ReporteDAL();

        /// <summary>
        /// Obtiene el DataTable de ventas entre dos fechas
        /// </summary>

        // Este método obtiene las ventas de un período y valida que las fechas sean correctas
        public DataTable ObtenerVentasPorPeriodo(DateTime inicio, DateTime fin)
        {
            // Validamos que la fecha de inicio no sea posterior a la fecha final (eso no tendría sentido)
            if (inicio > fin)
                // Si las fechas están al revés, lanzamos un error con un mensaje explicativo
                throw new ArgumentException("La fecha de inicio no puede ser mayor que la fecha fin.");

            // Si las fechas son válidas, llamamos al método de la DAL que trae las ventas de la base de datos
            return dal.ReporteVentas(inicio, fin);
        }
    }
}