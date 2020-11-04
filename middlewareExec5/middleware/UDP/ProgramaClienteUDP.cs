using System;
using System.Collections.Generic;
using System.Text;

namespace middleware.UDP
{
    class ProgramaClienteUDP
    {
        public void Main()
        {
            var clientRequestHandler = new ClientUDP();
            byte[] envio = System.Text.ASCIIEncoding.ASCII.GetBytes("1*2+3");
            var mensagem = clientRequestHandler.sendReceive(envio);
            Console.WriteLine(System.Text.ASCIIEncoding.ASCII.GetString(mensagem));
        }
    }
}
