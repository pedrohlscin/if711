using System;
using System.Collections.Generic;
using System.Text;

namespace middlewareExec6
{
    class AOR
    {
        public AOR(String host, int porta) { Host = host; Port = porta; }
        public String Host { get; set; }
        public int Port { get; set; }
    }
}
