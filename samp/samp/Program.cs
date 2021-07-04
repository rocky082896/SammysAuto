using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace samp
{
    class Program
    {
        static void Main(string[] args)
        {
            //BulkToMySQL("salesorder_h", @"D:\HT\R_Receive\salesorder_h.csv");
            //BulkToMySQL("salesorder_item", @"D:\HT\R_Receive\salesorder_item.csv");
            //Console.ReadKey();
            Export();
        }

        //private static readonly string server = "localhost";
        //private static readonly string database = "ajinomotoslss_db";
        //private static readonly string uid = "apcslss";
        //private static readonly string password = "slss@inst2018";

        private static readonly string server = "localhost";
        private static readonly string database = "aji_dbtest";
        private static readonly string uid = "root";
        private static readonly string password = "";

        public static void BulkToMySQL(String tables, String path)
        {
            string connStr = string.Format("server={0}; user id={1};password={2};database={3}", server, uid, password, database);
            // MySql Connection Object
            MySqlConnection conn = new MySqlConnection(connStr);

            //  csv file path
            string file = path;

            // MySQL BulkLoader
            MySqlBulkLoader bl = new MySqlBulkLoader(conn);
            bl.TableName = tables;
            bl.FieldTerminator = "|"; 
            bl.LineTerminator = "\n";
            bl.FileName = file;

            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
            
                // Upload data from file
                int count = bl.Load();
                Console.WriteLine(count + " lines uploaded.");

                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private static void ExportToCSV(string ornum)
        {
            try
            {
                string selectSalesOrder = string.Format("SELECT  header, doc_type, org, distrib_chan, division, po_no, po_date, delivery_date," +
                    " pricing_date, sold_to, ship_to, bill_to, bill_to, bill_to FROM salesorder_h WHERE or_num = '{0}'", ornum);

                string selectSalesItem = string.Format("SELECT item_index, item_no, material_code, or_qty, uom, item_cat, plant, cond_1, cond_1s, " +
                    "cond_2, cond_2s, cond_3, cond_3s, cond_4, cond_4s, cond_5, cond_5s FROM salesorder_item WHERE salesorder_h_or_num = '{0}'", ornum);
                
                CSVHelper.CSVForSAP(DatabaseManager.ReadAdapter(selectSalesOrder), DatabaseManager.ReadAdapter(selectSalesItem));
            }
            catch (Exception ex)
            {
                //Path.SetSAPSendError(ex.Message, ServiceType, "Sales Order");
            }
        }

        public static void Export()
        {
            List<string> List = new List<string>();

            string message = "Sales order successfully exported.";

            string query = "SELECT or_num FROM salesorder_h";
            DatabaseManager.ReadData(query);
            while (DatabaseManager.dr.Read())
            {
                List.Add(DatabaseManager.dr[0].ToString());
            }

            foreach (string str in List)
            {
                ExportToCSV(str);
                //Path.SetSAPLogsMessage(message, ServiceType, "Sales Order");
            }
        }
    }
}
