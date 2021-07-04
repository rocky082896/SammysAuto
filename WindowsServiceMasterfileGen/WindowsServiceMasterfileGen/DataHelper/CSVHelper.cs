using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsServiceMasterfileGen.DataHelper
{
    class CSVHelper
    {
        public static void CSVWriter(DataTable dataTable, string tablename)
        {
            StringBuilder sb = new StringBuilder();
            foreach (DataRow row in dataTable.Rows)
            {
                var fields = row.ItemArray.ToArray();
                sb.AppendLine(string.Join("|", fields));
            }
            //SAP_S_EXECUTION
            File.WriteAllText(Path.SAP_S_SEND + tablename + ".csv", sb.ToString(), Encoding.Default);
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
            File.AppendAllText(Path.SAP_S_SEND + "I1" + DateTime.Today.ToString("yyyymmdd") + ".csv", stringBuilder.ToString());
        }
    }
}