using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace middleware.UDP
{
    class RunUDP
    {
        public static void startUDP(int qtdClients)
        {
            var sUDP = new ServerUDP();
            Thread serverUdp = new Thread(sUDP.ReceiveSend);
            serverUdp.Start();
            Thread[] clients = new Thread[qtdClients];
            for(var i = 0; i < qtdClients; i++)
            {
                var cUDP = new ProgramaClienteUDP();
                clients[i] = new Thread(cUDP.Main);
                clients[i].Start();
            }

            for (var i = 0; i < qtdClients; i++)
            {
                clients[i].Join();
            }
        }

    }
}
