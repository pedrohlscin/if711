using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;

namespace eval
{
    class Program
    {
        static void Main(string[] args)
        {
            startTcp();
        }

        private static void startTcp(){
            Thread server = new Thread(CalculadoraTCP.runCalculadoraTcp);
            server.Start();
        }
    }
}
