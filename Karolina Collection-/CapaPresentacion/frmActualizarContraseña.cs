using Karolina_Collection_.CapaDatos;
using Karolina_Collection_.CapaEntidades;
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
    public partial class frmActualizarContraseña : Form
    {
        public frmActualizarContraseña()
        {
            InitializeComponent();
        }

        private void btnCambiarClave_Click(object sender, EventArgs e)
        {

            try
            {
                int id = SesionActual.id;
                if (id == 0) throw new Exception("No hay sesión activa.");

                // Verificar clave actual
                var user = Usuario_DAL.Login(SesionActual.nombre_usuario, PruebaHash(txtCactual.Text));
                if (user == null)
                {
                    MessageBox.Show("La contraseña actual es incorrecta.");
                    return;
                }

                if (txtNuevaC.Text != txtConfirmarC.Text)
                {
                    MessageBox.Show("La nueva contraseña y su confirmación no coinciden.");
                    return;
                }
                string nuevaClave = txtNuevaC.Text;
                if (nuevaClave.Length < 8 || !nuevaClave.Any(c => !char.IsLetterOrDigit(c)))
                {
                    MessageBox.Show("La contraseña debe tener al menos 8 caracteres y contener un signo o caracter especial.", "Contraseña Insegura", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; // Detiene la ejecución aquí
                }
                bool ok = Usuario_BLL.CambiarClave(id, txtNuevaC.Text);
                MessageBox.Show(ok ? "Contraseña actualizada." : "No se pudo actualizar.");
                if (ok) this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

            // Abre el frm de iniciar sesión
            SesionActual.Cerrar();
            frmLogin frm = new frmLogin();
            frm.Show();
        }
        // Método privado para generar hash temporal y verificar (evita duplicar Seguridad en UI)
        private string PruebaHash(string pass)
        {
            return Seguridad.Hash_SHA256(pass);

        }

        private void frmActualizarContraseña_Load(object sender, EventArgs e)
        {

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
            Inicio frm = new Inicio();
            frm.Show();
        }
    }
}

