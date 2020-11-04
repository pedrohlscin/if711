using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace middleware
{
    class ClientUDP
    {

        private const int listenPort = 9098;
        public byte[] sendReceive(byte[] input)
        {
            var client = new UdpClient();
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), listenPort); // endpoint where server is listening
            client.Connect(ep);
            // send data
            client.Send(input, input.Length);
            // then receive data
            var receivedData = client.Receive(ref ep);
            return receivedData;
        }
    }

}
