using SATOWindowsService.DataHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsServiceMasterfileGen.DataHelper
{
    class Path
    {
        #region SAP FOLDER
        public static string SAP_R_RECEIVE = @"D:\SAP\R_Receive\";
        public static string SAP_R_EXECUTION = @"D:\SAP\R_Execution\";
        public static string SAP_R_BACKUP = @"D:\SAP\R_Backup\";
        public static string SAP_R_ERROR = @"D:\SAP\R_Error\";
        public static string SAP_S_EXECUTION = @"D:\SAP\S_Execution\";
        public static string SAP_S_SEND = @"D:\SAP\S_Send\";
        public static string SAP_S_BACKUP = @"D:\SAP\S_Backup\";
        public static string SAP_S_ERROR = @"D:\SAP\S_Error\";
        public static string SAP_LOG = @"D:\SAP\Log\";
        #endregion

        #region HT FOLDER
        public static string HT_R_RECEIVE = @"D:\HT\R_Receive\";
        public static string HT_R_EXECUTION = @"D\HT\R_Execution\";
        public static string HT_R_BACKUP = @"D:\HT\R_Backup\";
        public static string HT_R_ERROR = @"D:\HT\R_Error\";
        public static string HT_S_EXECUTION = @"D:\HT\S_Execution\";
        public static string HT_S_SEND = @"D:\HT\S_Send\";
        public static string HT_S_BACKUP = @"D:\HT\S_Backup\";
        public static string HT_S_ERROR = @"D:\HT\S_Error\";
        public static string HT_LOG = @"D:\HT\Log\";
        #endregion

        public static void SetSAPLogsMessage(string message, string serviceType, string table)
        {
            LogWriter logWriter = new LogWriter(SAP_LOG, message, serviceType, table);
        }

        public static void SetSAPReceiveError(string message, string serviceType, string table)
        {
            LogWriter logWriter = new LogWriter(SAP_R_ERROR, message, serviceType, table);
        }

        public static void SetSAPSendError(string message, string serviceType, string table)
        {
            LogWriter logWriter = new LogWriter(SAP_S_ERROR, message, serviceType, table);
        }

        public static void SetHTLogsMessage(string message, string serviceType, string table)
        {
            LogWriter logWriter = new LogWriter(HT_LOG, message, serviceType, table);
        }

        public static void SetHTReceiveError(string message, string serviceType, string table)
        {
            LogWriter logWriter = new LogWriter(HT_R_ERROR, message, serviceType, table);
        }

        public static void SetHTSendError(string message, string serviceType, string table)
        {
            LogWriter logWriter = new LogWriter(HT_S_ERROR, message, serviceType, table);
        }
    }
}
