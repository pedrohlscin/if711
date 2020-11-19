using System;
using System.Collections.Generic;
using System.Text;

namespace middlewareExec6
{
    class Requestor
    {
        private string saida;

        public String Invoke(AOR aor, String operation, String param)
        {
            CRH crh = new CRH(aor.Port, aor.Host);
            saida = Marshall.Marshaller.Unmarshall(crh.sendReceive(Marshall.Marshaller.Marshall(param)));
            return saida;
        }
    }
}
