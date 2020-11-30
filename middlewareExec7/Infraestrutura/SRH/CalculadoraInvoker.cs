using System;
using System.Collections.Generic;
using middlewareExec7.Server;

namespace middlewareExec7
{
    internal class CalculadoraInvoker
    {
        public CalculadoraInvoker()
        {}

        internal byte[] Invoke(byte[] bytes)
        {
            String entrada = Marshall.Marshaller.Unmarshall(bytes);
            Server.Server server = null;
            while (server == null)
            {
                Pool.GetInstance().getServer();
            }
            string expExaluated = server.calculaCoisa(entrada);
            Pool.GetInstance().putServer(server);
            byte[] saida = Marshall.Marshaller.Marshall(expExaluated);
            return saida;
        }
    }
}