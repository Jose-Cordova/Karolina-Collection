using System;
using System.Data;
using System.Windows.Forms;
using Karolina_Collection_.CapaNegocio;
using Karolina_Collection_.Reportes;

namespace Karolina_Collection_.CapaPresentacion
{
    public partial class frmReportes : Form
    {
        private ReporteBLL reporteBLL = new ReporteBLL();

        public frmReportes()
        {
            InitializeComponent();
        }

        private void frmReportes_Load(object sender, EventArgs e)
        {
            dtpFechaInicial.Value = DateTime.Today;

            // Configuración del DataGridView
            dgvReporte.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvReporte.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvReporte.ReadOnly = true;
            dgvReporte.AllowUserToAddRows = false;
            dgvReporte.AllowUserToDeleteRows = false;
            dgvReporte.RowHeadersVisible = false;
        }

        // =========================
        // BOTÓN: Generar Reporte
        // =========================
        private void btnGenerarReporte_Click(object sender, EventArgs e)
        {
            DateTime inicio = dtpFechaInicial.Value.Date;
            DateTime fin = DateTime.Today;

            try
            {
                DataTable tabla = reporteBLL.ObtenerVentasPorPeriodo(inicio, fin);

                if (tabla.Rows.Count == 0)
                {
                    MessageBox.Show("No hay ventas en este período.", "Sin Datos",
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dgvReporte.DataSource = null;
                    return;
                }

                dgvReporte.DataSource = tabla;
                dgvReporte.AutoResizeColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener los datos:\n" + ex.Message, "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void btnVolver_menu_Click(object sender, EventArgs e)
        {
            Close();
        }

        // Este método puede estar vacío o eliminarlo
        private void dgvReporte_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Vacío
        }

        private void btnImportarExcel_Click(object sender, EventArgs e)
        {
            if (dgvReporte.DataSource == null)
            {
                MessageBox.Show("Primero genere el reporte en pantalla.", "Aviso",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DateTime inicio = dtpFechaInicial.Value.Date;
            DateTime fin = DateTime.Today;
            DataTable tabla = (DataTable)dgvReporte.DataSource;

            SaveFileDialog saveFileDialog1 = new SaveFileDialog
            {
                Title = "Guardar Reporte en PDF",
                Filter = "Archivo PDF (*.pdf)|*.pdf",
                FileName = $"ReporteVentas_{inicio:yyyy-MM-dd}_a_{fin:yyyy-MM-dd}.pdf"
            };

            if (saveFileDialog1.ShowDialog() != DialogResult.OK) return;

            string ruta = saveFileDialog1.FileName;

            try
            {
                // Generar PDF usando la clase separada
                ReporteVentasPDF.GenerarPDF(tabla, inicio, fin, ruta);

                MessageBox.Show("PDF generado correctamente:\n" + ruta, "Éxito",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar PDF:\n" + ex.Message, "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}