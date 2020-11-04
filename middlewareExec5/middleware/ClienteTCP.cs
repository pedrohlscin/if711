using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Data;
using System.Net;

namespace middleware
{
    class ClienteTCP
    {
        private String mensagem;
        private const int PORT = 9099;
        private static System.Net.IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
        private static IPAddress iPAddress = ipHost.AddressList[0];
        private static IPEndPoint localEndPoint = new IPEndPoint(iPAddress, PORT);


        //public Socket Connect(IPAddress iPAddress, IPEndPoint localEndPoint)
        //{
        //    try
        //    {
        //        Socket sender = new Socket(iPAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        //        sender.Connect(localEndPoint);
        //        Console.WriteLine("Conexão Estabelecida");
        //        return sender;
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.Message);
        //    }

        //}

    }
}

