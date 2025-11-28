using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karolina_Collection_.CapaEntidades
{
    public class Producto
    {
        public int id { get; set; }
        public string nombre_producto { get; set; }
        public string descripcion { get; set; }
        public Decimal precio_base { get; set; }
        public int id_sub_categoria { get; set; }
        public int id_marca { get; set; }
        public int id_proveedor { get; set; }
    }
}
