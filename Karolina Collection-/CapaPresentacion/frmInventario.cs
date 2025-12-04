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
    public partial class frmInventario : Form
    {
        //Variable para guardar el id selecionado
        private int id_variante_seleccione = 0;
        //Variable para almacenar el stock que habia antes y despues sumarlo con el nuevo
        private int stock_actual = 0;
        public frmInventario()
        {
            InitializeComponent();
        }
        private void Mostrar_inventario()
        {
            //Traer los datos para llenar el DataView
            dgvInventario.DataSource = Producto_variante_BLL.Listar_variantes();
            //Condicion para vereficar si el DateView tiene al menos una fila
            if (dgvInventario.Rows.Count > 0)
            {
                //Condicion para ocultar el id(El usuario no va a ver el id pero se trabajara con el)
                if (dgvInventario.Columns.Contains("id"))
                {
                    dgvInventario.Columns["id"].Visible = false;
                }
            }
        }
        private void Limpiar_campos()
        {
            id_variante_seleccione = 0; // Importante
            stock_actual = 0;           // Importante

            lblStockActual.Text = "Stock Actual: 0"; // Resetear label visual

            nudStock.Text = "0";  // O nudStock.Value = 0;
            nudMprecio.Value = 0;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnCancelarproducto_Click(object sender, EventArgs e)
        {
            //Validar que haya algo seleccionado
            if (id_variante_seleccione == 0)
            {
                MessageBox.Show("Selecciona primero el producto que deseas eliminar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //Pregunta de seguridad a la hora de eliminar un producto (Confirmación)
            DialogResult respuesta = MessageBox.Show(
                "¿Estás seguro de que deseas eliminar esta variante del inventario?\n\nEsta acción no se puede deshacer.", "Confirmar Eliminación",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            //Si se preciono si, se prosede a eliminar 
            if (respuesta == DialogResult.Yes)
            {
                //Llamar a la BLL para borrar
                bool exito = Producto_variante_BLL.Eliminar_variante(id_variante_seleccione);

                //Si se elimino correctamente
                if (exito)
                {
                    MessageBox.Show("Producto eliminado correctamente.", "Éxito", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //Refrescar y limpiar
                    Mostrar_inventario();
                    Limpiar_campos();
                }
                //Si no elimino correctamente
                else
                {
                    MessageBox.Show("No se pudo eliminar el producto.\nEs posible que ya tenga ventas registradas.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnVolver_menu_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmInventario_Load(object sender, EventArgs e)
        {
            Mostrar_inventario();
        }

        private void txtBuscarProducto_TextChanged(object sender, EventArgs e)
        {
            string busqueda = txtBuscarProducto.Text.Trim();

            if (busqueda.Length > 0)
            {
                // Si escribieron algo, buscamos en la base de datos
                dgvInventario.DataSource = Producto_variante_BLL.Buscar_variantes(busqueda);
            }
            else
            {
                // Si borraron todo, volvemos a mostrar la lista completa
                // (Podemos reutilizar el método MostrarInventario si lo creaste, o llamar a Listar)
                dgvInventario.DataSource = Producto_variante_BLL.Listar_variantes();
            }
        }

        private void dgvInventario_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Verificamos que no sea clic en el encabezado
            if (e.RowIndex >= 0)
            {
                //Obtiene la fila completa donde se hizo clic
                DataGridViewRow fila = dgvInventario.Rows[e.RowIndex];

                //Capturamos el id
                id_variante_seleccione = Convert.ToInt32(fila.Cells["id"].Value);

                //Extrae cuantas unidades hay actualmente
                stock_actual = Convert.ToInt32(fila.Cells["stock"].Value);

                //Mostrar en un label el stock actual
                lblStockActual.Text = "Stock Actual: " + stock_actual.ToString();

                // Reiniciamos a 0 para que ingreses solo lo nuevo
                nudStock.Value = 0;

                // Cargar precio actual
                try
                {
                    //Extraemos el precio de venta de la columna "precio_venta"
                    nudMprecio.Value = Convert.ToDecimal(fila.Cells["precio_venta"].Value);
                }
                catch
                {
                    //Ponemos el precio en 0 como valor seguro por defecto
                    nudMprecio.Value = 0;
                }
            }
        }

        private void btnGuardarCambios_Click(object sender, EventArgs e)
        {
            //Validar que ya se haya seleccionado algo en el CellClick
            if (id_variante_seleccione == 0)
            {
                MessageBox.Show("Por favor selecciona una fila de la tabla primero.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //Obtener la cantidad a SUMAR del NumericUpDown
            int cantidad_Sumar = (int)nudStock.Value;

            //MAGIA DE LA SUMA (Lo que había + lo nuevo)
            int stockFinal = stock_actual + cantidad_Sumar;

            //OBTENER PRECIO
            decimal precioFinal = nudMprecio.Value;

            //ENVIAR A BD
            bool exito = Producto_variante_BLL.Actualizar_variantes(id_variante_seleccione, stockFinal, precioFinal);

            //Si la actualizacion fue exitosa
            if (exito)
            {
                //Variable donde se armaran los mensajes
                string mensaje = "";

                // Si se agrego stock (cantidad mayor a 0)
                if (cantidad_Sumar > 0)
                {
                    //Mensaje detallado mostrando como cambio el Stock y Precio
                    mensaje = $"¡Stock y Precio Actualizados!\n\n" +
                              $"Stock Anterior: {stock_actual}\n" +
                              $"Agregado: +{cantidad_Sumar}\n" +
                              $"--------------------------\n" +
                              $"NUEVO STOCK TOTAL: {stockFinal}\n" +
                              $"Precio: {precioFinal:C2}";
                }
                //Si se agrego solo precio
                else
                {
                    //Mensaje detallado que solo se agrego el precio
                    mensaje = $"¡Precio Actualizado Correctamente!\n\n" +
                              $"Nuevo Precio: {precioFinal:C2}\n" +
                              $"Stock Actual: {stockFinal} (Sin cambios)";
                }
                //Mostrar una ventanita ermejente mostrando el mensaje de exito
                MessageBox.Show(mensaje, "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //Refresca la tabla inventarios
                Mostrar_inventario();
                //Limpia los campos de los formularios
                Limpiar_campos();
            }
            //Si la actualizacion no fue exitosa
            else
            {
                //Se mostrara una ventanita con el error
                MessageBox.Show("No se pudo actualizar. Verifica la conexión.", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

