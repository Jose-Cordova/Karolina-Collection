using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karolina_Collection_.CapaEntidades
{
    public class Cliente
    {
        public int id { get; set; }
        public string nombre_completo { get; set; }
        public int dui { get; set; }
        public int telefono { get; set; }
        public string correo_electronico { get; set; }
    }
}
