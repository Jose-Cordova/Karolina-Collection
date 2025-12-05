using System;
using System.Data;

namespace Karolina_Collection_.Reportes
{
    // Esta clase es un modelo (contenedor) que guarda los datos necesarios para generar el reporte de ventas
    public class ReporteVentasModel
    {
        // Propiedad que guarda la tabla con todos los datos de las ventas (solo lectura, no se puede modificar después)
        public DataTable Tabla { get; }

        // Propiedad que guarda la fecha de inicio del período del reporte (solo lectura)
        public DateTime Inicio { get; }

        // Propiedad que guarda la fecha de fin del período del reporte (solo lectura)
        public DateTime Fin { get; }

        // Constructor: recibe los tres datos necesarios y los guarda en las propiedades
        public ReporteVentasModel(DataTable tabla, DateTime inicio, DateTime fin)
        {
            // Guardamos la tabla de ventas
            Tabla = tabla;

            // Guardamos la fecha de inicio
            Inicio = inicio;

            // Guardamos la fecha de fin
            Fin = fin;
        }
    }
}