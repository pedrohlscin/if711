using eval;
using System;
using System.Threading;

namespace middlewareExec2
{
    class Program
    {
        static void Main(string[] args)
        {
            startTcp();
        }

        private static void startUdp()
        {
            Thread program = new Thread(CalculadoraUDP.runCalculadoraUdp);
            program.Start();
        }

        private static void startTcp()
        {
            Thread program = new Thread(CalculadoraTCP.runCalculadoraTcp);
            program.Start();
        }
    }
}
