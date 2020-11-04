using System;
using System.Collections.Generic;
using System.Text;

namespace middleware.UDP
{
    class ProgramaClienteUDP
    {
        public void Main()
        {
            for(var i = 0; i < 10000; i++)
            {
                var clientRequestHandler = new ClientUDP();
                byte[] envio = System.Text.ASCIIEncoding.ASCII.GetBytes(i + "*2+1");
                var mensagem = clientRequestHandler.sendReceive(envio);
                //Console.WriteLine(System.Text.ASCIIEncoding.ASCII.GetString(mensagem));
            }
        }
    }
}
