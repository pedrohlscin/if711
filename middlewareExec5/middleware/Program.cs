using middleware.UDP;
using middleware.TCP;
using System;

namespace middleware
{
    class Program
    {
        static void Main(string[] args)
        {
            int qtdClients = 1;
            //RunUDP.startUDP(qtdClients);
            RunTCP.startTCP(qtdClients);
            //Console.WriteLine("Hello World!");
        }
    }
}
