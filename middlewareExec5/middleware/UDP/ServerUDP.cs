using middleware.UDP;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace middleware
{
    public class ServerUDP
    {
        private const int listenPort = 9098;
        public void ReceiveSend()
        {
            UdpClient udpServer = new UdpClient(listenPort);
            var programaServidorUDP = new ProgramaServidorTCP();

            while (true)
            {
                var remoteEP = new IPEndPoint(IPAddress.Any, listenPort);
                var data = udpServer.Receive(ref remoteEP); // listen on port listenPort
                var retorno = programaServidorUDP.calculaCoisa(data);
                udpServer.Send(retorno, retorno.Length, remoteEP); // reply back
            }
        }
    }
}
