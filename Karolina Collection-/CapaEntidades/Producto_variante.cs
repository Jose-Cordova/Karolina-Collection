using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karolina_Collection_.CapaEntidades
{
    public class Producto_variante
    {
        public int id { get; set; }
        public int stock { get; set; }
        public Decimal precio_venta { get; set; }
        public int id_talla { get; set; }
        public int id_color { get; set; }
        public int id_producto { get; set; }
    }
}
