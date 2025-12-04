using Karolina_Collection_.CapaDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karolina_Collection_.CapaNegocio
{
    public class Cierre_cajaBLL
    {
        public static decimal ObtenerVentasDelDia(DateTime fecha)
        {
            //Llama el metodo para hacer el calculo
            return Cierre_cajaDAL.ObtenerVentasDelDia(fecha);
        }
    }
}
