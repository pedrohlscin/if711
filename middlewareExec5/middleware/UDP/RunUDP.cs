using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace middleware.UDP
{
    class RunUDP
    {
        public static void startUDP()
        {
            var sUDP = new ServerUDP();
            Thread serverUdp = new Thread(sUDP.ReceiveSend);
            serverUdp.Start();
            var cUDP = new ProgramaClienteUDP();
            cUDP.Main();
        }

    }
}
