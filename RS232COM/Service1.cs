using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace RS232COM
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        public void OnDebug()
        {
            OnStart(null);
        }

        protected override void OnStart(string[] args)
        {
            watch();
        }

        protected override void OnStop()
        {
            LogWriter.SetServiceLogs("Windows service has stopped.");
        }

        private void watch()
        {
            LogWriter.SetServiceLogs("Windows service has started.");

            //tmrSendingString.Enabled = true;
            //tmrSendingString.Interval += 1000;
           // tmrSendingString.Start();

            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.Path = @"D:\Cybil\";
            watcher.NotifyFilter = NotifyFilters.LastWrite;
            watcher.Filter = "*.*";
            watcher.Changed += new FileSystemEventHandler(fwImportCybil_Changed);
            watcher.EnableRaisingEvents = true;
        }

        private void fwImportCybil_Changed(object sender, System.IO.FileSystemEventArgs e)
        {
            DataImport dataImport = new DataImport();
            dataImport.ImportCybilData(e.FullPath);
        }

        private void tmrSendingString_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            DataSent dataSent = new DataSent();
            if (dataSent.GetUnfilledCylinder())
            {
                if (!string.IsNullOrEmpty(dataSent.GetString()))
                {
                    string detectedPort = null;
                    string[] port = SerialPort.GetPortNames();

                    foreach (var ports in port)
                    {
                        detectedPort = ports.ToString();
                    }

                    if (string.IsNullOrEmpty(detectedPort))
                    {
                        LogWriter.SetSentLogs("Cannot detect COM port.");
                    }
                    else
                    {
                        SerialPort serialPort;
                        using (serialPort = new SerialPort(detectedPort, 115200, Parity.None, 8, StopBits.One))
                        {
                            serialPort.Open();
                        }

                        try
                        {
                            LogWriter.SetSentLogs("String to be sent in Cybil : " + dataSent.GetString());
                            serialPort.Open();
                            serialPort.Write(dataSent.GetString());
                            dataSent.UpdateStatus();
                        }
                        catch (Exception ex)
                        {
                            LogWriter.SetSentLogs("Filling ID [" + dataSent.id + "] was failed to update.");
                            LogWriter.SetSentLogs("Failed due to serial connection error.");
                            LogWriter.SetSentLogs(ex.Message);
                            LogWriter.SetSentLogs("----------------------------------------");
                        }
                    }
                }
            }
        }
    }
}
