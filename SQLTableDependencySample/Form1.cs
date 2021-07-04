using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using SQLTableDependencySample;
using System.Data.SqlClient;
using TableDependency.SqlClient;
using TableDependency.SqlClient.Base.EventArgs;

namespace SQLTableDependencySample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            using (var tableDep = new SqlTableDependency<Items>(global.cons, "scan_codes"))
            {
                tableDep.OnChanged += TableDependency_Changed;
                tableDep.OnError += TableDep_OnError;
                tableDep.Start();
                richTextBox1.AppendText("Waiting for receiving notification");
            }
        }

        private void TableDep_OnError(object sender, TableDependency.SqlClient.Base.EventArgs.ErrorEventArgs e)
        {
            Exception ex = e.Error;
            throw ex;
        }

        private void TableDep_OnChanged(object sender, TableDependency.SqlClient.Base.EventArgs.RecordChangedEventArgs<Items> e)
        {
            throw new NotImplementedException();
        }

        static void TableDependency_Changed(object sender, RecordChangedEventArgs<Items> e)
        {
            if(e.ChangeType != TableDependency.SqlClient.Base.Enums.ChangeType.None)
            {
                var changeEntiy = e.Entity;
                MessageBox.Show(changeEntiy.Codes);
            }
        }
    }
}
