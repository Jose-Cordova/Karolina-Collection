using System;
using System.Data;
using System.Windows.Forms;
using Karolina_Collection_.CapaNegocio;
using Karolina_Collection_.Reportes;

namespace Karolina_Collection_.CapaPresentacion
{
    // Esta clase representa el formulario (ventana) de reportes de ventas
    public partial class frmReportes : Form
    {
        // Creamos una instancia de la capa de negocio para obtener los datos de ventas
        private ReporteBLL reporteBLL = new ReporteBLL();

        // Constructor: se ejecuta cuando se crea el formulario por primera vez
        public frmReportes()
        {
            // Inicializa todos los componentes visuales del formulario
            InitializeComponent();
        }

        // Se ejecuta automáticamente cuando el formulario termina de cargar (aparece en pantalla)
        private void frmReportes_Load(object sender, EventArgs e)
        {
            // Establecemos la fecha inicial como la fecha de hoy
            dtpFechaInicial.Value = DateTime.Today;

            // Configuración del DataGridView (tabla visual)
            // Las columnas se ajustan automáticamente para llenar todo el ancho disponible
            dgvReporte.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Al hacer clic, se selecciona toda la fila completa
            dgvReporte.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // El usuario no puede editar los datos directamente en la tabla
            dgvReporte.ReadOnly = true;

            // No permitir que el usuario agregue nuevas filas
            dgvReporte.AllowUserToAddRows = false;

            // No permitir que el usuario elimine filas
            dgvReporte.AllowUserToDeleteRows = false;

            // Ocultar la columna de encabezados de filas (los números a la izquierda)
            dgvReporte.RowHeadersVisible = false;
        }

        // =========================
        // BOTÓN: Generar Reporte
        // =========================
        // Se ejecuta cuando el usuario hace clic en el botón "Generar Reporte"
        private void btnGenerarReporte_Click(object sender, EventArgs e)
        {
            // Obtenemos la fecha de inicio del selector (solo la fecha, sin hora)
            DateTime inicio = dtpFechaInicial.Value.Date;

            // La fecha final es siempre HOY
            DateTime fin = DateTime.Today;

            // Intentamos obtener los datos del reporte
            try
            {
                // Llamamos a la capa de negocio para obtener las ventas del período
                DataTable tabla = reporteBLL.ObtenerVentasPorPeriodo(inicio, fin);

                // Verificamos si la tabla tiene datos (si hay ventas en ese período)
                if (tabla.Rows.Count == 0)
                {
                    // Si no hay ventas, mostramos un mensaje informativo
                    MessageBox.Show("No hay ventas en este período.", "Sin Datos",
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Limpiamos la tabla visual
                    dgvReporte.DataSource = null;

                    // Salimos del método
                    return;
                }

                // Si hay datos, los mostramos en la tabla visual
                dgvReporte.DataSource = tabla;

                // Ajustamos el ancho de las columnas automáticamente según el contenido
                dgvReporte.AutoResizeColumns();
            }
            // Si ocurre algún error al obtener los datos
            catch (Exception ex)
            {
                // Mostramos un mensaje de error con los detalles
                MessageBox.Show("Error al obtener los datos:\n" + ex.Message, "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Se ejecuta cuando el usuario hace clic en el botón "Volver al menú"
        private void btnVolver_menu_Click(object sender, EventArgs e)
        {
            // Cierra este formulario y regresa al menú anterior
            Close();
        }

        // Este método puede estar vacío o eliminarlo
        // Se ejecuta cuando se hace clic en una celda del DataGridView (actualmente no hace nada)
        private void dgvReporte_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Vacío
        }

        // Se ejecuta cuando el usuario hace clic en el botón "Exportar a PDF"
        private void btnImportarExcel_Click(object sender, EventArgs e)
        {
            // Verificamos que haya datos en la tabla para exportar
            if (dgvReporte.DataSource == null)
            {
                // Si no hay datos, mostramos un mensaje pidiendo que primero genere el reporte
                MessageBox.Show("Primero genere el reporte en pantalla.", "Aviso",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Obtenemos las fechas del período del reporte
            DateTime inicio = dtpFechaInicial.Value.Date;
            DateTime fin = DateTime.Today;

            // Obtenemos la tabla de datos que está mostrándose actualmente
            DataTable tabla = (DataTable)dgvReporte.DataSource;

            // Creamos un cuadro de diálogo para que el usuario elija dónde guardar el PDF
            SaveFileDialog saveFileDialog1 = new SaveFileDialog
            {
                // Título de la ventana
                Title = "Guardar Reporte en PDF",

                // Solo permite guardar archivos PDF
                Filter = "Archivo PDF (*.pdf)|*.pdf",

                // Nombre sugerido del archivo con las fechas incluidas
                FileName = $"ReporteVentas_{inicio:yyyy-MM-dd}_a_{fin:yyyy-MM-dd}.pdf"
            };

            // Si el usuario cancela el cuadro de diálogo, salimos del método
            if (saveFileDialog1.ShowDialog() != DialogResult.OK) return;

            // Obtenemos la ruta completa donde el usuario quiere guardar el archivo
            string ruta = saveFileDialog1.FileName;

            // Intentamos generar el PDF
            try
            {
                // Generar PDF usando la clase separada
                // Le pasamos la tabla de datos, las fechas y la ruta donde guardarlo
                ReporteVentasPDF.GenerarPDF(tabla, inicio, fin, ruta);

                // Si todo salió bien, mostramos mensaje de éxito con la ubicación del archivo
                MessageBox.Show("PDF generado correctamente:\n" + ruta, "Éxito",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            // Si ocurre algún error al generar el PDF
            catch (Exception ex)
            {
                // Mostramos un mensaje de error con los detalles
                MessageBox.Show("Error al generar PDF:\n" + ex.Message, "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}