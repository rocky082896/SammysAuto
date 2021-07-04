using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
namespace WindowsServiceMasterfileGen.DataHelper
{
    class MTSF
    {
        private static readonly string server = "localhost";
        private static readonly string database = "sap1";
        private static readonly string uid = "APCSLSS";//"apcslss";
        private static readonly string password = "SLSS@inst2018"; //"slss@inst2018"; 
        private static readonly List<string> Tables = new List<string>() {
                 "storage_location", "machine_master", "discount_master", "customer", "customer_details",
                "discount_list", "brand", "material", "conversion_master", "pricelist" };

        //PROCEDURES
        private static readonly List<string> procedures = new List<string>(){ "SEARCH_BRAND", "SEARCH_CONVERSION_MASTER", "SEARCH_CUSTOMER", "SEARCH_CUSTOMER_BANK", "SEARCH_QUANTITY_SCALE",
                "SEARCH_MATERIAL", "SEARCH_PRICELIST", "SEARCH_STORAGE_LOCATION", "SEARCH_DISCOUNT_LIST", "SEARCH_DISCOUNT_MASTER" };
        private static readonly string ServiceType = "MySql to Sales FTP";

        public static void ExportData()
        {
            string conString = string.Format("SERVER = {0}; DATABASE = {1}; UID = {2}; PASSWORD = {3}; default command timeout=1000;", server, database, uid, password);

            foreach (string table in procedures)
            {
                ExportToCSV(table);
            }
            DatabaseManager.Execute("INSERT_MACHINE_MASTER");
        }
        public static void ExportToCS2V(string table)
        {
            string message = table + " successfully exported.";

            try
            {
                string query = string.Format("SELECT * FROM {0}", table);
                CSVHelper.CSVWriter(DatabaseManager.ReadAdapter(table), table);
                Path.SetSAPLogsMessage(message, ServiceType, table);
            }
            catch (Exception ex)
            {
                Path.SetSAPSendError(ex.Message, ServiceType, table);
            }
        }
        public static void ExportToCSV(string table)
        {
            string message = table + " successfully exported.";
            try
            {
                string query = string.Format("SELECT * FROM {0}", table);
                if (table == "SEARCH_BRAND")
                {
                    CSVHelper.CSVWriter(DatabaseManager.ReadAdapter(table), "08_brand");
                    Path.SetSAPLogsMessage(message, ServiceType, table);
                }

                if (table == "SEARCH_CONVERSION_MASTER")
                {
                    CSVHelper.CSVWriter(DatabaseManager.ReadAdapter(table), "11_uom");
                    Path.SetSAPLogsMessage(message, ServiceType, table);
                }

                if (table == "SEARCH_CUSTOMER")
                {
                    string sql = "SELECT salesman_id FROM salesman";
                    Ajinomoto.ReadData(sql);
                    while (Ajinomoto.dr.Read())
                    {
                        try
                        {
                            CSVHelper.CSVWriter(DatabaseManager.ReadAdapter(table,
                                Ajinomoto.dr["salesman_id"].ToString()), "04_cstm_" + Ajinomoto.dr["salesman_id"].ToString());
                        }
                        catch (MySqlException ex)
                        {
                            Path.SetSAPSendError(ex.Message, ServiceType, table + ": " + Ajinomoto.dr["salesman_id"].ToString());
                        }
                    }

                    //    //string[] stations = { "30701514-0", "30701535-2", "30700587-8", "30700744-9",
                    //    //    "30701089-8", "30701384-3", "30700552-5", "30700224-1", "30701460-4" };
                    //    //foreach (string s in stations)
                    //    //{

                    //    //}
                    Path.SetSAPLogsMessage(message, ServiceType, table);
            }

                if (table == "SEARCH_CUSTOMER_BANK")
                {
                    CSVHelper.CSVWriter(DatabaseManager.ReadAdapter(table), "05_cstmd"); //12_qtysc
                    Path.SetSAPLogsMessage(message, ServiceType, table);
                }

                if (table == "SEARCH_QUANTITY_SCALE")
                {
                    CSVHelper.CSVWriter(DatabaseManager.ReadAdapter(table), "12_qtysc"); //12_qtysc
                    Path.SetSAPLogsMessage(message, ServiceType, table);
                }

                if (table == "SEARCH_MATERIAL")
                {
                    CSVHelper.CSVWriter(DatabaseManager.ReadAdapter(table), "09_mtrl");
                    Path.SetSAPLogsMessage(message, ServiceType, table);
                }

                if (table == "SEARCH_PRICELIST")
                {
                    CSVHelper.CSVWriter(DatabaseManager.ReadAdapter(table), "10_prcl");
                    Path.SetSAPLogsMessage(message, ServiceType, table);
                }

                if (table == "SEARCH_STORAGE_LOCATION")
                {
                    CSVHelper.CSVWriter(DatabaseManager.ReadAdapter(table), "01_strglc");
                    Path.SetSAPLogsMessage(message, ServiceType, table);
                }

                if (table == "SEARCH_DISCOUNT_LIST")
                {
                    CSVHelper.CSVWriter(DatabaseManager.ReadAdapter(table), "07_dsctl");
                    Path.SetSAPLogsMessage(message, ServiceType, table);
                }

                if (table == "SEARCH_DISCOUNT_MASTER")
                {
                    CSVHelper.CSVWriter(DatabaseManager.ReadAdapter(table), "06_dsct");
                    Path.SetSAPLogsMessage(message, ServiceType, table);
                }
            }
            catch (Exception ex)
            {
                Path.SetSAPSendError(ex.Message, ServiceType, table);
            }

            //DirectoryInfo d = new DirectoryInfo(Path.SAP_S_EXECUTION);
            //foreach (var file in d.GetFiles("*.csv"))
            //{
            //    Directory.Move(file.FullName, Path.SAP_S_SEND + @"\BK_" + DateTime.Today.ToString("yyyyMMdd") + file.Name);
            //}
        }
    }
}
