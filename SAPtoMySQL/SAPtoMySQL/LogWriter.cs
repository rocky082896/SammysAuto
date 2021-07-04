using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAPtoMySQL
{
    public class LogWriter
    {
        private readonly string path;
        private readonly string message;
        private readonly string serviceType;
        private readonly string table;

        public LogWriter(string path, string message, string serviceType, string table)
        {
            this.path = path;
            this.message = message;
            this.serviceType = serviceType;
            this.table = table;
            WriteLog();
        }

        public void WriteLog()
        {
            if (File.Exists(path + serviceType + " " + DateTime.Now.ToString("MMddyyyy") + ".logs"))
            {
                string content = File.ReadAllText(path + serviceType + " " + DateTime.Now.ToString("MMddyyyy") + ".logs");
                content = AddData() + "\n" + content;
                File.WriteAllText(path + serviceType + " " + DateTime.Now.ToString("MMddyyyy") + ".logs", content);
            }
            else
            {
                using (StreamWriter streamWriter = File.AppendText(path + serviceType + " " + DateTime.Now.ToString("MMddyyyy") + ".logs"))
                {
                    Log(streamWriter);
                }
            }
        }

        public void Log(TextWriter writer)
        {
            writer.WriteLine("Date/Time    : {0}", DateTime.Now.ToString("MMMM dd, yyyy hh:mm:ss tt"));
            writer.WriteLine("Service Type : {0} ({1})", serviceType, table);
            writer.WriteLine("Message      : {0}", message);
            writer.WriteLine("-----------------------------------------------------------------------------" + Environment.NewLine);
        }

        public string AddData()
        {
            string line = string.Format("Date/Time    : {0}" + Environment.NewLine, DateTime.Now.ToString("MMMM dd, yyyy hh:mm:ss tt"));
            string lineTwo = string.Format("Service Type : {0} ({1})" + Environment.NewLine, serviceType, table);
            string lineThree = string.Format("Message      : {0}" + Environment.NewLine, message);
            string lineFour = "-----------------------------------------------------------------------------" + Environment.NewLine;

            string result = line + lineTwo + lineThree + lineFour;

            return result;
        }
    }
}
