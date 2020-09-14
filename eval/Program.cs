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
            Thread server = new Thread(executeServer);
            server.Start();
            for(var i = 0; i < 10; i++){
                Thread client = new Thread(executeClient);
                client.Start(i.ToString() + "+2");
            }
        }

        private static void executeClient(Object obj)
        {
            var ipHost = Dns.GetHostEntry(Dns.GetHostName());
            var iPAddress = ipHost.AddressList[0];
            var localEndPoint = new IPEndPoint(iPAddress, 9099);
            // criando socket de conexão
            Socket sender = new Socket(iPAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            try{
                Console.WriteLine("Começando a transferir!");
                sender.Connect(localEndPoint);
                var bytesToTransfer = Encoding.ASCII.GetBytes(obj.ToString());
                sender.Send(bytesToTransfer);

                var msgReceived = new byte[1024];
                var bytesReceived = sender.Receive(msgReceived);
                Console.WriteLine("{0}", Encoding.ASCII.GetString(msgReceived, 0, bytesReceived));
                sender.Shutdown(SocketShutdown.Both);
                sender.Close();
            }catch(Exception e){
                Console.WriteLine(e.Message);
            }
        }

        private static void executeServer()
        {
            IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = ipHost.AddressList[0];
            IPEndPoint localEntryPoint = new IPEndPoint(ipAddress, 9099);
            Socket listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            try{
                //associar servidor ao ip
                listener.Bind(localEntryPoint);
                listener.Listen(10);
                while(true){
                    Console.WriteLine("Aguardando conexões!");
                    //esperar conexões, a thread dorme enquanto aguarda
                    Socket clientSocket = listener.Accept();
                    var t = new Thread(handleClient);
                    t.Start(clientSocket);
                }
            }catch(Exception e){
                Console.WriteLine(e.Message);
            }
        }

        private static void handleClient(object obj)
        {
            byte[] bytes = new Byte[1024]; 
            string data = null; 
            var client = (Socket)obj;
            var numBytes = client.Receive(bytes);
            data = Encoding.ASCII.GetString(bytes, 0, numBytes);
            Console.WriteLine(data);
        }
    }
}
