using System;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Data;
using System.Collections.Generic;
using System.Text;

namespace eval
{
    public class CalculadoraUDP
    {
        private const int listenPort = 9098;
        private const int qtdClients = 1;
        private const int qtdRequisicoes = 10000 ;
        public static long amountOfTimeEllapsed = 0;
        public static List<long> executions = new List<long>();

        public static void runCalculadoraUdp(){
            Thread[] clients = new Thread[qtdClients];
            Thread serverUdp = new Thread(executeServerUdp);
            serverUdp.Start();
            for(var i = 0; i < qtdClients; i++){
                clients[i] = new Thread(executeClientUdp);
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

        private static void executeServerUdp()
        {
            UdpClient udpServer = new UdpClient(listenPort);

            while (true)
            {
                var remoteEP = new IPEndPoint(IPAddress.Any, listenPort); 
                var data = udpServer.Receive(ref remoteEP); // listen on port listenPort
                // Console.WriteLine("server receive data " + System.Text.Encoding.ASCII.GetString(data));
                var retorno = calculaCoisa(System.Text.Encoding.ASCII.GetString(data));
                udpServer.Send(System.Text.Encoding.ASCII.GetBytes(retorno), System.Text.Encoding.ASCII.GetBytes(retorno).Length, remoteEP); // reply back
            }
        }

        private static void executeClientUdp(Object obj)
        {
            var client = new UdpClient();
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), listenPort); // endpoint where server is listening
            client.Connect(ep);
            for(var j = 0; j < qtdRequisicoes; j++){
                var watch = System.Diagnostics.Stopwatch.StartNew();
                // send data
                client.Send(System.Text.Encoding.ASCII.GetBytes(j.ToString() + "+1*3"), System.Text.Encoding.ASCII.GetBytes(j.ToString() + "+1*3").Length);

                // then receive data
                var receivedData = client.Receive(ref ep);
                watch.Stop();
                // Console.WriteLine("client receive data " + System.Text.Encoding.ASCII.GetString(receivedData, 0, receivedData.Length));
                if((int)obj == 0){
                    executions.Add(watch.ElapsedMilliseconds);
                    amountOfTimeEllapsed += watch.ElapsedMilliseconds;
                }
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