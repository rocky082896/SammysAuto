using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace SampleMasterDataGenerator
{
    class ExporterClass
    {
        // UOM CONVERSION MASTER TO BE ADDED HERE

        public static DataTable InitializeStorageLoc()
        {
            DataTable dt = new DataTable();
            String sql = "SELECT * FROM storage_location";
            MySqlConnection con = new MySqlConnection(global.constring);
            MySqlDataAdapter da = new MySqlDataAdapter(sql, con);
            da.Fill(dt);
            return dt;
        }
        public static DataTable InitializeMachineMaster()
        {
            DataTable dt = new DataTable();
            String sql = "SELECT * FROM machine_master";
            MySqlConnection con = new MySqlConnection(global.constring);
            MySqlDataAdapter da = new MySqlDataAdapter(sql, con);
            da.Fill(dt);
            return dt;
        }

        public static DataTable InitializeConversion()
        {
            DataTable dt = new DataTable();
            String sql = "SELECT * FROM conversion_master";
            MySqlConnection con = new MySqlConnection(global.constring);
            MySqlDataAdapter da = new MySqlDataAdapter(sql, con);
            da.Fill(dt);
            return dt;
        }

        public static DataTable InitializeSalesman()
        {
            DataTable dt = new DataTable();
            String sql = "SELECT * FROM salesman";
            MySqlConnection con = new MySqlConnection(global.constring);
            MySqlDataAdapter da = new MySqlDataAdapter(sql, con);
            da.Fill(dt);
            return dt;
        }
        public static DataTable InitializeDiscountMaster()
        {
            DataTable dt = new DataTable();
            String sql = "SELECT * FROM discount_master";
            MySqlConnection con = new MySqlConnection(global.constring);
            MySqlDataAdapter da = new MySqlDataAdapter(sql, con);
            da.Fill(dt);
            return dt;
        }
        public static DataTable InitializeDetails()
        {
            DataTable dt = new DataTable();
            String sql = "SELECT * FROM customer_details";
            MySqlConnection con = new MySqlConnection(global.constring);
            MySqlDataAdapter da = new MySqlDataAdapter(sql, con);
            da.Fill(dt);
            return dt;
        }
        public static DataTable InitializeCustomer()
        {
            DataTable dt = new DataTable();
            String sql = "SELECT * FROM customer";
            MySqlConnection con = new MySqlConnection(global.constring);
            MySqlDataAdapter da = new MySqlDataAdapter(sql, con);
            da.Fill(dt);
            return dt;
        }
        public static DataTable InitializeDiscountList()
        {
            DataTable dt = new DataTable();
            String sql = "SELECT * FROM discount_list";
            MySqlConnection con = new MySqlConnection(global.constring);
            MySqlDataAdapter da = new MySqlDataAdapter(sql, con);
            da.Fill(dt);
            return dt;
        }
        public static DataTable InitializeBrand()
        {
            DataTable dt = new DataTable();
            String sql = "SELECT * FROM brand";
            MySqlConnection con = new MySqlConnection(global.constring);
            MySqlDataAdapter da = new MySqlDataAdapter(sql, con);
            da.Fill(dt);
            return dt;
        }
        public static DataTable InitializeMaterial()
        {
            DataTable dt = new DataTable();
            String sql = "SELECT * FROM material";
            MySqlConnection con = new MySqlConnection(global.constring);
            MySqlDataAdapter da = new MySqlDataAdapter(sql, con);
            da.Fill(dt);
            return dt;
        }
        public static DataTable InitializePriceList()
        {
            DataTable dt = new DataTable();
            String sql = "SELECT * FROM pricelissst";
            MySqlConnection con = new MySqlConnection(global.constring);
            MySqlDataAdapter da = new MySqlDataAdapter(sql, con);
            da.Fill(dt);
            return dt;
        }

    }
}
