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

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        static string server = "localhost";
        static string database = "ajinomotoslss_db";
        static string uid = "APCSLSS";
        static string password = "SLSS@inst2018";

        private const int WM_PAINT = 0xF;
        private int buttonWidth = SystemInformation.HorizontalScrollBarArrowWidth;

        public Form1()
        {
            InitializeComponent();
             
            
        try
            {
                //string connString = string.Format("SERVER = {0}; DATABASE = {1}; UID = {2}; PASSWORD = {3}", server, database, uid, password);
                //MySqlConnection conn = new MySqlConnection(connString);
                //MySqlBulkLoader bulkLoader = new MySqlBulkLoader(conn)
                //{
                //    TableName = "salesorder_item",
                //    FieldTerminator = "|",
                //    LineTerminator = "\n",
                //    FileName = @"D:\HT\R_Receive\02_Salesi_A00000000000000920190219.csv"
                //};
                //int count = bulkLoader.Load();
                //conn.Close();
            }catch(MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
          
        }
    }
}
