using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace SampleMasterDataGenerator
{
    class global
    {
        /*static MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder()
        {
            Server = "10.235.23.50",
            UserID = "APCSLSS",
            Password = "SLSS@inst2018",
            Database = "ajinomotoslss_db"
        };

        public static string constring = builder.ToString(); */
        // "SERVER=10.235.23.50; username=APCSLSS; password=SLSS@inst2018; database=ajinomotoslss_db;";
        public static string constring = "SERVER=localhost; username=root; password=; database=aji_dbtest;";
    }
}
