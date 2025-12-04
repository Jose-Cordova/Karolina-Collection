using Karolina_Collection_.CapaNegocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Karolina_Collection_.CapaPresentacion
{
    public partial class frmUsuario : Form
    {
        public frmUsuario()
        {
            InitializeComponent();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {

            try
            {
                int id = int.Parse(txtId.Text);
                var r = MessageBox.Show("¿Eliminar usuario?", "Confirmar", MessageBoxButtons.YesNo);
                if (r == DialogResult.Yes)
                {
                    bool ok = Usuario_BLL.Eliminar(id);
                    MessageBox.Show(ok ? "Eliminado" : "No eliminado");
                    CargarUsuarios();
                    Limpiar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dgvUsuarios_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
          
        }
        private void Limpiar()
        {
            txtId.Text = "";
            txtUsuario.Text = "";
            txtClave.Text = "";
            cbxRol.SelectedIndex = -1;
            chkEstado.Checked = false;
            txtClave.Enabled = true;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                // --- VALIDACIÓN INTEGRADA ---
                string clave = txtClave.Text.Trim();

                if (clave.Length < 8 || !clave.Any(c => !char.IsLetterOrDigit(c)))
                {
                    MessageBox.Show("La contraseña debe tener al menos 8 caracteres y contener un signo.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                // -----------------------------

                int id = Usuario_BLL.Insertar(txtUsuario.Text.Trim(), clave, cbxRol.Text);
                MessageBox.Show("Usuario creado ID: " + id);
                Limpiar();
                CargarUsuarios();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                int id = int.Parse(txtId.Text);
                bool estado = chkEstado.Checked;
                bool ok = Usuario_BLL.Actualizar(id, txtUsuario.Text.Trim(), cbxRol.Text, estado);
                MessageBox.Show(ok ? "Actualizado" : "No se actualizó");
                CargarUsuarios();
                Limpiar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnRefrescar_Click(object sender, EventArgs e)
        {
            CargarUsuarios();
        }

        private void txtUsuario_TextChanged(object sender, EventArgs e)
        {

        }
        private void CargarUsuarios()
        {
            dgvUsuarios.DataSource = Usuario_BLL.Listar().Select(u => new {
                u.id,
                u.nombre_usuario,
                u.rol,
                estado = u.estado ? "Activo" : "Inactivo"
            }).ToList();

        }

        private void frmUsuario_Load(object sender, EventArgs e)
        {
            CargarUsuarios();
            cbxRol.Items.AddRange(new string[] { "Admin", "Empleado" });

        }

        private void dgvUsuarios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtId.Text = dgvUsuarios.Rows[e.RowIndex].Cells["id"].Value.ToString();
                txtUsuario.Text = dgvUsuarios.Rows[e.RowIndex].Cells["nombre_usuario"].Value.ToString();
                cbxRol.Text = dgvUsuarios.Rows[e.RowIndex].Cells["rol"].Value.ToString();
                chkEstado.Checked = dgvUsuarios.Rows[e.RowIndex].Cells["estado"].Value.ToString() == "Activo";
            }
            txtClave.Enabled = false;
        }
    }
}
