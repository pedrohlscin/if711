using System;
using System.Net;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.Data;
using System.Collections.Generic;

namespace eval
{
    class CalculadoraTCP
    {
        private const int PORT = 9099;
        private static IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
        private static IPAddress iPAddress = ipHost.AddressList[0];
        private static IPEndPoint localEndPoint = new IPEndPoint(iPAddress, PORT);
        private static int qtdClients = 1;
        private static int qtdIteracoes = 10000;

        public static long amountOfTimeEllapsed = 0;
        public static List<long> executions = new List<long>();

        public static void runCalculadoraTcp()
        {
            Thread[] clients = new Thread[qtdClients];
            Thread server = new Thread(executeServer);
            server.Start();
            for(var i = 0; i < qtdClients; i++){
                clients[i] = new Thread(executeClient);
                clients[i].Start(i);
            }
            for(var i = 0; i < qtdClients; i++){
                clients[i].Join();
            }
            Console.WriteLine("Total time ellapsed: {0} in milliseconds", amountOfTimeEllapsed);
            var mediaOfTimeReq = (double)amountOfTimeEllapsed/(double)10000/(double)qtdClients;
            Console.WriteLine("Media of time by requisition: {0} in milliseconds", mediaOfTimeReq);
            double deviation = 0;
            foreach(var i in executions){
                deviation += Math.Pow((i - mediaOfTimeReq),2);
            }
            deviation /= (double)10000/(double)qtdClients;
            deviation = Math.Sqrt(deviation);
            Console.WriteLine("Deviation: {0}", deviation);
        }

        private static void executeClient(Object obj)
        {
            for(var i = 0; i < qtdIteracoes; i++){
                var watch = System.Diagnostics.Stopwatch.StartNew();
                // criando socket de conexão
                Socket sender = new Socket(iPAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                try{
                    sender.Connect(localEndPoint);
                    var texto = i.ToString() + "+2*2";
                    var bytesToTransfer = Encoding.ASCII.GetBytes(texto);
                    // Console.WriteLine("Cliente enviando: {0}", texto.ToString() );
                    sender.Send(bytesToTransfer);

                    byte[] bytes = new Byte[1024]; 
                    int bytesReceived = sender.Receive(bytes);
                    // Console.WriteLine("Cliente recebeu: {0}",Encoding.ASCII.GetString(bytes));
                }catch(Exception e){
                    Console.WriteLine(e.Message);
                }finally
                {
                    sender.Shutdown(SocketShutdown.Both);
                    sender.Close();
                    watch.Stop();
                }
                if((int)i == 0){
                    executions.Add(watch.ElapsedMilliseconds);
                    amountOfTimeEllapsed += watch.ElapsedMilliseconds;
                }
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
            // Console.WriteLine("Servidor recebeu: {0}",data);
            try{
                var retorno = calculaCoisa(data);
                // Console.WriteLine("Servidor devolvendo: {0}",retorno);
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