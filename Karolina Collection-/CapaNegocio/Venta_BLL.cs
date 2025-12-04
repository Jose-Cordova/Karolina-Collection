using Karolina_Collection_.CapaDatos;
using Karolina_Collection_.CapaEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karolina_Collection_.CapaNegocio
{
    public class Venta_BLL
    {
        public static RespuestaOperacion ValidarVenta(Venta venta, List<Detalle_venta> detalles)
        {
            // =================================================================================
            // 1. VALIDACIONES ESTRUCTURALES BÁSICAS
            // Evitan que el programa "falle"  por datos vacíos o nulos.
            // =================================================================================

            // Verificamos que el objeto principal no sea nulo para evitar "NullReferenceException"
            if (venta == null)
                return new RespuestaOperacion { Exito = false, Mensaje = "Venta no válida." };

            // Validamos claves foráneas (FK): Un ID 0 o negativo no existe en la BD
            if (venta.id_cliente <= 0)
                return new RespuestaOperacion { Exito = false, Mensaje = "Debe seleccionar un cliente." };

            if (venta.id_metodo_pago <= 0)
                return new RespuestaOperacion { Exito = false, Mensaje = "Debe seleccionar un tipo de pago." };

            // Regla de Negocio: No se puede guardar una factura vacía (sin productos)
            if (detalles == null || detalles.Count == 0)
            {
                return new RespuestaOperacion
                {
                    Exito = false,
                    Mensaje = "La venta debe contener al menos un producto."
                };
            }

            // =================================================================================
            // 2. VALIDACIONES FINANCIERAS GLOBALES (CABECERA)
            // Reglas lógicas sobre el dinero antes de ver los productos.
            // =================================================================================

            if (venta.total_venta <= 0)
                return new RespuestaOperacion { Exito = false, Mensaje = "El total de la venta debe ser mayor a cero." };

            // IMPORTANTE: El IVA es un impuesto, nunca puede ser negativo.
            if (venta.monto_iva < 0)
            {
                return new RespuestaOperacion { Exito = false, Mensaje = "El monto del IVA no puede ser negativo." };
            }

            // Lógica de negocio: El impuesto nunca puede ser mayor que la venta total.
            // Esto previene errores de dedo (ej: Total 100, IVA 1000).
            if (venta.monto_iva >= venta.total_venta)
            {
                return new RespuestaOperacion { Exito = false, Mensaje = "El monto del IVA no puede ser mayor o igual al Total de la venta." };
            }

            // =================================================================================
            // 3. VALIDACIÓN DETALLADA (PRODUCTO POR PRODUCTO)
            // =================================================================================

            decimal sumaSubtotalesCalculada = 0; // Acumulador para la validación final

            foreach (var d in detalles)
            {
                // --- A. Integridad de datos ---
                // No podemos vender 0 o -1 productos, ni vender a precio 0 o negativo.
                if (d.cantidad <= 0)
                    return new RespuestaOperacion { Exito = false, Mensaje = $"La cantidad del producto ID {d.id_producto_variante} es inválida." };

                if (d.precio_unitario <= 0)
                    return new RespuestaOperacion { Exito = false, Mensaje = $"Precio unitario inválido para el producto ID {d.id_producto_variante}" };

                // --- B. Validación Matemática por Fila ---
                // Esto evita hackeos o errores de UI. Si dice 2 productos a $10, el subtotal OBLIGATORIAMENTE debe ser $20.
                decimal subtotalReal = d.cantidad * d.precio_unitario;

                if (d.sub_total != subtotalReal)
                    return new RespuestaOperacion { Exito = false, Mensaje = $"Subtotal incorrecto para el producto ID {d.id_producto_variante}." };

                // Vamos sumando lo que REALMENTE valen los productos para compararlo al final
                sumaSubtotalesCalculada += subtotalReal;

                // --- C. Validación de Inventario (Conexión con DAL) ---
                // Antes de vender, preguntamos a la base de datos si hay existencias.
                int stockActual = Producto_variante_DAL.ObtenerStock(d.id_producto_variante);

                if (stockActual < d.cantidad)
                {
                    return new RespuestaOperacion { Exito = false, Mensaje = $"Stock insuficiente del producto ID {d.id_producto_variante} (Stock actual: {stockActual})." };
                }
            }

            // =================================================================================
            // 4. VALIDACIÓN DE CONSISTENCIA MATEMÁTICA FINAL (CRÍTICO)
            // Esta es la validación más importante para la contabilidad.
            // =================================================================================

            // Fórmula: La suma de todos los productos + el impuesto DEBE ser igual al Total Final.
            decimal totalEsperado = sumaSubtotalesCalculada + venta.monto_iva;

            if (venta.total_venta != totalEsperado)
            {
                // Si no cuadra, rechazamos la venta. Esto evita que en la BD diga:
                // Productos: $100, IVA: $13, Total: $500 (Error grave).
                return new RespuestaOperacion
                {
                    Exito = false,
                    Mensaje = $"Error matemático: La suma de productos ({sumaSubtotalesCalculada}) + IVA ({venta.monto_iva}) no coincide con el Total ({venta.total_venta})."
                };
            }

            // =================================================================================
            // 5. ÉXITO
            // Si el código llega aquí, la venta es segura, tiene stock y los números cuadran.
            // =================================================================================
            return new RespuestaOperacion { Exito = true, Mensaje = "Validación correcta." };
        }
    }
}
