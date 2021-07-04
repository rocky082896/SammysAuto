using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
namespace TestService
{
    class UpdateSQL
    {
        public string constring = "server=localhost; uid=root; pass=; database=ajidb";
        
        public void InvoiceListing(string filename)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(constring);
               // using (var conn = new MySqlConnection(constring))
                //{
                    var bl = new MySqlBulkLoader(conn)
                    {
                        TableName = "invoice_listing",
                        FieldTerminator = "|",
                        LineTerminator = "\n",
                        FileName = filename,
                        NumberOfLinesToSkip = 1,
                        Columns = { "order_no", "invoice_num", "customer", "amount" }
                    };
                    var numberOfInsertedRows = bl.Load();
                //}
            }
            catch (MySqlException ex)
            {
                // e.g. "d:/test.docx"

                try
                {
                    CreateTextFile(ex.Message, filename);
                    string sourcefilepath = Path.Combine(@"D:\AjiFolder\error\", filename + "-ErrorLog.txt");
                    UploadFileToFTP(sourcefilepath);
                }
                catch (Exception e)
                {
                    CreateTextFile(ex.Message, filename);
                    string sourcefilepath = Path.Combine(@"D:\AjiFolder\error\", filename + "-ErrorLog.txt");
                    UploadFileToFTP(sourcefilepath);
                   // CreateTextFile(e.Message + ", " + ex.Message, filename);
                }

                //MySqlConnection con = new MySqlConnection(constring);
                //string sql = "INSERT INTO status VALUES (@filename, @status)";
                //con.Open();
                //MySqlCommand cmd = new MySqlCommand(sql, con);
                //con.Open();
                //cmd.Parameters.AddWithValue("@filename", filename);
                //cmd.Parameters.AddWithValue("@status", ex.Message);
                //cmd.ExecuteNonQuery();
                //con.Close();
            }
        }
        private void UploadFileToFTP(string source)
        {
            string ftpurl = "ftp://10.63.3.17/error/"; // e.g. ftp://serverip/foldername/foldername
            string ftpusername = "sps.roland.bitos"; // e.g. username
            string ftppassword = "$Rb1010101010"; // e.g. password

            FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(ftpurl + "/" +Path.GetFileName(source));
            request.Method = WebRequestMethods.Ftp.UploadFile;
            request.Credentials = new NetworkCredential(ftpusername, ftppassword);
            request.UsePassive = true;
            request.UseBinary = true;
            request.KeepAlive = false;

            FileStream stream = File.OpenRead(source);
            byte[] buffer = new byte[stream.Length];

            stream.Read(buffer, 0, buffer.Length);
            stream.Close();

            Stream reqStream = request.GetRequestStream();
            reqStream.Write(buffer, 0, buffer.Length);
            reqStream.Close();
        }

        private void CreateTextFile(string error, string filename)
        {
            StreamWriter SW;
            if (!File.Exists(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),  filename + "-ErrorLog.txt")))
            {
                SW = File.CreateText(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), filename + "-ErrorLog.txt"));
                SW.Close();
            }
            using (SW = File.AppendText(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), filename + "-ErrorLog.txt")))
            {
                SW.WriteLine("Error: " + error);
                SW.Close();
            }
        }
    }
}