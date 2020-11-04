using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace middleware.UDP
{
    class ProgramaServidorUDP
    {
        public byte[] calculaCoisa(byte[] input)
        {
            String texto = System.Text.ASCIIEncoding.ASCII.GetString(input);
            var loDataTable = new DataTable();
            var loDataColumn = new DataColumn("Eval", typeof(double), texto);
            loDataTable.Columns.Add(loDataColumn);
            loDataTable.Rows.Add(0);
            return System.Text.Encoding.ASCII.GetBytes(loDataTable.Rows[0]["Eval"].ToString());
        }
    }
}
