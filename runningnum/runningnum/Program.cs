using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace runningnum
{
    class Program
    {
        private static readonly string server = "boost.islagrp.com:3306";
        private static readonly string database = "islaboost_db";
        private static readonly string uid = "root";
        private static readonly string password = "S@t0_M@st3r";

        private static readonly string _server = "localhost";
        private static readonly string _database = "islaboost_db";
        private static readonly string _uid = "root";
        private static readonly string _password = "";

        public static readonly string CONN = string.Format("SERVER = {0}; DATABASE = islaboost_db; UID = {1}; " +
            "PASSWORD = {2}; SslMode = NONE", server, uid, password);

        public static readonly string _CONN = string.Format("SERVER = localhost; DATABASE = islaboost_db; UID = {2}; " +
            "PASSWORD = {3}; SslMode = NONE", _server, _database, _uid, _password);

        static void Main(string[] args)
        {

            //List<string> date = new List<string> {
            //    "MAR-21", "JUL-23", "AUG-25", "MAY-25",  "NOV-26", "JAN-27", "NOV-24", "APR-27"
            //};
            //foreach (string item in date)
            //{
            //    string[] arr = item.Split('-');
            //    string literal = DateTime.Parse(item).AddYears(int.Parse(arr[1])).AddYears(-19).ToString("yyyy-MM-01");
            //    Console.WriteLine(literal);
            //}

            //string s = "01/29/19";
            //Console.WriteLine(DateTime.Parse(s).ToString("MM-dd-yyyy"));

            //List<string> l1 = new List<string> { "1", "2", "3", "4", "5", "6" };
            //List<string> l2 = new List<string> { "1", "2", "3", "4", "5", "6" };
            ////List<string> l2 = new List<string> { "1", "3", "2", "4", "5", "6", "7", "8", "9", "10" };

            //List<string> container1 = l1.Distinct().ToList();
            //List<string> container2 = l2.Distinct().ToList();

            //bool istrue = Enumerable.SequenceEqual(container1.OrderBy(fElement => fElement),
            //             container2.OrderBy(sElement => sElement));

            //Console.Write(istrue+"");

            //StringBuilder notEquals = new StringBuilder();

            //foreach (var item in l1)
            //{
            //    if (!l2.Contains(item))
            //        notEquals.Append(item);
            //}

            //if (notEquals.ToString().Trim() == "")
            //    Console.Write("Ayos tayo jan");
            //else
            //    Console.Write("k0w");

            //Console.Write(notEquals);

            //char cha = 'Z';
            //int runnum = 99999;

            //if (runnum >= 99999)
            //    Console.Write(++cha);
            //else
            //    Console.Write(cha);

            //int i = 0;
            //string s = i.ToString("D2");
            //Console.WriteLine(s);

            //string s = "50 kg";
            //Console.WriteLine(s.Trim());

            //List<string> cids = new List<string> { "1", "2", "3", "4", "5", "66" };
            //string date = "8-29-19";
            //string LPN = "ABC-123";
            //string cmb = "21.22";
            //StringBuilder finalString = new StringBuilder();
            //int count = cids.Count;
            //int i = 0;
            //foreach (string item in cids)
            //{
            //    i += 1;
            //    if(i != count)
            //        finalString.Append("'" + item + "', ");
            //    else
            //        finalString.Append("'" + item + "'");
            //    //if (i != count)
            //    //    finalString.Append("(" + date + ", " + item + ", " + LPN + ", " + cmb +  "),");
            //    //else
            //    //    finalString.Append("(" + date + ", " + item + ", " + LPN + ", " + cmb +  ")");
            //}

            //Console.WriteLine(finalString.ToString());

            //double txpow, rssi;

            //Console.Write("Input Tx power: " );
            //txpow = double.Parse(Console.ReadLine());

            //Console.Write("Input RSSI: ");
            //rssi = double.Parse(Console.ReadLine());

            //Console.Write("Answer: " + getDistance(rssi, txpow) + " mm");

            int IPCounter = 0;
            while (IPCounter != 0 || IPCounter != 5)
            {
                try
                {
                    Ping ping = new Ping();
                    PingReply pingReply = ping.Send("boost.islagrp.com", 1000);
                    Console.WriteLine("Connected");
                   // conn.ConnectionString = Global.CONN;
                    IPCounter = 0;
                }
                catch (Exception)
                {
                    IPCounter += 1;
                    if (IPCounter >= 5)
                    {
                        Console.WriteLine("Not Connected :" + IPCounter);
                    }
                }

            }

            //int n = 0;
            //while(n != 100000000)
            //{
            //    try
            //    {
            //        Thread.Sleep(500);
            //        Ping ping = new Ping();
            //        PingReply pingReply = ping.Send("boost.islagrp.com", 1000);
            //        Console.WriteLine(CONN);
            //        // Console.WriteLine(pingReply.Status.ToString());
            //    }
            //    catch (Exception)
            //    {
            //        Console.WriteLine(_CONN);
            //        //Console.WriteLine("TimedOut");
            //    }
            //    n++;
            //}

            Console.ReadKey();
        }

        //public static bool CheckConnection()
        //{
        //    bool isConencted = false;
        //    IPAddress IP;
        //    if (IPAddress.TryParse("boost.islagrp.com", out IP))
        //    {
        //        Socket s = new Socket(AddressFamily.InterNetwork,
        //        SocketType.Stream,
        //        ProtocolType.Tcp);
        //        try
        //        {
        //            s.Connect(IP, port);
        //            Console.WriteLine(s.Connected); // = true;
        //        }
        //        catch
        //        {
        //            isConencted = false;
        //        }
        //    }

        //    return isConencted;
        //}
    }
}
