using System;
using System.Data;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Karolina_Collection_.Reportes
{
    // Esta clase define cómo se verá el documento PDF del reporte de ventas (diseño y contenido)
    public class ReporteVentasDocumento : IDocument
    {
        // Variable privada que guarda los datos del reporte (fechas y tabla de ventas)
        private readonly ReporteVentasModel Modelo;

        // Constructor: recibe el modelo con los datos y lo guarda para usarlo después
        public ReporteVentasDocumento(ReporteVentasModel modelo)
        {
            Modelo = modelo;
        }

        // Método requerido por QuestPDF que devuelve metadatos del documento (título, autor, etc.)
        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

        // Método requerido por QuestPDF que devuelve configuraciones del documento
        public DocumentSettings GetSettings() => new DocumentSettings();

        // Este es el método principal que arma todo el documento PDF
        public void Compose(IDocumentContainer container)
        {
            // Creamos una página en el contenedor
            container.Page(page =>
            {
                // Establecemos el tamaño de la página como A4 (tamaño estándar de hoja)
                page.Size(PageSizes.A4);

                // Establecemos márgenes de 30 puntos en todos los lados
                page.Margin(30);

                // ========== ENCABEZADO (HEADER) ==========
                // Creamos el encabezado superior del documento
                page.Header().Column(col =>
                {
                    // Título principal del negocio en dorado y grande
                    col.Item().Text("KAROLINA COLLECTION").Bold().FontSize(20).FontColor("#d4af37");

                    // Eslogan en gris, cursiva y más pequeño
                    col.Item().Text("Elegancia y Estilo para Ti").FontSize(12).Italic().FontColor("#666666");

                    // Título del reporte con espacio arriba
                    col.Item().PaddingTop(10).Text("Reporte de Ventas por Período").FontSize(14).SemiBold();

                    // Rango de fechas del reporte (Desde... hasta...)
                    col.Item().Text($"Desde {Modelo.Inicio:dd/MM/yyyy} hasta {Modelo.Fin:dd/MM/yyyy}").FontSize(11);
                });

                // ========== CONTENIDO PRINCIPAL (CONTENT) ==========
                // Agregamos espacio de 20 puntos arriba y llamamos al método que genera la tabla
                page.Content().PaddingTop(20).Element(GenerarTabla);

                // ========== PIE DE PÁGINA (FOOTER) ==========
                // Creamos el pie de página centrado con número de página y fecha de generación
                page.Footer().AlignCenter().Text(txt =>
                {
                    txt.Span("Página ");
                    txt.CurrentPageNumber(); // Número de página actual
                    txt.Span(" | Generado el ");
                    txt.Span(DateTime.Now.ToString("dd/MM/yyyy HH:mm")); // Fecha y hora actual
                });
            });
        }

        // Método que genera la tabla de ventas con todos los datos
        private void GenerarTabla(IContainer container)
        {
            // Creamos una tabla dentro del contenedor
            container.Table(table =>
            {
                // ========== DEFINICIÓN DE COLUMNAS ==========
                // Definimos el ancho relativo de cada columna (números más altos = más anchas)
                table.ColumnsDefinition(cols =>
                {
                    cols.RelativeColumn(1.5f); // Fecha - ancho medio
                    cols.RelativeColumn(2.5f); // Cliente - más ancho
                    cols.RelativeColumn(3);    // Producto - el más ancho
                    cols.RelativeColumn(1);    // Cantidad - angosto
                    cols.RelativeColumn(1.5f); // Precio Unit. - medio
                    cols.RelativeColumn(1.5f); // Subtotal - medio
                });

                // ========== ENCABEZADO DE LA TABLA ==========
                // Creamos la fila de títulos de las columnas con fondo oscuro y letras blancas
                table.Header(header =>
                {
                    // Celda "Fecha" con fondo negro-azulado, padding de 8 puntos, texto blanco en negrita
                    header.Cell().Background("#1a1a2e").Padding(8).Text("Fecha").SemiBold().FontColor("#ffffff").FontSize(10);

                    // Celda "Cliente"
                    header.Cell().Background("#1a1a2e").Padding(8).Text("Cliente").SemiBold().FontColor("#ffffff").FontSize(10);

                    // Celda "Producto"
                    header.Cell().Background("#1a1a2e").Padding(8).Text("Producto").SemiBold().FontColor("#ffffff").FontSize(10);

                    // Celda "Cant." (abreviatura de Cantidad)
                    header.Cell().Background("#1a1a2e").Padding(8).Text("Cant.").SemiBold().FontColor("#ffffff").FontSize(10);

                    // Celda "Precio Unit." (Precio Unitario)
                    header.Cell().Background("#1a1a2e").Padding(8).Text("Precio Unit.").SemiBold().FontColor("#ffffff").FontSize(10);

                    // Celda "Subtotal"
                    header.Cell().Background("#1a1a2e").Padding(8).Text("Subtotal").SemiBold().FontColor("#ffffff").FontSize(10);
                });

                // Variable para acumular el total de todas las ventas
                decimal totalGeneral = 0;

                // Variable para contar las filas (usado para alternar colores)
                int fila = 0;

                // ========== FILAS DE DATOS ==========
                // Recorremos cada fila de la tabla de datos del modelo
                foreach (DataRow row in Modelo.Tabla.Rows)
                {
                    // Extraemos cada campo de la fila y lo convertimos al tipo correcto
                    DateTime fecha = Convert.ToDateTime(row["fecha"]);
                    string cliente = row["Cliente"].ToString();
                    string producto = row["Producto"].ToString();
                    int cantidad = Convert.ToInt32(row["cantidad"]);
                    decimal precio = Convert.ToDecimal(row["precio_unitario"]);
                    decimal subtotal = Convert.ToDecimal(row["SubTotal"]);

                    // Sumamos el subtotal al total general
                    totalGeneral += subtotal;

                    // Alternamos el color de fondo entre blanco y gris claro para mejor lectura
                    // Si la fila es par (0, 2, 4...) = blanco, si es impar (1, 3, 5...) = gris claro
                    string colorFondo = (fila % 2 == 0) ? "#ffffff" : "#f9f9f9";
                    fila++; // Incrementamos el contador de filas

                    // Agregamos cada celda de la fila con su respectivo dato
                    // Fecha: formato día/mes/año
                    table.Cell().Background(colorFondo).Padding(6).Text(fecha.ToString("dd/MM/yyyy")).FontSize(9);

                    // Cliente: texto normal
                    table.Cell().Background(colorFondo).Padding(6).Text(cliente).FontSize(9);

                    // Producto: texto normal
                    table.Cell().Background(colorFondo).Padding(6).Text(producto).FontSize(9);

                    // Cantidad: centrado
                    table.Cell().Background(colorFondo).Padding(6).AlignCenter().Text(cantidad.ToString()).FontSize(9);

                    // Precio: alineado a la derecha con formato de moneda (C2 = 2 decimales)
                    table.Cell().Background(colorFondo).Padding(6).AlignRight().Text(precio.ToString("C2")).FontSize(9);

                    // Subtotal: alineado a la derecha con formato de moneda
                    table.Cell().Background(colorFondo).Padding(6).AlignRight().Text(subtotal.ToString("C2")).FontSize(9);
                }

                // ========== FILA DE TOTAL GENERAL ==========
                // Creamos una fila final que abarca todas las 6 columnas con el total general
                // ColumnSpan(6) hace que esta celda ocupe las 6 columnas
                table.Cell().ColumnSpan(6).AlignRight().Padding(10).Background("#d4af37")
                     .Text($"TOTAL GENERAL: {totalGeneral:C2}").Bold().FontSize(12).FontColor("#1a1a2e");
            });
        }
    }
}