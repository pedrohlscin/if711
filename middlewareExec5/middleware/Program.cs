using middleware.UDP;
using System;

namespace middleware
{
    class Program
    {
        static void Main(string[] args)
        {
            int qtdClients = 2;
            RunUDP.startUDP(qtdClients);
            //Console.WriteLine("Hello World!");
        }
    }
}
