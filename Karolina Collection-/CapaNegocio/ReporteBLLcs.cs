using System;
using System.Data;
using Karolina_Collection_.CapaDatos;

namespace Karolina_Collection_.CapaNegocio
{
    public class ReporteBLL
    {
        private ReporteDAL dal = new ReporteDAL();

        /// <summary>
        /// Obtiene el DataTable de ventas entre dos fechas
        /// </summary>
        public DataTable ObtenerVentasPorPeriodo(DateTime inicio, DateTime fin)
        {
            if (inicio > fin)
                throw new ArgumentException("La fecha de inicio no puede ser mayor que la fecha fin.");

            return dal.ReporteVentas(inicio, fin);
        }
    }
}
