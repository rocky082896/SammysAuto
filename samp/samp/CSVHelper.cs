using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace samp
{
    class CSVHelper
    {
        public static void CSVWriter(DataTable dataTable)
        {
            StringBuilder sb = new StringBuilder();
            foreach (DataRow row in dataTable.Rows)
            {
                var fields = row.ItemArray.ToArray();
                sb.AppendLine(string.Join("|", fields));
            }
            File.WriteAllText(@"D:\SAP\S_SEND\I1xxx" + DateTime.Today.ToString("yyyy/MM/dd") + ".csv", sb.ToString(), Encoding.Default);
        }

        public static void CSVForSAP(DataTable salesOrder, DataTable salesItem)
        {
            StringBuilder stringBuilder = new StringBuilder();

            foreach (DataRow row in salesOrder.Rows)
            {
                for (int i = 0; i < salesOrder.Columns.Count; i++)
                {
                    stringBuilder.Append(row[i].ToString() + "|");
                }

                stringBuilder.Append(Environment.NewLine);
            }

            foreach (DataRow row in salesItem.Rows)
            {
                for (int i = 0; i < salesItem.Columns.Count; i++)
                {
                    stringBuilder.Append(row[i].ToString() + "|");
                }

                stringBuilder.Append(Environment.NewLine);
            }

            //File.WriteAllText(@"D:\SAP\S_Send\salesorder.csv", stringBuilder.ToString(), Encoding.Default);
            File.AppendAllText(@"D:\SAP\S_SEND\I1xxx" + DateTime.Today.ToString("yyyy/MM/dd") + ".csv", stringBuilder.ToString());
        }
    }
}
