using Karolina_Collection_.CapaDatos;
using Karolina_Collection_.CapaEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karolina_Collection_.CapaNegocio
{
    public class ProveedorBLL
    {
        //Metodo que retorna una lista de proveedores desde la capa de datos
        private ProveedorDAL dal = new ProveedorDAL();

        //Metodo que retorna una lista de proveedores
        public List<Proveedor> Listar_proveedor()
        { 
            try
            {
                // Llamada al método de la capa de datos para obtener la lista de proveedores
                return dal.Listar_proveedor();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al listar proveedores: " + ex.Message);
                // En caso de error, retornar una lista vacía
                return new List<Proveedor>();
            }

        }
    }
}
