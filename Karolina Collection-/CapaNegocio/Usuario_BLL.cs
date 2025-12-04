using Karolina_Collection_.CapaDatos;
using Karolina_Collection_.CapaEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karolina_Collection_.CapaNegocio
{
    public class Usuario_BLL
    {
        
        public static Usuario Login(string usuario, string clave)
        {
            if (string.IsNullOrWhiteSpace(usuario) || string.IsNullOrWhiteSpace(clave))
                throw new ArgumentException("Debe ingresar usuario y contraseña.");

            string hash = Seguridad.Hash_SHA256(clave);
            return Usuario_DAL.Login(usuario, hash);
        }

        public static List<Usuario> Listar()
        {
            return Usuario_DAL.Listar();
        }

        public static int Insertar(string nombreUsuario, string clave, string rol)
        {
            if (string.IsNullOrWhiteSpace(nombreUsuario) || string.IsNullOrWhiteSpace(clave))
                throw new ArgumentException("Usuario y contraseña requeridos.");

            
            string hash = Seguridad.Hash_SHA256(clave);
            return Usuario_DAL.Insertar(nombreUsuario.Trim(), hash, rol);
        }
        public static bool Actualizar(int id, string nombreUsuario, string rol, bool estado)
        {
            return Usuario_DAL.Actualizar(id, nombreUsuario.Trim(), rol, estado);
        }

        public static bool Eliminar(int id)
        {
            return Usuario_DAL.Eliminar(id);
        }

        public static bool CambiarClave(int id, string claveNueva)
        {
            if (string.IsNullOrWhiteSpace(claveNueva))
                throw new ArgumentException("La nueva contraseña no puede estar vacía.");
            
            string hash = Seguridad.Hash_SHA256(claveNueva);
            return Usuario_DAL.CambiarClave(id, hash);
        }

    }
}
