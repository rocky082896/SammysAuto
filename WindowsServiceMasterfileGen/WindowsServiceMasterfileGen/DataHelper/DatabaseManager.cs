using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace WindowsServiceMasterfileGen.DataHelper
{
    class DatabaseManager
    {
        private static readonly string server = "localhost";
        private static readonly string database = "sap1"; //"ajinomotoslss_db";
        private static readonly string uid = "APCSLSS";//"apcslss";
        private static readonly string password = "SLSS@inst2018"; //"slss@inst2018"; 

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
                conn.ConnectionString = string.Format("SERVER = {0}; DATABASE = {1}; UID = {2}; PASSWORD = {3}; " +
                    "default command timeout=1000;", server, database, uid, password);
                conn.Open();
            }
        }

        public static void ReadData(string query)
        {
            GetDatabase();
            cmd = new MySqlCommand(query, conn);
            dr = cmd.ExecuteReader();
        }

        public static void CloseData()
        {
            dr.Close();
        }

        public static DataTable ReadAdapter(string query)
        {
            DataTable dt = new DataTable();
            GetDatabase();
            da = new MySqlDataAdapter(query, conn);
            da.SelectCommand.CommandTimeout = 2000;
            //for (int i = dt.Rows.Count - 1; i >= 0; i--)
            //{
            //    if (dt.Rows[i][1] == System.DBNull.Value)
            //        dt.Rows[i].Delete();
            //}
            //dt.AcceptChanges();
            da.Fill(dt);
            return dt;
        }

        public static DataTable ReadAdapter(string query, string salesman)
        {
            cmd = new MySqlCommand();
            da = new MySqlDataAdapter();
            DataTable dt = new DataTable();
            GetDatabase();

            cmd = new MySqlCommand("SEARCH_CUSTOMER", conn);
            cmd.Parameters.Add(new MySqlParameter("@salesman", salesman));
            cmd.CommandType = CommandType.StoredProcedure;
            da.SelectCommand = cmd;
            da.SelectCommand.CommandTimeout = 2000;
            //for (int i = dt.Rows.Count - 1; i >= 0; i--)
            //{
            //    if (dt.Rows[i][1] == System.DBNull.Value)
            //        dt.Rows[i].Delete();
            //}
            //dt.AcceptChanges();
            da.Fill(dt);
            return dt;
        }

        public static DataTable ReadAdapterCustomer(string query)
        {
            string[] stations = { "A100", "5001", "1400", "2100", "1600", "3500", "3400", "5400", "AK00" };
            DataTable dt = new DataTable();
            foreach(string s in stations)
            {
                GetDatabase();
                using (MySqlCommand cmd = new MySqlCommand("SEARCH_CUSTOMER", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@station", SqlDbType.VarChar);
                    da = new MySqlDataAdapter(query, conn);
                    cmd.ExecuteNonQuery();
                }
                
                da.SelectCommand.CommandTimeout = 180;
                for (int i = dt.Rows.Count - 1; i >= 0; i--)
                {
                    if (dt.Rows[i][1] == System.DBNull.Value)
                        dt.Rows[i].Delete();
                }
                dt.AcceptChanges();
                da.Fill(dt);
            }
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
