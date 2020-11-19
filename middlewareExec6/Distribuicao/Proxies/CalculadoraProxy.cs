using System;
using System.Collections.Generic;
using System.Text;

namespace middlewareExec6.Proxies
{
    class CalculadoraProxy : ICalculadora
    {
        public AOR aorServer { get; set; }
        public CalculadoraProxy(AOR aor) { aorServer = aor }
        public int Eval(string text)
        {
            request = Re
            throw new NotImplementedException();
        }
    }
}
