using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karolina_Collection_.CapaEntidades
{
    public class SesionActual
    {
        public static int id { get; set; }
        public static string nombre_usuario { get; set; }
        public static string rol { get; set; }

        //método para cuando cerremos sesión, borra los datos guardados.
        public static void Cerrar()
        {
            id = 0;
            nombre_usuario = null;
            rol = null;
        }

    }
}
