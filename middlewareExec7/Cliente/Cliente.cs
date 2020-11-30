using middlewareExec7.Proxies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace middlewareExec7.Cliente
{
    class Cliente
    {
        private static long tempoTranscorrido = 0;
        private static List<double> tempos = new List<double>();

        static int Clientes { get; set; }

        public static void run(int qtdClients)
        {
        Clientes = qtdClients;

        Thread[] clients = new Thread[Clientes];
            for (var i = 0; i < Clientes; i++)
            {
                clients[i] = new Thread(executeClient);
                clients[i].Start(i);
            }
            for (var i = 0; i < Clientes; i++)
            {
                clients[i].Join();
            }

            Console.WriteLine("Tempo {0}", tempoTranscorrido);
            Console.WriteLine("Media: {0}", (double)tempoTranscorrido / 10000);
            Console.WriteLine("Media 2 : {0}", tempos.Average(item => item));
            Console.WriteLine("Desvio padrão: {0}", CalculateStandardDeviation(tempos));
        }

        private static void executeClient(Object obj)
        {
            LookupProxy lookupProxy = LookupProxy.GetInstance();
            ICalculadora calculadora = new CalculadoraProxy(lookupProxy.Lookup("calculadora"));
            for (int i = 0; i < 10000; i++)
            {
                var watch = new System.Diagnostics.Stopwatch();
                watch.Start();
                calculadora.Eval(i + "*2+1");
                watch.Stop();
                tempoTranscorrido += watch.ElapsedMilliseconds;
                tempos.Add(watch.ElapsedMilliseconds);
            }
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
                standardDeviation = Math.Sqrt((sum) / (values.Count() - 1));
            }
            return standardDeviation;
        }
    }
}
