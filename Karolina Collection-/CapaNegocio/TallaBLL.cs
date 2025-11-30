using Karolina_Collection_.CapaDatos;
using Karolina_Collection_.CapaEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karolina_Collection_.CapaNegocio
{
    public class TallaBLL
    {
        //Metodo que retorna una lista de tallas desde la capa de datos
        private TallaDAL dal = new TallaDAL();

        //Metodo que retorna una lista de tallas
        public List<Talla> Listar_talla()
        {
            try
            {
                //Llamada al metodo de la capa de datos
                return dal.Listar_talla();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en BLL: " + ex.Message);
                //En caso de error, retornar una lista vacia
                return new List<Talla>();
            }
        }
    }
}
