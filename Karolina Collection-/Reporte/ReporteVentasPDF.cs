using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using System;
using System.Data;

namespace Karolina_Collection_.Reportes
{
    public class ReporteVentasPDF
    {
        public static void GenerarPDF(DataTable tabla, DateTime inicio, DateTime fin, string rutaArchivo)
        {
            QuestPDF.Settings.License = LicenseType.Community;

            var modelo = new ReporteVentasModel(tabla, inicio, fin);
            var documento = new ReporteVentasDocumento(modelo);

            documento.GeneratePdf(rutaArchivo);
        }
    }
}
