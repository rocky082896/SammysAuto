using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RS232COM
{
    class ServerAuth
    {
        public static int IPCounter = 0;
        public static bool IsConnected()
        {
            try
            {
                Thread.Sleep(500);
                Ping ping = new Ping();
                PingReply pingReply = ping.Send("boost.islagrp.com", 1000);
                IPCounter = 0;
                return true;
            }
            catch (Exception)
            {
                IPCounter += 1;
                return false;
            }
        }
    }
}
