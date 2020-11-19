using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using middlewareExec6.Proxies;

namespace middlewareExec6
{
    class SRH
    {
        private const int PORT = 9099;
        private static IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
        private static IPAddress iPAddress = ipHost.AddressList[0];
        private static IPEndPoint localEndPoint = new IPEndPoint(iPAddress, PORT);
        private AOR aor = new AOR(iPAddress.ToString(), PORT);

        public SRH()
        {
            LookupProxy proxy = LookupProxy.GetInstance();
            proxy.Register("calculadora", aor);
        }
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
                //usar o invoker
                //CalculadorInvoker calculadorInvoker = new CalculadorInvoker();
                //var retorno = calculadorInvoker.Invoke(bytes);
                //retornar o resultador
                //client.Send(retorno);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
