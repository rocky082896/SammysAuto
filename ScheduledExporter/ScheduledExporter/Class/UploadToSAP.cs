using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ScheduledExporter.Class
{
    class UploadToSAP
    {
        public void UploadFileToFTP(string file)
        {
            string ftpurl = "ftp://10.63.3.17/SAP/"; // e.g. ftp://serverip/foldername/foldername
            string ftpusername = "sps.roland.bitos"; // e.g. username
            string ftppassword = "$Rb1010101010"; // e.g. password

            FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(ftpurl + "/" + Path.GetFileName(file));
            request.Method = WebRequestMethods.Ftp.UploadFile;
            request.Credentials = new NetworkCredential(ftpusername, ftppassword);
            request.UsePassive = true;
            request.UseBinary = true;
            request.KeepAlive = false;

            FileStream stream = File.OpenRead(file);
            byte[] buffer = new byte[stream.Length];

            stream.Read(buffer, 0, buffer.Length);
            stream.Close();

            Stream reqStream = request.GetRequestStream();
            reqStream.Write(buffer, 0, buffer.Length);
            reqStream.Close();
        }
    }
}
