using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace WindowsServiceMasterfileGen.DataHelper
{
    class STM
    {
        private static readonly string server = "localhost";
        private static readonly string database = "sap1";
        private static readonly string uid = "APCSLSS";//"apcslss";
        private static readonly string password = "SLSS@inst2018"; //"slss@inst2018"; 

        private static readonly List<string> Tables = new List<string>() {
            "ZR70OH103", "ZR70OH104", "ZR70OH106", "ZR70OH107", "ZR70OH108", "ZR70OH135",
            "ZR70OH142", "ZR70OH148", "ZR70OH163", "ZR70OH201", "ZR70OH210", "ZR70OH213", "ZR70OH224",
            "ZR70OH228", "ZR70OH247", "ZR70OH703", "ZR70OH131", "ZR70OH101", "ZR43021"
        };

        private static readonly string ServiceType = "SAP to MySql";

        public static void ImportData()
        {
            DatabaseManager.Execute("TRUNCATE_SAP_TEMP_TABLE");
            foreach (string table in Tables)
            {
                UploadData(table);
            }

            DirectoryInfo d = new DirectoryInfo(Path.SAP_R_EXECUTION);
            foreach (var file in d.GetFiles("*.csv"))
            {
                //if (!File.Exists(file.FullName))
                    Directory.Move(file.FullName, Path.SAP_R_BACKUP + @"\BK_" + DateTime.Today.ToString("yyyyMMdd") + file.Name);
            }
        }

        public static void UploadData(string table)
        {
            string message = table + " successfully uploaded.";
            try
            {
                string connString = string.Format("SERVER = {0}; DATABASE = {1}; UID = {2}; PASSWORD = {3}; default command timeout=1000;", server, database, uid, password);
                //string server = "localhost";
                //        string database = "sap1";
                //        string uid = "APCSLSS";
                //        string password = "SLSS@inst2018";
                MySqlConnection conn = new MySqlConnection(connString);
                MySqlBulkLoader bulkLoader = new MySqlBulkLoader(conn)
                {
                    TableName = table,
                    FieldTerminator = "|",
                    LineTerminator = "\n",
                    CharacterSet = "utf8",
                    FileName = Path.SAP_R_EXECUTION + table + ".csv"
                };

                int count = bulkLoader.Load();
                Path.SetSAPLogsMessage(message, ServiceType, table);
            }
            catch (MySqlException ex)
            {
                Path.SetSAPSendError(ex.Message, ServiceType, table);
                if(File.Exists(Path.SAP_R_EXECUTION + table + ".csv"))
                    Directory.Move(Path.SAP_R_EXECUTION + table + ".csv", Path.SAP_R_ERROR + table + ".csv");
            }        }
    }
}