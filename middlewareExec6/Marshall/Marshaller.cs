using System;
using System.Collections.Generic;
using System.Text;

namespace middlewareExec6.Marshall
{
    class Marshaller
    {
        public static byte[] Marshall(String entrada)
        {
            return System.Text.ASCIIEncoding.ASCII.GetBytes(entrada);
        }

        public static String Unmarshall(byte[] entrada)
        {
            return System.Text.ASCIIEncoding.ASCII.GetString(entrada);
        }

    }
}
