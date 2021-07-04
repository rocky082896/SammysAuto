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

namespace SQLInjection
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();

                string cons = "SERVER=10.63.3.23; DATABASE=islaboost_db; username=root; password=; port=3306;";
                string sql = "SELECT customer_id, customer_barcode, customer_name " +
                    "FROM tbl_customer WHERE customer_id = @param ORDER BY customer_id DESC";

                MySqlConnection con = new MySqlConnection(cons);
                MySqlCommand cmd;
                MySqlDataAdapter da;
                con.Open();
                cmd = con.CreateCommand();
                cmd.Connection = con;
                cmd.CommandText = sql;
                cmd.Parameters.AddWithValue("@param", textBox1.Text);
                da = new MySqlDataAdapter(cmd);
                da.Fill(dt);
                con.Close();


                dataGridView1.DataSource = dt.DefaultView;
            }catch(MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
