using Karolina_Collection_.CapaDatos;
using Karolina_Collection_.CapaEntidades;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karolina_Collection_.CapaNegocio
{
    public class ProductoBLL
    {
        //Metodo que retorna una lista de productos desde la capa de datos
        private ProductoDAL dal = new ProductoDAL();

        //Metodo que retorna una lista de productos
        public DataTable Listar_producto()
        {
            return dal.Listar_producto();
        }

        //Método que registra un producto junto con sus variantes --Lista de variantes (tallas/colores) del producto
        public bool Registrar_Producto_Completo(Producto p, List<Producto_variante> listaVariantes, out string Mensaje)
        {
            //Validar que el nombre del producto no esté vacío
            if (string.IsNullOrWhiteSpace(p.nombre_producto))
            {
                Mensaje = "El nombre del producto es obligatorio.";
                return false;
            }

            //Validar que el precio base sea mayor a 0
            if (p.precio_base <= 0)
            {
                Mensaje = "El precio base debe ser mayor a 0.";
                return false;
            }

            //Validar que al menos una variante haya sido agregada
            if (listaVariantes.Count == 0)
            {
                Mensaje = "Debes agregar al menos una variante (Talla/Color) al producto.";
                return false;
            }
            //Llamar al método de la capa de datos para registrar el producto y sus variantes
            return dal.Registrar_producto_transaccional(p, listaVariantes, out Mensaje);
        }
    }
}
