using System;
using middlewareExec6.Server;

namespace middlewareExec6
{
    internal class CalculadoraInvoker
    {
        public CalculadoraInvoker()
        {
        }

        internal byte[] Invoke(byte[] bytes)
        {
            String entrada = Marshall.Marshaller.Unmarshall(bytes);
            string expExaluated = Server.Server.calculaCoisa(entrada);
            byte[] saida = Marshall.Marshaller.Marshall(expExaluated);
            return saida;
        }
    }
}