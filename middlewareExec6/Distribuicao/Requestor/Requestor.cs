using System;
using System.Collections.Generic;
using System.Text;

namespace middlewareExec6
{
    class Requestor
    {

        public static String Invoke(AOR aor, String operation, String param)
        {
            string saida;
            CRH crh = new CRH(aor.Port, aor.Host);
            saida = Marshall.Marshaller.Unmarshall(crh.sendReceive(Marshall.Marshaller.Marshall(param)));
            return saida;
        }
    }
}
