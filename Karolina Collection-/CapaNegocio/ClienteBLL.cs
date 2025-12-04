using Karolina_Collection_.CapaDatos;
using Karolina_Collection_.CapaEntidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Karolina_Collection_.CapaNegocio
{
    public class ClienteBLL
    {
        Cliente_DAL dal = new Cliente_DAL();
        public DataTable Listar()
        {
            return dal.Listar();
        }

        public int Guardar(Cliente c)
        {
            if (string.IsNullOrEmpty(c.nombre_completo))
            {
                throw new ArgumentException("El nombre del cliente no puede estar vacío.");

            }
            if (c.id == 0)
            {
                // Nuevo cliente
                return dal.Insertar(c);
            }
            else
            {
                dal.Actualizar(c);
                return c.id;
            }

        }
        public void Eliminar(int id)
        {
            dal.Eliminar(id);
        }
        public DataTable Buscar(string texto)
        {
            return dal.Buscar(texto);
        }

        public void Actualizar(Cliente c)
        {
            dal.Actualizar(c);
        }
        public bool ExisteClientePorDui(string dui)
        {
            return dal.ExisteClientePorDui(dui);
        }


    }
}
