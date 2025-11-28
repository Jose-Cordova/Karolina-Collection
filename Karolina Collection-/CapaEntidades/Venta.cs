using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karolina_Collection_.CapaEntidades
{
    public class Venta
    {
        public int id { get; set; }
        public DateTime fecha { get; set; }
        public Decimal subtotal { get; set; }
        public Decimal monto_iva { get; set; }
        public Decimal total_venta { get; set; }
        public int id_cliente { get; set; }
        public int id_metodo_pago { get; set; }
    }
}
