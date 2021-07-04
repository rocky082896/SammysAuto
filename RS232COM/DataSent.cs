using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RS232COM
{
    class DataSent
    {
        public string id;
        private readonly string dateNow = DateTime.Now.ToString("MM-dd-yyyy hh:mm:ss");
        private static string ConvertVolume(string value)
        {
            switch (value)
            {
                case "6 kg":
                    value = "01";
                    break;
                case "11 kg":
                    value = "02";
                    break;
                case "22 kg":
                    value = "03";
                    break;
                case "50 kg":
                    value = "04";
                    break;
                case "47 kg":
                    value = "05";
                    break;
                case "5 kg":
                    value = "06";
                    break;
            }
            return value;
        }

        private static string ConvertTareweight(string value)
        {
            if (value.Length == 2)
            {
                value = value + "0";
            }
            return value;
        }

        public bool GetUnfilledCylinder()
        {
            string cons;
            using (var conn = new MySqlConnection(Global._CONN))
            {
                conn.Open();
                using (MySqlCommand cmd = conn.CreateCommand())
                {
                    try
                    {
                        cmd.CommandText = "SELECT COUNT(*) FROM tbl_filling WHERE status = '0'";
                        using (MySqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                if (int.Parse(dr[0].ToString()) > 0)
                                {
                                    LogWriter.SetSentLogs("Date and Time : " + DateTime.Now.ToString("MM-dd-yyyy"));
                                    LogWriter.SetSentLogs("Time          : " + DateTime.Now.ToString("hh:mm tt"));
                                    LogWriter.SetSentLogs("Record        : " + dr[0].ToString() + " unfilled data has detected.");
                                    return true;
                                }
                            }
                        }
                    }catch(Exception ex)
                    {
                        LogWriter.SetErrorSentLogs("Date and Time  : " + dateNow);
                        LogWriter.SetErrorSentLogs("Remarks        : Failed Getting Unfilled Cylinder.");
                        LogWriter.SetErrorSentLogs("Connection     : " + Global._CONN);
                        LogWriter.SetErrorSentLogs("Error          : " + ex.Message);
                        //LogWriter.SetErrorImportLogs("Query          : " + sb.ToString());
                        LogWriter.SetErrorSentLogs("----------------------------------------");
                    }
                }
            }
            return false;
        }

        public string GetString()
        {
            string cons;
            using (var conn = new MySqlConnection(Global._CONN))
            {
                conn.Open();
                using (MySqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT tbl_filling.filling_id, tbl_filling.machine_no, tbl_filling.username, " +
                        "tbl_filling.cylinder_barcode, tbl_cylinder.tare_weight, tbl_cylinder.volume FROM tbl_filling " +
                        "JOIN tbl_cylinder ON tbl_cylinder.cylinder_barcode = tbl_filling.cylinder_barcode WHERE " +
                        "tbl_filling.status = '0' AND to_plant LIKE 'San Fernando' ORDER BY filling_id ASC LIMIT 1";
                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            id = dr["filling_id"].ToString();
                            string stationID = dr["machine_no"].ToString();
                            string operatorID = dr["username"].ToString();
                            string cylinderBarcode = dr["cylinder_barcode"].ToString();
                            string tareweight = dr["tare_weight"].ToString().Replace(".", "") + "0";
                            string volume = dr["volume"].ToString();
                            string result = "*" + stationID + operatorID + cylinderBarcode + ConvertTareweight(tareweight) + ConvertVolume(volume) + "+";
                            return result;
                        }
                    }
                }
            }
            return null;
        }

        public bool UpdateStatus()
        {
            string cons;
            using (var conn = new MySqlConnection(Global._CONN))
            {
                conn.Open();
                using (MySqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "UPDATE tbl_filling SET status = '1' WHERE filling_id = @ID";
                    cmd.Parameters.AddWithValue("@ID", id);
                    cmd.ExecuteNonQuery();
                    LogWriter.SetSentLogs("String successfully sent to Cybil PC.");
                    LogWriter.SetSentLogs("Filling ID [" + id + "] has successfully updated the status to filled.");
                    LogWriter.SetSentLogs("-------------------------------------");
                    return true;
                }
            }
        }
    }
}
