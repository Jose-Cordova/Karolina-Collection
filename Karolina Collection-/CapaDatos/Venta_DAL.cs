using Karolina_Collection_.CapaEntidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karolina_Collection_.CapaDatos
{
    public class Venta_DAL
    {
        public static (bool Exito, string Mensaje) RegistrarVentaTransaccional(Venta venta, List<Detalle_venta> detalles)
        {
            using (SqlConnection con = new SqlConnection(Conexion_DB.cadena_conexion))
            {
                con.Open();

                // Inicia el proceso pero si falla cancela el proceso antes de tirar error.
                SqlTransaction tx = con.BeginTransaction();

                // En caso de error lo captura
                try
                {
                    // 1) INSERTAMOS VENTA Y RECUPERAMOS ID DE LA VENTA INGRESADA DESDE LA DB
                    
                    
                    string sqlVenta = @"
                        INSERT INTO Venta (fecha, monto_iva, total_venta, id_metodo_pago, id_cliente)
                        VALUES (@fecha, @monto_iva, @total_venta, @id_metodo_pago, @id_cliente);
                        SELECT SCOPE_IDENTITY();";

                    // LLeva la consulta ala DB
                    using (SqlCommand cmd = new SqlCommand(sqlVenta, con, tx))
                    {
                        cmd.Parameters.AddWithValue("@fecha", venta.fecha);
                        cmd.Parameters.AddWithValue("@monto_iva", venta.monto_iva);
                        cmd.Parameters.AddWithValue("@total_venta", venta.total_venta);
                        cmd.Parameters.AddWithValue("@id_metodo_pago", venta.id_metodo_pago);
                        cmd.Parameters.AddWithValue("@id_cliente", venta.id_cliente);

                        // Recuperamos ID generado
                        venta.id = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                    // 2) INSERTAMOS LOS DETALLES

                    string sqlDetalle = @"
                        INSERT INTO Detalle_venta (cantidad, precio_unitario, sub_total, id_venta, id_producto_variante)
                        VALUES (@cantidad, @precio_unitario, @sub_total, @id_venta, @id_producto_variante);";

                    // Acumular cantidades por producto (para descontar stock una sola vez)
                    var acumulador = new Dictionary<int, int>();
                    // Recorre todos los productos de la lista
                    foreach (var d in detalles)
                    {
                        // A) Insertar detalle de venta
                        using (SqlCommand cmdDet = new SqlCommand(sqlDetalle, con, tx))
                        {
                            cmdDet.Parameters.AddWithValue("@cantidad", d.cantidad);
                            cmdDet.Parameters.AddWithValue("@precio_unitario", d.precio_unitario);
                            cmdDet.Parameters.AddWithValue("@sub_total", d.sub_total);
                            cmdDet.Parameters.AddWithValue("@id_venta", venta.id);
                            cmdDet.Parameters.AddWithValue("@id_producto_variante", d.id_producto_variante);

                            cmdDet.ExecuteNonQuery();
                        }

                        // B) Actualizar acumulador
                        // Si este producto NO está en el acumulador, lo agregamos con cantidad 0

                        if (!acumulador.ContainsKey(d.id_producto_variante))
                            acumulador[d.id_producto_variante] = 0;

                        // Sumamos la cantidad vendida al acumulador de este producto
                        acumulador[d.id_producto_variante] += d.cantidad;
                    }

                    // 3) DESCONTAREMOS EL STOCK (validación interna)

                    string sqlStock = @"
                        UPDATE Producto_variante
                        SET stock = stock - @Cant
                        WHERE id = @id_producto_variante AND stock >= @Cant;";


                    // Recorremos el acumulador (cada producto único vendido con su cantidad total)
                    foreach (var item in acumulador)
                    {
                        using (SqlCommand cmdStock = new SqlCommand(sqlStock, con, tx))
                        {
                            // item.Value = cantidad total a descontar
                            // item.Key = ID del producto
                            cmdStock.Parameters.AddWithValue("@Cant", item.Value);
                            cmdStock.Parameters.AddWithValue("@id_producto_variante", item.Key);

                            int filas = cmdStock.ExecuteNonQuery();

                            // Si no se afectó ninguna fila → NO había stock suficiente
                            if (filas == 0)
                                throw new Exception("Stock insuficiente para el producto ID: " + item.Key);
                        }
                    }
                    // 4) CONFIRMAMOS LA TRANSACCIÓN
                    tx.Commit();
                    // Se le muestra al usuario final el mensaje en caso si se cumplieran las validaciones
                    return (true, "Venta registrada con éxito. ID generado: " + venta.id);
                }

                catch (Exception ex)
                {
                    // Si algo falla → revertir todo
                    // Rollback() DESHACE TODO lo que hicimos
        // Es como presionar "deshacer" - la base de datos vuelve al estado original
        // Esto garantiza que no queden datos a medias
                    tx.Rollback();
                    // Mostrara el que hubo un error a registar
                    return (false, "Error al registrar venta: " + ex.Message);
                }
            }

        }
    }
}
