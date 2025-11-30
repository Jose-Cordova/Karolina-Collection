using Karolina_Collection_.CapaDatos;
using Karolina_Collection_.CapaEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karolina_Collection_.CapaNegocio
{
    public class ColorBLL
    {
        //Metodo que retorna una lista de colores desde la capa de datos
        private ColorDAL dal = new ColorDAL();
        //Metodo que retorna una lista de colores
        public List<Color> Listar_color()
        {
            try
            {
                //Llamada al metodo de la capa de datos
                return dal.Listar_color();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en BLL: " + ex.Message);
                //Retorna una lista vacia en caso de error
                return new List<Color>();
            }
        }
    }
}
