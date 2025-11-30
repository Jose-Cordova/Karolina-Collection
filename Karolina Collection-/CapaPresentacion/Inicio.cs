using Karolina_Collection_.CapaPresentacion;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Karolina_Collection_
{
    public partial class Inicio : Form
    {
        public Inicio()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void botonPremium2_Click(object sender, EventArgs e)
        {

        }

        private void Karolins_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void roundedPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void botonPremium1_Click(object sender, EventArgs e)
        {
            // Crear una instancia del formulario frmVenta
            frmVenta frm = new frmVenta();
            // Muestra el formulario
            frm.ShowDialog();
        }

        private void roundedPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void botonPremium2_Click_1(object sender, EventArgs e)
        {
            // Abrir el formulario frmProducto al hacer clic en el botón
            frmProducto frm = new frmProducto();
            // Muestra el formulario
            frm.ShowDialog();
        }

        private void dividerLine4_Click(object sender, EventArgs e)
        {

        }

        private void botonPremium3_Click(object sender, EventArgs e)
        {
            // Abrir el formulario frmProducto al hacer clic en el botón
            frmInventario frm = new frmInventario();
            // Muestra el formulario
            frm.ShowDialog();
        }

        private void dividerLine5_Click(object sender, EventArgs e)
        {

        }

        private void botonPremium6_Click(object sender, EventArgs e)
        {

        }

        private void roundedPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void botonPremium6_Click_1(object sender, EventArgs e)
        {

        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            //Advertencia antes de cerrar el formulario
            if (MessageBox.Show("¿Está seguro de cerrar el programa?", "Confirmar",
            MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close(); //Cerrar el formulario
            }
        }

        private void btnCliente_Click(object sender, EventArgs e)
        {
            // Abrir el formulario frmProducto al hacer clic en el botón
            frmClientes frm = new frmClientes();
            // Muestra el formulario
            frm.ShowDialog();
        }
    }
}
