using iTextSharp.text;
using iTextSharp.text.pdf;
using Karolina_Collection_.CapaNegocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Font = iTextSharp.text.Font;

namespace Karolina_Collection_.CapaPresentacion
{
    public partial class frmCierreCaja : Form
    {
        public frmCierreCaja()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmCierreCaja_Load(object sender, EventArgs e)
        {
            //Llama al método que carga los datos iniciales automáticamente
            CargarDatosAutomaticos();
        }
        //Este método calcula y muestra automáticamente los valores
        private void CargarDatosAutomaticos()
        {
            //FONDO DE CAJA INICIAL (Izquierda)
            // Lo ponemos fijo en 50.00 como ejemplo (o 0 si prefieres)
            decimal inicial = 50.00m;
            nudFondoCajaInicial.Value = inicial;

            //TOTAL DE INGRESO (Izquierda)
            //Traemos las ventas de la BD
            decimal ventasHoy = Cierre_cajaBLL.ObtenerVentasDelDia(DateTime.Now);
            nudTotalIngreso.Value = ventasHoy;

            //TOTAL ESPERADO (Izquierda - Calculado)
            //Fórmula: (Inicial + Ingreso)
            decimal esperado = (inicial + ventasHoy);
            nudTotalEsperado.Value = esperado;

            //Bloquear controles de la izquierda para que no los editen manualmente
            nudFondoCajaInicial.Enabled = false;
            nudTotalIngreso.Enabled = false;
            nudTotalEsperado.Enabled = false;
        }

        private void btnConfirmarCierre_Click(object sender, EventArgs e)
        {
            //Validar que ingresó el monto físico (Caja derecha "Fondo inicial")
            if (string.IsNullOrWhiteSpace(nudDineroFisico.Text))
            {
                MessageBox.Show("Por favor ingresa el dinero contado en la caja (Lado derecho).", "Campo Requerido", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //Convertir a numeros decimales
            decimal montoFisico = 0;
            //Validacion que intentan convertir a decimal
            if (!decimal.TryParse(nudDineroFisico.Text, out montoFisico))
            {
                MessageBox.Show("Ingresa un número válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //Se muestra una ventana para la confirmacion
            DialogResult respuesta = MessageBox.Show(
                "¿Estás seguro de cerrar caja? Se generará el reporte PDF.", "Confirmar",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            //Si el usuario preciona si
            if (respuesta == DialogResult.Yes)
            {
                //Genera el PDF
                GenerarPDF(montoFisico);
            }
        }
        // Este método genera el PDF con el reporte de cierre de caja
        private void GenerarPDF(decimal fisico)
        {
            //Usuario elige donde guardar el PDF
            SaveFileDialog savefile = new SaveFileDialog();
            savefile.FileName = $"Cierre_{DateTime.Now.ToString("dd-MM-yyyy_HH-mm")}.pdf";
            savefile.Filter = "PDF Files|*.pdf";

            //Condicion si el usuario preciona si
            if (savefile.ShowDialog() == DialogResult.OK)
            {
                //Se intenta convertir el PDF
                try
                {
                    //Crea un documento PDF tamaño carta
                    Document doc = new Document(PageSize.LETTER, 25, 25, 25, 25);
                    //Se crea el archivo en la ruta que se eligio
                    PdfWriter.GetInstance(doc, new FileStream(savefile.FileName, FileMode.Create));

                    doc.Open();

                    //Encabesado del pdf
                    Paragraph titulo = new Paragraph("REPORTE DE CIERRE DE CAJA\n");
                    titulo.Alignment = Element.ALIGN_CENTER; //Centrado
                    titulo.Font = FontFactory.GetFont("Arial", 18, iTextSharp.text.Font.BOLD); //Tipo de letra
                    doc.Add(titulo); //Agregamos el titulo de el documento

                    //Se agrega la fecha y hora actual de cierre de caja
                    doc.Add(new Paragraph($"Fecha: {DateTime.Now.ToString("dd/MM/yyyy HH:mm")}\n\n"));

                    //TABLA
                    PdfPTable tabla = new PdfPTable(2);
                    tabla.WidthPercentage = 100; //Ocupa el 100% de el documento

                    // Estilos de celda
                    Font fuenteNegrita = FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD);

                    //FILAS
                    //Total de Ingreso (Ventas del dia)
                    AgregarFila(tabla, "Total de Ingreso", nudTotalIngreso.Value.ToString("C2"));

                    //Fondo de caja inicial (Sistema)
                    AgregarFila(tabla, "Fondo de Caja Inicial (Sistema)", nudFondoCajaInicial.Value.ToString("C2"));

                    //Total Esperado (Fila resaltada con gris)
                    //Celda Isquierda: concepto
                    PdfPCell celdaEsp = new PdfPCell(new Phrase("TOTAL ESPERADO", fuenteNegrita));
                    celdaEsp.BackgroundColor = BaseColor.LIGHT_GRAY;
                    tabla.AddCell(celdaEsp);

                    //Celda derecha: valor
                    PdfPCell celdaEspValor = new PdfPCell(new Phrase(nudTotalEsperado.Value.ToString("C2"), fuenteNegrita));
                    celdaEspValor.BackgroundColor = BaseColor.LIGHT_GRAY;
                    celdaEspValor.HorizontalAlignment = Element.ALIGN_RIGHT;
                    tabla.AddCell(celdaEspValor);

                    //Dinero fisico contado por el usuario
                    AgregarFila(tabla, "Dinero Contado (Físico)", fisico.ToString("C2"));

                    //Fórmula: Dinero contado - Dinero esperado
                    //Si es positivo: sobra dinero, si es negativo: falta dinero
                    decimal diferencia = fisico - nudTotalEsperado.Value;

                    //Celda izquierda: concepto
                    PdfPCell celdaDif = new PdfPCell(new Phrase("DIFERENCIA", fuenteNegrita));
                    tabla.AddCell(celdaDif);

                    //Celda derecha: valor de la diferencia
                    PdfPCell celdaDifValor = new PdfPCell(new Phrase(diferencia.ToString("C2"), fuenteNegrita));
                    celdaDifValor.HorizontalAlignment = Element.ALIGN_RIGHT;
                    tabla.AddCell(celdaDifValor);

                    //Agregamos la tabla completa al documento
                    doc.Add(tabla);

                    //Verificamos si el campo de observaciones tiene texto
                    if (!string.IsNullOrWhiteSpace(txtObservaciones.Text))
                    {
                        //Agregamos el título "Observaciones:"
                        doc.Add(new Paragraph("\nObservaciones:", fuenteNegrita));
                        //Agregamos el texto que escribió el usuario
                        doc.Add(new Paragraph(txtObservaciones.Text));
                    }
                    //Cerramos el documento (esto guarda el archivo)
                    doc.Close();

                    //Abrimos automáticamente el PDF recién creado
                    System.Diagnostics.Process.Start(savefile.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al generar PDF: " + ex.Message);
                }
            }
        }
        //Metodo para agregar todas las filas del PDF
        private void AgregarFila(PdfPTable tabla, string concepto, string valor)
        {
            tabla.AddCell(new Phrase(concepto));
            PdfPCell celdaValor = new PdfPCell(new Phrase(valor));
            celdaValor.HorizontalAlignment = Element.ALIGN_RIGHT;
            tabla.AddCell(celdaValor);
        }
    }
    
}
