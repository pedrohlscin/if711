using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace middlewareExec6.Server
{
    class Server
    {
        public Server()
        {
            Console.WriteLine("Server inicializando");
            SRH srh = new SRH();
            Thread t = new Thread(srh.ReceiveSend);
            t.Start();
        }
    }
}
