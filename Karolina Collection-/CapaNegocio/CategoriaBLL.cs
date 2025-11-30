using Karolina_Collection_.CapaDatos;
using Karolina_Collection_.CapaEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karolina_Collection_.CapaNegocio
{
    public class CategoriaBLL
    {
        //Metodo que retorna una lista de categorias desde la capa de datos
        private CategoriaDAL dal = new CategoriaDAL();

        //Metodo que retorna una lista de categorias
        public List<Categoria> Listar_categoria()
        {
            try
            {
                //Llama al metodo de la capa de datos para obtener la lista de categorias
                return dal.Listar_categoria();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en BLL: " + ex.Message);
                //En caso de error, retorna una lista vacia
                return new List<Categoria>();
            }
        }
    }
}
