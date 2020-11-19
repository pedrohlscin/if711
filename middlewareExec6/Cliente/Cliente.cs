using middlewareExec6.Proxies;
using System;
using System.Collections.Generic;
using System.Text;

namespace middlewareExec6.Cliente
{
    class Cliente
    {
        public static void run()
        {
            LookupProxy lookupProxy = LookupProxy.GetInstance();
            CalculadoraProxy calculadora = new CalculadoraProxy(lookupProxy.Lookup("Calculadora"));
            Console.WriteLine(calculadora.Eval("2*3"));
        }
    }
}
