using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace samp
{
    class DatabaseManager
    {
        private static readonly string server = "localhost";
        private static readonly string database = "ajinomotoslss_db";
        private static readonly string uid = "apcslss";
        private static readonly string password = "slss@inst2018";

        //private static readonly string server = "localhost";
        //private static readonly string database = "db_ajinomoto";
        //private static readonly string uid = "root";
        //private static readonly string password = "";


        public static MySqlConnection conn = new MySqlConnection();
        public static MySqlCommand cmd;
        public static MySqlDataReader dr;
        public static MySqlDataAdapter da;

        public static void GetDatabase()
        {
            conn.Close();
            conn.Dispose();
            if (conn.State == ConnectionState.Closed)
            {
                conn.ConnectionString = string.Format("SERVER = {0}; DATABASE = {1}; UID = {2}; PASSWORD = {3}", server, database, uid, password);
                conn.Open();
            }
        }

        public static void ReadData(string query)
        {
            GetDatabase();
            cmd = new MySqlCommand(query, conn);
            dr = cmd.ExecuteReader();
        }

        public static DataTable ReadAdapter(string query)
        {
            DataTable dt = new DataTable();
            GetDatabase();
            da = new MySqlDataAdapter(query, conn);
            da.Fill(dt);
            return dt;
        }

        public static void ReadDataAdapter(string query)
        {
            GetDatabase();
            cmd = new MySqlCommand(query, conn);
            da = new MySqlDataAdapter(cmd);
        }

        public static void Execute(string query)
        {
            GetDatabase();
            cmd = new MySqlCommand(query, conn);
            cmd.ExecuteNonQuery();
        }
    }
}
