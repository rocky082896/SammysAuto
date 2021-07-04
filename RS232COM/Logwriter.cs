using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RS232COM
{
    class LogWriter
    {
        private readonly static string serviceLogsPath = @"D:\Logs\Service Logs\";
        private readonly static string plantLogsPath = @"D:\Logs\Plant Logs\";
        private readonly static string sentLogsPath = @"D:\Logs\Sent Logs\";
        private readonly static string importLogsPath = @"D:\Logs\Import Logs\";
        private readonly static string dateNowLogs = DateTime.Now.ToString("MMddyyyy") + ".logs";
        private readonly static string dateTimeNow = DateTime.Now.ToString("MMMM dd, yyyy hh:mm:ss tt");

        public static void WriteLog(string path, string message)
        {
            if (File.Exists(path + dateNowLogs))
            {
                string content = File.ReadAllText(path + dateNowLogs);
                content = AddString(message) + "\n" + content;
                File.WriteAllText(path + dateNowLogs, content);
            }
            else
            {
                using (StreamWriter writer = File.AppendText(path + dateNowLogs))
                {
                    Log(writer, message);
                }
            }
        }

        public static void Log(TextWriter writer, string message)
        {
            writer.WriteLine("Date and Time : {0}", dateTimeNow);
            writer.WriteLine("Message       : {0}", message);
            writer.WriteLine("-----------------------------------------------------------------------------" + Environment.NewLine);
        }

        public static string AddString(string message)
        {
            string dateTime = "Date and Time    : " + dateTimeNow + Environment.NewLine;
            string messageContent = string.Format("Message         : {0}\n" + Environment.NewLine, message);
            string separator = "-----------------------------------------------------------------------------" + Environment.NewLine;
            return dateTime + messageContent + separator;
        }

        public static void SetServiceLogs(string message)
        {
            if (!Directory.Exists(serviceLogsPath))
            {
                DirectoryInfo directoryInfo = Directory.CreateDirectory(serviceLogsPath);
            }
            WriteLog(serviceLogsPath, message);
        }

        public static void SetErrorImportLogs(string content)
        {
            if (!Directory.Exists(importLogsPath))
            {
                DirectoryInfo directoryInfo = Directory.CreateDirectory(importLogsPath);
            }

            using (StreamWriter writer = File.AppendText(importLogsPath + "_Err" + dateNowLogs))
            {
                writer.WriteLine(content);
            }
        }

        public static void SetImportLogs(string content)
        {
            if (!Directory.Exists(importLogsPath))
            {
                DirectoryInfo directoryInfo = Directory.CreateDirectory(importLogsPath);
            }

            using (StreamWriter writer = File.AppendText(importLogsPath + dateNowLogs))
            {
                writer.WriteLine(content);
            }
        }

        public static void SetPlantLogs(string content)
        {
            if (!Directory.Exists(plantLogsPath))
            {
                DirectoryInfo directoryInfo = Directory.CreateDirectory(plantLogsPath);
            }

            using (StreamWriter writer = File.AppendText(plantLogsPath + dateNowLogs))
            {
                writer.WriteLine(content);
            }
        }

        public static void SetSentLogs(string content)
        {
            if (!Directory.Exists(sentLogsPath))
            {
                DirectoryInfo directoryInfo = Directory.CreateDirectory(sentLogsPath);
            }

            using (StreamWriter writer = File.AppendText(sentLogsPath + dateNowLogs))
            {
                writer.WriteLine(content);
            }
        }

        public static void SetErrorSentLogs(string content)
        {
            if (!Directory.Exists(sentLogsPath))
            {
                DirectoryInfo directoryInfo = Directory.CreateDirectory(sentLogsPath);
            }

            using (StreamWriter writer = File.AppendText(sentLogsPath +"_Err" + dateNowLogs))
            {
                writer.WriteLine(content);
            }
        }
    }
}
