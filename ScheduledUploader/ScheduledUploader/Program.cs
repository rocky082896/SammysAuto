using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ScheduledUploader
{
    class Program
    {
        static void Main(string[] args)
        {
            UpdateSQL update;
            string path = @"C:\Users\sps.roland.bitos\source\repos\TestService\TestService\bin\Debug\backup";
            string[] files = Directory.GetFiles(path, "*.csv", SearchOption.AllDirectories);
            foreach(string p in files)
            {
                update = new UpdateSQL();
                update.InvoiceListing(p);
            }
            Thread.Sleep(5000);
            Environment.Exit(0);
        }
    }
}
