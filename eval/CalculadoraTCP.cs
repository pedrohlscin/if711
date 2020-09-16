using System;
using System.Net;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.Data;

namespace eval
{
    class CalculadoraTCP
    {
        private const int PORT = 9099;
        private static IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
        private static IPAddress iPAddress = ipHost.AddressList[0];
        private static IPEndPoint localEndPoint = new IPEndPoint(iPAddress, PORT);

        public static void runCalculadoraTcp()
        {
            Thread server = new Thread(executeServer);
            server.Start();
            for(var i = 0; i < 1000; i++){
                Thread client = new Thread(executeClient);
                // client.Start(i.ToString() + "+2");
                client.Start(i.ToString() + "2*2");
            }
        }

        private static void executeClient(Object obj)
        {
            // criando socket de conexão
            Socket sender = new Socket(iPAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            try{
                sender.Connect(localEndPoint);
                var bytesToTransfer = Encoding.ASCII.GetBytes(obj.ToString());
                Console.WriteLine("Cliente enviando: {0}", obj.ToString() );
                sender.Send(bytesToTransfer);

                byte[] bytes = new Byte[1024]; 
                int bytesReceived = sender.Receive(bytes);
                Console.WriteLine("Cliente recebeu: {0}",Encoding.ASCII.GetString(bytes));
                sender.Shutdown(SocketShutdown.Both);
                sender.Close();
            }catch(Exception e){
                Console.WriteLine(e.Message);
            }
        }

        private static void executeServer()
        {
            Socket listener = new Socket(iPAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            try{
                //associar servidor ao ip
                listener.Bind(localEndPoint);
                // tamanho máximo da queue de espera
                listener.Listen(50);
                while(true){
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
            Console.WriteLine("Servidor recebeu: {0}",data);
            try{
                var retorno = calculaCoisa(data);
                Console.WriteLine("Servidor devolvendo: {0}",retorno);
                var bytesToTransfer = Encoding.ASCII.GetBytes(retorno);
                client.Send(bytesToTransfer);
            }catch(Exception e){
                Console.WriteLine(e.Message);
            }
        }

        private static string calculaCoisa(string texto)
        {
            var loDataTable = new DataTable();
            var loDataColumn = new DataColumn("Eval", typeof (double), texto);
            loDataTable.Columns.Add(loDataColumn);
            loDataTable.Rows.Add(0);
            return loDataTable.Rows[0]["Eval"].ToString();
        }
    }
}