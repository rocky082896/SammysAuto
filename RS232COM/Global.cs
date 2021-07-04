using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RS232COM
{
    class Global
    {
        private static readonly string server = "boost.islagrp.com";
        private static readonly string database = "islaboost_db";
        private static readonly string uid = "root";
        private static readonly string password = "S@t0_M@st3r";
        private static readonly string port = "3308";

        private static readonly string _server = "localhost";
        private static readonly string _database = "islaboost_db";
        private static readonly string _uid = "root";
        private static readonly string _password = "";

        public static readonly string CONN = string.Format("SERVER = {0}; DATABASE = islaboost_db; UID = {1}; " +
            "PASSWORD = {2}; PORT ={3}; SslMode = NONE", server, uid, password, port);

        public static readonly string _CONN = string.Format("SERVER = localhost; DATABASE = islaboost_db; UID = {2}; " +
            "PASSWORD = {3}; SslMode = NONE", _server, _database, _uid, _password);
    }
}