using Karolina_Collection_.CapaEntidades;
using Karolina_Collection_.CapaNegocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Karolina_Collection_.CapaPresentacion
{
    public partial class frmClientes : Form
    {
         int cliente_id = 0;
        ClienteBLL bll = new ClienteBLL();
        public frmClientes()
        {
            InitializeComponent();
        }

        private void btnVolver_menu_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmClientes_Load(object sender, EventArgs e)
        {
            CargarDatos();
            Limpiar();
            ActualizarBotones();



        }
        private void CargarDatos()
        {
            dgvClientes.DataSource = bll.Listar();

            dgvClientes.Columns["id"].HeaderText = "ID";
            dgvClientes.Columns["nombre_completo"].HeaderText = "Nombre Completo";
            dgvClientes.Columns["dui"].HeaderText = "DUI";
            dgvClientes.Columns["telefono"].HeaderText = "Teléfono";
            dgvClientes.Columns["correo_electronico"].HeaderText = "Correo Electrónico";
        }
        private void Limpiar()
        {
            cliente_id = 0;
            txtNombreCompleto.Clear();
            mkdDui.Clear();
            mkdTelefono.Clear();
            txtCorreo.Clear();

            cliente_id = 0;
            ActualizarBotones();
        }
        private void Actualizar()
        {
            try
            {
                if (cliente_id == 0)
                {
                    MessageBox.Show("Seleccione un cliente para actualizar.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                // Crear objeto Cliente con tus propiedades reales
                Cliente c = new Cliente
                {
                    id = cliente_id,
                    nombre_completo = txtNombreCompleto.Text,
                    dui = mkdDui.Text,
                    telefono = mkdTelefono.Text,
                    correo_electronico = txtCorreo.Text
                };
                // Actualizar usando tu método BLL
                bll.Actualizar(c);
                // Mensaje de éxito
                MessageBox.Show("Cliente actualizado con éxito", "Actualización",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarDatos();
                Limpiar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {

            // --- VALIDACIONES BÁSICAS ---
            if (string.IsNullOrWhiteSpace(txtNombreCompleto.Text))
            {
                MessageBox.Show("El nombre del cliente es obligatorio.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtNombreCompleto.Focus();
                return;
            }
            //Validacion para que el correo sea correcto
            if (!Validaciones.EsCorreoElectronico(txtCorreo.Text))
            {
                MessageBox.Show("El correo electrónico no es válido.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCorreo.Focus();
                return;
            }

            if (!mkdDui.MaskCompleted)
            {
                MessageBox.Show("El DUI es obligatorio.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                mkdDui.Focus();
                return;
            }

            if (!mkdTelefono.MaskCompleted)
            {
                MessageBox.Show("El teléfono es obligatorio.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                mkdTelefono.Focus();
                return;
            }




            // --- CREAR OBJETO CLIENTE ---
            Cliente c = new Cliente
            {
                id = 0, // <-- SOLO INSERTAR, nunca modificar
                nombre_completo = txtNombreCompleto.Text.Trim(),
                dui = mkdDui.Text.Trim(),
                telefono = mkdTelefono.Text.Trim(),
                correo_electronico = txtCorreo.Text.Trim()
            };

            // --- GUARDAR EN BASE DE DATOS ---
            try
            {
                int nuevoId = bll.Guardar(c);

                MessageBox.Show("Cliente registrado con éxito.",
                    "Guardado", MessageBoxButtons.OK, MessageBoxIcon.Information);

                CargarDatos();
                Limpiar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al registrar: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            ActualizarBotones();

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (cliente_id == 0)
            {
                MessageBox.Show("Seleccione un cliente para eliminar.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Confirmación YES/NO
            if (MessageBox.Show("¿Está seguro de eliminar este cliente?", "Confirmación",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    // Llama a la BLL para eliminar
                    bll.Eliminar(cliente_id);

                    MessageBox.Show("Cliente eliminado con éxito.", "Eliminación",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    CargarDatos(); // Recarga la tabla
                    Limpiar();     // Limpia los campos
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar: " + ex.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void txtBuscarCliente_TextChanged(object sender, EventArgs e)
        {
            dgvClientes.DataSource = bll.Buscar(txtBuscarCliente.Text);
        }

        private void dgvClientes_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0)
            {
                // Toma el ID del cliente
                cliente_id = Convert.ToInt32(dgvClientes.Rows[e.RowIndex].Cells["id"].Value);

                // Pasa los datos a las cajas de texto
                txtNombreCompleto.Text = dgvClientes.Rows[e.RowIndex].Cells["nombre_completo"].Value.ToString();
                mkdDui.Text = dgvClientes.Rows[e.RowIndex].Cells["dui"].Value.ToString();
                mkdTelefono.Text = dgvClientes.Rows[e.RowIndex].Cells["telefono"].Value.ToString();
                txtCorreo.Text = dgvClientes.Rows[e.RowIndex].Cells["correo_electronico"].Value.ToString();
            }
            ActualizarBotones();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            Actualizar();
        }
        public void ActualizarBotones()
        {
            bool haySeleccion = cliente_id > 0;
            bool clienteExiste = ClienteExiste();

            bool dominioAgregar =
                !string.IsNullOrWhiteSpace(txtNombreCompleto.Text) &&
                mkdDui.MaskCompleted &&
                mkdTelefono.MaskCompleted &&
                !string.IsNullOrWhiteSpace(txtCorreo.Text) &&
                !clienteExiste &&
                !haySeleccion;

            btnRegistrar.Enabled = dominioAgregar;   // Agregar
            btnActualizar.Enabled = haySeleccion;    // Editar
            btnEliminar.Enabled = haySeleccion;      // Eliminar

            btnLimpiar.Enabled =
                !string.IsNullOrWhiteSpace(txtNombreCompleto.Text) ||
                mkdDui.MaskCompleted ||
                mkdTelefono.MaskCompleted ||
                !string.IsNullOrWhiteSpace(txtCorreo.Text);
        }
        public bool ClienteExiste()
        {
            ClienteBLL bll = new ClienteBLL();
            return bll.ExisteClientePorDui(mkdDui.Text);
        }

        private void txtNombreCompleto_TextChanged(object sender, EventArgs e)
        {
            ActualizarBotones();
        }

        private void txtCorreo_TextChanged(object sender, EventArgs e)
        {
            ActualizarBotones();
        }

        private void mkdTelefono_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void mkdDui_TextChanged(object sender, EventArgs e)
        {
            ActualizarBotones();
        }

        private void mkdTelefono_TextChanged(object sender, EventArgs e)
        {
            ActualizarBotones();
        }
    }
}
