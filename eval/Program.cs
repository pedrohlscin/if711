﻿using System;
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
            startUdp();
        }

        private static void startUdp()
        {
            Thread program = new Thread(CalculadoraUDP.runCalculadoraUdp);
            program.Start();
        }

        private static void startTcp(){
            Thread program = new Thread(CalculadoraTCP.runCalculadoraTcp);
            program.Start();
        }
    }
}
