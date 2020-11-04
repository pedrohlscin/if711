using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace middleware.UDP
{
    class RunUDP
    {
        public static long amountOfTimeEllapsed = 0;
        public static List<long> executions = new List<long>();

        public static void startUDP(int qtdClients)
        {
            var sUDP = new ServerUDP();
            Thread serverUdp = new Thread(sUDP.ReceiveSend);
            serverUdp.Start();
            Thread[] clients = new Thread[qtdClients];
            Stopwatch watch = Stopwatch.StartNew();
            for (var i = 0; i < qtdClients; i++)
            {
                var cUDP = new ProgramaClienteUDP();
                clients[i] = new Thread(cUDP.Main);
                clients[i].Start();
            }

            for (var i = 0; i < qtdClients; i++)
            {
                if (i == 0)
                {
                    watch.Stop();
                }
                clients[i].Join();
            }
            executions.Add(watch.ElapsedMilliseconds);
            amountOfTimeEllapsed += watch.ElapsedMilliseconds;
            Console.WriteLine("Total time ellapsed: {0} in milliseconds", amountOfTimeEllapsed);
            var mediaOfTimeReq = (double)amountOfTimeEllapsed / (double)10000;
            Console.WriteLine("Media of time by requisition: {0} in milliseconds", mediaOfTimeReq);
            double deviation = 0;
            foreach (var i in executions)
            {
                deviation += Math.Pow((i - mediaOfTimeReq), 2);
            }
            deviation /= (double)10000;
            deviation = Math.Sqrt(deviation);
            Console.WriteLine("Deviation: {0}", deviation);
        }

    }
}
