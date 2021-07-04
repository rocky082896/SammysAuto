using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;


namespace WindowsServiceMasterfileGen.DataHelper
{
    class Ajinomoto
    {
        private static readonly string server = "localhost";
        private static readonly string database = "ajinomotoslss_db";
        private static readonly string uid = "APCSLSS";
        private static readonly string password = "SLSS@inst2018";


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
            DataTable dataTable = new DataTable();
            GetDatabase();
            da = new MySqlDataAdapter(query, conn);
            da.Fill(dataTable);
            return dataTable;
        }

        public static void Execute(string query)
        {
            GetDatabase();
            MySqlTransaction myTrans;
            myTrans = conn.BeginTransaction();
            cmd = new MySqlCommand(query, conn)
            {
                Connection = conn,
                Transaction = myTrans,
                CommandText = query
            };
            cmd.ExecuteNonQuery();
            myTrans.Commit();
        }
    }
}
