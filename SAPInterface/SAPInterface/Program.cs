using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace SAPInterface
{
    class Program
    {
        static void Main(string[] args)
        {
        }

        public static void UploadHeaderData()
        {
            try
            {
                string table = "salesorder_h";
                string server = "localhost";
                string database = "aji_dbtest";
                string uid = "root";
                string password = "";

                //string server = "localhost";
                //string database = "sap1";
                //string uid = "APCSLSS";
                //string password = "SLSS@inst2018";

                string connString = string.Format("SERVER = {0}; DATABASE = {1}; UID = {2}; PASSWORD = {3}", server, database, uid, password);

                MySqlConnection conn = new MySqlConnection(connString);

                MySqlBulkLoader bulkLoader = new MySqlBulkLoader(conn)
                {
                    TableName = table,
                    FieldTerminator = "|",
                    LineTerminator = "\n",
                    FileName = @"C:\Users\sps.roland.bitos\Desktop\SAMPLE PATH" + table + ".csv"
                };
                int count = bulkLoader.Load();

            }
            catch (MySqlException ex)
            {
            }
        }
    }
}
