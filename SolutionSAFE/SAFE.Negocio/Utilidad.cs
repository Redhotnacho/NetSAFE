using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAFE.Negocio
{
    public static class Utilidad
    {
        // validador de RUTs
        public static bool ValidaRut(String rut)
        {

            String[] lstring = rut.Split('-');
            if (lstring.Length != 2)
            {
                return false;
            }
            else if (lstring[1].Length > 1)
            {
                return false;
            }
            int suma = 0;
            int[] serie = { 2, 3, 4, 5, 6, 7 };
            int contadors = 0;
            for (int i = lstring[0].ToCharArray().Length - 1; i >= 0; i--)
            {
                if (lstring[0].ToCharArray()[i] == '0'
                        || lstring[0].ToCharArray()[i] == '1'
                        || lstring[0].ToCharArray()[i] == '2'
                        || lstring[0].ToCharArray()[i] == '3'
                        || lstring[0].ToCharArray()[i] == '4'
                        || lstring[0].ToCharArray()[i] == '5'
                        || lstring[0].ToCharArray()[i] == '6'
                        || lstring[0].ToCharArray()[i] == '7'
                        || lstring[0].ToCharArray()[i] == '8'
                        || lstring[0].ToCharArray()[i] == '9')
                {
                    suma += serie[contadors] * int.Parse("" + lstring[0].ToCharArray()[i]);
                    contadors++;
                    if (contadors > 5)
                    {
                        contadors = 0;
                    }
                }
            }
            int cverif = 11 - (Math.Abs(suma % 11));
            if (lstring[1].ToCharArray()[0] == '0'
                    || lstring[1].ToCharArray()[0] == '1'
                    || lstring[1].ToCharArray()[0] == '2'
                    || lstring[1].ToCharArray()[0] == '3'
                    || lstring[1].ToCharArray()[0] == '4'
                    || lstring[1].ToCharArray()[0] == '5'
                    || lstring[1].ToCharArray()[0] == '6'
                    || lstring[1].ToCharArray()[0] == '7'
                    || lstring[1].ToCharArray()[0] == '8'
                    || lstring[1].ToCharArray()[0] == '9')
            {

                if (cverif == 11)
                {
                    return int.Parse(lstring[1]) == 0;
                }
                return int.Parse(lstring[1]) == cverif;
            }

            if (cverif == 10)
            {
                return lstring[1].ToLower().Equals("k");
            }
            return false;
        }

        public static bool ValidaCorreo(string correo)
        {
            String[] lstring = correo.Split('@');
            if (lstring.Length != 2)
            {
                return false;
            }
            else if (lstring[1].Length < 3)
            {
                return false;
            }
            return true;
        }

        //metodo que formatea los precios
        public static String FormatValor(int precio)
        {
            String sprecio = precio.ToString();
            if (sprecio.Length > 3)
            {
                String nprecio = "";
                int plus = -1;
                for (int i = sprecio.ToCharArray().Length - 1; i >= 0; i--)
                {
                    plus++;
                    if (plus == 3)
                    {
                        nprecio = nprecio + "." + sprecio.ToCharArray()[i];
                        plus = 0;
                    }
                    else
                    {
                        nprecio = nprecio + sprecio.ToCharArray()[i];
                    }
                }
                return InversoValor(nprecio);
            }
            return sprecio;
        }

        // Para ingresar ruts a la BD
        public static String FormatRutIngreso(String rut)
        {
            String[] lstring = rut.Split('-');
            if (lstring.Length != 2)
            {
                return "";
            }
            else if (lstring[1].Length > 1)
            {
                return "";
            }
            else
            {
                String rut2 = "";
                foreach (char c in lstring[0].ToCharArray())
                {
                    if (c == '0' || c == '1' || c == '2' || c == '3' || c == '4' || c == '5' || c == '6' || c == '7' || c == '8' || c == '9')
                    {
                        rut2 = rut2 + c;
                    }
                }
                return rut2 + '-' + lstring[1];
            }
        }

        // Para formatear ruts desde la BD
        public static String FormatRutSalida(String rut)
        {
            String[] lstring = rut.Split('-');
            if (lstring.Length != 2)
            {
                return "";
            }
            else if (lstring[1].Length > 1)
            {
                return "";
            }
            else
            {
                String rut2 = "";
                foreach (char c in lstring[0].ToCharArray())
                {
                    if (c == '0' || c == '1' || c == '2' || c == '3' || c == '4' || c == '5' || c == '6' || c == '7' || c == '8' || c == '9')
                    {
                        rut2 = rut2 + c;
                    }
                }
                return FormatValor(int.Parse(rut2)) + '-' + lstring[1];
            }
        }


        // necesario para formatValor()
        private static String InversoValor(String nprecio)
        {
            String nprecio2 = "";
            for (int i = nprecio.ToCharArray().Length - 1; i >= 0; i--)
            {
                nprecio2 = nprecio2 + nprecio.ToCharArray()[i];
            }

            return nprecio2;
        }
    }
}
