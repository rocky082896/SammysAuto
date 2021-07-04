using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using ScheduledExporter.Class;

namespace ScheduledExporter
{
    class Program
    {
        static void Main(string[] args)
        {
            Exporter exporter = new Exporter();
            Console.WriteLine("Exporting.....");
            Thread.Sleep(3000);
            exporter.TableLooper();
            Console.Write("Done..");
            Environment.Exit(0);
        }
    }
}
