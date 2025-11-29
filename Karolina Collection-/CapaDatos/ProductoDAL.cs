using Karolina_Collection_.CapaEntidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karolina_Collection_.CapaDatos
{
    public class ProductoDAL
    {
        //Metodo para listar
        public DataTable Listar_producto()
        {
            //Crea una instancia para almacenar datos
            DataTable dt = new DataTable();
            //Se establece la conexion con DB
            using (SqlConnection conexion = new SqlConnection(Conexion_DB.cadena_conexion))
            {
                string consulta = @"
                    SELECT p.id, 
                           p.nombre_producto, 
                           p.descripcion, 
                           p.precio_base, 
                           sc.nombre_sub_categoria, 
                           c.nombre_categoria,
                           m.nombre_marca, 
                           pr.nombre_proveedor
                    FROM Producto p
                    INNER JOIN Sub_categoria sc ON p.id_sub_categoria = sc.id
                    INNER JOIN Categoria c ON sc.id_categoria = c.id
                    INNER JOIN Marca m ON p.id_marca = m.id
                    INNER JOIN Proveedor pr ON p.id_proveedor = pr.id";
                //Se manejan los errores
                try
                {
                    //Abrir conexion
                    conexion.Open();
                    //Se lleva la consulta a la DB
                    using (SqlCommand cmd = new SqlCommand(consulta, conexion))
                    {
                        //Se ejecuta la consulta
                        new SqlDataAdapter(cmd).Fill(dt);
                    }
                }
                catch (Exception ex)
                {
                    dt = null;
                    Console.WriteLine("Error al listar productos: " + ex.Message);
                }
            }
            return dt;
        }
        public bool Registrar_producto_transaccional(Producto p, List<Producto_variante> listaVariantes, out string Mensaje)
        {
            Mensaje = string.Empty;

            using (SqlConnection conexion = new SqlConnection(Conexion_DB.cadena_conexion))
            {
                try
                {
                    conexion.Open();
                    using (SqlTransaction transaccion = conexion.BeginTransaction())
                    {
                        try
                        {
                            string consulta = @"
                                INSERT INTO Producto(nombre_producto, descripcion, precio_base, id_sub_categoria, id_marca, id_proveedor) 
                                VALUES(@nombre_producto, @descripcion, @precio_base, @id_sub_categoria, @id_marca, @id_proveedor);
                                SELECT SCOPE_IDENTITY();";

                            int id_generado = 0;

                            using (SqlCommand cmd = new SqlCommand(consulta, conexion, transaccion))
                            {
                                cmd.Parameters.AddWithValue("@nombre_producto", p.nombre_producto);
                                cmd.Parameters.AddWithValue("@descripcion", p.descripcion);
                                cmd.Parameters.AddWithValue("@precio_base", p.precio_base);
                                cmd.Parameters.AddWithValue("@id_sub_categoria", p.id_sub_categoria);
                                cmd.Parameters.AddWithValue("@id_marca", p.id_marca);
                                cmd.Parameters.AddWithValue("@id_proveedor", p.id_proveedor);

                                id_generado = Convert.ToInt32(cmd.ExecuteScalar());
                            }
                            foreach (Producto_variante item in listaVariantes)
                            {
                                string consulta_variante = @"
                                     INSERT INTO Producto_variante(stock, precio_venta, id_talla, id_color, id_producto) 
                                     VALUES(@stock, @precio_venta, @id_talla, @id_color, @id_producto)";

                                using (SqlCommand cmdVar = new SqlCommand(consulta_variante, conexion, transaccion))
                                {
                                    cmdVar.Parameters.AddWithValue("@stock", item.stock);
                                    cmdVar.Parameters.AddWithValue("@precio_venta", item.precio_venta);
                                    cmdVar.Parameters.AddWithValue("@id_talla", item.id_talla);
                                    cmdVar.Parameters.AddWithValue("@id_color", item.id_color);
                                    cmdVar.Parameters.AddWithValue("@id_producto", id_generado);

                                    cmdVar.ExecuteNonQuery();
                                }
                            }
                            transaccion.Commit();
                            Mensaje = "Producto y variantes registrados con éxito.";
                            return true;
                        }
                        catch (Exception ex)
                        {
                            transaccion.Rollback();
                            Mensaje = "Error en la transacción: " + ex.Message;
                            return false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Mensaje = "Error de conexión: " + ex.Message;
                    return false;
                }
            }
        }
        public bool Actualizar_producto(Producto p)
        {
            using (SqlConnection conexion = new SqlConnection(Conexion_DB.cadena_conexion))
            {
                try
                {
                    conexion.Open();
                    string consulta = @"
                        UPDATE Producto SET 
                        nombre_producto = @nombre_producto,
                        descripcion = @descripcion,
                        precio_base = @precio_base,
                        id_sub_categoria = @id_sub_categoria,
                        id_marca = @id_marca,
                        id_proveedor = @id_proveedor
                        WHERE id = @id";
                    SqlCommand cmd = new SqlCommand(consulta, conexion);
                    //Agrega los parametros insertados a la consulta
                    cmd.Parameters.AddWithValue("@id", p.id);
                    cmd.Parameters.AddWithValue("@nombre_producto", p.nombre_producto);
                    cmd.Parameters.AddWithValue("@descripcion", p.descripcion);
                    cmd.Parameters.AddWithValue("@precio_base", p.precio_base);
                    cmd.Parameters.AddWithValue("@id_sub_categoria", p.id_sub_categoria);
                    cmd.Parameters.AddWithValue("@id_marca", p.id_marca);
                    cmd.Parameters.AddWithValue("@id_proveedor", p.id_proveedor);

                    //Ejecuta la consulta y devuelve el numero de filas afectadas
                    return cmd.ExecuteNonQuery() > 0;

                }
                catch (Exception ex)
                {
                    //En caso de error, retorna false
                    Console.WriteLine("Error al actualizar producto: " + ex.Message);
                    return false;
                }
            }
        }
        public bool Eliminar_producto(Producto p)
        {
            using (SqlConnection conexion = new SqlConnection(Conexion_DB.cadena_conexion))
            {
                try
                {
                    conexion.Open();
                    //Validar si tiene relaciones en otras tablas
                    string consulta_validar = @"
                        SELECT COUNT(*) FROM Producto_variante WHERE id_producto = @id";
                    SqlCommand cmd_validar = new SqlCommand(consulta_validar, conexion);
                    cmd_validar.Parameters.AddWithValue("@id", p.id);

                    int relaciones = (int)cmd_validar.ExecuteScalar();
                    if (relaciones > 0)
                    {
                        Console.WriteLine("No se puede eliminar el producto porque tiene variantes asociadas.");
                        return false;
                    }
                    //Proceder a eliminar el producto
                    string consulta = "DELETE FROM Producto WHERE id = @id";
                    SqlCommand cmd = new SqlCommand(consulta, conexion);
                    cmd.Parameters.AddWithValue("@id", p.id);
                    return cmd.ExecuteNonQuery() > 0;
                }
                catch (Exception ex)
                { 
                    //En caso de error, retorna false
                    Console.WriteLine("Error al eliminar producto: " + ex.Message);
                    return false;
                }

            }

        }

    }
}
