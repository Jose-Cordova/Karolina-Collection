using Karolina_Collection_.CapaDatos;
using Karolina_Collection_.CapaEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karolina_Collection_.CapaNegocio
{
    public class MarcaBLL
    {
        //Metodo que retorna una lista de marcas desde la capa de datos
        private MarcaDAL dal = new MarcaDAL();

        //Metodo que retorna una lista de marcas
        public List<Marca> Listar_marca()
        {
            try
            {
                //Llama al metodo de la capa de datos para obtener la lista de marcas
                return dal.Listar_marca();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en BLL: " + ex.Message);
                //En caso de error, retorna una lista vacia
                return new List<Marca>();
            }
        }
    }
}
