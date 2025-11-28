using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karolina_Collection_.CapaEntidades
{
    public class Detalle_venta
    {
        public int id { get; set; }
        public Decimal precio_unitario { get; set; }
        public int cantidad { get; set; }
        public int id_producto_variante { get; set; }
        public int id_venta { get; set; }
    }
}
