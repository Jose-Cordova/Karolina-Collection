using Karolina_Collection_.CapaDatos;
using Karolina_Collection_.CapaEntidades;
using Karolina_Collection_.CapaNegocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Karolina_Collection_.CapaPresentacion
{
    public partial class frmVenta : Form
    {
        private void habilitarBotones()
        {

        }
        public frmVenta()
        {
            InitializeComponent();
        }
   
        private void ValidarBotones()
        {
            // 1. ¿Hay productos en la lista?
            bool hayProductos = dgvDetalles.Rows.Count > 0 && !dgvDetalles.Rows[0].IsNewRow;

            // Activar "Registrar" y "Limpiar" solo si hay productos
            btnRegistraVenta.Enabled = hayProductos;
            btnLimpiarDetalle.Enabled = hayProductos;

            // 2. ¿Hay una fila seleccionada para quitar?
            // Solo activamos "Quitar" si hay filas Y una de ellas está seleccionada
            bool filaSeleccionada = dgvDetalles.SelectedRows.Count > 0;

            // Un pequeño truco: a veces la "fila nueva" vacía cuenta como selección, hay que evitarlo
            if (filaSeleccionada && dgvDetalles.SelectedRows[0].IsNewRow)
            {
                filaSeleccionada = false;
            }

            btnQuitar.Enabled = filaSeleccionada;

            // El botón Volver siempre debe estar activo
            btnVolver_menu.Enabled = true;
        }
        private void frmVenta_Load(object sender, EventArgs e)
        {
            // El botón inicia apagado porque no has seleccionado nada aún
            btnAgregarProducto.Enabled = false;
            ValidarBotones();
            // --- CLIENTES ---
            cboCliente.DataSource = Cliente_DAL.ListarActivos();
            cboCliente.DisplayMember = "nombre_completo";
            cboCliente.ValueMember = "Id";

            // --- TIPO PAGO ---
            cboMetodo_pago.DataSource = Metodo_pago_DAL.Listar(); //asiganmos datos al desplegable
            cboMetodo_pago.DisplayMember = "tipo"; //lo que muestra
            cboMetodo_pago.ValueMember = "id"; //el valor que nos importa ID


            // --- FECHA ---
            dtpFecha.Value = DateTime.Now;//obtiene la fecha de ahora
            CargarProductos(string.Empty);
            // --- CONFIGURAR COLUMNAS DEL DETALLE ---
            ConfigurarTablaDetalles();
        }
        private void ConfigurarTablaDetalles()
        {
            dgvDetalles.Columns.Clear();

            // ID PRODUCTO
            DataGridViewTextBoxColumn colIdProd = new DataGridViewTextBoxColumn();
            colIdProd.Name = "id_producto_variante";
            colIdProd.HeaderText = "ID";
            colIdProd.Visible = false;
            dgvDetalles.Columns.Add(colIdProd);

            // NOMBRE PRODUCTO
            dgvDetalles.Columns.Add("NombreProducto", "Producto");

            // CANTIDAD
            DataGridViewTextBoxColumn colCant = new DataGridViewTextBoxColumn();
            colCant.Name = "cantidad";
            colCant.HeaderText = "Cantidad";
            dgvDetalles.Columns.Add(colCant);

            // PRECIO UNITARIO
            DataGridViewTextBoxColumn colPrecio = new DataGridViewTextBoxColumn();
            colPrecio.Name = "precio_unitario";
            colPrecio.HeaderText = "Precio Unitario";
            dgvDetalles.Columns.Add(colPrecio);

            // SUBTOTAL
            DataGridViewTextBoxColumn colSub = new DataGridViewTextBoxColumn();
            colSub.Name = "sub_total";
            colSub.HeaderText = "Subtotal";
            colSub.ReadOnly = true;
            dgvDetalles.Columns.Add(colSub);
            // Asegurar permisos de edición
            dgvDetalles.ReadOnly = false;

            // Columnas NO editables
            dgvDetalles.Columns["sub_total"].ReadOnly = true;
            dgvDetalles.Columns["precio_unitario"].ReadOnly = true;
            dgvDetalles.Columns["NombreProducto"].ReadOnly = true;
            dgvDetalles.Columns["id_producto_variante"].ReadOnly = true;

            // ÚNICA columna editable:
            dgvDetalles.Columns["cantidad"].ReadOnly = false;

        }
        //Meétodo que trae los datos al dgv, colocado fuera del LOAD
        private void CargarProductos(string filtro)
        {
            // 1. Obtenemos la tabla (Que ya trae el JOIN con los nombres)
            var tabla = Producto_variante_DAL.Listar();

            // 2. Filtrar si hay texto
            if (!string.IsNullOrWhiteSpace(filtro))
            {
                var dv = tabla.DefaultView;

                // CORRECCIÓN AQUÍ:
                // Usamos "Producto" porque así le pusiste en el SQL (AS Producto).
                // Agregué también Talla y Color para que la búsqueda sea más potente.
                dv.RowFilter = $"Producto LIKE '%{filtro}%' OR Talla LIKE '%{filtro}%' OR Color LIKE '%{filtro}%'";

                dgvProductos.DataSource = dv;
            }
            else
            {
                dgvProductos.DataSource = tabla;
            }

            // 3. Ocultar columnas técnicas
            if (dgvProductos.Columns["id"] != null)
            {
                dgvProductos.Columns["id"].Visible = false;
            }
        }
        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void botonPremium6_Click(object sender, EventArgs e)
        {
            Close();
            
        }

        private void roundedPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnBuscarProducto_Click(object sender, EventArgs e)
        {
            string texto = txtBuscarProducto.Text.Trim();
            CargarProductos(texto);
        }

        private void txtBuscarProducto_TextChanged(object sender, EventArgs e)
        {
            

        }

        private void txtBuscarProducto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CargarProductos(txtBuscarProducto.Text.Trim());
            }
        }

        private void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            // 1. Validar que haya selección
            if (dgvProductos.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un producto de la lista.");
                return;
            }

            DataGridViewRow row = dgvProductos.SelectedRows[0];

            // 2. Obtener datos usando los nombres EXACTOS de tu DAL (Producto_variante_DAL.Listar)
            int idVariante = Convert.ToInt32(row.Cells["id"].Value);

            // Concatenamos para que se vea bonito: "Camisa - Rojo (M)"
            string nombreCompleto = row.Cells["Producto"].Value.ToString() + " " +
                                    row.Cells["Color"].Value.ToString() + " " +
                                    row.Cells["Talla"].Value.ToString();

            decimal precio = Convert.ToDecimal(row.Cells["precio_venta"].Value);
            int stockDisponible = Convert.ToInt32(row.Cells["stock"].Value);

            // 3. Validar Stock Inicial (Si no hay nada en bodega, no dejar vender)
            if (stockDisponible <= 0)
            {
                MessageBox.Show("Este producto no tiene stock disponible.");
                return;
            }

            // 4. Buscar si el producto YA existe en el carrito (dgvDetalles)
            bool productoExiste = false;

            foreach (DataGridViewRow filaVenta in dgvDetalles.Rows)
            {
                if (filaVenta.IsNewRow) continue; // Ignorar fila vacía

                // Comparamos los IDs (Columna "id_producto_variante" que definiste en ConfigurarTablaDetalles)
                if (Convert.ToInt32(filaVenta.Cells["id_producto_variante"].Value) == idVariante)
                {
                    // --- PRODUCTO ENCONTRADO: SUMAR CANTIDAD ---

                    int cantidadActual = Convert.ToInt32(filaVenta.Cells["cantidad"].Value);

                    // Validar que no nos pasemos del stock al sumar 1
                    if (cantidadActual + 1 > stockDisponible)
                    {
                        MessageBox.Show($"No puedes agregar más. Stock máximo: {stockDisponible}");
                        return;
                    }

                    int nuevaCantidad = cantidadActual + 1;

                    // Actualizamos la fila existente
                    filaVenta.Cells["cantidad"].Value = nuevaCantidad;
                    filaVenta.Cells["sub_total"].Value = nuevaCantidad * precio;

                    productoExiste = true;
                    break; // Terminamos de buscar
                }
            }

            // 5. SI NO EXISTE: AGREGAR FILA NUEVA
            if (!productoExiste)
            {
                // Respetamos el orden de columnas que hiciste en 'ConfigurarTablaDetalles':
                // 1. ID (Oculto), 2. Nombre, 3. Cantidad, 4. Precio, 5. Subtotal

                decimal subTotal = precio * 1; // Cantidad inicial siempre es 1

                dgvDetalles.Rows.Add(
                    idVariante,      // id_producto_variante
                    nombreCompleto,  // NombreProducto
                    1,               // cantidad
                    precio,          // precio_unitario
                    subTotal         // sub_total
                );
            }

            // 6. Recalcular los totales finales
            RecalcularTotal(); ;//dará error, mas adelante se creará el método, comenta esta linea cuando ejecutes

            // Opcional: Quitar la selección del grid de arriba para que se vea limpio
            dgvProductos.ClearSelection();
        }
        private void RecalcularTotal()
        {
            // ---------------------------------------------------------
            // PASO 1: SUMAR LA GRILLA (TU CÓDIGO)
            // ---------------------------------------------------------
            decimal sumaTotalGrid = 0;

            foreach (DataGridViewRow row in dgvDetalles.Rows)
            {
                // Validación importante: Ignorar la fila nueva vacía del final
                if (row.IsNewRow) continue;

                // Validación de nulo: Solo sumar si hay algo en la celda "sub_total"
                if (row.Cells["sub_total"].Value != null)
                {
                    sumaTotalGrid += Convert.ToDecimal(row.Cells["sub_total"].Value);
                }
            }

            // ---------------------------------------------------------
            // PASO 2: CALCULAR IVA Y TOTAL FINAL
            // ---------------------------------------------------------
            // Asumimos que la lista es el "Subtotal" (Neto) y el impuesto se suma encima.

            decimal subTotalNeto = sumaTotalGrid;
            decimal tasaIVA = 0.13m; // 13% El Salvador (Cámbialo si es diferente)

            decimal montoIVA = subTotalNeto * tasaIVA;      // Ej: 100 * 0.13 = 13
            decimal totalFinal = subTotalNeto + montoIVA;   

            // ---------------------------------------------------------
            // PASO 3: MOSTRAR EN PANTALLA (VISUAL)
            // ---------------------------------------------------------

            // Label Amarillo (Lo usas como Subtotal)
            lblTotal.Text = subTotalNeto.ToString("C2"); // "C2" pone signo $ y 2 decimales

            // Label Verde (Total Final a Pagar)
            // Asegúrate de tener este label creado en tu diseño, si no, comenta esta línea
            if (lblTotalFinal != null)
            {
                lblTotalFinal.Text = totalFinal.ToString("C2");
            }

            // ---------------------------------------------------------
            // PASO 4: TRUCO PRO (VALORES PARA GUARDAR EN BD)
            // ---------------------------------------------------------
            // Actualizamos los TextBoxes (que pueden estar ocultos) para que el botón "Registrar Venta"
            // tome los valores numéricos exactos sin tener que leer los Labels.

            // Si no tienes estos textbox, coméntalos, pero te recomiendo tenerlos.
            // txtSubTotal.Text = subTotalNeto.ToString(); 
            // txtTotalIVA.Text = montoIVA.ToString();
            // txtTotal.Text = totalFinal.ToString();
        }
        private void dgvProductos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btnAgregarProducto_Click(sender, e);
        }

        private void dgvProductos_DoubleClick(object sender, EventArgs e)
        {
            btnAgregarProducto_Click(sender, e);
        }
        //Utilizaremos un nuevo evento en el dgvDetalles, llamado “CellEndEdit”,
        //con este permitiremos que el usuario unicamente pueda editar la celda de cantidad en el
        //dgv(si lleva mas de un mismo producto)
        private void dgvProductos_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
           

        }

        private void dgvDetalles_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            // Si editaron la columna Cantidad
            if (dgvDetalles.Columns[e.ColumnIndex].Name == "cantidad")
            {
                DataGridViewRow row = dgvDetalles.Rows[e.RowIndex];

                int cantidad;
                bool ok = int.TryParse(row.Cells["cantidad"].Value?.ToString(), out cantidad);

                if (!ok || cantidad <= 0)
                {
                    MessageBox.Show("Cantidad inválida.");
                    row.Cells["cantidad"].Value = 1;
                    cantidad = 1;
                }

                decimal precio = Convert.ToDecimal(row.Cells["precio_unitario"].Value);
                decimal subTotal = cantidad * precio;

                row.Cells["sub_total"].Value = subTotal;

                // Recalcular total general
                RecalcularTotal();
            }
        }

        private void btnQuitar_Click(object sender, EventArgs e)
        {
            if (dgvDetalles.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione una fila para quitar.");
                return;
            }

            dgvDetalles.Rows.RemoveAt(dgvDetalles.SelectedRows[0].Index);

            RecalcularTotal();
            
        }
        //progrmamos boton limpiar detalle
        

        private void btnLimpiarDetalle_Click_1(object sender, EventArgs e)
        {
            dgvDetalles.Rows.Clear();
            RecalcularTotal();
            
        }

        private void btnRegistraVenta_Click(object sender, EventArgs e)
        {
            
            try
            {
                // 1. Validar que hay productos
                // Verificamos si la grilla está vacía o solo tiene la fila nueva
                if (dgvDetalles.Rows.Count == 0 || (dgvDetalles.Rows.Count == 1 && dgvDetalles.Rows[0].IsNewRow))
                {
                    MessageBox.Show("La venta no tiene productos.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // ---------------------------------------------------
                // 2. CÁLCULOS MATEMÁTICOS 
                // ---------------------------------------------------

                // A) Obtenemos la suma pura de los productos (Esto es el SubTotal Neto)
                decimal sumaProductosNeto = ObtenerTotalVenta();

                // B) Calculamos el IVA (13%)
                decimal montoIvaCalculado = sumaProductosNeto * 0.13m;

                // C) Calculamos el TOTAL FINAL (Suma + IVA)
                // ESTO ES LO QUE FALTABA: Sumar el impuesto al total
                decimal totalFinalCalculado = sumaProductosNeto + montoIvaCalculado;

                // ---------------------------------------------------
                // 3. CREAR OBJETO VENTA
                // ---------------------------------------------------
                Venta venta = new Venta()
                {
                    fecha = dtpFecha.Value,

                    // Aquí enviamos los montos correctos para que cuadren con la BLL
                    total_venta = totalFinalCalculado, // $122.63
                    monto_iva = montoIvaCalculado,     // $12.65

                    id_cliente = cboCliente.SelectedValue != null ? Convert.ToInt32(cboCliente.SelectedValue) : 0,
                    id_metodo_pago = cboMetodo_pago.SelectedValue != null ? Convert.ToInt32(cboMetodo_pago.SelectedValue) : 0
                };

                // ---------------------------------------------------
                // 4. CREAR LISTA DE DETALLES
                // ---------------------------------------------------
                List<Detalle_venta> detalles = new List<Detalle_venta>();

                foreach (DataGridViewRow row in dgvDetalles.Rows)
                {
                    if (row.IsNewRow) continue;

                    detalles.Add(new Detalle_venta()
                    {
                        id_producto_variante = Convert.ToInt32(row.Cells["id_producto_variante"].Value),
                        cantidad = Convert.ToInt32(row.Cells["cantidad"].Value),
                        precio_unitario = Convert.ToDecimal(row.Cells["precio_unitario"].Value),
                        sub_total = Convert.ToDecimal(row.Cells["sub_total"].Value)
                    });
                }

                // ---------------------------------------------------
                // 5. VALIDAR Y GUARDAR
                // ---------------------------------------------------
                var validacion = Venta_BLL.ValidarVenta(venta, detalles);

                if (!validacion.Exito)
                {
                    MessageBox.Show(validacion.Mensaje, "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var resultado = Venta_DAL.RegistrarVentaTransaccional(venta, detalles);

                if (resultado.Exito)
                {
                    MessageBox.Show(resultado.Mensaje, "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarFormulario();
                }
                else
                {
                    MessageBox.Show(resultado.Mensaje, "Error en BD", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message);
            }
            
        }
        // --- MÉTODOS AUXILIARES ADAPTADOS ---

        private decimal ObtenerTotalVenta()
        {
            decimal total = 0;
            foreach (DataGridViewRow row in dgvDetalles.Rows)
            {
                if (row.IsNewRow) continue; // Evitar error con fila vacía

                if (row.Cells["sub_total"].Value != null)
                {
                    total += Convert.ToDecimal(row.Cells["sub_total"].Value);
                }
            }
            return total;
        }

        private void LimpiarFormulario()
        {
            dgvDetalles.Rows.Clear();
            lblTotal.Text = "Total: $0.00";
            lblTotalFinal.Text = "Total: $0.00";
            txtMresivido.Clear();
            txtCambio.Clear();
            txtBuscarProducto.Clear();

            // Reiniciar combos
            cboCliente.SelectedIndex = -1;
            cboMetodo_pago.SelectedIndex = -1;

            // Recargar stock por si cambió
            CargarProductos(string.Empty);
        }

        private void btnNuevoCliente_Click(object sender, EventArgs e)
        {
            //Creo una instancia en el formulario de frmClientes
            frmClientes frmClientes = new frmClientes();
            //muestro el formulario 
            frmClientes.ShowDialog();
            cboCliente.DataSource = Cliente_DAL.ListarActivos();
        }

        private void roundedPanel6_Paint(object sender, PaintEventArgs e)
        {

        }
        // Se utiliza para q aun que se agrege una fila este desactivado el boton de quitar
        private void dgvDetalles_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            ValidarBotones();
        }
       // Y este es para cuando se limpie o se agrege el detalle y la tabla
       // este vacia se desactiven de nuevo los botones
        private void dgvDetalles_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            ValidarBotones();
             RecalcularTotal();
        }
        // y en este evento funciona para que cada vez pasamos una
        // fila a detalle se vuelva a desactivar el boton de quitar 
        private void dgvDetalles_SelectionChanged(object sender, EventArgs e)
        {
            ValidarBotones();
        }

        // Para  que cuando Seleccione la fila
        // se active el boton de agregar ala tabla de detalle
        private void dgvProductos_SelectionChanged(object sender, EventArgs e)
        {
            // Solo se activa si hay al menos una fila seleccionada (azul)
            btnAgregarProducto.Enabled = (dgvProductos.SelectedRows.Count > 0);
        }

        private void txtMresivido_TextChanged(object sender, EventArgs e)
        {
            try
            {
                // 1. OBTENER EL TOTAL A PAGAR
                // Limpiamos el texto para quitar el signo "$" o la palabra "Total:" si la tuviera
                string textoTotal = lblTotalFinal.Text.Replace("$", "").Replace("Total:", "").Trim();

                decimal totalPagar = 0;
                // Usamos TryParse para evitar errores si el label tiene texto raro
                decimal.TryParse(textoTotal, out totalPagar);

                // 2. OBTENER EL MONTO RECIBIDO
                decimal montoRecibido = 0;
                // Intentamos convertir lo que el usuario está escribiendo
                decimal.TryParse(txtMresivido.Text.Trim(), out montoRecibido);

                // 3. CALCULAR LA RESTA (CAMBIO)
                decimal cambio = montoRecibido - totalPagar;

                // 4. MOSTRAR EL RESULTADO
                txtCambio.Text = cambio.ToString("N2");

                
            }
            catch (Exception)
            {
                // Si ocurre cualquier error raro, ponemos 0
                txtCambio.Text = "0.00";
            }
        }
    }  
}
