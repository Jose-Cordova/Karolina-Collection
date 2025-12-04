using System;
using System.Data;

namespace Karolina_Collection_.Reportes
{
    public class ReporteVentasModel
    {
        public DataTable Tabla { get; }
        public DateTime Inicio { get; }
        public DateTime Fin { get; }

        public ReporteVentasModel(DataTable tabla, DateTime inicio, DateTime fin)
        {
            Tabla = tabla;
            Inicio = inicio;
            Fin = fin;
        }
    }
}
