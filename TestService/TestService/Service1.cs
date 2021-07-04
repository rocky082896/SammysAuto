using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Threading;
using System.IO;
using System.Reflection;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Security.AccessControl;
using System.Net;
using System.Net.Cache;

namespace TestService
{
    public partial class Service1 : ServiceBase
    {
        string Watcher = ConfigurationManager.AppSettings["WatchFolder"];
        System.Timers.Timer _timer;
        DateTime _scheduleTime;
        public Service1()
        {
            InitializeComponent();
            _timer = new System.Timers.Timer();
            _scheduleTime = DateTime.Today.AddHours(1);
            fileSystemWatcher1.Created += fileSystemWatcher1_created;
        }

        private bool CheckFileExistance(string FullPath, string FileName)
        {
            // Get the subdirectories for the specified directory.'
            bool IsFileExist = false;
            DirectoryInfo dir = new DirectoryInfo(FullPath);
            if (!dir.Exists)
                IsFileExist = false;
            else
            {
                string FileFullPath = Path.Combine(FullPath, FileName);
                if (File.Exists(FileFullPath))
                    IsFileExist = true;
            }
            return IsFileExist;
        }

        public static void Create_ServiceStoptextfile()
        {
            string Destination = "D:\\AjiFolder";
            StreamWriter SW;
            if (Directory.Exists(Destination))
            {
                Destination = System.IO.Path.Combine(Destination, "txtServiceStop_" + DateTime.Now.ToString("yyyyMMdd") + ".txt");
                if (!File.Exists(Destination))
                {
                    SW = File.CreateText(Destination);
                    SW.Close();
                }
            }
            using (SW = File.AppendText(Destination))
            {
                SW.Write("\r\n\n");
                SW.WriteLine("Service Stopped at: " + DateTime.Now.ToString("dd-MM-yyyy H:mm:ss"));
                SW.Close();
            }
        }

        private void CreateTextFile(string FullPath, string FileName, String type)
        {
            StreamWriter SW;
            if (!File.Exists(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), type+"_" + FileName+ DateTime.Now.ToString("yyyyMMdd") + ".txt")))
            {
                SW = File.CreateText(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), type+"_" + FileName+ DateTime.Now.ToString("yyyyMMdd") + ".txt"));
                SW.Close();
            }
            using (SW = File.AppendText(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), type+"_" + FileName + DateTime.Now.ToString("yyyyMMdd") + ".txt")))
            {
                SW.WriteLine("File Created with Name: " + FileName + " at this location: " + FullPath);
                SW.Close();
            }
        }

        private void Check(string c)
        {
            StreamWriter SW;
            if (!File.Exists(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Check" + ".csv"))){
                SW = File.CreateText(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Check" + ".csv"));
                SW.Close();
            }
            using (SW = File.AppendText(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Check" + ".csv"))){
                SW.WriteLine(c);
                SW.Close();
            }
        }

        void fileSystemWatcher1_created(Object sender, System.IO.FileSystemEventArgs e)
        {
            MasterFileGenerator master = new MasterFileGenerator();
            master.GenerateBrand();
            master.GenerateCustomer();
            master.GenerateDetails();
            master.GenerateDiscountList();
            master.GenerateDiscountMas();
            master.GenerateMachine();
            master.GenerateMaterial();
            master.GeneratePriceList();
            master.GenerateStorLoc();
            //master.GenerateSalesman();
        }
        protected override void OnStart(string[] args)
        {
            try{
                fileSystemWatcher1.Path = Watcher;
            }
            catch (Exception ex){
                CreateTextFile(ex.Message,ex.Message, "Error Log");
                throw ex;
            }
        }

        protected override void OnStop()
        {
            try
            {
                //Create_ServiceStoptextfile();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void fileSystemWatcher1_Changed(object sender, FileSystemEventArgs e){

        }
    }
}