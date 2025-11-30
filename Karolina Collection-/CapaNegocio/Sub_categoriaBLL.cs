using Karolina_Collection_.CapaDatos;
using Karolina_Collection_.CapaEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karolina_Collection_.CapaNegocio
{
    public class Sub_categoriaBLL
    {
        //Metodo que retorna una lista de subcategorias desde la capa de datos
        private Sub_categoriaDAL dal = new Sub_categoriaDAL();
        //Método que recibe un id_categoria y retorna una lista de subcategorías asociadas a esa categoría.
        public List<Sub_categoria> Listar_sub_categoria(int id_categoria)
        {
            try
            {
                //Llama al metodo de la capa de datos
                return dal.Listar_sub_categoria(id_categoria);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en BLL: " + ex.Message);
                //Retorna una lista vacia en caso de error
                return new List<Sub_categoria>();
            }
        }
    }
}
