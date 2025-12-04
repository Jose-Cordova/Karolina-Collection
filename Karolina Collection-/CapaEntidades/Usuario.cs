using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karolina_Collection_.CapaEntidades
{
    public class Usuario
    {
        public int id { get; set; }
        public string nombre_usuario { get; set; }
        public string clave_hash { get; set; }
        public string rol { get; set; }
        public bool estado { get; set; }
        public string fecha_creacion { get; set; }
    }
}
