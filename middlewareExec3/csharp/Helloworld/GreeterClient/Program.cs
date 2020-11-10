// Copyright 2015 gRPC authors.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Threading;
using System.Collections.Generic;
using Grpc.Core;
using Helloworld;

namespace GreeterClient
{
    class Program
    {
        public static List<long> executions = new List<long>();
        public static long amountOfTimeEllapsed = 0;
        const int CLIENTS = 1;
        public static void Main(string[] args)
        {
            for (int laco = 0; laco < 3; laco++)
            {
                Thread[] t = new Thread[CLIENTS];
                for (var i = 0; i < CLIENTS; i++)
                {
                    t[i] = new Thread(start);
                    t[i].Start(i);
                }
                for (var i = 0; i < CLIENTS; i++)
                {
                    t[i].Join();
                }
                Console.WriteLine("Total time ellapsed: {0} in milliseconds", amountOfTimeEllapsed);
                var mediaOfTimeReq = (double)amountOfTimeEllapsed / (double)10000 / (double)CLIENTS;
                Console.WriteLine("Media of time by requisition: {0} in milliseconds", mediaOfTimeReq);
                double deviation = 0;
                foreach (var i in executions)
                {
                    deviation += Math.Pow((i - mediaOfTimeReq), 2);
                }
                deviation /= (double)10000 / (double)CLIENTS;
                deviation = Math.Sqrt(deviation);
                Console.WriteLine("Deviation: {0}", deviation);
            }
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        public static void start(Object obj)
        {
            Channel channel = new Channel("127.0.0.1:30051", ChannelCredentials.Insecure);

            var client = new Greeter.GreeterClient(channel);
            for (int i = 0; i < 10000; i++)
            {
                var watch = System.Diagnostics.Stopwatch.StartNew();
                String user = i.ToString() + "*2+1";

                var reply = client.SayHello(new HelloRequest { Name = user });
                watch.Stop();
                //Console.WriteLine("Greeting: " + reply.Message);
                if ((int)obj == 0)
                {
                    executions.Add(watch.ElapsedMilliseconds);
                    amountOfTimeEllapsed += watch.ElapsedMilliseconds;
                }
            }

            channel.ShutdownAsync().Wait();
            //Console.WriteLine("Press any key to exit...");
            //Console.ReadKey();
        }
    }
}
