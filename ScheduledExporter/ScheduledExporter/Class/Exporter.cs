using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace ScheduledExporter.Class
{
    class Exporter
    {
        public DataTable GetConsolidatedData(string table)
        {
            
            DataTable dt = new DataTable();
            var today = DateTime.Now.ToShortDateString();
            var yesterday = DateTime.Parse(today).AddDays(-1).ToShortDateString();
            Global g = new Global();
            string sql = "SELECT * FROM " + table + " WHERE upload_date =@date";
            MySqlConnection con = new MySqlConnection(g.constring);
            MySqlDataAdapter da;
            MySqlCommand cmd = new MySqlCommand(sql, con);
            con.Open();
            cmd.Parameters.AddWithValue("@date", yesterday);
            da = new MySqlDataAdapter(cmd);
            da.Fill(dt);
            con.Close();
            return dt;
        }

        public void TableLooper()
        {
            ExportToCSV export = new ExportToCSV();
            string[] tablenames = { "invoice_listing", "dummy" };
            foreach(string table in tablenames)
            {
                export.DataTableToCsv(GetConsolidatedData(table), table);
            }
        }
    }
}
