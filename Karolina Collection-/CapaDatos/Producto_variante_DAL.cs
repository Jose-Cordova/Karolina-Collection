using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Karolina_Collection_.CapaDatos
{
    public class Producto_variante_DAL
    {
        // Funcion que obtiene el stock de la tabla producto_variante
        public static int ObtenerStock(int id_producto_variante)
        {
            int stock = 0;
            // Conexion para el stock en la DB 
            using (SqlConnection con = new SqlConnection(Conexion_DB.cadena_conexion))
            {
                string sql = "SELECT Stock FROM Producto_variante WHERE id = @id";

                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@id", id_producto_variante);

                    con.Open();
                    object result = cmd.ExecuteScalar();

                    if (result != null)
                        stock = Convert.ToInt32(result);
                }
            }

            return stock;
        }

        //La funcion que mostrara en el datagridview la tabla de producto 
        public static DataTable Listar()
        {
            DataTable tabla = new DataTable();

            using (SqlConnection con = new SqlConnection(Conexion_DB.cadena_conexion))
            {
                string sql = @"
                    SELECT pv.id, p.nombre_producto AS Producto, pv.stock, pv.precio_venta, t.nombre_talla AS Talla, c.nombre_color AS Color 
                    FROM Producto_variante pv
                    INNER JOIN Talla t ON pv.id_talla = t.id 
                    INNER JOIN Color c ON pv.id_color = c.id 
                    INNER JOIN Producto p ON pv.id_producto = p.id";

                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    con.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        tabla.Load(dr);
                    }
                }
            }
            return tabla;
        }
        //Metodo para cambiar solo el Stock y el Precio de la venta
        public static bool Actualizar_producto_variente(int id_variante, int nuevo_stock, decimal nuevo_precio)
        {
            bool exito = false;
            //Habrimos conexion a la base de datos
            using (SqlConnection conexion = new SqlConnection(Conexion_DB.cadena_conexion))
            {
                //Consulta para actualizar la variante espesifica por su id
                string consulta = @"
                    UPDATE Producto_variante 
                    SET stock = @stock, 
                        precio_venta = @precio_venta
                        WHERE id = @id";

                //Enviamos la consulta a la base de datos
                using (SqlCommand cmd = new SqlCommand(consulta, conexion))
                {
                    //Pasamos los parametros 
                    cmd.Parameters.AddWithValue("@stock", nuevo_stock);
                    cmd.Parameters.AddWithValue("@precio_venta", nuevo_precio);
                    cmd.Parameters.AddWithValue("@id", id_variante);

                    //Se ejecuta la operacion y se controla si hay error
                    try
                    {
                        conexion.Open();
                        // ExecuteNonQuery devuelve el número de filas afectadas.
                        // Si es mayor a 0, significa que sí encontró y actualizó el registro.
                        int filas_afectadas = cmd.ExecuteNonQuery();
                        exito = (filas_afectadas > 0);

                    }
                    //Se captura el error
                    catch (Exception e)
                    {
                        Console.WriteLine("Ocurrior un error:", e);
                        //Queda en falso por que hubo problemas
                        exito = false;
                    }
                }   
            }
            return exito;
        }
        //Metodo para eliminar variantes especificas y no eliminar el producto
        public static bool Eliminar_producto_variente(int id_variante)
        {
            bool exito = false;
            //Habrimos conexion a la base de datos
            using (SqlConnection conexion = new SqlConnection(Conexion_DB.cadena_conexion))
            {   //Consulta para eliminar la variante espesifica por su id
                string consulta = @"DELETE FROM Producto_variante WHERE id = @id";

                //Enviamos la consulta a la base de datos
                using (SqlCommand cmd = new SqlCommand(consulta, conexion))
                {
                    //Pasamos los parametros 
                    cmd.Parameters.AddWithValue("@id", id_variante);

                    //Se ejecuta la operacion y se controla si hay error
                    try
                    {
                        //Si es mayor a 0, significa que sí encontró y actualizó el registro.
                        conexion.Open();
                        int filas_afectadas = cmd.ExecuteNonQuery();
                        exito = (filas_afectadas > 0);
                    }
                    //Se captura el error
                    catch (Exception e)
                    {
                        Console.WriteLine("Ocurrio un error:", e);
                        //Queda en falso por que hubo problemas
                        exito = false;
                    }
                }
            }
            return exito;
        }
        public static DataTable Buscar_producto_variante(string textoBuscar)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conexion = new SqlConnection(Conexion_DB.cadena_conexion))
            {
                // Usamos la misma consulta base, pero agregamos el filtro WHERE
                string consulta = @"
            SELECT 
                pv.id, 
                p.nombre_producto AS Producto, 
                m.nombre_marca AS Marca, 
                t.nombre_talla AS Talla, 
                c.nombre_color AS Color, 
                pv.stock, 
                pv.precio_venta
            FROM Producto_variante pv
            INNER JOIN Producto p ON pv.id_producto = p.id
            INNER JOIN Marca m ON p.id_marca = m.id
            INNER JOIN Talla t ON pv.id_talla = t.id
            INNER JOIN Color c ON pv.id_color = c.id
            WHERE 
                p.nombre_producto LIKE @texto OR 
                m.nombre_marca LIKE @texto OR 
                t.nombre_talla LIKE @texto OR 
                c.nombre_color LIKE @texto";

                using (SqlCommand cmd = new SqlCommand(consulta, conexion))
                {
                    // Agregamos los % para que busque coincidencias parciales
                    cmd.Parameters.AddWithValue("@texto", "%" + textoBuscar + "%");

                    try
                    {
                        // Abrimos la conexión
                        conexion.Open();

                        // Ejecutamos la consulta y leemos los resultados
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            // Cargamos todos los resultados encontrados en la tabla
                            dt.Load(dr);
                        }
                    }
                    // Si algo falló
                    catch (Exception ex)
                    {
                        // Mostramos el error
                        Console.WriteLine("Error encontrado:", ex);

                        // Devolvemos null para indicar que hubo un problema
                        dt = null;

                    }
                }
            }
            return dt;
        }
    }
}
