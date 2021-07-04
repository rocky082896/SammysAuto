using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Deployment.Compression.Cab;


namespace Unzip
{
    class Program
    {
        static void Main(string[] args)
        {
            Decompress(@"D:\samp\ZR70CA001.cab", @"D:\samp\");
        }

        public static bool Decompress(string filePath, string extractFolder)
        {
            try
            {
                var cab = new CabInfo(filePath);
                cab.Unpack(extractFolder);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
