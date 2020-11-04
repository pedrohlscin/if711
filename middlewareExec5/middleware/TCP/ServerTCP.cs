using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace middleware.TCP
{
    class ServerTCP
    {
        private const int PORT = 9099;
        private static IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
        private static IPAddress iPAddress = ipHost.AddressList[0];
        private static IPEndPoint localEndPoint = new IPEndPoint(iPAddress, PORT);
        public void ReceiveSend()
        {
            Socket listener = new Socket(iPAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                //associar servidor ao ip
                listener.Bind(localEndPoint);
                // tamanho máximo da queue de espera
                listener.Listen(50);
                while (true)
                {
                    //esperar conexões, a thread dorme enquanto aguarda
                    Socket clientSocket = listener.Accept();
                    var t = new Thread(handleReq);
                    t.Start(clientSocket);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        private static void handleReq(object obj)
        {
            byte[] bytes = new Byte[1024];
            var client = (Socket)obj;
            var numBytes = client.Receive(bytes);
            //data = Encoding.ASCII.GetString(bytes, 0, numBytes);
            // Console.WriteLine("Servidor recebeu: {0}",data);
            try
            {
                ProgramaServidorTCP serverTCP = new ProgramaServidorTCP();
                var retorno = serverTCP.calculaCoisa(bytes);
                // Console.WriteLine("Servidor devolvendo: {0}",retorno);
                client.Send(retorno);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
