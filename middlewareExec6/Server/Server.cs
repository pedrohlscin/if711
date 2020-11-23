using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading;

namespace middlewareExec6.Server
{
    class Server
    {
        public Server()
        {
            Console.WriteLine("Server inicializando");
            SRH srh = new SRH();
            Thread t = new Thread(srh.ReceiveSend);
            t.Start();
        }

        public static string calculaCoisa(string texto)
        {
            var loDataTable = new DataTable();
            var loDataColumn = new DataColumn("Eval", typeof(double), texto);
            loDataTable.Columns.Add(loDataColumn);
            loDataTable.Rows.Add(0);
            return loDataTable.Rows[0]["Eval"].ToString();
        }
    }
}
