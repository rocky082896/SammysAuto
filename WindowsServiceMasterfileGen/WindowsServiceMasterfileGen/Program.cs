using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsServiceMasterfileGen.DataHelper;
using MySql.Data.MySqlClient;
using System.IO;

namespace WindowsServiceMasterfileGen
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Windows Service' background process is running, do not close!....");
                DirectoryInfo d = new DirectoryInfo(DataHelper.Path.SAP_S_SEND);
                foreach (var file in d.GetFiles("*.csv"))
                {
                    if (!File.Exists(file.FullName))
                        Directory.Move(file.FullName, DataHelper.Path.SAP_S_BACKUP + @"\BK_" + DateTime.Today.ToString("yyyyMMdd") + file.Name);
                }
                STM.ImportData(); // program 1 (day)
                MTSF.ExportData(); // program 1 (day)
            }
            catch (MySqlException ex)
            {
                DataHelper.Path.SetSAPSendError(ex.Message, "day program", "no table");
            }
        }
    }
}
