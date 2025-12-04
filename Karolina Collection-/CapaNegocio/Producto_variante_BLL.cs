using Karolina_Collection_.CapaDatos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karolina_Collection_.CapaNegocio
{
    public class Producto_variante_BLL
    {
        //Se llama el metodo listar de la DAL
        public static DataTable Listar_variantes()
        {
            //Lista los productos_variantes
            return Producto_variante_DAL.Listar();
        }
        //Se llama el metodo actualizar de la DAL
        public static bool Actualizar_variantes(int id_variante, int nuevo_stock, decimal nuevo_precio)
        {
            //Lista los productos_variantes
            return Producto_variante_DAL.Actualizar_producto_variente(id_variante, nuevo_stock, nuevo_precio);
        }
        //Se llama el metodo eliminar de la DAL
        public static bool Eliminar_variante(int id_variante)
        {
            return Producto_variante_DAL.Eliminar_producto_variente(id_variante);
        }
        public static DataTable Buscar_variantes(string texto)
        {
            return Producto_variante_DAL.Buscar_producto_variante(texto);
        }
    }
}
