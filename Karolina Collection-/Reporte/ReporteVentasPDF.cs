using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using System;
using System.Data;

namespace Karolina_Collection_.Reportes
{
    // Esta clase es la encargada de generar el archivo PDF del reporte de ventas
    public class ReporteVentasPDF
    {
        // Método estático que genera el PDF con los datos proporcionados y lo guarda en una ruta específica
        public static void GenerarPDF(DataTable tabla, DateTime inicio, DateTime fin, string rutaArchivo)
        {
            // Establecemos la licencia de QuestPDF como Community (versión gratuita)
            QuestPDF.Settings.License = LicenseType.Community;

            // Creamos el modelo que contiene los datos del reporte (tabla de ventas y fechas)
            var modelo = new ReporteVentasModel(tabla, inicio, fin);

            // Creamos el documento PDF pasándole el modelo con los datos
            var documento = new ReporteVentasDocumento(modelo);

            // Generamos el archivo PDF y lo guardamos en la ruta especificada
            documento.GeneratePdf(rutaArchivo);
        }
    }
}