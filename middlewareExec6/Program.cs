using System;

namespace middlewareExec6
{
    class Program
    {
        static void Main(string[] args)
        {
            Server.Server s = new Server.Server();
            Cliente.Cliente.run(10);
        }
    }
}
