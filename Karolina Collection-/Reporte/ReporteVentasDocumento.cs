using System;
using System.Data;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Karolina_Collection_.Reportes
{
    public class ReporteVentasDocumento : IDocument
    {
        private readonly ReporteVentasModel Modelo;

        public ReporteVentasDocumento(ReporteVentasModel modelo)
        {
            Modelo = modelo;
        }

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;
        public DocumentSettings GetSettings() => new DocumentSettings();

        public void Compose(IDocumentContainer container)
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(30);

                // HEADER
                page.Header().Column(col =>
                {
                    col.Item().Text("KAROLINA COLLECTION").Bold().FontSize(20).FontColor("#d4af37");
                    col.Item().Text("Elegancia y Estilo para Ti").FontSize(12).Italic().FontColor("#666666");
                    col.Item().PaddingTop(10).Text("Reporte de Ventas por Período").FontSize(14).SemiBold();
                    col.Item().Text($"Desde {Modelo.Inicio:dd/MM/yyyy} hasta {Modelo.Fin:dd/MM/yyyy}").FontSize(11);
                });

                // CONTENT
                page.Content().PaddingTop(20).Element(GenerarTabla);

                // FOOTER
                page.Footer().AlignCenter().Text(txt =>
                {
                    txt.Span("Página ");
                    txt.CurrentPageNumber();
                    txt.Span(" | Generado el ");
                    txt.Span(DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
                });
            });
        }

        private void GenerarTabla(IContainer container)
        {
            container.Table(table =>
            {
                // DEFINICIÓN DE COLUMNAS
                table.ColumnsDefinition(cols =>
                {
                    cols.RelativeColumn(1.5f); // Fecha
                    cols.RelativeColumn(2.5f); // Cliente
                    cols.RelativeColumn(3);    // Producto
                    cols.RelativeColumn(1);    // Cantidad
                    cols.RelativeColumn(1.5f); // Precio Unit.
                    cols.RelativeColumn(1.5f); // Subtotal
                });

                // HEADER DE LA TABLA
                table.Header(header =>
                {
                    header.Cell().Background("#1a1a2e").Padding(8).Text("Fecha").SemiBold().FontColor("#ffffff").FontSize(10);
                    header.Cell().Background("#1a1a2e").Padding(8).Text("Cliente").SemiBold().FontColor("#ffffff").FontSize(10);
                    header.Cell().Background("#1a1a2e").Padding(8).Text("Producto").SemiBold().FontColor("#ffffff").FontSize(10);
                    header.Cell().Background("#1a1a2e").Padding(8).Text("Cant.").SemiBold().FontColor("#ffffff").FontSize(10);
                    header.Cell().Background("#1a1a2e").Padding(8).Text("Precio Unit.").SemiBold().FontColor("#ffffff").FontSize(10);
                    header.Cell().Background("#1a1a2e").Padding(8).Text("Subtotal").SemiBold().FontColor("#ffffff").FontSize(10);
                });

                decimal totalGeneral = 0;
                int fila = 0;

                // FILAS DE DATOS
                foreach (DataRow row in Modelo.Tabla.Rows)
                {
                    DateTime fecha = Convert.ToDateTime(row["fecha"]);
                    string cliente = row["Cliente"].ToString();
                    string producto = row["Producto"].ToString();
                    int cantidad = Convert.ToInt32(row["cantidad"]);
                    decimal precio = Convert.ToDecimal(row["precio_unitario"]);
                    decimal subtotal = Convert.ToDecimal(row["SubTotal"]);

                    totalGeneral += subtotal;

                    // Alternar color de fondo
                    string colorFondo = (fila % 2 == 0) ? "#ffffff" : "#f9f9f9";
                    fila++;

                    table.Cell().Background(colorFondo).Padding(6).Text(fecha.ToString("dd/MM/yyyy")).FontSize(9);
                    table.Cell().Background(colorFondo).Padding(6).Text(cliente).FontSize(9);
                    table.Cell().Background(colorFondo).Padding(6).Text(producto).FontSize(9);
                    table.Cell().Background(colorFondo).Padding(6).AlignCenter().Text(cantidad.ToString()).FontSize(9);
                    table.Cell().Background(colorFondo).Padding(6).AlignRight().Text(precio.ToString("C2")).FontSize(9);
                    table.Cell().Background(colorFondo).Padding(6).AlignRight().Text(subtotal.ToString("C2")).FontSize(9);
                }

                // FILA DE TOTAL
                table.Cell().ColumnSpan(6).AlignRight().Padding(10).Background("#d4af37")
                     .Text($"TOTAL GENERAL: {totalGeneral:C2}").Bold().FontSize(12).FontColor("#1a1a2e");
            });
        }
    }
}