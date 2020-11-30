using System;
using System.Collections.Generic;

namespace middlewareExec7
{
    class Program
    {
        static void Main(string[] args)
        {
            Pool p = Pool.GetInstance();
            Cliente.Cliente.run(1);
        }
    }
}
