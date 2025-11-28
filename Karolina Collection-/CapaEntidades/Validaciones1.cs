using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Karolina_Collection_.CapaEntidades
{
    public static class Validaciones1
    {
        //Validar que el valor sea entero
        public static bool EsEntero(string s)
        {
            int numero;
            return int.TryParse(s, out numero); //Metodo para connvertir una cadena a numeros enteros
        }
        //Validar que el valor sea decimal
        public static bool EsDecimal(string s)
        {
            decimal numero;
            return decimal.TryParse(s, out numero); //Metodo para connvertir una cadena a numeros decimales
        }
        //Validar direccion de correo electronico
        public static bool EsCorreoElectronico(string correo)
        {
            if (string.IsNullOrWhiteSpace(correo)) //Vereficar si el correo esta vacio
                return false;
            //Exprecion regular para validar correo
            var patron = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(correo, patron); //Metodo que compara la cadena con el patron
        }
    }
}
