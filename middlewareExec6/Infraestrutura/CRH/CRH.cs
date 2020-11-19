using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace middlewareExec6
{
    class CRH
    {
        private int port;
        private IPAddress iPAddress;
        private IPEndPoint localEndPoint;

        public CRH(int porta, String enderecoIp)
        {
            port = porta;
            iPAddress = IPAddress.Parse(enderecoIp);
            localEndPoint = new IPEndPoint(iPAddress, port);
        }

        public byte[] sendReceive(byte[] envio)
        {
            // criando socket de conexão
            Socket sender = new Socket(iPAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                sender.Connect(localEndPoint);
                // Console.WriteLine("Cliente enviando: {0}", texto.ToString() );
                sender.Send(envio);

                byte[] bytes = new Byte[1024];
                int bytesReceived = sender.Receive(bytes);
                // Console.WriteLine("Cliente recebeu: {0}",Encoding.ASCII.GetString(bytes));
                return bytes;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                sender.Shutdown(SocketShutdown.Both);
                sender.Close();
            }

            return null;
        }
    }
}
