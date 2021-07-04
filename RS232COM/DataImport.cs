using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RS232COM
{
    class DataImport
    {
        private readonly string dateNow = DateTime.Now.ToString("MM-dd-yyyy hh:mm:ss");
        private readonly string path = @"D:\Cybil\";
        private MySqlCommand cmd;
        private MySqlTransaction trans;

        private DataTable GetDataTableFromCsv(string path)
        {
            string header = "No";

            string pathOnly = Path.GetDirectoryName(path);
            string fileName = Path.GetFileName(path);

            string sql = @"SELECT F1, F2, F3, F4, F5, F6, F7, F8, F9, F10, F11 FROM [" + fileName + "] WHERE F7 NOT LIKE 0";

            using (OleDbConnection connection = new OleDbConnection(
                      @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + pathOnly +
                      ";Extended Properties=\"Text;HDR=" + header + "\"; "))
            using (OleDbCommand command = new OleDbCommand(sql, connection))
            using (OleDbDataAdapter adapter = new OleDbDataAdapter(command))
            {
                DataTable dataTable = new DataTable();
                dataTable.Locale = CultureInfo.CurrentCulture;
                adapter.Fill(dataTable);
                return dataTable;
            }
        }

        public void ImportCybilData(string path)
        {
            bool isSuccess = true;
            string exCeption = "";

            DataTable dt = GetDataTableFromCsv(path);
            string sql = "INSERT INTO tbl_cybil VALUES ";
            StringBuilder sb = new StringBuilder();
            sb.Append(sql);
            int count = 0;
            foreach (DataRow item in dt.Rows)
            {
                count += 1;
                sb.Append("(");
                sb.Append("'" + item[0].ToString() + "', '" + DateTime.Parse(item[1].ToString()).ToString("yyyy-MM-dd") + "', '" + item[2].ToString() + "', '" +
                        item[3].ToString() + "', '" + item[4].ToString() + "', '" + item[5].ToString() + "', '" + item[6].ToString() + "', '"
                        + item[7].ToString() + "', '" + item[8].ToString() + "', '" + item[9].ToString() + "', '" + item[10].ToString());

                if (count < dt.Rows.Count)
                    sb.Append("'),");
                else
                    sb.Append("')");
            }

            MySqlConnection conn = new MySqlConnection();
            if (ServerAuth.IsConnected())
                conn.ConnectionString = Global.CONN;
            else
                conn.ConnectionString = Global._CONN;

            try
            {
                conn.Open();
                cmd = new MySqlCommand(sb.ToString(), conn);
                cmd.ExecuteNonQuery();
                conn.Close();
                if (count > 0)
                {
                    isSuccess = true;
                }
            }
            catch (Exception ex)
            {
                exCeption = ex.Message;
                isSuccess = false;

            }
            finally
            {
                if (isSuccess)
                {
                    LogWriter.SetImportLogs("Date and Time : " + dateNow);
                    LogWriter.SetImportLogs("File Name     : " + Path.GetFileName(path));
                    LogWriter.SetImportLogs("Remarks       : " + count + " lines uploaded.");
                    LogWriter.SetImportLogs("Connection    : " + Global.CONN);
                    LogWriter.SetImportLogs("----------------------------------------");
                }
                else
                {
                    LogWriter.SetErrorImportLogs("Date and Time  : " + dateNow);
                    LogWriter.SetErrorImportLogs("File Name      : " + Path.GetFileName(path));
                    LogWriter.SetErrorImportLogs("Remarks        : Failed to import due to some error.");
                    LogWriter.SetErrorImportLogs("Connection     : " + Global.CONN);
                    LogWriter.SetErrorImportLogs("Error          : " + exCeption);
                    LogWriter.SetErrorImportLogs("----------------------------------------");
                }
            }
        }
    }
}
