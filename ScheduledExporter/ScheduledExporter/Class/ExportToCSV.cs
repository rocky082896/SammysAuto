using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ScheduledExporter.Class
{
    class ExportToCSV
    {
        public void DataTableToCsv(DataTable dt, string table_name)
        {
            UploadToSAP upload = new UploadToSAP();
            string location = @"C:\Users\sps.roland.bitos\source\repos\TestService\TestService\bin\Debug\backup-sap";
            StringBuilder sb = new StringBuilder();

            var columnNames = dt.Columns.Cast<DataColumn>().Select(column => "\"" + column.ColumnName.Replace("\"", "\"\"") + "\"").ToArray();
            sb.AppendLine(string.Join("|", columnNames));

            foreach (DataRow row in dt.Rows)
            {
                var fields = row.ItemArray.Select(field => "\"" + field.ToString().Replace("\"", "\"\"") + "\"").ToArray();
                sb.AppendLine(string.Join("|", fields));
            }

            File.WriteAllText(location + @"\" + DateTime.Now.ToLongDateString() + table_name + ".csv", sb.ToString(), Encoding.Default);
            Thread.Sleep(3000);
            upload.UploadFileToFTP(location + @"\" + DateTime.Now.ToLongDateString() + table_name + ".csv");
        }
    }
}
