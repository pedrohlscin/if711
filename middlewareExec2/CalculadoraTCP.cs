using System;
using System.Net;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.Data;
using System.Collections.Generic;
using System.Linq;

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
        private static long tempoTranscorrido;
        private static List<double> tempos = new List<double>();

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
            Console.WriteLine("Tempo {0}", tempoTranscorrido);
            Console.WriteLine("Media: {0}", (double)tempoTranscorrido/(double)qtdIteracoes);
            Console.WriteLine("Media 2 : {0}", tempos.Average(item => item));
            Console.WriteLine("Desvio padrão: {0}", CalculateStandardDeviation(tempos));
        }

        private static double CalculateStandardDeviation(IEnumerable<double> values)
        {   
            double standardDeviation = 0;

            if (values.Any()) 
            {      
                // Compute the average.     
                double avg = values.Average();

                // Perform the Sum of (value-avg)_2_2.      
                double sum = values.Sum(d => Math.Pow(d - avg, 2));

                // Put it all together.      
                standardDeviation = Math.Sqrt((sum) / (values.Count()-1));   
            }  

            return standardDeviation;
        }

        private static void executeClient(Object obj)
        {
            var contadorThread = 0;
            Socket sender = new Socket(iPAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            sender.Connect(localEndPoint);
            for(var i = 0; i < qtdIteracoes; i++){
                var watch = new System.Diagnostics.Stopwatch();
                watch.Start();
                if(i % 1000 == 0)
                {
                    Console.WriteLine("Current Iteration: " + i);
                }
                // criando socket de conexão
                try{
                    var texto = i.ToString() + "+2*2";
                    var bytesToTransfer = Encoding.ASCII.GetBytes(texto);
                    // Console.WriteLine("Cliente enviando: {0}", texto.ToString() );
                    sender.Send(bytesToTransfer);

                    byte[] bytes = new Byte[sender.Available]; 
                    int bytesReceived = sender.Receive(bytes);
                    // Console.WriteLine("Cliente recebeu: {0}",Encoding.ASCII.GetString(bytes));
                }catch(Exception e){
                    //Console.WriteLine(e.Message);
                }finally
                {
                    watch.Stop();
                }
                if((int)obj == 0)
                {
                    contadorThread++;
                    tempoTranscorrido += watch.ElapsedMilliseconds;
                    tempos.Add(watch.ElapsedMilliseconds);
                }
            }
            sender.Shutdown(SocketShutdown.Both);
            sender.Close();
            // Console.WriteLine("Quantidade de execuções p thread {0}", contadorThread);
        }

        private static void executeServer()
        {
            Socket listener = new Socket(iPAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            try{
                //associar servidor ao ip
                listener.Bind(localEndPoint);
                // tamanho máximo da queue de espera
                listener.Listen(2147483647);
                Socket clientSocket = listener.Accept();
                while(clientSocket.Connected){
                    byte[] bytes = new Byte[1024];
                    string data = null;
                    // Console.WriteLine("Servidor recebeu: {0}",data);
                    try
                    {
                        var numBytes = clientSocket.Receive(bytes);
                        data = Encoding.ASCII.GetString(bytes, 0, numBytes);
                        var retorno = calculaCoisa(data);
                        // Console.WriteLine("Servidor devolvendo: {0}",retorno);
                        var bytesToTransfer = Encoding.ASCII.GetBytes(retorno);
                        clientSocket.Send(bytesToTransfer);
                    }
                    catch (SocketException e)
                    {
                    }
                }
            }catch(Exception e){
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