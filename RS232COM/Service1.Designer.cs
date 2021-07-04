namespace RS232COM
{
    partial class Service1
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.fwImportCybil = new System.IO.FileSystemWatcher();
            this.tmrSendingString = new System.Timers.Timer();
            ((System.ComponentModel.ISupportInitialize)(this.fwImportCybil)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tmrSendingString)).BeginInit();
            // 
            // fwImportCybil
            // 
            this.fwImportCybil.EnableRaisingEvents = true;
            this.fwImportCybil.Changed += new System.IO.FileSystemEventHandler(this.fwImportCybil_Changed);
            // 
            // tmrSendingString
            // 
            this.tmrSendingString.Elapsed += new System.Timers.ElapsedEventHandler(this.tmrSendingString_Elapsed);
            // 
            // Service1
            // 
            this.ServiceName = "Service1";
            ((System.ComponentModel.ISupportInitialize)(this.fwImportCybil)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tmrSendingString)).EndInit();

        }

        #endregion

        private System.IO.FileSystemWatcher fwImportCybil;
        private System.Timers.Timer tmrSendingString;
    }
}
