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
    public partial class frmProducto : Form
    {
        //Intancia para manejar las operaciones de categorias, subcategorias, marcas, proveedores, tallas, colores y productos
        CategoriaBLL categoriaBLL = new CategoriaBLL(); 
        Sub_categoriaBLL sub_CategoriaBLL = new Sub_categoriaBLL();
        MarcaBLL marcaBLL = new MarcaBLL();
        ProveedorBLL proveedorBLL = new ProveedorBLL();

        TallaBLL tallaBLL = new TallaBLL();
        ColorBLL colorBLL = new ColorBLL();

        ProductoBLL productoBLL = new ProductoBLL();

        public frmProducto()
        {
            InitializeComponent();
        }
        //Metodo para limpiar los campos del formulario
        private void Limpiar_campos()
        {
            //Limpiar los campos del formulario registrar producto
            txtNombre.Clear();
            txtDescripcion.Clear();
            cbxMarca.SelectedIndex = -1;
            cbxProveedor.SelectedIndex = -1;
            cbxCategoria.SelectedIndex = -1;
            cbxSubcategoria.SelectedIndex = -1;
            dgvProductovariante.Rows.Clear();
            txtNombre.Focus();
        }
        private void Desabilitar_botones()
        {
            btnRegistrarproducto.Enabled = false;
            btnCancelarproducto.Enabled = false;
        }
        private void Habilitar_botones()
        {
            btnRegistrarproducto.Enabled = true;
            btnCancelarproducto.Enabled = true;
        }

        private void label3_Click(object sender, EventArgs e)
        {
            
        }

        private void roundedPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void botonPremium1_Click(object sender, EventArgs e)
        {
            //Vereficar si hay una fila seleccionada
            if (dgvProductovariante.SelectedRows.Count > 0)
            {
                //Eliminar la fila seleccionada
                //Obtiene el indice de la fila seleccionada y la elimina
                dgvProductovariante.Rows.RemoveAt(dgvProductovariante.SelectedRows[0].Index);
            }
            else
            {
                //Mensaje de aviso si no hay fila seleccionada
                MessageBox.Show("Seleccione una fila para eliminar.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            //Si no hay variantes en el grid Y el nombre está vacío, deshabilita los botones
            if (dgvProductovariante.Rows.Count == 0 && txtNombre.Text.Trim().Length == 0)
            {
                Desabilitar_botones();
            }
        }

        private void btnVolver_menu_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void roundedPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void frmProducto_Load(object sender, EventArgs e)
        {
            //Se desavilitan los botones
            Desabilitar_botones();
            //Cargar las marcas en el combobox
            cbxMarca.DataSource = marcaBLL.Listar_marca(); //LLama al metodo para listar las marcas
            cbxMarca.DisplayMember = "nombre_marca"; //Nombre que se muestra en el combobox
            cbxMarca.ValueMember = "id"; //Valor que se utiliza internamente
            cbxMarca.SelectedIndex = -1; //Empezar vacío

            //Cargar los proveedores en el combobox
            cbxProveedor.DataSource = proveedorBLL.Listar_proveedor(); //LLama al metodo para listar los proveedores
            cbxProveedor.DisplayMember = "nombre_proveedor"; //Nombre que se muestra en el combobox
            cbxProveedor.ValueMember = "id"; //Valor que se utiliza internamente
            cbxProveedor.SelectedIndex = -1; //Empezar vacío

            //Cargar las categorias en el combobox
            cbxCategoria.DataSource = categoriaBLL.Listar_categoria(); //LLama al metodo para listar las categorias
            cbxCategoria.DisplayMember = "nombre_categoria"; //Nombre que se muestra en el combobox
            cbxCategoria.ValueMember = "id"; //Valor que se utiliza internamente
            cbxCategoria.SelectedIndex = -1; //Empezar vacío

            //Cargar las tallas en el combobox
            colTalla.DataSource = tallaBLL.Listar_talla(); //LLama al metodo para listar las tallas
            colTalla.DisplayMember = "nombre_talla"; //Nombre que se muestra en el combobox
            colTalla.ValueMember = "id"; //Valor que se utiliza internamente

            //Cargar los colores en el combobox
            colColor.DataSource = colorBLL.Listar_color(); //LLama al metodo para listar los colores
            colColor.DisplayMember = "nombre_color"; //Nombre que se muestra en el combobox
            colColor.ValueMember = "id"; //Valor que se utiliza internamente

        }

        private void cbxSubcategoria_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cbxSubcategoria_SelectionChangeCommitted(object sender, EventArgs e)
        {
           
        }

        private void cbxCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Usamos BeginInvoke para que cuando se seleccione una categoría, se llene el combo de subcategorías
            this.BeginInvoke(new Action(() =>
            {
                // Verificamos que haya algo seleccionado realmente
                if (cbxCategoria.SelectedItem != null)
                {
                    try
                    {
                        // Obtenemos el ID de la Categoría que acabas de tocar
                        Categoria cat = (Categoria)cbxCategoria.SelectedItem;
                        int id = cat.id;

                        //Mandamos a llenar el OTRO combo (Subcategoría)
                        cbxSubcategoria.DataSource = null;
                        cbxSubcategoria.DataSource = sub_CategoriaBLL.Listar_sub_categoria(id); //LLena el combo con las subcategorias de la categoria seleccionada
                        cbxSubcategoria.DisplayMember = "nombre_sub_categoria";
                        cbxSubcategoria.ValueMember = "id";
                        cbxSubcategoria.SelectedIndex = -1; //Empezar vacío
                    }
                    catch (Exception ex)
                    {
                        // Mostrar cualquier error que pueda surgir
                        MessageBox.Show(ex.Message);
                    }
                }
            }));
        }

        private void btnAgregarfila_Click(object sender, EventArgs e)
        {
            //Se agrega una nueva fila al datagridview
            dgvProductovariante.Rows.Add();
        }

        //Metodo para cancelar el producto
        private void btnCancelarproducto_Click(object sender, EventArgs e)
        {
            //Muestra un cuadro de dialogo y guarda la respuesta de el usuario
            DialogResult resultado = MessageBox.Show("¿Está seguro que desea cancelar el registro del producto?", "Confirmar Cancelación",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //Si el usuario preciono si, cancela el registro y limpia todos los campos
            if (resultado == DialogResult.Yes)
            {
                Limpiar_campos();
            }
        }

        private void btnRegistrarproducto_Click(object sender, EventArgs e)
        {
        
            //VALIDACIONES PRINCIPALES
            //Verificar que el nombre no este vasio y no tenga numeros
            if (string.IsNullOrWhiteSpace(txtNombre.Text) || txtNombre.Text.Any(char.IsDigit))
            {
                MessageBox.Show("Por favor ingrese un nombre de producto válido (sin números). ", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //Verefivar que no esten vasios las Marca y el Proveedor
            if (cbxMarca.SelectedIndex == -1 || cbxProveedor.SelectedIndex == -1)
            {
                MessageBox.Show("Por favor seleccione una Marca y un proveedor. ", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //Vereficar que no esten vasios las Categorias y SubCategorias
            if (cbxCategoria.SelectedIndex == -1 || cbxSubcategoria.SelectedIndex == -1)
            {
                MessageBox.Show("Por favor seleccione una Categoria y SubCategoria. ", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //Recorrer cada fila y vereficar que los campos no esten vasios
            foreach (DataGridViewRow fila in dgvProductovariante.Rows)
            {
                if (fila.Cells["colTalla"].Value == null || fila.Cells["colColor"].Value == null ||
                    fila.Cells["colStock"].Value == null || fila.Cells["colPrecio"].Value == null)
                {
                    MessageBox.Show("Por favor complete todos los campos de las variantes de producto. ", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                //Vereficar que Stock y precio venta sean numeros enteros
                int stock;
                decimal precio;
                //Valida que el valor de Stock sea un número entero válido y mayor a 0
                if (!int.TryParse(fila.Cells["colStock"].Value?.ToString(), out stock) || stock <= 0)
                {
                    MessageBox.Show("Por favor ingrese un valor válido para el Stock en todas las variantes. ", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                //Valida que el valor de Precio sea un número entero válido y mayor a 0
                if (!decimal.TryParse(fila.Cells["colPrecio"].Value?.ToString(), out precio) || precio <= 0)
                {
                    MessageBox.Show("Por favor ingrese un valor válido para el Precio Venta en todas las variantes. ", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            //Crea una instancia nueva de producto
            Producto pd = new Producto();
            pd.nombre_producto = txtNombre.Text.Trim();
            pd.descripcion = txtDescripcion.Text.Trim();
            
            decimal precio_base = 0;
            decimal.TryParse(nudCostobase.Text.Trim(), out precio_base);
            pd.precio_base = precio_base;

            //Obtener los IDs seleccionados en los combobox
            pd.id_sub_categoria = Convert.ToInt32(cbxSubcategoria.SelectedValue);
            pd.id_marca = Convert.ToInt32(cbxMarca.SelectedValue);
            pd.id_proveedor = Convert.ToInt32(cbxProveedor.SelectedValue);

            //Crear lista de variantes
            //Almacenar todas las variantes de el producto antes de enviar a la base de datos
            List<Producto_variante> lista_variantes = new List<Producto_variante>();
            foreach (DataGridViewRow fila in dgvProductovariante.Rows)
            {
                //Creamos una variante por cada fila del datagridview
                Producto_variante pv = new Producto_variante();

                //Obtener los valores de cada celda
                pv.id_talla = Convert.ToInt32(fila.Cells["colTalla"].Value);
                pv.id_color = Convert.ToInt32(fila.Cells["colColor"].Value);

                //Stock y precio de venta
                pv.stock = Convert.ToInt32(fila.Cells["colStock"].Value);
                pv.precio_venta = Convert.ToDecimal(fila.Cells["colPrecio"].Value);

                //Agregar la variante a la lista
                lista_variantes.Add(pv);
            }
            //Llamar al metodo para registrar el producto junto con sus variantes
            //Resivira mensajes del metodo producto
            string mensaje = string.Empty;
            //Llama al método para guardar el producto completo y recibe si fue exitoso 
            bool registrado = productoBLL.Registrar_Producto_Completo(pd, lista_variantes, out mensaje);
            if (registrado)
            {
                //Muestra el mensaje de que se registro y limpia los campos
                MessageBox.Show("Producto registrado exitosamente. ", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                Limpiar_campos();
            }
            else
            {
                //Muestra el error de que no se pudo registrar
                MessageBox.Show("Error al registrar el producto: " + mensaje, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void nudCostobase_ValueChanged(object sender, EventArgs e)
        {

        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
            //Verifica si el campo nombre tiene al menos un carácter
            if (txtNombre.Text.Trim().Length > 0)
            {
                Habilitar_botones(); 
            }
            else
            {
                //Si NO hay texto en el nombre, verifica si tampoco hay variantes
                if (dgvProductovariante.Rows.Count == 0)
                {
                    //Si no hay nombre Y no hay variantes, deshabilita los botones
                    Desabilitar_botones();
                }
            }
        }
    }  
}
